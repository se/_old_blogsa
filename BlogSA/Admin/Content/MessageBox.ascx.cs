using System;

public partial class MessageBox : System.Web.UI.UserControl
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Message = String.Empty;
        this.Visible = false;
    }

    public string Message
    {
        get
        {
            String s = (String)ViewState["Message"];
            return (s ?? String.Empty);
        }
        set
        {
            ViewState["Message"] = value;
            ltMessage.Text = value;

            if (!String.IsNullOrEmpty(value))
            {
                this.Visible = true;
            }
        }
    }

    public ShowType Type
    {
        get
        {
            ShowType s = ShowType.Normal;
            if (ViewState["Type"] != null)
                s = (ShowType)ViewState["Type"];

            return s;
        }

        set
        {
            ViewState["Type"] = value;

            switch (Type)
            {
                case ShowType.Information:
                    divOuter.Attributes["class"] = "ui-state-highlight ui-corner-all";
                    spanIcon.Attributes["class"] = "ui-icon ui-icon-info";
                    break;
                case ShowType.Error:
                    divOuter.Attributes["class"] = "ui-state-error ui-corner-all";
                    spanIcon.Attributes["class"] = "ui-icon ui-icon-info";
                    break;
                case ShowType.Normal:
                    divOuter.Attributes["class"] = "ui-state-highlight ui-corner-all";
                    spanIcon.Attributes["class"] = "ui-icon ui-icon-info";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum ShowType
    {
        Information,
        Error,
        Normal
    }
}
