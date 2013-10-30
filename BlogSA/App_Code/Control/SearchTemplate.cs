using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for SearchTemplate
/// </summary>
public class SearchTemplate : UserControl
{
    protected BSSearchItem SearchItem
    {
        get { return (BSSearchItem)((RepeaterItem)base.NamingContainer).DataItem; }
    }
    public override void DataBind()
    {
        base.DataBind();
    }
}