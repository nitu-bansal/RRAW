Public Class UserModulesMapping
    Inherits System.Web.UI.Page

    Private Sub ChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5)) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource2.ConnectionString = CurrentDBConnection
    End Sub

    Protected Sub lstUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstUsers.SelectedIndexChanged
        Dim dr As DataRow = DB.Users.GetDetails(Convert.ToInt32(lstUsers.SelectedValue.ToString()))
        lblSelectedUser.Text = dr("UserID").ToString()
        Dim dt As DataTable = DB.UserModulesMapping.GetAccesibleModulesOfUser(Convert.ToInt32(lstUsers.SelectedValue))
        chlModules.ClearSelection()
        For Each row As DataRow In dt.Rows
            chlModules.Items.FindByValue(row("ID").ToString).Selected = True
        Next

        lblStatus.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();", True)
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            DB.UserModulesMapping.RemoveAllModuleAccessOfUser(Convert.ToInt32(lstUsers.SelectedValue))

            For Each item As ListItem In chlModules.Items
                If item.Selected = True Then
                    Dim AccessGivenAt As DateTime = Convert.ToDateTime(hidCurrentDateTime.Value)
                    Dim AccessExpiresAt As DateTime = AccessGivenAt.AddYears(1)
                    DB.UserModulesMapping.AddModuleAccessToUser(CurrentUserID, Convert.ToInt32(lstUsers.SelectedValue), Convert.ToInt32(item.Value), AccessGivenAt, AccessExpiresAt)
                End If
            Next

            lblStatus.Visible = True
            lblStatus.ForeColor = Drawing.Color.Blue
            lblStatus.Text = "Accessbile modules of " & lstUsers.SelectedItem.Text & " is updated successfully."
        Catch ex As Exception
            lblStatus.Visible = True
            lblStatus.ForeColor = Drawing.Color.Red
            lblStatus.Text = "Accessbile modules of " & lstUsers.SelectedItem.Text & " is not updated." & vbNewLine & _
                                "Error details are: " & ex.Message
        End Try
    End Sub
End Class
