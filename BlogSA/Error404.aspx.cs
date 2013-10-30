using System;

public partial class Error404 : BSThemedPage
{
    private string _OriginalUrl;
    public string OriginalUrl
    {
        get { return _OriginalUrl; }
        set { _OriginalUrl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        OriginalUrl = Request.QueryString["aspxerrorpath"] ?? Request.RawUrl;
        Server.ClearError();
    }
}
