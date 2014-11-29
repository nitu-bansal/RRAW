Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class CustomClearanceModes

        Private Sub New()
        End Sub

        Public Shared Function GetAllCustomClearanceModes() As DataTable
            Dim query As String = "GetAllCustomClearanceModes"

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
