using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Admin_Default : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BSSetting bsSettingWidgetPositions = BSSetting.GetSetting("widgetpositions");

            if (bsSettingWidgetPositions != null)
            {
                string[] strPositions = bsSettingWidgetPositions.Value.Split('|');
                if (strPositions.Length > 1)
                {
                    string[] strLeft = strPositions[0].Split(',');
                    string[] strRight = strPositions[1].Split(',');
                    for (int i = 0; i < strLeft.Length; i++)
                    {
                        try
                        {
                            if (strLeft[i].Trim() != "")
                            {
                                phLeftPanel.Controls.Add(Page.LoadControl("Widgets/" + strLeft[i] + ".ascx"));
                            }
                        }
                        catch { }

                    }
                    for (int i = 0; i < strRight.Length; i++)
                    {
                        try
                        {
                            if (strRight[i].Trim() != "")
                            {
                                phRightPanel.Controls.Add(Page.LoadControl("Widgets/" + strRight[i] + ".ascx"));
                            }
                        }
                        catch { }
                    }
                }
            }
            else
            {
                try
                {
                    phLeftPanel.Controls.Add(Page.LoadControl("Widgets/Now.ascx"));
                    phLeftPanel.Controls.Add(Page.LoadControl("Widgets/RecentComments.ascx"));
                    phLeftPanel.Controls.Add(Page.LoadControl("Widgets/HitPosts.ascx"));
                    phRightPanel.Controls.Add(Page.LoadControl("Widgets/QuickPost.ascx"));
                    phRightPanel.Controls.Add(Page.LoadControl("Widgets/BlogsaNews.ascx"));
                }
                catch { }

                BSSetting bsSetting = new BSSetting();
                bsSetting.Name = "widgetpositions";
                bsSetting.Value = "Now,RecentComments,HitPosts|QuickPost,BlogsaNews";
                bsSetting.Save();
            }
        }
        catch (System.Exception ex)
        {
            ltError.Text = ex.Message;
        }
    }
}
