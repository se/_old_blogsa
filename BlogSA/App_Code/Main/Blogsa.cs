using System.Configuration;
using System.Web;
using System;
using System.Text.RegularExpressions;

public class Blogsa
{
    static Blogsa() { }

    public static string Version
    {
        get { return "1.2.0.0.8.Beta"; }
    }

    private static string _lastVersion;

    public static string LastVersion
    {
        get
        {
            if (_lastVersion == null)
            {
                try
                {
                    /*using (WebClient Wc = new WebClient())
                    {
                        Wc.QueryString.Add("p", "checkversion");
                        Wc.QueryString.Add("code", "Blogsa");
                        Wc.QueryString.Add("email", Blogsa.Settings["smtp_email"].ToString());
                        Wc.QueryString.Add("host", HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath);
                        _LastVersion = Wc.DownloadString("http://www.blogsa.net/Services/VersionControl.ashx");
                    }*/
                }
                catch (System.Exception ex)
                {
                    _lastVersion = ex.Message;
                    _lastVersion = Language.Admin["VersionError"];
                }
            }
            return _lastVersion;
        }
    }

    public static string LatestVersion
    {
        get { return "Beta"; }
    }

    public static BSUser ActiveUser
    {
        get
        {
            return HttpContext.Current.Session == null ? null : (BSUser)HttpContext.Current.Session["ActiveUser"];
        }
        set { HttpContext.Current.Session["ActiveUser"] = value; }
    }

    /// <summary>
    /// Blog Url
    /// </summary>
    public static string Url
    {
        get
        {
            string url = HttpContext.Current.Request.Url.ToString();
            string path = HttpContext.Current.Request.Path;

            url = url.Substring(0, url.IndexOf(path));

            url += HttpContext.Current.Request.ApplicationPath;

            if (!url.EndsWith("/"))
                url += "/";

            return url;
        }
    }

    /// <summary>
    /// Theme Folder Url (etc. http://blog_url/Themes/Classic/)
    /// </summary>
    public static string ThemeUrl
    {
        get { return Url + "Themes/" + ActiveTheme + "/"; }
    }

    private static BSSettings _settings;
    /// <summary>
    /// Current Blog Settings
    /// </summary>
    public static BSSettings Settings
    {
        get
        {
            if (_settings == null)
            {
                _settings = BSSetting.GetSettings();
                RefreshBlogLanguage(DefaultBlogLanguage);
                RefreshBlogAdminLanguage(DefaultBlogAdminLanguage);
            }
            return _settings;
        }
        set
        {
            _settings = value;
        }
    }

    /// <summary>
    /// Current Theme
    /// </summary>
    public static string ActiveTheme
    {
        get
        {
            return Settings["theme"].Value;
        }
        set
        {
            Settings["theme"].Value = value;
        }
    }

    /// <summary>
    /// Main Menu
    /// </summary>
    public static BSMenuGroup MainMenu
    {
        get { return BSMenuGroup.GetDefault(); }
    }

    /// <summary>
    /// Url Extension (etc. aspx,html)
    /// </summary>
    public static string UrlExtension
    {
        get
        {
            Regex rx = new Regex(".([A-Z0-9a-z-]+)$");
            string strExpression = Blogsa.Settings["permaexpression"] != null ? Blogsa.Settings["permaexpression"].Value : string.Empty;
            Match matchExt = rx.Match(strExpression);
            if (rx.Match(strExpression).Success)
                return matchExt.Value;
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// Is BlogSA Installed
    /// </summary>
    public static bool IsInstalled
    {
        get
        {
            return InstallKey != Guid.Empty;
        }
    }

    private static Guid _installKey = Guid.Empty;

    /// <summary>
    /// Setup Install Key
    /// </summary>
    public static Guid InstallKey
    {
        get
        {
            try
            {
                if (_installKey == Guid.Empty)
                    _installKey = new Guid(ConfigurationManager.AppSettings["Setup"]);
                return _installKey;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }

    /// <summary>
    /// Blog Title
    /// </summary>
    public static string Title
    {
        get { return Settings["blog_name"].Value; }
    }

    /// <summary>
    /// Blog Title Description
    /// </summary>
    public static string Description
    {
        get { return Settings["blog_description"].Value; }
    }

    /// <summary>
    /// Log status. Opened = true , Closed = false
    /// </summary>
    public static bool Log
    {
        get
        {
            BSSetting bsSetting = Blogsa.Settings["log"];
            if (bsSetting == null)
            {
                bsSetting = new BSSetting();
                bsSetting.Name = "log";
                bsSetting.Value = bool.TrueString;
                bsSetting.Main = true;
                bsSetting.Visible = false;
                bsSetting.Save();
            }

            return Convert.ToBoolean(bsSetting.Value);
        }
    }

    /// <summary>
    /// Blogsa Multi Language support.
    /// </summary>
    public static bool MutliLanguage
    {
        get { return Settings["multilanguage"].Value.Equals("1"); }
    }

    /// <summary>
    /// Default Blog Language
    /// </summary>
    public static string DefaultBlogLanguage
    {
        get { return Settings["language"].Value; }
    }

    /// <summary>
    /// Default Admin Language
    /// </summary>
    public static string DefaultBlogAdminLanguage
    {
        get { return Settings["admin_language"].Value; }
    }

    /// <summary>
    /// Current Blog Language
    /// </summary>
    public static string CurrentBlogLanguage
    {
        get
        {
            if (!String.IsNullOrEmpty((String)HttpContext.Current.Session["lang"]))
            {
                return (String)HttpContext.Current.Session["lang"];
            }
            return DefaultBlogLanguage;
        }
        set
        {
            HttpContext.Current.Session["lang"] = value;
        }
    }

    /// <summary>
    /// Current Admin Panel Language
    /// </summary>
    public static string CurrentBlogAdminLanguage
    {
        get
        {
            if (!String.IsNullOrEmpty((String)HttpContext.Current.Session["lang_admin"]))
            {
                return (String)HttpContext.Current.Session["lang_admin"];
            }
            return DefaultBlogAdminLanguage;
        }
        set
        {
            HttpContext.Current.Session["lang_admin"] = value;
        }
    }

    /// <summary>
    /// Language Files Method
    /// </summary>
    /// <param name="languageCode">Language Code (etc. en, de, tr</param>
    public static void RefreshBlogLanguage(string languageCode)
    {
        Language.Get = BSHelper.GetLanguageDictionary("Languages/Main/" + languageCode + ".xml");
    }

    /// <summary>
    /// Language Admin Method
    /// </summary>
    /// <param name="languageCode">Language Code (etc. en, de, tr</param>
    public static void RefreshBlogAdminLanguage(string languageCode)
    {
        Language.Admin = BSHelper.GetLanguageDictionary("Languages/Admin/" + languageCode + ".xml");
    }
}
