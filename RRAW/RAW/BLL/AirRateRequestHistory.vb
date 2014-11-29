Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class AirRateRequestHistory

        Private Sub New()
        End Sub

        Public Shared Function PostNewAirRateRequestHistory(ByVal RateRequestID As Integer, ByVal UpdatorID As Integer, ByVal UpdateDate As DateTime, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double, ByVal OtherCharges As String) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Or UpdatorID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "PostNewAirRateRequestHistory"
            If OtherCharges Is Nothing Then OtherCharges = " "

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@UpdatorID", UpdatorID)
            param(2) = New SqlParameter("@UpdateDate", UpdateDate)
            param(3) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(4) = New SqlParameter("@FreightRate", FreightRate)
            param(5) = New SqlParameter("@SecurityRate", SecurityRate)
            param(6) = New SqlParameter("@OtherCharges", OtherCharges)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRateRequestHistory(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAirRateRequestHistory"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewAirRateRequestHistory_15july(ByVal RateRequestID As Integer, ByVal UpdatorID As Integer, ByVal UpdateDate As DateTime, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal OtherCharges As String) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Or UpdatorID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "PostNewAirRateRequestHistory_15july"
            If OtherCharges Is Nothing Then OtherCharges = " "

            Dim param(5) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@UpdatorID", UpdatorID)
            param(2) = New SqlParameter("@UpdateDate", UpdateDate)
            param(3) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(4) = New SqlParameter("@FreightRate", FreightRate)
            param(5) = New SqlParameter("@OtherCharges", OtherCharges)

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