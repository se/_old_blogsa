using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_BSViewOptions : System.Web.UI.UserControl
{
    private NameValueCollection _items;

    public NameValueCollection Items
    {
        get
        {
            if (_items == null)
            {
                _items = new NameValueCollection();
            }
            return _items;
        }
        set { _items = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltView.Text = String.Empty;
        
        foreach (string item in Items)
        {
            string text = item;
            string url = ResolveUrl(Items[item]);
            bool isUrl = Request.RawUrl.ToString(CultureInfo.InvariantCulture).ToLowerInvariant().Equals(url.ToLowerInvariant());
            ltView.Text += String.Format("<li class=\"{2}\"><a href=\"{0}\">{1}</a></li>", url, text, isUrl ? "selected" : "");
        }
    }
}