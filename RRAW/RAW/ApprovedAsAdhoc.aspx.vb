Public Class ApprovedAsAdhoc
    Inherits System.Web.UI.Page

    Dim DT_AdhocTariff As DataTable

    Private Function GetCurrentDateTime() As DateTime
        Try
            Return Convert.ToDateTime(hidCurrentDateTime.Value)
        Catch
            Return Now
        End Try
    End Function

    'Private Sub ApprovedAsAdhoc_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
    '    Dim ctx As HttpContext = HttpContext.Current

    '    Dim ex As Exception = Server.GetLastError.GetBaseException

    '    Dim errorInfo As String = "<br>Offending URL: " + Request.Url.ToString() + "<br>Source: " + ex.Source + "<br>Message: " + ex.Message + "<br>Stack trace: " + ex.StackTrace

    '    Response.Write(errorInfo)

    '    ctx.Server.ClearError()

    '    MyBase.OnError(e)
    'End Sub

    Private Sub ApprovedAsAdhoc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtCustomer.Text = qStrCustomer
        txtOriginAirport.Text = qStrOriginAirport
        txtDestAirport.Text = qStrDestAirport
        txtDestCity.Text = qStrDestCity
        txtCEVATransitMode.Text = qStrCEVATransitMode
        txtShipMethod.Text = qStrShipMethod
        txtMinFreightRate.Text = qStrMinFreightRate
        txtFreightRate.Text = qStrFreightRate
        txtSecurityRate.Text = qStrSecurityRate
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Page.IsPostBack = False) Then
            IsAdhocLocalCurrency = 1
        End If

        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, "tariffexport", CurrentClientID) = True Then
            btnExportToCSV.Visible = True
            btnExportToExcel.Visible = True
        End If

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, "tariffoperations", CurrentClientID) = True Then
            btnRemoveRequest.Visible = True
            'btnTransferToTariff.Visible = True
            btnGenerateNewRateRequest.Visible = True
        End If

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource2.ConnectionString = CurrentDBConnection
        SqlDataSource3.ConnectionString = CurrentDBConnection

        ApplyFilter()

        If gridApprovedAsAdhoc.Rows.Count = 1 Then
            gridApprovedAsAdhoc.SelectedIndex = 0
            btnRemoveRequest.Enabled = True
            'btnTransferToTariff.Enabled = True
            btnGenerateNewRateRequest.Enabled = True
        End If

        If Page.IsPostBack = False Then
            DB.Logs.PostNewLog(EnumUserAction.SearchingRates)
        End If
    End Sub

    Private Sub ApplyFilter()
        'Dim varMinFreightRate As Double = CDbl(Val(txtMinFreightRate.Text))
        'Dim varFreighRate As Double = CDbl(Val(txtFreightRate.Text))
        'Dim varSecurityRate As Double = CDbl(Val(txtSecurityRate.Text))

        Dim varActive As Integer = cmbActive.SelectedIndex - 1

        If IsDate(txtEffectiveDate.Text) = True Then
            txtEffectiveDate.Text = CDate(txtEffectiveDate.Text).ToString("MM/dd/yyyy")
        Else
            txtEffectiveDate.Text = ""
        End If

        If IsDate(txtApprovalDateFrom.Text) = True Then
            txtApprovalDateFrom.Text = CDate(txtApprovalDateFrom.Text).ToString("MM/dd/yyyy")
        Else
            txtApprovalDateFrom.Text = ""
        End If

        If IsDate(txtApprovalDateTo.Text) = True Then
            txtApprovalDateTo.Text = CDate(txtApprovalDateTo.Text).ToString("MM/dd/yyyy")
        Else
            txtApprovalDateTo.Text = ""
        End If

        If IsDate(txtRateRequestDate.Text) = True Then
            txtRateRequestDate.Text = CDate(txtRateRequestDate.Text).ToString("MM/dd/yyyy")
        Else
            txtRateRequestDate.Text = ""
        End If

        If IsDate(txtEntryDate.Text) = True Then
            txtEntryDate.Text = CDate(txtEntryDate.Text).ToString("MM/dd/yyyy")
        Else
            txtEntryDate.Text = ""
        End If

        DT_AdhocTariff = DB.AirRateRequests.GetApprovedAsAdhocRateRequests(txtHAWBNumber.Text.Trim, txtRateRequestID.Text.Trim, txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text, txtFreightRate.Text, txtSecurityRate.Text, txtOtherCharges.Text.Trim, txtEffectiveDate.Text, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, txtApprovalDateFrom.Text, txtApprovalDateTo.Text, txtApprovedBy.Text.Trim, txtRateRequestDate.Text, txtEntryDate.Text, IsAdhocLocalCurrency)
        gridApprovedAsAdhoc.DataSource = DT_AdhocTariff
        gridApprovedAsAdhoc.DataBind()

        'SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.Text
        'SqlDataSource1.SelectCommand = DB.AirRateRequests.GetApprovedAsAdhocRateRequestsQuery(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, varMinFreightRate.ToString, varFreighRate.ToString, varSecurityRate.ToString, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, txtApprovedBy.Text.Trim, varRateRequestDate, varEntryDate)
        'SqlDataSource1.DataBind()

        If gridApprovedAsAdhoc.Rows.Count = 0 Then
            DT_AdhocTariff = DB.Tariffs.GetAirTariff(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text.Trim, txtFreightRate.Text.Trim, txtSecurityRate.Text.Trim, txtOtherCharges.Text.Trim, txtEffectiveDate.Text, "", txtFreightForwarder.Text.Trim, varActive, txtNotes.Text.Trim, txtApprovalDateFrom.Text, txtApprovalDateTo.Text, txtApprovedBy.Text.Trim, txtApprovalNotes.Text.Trim, txtAdditionalNotes.Text.Trim, txtRateRequestDate.Text, txtEntryDate.Text, IsAdhocLocalCurrency)
            gridTariff.DataSource = DT_AdhocTariff
            gridTariff.DataBind()

            'SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.Text
            'SqlDataSource3.SelectCommand = DB.Tariffs.GetTariffQuery(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text.Trim, txtFreightRate.Text.Trim, txtSecurityRate.Text.Trim, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, varActive, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, txtApprovedBy.Text.Trim, txtApprovalNotes.Text.Trim, txtAdditionalNotes.Text.Trim, varRateRequestDate, varEntryDate)
            'SqlDataSource3.DataBind()

            If gridTariff.Rows.Count > 0 Then
                lblTariffLaneFound.Visible = True

                lblPages.Text = "Pages: " & (gridTariff.PageIndex + 1).ToString & "/" & gridTariff.PageCount.ToString
                lblRecords.Text = "Total Records: " & DB.Tariffs.GetTariffRecordCount(SqlDataSource3.SelectCommand)
            Else
                lblLaneNotFoundInRRAW.Visible = True
                linkPostNewRateRequest.Visible = True
            End If
        Else
            lblPages.Text = "Pages: " & (gridApprovedAsAdhoc.PageIndex + 1).ToString & "/" & gridApprovedAsAdhoc.PageCount.ToString
            lblRecords.Text = "Total Records: " & DB.AirRateRequests.GetApprovedAsAdhocRateRequestRecordCount(SqlDataSource1.SelectCommand)
        End If

        'If gridApprovedAsAdhoc.Rows.Count = 0 Then
        '    If (txtOriginAirport.Text.Trim <> "") Or (txtOriginRegion.Text.Trim <> "") Or (txtOriginCode.Text.Trim <> "") Or (txtDestAirport.Text.Trim <> "") Or (txtDestCity.Text.Trim <> "") Or (txtDestState.Text.Trim <> "") Or (txtDestCountry.Text.Trim <> "") Or (txtDestRegion.Text.Trim <> "") Or (txtDestZipcode.Text.Trim <> "") Or (txtCEVATransitMode.Text.Trim <> "") Or (txtShipMethod.Text.Trim <> "") Or (txtForwarderZipcode.Text.Trim <> "") Or (txtCustomClearanceMode.Text.Trim <> "") Or (txtForwarderServiceLevel.Text.Trim <> "") Or (varMinFreighRate > 0) Or (varFreighRate > 0) Or (varSecurityRate > 0) Or (txtOtherCharges.Text.Trim <> "") Or (txtEffectiveDate.Text.Trim <> "") Or (txtExpiryDate.Text.Trim <> "") Or (txtFreightForwarder.Text.Trim <> "") Or (txtNotes.Text.Trim <> "") Or (txtRateRequestDate.Text.Trim <> "") Then
        '        SqlDataSource2.SelectCommandType = SqlDataSourceCommandType.Text
        '        SqlDataSource2.SelectCommand = DB.AirRateRequests.GetAdhocLaneQuery(txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, varRateRequestDate)
        '        SqlDataSource2.DataBind()
        '        gridAdhocLanes.DataBind()
        '        If gridAdhocLanes.Rows.Count > 0 Then
        '            lblAdhocLaneFound.Visible = True
        '            gridAdhocLanes.Visible = True
        '        Else
        '            lblAdhocLaneFound.Visible = False
        '            gridAdhocLanes.Visible = False
        '        End If
        '    End If
        '    lblLaneNotFoundMessage.Visible = True
        '    linkPostNewRateRequest.Visible = True
        'Else
        '    gridAdhocLanes.Visible = False

        '    lblLaneNotFoundMessage.Visible = False
        '    linkPostNewRateRequest.Visible = False
        'End If

        'gridAdhocLanes.SelectedIndex = -1
    End Sub

    'Protected Sub linkPostNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkPostNewRateRequest.Click
    '    'Response.Redirect("NewRateRequest.aspx?OriginAirport=" & txtOriginAirport.Text & "&DestAirport=" & txtDestAirport.Text & "&DestCity=" & txtDestCity.Text & "&DestState=" & txtDestState.Text & "&DestCountry=" & txtDestCountry.Text & "&DestZipcode=" & txtDestZipcode.Text & "&ServiceLevel=" & txtServiceLevel.Text & "&ServiceLevelDesc=" & txtServiceLevelDesc.Text)
    '    Response.Redirect("NewRateRequest.aspx?OriginAirport=" & txtOriginAirport.Text & "&DestAirport=" & txtDestAirport.Text & "&DestCity=" & txtDestCity.Text & "&DestCountry=" & txtDestCountry.Text & "&DestZipcode=" & txtDestZipcode.Text)
    'End Sub

    Private Sub gridTariff_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Width = 210
            e.Row.Cells(1).Width = 60
            e.Row.Cells(2).Width = 60
            e.Row.Cells(3).Width = 60
            e.Row.Cells(4).Width = 60
            e.Row.Cells(5).Width = 150
            e.Row.Cells(6).Width = 60
            e.Row.Cells(7).Width = 60
            e.Row.Cells(8).Width = 100
            e.Row.Cells(9).Width = 60
            e.Row.Cells(10).Width = 100
            e.Row.Cells(11).Width = 200
            e.Row.Cells(12).Width = 70
            e.Row.Cells(13).Width = 70
            e.Row.Cells(14).Width = 70
            e.Row.Cells(15).Width = 60
            e.Row.Cells(16).Width = 60
            e.Row.Cells(17).Width = 60
            e.Row.Cells(18).Width = 100
            e.Row.Cells(19).Width = 60
            e.Row.Cells(20).Width = 70
            e.Row.Cells(21).Width = 70
            e.Row.Cells(22).Width = 60
            e.Row.Cells(23).Width = 300
            e.Row.Cells(24).Width = 70
            e.Row.Cells(25).Width = 100
            e.Row.Cells(26).Width = 300
            e.Row.Cells(27).Width = 300
            e.Row.Cells(28).Width = 70
            e.Row.Cells(29).Width = 70

            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(14).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(17).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(18).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(19).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(20).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(22).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(24).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(28).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(29).HorizontalAlign = HorizontalAlign.Center
        End If
    End Sub

    Private Sub gridApprovedAsAdhoc_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridApprovedAsAdhoc.PageIndexChanging
        gridApprovedAsAdhoc.PageIndex = e.NewPageIndex
        gridApprovedAsAdhoc.DataBind()

        lblPages.Text = "Pages: " & (gridApprovedAsAdhoc.PageIndex + 1).ToString & "/" & gridApprovedAsAdhoc.PageCount.ToString
        lblRecords.Text = "Total Records: " & DB.AirRateRequests.GetApprovedAsAdhocRateRequestRecordCount(SqlDataSource1.SelectCommand)
    End Sub

    Private Sub gridApprovedAsAdhoc_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridApprovedAsAdhoc.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(1).Style.Add("display", "none")
        End If
        '#DFF9FF
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(1).Style.Add("display", "none")

            e.Row.Cells(1).Width = 100
            e.Row.Cells(2).Width = 60
            e.Row.Cells(3).Width = 160
            e.Row.Cells(4).Width = 60
            e.Row.Cells(5).Width = 60
            e.Row.Cells(6).Width = 60
            e.Row.Cells(7).Width = 60
            e.Row.Cells(8).Width = 150
            e.Row.Cells(9).Width = 60
            e.Row.Cells(10).Width = 60
            e.Row.Cells(11).Width = 100
            e.Row.Cells(12).Width = 60
            e.Row.Cells(13).Width = 100
            e.Row.Cells(14).Width = 200
            e.Row.Cells(15).Width = 70
            e.Row.Cells(16).Width = 70
            e.Row.Cells(17).Width = 70 '60
            e.Row.Cells(18).Width = 100 '70
            e.Row.Cells(19).Width = 100 '60
            e.Row.Cells(20).Width = 100 '60
            e.Row.Cells(21).Width = 100
            e.Row.Cells(22).Width = 70 '60
            'e.Row.Cells(23).Width = 70
            e.Row.Cells(23).Width = 60 '70
            e.Row.Cells(24).Width = 60
            e.Row.Cells(25).Width = 300
            e.Row.Cells(26).Width = 145
            e.Row.Cells(27).Width = 100
            e.Row.Cells(28).Width = 300
            e.Row.Cells(29).Width = 300
            e.Row.Cells(30).Width = 70
            e.Row.Cells(31).Width = 70

            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(17).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(18).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(19).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(20).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(21).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(22).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(23).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(24).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(26).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(30).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(31).HorizontalAlign = HorizontalAlign.Center
        End If
    End Sub

    Private Sub gridApprovedAsAdhoc_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gridApprovedAsAdhoc.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Display the company name in italics.
            e.Row.Cells(2).Text = "<a href='NewRateRequest.aspx?RateRequestID=" & e.Row.Cells(2).Text & "'>" & e.Row.Cells(2).Text & "</a>"
        End If
    End Sub


    Protected Sub btnExportToCSV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToCSV.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)

        'CurrentSelectCommand = SqlDataSource1.SelectCommand
        'Response.Redirect("ExportFile.aspx?isStoredProcedure=TRUE&fileNamePrefix=ApprovedAsAdhoc&FromDate=1/1/1753&ToDate=12/31/9999")
        ''Response.Redirect(Page.ResolveUrl(General.CreateCSV(SqlDataSource1.SelectCommand, False, "ApprovedAsAdhoc")))

        ExportFile(DT_AdhocTariff, "CSV", "AdhocAirTariff")
    End Sub

    Protected Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)

        'CurrentSelectCommand = SqlDataSource1.SelectCommand
        'Response.Redirect("ExportFile.aspx?isStoredProcedure=TRUE&fileNamePrefix=ApprovedAsAdhoc&fileType=Excel&FromDate=1/1/1753&ToDate=12/31/9999")
        ''Response.Redirect(Page.ResolveUrl(General.CreateExcel(SqlDataSource1.SelectCommand, "ApprovedAsAdhoc")))

        ExportFile(DT_AdhocTariff, "Excel", "AdhocAirTariff")
    End Sub

    Public Sub ExportFile(ByVal FileTable As DataTable, ByVal FileType As String, ByVal fileNamePrefix As String)
        Dim fileName As String = fileNamePrefix & "_" & Now.ToString("yyyyMMddHHmmss") & If(FileType = "Excel", ".xls", ".csv")
        Dim csvContent As New StringBuilder
        Dim fieldSeparator As String
        Try
            Response.Clear()

            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)

            If FileType = "Excel" Then
                fieldSeparator = vbTab
                Response.ContentType = "application/ms-excel"
            Else
                fieldSeparator = ","
                Response.ContentType = "application/octet-stream"
            End If

            For Each col As DataColumn In FileTable.Columns
                csvContent.Append(col.ColumnName & fieldSeparator)
            Next
            csvContent.Remove(csvContent.Length - 1, 1)
            csvContent.AppendLine()

            For i = 0 To FileTable.Rows.Count - 1
                For j = 0 To FileTable.Columns.Count - 1
                    csvContent.Append("""" & FileTable.Rows(i).Item(j).ToString & """" & fieldSeparator)
                Next
                csvContent.Remove(csvContent.Length - 1, 1)
                csvContent.AppendLine()
            Next

            Dim fileContent As String = csvContent.ToString

            Dim Encoding As New System.Text.UnicodeEncoding
            Response.AddHeader("Content-Length", Encoding.GetByteCount(fileContent).ToString())
            Response.BinaryWrite(Encoding.GetBytes(fileContent))
            Response.Charset = "iso-8859-2"
        Catch
            Throw
        End Try
    End Sub

    Protected Sub linkPostNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkPostNewRateRequest.Click
        Dim responseString As New StringBuilder

        If txtOriginAirport.Text.Trim() <> "" Then
            responseString.Append("OriginAirport=" & txtOriginAirport.Text)
        End If

        If txtOriginRegion.Text.Trim() <> "" Then
            responseString.Append("&OriginRegion=" & txtOriginRegion.Text.Trim())
        End If

        If txtDestAirport.Text.Trim() <> "" Then
            responseString.Append("&DestAirport=" & txtDestAirport.Text.Trim())
        End If

        If txtDestCity.Text.Trim() <> "" Then
            responseString.Append("&DestCity=" & txtDestCity.Text.Trim())
        End If

        If txtDestState.Text.Trim() <> "" Then
            responseString.Append("&DestState=" & txtDestState.Text.Trim())
        End If

        If txtDestCountry.Text.Trim() <> "" Then
            responseString.Append("&DestCountry=" & txtDestCountry.Text.Trim())
        End If

        If txtDestRegion.Text.Trim() <> "" Then
            responseString.Append("&DestRegion=" & txtDestRegion.Text.Trim())
        End If

        If txtDestZipcode.Text.Trim() <> "" Then
            responseString.Append("&DestZipcode=" & txtDestZipcode.Text.Trim())
        End If

        If txtCEVATransitMode.Text.Trim() <> "" Then
            responseString.Append("&CEVATransitMode=" & txtCEVATransitMode.Text.Trim())
        End If

        If txtShipMethod.Text.Trim() <> "" Then
            responseString.Append("&ShipMethod=" & txtShipMethod.Text.Trim())
        End If

        If txtForwarderZipcode.Text.Trim() <> "" Then
            responseString.Append("&ForwarderZipcode=" & txtForwarderZipcode.Text.Trim())
        End If

        If txtCustomClearanceMode.Text.Trim() <> "" Then
            responseString.Append("&CustomClearanceMode=" & txtCustomClearanceMode.Text.Trim())
        End If

        If txtForwarderServiceLevel.Text.Trim() <> "" Then
            responseString.Append("&ForwarderServiceLevel=" & txtForwarderServiceLevel.Text.Trim())
        End If

        If txtMinFreightRate.Text.Trim() <> "" Then
            responseString.Append("&MinFreightRate=" & txtMinFreightRate.Text.Trim())
        End If

        If txtFreightRate.Text.Trim() <> "" Then
            responseString.Append("&FreightRate=" & txtFreightRate.Text.Trim())
        End If

        If txtSecurityRate.Text.Trim() <> "" Then
            responseString.Append("&SecurityRate=" & txtSecurityRate.Text.Trim())
        End If

        If txtOtherCharges.Text.Trim() <> "" Then
            responseString.Append("&OtherCharges=" & txtOtherCharges.Text.Trim())
        End If

        If txtFreightForwarder.Text.Trim() <> "" Then
            responseString.Append("&FreightForwarder=" & txtFreightForwarder.Text.Trim())
        End If

        Response.Redirect("NewRateRequest.aspx?" & responseString.ToString)
    End Sub

    Private Function extractIdentifier(ByVal sender As Object) As String
        Return CType(sender, ImageButton).ID.Remove(0, 9)
    End Function

    Protected Sub btnRemoveRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveRequest.Click
        Dim RateRequestID As Integer
        If gridApprovedAsAdhoc.SelectedIndex >= 0 Then
            RateRequestID = Convert.ToInt32(gridApprovedAsAdhoc.Rows(gridApprovedAsAdhoc.SelectedIndex).Cells(1).Text)

            DB.AirRateRequests.RemoveRateRequest(RateRequestID)

            gridApprovedAsAdhoc.Visible = False
            lblRateRequestRemoved.Visible = True
            btnRefresh.Visible = True
        End If
    End Sub

    Protected Sub btnTransferToTariff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransferToTariff.Click
        Dim RateRequestID As Integer
        If gridApprovedAsAdhoc.SelectedIndex >= 0 Then
            RateRequestID = Convert.ToInt32(gridApprovedAsAdhoc.Rows(gridApprovedAsAdhoc.SelectedIndex).Cells(1).Text)

            DB.AirRateRequests.ResetAdhocRateRequest(RateRequestID)
            DB.Tariffs.RateRequestToTariff(RateRequestID, GetCurrentDateTime, GetCurrentDateTime)

            gridApprovedAsAdhoc.Visible = False
            lblTransferedToTariff.Visible = True
            btnTariff.Visible = True
        End If
    End Sub

    Protected Sub btnGenerateNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerateNewRateRequest.Click
        Dim RateRequestID As Integer
        If gridApprovedAsAdhoc.SelectedIndex >= 0 Then
            RateRequestID = Convert.ToInt32(gridApprovedAsAdhoc.Rows(gridApprovedAsAdhoc.SelectedIndex).Cells(1).Text)

            GenerateNewRateRequest()
        End If
    End Sub

    Private Sub GenerateNewRateRequest()
        Dim responseString As New StringBuilder
        Dim currentRow As GridViewRow = gridApprovedAsAdhoc.Rows(gridApprovedAsAdhoc.SelectedIndex)

        If currentRow.Cells(3).Text <> "" Then
            responseString.Append("OriginAirport=" & currentRow.Cells(3).Text)
        End If

        If currentRow.Cells(4).Text <> "" Then
            responseString.Append("&OriginRegion=" & currentRow.Cells(4).Text)
        End If

        If currentRow.Cells(6).Text <> "" Then
            responseString.Append("&DestAirport=" & currentRow.Cells(6).Text)
        End If

        If currentRow.Cells(7).Text <> "" Then
            responseString.Append("&DestCity=" & currentRow.Cells(7).Text)
        End If

        If currentRow.Cells(8).Text <> "" Then
            responseString.Append("&DestState=" & currentRow.Cells(8).Text)
        End If

        If currentRow.Cells(9).Text <> "" Then
            responseString.Append("&DestCountry=" & currentRow.Cells(9).Text)
        End If

        If currentRow.Cells(10).Text <> "" Then
            responseString.Append("&DestRegion=" & currentRow.Cells(10).Text)
        End If

        If currentRow.Cells(11).Text <> "" Then
            responseString.Append("&DestZipcode=" & currentRow.Cells(11).Text)
        End If

        If currentRow.Cells(12).Text <> "" Then
            responseString.Append("&CEVATransitMode=" & currentRow.Cells(12).Text)
        End If

        If currentRow.Cells(13).Text <> "" Then
            responseString.Append("&ShipMethod=" & currentRow.Cells(13).Text)
        End If

        If currentRow.Cells(14).Text <> "" Then
            responseString.Append("&ForwarderZipcode=" & currentRow.Cells(14).Text)
        End If

        If currentRow.Cells(15).Text <> "" Then
            responseString.Append("&CustomClearanceMode=" & currentRow.Cells(15).Text)
        End If

        If currentRow.Cells(16).Text <> "" Then
            responseString.Append("&ForwarderServiceLevel=" & currentRow.Cells(16).Text)
        End If

        If currentRow.Cells(17).Text <> "" Then
            responseString.Append("&MinFreightRate=" & currentRow.Cells(17).Text)
        End If

        If currentRow.Cells(18).Text <> "" Then
            responseString.Append("&FreightRate=" & currentRow.Cells(18).Text)
        End If

        If currentRow.Cells(19).Text <> "" Then
            responseString.Append("&SecurityRate=" & currentRow.Cells(19).Text)
        End If

        If currentRow.Cells(20).Text <> "" Then
            responseString.Append("&OtherCharges=" & currentRow.Cells(20).Text)
        End If

        If currentRow.Cells(23).Text <> "" Then
            responseString.Append("&FreightForwarder=" & currentRow.Cells(23).Text)
        End If

        Response.Redirect("NewRateRequest.aspx?" & responseString.ToString)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Response.Redirect("ApprovedAsAdhoc.aspx", False)
    End Sub

    Private Sub btnTariff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTariff.Click
        Response.Redirect("Tariff.aspx", False)
    End Sub

    Private Sub gridApprovedAsAdhoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridApprovedAsAdhoc.SelectedIndexChanged
        If gridApprovedAsAdhoc.SelectedIndex >= 0 Then
            btnRemoveRequest.Enabled = True
            'btnTransferToTariff.Enabled = True
            btnGenerateNewRateRequest.Enabled = True
        End If
    End Sub

    Private ReadOnly Property qStrCustomer() As String
        Get
            Return Server.UrlDecode(Request.QueryString("Customer"))
        End Get
    End Property

    Private ReadOnly Property qStrOriginAirport() As String
        Get
            Return Server.UrlDecode(Request.QueryString("OriginAirport"))
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

    Private Sub btnConvertCurrency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConvertCurrency.Click
        If IsAdhocLocalCurrency = 0 Then
            IsAdhocLocalCurrency = 1
            'btnConvertCurrency.Text = "Convert Currency(Local)"
        Else
            IsAdhocLocalCurrency = 0
            'btnConvertCurrency.Text = "Convert Currency(USD)"
        End If

        ApplyFilter()
    End Sub
End Class