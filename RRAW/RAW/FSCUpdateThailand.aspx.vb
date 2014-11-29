Public Class FSCUpdateThailand
    Inherits System.Web.UI.Page

    'Private Function GetCurrentDateTime() As DateTime
    '    Try
    '        Return Convert.ToDateTime(hidCurrentDateTime.Value)
    '    Catch
    '        Return Now
    '    End Try
    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        For Each row As DataRow In DB.UserModulesMapping.GetAccesibleModulesOfUser(CurrentUserID, CurrentClientID).Rows
            If row("Name").ToString.Trim = "fscremovethailand" Then
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
                DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.FSCUpdateThailand, 0, txtTitle.Text, filePath)

                txtTitle.Text = ""
                gridUpdates.DataBind()

                lblStatus.Text = "New FSC Update for Thailand has been uploaded successfully"
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub
End Class