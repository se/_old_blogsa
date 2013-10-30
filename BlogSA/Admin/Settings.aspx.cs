using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Admin_Settings : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPage = Request["p"];
        switch (strPage)
        {
            case "main": divMainSettings.Visible = true;
                lblTitle.Text = Language.Admin["BlogSettings"];
                break;
            case "perma": divPermaLinkSettings.Visible = true;
                lblTitle.Text = Language.Admin["PermalinkSettings"];
                break;
            case "postcomment": divPostCommentSettings.Visible = true;
                lblTitle.Text = Language.Admin["PostCommentSettings"];
                break;
            case "paging": divPagingSettings.Visible = true;
                lblTitle.Text = Language.Admin["PagingSettings"];
                break;
            case "mail": divMailSettings.Visible = true;
                lblTitle.Text = Language.Admin["MailSettings"];
                break;
            case "library": divLibrarySettings.Visible = true;
                lblTitle.Text = Language.Admin["LibrarySettings"];
                break;
            default:
                lblTitle.Text = Language.Admin["BlogSettings"];
                divMainSettings.Visible = true;
                break;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillSettings();

        // Paging Style
        ddlpagingstyle.Items[0].Text = Language.Admin["None"];
        ddlpagingstyle.Items[1].Text = Language.Admin["Numeric"];
        ddlpagingstyle.Items[2].Text = Language.Admin["NextPrevious"];
        ddlpagingstyle.Items[3].Text = Language.Admin["NumericNextPrevious"];
        ddlpagingstyle.Items[4].Text = Language.Admin["NextPreviousFirstLast"];
        ddlpagingstyle.Items[5].Text = Language.Admin["NumericFirstLast"];
        ddlpagingstyle.Items[6].Text = Language.Admin["NumericNextPreviousFirstLast"];
        ddlpagingstyle.Items[7].Text = Language.Admin["OldPostNewPost"];
        ddlpagingstyle.Items[8].Text = Language.Admin["OldPostNewPostHomePage"];

        // Mail
        ddlsmtp_usessl.Items[0].Text = Language.Admin["Active"];
        ddlsmtp_usessl.Items[1].Text = Language.Admin["Passive"];

        // Paging
        ddlpaging.Items[0].Text = Language.Admin["Active"];
        ddlpaging.Items[1].Text = Language.Admin["Passive"];

        // Permalink
        rblpermalink.Items[0].Text = Language.Admin["PermaDefault"];
        rblpermalink.Items[1].Text = Language.Admin["PermaNumeric"];
        rblpermalink.Items[2].Text = Language.Admin["PermaDateName"];
        rblpermalink.Items[3].Text = Language.Admin["PermaYearName"];
        rblpermalink.Items[4].Text = Language.Admin["PermaYearNumber"];
        rblpermalink.Items[5].Text = Language.Admin["PermaCustom"];

        // Comment Approve
        ddladd_comment_approve.Items[0].Text = Language.Admin["Active"];
        ddladd_comment_approve.Items[1].Text = Language.Admin["Passive"];

        // Comment Send Mail
        ddlcomment_sendmail.Items[0].Text = Language.Admin["Active"];
        ddlcomment_sendmail.Items[1].Text = Language.Admin["Passive"];

        // Multi-Language
        ddlmultilanguage.Items[0].Text = Language.Admin["Active"];
        ddlmultilanguage.Items[1].Text = Language.Admin["Passive"];

        // Library
        ddllibrary_usethumbnail.Items[0].Text = Language.Admin["Active"];
        ddllibrary_usethumbnail.Items[1].Text = Language.Admin["Passive"];

        if (!String.IsNullOrEmpty(Request["Message"]) && Request["Message"].Equals("1"))
        {
            MessageBox1.Message = Language.Admin["SettingSaved"];
            MessageBox1.Type = MessageBox.ShowType.Information;
        }
    }

    private void FillSettings()
    {
        try
        {
            if (Blogsa.Settings.Count > 0)
            {
                txtblog_description.Text = Blogsa.Settings["blog_description"].ToString();
                txtblog_name.Text = Blogsa.Settings["blog_name"].ToString();
                txtrecent_comments_count.Text = Blogsa.Settings["recent_comments_count"].ToString();
                txtrecent_posts_count.Text = Blogsa.Settings["recent_posts_count"].ToString();
                txtshow_post_count.Text = Blogsa.Settings["show_post_count"].ToString();

                txtpermaexpression.Text = Blogsa.Settings["permaexpression"].ToString();
                rblpermalink.SelectedValue = Blogsa.Settings["permalink"].ToString();

                ddladd_comment_approve.SelectedValue = Blogsa.Settings["add_comment_approve"].ToString();

                txtallowed_html_tags.Text = Blogsa.Settings["allowed_html_tags"].Value;

                // Language
                ListItemCollection licAdminLanguages = BSHelper.LanguagesByFolder("/Languages/Admin/");
                ListItemCollection licLanguages = BSHelper.LanguagesByFolder("/Languages/Main/");
                
                foreach (ListItem item in licLanguages)
                    ddllanguage.Items.Add(item);

                foreach (ListItem item in licAdminLanguages)
                    ddladmin_language.Items.Add(item);

                ddllanguage.SelectedValue = Blogsa.Settings["language"].ToString();
                ddlpaging.SelectedValue = Blogsa.Settings["paging"].ToString();
                ddlpagingstyle.SelectedValue = Blogsa.Settings["pagingstyle"].ToString();

                ddlcomment_sendmail.SelectedValue = Blogsa.Settings["comment_sendmail"].ToString();

                ddladmin_language.SelectedValue = Blogsa.Settings["admin_language"].Value;

                // Smpt Settings
                txtsmtp_email.Text = Blogsa.Settings["smtp_email"].ToString();
                txtsmtp_name.Text = Blogsa.Settings["smtp_name"].ToString();
                txtsmtp_pass.Text = Blogsa.Settings["smtp_pass"].ToString();
                txtsmtp_port.Text = Blogsa.Settings["smtp_port"].ToString();
                txtsmtp_server.Text = Blogsa.Settings["smtp_server"].ToString();
                txtsmtp_user.Text = Blogsa.Settings["smtp_user"].ToString();
                ddlsmtp_usessl.SelectedValue = Blogsa.Settings["smtp_usessl"].ToString();

                // Library Settings
                txtlibrary_thumbnail_height.Text = Blogsa.Settings["library_thumbnail_height"].ToString();
                txtlibrary_thumbnail_width.Text = Blogsa.Settings["library_thumbnail_width"].ToString();
                txtlibrary_thumbnail_web_height.Text = Blogsa.Settings["library_thumbnail_web_height"].ToString();
                txtlibrary_thumbnail_web_width.Text = Blogsa.Settings["library_thumbnail_web_width"].ToString();
                ddllibrary_usethumbnail.SelectedValue = Blogsa.Settings["library_usethumbnail"].ToString();

                ddlmultilanguage.SelectedValue = Blogsa.Settings["multilanguage"].Value;
            }
            else
            {
                MessageBox1.Message = Language.Admin["SettingNotFound"];
                MessageBox1.Type = MessageBox.ShowType.Normal;
            }
        }
        catch (System.Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string error = String.Empty;
        try
        {
            string strPage = Request["p"];
            if (string.IsNullOrEmpty(strPage))
                strPage = "main";

            if (strPage.Equals("perma"))
            {
                if (!rblpermalink.SelectedValue.Equals("{custom}"))
                    txtpermaexpression.Text = rblpermalink.SelectedValue;
            }

            HtmlGenericControl htmlDiv = null;
            switch (strPage)
            {
                case "main": htmlDiv = divMainSettings; break;
                case "perma": htmlDiv = divPermaLinkSettings; break;
                case "postcomment": htmlDiv = divPostCommentSettings; break;
                case "paging": htmlDiv = divPagingSettings; break;
                case "mail": htmlDiv = divMailSettings; break;
                case "library": htmlDiv = divLibrarySettings; break;
                default: htmlDiv = null; break;
            }

            if (htmlDiv != null)
            {
                foreach (Control wC in htmlDiv.Controls)
                {
                    if (wC is TextBox)
                    {
                        TextBox txtBox = (TextBox)wC;
                        if (txtBox.ID.Equals("txtsmtp_pass") && string.IsNullOrEmpty(txtBox.Text))
                        {
                            txtBox.Text = Blogsa.Settings["smtp_pass"].ToString();
                        }
                        string strName = txtBox.ID.ToString().Substring(3, txtBox.ID.ToString().Length - 3);

                        SaveValue(strName, txtBox.Text);
                    }
                    else if (wC is DropDownList)
                    {
                        DropDownList ddList = (DropDownList)wC;
                        string strName = ddList.ID.ToString().Substring(3, ddList.ID.ToString().Length - 3);

                        SaveValue(strName, ddList.SelectedValue);
                    }
                    else if (wC is RadioButtonList)
                    {
                        RadioButtonList rblList = (RadioButtonList)wC;
                        string strName = rblList.ID.ToString().Substring(3, rblList.ID.ToString().Length - 3);

                        SaveValue(strName, rblList.SelectedValue);
                    }
                }
            }

        }
        catch (System.Exception ex)
        {
            error = ex.Message;
        }

        if (!String.IsNullOrEmpty(error))
        {
            MessageBox1.Message = Language.Admin["SettingError"] + error;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
        else
        {
            Blogsa.RefreshBlogLanguage(Blogsa.DefaultBlogLanguage);
            Blogsa.RefreshBlogAdminLanguage(Blogsa.DefaultBlogAdminLanguage);
            Response.Redirect(String.Format("Settings.aspx?p={0}&Message=1", Request["p"]), true);
        }
    }

    private void SaveValue(string name, string value)
    {
        BSSetting setting = BSSetting.GetSetting(name);
        if (setting != null)
        {
            setting.Value = value;
            if (setting.Save())
            {
                Blogsa.Settings[setting.Name].Value = value;
            }
        }
    }
}
