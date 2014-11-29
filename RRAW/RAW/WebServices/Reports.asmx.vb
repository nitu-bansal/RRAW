Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Reports
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function GetApprovalTimelineForPendingRequests(authenticationToken As String) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/ApprovalTimelineForPendingRequests_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("Approval Timeline For Pending Requests", DB.Reports.GetApprovalTimelineForPendingRequests()), False)

            Return fileName
        End Function

        <WebMethod()> _
        Public Function SummaryOfRequestsByMonth(authenticationToken As String, monthNumber As Integer) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/SummaryOfRequestsByMonth_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("Summary of Requests (" & MonthName(monthNumber) & ")", DB.Reports.GetSummaryOfRequestsByMonth(monthNumber)), False)

            Return fileName
        End Function

        <WebMethod()> _
        Public Function NewLanesAddedInMonth(ByVal authenticationToken As String, ByVal monthNumber As Integer, ByVal RequestType As String) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/NewLanesAddedInMonth" + RequestType+"_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("New Lanes Added in " + RequestType + " in " & MonthName(monthNumber), DB.Reports.GetNewLanesAddedInMonth(monthNumber, RequestType)), False)

            Return fileName
        End Function

        <WebMethod()> _
        Public Function MonthlyApprovalTimeline(authenticationToken As String, monthNumber As Integer) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/MonthlyApprovalTimeline_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("Monthly Approval Timelines", DB.Reports.GetMonthlyApprovalTimeline(monthNumber)), False)

            Return fileName
        End Function

        <WebMethod()> _
        Public Function PendingApprovalTimeline(ByVal authenticationToken As String) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/PendingApprovalTimeline_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("Pending Approval Timelines", DB.Reports.GetPendingApprovalTimeline()), False)

            Return fileName
        End Function

        <WebMethod()> _
        Public Function MonthlyRequestFrequency(ByVal authenticationToken As String, ByVal RequestType As String) As String
            If Not IsAuthenticated(authenticationToken) Then Return "AuthenticationFailed.aspx"

            Dim fileName As String = "Reports/MonthlyRequestFrequency" + RequestType+"_" & Now.Ticks & ".csv"

            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & fileName), DataTableToCSV("Details of Origin - Destination Monthly Request Frequency (" + RequestType + ")", DB.Reports.GetMonthlyRequestFrequency(RequestType)), False)

            Return fileName
        End Function
    End Class
End Namespace