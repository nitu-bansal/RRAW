Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class ShipMethods

        Private Sub New()
        End Sub

        Public Shared Function GetAllShipMethods() As DataTable
            Dim query As String = "GetAllShipMethods"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllShipMethods_15july() As DataTable
            Dim query As String = "GetAllShipMethods_15july"

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
