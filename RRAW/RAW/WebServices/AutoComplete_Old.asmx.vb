Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class AutoComplete_Old
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function GetTariffAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM Tariff WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
            'Dim query As String = "SELECT DISTINCT " & contextKey & " FROM Tariff"

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetTariffAutoComplete_New(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM Tariff_14Jul WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
            'Dim query As String = "SELECT DISTINCT " & contextKey & " FROM Tariff"

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetEuropeanZoneAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM EuropeanZones WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
            'Dim query As String = "SELECT DISTINCT " & contextKey & " FROM Tariff"

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetApprovedAsAdhocAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM AirRateRequests WHERE IsAdhoc = 1 AND ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetOceanTariffAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Try
                If contextKey <> "Requestor" And contextKey <> "Approver" Then
                    Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM OceanRateRequests WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%' and approverID is not null and approverID!='' and ApprovalDate is not null and ApprovalDate!=''"
                    'Dim query As String = "SELECT DISTINCT " & contextKey & " FROM Tariff"

                    Using DB As New RRAW.DB.DBClass(query)
                        Dim dt As DataTable = DB.GetDataTable
                        Dim dataArray As New Generic.List(Of String)
                        For Each row As DataRow In dt.Rows
                            dataArray.Add(row(0).ToString)
                        Next
                        Return dataArray.ToArray
                    End Using
                Else
                    Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) from (select  Requestor.name as 'Requestor' , Approver.name as 'Approver' from OceanRateRequests ORS join Users Requestor on (ORS.RequestorID=Requestor.id) left join Users Approver on (ORS.ApproverID=Approver.id) where ORS.approverID is not null and ORS.approverID!='' and ApprovalDate is not null and ApprovalDate!='') USERS where  ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
                    Using DB As New RRAW.DB.DBClass(query)
                        Dim dt As DataTable = DB.GetDataTable
                        Dim dataArray As New Generic.List(Of String)
                        For Each row As DataRow In dt.Rows
                            dataArray.Add(row(0).ToString)
                        Next
                        Return dataArray.ToArray
                    End Using
                End If
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetOceanTariffAutoComplete_New(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Try
                If contextKey <> "RequestedBy" And contextKey <> "Approver" Then
                    Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM OceanRateRequests_14Jul WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
                    'Dim query As String = "SELECT DISTINCT " & contextKey & " FROM Tariff"

                    Using DB As New RRAW.DB.DBClass(query)
                        Dim dt As DataTable = DB.GetDataTable
                        Dim dataArray As New Generic.List(Of String)
                        For Each row As DataRow In dt.Rows
                            dataArray.Add(row(0).ToString)
                        Next
                        Return dataArray.ToArray
                    End Using
                Else
                    Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) from (select  Requestor.name as 'Requestor' , Approver.name as 'Approver' from OceanRateRequests_14Jul ORS join Users Requestor on (ORS.RequestorID=Requestor.id) left join Users Approver on (ORS.ApproverID=Approver.id) where ORS.approverID is not null and ORS.approverID!='' and ApprovalDate is not null and ApprovalDate!='') USERS where  ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"
                    Using DB As New RRAW.DB.DBClass(query)
                        Dim dt As DataTable = DB.GetDataTable
                        Dim dataArray As New Generic.List(Of String)
                        For Each row As DataRow In dt.Rows
                            dataArray.Add(row(0).ToString)
                        Next
                        Return dataArray.ToArray
                    End Using
                End If
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace