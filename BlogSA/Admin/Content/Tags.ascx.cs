using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class Admin_Content_Tags : System.Web.UI.UserControl
{
    public void SaveTags(int ObjectID)
    {
        string strTags = txtTags.Value;
        Regex rex = new Regex("\\{(.*?)\\}");

        BSTerm.RemoveTo(TermTypes.Tag, ObjectID);

        foreach (Match item in rex.Matches(strTags))
        {
            Regex rx = new Regex("'(.*?)'");
            string strText = rx.Matches(item.Value)[1].Value;
            string strValue = rx.Matches(item.Value)[0].Value;

            strText = strText.Substring(1, strText.Length - 2);
            strValue = strValue.Substring(1, strValue.Length - 2);

            string code = BSHelper.CreateCode(strText);

            BSTerm bsTerm = BSTerm.GetTerm(code, TermTypes.Tag);
            if (bsTerm == null)
            {
                bsTerm = new BSTerm();
                bsTerm.Name = strText;
                bsTerm.Code = code;
                bsTerm.Type = TermTypes.Tag;
                bsTerm.Objects.Add(ObjectID);
                bsTerm.Save();
            }
            else
            {
                bsTerm.Objects.Add(ObjectID);
                bsTerm.Save();
            }
        }
    }

    public void LoadTags(int ObjectID)
    {
        txtTags.Value = "";

        List<BSTerm> terms = BSTerm.GetTermsByObjectID(ObjectID, TermTypes.Tag);

        foreach (BSTerm term in terms)
        {
            string strText = term.Name;
            string strValue = term.TermID.ToString();

            txtTags.Value += "{value='" + strValue + "',text='" + strText + "'}";
            sAutoComp.Items.Add(new ListItem(strText, strValue));

        }
    }
}
