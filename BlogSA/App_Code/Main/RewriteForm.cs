using System.Web;
using System.Web.UI;

public class RewriteForm : HtmlTextWriter {

    public RewriteForm(Html32TextWriter writer)
        : base(writer) {
        this.InnerWriter = writer.InnerWriter;
    }

    public RewriteForm(System.IO.TextWriter writer)
        : base(writer) {
        this.InnerWriter = writer;
    }

    public override void WriteAttribute(string name, string value, bool fEncode) {
        if (name == "action") {
            if (HttpContext.Current.Items["ActionAlreadyWritten"] == null) {
                value = HttpContext.Current.Request.RawUrl;
                HttpContext.Current.Items["ActionAlreadyWritten"] = true;
            }
        }
        base.WriteAttribute(name, value, fEncode);
    }


}
