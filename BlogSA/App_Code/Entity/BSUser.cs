using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Xml.Serialization;

[XmlType("User")]
public class BSUser
{
    #region Variables
    private int _SiteID;
    private int _UserID;
    private string _UserName;
    private string _Password;
    private short _State;
    private string _Name;
    private string _Email;
    private string _WebPage;
    private DateTime? _LastLoginDate;
    private string _Role;
    private DateTime _CreateDate;
    #endregion

    #region Properties
    public int SiteID
    {
        get { return _SiteID; }
        set { _SiteID = value; }
    }

    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }

    public string Password
    {
        get { return _Password; }
        set { _Password = value; }
    }
    
    public short State
    {
        get { return _State; }
        set { _State = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    public string WebPage
    {
        get { return _WebPage; }
        set { _WebPage = value; }
    }

    public DateTime? LastLoginDate
    {
        get { return _LastLoginDate; }
        set { _LastLoginDate = value; }
    }

    public string Role
    {
        get { return _Role; }
        set { _Role = value; }
    }


    public DateTime CreateDate
    {
        get { return _CreateDate; }
        set { _CreateDate = value; }
    }
    #endregion

    #region Constructor
    public BSUser()
    {
        CreateDate = DateTime.Now;
    }
    #endregion

    #region Methods
    public static bool ValidateUser(string userName, string password)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("UserName", userName);
            dp.AddParameter("Password", password);
            dp.ExecuteReader("SELECT * FROM Users WHERE [UserName]=@UserName AND [Password]=@Password");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    return dr != null && dr.Read();
                }
            }
        }

        return false;
    }

    public static bool ValidateEmail(string email)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Email", email);
            dp.ExecuteReader("SELECT * FROM Users WHERE [Email]=@Email");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    return dr != null && dr.Read();
                }
            }
        }

        return false;
    }

    public static List<BSUser> GetUsers()
    {
        List<BSUser> users = new List<BSUser>();
        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteReader("SELECT * FROM Users");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSUser user = new BSUser();
                        FillUser(dr, user);
                        users.Add(user);
                    }
                }
            }
        }
        return users;
    }

    public static BSUser GetUser(int iUserID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("UserID", iUserID);
            dp.ExecuteReader("SELECT * FROM Users WHERE [UserID]=@UserID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSUser user = new BSUser();
                        FillUser(dr, user);
                        return user;
                    }
                }
            }
        }
        return null;
    }

    public static BSUser GetUserByUserName(string userName)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("UserName", userName);
            dp.ExecuteReader("SELECT * FROM Users WHERE [UserName]=@UserName");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSUser user = new BSUser();
                        FillUser(dr, user);
                        return user;
                    }
                }
            }
        }
        return null;
    }

    public static BSUser GetUser(string userName, string password)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("UserName", userName);
            dp.AddParameter("Password", password);
            dp.ExecuteReader("SELECT * FROM Users WHERE [UserName]=@UserName AND [Password]=@Password");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSUser user = new BSUser();
                        FillUser(dr, user);
                        return user;
                    }
                }
            }
        }
        return null;
    }

    public static BSUser GetUserByEmail(string strEmail)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Email", strEmail);
            dp.ExecuteReader("SELECT * FROM Users WHERE [Email]=@Email");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSUser user = new BSUser();
                        FillUser(dr, user);
                        return user;
                    }
                }
            }
        }
        return null;
    }

    public List<BSPost> GetPosts()
    {
        return BSPost.GetPostsByColumnValue("UserID", UserID, 0, "ORDER By CreateDate DESC", PostTypes.Article, PostStates.All);
    }

    public List<BSPost> GetPosts(PostTypes postType)
    {
        return BSPost.GetPostsByColumnValue("UserID", UserID, 0, "ORDER By CreateDate DESC", postType, PostStates.All);
    }

    private static void FillUser(IDataReader dr, BSUser user)
    {
        user.UserID = Convert.ToInt32(dr["UserID"]);
        user.Email = dr["Email"].ToString();
        user.LastLoginDate = dr["LastLoginDate"] == DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : Convert.ToDateTime(dr["LastLoginDate"]);
        user.WebPage = dr["WebPage"].ToString();
        user.Name = dr["Name"].ToString();
        user.UserName = dr["UserName"].ToString();
        user.Role = dr["Role"].ToString();
        user.State = Convert.ToInt16(dr["State"]);
        user.Password = dr["Password"].ToString();
        user.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("SiteID", SiteID);
            dp.AddParameter("UserName", UserName);
            dp.AddParameter("Password", Password);
            dp.AddParameter("Name", Name);
            dp.AddParameter("Email", Email);
            dp.AddParameter("WebPage", WebPage);
            dp.AddParameter("CreateDate", CreateDate);
            dp.AddParameter("LastLoginDate", LastLoginDate);
            dp.AddParameter("State", State);
            dp.AddParameter("Role", Role);

            if (UserID != 0)
            {
                dp.AddParameter("UserID", UserID);

                dp.ExecuteNonQuery("UPDATE Users SET [SiteID]=@SiteID,[UserName]=@UserName,[Password]=@Password,[Name]=@Name,"
                    + "[Email]=@Email,[WebPage]=@WebPage,[CreateDate]=@CreateDate,[LastLoginDate]=@LastLoginDate,[State]=@State,[Role]=@Role WHERE [UserID] = @UserID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Users([SiteID],[UserName],[Password],[Name],[Email],[WebPage],[CreateDate],[LastLoginDate],[State],[Role]) "
                    + "VALUES(@SiteID,@UserName,@Password,@Name,@Email,@WebPage,@CreateDate,@LastLoginDate,@State,@Role)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        UserID = Convert.ToInt32(dp.Return.Value);
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
                dp.AddParameter("UserID", UserID);
                dp.ExecuteNonQuery("DELETE FROM Users WHERE [UserID] = @UserID");
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

    public static void OnSaving(BSUser user, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(user, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSUser user, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(user, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSUser user, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(user, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSUser user, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(user, e);
        }
    }
    #endregion
}