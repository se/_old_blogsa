using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

public partial class Admin_Widgets_BlogsaNews : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*XmlTextReader XText = new XmlTextReader(@"http://blog.blogsa.net/Feed.aspx");
            DataSet DS = new DataSet();
            DS.ReadXml(XText);
            rpFeed.DataSource = DS.Tables[2];
            rpFeed.DataBind();*/
        }
        catch { }
    }
}
