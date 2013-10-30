using System;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class LoginFormBase : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.ToString() == "logout")
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        Panel divLogin = (Panel)FindControl("divLogin");
        HtmlControl divLostPassword = (HtmlControl)FindControl("divLostPassword");

        TextBox txtUserName = (TextBox)FindControl("txtUserName");
        TextBox txtPassword = (TextBox)FindControl("txtPassword");

        if (Request.QueryString.ToString() == "lostpassword")
        {
            divLogin.Visible = false;
            divLostPassword.Visible = true;
        }
        else if (Page.User.Identity.IsAuthenticated && Session["ActiveUser"] != null)
        {
            if (Page.User.IsInRole("Admin"))
                Response.Redirect("~/Admin/");
            else if (Page.User.IsInRole("Editor"))
                Response.Redirect("~/Admin/Editor.aspx");
        }
        else
        {
            divLogin.Visible = true;
            divLostPassword.Visible = false;

            if (string.IsNullOrEmpty(txtUserName.Text))
                txtUserName.Focus();
            else
                txtPassword.Focus();
        }
    }

    protected void btnSendPassword_Click(object sender, EventArgs e)
    {
        TextBox txtEmail = (TextBox)FindControl("txtEmail");

        Label lblInfo = (Label)FindControl("lblInfo");

        try
        {
            if (BSHelper.CheckEmail(txtEmail.Text) & BSUser.ValidateEmail(txtEmail.Text))
            {
                BSUser user = BSUser.GetUserByEmail(txtEmail.Text);

                string strNewPassword = BSHelper.GetRandomStr(8);
                string strMessage = Language.Get["YourNewPassword"].Replace("%", strNewPassword);
                string strMail = Blogsa.Settings["admin_email"].Value;

                user.Password = BSHelper.GetMd5Hash(strNewPassword);
                user.Save();

                if (BSHelper.SendMail("Blogsa - Password Reminder", strMail, Blogsa.Settings[0].Value, user.Email, user.Name, strMail, true))
                    lblInfo.Text = Language.Get["PasswordMailSend"];
                else
                    lblInfo.Text = "Mail not send! Please check your mail settings!";
            }
            else
            {
                lblInfo.Text = Language.Get["ErrorMail"];
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        TextBox txtUserName = (TextBox)FindControl("txtUserName");
        TextBox txtPassword = (TextBox)FindControl("txtPassword");

        Label lblInfo = (Label)FindControl("lblInfo");
        CheckBox cbRememberMe = (CheckBox)FindControl("cbRememberMe");

        BSUser user = BSUser.GetUser(txtUserName.Text, BSHelper.GetMd5Hash(txtPassword.Text));
        if (user != null)
        {
            Session.Timeout = 129600;
            Blogsa.ActiveUser = user;

            Roles.AddUserToRole(Blogsa.ActiveUser.UserName, Blogsa.ActiveUser.Role);

            user.LastLoginDate = DateTime.Now;
            user.Save();
            FormsAuthentication.RedirectFromLoginPage(Blogsa.ActiveUser.UserName, cbRememberMe.Checked);
        }
        else
            lblInfo.Text = Language.Get["ErrorUserPassword"];
    }
}