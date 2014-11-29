Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Mobile
        Inherits System.Web.Services.WebService

        Public Delegate Function delGetRequestCount(ByVal CurrentUserID As Integer, ByVal ClientId As Integer, ByVal PeriodFrom As Date, ByVal PeriodTo As Date) As Integer

        <WebMethod()> _
        Public Function VerifyLogin(ByVal UserName As String, ByVal Password As String) As Dictionary(Of String, String)
            Dim d As New Dictionary(Of String, String)

            Dim User As DataRow = DB.Users.Verify(UserName, Password)

            If User IsNot Nothing Then
                d.Add("CurrentUserID", User("ID").ToString)
                d.Add("AuthenticationToken", Guid.NewGuid.ToString)
                d.Add("CurrentUserName", User("Name").ToString)
                d.Add("CurrentAppVersion", CurrentReleaseInfo.Release & IIf(CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.LIVE, " (" & CurrentAppVersionExtension.ToString & ")", "").ToString)
                d.Add("CurrentUserType", User("UserType").ToString)

                'DB.Logs.PostNewLog(EnumUserAction.Login, Now)

                Return d
            Else
                Return d
            End If
        End Function

        <WebMethod()> _
        Public Function GetRequests(ByVal CurrentUserID As Integer, ByVal CurrentClientID As Integer) As List(Of Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))
            Dim res As New List(Of Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))))

            res.Add(GetDivision(CurrentUserID, CurrentClientID, "Open To Me", AddressOf DB.AirRateRequests.GetOpenToMeRequestsCount, "GetOpenToMeRequests"))
            res.Add(GetDivision(CurrentUserID, CurrentClientID, "My Approved", AddressOf DB.AirRateRequests.GetApprovedRequestsCount, "GetApprovedRequests"))
            res.Add(GetDivision(CurrentUserID, CurrentClientID, "My In Discussion", AddressOf DB.AirRateRequests.GetInDiscussionStationRequestsCount, "GetInDiscussionStationRequestsCount"))
            res.Add(GetDivision(CurrentUserID, CurrentClientID, "My Loop In Discussion", AddressOf DB.AirRateRequests.GetInDiscussionInLoopStationRequestsCount, "GetInDiscussionInLoopStationRequests"))

            Return res
        End Function

        Private Function GetDivision(ByVal CurrentUserID As Integer, ByVal CurrentClientID As Integer, ByVal Title As String, ByVal funcGetRequestCount As delGetRequestCount, ByVal RequestSPName As String) As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
            Dim dictDivision As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))

            'Collect and push Metadata (Title, Count, etc.)
            Dim dictTitleCount As New Dictionary(Of String, Dictionary(Of String, String))

            Dim lstMetadata As New Dictionary(Of String, String)
            lstMetadata.Add("Title", Title)
            lstMetadata.Add("Count", funcGetRequestCount.Invoke(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString)
            dictTitleCount.Add("TitleCount", lstMetadata)
            dictDivision.Add("Metadata", dictTitleCount)

            'Collect and push Requests
            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@UserID", CurrentUserID)
            param(1) = New SqlParameter("@FromDate", PeriodFrom)
            param(2) = New SqlParameter("@ToDate", PeriodTo)

            Try
                Using DB As New RRAW.DB.DBClass(RequestSPName, True, param)
                    dictDivision.Add("Requests", DataTableToComplexDictionaryWithColumn(DB.GetDataTable))
                End Using
            Catch
                Throw
            End Try

            Return dictDivision
        End Function

        Private ReadOnly Property PeriodFrom As Date
            Get
                Dim curMM As String = Now.ToString("MM")
                Dim curYYYY As String = Now.ToString("yyyy")

                If (Convert.ToInt32(curMM) >= 1 And Convert.ToInt32(curMM) < 4) Then
                    curMM = "01"
                ElseIf (Convert.ToInt32(curMM) >= 4 And Convert.ToInt32(curMM) < 7) Then
                    curMM = "04"
                ElseIf (Convert.ToInt32(curMM) >= 7 And Convert.ToInt32(curMM) < 10) Then
                    curMM = "07"
                ElseIf (Convert.ToInt32(curMM) >= 10) Then
                    curMM = "10"
                End If

                Return CDate(curMM & "/01/" & curYYYY)
            End Get
        End Property

        Private ReadOnly Property PeriodTo As Date
            Get
                Return CDate(Now.ToString("MM/dd/yyyy"))
            End Get
        End Property
    End Class
End Namespace