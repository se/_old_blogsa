using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace App_Code.Control
{
    public class BSWidgetHolder : PlaceHolder
    {
        protected override void OnInit(EventArgs e)
        {
            //Widget Preview
            if (HttpContext.Current.Request["widget"] != null && this.ID.Equals("Default"))
            {
                using (PlaceHolder ph = (PlaceHolder)BSHelper.FindChildControl(Page, "Default"))
                    ph.Controls.Add(Page.LoadControl("~/Widgets/" + BSHelper.CreateCode(HttpContext.Current.Request["widget"]) + "/" + "Widget.ascx"));
            }

            List<BSWidget> widgets = BSWidget.GetWidgetsByPlaceHolder(this.ID, true);

            if (widgets.Count > 0)
            {
                foreach (BSWidget widget in widgets)
                {
                    PlaceHolder ph = (PlaceHolder)BSHelper.FindChildControl(Page, widget.PlaceHolder);
                    if (ph != null)
                    {
                        WidgetBase wb = (WidgetBase)Page.LoadControl(Templates.Widget);
                        wb.Widget = widget;
                        wb.DataBind();
                        ph.Controls.Add(wb);
                    }
                }
            }

            base.OnInit(e);
        }
    }
}