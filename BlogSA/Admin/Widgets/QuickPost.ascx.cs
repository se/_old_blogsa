using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Admin_Widgets_QuickPost : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSavePublish_Click(object sender, EventArgs e)
    {
        SavePost(PostStates.Published);
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        SavePost(PostStates.Draft);
    }

    private void SavePost(PostStates state)
    {
        BSPost bsPost = new BSPost();
        bsPost.UserID = Blogsa.ActiveUser.UserID;
        bsPost.Title = txtPostTitle.Text;
        bsPost.Code = BSHelper.CreateCode(txtPostTitle.Text);
        bsPost.Content = txtPostContent.Text;
        bsPost.State = state;
        bsPost.Date = DateTime.Now;

        if (bsPost.Save())
        {
            SaveTags(bsPost.PostID);
            Response.Redirect("Posts.aspx?PostID=" + bsPost.PostID + "&Message=1");
        }
    }

    private void SaveTags(int iPostID)
    {
        string[] strTags = txtPostTags.Text.Split(',');
        for (int i = 0; i < strTags.Length; i++)
        {
            if (strTags[i].Trim() != "")
            {
                string code = BSHelper.CreateCode(strTags[i].Trim());
                string name = strTags[i].Trim();

                BSTerm bsTerm = BSTerm.GetTerm(code, TermTypes.Tag);
                if (bsTerm== null)
                {
                    bsTerm = new BSTerm();
                    bsTerm.Name = name;
                    bsTerm.Code = code;
                    bsTerm.Type = TermTypes.Tag;
                    bsTerm.Save();
                }

                bsTerm.Objects.Add(iPostID);
                bsTerm.Save();
            }
        }
    }
}
