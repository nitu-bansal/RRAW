<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="RRAW.Dashboard" %>

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
    <link href="CSS/Dashboard.min.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{locate(this.offsetTop,'dashboardlink')}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <!--function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f()"
    ;function f2(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' 23:59:59')}; document.getElementById('hidPeriodTo').value=f2()-->
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
            EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
            LoadScriptsBeforeUI="False">
        </asp:ToolkitScriptManager>
        <asp:HiddenField ID="hidPeriodTo" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidRequestDate" runat="server" />
        <asp:HiddenField ID="hidRequestor" runat="server" />
        <asp:HiddenField ID="hidOriginAirport" runat="server" />
        <asp:HiddenField ID="hidOriginZipcode" runat="server" />
        <asp:HiddenField ID="hidDestAirport" runat="server" />
        <asp:HiddenField ID="hidDestZipcode" runat="server" />
        <asp:HiddenField ID="hidCurrentApprover" runat="server" />
        <asp:HiddenField ID="hidOriginComment" runat="server" />
        <div class="page_title">
            Dashboard</div>
        <asp:Label ID="hidCallback" runat="server" EnableViewState="False" onchange="document.getElementById('UpdateProgress1').style.display='none'"
            Visible="False"></asp:Label>
        <table style="width: 100%">
            <tr>
                <td align="left">
                    Display data as from<asp:TextBox ID="txtPeriodFrom" runat="server" BorderWidth="0"
                        BorderStyle="None" Font-Bold="True" CssClass="CenterAlign, textTopAligned" Width="82px"
                        ForeColor="#507CD1" AutoPostBack="True" Font-Underline="True" ToolTip="Click here to change FROM DATE">10/26/2010</asp:TextBox><asp:CalendarExtender
                            ID="txtPeriodFrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPeriodFrom"
                            Format="MM/dd/yyyy">
                        </asp:CalendarExtender>
                    to<asp:TextBox ID="txtPeriodTo" runat="server" BorderWidth="0" BorderStyle="None"
                        onchange="var myDate = new Date(document.getElementById('txtPeriodTo').value); myDate.setDate(myDate.getDate() + 1); document.getElementById('hidPeriodTo').value = myDate.format('MM/dd/yyyy')"
                        Font-Bold="True" CssClass="CenterAlign, textTopAligned" Width="82px" ForeColor="#507CD1"
                        AutoPostBack="True" Font-Underline="True" ToolTip="Click here to change TO DATE">10/26/2010</asp:TextBox><asp:CalendarExtender
                            ID="txtPeriodTo_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPeriodTo"
                            Format="MM/dd/yyyy">
                        </asp:CalendarExtender>
                    <hr />
                </td>
            </tr>
        </table>
        <!--<div runat="server" id="mainDiv" style="overflow-y: auto; height: 466px">-->
        <asp:Panel ID="mainDiv2" Style="overflow-y: auto" runat="server">
            <table runat="server" id="tb1">
                <tr>
                    <td style="width: 49%">
                        <table cellspacing="1" class="tb" width="100%">
                            <tr>
                                <td style="border-bottom: none; font-weight: 700" align="center">
                                    Status of my requests
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellspacing="0" cellpadding="4">
                                                <tr style="background-color: #507CD1;">
                                                    <td align="center" style="width: 75px; min-width: 10px; max-width: 100px; border-style: none">
                                                        <asp:LinkButton ID="linkOpenRequests" runat="server" Font-Bold="true" Style="color: #fff"
                                                            ToolTip="Click here to see requests that are open for others" EnableViewState="False">Open For_Others</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkInDiscussionCEVARequests" Style="color: #fff" runat="server"
                                                            Font-Bold="true" ToolTip="Click here to see In Discussion (CEVA Station) requests"
                                                            EnableViewState="False">In_Discussion (CEVA_Station)</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkInDiscussionClientRequests" Style="color: #fff" runat="server"
                                                            Font-Bold="true" ToolTip="Click here to see In Discussion (WD) requests" EnableViewState="False">In_Discussion (WD)</asp:LinkButton>
                                                    </td>
                                                    <td align="center" style="min-width: 10px; border-style: none">
                                                        <asp:LinkButton ID="linkApprovedRequests" runat="server" Font-Bold="true" Style="color: #fff"
                                                            ToolTip="Click here to see Approved requests" EnableViewState="False">Approved</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkRejectedRequests" runat="server" Style="color: #fff" Font-Bold="true"
                                                            ToolTip="Click here to see Rejected requests" EnableViewState="False">Rejected</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr style="background-color: #EFF3FB;">
                                                    <td align="right" style="min-width: 5px; max-width: 50px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblOpenToAllRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblInDiscussionCEVARequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblInDiscussionClientRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblApprovedRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px;">
                                                        <asp:Label ID="lblRejectedRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtPeriodFrom" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtPeriodTo" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <div style="height: 19px;">
                        </div>
                        <table cellspacing="1" class="tb" width="100%">
                            <tr>
                                <td style="border-bottom: none; font-weight: 700" align="center">
                                    Status of requests in my loop
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: none;">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellspacing="0" cellpadding="4">
                                                <tr style="background-color: #507CD1;">
                                                    <td style="width: 75px; min-width: 10px; max-width: 100px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkOpenToMeRequests" runat="server" Font-Bold="true" Style="color: #fff"
                                                            ToolTip="Click here to see requests that are open to me" EnableViewState="False">Open To_Me</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkInDiscussionInLoopCEVARequests" Style="color: #fff" runat="server"
                                                            Font-Bold="true" ToolTip="Click here to see In Discussion (CEVA Station) requests"
                                                            EnableViewState="False">In_Discussion (CEVA_Station)</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkInDiscussionInLoopClientRequests" Style="color: #fff" runat="server"
                                                            Font-Bold="true" ToolTip="Click here to see In Discussion (WD) requests" EnableViewState="False">In_Discussion (WD)</asp:LinkButton>
                                                    </td>
                                                    <td align="center" style="min-width: 10px; border-style: none">
                                                        <asp:LinkButton ID="linkApprovedInLoopRequests" runat="server" Font-Bold="true" Style="color: #fff"
                                                            ToolTip="Click here to see Approved requests" EnableViewState="False">Approved</asp:LinkButton>
                                                    </td>
                                                    <td style="min-width: 10px; border-style: none" align="center">
                                                        <asp:LinkButton ID="linkRejectedInLoopRequests" runat="server" Style="color: #fff"
                                                            Font-Bold="true" ToolTip="Click here to see Rejected requests" EnableViewState="False">Rejected</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr style="background-color: #EFF3FB;">
                                                    <td align="right" style="min-width: 5px; max-width: 50px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblOpenToMeRequests" runat="server" Text="0" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblInDiscussionInLoopCEVARequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblInDiscussionInLoopClientRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px; border-right-style: none">
                                                        <asp:Label ID="lblApprovedInLoopRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td align="right" style="min-width: 5px; padding-right: 3px;">
                                                        <asp:Label ID="lblRejectedInLoopRequests" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtPeriodFrom" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtPeriodTo" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="border-style: none">
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table cellspacing="1" class="tb" width="100%">
                                    <tr>
                                        <td style="border-bottom: none; font-weight: 700;" align="center">
                                            Region wise status &amp;
                                            <asp:Label ID="lblSummaryStatus" runat="server" Text="Counts"></asp:Label>
                                            of requests
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-style: none;">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                DisplayAfter="1">
                                                <ProgressTemplate>
                                                    <div class="loading_notifier">
                                                        <table class="loading_notifier_box">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="middle" style="font-size: 10px; font-weight: 700;">
                                                                        <span id="lblProgressText">Loading Data...</span>
                                                                        <div class="processing_image">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gridRateRequestGroupings" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                                                ForeColor="#333" GridLines="None" Width="100%" ShowFooter="True" PageSize="4">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Origin Region" HeaderText="Origin Region" SortExpression="Origin Region" />
                                                    <asp:BoundField DataField="Origin Airport" HeaderText="Origin Airport" SortExpression="Origin Airport" />
                                                    <asp:BoundField DataField="Open" HeaderText="Open" ReadOnly="True" SortExpression="Open" />
                                                    <asp:BoundField DataField="In_Discussion (CEVA_Station)" HeaderText="In_Discussion (CEVA_Station)"
                                                        ReadOnly="True" SortExpression="In_Discussion (CEVA_Station)" />
                                                    <asp:BoundField DataField="In_Discussion (WD)" HeaderText="In_Discussion (WD)" ReadOnly="True"
                                                        SortExpression="In_Discussion (WD)" />
                                                    <asp:BoundField DataField="Approved" HeaderText="Approved" ReadOnly="True" SortExpression="Approved" />
                                                    <asp:BoundField DataField="Approved As Adhoc" HeaderText="Approved As Adhoc" ReadOnly="True"
                                                        SortExpression="Approved As Adhoc" />
                                                    <asp:BoundField DataField="Rejected" HeaderText="Rejected" ReadOnly="True" SortExpression="Rejected" />
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <EmptyDataTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="border: none">
                                                                No request available for specified filter.
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#507CD1" CssClass="no-border" Font-Bold="True" ForeColor="White"
                                                    Height="15px" VerticalAlign="Bottom" />
                                                <HeaderStyle BackColor="#507CD1" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                    CssClass="with-border" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="Next" PreviousPageText="Previous" />
                                                <PagerStyle BackColor="#2461BF" CssClass="no-border" Font-Bold="False" Font-Size="Larger"
                                                    ForeColor="White" HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure"
                                                EnableViewState="false">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtPeriodFrom" Name="FromDate" PropertyName="Text"
                                                        Type="DateTime" />
                                                    <asp:ControlParameter ControlID="hidPeriodTo" Name="ToDate" PropertyName="Value"
                                                        Type="DateTime" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtPeriodFrom" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtPeriodTo" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            <table cellspacing="1" runat="server" id="tb2" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="False">
                            <ContentTemplate>
                                <table cellspacing="1" class="tb" width="100%">
                                    <tr>
                                        <td style="width: 50%; font-weight: 700; border-bottom-style: none;" id="link1" align="center">
                                            <asp:Label ID="lblStatusTitle" runat="server" Text="Recent activity on requests that are Open To Me"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border: none; width: 100%;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="1">
                                                <ProgressTemplate>
                                                    <div class="loading_notifier">
                                                        <table class="loading_notifier_box">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="font-size: 10px; font-weight: 700;" valign="middle">
                                                                        <span id="lblProgressText">Loading Data...</span>
                                                                        <div class="processing_image">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <div>
                                                <asp:GridView ID="gridRateRequests" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="RateRequestID" DataSourceID="SqlDataSource3"
                                                    EnableModelValidation="True" EnableViewState="False" ForeColor="#000333" GridLines="None"
                                                    PageSize="7" ShowFooter="True" Width="100%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RateRequestID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="RateRequestID" />
                                                        <asp:BoundField DataField="Request Date" HeaderText="Request Date" ReadOnly="True"
                                                            SortExpression="Request Date"></asp:BoundField>
                                                        <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor" />
                                                        <asp:BoundField DataField="Origin Port" HeaderText="Origin Port" SortExpression="Origin Port">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Origin Zipcode" HeaderText="Origin Zipcode" SortExpression="Origin Zipcode">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Destination Port" HeaderText="Destination Port" SortExpression="Destination Port">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Destination Zipcode" HeaderText="Destination Zipcode"
                                                            SortExpression="Destination Zipcode"></asp:BoundField>
                                                        <asp:BoundField DataField="Current Approver" HeaderText="Current Approver" SortExpression="Current Approver" />
                                                        <asp:BoundField DataField="Origin Comment" HeaderText="Origin Comment" SortExpression="Origin Comment">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HasAttachment" HeaderText="Has Attachment" />
                                                        <asp:BoundField DataField="RateRequestType" HeaderText="RateRequestType" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <EmptyDataTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" style="border: none">
                                                                    No request available for specified filter.<br />
                                                                    <br />
                                                                    <a id="linkClearFilterValues" href="javascript:ClearFilterValues(); __doPostBack('linkClearFilterValues')">
                                                                        Clear Filter</hyperlink>
                                                                        <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <FooterStyle BackColor="#507CD1" CssClass="no-border" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                        CssClass="with-border" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                                                        NextPageText="Next" PreviousPageText="Previous" />
                                                    <PagerStyle BackColor="#2461BF" CssClass="no-border" Font-Size="Larger" ForeColor="White"
                                                        HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                                </asp:GridView>
                                            </div>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" EnableViewState="False" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="UserID" SessionField="UserID" Type="Int32" />
                                                    <asp:ControlParameter ControlID="txtPeriodFrom" Name="FromDate" PropertyName="Text"
                                                        Type="DateTime" />
                                                    <asp:ControlParameter ControlID="hidPeriodTo" Name="ToDate" PropertyName="Value"
                                                        Type="DateTime" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="linkApprovedRequests" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="linkInDiscussionCEVARequests" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="linkOpenRequests" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="linkRejectedRequests" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtPeriodFrom" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtPeriodTo" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="linkInDiscussionClientRequests" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="linkOpenToMeRequests" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!--</div>-->
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
<script src="Scripts/Dashboard.min.js" type="text/javascript"></script>
</html>
