using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Settings
/// </summary>
[XmlType("Setting")]
public class BSSetting
{
    #region Properties
    private int _SettingID;

    public int SettingID
    {
        get { return _SettingID; }
        set { _SettingID = value; }
    }
    private string _Name;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private string _Value;

    public string Value
    {
        get { return _Value; }
        set { _Value = value; }
    }
    private string _Title;

    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }
    private string _Description;

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
    private bool _Main;

    public bool Main
    {
        get { return _Main; }
        set { _Main = value; }
    }
    private int _Sort;

    public int Sort
    {
        get { return _Sort; }
        set { _Sort = value; }
    }

    private bool _Visible;
    public bool Visible
    {
        get { return _Visible; }
        set { _Visible = value; }
    }

    private int _SiteID;
    public int SiteID
    {
        get { return _SiteID; }
        set { _SiteID = value; }
    }
    #endregion

    #region Constructor
    public BSSetting()
    {

    }
    #endregion

    #region  Methods
    public override string ToString()
    {
        return Value;
    }

    public static BSSetting GetSetting(int iSettingID)
    {
        BSSetting bsSetting = new BSSetting();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("SettingID", iSettingID);

            dp.ExecuteReader("SELECT * FROM Settings WHERE SettingID = @SettingID");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr.Read())
                    {
                        FillValue(dr, bsSetting);

                        return bsSetting;
                    }
                }
            }
        }

        return null;
    }

    public static BSSetting GetSetting(string name)
    {
        BSSetting bsSetting = new BSSetting();

        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("Name", name);

            dp.ExecuteReader("SELECT * FROM Settings WHERE Name = @Name");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    if (dr.Read())
                    {
                        FillValue(dr, bsSetting);

                        return bsSetting;
                    }
                }
            }
        }

        return null;
    }

    public static BSSettings GetSettings()
    {
        BSSettings settings = new BSSettings();

        using (DataProcess dp = new DataProcess())
        {
            dp.ExecuteReader("SELECT * FROM Settings");

            if (dp.Return.Status == DataProcessState.Success)
            {
                using (IDataReader dr = dp.Return.Value as IDataReader)
                {
                    while (dr.Read())
                    {
                        BSSetting bsSetting = new BSSetting();

                        FillValue(dr, bsSetting);

                        settings.Add(bsSetting);
                    }
                }
            }
        }

        return settings;
    }

    private static void FillValue(IDataReader dr, BSSetting bsSetting)
    {
        bsSetting.SettingID = Convert.ToInt32(dr["SettingID"]);
        bsSetting.Name = dr["Name"].ToString();
        bsSetting.Value = dr["Value"].ToString();
        bsSetting.Title = dr["Title"].ToString();
        bsSetting.Description = dr["Description"].ToString();
        bsSetting.Main = Convert.ToBoolean(dr["Main"]);
        bsSetting.Sort = Convert.ToInt32(dr["Sort"]);
        bsSetting.Visible = Convert.ToBoolean(dr["Visible"]);
    }

    public bool Save()
    {
        DataProcess dp = new DataProcess();

        string sql = String.Empty;

        if (SettingID == 0)
            sql = "INSERT INTO Settings([SiteID],[Name],[Value],[Title],[Description],[Main],[Sort],[Visible]) "
                  + "VALUES(@SiteID,@Name,@Value,@Title,@Description,@Main,@Sort,@Visible);";
        else
            sql = "UPDATE Settings SET [SiteID]=@SiteID,[Name]=@Name,[Value]=@Value,[Title]=@Title,[Description]=@Description,[Main]=@Main,[Sort]=@Sort,[Visible]=@Visible WHERE [SettingID] = @SettingID;";

        dp.AddParameter("SiteID", this.SiteID);
        dp.AddParameter("Name", this.Name);
        dp.AddParameter("Value", this.Value);
        dp.AddParameter("Title", this.Title);
        dp.AddParameter("Description", this.Description);
        dp.AddParameter("Main", this.Main);
        dp.AddParameter("Sort", this.Sort);
        dp.AddParameter("Visible", this.Visible);

        if (SettingID != 0)
            dp.AddParameter("SettingID", this.SettingID);

        dp.ExecuteNonQuery(sql);

        return dp.Return.Status == DataProcessState.Success;
    }
    #endregion
}
