<%@ WebHandler Language="C#" Class="Ajax" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;

public class Ajax : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string act = context.Request.QueryString["act"];
        switch (act)
        {
            case "savesortlist": SaveSortList(context); break;
            case "saveplace": SavePlace(context); break;
            case "gettags": GetTagList(context); break;
            case "comment": CommentAction(context); break;
            case "post": PostAction(context); break;
            case "widgetposition": WidgetPosition(context); break;
            default: break;
        }
    }

    private void MenuPosition(HttpContext context)
    {
        try
        {

        }
        catch (Exception ex)
        {
        }
    }

    private void WidgetPosition(HttpContext context)
    {
        try
        {
            string strLeft = context.Request["left"];
            string strRight = context.Request["right"];
            strLeft = strLeft.Substring(0, strLeft.Length - 1);
            strLeft += "|";
            strRight = strRight.Substring(0, strRight.Length - 1);

            string value = strLeft + strRight;

            BSSetting bsSetting = BSSetting.GetSetting("widgetpositions");
            bsSetting.Value = value;

            if (bsSetting.Save())
                context.Response.Write("OK");
            else
                context.Response.Write("Error");
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
            context.Response.End();
        }
    }

    private void CommentAction(HttpContext context)
    {
        try
        {
            string strProcess = context.Request["process"];
            string CommentID = context.Request["id"];

            int iCommentID = 0;
            int.TryParse(CommentID, out iCommentID);

            if (iCommentID != 0)
            {
                Hashtable Ht = new Hashtable();

                switch (strProcess)
                {
                    case "approve":
                        BSComment.GetComment(Convert.ToInt32(CommentID)).DoApprove(true);
                        break;
                    case "unapprove":
                        BSComment.GetComment(Convert.ToInt32(CommentID)).DoApprove(false);
                        break;
                    case "delete":
                        BSComment.GetComment(Convert.ToInt32(CommentID)).Remove();
                        break;
                    default: break;
                }
                context.Response.Write("OK");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error : " + ex.Message);
        }
    }

    private void PostAction(HttpContext context)
    {
        try
        {
            string strProcess = context.Request["process"];
            string PostID = context.Request["id"];

            int iPostID = 0;

            int.TryParse(PostID, out iPostID);

            if (iPostID != 0)
            {
                BSPost bsPost = BSPost.GetPost(Convert.ToInt32(PostID));
                if (bsPost != null && Blogsa.ActiveUser.Role.Equals("editor") && Blogsa.ActiveUser.UserID == bsPost.UserID)
                {
                    if (strProcess == "draft") bsPost.State = PostStates.Draft;
                    else if (strProcess == "publish") bsPost.State = PostStates.Published;
                    else if (strProcess == "trash") bsPost.State = PostStates.Removed;
                    else if (strProcess == "delete") bsPost.Remove();
                    if (bsPost.Save())
                    {
                        context.Response.Write("OK");
                    }
                    else
                    {
                        context.Response.Write("ERROR");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error : " + ex.Message);
        }
    }

    private void SavePlace(HttpContext context)
    {
        try
        {
            string PlaceName = context.Request["placename"];
            string WidgetID = context.Request["widgetid"];

            int iWidgetID = 0;

            int.TryParse(WidgetID, out iWidgetID);

            if (iWidgetID != 0)
            {
                Hashtable htUP = new Hashtable();
                htUP.Add("PlaceHolder", PlaceName);

                BSWidget bsWidget = BSWidget.GetWidget(iWidgetID);
                bsWidget.PlaceHolder = PlaceName;

                if (bsWidget.Save())
                {
                    context.Response.Write("OK");
                }
                else
                {
                    context.Response.Write(PlaceName + WidgetID);
                }
            }
            else
            {
                context.Response.Write("Error");
            }
        }
        catch
        {

        }
    }
    public void SaveSortList(HttpContext context)
    {
        try
        {
            string sortList = context.Request.QueryString["sortlist"];
            string[] list = sortList.Split('|');
            for (int i = 0; i < list.Length - 1; i++)
            {
                BSWidget bsWidget = BSWidget.GetWidget(Convert.ToInt32(list[i]));
                bsWidget.Sort = (short)(i + 1);
                bsWidget.Save();
            }
            context.Response.Write("OK");
        }
        catch { }
    }

    public void GetTagList(HttpContext context)
    {
        string strTag = context.Request["search"];
        if (!String.IsNullOrEmpty(strTag))
        {
            string strTerms = "[";
            System.Collections.Generic.Dictionary<string, object> dic =
                new System.Collections.Generic.Dictionary<string, object>();
            dic.Add("Tag", strTag);
            List<BSTerm> terms = BSTerm.GetTermsByContainsName(strTag, TermTypes.Tag, 5);
            if (terms.Count > 0)
            {
                int i = 0;
                foreach (BSTerm term in terms)
                {
                    i++;
                    string strText = term.Name;
                    string strValue = term.TermID.ToString();
                    strTerms += String.Format("[\"{0}\",\"{1}\",\"{1}\"]", strValue, strText);
                    //strTerms += "[\"\",\"" + strText + "\"," + strValue + "]";
                    //strTerms += "{\"caption\":\"" + strText + "\",\"value\":" + strValue + "}";
                    if (i < terms.Count)
                    {
                        strTerms += ",";
                    }
                }
            }
            strTerms += "]";
            context.Response.ContentType = "text/json";
            context.Response.Write(strTerms);
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