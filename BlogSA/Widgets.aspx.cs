using System;
using System.IO;

public partial class Widgets : BSThemedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string widget = Request["w"];
        if (!string.IsNullOrEmpty(widget))
        {
            widget = BSHelper.CreateCode(widget);
            BSWidget bsWidget = BSWidget.GetWidget(widget);
            if (bsWidget != null)
                phWidget.Controls.Add(LoadControl("~/Widgets/" + bsWidget.FolderName + "/View.ascx"));
            else if (BSTheme.Current.Widgets[widget] != null)
            {
                BSWidget w = BSTheme.Current.Widgets[widget];
                phWidget.Controls.Add(LoadControl(String.Format("~/Themes/{0}/Widgets/{1}/View.ascx", Blogsa.ActiveTheme, w.FolderName)));
            }
        }
    }
}
