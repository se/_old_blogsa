using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ConvertLineToBr
/// </summary>
[ExtensionManager.Extension("File Holder", "1", "Blogsa.net")]
public class FileHolder
{
    public FileHolder()
    {
        BSPost.Showing += new EventHandler<System.ComponentModel.CancelEventArgs>(Post_Showing);
    }

    void Post_Showing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        BSPost bsPost = (BSPost)sender;
        string strContent = bsPost.Content;
        if (bsPost.Type == PostTypes.File)
        {
            if (bsPost.Show == 0)
                bsPost.Content = "<a href=\"" + Blogsa.Url + "FileHandler.ashx?FileID=" + bsPost.PostID + "\">" + bsPost.Title + "</a><br><br>";
            else if (bsPost.Show == PostVisibleTypes.Public)
                bsPost.Content = "<img src=\"" + Blogsa.Url + "Upload/Images/" + bsPost.Title + "\"/><br><br>";
            else if (bsPost.Show == PostVisibleTypes.Custom)
                bsPost.Content = "<a href=\"" + Blogsa.Url + "FileHandler.ashx?FileID=" + bsPost.PostID + "\">" + bsPost.Title + "</a><br><br>";
            bsPost.Content += strContent;
        }
    }
}