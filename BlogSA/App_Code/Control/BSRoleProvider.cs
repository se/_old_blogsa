using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;

/// <summary>
/// Providers
/// </summary>
public class BSRoleProvider : RoleProvider
{
    public static List<string> UserRoles = new List<string>();
    // BlogsaRoleProvider properties
    public override string ApplicationName
    {
        get { throw new NotSupportedException(); }
        set { throw new NotSupportedException(); }
    }
    // BlogsaRoleProvider methods
    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
            throw new ArgumentNullException("config");
        if (String.IsNullOrEmpty(name))
            name = "BlogsaRoleProvider";
        if (string.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Blogsa Role Provider");
        }
        base.Initialize(name, config);
        //FileIOPermission permission =
        //    new FileIOPermission(FileIOPermissionAccess.Read,
        //    _XmlFileName);
        //permission.Demand();
        if (config.Count > 0)
        {
            string attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException
                    ("Unrecognized attribute: " + attr);
        }
    }
    public override bool IsUserInRole(string username, string roleName)
    {
        throw new NotSupportedException();
    }
    public override string[] GetRolesForUser(string username)
    {
        return UserRoles.ToArray();
    }
    public override string[] GetUsersInRole(string roleName)
    {
        throw new NotSupportedException();
    }
    public override string[] GetAllRoles()
    {
        throw new NotSupportedException();
    }
    public override bool RoleExists(string roleName)
    {
        throw new NotSupportedException();
    }
    public override void CreateRole(string roleName)
    {
        throw new NotSupportedException();
    }
    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
        throw new NotSupportedException();
    }
    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        UserRoles.Clear();
        UserRoles.Add(roleNames[0]);
    }
    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
        throw new NotSupportedException();
    }
    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
        throw new NotSupportedException();
    }
}