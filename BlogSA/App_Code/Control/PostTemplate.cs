using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class PostTemplate : UserControl
{
    private BSPost _post;

    public BSPost Post
    {
        get
        {
            if (base.NamingContainer is RepeaterItem)
                return (BSPost)((RepeaterItem)base.NamingContainer).DataItem;
            else if (base.NamingContainer is PostTemplate)
                return (BSPost)((RepeaterItem)base.NamingContainer.Parent.Parent).DataItem;

            return _post;
        }

        set { _post = value; }
    }
    public override void DataBind()
    {
        Post.Code = BSHelper.CreateCode(Post.Title);
        Post.Link = BSHelper.GetLink(Post);

        bool anyId = !String.IsNullOrEmpty(HttpContext.Current.Request["PostID"]) || !String.IsNullOrEmpty(HttpContext.Current.Request["Code"]);

        if (anyId)
        {
            Post.Content = Post.Content.Replace("<!--pagebreak -->", "<a id=\"continue\" name=\"continue\"></a>");
            Post.Content = Post.Content.Replace("<!-- pagebreak -->", "<a id=\"continue\" name=\"continue\"></a>");
        }
        else if (Post.Content.Contains("<!--pagebreak -->"))
        {
            Post.Content = TagCloser(Post.Content.Substring(0, Post.Content.IndexOf("<!--pagebreak -->")));
            Post.Content += String.Format("<a id=\"continue_{0}\" class=\"continue\" href=\"{1}#continue\">{2}</a>", Post.PostID, Post.Link, Language.Get["Continue"]);
        }
        else if (Post.Content.Contains("<!-- pagebreak -->"))
        {
            Post.Content = TagCloser(Post.Content.Substring(0, Post.Content.IndexOf("<!-- pagebreak -->")));
            Post.Content += String.Format("<a id=\"continue_{0}\" class=\"continue\" href=\"{1}#continue\">{2}</a>", Post.PostID, Post.Link, Language.Get["Continue"]);
        }

        if (Page.User != null && Page.User.IsInRole("admin"))
        {
            const string strHideStyle = "style=\"display:none;\"";
            Post.Content = "<div class=\"post-edit\"><div class=\"post-edit-box\">"
                + "<a href=\"" + ResolveUrl("~/Admin/" + (Post.Type == 0 ? "Posts" : "Pages") + ".aspx?PostID=" + Post.PostID) + "\">" + Language.Get["Edit"] + "</a>"
                + "<a " + (Post.State == PostStates.Removed ? strHideStyle : "") + " id=\"post_trash_" + Post.PostID + "\" href=\"javascript:;\" onclick=\"Blogsa.TrashPost(this," + Post.PostID + ");\">" + Language.Get["Trash"] + "</a>"
                + "<a " + (Post.State == PostStates.Published ? strHideStyle : "") + " id=\"post_publish_" + Post.PostID + "\" href=\"javascript:;\" onclick=\"Blogsa.PublishPost(this," + Post.PostID + ");\">" + Language.Get["Publish"] + "</a>"
                + "<a " + (Post.State == PostStates.Draft ? strHideStyle : "") + " id=\"post_draft_" + Post.PostID + "\" href=\"javascript:;\" onclick=\"Blogsa.DraftPost(this," + Post.PostID + ");\">" + Language.Get["Draft"] + "</a>"
                + "<span></span>"
                + "</div>"
                + Post.Content + "</div>";
        }

        PlaceHolder ph = (PlaceHolder)this.FindControl("Content");

        if (ph != null)
        {
            Regex rex = new Regex("<!--widget-(\\w{0,120})-->");
            List<string> lstContents = new List<string>();

            foreach (Match item in rex.Matches(Post.Content))
            {
                int iStart = Post.Content.IndexOf(item.Value);
                lstContents.Add(Post.Content.Substring(0, iStart));
                lstContents.Add(item.Value);
                Post.Content = Post.Content.Substring(iStart + item.Value.Length, Post.Content.Length - iStart - item.Value.Length);
            }
            lstContents.Add(Post.Content);

            for (int i = 0; i < lstContents.Count; i++)
            {
                string str = lstContents[i];
                if (rex.Match(str).Success)
                    ph.Controls.Add(this.LoadControl("~/Widgets/" + str.Substring(11, str.Length - 14) + "/View.ascx"));
                else
                {
                    Literal lt = new Literal();
                    lt.Text = str;
                    ph.Controls.Add(lt);
                }
            }
        }

        BSPost.CurrentPost = this.Post;
        CancelEventArgs eventArgs = new CancelEventArgs();
        BSPost.OnShowing(this.Post, eventArgs);

        if (!eventArgs.Cancel)
        {
            base.DataBind();
            BSPost.OnShowed(this.Post, EventArgs.Empty);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    private static Regex rexOpenTag = new Regex(@"<([A-Z][A-Z0-9]*?)\b[^>/]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static Regex rexCloseTag = new Regex(@"</([A-Z][A-Z0-9]*?)\b[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private string TagCloser(string _Content)
    {
        MatchCollection openedTags = rexOpenTag.Matches(_Content);
        if (openedTags.Count > 0)
        {
            List<string> openingTags = new List<string>();
            foreach (Match openTag in openedTags)
                if (openTag.Groups.Count == 2)
                    openingTags.Add(openTag.Groups[1].Value);
            MatchCollection closedTags = rexCloseTag.Matches(_Content);
            foreach (Match closedTag in closedTags)
                if (closedTag.Groups.Count == 2)
                {
                    int indexToRemove = openingTags.FindIndex(delegate(string openTag) { return openTag.Equals(closedTag.Groups[1].Value, StringComparison.InvariantCultureIgnoreCase); });
                    if (indexToRemove != -1)
                        openingTags.RemoveAt(indexToRemove);
                }
            if (openingTags.Count > 0)
                openingTags.Reverse();
            _Content += "</" + string.Join("></", openingTags.ToArray()) + ">";
        }

        return _Content;
    }
}
