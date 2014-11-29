Partial Public Class TruckRateRequest
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
            If row("Name").ToString.Trim = "truckraterequestupload" Then
                tblTruckUpload.Visible = True
            End If
        Next

        SqlDataSource1.ConnectionString = CurrentDBConnection
    End Sub

    Protected Sub gridOceanUpdates_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridUpdates.RowCreated
        e.Row.Cells(0).Style.Add("display", "none")
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
        End If
    End Sub

    Protected Sub btnUploadOcean_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateOcean.Click
        Try
            Page.Validate()

            If Page.IsValid Then
                Dim filePath As String = Server.MapPath("~/Attachments/") + uploadOceanUpdate.FileName
                uploadOceanUpdate.SaveAs(filePath)
                lblStatus.Text = filePath
                'DB.OceanUpdates.AddOceanUpdate(txtTitle.Text, filePath, GetCurrentDateTime)
                If gridUpdates.Rows.Count > 0 Then
                    DB.Attachments.RemoveAttachment(CInt(gridUpdates.Rows(0).Cells(0).Text))
                End If
                DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.TruckRateRequestTemplate, 0, txtTitle.Text, filePath)

                txtTitle.Text = ""
                gridUpdates.DataBind()

                lblStatus.Text = "New Truck Rate Request Template has been uploaded successfully"
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub
End Class