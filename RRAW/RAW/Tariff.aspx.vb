Public Class Tariff
    Inherits System.Web.UI.Page

    Dim DT_Tariff As DataTable
    Dim DT_ExportTariff As DataTable

    Private Sub Tariff_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        'Dim ctx As HttpContext = HttpContext.Current

        Dim ex As Exception = Server.GetLastError.GetBaseException

        Dim errorInfo As String = "<br>Offending URL: " + Request.Url.ToString() + "<br>Source: " + ex.Source + "<br>Message: " + ex.Message + "<br>Stack trace: " + ex.StackTrace

        Response.Write(errorInfo)

        'ctx.Server.ClearError()

        MyBase.OnError(e)
    End Sub

    Private Sub Tariff_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtCustomer.Text = qStrCustomer
        txtOriginAirport.Text = qStrOriginAirport
        txtDestAirport.Text = qStrDestAirport
        txtDestCity.Text = qStrDestCity
        txtCEVATransitMode.Text = qStrCEVATransitMode
        txtShipMethod.Text = qStrShipMethod
        txtMinFreightRate.Text = qStrMinFreightRate
        txtFreightRate.Text = qStrFreightRate
        txtSecurityRate.Text = qStrSecurityRate
        If Request.QueryString("Active") = "1" Then
            cmbActive.SelectedIndex = 1
            cmbActive.Enabled = False
        ElseIf Request.QueryString("Active") = "2" Then
            cmbActive.SelectedIndex = 2
            cmbActive.Enabled = False
        Else
            cmbActive.SelectedIndex = 0
            cmbActive.Enabled = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (IsPostBack = False) Then
            IsLocalCurrency = 1
        End If
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, "tariffexport", CurrentClientID) = True Then
            btnExportToCSV.Visible = True
            btnExportToExcel.Visible = True
        End If

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource2.ConnectionString = CurrentDBConnection

        ApplyFilter()

        If Page.IsPostBack = False Then
            DB.Logs.PostNewLog(EnumUserAction.SearchingRates)

        End If
    End Sub

    Private Sub ApplyFilter()
        'Dim varMinFreighRate As Double = CDbl(Val(txtMinFreightRate.Text))
        'Dim varFreighRate As Double = CDbl(Val(txtFreightRate.Text))
        'Dim varSecurityRate As Double = CDbl(Val(txtSecurityRate.Text))

        Dim varActive As Integer = cmbActive.SelectedIndex - 1

        If IsDate(txtEffectiveDate.Text) = True Then
            txtEffectiveDate.Text = CDate(txtEffectiveDate.Text).ToString("MM/dd/yyyy")
        Else
            txtEffectiveDate.Text = ""
        End If

        If IsDate(txtExpiryDate.Text) = True Then
            txtExpiryDate.Text = CDate(txtExpiryDate.Text).ToString("MM/dd/yyyy")
        Else
            txtExpiryDate.Text = ""
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

        DT_Tariff = DB.Tariffs.GetAirTariff(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text.Trim, txtFreightRate.Text.Trim, txtSecurityRate.Text.Trim, txtOtherCharges.Text.Trim, txtEffectiveDate.Text, txtExpiryDate.Text, txtFreightForwarder.Text.Trim, varActive, txtNotes.Text.Trim, txtApprovalDateFrom.Text, txtApprovalDateTo.Text, txtApprovedBy.Text.Trim, txtApprovalNotes.Text.Trim, txtAdditionalNotes.Text.Trim, txtRateRequestDate.Text, txtEntryDate.Text, IsLocalCurrency)
        DT_ExportTariff = DT_Tariff.Copy
        DT_ExportTariff.Columns.Remove("Origin Code")
        DT_ExportTariff.Columns.Remove("Other Charges")
        DT_ExportTariff.Columns.Remove("Entry Date")
        DT_ExportTariff.Columns.Remove("Freight Forwarder")
        DT_ExportTariff.Columns.Remove("ActiveBool")
        DT_ExportTariff.Columns.Remove("Rate Request Date")

        gridTariff.DataSource = DT_Tariff
        gridTariff.DataBind()

        lblRecords.Text = "Total Records: " & DT_Tariff.Rows.Count

        'SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.Text
        'SqlDataSource1.SelectCommand = DB.Tariffs.GetTariffQuery(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text.Trim, txtFreightRate.Text.Trim, txtSecurityRate.Text.Trim, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, varActive, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, txtApprovedBy.Text.Trim, txtApprovalNotes.Text.Trim, txtAdditionalNotes.Text.Trim, varRateRequestDate, varEntryDate)
        'SqlDataSource1.DataBind()

        If gridTariff.Rows.Count = 0 Then
            'gridAdhocLanes.DataSource = DB.AirRateRequests.GetApprovedAsAdhocRateRequests(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text, txtFreightRate.Text, txtSecurityRate.Text, txtOtherCharges.Text.Trim, txtEffectiveDate.Text, txtExpiryDate.Text, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, txtApprovalDateFrom.Text, txtApprovalDateTo.Text, txtApprovedBy.Text.Trim, txtRateRequestDate.Text, txtEntryDate.Text)
            'gridAdhocLanes.DataBind()

            'SqlDataSource2.SelectCommandType = SqlDataSourceCommandType.Text
            ''SqlDataSource2.SelectCommand = DB.AirRateRequests.GetAdhocLaneQuery(txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, varMinFreighRate, varFreighRate, varSecurityRate, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, varRateRequestDate)
            'SqlDataSource2.SelectCommand = DB.AirRateRequests.GetApprovedAsAdhocRateRequestsQuery(txtCustomer.Text.Trim, txtOriginAirport.Text.Trim, txtOriginRegion.Text.Trim, txtOriginCode.Text.Trim, txtDestAirport.Text.Trim, txtDestCity.Text.Trim, txtDestState.Text.Trim, txtDestCountry.Text.Trim, txtDestRegion.Text.Trim, txtDestZipcode.Text.Trim, txtCEVATransitMode.Text.Trim, txtShipMethod.Text.Trim, txtForwarderZipcode.Text.Trim, txtCustomClearanceMode.Text.Trim, txtForwarderServiceLevel.Text.Trim, txtMinFreightRate.Text.Trim, txtFreightRate.Text.Trim, txtSecurityRate.Text.Trim, txtOtherCharges.Text.Trim, varEffectiveDate, varExpiryDate, txtFreightForwarder.Text.Trim, txtNotes.Text.Trim, varApprovalDateFrom, varApprovalDateTo, txtApprovedBy.Text.Trim, varRateRequestDate, varEntryDate)
            'SqlDataSource2.DataBind()
            'If gridAdhocLanes.Rows.Count > 0 Then
            'lblAdhocLaneFound.Visible = True
            'gridAdhocLanes.Visible = True
            'Else
            lblAdhocLaneFound.Visible = False
            gridAdhocLanes.Visible = False
            'End If
            lblLaneNotFoundMessage.Visible = True
            linkPostNewRateRequest.Visible = True
        Else
            gridAdhocLanes.Visible = False

            lblLaneNotFoundMessage.Visible = False
            linkPostNewRateRequest.Visible = False
        End If
    End Sub

    Private Sub gridTariff_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridTariff.DataBound
        lblPages.Text = "Pages: " & (gridTariff.PageIndex + 1).ToString & "/" & gridTariff.PageCount.ToString
        'lblRecords.Text = "Total Records: " & DB.Tariffs.GetTariffRecordCount(SqlDataSource1.SelectCommand)
    End Sub

    Private Sub gridTariff_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTariff.PageIndexChanging
        gridTariff.PageIndex = e.NewPageIndex
        gridTariff.DataBind()
    End Sub

    'Protected Sub linkPostNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkPostNewRateRequest.Click
    '    'Response.Redirect("NewRateRequest.aspx?OriginAirport=" & txtOriginAirport.Text & "&DestAirport=" & txtDestAirport.Text & "&DestCity=" & txtDestCity.Text & "&DestState=" & txtDestState.Text & "&DestCountry=" & txtDestCountry.Text & "&DestZipcode=" & txtDestZipcode.Text & "&ServiceLevel=" & txtServiceLevel.Text & "&ServiceLevelDesc=" & txtServiceLevelDesc.Text)
    '    Response.Redirect("NewRateRequest.aspx?OriginAirport=" & txtOriginAirport.Text & "&DestAirport=" & txtDestAirport.Text & "&DestCity=" & txtDestCity.Text & "&DestCountry=" & txtDestCountry.Text & "&DestZipcode=" & txtDestZipcode.Text)
    'End Sub

    Private Sub gridTariff_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Width = 160
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

        'If e.Row.RowType = DataControlRowType.Header Then
        '    Dim gvRow As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
        '    Dim tableCell As TableCell = New TableHeaderCell()

        '    tableCell.HorizontalAlign = HorizontalAlign.Right
        '    'tableCell.ColumnSpan = this.Columns.Count
        '    'tableCell.MergeStyle(this.FilterStyle)

        '    'Add the search textbox
        '    Dim searchBox As New TextBox()
        '    tableCell.Controls.Add(searchBox)
        '    searchBox.ID = "txtSearch"

        '    searchBox.AutoPostBack = True
        '    AddHandler searchBox.TextChanged, AddressOf ProcessGridRequest

        '    gvRow.Cells.Add(tableCell)

        '    Dim tbl As Table = CType(Controls(0), Table)
        '    tbl.Rows.AddAt(0, gvRow)
        'End If
    End Sub

    Private Sub ProcessGridRequest(ByVal sender As Object, ByVal e As EventArgs)
        txtCustomer.Text = "Dharmesh"
    End Sub

    Protected Sub btnExportToCSV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToCSV.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)

        'CurrentSelectCommand = DB.Tariffs.ConvertToWDTariff(SqlDataSource1.SelectCommand)
        'Response.Redirect("ExportFile.aspx?isStoredProcedure=FALSE&fileNamePrefix=Tariff&FromDate=1/1/1753&ToDate=12/31/9999")
        ''Response.Redirect(Page.ResolveUrl(General.CreateCSV(SqlDataSource1.SelectCommand, False, "Tariff")))

        ExportFile(DT_ExportTariff, "CSV", "AirTariff")
    End Sub

    Protected Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)

        'CurrentSelectCommand = DB.Tariffs.ConvertToWDTariff(SqlDataSource1.SelectCommand)
        'Response.Redirect("ExportFile.aspx?isStoredProcedure=FALSE&fileNamePrefix=Tariff&fileType=Excel&FromDate=1/1/1753&ToDate=12/31/9999")
        ''Response.Redirect(Page.ResolveUrl(General.CreateExcel(SqlDataSource1.SelectCommand, "Tariff")))

        ExportFile(DT_ExportTariff, "Excel", "AirTariff")
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
            responseString.Append("OriginAirport=" & Server.UrlEncode(txtOriginAirport.Text))
        End If

        If txtOriginRegion.Text.Trim() <> "" Then
            responseString.Append("&OriginRegion=" & Server.UrlEncode(txtOriginRegion.Text.Trim()))
        End If

        If txtDestAirport.Text.Trim() <> "" Then
            responseString.Append("&DestAirport=" & Server.UrlEncode(txtDestAirport.Text.Trim()))
        End If

        If txtDestCity.Text.Trim() <> "" Then
            responseString.Append("&DestCity=" & Server.UrlEncode(txtDestCity.Text.Trim()))
        End If

        If txtDestState.Text.Trim() <> "" Then
            responseString.Append("&DestState=" & Server.UrlEncode(txtDestState.Text.Trim()))
        End If

        If txtDestCountry.Text.Trim() <> "" Then
            responseString.Append("&DestCountry=" & Server.UrlEncode(txtDestCountry.Text.Trim()))
        End If

        If txtDestRegion.Text.Trim() <> "" Then
            responseString.Append("&DestRegion=" & Server.UrlEncode(txtDestRegion.Text.Trim()))
        End If

        If txtDestZipcode.Text.Trim() <> "" Then
            responseString.Append("&DestZipcode=" & Server.UrlEncode(txtDestZipcode.Text.Trim()))
        End If

        If txtCEVATransitMode.Text.Trim() <> "" Then
            responseString.Append("&CEVATransitMode=" & Server.UrlEncode(txtCEVATransitMode.Text.Trim()))
        End If

        If txtShipMethod.Text.Trim() <> "" Then
            responseString.Append("&ShipMethod=" & Server.UrlEncode(txtShipMethod.Text.Trim()))
        End If

        If txtForwarderZipcode.Text.Trim() <> "" Then
            responseString.Append("&ForwarderZipcode=" & Server.UrlEncode(txtForwarderZipcode.Text.Trim()))
        End If

        If txtCustomClearanceMode.Text.Trim() <> "" Then
            responseString.Append("&CustomClearanceMode=" & Server.UrlEncode(txtCustomClearanceMode.Text.Trim()))
        End If

        If txtForwarderServiceLevel.Text.Trim() <> "" Then
            responseString.Append("&ForwarderServiceLevel=" & Server.UrlEncode(txtForwarderServiceLevel.Text.Trim()))
        End If

        If txtMinFreightRate.Text.Trim() <> "" Then
            responseString.Append("&MinFreightRate=" & Server.UrlEncode(txtMinFreightRate.Text.Trim()))
        End If

        If txtFreightRate.Text.Trim() <> "" Then
            responseString.Append("&FreightRate=" & Server.UrlEncode(txtFreightRate.Text.Trim()))
        End If

        If txtSecurityRate.Text.Trim() <> "" Then
            responseString.Append("&SecurityRate=" & Server.UrlEncode(txtSecurityRate.Text.Trim()))
        End If

        If txtOtherCharges.Text.Trim() <> "" Then
            responseString.Append("&OtherCharges=" & Server.UrlEncode(txtOtherCharges.Text.Trim()))
        End If

        If txtFreightForwarder.Text.Trim() <> "" Then
            responseString.Append("&FreightForwarder=" & Server.UrlEncode(txtFreightForwarder.Text.Trim()))
        End If

        Response.Redirect("NewRateRequest.aspx?" & responseString.ToString)
    End Sub

    Private Function extractIdentifier(ByVal sender As Object) As String
        Return CType(sender, ImageButton).ID.Remove(0, 9)
    End Function

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
        If IsLocalCurrency = 0 Then
            IsLocalCurrency = 1
            'btnConvertCurrency.Text = "Convert Currency(Local)"
        Else
            IsLocalCurrency = 0
            'btnConvertCurrency.Text = "Convert Currency(USD)"
        End If

        ApplyFilter()
    End Sub

    
End Class