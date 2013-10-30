using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThemeSettingCollection
/// </summary>
public class BSThemeSettings : ICollection<BSThemeSetting>
{
    internal List<BSThemeSetting> objectList;

    public BSThemeSettings()
    {
        objectList = new List<BSThemeSetting>();
    }

    public BSThemeSetting this[int index]
    {
        get
        {
            return (BSThemeSetting)objectList[index];
        }
        set
        {
            objectList[index] = value;
        }
    }

    public BSThemeSetting this[String settingName]
    {
        get
        {
            foreach (BSThemeSetting setting in objectList)
            {
                if (setting.Key.Equals(settingName))
                    return setting;
            }
            return null;
        }
        set
        {
            foreach (BSThemeSetting setting in objectList)
            {
                if (setting.Key.Equals(settingName))
                    objectList[objectList.IndexOf(setting)] = value;
            }
        }
    }

    public void Add(BSThemeSetting item)
    {
        objectList.Add(item);
    }

    public void Clear()
    {
        objectList.Clear();
    }

    public bool Contains(BSThemeSetting item)
    {
        return objectList.Contains(item);
    }

    public void CopyTo(BSThemeSetting[] array, int arrayIndex)
    {
        objectList.CopyTo(array, arrayIndex);
    }

    public int Count
    {
        get { return objectList.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(BSThemeSetting item)
    {
        return objectList.Remove(item);
    }

    public IEnumerator<BSThemeSetting> GetEnumerator()
    {
        return objectList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return objectList.GetEnumerator();
    }
}
