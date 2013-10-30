using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ConvertLineToBr
/// </summary>
[ExtensionManager.Extension("Yorumlarınızda bulunan satırları <br> tagları haline getirir.", "1", "Blogsa.net")]
public class ConvertLineToBr
{
    public ConvertLineToBr()
    {
        BSComment.Showing += new EventHandler<System.ComponentModel.CancelEventArgs>(Comment_Showing);
    }

    void Comment_Showing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        BSComment bsComment = (BSComment)sender;
        bsComment.Content = bsComment.Content.Replace("\n", "<br/>");
    }
}