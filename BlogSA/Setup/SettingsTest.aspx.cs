using System;
using System.Configuration;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

public partial class SettingsTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String strSetup = ConfigurationManager.AppSettings["Setup"];
        String strProvider = ConfigurationManager.AppSettings["Provider"];
        String strConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        lblSetup.Text = strSetup;
        lblConnectionString.Text = "[Hidden]";
        lblProvider.Text = strProvider;

        String strDetailMessage = "<div style='border:1px solid #B20012;background:#EAD1D3;display:block;padding:10px;margin:10px;'>{0}</div>";

        DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(strProvider);
        using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
        {
            try
            {
                dbConnection.ConnectionString = strConnectionString;
                dbConnection.Open();
                lblConnectionTest.Text = "Success";
                lblConnectionTest.ForeColor = Color.FromName("#0F6004");
                lblConnectionTest.Font.Bold = true;
                dbConnection.Close();

            }
            catch (Exception ex)
            {
                Exception inner = ex.InnerException;

                string strTitle = String.Format("<b>{0}</b><br><br>", ex.Message);

                lblConnectionTest.Text = "Error";
                lblConnectionTest.Text += String.Format(strDetailMessage, strTitle + ex.StackTrace.Replace("\n", "<br>"));

                while (inner != null)
                {
                    strTitle = String.Format("<b>{0}</b><br><br>", inner.Message);
                    lblConnectionTest.Text += String.Format(strDetailMessage, strTitle + inner.StackTrace.Replace("\n", "<br>"));
                    inner = inner.InnerException;

                }
            }
        }


        // Mail Settings

        try
        {
            string strSubject = "Test Mail", strFrom = Blogsa.Settings["smtp_email"].Value, strTo = strFrom,
                strBody = "This mail sended because of test. If you see this mail then your smtp settings is okey.";

            bool bIsBodyHtml = true;

            MailMessage message = new MailMessage();
            message.Subject = "Test Mail";
            message.From = new MailAddress(strFrom);
            message.To.Add(new MailAddress(strTo, strTo));
            message.Body = strBody;
            message.IsBodyHtml = bIsBodyHtml;
            message.Priority = MailPriority.High;

            string strServer = Blogsa.Settings["smtp_server"].Value;
            int iPort = Convert.ToInt32(Blogsa.Settings["smtp_port"].Value);

            string strUserName = Blogsa.Settings["smtp_user"].Value;
            string strPassword = Blogsa.Settings["smtp_pass"].Value;

            lblSMTPUsername.Text = strUserName;
            lblSMTPPassword.Text = "[Hidden]";

            lblSMTPServerPort.Text = String.Format("{0} / {1}", strServer, iPort);

            SmtpClient client = new SmtpClient(strServer, iPort);
            
            NetworkCredential SMTPUserInfo = new NetworkCredential(strUserName, strPassword);
            client.UseDefaultCredentials = false;

            client.Credentials = SMTPUserInfo;
            client.EnableSsl = Convert.ToBoolean(Blogsa.Settings["smtp_usessl"].Value);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(message);

            SuccessMessage(lblSMTPTest, "Success");
        }
        catch (Exception ex)
        {
            Exception inner = ex.InnerException;

            string strTitle = String.Format("<b>{0}</b><br><br>", ex.Message);

            lblSMTPTest.Text = "Error";
            lblSMTPTest.Text += String.Format(strDetailMessage, strTitle + ex.StackTrace.Replace("\n", "<br>"));

            while (inner != null)
            {
                strTitle = String.Format("<b>{0}</b><br><br>", inner.Message);
                lblSMTPTest.Text += String.Format(strDetailMessage, strTitle + inner.StackTrace.Replace("\n", "<br>"));
                inner = inner.InnerException;

            }
        }

        // File System Settings
        try
        {
            FileStream file = File.Open(Server.MapPath("~/web.config"), FileMode.Append);
            file.Close();
            SuccessMessage(lblWebConfig, "Read / Write [OK]");
        }
        catch
        {
            SuccessMessage(lblWebConfig, "Read / Write [Access Error]");
        }
        try
        {
            StreamWriter Sw = new StreamWriter(Server.MapPath("~/App_Data/accesscontrol.txt"));
            Sw.Write("please delete this file");
            Sw.Close();
            File.Delete(Server.MapPath("~/App_Data/accesscontrol.txt"));
            SuccessMessage(lblAppData, "Read / Write [OK]");
        }
        catch
        {
            SuccessMessage(lblAppData, "Read / Write [Access Error]");
        }
        try
        {
            StreamWriter Sw = new StreamWriter(Server.MapPath("~/Upload/accesscontrol.txt"));
            Sw.Write("please delete this file");
            Sw.Close();
            File.Delete(Server.MapPath("~/Upload/accesscontrol.txt"));
            SuccessMessage(lblUpload, "Read / Write [OK]");
        }
        catch
        {
            SuccessMessage(lblUpload, "Read / Write [Access Error]");
        }
    }

    void ErrorMessage(Label lbl, string strMessage)
    {
        lbl.Text = strMessage;
        lbl.Font.Bold = true;
        lbl.ForeColor = Color.Firebrick;
    }

    void SuccessMessage(Label lbl, string strMessage)
    {
        lbl.Text = strMessage;
        lbl.Font.Bold = true;
        lbl.ForeColor = Color.ForestGreen;
    }
}