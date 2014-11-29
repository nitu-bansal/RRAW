Imports System.Data.SqlClient
Public Class IdentifyEuropeanZones
    Public Shared Function GetEuropeanZone(ByVal flag As String, ByVal Country As String, ByVal Pc2 As String) As DataTable
        Dim query As String = "GetAllEuropianZone"

        Try
            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@Flag", flag)
            param(1) = New SqlParameter("@Country", Country)
            param(2) = New SqlParameter("@PC2", Pc2)
            Using DB As New DB.DBClass(query, True, param)
                Return DB.GetDataTable
            End Using
        Catch
            Throw
        End Try
    End Function
End Class
