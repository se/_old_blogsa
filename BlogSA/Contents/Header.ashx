<%@ WebHandler Language="C#" Class="Header" %>

using System;
using System.Web;

public class Header : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string strHead = HttpContext.Current.Request["head"];
        if (!string.IsNullOrEmpty(strHead))
        {
            switch (strHead)
            {
                case "opensearchdescription":
                    context.Response.ContentType = "text/xml";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine("<SearchPlugin xmlns=\"http://www.mozilla.org/2006/browser/search/\" xmlns:os=\"http://a9.com/-/spec/opensearch/1.1/\">");
                    sb.AppendLine("<os:ShortName>" + Blogsa.Settings["blog_name"] + "</os:ShortName>");
                    sb.AppendLine("<os:Description>" + Blogsa.Settings["blog_name"] + "</os:Description>");
                    sb.AppendLine("<os:InputEncoding>UTF-8</os:InputEncoding>");
                    sb.AppendLine("<SearchForm>" + Blogsa.Url + "</SearchForm>");
                    sb.AppendLine("<os:Url type=\"text/html\" method=\"GET\" template=\"" + Blogsa.Url + "?word={searchTerms}\"></os:Url>");
                    sb.AppendLine("</SearchPlugin>");
                    //<os:Image width="16" height="16"></os:Image>

                    context.Response.Write(sb.ToString());
                    break;
                default: break;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}