using System;

public partial class Feed : BSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Feeder.GetRSS(this, RssTypes.Posts);
    }
}
