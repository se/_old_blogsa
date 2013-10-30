<%@ WebHandler Language="C#" Class="Captcha" %>

using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        // Create a new 32-bit bitmap image.

        int width = 100;
        int height = 30;
        string text = BSHelper.GetRandomStr(4).ToUpperInvariant();

        HttpContext.Current.Session["SecurityCode" + HttpContext.Current.Request["Unique"]] = text;

        Random random = new Random();

        Bitmap bitmap = new Bitmap(
          width,
          height,
          PixelFormat.Format32bppArgb);

        // Create a graphics object for drawing.

        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, width, height);

        // Fill in the background.

        HatchBrush hatchBrush = new HatchBrush(
          HatchStyle.SmallConfetti,
          Color.LightGray,
          Color.White);
        g.FillRectangle(hatchBrush, rect);

        // Set up the text font.

        SizeF size;
        float fontSize = rect.Height + 1;
        Font font;
        // Adjust the font size until the text fits within the image.

        do
        {
            fontSize--;
            font = new Font(
              "Verdana",
              fontSize,
              FontStyle.Bold);
            size = g.MeasureString(text, font);
        } while (size.Width > rect.Width);

        // Set up the text format.

        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;

        // Create a path using the text and warp it randomly.

        GraphicsPath path = new GraphicsPath();
        path.AddString(
          text,
          font.FontFamily,
          (int)font.Style,
          font.Size, rect,
          format);
        float v = 4F;
        PointF[] points =
      {
        new PointF(
          random.Next(rect.Width) / v,
          random.Next(rect.Height) / v),
        new PointF(
          rect.Width - random.Next(rect.Width) / v,
          random.Next(rect.Height) / v),
        new PointF(
          random.Next(rect.Width) / v,
          rect.Height - random.Next(rect.Height) / v),
        new PointF(
          rect.Width - random.Next(rect.Width) / v,
          rect.Height - random.Next(rect.Height) / v)
      };
        Matrix matrix = new Matrix();
        matrix.Translate(0F, 0F);
        path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

        // Draw the text.

        hatchBrush = new HatchBrush(
          HatchStyle.LargeConfetti,
          Color.LightGray,
          Color.DarkGray);
        g.FillPath(hatchBrush, path);

        // Add some random noise.

        int m = Math.Max(rect.Width, rect.Height);
        for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
        {
            int x = random.Next(rect.Width);
            int y = random.Next(rect.Height);
            int w = random.Next(m / 50);
            int h = random.Next(m / 50);
            g.FillEllipse(hatchBrush, x, y, w, h);
        }

        // Clean up.

        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();

        // Set the image.


        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        byte[] bmpBytes = ms.GetBuffer();
        bitmap.Dispose();
        ms.Close();
        context.Response.BinaryWrite(bmpBytes);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}