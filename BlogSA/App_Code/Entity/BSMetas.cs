using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThemeSettingCollection
/// </summary>
public class BSMetas : ICollection<BSMeta>
{
    internal List<BSMeta> objectList;

    public BSMetas()
    {
        objectList = new List<BSMeta>();
    }

    public BSMeta this[int index]
    {
        get
        {
            return (BSMeta)objectList[index];
        }
        set
        {
            objectList[index] = value;
        }
    }

    public BSMeta this[String key]
    {
        get
        {
            foreach (BSMeta item in objectList)
            {
                if (item.Key.Equals(key))
                    return item;
            }
            return null;
        }
        set
        {
            foreach (BSMeta item in objectList)
            {
                if (item.Key.Equals(key))
                    objectList[objectList.IndexOf(item)] = value;
            }
        }
    }

    public void Add(BSMeta item)
    {
        objectList.Add(item);
    }

    public void Clear()
    {
        objectList.Clear();
    }

    public bool Contains(BSMeta item)
    {
        return objectList.Contains(item);
    }

    public void CopyTo(BSMeta[] array, int arrayIndex)
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

    public bool Remove(BSMeta item)
    {
        return objectList.Remove(item);
    }

    public IEnumerator<BSMeta> GetEnumerator()
    {
        return objectList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return objectList.GetEnumerator();
    }
}
