using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Theme
/// </summary>
public class BSTheme
{
    private string _name;
    private string _description;
    private string _version;
    private string _author;
    private string _webSite;
    private string _updateUrl;
    private string _screenShot;
    private string _folder;
    private BSThemeSettings _settings;

    private static BSTheme _current;
    public static BSTheme Current
    {
        get
        {
            if (_current == null)
            {
                _current = Get(Blogsa.ActiveTheme);
            }

            return _current;
        }

        set { _current = value; }
    }

    public BSThemeSetting this[String settingName]
    {
        get
        {
            return _settings[settingName];
        }
    }

    public static BSTheme Get(string themeName)
    {
        using (StreamReader srXmlFile = new StreamReader(HttpContext.Current.Server.MapPath(String.Format("~/Themes/{0}/Settings.xml", themeName))))
        {
            XmlDocument _doc = new XmlDocument();

            BSThemeSettings _Settings = new BSThemeSettings();
            BSTheme _theme = new BSTheme();

            _doc.Load(srXmlFile);

            XmlNode _nodeTheme = _doc.SelectSingleNode("theme");

            _theme.Author = _nodeTheme["author"].InnerText;

            _theme.Description = _nodeTheme["description"].InnerText;

            _theme.Folder = _nodeTheme["folder"].InnerText;
            _theme.Name = _nodeTheme["name"].InnerText;
            _theme.ScreenShot = _nodeTheme["screenshot"].InnerText;
            _theme.UpdateUrl = _nodeTheme["updateurl"].InnerText;
            _theme.Version = _nodeTheme["version"].InnerText;
            _theme.WebSite = _nodeTheme["website"].InnerText;

            XmlNode _nodeSetting = _nodeTheme.SelectSingleNode("settings");

            if (_nodeSetting != null && _nodeSetting.ChildNodes.Count > 0)
            {
                XmlNodeList _nodeKeyList = _nodeSetting.SelectNodes("add");
                foreach (XmlNode node in _nodeKeyList)
                {
                    BSThemeSetting _setting = new BSThemeSetting();
                    _setting.Title = node.Attributes["title"] != null
                                         ? node.Attributes["title"].Value
                                         : String.Empty;
                    _setting.Description = node.Attributes["description"] != null
                                               ? node.Attributes["description"].Value
                                               : String.Empty;
                    _setting.Key = node.Attributes["key"] != null ? node.Attributes["key"].Value : String.Empty;
                    _setting.Value = node.Attributes["defaultValue"] != null ? node.Attributes["defaultValue"].Value : node.InnerText;

                    string typeText = node.Attributes["type"].Value.ToLowerInvariant();

                    switch (typeText)
                    {
                        case "text":
                            _setting.Type = ThemeSettingType.Text;
                            break;
                        case "number":
                            _setting.Type = ThemeSettingType.Number;
                            break;
                        case "rich":
                            _setting.Type = ThemeSettingType.Rich;
                            break;
                        case "choose":
                            _setting.Type = ThemeSettingType.Choose;
                            break;
                        case "multiline":
                            _setting.Type = ThemeSettingType.MultiLine;
                            break;
                    }

                    BSSetting _savedSetting = BSSetting.GetSetting(String.Format("{0}_{1}", _theme.Name, _setting.Key));

                    if (_savedSetting != null)
                        _setting.Value = _savedSetting.Value;

                    _Settings.Add(_setting);
                }
            }

            _theme.Settings = _Settings;

            srXmlFile.Close();

            return _theme;
        }
    }

    public static List<BSTheme> GetThemes()
    {
        List<BSTheme> themes = new List<BSTheme>();

        DirectoryInfo DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Themes/"));

        DirectoryInfo[] directories = DirInfo.GetDirectories();

        foreach (DirectoryInfo directoryInfo in directories)
        {
            BSTheme _theme = Get(directoryInfo.Name);
            if (_theme != null)
            {
                themes.Add(_theme);
            }
        }

        return themes;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Version
    {
        get { return _version; }
        set { _version = value; }
    }

    public string Author
    {
        get { return _author; }
        set { _author = value; }
    }

    public string WebSite
    {
        get { return _webSite; }
        set { _webSite = value; }
    }

    public string UpdateUrl
    {
        get { return _updateUrl; }
        set { _updateUrl = value; }
    }

    public string ScreenShot
    {
        get { return _screenShot; }
        set { _screenShot = value; }
    }

    public string Folder
    {
        get { return _folder; }
        set { _folder = value; }
    }

    public BSThemeSettings Settings
    {
        get { return _settings; }
        set { _settings = value; }
    }

    public BSWidgets Widgets
    {
        get
        {
            _widgets = new BSWidgets();
            DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(String.Format("~/Themes/{0}/Widgets/", Current.Folder)));
            if (directory.Exists)
            {
                FileInfo[] widgetFiles = directory.GetFiles("Widget.xml", SearchOption.AllDirectories);
                foreach (FileInfo widgetFile in widgetFiles)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(widgetFile.FullName);

                    XmlNode nodeName = doc.SelectSingleNode("/widget/name");
                    XmlNode nodeTitle = doc.SelectSingleNode("/widget/title");
                    XmlNode nodeDescription = doc.SelectSingleNode("/widget/description");
                    XmlNode nodeFolder = doc.SelectSingleNode("/widget/folder");
                    XmlNode nodeType = doc.SelectSingleNode("/widget/type");

                    BSWidget w = new BSWidget();
                    if (nodeName != null) w.Name = nodeName.InnerText;
                    if (nodeDescription != null) w.Description = nodeDescription.InnerText;
                    if (nodeTitle != null) w.Title = nodeTitle.InnerText;
                    if (nodeFolder != null) w.FolderName = nodeFolder.InnerText;
                    if (nodeType != null) w.Type = nodeType.InnerText.Equals("page") ? WidgetTypes.Page : WidgetTypes.Normal;

                    w.Path = Path.Combine(directory.FullName, w.FolderName);

                    _widgets.Add(w);
                }
            }

            return _widgets;
        }
        set { _widgets = value; }
    }

    private BSWidgets _widgets;

}