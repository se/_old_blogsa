<%@ Application Language="C#" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        ExtensionManager.CompileExtension();
        Application.Add("OnlineUsers", 0);
    }

    void Application_End(object sender, EventArgs e)
    {
        Application.Remove("OnlineUsers");
    }

    void Application_Error(object sender, EventArgs e)
    {
        HttpContext context = ((HttpApplication)sender).Context;
        Exception ex = context.Server.GetLastError();

        if (ex == null || !(ex is HttpException) || (ex as HttpException).GetHttpCode() == 404)
        {
            return;
        }

        if (Blogsa.IsInstalled && Blogsa.Log)
        {
            try
            {
                while (ex != null)
                {
                    BSLog log = new BSLog();

                    log.LogID = Guid.NewGuid();
                    log.LogType = BSLogType.Error;
                    log.CreateDate = DateTime.Now;
                    log.Message = ex.Message;
                    log.StackTrace = ex.StackTrace;
                    log.Url = context.Request.Url.ToString();
                    log.RawUrl = context.Request.RawUrl;
                    log.Source = ex.Source;
                    log.TargetSite = ex.TargetSite;
                    log.UserID = Blogsa.ActiveUser != null ? Blogsa.ActiveUser.UserID : 0;

                    log.Save();

                    ex = ex.InnerException;
                }
            }
            catch
            {
                // If you want you can check log error!
            }
        }
    }

    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
    }

    void Session_Start(object sender, EventArgs e)
    {
        Application.Lock();
        Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e)
    {
        Application.Lock();
        Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
        Application.UnLock();
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        /* Fix for the Flash Player Cookie bug in Non-IE browsers.
         * Since Flash Player always sends the IE cookies even in FireFox
         * we have to bypass the cookies by sending the values as part of the POST or GET
         * and overwrite the cookies with the passed in values.
         * 
         * The theory is that at this point (BeginRequest) the cookies have not been read by
         * the Session and Authentication logic and if we update the cookies here we'll get our
         * Session and Authentication restored correctly
         */

        try
        {
            string session_param_name = "ASPSESSID";
            string session_cookie_name = "ASP.NET_SessionId";

            if (HttpContext.Current.Request.Form[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
            }
        }
        catch (Exception)
        {
        }

        try
        {
            string auth_param_name = "AUTHID";
            string auth_cookie_name = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Form[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
            }

        }
        catch (Exception)
        {
        }
    }
    void UpdateCookie(string cookie_name, string cookie_value)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        if (cookie == null)
        {
            cookie = new HttpCookie(cookie_name);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }
        cookie.Value = cookie_value;
        HttpContext.Current.Request.Cookies.Set(cookie);
    }
</script>
