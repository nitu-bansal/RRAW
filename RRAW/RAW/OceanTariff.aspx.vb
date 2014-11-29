Public Class OceanTariff
    Inherits System.Web.UI.Page
    Dim DT_OceanTariff As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ApplyFilter()
    End Sub
    Private Sub ApplyFilter()
        Dim varShipdate As Date
        If IsDate(txtShipDate.Text) = True Then
            varShipdate = CDate(txtShipDate.Text)
            txtShipDate.Text = CDate(txtShipDate.Text).ToString("MM/dd/yyyy")
        Else
            txtShipDate.Text = ""
        End If

        Dim varEffectiveDate As Date
        If IsDate(txtEffectiveDate.Text) = True Then
            varEffectiveDate = CDate(txtEffectiveDate.Text)
            txtEffectiveDate.Text = CDate(txtEffectiveDate.Text).ToString("MM/dd/yyyy")
        Else
            txtEffectiveDate.Text = ""
        End If

        Dim varExpiryDate As Date
        If IsDate(txtExpiryDate.Text) = True Then
            varExpiryDate = CDate(txtExpiryDate.Text)
            txtExpiryDate.Text = CDate(txtExpiryDate.Text).ToString("MM/dd/yyyy")
        Else
            txtExpiryDate.Text = ""
        End If

        Dim varApprovalDateFrom As Date
        If IsDate(txtApprovalDateFrom.Text) = True Then
            varApprovalDateFrom = CDate(txtApprovalDateFrom.Text)
            txtApprovalDateFrom.Text = CDate(txtApprovalDateFrom.Text).ToString("MM/dd/yyyy")
        Else
            txtApprovalDateFrom.Text = ""
        End If

        Dim varApprovalDateTo As Date
        If IsDate(txtApprovalDateTo.Text) = True Then
            varApprovalDateTo = CDate(txtApprovalDateTo.Text)
            txtApprovalDateTo.Text = CDate(txtApprovalDateTo.Text).ToString("MM/dd/yyyy")
        Else
            txtApprovalDateTo.Text = ""
        End If

        Dim varRateRequestDate As Date
        If IsDate(txtRateRequestDate.Text) = True Then
            varRateRequestDate = CDate(txtRateRequestDate.Text)
            txtRateRequestDate.Text = CDate(txtRateRequestDate.Text).ToString("MM/dd/yyyy")
        Else
            txtRateRequestDate.Text = ""
        End If

        DT_OceanTariff = DB.Tariffs.GetOceanTariff(txtRateRequestID.Text.Trim, txtCustomer.Text.Trim, txtContainerNo.Text.Trim, txtOceanHBL.Text.Trim, txtShipDate.Text.Trim, txtFreightTerm.Text.Trim, txtWDShipMethod.Text.Trim, txtShipperName.Text.Trim, txtOriginCity.Text.Trim, txtOriginPort.Text.Trim, txtOriginZipcode.Text.Trim, txtOriginRegion.Text.Trim, txtDestCity.Text.Trim, txtDestPort.Text.Trim, txtDestZipcode.Text.Trim, txtDestRegion.Text.Trim, txtRatesValidFor.Text.Trim, txtRatesValidTill.Text.Trim, txtTotalLCL.Text.Trim, txtBAFLCL.Text.Trim, txtPSSLCL.Text.Trim, txtTotalFCL20GP.Text.Trim, txtBAFFCL20GP.Text.Trim, txtPSSFCL20GP.Text.Trim, txtTotalFCL40GP.Text.Trim, txtBAFFCL40GP.Text.Trim, txtPSSFCL40GP.Text.Trim, txtTotalFCL40HC.Text.Trim, txtBAFFCL40HC.Text.Trim, txtPSSFCL40HC.Text.Trim, txtComment.Text.Trim, txtRequestor.Text.Trim, txtRateRequestDate.Text.Trim, txtApprovalDateFrom.Text.Trim, txtApprovalDateTo.Text.Trim, txtApprover.Text.Trim, txtEffectiveDate.Text.Trim, txtExpiryDate.Text.Trim)
        gridTariff.DataSource = DT_OceanTariff
        gridTariff.DataBind()

        If gridTariff.Rows.Count = 0 Then
            lblLaneNotFoundMessage.Visible = True
            linkPostNewOceanRateRequest.Visible = True
        End If
    End Sub

    Protected Sub linkPostNewRateRequest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkPostNewOceanRateRequest.Click
        Dim responseString As New StringBuilder

        Response.Redirect("OceanRateRequest.aspx?" & responseString.ToString)
    End Sub

    Private Sub gridTariff_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridTariff.DataBound
        For i As Integer = 0 To gridTariff.Rows.Count - 1
            'gridTariff.Rows(i).Cells(0).Text = "<a href='NewRateRequest.aspx?RateRequestID=" & gridTariff.Rows(i).Cells(0).Text & "&rand=" & (New Random).Next(9999) & "'>" & gridTariff.Rows(i).Cells(0).Text & "</a>"
            gridTariff.Rows(i).Cells(0).Text = "<a href='OceanRateRequest.aspx?RateRequestID=" & gridTariff.Rows(i).Cells(0).Text & "'>Details</a>"
        Next
        lblPages.Text = "Pages: " & (gridTariff.PageIndex + 1).ToString & "/" & gridTariff.PageCount.ToString
        lblRecords.Text = "Total Records: " & gridTariff.Rows.Count.ToString()
    End Sub

    Private Sub gridTariff_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridTariff.PageIndexChanging
        gridTariff.PageIndex = e.NewPageIndex
        gridTariff.DataBind()
    End Sub

    Private Sub gridTariff_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Width = 60
            e.Row.Cells(1).Width = 160
            e.Row.Cells(2).Width = 60
            e.Row.Cells(3).Width = 60
            e.Row.Cells(4).Width = 70
            e.Row.Cells(5).Width = 100
            e.Row.Cells(6).Width = 160
            e.Row.Cells(7).Width = 160
            e.Row.Cells(8).Width = 150
            e.Row.Cells(9).Width = 60
            e.Row.Cells(10).Width = 60
            e.Row.Cells(11).Width = 100
            e.Row.Cells(12).Width = 150
            e.Row.Cells(13).Width = 60
            e.Row.Cells(14).Width = 60
            e.Row.Cells(15).Width = 100
            e.Row.Cells(16).Width = 100
            e.Row.Cells(17).Width = 100
            e.Row.Cells(18).Width = 100
            e.Row.Cells(19).Width = 100
            e.Row.Cells(20).Width = 100
            e.Row.Cells(21).Width = 100
            e.Row.Cells(22).Width = 100
            e.Row.Cells(23).Width = 100
            e.Row.Cells(24).Width = 100
            e.Row.Cells(25).Width = 100
            e.Row.Cells(26).Width = 100
            e.Row.Cells(27).Width = 100
            e.Row.Cells(28).Width = 100
            e.Row.Cells(29).Width = 100
            e.Row.Cells(30).Width = 250
            e.Row.Cells(31).Width = 100
            e.Row.Cells(32).Width = 70
            e.Row.Cells(33).Width = 145
            e.Row.Cells(34).Width = 100
            e.Row.Cells(35).Width = 70
        End If

    End Sub

    Private Sub gridTariff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowDataBound
        
        Dim creatCels2 As New SortedList()
        creatCels2.Add("1", ",18,1")
        creatCels2.Add("2", "LCL (per CBM),3,1")
        creatCels2.Add("3", "FCL (per 20GP),3,1")
        creatCels2.Add("4", "FCL (per 40GP),3,1")
        creatCels2.Add("5", "FCL (per 40HC),3,1")
        creatCels2.Add("6", ",7,1")
        GetMyMultiHeader(e, creatCels2)
    End Sub
    Public Sub GetMyMultiHeader(ByVal e As GridViewRowEventArgs, ByVal GetCels As SortedList)

        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As GridViewRow
            Dim enumCels As IDictionaryEnumerator = GetCels.GetEnumerator()

            row = New GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal)
            While enumCels.MoveNext()
                Dim cont As String() = enumCels.Value.ToString().Split(Convert.ToChar(","))
                Dim Cell As TableCell
                Cell = New TableCell()
                Cell.RowSpan = Convert.ToInt16(cont(2).ToString())
                Cell.ColumnSpan = Convert.ToInt16(cont(1).ToString())
                Cell.Controls.Add(New LiteralControl(cont(0).ToString()))
                Cell.HorizontalAlign = HorizontalAlign.Center
                Cell.ForeColor = System.Drawing.Color.White
                row.Cells.Add(Cell)
            End While
            e.Row.Parent.Controls.AddAt(0, row)
        End If
    End Sub

    Public Sub ExportFile(ByVal FileTable As DataTable, ByVal FileType As String, ByVal fileNamePrefix As String)
        Dim fileName As String = fileNamePrefix & "_" & Now.ToString("yyyyMMddHHmmss") & If(FileType = "Excel", ".xls", ".csv")
        Dim csvContent As New StringBuilder
        Dim fieldSeparator As String
        Try
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

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)

            Dim Encoding As New System.Text.UnicodeEncoding
            Response.AddHeader("Content-Length", Encoding.GetByteCount(fileContent).ToString())
            Response.BinaryWrite(Encoding.GetBytes(fileContent))
            Response.Charset = "iso-8859-2"
        Catch
            Throw
        End Try
    End Sub
    Protected Sub btnExportToCSV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToCSV.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)
        ExportFile(DT_OceanTariff, "CSV", "OceanTariff")
    End Sub

    Protected Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click
        DB.Logs.PostNewLog(EnumUserAction.ExportingRates)
        ExportFile(DT_OceanTariff, "Excel", "OceanTariff")
    End Sub
End Class