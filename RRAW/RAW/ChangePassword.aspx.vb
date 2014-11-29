Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePassword.Click
        Try
            Dim UserID As String = DB.Users.GetDetails(CurrentUserID)("UserName").ToString
            If Convert.ToInt32(DB.Users.Verify(UserID, txtCurrentPassword.Text)("ID")) > 0 Then
                If DB.Users.ChangePassword(CurrentUserID, txtNewPassword.Text) <> 0 Then
                    lblStatus.Text = "Password changed successfully."
                    lblStatus.ForeColor = Drawing.Color.Blue
                    ClearFields()
                    lblUserID.Focus()
                Else
                    lblStatus.Text = "There is an unknown error in changing password. Please try again."
                    lblStatus.ForeColor = Drawing.Color.Red
                End If
            Else
                lblStatus.Text = "Invalid Username or Password.<br>Please try again."
                lblStatus.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
            lblStatus.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub ClearFields()
        txtCurrentPassword.Text = ""
        txtNewPassword.Text = ""
        txtConfirmNewPassword.Text = ""
    End Sub

    Private Sub ChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        Try
            lblUserID.Text = CurrentUserName
        Catch ex As Exception
            Response.Redirect("Login.aspx", False)
        End Try
    End Sub
End Class
