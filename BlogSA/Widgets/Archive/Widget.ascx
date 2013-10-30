<%@ Control Language="C#" ClassName="Archive" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">
    private string GetMonths()
    {
        StringBuilder sbMonths = new StringBuilder();
        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteDataset("SELECT Month(CreateDate) AS pMonth,Year(CreateDate) AS pYear,(SELECT Count(PostID) From "
                              +
                              "Posts WHERE  State=1 AND (Type=0 OR Type=3) AND Year(CreateDate)=Year(P.CreateDate) AND Month(CreateDate)=Month(P.CreateDate)) AS PostCount FROM Posts AS P"
                              +
                              " WHERE State=1 AND (Type=0 OR Type=3)  GROUP BY Month(CreateDate), Year(CreateDate) ORDER BY Year(CreateDate) DESC");

            if (dp.Return.Status == DataProcessState.Success)
            {
                DataSet dsMonthYears = dp.Return.Value as DataSet;
                if (dsMonthYears != null)
                {
                    for (int i = 0; i < dsMonthYears.Tables[0].Rows.Count; i++)
                    {
                        DataRow Dr = dsMonthYears.Tables[0].Rows[i];
                        DateTime dtPost = new DateTime(Convert.ToInt16(Dr["pYear"]), Convert.ToInt16(Dr["pMonth"]), 1);
                        string strLink = "";
                        strLink = Blogsa.Url + dtPost.ToString("yyyy\\/MM") + Blogsa.UrlExtension;
                        sbMonths.AppendLine("<li><a href=\"" + strLink + "\">" + dtPost.ToString("MMMM yyyy"));
                        sbMonths.Append(" (" + Dr["PostCount"].ToString() + ")");
                        sbMonths.Append("</a></li>");
                    }
                }
            }
        }
        return sbMonths.ToString();
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Archive"]%></span></div>
    <div class="content">
        <ul>
            <%=GetMonths() %>
        </ul>
    </div>
</div>
