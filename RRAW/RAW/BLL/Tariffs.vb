Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Tariffs

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

        Public Shared Function GetTariffQuery(ByVal Customer As String, ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String, ByVal OtherCharges As String, ByVal EffectiveDate As Date, ByVal ExpiryDate As Date, ByVal FreightForwarder As String, ByVal Active As Integer, ByVal Notes As String, ByVal ApprovalDateFrom As Date, ByVal ApprovalDateTo As Date, ByVal ApprovedBy As String, ByVal ApprovalNotes As String, ByVal AdditionalNotes As String, ByVal RateRequestDate As Date, ByVal EntryDate As Date) As String
            Dim query As String = "SELECT " & _
                                    "Customer, " & _
                                    "OriginAirport AS 'Origin Airport', " & _
                                    "OriginRegion AS 'Origin Region', " & _
                                    "OriginCode AS 'Origin Code', " & _
                                    "DestAirport AS 'Dest Airport', " & _
                                    "DestCity AS 'Dest City', " & _
                                    "DestState AS 'Dest State', " & _
                                    "DestCountry AS 'Dest Country', " & _
                                    "DestRegion AS 'Dest Region', " & _
                                    "DestZipcode AS 'Dest Zipcode', " & _
                                    "CEVATransitMode AS 'CEVA Transit Mode', " & _
                                    "ShipMethod AS 'Ship Method', " & _
                                    "ForwarderZipcode AS 'Forwarder Zipcode', " & _
                                    "CustomClearanceMode AS 'Custom Clearance Mode', " & _
                                    "ForwarderServiceLevel AS 'Forwarder Service Level', " & _
                                    "CONVERT(VARCHAR(20), MinFreightRate, 1) AS 'Min Freight Rate', " & _
                                    "CONVERT(VARCHAR(20), FreightRate, 1) AS 'Freight Rate', " & _
                                    "CONVERT(VARCHAR(20), SecurityRate, 1) AS 'Security Rate', " & _
                                    "OtherCharges AS 'Other Charges', " & _
                                    "CONVERT(VARCHAR(10), EffectiveDate, 101) AS 'Effective Date', " & _
                                    "CONVERT(VARCHAR(10), ExpiryDate, 101) AS 'Expiry Date', " & _
                                    "FreightForwarder AS 'Freight Forwarder', " & _
                                    "Active, " & _
                                    "Notes, " & _
                                    "CONVERT(VARCHAR(10), ApprovalDate, 101) AS 'Approval Date', " & _
                                    "ApprovedBy AS 'Approved By', " & _
                                    "ApprovalNotes AS 'Approval Notes', " & _
                                    "AdditionalNotes AS 'Additional Notes', " & _
                                    "CONVERT(VARCHAR(10), RateRequestDate, 101) AS 'Rate Request Date', " & _
                                    "CONVERT(VARCHAR(10), tstamp, 101) AS 'Entry Date' " & _
                                "FROM " & _
                                    "Tariff " & _
                                "WHERE " & _
                                    "1 = 1"
            If Customer <> Nothing Then
                query += " AND Customer LIKE '%" & Customer & "%'"
            End If
            If OriginAirport <> Nothing Then
                query += " AND OriginAirport LIKE '%" & OriginAirport & "%'"
            End If
            If OriginRegion <> Nothing Then
                query += " AND OriginRegion LIKE '%" & OriginRegion & "%'"
            End If
            If OriginCode <> Nothing Then
                query += " AND OriginCode LIKE '%" & OriginCode & "%'"
            End If
            If DestAirport <> Nothing Then
                query += " AND DestAirport LIKE '%" & DestAirport & "%'"
            End If
            If DestCity <> Nothing Then
                query += " AND DestCity LIKE '%" & DestCity & "%'"
            End If
            If DestState <> Nothing Then
                query += " AND DestState LIKE '%" & DestState & "%'"
            End If
            If DestCountry <> Nothing Then
                query += " AND DestCountry LIKE '%" & DestCountry & "%'"
            End If
            If DestRegion <> Nothing Then
                query += " AND DestRegion LIKE '%" & DestRegion & "%'"
            End If
            If DestZipcode <> Nothing Then
                query += " AND DestZipcode LIKE '%" & DestZipcode & "%'"
            End If
            If CEVATransitMode <> Nothing Then
                query += " AND CEVATransitMode LIKE '%" & CEVATransitMode & "%'"
            End If
            If ShipMethod <> Nothing Then
                query += " AND ShipMethod LIKE '%" & ShipMethod & "%'"
            End If
            If ForwarderZipcode <> Nothing Then
                query += " AND ForwarderZipcode LIKE '%" & ForwarderZipcode & "%'"
            End If
            If CustomClearanceMode <> Nothing Then
                query += " AND CustomClearanceMode LIKE '%" & CustomClearanceMode & "%'"
            End If
            If ForwarderServiceLevel <> Nothing Then
                query += " AND ForwarderServiceLevel LIKE '%" & ForwarderServiceLevel & "%'"
            End If
            If MinFreightRate <> Nothing Then
                query += " AND MinFreightRate LIKE '%" & MinFreightRate & "%'"
            End If
            If FreightRate <> Nothing Then
                query += " AND FreightRate LIKE '%" & FreightRate & "%'"
            End If
            If SecurityRate <> Nothing Then
                query += " AND SecurityRate LIKE '%" & SecurityRate & "%'"
            End If
            If OtherCharges <> Nothing Then
                query += " AND OtherCharges LIKE '%" & OtherCharges & "%'"
            End If
            If EffectiveDate <> Nothing Then
                query += " AND CONVERT(VARCHAR(10), EffectiveDate, 101) = '" & EffectiveDate.ToString("MM/dd/yyyy") & "'"
            End If
            If ExpiryDate <> Nothing Then
                query += " AND CONVERT(VARCHAR(10), ExpiryDate, 101) = '" & ExpiryDate.ToString("MM/dd/yyyy") & "'"
            End If
            If FreightForwarder <> Nothing Then
                query += " AND FreightForwarder LIKE '%" & FreightForwarder & "%'"
            End If
            If Active >= 0 Then
                query += " AND Active = " & Active
            End If
            If Notes <> Nothing Then
                query += " AND Notes LIKE '%" & Notes & "%'"
            End If
            If ApprovalDateFrom <> Nothing Then
                If ApprovalDateTo <> Nothing Then
                    query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) BETWEEN '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "' AND '" & ApprovalDateTo.ToString("MM/dd/yyyy") & "'"
                Else
                    query += " AND CONVERT(VARCHAR(10), ApprovalDate, 101) = '" & ApprovalDateFrom.ToString("MM/dd/yyyy") & "'"
                End If
            End If
            If ApprovedBy <> Nothing Then
                query += " AND ApprovedBy LIKE '%" & ApprovedBy & "%'"
            End If
            If ApprovalNotes <> Nothing Then
                query += " AND ApprovalNotes LIKE '%" & ApprovalNotes & "%'"
            End If
            If AdditionalNotes <> Nothing Then
                query += " AND AdditionalNotes LIKE '%" & AdditionalNotes & "%'"
            End If
            If RateRequestDate <> Nothing Then
                query += " AND CONVERT(VARCHAR(10), RateRequestDate, 101) = '" & RateRequestDate.ToString("MM/dd/yyyy") & "'"
            End If
            If EntryDate <> Nothing Then
                query += " AND CONVERT(VARCHAR(10), tstamp, 101) = '" & EntryDate.ToString("MM/dd/yyyy") & "'"
            End If
            Return query
        End Function

        Public Shared Function GetAirTariff(ByVal Customer As String, ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String, ByVal OtherCharges As String, ByVal EffectiveDate As String, ByVal ExpiryDate As String, ByVal FreightForwarder As String, ByVal Active As Integer, ByVal Notes As String, ByVal ApprovalDateFrom As String, ByVal ApprovalDateTo As String, ByVal ApprovedBy As String, ByVal ApprovalNotes As String, ByVal AdditionalNotes As String, ByVal RateRequestDate As String, ByVal EntryDate As String, ByVal IsLocalCurrency As Integer) As DataTable
            If Customer = "" And OriginAirport = "" And OriginRegion = "" And OriginCode = "" And DestAirport = "" And DestCity = "" And DestState = "" And DestCountry = "" And DestRegion = "" And DestZipcode = "" And CEVATransitMode = "" And ShipMethod = "" And ForwarderZipcode = "" And CustomClearanceMode = "" And ForwarderServiceLevel = "" And MinFreightRate = "" And FreightRate = "" And SecurityRate = "" And OtherCharges = "" And EffectiveDate = "" And ExpiryDate = "" And FreightForwarder = "" And Active = -1 And Notes = "" And ApprovalDateFrom = "" And ApprovalDateTo = "" And ApprovedBy = "" And ApprovalNotes = "" And AdditionalNotes = "" And RateRequestDate = "" And EntryDate = "" Then
                Dim cacheItemName As String = "AirTariff"
                Dim tableName As String = "Tariff"

                'Dim res As DataTable = GetCachedDataTable(cacheItemName)
                'If res Is Nothing Then
                Return GetAirTariffFromDB(Customer, OriginAirport, OriginRegion, OriginCode, DestAirport, DestCity, DestState, DestCountry, DestRegion, DestZipcode, CEVATransitMode, ShipMethod, ForwarderZipcode, CustomClearanceMode, ForwarderServiceLevel, MinFreightRate, FreightRate, SecurityRate, OtherCharges, EffectiveDate, ExpiryDate, FreightForwarder, Active, Notes, ApprovalDateFrom, ApprovalDateTo, ApprovedBy, ApprovalNotes, AdditionalNotes, RateRequestDate, EntryDate, IsLocalCurrency)

                'CacheDataTable(cacheItemName, tableName, res)
                'End If

                'Return res
            Else
                Return GetAirTariffFromDB(Customer, OriginAirport, OriginRegion, OriginCode, DestAirport, DestCity, DestState, DestCountry, DestRegion, DestZipcode, CEVATransitMode, ShipMethod, ForwarderZipcode, CustomClearanceMode, ForwarderServiceLevel, MinFreightRate, FreightRate, SecurityRate, OtherCharges, EffectiveDate, ExpiryDate, FreightForwarder, Active, Notes, ApprovalDateFrom, ApprovalDateTo, ApprovedBy, ApprovalNotes, AdditionalNotes, RateRequestDate, EntryDate, IsLocalCurrency)
            End If
        End Function

        Private Shared Function GetAirTariffFromDB(ByVal Customer As String, ByVal OriginAirport As String, ByVal OriginRegion As String, ByVal OriginCode As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestState As String, ByVal DestCountry As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal CEVATransitMode As String, ByVal ShipMethod As String, ByVal ForwarderZipcode As String, ByVal CustomClearanceMode As String, ByVal ForwarderServiceLevel As String, ByVal MinFreightRate As String, ByVal FreightRate As String, ByVal SecurityRate As String, ByVal OtherCharges As String, ByVal EffectiveDate As String, ByVal ExpiryDate As String, ByVal FreightForwarder As String, ByVal Active As Integer, ByVal Notes As String, ByVal ApprovalDateFrom As String, ByVal ApprovalDateTo As String, ByVal ApprovedBy As String, ByVal ApprovalNotes As String, ByVal AdditionalNotes As String, ByVal RateRequestDate As String, ByVal EntryDate As String, ByVal IsLocalCurrency As Integer) As DataTable
            Dim query As String = "GetAirTariffQuery"

            Try
                Dim param(31) As SqlParameter

                param(0) = New SqlParameter("@Customer", Customer)
                param(1) = New SqlParameter("@OriginAirport", OriginAirport)
                param(2) = New SqlParameter("@OriginRegion", OriginRegion)
                param(3) = New SqlParameter("@OriginCode", OriginCode)
                param(4) = New SqlParameter("@DestAirport", DestAirport)
                param(5) = New SqlParameter("@DestCity", DestCity)
                param(6) = New SqlParameter("@DestState", DestState)
                param(7) = New SqlParameter("@DestCountry", DestCountry)
                param(8) = New SqlParameter("@DestRegion", DestRegion)
                param(9) = New SqlParameter("@DestZipcode", DestZipcode)
                param(10) = New SqlParameter("@CEVATransitMode", CEVATransitMode)
                param(11) = New SqlParameter("@ShipMethod", ShipMethod)
                param(12) = New SqlParameter("@ForwarderZipcode", ForwarderZipcode)
                param(13) = New SqlParameter("@CustomClearanceMode", CustomClearanceMode)
                param(14) = New SqlParameter("@ForwarderServiceLevel", ForwarderServiceLevel)
                param(15) = New SqlParameter("@MinFreightRate", MinFreightRate)
                param(16) = New SqlParameter("@FreightRate", FreightRate)
                param(17) = New SqlParameter("@SecurityRate", SecurityRate)
                param(18) = New SqlParameter("@OtherCharges", OtherCharges)
                param(19) = New SqlParameter("@EffectiveDate", EffectiveDate)
                param(20) = New SqlParameter("@ExpiryDate", ExpiryDate)
                param(21) = New SqlParameter("@FreightForwarder", FreightForwarder)
                param(22) = New SqlParameter("@Active", Active)
                param(23) = New SqlParameter("@Notes", Notes)
                param(24) = New SqlParameter("@ApprovalDateFrom", ApprovalDateFrom)
                param(25) = New SqlParameter("@ApprovalDateTo", ApprovalDateTo)
                param(26) = New SqlParameter("@ApprovedBy", ApprovedBy)
                param(27) = New SqlParameter("@ApprovalNotes", ApprovalNotes)
                param(28) = New SqlParameter("@AdditionalNotes", AdditionalNotes)
                param(29) = New SqlParameter("@RateRequestDate", RateRequestDate)
                param(30) = New SqlParameter("@EntryDate", EntryDate)
                param(31) = New SqlParameter("@IsLocalCurrency", IsLocalCurrency)

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

        Public Shared Function GetOceanTariff(ByVal ID As String, ByVal ConsigneeName As String, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal TotalLCL As String, ByVal BAFLCL As String, ByVal PSSLCL As String, ByVal TotalFCL20GP As String, ByVal BAFFCL20GP As String, ByVal PSSFCL20GP As String, ByVal TotalFCL40GP As String, ByVal BAFFCL40GP As String, ByVal PSSFCL40GP As String, ByVal TotalFCL40HC As String, ByVal BAFFCL40HC As String, ByVal PSSFCL40HC As String, ByVal Comments As String, ByVal Requestor As String, ByVal RequestDate As String, ByVal ApprovalDateFrom As String, ByVal ApprovalDateTo As String, ByVal Approver As String, ByVal EffectiveDate As String, ByVal ExpiryDate As String) As DataTable
            Dim query As String = "GetOceanTariff"

            Try
                Dim param(37) As SqlParameter

                param(0) = New SqlParameter("@ID", ID)
                param(1) = New SqlParameter("@ConsigneeName", ConsigneeName)
                param(2) = New SqlParameter("@ContainerNo", ContainerNo)
                param(3) = New SqlParameter("@OceanHBL", OceanHBL)
                param(4) = New SqlParameter("@ShipDate", ShipDate)
                param(5) = New SqlParameter("@FreightTerm", FreightTerm)
                param(6) = New SqlParameter("@WDShipMethod", WDShipMethod)
                param(7) = New SqlParameter("@ShipperName", ShipperName)
                param(8) = New SqlParameter("@OriginCity", OriginCity)
                param(9) = New SqlParameter("@OriginPort", OriginPort)
                param(10) = New SqlParameter("@OriginZipcode", OriginZipcode)
                param(11) = New SqlParameter("@OriginRegion", OriginRegion)
                param(12) = New SqlParameter("@DestCity", DestCity)
                param(13) = New SqlParameter("@DestPort", DestPort)
                param(14) = New SqlParameter("@DestZipcode", DestZipcode)
                param(15) = New SqlParameter("@DestRegion", DestRegion)
                param(16) = New SqlParameter("@RatesValidFor", RatesValidFor)
                param(17) = New SqlParameter("@RatesValidTill", RatesValidTill)
                param(18) = New SqlParameter("@TotalLCL", TotalLCL)
                param(19) = New SqlParameter("@BAFLCL", BAFLCL)
                param(20) = New SqlParameter("@PSSLCL", PSSLCL)
                param(21) = New SqlParameter("@TotalFCL20GP", TotalFCL20GP)
                param(22) = New SqlParameter("@BAFFCL20GP", BAFFCL20GP)
                param(23) = New SqlParameter("@PSSFCL20GP", PSSFCL20GP)
                param(24) = New SqlParameter("@TotalFCL40GP", TotalFCL40GP)
                param(25) = New SqlParameter("@BAFFCL40GP", BAFFCL40GP)
                param(26) = New SqlParameter("@PSSFCL40GP", PSSFCL40GP)
                param(27) = New SqlParameter("@TotalFCL40HC", TotalFCL40HC)
                param(28) = New SqlParameter("@BAFFCL40HC", BAFFCL40HC)
                param(29) = New SqlParameter("@PSSFCL40HC", PSSFCL40HC)
                param(30) = New SqlParameter("@Comments", Comments)
                param(31) = New SqlParameter("@Requestor", Requestor)
                param(32) = New SqlParameter("@RequestDate", RequestDate)
                param(33) = New SqlParameter("@ApprovalDateFrom", ApprovalDateFrom)
                param(34) = New SqlParameter("@ApprovalDateTo", ApprovalDateTo)
                param(35) = New SqlParameter("@Approver", Approver)
                param(36) = New SqlParameter("@EffectiveDate", EffectiveDate)
                param(37) = New SqlParameter("@ExpiryDate", ExpiryDate)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSimilarTariffLanes_15july(ByVal ExceptRateRequestID As Integer, ByVal OriginAirport As String, ByVal DestAirport As String, ByVal ShipMethod As String) As DataTable
            Dim query As String = "GetSimilarTariffLanes_15july"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@ExceptRateRequestID", ExceptRateRequestID)
            param(1) = New SqlParameter("@OriginAirport", DBStrValue(OriginAirport))
            param(2) = New SqlParameter("@DestAirport", DBStrValue(DestAirport))
            param(3) = New SqlParameter("@ShipMethod", ShipMethod)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RateRequestToTariff_15july(ByVal RateRequestID As Integer, ByVal ApprovalDate As DateTime, ByVal TransferDate As DateTime) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "RateRequestToTariff_15july"

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

        Public Shared Function RateRequestToTariff_15july(ByVal RateRequestID As Integer, ByVal ApprovalDate As DateTime, ByVal TransferDate As DateTime, ByVal UserID As Integer) As Integer
            'Validate Inputs
            If RateRequestID <= 0 Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "RateRequestToTariff_15july"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@ApprovalDate", ApprovalDate)
            param(2) = New SqlParameter("@TransferDate", TransferDate)
            param(3) = New SqlParameter("@UserId", UserID)

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