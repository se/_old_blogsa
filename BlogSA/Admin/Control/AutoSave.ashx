<%@ WebHandler Language="C#" Class="AutoSave" %>

using System;
using System.Web;
using System.Collections;
using System.Data;

public class AutoSave : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string act = context.Request.QueryString["act"];
        switch (act)
        {
            case "savedraft": SaveDraft(context); break;
            default: break;
        }
    }

    private void SaveDraft(HttpContext context)
    {
        BSPost bsPost = new BSPost();
        bsPost.Title = context.Request.Form["Title"];
        bsPost.Content = context.Request.Form["Content"];
        bsPost.UserID = Blogsa.ActiveUser.UserID;
        bsPost.Type = PostTypes.AutoSave;
        bsPost.Save();
        context.Response.Write(DateTime.Now);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}