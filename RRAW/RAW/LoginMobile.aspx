<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginMobile.aspx.vb" Inherits="RRAW.LoginMobile"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RRAW (Rate Request and Approval Workflow)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--<link rel="stylesheet" type="text/css" href="CSS/jquery.mobile-1.0b3.min.css" />-->
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.2.min.js"></script>
    <!--<script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>-->
    <script type="text/javascript" src="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.js"></script>
    <!--<script src="Scripts/jquery.mobile-1.0b3.min.js" type="text/javascript"></script>-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Mobile.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <div data-role="page" id="LoginMobilePage">
        <div data-role="header" data-theme="b">
            <h1>
                RRAW Login</h1>
        </div>
        <div data-role="content" data-theme="b">
            <input type="text" id="txtUserID" data-theme="c" placeholder="User ID" style="width: 99%" />
            <input type="password" id="txtPassword" data-theme="c" placeholder="Password" style="width: 99%" />
            <input id="btnLogin" type="button" value="Login" data-theme="b" data-transition="fade" />
        </div>
        <div data-role="footer" style="padding: 5px 0">
            <a href="http://searce.com" data-ajax="false">Searce</a>&nbsp;&nbsp;&nbsp;<span id="lblError"
                style="color: Red; display: none;">Invalid Username or Password. Please try again.</span>
        </div>
        <script src="Scripts/LoginMobile.js" type="text/javascript"></script>
    </div>
    </form>
</body>
</html>
