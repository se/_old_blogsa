<%@ WebHandler Language="C#" Class="ActionHandler" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class ActionResult
{
    public bool Success;
    public object Result;
    public string Error;
    public string StackTrace;
}

public class ActionHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";

        string p = context.Request["p"];
        string id = context.Request["id"];

        int iId = 0;
        int.TryParse(id, out iId);

        switch (p)
        {
            case "approve_comment":
                ApproveComment(iId, true);
                break;
            case "unapprove_comment":
                ApproveComment(iId, false);
                break;
            default:
                break;

        }
    }

    private void ApproveComment(int iId, bool bApprove)
    {
        BSComment comment = BSComment.GetComment(iId);
        if (comment == null)
        {
            Result(false, null, "Comment not found!", String.Empty);
        }
        else
        {
            Result(comment.DoApprove(bApprove), BSComment.GetComments(CommentStates.UnApproved).Count, String.Empty, String.Empty);
        }
    }

    private void Result(bool bSuccess, object oResult, string strError, string strStackTrace)
    {
        ActionResult ar = new ActionResult();
        ar.Success = bSuccess;
        ar.Result = oResult;
        ar.Error = strError;
        ar.StackTrace = strStackTrace;
        string returnValue = JsonConvert.SerializeObject(ar);
        HttpContext.Current.Response.Write(returnValue);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}