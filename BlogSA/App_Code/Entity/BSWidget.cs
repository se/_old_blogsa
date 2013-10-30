using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Data;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Widget
/// </summary>
/// 
/// 

public enum WidgetTypes
{
    Normal = 0,
    Page = 1,
    Admin = 2
}

[XmlType("Widget")]
public class BSWidget
{
    #region Properties
    private int _SiteID;

    public int SiteID
    {
        get { return _SiteID; }
        set { _SiteID = value; }
    }

    private string _LanguageCode;

    public string LanguageCode
    {
        get { return _LanguageCode; }
        set { _LanguageCode = value; }
    }

    private int _WidgetID;

    public int WidgetID
    {
        get { return _WidgetID; }
        set { _WidgetID = value; }
    }
    private string _Title;

    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }
    private string _Description;

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
    private string _FolderName;

    public string FolderName
    {
        get { return _FolderName; }
        set { _FolderName = value; }
    }
    private WidgetTypes _Type;

    public WidgetTypes Type
    {
        get { return _Type; }
        set { _Type = value; }
    }
    private short _Sort;

    public short Sort
    {
        get { return _Sort; }
        set { _Sort = value; }
    }
    private bool _Visible;

    public bool Visible
    {
        get { return _Visible; }
        set { _Visible = value; }
    }
    private string _PlaceHolder;
    private string _name;
    private string _path;

    public string PlaceHolder
    {
        get { return _PlaceHolder; }
        set { _PlaceHolder = value; }
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

    public String Path
    {
        get
        {
            return _path;
        }
        set
        {
            _path = value;
        }
    }

    #endregion

    #region  Methods
    public static BSWidget GetWidget(int iWidgetID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("WidgetID", iWidgetID);
            dp.ExecuteReader("SELECT * FROM Widgets WHERE [WidgetID]=@WidgetID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        return bsWidget;
                    }
                }
            }
        }
        return null;
    }

    public static BSWidget GetWidget(string folderName)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("FolderName", folderName);
            dp.ExecuteReader("SELECT * FROM Widgets WHERE [FolderName]=@FolderName");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        return bsWidget;
                    }
                }
            }
        }
        return null;
    }

    public static List<BSWidget> GetWidgets()
    {
        List<BSWidget> widgets = new List<BSWidget>();

        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteReader("SELECT * FROM [Widgets] ORDER BY [Sort]");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        widgets.Add(bsWidget);
                    }
                }
            }
        }

        return widgets;
    }

    public static List<BSWidget> GetWidgets(bool visible)
    {
        List<BSWidget> widgets = new List<BSWidget>();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Visible", visible);
            dp.ExecuteReader("SELECT * FROM [Widgets] WHERE [Visible]=@Visible ORDER BY [Sort]");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        widgets.Add(bsWidget);
                    }
                }
            }
        }

        return widgets;
    }

    public static List<BSWidget> GetWidgetsByPlaceHolder(string placeHolder, bool visible)
    {
        List<BSWidget> widgets = new List<BSWidget>();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("PlaceHolder", placeHolder);
            dp.AddParameter("Visible", visible);
            dp.ExecuteReader("SELECT * FROM [Widgets] WHERE [PlaceHolder]=@PlaceHolder AND [Visible]=@Visible ORDER BY [Sort]");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        widgets.Add(bsWidget);
                    }
                }
            }
        }

        return widgets;
    }

    public static List<BSWidget> GetWidgetsByColumnValue(string column, object value, int widgetCount, string orderBy)
    {
        List<BSWidget> widgets = new List<BSWidget>();
        using (DataProcess dp = new DataProcess())
        {
            string top = widgetCount > 0 ? "TOP " + widgetCount : String.Empty;

            if (!String.IsNullOrEmpty(column) && value != null)
            {
                dp.AddParameter(column, value);
                dp.ExecuteReader(String.Format("SELECT {0} * FROM [Widgets] WHERE [{1}]=@{1} {2}", top, column, orderBy));
            }
            else
            {
                dp.ExecuteReader(String.Format("SELECT {0} * FROM [Widgets] {1}", top, orderBy));
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();
                        FillWidget(dr, bsWidget);
                        widgets.Add(bsWidget);
                    }
                }
            }
        }
        return widgets;
    }

    public static List<BSWidget> GetWidgetsBySorted()
    {
        List<BSWidget> widgets = new List<BSWidget>();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Visible", true);
            dp.ExecuteReader("SELECT * FROM Widgets WHERE [Visible] = @Visible ORDER BY [Sort]");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr.Read())
                    {
                        BSWidget bsWidget = new BSWidget();

                        FillWidget(dr, bsWidget);

                        widgets.Add(bsWidget);
                    }
                }
            }
        }

        return widgets;
    }

    private static void FillWidget(IDataReader dr, BSWidget bsWidget)
    {
        bsWidget.WidgetID = Convert.ToInt32(dr["WidgetID"]);
        bsWidget.PlaceHolder = dr["PlaceHolder"].ToString();
        bsWidget.FolderName = dr["FolderName"].ToString();
        bsWidget.Title = dr["Title"].ToString();
        bsWidget.Description = dr["Description"].ToString();
        bsWidget.Type = (WidgetTypes)(short)dr["Type"];
        bsWidget.Sort = Convert.ToInt16(dr["Sort"]);
        bsWidget.Visible = Convert.ToBoolean(dr["Visible"]);
        bsWidget.SiteID = Convert.ToInt32(dr["SiteID"]);
        bsWidget.LanguageCode = Convert.ToString(dr["LanguageCode"]);
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("SiteID", this.SiteID);
            dp.AddParameter("Description", this.Description);
            dp.AddParameter("FolderName", this.FolderName);
            dp.AddParameter("PlaceHolder", this.PlaceHolder);
            dp.AddParameter("Sort", this.Sort);
            dp.AddParameter("Title", this.Title);
            dp.AddParameter("Visible", this.Visible);
            dp.AddParameter("Type", this.Type);
            dp.AddParameter("LanguageCode", this.LanguageCode);

            if (WidgetID != 0)
            {
                dp.AddParameter("WidgetID", WidgetID);

                dp.ExecuteNonQuery("UPDATE Widgets SET [SiteID]=@SiteID,[Description]=@Description,[FolderName]=@FolderName,[PlaceHolder]=@PlaceHolder,[Sort]=@Sort,"
                    + "[Title]=@Title,[Visible]=@Visible,[Type]=@Type,[LanguageCode]=@LanguageCode WHERE [WidgetID] = @WidgetID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Widgets([SiteID],[Description],[FolderName],[PlaceHolder],[Sort],[Title],[Visible],[Type],[LanguageCode]) "
                    + "VALUES(@SiteID,@Description,@FolderName,@PlaceHolder,@Sort,@Title,@Visible,@Type,@LanguageCode)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        WidgetID = Convert.ToInt32(dp.Return.Value);
                    }
                }
            }
        }
        return bReturnValue;
    }

    public bool Remove()
    {
        using (DataProcess dp = new DataProcess())
        {
            CancelEventArgs cancelEvent = new CancelEventArgs();
            OnDeleting(this, cancelEvent);
            if (!cancelEvent.Cancel)
            {
                dp.AddParameter("WidgetID", WidgetID);
                dp.ExecuteNonQuery("DELETE FROM Widgets WHERE [WidgetID] = @WidgetID");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Events

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSWidget bsWidget, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(bsWidget, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSWidget bsWidget, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(bsWidget, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSWidget bsWidget, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(bsWidget, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSWidget bsWidget, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(bsWidget, e);
        }
    }

    #endregion
}
