using System;
using System.Data;

/// <summary>
/// Summary description for SendPings
/// </summary>
[ExtensionManager.Extension("Yorumlanan yazılar için \"Yeni yorum geldiğinde bana bildir.\" seçeneğini işaretliyenlere bildirim gönderir.", "1", "Blogsa.net")]
public class CommentNotifyMe
{
    public CommentNotifyMe()
    {
        BSComment.Approved += new EventHandler<EventArgs>(Comment_Approved);
    }

    void Comment_Approved(object sender, EventArgs e)
    {
        BSComment bsComment = (BSComment)sender;
        if (bsComment.Approve)
        {
            using (DataProcess dp = new DataProcess())
            {
                dp.AddParameter("PostID", bsComment.PostID);
                dp.AddParameter("NotifyMe", true);
                dp.AddParameter("Approve", true);

                dp.ExecuteReader("SELECT DISTINCT Email FROM Comments WHERE PostID=@PostID AND NotifyMe=@NotifyMe AND Approve=@Approve");
                if (dp.Return.Status == DataProcessState.Success)
                {
                    BSPost bsPost = BSPost.GetPost(bsComment.PostID);
                    using (IDataReader dr = dp.Return.Value as IDataReader)
                    {
                        while (dr.Read())
                        {
                            string strEmail = (string)dr["Email"];
                            System.Threading.ThreadPool.QueueUserWorkItem(delegate
                            {
                                BSHelper.SendMail(Language.Get["NewCommentNotice"], Blogsa.Settings["smtp_email"].ToString(), Blogsa.Settings["smtp_name"].ToString()
                                    , strEmail, "", Language.Get["NewCommentNoticeDescription"]
                                    + "<br><br><a href=\"" + bsPost.Link + "\">" + bsPost.Title + "</a>", true);
                            });
                        }
                    }
                }
            }
        }
    }
}
