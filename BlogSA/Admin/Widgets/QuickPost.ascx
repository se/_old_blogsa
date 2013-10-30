<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickPost.ascx.cs" Inherits="Admin_Widgets_QuickPost" %>
<div class="drgmain" id="wgHitPosts" name="QuickPost">
    <div class="drgbox">
        <div class="drgtitle">
            <span class="spantitle">
                <%=Language.Admin["QuickPost"] %></span>
        </div>
        <div class="drgcontent">
            <div class="frm">
                <div class="frmbox">
                    <div class="frmtitle">
                        <span>
                            <%=Language.Admin["Title"] %></span></div>
                    <div class="frmrow">
                        <asp:TextBox runat="Server" CssClass="fmrtxtbox" ID="txtPostTitle"></asp:TextBox></div>
                    <div class="frmtitle">
                        <span>
                            <%=Language.Admin["Content"] %></span></div>
                    <div class="frmrow">
                        <asp:TextBox runat="Server" CssClass="fmrtxtbox" ID="txtPostContent" Height="40px"
                            TextMode="MultiLine"></asp:TextBox></div>
                    <div class="frmtitle">
                        <span>
                            <%=Language.Admin["Tags"] %></span></div>
                    <div class="frmrow">
                        <asp:TextBox runat="Server" CssClass="fmrtxtbox" ID="txtPostTags"></asp:TextBox></div>
                                        <div class="frmsubtitle">
                        <%=Language.Admin["CommaWithMultiple"] %>
                    </div>
                    <div class="frmactions">
                        <asp:LinkButton runat="Server" ID="btnSavePublish" CssClass="bsbtn small green" 
                            onclick="btnSavePublish_Click"><%=Language.Admin["Publish"] %></asp:LinkButton>
                        <asp:LinkButton runat="Server" ID="btnSaveDraft" CssClass="bsbtn small black" 
                            onclick="btnSaveDraft_Click"><%=Language.Admin["Draft"] %></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
