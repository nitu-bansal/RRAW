
Imports System.Data.SqlClient
Namespace DB
    Public NotInheritable Class Dashboard
        Private Sub New()
        End Sub

        Public Shared Function GetAllClients(ByVal UserID As Integer) As DataTable
            Dim query As String = "GetAllClients"

            Try
                Dim param(0) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        

        Public Shared Function GetAllAccountManagerToSendEmail(ByVal IsAlert As Integer) As DataTable
            Dim query As String = "GetAllAccountManagerToSendEmail"

            Try

                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllApproverToSendEmail(ByVal IsAlert As Integer) As DataTable
            Dim query As String = "GetAllApproverToSendEmail"

            Try
                Dim param(0) As SqlParameter

                param(0) = New SqlParameter("@IsAlert", IsAlert)
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        

    End Class
End Namespace
