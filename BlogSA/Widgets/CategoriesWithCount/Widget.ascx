<%@ Control Language="C#" ClassName="CategoriesBlock" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">
    public string GetLinks()
    {
        DataSet dsCategories = null;
        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteDataset("SELECT TermID,COUNT(TermID) PostCount,"
                              + "(SELECT Name FROM Terms WHERE TermID = T.TermID) Name, "
                              + "(SELECT Code FROM Terms WHERE TermID = T.TermID) Code FROM TermsTo T WHERE T.Type='category' AND "
                              + "ObjectID IN (SELECT PostID FROM Posts WHERE (Type=0 OR Type=3) AND State = 1) "
                              + "GROUP BY TermID");

            dsCategories = dp.Return.Value as DataSet;
        }

        if (dsCategories != null)
        {
            string Tags = "";
            foreach (DataRow Row in dsCategories.Tables[0].Rows)
            {
                if ((int)Row["PostCount"] > 0)
                {
                    string _Link = "Posts.aspx?Category=" + Row["Code"].ToString();
                    _Link = BSHelper.GetPermalink("Category", Row["Code"].ToString(), Blogsa.UrlExtension);
                    Tags += "<li><a href=\"" + _Link + "\">" + Row["Name"].ToString() + "&nbsp;(" + Row["PostCount"] + ")</a></li>";
                }
            }
            return Tags;
        }
        else
        {
            return "";
        }
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Categories"] %></span></div>
    <div class="content">
        <ul>
            <%=GetLinks()%>
        </ul>
    </div>
</div>
