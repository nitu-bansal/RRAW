Public Class OceanTariff_New
    Inherits System.Web.UI.Page

    Dim DT_Tariff As DataTable
    Dim DT_ExportTariff As DataTable
    Dim intGridColWidth As Integer() = {}

    Private Sub Tariff_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error

        Dim ex As Exception = Server.GetLastError.GetBaseException

        Dim errorInfo As String = "<br>Offending URL: " + Request.Url.ToString() + "<br>Source: " + ex.Source + "<br>Message: " + ex.Message + "<br>Stack trace: " + ex.StackTrace

        Response.Write(errorInfo)

        MyBase.OnError(e)
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (IsPostBack = False) Then
            IsLocalCurrency = 1
        End If
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        'If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, "TARIFFEXPORT", CurrentClientID) = True Then
        '    btnExportToCSV.Visible = True
        '    btnExportToExcel.Visible = True
        'End If

        SqlDataSource1.ConnectionString = CurrentDBConnection


        ApplyFilter()

        If Page.IsPostBack = False Then
            DB.Logs.PostNewLog(EnumUserAction.SearchingRates)

        End If
    End Sub

    Private Sub ApplyFilter()
        Dim varActive As Integer = 1
        DT_Tariff = DB.Tariffs_New.GetTariffFromDB(2, CurrentClientID, 0)

        DT_ExportTariff = DT_Tariff.Copy

        gridTariff.DataSource = DT_Tariff
        gridTariff.DataBind()



        If DT_Tariff.Rows.Count = 0 Then
            lblLaneNotFoundMessage.Visible = True
            linkPostNewRateRequest.Visible = False
        Else
            lblLaneNotFoundMessage.Visible = False
            linkPostNewRateRequest.Visible = False
        End If
    End Sub

    

    Private Sub gridTariff_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTariff.PageIndexChanging
        gridTariff.PageIndex = e.NewPageIndex
        gridTariff.DataBind()
    End Sub

    

    Private Sub ProcessGridRequest(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    'Protected Sub btnExportToCSV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToCSV.Click
    '    DB.Logs.PostNewLog(EnumUserAction.ExportingRates)



    '    ExportFile(DT_ExportTariff, "CSV", "OceanTariff")
    'End Sub

    'Protected Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click
    '    DB.Logs.PostNewLog(EnumUserAction.ExportingRates)


    '    ExportFile(DT_ExportTariff, "Excel", "OceanTariff")
    'End Sub

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


    End Sub

    Private Function extractIdentifier(ByVal sender As Object) As String
        Return CType(sender, ImageButton).ID.Remove(0, 9)
    End Function



    Private ReadOnly Property qStrOriginAirportCode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("OriginAirportCode"))
        End Get
    End Property

    Private ReadOnly Property qStrDestinationAirportCode() As String
        Get
            Return Server.UrlDecode(Request.QueryString("DestinationAirportCode"))
        End Get
    End Property

    Private ReadOnly Property qStrMinFreightRate() As String
        Get
            Return Server.UrlDecode(Request.QueryString("MinFreightRate"))
        End Get
    End Property

    Private ReadOnly Property qStrFreightRatePerKG() As String
        Get
            Return Server.UrlDecode(Request.QueryString("FreightRatePerKG"))
        End Get
    End Property

    

    Private Sub gridTariff_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridTariff.DataBound

        For i As Integer = 0 To gridTariff.Rows.Count - 1
            gridTariff.Rows(i).Cells(0).Text = "<a href='NewOceanRateRequest.aspx?RateRequestID=" & gridTariff.Rows(i).Cells(0).Text & "&TransPortModeId=2&rand=" & (New Random).Next(9999) & "'>" & gridTariff.Rows(i).Cells(0).Text & "</a>"
        Next

    End Sub
    Private Sub gridTariff_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then
            For j As Integer = 0 To e.Row.Cells.Count - 1
                If DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "addrRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "TariffRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "ViewRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "ServiceRateRequestId" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "A" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "B" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "C" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldHeaderCell).ContainingField.ToString() = "D" Then
                    e.Row.Cells(j).Visible = False
                End If
            Next
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            For j As Integer = 0 To e.Row.Cells.Count - 1
                If DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "addrRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "TariffRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "ViewRateRequestID" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "ServiceRateRequestId" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "A" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "B" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "C" Or DirectCast(e.Row.Cells(j), System.Web.UI.WebControls.DataControlFieldCell).ContainingField.ToString() = "D" Then
                    e.Row.Cells(j).Visible = False
                End If
                e.Row.Cells(j).HorizontalAlign = HorizontalAlign.Center
            Next
        End If
    End Sub


End Class