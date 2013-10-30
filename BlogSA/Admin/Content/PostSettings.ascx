<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostSettings.ascx.cs"
    Inherits="Admin_Content_PostSettings" %>
<%@ Register TagPrefix="blogsausercontrols" TagName="datetimeselector" Src="~/Admin/Content/DateTimeSelector.ascx" %>
<%@ Register TagPrefix="uc1" TagName="categories" Src="~/Admin/Content/Categories.ascx" %>
<%@ Register TagPrefix="uc3" TagName="tags" Src="~/Admin/Content/Tags.ascx" %>
<%@ Register TagPrefix="blogsausercontrols" TagName="languagepicker" Src="~/Admin/Content/LanguagePicker.ascx" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#mnAdd").attr("class", "current");

        $('#<%=rblDate.ClientID %> input:first').click(function () {
            $('#divChanged').slideUp();
        });
        $('#<%=rblDate.ClientID %> input:last').click(function () {
            $('#divChanged').slideDown();
        });
    });
</script>
<div id="divAddPostSide" runat="server">
    <asp:DropDownList ID="ddState" runat="server" Width="100%">
        <asp:ListItem Selected="True" Value="1" Text='<%=Language.Admin["Publish"] %>'></asp:ListItem>
        <asp:ListItem Value="0" Text='<%=Language.Admin["Draft"] %>'></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <div id="divChangedatetime">
        <asp:RadioButtonList ID="rblDate" runat="server" RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="0"></asp:ListItem>
            <asp:ListItem Text="" Value="1"></asp:ListItem>
        </asp:RadioButtonList>
        <div id="divChanged" style="display: none; padding: 5px; border: 1px solid #e7e7e7;">
            <blogsausercontrols:datetimeselector runat="server" ID="dtsDateTime" />
        </div>
    </div>
    <br />
    <uc1:categories TermType="Category" ID="Categories1" runat="server" />
    <uc3:tags ID="Tags1" runat="server" />
    <div class="title">
        <%=Language.Admin["PostSettings"] %></div>
    <div class="sidecontent">
        <asp:CheckBox ID="cblAddComment" runat="server" Checked="True" Text='<%=Language.Admin["CommentAdd"] %>' /></div>
    <blogsausercontrols:languagepicker ID="postLanguagePicker" runat="server" />
</div>
