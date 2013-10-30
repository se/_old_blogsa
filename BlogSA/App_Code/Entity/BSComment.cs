using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;

public enum CommentStates : short
{
    Approved = 0,
    UnApproved = 1,
    All = 9
}

[XmlType("Comment")]
public class BSComment
{
    #region Properties
    private int _CommentID;
    public int CommentID
    {
        get { return _CommentID; }
        set { _CommentID = value; }
    }
    private string _UserName;
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    private string _Content;
    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }
    private string _Email;
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }
    private DateTime _Date;
    public DateTime Date
    {
        get { return _Date; }
        set { _Date = value; }
    }
    private string _IP;
    public string IP
    {
        get { return _IP; }
        set { _IP = value; }
    }
    private string _WebPage;
    public string WebPage
    {
        get { return _WebPage; }
        set { _WebPage = value; }
    }
    private int _PostID;
    public int PostID
    {
        get { return _PostID; }
        set { _PostID = value; }
    }
    private int _UserID;
    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    private string _GravatarLink;
    public string GravatarLink
    {
        get { return _GravatarLink; }
        set { _GravatarLink = value; }
    }
    private bool _Approve;
    public bool Approve
    {
        get { return _Approve; }
        set { _Approve = value; }
    }
    public bool NotifyMe
    {
        get
        {
            return _Approve;
        }
        set
        {
            _Approve = value;
        }
    }

    private bool _isAdmin;

    public bool IsAdmin
    {
        get { return _isAdmin; }
        set { _isAdmin = value; }
    }

    public string Link
    {
        get
        {
            BSPost post = BSPost.GetPost(PostID);
            if (post != null)
                return String.Format("{0}#{1}", post.Link, CommentID);
            else
                return String.Empty;
        }
    }

    #endregion

    #region Events
    public static event EventHandler<CancelEventArgs> Showing;
    public static void OnShowing(BSComment BsComment, CancelEventArgs e)
    {
        if (Showing != null)
        {
            Showing(BsComment, e);
        }
    }

    public static event EventHandler<EventArgs> Showed;
    public static void OnShowed(BSComment BsComment, EventArgs e)
    {
        if (Showed != null)
        {
            Showed(BsComment, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Adding;
    public static void OnAdding(BSComment BsComment, CancelEventArgs e)
    {
        if (Adding != null)
        {
            Adding(BsComment, e);
        }
    }

    public static event EventHandler<EventArgs> Added;
    public static void OnAdded(BSComment BsComment, EventArgs e)
    {
        if (Added != null)
        {
            Added(BsComment, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Approving;
    public static void OnApproving(BSComment BsComment, CancelEventArgs e)
    {
        if (Approving != null)
        {
            Approving(BsComment, e);
        }
    }

    public static event EventHandler<EventArgs> Approved;
    public static void OnApproved(BSComment BsComment, EventArgs e)
    {
        if (Approved != null)
        {
            Approved(BsComment, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Saving;
    public static void OnSaving(BSComment BsComment, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(BsComment, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;
    public static void OnSaved(BSComment BsComment, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(BsComment, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;
    public static void OnDeleting(BSComment BsComment, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(BsComment, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;
    public static void OnDeleted(BSComment BsComment, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(BsComment, e);
        }
    }
    #endregion

    #region Methods
    public static BSComment GetComment(int iCommentID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("CommentID", iCommentID);
            dp.ExecuteReader("SELECT * FROM Comments WHERE [CommentID]=@CommentID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSComment bsComment = new BSComment();
                        FillComment(dr, bsComment);
                        return bsComment;
                    }
                }
            }
        }
        return null;
    }

    public static List<BSComment> GetComments(CommentStates state)
    {
        return GetComments(state, 0);
    }

    public static List<BSComment> GetComments(CommentStates state, int iCommentCount)
    {
        List<BSComment> comments = new List<BSComment>();
        using (DataProcess dp = new DataProcess())
        {
            string top = iCommentCount == 0 ? String.Empty : "TOP " + iCommentCount;

            if (state == CommentStates.All)
            {
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Comments ORDER By CreateDate DESC", top));
            }
            else
            {
                dp.AddParameter("Approve", state == CommentStates.Approved);
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Comments WHERE [Approve]=@Approve ORDER By CreateDate DESC", top));
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSComment bsComment = new BSComment();
                        FillComment(dr, bsComment);
                        comments.Add(bsComment);
                    }
                }
            }
        }
        return comments;
    }

    public static List<BSComment> GetCommentsByPostID(int iPostID, CommentStates state)
    {
        List<BSComment> comments = new List<BSComment>();
        using (DataProcess dp = new DataProcess())
        {
            if (state == CommentStates.All)
            {
                dp.AddParameter("PostID", iPostID);
                dp.ExecuteReader("SELECT * FROM Comments WHERE [PostID]=@PostID ORDER By CreateDate DESC");
            }
            else
            {
                dp.AddParameter("PostID", iPostID);
                dp.AddParameter("Approve", state == CommentStates.Approved);
                dp.ExecuteReader("SELECT * FROM Comments WHERE [PostID]=@PostID AND [Approve]=@Approve ORDER By CreateDate DESC");
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSComment bsComment = new BSComment();
                        FillComment(dr, bsComment);
                        comments.Add(bsComment);
                    }
                }
            }
        }
        return comments;
    }

    public static List<BSComment> GetCommentsByUserID(int iUserID, CommentStates state)
    {
        List<BSComment> comments = new List<BSComment>();
        using (DataProcess dp = new DataProcess())
        {
            if (state == CommentStates.All)
            {
                dp.AddParameter("UserID", iUserID);
                dp.ExecuteReader("SELECT * FROM Comments WHERE [UserID]=@UserID ORDER By CreateDate DESC");
            }
            else
            {
                dp.AddParameter("UserID", iUserID);
                dp.AddParameter("Approve", state == CommentStates.Approved);
                dp.ExecuteReader("SELECT * FROM Comments WHERE [UserID]=@UserID AND [Approve]=@Approve ORDER By CreateDate DESC");
            }

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSComment bsComment = new BSComment();
                        FillComment(dr, bsComment);
                        comments.Add(bsComment);
                    }
                }
            }
        }
        return comments;
    }

    public static void Delete(int iCommentID)
    {
        CancelEventArgs eventArgs = new CancelEventArgs();
        BSComment bsComment = GetComment(iCommentID);
        BSComment.OnDeleting(bsComment, eventArgs);

        if (!eventArgs.Cancel)
        {
            using (DataProcess dp = new DataProcess())
            {
                dp.AddParameter("CommentID", iCommentID);
                dp.ExecuteReader("DELETE FROM Comments WHERE [CommentID]=@CommentID");
                if (dp.Return.Status == DataProcessState.Success)
                {
                    BSComment.OnDeleted(bsComment, EventArgs.Empty);
                }
            }
        }
    }

    static void FillComment(IDataReader dr, BSComment bsComment)
    {
        bsComment.CommentID = Convert.ToInt32(dr["CommentID"]);
        bsComment.Content = dr["Comment"].ToString();
        bsComment.Date = Convert.ToDateTime(dr["CreateDate"]);
        bsComment.Email = dr["EMail"].ToString();
        bsComment.GravatarLink = BSHelper.GetGravatar(bsComment.Email);
        bsComment.IP = dr["IP"].ToString();
        bsComment.PostID = Convert.ToInt32(dr["PostID"]);
        bsComment.UserID = Convert.ToInt32(dr["UserID"]);
        bsComment.UserName = dr["Name"].ToString();
        bsComment.WebPage = dr["WebPage"].ToString();
        bsComment.Approve = Convert.ToBoolean(dr["Approve"]);

        if (bsComment.UserID != 0)
        {
            BSUser user = BSUser.GetUser(bsComment.UserID);
            if (user != null)
            {
                bsComment.UserName = user.Name;
                bsComment.WebPage = user.WebPage;
                bsComment.Email = user.Email;
                bsComment.IsAdmin = user.Role.Equals("admin");
            }
        }
    }

    public bool DoApprove(bool bApprove)
    {
        CancelEventArgs eventArgs = new CancelEventArgs();
        BSComment bsComment = GetComment(CommentID);
        OnApproving(bsComment, eventArgs);
        if (!eventArgs.Cancel)
        {
            using (DataProcess dp = new DataProcess())
            {
                dp.AddParameter("Approve", bApprove);
                dp.AddParameter("CommentID", CommentID);
                dp.ExecuteNonQuery("UPDATE Comments SET [Approve]=@Approve WHERE [CommentID]=@CommentID");
                if (dp.Return.Status == DataProcessState.Success)
                {
                    bsComment.Approve = bApprove;
                    OnApproved(bsComment, null);
                    return true;
                }
            }
        }
        return false;
    }

    public bool Remove()
    {
        using (DataProcess dp = new DataProcess())
        {
            CancelEventArgs cancelEvent = new CancelEventArgs();
            OnDeleting(this, cancelEvent);
            if (!cancelEvent.Cancel)
            {
                dp.AddParameter("CommentID", CommentID);
                dp.ExecuteNonQuery("DELETE FROM Comments WHERE [CommentID] = @CommentID");
                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("UserID", UserID);
            dp.AddParameter("PostID", PostID);
            dp.AddParameter("Name", UserName);
            dp.AddParameter("Comment", Content);
            dp.AddParameter("Email", Email);
            dp.AddParameter("WebPage", WebPage);
            dp.AddParameter("Ip", IP);
            dp.AddParameter("CreateDate", Date);
            dp.AddParameter("Approve", Approve);
            dp.AddParameter("NotifyMe", NotifyMe);

            if (CommentID != 0)
            {
                dp.AddParameter("CommentID", CommentID);

                dp.ExecuteNonQuery("UPDATE Comments SET [UserID]=@UserID,[PostID]=@PostID,[Name]=@Name,[Comment]=@Comment,"
                    + "[Email]=@Email,[WebPage]=@WebPage,[Ip]=@Ip,[CreateDate]=@CreateDate,[Approve]=@Approve,[NotifyMe]=@NotifyMe WHERE [CommentID] = @CommentID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Comments([UserID],[PostID],[Name],[Comment],[Email],[WebPage],[Ip],[CreateDate],[Approve],[NotifyMe]) "
                    + "VALUES(@UserID,@PostID,@Name,@Comment,@Email,@WebPage,@Ip,@CreateDate,@Approve,@NotifyMe)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        CommentID = Convert.ToInt32(dp.Return.Value);
                    }
                }
            }
        }
        return bReturnValue;
    }
    #endregion
}