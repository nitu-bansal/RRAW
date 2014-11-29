Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Utilities
        Private Sub New()
        End Sub

        Friend Shared Function GetCurrentDate() As Date
            Dim query As String = "GetCurrentDate"

            Try
                Using DB As New DBClass(query, True)
                    Return CDate(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace