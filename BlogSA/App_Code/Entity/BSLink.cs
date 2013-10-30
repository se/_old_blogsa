using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;

[XmlType("Link")]
public class BSLink
{
    #region Variables
    private int _siteID;
    private int _linkID;
    private string _name;
    private string _description;
    private string _target;
    private string _url;
    private string _languageCode;
    #endregion

    #region Properties
    public int SiteID
    {
        get { return _siteID; }
        set { _siteID = value; }
    }

    public string Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public int LinkID
    {
        get
        {
            return _linkID;
        }
        set
        {
            _linkID = value;
        }
    }

    public string Url
    {
        get
        {
            return _url;
        }
        set
        {
            _url = value;
        }
    }

    public string LanguageCode
    {
        get { return _languageCode; }
        set { _languageCode = value; }
    }
    #endregion

    #region Methods
    public bool Save()
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("SiteID", SiteID);
            dp.AddParameter("Name", Name);
            dp.AddParameter("Description", Description);
            dp.AddParameter("Target", Target);
            dp.AddParameter("Url", Url);
            dp.AddParameter("LanguageCode", LanguageCode);

            if (LinkID != 0)
            {
                dp.AddParameter("LinkID", LinkID);
                dp.ExecuteNonQuery(
                    "UPDATE Links SET [SiteID]=@SiteID,[Name]=@Name,[Description]=@Description,[Target]=@Target,[Url]=@Url,[LanguageCode]=@LanguageCode WHERE [LinkID]=@LinkID");
                return dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Links([SiteID],[Name],[Description],[Target],[Url],[LanguageCode]) VALUES(@SiteID,@Name,@Description,@Target,@Url,@LanguageCode)");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        LinkID = Convert.ToInt32(dp.Return.Value);
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public static BSLink GetLink(int iLinkID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("LinkID", iLinkID);
            dp.ExecuteReader("SELECT * FROM Links WHERE [LinkID]=@LinkID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSLink link = new BSLink();
                        FillLink(dr, link);
                        return link;
                    }
                }
            }
        }
        return null;
    }

    public bool Remove()
    {
        using (DataProcess dp = new DataProcess())
        {
            CancelEventArgs cancelEvent = new CancelEventArgs();
            OnDeleting(this, cancelEvent);
            if (!cancelEvent.Cancel)
            {
                dp.AddParameter("LinkID", LinkID);
                dp.ExecuteNonQuery("DELETE FROM [Links] WHERE [LinkID] = @LinkID");
                dp.AddParameter("ObjectID", LinkID);
                dp.ExecuteNonQuery("DELETE FROM [TermsTo] WHERE [ObjectID] = @ObjectID");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }

    public static List<BSLink> GetLinks()
    {
        List<BSLink> links = new List<BSLink>();
        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteReader("SELECT * FROM [Links]");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSLink link = new BSLink();
                        FillLink(dr, link);
                        links.Add(link);
                    }
                }
            }
        }
        return links;
    }

    public static List<BSLink> GetLinks(int iTermID)
    {
        List<BSLink> links = new List<BSLink>();
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("TermID", iTermID);
            dp.ExecuteReader("SELECT * FROM [Links] WHERE [LinkID] IN (SELECT [ObjectID] FROM [TermsTo] WHERE [TermID]=@TermID)");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSLink link = new BSLink();
                        FillLink(dr, link);
                        links.Add(link);
                    }
                }
            }
        }
        return links;
    }

    public List<BSTerm> GetCategories()
    {
        return BSTerm.GetTermsByObjectID(LinkID, TermTypes.LinkCategory);
    }

    private static void FillLink(IDataReader dr, BSLink link)
    {
        link.LinkID = (int)dr["LinkID"];
        link.Name = (string)dr["Name"];
        link.Url = (string)dr["Url"];
        link.Description = (string)dr["Description"];
        link.Target = (string)dr["Target"];
        link.LanguageCode = (string)dr["LanguageCode"];
    }
    #endregion

    #region Events

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSLink link, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(link, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSLink link, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(link, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSLink link, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(link, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSLink link, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(link, e);
        }
    }

    #endregion
}