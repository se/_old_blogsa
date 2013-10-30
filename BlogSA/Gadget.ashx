<%@ WebHandler Language="C#" Class="Gadget" %>

using System;
using System.Web;

public class Gadget : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (BSUser.ValidateUser(context.Request["user"].ToString(), BSHelper.GetMd5Hash(context.Request["pw"].ToString())))
        {
            context.Response.ContentType = "text/html";
            context.Response.Write(BSComment.GetComments(CommentStates.UnApproved).Count + " Onaysız<br>");
            context.Response.Write(BSPost.GetReadCounts(0) + "<br>");
            context.Response.Write(context.Application["OnlineUsers"] + " Kişi");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}