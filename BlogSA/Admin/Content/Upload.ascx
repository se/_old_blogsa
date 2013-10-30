<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="fiUpload" %>
<%--    
// File Upload Control - fiUpload
// Programmer  : Selçuk ERMAYA
// My Web Site : www.selcukermaya.com
// v1.0
--%>
<style type="text/css">
    .divCaption {
        display: block;
        border: 1px solid black;
        font-weight: bold;
        background-color: #F0F0F0;
        font-family: Consolas;
        font-size: 12px;
        text-align: center;
        width: 150px;
        padding: 4px;
    }
    .black_overlay {
        display: none;
        position: absolute;
        top: 0%;
        left: 0%;
        width: 100%;
        height: 100%;
        background-color: black;
        z-index: 1001;
        -moz-opacity: 0.8;
        opacity: .80;
        filter: alpha(opacity=80);
    }
    .white_content {
        display: none;
        position: absolute;
        top: 10%;
        left: 25%;
        width: 50%;
        height: 65%;
        padding: 6px;
        border: 8px solid Darkgray;
        background-color: white;
        z-index: 1002;
        overflow: auto;
    }
</style>
<div runat="server" id="divControls" visible="false">
    <div id="fade" class="black_overlay" style="left: 0%; top: 0%">
    </div>
    <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='block';document.getElementById('fade').style.display='block'">
        <asp:Image ID="imgPicture" runat="server" Visible="False" BorderColor="#404040" BorderStyle="Solid"
            BorderWidth="1px" Height="128px" Width="128px" Style="vertical-align: bottom" /></a>
    <div id="light" class="white_content" style="left: 25%; top: 10%">
        <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
            <img style="border: 0;" alt="" id="imgShowLarge" runat="server" src="" /></a>
    </div>
    <asp:Label ID="lblFile" runat="server" BackColor="#F4F4F4" BorderColor="#404040"
        BorderWidth="1px" Style="padding-right: 3px; padding-left: 3px; padding-bottom: 1px;
        padding-top: 1px; border-left: #ff0033 5px solid; vertical-align: middle;" 
        Visible="False"></asp:Label><asp:Label
            ID="lblCaption" runat="server" Font-Bold="True" Style="vertical-align: middle"><%=Language.Admin["UploadCaption"]%></asp:Label>
    <asp:FileUpload ID="fupFile" runat="server" Style="vertical-align: middle" />
    <asp:LinkButton ID="btnUpload" runat="server" UseSubmitBehavior="false" OnClick="btnUpload_Click"
        CssClass="bsbutton blue" Style="vertical-align: middle"><span><%=Language.Admin["Upload"] %></span></asp:LinkButton>
    <asp:LinkButton ID="btnDelete" runat="server" UseSubmitBehavior="false" Visible="False"
        OnClick="btnDelete_Click" Style="vertical-align: middle" CssClass="bsbutton red"><span><%=Language.Admin["Delete"] %></span></asp:LinkButton>
    <asp:LinkButton ID="btnComeBack" runat="server" Visible="False" OnClick="btnComeBack_Click"
        UseSubmitBehavior="false" Style="vertical-align: middle" CssClass="bsbutton"><span><%=Language.Admin["FileCallBack"]%></span></asp:LinkButton></div>
<asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Font-Bold="True" Style="vertical-align: middle"></asp:Label>
<div runat="server" id="divCaption" class="divCaption" visible="true">
    Upload Control</div>
