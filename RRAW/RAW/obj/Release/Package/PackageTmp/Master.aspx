<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Master.aspx.vb" Inherits="RRAW.Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Master.min.css" rel="stylesheet" type="text/css" />
    <title>RRAW (Rate Request and Approval Workflow)</title>
</head>
<body onload="function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ScriptManager>
    <input type="hidden" id="hidCurrentDateTime" />
    <asp:HiddenField ID="hidAccessibleModules" runat="server" />
    <asp:HiddenField ID="hidNavigationSequence" runat="server" />
    <div class="header" id="divHeader">
        <table style="width: 100%">
            <tr>
                <td class="logo">
                    <img id="searceLogo" src="Images/searce_logo.png" alt="Searce" />
                </td>
                <td class="title">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rate Request and Approval Workflow - RRAW<br />
                    <asp:Label ID="lblAppState" runat="server" Font-Bold="true" EnableViewState="false"
                        NavigateUrl="Blog.aspx"></asp:Label>
                </td>
                <td class="login" style="font-size: 12px;">
                    <asp:HyperLink ID="lblAppVersion" runat="server" Font-Bold="true" EnableViewState="false"
                        NavigateUrl="Blog.aspx">Application Version</asp:HyperLink>
                    <br />
                    <br />
                    Welcome
                    <asp:Label runat="server" ID="lblUserName" Font-Bold="True" EnableViewState="False"></asp:Label>
                    <asp:Label runat="server" ID="lblCurrentDateTime" Font-Bold="True" EnableViewState="False"></asp:Label>
                    <br />
                    <a href="javascript:window.top.location = 'Logout.aspx';">Log Out</a>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="sidebar">
                    <div id="menu_pane" class="sidebar-pan scrollable" style="">
                        <ul id="api_menu" class="menu-items">
                            <li>
                                <div class="menu-item">
                                    <a class="section" title="" style="text-align: center">RRAW &nbsp;&nbsp;</a></div>
                                <div id="processing_image">
                                </div>
                                <ul class="">
                                    <li id="dashboard" style="display: none">
                                        <div class="menu-item" id="opsMenu">
                                            <a class="class" href="Dashboard.aspx" target="main" title="" id="dashboardlink"
                                                onclick="locate(this.offsetTop, this.id);">Dashboard</a></div>
                                    </li>
                                    <li id="rates" style="display: none">
                                        <div class="menu-item">
                                            <a class="mixin" href="Tariff.aspx" target="main" title="" id="rateslink" onclick="locate(this.offsetTop, this.id, 'tarifflink');">
                                                Rates</a></div>
                                        <ul>
                                            <li id="tariff" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="mixin" href="Tariff.aspx" target="main" title="" id="tarifflink" onclick="locate(this.offsetTop, 'rateslink', this.id);">
                                                        Air Freight Rates</a></div>
                                            </li>
                                            <li id="approvedasadhoc" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="mixin" href="ApprovedAsAdhoc.aspx" target="main" title="" id="approvedasadhoclink"
                                                        onclick="locate(this.offsetTop, 'rateslink', this.id);">Air Freight Rates (ADHOC)</a></div>
                                            </li>
                                            <li id="oceanrates" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="mixin" href="OceanTariff.aspx" target="main" title="" id="oceanrateslink"
                                                        onclick="locate(this.offsetTop, 'rateslink', this.id);">Ocean Freight Rates</a>
                                                    <!--<a class="mixin" href="OceanRates.aspx" target="main" title="" id="oceanrateslink"
                                                        onclick="locate(this.offsetTop, 'rateslink', this.id);">Ocean Rates</a>-->
                                                </div>
                                            </li>
                                            <li id="truckrates" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="mixin" href="TruckRates.aspx" target="main" title="" id="truckrateslink"
                                                        onclick="locate(this.offsetTop, 'rateslink', this.id);">Truck Freight Rates</a>
                                                    <!--<a class="mixin" href="TruckRates.aspx" target="main" title="" id="truckrateslink"
                                                        onclick="locate(this.offsetTop, 'rateslink', this.id);">Truck Rates</a>-->
                                                </div>
                                            </li>
                                        </ul>
                                    </li>
                                    <li id="raterequests" style="display: none">
                                        <div class="menu-item">
                                            <a class="alias" href="NewRateRequest.aspx" target="main" title="" id="raterequestslink"
                                                onclick="locate(this.offsetTop, this.id, 'newraterequestlink');">Rate Requests</a></div>
                                        <ul>
                                            <li id="newraterequest" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="alias" href="NewRateRequest.aspx" target="main" title="" id="newraterequestlink"
                                                        onclick="locate(this.offsetTop, 'raterequestslink', this.id);">Air Rate Request</a></div>
                                            </li>
                                            <li id="oceanraterequest" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="alias" href="OceanRateRequest.aspx?ver=4" target="main" title="" id="oceanraterequestlink"
                                                        onclick="locate(this.offsetTop, 'raterequestslink', this.id);">Ocean Rate Request</a></div>
                                            </li>
                                            <li id="truckraterequest" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a class="alias" href="TruckRateRequest.aspx" target="main" title="" id="truckraterequestlink"
                                                        onclick="locate(this.offsetTop, 'raterequestslink', this.id);">Truck Rate Request</a></div>
                                            </li>
                                        </ul>
                                    </li>
                                    <li id="fscupdate" style="display: none">
                                        <div class="menu-item">
                                            <a class="class_property" href="FSCUpdateMalaysia.aspx" target="main" title="" id="fscupdatelink"
                                                onclick="locate(this.offsetTop, this.id, 'fscupdatemalaysialink');">FSC Update</a></div>
                                        <ul>
                                            <li id="fscupdatemalaysia" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a target="main" id="fscupdatemalaysialink" class="class_property" href="FSCUpdateMalaysia.aspx"
                                                        title="" onclick="locate(this.offsetTop, 'fscupdatelink',this.id);">FSC Update -
                                                        Malaysia</a></div>
                                            </li>
                                            <li id="fscupdatethailand" class="sub-menu" style="display: none">
                                                <div class="menu-item">
                                                    <a target="main" id="fscupdatethailandlink" href="FSCUpdateThailand.aspx" class="class_property"
                                                        title="" onclick="locate(this.offsetTop, 'fscupdatelink',this.id);">FSC Update -
                                                        Thailand</a></div>
                                            </li>
                                        </ul>
                                    </li>
                                    <li id="changepassword" style="display: none">
                                        <div class="menu-item">
                                            <a id="changepasswordlink" class="constructor" href="ChangePassword.aspx" title=""
                                                target="main" onclick="locate(this.offsetTop, this.id);">Change Password</a></div>
                                    </li>
                                    <li id="userreports" style="display: none">
                                        <div class="menu-item">
                                            <a id="userreportslink" class="user" href="UserReports.aspx" title="" target="main"
                                                onclick="locate(this.offsetTop, this.id);">User Reports</a></div>
                                    </li>
                                    <li id="adminpanel" style="display: none">
                                        <div class="menu-item">
                                            <a id="useradministrationlink" class="constructor" href="AdminPanel.aspx" title=""
                                                target="main" onclick="locate(this.offsetTop, this.id);">User Administration</a></div>
                                    </li>
                                    <li id="rrawonlinemanual" style="display: inline">
                                        <div class="menu-item">
                                            <a id="rrawonlinemanuallink" class="description" href="https://docs.google.com/document/d/1W9ws6oC-efc5xmuTr1cTjcFUSMmNgt9iOAs_FP_ncGc/edit?hl=en&authkey=CPe_1bAJ"
                                                title="" target="_blank" style="">RRAW Manual</a></div>
                                    </li>
                                    <li id="searcehelpdesk" style="display: inline">
                                        <div class="menu-item">
                                            <a id="searcehelpdesklink" class="description" href="http://searce.com/help" title=""
                                                target="_blank" style="">Searce Help Desk</a></div>
                                    </li>
                                    <!--<li id="test" style="display: inline">
                                        <div class="menu-item">
                                            <a id="testlink" class="description" href="WebForm1.aspx" title="" target="main">Test</a></div>
                                    </li>-->
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </td>
            <td>
                <div id="page">
                    <iframe runat="server" frameborder="0" id="mainFrame" name="main" class="page-frame"
                        src="Dashboard.aspx"></iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
<script src="Scripts/Master.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>
