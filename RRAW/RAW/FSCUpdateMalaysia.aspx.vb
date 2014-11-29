Public Class FSCUpdateMalaysia
    Inherits System.Web.UI.Page

    'Private Function GetCurrentDateTime() As DateTime
    '    Try
    '        Return Convert.ToDateTime(hidCurrentDateTime.Value)
    '    Catch
    '        Return Now
    '    End Try
    'End Function

    Dim dtdata As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)
        If Not Page.IsPostBack Then
            lstEfftDt.DataSource = DB.FSC.GetFSCDatesToFillCombo()
            lstEfftDt.DataTextField = "EffectiveDate"
            lstEfftDt.DataValueField = "EffectiveDate"
            lstEfftDt.DataTextFormatString = "{0:dd-MMM-yyyy}"
            lstEfftDt.DataBind()
            lstEfftDt.SelectedIndex = 0
            btnShowTariff_Click(sender, e)
        End If


        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Response.Redirect("FSCUpdateThailand.aspx", True)

        For Each row As DataRow In DB.UserModulesMapping.GetAccesibleModulesOfUser(CurrentUserID, CurrentClientID).Rows
            If row("Name").ToString.Trim = "fscremovemalaysia" Then
                tblFSCUpload.Visible = True
                gridUpdates.Columns(0).Visible = True
            End If
        Next

        SqlDataSource1.ConnectionString = CurrentDBConnection
    End Sub

    Private Sub gridFSCUpdates_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridUpdates.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
        End If
    End Sub

    Protected Sub btnUploadFSC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUploadFSC.Click
        Try
            Page.Validate()

            If Page.IsValid Then
                Dim filePath As String = Server.MapPath("~/Attachments/") + uploadFSCUpdate.FileName
                uploadFSCUpdate.SaveAs(filePath)
                lblStatus.Text = filePath
                DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.FSCUpdateMalaysia, 0, txtTitle.Text, filePath)

                txtTitle.Text = ""
                gridUpdates.DataBind()

                lblStatus.Text = "New FSC Update for Malaysia has been uploaded successfully"
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnShowTariff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowTariff.Click
        Dim Qry As String
        Try
            dtdata = DB.FSC.GetFSCData(CDate(lstEfftDt.SelectedItem.Text))
            gridTariff.DataSource = dtdata
            gridTariff.DataBind()
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(0).Attributes("colspan") = "5"
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(1).Visible = False
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(2).Visible = False
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(3).Visible = False
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(4).Visible = False
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(0).Style("Color") = "red"
            gridTariff.Rows(gridTariff.Rows.Count - 1).Cells(0).Style("text-align") = "left" 'UpdateMalaysiaFooter
            gridTariff.Rows(gridTariff.Rows.Count - 1).Attributes("class") = "UpdateMalaysiaFooter"
            'gridTariff.FooterRow.Cells(0).Text = dtdata.Rows(0)(7).ToString
            lblFSCRateHeader.Text = "WD FSC Rate ( " & MonthName(CDate(dtdata.Rows(0)(5).ToString).Month) & " ) - " & CDate(dtdata.Rows(0)(5).ToString).ToString("dd MMM ") & "till " & CDate(dtdata.Rows(0)(6).ToString).ToString("dd MMM yyyy ")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridTariff_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridTariff.DataBound
        

    End Sub

    Private Sub gridTariff_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowCreated
        
    End Sub

    Private Sub gridTariff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTariff.RowDataBound
        If (e.Row.RowType = DataControlRowType.Footer) Then

            'Dim lblfooter As Label = DirectCast(e.Row.FindControl("lblfooter"), Label)
            'lblfooter.Text = dtdata.Rows(0)(7).ToString
            ' Get the OrderTotalLabel Label control in the footer row.

        End If
    End Sub
End Class