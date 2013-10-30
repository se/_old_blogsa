using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Serialization;

[XmlRoot("Data")]
[XmlType("Data")]
public class BSData
{
    private List<BSSite> _sites;
    private List<BSLink> _links;
    private List<BSPost> _posts;
    private List<BSSetting> _settings;
    private List<BSTerm> _terms;
    private List<BSMenu> _menus;
    private List<BSMenuGroup> _menuGroups;
    private List<BSUser> _users;
    private List<BSWidget> _widgets;

    public List<BSSite> Sites
    {
        get { return _sites; }
        set { _sites = value; }
    }

    public List<BSPost> Posts
    {
        get { return _posts; }
        set { _posts = value; }
    }

    public List<BSSetting> Settings
    {
        get { return _settings; }
        set { _settings = value; }
    }

    public List<BSMenu> Menus
    {
        get { return _menus; }
        set { _menus = value; }
    }

    public List<BSMenuGroup> MenuGroups
    {
        get { return _menuGroups; }
        set { _menuGroups = value; }
    }

    public List<BSUser> Users
    {
        get { return _users; }
        set { _users = value; }
    }

    public List<BSWidget> Widgets
    {
        get { return _widgets; }
        set { _widgets = value; }
    }

    public List<BSLink> Links
    {
        get { return _links; }
        set { _links = value; }
    }

    public List<BSTerm> Terms
    {
        get { return _terms; }
        set { _terms = value; }
    }
}