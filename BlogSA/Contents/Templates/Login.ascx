<%@ Control Language="C#" Inherits="LoginFormBase" %>
<link rel="stylesheet" href="<%=Blogsa.Url +"Resources/Login/StyleSheet.css" %>" />
<script type="text/javascript">
    function keyEnter(event) {
        if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
            __doPostBack('<%=btnLogin.UniqueID %>', ''); return false;
        }
        else return true;
    }
</script>
<asp:Panel runat="Server" CssClass="login" DefaultButton="btnLogin" ID="pnlLogin">
    <div class="topbuttons">
        <a class="home" href="Default.aspx"></a><a class="help" href="http://www.blogsa.net/Help.aspx">
        </a>
    </div>
    <asp:Panel runat="Server" id="divLogin" DefaultButton="btnLogin">
        <div class="inputs">
            <div class="txtbox">
                <span>
                    <%=Language.Get["UserName"] %></span>
                <asp:TextBox ID="txtUserName" onkeydown="keyEnter(event)" runat="server"></asp:TextBox>
            </div>
            <div class="txtbox">
                <span>
                    <%=Language.Get["Password"] %></span>
                <asp:TextBox ID="txtPassword" onkeydown="keyEnter(event)" TextMode="Password" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:LinkButton ID="btnLogin" OnClick="btnLogin_Click" CssClass="button" runat="server"><span><%=Language.Get["Login"] %></span></asp:LinkButton>
        <div class="others">
            <div>
                <asp:CheckBox ID="cbRememberMe" runat="server" />
                <label for="<%=cbRememberMe.ClientID %>">
                    <%=Language.Get["RememberMe"] %>
                </label>
            </div>
            <div>
                <a href="Login.aspx?lostpassword">
                    <%=Language.Get["LostPassword"] %></a>
            </div>
        </div>
    </asp:Panel>
    <div id="divLostPassword" runat="Server" visible="false">
        <div class="inputs">
            <div class="txtbox">
                <span>
                    <%=Language.Get["EMail"] %></span>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:LinkButton ID="btnSendPassword" CssClass="button" runat="server" OnClick="btnSendPassword_Click"><span><%=Language.Get["Send"] %></span></asp:LinkButton>
        <a href="Login.aspx" class="button"><span>
            <%=Language.Get["Back"] %></span></a>
    </div>
</asp:Panel>
<div class="error">
    <asp:Label runat="Server" ID="lblInfo"></asp:Label>
</div>
