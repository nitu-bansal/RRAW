<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OceanTariff.aspx.vb" Inherits="RRAW.OceanTariff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Tariff.min.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{locate(this.offsetTop, 'rateslink','oceanrateslink')}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};javascript:document.getElementById('txtCustomer').focus();">
    <form id="form1" runat="server" defaultbutton="btnFilter">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span style="text-decoration: underline; font-size: small; font-weight: 700">Old Ocean Tariff expired 14th Jul’12</span>
    </div>
    <br />
    <br />
    <br />
    <div style="height: 490px; width: 3775px; overflow: auto">
        <table style="text-align: center;" cellpadding="5" cellspacing="0">
            <tr>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Customer
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Container No.
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Ocean HBL
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Ship Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Freight Term
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    WD Ship Method
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Shipper Name
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin City
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Port
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Zipcode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination City
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination Port
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination Zipcode
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Rates Valid For
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Rates Valid Till
                </td>
                <td style="width: 300px; text-align: center; background-color: #507CD1; color: #fff;
                    font-weight: 700;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;"
                                colspan="3">
                                LCL (per CBM)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                Total
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                BAF
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                PSS
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;"
                                colspan="3">
                                FCL (per 20GP)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                Total
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                BAF
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                PSS
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;"
                                colspan="3">
                                FCL (per 40GP)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                Total
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                BAF
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                PSS
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;"
                                colspan="3">
                                FCL (per 40HC)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                Total
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                BAF
                            </td>
                            <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                                PSS
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Last Comments
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Requested By
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Request Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Approval Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Approved By
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Effective Date
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Expiry Date
                </td>
            </tr>
            <tr>
                <td valign="top" width="60px" bgcolor="#EFF3FB" align="right" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRateRequestID" runat="server" Width="100%" AutoCompleteType="Disabled"
                        Visible="false"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender30" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtRateRequestID" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="RateRequestID">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="160px" bgcolor="#EFF3FB" align="right" style="padding-right: 5px;">
                    <asp:TextBox ID="txtCustomer" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtCustomer" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="consigneename">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtContainerNo" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtContainerNo_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetOceanTariffAutoComplete" TargetControlID="txtContainerNo" UseContextKey="True"
                        CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ContainerNo">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOceanHBL" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtOceanHBL" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OceanHBL">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtShipDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtShipDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtShipDate" Animated="False" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtFreightTerm" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtFreightTerm" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightTerm">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="160px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtWDShipMethod" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtWDShipMethod" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="WDShipMethod">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="160px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtShipperName" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtShipperName" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ShipperName">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="150px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginCity" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtOriginCity" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginCity">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginPort" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtOriginPort" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginPort">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtOriginZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtOriginRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="150px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestCity" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtDestCity" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestCity">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestPort" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender29" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtDestPort" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestPort">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestZipcode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender27" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtDestZipcode" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestZipcode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender26" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtDestRegion" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRatesValidFor" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender25" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtRatesValidFor" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="RatesValidFor">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRatesValidTill" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtRatesValidTill"
                        Animated="False" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px">
                    <table>
                        <tr>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtTotalLCL" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtBAFLCL" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="100px" bgcolor="#EFF3FB">
                                <asp:TextBox ID="txtPSSLCL" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px">
                    <table>
                        <tr>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtTotalFCL20GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtBAFFCL20GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="100px" bgcolor="#EFF3FB">
                                <asp:TextBox ID="txtPSSFCL20GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 5px">
                    <table>
                        <tr>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtTotalFCL40GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtBAFFCL40GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="95px" bgcolor="#EFF3FB">
                                <asp:TextBox ID="txtPSSFCL40GP" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="300px" bgcolor="#EFF3FB" style="padding-right: 0px; padding-left: 5px">
                    <table>
                        <tr>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtTotalFCL40HC" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="95px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                                <asp:TextBox ID="txtBAFFCL40HC" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td valign="top" width="100px" bgcolor="#EFF3FB">
                                <asp:TextBox ID="txtPSSFCL40HC" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="250px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtComment" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender17" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtComment" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Comment">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRequestor" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender18" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtRequestor" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Requestor">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRateRequestDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="txtRateRequestDate_CalendarExtender" runat="server" Animated="False"
                        Enabled="True" TargetControlID="txtRateRequestDate" Format="MM/dd/yyyy">
                    </asp:CalendarExtender>
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
                    <asp:TextBox ID="txtApprover" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender19" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete"
                        TargetControlID="txtApprover" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Approver">
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
            </tr>
            <tr style="display: none;">
                <td colspan="32">
                    <div style="height: 140px; width: 200px; overflow-y: auto; border: 1px solid gray;
                        text-align: left;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div id="divItems">
                                    <asp:CheckBoxList runat="server" ID="chkItems">
                                    </asp:CheckBoxList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="text-align: right; width: 200px;">
                        <table>
                            <tr>
                                <td style="width: 100%">
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" Text="OK" runat="server" Style="width: 75px; height: 25px;" />
                                </td>
                                <td>
                                    <input id="btnCancel" type="button" value="Cancel" style="width: 75px; height: 25px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="32" align="left">
                    <asp:Button ID="btnFilter" runat="server" Text="Apply Filter" OnClientClick="DisplayProgress();"
                        EnableViewState="false" />
                    <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" OnClientClick="ClearOceanFields(); DisplayProgress();"
                        EnableViewState="false" />
                    <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" Visible="true"
                        EnableViewState="false" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Visible="true"
                        EnableViewState="false" /><br />
                    <br />
                </td>
            </tr>
        </table>
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
                <asp:GridView ID="gridTariff" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" ForeColor="#333"
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth, spborder" PageSize="20"
                    Width="100%" EnableViewState="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" HeaderStyle-Width="60"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer"
                            HeaderStyle-Width="160" />
                        <asp:BoundField DataField="Container No." HeaderText="Container No." SortExpression="Container No."
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Ocean HBL" HeaderText="Ocean HBL" SortExpression="Ocean HBL"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Ship Date" HeaderText="Ship Date" SortExpression="Ship Date"
                            HeaderStyle-Width="70" />
                        <asp:BoundField DataField="Freight Term" HeaderText="Freight Term" SortExpression="Freight Term"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="WD Ship Method" HeaderText="WD Ship Method" SortExpression="WD Ship Method"
                            HeaderStyle-Width="160" />
                        <asp:BoundField DataField="Shipper Name" HeaderText="Shipper Name" SortExpression="Shipper Name"
                            HeaderStyle-Width="160" />
                        <asp:BoundField DataField="Origin City" HeaderText="Origin City" SortExpression="Origin City"
                            HeaderStyle-Width="150" />
                        <asp:BoundField DataField="Origin Port" HeaderText="Origin Port" SortExpression="Origin Port"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Origin Zipcode" HeaderText="Origin Zipcode" SortExpression="Origin Zipcode"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="Destination City" HeaderText="Destination City" SortExpression="Destination City"
                            HeaderStyle-Width="150" />
                        <asp:BoundField DataField="Destination Port" HeaderText="Destination Port" SortExpression="Destination Port"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Destination Zipcode" HeaderText="Destination Zipcode"
                            SortExpression="Destination Zipcode" HeaderStyle-Width="60" />
                        <asp:BoundField DataField="Destination Region" HeaderText="Destination Region" SortExpression="Destination Region"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="Rates Valid For" HeaderText="Rates Valid For" ReadOnly="True"
                            SortExpression="Rates Valid For">
                            <HeaderStyle Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rates Valid Till" HeaderText="Rates Valid Till" ReadOnly="True"
                            SortExpression="Rates Valid Till">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalLCL" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BAFLCL" HeaderText="BAF" ReadOnly="True" SortExpression="BAF">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PSSLCL" HeaderText="PSS" ReadOnly="True" SortExpression="PSS"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="TotalFCL20GP" HeaderText="Total" ReadOnly="True" SortExpression="TotalFCL20GP"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="BAFFCL20GP" HeaderText="BAF" ReadOnly="True" SortExpression="BAFFCL20GP"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="PSSFCL20GP" HeaderText="PSS" SortExpression="PSSFCL20GP"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="TotalFCL40GP" HeaderText="Total" SortExpression="TotalFCL40GP"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="BAFFCL40GP" HeaderText="BAF" ReadOnly="True" SortExpression="BAFFCL40GP">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PSSFCL40GP" HeaderText="PSS" SortExpression="PSSFCL40GP"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="TotalFCL40HC" HeaderText="Total" SortExpression="TotalFCL40HC"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="BAFFCL40HC" HeaderText="BAF" SortExpression="BAFFCL40HC"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="PSSFCL40HC" HeaderText="PSS" ReadOnly="True" SortExpression="PSSFCL40HC"
                            HeaderStyle-Width="100" />
                        <asp:BoundField DataField="Last Comments" HeaderText="Last Comments" ReadOnly="True"
                            HeaderStyle-Width="250" SortExpression="Last Comments" />
                        <asp:BoundField DataField="Requested By" HeaderText="Requested By" ReadOnly="True"
                            HeaderStyle-Width="100" SortExpression="Requested By" />
                        <asp:BoundField DataField="Request Date" HeaderText="Request Date" ReadOnly="True"
                            HeaderStyle-Width="70" SortExpression="Request Date" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" ReadOnly="True"
                            HeaderStyle-Width="145" SortExpression="Approval Date" />
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" ReadOnly="True"
                            HeaderStyle-Width="100" SortExpression="Approved By" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" ReadOnly="True"
                            HeaderStyle-Width="70" SortExpression="Effective Date" />
                        <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" ReadOnly="True"
                            HeaderStyle-Width="70" SortExpression="Expiry Date" />
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
                <asp:Label ID="lblAdhocLaneFound" runat="server" Font-Bold="true" Text="Above lane has been previously approved as <span style='color:red; font-weight:bold'>ADHOC</span> as described below."
                    Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblLaneNotFoundMessage" runat="server" Text="Above lane is not available in RRAW Portal, "
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="linkPostNewOceanRateRequest" runat="server" Visible="False" OnClientClick="locate(this.offsetTop, 'raterequestslink', 'oceanraterequestlink');">Click here to Post a New Rate Request</asp:LinkButton>
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
                font-weight: 700; width: 1149px;">
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
