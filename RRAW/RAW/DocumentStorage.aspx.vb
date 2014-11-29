Imports RRAW.DB

Public Class DocumentStorage
    Inherits System.Web.UI.Page

    Dim dtdata As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ddlCategory.DataSource = Masters.GetAllDocumentCategory()
            ddlCategory.DataBind()
        End If

        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If (DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False) Then

        End If
        For Each row As DataRow In DB.UserModulesMapping.GetAccesibleModulesOfUser(CurrentUserID, CurrentClientID).Rows
            If row("ControlID").ToString.Trim = "FSCREMOVEMALAYSIA" Then
                tblDocumentUpload.Visible = True
            ElseIf row("ControlID").ToString.Trim = "FSCUPDATEMALAYSIA" Then
                tblDocumentDownload.Visible = True

            End If
        Next

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource1.SelectParameters("ClientId").DefaultValue = CurrentClientID.ToString()

    End Sub
    

    Protected Sub btnUploadDocument_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUploadDocument.Click
        Try
            Page.Validate()
            If Page.IsValid Then
                Dim filePath As String = Server.MapPath("~/Documents/") + uploadDocument.FileName
                uploadDocument.SaveAs(filePath)
                lblStatus.Text = filePath
                DB.Attachments.AddDocument(CurrentClientID, CurrentUserID, Convert.ToInt32(ddlCategory.SelectedValue), uploadDocument.FileName, txtTitle.Text, CDate(txtEffectiveDate.Text).ToString("MM/dd/yyyy"), CDate(txtExpiryDate.Text).ToString("MM/dd/yyyy"))

                txtTitle.Text = ""
                gridUpdates.DataBind()

                lblStatus.Text = "New document has been uploaded successfully"
                txtEffectiveDate.Text = ""
                txtExpiryDate.Text = ""
                ddlCategory.SelectedValue = "0"
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try
    End Sub
End Class