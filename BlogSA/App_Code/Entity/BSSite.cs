using System;
using System.ComponentModel;
using System.Xml.Serialization;

public enum SiteStates : short
{
    Passive = 0,
    Active = 1,
    Closed = 2,
    Removed = -1
}

[XmlType("Site")]
public class BSSite
{
    #region Variables
    private int siteID;
    private int parentID;
    private int userID;
    private string code;
    private SiteStates state;
    private DateTime createDate;
    #endregion

    #region Properties
    public int SiteID
    {
        get { return siteID; }
        set { siteID = value; }
    }

    public int ParentID
    {
        get { return parentID; }
        set { parentID = value; }
    }

    public int UserID
    {
        get { return userID; }
        set { userID = value; }
    }

    public string Code
    {
        get { return code; }
        set { code = value; }
    }

    public SiteStates State
    {
        get { return state; }
        set { state = value; }
    }

    public DateTime CreateDate
    {
        get { return createDate; }
        set { createDate = value; }
    }
    #endregion

    #region Events

    public static event EventHandler<CancelEventArgs> Showing;

    public static void OnShowing(BSSite bsSite, CancelEventArgs e)
    {
        if (Showing != null)
        {
            Showing(bsSite, e);
        }
    }

    public static event EventHandler<EventArgs> Showed;

    public static void OnShowed(BSSite bsSite, EventArgs e)
    {
        if (Showed != null)
        {
            Showed(bsSite, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSSite bsSite, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(bsSite, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSSite bsSite, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(bsSite, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSSite bsSite, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(bsSite, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSSite bsSite, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(bsSite, e);
        }
    }

    #endregion

    #region Methods
    public bool Save()
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("ParentID", this.ParentID);
            dp.AddParameter("UserID", this.UserID);
            dp.AddParameter("Code", this.Code);
            dp.AddParameter("State", this.State);

            string sql = "INSERT INTO Sites([ParentID],[UserID],[Code],[State]) "
                      + "VALUES(@ParentID,@UserID,@Code,@State);";

            if (SiteID != 0)
            {
                sql =
                    "UPDATE Settings SET [ParentID]=@ParentID,[UserID]=@UserID,[Code]=@Code,[State]=@State WHERE [SiteID] = @SiteID;";
                dp.AddParameter("SiteID", this.SiteID);
            }

            dp.ExecuteNonQuery(sql);

            return dp.Return.Status == DataProcessState.Success;
        }
    }
    #endregion
}