<%@ WebHandler Language="C#" Class="sitemap" %>

using System;
using System.Collections.Generic;
using System.Web;

public class sitemap : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.AppendLine("<urlset xmlns=\"http://www.google.com/schemas/sitemap/0.84\">");
        List<BSPost> posts = BSPost.GetPosts(PostTypes.Article, PostStates.Published, 0);
        foreach (BSPost post in posts)
        {
            sb.AppendLine("<url>");
            string strUrl = post.Link;
            DateTime dtPostDate = post.UpdateDate;
            sb.AppendLine("<loc>" + strUrl + "</loc>");
            sb.AppendLine("<lastmod>" + dtPostDate.ToString("yyyy-MM-dd") + "</lastmod>");
            sb.AppendLine("<changefreq>monthly</changefreq> ");
            sb.AppendLine("</url>");
        }
        sb.AppendLine("</urlset> ");
        context.Response.Write(sb);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}