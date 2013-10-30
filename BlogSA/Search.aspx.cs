using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

public partial class Search : BSThemedPage
{
    private String _searchText;
    String SearchText
    {
        get
        {
            if (String.IsNullOrEmpty(_searchText))
            {
                _searchText = Request["q"];
            }
            return _searchText;
        }
        set { _searchText = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(SearchText))
            Response.Redirect("~/");

        using (DataProcess dp = new DataProcess())
        {
            try
            {
                dp.AddParameter("Content", SearchText);
                dp.ExecuteReader("SELECT * FROM Posts WHERE Type = 0 AND State = 1 AND (Content LIKE @Content + ' %' OR Content LIKE '% ' + @Content OR Content LIKE '% ' + @Content + ' %')");

                int p = 0;
                int.TryParse(Request["p"], out p);

                p = p == 0 ? 1 : p;

                IDataReader dr = dp.Return.Value as IDataReader;

                List<BSPost> posts = new List<BSPost>();

                while (dr.Read())
                {
                    BSPost post = new BSPost();
                    BSPost.FillPost(dr, post);
                    posts.Add(post);
                }

                if (posts.Count > 0)
                {
                    ltResult.Text = String.Format(Language.Get["SearchFoundedItemCount"], String.Format("<b>{0}</b>", posts.Count));
                }
                else
                    ltResult.Text = Language.Get["SearchNotFound"];

                ltPaging.Visible = posts.Count > 0;

                rpSearch.DataSource = Data.Paging(posts, p, ltPaging);
                rpSearch.DataBind();
            }
            catch (Exception)
            {
            }
        }
    }

    protected string GetSearchedContent(string content)
    {
        content = Regex.Replace(content, "<.*?>", string.Empty);
        content = Server.HtmlDecode(content).Trim().TrimStart().TrimEnd();

        RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled;

        MatchCollection mc = Regex.Matches(content, SearchText, options);

        int startIndex = mc[0].Index;
        int length = 150;

        if (length > content.Length - startIndex)
        {
            length = content.Length - startIndex;
        }

        if (startIndex > 0)
            content = "..." + content.Substring(startIndex, length) + "...";
        else
            content = content.Substring(startIndex, length) + "...";

        content = Regex.Replace(content, SearchText, String.Format("<span class=\"founded-text\">{0}</span>", "$0"), options);

        return content;
    }
}
