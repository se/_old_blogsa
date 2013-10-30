using System.Collections.Generic;
using System.Data;

public class Admin
{
    public static string GetWidgetState(string FolderName)
    {
        using (DataProcess dp = new DataProcess())
        {
            dp.AddParameter("FolderName", FolderName);
            dp.ExecuteScalar("SELECT WidgetID FROM Widgets WHERE FolderName = @FolderName");

            if (dp.Return.Status == DataProcessState.Success)
                return (string)dp.Return.Value;
            else
                return string.Empty;
        }
    }

    public static string GetWidgets()
    {
        string value = "";

        List<BSWidget> widgets = BSWidget.GetWidgetsBySorted();

        if (widgets != null && widgets.Count>0)
        {
            foreach (BSWidget widget in widgets)
            {
                value += "<li id='" + widget.WidgetID + "'>" + widget.Title + "</li>";
            }
        }

        return value;
    }
}