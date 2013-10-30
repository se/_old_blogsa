<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HitPosts.ascx.cs" Inherits="Admin_Widgets_HitPosts" %>
<div class="drgmain" id="wgHitPosts" name="HitPosts">
    <div class="drgbox">
        <div class="drgtitle">
            <span class="spantitle">
                <%=Language.Admin["HitPosts"] %></span>&nbsp;<a href="Posts.aspx" class="sbtn bsgray">
                    <span>
                        <%=Language.Admin["SeeAll"] %></span></a>
        </div>
        <div class="drgcontent">
            <asp:Literal ID="ltNoData" runat="Server"></asp:Literal>
            <asp:Repeater runat="server" ID="rpPopuler5Post">
                <ItemTemplate>
                    <div class="feeddiv">
                        <div class="leftdiv">
                            <a href="Posts.aspx?PostID=<%#Eval("PostID") %>" class="sbtn bsblue"><span>
                                <%=Language.Admin["EditPost"] %></span></a><a href='Posts.aspx?PostID=<%#Eval("PostID") %>'>
                                    <%#Eval("Title").ToString().Length < 40 ? Eval("Title").ToString() : Eval("Title").ToString().Substring(0, 39) + "..."%></a><br />
                        </div>
                        <div class="rightdiv">
                            <b>
                                <%#Eval("ReadCount")%>
                                <%=Language.Admin["Readed"] %></b>
                        </div>
                        <div style="clear: both; display: block;">
                        </div>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
