using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_DateTimeSelector : System.Web.UI.UserControl
{
    private DateTime? _selectedDateTime;
    public DateTime SelectedDateTime
    {
        get
        {
            if (_selectedDateTime == null)
            {
                return DateTime.Now;
            }
            return _selectedDateTime.Value;
        }
        set
        {
            _selectedDateTime = value;
            SetDateTime();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDateTime();
        }
        else
        {
            //IFormatProvider provider = CultureInfo.CurrentCulture.DateTimeFormat;

            int year = Convert.ToInt32(txtDateYear.Text);
            int month = Convert.ToInt32(ddlDateMonth.SelectedIndex + 1);
            int day = Convert.ToInt32(txtDateDay.Text.ToString(CultureInfo.InvariantCulture));

            int hour = Convert.ToInt32(txtTimeHour.Text);
            int minute = Convert.ToInt32(txtTimeMinute.Text);

            SelectedDateTime = new DateTime(year, month, day, hour, minute, 0);
        }
    }

    private void SetDateTime()
    {
        ddlDateMonth.DataSource = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
        ddlDateMonth.DataBind();

        ddlDateMonth.Items.RemoveAt(ddlDateMonth.Items.Count - 1);

        ddlDateMonth.SelectedIndex = SelectedDateTime.Month - 1;

        txtDateDay.Text = SelectedDateTime.Day.ToString(CultureInfo.InvariantCulture);
        txtDateYear.Text = SelectedDateTime.Year.ToString(CultureInfo.InvariantCulture);

        txtTimeHour.Text = SelectedDateTime.Hour.ToString(CultureInfo.InvariantCulture);
        txtTimeMinute.Text = SelectedDateTime.Minute.ToString(CultureInfo.InvariantCulture);
    }
}