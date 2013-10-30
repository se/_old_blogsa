using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Contents_RelatedPosts : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<BSPost> posts = new List<BSPost>();

            using (DataProcess dp = new DataProcess())
            {
                dp.AddParameter("PostID", BSPost.CurrentPost.PostID);
                dp.AddParameter("Title", BSPost.CurrentPost.Title);
                dp.ExecuteReader("SELECT TOP 5 * FROM Posts WHERE [PostID]<>@PostID AND [Type]=0 AND [State]=1 AND ([Title] Like '%'+@Title+'%' OR Content Like '%'+@Title+'%') ORDER BY [CreateDate] DESC");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    using (IDataReader dr = dp.Return.Value as IDataReader)
                    {
                        while (dr.Read())
                        {
                            BSPost bsPost = new BSPost();
                            BSPost.FillPost(dr, bsPost);
                            posts.Add(bsPost);
                        }
                    }
                }
            }

            if (posts.Count > 0)
            {
                rpRelatedPosts.DataSource = posts;
                rpRelatedPosts.DataBind();
            }
            else
                this.Visible = false;
        }
        catch (System.Exception ex)
        {
        }
    }
}
