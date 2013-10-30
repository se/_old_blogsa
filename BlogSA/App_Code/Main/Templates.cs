using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

public class Templates
{
    public static string Default
    {
        get
        {
            const string template = "~/Contents/Templates/Default.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Default.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string Widget
    {
        get
        {
            const string template = "~/Contents/Templates/Widget.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Widget.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string Posts
    {
        get
        {
            const string template = "~/Contents/Posts.ascx";
            return template;
        }
    }

    public static string Comments
    {
        get
        {
            const string template = "~/Contents/Comments.ascx";
            return template;
        }
    }

    public static string RelatedPosts
    {
        get
        {
            const string template = "~/Contents/RelatedPosts.ascx";
            return template;
        }
    }

    public static string View
    {
        get
        {
            const string template = "~/Contents/Templates/View.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/View.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else if (File.Exists(HttpContext.Current.Server.MapPath(template)))
                return template;
            else
                return Default;
        }
    }

    public static string Search
    {
        get
        {
            const string template = "~/Contents/Templates/Search.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Search.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string Post
    {
        get
        {
            const string template = "~/Contents/Templates/Post.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Post.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string PostDetail
    {
        get
        {
            const string template = "~/Contents/Templates/PostDetail.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/PostDetail.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string Comment
    {
        get
        {
            const string template = "~/Contents/Templates/Comment.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Comment.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string Login
    {
        get
        {
            const string template = "~/Contents/Templates/Login.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/Login.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }

    public static string CommentForm
    {
        get
        {
            const string template = "~/Contents/Templates/CommentForm.ascx";
            string themedTemplate = String.Format("~/Themes/{0}/Templates/CommentForm.ascx", Blogsa.ActiveTheme);
            if (File.Exists(HttpContext.Current.Server.MapPath(themedTemplate)))
                return themedTemplate;
            else
                return template;
        }
    }
}