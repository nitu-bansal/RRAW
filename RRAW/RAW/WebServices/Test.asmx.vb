Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
Namespace WebServices
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Test
        Inherits System.Web.Services.WebService

        '<ScriptMethod(UseHttpGet:=True)>
        <WebMethod()> _
        Public Function GetTabularDataColumns(DBFunctionName As String) As List(Of String)
            Dim res As New List(Of String)

            Try
                Using DB As New RRAW.DB.DBClass(DBFunctionName, True)
                    res = DataTableColumns(DB.GetDataTable)
                End Using
            Catch
                Throw
            End Try

            Return res
        End Function

        <WebMethod()> _
        Public Function GetTabularData(DBFunctionName As String, StartRowNum As Integer, RowCount As Integer, Filters As Dictionary(Of String, String)) As Dictionary(Of String, List(Of String))
            Dim res As New Dictionary(Of String, List(Of String))

            Dim param(Filters.Count + 1) As SqlParameter

            param(0) = New SqlParameter("@StartRowCount", StartRowNum)
            param(1) = New SqlParameter("@EndRowCount", StartRowNum + RowCount)
            Dim i As Integer = 2
            For Each item In Filters
                param(i) = New SqlParameter("@" & item.Key, item.Value)
                i += 1
            Next

            Try
                Using DB As New RRAW.DB.DBClass(DBFunctionName, True, param)
                    res = DataTableToComplexDictionary(DB.GetDataTable)
                End Using
            Catch
                Throw
            End Try

            Return res
        End Function

        'Public Function GetRequests(StartRowNum As Integer, RowCount As Integer, ID As String, RequestDate As String, Requestor As String, OriginPort As String, OriginZipcode As String, DestinationPort As String, DestinationZipcode As String, CurrentApprover As String, OriginComment As String) As Dictionary(Of String, Dictionary(Of String, String))
        '    Dim res As New Dictionary(Of String, Dictionary(Of String, String))

        '    Dim param(10) As SqlParameter

        '    param(0) = New SqlParameter("@StartRowCount", StartRowNum)
        '    param(1) = New SqlParameter("@EndRowCount", StartRowNum + RowCount)
        '    param(2) = New SqlParameter("@ID", ID)
        '    param(3) = New SqlParameter("@RequestDate", RequestDate)
        '    param(4) = New SqlParameter("@Requestor", Requestor)
        '    param(5) = New SqlParameter("@OriginPort", OriginPort)
        '    param(6) = New SqlParameter("@OriginZipcode", OriginZipcode)
        '    param(7) = New SqlParameter("@DestinationPort", DestinationPort)
        '    param(8) = New SqlParameter("@DestinationZipcode", DestinationZipcode)
        '    param(9) = New SqlParameter("@CurrentApprover", CurrentApprover)
        '    param(10) = New SqlParameter("@OriginComment", OriginComment)

        '    Try
        '        Using DB As New RRAW.DB.DBClass("GetAirRateRequests_TEST", True, param)
        '            res = DataTableToComplexDictionaryWithColumn(DB.GetDataTable)
        '        End Using
        '    Catch
        '        Throw
        '    End Try

        '    Return res
        'End Function

        '<ScriptMethod(UseHttpGet:=True)>
        <WebMethod()> _
        Public Function GetTabularDataRowCount(DBFunctionName As String, Filters As Dictionary(Of String, String)) As Integer
            Dim res As New Integer

            Dim param(Filters.Count - 1) As SqlParameter

            Dim i As Integer = 0
            For Each item In Filters
                param(i) = New SqlParameter("@" & item.Key, item.Value)
                i += 1
            Next

            Try
                Using DB As New RRAW.DB.DBClass(DBFunctionName, True, param)
                    res = CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try

            Return res
        End Function

        '<ScriptMethod(UseHttpGet:=True)>
        '<WebMethod()> _
        'Public Function GetRequestsCount(ID As String, RequestDate As String, Requestor As String, OriginPort As String, OriginZipcode As String, DestinationPort As String, DestinationZipcode As String, CurrentApprover As String, OriginComment As String) As Integer
        '    Dim res As New Integer

        '    Dim param(8) As SqlParameter

        '    param(0) = New SqlParameter("@ID", ID)
        '    param(1) = New SqlParameter("@RequestDate", RequestDate)
        '    param(2) = New SqlParameter("@Requestor", Requestor)
        '    param(3) = New SqlParameter("@OriginPort", OriginPort)
        '    param(4) = New SqlParameter("@OriginZipcode", OriginZipcode)
        '    param(5) = New SqlParameter("@DestinationPort", DestinationPort)
        '    param(6) = New SqlParameter("@DestinationZipcode", DestinationZipcode)
        '    param(7) = New SqlParameter("@CurrentApprover", CurrentApprover)
        '    param(8) = New SqlParameter("@OriginComment", OriginComment)

        '    Try
        '        Using DB As New RRAW.DB.DBClass("GetAirRateRequestsCount_TEST", True, param)
        '            res = CInt(DB.ExecuteScalar)
        '        End Using
        '    Catch
        '        Throw
        '    End Try

        '    Return res
        'End Function

        <WebMethod()> _
        Public Function GetTabularDataCSVFile(DBFunctionName As String, CSVFileName As String, CSVFileType As String, TitleInFile As String, Filters As Dictionary(Of String, String)) As String
            Dim param(Filters.Count - 1) As SqlParameter

            Dim i As Integer = 0
            For Each item In Filters
                param(i) = New SqlParameter("@" & item.Key, item.Value)
                i += 1
            Next

            Try
                Using DB As New RRAW.DB.DBClass(DBFunctionName, True, param)
                    CSVFileName = "Reports/" & CSVFileName & "_" & Now.Ticks & IIf(CSVFileType = "Excel", ".xls", ".csv").ToString

                    My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & CSVFileName), DataTableToCSV(TitleInFile, DB.GetDataTable), False)
                End Using
            Catch
                Throw
            End Try

            Return CSVFileName
        End Function

        '<ScriptMethod(UseHttpGet:=True)>
        '<WebMethod()> _
        'Public Function GetRequestsCSV(CSVFileName As String, CSVFileType As String, TitleInFile As String, ID As String, RequestDate As String, Requestor As String, OriginPort As String, OriginZipcode As String, DestinationPort As String, DestinationZipcode As String, CurrentApprover As String, OriginComment As String) As String
        '    Dim param(8) As SqlParameter

        '    param(0) = New SqlParameter("@ID", ID)
        '    param(1) = New SqlParameter("@RequestDate", RequestDate)
        '    param(2) = New SqlParameter("@Requestor", Requestor)
        '    param(3) = New SqlParameter("@OriginPort", OriginPort)
        '    param(4) = New SqlParameter("@OriginZipcode", OriginZipcode)
        '    param(5) = New SqlParameter("@DestinationPort", DestinationPort)
        '    param(6) = New SqlParameter("@DestinationZipcode", DestinationZipcode)
        '    param(7) = New SqlParameter("@CurrentApprover", CurrentApprover)
        '    param(8) = New SqlParameter("@OriginComment", OriginComment)

        '    Try
        '        Using DB As New RRAW.DB.DBClass("GetAirRateRequestsCSV_TEST", True, param)
        '            CSVFileName = "Reports/" & CSVFileName & "_" & Now.Ticks & IIf(CSVFileType = "Excel", ".xls", ".csv").ToString

        '            My.Computer.FileSystem.WriteAllText(Server.MapPath("~/" & CSVFileName), DataTableToCSV(TitleInFile, DB.GetDataTable), False)
        '        End Using
        '    Catch
        '        Throw
        '    End Try

        '    Return CSVFileName
        'End Function
    End Class
End Namespace