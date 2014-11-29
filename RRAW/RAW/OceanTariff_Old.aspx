<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OceanTariff_Old.aspx.vb" Inherits="RRAW.OceanTariff_Old" %>

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
<body onload="try{locate(this.offsetTop, 'rateslink','oceantariff_newlink')}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};javascript:document.getElementById('txtFreightCompany').focus();">
    <form id="form1" runat="server" defaultbutton="btnFilter">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span style="text-decoration: underline; font-size: small; font-weight: 700">Ocean Freight Rates 14 July</span>
    </div>
    <br />
    <br />
    <br />
    <div style="height: 515px; width: 1139px; overflow: auto">
        <table style="text-align: center;" cellpadding="5" cellspacing="0" width="2300px">
            <tr>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;
                    display: none;">
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Freight Company
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Item
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Ship From Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Origin
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Ship To Region
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Dest
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Service WD
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Service Level
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Currency
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    LCL Minimum
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Cost/Unit (FCL or CBM for LCL)
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Lines
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Departures per week
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
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Entry Date
                </td>
            </tr>
            <tr>
                <%--<td valign="top" width="60px" bgcolor="#EFF3FB" align="right" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRateRequestID" runat="server" Width="100%" AutoCompleteType="Disabled"
                        Visible="false"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender30" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtRateRequestID" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="RateRequestID">
                    </asp:AutoCompleteExtender>
                </td>--%>
                <td valign="top" width="80px" bgcolor="#EFF3FB" align="right" style="padding-right: 5px;">
                    <asp:TextBox ID="txtFreightCompany" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtFreightCompany_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetOceanTariffAutoComplete_New" TargetControlID="txtFreightCompany"
                        UseContextKey="True" CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1"
                        ContextKey="FreightCompany">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtReportItemIndex" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtReportItemIndex_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetOceanTariffAutoComplete_New" TargetControlID="txtReportItemIndex"
                        UseContextKey="True" CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1"
                        ContextKey="ReportItemIndex">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtOriginRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtOriginPort" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtOriginPort" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="OriginPort">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="100px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestinationRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender26" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtDestinationRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestinationRegion">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDestPort" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender29" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtDestPort" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestPort">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtServiceLevel" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender27" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtServiceLevel" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ServiceLevel">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtServiceType" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender25" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtServiceType" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ServiceType">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtCurrency" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtCurrency" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Currency">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtMinFreight" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtMinFreight" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="MinFreight">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="150px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtFreightRate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtLines" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtLines" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Lines">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtDeparturesPerWeek" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtDeparturesPerWeek" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DeparturesPerWeek">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtComments" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender17" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtComments" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="Comments">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtRequestedBy" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
                        TargetControlID="txtRequestedBy" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="RequestedBy">
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
                <td valign="top" width="60px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtApprover" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender19" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetOceanTariffAutoComplete_New"
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
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;">
                    <asp:TextBox ID="txtEntryDate" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtEntryDate"
                        Animated="False" Format="MM/dd/yyyy">
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
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth, spborder" PageSize="12"
                    Width="2300px" EnableViewState="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" HeaderStyle-Width="60"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <asp:BoundField DataField="FreightCompany" HeaderText="Freight Company" SortExpression="FreightCompany"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="ReportItemIndex" HeaderText="Item" SortExpression="ReportItemIndex"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="OriginRegion" HeaderText="Ship From Region" SortExpression="OriginRegion"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="OriginPort" HeaderText="Origin" SortExpression="OriginPort"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="DestinationRegion" HeaderText="Ship To Region" SortExpression="DestinationRegion"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="DestPort" HeaderText="Destination" SortExpression="DestPort"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="ServiceLevel" HeaderText="Service WD" SortExpression="ServiceLevel"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="ServiceType" HeaderText="Service Level" SortExpression="ServiceType"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="MinFreight" HeaderText="LCL Minimum" SortExpression="MinFreight"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="FreightRate" HeaderText="Cost/Unit (FCL or CBM for LCL)"
                            SortExpression="FreightRate" HeaderStyle-Width="70" />
                        <asp:BoundField DataField="Lines" HeaderText="Lines" SortExpression="Lines" HeaderStyle-Width="35" />
                        <asp:BoundField DataField="DeparturesPerWeek" HeaderText="Departures per week" SortExpression="DeparturesPerWeek"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="Comments" HeaderText="Last Comment" SortExpression="Comments"
                            HeaderStyle-Width="35" />
                        <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" SortExpression="RequestedBy"
                            HeaderStyle-Width="60" />
                        <asp:BoundField DataField="RateRequestDate" HeaderText="Request Date" SortExpression="RateRequestDate"
                            HeaderStyle-Width="70" />
                        <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" ReadOnly="True"
                            SortExpression="ApprovalDate">
                            <HeaderStyle Width="70" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" ReadOnly="True" SortExpression="ApprovedBy">
                            <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" ReadOnly="True"
                            SortExpression="EffectiveDate">
                            <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" ReadOnly="True" SortExpression="ExpiryDate">
                            <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" ReadOnly="True" SortExpression="EntryDate">
                            <HeaderStyle Width="70px" />
                        </asp:BoundField>
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
                <asp:Label ID="lblLaneNotFoundMessage" runat="server" Text="Above lane is not available in RRAW Portal, "
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="linkPostNewOceanRateRequest" runat="server" Visible="False" OnClientClick="locate(this.offsetTop, 'raterequestslink', 'oceanraterequestlink');">Click here to Post a New Rate Request</asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRemoveFilter" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
     
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="position: fixed; padding: 3px; background-color: #507CD1; color: #fff;
                font-weight: 700; width: 1149px;">
                <table width="1130px">
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
<script src="Scripts/Tariff_New.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>

