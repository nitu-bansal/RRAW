<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Master.aspx.vb" Inherits="RRAW.Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <link href="jquery-ui-1.9.2.overcast/css/overcast/jquery-ui-1.9.2.custom.css" rel="stylesheet"
        type="text/css" />
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Master.min.css" rel="stylesheet" type="text/css" />
    <title>RRAW (Rate Request and Approval Workflow)</title>
</head>
<body onload="function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();">
    <div class="outerpart">
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
                    <td>
                        <div class="logo">
                            <img id="Img1" src="Images/cevalogo.png" alt="Ceva" />
                        </div>
                    </td>
                    <td class="title">
                       <div class="centerLogo">
                       <%-- <img id="cevaLogo" src="Images/RrawLogo.png" alt="Ceva" />
                        --%>
                         <p style="font-weight:bold;color:#000000;letter-spacing:6pt;word-spacing:4pt;font-size:25x;text-align:left;font-family:arial, helvetica, sans-serif;line-height:0;">Rate Request &amp; Approval Workflow(RRAW)</p>
                       
                       </div>
                        <asp:Label ID="lblAppState" runat="server" Font-Bold="true" EnableViewState="false"
                            NavigateUrl="Blog.aspx" CssClass="hideControl"></asp:Label>
                    </td>
                    <td class="login" style="font-size: 12px;">
                        <asp:HyperLink ID="lblAppVersion" runat="server" Font-Bold="true" EnableViewState="false"
                            NavigateUrl="Blog.aspx">Application Version</asp:HyperLink>
                        <br />
                        Welcome
                        <asp:Label runat="server" ID="lblUserName" Font-Bold="True" EnableViewState="False"></asp:Label>
                        &nbsp<a href="javascript:window.top.location = 'Logout.aspx';"> Log Out</a>
                        <asp:Label runat="server" ID="lblCurrentDateTime" Font-Bold="True" EnableViewState="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="ui-widget-header ui-state-active  bluebar"  style="height: 25px;">
        </div>
        <table cellpadding="0" cellspacing="0" border="0" width="98%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td rowspan="2">

  <div id="panel"> <!--the hidden panel -->
       <div class="content">
              <!--insert your content here -->
              <div id="sidebar">
                        <div id="menu_pane" class="sidebar-pan" style="">
                            <ul id="api_menu" class="menu-items">
                                <li>
                                    <div class="menu-item ui-accordion-header ui-helper-reset ui-state-default ui-state-active"">
                                     Client: &nbsp &nbsp<asp:DropDownList ID="cmbClients" AutoPostBack="true" OnSelectedIndexChanged="cmbClients_SelectedIndexChanged"
                                                        CssClass="CenterAlign, textTopAligned" runat="server" DataTextField="Name" DataValueField="ID"
                                                        Style="width: 150px; height: 21px">
                                                    </asp:DropDownList>
                                    </div>
                                  
                                    <div id="processing_image">
                                    </div>
                                    <ul class="menu-items">
                                        <li id="DASHBOARD" style="display: none">
                                            <div class="menu-item" id="opsMenu">
                                                <a class="class" href="Dashboard.aspx" target="main" title="" id="dashboardlink"
                                                    onclick="ShowHideClient('dashboardlink');locate(this.offsetTop, this.id);">Dashboard</a></div>
                                        </li>
                                        <li id="rates" style="display: none">
                                            <div class="menu-item">
                                                <a class="mixin"  target="main" title="" id="rateslink">
                                                    View Tariff</a></div>
                                            <ul>
                                                <li id="TARIFF_NEW" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="Tariff_New.aspx" target="main" title="Air-New Tariff effectives 15th Jul’12"
                                                            id="tariff_newlink" onclick="ShowHideClient('tariff_newlink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Air</a></div>
                                                </li>
                                                <li id="OCEANTARIFF_NEW" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="OceanTariff_New.aspx" target="main" title="Ocean-New Tariff effectives 15th Jul’12"
                                                            id="oceantariff_newlink" onclick="ShowHideClient('oceantariff_newlink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Ocean</a></div>
                                                </li>
                                                <li id="TRUCKRATES" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="TruckRates.aspx" target="main" title="" id="truckrateslink"
                                                            onclick="ShowHideClient('truckrateslink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Ground</a>
                                                    </div>
                                                </li>
                                                <li id="TARIFF" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="Tariff.aspx?Active=2" target="main" title="Old Tariff rate expired 14th Jul’12"
                                                            id="tarifflink" onclick="ShowHideClient('tarifflink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Old AIR Tariff </a>
                                                    </div>
                                                </li>
                                                <li id="INACTIVETARIFF" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="Tariff.aspx?Active=1" target="main" title="Inactive Old Tariff rate expired 14th Jul’12"
                                                            id="inactivetarifflink" onclick="ShowHideClient('inactivetarifflink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Inactive Old AIR Tariff</a></div>
                                                </li>
                                                <li id="APPROVEDASADHOC" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="ApprovedAsAdhoc.aspx" target="main" title="" id="approvedasadhoclink"
                                                            onclick="ShowHideClient('approvedasadhoclink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Old Air Freight (ADHOC)</a></div>
                                                </li>
                                                <li id="OCEANRATES" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="mixin" href="OceanTariff.aspx" target="main" title="Old Ocean Tariff expired 14th Jul’12"
                                                            id="oceanrateslink" onclick="ShowHideClient('oceanrateslink');locate(this.offsetTop, 'rateslink', this.id);">
                                                            Old Ocean Tariff</a>
                                                    </div>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="raterequests" style="display: none">
                                            <div class="menu-item">
                                                <a class="alias"  target="main" title="" id="raterequestslink">
                                                    Rate Requests</a></div>
                                            <ul>
                                                <li id="NEWRATEREQUEST" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="alias" href="" target="main" title="" id="newraterequestlink"
                                                            onclick="locate(this.offsetTop, 'raterequestslink', this.id);">Air - Old</a></div>
                                                </li>
                                                <li id="NEWAIRRATEREQUEST" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="alias" href="NewAirRateRequest.aspx?TransPortModeId=1" target="main" title=""
                                                            id="newraterequestlink_15july" onclick="locate(this.offsetTop, 'raterequestslink', this.id);">
                                                            Air</a></div>
                                                </li>
                                                <li id="OCEANRATEREQUEST" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="alias" href="NewOceanRateRequest.aspx?TransPortModeId=2" target="main"
                                                            title="" id="oceanraterequestlink" onclick="locate(this.offsetTop, 'raterequestslink', this.id);">
                                                            Ocean</a></div>
                                                </li>
                                                <li id="NEWGROUNDRATEREQUEST" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="alias" href="NewGroundRateRequest.aspx?TransPortModeId=3" target="main"
                                                            title="" id="newgroundraterquestlink" onclick="locate(this.offsetTop, 'raterequestslink', this.id);">
                                                            Ground</a></div>
                                                </li>
                                                <li id="TRUCKRATEREQUEST" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a class="alias" href="TruckRateRequest.aspx" target="main" title="" id="truckraterequestlink"
                                                            onclick="locate(this.offsetTop, 'raterequestslink', this.id);">Truck</a></div>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="fscupdate" style="display: none">
                                            <div class="menu-item">
                                                <a class="class_property"  target="main" title="" id="fscupdatelink">
                                                    Documents</a></div>
                                            <ul>
                                                <li id="FSCINTLAIRFREIGHTRULES" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a target="main" id="fscintlairfreightruleslink" href="FSCIntnlAirFreightfuelRules.aspx"
                                                            class="class_property" title="" onclick="ShowHideClient('fscintlairfreightruleslink');locate(this.offsetTop, 'fscupdatelink',this.id);">
                                                            WD Int'l Air FSC Rules</a></div>
                                                </li>
                                                <li id="FSCUPDATEMALAYSIA" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a target="main" id="fscupdatemalaysialink" class="class_property" href="DocumentStorage.aspx"
                                                            title="" onclick="ShowHideClient('fscupdatemalaysialink');locate(this.offsetTop, 'fscupdatelink',this.id);">
                                                            Document Storage</a></div>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="knowledgebase" style="display: none">
                                            <div class="menu-item">
                                                <a class="description" href="IdentifyEuropeanZone.aspx" target="main" title="" id="knowledgebaselink"
                                                    onclick="ShowHideClient('knowledgebaselink');locate(this.offsetTop, this.id, 'identifyeuropeanzonelink');">
                                                    Knowledge Base</a></div>
                                            <ul>
                                                <li id="IDENTIFYEUROPEANZONE" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a target="main" id="identifyeuropeanzonelink" class="description" href="IdentifyEuropeanZone.aspx"
                                                            title="" onclick="ShowHideClient('identifyeuropeanzonelink');locate(this.offsetTop, 'knowledgebaselink',this.id);">
                                                            European Zones Code</a></div>
                                                </li>
                                                <li id="RRAWONLINEMANUAL" class="sub-menu" style="display: block">
                                                    <div class="menu-item">
                                                        <a id="rrawonlinemanuallink" class="description" href="https://docs.google.com/document/d/1W9ws6oC-efc5xmuTr1cTjcFUSMmNgt9iOAs_FP_ncGc/edit?hl=en&authkey=CPe_1bAJ"
                                                            title="" target="_blank" style="" onclick="ShowHideClient('rrawonlinemanuallink');locate(this.offsetTop, 'knowledgebaselink',this.id);">
                                                            RRAW Manual</a></div>
                                                </li>
                                                <li id="SEARCEHELPDESK" class="sub-menu" style="display: block">
                                                    <div class="menu-item">
                                                        <a id="searcehelpdesklink" class="description" href="http://searce.com/help" title=""
                                                            target="_blank" style="" onclick="ShowHideClient('searcehelpdesklink');locate(this.offsetTop, 'knowledgebaselink',this.id);">
                                                            Searce Help Desk</a></div>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="CHANGEPASSWORD" style="display: none">
                                            <div class="menu-item">
                                                <a id="changepasswordlink" class="constructor" href="ChangePassword.aspx" title=""
                                                    target="main" onclick="ShowHideClient('changepasswordlink');locate(this.offsetTop, this.id);">
                                                    Change Password</a></div>
                                        </li>
                                        <li id="USERREPORTS" style="display: none">
                                            <div class="menu-item">
                                                <a id="userreportslink" class="user" href="UserReports.aspx" title="" target="main"
                                                    onclick="ShowHideClient('userreportslink');locate(this.offsetTop, this.id);">User
                                                    Reports</a></div>
                                        </li>
                                        <li id="admin" style="display: none">
                                            <div class="menu-item">
                                                <a class="constructor" target="main" title="" id="adminlink">
                                                    Client Configuration</a></div>
                                            <ul>
                                                <li id="ADMINPANEL" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a id="useradministrationlink" class="constructor" href="AdminPanel.aspx" title=""
                                                            target="main" onclick="ShowHideClient('useradministrationlink');locate(this.offsetTop, this.id);">
                                                            General Configuration</a></div>
                                                </li>
                                                <li id="RATEREQUESTWORKFLOW" class="sub-menu" style="display: none">
                                                    <div class="menu-item">
                                                        <a id="raterequestworkflowlink" class="constructor" href="RateRequestWorkFlow.aspx"
                                                            title="" target="main" onclick="ShowHideClient('raterequestworkflowlink');locate(this.offsetTop, this.id);">
                                                            Workflow Configuration</a></div>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
       </div>	
</div>
<!--if javascript is disabled use this link-->
	<%--<a href="/about.html" onclick="return()">--%>
              <div id="tab" class="ui-dialog-titlebar"> <!-- this will activate your panel. -->
              <table>
              <tr>
              <td>
              <p>&nbsp Menu</p>
              </td>
              
              <td>
               <img class="unpinnedmenu" src="Images/unpinned.png" alt="" style="display:none" /><img class="pinnedmenu"  src="Images/pinned.png" alt="" style="display:none" />
              </td>
              </tr>
              </table>
             
              </div> 
       <%--  </a>--%>
         
                    
                </td>
                <td>
                    <div id="page" class="">
                        <iframe runat="server" frameborder="0" id="mainFrame" name="main" class="page-frame"
                            src="Dashboard.aspx"></iframe>
                    </div>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div class="ui-widget-header  ui-state-default">
        <center>
              <span>© 2013 <a href="http://searce.com" target="_blank"></a>Searce. Invoize Suite! All Rights Reserved. </span>
        </center>
    </div>
</body>
<script src="jquery-ui-1.9.2.overcast/js/jquery-1.8.3.js" type="text/javascript"></script>
<script src="Scripts/Master.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
</html>
