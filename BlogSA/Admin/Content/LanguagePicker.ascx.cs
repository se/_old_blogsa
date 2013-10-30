using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_LanguagePicker : System.Web.UI.UserControl
{
    public string LangaugeCode
    {
        get { return ddlLanguages.SelectedValue; }
        set
        {
            if (ddlLanguages.Items.Count == 0)
            {
                ListItemCollection licLanguages = BSHelper.LanguagesByFolder("/Languages/Main/");
                foreach (ListItem licLanguage in licLanguages)
                    ddlLanguages.Items.Add(licLanguage);

                ddlLanguages.Items.Insert(0, new ListItem("All", System.Globalization.CultureInfo.InvariantCulture.TwoLetterISOLanguageName));
            }

            ddlLanguages.SelectedValue = value;
        }
    }
}