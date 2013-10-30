using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_EditControl : System.Web.UI.UserControl
{
    private string _title;
    private ThemeSettingType _controlType;
    private string _key;
    private string _description;

    protected void Page_Load(object sender, EventArgs e)
    {
        ltTitle.Text = Title;
        label.Attributes["title"] = Description;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (IsPostBack)
        {
            GetValue();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        switch (ControlType)
        {
            case ThemeSettingType.Text:
                label.Attributes["for"] = tb.ClientID;
                tb.Visible = true;
                break;
            case ThemeSettingType.Choose:
                break;
            case ThemeSettingType.Number:
                label.Attributes["for"] = tb.ClientID;
                tb.Visible = true;
                break;
            case ThemeSettingType.Rich:
                label.Attributes["for"] = tb.ClientID;
                tb.Visible = true;
                break;
            case ThemeSettingType.MultiLine:
                label.Attributes["for"] = tb.ClientID;
                tb.TextMode = TextBoxMode.MultiLine;
                tb.Rows = 4;
                tb.Visible = true;
                break;
        }
    }

    public ThemeSettingType ControlType
    {
        get
        {
            return _controlType;
        }
        set
        {
            _controlType = value;
        }
    }

    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
        }
    }

    public string Key
    {
        get { return _key; }
        set { _key = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Value
    {
        get { return GetValue(); }
        set
        {
            SetValue(value);
        }
    }

    private void SetValue(string value)
    {
        switch (ControlType)
        {
            case ThemeSettingType.Text:
                tb.Text = value;
                break;
            case ThemeSettingType.Choose:
                break;
            case ThemeSettingType.Number:
                tb.Text = value;
                break;
            case ThemeSettingType.Rich:
                tb.Text = value;
                break;
            case ThemeSettingType.MultiLine:
                tb.Text = value;
                break;
        }

    }

    private String GetValue()
    {
        switch (ControlType)
        {
            case ThemeSettingType.Text:
                return tb.Text;
            case ThemeSettingType.Choose:
                return tb.Text;
            case ThemeSettingType.Number:
                return tb.Text;
            case ThemeSettingType.Rich:
                return tb.Text;
            case ThemeSettingType.MultiLine:
                return tb.Text;
        }
        return String.Empty;
    }
}