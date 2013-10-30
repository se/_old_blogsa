<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BSGridViewHeader.ascx.cs"
    Inherits="Admin_Content_BSGridViewHeader" %>
<div class="zc">
    <div class="pc">
        <asp:Panel runat="server" ID="pnlSearch">
            <ul class="oV">
                <li class="Tf dx Od">
                    <ul class="b-y-C Wc">
                        <li class="Jd">
                            <asp:TextBox runat="server" ID="tbSearchText" />
                        </li>
                        <li class="zb ug">
                            <asp:LinkButton ID="btnSearch" runat="server"><span><%=Language.Get["Search"] %></span></asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlButtons" CssClass="bsright">
        </asp:Panel>
    </div>
</div>
