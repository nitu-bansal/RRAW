<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Tariff_old.aspx.vb" Inherits="RRAW.Tariff_old" %>

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
<body  onload="try{locateonload(this.offsetTop)}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};javascript:document.getElementById('txtFreightCompany').focus();">
    <form id="form1" runat="server" defaultbutton="btnFilter">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span id="lblTitle" style="text-decoration: underline; font-size: small; font-weight: 700;">
            Air-New Tariff effectives 15th Jul’12</span>
    </div>
    <br />
    <br />
    <br />
    <div id="divTariff" style="height: 515px; width: 1139px; overflow: auto;">
        <table style="text-align: center; position: relative; width: 2045;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);"
            cellpadding="5" cellspacing="0">
            <tr>
                
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
                    Destination
                </td>
                 <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination Airport Code
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                    Destination Station
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
                    Min Freight Rate
                </td>
                <td style="text-align: center; background-color: #507CD1; color: #fff; font-weight: 700;">
                      Freight Rate Per Kg 
                </td>
               
            </tr>
            <tr>
               
             
             <td valign="top" width="45px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:45px;">
                    <asp:TextBox ID="txtReportItemIndex" runat="server" Width="100%" AutoCompleteType="Disabled" ></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtReportItemIndex_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtReportItemIndex" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="ReportItemIndex">
                    </asp:AutoCompleteExtender>
                </td>
                
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:70px;">
                    <asp:TextBox ID="txtOriginRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtOriginRegion" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginRegion">
                    </asp:AutoCompleteExtender>
                </td>
                   <td valign="top" width="45px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:45px;">
                    <asp:TextBox ID="txtOriginAirportCode" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtOriginAirportCode_AutoCompleteExtender1" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                        ServiceMethod="GetTariffAutoComplete_New" TargetControlID="txtOriginAirportCode" UseContextKey="True"
                        CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="OriginAirportCode">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:70px;">
                    <asp:TextBox ID="txtDestRegion" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtDestRegion" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="DestinationRegion">
                    </asp:AutoCompleteExtender>
                </td>
                 <td valign="top" width="45px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:80px;">
                    <asp:TextBox ID="txtDestination" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtDestination" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="Destination">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="45px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:85px;">
                    <asp:TextBox ID="txtDestAirport" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtDestAirport" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestinationAirportCode">
                    </asp:AutoCompleteExtender>
                </td>
                 <td valign="top" width="45px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:80px;">
                    <asp:TextBox ID="txtDestinationStation" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtDestinationStation" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="DestinationStation">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="55px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:95px;">
                    <asp:TextBox ID="txtServiceLevel" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtServiceLevel_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtServiceLevel" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ServiceLevel">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="55px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:55px;">
                    <asp:TextBox ID="txtServiceType" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtServiceType_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtServiceType" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                        MinimumPrefixLength="1" ContextKey="ServiceType">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="70px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:60px;">
                    <asp:TextBox ID="txtCurrency" runat="server" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCurrency_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtCurrency" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="Currency">
                    </asp:AutoCompleteExtender>
                </td>
                
                <td valign="top" width="80px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:75px;">
                    <asp:TextBox ID="txtMinFreightRate" runat="server" Width="100%" CssClass="style2"
                        AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender23" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtMinFreightRate" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="MinFreightRate">
                    </asp:AutoCompleteExtender>
                </td>
                <td valign="top" width="80px" bgcolor="#EFF3FB" style="padding-right: 5px;min-width:75px;">
                    <asp:TextBox ID="txtFreightRatePerKG" runat="server" Width="100%" CssClass="style2" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtFreightRatePerKG_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetTariffAutoComplete_New"
                        TargetControlID="txtFreightRatePerKG" UseContextKey="True" CompletionInterval="1"
                        FirstRowSelected="True" MinimumPrefixLength="1" ContextKey="FreightRatePerKG">
                    </asp:AutoCompleteExtender>
                </td>
                
                
            </tr>
            <tr>
                <td id="tdOperations" colspan="32" align="left" style="background-color: White; height: 30px;">
                    <div id="divOperations" >
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
        <%--<div id="divOperationAdjustments" style="height: 100px;">
        </div>--%>
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
                    GridLines="None" CaptionAlign="Right" CssClass="GridMaxWidth, spborder" PageSize="12"
                    Width="2045px" EnableViewState="true" ShowHeader="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Index" HeaderText="Index" SortExpression="Index" Visible="false" />
                        
                        <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                        <asp:BoundField DataField="Ship From Region" HeaderText="Ship From Region" SortExpression="Ship From Region" />
                        <asp:BoundField DataField="Origin" HeaderText="Origin" SortExpression="Origin" />
                        <asp:BoundField DataField="Ship To Region" HeaderText="Ship To Region" SortExpression="Ship To Region" />
                        <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                        <asp:BoundField DataField="Dest" HeaderText="Destination Airport Code" SortExpression="Dest" />
                        <asp:BoundField DataField="DestinationStation" HeaderText="Destination Station" SortExpression="DestinationStation" />
                        <asp:BoundField DataField="Service WD" HeaderText="Service WD" SortExpression="Service WD" />
                        <asp:BoundField DataField="Service Level" HeaderText="Service Level" SortExpression="Service Level" />
                        <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                        <asp:BoundField DataField="Min Freight Rate" HeaderText="Min Freight Rate" SortExpression="Min Freight Rate" />
                        <asp:BoundField DataField="Freight Rate Per Kg" HeaderText="Freight Rate Per Kg" SortExpression="Freight Rate Per Kg" />
                        <asp:BoundField DataField="Airlines" HeaderText="Airlines" SortExpression="Airlines" />
                        <asp:BoundField DataField="DaysOfDeparture" HeaderText="Days Of Departure" SortExpression="DaysOfDeparture" />
                        <asp:BoundField DataField="Effective Date" HeaderText="Effective Date" SortExpression="Effective Date" />
                        <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" SortExpression="Expiry Date" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" SortExpression="Approval Date" />
                        <asp:BoundField DataField="Approved By" HeaderText="Approved By" ReadOnly="True"
                            SortExpression="Approved By">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Request Date" HeaderText="Request Date" ReadOnly="True" SortExpression="Request Date">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Entry Date" HeaderText="Entry Date" ReadOnly="True" SortExpression="Entry Date">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActiveBool" HeaderText="ActiveBool" ReadOnly="True" SortExpression="ActiveBool" Visible="false"/>
                        <asp:BoundField DataField="Active" HeaderText="Active" ReadOnly="True" SortExpression="Active" />
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
<script src="Scripts/Tariff_Old.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>
