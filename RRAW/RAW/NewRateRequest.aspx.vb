Public Class NewRateRequest
    Inherits System.Web.UI.Page

    Private Shared memMinFreightRate As Double
    Private Shared memFreightRate As Double
    Private Shared memSecurityRate As Double
    Private Shared memOtherCharges As String

    Private Function GetCurrentDateTime() As DateTime
        Try
            Return Convert.ToDateTime(hidCurrentDateTime.Value)
        Catch
            Return Now
        End Try
    End Function

    Private Sub ClearFields()
        txtHAWBNumber.Text = ""
        cmbCEVATransitMode.ClearSelection()
        txtShipDate.Text = ""
        cmbServiceLevel.ClearSelection()
        cmbForwarderService.ClearSelection()
        txtWeight.Text = ""
        txtShipperName.Text = ""
        txtOriginAirport.Text = ""
        txtOriginCity.Text = ""
        cmbOriginRegion.ClearSelection()
        txtOriginZipCode.Text = ""
        txtConsigneeName.Text = ""
        txtDestAirport.Text = ""
        txtDestCity.Text = ""
        txtDestState.Text = ""
        cmbDestCountry.ClearSelection()
        cmbDestRegion.ClearSelection()
        txtDestZipCode.Text = ""
        cmbShipMethod.ClearSelection()
        cmbDocType.ClearSelection()
        cmbCustomClearanceMode.ClearSelection()
        txtRateDeterMethodology.Text = ""
        txtMinFreightRate.Text = ""
        txtFreightRate.Text = ""
        txtSecurityRate.Text = ""
        txtOtherCharges.Text = ""
        cmbFreightForwarder.ClearSelection()
        txtComment.Text = ""
    End Sub

    Private Sub PopulateShipMethods()
        Dim l As ListItem

        cmbShipMethod.Items.Add(New ListItem("Select", ""))
        Dim dt = DB.ShipMethods.GetAllShipMethods

        Dim arrToSort(dt.Rows.Count - 1) As String
        For i As Integer = 0 To dt.Rows.Count - 1
            arrToSort(i) = dt.Rows(i)("ShipMethod").ToString
        Next

        Dim zeroIncrValue = 10000
        Dim arrToCompare(dt.Rows.Count - 1) As Integer
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim val = dt.Rows(i)("ShipMethod").ToString
            Dim temp As String = ""
            Dim flag As Boolean = False
            For j As Integer = 1 To val.Length - 1
                If Asc(val(j)) >= 48 And Asc(val(j)) <= 57 Then
                    If flag = False Then
                        temp &= CInt(CStr(val(j))) * (j * 1000 - 999)
                    Else
                        temp &= val(j)
                    End If
                    flag = True
                ElseIf flag = True Then
                    flag = False
                    Exit For
                End If
            Next
            If IsNumeric(temp) Then
                arrToCompare(i) = CInt(temp)
            Else
                arrToCompare(i) = zeroIncrValue
                zeroIncrValue += 1
            End If
        Next

        SortItemsByNaturalOrder(arrToCompare, arrToSort)
        For Each item As String In arrToSort
            l = New ListItem(item, item)
            cmbShipMethod.Items.Add(l)
        Next
    End Sub

    Private Sub SortItemsByNaturalOrder(ByVal arrToCompare As Integer(), ByRef arrToSort As String())
        Dim tempToSort As String
        Dim tempToCompare As Integer

        For i As Integer = 0 To arrToCompare.Length - 1
            For j As Integer = i + 1 To arrToCompare.Length - 1
                If (arrToCompare(i) > arrToCompare(j)) Then
                    tempToCompare = arrToCompare(i)
                    arrToCompare(i) = arrToCompare(j)
                    arrToCompare(j) = tempToCompare

                    tempToSort = arrToSort(i)
                    arrToSort(i) = arrToSort(j)
                    arrToSort(j) = tempToSort
                End If
            Next
        Next
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource8.ConnectionString = CurrentDBConnection
        SqlDataSource9.ConnectionString = CurrentDBConnection

        PopulateShipMethods()

        If qStrCurrentRateRequestID = 0 Then
            'pnlAttachments.Visible = True
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Cache.SetCacheability(HttpCacheability.Private)
        'Response.Cache.SetMaxAge(New TimeSpan(7, 1, 0, 0))
    End Sub

    Private Sub TestAssignments()
        'cmbCEVATransitMode.SelectedIndex = 1
        'cmbServiceLevel.SelectedIndex = 1
        'cmbForwarderService.SelectedIndex = 1
        'cmbOriginRegion.SelectedIndex = 1
        'cmbDestRegion.SelectedIndex = 1
        'cmbDestCountry.SelectedIndex = 1
        'cmbDocType.SelectedIndex = 1
        'cmbCustomClearanceMode.SelectedIndex = 1
        'cmbShipMethod.SelectedIndex = 1

        'txtShipDate.Text = "1/1/2011"
    End Sub

    Private Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        If Not IsPostBack Then
            If qStrCurrentRateRequestID > 0 Then
                LoadDetails(qStrCurrentRateRequestID)
            Else
                PrepareForNew()
            End If
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        BrowserAdjustments()
    End Sub

    Private Sub BrowserAdjustments()
        Select Case Request.Browser.Browser
            Case "IE"
                Select Case Request.Browser.Version
                    Case "6.0"
                        PanelNewRateRequest.Height = 492
                        txtCapture.Height = 471
                    Case "7.0"
                        PanelNewRateRequest.Height = 473
                        txtCapture.Height = 454
                    Case Else
                        PanelNewRateRequest.Height = 492
                        txtCapture.Height = 473
                End Select
            Case Else
                PanelNewRateRequest.Height = 490
                txtCapture.Height = 477
        End Select
    End Sub

    Private Sub LoadDetails(ByVal rateRequestID As Integer)
        Dim dr As DataRow = DB.AirRateRequests.GetRateRequestByID(rateRequestID)
        Dim strCurrency As String
        DisableFields()

        If dr IsNot Nothing Then
            lblRequestDate.Text = "Date: " & If(dr("Request Date") IsNot DBNull.Value, CDate(dr("Request Date")).ToString("MM/dd/yyyy"), "")
            txtHAWBNumber.Text = If(dr("HAWB Number") IsNot DBNull.Value, dr("HAWB Number"), "").ToString.Trim
            Try
                cmbShipMethod.Items.FindByText(If(dr("Ship Method") IsNot DBNull.Value, dr("Ship Method"), "").ToString()).Selected = True
            Catch ex As Exception
            End Try
            txtShipDate.Text = If(dr("Ship Date") IsNot DBNull.Value, CDate(dr("Ship Date")).ToString("MM/dd/yyyy"), "")
            Try
                cmbServiceLevel.Items.FindByText(If(dr("Requested Service Level") IsNot DBNull.Value, dr("Requested Service Level"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            txtWeight.Text = If(dr("Weight") IsNot DBNull.Value, dr("Weight"), "").ToString.Trim
            Try
                cmbForwarderService.Items.FindByText(If(dr("Forwarder Service") IsNot DBNull.Value, dr("Forwarder Service"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            txtShipperName.Text = If(dr("Shipper Name") IsNot DBNull.Value, dr("Shipper Name"), "").ToString.Trim
            txtOriginAirport.Text = If(dr("Origin Airport") IsNot DBNull.Value, dr("Origin Airport"), "").ToString.Trim
            txtOriginCity.Text = If(dr("Origin City") IsNot DBNull.Value, dr("Origin City"), "").ToString.Trim
            Try
                cmbOriginRegion.Items.FindByText(If(dr("Origin Region") IsNot DBNull.Value, dr("Origin Region"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            txtOriginZipCode.Text = If(dr("Origin Zipcode") IsNot DBNull.Value, dr("Origin Zipcode"), "").ToString.Trim
            txtConsigneeName.Text = If(dr("Consignee Name") IsNot DBNull.Value, dr("Consignee Name"), "").ToString.Trim
            txtDestAirport.Text = If(dr("Destination Airport") IsNot DBNull.Value, dr("Destination Airport"), "").ToString.Trim
            txtDestCity.Text = If(dr("Destination City") IsNot DBNull.Value, dr("Destination City"), "").ToString.Trim
            txtDestState.Text = If(dr("Destination State") IsNot DBNull.Value, dr("Destination State"), "").ToString.Trim
            Try
                cmbDestCountry.Items.FindByText(If(dr("Destination Country") IsNot DBNull.Value, dr("Destination Country"), "").ToString.Trim).Selected = True
            Catch ex As Exception
            End Try
            Try
                cmbDestRegion.Items.FindByText(If(dr("Destination Region") IsNot DBNull.Value, dr("Destination Region"), "").ToString.Trim).Selected = True
            Catch ex As Exception
            End Try
            txtDestZipCode.Text = If(dr("Destination Zipcode") IsNot DBNull.Value, dr("Destination Zipcode"), "").ToString.Trim
            txtForwarderZipcode.Text = If(dr("Forwarder Zipcode") IsNot DBNull.Value, dr("Forwarder Zipcode"), "").ToString.Trim
            Try
                cmbCEVATransitMode.Items.FindByText(If(dr("CEVA Transit Mode") IsNot DBNull.Value, dr("CEVA Transit Mode"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            Try
                cmbDocType.Items.FindByText(If(dr("Doc Type") IsNot DBNull.Value, dr("Doc Type"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            Try
                cmbCustomClearanceMode.Items.FindByValue(If(dr("Custom Clearance Mode") IsNot DBNull.Value, dr("Custom Clearance Mode"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try
            txtRateDeterMethodology.Text = If(dr("Rate Determination Method") IsNot DBNull.Value, dr("Rate Determination Method"), "").ToString.Trim
            txtMinFreightRate.Text = If(dr("Min Freight Rate") IsNot DBNull.Value, dr("Min Freight Rate"), "").ToString.Trim

            'USD Currency
            strCurrency = GetCurrency(txtOriginAirport.Text, txtDestAirport.Text, cmbCEVATransitMode.SelectedItem.Text)

            lblMinFreightRateUSD.Text = IIf(strCurrency <> "USD", ("(" + IIf(dr("Min Freight Rate USD") IsNot DBNull.Value, dr("Min Freight Rate USD"), "0").ToString.Trim + ")"), "").ToString
            'ConvertToUSD(lblMinFreightRateUSD, If(dr("Min Freight Rate") IsNot DBNull.Value, dr("Min Freight Rate"), "").ToString.Trim, 0, GetCurrency(txtOriginAirport.Text, txtDestAirport.Text, cmbCEVATransitMode.SelectedItem.Text))

            txtFreightRate.Text = If(dr("Freight Rate") IsNot DBNull.Value, dr("Freight Rate"), "").ToString.Trim

            'USD Currency
            lblFreightRateUSD.Text = IIf(strCurrency <> "USD", ("(" + IIf(dr("Freight Rate USD") IsNot DBNull.Value, dr("Freight Rate USD"), "0").ToString.Trim + ")"), "").ToString
            'ConvertToUSD(lblFreightRateUSD, If(dr("Freight Rate") IsNot DBNull.Value, dr("Freight Rate"), "").ToString.Trim, 0, GetCurrency(txtOriginAirport.Text, txtDestAirport.Text, cmbCEVATransitMode.SelectedItem.Text))

            txtSecurityRate.Text = If(dr("Security Rate") IsNot DBNull.Value, dr("Security Rate"), "").ToString.Trim

            'USD Currency
            lblSecurityRateUSD.Text = IIf(strCurrency <> "USD", ("(" + If(dr("Security Rate USD") IsNot DBNull.Value, dr("Security Rate USD"), "0").ToString.Trim + ")"), "").ToString
            'ConvertToUSD(lblSecurityRateUSD, If(dr("Security Rate") IsNot DBNull.Value, dr("Security Rate"), "").ToString.Trim, 0, GetCurrency(txtOriginAirport.Text, txtDestAirport.Text, cmbCEVATransitMode.SelectedItem.Text))

            txtOtherCharges.Text = If(dr("Other Charges") IsNot DBNull.Value, dr("Other Charges"), "").ToString.Trim
            Try
                cmbFreightForwarder.Items.FindByText(If(dr("Freight Forwarder") IsNot DBNull.Value, dr("Freight Forwarder"), "").ToString().Trim).Selected = True
            Catch ex As Exception
            End Try

            btnApprove.Visible = False
            btnApproveAsAdhoc.Visible = False
            btnBackToDashboard.Visible = True
            btnPostComment.Visible = True
            btnNeedToReviseRate.Visible = False
            btnPostNewRateRequest.Visible = False
            btnReject.Visible = False
            btnRevoke.Visible = False
            btnRemoveAllComments.Visible = False
            btnSendBackToReviseRate.Visible = False
            btnTransferToTariff.Visible = False
            btnDelete.Visible = False
            btnSave.Visible = False
            'btnApprove_Permanent.Visible = False
            'btnSendBackToRequestor.Visible = False

            If CInt(dr("RequestorID")) = CurrentUserID Then
                btnDelete.Visible = False
            Else
                btnDelete.Visible = True
            End If

            If dr("Approver") Is DBNull.Value Then
                Dim currentRateRequestHolders = DB.RateRequestHolders.GetRateRequestHolders(rateRequestID)
                If currentRateRequestHolders.Contains(CurrentUserID) Then
                    btnApprove.Visible = True
                    If CurrentUserType = "Client" Then
                        btnApproveAsAdhoc.Visible = True
                        btnNeedToReviseRate.Visible = False
                        btnReject.Visible = True

                        pnlAttachments.Visible = True
                    ElseIf CurrentUserID = 3 Then
                        btnBackToDashboard.Visible = False
                        btnNeedToReviseRate.Visible = True
                        btnApprove_Permanent.Visible = True
                        pnlAttachments.Visible = False
                    Else
                        btnBackToDashboard.Visible = False
                        btnNeedToReviseRate.Visible = True

                        pnlAttachments.Visible = False
                    End If
                End If

                If CurrentUserType = "Admin" Then
                    btnDelete.Visible = True
                    btnBackToDashboard.Visible = False
                    btnRemoveAllComments.Visible = True
                    If (currentRateRequestHolders.Count <> 0) Then
                        If (currentRateRequestHolders.Contains(3) Or currentRateRequestHolders.Contains(11)) Then
                            btnSendBackToRequestor.Visible = True
                        End If
                        btnNeedToReviseRate.Visible = True
                    End If


                    'btnNeedToReviseRate_Click(Nothing, Nothing)
                End If

                txtComment.Enabled = True
                btnPostComment.Enabled = True

                lblStatus.Text = "Details for all fields are loaded successfully. You can add your comment(s) to this rate request."
                lblStatus.ForeColor = Drawing.Color.Black
            Else
                Dim currentRateRequestHolders = DB.RateRequestHolders.GetRateRequestHolders(rateRequestID)
                If currentRateRequestHolders.Contains(CurrentUserID) Then
                    If CurrentUserType = "Admin" Then
                        btnDelete.Visible = True
                        btnBackToDashboard.Visible = False
                        btnRemoveAllComments.Visible = True
                        If (currentRateRequestHolders.Count <> 0) Then
                            If (currentRateRequestHolders.Contains(3) Or currentRateRequestHolders.Contains(11)) Then
                                btnSendBackToRequestor.Visible = True
                            End If
                            btnNeedToReviseRate.Visible = True
                            'btnNeedToReviseRate_Click(Nothing, Nothing)
                        End If
                    End If
                End If

                txtComment.Enabled = False
                btnPostComment.Enabled = False

                If CBool(If(dr("Is Adhoc") IsNot DBNull.Value, dr("Is Adhoc"), False)) = False Then
                    lblStatus.Text = "This lane was approved by '" & dr("Approver").ToString & "' on " & If(dr("Approval Date") IsNot DBNull.Value, dr("Approval Date"), "").ToString.Trim & " as effective from " & If(dr("Effective Date") IsNot DBNull.Value, dr("Effective Date"), "").ToString.Trim & " to " & If(dr("Expiry Date") IsNot DBNull.Value, dr("Expiry Date"), "").ToString.Trim & "."
                Else
                    lblStatus.Text = "This lane was approved as adhoc by '" & dr("Approver").ToString & "' on " & If(dr("Approval Date") IsNot DBNull.Value, dr("Approval Date"), "").ToString.Trim & " as effective from " & If(dr("Effective Date") IsNot DBNull.Value, dr("Effective Date"), "").ToString.Trim & " to " & If(dr("Expiry Date") IsNot DBNull.Value, dr("Expiry Date"), "").ToString.Trim & "."
                End If
                lblStatus.ForeColor = Drawing.Color.Blue
                End If

                pnlPreviousComments.Visible = True
                pnlAdditionalInformation.Visible = True

                rptrPreviousComments.DataBind()
                rptrRateRequestHistory.DataBind()

                lblTitle.Text = "Air Rate Request Details<br>ID: " & qStrCurrentRateRequestID

                'Load Similar lane details
                'Load Similar rate requests
                gridSimilarRateRequests.DataSource = DB.AirRateRequests.GetSimilarRateRequests(rateRequestID, dr("Origin Airport").ToString, dr("Destination Airport").ToString, dr("Destination City").ToString, dr("Destination Country").ToString, dr("CEVA Transit Mode").ToString, dr("Ship Method").ToString)
                gridSimilarRateRequests.DataBind()

                'Load Similar air freight rate lanes
                gridSimilarTariffLanes.DataSource = DB.Tariffs.GetSimilarTariffLanes(rateRequestID, dr("Origin Airport").ToString, dr("Destination Airport").ToString, dr("Destination City").ToString, dr("CEVA Transit Mode").ToString, dr("Ship Method").ToString)
                gridSimilarTariffLanes.DataBind()

                'Load Similar approved as adhoc lanes
                gridSimilarAdhocLanes.DataSource = DB.AirRateRequests.GetSimilarAdhocLanes(rateRequestID, dr("Origin Airport").ToString, dr("Destination Airport").ToString, dr("Destination City").ToString, dr("Destination Country").ToString, dr("CEVA Transit Mode").ToString, dr("Ship Method").ToString)
                gridSimilarAdhocLanes.DataBind()

                If CurrentUserType = "Client" AndAlso DB.RateRequestLog.GetRateRequestLogsByRequestID(qStrCurrentRateRequestID).Select("OperatorID=" & CurrentUserID).Length > 0 Then
                    If dr("Effective Date") IsNot DBNull.Value Then
                        If CDate(dr("Effective Date")) > Now Then
                            btnRevoke.Visible = True
                        End If
                    End If
                End If
        Else
                txtComment.Enabled = False
                lblStatus.Text = "No data available for this rate request."
                lblStatus.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Sub PrepareForNew()
        TestAssignments()
        lblRequestDate.Text = "Date: " & GetCurrentDateTime.ToString("MM/dd/yyyy")

        txtOriginAirport.Text = qStrOriginAirport
        If qStrOriginRegion IsNot Nothing Then
            Try
                cmbOriginRegion.Items.FindByText(qStrOriginRegion).Selected = True
            Catch ex As Exception
            End Try
        End If
        txtDestAirport.Text = qStrDestAirport
        txtDestCity.Text = qStrDestCity
        txtDestState.Text = qStrDestState
        If qStrDestCountry IsNot Nothing Then
            Try
                cmbDestCountry.Items.FindByText(qStrDestCountry).Selected = True
            Catch ex As Exception
            End Try
        End If
        If qStrDestRegion IsNot Nothing Then
            Try
                cmbDestRegion.Items.FindByText(qStrDestRegion).Selected = True
            Catch ex As Exception
            End Try
        End If
        txtDestZipCode.Text = qStrDestZipcode
        If qStrCEVATransitMode IsNot Nothing Then
            Try
                cmbCEVATransitMode.Items.FindByText(qStrCEVATransitMode).Selected = True
            Catch ex As Exception
            End Try
        End If
        If qStrShipMethod IsNot Nothing Then
            Try
                cmbShipMethod.Items.FindByText(qStrShipMethod).Selected = True
            Catch ex As Exception
            End Try
        End If
        txtForwarderZipcode.Text = qStrForwarderZipcode
        If qStrCustomClearanceMode IsNot Nothing Then
            Try
                cmbCustomClearanceMode.Items.FindByText(qStrCustomClearanceMode).Selected = True
            Catch ex As Exception
            End Try
        End If
        If qStrServiceLevel IsNot Nothing Then
            Try
                cmbServiceLevel.Items.FindByText(qStrServiceLevel).Selected = True
            Catch ex As Exception
            End Try
        End If
        If qStrForwarderService IsNot Nothing Then
            Try
                cmbForwarderService.Items.FindByText(qStrForwarderService).Selected = True
            Catch ex As Exception
            End Try
        End If
        txtMinFreightRate.Text = qStrMinFreightRate
        txtFreightRate.Text = qStrFreightRate
        txtSecurityRate.Text = qStrSecurityRate
        txtOtherCharges.Text = qStrOtherCharges
        If qStrFreightForwarder IsNot Nothing Then
            Try
                cmbFreightForwarder.Items.FindByText(qStrFreightForwarder).Selected = True
            Catch ex As Exception
            End Try
        End If

        'pnlAttachments.Visible = True
    End Sub

    Private Sub DisableFields()
        txtHAWBNumber.Enabled = False
        txtShipDate.Enabled = False
        txtWeight.Enabled = False
        txtShipperName.Enabled = False
        txtOriginCity.Enabled = False
        txtOriginAirport.Enabled = False
        txtOriginZipCode.Enabled = False
        cmbOriginRegion.Enabled = False
        txtConsigneeName.Enabled = False
        txtDestCity.Enabled = False
        txtDestState.Enabled = False
        txtDestAirport.Enabled = False
        txtDestZipCode.Enabled = False
        cmbDestRegion.Enabled = False
        cmbDestCountry.Enabled = False
        txtForwarderZipcode.Enabled = False
        txtMinFreightRate.Enabled = False
        txtFreightRate.Enabled = False
        txtSecurityRate.Enabled = False
        txtRateDeterMethodology.Enabled = False
        txtOtherCharges.Enabled = False

        cmbCEVATransitMode.Enabled = False
        cmbCustomClearanceMode.Enabled = False
        cmbDocType.Enabled = False
        cmbForwarderService.Enabled = False
        cmbFreightForwarder.Enabled = False
        cmbServiceLevel.Enabled = False
        cmbShipMethod.Enabled = False

        pnlCapture.Visible = False

        pnlAttachments.Visible = False
    End Sub

    Private Sub EnableFields()
        txtHAWBNumber.Enabled = True
        txtShipDate.Enabled = True
        txtWeight.Enabled = True
        txtShipperName.Enabled = True
        txtOriginCity.Enabled = True
        txtOriginAirport.Enabled = True
        txtOriginZipCode.Enabled = True
        cmbOriginRegion.Enabled = True
        txtConsigneeName.Enabled = True
        txtDestCity.Enabled = True
        txtDestState.Enabled = True
        txtDestAirport.Enabled = True
        txtDestZipCode.Enabled = True
        txtForwarderZipcode.Enabled = True
        cmbDestRegion.Enabled = True
        cmbDestCountry.Enabled = True
        txtMinFreightRate.Enabled = True
        txtFreightRate.Enabled = True
        txtSecurityRate.Enabled = True
        txtRateDeterMethodology.Enabled = True
        txtOtherCharges.Enabled = True

        cmbCEVATransitMode.Enabled = True
        cmbCustomClearanceMode.Enabled = True
        cmbDocType.Enabled = True
        cmbForwarderService.Enabled = True
        cmbFreightForwarder.Enabled = True
        cmbServiceLevel.Enabled = True
        cmbShipMethod.Enabled = True

        pnlAttachments.Visible = True

        EnableAllAttachmentCheckboxes()
    End Sub

    Private ReadOnly Property qStrCurrentRateRequestID() As Integer
        Get
            Return CInt(Server.UrlDecode(Request.QueryString("RateRequestID")))
        End Get
    End Property

    Private ReadOnly Property qStrOriginAirport() As String
        Get
            Return Server.UrlDecode(Request.QueryString("OriginAirport"))
        End Get
    End Property

    Private ReadOnly Property qStrOriginRegion() As String
        Get
            Return Server.UrlDecode(Request.QueryString("OriginRegion"))
        End Get
    End Property

    Private ReadOnly Property qStrDestAirport() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestAirport"))
        End Get
    End Property

    Private ReadOnly Property qStrDestCity() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestCity"))
        End Get
    End Property

    Private ReadOnly Property qStrDestState() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestState"))
        End Get
    End Property

    Private ReadOnly Property qStrDestCountry() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestCountry"))
        End Get
    End Property

    Private ReadOnly Property qStrDestRegion() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestRegion"))
        End Get
    End Property

    Private ReadOnly Property qStrDestZipcode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestZipcode"))
        End Get
    End Property

    Private ReadOnly Property qStrCEVATransitMode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("CEVATransitMode"))
        End Get
    End Property

    Private ReadOnly Property qStrShipMethod() As String
        Get
            Return Server.UrlDecode(Request.QueryString("ShipMethod"))
        End Get
    End Property

    Private ReadOnly Property qStrForwarderZipcode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("ForwarderZipcode"))
        End Get
    End Property

    Private ReadOnly Property qStrCustomClearanceMode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("CustomClearanceMode"))
        End Get
    End Property

    Private ReadOnly Property qStrServiceLevel() As String
        Get
            Dim vForwarderService As String = Request.QueryString("ForwarderServiceLevel")
            If vForwarderService <> "" Then
                Try
                    Return vForwarderService.Substring(0, vForwarderService.IndexOf(" "c))
                Catch
                    Return ""
                End Try
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private ReadOnly Property qStrForwarderService() As String
        Get
            Dim vForwarderService As String = Request.QueryString("ForwarderServiceLevel")
            If vForwarderService <> "" Then
                Try
                    Return vForwarderService.Substring(vForwarderService.IndexOf(" "c) + 1, vForwarderService.Length - (vForwarderService.IndexOf(" "c) + 1))
                Catch
                    Return ""
                End Try
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private ReadOnly Property qStrMinFreightRate() As String
        Get
            Return Server.UrlDecode(Request.QueryString("MinFreightRate"))
        End Get
    End Property

    Private ReadOnly Property qStrFreightRate() As String
        Get
            Return Server.UrlDecode(Request.QueryString("FreightRate"))
        End Get
    End Property

    Private ReadOnly Property qStrSecurityRate() As String
        Get
            Return Server.UrlDecode(Request.QueryString("SecurityRate"))
        End Get
    End Property

    Private ReadOnly Property qStrOtherCharges() As String
        Get
            Return Server.UrlDecode(Request.QueryString("OtherCharges"))
        End Get
    End Property

    Private ReadOnly Property qStrFreightForwarder() As String
        Get
            Return Server.UrlDecode(Request.QueryString("FreightForwarder"))
        End Get
    End Property

    'Private Function UpLoadFileToMail(ByVal MailServer As String, ByVal fromAddr As String, ByVal passWd As String) As Boolean
    '    Try
    '        Dim mailClient As New SmtpClient(MailServer, 25)
    '        mailClient.UseDefaultCredentials = False
    '        mailClient.Credentials = New NetworkCredential(fromAddr, passWd)

    '        Using message As New MailMessage()
    '            message.From = New MailAddress(fromAddr)
    '            SetAddressCollection(message.[To])

    '            'SUBJECT
    '            message.Subject = Path.GetFileName(excelFile).Replace(Path.GetExtension(excelFile), "")

    '            'BODY
    '            Dim bodyTop As String = "<html><table><tr><td><font face='verdana' style='font-size: 10pt'><body><div width=""100%""><br>" & "Hi Team,<br><br>Good Morning, please find enclosed <b>Inter-Branch Report</b> generated for <b>today</b>." & "<br></td></tr>"
    '            'dtSummary.DefaultView.Sort = dtSummary.Columns(0).Caption

    '            Dim bodyMiddle As String = createHtmlCode(dtSummary)
    '            Dim bodyBottom As String = "<tr><td><div width=""100%""><table cellpadding=""3"" align=""left"" style=""border-collapse:collapse;font-family:verdana;font-size:10pt"" width='100%'>" & "<tr><td><br><br>Thanks,<br>" & StrConv(fromAddr.Split("@")(0).Replace(".", " "), VbStrConv.ProperCase) & "</td></tr></table></div></td></tr></div></font></body></html>"
    '            message.Body = bodyTop + bodyMiddle + bodyBottom
    '            message.IsBodyHtml = True

    '            'ATTACHMENT
    '            If Not [String].IsNullOrEmpty(excelFile) Then
    '                message.Attachments.Add(New System.Net.Mail.Attachment(excelFile))
    '            End If

    '            mailClient.EnableSsl = True
    '            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
    '            message.Priority = MailPriority.Normal
    '            mailClient.Timeout = 60 * 1000 * 5

    '            mailClient.Send(message)
    '        End Using

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show("Error : " & ex.Message)
    '        Return False
    '    End Try

    'End Function

    Private Sub BackupRates()
        memMinFreightRate = StringToDouble(txtMinFreightRate.Text)
        memFreightRate = StringToDouble(txtFreightRate.Text)
        memSecurityRate = StringToDouble(txtSecurityRate.Text)
        memOtherCharges = txtOtherCharges.Text
    End Sub

    Private Function DropDownHasValue(ByVal ctrlDropDownList As DropDownList, ByVal Value As String) As Boolean
        For Each item In ctrlDropDownList.Items
            If item.ToString = Value Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))

        DB.RateRequestLog.RemoveRateRequestLogs(qStrCurrentRateRequestID)

        DB.AirRateRequestHistory.RemoveRateRequestHistory(qStrCurrentRateRequestID)

        DB.RateRequestHolders.RemoveRateRequestHolders(qStrCurrentRateRequestID)

        DB.AirRateRequests.RemoveAllComments(qStrCurrentRateRequestID)

        DB.AirRateRequests.RemoveRateRequest(qStrCurrentRateRequestID)

        DB.Attachments.RemoveAttachmentsByReferenceID(qStrCurrentRateRequestID)

        'Send mail to users in chain
        For Each row As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been deleted by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
        Next

        SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been deleted by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Protected Sub btnPostNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPostNewRateRequest.Click
        Page.Validate()

        If Page.IsValid Then

            'If (Not CurrentUserName.ToUpper = "KATIE TRANG" AndAlso Now <= CDate("13-Apr-2012 23:59:59")) Then
            '    Page.Validate()
            'End If

            'If Page.IsValid Or (CurrentUserName.ToUpper = "KATIE TRANG" AndAlso Now <= CDate("13-Apr-2012 23:59:59")) Then

            Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
            If IsDate(txtShipDate.Text) = True Then
                varShipDate = CDate(txtShipDate.Text)
                txtShipDate.Text = CDate(txtShipDate.Text).ToString("MM/dd/yyyy")
            Else
                txtShipDate.Text = ""
            End If

            Dim varWeight As Double = StringToDouble(txtWeight.Text)
            Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
            Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
            Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

            'Check for existing Similar lanes
            Dim dt As DataTable
            'Check in Rate Requests
            dt = DB.AirRateRequests.GetRateRequestByDetails(txtConsigneeName.Text, txtOriginAirport.Text, txtDestAirport.Text, txtDestCity.Text, cmbCEVATransitMode.SelectedValue, cmbShipMethod.SelectedValue, varMinFreighRate, varFreighRate, varSecurityRate)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                lblStatus.Text = "<a style='font-weight:bold' href='NewRateRequest.aspx?RateRequestID=" & dr("ID").ToString & "'> Rate Request No. " & dr("ID").ToString & " was previously generated by '" & dr("Requestor").ToString & "' with similar details.</a> You cannot generate new rate request for the same information."
                lblStatus.ForeColor = Drawing.Color.Red
                Exit Sub
            End If

            'Check in Tariff
            dt = DB.Tariffs.GetTariffByDetails(txtConsigneeName.Text, txtOriginAirport.Text, txtDestAirport.Text, txtDestCity.Text, cmbCEVATransitMode.SelectedValue, cmbShipMethod.SelectedValue, varMinFreighRate.ToString, varFreighRate.ToString, varSecurityRate.ToString)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                lblStatus.Text = "<a style='font-weight:bold' href='Tariff.aspx?OriginAirport=" & dr("OriginAirport").ToString & "&DestAirport=" & dr("DestAirport").ToString & "&DestCity=" & dr("DestCity").ToString & "&CEVATransitMode=" & dr("CEVATransitMode").ToString & "&ShipMethod=" & dr("ShipMethod").ToString & "&MinFreightRate=" & dr("MinFreightRate").ToString.Trim & "&FreightRate=" & dr("FreightRate").ToString.Trim & "&SecurityRate=" & dr("SecurityRate").ToString.Trim & "'>Rates for the same lane is available in tariff.</a> You cannot generate new rate request for the same information."
                lblStatus.ForeColor = Drawing.Color.Red
                Exit Sub
            End If

            'Check in Adhoc Rate Requests
            dt = DB.AirRateRequests.GetAdhocRateRequestByDetails(txtConsigneeName.Text, txtOriginAirport.Text, txtDestAirport.Text, txtDestCity.Text, cmbCEVATransitMode.SelectedValue, cmbShipMethod.SelectedValue, varMinFreighRate, varFreighRate, varSecurityRate)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                lblStatus.Text = "<a style='font-weight:bold' href='ApprovedAsAdhoc.aspx?OriginAirport=" & dr("OriginAirport").ToString & "&DestAirport=" & dr("DestAirport").ToString & "&DestCity=" & dr("DestCity").ToString & "&CEVATransitMode=" & dr("CEVATransitMode").ToString & "&ShipMethod=" & dr("ShipMethod").ToString & "&MinFreightRate=" & CDbl(dr("MinFreightRate")).ToString("0.00") & "&FreightRate=" & CDbl(dr("FreightRate")).ToString("0.00") & "&SecurityRate=" & CDbl(dr("SecurityRate")).ToString("0.00") & "'>Rates for the same lane was previously approved as adhoc.</a> You cannot generate new rate request for the same information."
                lblStatus.ForeColor = Drawing.Color.Red
                Exit Sub
            End If
            'Add link to tariff page including parameters and filters in tariff page

            'Post New Rate Request
            Dim newID As Integer = DB.AirRateRequests.PostNewRateRequest(CurrentUserID, GetCurrentDateTime, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text)

            UploadAttachmentsForRateRequest(newID)

            DB.RateRequestHolders.AddRateRequestHolders(newID, DB.Users.GetSuperiorUserString(CurrentUserID))

            If newID <> 0 Then
                DB.Comments.PostNewAirComment(newID, CurrentUserID, txtComment.Text)

                'Send mail to superior users
                Dim row As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
                SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A New Rate request has been generated by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & newID)
            Else
                Server.Transfer("Errors.aspx?Msg=There is an unknown error in posting your rate request. Please try again.", False)
            End If

            DB.RateRequestLog.PostRateRequestLog(newID, DB.RateRequestLog.RateRequestOperations.Generated, GetCurrentDateTime, CurrentUserID)

            DB.Logs.PostNewLog(EnumUserAction.NewRateRequest, GetCurrentDateTime)

            Response.Redirect("NewRateRequest.aspx?RateRequestID=" & newID, False)
        End If
    End Sub

    Private Sub UploadAttachmentsForRateRequest(ByVal RateRequestID As Integer)
        For i As Integer = 1 To 101
            Dim ctrlChkAttach = FindControl("chkAttachment_" & i)
            Dim chkAttachment As CheckBox = CType(ctrlChkAttach, CheckBox)
            If chkAttachment IsNot Nothing Then
                If chkAttachment.Checked = True Then
                    Dim ctrlFUpload = FindControl("fUpload_" & i)
                    If ctrlFUpload IsNot Nothing Then
                        Dim fUpload As FileUpload = CType(ctrlFUpload, FileUpload)
                        If fUpload.HasFile Then
                            fUpload.SaveAs(Server.MapPath("~\Attachments\RateRequestAttachment\") & fUpload.FileName)
                            DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID, fUpload.FileName, Server.MapPath("\Attachments\RateRequestAttachment\") & fUpload.FileName)
                        End If
                    Else
                        Exit For
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub EnableAllAttachmentCheckboxes()
        For i As Integer = 1 To 101
            Dim ctrlChkAttach = FindControl("chkAttachment_" & i)
            Dim chkAttachment As CheckBox = CType(ctrlChkAttach, CheckBox)
            If chkAttachment IsNot Nothing Then
                chkAttachment.Checked = True
            End If
        Next
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        If txtHAWBNumber.Enabled = True Then

            'If (DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A").Item(1).ToString.ToUpper <> "KATIE TRANG" And Now < CDate("13-Apr-2012 23:59:59")) Then
            Page.Validate()
            'End If


            If Page.IsValid Then ' Or (DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A").Item(1).ToString.ToUpper = "KATIE TRANG" And Now < CDate("13-Apr-2012 23:59:59")) Then
                Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
                If IsDate(txtShipDate.Text) = True Then
                    varShipDate = CDate(txtShipDate.Text)
                    txtShipDate.Text = CDate(txtShipDate.Text).ToString()
                Else
                    txtShipDate.Text = ""
                End If

                Dim varWeight As Double = StringToDouble(txtWeight.Text)
                Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
                Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
                Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

                DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

                DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))

                UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)
            End If
        End If

        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        If DB.AirRateRequests.IsRateRequestGenerated(qStrCurrentRateRequestID) = 0 Then
            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, GetCurrentDateTime, CurrentUserID)
        Else
            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, GetCurrentDateTime, CurrentUserID)
        End If

        If CurrentUserType = "Client" Then
            DB.AirRateRequests.ApproveRateRequestByClient(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, txtComment.Text, False)

            'DB.Tariffs.RateRequestToTariff(qStrCurrentRateRequestID, GetCurrentDateTime, GetCurrentDateTime)

            'Send mail to previous level users
            For Each row1 As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
                SendMail(row1("Email").ToString, "Hi " & row1("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.")
            Next

            'Send mail to request generator
            Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
            SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.")
        Else
            DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, DB.Users.GetSuperiorUserString(CurrentUserID))

            DB.AirRateRequests.ClearRejectedTag(qStrCurrentRateRequestID)

            'Send mail to supirior user
            Dim row As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
        End If

        'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Protected Sub btnNeedToReviseRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNeedToReviseRate.Click
        LoadDetails(qStrCurrentRateRequestID)

        EnableFields()

        BackupRates()

        lblRateGroupTitle.Text = "Changing rates will keep copy of older rates"

        btnNeedToReviseRate.Visible = False
        btnBackToDashboard.Visible = False
        btnPostNewRateRequest.Visible = False
        btnPostComment.Visible = False
        btnApprove.Visible = True
        btnSave.Visible = True
        Dim currentRateRequest = DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)
        If CInt(currentRateRequest("RequestorID")) <> CurrentUserID Then
            btnSendBackToReviseRate.Visible = True
        End If

        Dim currentRateRequestHolders = DB.RateRequestHolders.GetRateRequestHolders(qStrCurrentRateRequestID)
        If CurrentUserType = "Admin" Then
            btnNeedToReviseRate.Visible = False
            If currentRateRequestHolders.Contains(CurrentUserID) Then
                btnApprove.Visible = True
                btnSendBackToReviseRate.Visible = True
                btnTransferToTariff.Visible = True
            Else
                btnApprove.Visible = False
                btnSendBackToReviseRate.Visible = False
                btnTransferToTariff.Visible = False
            End If
        End If

        If currentRateRequestHolders.Contains(CurrentUserID) AndAlso CInt(currentRateRequest("RequestorID")) = CurrentUserID Then
            btnApprove.Text = "Post New Rate Request"
            btnApprove.Width = btnPostNewRateRequest.Width
        End If
    End Sub

    Protected Sub btnPostComment_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPostComment.Click
        If txtComment.Text.Trim() <> "" Then
            DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

            'Send mail to superior user
            Dim row As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", Comment has been posted on a Rate request by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

            Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)
        Else
            lblStatus.Text = "Please fill up comment box to post comment."
            lblStatus.ForeColor = Drawing.Color.Red
        End If

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)
    End Sub

    Protected Sub btnSendBackToReviseRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendBackToReviseRate.Click
        Page.Validate()

        If Page.IsValid Then
            Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
            If IsDate(txtShipDate.Text) = True Then
                varShipDate = CDate(txtShipDate.Text)
                txtShipDate.Text = CDate(txtShipDate.Text).ToString()
            Else
                txtShipDate.Text = ""
            End If

            Dim varWeight As Double = StringToDouble(txtWeight.Text)
            Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
            Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
            Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

            DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

            DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))

            UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)

            DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, DB.Users.GetSubordinateUserString(qStrCurrentRateRequestID, CurrentUserID))

            DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, GetCurrentDateTime, CurrentUserID)

            'Send mail to suoerior user 
            Dim row As DataRow = DB.Users.GetSubordinateUser(qStrCurrentRateRequestID, CurrentUserID)
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been sent back to revise by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

            'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

            DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

            Response.Redirect("Dashboard.aspx", False)
        End If
    End Sub

    Protected Sub btnReject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        Dim varWeight As Double = StringToDouble(txtWeight.Text)
        Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
        Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
        Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

        DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

        DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, DB.Users.GetRelatedUsersString(qStrCurrentRateRequestID, CurrentUserID))

        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, GetCurrentDateTime, CurrentUserID)

        DB.AirRateRequests.RejectRateRequestByClient(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime)

        UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)

        'Send mail to users in chain
        For Each row As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been rejected by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
        Next

        'Send mail to request generator
        Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
        SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been rejected by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

        'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Protected Sub btnBackToDashboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBackToDashboard.Click
        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Protected Sub btnApproveAsAdhoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveAsAdhoc.Click
        DB.AirRateRequests.ApproveRateRequestByClient(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, txtComment.Text, True)

        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, GetCurrentDateTime, CurrentUserID)

        UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)

        'Send mail to previous level users
        For Each row1 As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
            SendMail(row1("Email").ToString, "Hi " & row1("Name").ToString & ", A Rate request has been approved as adhoc by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
        Next

        'Send mail to request generator
        Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
        SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

        'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Protected Sub btnTransferToTariff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransferToTariff.Click
        Page.Validate()

        If Page.IsValid Then
            Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
            If IsDate(txtShipDate.Text) = True Then
                varShipDate = CDate(txtShipDate.Text)
                txtShipDate.Text = CDate(txtShipDate.Text).ToString()
            Else
                txtShipDate.Text = ""
            End If

            Dim varWeight As Double = StringToDouble(txtWeight.Text)
            Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
            Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
            Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

            DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

            DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))
        End If

        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, GetCurrentDateTime, CurrentUserID)

        DB.RateRequestHolders.RemoveRateRequestHolders(qStrCurrentRateRequestID)

        DB.Tariffs.RateRequestToTariff(qStrCurrentRateRequestID, GetCurrentDateTime, GetCurrentDateTime)

        'Send mail to admin user
        'Dim row As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
        'SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

        'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Private Sub cmbShipMethod_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShipMethod.DataBound
        Try
            'cmbShipMethod.Items.FindByText(lblShipMethod.SelectedItem.Text).Selected = True
        Catch
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        If txtHAWBNumber.Enabled = True Then
            Page.Validate()

            If Page.IsValid Then
                Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
                If IsDate(txtShipDate.Text) = True Then
                    varShipDate = CDate(txtShipDate.Text)
                    txtShipDate.Text = CDate(txtShipDate.Text).ToString()
                Else
                    txtShipDate.Text = ""
                End If

                Dim varWeight As Double = StringToDouble(txtWeight.Text)
                Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
                Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
                Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

                If qStrCurrentRateRequestID > 0 Then
                    DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

                    DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))

                    UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)

                    DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, GetCurrentDateTime, CurrentUserID)

                    'Send mail to previous level users
                    For Each prevUsersRows As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
                        SendMail(prevUsersRows("Email").ToString, "Hi " & prevUsersRows("Name").ToString & ", A Rate request has been revised by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
                    Next

                    'Send mail to request generator
                    Dim requestorRow As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
                    SendMail(requestorRow("Email").ToString, "Hi " & requestorRow("Name").ToString & ", A Rate request has been revised by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

                    'Send mail to supirior user
                    'Dim approverRow As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
                    'SendMail(approverRow("Email").ToString, "Hi " & approverRow("Name").ToString & ", A Rate request has been revised by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
                Else
                    Dim newID As Integer = DB.AirRateRequests.PostNewRateRequest(CurrentUserID, GetCurrentDateTime, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text)

                    UploadAttachmentsForRateRequest(newID)

                    Dim colCurrentUsers As New List(Of Integer)
                    colCurrentUsers.Add(CurrentUserID)

                    DB.RateRequestHolders.AddRateRequestHolders(newID, colCurrentUsers)
                End If


                'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)
                Response.Redirect("Dashboard.aspx", False)
            End If
        End If

        If qStrCurrentRateRequestID > 0 Then
            DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)
        End If

        'DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, DB.Users.GetSuperiorUserString(CurrentUserID))

        'If CurrentUserType = "Client" Then
        'DB.AirRateRequests.ApproveRateRequestByClient(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime)
    End Sub

    Private Sub btnRemoveAllComments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveAllComments.Click
        DB.AirRateRequests.RemoveAllComments(qStrCurrentRateRequestID)

        DB.Logs.PostNewLog(EnumUserAction.RemoveAllComments, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)
    End Sub

    Private Sub gridSimilarRateRequests_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridSimilarRateRequests.DataBound
        For Each row As GridViewRow In gridSimilarRateRequests.Rows
            row.Cells(0).Text = "<a href=NewRateRequest.aspx?RateRequestID=" & Server.UrlEncode(row.Cells(0).Text) & ">" & Server.UrlEncode(row.Cells(0).Text) & "</a>"
        Next
    End Sub

    Private Sub gridSimilarRateRequests_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSimilarRateRequests.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Private Sub gridSimilarTariffLanes_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridSimilarTariffLanes.DataBound
        For Each row As GridViewRow In gridSimilarTariffLanes.Rows
            row.Cells(0).Text = "<a href=Tariff.aspx?Customer=" & Server.UrlEncode(row.Cells(1).Text.Trim) & "&OriginAirport=" & Server.UrlEncode(row.Cells(2).Text.Trim) & "&DestAirport=" & Server.UrlEncode(row.Cells(3).Text.Trim) & "&DestCity=" & Server.UrlEncode(row.Cells(4).Text.Trim) & "&CEVATransitMode=" & Server.UrlEncode(row.Cells(5).Text.Trim) & "&ShipMethod=" & Server.UrlEncode(row.Cells(6).Text.Trim) & "&MinFreightRate=" & Server.UrlEncode(row.Cells(7).Text.Trim) & "&FreightRate=" & Server.UrlEncode(row.Cells(8).Text.Trim) & "&SecurityRate=" & Server.UrlEncode(row.Cells(9).Text.Trim) & ">" & row.Cells(0).Text & "</a>"
        Next
    End Sub

    Private Sub gridSimilarTariffLanes_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSimilarTariffLanes.RowCreated
        If e.Row.RowType <> DataControlRowType.EmptyDataRow Then
            With e.Row
                .Cells(2).Visible = False
                .Cells(3).Visible = False
                .Cells(4).Visible = False
                .Cells(5).Visible = False
                .Cells(6).Visible = False
                If .RowType = DataControlRowType.DataRow Then
                    .Cells(7).HorizontalAlign = HorizontalAlign.Right
                    .Cells(8).HorizontalAlign = HorizontalAlign.Right
                    .Cells(9).HorizontalAlign = HorizontalAlign.Right
                    .Cells(10).HorizontalAlign = HorizontalAlign.Right
                End If
            End With
        End If
    End Sub

    Private Sub gridSimilarAdhocLanes_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridSimilarAdhocLanes.DataBound
        For Each row As GridViewRow In gridSimilarAdhocLanes.Rows
            row.Cells(0).Text = "<a href=ApprovedAsAdhoc.aspx?Customer=" & Server.UrlEncode(row.Cells(1).Text.Trim) & "&OriginAirport=" & Server.UrlEncode(row.Cells(2).Text.Trim) & "&DestAirport=" & Server.UrlEncode(row.Cells(3).Text.Trim) & "&DestCity=" & Server.UrlEncode(row.Cells(4).Text.Trim) & "&CEVATransitMode=" & Server.UrlEncode(row.Cells(5).Text.Trim) & "&ShipMethod=" & Server.UrlEncode(row.Cells(6).Text.Trim) & "&MinFreightRate=" & Server.UrlEncode(row.Cells(7).Text.Trim) & "&FreightRate=" & Server.UrlEncode(row.Cells(8).Text.Trim) & "&SecurityRate=" & Server.UrlEncode(row.Cells(9).Text.Trim) & ">" & row.Cells(0).Text & "</a>"
        Next
    End Sub

    Private Sub gridSimilarAdhocLanes_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSimilarAdhocLanes.RowCreated
        If e.Row.RowType <> DataControlRowType.EmptyDataRow Then
            With e.Row
                .Cells(2).Visible = False
                .Cells(3).Visible = False
                .Cells(4).Visible = False
                .Cells(5).Visible = False
                .Cells(6).Visible = False
                If .RowType = DataControlRowType.DataRow Then
                    .Cells(7).HorizontalAlign = HorizontalAlign.Right
                    .Cells(8).HorizontalAlign = HorizontalAlign.Right
                    .Cells(9).HorizontalAlign = HorizontalAlign.Right
                    .Cells(10).HorizontalAlign = HorizontalAlign.Right
                End If
            End With
        End If
    End Sub

    Protected Sub btnRevoke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRevoke.Click
        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Revoked, GetCurrentDateTime, CurrentUserID)

        DB.AirRateRequests.RevokeRateRequest(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        'DB.Tariffs.RateRequestToTariff(qStrCurrentRateRequestID, GetCurrentDateTime, GetCurrentDateTime)

        'Send mail to previous level users
        For Each row1 As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
            SendMail(row1("Email").ToString, "Hi " & row1("Name").ToString & ", A Rate request has been pulled back by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & ".")
        Next

        'Send mail to request generator
        Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
        SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been pulled back by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & ".")

        DB.Logs.PostNewLog(EnumUserAction.RevokeRateRequest, GetCurrentDateTime)

        Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)
    End Sub

    Private Sub btnApprove_Permanent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove_Permanent.Click
        If txtHAWBNumber.Enabled = True Then
            'Page.Validate()

            'If Page.IsValid Then
            'If (DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A").Item(1).ToString.ToUpper <> "KATIE TRANG" And Now < CDate("13-Apr-2012 23:59:59")) Then
            Page.Validate()
            'End If


            If Page.IsValid Then 'Or (DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A").Item(1).ToString.ToUpper = "KATIE TRANG" And Now < CDate("13-Apr-2012 23:59:59")) Then
                Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
                If IsDate(txtShipDate.Text) = True Then
                    varShipDate = CDate(txtShipDate.Text)
                    txtShipDate.Text = CDate(txtShipDate.Text).ToString()
                Else
                    txtShipDate.Text = ""
                End If

                Dim varWeight As Double = StringToDouble(txtWeight.Text)
                Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
                Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
                Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

                DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

                DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))

                UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)
            End If
        End If

        DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

        If DB.AirRateRequests.IsRateRequestGenerated(qStrCurrentRateRequestID) = 0 Then
            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, GetCurrentDateTime, CurrentUserID)
        Else
            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, GetCurrentDateTime, CurrentUserID)
        End If

        If CurrentUserID = 3 Then
            DB.AirRateRequests.ApproveRateRequestByClient(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, txtComment.Text, False)

            'DB.Tariffs.RateRequestToTariff(qStrCurrentRateRequestID, GetCurrentDateTime, GetCurrentDateTime)

            'Send mail to previous level users
            For Each row1 As DataRow In DB.Users.GetRelatedUsers(qStrCurrentRateRequestID, CurrentUserID).Rows
                SendMail(row1("Email").ToString, "Hi " & row1("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.")
            Next

            'Send mail to request generator
            Dim row2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID(qStrCurrentRateRequestID)("RequestorID")))
            SendMail(row2("Email").ToString, "Hi " & row2("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.")
            'Else
            '    DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, DB.Users.GetSuperiorUserString(CurrentUserID))

            '    DB.AirRateRequests.ClearRejectedTag(qStrCurrentRateRequestID)

            '    'Send mail to supirior user
            '    Dim row As DataRow = DB.Users.GetSuperiorUser(CurrentUserID)
            '    SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been approved by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)
        End If

        'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

        DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

        Response.Redirect("Dashboard.aspx", False)

    End Sub

    Private Sub btnSendBackToRequestor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendBackToRequestor.Click
        Page.Validate()

        If Page.IsValid Then
            Dim varShipDate As Date = Data.SqlTypes.SqlDateTime.MinValue.Value
            If IsDate(txtShipDate.Text) = True Then
                varShipDate = CDate(txtShipDate.Text)
                txtShipDate.Text = CDate(txtShipDate.Text).ToString()
            Else
                txtShipDate.Text = ""
            End If

            Dim varWeight As Double = StringToDouble(txtWeight.Text)
            Dim varMinFreighRate As Double = StringToDouble(txtMinFreightRate.Text)
            Dim varFreighRate As Double = StringToDouble(txtFreightRate.Text)
            Dim varSecurityRate As Double = StringToDouble(txtSecurityRate.Text)

            'DB.AirRateRequestHistory.PostNewAirRateRequestHistory(qStrCurrentRateRequestID, CurrentUserID, GetCurrentDateTime, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text)

            'DB.AirRateRequests.UpdateRateRequest(qStrCurrentRateRequestID, txtHAWBNumber.Text, cmbShipMethod.SelectedValue, varShipDate, cmbServiceLevel.SelectedValue, cmbForwarderService.SelectedValue, varWeight, txtShipperName.Text, txtOriginAirport.Text, txtOriginCity.Text, cmbOriginRegion.SelectedValue, txtOriginZipCode.Text, txtConsigneeName.Text, txtDestAirport.Text, txtDestCity.Text, txtDestState.Text, cmbDestCountry.SelectedValue, cmbDestRegion.SelectedValue, txtDestZipCode.Text, txtForwarderZipcode.Text, cmbCEVATransitMode.SelectedValue, cmbDocType.SelectedValue, cmbCustomClearanceMode.SelectedValue, txtRateDeterMethodology.Text, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text, cmbFreightForwarder.SelectedValue, txtComment.Text, DB.Users.GetNextLevelUsersString(CurrentUserID))

            'UploadAttachmentsForRateRequest(qStrCurrentRateRequestID)
            Dim row As DataRow = DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A")

            Dim lst As New List(Of Integer)
            lst.Add(CInt(row("ID")))
            DB.RateRequestHolders.UpdateRateRequestHolders(qStrCurrentRateRequestID, lst)

            DB.Comments.PostNewAirComment(qStrCurrentRateRequestID, CurrentUserID, txtComment.Text)

            DB.RateRequestLog.PostRateRequestLog(qStrCurrentRateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, GetCurrentDateTime, CurrentUserID)

            'Send mail to superior user 
            'Dim row As DataRow = DB.Users.GetRateRequestor(qStrCurrentRateRequestID, "A")
            SendMail(row("Email").ToString, "Hi " & row("Name").ToString & ", A Rate request has been sent back to revise by " & CurrentUserName, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID)

            'Response.Redirect("NewRateRequest.aspx?RateRequestID=" & qStrCurrentRateRequestID, False)

            DB.Logs.PostNewLog(EnumUserAction.ReplyRateRequest, GetCurrentDateTime)

            Response.Redirect("Dashboard.aspx", False)
        End If
    End Sub

    Public Sub ConvertToUSD(ByVal lbl As Label, ByVal OtherCurr As String, ByVal Rate As Double, ByVal CurrType As String)
        If CurrType = "MYR" Then
            'lbl.Text = "(" + (Double.Parse(OtherCurr) * 1.9).ToString + " USD" + ")"
        ElseIf CurrType = "THB" Then
            'lbl.Text = "(" + (Double.Parse(OtherCurr) * 1.8).ToString + " USD" + ")"
        End If
    End Sub

    Public Function GetCurrency(ByVal org As String, ByVal dest As String, ByVal TransitMode As String) As String

        If (TransitMode.ToUpper = "GROUND / AIR") Then
            GetCurrency = "MYR"
        ElseIf (org = "BKK" AndAlso Not dest = "MAO" AndAlso Not dest = "VCP") Then
            GetCurrency = "THB"
        ElseIf (org = "KUL" AndAlso Not dest = "MAO" AndAlso Not dest = "VCP") Then
            GetCurrency = "MYR"
        ElseIf (org = "PEN" AndAlso Not dest = "MAO" AndAlso Not dest = "VCP") Then
            GetCurrency = "MYR"
        ElseIf (org = "JHB" AndAlso Not dest = "MAO" AndAlso Not dest = "VCP") Then
            GetCurrency = "MYR"
        Else
            GetCurrency = "USD"
        End If

    End Function


End Class
