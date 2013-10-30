<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogsaNews.ascx.cs" Inherits="Admin_Widgets_BlogsaNews" %>
<div class="drgmain" id="wgBlogsaNews" name="BlogsaNews">
    <div class="drgbox">
        <div class="drgtitle">
            <span class="spantitle">
                <%=Language.Admin["BlogsaNews"] %></span>
        </div>
        <div class="drgcontent">
            <asp:Repeater runat="server" ID="rpFeed">
                <ItemTemplate>
                    <div class="feeddiv">
                        <a class="sbtn bsgray" target="_blank" href='<%#Eval("Link") %>'><span>
                            <%#Language.Admin["Read"] %></span></a> <span class="feedtitle">
                                <%#Eval("Title")%></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
