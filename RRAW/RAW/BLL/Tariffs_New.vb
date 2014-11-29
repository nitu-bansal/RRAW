Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Tariffs_New

        Private Sub New()
        End Sub

        Public Shared Function GetTariff() As DataTable
            Dim query As String = "GetTariff"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetExistingLanes(ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestCountry As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String) As DataTable
            Dim query As String = "GetExistingLanes"

            Dim param(5) As SqlParameter
            param(0) = New SqlParameter("@OriginAirport", If(OriginAirport <> Nothing, OriginAirport, "NULL"))
            param(1) = New SqlParameter("@DestAirport", If(DestAirport <> Nothing, DestAirport, "NULL"))
            param(2) = New SqlParameter("@DestCity", If(DestCity <> Nothing, DestCity, "NULL"))
            param(3) = New SqlParameter("@DestCountry", If(DestCountry <> Nothing, DestCountry, "NULL"))
            param(4) = New SqlParameter("@DestZipcode", If(DestZipcode <> Nothing, DestZipcode, "NULL"))
            param(5) = New SqlParameter("@CEVATransitMode", CEVATransitMode)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

       
        Public Shared Function GetTariffFromDB(ByVal TransportId As Integer, ByVal ClientId As Integer, ByVal GroupId As Integer) As DataTable
            Dim query As String = "GetTariffQuery"

            Try
                Dim param(2) As SqlParameter
                param(0) = New SqlParameter("@TransportId", TransportId)
                param(1) = New SqlParameter("@ClientId", ClientId)
                param(2) = New SqlParameter("@GroupId", GroupId)
                

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAirTariff(ByVal ReportItemIndex As String, ByVal OriginRegion As String, ByVal OriginAirportCode As String, ByVal DestinationRegion As String, ByVal Destination As String, ByVal DestinationAirportCode As String, ByVal DestinationStation As String, ByVal ServiceLevel As String, ByVal ServiceType As String, ByVal Currency As String, ByVal MinFreightRate As String, ByVal FreighRatePerKG As String) As DataTable
            Return GetAirTariffFromDB(ReportItemIndex, OriginRegion, OriginAirportCode, DestinationRegion, Destination, DestinationAirportCode, DestinationStation, ServiceLevel, ServiceType, Currency, MinFreightRate, FreighRatePerKG)
        End Function


        Private Shared Function GetAirTariffFromDB(ByVal ReportItemIndex As String, ByVal OriginRegion As String, ByVal OriginAirportCode As String, ByVal DestinationRegion As String, ByVal Destination As String, ByVal DestinationAirportCode As String, ByVal DestinationStation As String, ByVal ServiceLevel As String, ByVal ServiceType As String, ByVal Currency As String, ByVal MinFreightRate As String, ByVal FreighRatePerKG As String) As DataTable
            Dim query As String = "GetAirTariffQuery_14Jul"

            Try
                Dim param(11) As SqlParameter

                ' param(0) = New SqlParameter("@FreightCompany", FreightCompany)
                param(0) = New SqlParameter("@ReportItemIndex", ReportItemIndex)
                param(1) = New SqlParameter("@OriginRegion", OriginRegion)
                param(2) = New SqlParameter("@OriginAirportCode", OriginAirportCode)
                param(3) = New SqlParameter("@DestinationRegion", DestinationRegion)
                param(4) = New SqlParameter("@Destination", Destination)
                param(5) = New SqlParameter("@DestinationAirportCode", DestinationAirportCode)
                param(6) = New SqlParameter("@DestinationStation", DestinationStation)
                param(7) = New SqlParameter("@ServiceLevel", ServiceLevel)
                param(8) = New SqlParameter("@ServiceType", ServiceType)
                param(9) = New SqlParameter("@Currency", Currency)
                param(10) = New SqlParameter("@MinFreightRate", MinFreightRate)
                param(11) = New SqlParameter("@FreightRatePerKG", FreighRatePerKG)


                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetTariffRecordCount(ByVal query As String) As Integer
            Try
                Try
                    query = query.Replace(query.Substring(0, query.IndexOf("FROM ")), "SELECT COUNT(*) ")
                    Using DB As New DBClass(query)
                        Return CInt(DB.ExecuteScalar)
                    End Using
                Catch ex As Exception
                    query = "SELECT COUNT(*) FROM Tariff"
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
                    query = "SELECT DISTINCT " & fieldName & " FROM Tariff"
                    Using DB As New DBClass(query)
                        Return DB.GetDataTable
                    End Using
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function RateRequestToTariff(ByVal RateRequestID As Integer, ByVal ApprovalDate As DateTime, ByVal TransferDate As DateTime) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "RateRequestToTariff"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApprovalDate", ApprovalDate)
            param(2) = New SqlParameter("@TransferDate", TransferDate)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function ConvertToWDTariff(ByVal query As String) As String
            Return query.Replace(query.Substring(query.IndexOf("SELECT"), query.IndexOf("FROM")), "SELECT ID AS 'Index', Customer AS 'Customer Name', OriginAirport AS 'Origin Airport Code', OriginRegion AS 'Origin Region', DestCity AS 'Destination City', DestState AS 'Destination  State          If Applicable', DestCountry AS 'Destination Country Code', DestZipCode AS 'Destination Postal (ZIP) Code', DestRegion AS 'Destination Region', CEVATransitMode AS 'CEVA Mode Of Transit', ShipMethod AS 'WD Ship Method', DestAirport AS 'Destination Airpot', ForwarderZipCode AS 'Forwarder Delivery City or Postal Code', CustomClearanceMode AS 'With or Without Customs Clearance', NULL AS 'Default Service', ForwarderServiceLevel AS 'Forwarder Service', CASE Active WHEN '1' THEN 'YES' ELSE 'NO' END AS 'Active', CONVERT(VARCHAR(10), EffectiveDate, 101) AS 'Effective Date', CONVERT(VARCHAR(10), ExpiryDate, 101) AS 'Expiry Date', NULL AS 'Ocean 20''', NULL AS 'Ocean 40''', NULL AS 'Ocean 40''HC', NULL AS 'Original Tariff Rate', NULL AS 'Per Kilo Discount Achieved through Cost Savings or Trans-Ship Program', NULL AS 'Adjusted Rate (Equals Y - Z) Applicable now for Trans-ship and HKG exports but after Oct 1 discount from KUL and BKK', MinFreightRate AS 'Valid Minimum Charge', NULL AS 'Valid Int''l Per Kilo Rate Effective till Dec 31 #### (Last Year)', FreightRate AS 'USD Valid Int''l Per Kilo Rate Effective January 1 #### (Current Year)', SecurityRate AS 'USD Security Charge Effective January 1 #### (Current Year)', NULL AS 'Currency Exchange Factor', NULL AS 'THB / MYR Minimum Charges', NULL AS 'THB / MYR Valid Int''l Per Kilo Rate Effective January 1 #### (Current Year)', NULL AS 'THB / MYR Security Charge Effective January 1 #### (Current Year)', NULL AS 'Per Pound', NULL AS 'Country or US Domestic Zone', NULL AS 'USA Truckload', Notes, CONVERT(VARCHAR(10), ApprovalDate, 101) AS 'Approval Date', ApprovedBy AS 'Approved By', ApprovalNotes AS 'Approval Notes', AdditionalNotes AS 'Additional Notes'")
        End Function

        Public Shared Function GetTariffByDetails(ByVal Customer As String, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String) As DataTable
            Dim query As String = "GetTariffByDetails"

            Dim param(8) As SqlParameter

            param(0) = New SqlParameter("@Customer", Customer)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(5) = New SqlParameter("@ShipMethod", ShipMethod)
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

        Public Shared Function GetSimilarTariffLanes(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarTariffLanes"

            Dim param(5) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@DestCity", DBStrValue(DestCity))
            param(4) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
            param(5) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetOceanTariff(ByVal FreightCompany As String, ByVal ReportItemIndex As String, ByVal OriginRegion As String, ByVal OriginAirportCode As String, ByVal DestinationRegion As String, ByVal DestinationAirportCode As String, ByVal ServiceLevel As String, ByVal ServiceType As String, ByVal Currency As String, ByVal MinFreight As String, ByVal FreightRate As String, ByVal Lines As String, ByVal EffectiveDate As String, ByVal ExpiryDate As String, ByVal Comments As String, ByVal Requestor As String, ByVal RequestDate As String, ByVal ApprovalDateFrom As String, ByVal ApprovalDateTo As String, ByVal Approver As String, ByVal EntryTimeStamp As String) As DataTable
            Dim query As String = "GetOceanTariffQuery_14Jul"

            Try
                Dim param(20) As SqlParameter

                param(0) = New SqlParameter("@FreightCompany", FreightCompany)
                param(1) = New SqlParameter("@ReportItemIndex", ReportItemIndex)
                param(2) = New SqlParameter("@OriginRegion", OriginRegion)
                param(3) = New SqlParameter("@OriginAirportCode", OriginAirportCode)
                param(4) = New SqlParameter("@DestinationRegion", DestinationRegion)
                param(5) = New SqlParameter("@DestinationAirportCode", DestinationAirportCode)
                param(6) = New SqlParameter("@ServiceLevel", ServiceLevel)
                param(7) = New SqlParameter("@ServiceType", ServiceType)
                param(8) = New SqlParameter("@Currency", Currency)
                param(9) = New SqlParameter("@MinFreight", MinFreight)
                param(10) = New SqlParameter("@FreightRate", FreightRate)
                param(11) = New SqlParameter("@Lines", Lines)
                param(12) = New SqlParameter("@EffectiveDate", EffectiveDate)
                param(13) = New SqlParameter("@ExpiryDate", ExpiryDate)
                param(14) = New SqlParameter("@Comments", Comments)
                param(15) = New SqlParameter("@Requestor", Requestor)
                param(16) = New SqlParameter("@RequestDate", RequestDate)
                param(17) = New SqlParameter("@ApprovalDateFrom", ApprovalDateFrom)
                param(18) = New SqlParameter("@ApprovalDateTo", ApprovalDateTo)
                param(19) = New SqlParameter("@Approver", Approver)

                param(20) = New SqlParameter("@EntryTimeStamp", EntryTimeStamp)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function GetSearchstringColumns(ByVal SearchStr As String) As DataTable
            Dim query As String = "GetSearchstringColumns"
            Try
                Dim param(0) As SqlParameter
                param(0) = New SqlParameter("@SearchStr", SearchStr)
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetTariffGlobally(ByVal SearchStr As String) As DataTable
            Dim query As String = "GetTariffGlobalQuery"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@SearchStr", SearchStr)


            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace