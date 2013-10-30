<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;

public class FileHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            int iFileID = 0;
            int.TryParse(context.Request["FileID"], out iFileID);

            bool bFileLoaded = false;

            if (iFileID > 0)
            {
                BSPost bsPost = BSPost.GetPost(iFileID);
                if (bsPost.Type == PostTypes.File)
                {
                    string path = string.Empty;
                    if (bsPost.Show == PostVisibleTypes.Hidden)
                        path = "~/Upload/Files";
                    else if (bsPost.Show ==  PostVisibleTypes.Public)
                        path = "~/Upload/Images";
                    else if (bsPost.Show == PostVisibleTypes.Custom)
                        path = "~/Upload/Medias";
                    if (string.IsNullOrEmpty(path))
                        context.Response.Write("File not found!");
                    else
                    {
                        string file = context.Server.MapPath(System.IO.Path.Combine(path, bsPost.Title));
                        if (System.IO.File.Exists(file))
                        {
                            bsPost.ReadCount++;
                            bsPost.Save();
                            bFileLoaded = true;
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + bsPost.Title);
                            context.Response.TransmitFile(file);
                        }
                    }
                }
            }

            if (!bFileLoaded)
            {
                context.Response.Write("File not found!");             
            }
        }
        catch
        {
            context.Response.Write("File not found!");
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