using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for WidgetBase
/// </summary>
public class WidgetBase : UserControl
{
    private BSWidget _widget;
    public BSWidget Widget
    {
        protected get { return _widget; }
        set { _widget = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Widget.Type == WidgetTypes.Normal)
        {
            Control cDynamic = FindControl("Dynamic");
            Control cStatic = FindControl("Static");

            if (cDynamic == null && cStatic == null)
            {
                this.Controls.Clear();
                this.Controls.Add(LoadControl("~/Widgets/" + Widget.FolderName + "/" + "Widget.ascx"));
            }
            else
            {
                cDynamic.Controls.Clear();
                cDynamic.Controls.Add(LoadControl("~/Widgets/" + Widget.FolderName + "/" + "Widget.ascx"));

                cStatic.Visible = false;                
            }
        }

        DataBind();
    }

    public override void DataBind()
    {
        base.DataBind();
    }
}