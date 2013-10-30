using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThemeSettingCollection
/// </summary>
public class BSSettings : ICollection<BSSetting>
{
    internal List<BSSetting> objectList;

    public BSSettings()
    {
        objectList = new List<BSSetting>();
    }

    public BSSetting this[int index]
    {
        get
        {
            return (BSSetting)objectList[index];
        }
        set
        {
            objectList[index] = value;
        }
    }

    public BSSetting this[String settingName]
    {
        get
        {
            foreach (BSSetting setting in objectList)
            {
                if (setting.Name.Equals(settingName))
                    return setting;
            }
            return null;
        }
        set
        {
            int foundedIndex = -1;

            foreach (BSSetting setting in objectList)
            {
                if (setting.Name.Equals(settingName))
                    foundedIndex = objectList.IndexOf(setting);
            }

            if (foundedIndex != -1)
            {
                objectList[foundedIndex] = value;
            }
        }
    }

    public void Add(BSSetting item)
    {
        objectList.Add(item);
    }

    public void Clear()
    {
        objectList.Clear();
    }

    public bool Contains(BSSetting item)
    {
        return objectList.Contains(item);
    }

    public void CopyTo(BSSetting[] array, int arrayIndex)
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

    public bool Remove(BSSetting item)
    {
        return objectList.Remove(item);
    }

    public IEnumerator<BSSetting> GetEnumerator()
    {
        return objectList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return objectList.GetEnumerator();
    }
}
