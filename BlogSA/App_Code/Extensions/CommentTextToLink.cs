using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for CommentTextToLink
/// </summary>
[ExtensionManager.Extension("Yorumlarınızda bulunan linkleri tıklanabilir hale getirir.", "1", "Blogsa.net")]
public class CommentTextToLink
{
    public CommentTextToLink()
    {
        BSComment.Showing += new EventHandler<System.ComponentModel.CancelEventArgs>(Comment_Showing);
    }

    void Comment_Showing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        BSComment bsComment = (BSComment)sender;
        #region Find Url
        string urlPattern = @"\b([\d\w\.\/\+\-\?\:]*)((ht|f)tp(s|)\:\/\/|[\d\d\d|\d\d]\.[\d\d\d|\d\d]\.|www\.|\.tv|\.ac|\.com|\.edu|\.gov|\.int|\.mil|\.net|\.org|\.biz|\.info|\.name|\.pro|\.museum|\.co)([\d\w\.\/\%\+\-\=\&amp;\?\:\\\&quot;\'\,\|\~\;]*)\b";
        Regex RegExp = new Regex(urlPattern, RegexOptions.Compiled);
        foreach (Match val in RegExp.Matches(bsComment.Content))
        {
            bsComment.Content = bsComment.Content.Replace(val.Value, "<a href=\"" + val.Value + "\">" + val.Value + "</a>");
        }
        #endregion
    }
}