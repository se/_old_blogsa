<%@ Control Language="C#" ClassName="Calendar" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Controls.Clear();
        LiteralControl lC = new LiteralControl();
        lC.Text = e.Day.Date.Day.ToString();
        e.Cell.Controls.Add(lC);
    }
</script>
<div class="widget">
<div class="title"><span><%=Language.Get["Calendar"] %></span></div>
<div class="content">
    <span>
    <asp:Calendar ToolTip="" ID="Calendar1" CssClass="calendar" runat="server" OnDayRender="Calendar1_DayRender" BorderStyle="None" DayNameFormat="FirstLetter" SelectionMode="None" CellPadding="0">
        <TitleStyle CssClass="header" />
        <DayStyle  CssClass="day"/>
        <NextPrevStyle CssClass="nextprev" />
        <TodayDayStyle CssClass="today"/>
        <SelectedDayStyle CssClass="selected" />
        <WeekendDayStyle CssClass="weekend" />
        <DayHeaderStyle CssClass="dayheader" />
    </asp:Calendar>
    </span>
</div>
</div>