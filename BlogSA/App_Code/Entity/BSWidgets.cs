using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThemeSettingCollection
/// </summary>
public class BSWidgets : ICollection<BSWidget>
{
    internal List<BSWidget> objectList;

    public BSWidgets()
    {
        objectList = new List<BSWidget>();
    }

    public BSWidget this[int index]
    {
        get
        {
            return (BSWidget)objectList[index];
        }
        set
        {
            objectList[index] = value;
        }
    }

    public BSWidget this[String widgetName]
    {
        get
        {
            foreach (BSWidget widget in objectList)
            {
                if (widget.Name.Equals(widgetName))
                    return widget;
            }
            return null;
        }
        set
        {
            foreach (BSWidget widget in objectList)
            {
                if (widget.Name.Equals(widgetName))
                    objectList[objectList.IndexOf(widget)] = value;
            }
        }
    }

    public void Add(BSWidget item)
    {
        objectList.Add(item);
    }

    public void Clear()
    {
        objectList.Clear();
    }

    public bool Contains(BSWidget item)
    {
        return objectList.Contains(item);
    }

    public void CopyTo(BSWidget[] array, int arrayIndex)
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

    public bool Remove(BSWidget item)
    {
        return objectList.Remove(item);
    }

    public IEnumerator<BSWidget> GetEnumerator()
    {
        return objectList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return objectList.GetEnumerator();
    }
}
