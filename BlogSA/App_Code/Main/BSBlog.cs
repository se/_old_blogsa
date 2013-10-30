using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for BSBlog
/// </summary>
public class BSBlog
{
    private List<BSPost> _posts;


    public List<BSPost> Posts
    {
        get { return _posts; }
        set { _posts = value; }
    }

    public List<BSPost> GetPosts()
    {
        return (_posts = BSPost.GetPosts());
    }
}