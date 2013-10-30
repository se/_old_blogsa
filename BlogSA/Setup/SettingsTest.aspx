<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettingsTest.aspx.cs" Inherits="SettingsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Settings Test</title>
    <style type="text/css">
        body
        {
            font-family: Trebuchet MS;
            font-size: 8pt;
        }
        
        legend
        {
            font-weight: bold;
            font-size: 10pt;
            color: #2D3E5C;
        }
        
        fieldset
        {
            border: 1px solid #2D3E5C;
        }
        
        p
        {
            border-bottom: 1px solid #e7e7e7;
        }
        
        label
        {
            font-weight: bold;
            color: #2D3E5C;
            width: 160px;
            display: inline-block;
            border-right: 1px solid #e7e7e7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend>Setup</legend>
            <p>
                <label>
                    Setup
                </label>
                <asp:Label Text="" ID="lblSetup" runat="server" />
            </p>
            <p>
                <label>
                    Database Type
                </label>
                <asp:Label Text="" ID="lblProvider" runat="server" />
            </p>
            <p>
                <label>
                    Connection String
                </label>
                <asp:Label Text="" ID="lblConnectionString" runat="server" />
            </p>
            <p>
                <label>
                    Connection Test
                </label>
                <asp:Label Text="" ID="lblConnectionTest" runat="server" />
            </p>
        </fieldset>
        <fieldset>
            <legend>File System Settings</legend>
            <p>
                <label>
                    Web.config
                </label>
                <asp:Label Text="" ID="lblWebConfig" runat="server" />
            </p>
            <p>
                <label>
                    App_Data
                </label>
                <asp:Label Text="" ID="lblAppData" runat="server" />
            </p>
            <p>
                <label>
                    Upload
                </label>
                <asp:Label Text="" ID="lblUpload" runat="server" />
            </p>
        </fieldset>
        <fieldset>
            <legend>SMTP Settings</legend>
            <p>
                <label>
                    SMTP Server / Port
                </label>
                <asp:Label Text="" ID="lblSMTPServerPort" runat="server" />
            </p>
            <p>
                <label>
                    Username
                </label>
                <asp:Label Text="" ID="lblSMTPUsername" runat="server" />
            </p>
            <p>
                <label>
                    Password
                </label>
                <asp:Label Text="" ID="lblSMTPPassword" runat="server" />
            </p>
            <p>
                <label>
                    SMTP Test
                </label>
                <asp:Label Text="" ID="lblSMTPTest" runat="server" />
            </p>
        </fieldset>
    </div>
    </form>
</body>
</html>
