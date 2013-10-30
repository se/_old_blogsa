using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_Posts : System.Web.UI.UserControl
{
    private Boolean PostDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        string tag = Request["Tag"];
        string postID = Request["PostID"];
        string postCode = Request["Code"];
        string category = Request["Category"];
        string fileID = Request["FileID"];
        string langCode = Request["Language"];

        if (!string.IsNullOrEmpty(fileID))
            postID = fileID;

        int iPostID = 0;
        int.TryParse(postID, out iPostID);

        BSPost bsPost = null;
        if (iPostID != 0)
        {
            bsPost = BSPost.GetPost(iPostID);
        }
        else if (!string.IsNullOrEmpty(postCode))
        {
            bsPost = BSPost.GetPost(postCode);
        }

        if (bsPost != null)
        {
            if (bsPost.State == PostStates.Published && (bsPost.Type != PostTypes.AutoSave))
            {
                PostDetail = true;
                bsPost.ReadCount++;
                bsPost.Save();

                this.Page.Title = Blogsa.Title + " - " + bsPost.Title;
                BSHelper.AddHeader(this.Page, "keywords", bsPost.GetTagsWithComma());
                System.Web.UI.HtmlControls.HtmlGenericControl gc = new System.Web.UI.HtmlControls.HtmlGenericControl();
                gc.InnerHtml = bsPost.Content;

                BSHelper.AddHeader(this.Page, "description", gc.InnerText.Length > 160 ? gc.InnerText.Substring(0, 160) : gc.InnerText);
                BSHelper.AddHeader(this.Page, "robots", "index,follow");

                List<BSPost> posts = new List<BSPost>();
                posts.Add(bsPost);

                rpPosts.DataSource = posts;
                rpPosts.DataBind();
            }
        }
        else if (tag != null)
        {
            tag = BSHelper.CreateCode(tag);

            List<BSPost> posts = BSPost.GetPostsByTerm(0, tag, TermTypes.Tag, PostTypes.Article, PostStates.Published);
            ObjectPager pager = new ObjectPager();
            int pageCount = pager.PageCount(posts, 10);

            rpPosts.DataSource = pager.GetPage(posts, 0, 10);
            rpPosts.DataBind();
        }
        else if (category != null)
        {
            category = BSHelper.CreateCode(category);

            List<BSPost> posts = BSPost.GetPostsByTerm(0, category, TermTypes.Category, PostTypes.Article, PostStates.Published);
            ObjectPager pager = new ObjectPager();
            int pageCount = pager.PageCount(posts, 10);

            rpPosts.DataSource = pager.GetPage(posts, 0, 10);
            rpPosts.DataBind();
        }
        else
        {
            int iCurrentPage = 0;
            int.TryParse(Request["Page"], out iCurrentPage);
            if (iCurrentPage <= 0)
                iCurrentPage = 1;

            List<BSPost> posts = BSPost.GetPosts(PostTypes.Article, PostStates.Published, 0);

            Panel pnlPaging = new Panel();
            pnlPaging.CssClass = "paging";
            Literal ltPaging = new Literal();
            pnlPaging.Controls.Add(ltPaging);

            BSPlaceHolderPaging.Controls.Add(pnlPaging);

            rpPosts.DataSource = Data.Paging(posts, iCurrentPage, ltPaging);
            rpPosts.DataBind();
        }

        if (rpPosts.Items.Count == 0)
        {
            Literal l = new Literal();
            l.Text = Language.Get["NoWrite"];
            Controls.AddAt(0, l);
        }
    }

    protected void phPost_OnInit(object sender, EventArgs e)
    {
        PlaceHolder bsPostPlaceHolder = ((PlaceHolder)sender);

        Control findableControl = this.Page;

        // Post Detail
        if (PostDetail)
        {
            PostTemplate postDetail = (PostTemplate)LoadControl(Templates.PostDetail);

            PlaceHolder bsInnerPostPlaceHolder = (PlaceHolder)BSHelper.FindChildControl(postDetail, "BSPostPlaceHolder");

            if (bsInnerPostPlaceHolder != null)
                bsInnerPostPlaceHolder.Controls.Add(LoadControl(Templates.Post));

            bsPostPlaceHolder.Controls.Add(postDetail);
        }
        // Post
        else
            bsPostPlaceHolder.Controls.Add(LoadControl(Templates.Post));

        // Related Posts
        PlaceHolder bsRelatedPostsPlaceHolder = (PlaceHolder)BSHelper.FindChildControl(findableControl, "BSRelatedPostsPlaceHolder");

        if (bsRelatedPostsPlaceHolder != null)
            bsRelatedPostsPlaceHolder.Controls.Add(LoadControl(Templates.RelatedPosts));

        // Comments Template
        PlaceHolder bsCommentsPlaceHolder = (PlaceHolder)BSHelper.FindChildControl(findableControl, "BSCommentsPlaceHolder");

        if (bsCommentsPlaceHolder != null)
            bsCommentsPlaceHolder.Controls.Add(LoadControl(Templates.Comments));

        // Comment Form Template
        PlaceHolder bsCommentFormPlaceHolder = (PlaceHolder)BSHelper.FindChildControl(findableControl, "BSCommentFormPlaceHolder");

        if (bsCommentFormPlaceHolder != null)
            bsCommentFormPlaceHolder.Controls.Add(LoadControl(Templates.CommentForm));

    }
}