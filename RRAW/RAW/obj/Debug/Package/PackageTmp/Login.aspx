<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RRAW.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RRAW (Rate Request and Approval Workflow)</title>
    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link href="jquery-ui-1.9.2.overcast/css/overcast/jquery-ui-1.9.2.custom.css" rel="stylesheet"
        type="text/css" />
</head>
<body onload="function w(){return screen.width} function h(){return screen.height} document.getElementById('hidScreenWidth').value=w(); document.getElementById('hidScreenHeight').value=h();function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();document.getElementById('txtUserID').focus();">
    <form id="form1" runat="server">
    <div id="LoginContainerbox">
        <div id="login-box">
            <div style="text-align: center;">
                <asp:Label ID="lblAppState" runat="server" Text=""></asp:Label></div>
            <img src="images/cevalogo.png" alt="Logo" />
            <br />
            <br />
            <span class=" ui-state-default">Please provide your <b>RAW</b> credentials to access
                portal. </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div id="login-box-field" style="margin-top: 20px;">
                <label style="margin-left: 30px;" for="txtUserID">
                    UserName
                </label>
                <asp:TextBox ID="txtUserID" class="form-login inputbox" runat="server" Width="190px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="User Name Required"
                    ControlToValidate="txtUserID" ForeColor="Yellow" CssClass="validators"></asp:RequiredFieldValidator>
                <br />
            </div>
            <div id="login-box-field">
                <label style="margin-left: 30px;" for="txtPassword">
                    Password &nbsp</label>
                <asp:TextBox ID="txtPassword" class="form-login inputbox" runat="server" TextMode="Password"
                    Width="190px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password Required"
                    ControlToValidate="txtPassword" ForeColor="Yellow" CssClass="validators"> </asp:RequiredFieldValidator>
            </div>
            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/login-btn.png" Width="77px"
                Height="32px" Style="margin-left: 215px;"  />
            <br />
            <span style="margin-left: 35px;">
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Invalid Username or Password.Please try again."
                    Visible="False"></asp:Label>
            </span>
            <br />
            <div style="text-align: center">
                <span>© 2013 <a href="http://searce.com" target="_blank">Searce. Invoize Suite!</a> All Rights Reserved.
                </span>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
    <asp:HiddenField ID="hidScreenWidth" runat="server" EnableViewState="False" />
    <asp:HiddenField ID="hidScreenHeight" runat="server" EnableViewState="False" />
    </form>
</body>
</html>
