<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RRAW.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RRAW (Rate Request and Approval Workflow)</title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <!--<link href="http://rrawcdn.appspot.com/css/Global.min.css" rel="stylesheet" type="text/css" />-->
</head>
<body onload="function w(){return screen.width} function h(){return screen.height} document.getElementById('hidScreenWidth').value=w(); document.getElementById('hidScreenHeight').value=h();function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();document.getElementById('txtUserID').focus();">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <div class="page_title">
            Login to RRAW System
            <br />
            <asp:Label ID="lblAppState" runat="server" Text=""></asp:Label>
            </div>
        
        <br />
        <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
        <asp:HiddenField ID="hidScreenWidth" runat="server" EnableViewState="False" />
        <asp:HiddenField ID="hidScreenHeight" runat="server" EnableViewState="False" />
        <table style="margin: auto">
            <tr>
                <td style="text-align: left">
                    User ID:
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtUserID" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Password:
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Button runat="server" ID="btnLogin" Text="Login" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Invalid Username or Password.<br> Please try again."
                        Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
