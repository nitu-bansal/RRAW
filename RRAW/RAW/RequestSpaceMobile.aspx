<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RequestSpaceMobile.aspx.vb"
    Inherits="RRAW.RequestSpaceMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Mobile.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <div data-role="page" id="RequestSpaceMobilePage">
        <div data-role="header" data-theme="b">
            <a href="#" data-rel="back" data-icon="arrow-l">Back</a>
            <h1>
                RRAW Request Space</h1>
            <a href="Navigation.htm" data-icon="home" data-role="button">Home</a>
        </div>
        <div data-role="content" data-theme="b">
            <ul data-role="listview" data-filter="true" data-filter-placeholder="Search request..."
                data-filter-theme="b" class="ui-listview" id="listRequests">
            </ul>
        </div>
        <div data-role="footer" style="padding: 5px 0">
            <a href="LoginMobile.aspx" data-role="button" data-icon="delete" data-transition="slide">Log
                Out&nbsp;<span id="lblUserNameRequestSpaceMobile"></span></a><a href="Blog.aspx"
                    data-role="button" style="float: right" data-ajax="false"><span id="lblAppVersionRequestSpaceMobile"></span>
                </a>
        </div>
        <script src="Scripts/RequestSpaceMobile.js" type="text/javascript"></script>
    </div>
    </form>
</body>
</html>
