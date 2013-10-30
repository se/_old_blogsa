using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using System.Threading;

/// <summary>
/// Summary description for SendPings
/// </summary>
[ExtensionManager.Extension("Yazılarınızı otomatik olarak Google ve benzeri sitelere Ping atar.", "1", "Blogsa.net")]
public class SendPings
{
    string[] strPingList = new string[] { 
        "http://blogsearch.google.com/ping/RPC2", 
        "http://api.my.yahoo.com/rpc2", 
        "http://ping.feedburner.com",
        "http://rpc.pingomatic.com/rpc2", 
        "http://rpc.pingthesemanticweb.com/",
        "http://rpc.technorati.com/rpc/ping",
        "http://services.newsgator.com/ngws/xmlrpcping.aspx" ,
        "http://ping.feedburner.com" ,
        "http://www.bloglines.com/ping" 
    };

    public SendPings()
    {
        BSPost.Saved += new EventHandler<EventArgs>(Post_Saved);
    }

    void Post_Saved(object sender, EventArgs e)
    {
        BSPost bsPost = ((BSPost)sender);
        for (int i = 0; i < strPingList.Length; i++)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strPingList[i]);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ProtocolVersion = HttpVersion.Version11;
                req.Headers["Accept-Language"] = "en-us";
                AddRequestXml(bsPost.Link, req);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                resp.Close();
            }
            catch { }
        }
    }

    private void AddRequestXml(string url, HttpWebRequest req)
    {
        Stream stream = (Stream)req.GetRequestStream();
        using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("methodCall");
            writer.WriteElementString("methodName", "pingback.ping");
            writer.WriteStartElement("params");

            writer.WriteStartElement("param");
            writer.WriteStartElement("value");
            writer.WriteElementString("string", Blogsa.Url);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("param");
            writer.WriteStartElement("value");
            writer.WriteElementString("string", url);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
