using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Serialization;

/// <summary>
/// Summary description for BSMenuGroup
/// </summary>
[XmlType("MenuGroup")]
public class BSMenuGroup
{
    #region Variables
    private int _SiteID;
    private int _menuGroupID;
    private string _title;
    private string _description;
    private string _code;
    private bool _default;
    private List<BSMenu> _menu;
    private string _languageCode;
    #endregion

    #region Properties
    public int SiteID
    {
        get { return _SiteID; }
        set { _SiteID = value; }
    }

    public List<BSMenu> Menu
    {
        get
        {
            if (_menu == null)
                _menu = new List<BSMenu>();

            return _menu;
        }
        set { _menu = value; }
    }

    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public int MenuGroupID
    {
        get { return _menuGroupID; }
        set { _menuGroupID = value; }
    }

    public bool Default
    {
        get { return _default; }
        set { _default = value; }
    }

    public string LanguageCode
    {
        get { return _languageCode; }
        set { _languageCode = value; }
    }
    #endregion

    #region Methods
    public static BSMenuGroup GetMenuGroup(int menuGroupID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("MenuGroupID", menuGroupID);
            dp.ExecuteReader("SELECT * FROM [MenuGroups] WHERE [MenuGroupID]=@MenuGroupID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSMenuGroup menuGroup = new BSMenuGroup();
                        FillMenuGroup(dr, menuGroup);
                        return menuGroup;
                    }
                }
            }
        }
        return null;
    }

    public static BSMenuGroup GetMenuGroup(string menuCode)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Code", menuCode);
            dp.ExecuteReader("SELECT * FROM [MenuGroups] WHERE [Code]=@Code");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSMenuGroup menuGroup = new BSMenuGroup();
                        FillMenuGroup(dr, menuGroup);
                        return menuGroup;
                    }
                }
            }
        }
        return null;
    }

    public static List<BSMenuGroup> GetMenuGroups()
    {
        List<BSMenuGroup> menuGroups = new List<BSMenuGroup>();
        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteReader("SELECT * FROM [MenuGroups]");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSMenuGroup menuGroup = new BSMenuGroup();
                        FillMenuGroup(dr, menuGroup);
                        menuGroups.Add(menuGroup);
                    }
                }
            }
        }
        return menuGroups;
    }

    public static BSMenuGroup GetDefault()
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Default", true);
            dp.ExecuteReader("SELECT * FROM [MenuGroups] WHERE [Default]=@Default");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSMenuGroup menuGroup = new BSMenuGroup();
                        FillMenuGroup(dr, menuGroup);
                        return menuGroup;
                    }
                }
            }
        }

        BSMenuGroup defaultMenuGroup = new BSMenuGroup();
        return defaultMenuGroup;
    }

    private static void FillMenuGroup(IDataReader dr, BSMenuGroup menuGroup)
    {
        menuGroup.MenuGroupID = (int)dr["MenuGroupID"];
        menuGroup.Title = (string)dr["Title"];
        menuGroup.Description = (string)dr["Description"];
        menuGroup.Code = (string)dr["Code"];
        menuGroup.LanguageCode = (string)dr["LanguageCode"];
        menuGroup.Default = (bool)dr["Default"];
        menuGroup.Menu = BSMenu.GetMenus(menuGroup.MenuGroupID);
    }

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            if (this.Default)
            {
                dp.AddParameter("Default", false);
                dp.ExecuteNonQuery("UPDATE [MenuGroups] SET [Default]=@Default");
            }

            dp.AddParameter("SiteID", this.SiteID);
            dp.AddParameter("Code", this.Code);
            dp.AddParameter("Description", this.Description);
            dp.AddParameter("Title", this.Title);
            dp.AddParameter("Default", this.Default);
            dp.AddParameter("LanguageCode", this.LanguageCode);

            if (MenuGroupID != 0)
            {
                dp.AddParameter("MenuGroupID", this.MenuGroupID);

                dp.ExecuteNonQuery("UPDATE [MenuGroups] SET [SiteID]=@SiteID,[Code]=@Code,[Description]=@Description,[Title]=@Title,[Default]=@Default,[LanguageCode]=@LanguageCode WHERE [MenuGroupID] = @MenuGroupID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    foreach (BSMenu bsMenu in Menu)
                    {
                        bsMenu.Save();
                    }
                }
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO [MenuGroups]([SiteID],[Code],[Description],[Title],[Default],[LanguageCode]) VALUES(@SiteID,@Code,@Description,@Title,@Default,@LanguageCode)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    foreach (BSMenu bsMenu in Menu)
                    {
                        bsMenu.Save();
                    }

                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        MenuGroupID = Convert.ToInt32(dp.Return.Value);
                    }
                }
            }
        }
        return bReturnValue;
    }

    public bool Remove()
    {
        using (DataProcess dp = new DataProcess())
        {
            //CancelEventArgs cancelEvent = new CancelEventArgs();
            //OnDeleting(this, cancelEvent);
            //if (!cancelEvent.Cancel)
            //{
            dp.AddParameter("MenuGroupID", MenuGroupID);
            dp.ExecuteNonQuery("DELETE FROM [MenuGroups] WHERE [MenuGroupID] = @MenuGroupID");

            if (dp.Return.Status == DataProcessState.Success)
            {
                //OnDeleted(this, null);
                return true;
            }
            //}
        }
        return false;
    }

    public string GetHtml()
    {
        return GetHtml(true, true);
    }

    public string GetHtml(bool addUlTag, bool addInnerSpan)
    {
        StringBuilder sb = new StringBuilder();

        if (addUlTag)
            sb.AppendLine("<ul>");

        foreach (BSMenu menu in Menu)
        {
            if (addInnerSpan)
                sb.AppendLine(string.Format("<li><a href=\"{0}\" target=\"{2}\"><span>{1}</span></a></li>", menu.Url, menu.Title, menu.Target));
            else
                sb.AppendLine(string.Format("<li><a href=\"{0}\" target=\"{2}\">{1}</a></li>", menu.Url, menu.Title, menu.Target));
        }

        if (addUlTag)
            sb.AppendLine("</ul>");

        return sb.ToString();
    }

    public override string ToString()
    {
        return GetHtml(true, true);
    }
    #endregion
}