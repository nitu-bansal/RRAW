Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class OceanRates

        Private Sub New()
        End Sub

        Public Shared Function GetRates(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetOceanRates"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable()
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewRates(ByVal RateRequestID As Integer, ByVal RateTitle As String, ByVal BasedOn As String, ByVal PerCBM As String, ByVal Per20GP As String, ByVal Per40GP As String, ByVal Per40HC As String) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "PostNewOceanRates"

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateTitle", RateTitle)
            param(2) = New SqlParameter("@BasedOn", BasedOn)
            param(3) = New SqlParameter("@PerCBM", PerCBM)
            param(4) = New SqlParameter("@Per20GP", Per20GP)
            param(5) = New SqlParameter("@Per40GP", Per40GP)
            param(6) = New SqlParameter("@Per40HC", Per40HC)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateRates(ByVal RateID As Integer, ByVal RateTitle As String, ByVal BasedOn As String, ByVal PerCBM As String, ByVal Per20GP As String, ByVal Per40GP As String, ByVal Per40HC As String) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "UpdateOceanRate"

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@RateID", RateID)
            param(1) = New SqlParameter("@RateTitle", RateTitle)
            param(2) = New SqlParameter("@BasedOn", BasedOn)
            param(3) = New SqlParameter("@PerCBM", PerCBM)
            param(4) = New SqlParameter("@Per20GP", Per20GP)
            param(5) = New SqlParameter("@Per40GP", Per40GP)
            param(6) = New SqlParameter("@Per40HC", Per40HC)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRates(ByVal RateID As Integer) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "RemoveOceanRate"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateID", RateID)

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
