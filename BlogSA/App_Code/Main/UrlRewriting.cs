using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web.UI;

/// <summary>
/// Summary description for UrlRewriting
/// </summary>
/// 
public class UrlRewriting : IHttpModule, IRequiresSessionState
{
    public void Init(HttpApplication BlogsaApplication)
    {
        BlogsaApplication.BeginRequest += new EventHandler(BlogsaApplication_BeginRequest);
    }

    void BlogsaApplication_BeginRequest(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = ((HttpApplication)sender).Context;
            HttpRequest request = context.Request;
            string _AbsolutePath = request.RawUrl.ToLowerInvariant();
            string _Page = request.Url.Segments[request.Url.Segments.Length - 1].ToLowerInvariant();

            bool isFileExist = System.IO.File.Exists(context.Server.MapPath(context.Request.Url.LocalPath));

            if (!isFileExist && _Page.EndsWith(Blogsa.UrlExtension))
            {
                Regex rexAdmin = new Regex(@"/admin/(.*)$");
                if (!rexAdmin.Match(_AbsolutePath).Success)
                {
                    string strUrlUnLowered = request.Url.Segments[request.Url.Segments.Length - 1];
                    string strNormalUrl = strUrlUnLowered.ToLowerInvariant();

                    string[] Paths = _AbsolutePath.Split('/');
                    if (request.Url.ToString().ToLowerInvariant().EndsWith("/sitemap.xml"))
                        HttpContext.Current.RewritePath("~/sitemap.ashx", false);
                    else
                    {
                        string language = String.Empty;

                        if (Blogsa.MutliLanguage)
                        {
                            string blogsaUrl = Blogsa.Url;
                            string url = request.Url.ToString();

                            url = url.Substring(blogsaUrl.Length, url.Length - blogsaUrl.Length);

                            if (url.IndexOf("/") == -1)
                            {
                                context.Response.Redirect(string.Format("{0}{1}/{2}", Blogsa.Url, Blogsa.DefaultBlogLanguage, url));
                            }
                            else
                            {
                                language = "&lang=" + url.Substring(0, url.IndexOf("/"));
                            }
                        }

                        // If Permalink Custom , so permalink = 2
                        // Long Url
                        string strUrl = request.Url.ToString();
                        // Short Url
                        string strRelativeUnlowered = strUrl.Substring(Blogsa.Url.Length - 1, strUrl.Length - Blogsa.Url.Length + 1);
                        string strRelativeUrl = strRelativeUnlowered;
                        RegexOptions eROP = RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
                        if (new Regex(@"/(\d{4})/(\d{2})" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/(\d{4})/(\d{2})" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Widgets.aspx?w=Archive&y=$1&m=$2" + language), false);
                        }
                        else if (new Regex(@"/category/(.+)/page/(\d+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/category/(.+)/page/(\d+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Posts.aspx?Category=$1&Page=$2" + language), false);
                        }
                        else if (new Regex(@"/tag/(.+)/page/(\d+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/tag/(.+)/page/(\d+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Posts.aspx?Tag=$1&Page=$2" + language), false);
                        }
                        else if (new Regex(@"/page/(\d+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/page/(\d+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl.ToLowerInvariant(), "~/Posts.aspx?Page=$1" + language), false);
                        }
                        else if (new Regex(@"/category/(.+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/category/(.+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Posts.aspx?Category=$1" + language), false);
                        }
                        else if (new Regex(@"/tag/(.+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/tag/(.+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Posts.aspx?Tag=$1" + language), false);
                        }
                        else if (new Regex(@"/widgets/(.+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/widgets/(.+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Widgets.aspx?w=$1" + language), false);
                        }
                        else if (new Regex(@"/user/(.+)" + Blogsa.UrlExtension + "$", eROP).Match(strRelativeUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/user/(.+)" + Blogsa.UrlExtension + "$", eROP)
                                .Replace(strRelativeUrl, "~/Users.aspx?p=$1" + language), false);
                        }
                        else
                        {
                            Dictionary<string, string> dicValues = GetCustomPermaValues(request, strRelativeUrl, strUrl);
                            if (dicValues.ContainsKey("{id}"))
                                HttpContext.Current.RewritePath("~/Posts.aspx?PostID=" + dicValues["{id}"] + language);
                            else if (dicValues.ContainsKey("{name}"))
                                HttpContext.Current.RewritePath("~/Posts.aspx?Code=" + dicValues["{name}"] + language);
                        }

                    }
                }
            }
        }
        catch
        {

        }
    }

    private static Dictionary<string, string> GetCustomPermaValues(HttpRequest request, string strRelativeUrl, string strUrl)
    {
        try
        {
            Dictionary<string, string> dicExpressions = new Dictionary<string, string>();
            dicExpressions.Add("{name}", @"/([A-Z0-9a-z-]+)");
            dicExpressions.Add("{author}", @"/([A-Z0-9a-z-]+)");
            dicExpressions.Add("{category}", @"/([A-Z0-9a-z-]+)");
            dicExpressions.Add("{year}", @"/(\d{4})");
            dicExpressions.Add("{month}", @"/(\d{2})");
            dicExpressions.Add("{day}", @"/(\d{2})");
            dicExpressions.Add("{id}", @"/(\d+)");

            // Perma Settings
            string strReturnUrl = strRelativeUrl;
            string strExpression = Blogsa.Settings["permaexpression"].ToString() + "$";
            string strExpressionNormal = strExpression.Replace("{year}/", "").Replace("{day}/", "").Replace("{month}/", "");
            List<string> lstExpressions = new List<string>();
            Regex rexList = new Regex("({(.+?)})", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            foreach (Match match in rexList.Matches(strExpression))
            {
                strExpression = strExpression.Replace(match.Value, dicExpressions[match.Value]);
                strExpressionNormal = strExpressionNormal.Replace(match.Value, dicExpressions[match.Value]);
                lstExpressions.Add(match.Value);
            }
            Dictionary<string, string> dicValues = new Dictionary<string, string>();
            strExpression = strExpression.Replace("//", "/");
            strExpressionNormal = strExpressionNormal.Replace("//", "/");
            Regex rex = new Regex(strExpression, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            Regex rexNormal = new Regex(strExpressionNormal, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            if (rex.Match(strRelativeUrl).Success)
                GetRegexUrlValues(strRelativeUrl, ref strReturnUrl, ref strExpression, lstExpressions, dicValues, ref rex);
            else if (rexNormal.Match(strRelativeUrl).Success)
            {
                lstExpressions.Remove("{year}");
                lstExpressions.Remove("{day}");
                lstExpressions.Remove("{month}");
                GetRegexUrlValues(strRelativeUrl, ref strReturnUrl, ref strExpressionNormal, lstExpressions, dicValues, ref rex);
            }
            return dicValues;
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private static void GetRegexUrlValues(string strRelativeUrl, ref string strReturnUrl, ref string strExpression, List<string> lstExpressions, Dictionary<string, string> dicValues, ref Regex rex)
    {
        try
        {
            int iGroupCount = rex.GetGroupNumbers().Length;
            string strReplace = string.Empty;
            for (int i = 1; i < iGroupCount; i++)
            {
                strReplace += "{$" + i + "}";
            }
            strReturnUrl = rex.Replace(strRelativeUrl, strReplace);
            rex = new Regex("{(.*?)}", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            int counter = 0;
            foreach (Match item in rex.Matches(strReturnUrl))
                dicValues.Add(lstExpressions[counter++], item.Value.Substring(1, item.Value.Length - 2));
            if (dicValues.Count == 0)
            {
                strExpression = Blogsa.Settings["permaexpression"].ToString();
                string strExt = Blogsa.UrlExtension;
                Regex rx = new Regex("{id}", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                Regex regexCode = new Regex(@"/(\d+)" + strExt + "$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                Match match = regexCode.Match(strRelativeUrl);
                if (match.Success && rx.Match(strExpression).Success)
                {
                    string strVal = match.Value.Replace("/", "");
                    strVal = new Regex("." + Blogsa.UrlExtension + "$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase).Replace(strVal, "");
                    dicValues.Add("{id}", strVal);
                }
                else
                {
                    rx = new Regex("{name}", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                    if (rx.Match(strExpression).Success)
                    {
                        regexCode = new Regex(@"/([A-Z0-9a-z-]+)" + strExt + "$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                        match = regexCode.Match(strRelativeUrl);
                        if (match.Success)
                        {
                            string strVal = match.Value.Replace("/", "");
                            strVal = new Regex(strExt + "$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase).Replace(strVal, "");
                            dicValues.Add("{name}", strVal);
                        }
                    }
                }
            }
        }
        catch
        {

        }
    }

    public void Dispose() { }
}