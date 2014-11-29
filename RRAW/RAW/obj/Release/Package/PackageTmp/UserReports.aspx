<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserReports.aspx.vb" Inherits="RRAW.UserReports"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Reports</title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
</head>
<body onload="onLoad()">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
            <Services>
                <asp:ServiceReference Path="~/WebServices/Reports.asmx" InlineScript="true" />
            </Services>
        </asp:ScriptManager>
        <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
            <br />
            <span style="text-decoration: underline; font-size: small; font-weight: 700">User Reports</span>
        </div>
        <br />
        <br />
        <br />
        Select Month:&nbsp;<select id="selectMonth" onchange="UpdateMonthName(this.options[this.selectedIndex].text)">
            <option>January</option>
            <option>February</option>
            <option>March</option>
            <option>April</option>
            <option>May</option>
            <option>June</option>
            <option>July</option>
            <option>August</option>
            <option>September</option>
            <option>October</option>
            <option>November</option>
            <option>December</option>
        </select>
        <br />
        <br />
        <div id="divReports">
            <div id="divSummaryOfRequestsByMonth">
                <a id="linkSummaryOfRequestsByMonth" href="#" onclick="return SummaryOfRequestsByMonth(document.getElementById('selectMonth').selectedIndex + 1, true)">
                    Generate & Download Report for <span style="font-weight: 700">Summary of Requests for
                        <span id="selectedSummaryOfRequestsByMonthMonthName"></span></span></a>
            </div>
            <br />
            <br />
            <div id="divNewLanesAddedInMonth">
                <a id="linkNewLanesAddedInMonth" href="#" onclick="return NewLanesAddedInMonth(document.getElementById('selectMonth').selectedIndex + 1, true)">
                    Generate & Download Report for <span style="font-weight: 700">New Lanes Added in <span
                        id="selectedNewLanesAddedInMonthMonthName"></span></span></a>
            </div>
            <br />
            <br />
            <div id="divMonthlyApprovalTimeline">
                <a id="linkMonthlyApprovalTimeline" href="#" onclick="return MonthlyApprovalTimeline(document.getElementById('selectMonth').selectedIndex + 1, true)">
                    Generate & Download Report for <span style="font-weight: 700">Monthly Approval Timeline
                        for <span id="selectedMonthlyApprovalTimelineMonthName"></span></span></a>
            </div>
            <br />
            <br />
            <div id="divApprovalTimelineForPendingRequests">
                <a id="linkApprovalTimelineForPendingRequests" href="#" onclick="return GetApprovalTimelineForPendingRequests(true)">
                    Generate & Download Report for <span style="font-weight: 700">Approval Timeline For
                        Pending Requests</span></a>
            </div>
            <!--<br />
            <br />
            <div id="divPendingApprovalTimeline">
                <a id="A1" href="#" onclick="return PendingApprovalTimeline(true)">Generate & Download
                    Report for <span style="font-weight: 700">Pending Approval Timeline</span></a>
            </div>-->
            <br />
            <br />
            <div id="divMonthlyRequestFrequency">
                <a id="linkMonthlyRequestFrequency" href="#" onclick="return MonthlyRequestFrequency(true)">
                    Generate & Download Report for <span style="font-weight: 700">Origin - Destination Monthly
                        Request Frequency</span></a>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/UserReports.min.js" type="text/javascript"></script>
</html>
