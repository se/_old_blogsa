using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Xml;

/// <summary>
/// Summary description for Feed
/// </summary>
public enum RssTypes
{
    Comments,
    Posts,
    CommentsByPost
}

public class Feeder
{
    // Call RSS
    public static void GetRSS(Page page, RssTypes type)
    {
        XmlTextWriter writer = new XmlTextWriter(page.Response.OutputStream, System.Text.Encoding.UTF8);
        WriteRSSPrologue(writer, type);

        List<BSPost> posts = new List<BSPost>();

        int intFeedCount = Blogsa.Settings["show_feed_count"] != null ? Convert.ToInt32(Blogsa.Settings["show_feed_count"]) : 10;
        if (type == RssTypes.Comments)
        {
            List<BSComment> comments = BSComment.GetComments(CommentStates.Approved);
            if (comments.Count > 0)
            {
                foreach (BSComment comment in comments)
                {
                    BSPost bsPost = BSPost.GetPost(comment.PostID);
                    AddRSSItem(writer, bsPost.Title, bsPost.Link + "#Comments" + comment.CommentID,
                        comment.Content, comment.Date.ToString("EEE, dd MMMM yyyy HH:mm:ss Z"), comment.UserName);
                }
                WriteRSSClosing(writer);
                writer.Flush();
                writer.Close();
                page.Response.ContentEncoding = System.Text.Encoding.UTF8;
                page.Response.ContentType = "text/xml";
                page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                page.Response.End();
            }
        }
        else if (type == RssTypes.CommentsByPost)
        {
            List<BSComment> comments = BSComment.GetCommentsByPostID(Convert.ToInt32(HttpContext.Current.Request["PostID"]), CommentStates.Approved);
            if (comments.Count > 0)
            {
                BSPost bsPost = BSPost.GetPost(Convert.ToInt32(HttpContext.Current.Request["PostID"]));
                foreach (BSComment comment in comments)
                {
                    AddRSSItem(writer, bsPost.Title, bsPost.Link + "#Comments" + comment.CommentID,
                        comment.Content, comment.Date.ToString("EEE, dd MMMM yyyy HH:mm:ss Z"), comment.UserName);
                }
                WriteRSSClosing(writer);
                writer.Flush();
                writer.Close();
                page.Response.ContentEncoding = System.Text.Encoding.UTF8;
                page.Response.ContentType = "text/xml";
                page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                page.Response.End();
            }
        }
        else if (type == RssTypes.Posts)
        {
            posts = BSPost.GetPosts(PostTypes.Article, PostStates.Published, 10);
            if (posts.Count > 0)
            {
                foreach (BSPost post in posts)
                {
                    AddRSSItem(writer, post.Title, post.Link, post.Content, post.Date.ToString("EEE, dd MMMM yyyy HH:mm:ss Z"), post.UserName);
                }
                WriteRSSClosing(writer);
                writer.Flush();
                writer.Close();
                page.Response.ContentEncoding = System.Text.Encoding.UTF8;
                page.Response.ContentType = "text/xml";
                page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                page.Response.End();
            }
        }
    }
    // Creating RSS Header
    private static XmlTextWriter WriteRSSPrologue(XmlTextWriter writer, RssTypes type)
    {
        writer.WriteStartDocument();
        writer.WriteStartElement("rss");
        writer.WriteAttributeString("version", "2.0");
        writer.WriteAttributeString("xmlns:blogChannel", Blogsa.Url);
        writer.WriteStartElement("channel");

        string strTitle = Blogsa.Settings["blog_name"] + " - " + Blogsa.Settings["blog_description"];
        if (type == RssTypes.Comments || type == RssTypes.CommentsByPost)
            strTitle += (" " + Language.Get["Comments"]);

        writer.WriteElementString("title", strTitle);
        writer.WriteElementString("link", Blogsa.Url);
        writer.WriteElementString("description", Blogsa.Description);
        writer.WriteElementString("copyright", "Copyright " + Blogsa.Settings["blog_name"]);
        writer.WriteElementString("generator", Blogsa.Url);
        return writer;
    }
    // Creating RSS Item
    private static XmlTextWriter AddRSSItem(XmlTextWriter writer, string sItemTitle, string sItemLink, string sItemDescription, string sDate, string sAuthor)
    {
        writer.WriteStartElement("item");
        writer.WriteElementString("title", HttpContext.Current.Server.HtmlDecode(sItemTitle));
        writer.WriteElementString("link", sItemLink);
        writer.WriteElementString("description", sItemDescription);
        writer.WriteElementString("pubDate", sDate);
        writer.WriteElementString("author", HttpContext.Current.Server.HtmlDecode(sAuthor));
        writer.WriteEndElement();

        return writer;
    }
    // Creating RSS Footer
    private static XmlTextWriter WriteRSSClosing(XmlTextWriter writer)
    {
        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndDocument();

        return writer;
    }
}
