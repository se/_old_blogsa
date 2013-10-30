<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

public class Upload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string strReturnValue = string.Empty;
        try
        {
            if (context.Request.Files.Count > 0 && Blogsa.ActiveUser.Role.Equals("admin"))
            {
                foreach (string strKey in context.Request.Files)
                {
                    System.Collections.Generic.Dictionary<string, string> dicFileTypeToFolder = new System.Collections.Generic.Dictionary<string, string>();
                    dicFileTypeToFolder.Add(".jpg", "Images");
                    dicFileTypeToFolder.Add(".jpeg", "Images");
                    dicFileTypeToFolder.Add(".tif", "Images");
                    dicFileTypeToFolder.Add(".png", "Images");
                    dicFileTypeToFolder.Add(".gif", "Images");
                    dicFileTypeToFolder.Add(".raw", "Images");
                    dicFileTypeToFolder.Add(".bmp", "Images");
                    dicFileTypeToFolder.Add(".avi", "Medias");
                    dicFileTypeToFolder.Add(".mpg", "Medias");
                    dicFileTypeToFolder.Add(".mpeg", "Medias");
                    dicFileTypeToFolder.Add(".mp4", "Medias");
                    dicFileTypeToFolder.Add(".flv", "Medias");
                    dicFileTypeToFolder.Add(".wmv", "Medias");
                    dicFileTypeToFolder.Add(".mp3", "Medias");
                    dicFileTypeToFolder.Add(".wav", "Medias");
                    HttpPostedFile file = (HttpPostedFile)context.Request.Files[strKey];
                    string strExtension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
                    string strFileName = System.IO.Path.GetFileName(file.FileName);
                    string strUploadPath = string.Empty;
                    if (dicFileTypeToFolder.ContainsKey(strExtension))
                    {
                        strUploadPath = context.Server.MapPath("~/Upload/" + dicFileTypeToFolder[strExtension] + "/");
                    }
                    else
                        strUploadPath = context.Server.MapPath("~/Upload/Files/");
                    if (!System.IO.Directory.Exists(strUploadPath))
                    {
                        System.IO.Directory.CreateDirectory(strUploadPath);
                    }
                    file.SaveAs(System.IO.Path.Combine(strUploadPath, strFileName));

                    if (dicFileTypeToFolder.ContainsKey(strExtension) && dicFileTypeToFolder[strExtension] == "Images")
                    {
                        if (Convert.ToBoolean(Blogsa.Settings["library_usethumbnail"].Value))
                        {
                            string thumbnailPath =
                                context.Server.MapPath("~/Upload/" + dicFileTypeToFolder[strExtension] + "/_t/");

                            if (!Directory.Exists(thumbnailPath))
                            {
                                Directory.CreateDirectory(thumbnailPath);
                            }

                            string thumbnailWebPath =
                                context.Server.MapPath("~/Upload/" + dicFileTypeToFolder[strExtension] + "/_w/");

                            if (!Directory.Exists(thumbnailWebPath))
                            {
                                Directory.CreateDirectory(thumbnailWebPath);
                            }


                            System.Drawing.Image original_image = System.Drawing.Image.FromStream(file.InputStream);

                            int target_width = Convert.ToInt32(Blogsa.Settings["library_thumbnail_width"].Value);
                            int target_height = Convert.ToInt32(Blogsa.Settings["library_thumbnail_height"].Value);

                            int target_width_web = Convert.ToInt32(Blogsa.Settings["library_thumbnail_web_width"].Value);
                            int target_height_web = Convert.ToInt32(Blogsa.Settings["library_thumbnail_web_height"].Value);

                            int new_width = 0;
                            int new_height = 0;

                            int new_width_web = 0;
                            int new_height_web = 0;

                            double target_ratio = target_width / target_height;
                            double image_ratio = original_image.Width / original_image.Height;

                            if (target_ratio > image_ratio)
                            {
                                new_height = target_height;
                                new_width = (int)Math.Floor(image_ratio * target_height);

                                new_height_web = target_height_web;
                                new_width_web = (int)Math.Floor(image_ratio * target_height_web);
                            }
                            else
                            {
                                new_width = target_width;
                                new_height = (int)Math.Floor(target_width / image_ratio);

                                new_width = target_width_web;
                                new_height = (int)Math.Floor(target_width_web / image_ratio);
                            }

                            System.Drawing.Image thumbnailImage = original_image.GetThumbnailImage(target_width, target_height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
                            System.Drawing.Image thumbnailWebImage = original_image.GetThumbnailImage(target_width_web, target_height_web, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

                            thumbnailImage.Save(Path.Combine(thumbnailPath, strFileName), ImageFormat.Jpeg);
                            thumbnailWebImage.Save(Path.Combine(thumbnailWebPath, strFileName), ImageFormat.Jpeg);
                        }
                    }

                    // Add Media
                    BSPost bsPost = new BSPost();
                    bsPost.Title = strFileName;
                    bsPost.Content = "";
                    bsPost.Date = DateTime.Now;
                    bsPost.LanguageCode = System.Globalization.CultureInfo.InvariantCulture.TwoLetterISOLanguageName;
                    bsPost.ReadCount = 0;
                    bsPost.State = 0;
                    bsPost.Show = (PostVisibleTypes)(short)
                        (dicFileTypeToFolder.ContainsKey(strExtension) ?
                        (dicFileTypeToFolder[strExtension].Equals("Images") ? FileTypes.Image : FileTypes.Media) : FileTypes.File);
                    bsPost.Type = PostTypes.File;
                    bsPost.UserID = Blogsa.ActiveUser.UserID;
                    bsPost.Code = strExtension.Replace(".", "");
                    bool bStatus = bsPost.Save();

                    string thumbnailUrl = String.Empty;

                    if ((FileTypes)bsPost.Show == FileTypes.Image)
                    {
                        thumbnailUrl = "../Upload/Images/_t/" + bsPost.Title;
                    }
                    else
                    {
                        thumbnailUrl = "Images/FileTypes/file.png";
                    }

                    strReturnValue = "[{'url':'Library.aspx?FileID=" + bsPost.PostID + "','status':" + bStatus.ToString().ToLowerInvariant()
                        + ",'thumbnailUrl':'" + thumbnailUrl + "'}]";
                }
            }
        }
        catch (Exception ex)
        {
            strReturnValue = "[{'error':'" + ex.Message + "'}]";
        }
        context.Response.Write(strReturnValue);
    }

    private bool ThumbnailCallback()
    {
        return true;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}