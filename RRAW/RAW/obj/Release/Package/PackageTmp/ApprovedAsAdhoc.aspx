<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ApprovedAsAdhoc.aspx.vb"
    Inherits="RRAW.ApprovedAsAdhoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/ApprovedAsAdhoc.min.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};javascript:document.getElementById('txtHAWBNumber').focus();function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();">
    <form id="form1" runat="server" defaultbutton="btnFilter">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <div class="page_title">
            Approved As Adhoc Rates</div>
    </div>
    <br />
    <br />
    <div id="divTariff" style="height: 504px; overflow: auto; width: 3779px;">
        <table id="tblFilters" style="width: 3761px; text-align: center; position: absolute;
            overflow: scroll; /*width: 3595px; */ box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);" cellpadding="5" cellspacing="0">
            <tr>
                <td bgcolor="#507CD1">
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    HAWB
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Request ID
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Customer
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Origin Airport
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Origin Region
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Origin Code
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest Airport
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest City
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest State
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest Country
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest Region
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Dest Zipcode
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    CEVA Transit Mode
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Ship Method
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Forwarder Zipcode
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Custom Clearance Mode
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Forwarder Service Level
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Min Freight Rate
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Freight Rate
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Security Rate
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Other Charges
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Effective Date
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Freight Frwd.
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Active
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Notes
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Approval Date
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Approved By
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Approval Notes
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Additional Notes
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Rate Request Date
                </td>
                <td align="center" bgcolor="#507CD1" style="color: #fff; font-weight: 700;">
                    Entry Date
                </td>
            </tr>
            <tr>
                <td width="34px" style="min-width: 34px" bgcolor="#EFF3FB">
                </td>
                <td width="100px" valign="top" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtHAWBNumber" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtHAWBNumber" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="HAWBNumber">
                    </asp:AutoCompleteExtender>
                </td>
                <td width="60px" valign="top" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtRateRequestID" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtRateRequestID" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ID">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="160px" bgcolor="#EFF3FB" align="right" style="padding-right: 6px;
                    min-width: 160px">
                    <asp:TextBox ID="txtCustomer" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtCustomer" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ConsigneeName">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtOriginAirport" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtOriginAirport_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetApprovedAsAdhocAutoComplete" TargetControlID="txtOriginAirport"
                        UseContextKey="True" CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1"
                        ContextKey="OriginAirport">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtOriginRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtOriginRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtOriginCode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtOriginCode" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginZipCode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtDestAirport" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestAirport" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestAirport">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="150px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 150px">
                    <asp:TextBox ID="txtDestCity" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestCity" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestCity">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtDestState" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestState" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestState">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtDestCountry" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestCountry" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestCountry">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtDestRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestRegion" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtDestZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtDestZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtCEVATransitMode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtCEVATransitMode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="CEVATransitMode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="200px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 200px">
                    <asp:TextBox ID="txtShipMethod" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender27" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtShipMethod" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ShipMethod">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 70px">
                    <asp:TextBox ID="txtForwarderZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender26" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtForwarderZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ForwarderZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 70px">
                    <asp:TextBox ID="txtCustomClearanceMode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender25" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtCustomClearanceMode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="CustomClearanceMode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 70px">
                    <asp:TextBox ID="txtForwarderServiceLevel" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender24" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtForwarderServiceLevel" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ForwarderServiceLevel">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtMinFreightRate" runat="server" Width="100%" CssClass="alignRight"
                        AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender23" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtMinFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="MinFreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtFreightRate" runat="server" Width="100%" CssClass="alignRight"
                        AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender22" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtSecurityRate" runat="server" Width="100%" CssClass="alignRight"
                        AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtSecurityRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="SecurityRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtOtherCharges" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender20" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtOtherCharges" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OtherCharges">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 70px">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtEffectiveDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEffectiveDate" Animated="False">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:TextBox ID="txtFreightForwarder" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender16" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtFreightForwarder" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightForwarder">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 60px">
                    <asp:DropDownList ID="cmbActive" runat="server" Width="100%" Enabled="False">
                        <asp:ListItem Selected="True">Any</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 300px">
                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetApprovedAsAdhocAutoComplete"
                        TargetControlID="txtNotes" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginComment">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="145px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 145px">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtApprovalDateFrom" runat="server" Width="100%" AutoCompleteType="Disabled"
                                    CssClass="align_center"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txtApprovalDateFrom_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtApprovalDateFrom" WatermarkCssClass="water_mark"
                                    WatermarkText="From Date">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="txtApprovalDateFrom_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtApprovalDateFrom" Animated="False" Format="MM/dd/yyyy">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtApprovalDateTo" runat="server" Width="100%" AutoCompleteType="Disabled"
                                    CssClass="align_center"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txtApprovalDateTo_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtApprovalDateTo" WatermarkCssClass="water_mark"
                                    WatermarkText="To Date">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtApprovalDateTo"
                                    Animated="true" Format="MM/dd/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 100px">
                    <asp:TextBox ID="txtApprovedBy" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 300px">
                    <asp:TextBox ID="txtApprovalNotes" runat="server" Width="100%" AutoCompleteType="Disabled"
                        Enabled="False"></asp:TextBox>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 300px">
                    <asp:TextBox ID="txtAdditionalNotes" runat="server" Width="100%" AutoCompleteType="Disabled"
                        Enabled="False"></asp:TextBox>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 6px; min-width: 70px">
                    <asp:TextBox ID="txtRateRequestDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtRateRequestDate_CalendarExtender" runat="server" Animated="False"
                        Enabled="True" TargetControlID="txtRateRequestDate">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 10px; min-width: 70px">
                    <asp:TextBox ID="txtEntryDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEntryDate" Animated="False">
                    </asp:CalendarExtender>
                    <br />
                </td>
            </tr>
            <tr>
                <td id="tdOperations" colspan="32" align="left" style="height: 40px;">
                    <div id="divOperations" style="position: fixed; margin-top: -18px">
                        <asp:Button ID="btnFilter" runat="server" OnClientClick="DisplayProgress();" Text="Apply Filter" />&nbsp;<asp:Button
                            ID="btnRemoveFilter" runat="server" OnClientClick="DisplayProgress(); ClearFields();"
                            Text="Remove Filter" />&nbsp;<asp:Button ID="btnExportToCSV" runat="server" EnableViewState="False"
                                Visible="False" Text="Export To CSV" />&nbsp;<asp:Button ID="btnExportToExcel" runat="server"
                                    EnableViewState="False" Visible="False" Text="Export To Excel" />&nbsp;<asp:Button
                                        ID="btnRemoveRequest" runat="server" EnableViewState="False" Visible="False"
                                        Enabled="false" OnClientClick="DisplayProgress();" Text="Remove" />&nbsp;<asp:Button
                                            ID="btnTransferToTariff" runat="server" EnableViewState="False" Visible="False"
                                            Enabled="false" OnClientClick="DisplayProgress();" Text="Transfer To Tariff" />&nbsp;<asp:Button
                                                ID="btnGenerateNewRateRequest" runat="server" EnableViewState="False" Visible="False"
                                                Enabled="false" OnClientClick="DisplayProgress();" Text="Generate New Rate Request" />
                        <br />
                    </div>
                </td>
            </tr>
        </table>
        <div id="divOperationAdjustments" style="height: 133px;">
        </div>
        <!--<div style="position: absolute; left: 50%; top: 50%; width: 32px;">-->
        <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="False" DisplayAfter="500">
            <ProgressTemplate>
                <div id="DivUpdateProgress2">
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                <div class="loading_notifier">
                                    <table class="loading_notifier_box">
                                        <tbody>
                                            <tr>
                                                <td valign="middle" style="font-size: 10px; font-weight: 700;">
                                                    Processing... please wait
                                                    <div class="processing_image">
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <!--</div>-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivUpdateProgress">
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                <div class="loading_notifier">
                                    <table class="loading_notifier_box">
                                        <tbody>
                                            <tr>
                                                <td valign="middle" style="font-size: 10px; font-weight: 700;">
                                                    Processing... please wait
                                                    <div class="processing_image">
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hidSelections" runat="server" />
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Above lane is not availble has been previously approved as <span style='color:red; font-weight:bold'>ADHOC</span> as described below."
                    Visible="False"></asp:Label>
                <!--Width="3695px"-->
                <asp:GridView ID="gridApprovedAsAdhoc" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" ForeColor="#333"
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth" PageSize="20" EnableViewState="false"
                    ShowHeader="true">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="HAWB Number" HeaderText="HAWB Number" SortExpression="HAWB Number" />
                        <asp:BoundField DataField="RateRequestID" HeaderText="Request ID" SortExpression="RateRequestID" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="Origin Airport" HeaderText="Origin Airport" SortExpression="Origin Airport" />
                        <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region" />
                        <asp:BoundField DataField="Origin Code" HeaderText="Origin Code" SortExpression="Origin Code" />
                        <asp:BoundField DataField="Dest Airport" HeaderText="Dest Airport" SortExpression="Dest Airport" />
                        <asp:BoundField DataField="Dest City" HeaderText="Dest City" SortExpression="Dest City" />
                        <asp:BoundField DataField="Dest State" HeaderText="Dest State" SortExpression="Dest State" />
                        <asp:BoundField DataField="Dest Country" HeaderText="Dest Country" SortExpression="Dest Country" />
                        <asp:BoundField DataField="Dest Region" HeaderText="Dest Region" SortExpression="Dest Region" />
                        <asp:BoundField DataField="Dest Zipcode" HeaderText="Dest Zipcode" SortExpression="Dest Zipcode" />
                        <asp:BoundField DataField="CEVA Transit Mode" HeaderText="CEVA Transit Mode" SortExpression="CEVA Transit Mode" />
                        <asp:BoundField DataField="Ship Method" HeaderText="Ship Method" SortExpression="Ship Method" />
                        <asp:BoundField DataField="Forwarder Zipcode" HeaderText="Forwarder Zipcode" SortExpression="Forwarder Zipcode" />
                        <asp:BoundField DataField="Custom Clearance Mode" HeaderText="Custom Clearance Mode"
                            SortExpression="Custom Clearance Mode" />
                        <asp:BoundField DataField="Forwarder Service Level" HeaderText="Forwarder Service Level"
                            SortExpression="Forwarder Service Level" />
                        <asp:BoundField DataField="Min Freight Rate" HeaderText="Min Freight Rate" ReadOnly="True"
                            SortExpression="Min Freight Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Freight Rate" HeaderText="Freight Rate" ReadOnly="True"
                            SortExpression="Freight Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Security Rate" HeaderText="Security Rate" ReadOnly="True"
                            SortExpression="Security Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Other Charges" HeaderText="Other Charges" ReadOnly="True"
                            SortExpression="Other Charges" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" ReadOnly="True"
                            SortExpression="Effective Date" />
                        <asp:BoundField DataField="Freight Forwarder" HeaderText="Freight Forwarder" SortExpression="Freight Forwarder" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="Approval Date">
                            <HeaderStyle Width="145px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" SortExpression="Approved By" />
                        <asp:BoundField DataField="Approval Notes" HeaderText="Approval Notes" SortExpression="Approval Notes" />
                        <asp:BoundField DataField="Additional Notes" HeaderText="Additional Notes" SortExpression="Additional Notes" />
                        <asp:BoundField DataField="Rate Request Date" HeaderText="Rate Request Date" ReadOnly="True"
                            SortExpression="Rate Request Date" />
                        <asp:BoundField DataField="Entry Date" HeaderText="Entry Date" ReadOnly="True" SortExpression="Entry Date" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                        NextPageText="Next" PreviousPageText="Previous" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="FixedPosition"
                        Font-Bold="True" VerticalAlign="Top" BorderStyle="none" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure"
                    ConnectionString="<%$ ConnectionStrings:RRAW.My.MySettings.RRAWDatabaseConnectionLOCAL %>"
                    SelectCommand="GetApprovedAsAdhocRates"></asp:SqlDataSource>
                <asp:Label ID="lblRateRequestRemoved" runat="server" Font-Bold="true" Text="Specified Approved As Adhoc Rate has been removed from RRAW Portal."
                    Visible="False"></asp:Label>
                <asp:Label ID="lblTransferedToTariff" runat="server" Font-Bold="true" Text="Specified Approved As Adhoc Rate has been transfered to Tariff."
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="btnRefresh" runat="server" Visible="False">Click here to Refresh</asp:LinkButton>
                <asp:LinkButton ID="btnTariff" runat="server" Visible="False">Click here to navigate to the Tariff page</asp:LinkButton>
                <asp:Label ID="lblTariffLaneFound" runat="server" Font-Bold="true" Text="Above lane is available in <span style='color:red; font-weight:bold'>TARIFF</span> as described below."
                    Visible="False"></asp:Label>
                <asp:GridView ID="gridAdhocLanes" runat="server" AllowPaging="True" AllowSorting="True"
                    Visible="false" AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True"
                    ForeColor="#333" GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth"
                    PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Origin Airport" HeaderText="Origin Airport" SortExpression="Origin Airport" />
                        <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region" />
                        <asp:BoundField DataField="Origin Code" HeaderText="Origin Code" SortExpression="Origin Code" />
                        <asp:BoundField DataField="Dest Airport" HeaderText="Dest Airport" SortExpression="Dest Airport" />
                        <asp:BoundField DataField="Dest City" HeaderText="Dest City" SortExpression="Dest City" />
                        <asp:BoundField DataField="Dest State" HeaderText="Dest State" SortExpression="Dest State" />
                        <asp:BoundField DataField="Dest Country" HeaderText="Dest Country" SortExpression="Dest Country" />
                        <asp:BoundField DataField="Dest Region" HeaderText="Dest Region" SortExpression="Dest Region" />
                        <asp:BoundField DataField="Dest Zipcode" HeaderText="Dest Zipcode" SortExpression="Dest Zipcode" />
                        <asp:BoundField DataField="CEVA Transit Mode" HeaderText="CEVA Transit Mode" SortExpression="CEVA Transit Mode" />
                        <asp:BoundField DataField="Ship Method" HeaderText="Ship Method" SortExpression="Ship Method" />
                        <asp:BoundField DataField="Forwarder Zipcode" HeaderText="Forwarder Zipcode" SortExpression="Forwarder Zipcode" />
                        <asp:BoundField DataField="Custom Clearance Mode" HeaderText="Custom Clearance Mode"
                            SortExpression="Custom Clearance Mode" />
                        <asp:BoundField DataField="Forwarder Service Level" HeaderText="Forwarder Service Level"
                            SortExpression="Forwarder Service Level" />
                        <asp:BoundField DataField="Min Freight Rate" HeaderText="Min Freight Rate" ReadOnly="True"
                            SortExpression="Min Freight Rate" />
                        <asp:BoundField DataField="Freight Rate" HeaderText="Freight Rate" ReadOnly="True"
                            SortExpression="Freight Rate" />
                        <asp:BoundField DataField="Security Rate" HeaderText="Security Rate" ReadOnly="True"
                            SortExpression="Security Rate" />
                        <asp:BoundField DataField="Other Charges" HeaderText="Other Charges" ReadOnly="True"
                            SortExpression="Other Charges" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" ReadOnly="True"
                            SortExpression="Effective Date" />
                        <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" ReadOnly="True"
                            SortExpression="Expiry Date" />
                        <asp:BoundField DataField="Freight Forwarder" HeaderText="Freight Forwarder" SortExpression="Freight Forwarder" />
                        <asp:BoundField DataField="Origin Comment" HeaderText="Origin Comment" SortExpression="Origin Comment" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="Approval Date" />
                        <asp:BoundField DataField="Rate Request Date" HeaderText="Rate Request Date" ReadOnly="True"
                            SortExpression="Rate Request Date" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                        NextPageText="Next" PreviousPageText="Previous" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="FixedPosition"
                        Font-Bold="True" VerticalAlign="Top" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                <br />
                <br />
                <asp:Label ID="lblLaneNotFoundInRRAW" runat="server" Text="Above lane is not available in RRAW Portal, "
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="linkPostNewRateRequest" runat="server" OnClick="linkPostNewRateRequest_Click"
                    Visible="False" OnClientClick="locate('newraterequestlink');">Click here to Post a New Rate Request</asp:LinkButton>
                <%--<asp:GridView ID="gridTariff" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" ForeColor="#333"
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth, spborder" PageSize="20"
                    Width="3595px" EnableViewState="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="Origin Airport" HeaderText="Origin Airport" SortExpression="Origin Airport" />
                        <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region" />
                        <asp:BoundField DataField="Origin Code" HeaderText="Origin Code" SortExpression="Origin Code" />
                        <asp:BoundField DataField="Dest Airport" HeaderText="Dest Airport" SortExpression="Dest Airport" />
                        <asp:BoundField DataField="Dest City" HeaderText="Dest City" SortExpression="Dest City" />
                        <asp:BoundField DataField="Dest State" HeaderText="Dest State" SortExpression="Dest State" />
                        <asp:BoundField DataField="Dest Country" HeaderText="Dest Country" SortExpression="Dest Country" />
                        <asp:BoundField DataField="Dest Region" HeaderText="Dest Region" SortExpression="Dest Region" />
                        <asp:BoundField DataField="Dest Zipcode" HeaderText="Dest Zipcode" SortExpression="Dest Zipcode" />
                        <asp:BoundField DataField="CEVA Transit Mode" HeaderText="CEVA Transit Mode" SortExpression="CEVA Transit Mode" />
                        <asp:BoundField DataField="Ship Method" HeaderText="Ship Method" SortExpression="Ship Method" />
                        <asp:BoundField DataField="Forwarder Zipcode" HeaderText="Forwarder Zipcode" SortExpression="Forwarder Zipcode" />
                        <asp:BoundField DataField="Custom Clearance Mode" HeaderText="Custom Clearance Mode"
                            SortExpression="Custom Clearance Mode" />
                        <asp:BoundField DataField="Forwarder Service Level" HeaderText="Forwarder Service Level"
                            SortExpression="Forwarder Service Level" />
                        <asp:BoundField DataField="Min Freight Rate" HeaderText="Min Freight Rate" ReadOnly="True"
                            SortExpression="Min Freight Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Freight Rate" HeaderText="Freight Rate" ReadOnly="True"
                            SortExpression="Freight Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Security Rate" HeaderText="Security Rate" ReadOnly="True"
                            SortExpression="Security Rate">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Other Charges" HeaderText="Other Charges" ReadOnly="True"
                            SortExpression="Other Charges" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" ReadOnly="True"
                            SortExpression="Effective Date" />
                        <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" ReadOnly="True"
                            SortExpression="Expiry Date" />
                        <asp:BoundField DataField="Freight Forwarder" HeaderText="Freight Forwarder" SortExpression="Freight Forwarder" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="Approval Date">
                            <HeaderStyle Width="145px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" SortExpression="Approved By" />
                        <asp:BoundField DataField="Approval Notes" HeaderText="Approval Notes" SortExpression="Approval Notes" />
                        <asp:BoundField DataField="Additional Notes" HeaderText="Additional Notes" SortExpression="Additional Notes" />
                        <asp:BoundField DataField="Rate Request Date" HeaderText="Rate Request Date" ReadOnly="True"
                            SortExpression="Rate Request Date" />
                        <asp:BoundField DataField="Entry Date" HeaderText="Entry Date" ReadOnly="True" SortExpression="Entry Date" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                        NextPageText="Next" PreviousPageText="Previous" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="FixedPosition"
                        Font-Bold="True" VerticalAlign="Top" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                </asp:GridView>--%>
                <asp:GridView ID="gridTariff" runat="server" AllowPaging="True" AllowSorting="False"
                    AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" ForeColor="#333"
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth, spborder" PageSize="20"
                    Width="3595px" EnableViewState="true" ShowHeader="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Customer Name" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="Origin Airport Code" HeaderText="Origin Airport" SortExpression="Origin Airport Code" />
                        <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region" />
                        <asp:BoundField DataField="Origin Code" HeaderText="Origin Code" SortExpression="Origin Code" />
                        <asp:BoundField DataField="Destination Airpot" HeaderText="Dest Airport" SortExpression="Destination Airpot" />
                        <asp:BoundField DataField="Destination City" HeaderText="Dest City" SortExpression="Destination City" />
                        <asp:BoundField DataField="Destination  State          If Applicable" HeaderText="Dest State"
                            SortExpression="Destination  State          If Applicable" />
                        <asp:BoundField DataField="Destination Country Code" HeaderText="Dest Country" SortExpression="Destination Country Code" />
                        <asp:BoundField DataField="Destination Region" HeaderText="Dest Region" SortExpression="Destination Region" />
                        <asp:BoundField DataField="Destination Postal (ZIP) Code" HeaderText="Dest Zipcode"
                            SortExpression="Destination Postal (ZIP) Code" />
                        <asp:BoundField DataField="CEVA Mode Of Transit" HeaderText="CEVA Transit Mode" SortExpression="CEVA Mode Of Transit" />
                        <asp:BoundField DataField="WD Ship Method" HeaderText="Ship Method" SortExpression="WD Ship Method" />
                        <asp:BoundField DataField="Forwarder Delivery City or Postal Code" HeaderText="Forwarder Zipcode"
                            SortExpression="Forwarder Delivery City or Postal Code" />
                        <asp:BoundField DataField="With or Without Customs Clearance" HeaderText="Custom Clearance Mode"
                            SortExpression="With or Without Customs Clearance" />
                        <asp:BoundField DataField="Forwarder Service" HeaderText="Forwarder Service Level"
                            SortExpression="Forwarder Service" />
                        <asp:BoundField DataField="Valid Minimum Charge" HeaderText="Min Freight Rate" ReadOnly="True"
                            SortExpression="Valid Minimum Charge">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="USD Valid Int'l Per Kilo Rate Effective January 1 #### (Current Year)"
                            HeaderText="Freight Rate" ReadOnly="True" SortExpression="USD Valid Int'l Per Kilo Rate Effective January 1 #### (Current Year)">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="USD Security Charge Effective January 1 #### (Current Year)"
                            HeaderText="Security Rate" ReadOnly="True" SortExpression="USD Security Charge Effective January 1 #### (Current Year)">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Other Charges" HeaderText="Other Charges" ReadOnly="True"
                            SortExpression="Other Charges" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" ReadOnly="True"
                            SortExpression="Effective Date" />
                        <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" ReadOnly="True"
                            SortExpression="Expiry Date" />
                        <asp:BoundField DataField="Freight Forwarder" HeaderText="Freight Forwarder" SortExpression="Freight Forwarder" />
                        <asp:CheckBoxField DataField="ActiveBool" HeaderText="Active" SortExpression="ActiveBool" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="Approval Date">
                            <HeaderStyle Width="145px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" SortExpression="Approved By" />
                        <asp:BoundField DataField="Approval Notes" HeaderText="Approval Notes" SortExpression="Approval Notes" />
                        <asp:BoundField DataField="Additional Notes" HeaderText="Additional Notes" SortExpression="Additional Notes" />
                        <asp:BoundField DataField="Rate Request Date" HeaderText="Rate Request Date" ReadOnly="True"
                            SortExpression="Rate Request Date" />
                        <asp:BoundField DataField="Entry Date" HeaderText="Entry Date" ReadOnly="True" SortExpression="Entry Date" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                        NextPageText="Next" PreviousPageText="Previous" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" CssClass="FixedPosition"
                        Font-Bold="True" VerticalAlign="Top" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRemoveFilter" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="position: fixed; padding: 3px; background-color: #507CD1; color: #fff;
                font-weight: 700; width: 1133px; /*1149px*/">
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblPages"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label runat="server" ID="lblRecords"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gridApprovedAsAdhoc" EventName="DataBound" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/ApprovedAsAdhoc.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>
