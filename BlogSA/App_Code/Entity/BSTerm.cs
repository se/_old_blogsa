using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Data;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Terms
/// </summary>
/// 
/// 

public enum TermTypes : short
{
    Category,
    Tag,
    LinkCategory,
    All
}

[XmlType("Term")]
public class BSTerm
{
    #region Variables
    private int _siteID;
    private int _termID;
    private TermTypes _type;
    private string _name;
    private string _description;
    private string _code;
    private int _subID;
    private List<int> _objects;
    #endregion

    #region Properties
    public int SiteID
    {
        get { return _siteID; }
        set { _siteID = value; }
    }

    public int TermID
    {
        get { return _termID; }
        set { _termID = value; }
    }

    public TermTypes Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }

    public int SubID
    {
        get { return _subID; }
        set { _subID = value; }
    }

    public string Link
    {
        get
        {
            return BSHelper.GetPermalink(Type.ToString(), Code, Blogsa.UrlExtension);
        }
    }

    public List<int> Objects
    {
        get { return _objects; }
        set { _objects = value; }
    }
    #endregion

    #region Events

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSTerm bsTerm, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(bsTerm, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSTerm bsTerm, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(bsTerm, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSTerm bsTerm, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(bsTerm, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSTerm bsTerm, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(bsTerm, e);
        }
    }

    #endregion

    #region Methods

    public BSTerm()
    {
        Objects = new List<int>();
    }

    public static BSTerm GetTerm(int iTermID)
    {
        return GetTerm(iTermID, 0, TermTypes.All);
    }

    public static BSTerm GetTerm(int iTermID, int iSubID, TermTypes type)
    {
        using (DataProcess dp = new DataProcess())
        {
            if (iTermID > 0)
            {
                if (type == TermTypes.All)
                {
                    dp.AddParameter("TermID", iTermID);
                    dp.ExecuteReader("SELECT * FROM Terms WHERE [TermID] = @TermID");
                }
                else
                {
                    dp.AddParameter("TermID", iTermID);
                    dp.AddParameter("Type", type.ToString().ToLowerInvariant());
                    dp.ExecuteReader("SELECT * FROM Terms WHERE [TermID] = @TermID AND [Type]=@Type");
                }
            }
            else
            {
                if (type == TermTypes.All)
                {
                    dp.AddParameter("SubID", iSubID);
                    dp.ExecuteReader("SELECT * FROM Terms WHERE [SubID] = @SubID");
                }
                else
                {
                    dp.AddParameter("SubID", iSubID);
                    dp.AddParameter("Type", type.ToString().ToLowerInvariant());
                    dp.ExecuteReader("SELECT * FROM Terms WHERE [SubID] = @SubID AND [Type]=@Type");
                }
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        return bsTerm;
                    }
                }
            }
        }

        return null;
    }

    public static BSTerm GetTerm(string code, TermTypes type)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Code", code);
            dp.AddParameter("Type", type.ToString().ToLowerInvariant());
            dp.ExecuteReader("SELECT * FROM Terms WHERE [Code] = @Code AND [Type]=@Type");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        return bsTerm;
                    }
                }
            }
        }

        return null;
    }

    public static List<BSTerm> GetTerms()
    {
        return GetTerms(TermTypes.All, 0);
    }

    public static List<BSTerm> GetTerms(TermTypes termType)
    {
        return GetTerms(termType, 0);
    }

    public static List<BSTerm> GetTermsByObjectID(int objectId, TermTypes termType)
    {
        List<BSTerm> terms = new List<BSTerm>();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("ObjectID", objectId);

            if (termType != TermTypes.All)
            {
                dp.AddParameter("Type", termType.ToString().ToLowerInvariant());
                dp.ExecuteReader("SELECT * FROM Terms WHERE [TermID] IN (SELECT TermID FROM TermsTo WHERE [ObjectID]=@ObjectID AND [Type]=@Type) ORDER BY Name");
            }
            else
            {
                dp.ExecuteReader("SELECT * FROM Terms WHERE [TermID] IN (SELECT TermID FROM TermsTo WHERE [ObjectID]=@ObjectID) ORDER BY Name");
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        terms.Add(bsTerm);
                    }
                }
            }
        }

        return terms;
    }

    private static void FillTerm(IDataReader dr, BSTerm bsTerm)
    {
        bsTerm.SubID = (int)dr["SubID"];
        bsTerm.Name = (string)dr["Name"];
        bsTerm.Code = (string)dr["Code"];
        bsTerm.Description = dr["Description"].ToString();

        string termType = (string)dr["Type"];

        bsTerm.Type = GetTermType(termType);
        bsTerm.TermID = (int)dr["TermID"];

        bsTerm.Objects = new List<int>();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("TermID", bsTerm.TermID);
            dp.ExecuteReader("SELECT * FROM TermsTo WHERE [TermID]=@TermID");
            if (dp.Return.Status == DataProcessState.Success)
                using (IDataReader drObjects = (IDataReader)dp.Return.Value)
                    while (drObjects.Read())
                        bsTerm.Objects.Add((int)drObjects["ObjectID"]);
        }
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("SiteID", SiteID);
            dp.AddParameter("Name", Name);
            dp.AddParameter("Description", Description);
            dp.AddParameter("Code", Code);
            dp.AddParameter("Type", Type.ToString().ToLowerInvariant());
            dp.AddParameter("SubID", SubID);

            if (TermID != 0)
            {
                dp.AddParameter("TermID", TermID);

                dp.ExecuteNonQuery("UPDATE Terms SET [SiteID]=@SiteID,[Name]=@Name,[Description]=@Description,[Code]=@Code,[Type]=@Type,"
                    + "[SubID]=@SubID WHERE [TermID] = @TermID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    AddObjects();
                }
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Terms([SiteID],[Name],[Description],[Code],[Type],[SubID]) "
                    + "VALUES(@SiteID,@Name,@Description,@Code,@Type,@SubID)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        TermID = Convert.ToInt32(dp.Return.Value);

                        AddObjects();
                    }
                }
            }
        }
        return bReturnValue;
    }

    private void AddObjects()
    {
        using (DataProcess dp = new DataProcess())
        {
            string type = Type.ToString().ToLowerInvariant();
            foreach (int objectID in Objects)
            {
                dp.AddParameter("ObjectID", objectID);
                dp.AddParameter("TermID", TermID);
                dp.AddParameter("Type", type);
                dp.ExecuteNonQuery("INSERT INTO TermsTo([ObjectID],[TermID],[Type]) VALUES(@ObjectID,@TermID,@Type)");
            }
        }
    }

    public static List<BSTerm> GetTerms(TermTypes termType, int iTermCount)
    {
        List<BSTerm> terms = new List<BSTerm>();

        using (DataProcess dp = new DataProcess())
        {
            string top = iTermCount == 0 ? String.Empty : "TOP " + iTermCount;

            if (termType != TermTypes.All)
            {
                dp.AddParameter("Type", termType.ToString().ToLowerInvariant());
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Terms WHERE [Type]=@Type ORDER BY [Name] ASC", top));
            }
            else
            {
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Terms ORDER BY [Name] ASC", top));
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        terms.Add(bsTerm);
                    }
                }
            }
        }

        return terms;
    }

    public static string GetTermsByFormat(TermTypes termType, int objectId, int count, string format)
    {
        List<BSTerm> terms = objectId != 0 ? GetTermsByObjectID(objectId, termType) : GetTerms(termType, count);

        string html = String.Empty;
        if (terms.Count > 0)
        {
            foreach (BSTerm term in terms)
            {
                if (format.Contains("{2}"))
                    html += String.Format(format, BSHelper.GetPermalink("Tag", term.Code, Blogsa.UrlExtension), term.Name, term.Objects.Count);
                else
                    html += String.Format(format, BSHelper.GetPermalink("Tag", term.Code, Blogsa.UrlExtension), term.Name);
            }
        }
        else
        {
            html = Language.Get["NoTag"];
        }
        return html;
    }

    public static List<BSTerm> GetTermsByContainsName(string name, TermTypes termType, int iTermCount)
    {
        List<BSTerm> terms = new List<BSTerm>();

        using (DataProcess dp = new DataProcess())
        {
            string top = iTermCount == 0 ? String.Empty : "TOP " + iTermCount;

            if (termType != TermTypes.All)
            {
                string type = termType == TermTypes.Category ? "category" : "tag";

                dp.AddParameter("Type", type);
                dp.AddParameter("Name", name);
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Terms WHERE [Type]=@Type AND [Name] LIKE '%' + @Name + '%' ORDER BY [Name] ASC", top));
            }
            else
            {
                dp.AddParameter("Name", name);
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Terms WHERE [Name] LIKE '%' + @Name + '%' ORDER BY [Name] ASC", top));
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        terms.Add(bsTerm);
                    }
                }
            }
        }

        return terms;
    }

    public static List<BSTerm> GetTermsBySubID(TermTypes termType, int iSubID)
    {
        List<BSTerm> terms = new List<BSTerm>();

        using (DataProcess dp = new DataProcess())
        {
            if (termType != TermTypes.All)
            {
                dp.AddParameter("Type", termType.ToString().ToLowerInvariant());
                dp.AddParameter("SubID", iSubID);
                dp.ExecuteReader("SELECT * FROM Terms WHERE [Type]=@Type AND [SubID]=@SubID ORDER BY [Name] ASC");
            }
            else
            {
                dp.AddParameter("SubID", iSubID);
                dp.ExecuteReader("SELECT * FROM Terms WHERE [SubID]=@SubID ORDER BY [Name] ASC");
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSTerm bsTerm = new BSTerm();
                        FillTerm(dr, bsTerm);
                        terms.Add(bsTerm);
                    }
                }
            }
        }

        return terms;
    }

    public bool Remove()
    {
        using (DataProcess dp = new DataProcess())
        {
            CancelEventArgs cancelEvent = new CancelEventArgs();
            OnDeleting(this, cancelEvent);
            if (!cancelEvent.Cancel)
            {
                dp.AddParameter("TermID", TermID);
                dp.ExecuteNonQuery("DELETE FROM Terms WHERE [TermID] = @TermID");
                dp.AddParameter("TermID", TermID);
                dp.ExecuteNonQuery("DELETE FROM TermsTo WHERE [TermID] = @TermID");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }

    public static bool RemoveTo(TermTypes termType, int iObjectID)
    {
        using (DataProcess dp = new DataProcess())
        {
            if (termType == TermTypes.All)
            {
                dp.AddParameter("ObjectID", iObjectID);
                dp.ExecuteNonQuery("DELETE FROM TermsTo WHERE [ObjectID]=@ObjectID");

            }
            else
            {
                dp.AddParameter("Type", termType == TermTypes.Category ? "category" : "tag");
                dp.AddParameter("ObjectID", iObjectID);
                dp.ExecuteNonQuery("DELETE FROM TermsTo WHERE [Type]=@Type AND [ObjectID]=@ObjectID");
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                return true;
            }
        }
        return false;
    }

    public static TermTypes GetTermType(string termType)
    {
        TermTypes type = TermTypes.All;

        switch (termType)
        {
            case "tag":
                type = TermTypes.Tag;
                break;
            case "category":
                type = TermTypes.Category;
                break;
            case "linkcategory":
                type = TermTypes.LinkCategory;
                break;
        }

        return type;
    }

    #endregion
}
