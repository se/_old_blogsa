using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class Admin_Content_Categories : System.Web.UI.UserControl
{
    private TermTypes _TermType;
    public TermTypes TermType
    {
        get { return _TermType; }
        set { _TermType = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadData(int ObjectID)
    {
        if (!Page.IsPostBack)
        {
            List<BSTerm> terms = BSTerm.GetTerms(TermType);
            if (terms.Count > 0)
            {
                cblCats.DataSource = terms;
                cblCats.DataMember = "TermID";
                cblCats.DataTextField = "Name";
                cblCats.DataValueField = "TermID";
                cblCats.DataBind();
            }
            else
            {
                LiteralControl lC = new LiteralControl();
                lC.Text = Language.Admin["CategoryNotFound"] + "<br><br><a href=\"Categories.aspx?#Add\">" + Language.Admin["AddNewCategory"] + "</a>";
                divCats.Controls.Add(lC);
            }

            if (ObjectID != 0)
            {
                List<BSTerm> objectTerms = BSTerm.GetTermsByObjectID(ObjectID, TermType);
                foreach (BSTerm objectTerm in objectTerms)
                {
                    if (cblCats.Items.FindByValue(objectTerm.TermID.ToString()) != null)
                    {
                        cblCats.Items.FindByValue(objectTerm.TermID.ToString()).Selected = true;
                    }
                }
            }
        }
    }

    public void SaveData(int ObjectID)
    {
        using (DataProcess dp = new DataProcess())
        {
            BSTerm.RemoveTo(TermTypes.Category, ObjectID);
            for (int i = 0; i < cblCats.Items.Count; i++)
            {
                if (cblCats.Items[i].Selected == true)
                {
                    BSTerm bsTerm = BSTerm.GetTerm(Convert.ToInt32(cblCats.Items[i].Value));
                    bsTerm.Objects.Add(ObjectID);
                    bsTerm.Save();
                }
            }
        }
    }
}
