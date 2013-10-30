<%@ Control Language="C#" ClassName="TagCloud" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Generic.List<BSTerm> tags = BSTerm.GetTerms(TermTypes.Tag);

        foreach (BSTerm tag in tags)
        {
            double tagcount = tag.Objects.Count;
            if (tagcount > 0)
            {
                double totaltag = tags.Count;
                double fontweight = ((100 / totaltag) * tagcount) * 100;
                string style = "background:#e7e7e7;padding:3px 5px;border-bottom:1px solid #ddd;display:block;float:left;margin:0 5px 3px 0;color:#444;font-size:";
                if (fontweight >= 99)
                    style += "16px;";
                else if (fontweight >= 70)
                    style += "14px;";
                else if (fontweight >= 40)
                    style += "12px;";
                else if (fontweight >= 15)
                    style += "10px;";
                else if (fontweight >= 3)
                    style += "8px;";
                string href = BSHelper.GetPermalink("Tag", tag.Code, Blogsa.UrlExtension);

                string title = tag.Name;
                CloudLiteral.Text += "<a style=\"" + style + "\" href=" + href + ">" + title + "</a>";
            }
        }
    }

</script>
<div class="widget">
    <div class="title">
        <span>Tag Cloud</span></div>
    <div class="content">
        <asp:Literal ID="CloudLiteral" runat="server" />
        <div style="clear: both; height: 1px; display: block;">
        </div>
    </div>
</div>
