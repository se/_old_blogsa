<%@ Control Language="C#" ClassName="Search" %>

<script runat="server">

  protected void Page_Load(object sender, EventArgs e) {

  }

  protected void btnSave_Click(object sender, EventArgs e) {
    try {
      if (Session["SecurityCode"] != null && Session["SecurityCode"].ToString() == txtSecurityCode.Text) {
        if (Page.IsValid) {
          string strMessage = txtMessage.Text;
          string strEmail = txtEmail.Text;
          string strWebSite = txtWebSite.Text;
          string strName = txtName.Text;

          string strSubject = Language.Get["YouHaveMessage"].Replace("%", strName);
          string strBody = strName + " - " + strEmail + " - " + strWebSite + "<br><br>" + strMessage;
          string strTo = Blogsa.Settings["smtp_email"].ToString();
          string strToName = Blogsa.Settings["smtp_name"].ToString();

          BSHelper.SendMail(strSubject, strEmail, strName, strTo, strToName, strBody, true);

          lblMessage.Text = Language.Get["MessageSend"];
          divMessageSend.Visible = false;
          Session["SecurityCode"] = null;
        } else {
          lblMessage.Text = Language.Get["BeforeMessageSend"];
          divMessageSend.Visible = false;
        }
      } else {
        lblMessage.Text = Language.Get["YourSecurityCodeWrong"];
      }
    } catch (System.Exception ex) {
      lblMessage.Text = ex.Message;
    }

  }
</script>

<asp:Label ID="lblMessage" runat="server" Font-Bold="True"></asp:Label>
<div runat="server" id="divMessageSend" class="comments">
  <div class="content">
    <div class="table">
      <div class="tr">
        <div class="tdleft">
          <%=Language.Get["Name"]%>: *</div>
        <div class="tdright">
          <asp:TextBox CssClass="inputtext" runat="server" ID="txtName" ValidationGroup="WriteComment" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
            Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterName"] %>
          </asp:RequiredFieldValidator></div>
      </div>
      <div class="tr">
        <div class="tdleft">
          <%=Language.Get["EMail"]%>: *</div>
        <div class="tdright">
          <asp:TextBox CssClass="inputtext" runat="server" ID="txtEmail" ValidationGroup="WriteComment" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
            Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterMail"] %>
          </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
            runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            <%=Language.Get["ErrorMail"] %>
          </asp:RegularExpressionValidator></div>
      </div>
      <div class="tr">
        <div class="tdleft">
          <%=Language.Get["WebSite"]%>:
        </div>
        <div class="tdright">
          <asp:TextBox CssClass="inputtext" runat="server" ID="txtWebSite" ValidationGroup="WriteComment" />
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtWebSite"
            Display="Dynamic" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
            ValidationGroup="WriteComment">
                        <%=Language.Get["ErrorWebSite"] %>
          </asp:RegularExpressionValidator></div>
      </div>
      <div class="tr">
        <div class="tdleft">
          <%=Language.Get["Message"]%>: *</div>
        <div class="tdright">
          <asp:TextBox CssClass="inputtextarea" runat="server" ID="txtMessage" TextMode="MultiLine"
            ValidationGroup="WriteComment" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMessage"
            Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterYourMessage"] %>
          </asp:RequiredFieldValidator></div>
      </div>
      <div class="tr">
        <div class="tdleft">
          Güvenlik Kodu:
        </div>
        <div class="tdright">
          <img style="vertical-align:middle;" src="<%=Blogsa.Url + "Contents/Captcha.ashx" %>">
          <asp:TextBox Width="80px" runat="server" ID="txtSecurityCode" ValidationGroup="WriteComment" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSecurityCode"
            Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterSecurityCode"] %>
          </asp:RequiredFieldValidator>
        </div>
      </div>
      <div class="tr">
        <div class="tdleft">
          &nbsp;</div>
        <div class="tdright">
          <button class="inputbutton" runat="server" id="btnSave" onserverclick="btnSave_Click"
            validationgroup="WriteComment">
            <%=Language.Get["Send"] %>
          </button>
        </div>
      </div>
    </div>
  </div>
</div>
