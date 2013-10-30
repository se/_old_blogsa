using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_BSGridViewHeader : System.Web.UI.UserControl
{
    private bool _search;
    public bool Search
    {
        get { return _search; }
        set
        {
            pnlSearch.Visible = value;
            _search = value;
        }
    }

    private List<LinkButton> _buttons;


    public string SearchText
    {
        get
        {
            return tbSearchText.Text;
        }
        set { tbSearchText.Text = value; }
    }

    public LinkButton SearchButton
    {
        get
        {
            return btnSearch;
        }
        set { btnSearch = value; }
    }

    public List<LinkButton> Buttons
    {
        get
        {
            if (_buttons == null)
            {
                _buttons = new List<LinkButton>();
            }
            return _buttons;
        }
        set { _buttons = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlSearch.Visible = Search;

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons.Count > 2 && i % 2 == 0)
            {
                Literal lt = new Literal();
                lt.Text = "&nbsp;";
                pnlButtons.Controls.Add(lt);
            }
            pnlButtons.Controls.Add(Buttons[i]);
            if (Buttons.Count > 1 && i == 0)
            {
                Literal lt = new Literal();
                lt.Text = "&nbsp;";
                pnlButtons.Controls.Add(lt);
            }
        }
    }
}