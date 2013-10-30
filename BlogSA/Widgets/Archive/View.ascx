<%@ Control Language="C#" ClassName="Search" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int month = -1;
            int year = -1;

            int.TryParse(Request["m"], out month);
            int.TryParse(Request["y"], out year);

            if (month != -1 && year != -1)
            {
                Dictionary<string, object> dicParameters = new Dictionary<string, object>();
                DateTime dtStartDate = new DateTime(year, month, 1);
                DateTime dtEndDate = dtStartDate.AddMonths(1).AddDays(-1);
                dicParameters.Add("SD", dtStartDate);
                dicParameters.Add("ED", dtEndDate);

                using (DataProcess dp = new DataProcess())
                {
                    dp.AddParameter("SD", dtStartDate);
                    dp.AddParameter("ED", dtStartDate);

                    dp.ExecuteDataset("SELECT * FROM Posts WHERE [Type]=0 AND [State]=1 AND [CreateDate]>=@SD AND [CreateDate]<=@ED ORDER BY [CreateDate] Desc");

                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        rpPosts.DataSource = dp.Return.Value;
                        rpPosts.DataBind();
                    }
                }
            }
            else
            {
                divArchives.Visible = true;
            }
        }
        catch { }
    }

    protected void phPost_OnInit(object sender, EventArgs e)
    {
        try
        {
            ((PlaceHolder)sender).Controls.Add(LoadControl("~/Themes/" + Blogsa.Settings["theme"]
                + "/PostTemplate.ascx"));
        }
        catch
        {
            ((PlaceHolder)sender).Controls.Add(LoadControl("~/Contents/PostTemplate.ascx"));
        }
    }

    public string GetMonths(bool bPostCount)
    {
        StringBuilder sbMonths = new StringBuilder(), sbPosts = new StringBuilder();

        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteDataset("SELECT Month(CreateDate) AS pMonth,Year(CreateDate) AS pYear,(SELECT Count(PostID) From Posts "
                              +
                              "WHERE State=1 AND Type=0 AND Year(CreateDate)=Year(P.CreateDate) AND Month(CreateDate)=Month(P.CreateDate)) AS PostCount FROM Posts AS P "
                              +
                              "WHERE State=1 AND Type=0 GROUP BY Month(CreateDate), Year(CreateDate) ORDER BY Year(CreateDate) DESC");


            DataSet dsMonths = dp.Return.Value as DataSet;

            dp.ExecuteDataset("SELECT *,Month(CreateDate) AS pMonth,Year(CreateDate) AS pYear FROM Posts Where State=1 AND Type=0");

            DataSet dsPosts = dp.Return.Value as DataSet;

            if (dsMonths != null && dsPosts != null)
            {
                foreach (DataRow drMonth in dsMonths.Tables[0].Rows)
                {
                    DateTime dtPostMonthYear = new DateTime(Convert.ToInt16(drMonth["pYear"]), Convert.ToInt16(drMonth["pMonth"]), 1);
                    sbMonths.AppendLine("<li>" + dtPostMonthYear.ToString("MMMM") + " " + dtPostMonthYear.ToString("yyyy") + "(" + drMonth["PostCount"] + ")");
                    sbPosts = new StringBuilder();
                    sbPosts.AppendLine("<ul>");
                    foreach (DataRow drPost in dsPosts.Tables[0].Select("pYear=" + dtPostMonthYear.Year
                        + " AND pMonth=" + dtPostMonthYear.Month))
                    {
                        string strPostLink = BSPost.GetPost((int)drPost["PostID"]).Link;
                        sbPosts.AppendLine("<li><a href=\"" + strPostLink + "\">" + drPost["Title"] + "</a></li>");
                    }
                    sbPosts.AppendLine("</ul>");
                    sbMonths.AppendLine(sbPosts.ToString());
                    sbMonths.AppendLine("</li>");
                }
            }
        }
        return sbMonths.ToString();
    }
</script>
<style type="text/css">
    .archive
    {
        line-height: 20px;
    }
    .archive ul
    {
        margin-bottom: 40px;
        font-size: 12pt;
    }
    .archive ul ul
    {
        font-size: 10pt;
    }
    .archive ul li a
    {
        font-weight: bold;
    }
    .archive ul li ul li a
    {
        font-weight: normal;
    }
</style>
<div class="archive posts" runat="Server" id="divArchives" visible="false">
    <div class="content">
        <ul>
            <%=GetMonths(true) %></ul>
    </div>
</div>
<asp:Repeater runat="server" ID="rpPosts">
    <HeaderTemplate>
        <div class="title">
            <h2>
                <%=Language.Get["Archive"] %></h2>
        </div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="posts">
            <asp:PlaceHolder runat="server" OnInit="phPost_OnInit" ID="phPost"></asp:PlaceHolder>
        </div>
        <br />
    </ItemTemplate>
</asp:Repeater>
