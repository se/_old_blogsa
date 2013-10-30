<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Default/Master.master" AutoEventWireup="true"
    CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="search-results">
        <asp:Literal ID="ltResult" runat="server" />
    </div>
    <div class="search-contents">
        <div class="search-founded-items">
            <asp:Repeater runat="server" ID="rpSearch">
                <ItemTemplate>
                    <div class="search-item">
                        <div class="search-title">
                            <a href="<%#BSHelper.GetLink((int)Eval("PostID")) %>">
                                <%#Eval("Title") %></a>
                        </div>
                        <div class="search-subtitle">
                            <%#BSHelper.GetLink((int)Eval("PostID")) %>
                        </div>
                        <p class="search-content">
                            <%#GetSearchedContent((string)Eval("Content")) %>
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="search-pagination">
            <asp:Literal ID="ltPaging" runat="server" />
        </div>
    </div>
</asp:Content>
