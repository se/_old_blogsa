using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;

/// <summary>
/// Summary description for BlogsaMenu
/// </summary>
/// 

public enum ObjectTypes : short
{
    Custom = 0,
    Article = 1,
    Page = 2,
    File = 3,
    Link = 4,
    Term = 5,
    Comment = 6
}

public enum MenuTypes : short
{
    Single = 0,
    Multiple = 1
}

[XmlType("Menu")]
public class BSMenu
{
    private int _menuID;
    private int _menuGroupID;
    private int _parentID;
    private int _objectID;
    private ObjectTypes _objectType;
    private MenuTypes _menuType;
    private string _title;
    private string _description;
    private string _url;
    private string _target;
    private short _sort;

    public int ObjectID
    {
        get { return _objectID; }
        set { _objectID = value; }
    }

    public ObjectTypes ObjectType
    {
        get { return _objectType; }
        set { _objectType = value; }
    }

    public MenuTypes MenuType
    {
        get { return _menuType; }
        set { _menuType = value; }
    }

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Url
    {
        get { return _url; }
        set { _url = value; }
    }

    public string Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public int MenuID
    {
        get { return _menuID; }
        set { _menuID = value; }
    }

    public int ParentID
    {
        get { return _parentID; }
        set { _parentID = value; }
    }

    public int MenuGroupID
    {
        get { return _menuGroupID; }
        set { _menuGroupID = value; }
    }

    public short Sort
    {
        get { return _sort; }
        set { _sort = value; }
    }

    public static BSMenu GetMenu(int menuID)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("MenuID", menuID);
            dp.ExecuteReader("SELECT * FROM [Menus] WHERE [MenuID]=@MenuID");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr != null && dr.Read())
                    {
                        BSMenu menu = new BSMenu();
                        FillMenu(dr, menu);
                        return menu;
                    }
                }
            }
        }
        return null;
    }

    private static void FillMenu(IDataReader dr, BSMenu menu)
    {
        menu.Description = (string)dr["Description"];
        menu.MenuGroupID = (int)dr["MenuGroupID"];
        menu.MenuID = (int)dr["MenuID"];
        menu.MenuType = (MenuTypes)dr["MenuType"];
        menu.ObjectID = (int)dr["ObjectID"];
        menu.ObjectType = (ObjectTypes)dr["ObjectType"];
        menu.ParentID = (int)dr["ParentID"];
        menu.Sort = (short)dr["Sort"];
        menu.Target = (string)dr["Target"];
        menu.Title = (string)dr["Title"];
        menu.Url = (string)dr["Url"];

        if (menu.Url.StartsWith("~/"))
        {
            menu.Url = Blogsa.Url + menu.Url.Substring(2);
        }

        switch (menu.ObjectType)
        {
            case ObjectTypes.Article:
                BSPost article = BSPost.GetPost(menu.ObjectID);
                if (article != null)
                {
                    menu.Title = article.Title;
                    menu.Url = article.Link;
                }
                break;
            case ObjectTypes.Page:
                BSPost page = BSPost.GetPost(menu.ObjectID);
                if (page != null)
                {
                    menu.Title = page.Title;
                    menu.Url = page.Link;
                }
                break;
            case ObjectTypes.File:
                BSPost file = BSPost.GetPost(menu.ObjectID);
                if (file != null)
                {
                    menu.Title = file.Title;
                    menu.Url = file.Link;
                }
                break;
            case ObjectTypes.Link:
                BSLink link = BSLink.GetLink(menu.ObjectID);
                if (link != null)
                {
                    menu.Title = link.Name;
                    menu.Url = link.Url;
                }
                break;
            case ObjectTypes.Term:
                BSTerm term = BSTerm.GetTerm(menu.ObjectID);
                if (term != null)
                {
                    menu.Title = term.Name;
                    menu.Url = term.Link;
                }
                break;
            case ObjectTypes.Comment:
                BSComment comment = BSComment.GetComment(menu.ObjectID);
                if (comment != null)
                {
                    menu.Title = comment.Content;
                    menu.Url = comment.Link;
                }
                break;
            default:
                break;
        }
    }

    public static List<BSMenu> GetMenus(int menuGroupID)
    {
        List<BSMenu> menus = new List<BSMenu>();
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("MenuGroupID", menuGroupID);
            dp.ExecuteReader("SELECT * FROM [Menus] WHERE [MenuGroupID]=@MenuGroupID ORDER BY [Sort]");
            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr != null && dr.Read())
                    {
                        BSMenu menu = new BSMenu();
                        FillMenu(dr, menu);
                        menus.Add(menu);
                    }
                }
            }
        }
        return menus;
    }

    #region Events

    public static event EventHandler<CancelEventArgs> Showing;

    public static void OnShowing(BSMenu BsMenu, CancelEventArgs e)
    {
        if (Showing != null)
        {
            Showing(BsMenu, e);
        }
    }

    public static event EventHandler<EventArgs> Showed;

    public static void OnShowed(BSMenu BsMenu, EventArgs e)
    {
        if (Showed != null)
        {
            Showed(BsMenu, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Saving;

    public static void OnSaving(BSMenu BsMenu, CancelEventArgs e)
    {
        if (Saving != null)
        {
            Saving(BsMenu, e);
        }
    }

    public static event EventHandler<EventArgs> Saved;

    public static void OnSaved(BSMenu BsMenu, EventArgs e)
    {
        if (Saved != null)
        {
            Saved(BsMenu, e);
        }
    }

    public static event EventHandler<CancelEventArgs> Deleting;

    public static void OnDeleting(BSMenu BsMenu, CancelEventArgs e)
    {
        if (Deleting != null)
        {
            Deleting(BsMenu, e);
        }
    }

    public static event EventHandler<EventArgs> Deleted;

    public static void OnDeleted(BSMenu BsMenu, EventArgs e)
    {
        if (Deleted != null)
        {
            Deleted(BsMenu, e);
        }
    }

    #endregion

    public bool Save()
    {
        bool bReturnValue = false;
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Description", this.Description);
            dp.AddParameter("MenuGroupID", this.MenuGroupID);
            dp.AddParameter("MenuType", (short)this.MenuType);
            dp.AddParameter("ObjectID", this.ObjectID);
            dp.AddParameter("ObjectType", (short)this.ObjectType);
            dp.AddParameter("ParentID", this.ParentID);
            dp.AddParameter("Sort", this.Sort);
            dp.AddParameter("Target", this.Target);
            dp.AddParameter("Title", this.Title);
            dp.AddParameter("Url", this.Url);

            if (MenuID != 0)
            {
                dp.AddParameter("MenuID", MenuID);

                dp.ExecuteNonQuery("UPDATE [Menus] SET [Description]=@Description,[MenuGroupID]=@MenuGroupID,[MenuType]=@MenuType,"
                    + "[ObjectID]=@ObjectID,[ObjectType]=@ObjectType,[ParentID]=@ParentID,[Sort]=@Sort,[Target]=@Target,[Title]=@Title,[Url]=@Url WHERE [MenuID] = @MenuID");
                bReturnValue = dp.Return.Status == DataProcessState.Success;
            }
            else
            {
                dp.ExecuteNonQuery("INSERT INTO [Menus]([Description],[MenuGroupID],[MenuType],[ObjectID],[ObjectType],[ParentID],[Sort],[Target],[Title],[Url]) "
                    + "VALUES(@Description,@MenuGroupID,@MenuType,@ObjectID,@ObjectType,@ParentID,@Sort,@Target,@Title,@Url)");
                bReturnValue = dp.Return.Status == DataProcessState.Success;

                if (bReturnValue)
                {
                    dp.ExecuteScalar("SELECT @@IDENTITY");
                    if (dp.Return.Status == DataProcessState.Success)
                    {
                        MenuID = Convert.ToInt32(dp.Return.Value);
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
            CancelEventArgs cancelEvent = new CancelEventArgs();
            OnDeleting(this, cancelEvent);
            if (!cancelEvent.Cancel)
            {
                dp.AddParameter("MenuID", MenuID);
                dp.ExecuteNonQuery("DELETE FROM [Menus] WHERE [MenuID] = @MenuID");

                if (dp.Return.Status == DataProcessState.Success)
                {
                    OnDeleted(this, null);
                    return true;
                }
            }
        }
        return false;
    }
}