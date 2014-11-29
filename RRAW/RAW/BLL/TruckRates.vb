Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class TruckRates

        Private Sub New()
        End Sub

        Public Shared Function GetRates(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetTruckRates"

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

        Public Shared Function PostNewRates(ByVal RateRequestID As Integer, ByVal RateTitle As String, ByVal Tonnage As String, ByVal LTLPerCBM As Double, ByVal LTLMin As Double, ByVal FTL As Double) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "PostNewTruckRates"

            Dim param(5) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateTitle", RateTitle)
            param(2) = New SqlParameter("@Tonnage", Tonnage)
            param(3) = New SqlParameter("@LTLPerCBM", LTLPerCBM)
            param(4) = New SqlParameter("@LTLMin", LTLMin)
            param(5) = New SqlParameter("@FTL", FTL)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateRates(ByVal RateID As Integer, ByVal RateTitle As String, ByVal Tonnage As String, ByVal LTLPerCBM As Double, ByVal LTLMin As Double, ByVal FTL As Double) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "UpdateTruckRate"

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@RateID", RateID)
            param(1) = New SqlParameter("@RateTitle", RateTitle)
            param(2) = New SqlParameter("@Tonnage", Tonnage)
            param(3) = New SqlParameter("@LTLPerCBM", LTLPerCBM)
            param(4) = New SqlParameter("@LTLMin", LTLMin)
            param(5) = New SqlParameter("@FTL", FTL)

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
            Dim query As String = "RemoveTruckRate"

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
