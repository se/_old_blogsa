using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_PostSettings : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Categories1.LoadData(0);

        rblDate.Items[0].Text = Language.Admin["NowPublish"];
        rblDate.Items[1].Text = Language.Admin["ChangeDateTime"];

        ddState.Items[0].Text = Language.Admin["Publish"];
        ddState.Items[1].Text = Language.Admin["Draft"];
        cblAddComment.Text = Language.Admin["CommentAdd"];
    }

    public PostStates State
    {
        get { return (PostStates)Convert.ToInt16(ddState.SelectedValue); }
    }

    public bool AddComment
    {
        get { return cblAddComment.Checked; }
    }

    public DateTime Date
    {
        get
        {
            return rblDate.SelectedValue == "1" ? dtsDateTime.SelectedDateTime : DateTime.Now;
        }
    }

    public string LanguageCode
    {
        get { return postLanguagePicker.LangaugeCode; }
        set { postLanguagePicker.LangaugeCode = value; }
    }

    public void Save(int postId)
    {
        Categories1.SaveData(postId);
        Tags1.SaveTags(postId);
    }
}