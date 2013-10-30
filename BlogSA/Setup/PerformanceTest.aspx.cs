using System;

public partial class PerformanceTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreatePost_Click(object sender, EventArgs e)
    {
        int iCount = Convert.ToInt32(tbCount.Text);
        for (int i = 0; i < iCount; i++)
        {
            BSPost post = new BSPost();
            post.Title = String.Format("{0}-{1}", tbTitle.Text, i + 1);
            post.Content = tbContent.Text;
            post.AddComment = true;
            post.Code = BSHelper.CreateCode(post.Title);
            post.Date = DateTime.Now;
            post.Type = PostTypes.Article;
            post.UserID = 1;
            post.State = PostStates.Published;
            post.Save();
        }
    }
}