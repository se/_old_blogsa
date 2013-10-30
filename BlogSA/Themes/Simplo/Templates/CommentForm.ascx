<%@ Control Language="C#" Inherits="CommentFormBase" %>
<div id="commentform" class="writecomment">
    <a id="WriteComment"></a>
    <div class="title">
        <h2>
            <%=Language.Get["WriteComment"]%>
        </h2>
    </div>
    <div class="content">
        <asp:Label ID="lblCommentWriteInfo" runat="server" Font-Bold="True"></asp:Label>
        <div class="table">
            <%if (Blogsa.ActiveUser == null)
              {
            %>
            <div class="tr">
                <div class="tdleft">
                    <label for="<%=txtName.ClientID %>">
                        <%=Language.Get["Name"]%>: *</label></div>
                <div class="tdright">
                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtName" ValidationGroup="WriteComment" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                        Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterName"] %>
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="tr">
                <div class="tdleft">
                    <label for="<%=txtEmail.ClientID %>">
                        <%=Language.Get["EMail"]%>: *</label></div>
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
                    <label for="<%=txtWebSite.ClientID %>">
                        <%=Language.Get["WebSite"]%>:</label>
                </div>
                <div class="tdright">
                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtWebSite" ValidationGroup="WriteComment" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtWebSite"
                        Display="Dynamic" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                        ValidationGroup="WriteComment">
                        <%=Language.Get["ErrorWebSite"] %>
                    </asp:RegularExpressionValidator></div>
            </div>
            <%}
              else
              {%>
            <div class="tr">
                <div class="tdleft">
                </div>
                <div class="tdright">
                    <b>
                        <%=Blogsa.ActiveUser.Name%></b></div>
            </div>
            <%} %>
            <div class="tr">
                <div class="tdleft">
                    <label for="<%=txtComment.ClientID %>"><%=Language.Get["Comment"]%>: *</label></div>
                <div class="tdright">
                    <asp:TextBox CssClass="inputtextarea" runat="server" ID="txtComment" TextMode="MultiLine"
                        ValidationGroup="WriteComment" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtComment"
                        Display="Dynamic" ValidationGroup="WriteComment">
                        <%=Language.Get["PleaseEnterComment"] %>
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="tr">
                <div class="tdright">
                    <asp:CheckBox runat="server" ID="cbxNotifyMe" ValidationGroup="WriteComment" />
                </div>
            </div>
            <div class="tr">
                <div class="tdleft">
                    <%=Language.Get["SecurityCode"]%>: *
                </div>
                <div class="tdright">
                    <img alt="" src="<%=Blogsa.Url+"Contents/Captcha.ashx?Unique="+BSPost.CurrentPost.PostID%>"
                        style="vertical-align: middle; border:1px solid #e7e7e7;">
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
                    <asp:Button runat="Server" ID="btnSave" ValidationGroup="WriteComment" />
                </div>
            </div>
        </div>
    </div>
</div>
