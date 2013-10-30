#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Xml.Serialization;

#endregion

/// <summary>
/// Posts Properties, Events and Methods
/// </summary>
/// 

#region enums

public enum PostVisibleTypes : short
{
    All = -1,
    Hidden = 0,
    Public = 1,
    Custom = 2
}

public enum PostStates : short
{
    Draft = 0,
    Published = 1,
    Removed = 2,
    All = 9
}

public enum PostTypes : short
{
    Article = 0,
    Page = 1,
    AutoSave = 2,
    File = 3
}

public enum FileTypes : short
{
    All = -1,
    File = 0,
    Image = 1,
    Media = 2
}

#endregion

[XmlType("Post")]
public class BSPost
{
    public BSMeta this[String key]
    {
        get { return Metas[key]; }
        set { _metas[key] = value; }
    }

    #region Variables

    private bool _addComment;
    private string _categories;
    private string _code;
    private int _commentCount;
    private string _content;
    private DateTime _date = DateTime.Now;
    private string _link;
    private int _postID;
    private int _readCount;
    private PostVisibleTypes _show;
    private PostStates _state;
    private string _tags;
    private string _title;
    private PostTypes _type = PostTypes.Article;
    private DateTime _updateDate;
    private int _userID;
    private string _userName;
    private int _parentID;
    private BSMetas _metas;
    private string _languageCode = System.Globalization.CultureInfo.InvariantCulture.TwoLetterISOLanguageName;
    private static BSPost _currentPost;

    #endregion

    #region Properties

    public static BSPost CurrentPost
    {
        get { return BSPost._currentPost; }
        set { BSPost._currentPost = value; }
    }

    public int PostID
    {
        get { return _postID; }
        set { _postID = value; }
    }

    public PostStates State
    {
        get { return _state; }
        set { _state = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Content
    {
        get { return _content; }
        set { _content = value; }
    }

    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }

    public bool AddComment
    {
        get { return _addComment; }
        set { _addComment = value; }
    }

    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public DateTime UpdateDate
    {
        get { return _updateDate; }
        set { _updateDate = value; }
    }

    public int ReadCount
    {
        get { return _readCount; }
        set { _readCount = value; }
    }

    public string Link
    {
        get { return _link; }
        set { _link = value; }
    }

    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

    public int UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    public string Categories
    {
        get { return _categories; }
        set { _categories = value; }
    }

    public string Tags
    {
        get { return _tags; }
        set { _tags = value; }
    }

    public int CommentCount
    {
        get { return _commentCount; }
        set { _commentCount = value; }
    }

    public PostTypes Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public PostVisibleTypes Show
    {
        get { return _show; }
        set { _show = value; }
    }

    public string LinkedTitle
    {
        get { return String.Format("<a href=\"{0}\">{1}</a>", Link, Title); }
    }

    public BSMetas Metas
    {
        get { return _metas; }
        set { _metas = value; }
    }

    public int ParentID
    {
        get { return _parentID; }
        set { _parentID = value; }
    }

    public string LanguageCode
    {
        get { return _languageCode; }
        set { _languageCode = value; }
    }

    #endregion

    #region Events

    public static event EventHandler<CancelEventArgs> Showing;

    public static void OnShowing(BSPost BsPost, CancelEventArgs e)
    {
        if (Showing != null)
        {
            Showing(BsPost, e);
        }
    }

    public static event EventHandler<EventArgs> Showed;

    public static void OnShowed(BSPost BsPost, EventArgs e)
    {
        if (Showed != null)
        {
            Showed(BsPost, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSPost BsPost, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(BsPost, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSPost BsPost, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(BsPost, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSPost BsPost, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(BsPost, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSPost BsPost, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(BsPost, e);
        }
    }

    #endregion

    #region Static Methods

    public static BSPost GetPost(int iPostId)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("PostID", iPostId);
            dp.ExecuteReader("SELECT * FROM Posts WHERE [PostID]=@PostID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        return bsPost;
                    }
                }
            }
        }
        return null;
    }

    public static BSPost GetPost(string code)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Code", code);
            dp.ExecuteReader("SELECT * FROM Posts WHERE [Code]=@Code");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        return bsPost;
                    }
                }
            }
        }
        return null;
    }

    public static List<BSPost> GetPosts()
    {
        return GetPostsByColumnValue(null, null, 0, null, PostTypes.Article, PostStates.All);
    }

    public static List<BSPost> GetPostsByColumnValue(string column, object value, int postCount, string orderBy, PostTypes postType, PostStates postState)
    {
        List<BSPost> posts = new List<BSPost>();
        using (DataProcess dp = new DataProcess())
        {
            string top = postCount > 0 ? "TOP " + postCount : String.Empty;

            if (!String.IsNullOrEmpty(column) && value != null)
            {
                dp.AddParameter("Type", (short)postType);
                dp.AddParameter(column, value);
                if (postState == PostStates.All)
                {
                    dp.ExecuteReader(String.Format("SELECT {1} * FROM Posts WHERE [Type]=@Type AND [{0}]=@{0} {2}", column, top, orderBy));
                }
                else
                {
                    dp.AddParameter("State", (short)postState);
                    dp.ExecuteReader(String.Format("SELECT {1} * FROM Posts WHERE [Type]=@Type AND [{0}]=@{0} AND [State]=@State {2}", column, top, orderBy));
                }
            }
            else
            {
                dp.AddParameter("Type", (short)postType);
                if (postState == PostStates.All)
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type {1}", top, orderBy));
                else
                {
                    dp.AddParameter("State", (short)postState);
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type AND [State]=@State {1}", top, orderBy));
                }
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        posts.Add(bsPost);
                    }
                }
            }
        }
        return posts;
    }

    public static List<BSPost> GetPosts(PostTypes postType, PostStates postState, int postCount)
    {
        List<BSPost> posts = new List<BSPost>();
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Type", (short)postType);

            if (postState != PostStates.All)
            {
                dp.AddParameter("State", (short)postState);
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type AND [State]=@State ORDER By [CreateDate] DESC,[Title]"
                    , postCount == 0 ? String.Empty : "TOP " + postCount));
            }
            else
            {
                dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type ORDER By [CreateDate] DESC,[Title]"
                    , postCount == 0 ? String.Empty : "TOP " + postCount));
            }


            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        posts.Add(bsPost);
                    }
                }
            }
        }
        return posts;
    }

    public static List<BSPost> GetPosts(PostTypes postType, PostVisibleTypes postVisibleType, FileTypes fileType, int postCount)
    {
        List<BSPost> posts = new List<BSPost>();
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Type", (short)postType);

            if (postVisibleType != PostVisibleTypes.All)
            {
                if (fileType != FileTypes.All)
                {
                    dp.AddParameter("State", (short)postVisibleType);
                    dp.AddParameter("Show", (short)fileType);
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type AND [State]=@State AND [Show]=@Show ORDER By [CreateDate] DESC,[Title]"
                        , postCount == 0 ? String.Empty : "TOP " + postCount));
                }
                else
                {
                    dp.AddParameter("State", (short)postVisibleType);
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type AND [State]=@State ORDER By [CreateDate] DESC,[Title]"
                        , postCount == 0 ? String.Empty : "TOP " + postCount));
                }
            }
            else
            {
                if (fileType != FileTypes.All)
                {
                    dp.AddParameter("Show", (short)fileType);
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type AND [Show]=@Show ORDER By [CreateDate] DESC,[Title]"
                    , postCount == 0 ? String.Empty : "TOP " + postCount));
                }
                else
                {
                    dp.ExecuteReader(String.Format("SELECT {0} * FROM Posts WHERE [Type]=@Type ORDER By [CreateDate] DESC,[Title]"
                    , postCount == 0 ? String.Empty : "TOP " + postCount));
                }
            }


            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        posts.Add(bsPost);
                    }
                }
            }
        }
        return posts;
    }

    public static List<BSPost> GetPostsByTerm(int termId, string code, TermTypes termType, PostTypes postType, PostStates postState)
    {
        List<BSPost> posts = new List<BSPost>();
        using (DataProcess dp = new DataProcess())
        {
            BSTerm bsTerm = null;

            bsTerm = termId != 0 ? BSTerm.GetTerm(termId) : BSTerm.GetTerm(code, termType);

            if (bsTerm != null)
            {
                dp.AddParameter("TermID", bsTerm.TermID);

                if (postState != PostStates.All)
                {
                    dp.AddParameter("State", (short)postState);
                    dp.ExecuteReader("SELECT * FROM Posts WHERE [PostID] IN (SELECT [ObjectID] FROM TermsTo WHERE [TermID]=@TermID) AND [State]=@State ORDER By [CreateDate] DESC");
                }
                else
                {
                    dp.ExecuteReader("SELECT * FROM Posts WHERE [PostID] IN (SELECT [ObjectID] FROM TermsTo WHERE [TermID]=@TermID) AND [Type]=@Type ORDER By [CreateDate] DESC");
                }
            }
            else
                return posts;

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        posts.Add(bsPost);
                    }
                }
            }
        }
        return posts;
    }

    public static int GetReadCounts(int iPostID)
    {
        using (DataProcess dp = new DataProcess())
        {
            if (iPostID > 0)
            {
                dp.AddParameter("PostID", iPostID);
                dp.ExecuteScalar("SELECT SUM([ReadCount]) FROM Posts WHERE [PostID] = @PostID");
            }
            else
            {
                dp.ExecuteScalar("SELECT SUM([ReadCount]) FROM Posts");
            }
            if (dp.Return.Status == DataProcessState.Success)
            {
                if (dp.Return.Value != DBNull.Value)
                    return Convert.ToInt32(dp.Return.Value);
                else
                    return 0;
            }
        }
        return 0;
    }

    public static void FillPost(IDataReader dr, BSPost bsPost)
    {
        bsPost.Title = dr["Title"].ToString();
        bsPost.PostID = Convert.ToInt32(dr["PostID"]);
        bsPost.Code = dr["Code"].ToString();
        bsPost.Content = dr["Content"].ToString();
        bsPost.State = (PostStates)Convert.ToInt16(dr["State"]);
        bsPost.AddComment = Convert.ToBoolean(dr["AddComment"]);
        bsPost.Categories = bsPost.GetCategoriesHtml();
        bsPost.Tags = bsPost.GetTagsHtml();
        bsPost.CommentCount = bsPost.GetComments(CommentStates.Approved).Count;
        bsPost.ReadCount = Convert.ToInt32(dr["ReadCount"]);
        bsPost.UserID = Convert.ToInt32(dr["UserID"]);
        bsPost.UserName = BSUser.GetUser(bsPost.UserID).UserName;
        bsPost.Date = Convert.ToDateTime(dr["CreateDate"]);
        bsPost.UpdateDate = dr["ModifyDate"] == DBNull.Value
                              ? Convert.ToDateTime(dr["CreateDate"])
                              : Convert.ToDateTime(dr["ModifyDate"]);
        bsPost.Link = BSHelper.GetLink(bsPost);
        bsPost.Type = (PostTypes)Convert.ToInt16(dr["Type"]);
        bsPost.Show = (PostVisibleTypes)Convert.ToInt16(dr["Show"]);
        bsPost.ParentID = Convert.ToInt32(dr["ParentID"]);
        bsPost.LanguageCode = Convert.ToString(dr["LanguageCode"]);

        bsPost.Metas = BSMeta.GetMetas(bsPost.PostID);
    }

    #endregion

    #region Public Methods

    public List<BSComment> GetComments(CommentStates state)
    {
        return BSComment.GetCommentsByPostID(PostID, state);
    }

    public List<BSTerm> GetTags()
    {
        return BSTerm.GetTermsByObjectID(PostID, TermTypes.Tag);
    }

    public List<BSTerm> GetCategories()
    {
        return BSTerm.GetTermsByObjectID(PostID, TermTypes.Category);
    }

    public string GetTagsHtml()
    {
        return GetTagsHtml("<a href=\"{0}\">{1}</a>");
    }

    public string GetTagsHtml(string format)
    {
        return BSTerm.GetTermsByFormat(TermTypes.Tag, PostID, 0, format);
    }

    public string GetTagsWithComma()
    {
        string html = BSTerm.GetTermsByFormat(TermTypes.Tag, PostID, 0, "{1},");
        return html.Substring(0, html.Length - 1);
    }

    public string GetCategoriesHtml()
    {
        List<BSTerm> categories = GetCategories();
        string html = String.Empty;
        if (categories.Count > 0)
        {
            foreach (BSTerm category in categories)
            {
                html += "<a href=\"" + BSHelper.GetPermalink("Category", category.Code, Blogsa.UrlExtension) + "\">"
                + category.Name + "</a> ";
            }
        }
        else
        {
            html = Language.Get["NoCategory"];
        }
        return html;
    }

    public string GetCategoriesWithComma()
    {
        List<BSTerm> categories = GetCategories();
        string html = String.Empty;
        if (categories.Count > 0)
        {
            foreach (BSTerm category in categories)
            {
                html += category.Name + ",";
            }
            html = html.Substring(0, html.Length - 1);
        }
        else
        {
            html = Language.Get["NoCategory"];
        }
        return html;
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("AddComment", AddComment);
            dp.AddParameter("Code", Code);
            dp.AddParameter("Content", Content);
            dp.AddParameter("CreateDate", Date);
            dp.AddParameter("ReadCount", ReadCount);
            dp.AddParameter("Show", Show);
            dp.AddParameter("State", State);
            dp.AddParameter("Title", Title);
            dp.AddParameter("Type", Type);
            dp.AddParameter("UserID", UserID);
            dp.AddParameter("LanguageCode", LanguageCode);

            if (PostID != 0)
            {
                dp.AddParameter("PostID", PostID);

                dp.ExecuteNonQuery("UPDATE Posts SET [AddComment]=@AddComment,[Code]=@Code,[Content]=@Content,[CreateDate]=@CreateDate,"
                    + "[ReadCount]=@ReadCount,[Show]=@Show,[State]=@State,[Title]=@Title,[Type]=@Type,[UserID]=@UserID,[LanguageCode]=@LanguageCode WHERE [PostID] = @PostID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO Posts([AddComment],[Code],[Content],[CreateDate],[ReadCount],[Show],[State],[Title],[Type],[UserID],[LanguageCode]) "
                    + "VALUES(@AddComment,@Code,@Content,@CreateDate,@ReadCount,@Show,@State,@Title,@Type,@UserID,@LanguageCode)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        PostID = Convert.ToInt32(dp.Return.Value);
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
                dp.AddParameter("PostID", PostID);
                dp.ExecuteNonQuery("DELETE FROM Posts WHERE [PostID] = @PostID");
                dp.AddParameter("PostID", PostID);
                dp.ExecuteNonQuery("DELETE FROM Comments WHERE [PostID] = @PostID");
                dp.AddParameter("PostID", PostID);
                dp.ExecuteNonQuery("DELETE FROM TermsTo WHERE [ObjectID] = @PostID");
                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }

    public static List<BSPost> GetPostsByTitle(string title, int iUserId)
    {
        List<BSPost> posts = new List<BSPost>();
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Title", title);
            if (iUserId == 0)
            {
                dp.ExecuteReader("SELECT * FROM [Posts] WHERE [Title] LIKE '%' + @Title + '%' ORDER By [CreateDate] DESC,[Title]");
            }
            else
            {
                dp.AddParameter("UserID", iUserId);
                dp.ExecuteReader("SELECT * FROM [Posts] WHERE [Title] LIKE '%' + @Title + '%' AND [UserID]=@UserID ORDER By [CreateDate] DESC,[Title]");
            }


            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSPost bsPost = new BSPost();
                        FillPost(dr, bsPost);
                        posts.Add(bsPost);
                    }
                }
            }
        }
        return posts;
    }

    #endregion
}