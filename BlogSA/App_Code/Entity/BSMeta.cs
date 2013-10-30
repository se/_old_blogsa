using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Serialization;

/// <summary>
/// Summary description for BSMeta
/// </summary>
[XmlType("Meta")]
public class BSMeta
{
    private string _key;
    private string _value;
    private ValueTypes _valueType;
    private ExpressionTypes _expressionType;
    private string _expression;
    private int _objectId;
    private ObjectTypes _objectType;
    private int _sort;
    private bool _readOnly;
    private bool _visible;

    public string Key
    {
        get { return _key; }
        set { _key = value; }
    }

    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public ValueTypes ValueType
    {
        get { return _valueType; }
        set { _valueType = value; }
    }

    public ExpressionTypes ExpressionType
    {
        get { return _expressionType; }
        set { _expressionType = value; }
    }

    public string Expression
    {
        get { return _expression; }
        set { _expression = value; }
    }

    public int ObjectId
    {
        get { return _objectId; }
        set { _objectId = value; }
    }

    public ObjectTypes ObjectType
    {
        get { return _objectType; }
        set { _objectType = value; }
    }

    public int Sort
    {
        get { return _sort; }
        set { _sort = value; }
    }

    public bool ReadOnly
    {
        get { return _readOnly; }
        set { _readOnly = value; }
    }

    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }

    public override string ToString()
    {
        return Value;
    }

    public static BSMetas GetMetas(int objectId)
    {
        return new BSMetas();
    }
}

/// <summary>
/// Contains type of the value fields.
/// </summary>
public enum ValueTypes : short
{
    /// <summary>
    /// Type of text value fields.
    /// </summary>
    Text = 0,
    /// <summary>
    /// Type of numeric value fields.
    /// </summary>
    Numeric = 1,
    /// <summary>
    /// Type of Date and Time value fields.
    /// </summary>
    DateTime = 2,
    /// <summary>
    /// Type of Date value fields.
    /// </summary>
    Date = 3,
    /// <summary>
    /// Type of Time value fields.
    /// </summary>
    Time = 4,
    /// <summary>
    /// Type of File value fields.
    /// </summary>
    File = 5,
    /// <summary>
    /// Type of Picture value fields.
    /// </summary>
    Picture = 6,
    /// <summary>
    /// Type of Video value fields.
    /// </summary>
    Video = 7,
    /// <summary>
    /// Type of DropDown structure needed value fields.
    /// </summary>
    DropDown = 8,
    /// <summary>
    /// Type of CheckBox structure needed value fields.
    /// </summary>
    CheckBox = 9,
    /// <summary>
    /// Type of Radio structure needed value fields.
    /// </summary>
    Radio = 10,
    /// <summary>
    /// Type of RadioList structure needed value fields.
    /// </summary>
    RadioList = 11,
    /// <summary>
    /// Type of CheckList structure needed value fields.
    /// </summary>
    CheckList = 12,
    /// <summary>
    /// Type of Button structure needed value fields.
    /// </summary>
    Button = 13,
}

/// <summary>
/// Contains type of the expression types.
/// </summary>
public enum ExpressionTypes : short
{
    /// <summary>
    /// Type of Custom expression types.
    /// </summary>
    Custom = 1,
    /// <summary>
    /// Type of Email expression types.
    /// </summary>
    Email = 2,
    /// <summary>
    /// Type of Numeric expression types.
    /// </summary>
    Numeric = 3,
    /// <summary>
    /// Type of Text expression types.
    /// </summary>
    Text = 4,
    /// <summary>
    /// Type of Range expression types.
    /// </summary>
    Range = 5,
    /// <summary>
    /// Type of Compare expression types.
    /// </summary>
    Compare = 6
}