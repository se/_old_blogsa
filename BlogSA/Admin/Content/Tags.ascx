<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tags.ascx.cs" Inherits="Admin_Content_Tags" %>
<link href="Css/autocompleate.css" rel="stylesheet" type="text/css" />
<script src="Js/Jquery.AutoCompleate.js" type="text/javascript"></script>
<script language="JavaScript">
        $(document).ready(function() 
        {
            $("#<%=sAutoComp.ClientID %>").fcbkcomplete({
            json_url: "Control/Ajax.ashx?act=gettags",
            json_cache: false,
            filter_case: true,
            filter_hide: false,
            textboxid: '<%=txtTags.ClientID %>',
            complete_text:'<%=Language.Admin["Writesomething"] %>',
            newel: true        
          });
        });    
</script>
<div class="title">
    <%=Language.Admin["Tags"] %></div>
<div class="sidecontent">
    <select runat="server" id="sAutoComp" style="display: none;">
    </select>
    <asp:HiddenField runat="Server" ID="txtTags" />
</div>
