using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_CommentForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BSPlaceHolderCommentForm_Init(object sender, EventArgs e)
    {
        if (BSPost.CurrentPost.AddComment)
            ((PlaceHolder)sender).Controls.Add(LoadControl(Templates.CommentForm));
    }
}