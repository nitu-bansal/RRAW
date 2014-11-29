Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class RateRequestMaster

        Private Sub New()
        End Sub

        Public Shared Function PostNewRateRequestID(ByVal RateRequestType As Char) As Integer
            'Validate inputs for business logics
            If RateRequestType <> "A" And RateRequestType <> "O" And RateRequestType <> "T" Then Return -1


            'Call DAL function if valid
            Dim query As String = "PostNewRateRequestID"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestType", RateRequestType)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

    End Class
End Namespace
