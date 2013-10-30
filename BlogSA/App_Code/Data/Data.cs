using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;

public class Data
{
    public static IEnumerable<T> Paging<T>(IList<T> source, int currentPage, Literal pagerControl)
    {
        string strFirstPage = Language.Get["FirstPage"];
        string strPreviousPage = Language.Get["PreviousPage"];
        string strNextPage = Language.Get["NextPage"];
        string strLastPage = Language.Get["LastPage"];

        #region Paging Process
        int pagerStyle = Convert.ToInt32(Blogsa.Settings["pagingstyle"].Value);
        int pageSize = Convert.ToInt32(Blogsa.Settings["show_post_count"].Value);

        int rowCount = source.Count;
        int pageCount = (rowCount / pageSize) + (rowCount % pageSize <= 0 ? 0 : 1);

        currentPage = currentPage > pageCount ? 1 : currentPage;
        int startItem = (currentPage * pageSize) - pageSize;
        int endItem = currentPage * pageSize;
        endItem = endItem > rowCount ? rowCount : endItem;
        #endregion
        #region Numeric Style
        string strNumeric = "";
        for (int i = 1; i < pageCount + 1; i++)
        {
            strNumeric += GetLink(i, currentPage, i.ToString());
        }
        #endregion
        #region NextPrevious Style
        string strNext = currentPage != pageCount ? GetLink(((currentPage + 1) > (pageCount) ? pageCount : currentPage + 1), currentPage, strNextPage) : String.Empty;
        string strPrevious = currentPage > 1 ? GetLink(((currentPage - 1) == 0 ? 1 : currentPage - 1), currentPage, strPreviousPage) : String.Empty;
        string strFirst = currentPage > 2 ? GetLink(1, currentPage, strFirstPage) : String.Empty;
        string strLast = currentPage < pageCount - 1 ? GetLink(pageCount, currentPage, strLastPage) : String.Empty;

        string strPager = String.Empty;
        #endregion
        #region OldPostNewPost Style & OldPostNewPostHomePage Style
        string strOldPost = pageCount + 1 > 1 & currentPage - 1 != pageCount ? GetLink(currentPage + 1, currentPage, Language.Get["OldPosts"]) : String.Empty;
        string strNewPost = currentPage > 1 ? GetLink(currentPage - 1, currentPage, Language.Get["NewPosts"]) : String.Empty;
        string strHomePage = currentPage > 1 ? GetLink(1, currentPage, Language.Get["HomePage"]) : String.Empty;
        #endregion

        switch (pagerStyle)
        {
            //None
            case 0:
                break;
            //Numeric
            case 1: strPager = strNumeric;
                break;
            //NextPrevious
            case 2: strPager = strPrevious + strNext;
                break;
            //NumericNextPrevious
            case 3: strPager = strPrevious + strNumeric + strNext;
                break;
            //NextPreviousFirstLast
            case 4: strPager = strFirst + strPrevious + strNext + strLast;
                break;
            //NumericFirstLast
            case 5: strPager = strFirst + strNumeric + strLast;
                break;
            //NumericNextPreviousFirstLast
            case 6: strPager = strFirst + strPrevious + strNumeric + strNext + strLast;
                break;
            //OldPostNewPost
            case 7: strPager = strNewPost + strOldPost;
                break;
            //OldPostNewPostHomePage
            case 8: strPager = strNewPost + strOldPost + strHomePage;
                break;
            default:
                break;
        }

        pagerControl.Text += strPager;

        if (pageCount == 1)
        {
            pagerControl.Text = String.Empty;
        }

        for (int i = startItem; i < endItem && i < source.Count; i++)
        {
            yield return source[i];
        }
    }

    private static string GetLink(int pageNumber, int currentPage, string label)
    {
        string strLink = "";
        if (pageNumber == currentPage)
        {
            strLink += "<span>" + label + "</span>";
        }
        else
        {
            string tag = HttpContext.Current.Request.QueryString["Tag"];
            string category = HttpContext.Current.Request.QueryString["Category"];
            string strOtherQuery = "";
            if (!string.IsNullOrEmpty(tag))
                strOtherQuery = "Tag/" + tag + "/";
            else if (!string.IsNullOrEmpty(category))
                strOtherQuery = "Category/" + category + "/";
            strLink += "<a href=\"" + Blogsa.Url + strOtherQuery
                + "Page/" + pageNumber + Blogsa.UrlExtension + "\">" + label + "</a>";
        }
        return strLink;
    }
}
