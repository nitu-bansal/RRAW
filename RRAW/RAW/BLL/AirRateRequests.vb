Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class AirRateRequests

        Private Sub New()
        End Sub

        Public Shared Function GetMinRequestDate() As String
            Dim query As String = "GetMinRequestDate"

            Try
                Using DB As New DBClass(query, True)
                    Dim t As String = DB.ExecuteScalar.ToString
                    If IsDate(t) Then
                        Return CDate(t).ToString("MM/dd/yyyy")
                    Else
                        Return CDate("1/1/1753").ToString("MM/dd/yyyy")
                    End If
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetMaxRequestDate() As String
            Dim query As String = "GetMaxRequestDate"

            Try
                Using DB As New DBClass(query, True)
                    Dim t As String = DB.ExecuteScalar.ToString
                    If IsDate(t) Then
                        Return CDate(t).ToString("MM/dd/yyyy")
                    Else
                        Return CDate("12/31/9999 23:59:59").ToString("MM/dd/yyyy")
                    End If
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetOpenRequestsCount(ByVal RequestorID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetOpenRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@RequestorID", RequestorID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetInDiscussionStationRequestsCount(ByVal RequestorID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetInDiscussionStationRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@RequestorID", RequestorID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetInDiscussionClientRequestsCount(ByVal RequestorID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetInDiscussionClientRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@RequestorID", RequestorID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetApprovedRequestsCount(ByVal RequestorID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetApprovedRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@RequestorID", RequestorID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRejectedRequestsCount(ByVal RequestorID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetRejectedRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@RequestorID", RequestorID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetOpenToMeRequestsCount(ByVal UserID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetOpenToMeRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function GetInDiscussionInLoopStationRequestsCount(ByVal UserID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetInDiscussionInLoopStationRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetInDiscussionInLoopClientRequestsCount(ByVal UserID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetInDiscussionInLoopClientRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetApprovedInLoopRequestsCount(ByVal UserID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetApprovedInLoopRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRejectedInLoopRequestsCount(ByVal UserID As Integer, ByVal ClientId As Integer, ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetRejectedInLoopRequestsCount"

            Try
                Dim param(3) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@FromDate", FromDate)
                param(3) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllOpenRequestsCount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal ClientId As Integer) As Integer
            Dim query As String = "GetAllOpenRequestsCount"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)
                param(2) = New SqlParameter("@ClientId", ClientId)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllInDiscussionStationRequestsCount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal ClientId As Integer) As Integer
            Dim query As String = "GetAllInDiscussionStationRequestsCount"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)
                param(2) = New SqlParameter("@ClientId", ClientId)
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllInDiscussionClientRequestsCount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal ClientId As Integer) As Integer
            Dim query As String = "GetAllInDiscussionClientRequestsCount"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)
                param(2) = New SqlParameter("@ClientId", ClientId)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllApprovedRequestsCount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal ClientId As Integer) As Integer
            Dim query As String = "GetAllApprovedRequestsCount"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)
                param(2) = New SqlParameter("@ClientId", ClientId)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllRejectedRequestsCount(ByVal FromDate As Date, ByVal ToDate As Date, ByVal ClientId As Integer) As Integer
            Dim query As String = "GetAllRejectedRequestsCount"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)
                param(2) = New SqlParameter("@ClientId", ClientId)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllOpenRequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllOpenRequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllInDiscussionCEVARequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllInDiscussionCEVARequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllInDiscussionClientRequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllInDiscussionClientRequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllApprovedRequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllApprovedRequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllApprovedAsAdhocRequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllApprovedAsAdhocRequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllRejectedRequestsValue(ByVal FromDate As Date, ByVal ToDate As Date) As Integer
            Dim query As String = "GetAllRejectedRequestsValue"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@FromDate", FromDate)
                param(1) = New SqlParameter("@ToDate", ToDate)

                Using DB As New DBClass(query, True, param)
                    Dim res As Object = DB.ExecuteScalar
                    If IsDBNull(res) = False Then
                        Return CInt(res)
                    Else
                        Return 0
                    End If
                End Using
            Catch
                Throw
            End Try
        End Function

        'Public Shared Function GetOpenRequestsScopeCount(ByVal UserID As Integer) As Integer
        '    Dim query As String = "GetOpenRequestsScopeCount"

        '    Try
        '        Dim param1 As New SqlParameter("@UserID", UserID)

        '        Using DB As New DBClass(query, True, param1)
        '            Return CInt(DB.ExecuteScalar)
        '        End Using
        '    Catch
        '        Throw
        '    End Try
        'End Function

        'Public Shared Function GetInDiscussionRequestsScopeCount(ByVal UserID As Integer) As Integer
        '    Dim query As String = "GetInDiscussionRequestsScopeCount"

        '    Try
        '        Dim param1 As New SqlParameter("@UserID", UserID)

        '        Using DB As New DBClass(query, True, param1)
        '            Return CInt(DB.ExecuteScalar)
        '        End Using
        '    Catch
        '        Throw
        '    End Try
        'End Function

        'Public Shared Function GetApprovedRequestsScopeCount(ByVal UserID As Integer) As Integer
        '    Dim query As String = "GetApprovedRequestsScopeCount"

        '    Try
        '        Dim param1 As New SqlParameter("@UserID", UserID)

        '        Using DB As New DBClass(query, True, param1)
        '            Return CInt(DB.ExecuteScalar)
        '        End Using
        '    Catch
        '        Throw
        '    End Try
        'End Function

        Public Shared Function GetRateRequests() As DataTable
            Dim query As String = "GetAirRateRequests"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestByID(ByVal RateRequestID As Integer) As DataRow
            Dim query As String = "GetAirRateRequestByID"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataRow()
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRateRequest(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAirRateRequest"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        'Public Shared Function GetRateRequestQuery(Optional ByVal Requestor As String = Nothing, Optional ByVal RequestDate As Date = Nothing, Optional ByVal OriginAirport As String = Nothing, Optional ByVal DestinationAirport As String = Nothing, Optional ByVal DestinationCity As String = Nothing, Optional ByVal DestinationState As String = Nothing, Optional ByVal DestinationCountry As String = Nothing, Optional ByVal DestinationZipcode As String = Nothing, Optional ByVal ServiceLevel As String = Nothing, Optional ByVal ServiceLevelDesc As String = Nothing, Optional ByVal FinalApprover As String = Nothing, Optional ByVal MinFreightRate As Double = Nothing, Optional ByVal FreightRate As Double = Nothing, Optional ByVal SecurityRate As Double = Nothing, Optional ByVal CAFRate As Double = Nothing, Optional ByVal AEWRRate As Double = Nothing, Optional ByVal EffectiveDate As Date = Nothing, Optional ByVal ExpiryDate As Date = Nothing) As String
        '    Dim query As String = "SELECT " & _
        '                            "Users_Requestor.Name AS Requestor, " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.RequestDate, 101) AS 'Request Date', " & _
        '                            "AirRateRequests.OriginAirport AS 'Origin Airport', " & _
        '                            "AirRateRequests.DestAirport AS 'Dest Airport', " & _
        '                            "AirRateRequests.DestCity AS 'Dest City', " & _
        '                            "AirRateRequests.DestState AS 'Dest State', " & _
        '                            "AirRateRequests.DestZipcode AS 'Dest Zipcode', " & _
        '                            "AirRateRequests.DestCountry AS 'Dest Country', " & _
        '                            "AirRateRequests.ServiceLevel AS 'Service Level', " & _
        '                            "AirRateRequests.ServiceLevelDesc AS 'Service Level Description', " & _
        '                            "Users_FinalApprover.Name AS 'Final Approver', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.MinFreightRate, 1) AS 'Min Freight Rate', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.FreightRate, 1) AS 'Freight Rate', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.SecurityRate, 1) AS 'Security Rate', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.CAFRate, 1) AS 'CAF Rate', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.AEWRRate, 1) AS 'AEWR Rate', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.EffectiveDate, 101) AS 'Effective Date', " & _
        '                            "CONVERT(VARCHAR(10), AirRateRequests.ExpiryDate, 101) AS 'Expiry Date' " & _
        '                        "FROM " & _
        '                            "AirRateRequests " & _
        '                            "LEFT JOIN Users AS Users_Requestor ON Users_Requestor.ID = AirRateRequests.RequestorID " & _
        '                            "LEFT JOIN Users AS Users_FinalApprover ON Users_FinalApprover.ID = AirRateRequests.FinalApproverID " & _
        '                        "WHERE " & _
        '                            "1 = 1"
        '    If Requestor.Length > 0 Then
        '        query += " AND Users_Requestor.Name LIKE '%" & Requestor & "%'"
        '    End If
        '    If RequestDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), AirRateRequests.RequestDate, 101) = '" & RequestDate & "'"
        '    End If
        '    If OriginAirport.Length > 0 Then
        '        query += " AND AirRateRequests.OriginAirport LIKE '%" & OriginAirport & "%'"
        '    End If
        '    If DestinationAirport.Length > 0 Then
        '        query += " AND AirRateRequests.DestAirport LIKE '%" & DestinationAirport & "%'"
        '    End If
        '    If DestinationCity.Length > 0 Then
        '        query += " AND AirRateRequests.DestCity LIKE '%" & DestinationCity & "%'"
        '    End If
        '    If DestinationState.Length > 0 Then
        '        query += " AND AirRateRequests.DestState LIKE '%" & DestinationState & "%'"
        '    End If
        '    If DestinationCountry.Length > 0 Then
        '        query += " AND AirRateRequests.DestCountry LIKE '%" & DestinationCountry & "%'"
        '    End If
        '    If DestinationZipcode.Length > 0 Then
        '        query += " AND AirRateRequests.DestZipcode LIKE '%" & DestinationZipcode & "%'"
        '    End If
        '    If ServiceLevel.Length > 0 Then
        '        query += " AND AirRateRequests.ServiceLevel LIKE '%" & ServiceLevel & "%'"
        '    End If
        '    If ServiceLevelDesc.Length > 0 Then
        '        query += " AND AirRateRequests.ServiceLevelDesc LIKE '%" & ServiceLevelDesc & "%'"
        '    End If
        '    If FinalApprover <> Nothing Then
        '        query += " AND Users_FinalApprover.Name LIKE '%" & FinalApprover & "%'"
        '    End If
        '    If MinFreightRate <> Nothing Then
        '        query += " AND AirRateRequests.MinFreightRate = " & MinFreightRate
        '    End If
        '    If FreightRate <> Nothing Then
        '        query += " AND AirRateRequests.FreightRate = " & FreightRate
        '    End If
        '    If SecurityRate <> Nothing Then
        '        query += " AND AirRateRequests.SecurityRate = " & SecurityRate
        '    End If
        '    If CAFRate <> Nothing Then
        '        query += " AND AirRateRequests.CAFRate = " & CAFRate
        '    End If
        '    If AEWRRate <> Nothing Then
        '        query += " AND AirRateRequests.AEWRRate = " & AEWRRate
        '    End If
        '    If EffectiveDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), AirRateRequests.EffectiveDate, 101) = '" & EffectiveDate & "'"
        '    End If
        '    If ExpiryDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), AirRateRequests.ExpiryDate, 101) = '" & ExpiryDate & "'"
        '    End If
        '    Return query
        'End Function

        'Public Shared Function GetAdhocLaneQuery(ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginZipCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double, ByVal OtherCharges As String, ByVal EffectiveDate As Date, ByVal ExpiryDate As Date, ByVal FreightForwarder As String, ByVal OriginComment As String, ByVal ApprovalDateFrom As Date, ByVal ApprovalDateTo As Date, ByVal RequestDate As Date) As String
        '    Dim query As String = "SELECT " & _
        '                            "OriginAirport AS 'Origin Airport', " & _
        '                            "OriginRegion AS 'Origin Region', " & _
        '                            "OriginZipCode AS 'Origin Code', " & _
        '                            "DestAirport AS 'Dest Airport', " & _
        '                            "DestCity AS 'Dest City', " & _
        '                            "DestState AS 'Dest State', " & _
        '                            "DestCountry AS 'Dest Country', " & _
        '                            "DestRegion AS 'Dest Region', " & _
        '                            "DestZipcode AS 'Dest Zipcode', " & _
        '                            "CEVATransitMode AS 'CEVA Transit Mode', " & _
        '                            "ShipMethod AS 'Ship Method', " & _
        '                            "ForwarderZipcode AS 'Forwarder Zipcode', " & _
        '                            "CustomClearanceMode AS 'Custom Clearance Mode', " & _
        '                            "ForwarderServiceLevel AS 'Forwarder Service Level', " & _
        '                            "CONVERT(VARCHAR(20), MinFreightRate, 1) AS 'Min Freight Rate', " & _
        '                            "CONVERT(VARCHAR(20), FreightRate, 1) AS 'Freight Rate', " & _
        '                            "CONVERT(VARCHAR(20), SecurityRate, 1) AS 'Security Rate', " & _
        '                            "OtherCharges AS 'Other Charges', " & _
        '                            "CONVERT(VARCHAR(10), EffectiveDate, 101) AS 'Effective Date', " & _
        '                            "CONVERT(VARCHAR(10), ExpiryDate, 101) AS 'Expiry Date', " & _
        '                            "FreightForwarder AS 'Freight Forwarder', " & _
        '                            "OriginComment AS 'Origin Comment', " & _
        '                            "CONVERT(VARCHAR(10), ApprovalDate, 101) AS 'Approval Date', " & _
        '                            "CONVERT(VARCHAR(10), RequestDate, 101) AS 'Rate Request Date' " & _
        '                        "FROM " & _
        '                            "AirRateRequests " & _
        '                        "WHERE " & _
        '                            "IsAdhoc = 1"
        '    If OriginAirport <> Nothing Then
        '        query += " AND OriginAirport LIKE '%" & OriginAirport & "%'"
        '    End If
        '    If OriginRegion <> Nothing Then
        '        query += " AND OriginRegion LIKE '%" & OriginRegion & "%'"
        '    End If
        '    If OriginZipCode <> Nothing Then
        '        query += " AND OriginZipCode LIKE '%" & OriginZipCode & "%'"
        '    End If
        '    If DestAirport <> Nothing Then
        '        query += " AND DestAirport LIKE '%" & DestAirport & "%'"
        '    End If
        '    If DestCity <> Nothing Then
        '        query += " AND DestCity LIKE '%" & DestCity & "%'"
        '    End If
        '    If DestState <> Nothing Then
        '        query += " AND DestState LIKE '%" & DestState & "%'"
        '    End If
        '    If DestCountry <> Nothing Then
        '        query += " AND DestCountry LIKE '%" & DestCountry & "%'"
        '    End If
        '    If DestRegion <> Nothing Then
        '        query += " AND DestRegion LIKE '%" & DestRegion & "%'"
        '    End If
        '    If DestZipcode <> Nothing Then
        '        query += " AND DestZipcode LIKE '%" & DestZipcode & "%'"
        '    End If
        '    If CEVATransitMode <> Nothing Then
        '        query += " AND CEVATransitMode LIKE '%" & CEVATransitMode & "%'"
        '    End If
        '    If ShipMethod <> Nothing Then
        '        query += " AND ShipMethod LIKE '%" & ShipMethod & "%'"
        '    End If
        '    If ForwarderZipcode <> Nothing Then
        '        query += " AND ForwarderZipcode LIKE '%" & ForwarderZipcode & "%'"
        '    End If
        '    If CustomClearanceMode <> Nothing Then
        '        query += " AND CustomClearanceMode LIKE '%" & CustomClearanceMode & "%'"
        '    End If
        '    If ForwarderServiceLevel <> Nothing Then
        '        query += " AND ForwarderServiceLevel LIKE '%" & ForwarderServiceLevel & "%'"
        '    End If
        '    If MinFreightRate <> Nothing Then
        '        query += " AND MinFreightRate = " & MinFreightRate
        '    End If
        '    If FreightRate <> Nothing Then
        '        query += " AND FreightRate = " & FreightRate
        '    End If
        '    If SecurityRate <> Nothing Then
        '        query += " AND SecurityRate = " & SecurityRate
        '    End If
        '    If OtherCharges <> Nothing Then
        '        query += " AND OtherCharges LIKE '%" & OtherCharges & "%'"
        '    End If
        '    If EffectiveDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), EffectiveDate, 101) = '" & EffectiveDate & "'"
        '    End If
        '    If ExpiryDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), ExpiryDate, 101) = '" & ExpiryDate & "'"
        '    End If
        '    If FreightForwarder <> Nothing Then
        '        query += " AND FreightForwarder LIKE '%" & FreightForwarder & "%'"
        '    End If
        '    If OriginComment <> Nothing Then
        '        query += " AND OriginComment LIKE '%" & OriginComment & "%'"
        '    End If
        '    If ApprovalDateFrom <> Nothing Then
        '        If ApprovalDateTo <> Nothing Then
        '            query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) BETWEEN '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "' AND '" & ApprovalDateTo.ToString("MM/dd/yyyy") & "'"
        '        Else
        '            query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) = '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "'"
        '        End If
        '    End If
        '    If RequestDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), RequestDate, 101) = '" & RequestDate & "'"
        '    End If
        '    Return query
        'End Function

        ''' <summary>
        ''' Posts new rate request
        ''' </summary>
        ''' <remarks></remarks>

        Public Shared Function PostNewRateRequest(ByVal RequestorID As Integer, ByVal RateRequestDate As DateTime, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String, ByVal ForwarderServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal ForwarderZipcode As String, ByVal CEVATransitMode As String, ByVal DOCType As String, ByVal CustomClearanceMode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double, ByVal OtherCharges As String, ByVal FreightForwarder As String, ByVal OriginComment As String) As Integer
            'Validate Inputs
            If RequestorID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "PostNewAirRateRequest"
            Dim newRateRequestID As Integer

            Dim param(30) As SqlParameter

            param(0) = New SqlParameter("@RequestorID", RequestorID)
            param(1) = New SqlParameter("@RateRequestDate", RateRequestDate)
            param(2) = New SqlParameter("@HAWBNumber", DBStrValue(HAWBNumber))
            param(3) = New SqlParameter("@ShipMethod", ShipMethod)
            param(4) = New SqlParameter("@ShipDate", If(ShipDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, ShipDate, SqlTypes.SqlDateTime.Null))
            param(5) = New SqlParameter("@ServiceLevel", ServiceLevel)
            param(6) = New SqlParameter("@ForwarderServiceLevel", ForwarderServiceLevel)
            param(7) = New SqlParameter("@Weight", If(Weight > 0, Weight, Nothing))
            param(8) = New SqlParameter("@ShipperName", DBStrValue(ShipperName))
            param(9) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(10) = New SqlParameter("@OriginCity", DBStrValue(OriginCity))
            param(11) = New SqlParameter("@OriginRegion", DBStrValue(OriginRegion))
            param(12) = New SqlParameter("@OriginZipcode", DBStrValue(OriginZipcode))
            param(13) = New SqlParameter("@ConsigneeName", DBStrValue(Consignee))
            param(14) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(15) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(16) = New SqlParameter("@DestState", DBStrValue(DestState))
            param(17) = New SqlParameter("@DestCountry", DBStrValue(DestCountry))
            param(18) = New SqlParameter("@DestRegion", DBStrValue(DestRegion))
            param(19) = New SqlParameter("@DestZipcode", DBStrValue(DestZipcode))
            param(20) = New SqlParameter("@ForwarderZipcode", DBStrValue(ForwarderZipcode))
            param(21) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(22) = New SqlParameter("@DOCType", DOCType)
            param(23) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
            param(24) = New SqlParameter("@RateDeterMethod", DBStrValue(RateDeterMethod))
            param(25) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(26) = New SqlParameter("@FreightRate", FreightRate)
            param(27) = New SqlParameter("@SecurityRate", SecurityRate)
            param(28) = New SqlParameter("@OtherCharges", DBStrValue(OtherCharges))
            param(29) = New SqlParameter("@FreightForwarder", DBStrValue(FreightForwarder))
            param(30) = New SqlParameter("@OriginComment", DBStrValue(OriginComment))

            Try
                Using DB As New DBClass(query, True, param)
                    newRateRequestID = CInt(DB.ExecuteScalar())
                End Using

                Return newRateRequestID
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateRateRequest(ByVal RateRequestID As Integer, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String, ByVal ForwarderServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal ForwarderZipcode As String, ByVal CEVATransitMode As String, ByVal DOCType As String, ByVal CustomClearanceMode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double, ByVal OtherCharges As String, ByVal FreightForwarder As String, ByVal OriginComment As String, ByVal CurrentRateRequestHolders As List(Of Integer)) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "UpdateAirRateRequest"
            Dim updateResult As Integer

            Dim param(29) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@HAWBNumber", DBStrValue(HAWBNumber))
            param(2) = New SqlParameter("@ShipMethod", ShipMethod)
            param(3) = New SqlParameter("@ShipDate", If(ShipDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, ShipDate, SqlTypes.SqlDateTime.Null))
            param(4) = New SqlParameter("@ServiceLevel", ServiceLevel)
            param(5) = New SqlParameter("@ForwarderServiceLevel", ForwarderServiceLevel)
            param(6) = New SqlParameter("@Weight", If(Weight > 0, Weight, Nothing))
            param(7) = New SqlParameter("@ShipperName", DBStrValue(ShipperName))
            param(8) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(9) = New SqlParameter("@OriginCity", DBStrValue(OriginCity))
            param(10) = New SqlParameter("@OriginRegion", DBStrValue(OriginRegion))
            param(11) = New SqlParameter("@OriginZipcode", DBStrValue(OriginZipcode))
            param(12) = New SqlParameter("@ConsigneeName", DBStrValue(Consignee))
            param(13) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(14) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(15) = New SqlParameter("@DestState", DBStrValue(DestState))
            param(16) = New SqlParameter("@DestCountry", DBStrValue(DestCountry))
            param(17) = New SqlParameter("@DestRegion", DBStrValue(DestRegion))
            param(18) = New SqlParameter("@DestZipcode", DBStrValue(DestZipcode))
            param(19) = New SqlParameter("@ForwarderZipcode", DBStrValue(ForwarderZipcode))
            param(20) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(21) = New SqlParameter("@DOCType", DOCType)
            param(22) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
            param(23) = New SqlParameter("@RateDeterMethod", DBStrValue(RateDeterMethod))
            param(24) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(25) = New SqlParameter("@FreightRate", FreightRate)
            param(26) = New SqlParameter("@SecurityRate", SecurityRate)
            param(27) = New SqlParameter("@OtherCharges", DBStrValue(OtherCharges))
            param(28) = New SqlParameter("@FreightForwarder", DBStrValue(FreightForwarder))
            param(29) = New SqlParameter("@OriginComment", DBStrValue(OriginComment))

            Try
                Using DB As New DBClass(query, True, param)
                    updateResult = CInt(DB.ExecuteScalar())
                End Using

                Return updateResult
            Catch
                Throw
            End Try
        End Function

        Public Shared Function MakeRateRequestAdhoc(ByVal RateRequestID As Integer, ByVal OriginComment As String) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "MakeAirRateRequestAdhoc"
            Dim updateResult As Integer

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@OriginComment", DBStrValue(OriginComment))

            Try
                Using DB As New DBClass(query, True, param)
                    updateResult = CInt(DB.ExecuteScalar())
                End Using

                Return updateResult
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ResetAdhocRateRequest(ByVal RateRequestID As Integer) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "ResetAdhocAirRateRequest"
            Dim updateResult As Integer

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    updateResult = CInt(DB.ExecuteScalar())
                End Using

                Return updateResult
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestRecordCount(ByVal query As String) As Integer
            Try
                Try
                    query = query.Replace(query.Substring(0, query.IndexOf("FROM ")), "SELECT COUNT(*) ")
                    Using DB As New DBClass(query)
                        Return CInt(DB.ExecuteScalar)
                    End Using
                Catch ex As Exception
                    query = "SELECT COUNT(*) FROM AirRateRequests"
                    Using DB As New DBClass(query)
                        Return CInt(DB.ExecuteScalar)
                    End Using
                End Try
            Catch ex As Exception
            End Try
        End Function

        Public Shared Function ApproveRateRequestByClient(ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal ApprovalDate As Date, ByVal comment As String, ByVal isAdhoc As Boolean) As Integer
            If RateRequestID <= 0 Or ApproverID <= 0 Then Exit Function

            Dim query As String = "ApproveAirRateRequestByClient"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApproverID", ApproverID)
            param(2) = New SqlParameter("@Comment", comment)
            param(3) = New SqlParameter("@IsAdhoc", isAdhoc)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RevokeRateRequest(ByVal rateRequestID As Integer, ByVal rateRequestHolderID As Integer, ByVal comment As String) As Integer
            Dim query As String = "RevokeAirRateRequest"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", rateRequestID)
            param(1) = New SqlParameter("@RateRequestHolderID", rateRequestHolderID)
            param(2) = New SqlParameter("@Comment", comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RejectRateRequestByClient(ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RejectedDate As Date) As Integer
            If RateRequestID <= 0 Or RejectorID <= 0 Then Exit Function

            Dim query As String = "RejectAirRateRequestByClient"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RejectorID", RejectorID)
            param(2) = New SqlParameter("@RejectedDate", RejectedDate)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        'Public Shared Function GetApprovedAsAdhocRateRequestsQuery(ByVal Customer As String, ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String, ByVal OtherCharges As String, ByVal EffectiveDate As Date, ByVal ExpiryDate As Date, ByVal FreightForwarder As String, ByVal Notes As String, ByVal ApprovalDateFrom As Date, ByVal ApprovalDateTo As Date, ByVal ApprovedBy As String, ByVal RateRequestDate As Date, ByVal EntryDate As Date) As String
        '    Dim query As String = "SELECT " & _
        '                            "AirRateRequests.ID, " & _
        '                            "ConsigneeName AS 'Customer', " & _
        '                            "OriginAirport AS 'Origin Airport', " & _
        '                            "OriginRegion AS 'Origin Region', " & _
        '                            "OriginZipCode AS 'Origin Code', " & _
        '                            "DestAirport AS 'Dest Airport', " & _
        '                            "DestCity AS 'Dest City', " & _
        '                            "DestState AS 'Dest State', " & _
        '                            "DestCountry AS 'Dest Country', " & _
        '                            "DestRegion AS 'Dest Region', " & _
        '                            "DestZipcode AS 'Dest Zipcode', " & _
        '                            "CEVATransitMode AS 'CEVA Transit Mode', " & _
        '                            "ShipMethod AS 'Ship Method', " & _
        '                            "ForwarderZipcode AS 'Forwarder Zipcode', " & _
        '                            "CustomClearanceMode AS 'Custom Clearance Mode', " & _
        '                            "ForwarderServiceLevel AS 'Forwarder Service Level', " & _
        '                            "CASE CEVATransitMode " & _
        '                                "WHEN 'GROUND / AIR' THEN 'MYR' " & _
        '                                "ELSE " & _
        '                                    "(CASE OriginAirport " & _
        '                                        "WHEN 'BKK' THEN 'THB' " & _
        '                                        "WHEN 'KUL' THEN 'MYR' " & _
        '                                        "WHEN 'PEN' THEN 'MYR' " & _
        '                                        "WHEN 'JHB' THEN 'MYR' " & _
        '                                        "ELSE 'USD' " & _
        '                                    "END) " & _
        '                            "END + ' ' + CONVERT(VARCHAR(20), MinFreightRate, 1) AS 'Min Freight Rate'," & _
        '                            "CASE CEVATransitMode " & _
        '                                "WHEN 'GROUND / AIR' THEN 'MYR' " & _
        '                                "ELSE " & _
        '                                    "(CASE OriginAirport " & _
        '                                        "WHEN 'BKK' THEN 'THB' " & _
        '                                        "WHEN 'KUL' THEN 'MYR' " & _
        '                                        "WHEN 'PEN' THEN 'MYR' " & _
        '                                        "WHEN 'JHB' THEN 'MYR' " & _
        '                                        "ELSE 'USD' " & _
        '                                    "END) " & _
        '                            "END + ' ' + CONVERT(VARCHAR(20), FreightRate, 1) AS 'Freight Rate'," & _
        '                            "CASE CEVATransitMode " & _
        '                                "WHEN 'GROUND / AIR' THEN 'MYR' " & _
        '                                "ELSE " & _
        '                                    "(CASE OriginAirport " & _
        '                                        "WHEN 'BKK' THEN 'THB' " & _
        '                                        "WHEN 'KUL' THEN 'MYR' " & _
        '                                        "WHEN 'PEN' THEN 'MYR' " & _
        '                                        "WHEN 'JHB' THEN 'MYR' " & _
        '                                        "ELSE 'USD' " & _
        '                                    "END) " & _
        '                            "END + ' ' + CONVERT(VARCHAR(20), SecurityRate, 1) AS 'Security Rate'," & _
        '                            "OtherCharges AS 'Other Charges', " & _
        '                            "CONVERT(VARCHAR(10), EffectiveDate, 101) AS 'Effective Date', " & _
        '                            "CONVERT(VARCHAR(10), ExpiryDate, 101) AS 'Expiry Date', " & _
        '                            "FreightForwarder AS 'Freight Forwarder', " & _
        '                            "'TRUE' AS 'Active', " & _
        '                            "OriginComment AS 'Notes', " & _
        '                            "CONVERT(VARCHAR(10), ApprovalDate, 101) AS 'Approval Date', " & _
        '                            "Approvers.Name AS 'Approved By', " & _
        '                            "'' AS 'Approval Notes', " & _
        '                            "'' AS 'Additional Notes', " & _
        '                            "CONVERT(VARCHAR(10), RequestDate, 101) AS 'Rate Request Date', " & _
        '                            "CONVERT(VARCHAR(10), ApprovalDate, 101) AS 'Entry Date'" & _
        '                        "FROM " & _
        '                            "AirRateRequests JOIN Users AS Approvers ON Approvers.ID = AirRateRequests.ApproverID " & _
        '                        "WHERE " & _
        '                            "IsAdhoc = 1"
        '    If Customer <> Nothing Then
        '        query += " AND ConsigneeName LIKE '%" & Customer & "%'"
        '    End If
        '    If OriginAirport <> Nothing Then
        '        query += " AND OriginAirport LIKE '%" & OriginAirport & "%'"
        '    End If
        '    If OriginRegion <> Nothing Then
        '        query += " AND OriginRegion LIKE '%" & OriginRegion & "%'"
        '    End If
        '    If OriginCode <> Nothing Then
        '        query += " AND OriginZipCode LIKE '%" & OriginCode & "%'"
        '    End If
        '    If DestAirport <> Nothing Then
        '        query += " AND DestAirport LIKE '%" & DestAirport & "%'"
        '    End If
        '    If DestCity <> Nothing Then
        '        query += " AND DestCity LIKE '%" & DestCity & "%'"
        '    End If
        '    If DestState <> Nothing Then
        '        query += " AND DestState LIKE '%" & DestState & "%'"
        '    End If
        '    If DestCountry <> Nothing Then
        '        query += " AND DestCountry LIKE '%" & DestCountry & "%'"
        '    End If
        '    If DestRegion <> Nothing Then
        '        query += " AND DestRegion LIKE '%" & DestRegion & "%'"
        '    End If
        '    If DestZipcode <> Nothing Then
        '        query += " AND DestZipcode LIKE '%" & DestZipcode & "%'"
        '    End If
        '    If CEVATransitMode <> Nothing Then
        '        query += " AND CEVATransitMode LIKE '%" & CEVATransitMode & "%'"
        '    End If
        '    If ShipMethod <> Nothing Then
        '        query += " AND ShipMethod LIKE '%" & ShipMethod & "%'"
        '    End If
        '    If ForwarderZipcode <> Nothing Then
        '        query += " AND ForwarderZipcode LIKE '%" & ForwarderZipcode & "%'"
        '    End If
        '    If CustomClearanceMode <> Nothing Then
        '        query += " AND CustomClearanceMode LIKE '%" & CustomClearanceMode & "%'"
        '    End If
        '    If ForwarderServiceLevel <> Nothing Then
        '        query += " AND ForwarderServiceLevel LIKE '%" & ForwarderServiceLevel & "%'"
        '    End If
        '    If MinFreightRate <> Nothing And MinFreightRate <> "0" Then
        '        query += " AND MinFreightRate LIKE '%" & MinFreightRate & "%'"
        '    End If
        '    If FreightRate <> Nothing And FreightRate <> "0" Then
        '        query += " AND FreightRate LIKE '%" & FreightRate & "%'"
        '    End If
        '    If SecurityRate <> Nothing And SecurityRate <> "0" Then
        '        query += " AND SecurityRate LIKE '%" & SecurityRate & "%'"
        '    End If
        '    If OtherCharges <> Nothing Then
        '        query += " AND OtherCharges LIKE '%" & OtherCharges & "%'"
        '    End If
        '    If EffectiveDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), EffectiveDate, 101) = '" & EffectiveDate & "'"
        '    End If
        '    If ExpiryDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), ExpiryDate, 101) = '" & ExpiryDate & "'"
        '    End If
        '    If FreightForwarder <> Nothing Then
        '        query += " AND FreightForwarder LIKE '%" & FreightForwarder & "%'"
        '    End If
        '    If Notes <> Nothing Then
        '        query += " AND OriginComment LIKE '%" & Notes & "%'"
        '    End If
        '    If ApprovalDateFrom <> Nothing Then
        '        If ApprovalDateTo <> Nothing Then
        '            query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) BETWEEN '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "' AND '" & ApprovalDateTo.ToString("MM/dd/yyyy") & "'"
        '        Else
        '            query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) = '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "'"
        '        End If
        '    End If
        '    If ApprovedBy <> Nothing Then
        '        query += " AND Approvers.Name LIKE '%" & ApprovedBy & "%'"
        '    End If
        '    If RateRequestDate <> Nothing Then
        '        query += " AND CONVERT(VARCHAR(10), RequestDate, 101) = '" & RateRequestDate & "'"
        '    End If
        '    Return query
        'End Function

        Public Shared Function GetApprovedAsAdhocRateRequests(ByVal HAWBNumber As String, ByVal RateRequestID As String, ByVal Customer As String, ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String, ByVal OtherCharges As String, ByVal EffectiveDate As String, ByVal FreightForwarder As String, ByVal Notes As String, ByVal ApprovalDateFrom As String, ByVal ApprovalDateTo As String, ByVal ApprovedBy As String, ByVal RateRequestDate As String, ByVal EntryDate As String, ByVal IsLocalCurrency As Integer) As DataTable
            Dim query As String = "GetApprovedAsAdhocAirRateRequests"

            Try
                Dim param(29) As SqlParameter

                param(0) = New SqlParameter("@HAWBNumber", HAWBNumber)
                param(1) = New SqlParameter("@RateRequestID", RateRequestID)
                param(2) = New SqlParameter("@Customer", Customer)
                param(3) = New SqlParameter("@OriginAirport", OriginAirport)
                param(4) = New SqlParameter("@OriginRegion", OriginRegion)
                param(5) = New SqlParameter("@OriginCode ", OriginCode)
                param(6) = New SqlParameter("@DestAirport", DestAirport)
                param(7) = New SqlParameter("@DestCity", DestCity)
                param(8) = New SqlParameter("@DestState", DestState)
                param(9) = New SqlParameter("@DestCountry", DestCountry)
                param(10) = New SqlParameter("@DestRegion", DestRegion)
                param(11) = New SqlParameter("@DestZipcode", DestZipcode)
                param(12) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
                param(13) = New SqlParameter("@ShipMethod ", ShipMethod)
                param(14) = New SqlParameter("@ForwarderZipcode", ForwarderZipcode)
                param(15) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
                param(16) = New SqlParameter("@ForwarderServiceLevel", ForwarderServiceLevel)
                param(17) = New SqlParameter("@MinFreightRate", MinFreightRate)
                param(18) = New SqlParameter("@FreightRate", FreightRate)
                param(19) = New SqlParameter("@SecurityRate", SecurityRate)
                param(20) = New SqlParameter("@OtherCharges", OtherCharges)
                param(21) = New SqlParameter("@EffectiveDate", EffectiveDate)
                param(22) = New SqlParameter("@FreightForwarder", FreightForwarder)
                param(23) = New SqlParameter("@Notes", Notes)
                param(24) = New SqlParameter("@ApprovalDateFrom", ApprovalDateFrom)
                param(25) = New SqlParameter("@ApprovalDateTo", ApprovalDateTo)
                param(26) = New SqlParameter("@ApprovedBy", ApprovedBy)
                param(27) = New SqlParameter("@RateRequestDate", RateRequestDate)
                param(28) = New SqlParameter("@EntryDate", EntryDate)
                param(29) = New SqlParameter("@IsLocalCurrency", IsLocalCurrency)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetApprovedAsAdhocRateRequestRecordCount(ByVal query As String) As Integer
            Try
                Try
                    query = query.Replace(query.Substring(0, query.IndexOf("FROM ")), "SELECT COUNT(*) ")
                    Using DB As New DBClass(query)
                        Return CInt(DB.ExecuteScalar)
                    End Using
                Catch ex As Exception
                    query = "SELECT COUNT(*) FROM AirRateRequests WHERE IsAdhoc = 1"
                    Using DB As New DBClass(query)
                        Return CInt(DB.ExecuteScalar)
                    End Using
                End Try
            Catch ex As Exception
            End Try
        End Function

        Public Shared Function GetDistinctValues(ByVal query As String, ByVal fieldName As String) As DataTable
            Try
                Try
                    query = query.Replace(query.Substring(0, query.IndexOf("FROM ")), "SELECT DISTINCT " & fieldName & " ")
                    Using DB As New DBClass(query)
                        Return DB.GetDataTable
                    End Using
                Catch ex As Exception
                    query = "SELECT DISTINCT " & fieldName & " FROM AirRateRequests WHERE IsAdhoc = 1"
                    Using DB As New DBClass(query)
                        Return DB.GetDataTable
                    End Using
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function RemoveAllComments(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAllAirRateRequestComments"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        'Public Shared Function UpdateEffectiveAndExpiryDates(ByVal RateRequestID As Integer, ByVal EffectiveDate As Date, ByVal ExpiryDate As Date) As Integer
        '    'Validate Inputs
        '    If RateRequestID <= 0 Then Return 0

        '    'Post New Rate Request if valid
        '    Dim query As String = "UpdateEffectiveAndExpiryDates"
        '    Dim updateResult As Integer

        '    Dim param(2) As SqlParameter

        '    param(0) = New SqlParameter("@RateRequestID", RateRequestID)
        '    param(1) = New SqlParameter("@EffectiveDate", If(EffectiveDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, EffectiveDate, SqlTypes.SqlDateTime.Null))
        '    param(2) = New SqlParameter("@ExpiryDate", If(ExpiryDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, ExpiryDate, SqlTypes.SqlDateTime.Null))

        '    Try
        '        Using DB As New DBClass(query, True, param)
        '            updateResult = CInt(DB.ExecuteScalar())

        '            Return updateResult
        '        End Using
        '    Catch
        '        Throw
        '    End Try
        'End Function

        Public Shared Function GetRateRequestByDetails(ByVal ConsigneeName As String, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double) As DataTable
            Dim query As String = "GetAirRateRequestByDetails"

            Dim param(8) As SqlParameter

            param(0) = New SqlParameter("@ConsigneeName", DBStrValue(ConsigneeName))
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(5) = New SqlParameter("@ShipMethod", DBStrValue(ShipMethod))
            param(6) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(7) = New SqlParameter("@FreightRate", FreightRate)
            param(8) = New SqlParameter("@SecurityRate", SecurityRate)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAdhocRateRequestByDetails(ByVal ConsigneeName As String, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal SecurityRate As Double) As DataTable
            Dim query As String = "GetAdhocAirRateRequestByDetails"

            Dim param(8) As SqlParameter

            param(0) = New SqlParameter("@ConsigneeName", DBStrValue(ConsigneeName))
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@CEVATransitMode", DBStrValue(CEVATransitMode))
            param(5) = New SqlParameter("@ShipMethod", DBStrValue(ShipMethod))
            param(6) = New SqlParameter("@MinFreightRate", CDbl(MinFreightRate).ToString("0.00"))
            param(7) = New SqlParameter("@FreightRate", CDbl(FreightRate).ToString("0.00"))
            param(8) = New SqlParameter("@SecurityRate", CDbl(SecurityRate).ToString("0.00"))

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarRateRequests(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestCountry As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarRateRequests"

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@DestCountry", DBStrValue(DestCountry))
            param(5) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(6) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarAdhocLanes(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestCountry As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarAdhocLanes"

            Dim param(6) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@DestCountry", DBStrValue(DestCountry))
            param(5) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(6) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function FinalizeAirRateRequestBySystem() As Integer
            Dim query As String = "FinalizeAirRateRequestBySystem_15july"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetNeedToOperateRequests() As DataTable
            Dim query As String = "GetNeedToOperateAirRequests"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateNeedToOperateRequestsForMailOut() As Integer
            Dim query As String = "UpdateNeedToOperateAirRequestsForMailOut"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateNeedToTransferRequestsForMailOut() As Integer
            Dim query As String = "UpdateNeedToTransferRequestsForMailOut"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetNeedToTransferRequests() As DataTable
            Dim query As String = "GetNeedToTransferAirRequests"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ClearRejectedTag(ByVal rateRequestID As Integer) As Integer
            Dim query As String = "ClearAirRejectedTag"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", rateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function IsRateRequestGenerated(ByVal rateRequestID As Integer) As Integer
            Dim query As String = "IsAirRateRequestGenerated"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", rateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewRateRequest_15July(ByVal RequestorID As Integer, ByVal RateRequestDate As DateTime, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date,
                                                         ByVal ServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String,
                                                         ByVal Currency As String, ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer

            'Validate Inputs
            If RequestorID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "PostNewAirRateRequest_15july"
            Dim newRateRequestID As Integer

            Dim param(26) As SqlParameter

            param(0) = New SqlParameter("@RequestorID", RequestorID)
            param(1) = New SqlParameter("@RateRequestDate", RateRequestDate)
            param(2) = New SqlParameter("@HAWBNumber", DBStrValue(HAWBNumber))
            param(3) = New SqlParameter("@ShipMethod", ShipMethod)
            param(4) = New SqlParameter("@ShipDate", If(ShipDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, ShipDate, SqlTypes.SqlDateTime.Null))
            param(5) = New SqlParameter("@ServiceLevel", ServiceLevel)
            param(6) = New SqlParameter("@Weight", If(Weight > 0, Weight, Nothing))
            param(7) = New SqlParameter("@ShipperName", DBStrValue(ShipperName))
            param(8) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(9) = New SqlParameter("@OriginCity", DBStrValue(OriginCity))
            param(10) = New SqlParameter("@OriginRegion", DBStrValue(OriginRegion))
            param(11) = New SqlParameter("@OriginZipcode", DBStrValue(OriginZipcode))
            param(12) = New SqlParameter("@ConsigneeName", DBStrValue(Consignee))
            param(13) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(14) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(15) = New SqlParameter("@DestRegion", DBStrValue(DestRegion))
            param(16) = New SqlParameter("@DestZipcode", DBStrValue(DestZipcode))
            param(17) = New SqlParameter("@RateDeterMethod", DBStrValue(RateDeterMethod))
            param(18) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(19) = New SqlParameter("@FreightRate", FreightRate)
            param(20) = New SqlParameter("@OtherCharges", DBStrValue(OtherCharges))
            param(21) = New SqlParameter("@OriginComment", DBStrValue(OriginComment))
            param(22) = New SqlParameter("@OriginEuropeanZones", DBStrValue(OriginEuropeanZones))
            param(23) = New SqlParameter("@DestinitionEuropeanZones", DBStrValue(DestinitionEuropeanZones))
            param(24) = New SqlParameter("@Currency", DBStrValue(Currency))
            param(25) = New SqlParameter("@TransportMode", DBStrValue(TransportMode))
            param(26) = New SqlParameter("@GrossAddOnFees", GrossAddOnFees)

            Try
                Using DB As New DBClass(query, True, param)
                    newRateRequestID = CInt(DB.ExecuteScalar())
                End Using

                Return newRateRequestID
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateRateRequest_15july(ByVal RateRequestID As Integer, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String, ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer

            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "UpdateAirRateRequest_15july"
            Dim updateResult As Integer

            Dim param(25) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@HAWBNumber", DBStrValue(HAWBNumber))
            param(2) = New SqlParameter("@ShipMethod", ShipMethod)
            param(3) = New SqlParameter("@ShipDate", If(ShipDate <> Data.SqlTypes.SqlDateTime.MinValue.Value, ShipDate, SqlTypes.SqlDateTime.Null))
            param(4) = New SqlParameter("@ServiceLevel", ServiceLevel)
            param(5) = New SqlParameter("@Weight", If(Weight > 0, Weight, Nothing))
            param(6) = New SqlParameter("@ShipperName", DBStrValue(ShipperName))
            param(7) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(8) = New SqlParameter("@OriginCity", DBStrValue(OriginCity))
            param(9) = New SqlParameter("@OriginRegion", DBStrValue(OriginRegion))
            param(10) = New SqlParameter("@OriginZipcode", DBStrValue(OriginZipcode))
            param(11) = New SqlParameter("@ConsigneeName", DBStrValue(Consignee))
            param(12) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(13) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(14) = New SqlParameter("@DestRegion", DBStrValue(DestRegion))
            param(15) = New SqlParameter("@DestZipcode", DBStrValue(DestZipcode))
            param(16) = New SqlParameter("@RateDeterMethod", DBStrValue(RateDeterMethod))
            param(17) = New SqlParameter("@MinFreightRate", MinFreightRate)
            param(18) = New SqlParameter("@FreightRate", FreightRate)
            param(19) = New SqlParameter("@OtherCharges", DBStrValue(OtherCharges))
            param(20) = New SqlParameter("@OriginComment", DBStrValue(OriginComment))
            param(21) = New SqlParameter("@OriginEuropeanZones", DBStrValue(OriginEuropeanZones))
            param(22) = New SqlParameter("@DestinitionEuropeanZones", DBStrValue(DestinitionEuropeanZones))
            param(23) = New SqlParameter("@Currency", DBStrValue(Currency))
            param(24) = New SqlParameter("@TransportMode", DBStrValue(TransportMode))
            param(25) = New SqlParameter("@GrossAddOnFees", GrossAddOnFees)

            Try
                Using DB As New DBClass(query, True, param)
                    updateResult = CInt(DB.ExecuteScalar())
                End Using

                Return updateResult
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestByID_15july(ByVal RateRequestID As Integer) As DataRow
            Dim query As String = "GetAirRateRequestByID_15july"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataRow()
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetRateRequestByIDTable_15july(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetAirRateRequestByID_15july"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable()
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarRateRequests_15july(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarRateRequests_15july"

            Dim param(4) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarAdhocLanes_15july(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarAdhocLanes_15july"

            Dim param(4) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAllComments_15july(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAllAirRateRequestComments_15july"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRateRequest_15july(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveAirRateRequest_15july"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ApproveRateRequestByClient_15july(ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal ApprovalDate As Date, ByVal comment As String, ByVal isAdhoc As Boolean) As Integer
            If RateRequestID <= 0 Or ApproverID <= 0 Then Exit Function

            Dim query As String = "ApproveAirRateRequestByClient_15july"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApproverID", ApproverID)
            param(2) = New SqlParameter("@Comment", comment)
            param(3) = New SqlParameter("@IsAdhoc", isAdhoc)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ClearRejectedTag_15july(ByVal rateRequestID As Integer) As Integer
            Dim query As String = "ClearAirRejectedTag_15july"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", rateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return Convert.ToInt16(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RejectRateRequestByClient_15july(ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RejectedDate As Date) As Integer
            If RateRequestID <= 0 Or RejectorID <= 0 Then Exit Function

            Dim query As String = "RejectAirRateRequestByClient_15july"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RejectorID", RejectorID)
            param(2) = New SqlParameter("@RejectedDate", RejectedDate)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RevokeRateRequest_15july(ByVal rateRequestID As Integer, ByVal rateRequestHolderID As Integer, ByVal comment As String) As Integer
            Dim query As String = "RevokeAirRateRequest_15july"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", rateRequestID)
            param(1) = New SqlParameter("@RateRequestHolderID", rateRequestHolderID)
            param(2) = New SqlParameter("@Comment", comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function HasNewAirRateRequestAccess(ByVal userid As String) As Boolean
            Dim ModId As String = IIf(CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.LIVE, 29, 28).ToString()
            Dim str As String = "select * from usermodulesmapping where moduleid=" + ModId + " and userid=@UserID"
            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@UserID", userid)
            Using DB As New DBClass(str, param)
                Return CBool((DB.GetDataTable()).Rows.Count > 0)
            End Using
        End Function

        Public Shared Function GetRateRequestHistory(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetAirRateRequestHistory"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAirTariffRates(Optional ByVal RateRequestID As Integer = 0) As DataTable
            Dim query As String = "GetAirTariffRates"

            Dim param1 As New SqlParameter("@RateRequestID", IIf(RateRequestID = 0, DBNull.Value, RateRequestID))

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable()
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAirAddOnCharges(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetAirAddOnCharges"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable()
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function BreakDownChargesStatus(ByVal UserID As String) As String
            Dim query As String = "CanUserViewBreakDownCharges"

            Dim param1 As New SqlParameter("@UserID", UserID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.ExecuteScalar().ToString()
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewAirTariffRates(ByVal RateRequestID As Integer, ByVal RateId As Integer, ByVal MinValue As Double, ByVal PerKG As Double) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "PostNewAirTariffRates"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateID", RateId)
            param(2) = New SqlParameter("@MinValue", MinValue)
            param(3) = New SqlParameter("@PerKG", PerKG)
            

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewAddOnRates(ByVal RateRequestID As Integer, ByVal Description As String, ByVal UOM As String, ByVal Value As Double) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "AirAddOnRatesUpdation"

            Dim param(4) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@Description", Description)
            param(2) = New SqlParameter("@UOM", UOM)
            param(3) = New SqlParameter("@Value", Value)
            param(4) = New SqlParameter("@CheckFlag", 1)


            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateAddOnRates(ByVal ID As Integer, ByVal RateRequestID As Integer, ByVal Description As String, ByVal UOM As String, ByVal Value As Double) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "AirAddOnRatesUpdation"

            Dim param(5) As SqlParameter

            param(0) = New SqlParameter("@Id", ID)
            param(1) = New SqlParameter("@RateRequestID", RateRequestID)
            param(2) = New SqlParameter("@Description", Description)
            param(3) = New SqlParameter("@UOM", UOM)
            param(4) = New SqlParameter("@Value", Value)
            param(5) = New SqlParameter("@CheckFlag", 2)


            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function DeleteAddOnRates(ByVal ID As Integer) As Integer
            'Validate inputs for business logics
            'No logic to validate

            'Call DAL function if valid
            Dim query As String = "AirAddOnRatesUpdation"

            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@Id", ID)
            param(1) = New SqlParameter("@CheckFlag", 3)


            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function UpdateRateRequestTariff(ByVal RateRequestID As Integer, ByVal ApproverId As Integer, ByVal StatusId As Integer) As Integer

            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "UpdateTariffRateRequestMaster"
            Dim updateResult As Integer

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApproverId", ApproverId)
            param(2) = New SqlParameter("@StatusId", StatusId)

            Try
                Using DB As New DBClass(query, True, param)
                    updateResult = CInt(DB.ExecuteScalar())
                End Using

                Return updateResult
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarRateRequestsRightPanel(ByVal RateRequestId As Integer, ByVal IsApproved As Integer) As DataTable
            Dim query As String = "GetSimilarRateRequestQuery"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@RateRequestId", RateRequestId)
            param(1) = New SqlParameter("@IsApproved", IsApproved)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetTariffRateRequestHistory(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetTariffQuery_History"

            Dim param1 As New SqlParameter("@RateRequestId", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace