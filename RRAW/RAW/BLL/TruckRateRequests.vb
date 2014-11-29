Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class TruckRateRequests

        Private Sub New()
        End Sub

        Public Shared Function PostNewRateRequest(ByVal RateRequestID As Integer, ByVal RequestorID As Integer, ByVal ContainerNo As String, ByVal TruckHBL As String, ByVal ShipDate As Date, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As Date, ByVal WarehouseType As String, ByVal CustomClearanceMode As String, ByVal Comment As String) As Integer
            Dim query As String = "PostNewTruckRateRequest"

            Dim param(21) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RequestorID", RequestorID)
            param(2) = New SqlParameter("@ContainerNo", ContainerNo)
            param(3) = New SqlParameter("@TruckHBL", TruckHBL)
            param(4) = New SqlParameter("@ShipDate", ShipDate)
            param(5) = New SqlParameter("@FreightTerm", FreightTerm)
            param(6) = New SqlParameter("@WDShipMethod", WDShipMethod)
            param(7) = New SqlParameter("@ShipperName", ShipperName)
            param(8) = New SqlParameter("@OriginCity", OriginCity)
            param(9) = New SqlParameter("@OriginPort", OriginPort)
            param(10) = New SqlParameter("@OriginZipcode", OriginZipcode)
            param(11) = New SqlParameter("@OriginRegion", OriginRegion)
            param(12) = New SqlParameter("@ConsigneeName", ConsigneeName)
            param(13) = New SqlParameter("@DestCity", DestCity)
            param(14) = New SqlParameter("@DestPort", DestPort)
            param(15) = New SqlParameter("@DestZipcode", DestZipcode)
            param(16) = New SqlParameter("@DestRegion", DestRegion)
            param(17) = New SqlParameter("@RatesValidFor", RatesValidFor)
            param(18) = New SqlParameter("@RatesValidTill", RatesValidTill)
            param(19) = New SqlParameter("@WarehouseType", WarehouseType)
            param(20) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
            param(21) = New SqlParameter("@Comment", Comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateRateRequest(ByVal RateRequestID As Integer, ByVal ContainerNo As String, ByVal TruckHBL As String, ByVal ShipDate As Date, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As Date, ByVal WarehouseType As String, ByVal CustomClearanceMode As String, ByVal Comment As String) As Integer
            Dim query As String = "UpdateTruckRateRequest"

            Dim param(20) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ContainerNo", ContainerNo)
            param(2) = New SqlParameter("@TruckHBL", TruckHBL)
            param(3) = New SqlParameter("@ShipDate", ShipDate)
            param(4) = New SqlParameter("@FreightTerm", FreightTerm)
            param(5) = New SqlParameter("@WDShipMethod", WDShipMethod)
            param(6) = New SqlParameter("@ShipperName", ShipperName)
            param(7) = New SqlParameter("@OriginCity", OriginCity)
            param(8) = New SqlParameter("@OriginPort", OriginPort)
            param(9) = New SqlParameter("@OriginZipcode", OriginZipcode)
            param(10) = New SqlParameter("@OriginRegion", OriginRegion)
            param(11) = New SqlParameter("@ConsigneeName", ConsigneeName)
            param(12) = New SqlParameter("@DestCity", DestCity)
            param(13) = New SqlParameter("@DestPort", DestPort)
            param(14) = New SqlParameter("@DestZipcode", DestZipcode)
            param(15) = New SqlParameter("@DestRegion", DestRegion)
            param(16) = New SqlParameter("@RatesValidFor", RatesValidFor)
            param(17) = New SqlParameter("@RatesValidTill", RatesValidTill)
            param(18) = New SqlParameter("@WarehouseType", WarehouseType)
            param(19) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
            param(20) = New SqlParameter("@Comment", Comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequest(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetTruckRateRequest"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function PostNewComment(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
            'Validate Inputs
            If Comment.Trim() = "" Then Exit Function

            'Post Comment if valid
            Dim query As String = "PostNewTruckComment"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@UserID", UserID)
            param(2) = New SqlParameter("@Comment", Comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ClearRejectedTag(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "ClearTruckRejectedTag"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ApproveRateRequestByClient(ByVal RateRequestID As Integer, ByVal ApproverID As Integer) As Integer
            If RateRequestID <= 0 Or ApproverID <= 0 Then Exit Function

            Dim query As String = "ApproveTruckRateRequestByClient"

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApproverID", ApproverID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RejectRateRequest(ByVal RateRequestID As Integer, ByVal RejectorID As Integer) As Integer
            If RateRequestID <= 0 Or RejectorID <= 0 Then Exit Function

            Dim query As String = "RejectTruckRateRequestByClient"

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RejectorID", RejectorID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RevokeRateRequest(ByVal RateRequestID As Integer, ByVal RevokerID As Integer) As Integer
            Dim query As String = "RevokeTruckRateRequest"

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateRequestHolderID", RevokerID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ArchiveRateRequest(ByVal RateRequestID As Integer, ByVal ArchiverID As Integer) As Integer
            Dim query As String = "ArchiveTruckRateRequest"

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ArchiverID", ArchiverID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAllComments(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAllTruckRateRequestComments"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

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
