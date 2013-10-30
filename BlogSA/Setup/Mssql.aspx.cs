using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public partial class Setup_Mssql : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideDivs();
        switch ((string)Session["Type"])
        {
            case "ToWeb":
                divSetup.Visible = true;
                break;
            case "ToLocal":
                divSetup.Visible = true;
                break;
            default: divMain.Visible = true; break;
        }
        rblInstallMethod.Items[0].Text = Language.Setup["ToWeb"];
        rblInstallMethod.Items[1].Text = Language.Setup["ToLocal"];
    }
    private void HideDivs()
    {
        divMain.Visible = false;
        divSetup.Visible = false;
        divError.Visible = false;
    }
    protected void btnStep1_Click(object sender, EventArgs e)
    {
        Session["Type"] = rblInstallMethod.SelectedValue;
        Response.Redirect("Mssql.aspx");
    }
    protected void btnMssqlSetup_Click(object sender, EventArgs e)
    {
        if ((string)Session["Type"] == "ToWeb")
        {
            try
            {
                string ConnectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Integrated Security={4};";
                ConnectionString = string.Format(ConnectionString, txtWebServer.Text, txtWebCatalog.Text,
                    txtWebUser.Text, txtWebPass.Text, cbTrusted.Checked);
                SqlConnection SConn = new SqlConnection(ConnectionString);
                SConn.Open();
                SConn.Close();
                if (Install(ConnectionString, ""))
                {
                    string strLang = Session["lang"].ToString();
                    BSHelper.SaveWebConfig(ConnectionString, "System.Data.SqlClient");
                    Session["SetupCompleated"] = "OK";
                    Session["Step"] = "Settings";
                    Response.Redirect("Default.aspx?Setup=OK&lang=" + strLang);
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        else
        {
            string ConnectionString = "Data Source={0};User ID={1};Password={2};Integrated Security={3};";
            ConnectionString = string.Format(ConnectionString, txtWebServer.Text, txtWebUser.Text,
                txtWebPass.Text, cbTrusted.Checked);
            SqlConnection SConn = new SqlConnection(ConnectionString);
            SqlCommand SComm = new SqlCommand("CREATE DATABASE [" + txtWebCatalog.Text + "]", SConn);
            try
            {
                SConn.Open();
                SComm.ExecuteNonQuery();

                if (Install(ConnectionString, string.Format("USE [{0}]\n", txtWebCatalog.Text)))
                {
                    string strLang = (string)Session["lang"];
                    Response.Redirect("Completed.aspx?Setup=" + BSHelper.SaveWebConfig(ConnectionString + string.Format("Initial Catalog={0};", txtWebCatalog.Text), "System.Data.SqlClient") + "&lang=" + strLang);
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
            finally
            {
                if (SConn.State == ConnectionState.Open)
                {
                    SConn.Close();
                }
            }
        }
    }

    private bool Install(string ConnectionString, string AddDatabase)
    {
        try
        {
            SqlConnection SConn = new SqlConnection(ConnectionString);
            StreamReader Sr = new StreamReader(Server.MapPath("~/Setup/Scripts/mssql2005.sql"), Encoding.UTF8);
            string Commands = Sr.ReadToEnd();
            Commands = AddDatabase + Commands;
            Sr.Close();
            SConn.Open();
            SqlCommand SComm = new SqlCommand(Commands, SConn);
            SComm.ExecuteNonQuery();

            if (SConn.State == ConnectionState.Open)
                SConn.Close();
            return true;
        }
        catch (System.Exception ex)
        {
            lblError.Text = ex.Message;
            divError.Visible = true;
            return false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string strLang = Session["lang"].ToString();
        Session.Abandon();
        Response.Redirect("Default.aspx?lang=" + strLang);
    }
    protected void btnMssqlGoBack_Click(object sender, EventArgs e)
    {
        Session["Type"] = null;
        Response.Redirect("Mssql.aspx");
    }
}
