<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MasterMobile.aspx.vb"
    Inherits="RRAW.MasterMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RRAW (Rate Request and Approval Workflow)</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ScriptManager>
    <input type="hidden" id="hidCurrentDateTime" />
    <asp:HiddenField ID="hidAccessibleModules" runat="server" />
    <asp:HiddenField ID="hidNavigationSequence" runat="server" />
    <div data-role="page" id="foo">
        <div data-role="header" data-theme="b">
            <h1>
                Rate Request and Approval Workflow - RRAW</h1>
            <a href="javascript:window.top.location = 'Login.aspx';" data-role="button" data-icon="delete">
                Log Out&nbsp;<asp:Label runat="server" ID="lblUserName" Font-Bold="True" EnableViewState="False"></asp:Label></a>
        </div>
        <div data-role="content" data-theme="b">
            <div id="content-navigation">
                <ul data-role="listview">
                    <li><a class="class" href="Dashboard.aspx" title="" id="dashboardlink">Dashboard</a></li>
                    <li><a class="mixin" href="Tariff.aspx" title="" id="tarifflink">Air Freight Rates</a></li>
                    <li><a class="mixin" href="ApprovedAsAdhoc.aspx" title="" id="approvedasadhoclink">Air
                        Freight Rates (ADHOC)</a></li>
                    <li><a class="mixin" href="OceanTariff.aspx" title="" id="oceanrateslink">Ocean Freight
                        Rates</a></li>
                    <li><a class="mixin" href="TruckRates.aspx" title="" id="truckrateslink">Truck Rates</a></li>
                    <li><a class="mixin" href="NewRateRequest.aspx" title="" id="A1">Air Rate Request</a></li>
                    <li><a class="mixin" href="OceanRateRequest.aspx" title="" id="A2">Ocean Rate Request</a></li>
                    <li><a class="mixin" href="NewTruckRateRequest.aspx" title="" id="A3">Truck Rate Request</a></li>
                    <li><a class="mixin" href="DocumentStorage.aspx" title="" id="A4">FSC Update - Malaysia</a></li>
                    <li><a class="mixin" href="FSCUpdateThailand.aspx" title="" id="A5">FSC Update - Thailand</a></li>
                    <li><a class="mixin" href="ChangePassword.aspx" title="" id="A6">Change Password</a></li>
                    <li><a class="mixin" href="UserReports.aspx" title="" id="A7">User Reports</a></li>
                    <li><a class="mixin" href="AdminPanel.aspx" title="" id="A8">Admin Panel</a></li>
                    <li><a class="mixin" href="https://docs.google.com/document/d/1W9ws6oC-efc5xmuTr1cTjcFUSMmNgt9iOAs_FP_ncGc/edit?hl=en&authkey=CPe_1bAJ.aspx"
                        title="" id="A9">RRAW Manul</a></li>
                    <li><a class="mixin" href="http://searce.com/help.aspx" title="" id="A10">Searce Help
                        Desk</a></li>
                </ul>
            </div>
        </div>
        <div data-role="footer">
            <a href="http://searce.com">Searce</a><a href="Blog.aspx" id="lblAppVersion" data-role="button"
                data-icon="gear" style="float: right; margin: 5px 10px 7px 0"></a>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://code.jquery.com/jquery-1.6.2.min.js"></script>
<script src="http://code.jquery.com/mobile/1.0b3/jquery.mobile-1.0b3.min.js"></script>
</html>
