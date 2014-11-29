Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class FSC
        Private Sub New()
        End Sub


        Public Shared Function GetFSCData(ByVal EffectiveDate As Date) As DataTable
            Dim query As String = "GetFSCData"

            Try
                Dim param As SqlParameter

                param = New SqlParameter("@EffectiveDate", EffectiveDate)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetFSCDatesToFillCombo() As DataTable
            Dim query As String = "GetFSCDatesToFillCombo"

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
