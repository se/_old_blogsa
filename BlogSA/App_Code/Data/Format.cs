using System;
using System.Collections.Generic;
using System.Web;

public class Format
{
	public static string DateQuery(string NowDate)
    {
        if (System.Configuration.ConfigurationManager.AppSettings["Provider"] == "System.Data.SqlClient")
        {
            NowDate = "(CreateDate<CONVERT(DATETIME, '" + NowDate + "',102))";
        }
        else
        {
            NowDate = "(Posts.CreateDate<#" + NowDate + "#)";
        }
        return NowDate;
    }
}
