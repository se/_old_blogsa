using System;
using System.Data;
using System.IO;
using System.Data.OleDb;

public partial class Setup_Access : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideDivs();
        switch ((string)Session["AccessStep"])
        {
            case "0": divSetup.Visible = true; break;
            default: divSetup.Visible = true; break;
        }
    }
    private void HideDivs()
    {
        divSetup.Visible = false;
        divError.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string strLang = Session["lang"].ToString();
        Session.Abandon();
        Response.Redirect("Default.aspx?lang=" + strLang);
    }

    protected void btnInstall_Click(object sender, EventArgs e)
    {
        string ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\{0}.mdb;",
                txtDatabaseName.Text);
        OleDbConnection OConn = new OleDbConnection(ConnectionString);
        StreamReader Sr = new StreamReader(Server.MapPath("~/Setup/Scripts/Access.sql"));
        try
        {
            File.Copy(Server.MapPath("~/Setup/Scripts/Blogsa.mdb"), Server.MapPath(string.Format("~/App_Data/{0}.mdb", txtDatabaseName.Text)));

            //Update WebSite Url
            string strUrl = Request.Url.AbsoluteUri.Substring(0
                , Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath) + (Request.ApplicationPath.Equals("/") ? 0 : Request.ApplicationPath.Length)) + "/";


            OConn.Open();
            while (!Sr.EndOfStream)
            {
                //Create DB
                string Commands = Sr.ReadLine().ToString();
                if (!Commands.StartsWith("/*"))
                {
                    OleDbCommand OComm = new OleDbCommand(Commands, OConn);
                    OComm.ExecuteNonQuery();
                    OComm.Dispose();
                }
            }

            Sr.Close();
            string strLang = (string)Session["lang"];
            string strRedirectPage = String.Format("Completed.aspx?Setup={0}&lang={1}", BSHelper.SaveWebConfig(ConnectionString, "System.Data.OleDb"), strLang);
            Response.Redirect(strRedirectPage, false);
        }
        catch (Exception ex)
        {
            BSLog l = new BSLog();
            l.CreateDate = DateTime.Now;
            l.LogType = BSLogType.Error;
            l.LogID = Guid.NewGuid();
            l.RawUrl = Request.RawUrl;
            l.Source = ex.Source;
            l.StackTrace = ex.StackTrace;
            l.TargetSite = ex.TargetSite;
            l.Url = Request.Url.ToString();
            l.Save();

            divError.Visible = true;
            lblError.Text = ex.Message;
            if (OConn.State == ConnectionState.Open)
            {
                OConn.Close();
            }
            File.Delete(Server.MapPath("~/App_Data/" + txtDatabaseName.Text));
        }
        finally
        {
            if (OConn.State == ConnectionState.Open)
                OConn.Close();
            Sr.Close();
        }
    }
}
