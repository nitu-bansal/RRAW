Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Issues
        Private Sub New()
        End Sub

        Public Shared Function GetAllModules() As DataTable
            Dim query As String = "GetAllIssueModules"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

    End Class
End Namespace