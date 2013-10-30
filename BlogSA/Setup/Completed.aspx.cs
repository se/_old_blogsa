using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public partial class Setup_Completed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        divStepBeforeInstalled.Visible = false;
        divStepCompleate.Visible = false;

        Guid gSetupKey = Guid.Empty;
        try
        {
            String strSetupKey = Request["Setup"];
            if (!String.IsNullOrEmpty(strSetupKey))
                gSetupKey = new Guid(strSetupKey);
        }
        catch
        {
        }

        if (Blogsa.IsInstalled && Blogsa.InstallKey == gSetupKey)
        {
            SaveAllData();
        }

        if (!String.IsNullOrEmpty((string)Session["Password"]))
        {
            divStepCompleate.Visible = true;
            ltCompleate.Text = Language.Setup["Compleate"].Replace("{Password}", (string)Session["Password"]);
        }
        else if (!Blogsa.IsInstalled)
            Response.Redirect("Default.aspx");
        else
            divStepBeforeInstalled.Visible = true;
    }

    private void SaveAllData()
    {
        BSData dataSelectedLang = SerializeData((string)Session["lang"]);
        BSData dataInvariantLang = SerializeData("iv");

        if (dataSelectedLang != null)
        {
            if (dataSelectedLang.Settings == null)
                dataSelectedLang.Settings = new List<BSSetting>();

            if (dataInvariantLang.Settings != null)
            {
                foreach (BSSetting setting in dataInvariantLang.Settings)
                {
                    if (!dataSelectedLang.Settings.Contains(setting))
                        dataSelectedLang.Settings.Add(setting);
                }
            }

            if (dataSelectedLang.Sites != null)
                foreach (BSSite bsSite in dataSelectedLang.Sites)
                    bsSite.Save();

            if (dataSelectedLang.Settings != null)
                foreach (BSSetting bsSetting in dataSelectedLang.Settings)
                    bsSetting.Save();

            if (dataSelectedLang.Posts != null)
                foreach (BSPost bsPost in dataSelectedLang.Posts)
                    bsPost.Save();

            if (dataSelectedLang.MenuGroups != null)
                foreach (BSMenuGroup bsMenuGroup in dataSelectedLang.MenuGroups)
                    bsMenuGroup.Save();

            if (dataSelectedLang.Menus != null)
                foreach (BSMenu bsMenu in dataSelectedLang.Menus)
                    bsMenu.Save();

            if (dataSelectedLang.Users != null)
                foreach (BSUser bsUser in dataSelectedLang.Users)
                    bsUser.Save();

            if (dataSelectedLang.Widgets != null)
                foreach (BSWidget bsWidget in dataSelectedLang.Widgets)
                    bsWidget.Save();

            if (dataSelectedLang.Links != null)
                foreach (BSLink bsLink in dataSelectedLang.Links)
                    bsLink.Save();

            if (dataSelectedLang.Terms != null)
                foreach (BSTerm bsTerm in dataSelectedLang.Terms)
                    bsTerm.Save();

            Blogsa.Settings = null;
        }

        if (Session["Password"] == null)
        {
            string password = BSHelper.GetRandomStr(8);
            BSUser user = BSUser.GetUserByUserName("admin");
            user.Password = BSHelper.GetMd5Hash(password);
            user.Save();
            Session["Password"] = password;
        }

        Session["Step"] = "Finish";
        Response.Redirect("Completed.aspx");
    }

    private BSData SerializeData(string p)
    {
        XmlRootAttribute root = new XmlRootAttribute();
        root.ElementName = "Data";
        root.IsNullable = true;
        XmlSerializer dataSerializer = new XmlSerializer(typeof(BSData), root);
        using (TextReader reader = new StreamReader(Server.MapPath(String.Format("~/Setup/Data/{0}.xml", p))))
        {
            BSData data = (BSData)dataSerializer.Deserialize(reader);
            return data;
        }
    }
}