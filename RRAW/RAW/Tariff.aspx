<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Tariff.aspx.vb" Inherits="RRAW.Tariff" %>

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
    <link href="CSS/Tariff.min.css" rel="stylesheet" type="text/css" />
</head>
<body  onload="try{locateonload(this.offsetTop)}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};javascript:document.getElementById('txtCustomer').focus();">
    <form id="form1" runat="server" defaultbutton="btnFilter">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span id="lblTitle" style="text-decoration: underline; font-size: small; font-weight: 700;">
            Old Tariff rate expired 14th Jul’12</span>
    </div>
    <br />
    <br />
    <br />
    <div id="divTariff" style="height: 490px; width: 3612px; overflow: auto;">
        <table style="text-align: center; position: absolute; overflow: scroll; width: 3595px;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);"
            cellpadding="5" cellspacing="0">
            <tr>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Customer
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Airport
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Code
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest Airport
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest City
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest State
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest Country
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest Zipcode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    CEVA Transit Mode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Ship Method
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Forwarder Zipcode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Custom Clearance Mode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Forwarder Service Level
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Min Freight Rate
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Freight Rate
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Security Rate
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Other Charges
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Effective Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Expiry Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Freight Forwarder
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Active
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Notes
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Approval Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Approved By
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Approval Notes
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Additional Notes
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Request Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Entry Date
                </td>
            </tr>
            <tr>
                <td valign="top" width="160px" bgcolor="#EFF3FB" align="right" style="padding-right: 5px;">
                    <asp:TextBox ID="txtCustomer" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtCustomer" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Customer">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginAirport" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtOriginAirport_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetTariffAutoComplete" TargetControlID="txtOriginAirport" UseContextKey="True"
                        CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginAirport">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtOriginRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginCode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtOriginCode" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginCode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestAirport" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestAirport" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestAirport">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="150px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestCity" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestCity" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestCity">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestState" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestState" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestState">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestCountry" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestCountry" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestCountry">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestRegion" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtDestZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtCEVATransitMode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtCEVATransitMode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="CEVATransitMode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="200px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtShipMethod" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender27" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtShipMethod" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ShipMethod">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtForwarderZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender26" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtForwarderZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ForwarderZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtCustomClearanceMode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender25" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtCustomClearanceMode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="CustomClearanceMode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtForwarderServiceLevel" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender24" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtForwarderServiceLevel" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ForwarderServiceLevel">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtMinFreightRate" runat="server" Width="100%" CssClass="style2"
                        AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender23" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtMinFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="MinFreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtFreightRate" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender22" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtSecurityRate" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtSecurityRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="SecurityRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOtherCharges" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender20" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtOtherCharges" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OtherCharges">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtEffectiveDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEffectiveDate" Animated="False" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtExpiryDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtExpiryDate" Animated="False" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtFreightForwarder" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender16" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtFreightForwarder" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightForwarder">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:DropDownList ID="cmbActive" runat="server" Width="100%">
                        <asp:ListItem Selected="True">Any</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtNotes" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Notes">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="145px" bgcolor="#EFF3FB" style="padding-right: 5px;">
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
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtApprovedBy" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender28" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtApprovedBy" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ApprovedBy">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtApprovalNotes" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender13" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtApprovalNotes" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ApprovalNotes">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtAdditionalNotes" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender12" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete"
                        TargetControlID="txtAdditionalNotes" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="AdditionalNotes">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRateRequestDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtRateRequestDate_CalendarExtender" runat="server" Animated="False"
                        Enabled="True" TargetControlID="txtRateRequestDate" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 10px;">
                    <asp:TextBox ID="txtEntryDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEntryDate" Animated="False" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td id="tdOperations" colspan="32" align="left" style="background-color: White; height: 30px;">
                    <div id="divOperations" style="position: fixed;">
                        <asp:Button ID="btnFilter" runat="server" Text="Apply Filter" OnClientClick="DisplayProgress();"
                            EnableViewState="false" />
                        <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" OnClientClick="ClearFields(); DisplayProgress();"
                            EnableViewState="false" />
                        <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" Visible="False"
                            EnableViewState="false" />
                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Visible="False"
                            EnableViewState="false" />
                        <asp:Button ID="btnConvertCurrency" runat="server" Text="Convert Currency"
                            EnableViewState="false" OnClientClick="DisplayProgress();" Visible="false"/>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
        <div id="divOperationAdjustments" style="height: 100px;">
        </div>
        <!--<div style="position: absolute; left: 50%; top: 50%; width: 32px;">-->
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
        </asp:UpdateProgress>
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
                <%-- <Columns>
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
                --%>
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
                        <asp:BoundField DataField="Destination Airport Code" HeaderText="Dest Airport" SortExpression="Destination Airport Code" />
                        <asp:BoundField DataField="Destination City" HeaderText="Dest City" SortExpression="Destination City" />
                        <asp:BoundField DataField="Destination  State (If Applicable)" HeaderText="Dest State"
                            SortExpression="Destination  State (If Applicable)" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                <asp:Label ID="lblAdhocLaneFound" runat="server" Font-Bold="true" Text="Above lane has been previously approved as <span style='color:red; font-weight:bold'>ADHOC</span> as described below."
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
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="Approval Date" />
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" SortExpression="Approved By" />
                        <asp:BoundField DataField="Approval Notes" HeaderText="Approval Notes" SortExpression="Approval Notes" />
                        <asp:BoundField DataField="Additional Notes" HeaderText="Additional Notes" SortExpression="Additional Notes" />
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
                <asp:Label ID="lblLaneNotFoundMessage" runat="server" Text="Above lane is not available in RRAW Portal, "
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="linkPostNewRateRequest" runat="server" OnClick="linkPostNewRateRequest_Click"
                    Visible="False" OnClientClick="locate('newraterequestlink');">Click here to Post a New Rate Request</asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRemoveFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnConvertCurrency" EventName="Click" />
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
                        <td style="text-align: left;">
                            <asp:Label runat="server" ID="lblPages" EnableViewState="false"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label runat="server" ID="lblRecords" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gridTariff" EventName="DataBound" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/Tariff.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>
