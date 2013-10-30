<%@ Control Language="C#" AutoEventWireup="true" ClassName="Login"%>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        // Login Template
        PlaceHolder bsLoginPlaceHolder = (PlaceHolder)BSHelper.FindChildControl(this.Page, "BSLoginPlaceHolder");

        if (bsLoginPlaceHolder != null)
            bsLoginPlaceHolder.Controls.Add(LoadControl(Templates.Login));
    }
</script>
<asp:PlaceHolder runat="server" ID="BSLoginPlaceHolder" />
