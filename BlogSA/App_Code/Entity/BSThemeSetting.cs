using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThemeSetting
/// </summary>
/// 

public enum ThemeSettingType
{
    Text,
    Choose,
    Number,
    Rich,
    MultiLine
}

public class BSThemeSetting
{
    private String _Key ;
    private String _Value;
    private String _DefaultValue;
    private ThemeSettingType _Type;
    private Boolean _IsRequired;
    private String _Title;
    private String _Description;

    public override string ToString()
    {
        return Value;
    }

    public string DefaultValue
    {
        get { return _DefaultValue; }
        set { _DefaultValue = value; }
    }

    public string Value
    {
        get { return _Value; }
        set { _Value = value; }
    }

    public string Key
    {
        get { return _Key; }
        set { _Key = value; }
    }

    public ThemeSettingType Type
    {
        get { return _Type; }
        set { _Type = value; }
    }

    public bool IsRequired
    {
        get { return _IsRequired; }
        set { _IsRequired = value; }
    }

    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
}