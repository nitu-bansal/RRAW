Public Class Login
    Inherits System.Web.UI.Page

    Dim prevPage As String

    Private Function GetCurrentDateTime() As DateTime
        Try
            Return Convert.ToDateTime(hidCurrentDateTime.Value)
        Catch
            Return Now
        End Try
    End Function

   

    Private Sub btnLogin_Load(sender As Object, e As System.EventArgs) Handles btnLogin.Load
        If Request.QueryString("Mode") <> "Test" Then

            If Request.Url.AbsoluteUri.ToLower.Contains("172.16") Or Request.Url.AbsoluteUri.ToLower.Contains("local") Then
                lblAppState.Text = "(LOCAL)"
            ElseIf Request.Url.AbsoluteUri.ToLower.Contains("alpha") Then
                lblAppState.Text = "(TEST)"
            ElseIf Request.Url.AbsoluteUri.ToLower.Contains("beta") Then
                lblAppState.Text = "(TEST)"
            Else
                lblAppState.Text = ""
            End If

            If My.Computer.FileSystem.FileExists(Server.MapPath("/Images/UnderMaintenance.gif")) Then
                Response.Redirect("UnderMaintenance.aspx")
                Exit Sub
            End If
        End If
    End Sub

  

    Protected Sub btnLogin_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLogin.Click
        If Request.Url.AbsoluteUri.ToLower.Contains("172.16") Or Request.Url.AbsoluteUri.ToLower.Contains("local") Then
            Session("AppVersionExtension") = EnumCurrentAppVersionExtension.LOCAL
        ElseIf Request.Url.AbsoluteUri.ToLower.Contains("alpha") Then
            Session("AppVersionExtension") = EnumCurrentAppVersionExtension.ALPHA
        ElseIf Request.Url.AbsoluteUri.ToLower.Contains("beta") Then
            Session("AppVersionExtension") = EnumCurrentAppVersionExtension.BETA
        Else
            Session("AppVersionExtension") = EnumCurrentAppVersionExtension.LIVE
        End If

        Session("ScreenRes") = hidScreenWidth.Value & "x" & hidScreenHeight.Value

        Try
            Dim User As DataRow = DB.Users.Verify(txtUserID.Text, txtPassword.Text)
            If User IsNot Nothing Then
                Dim UserID As Integer = Convert.ToInt32(User("ID"))

                Dim authenticationToken As String = Guid.NewGuid.ToString
                Dim a As New HttpCookie("AuthenticationToken", authenticationToken)
                a.Expires = Now.AddHours(authenticationTimeOutHours)
                Response.SetCookie(a)
                DB.AuthenticationTokens.AddNewAuthenticationToken(authenticationToken, a.Expires)

                Dim u As New HttpCookie("CurrentUserID", UserID.ToString)
                u.Expires = Now.AddHours(authenticationTimeOutHours)
                Response.SetCookie(u)

                Dim UserName As String = User("Name").ToString
                Dim UserType As String = User("UserType").ToString
                ''Dim ApproversEmail As String = User("ApproversEmail").ToString


                Dim ClientId As Integer = Convert.ToInt32(User("ClientId"))
                Dim c As New HttpCookie("CurrentClientID", ClientId.ToString)
                c.Expires = Now.AddHours(authenticationTimeOutHours)
                Response.SetCookie(c)


                Session("UserID") = UserID
                Session("UserName") = UserName
                Session("UserType") = UserType
                Dim t As New HttpCookie("CurrentUserType", UserType)
                t.Expires = Now.AddHours(authenticationTimeOutHours)
                Response.SetCookie(t)
                ''Session("ApproversEmail") = ApproversEmail
                Session("ClientId") = ClientId

                'SendMail(UserName & " has logged in to the " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & " (" & AppVersion() & ") system from " & Request.ServerVariables("REMOTE_ADDR") & " using " & Request.Browser.Browser & " " & Request.Browser.Version & If(Request.Browser.Beta = True, " (BETA)", "") & If(Request.Browser.IsMobileDevice = True, " (Mobile - " & Request.Browser.MobileDeviceManufacturer & " " & Request.Browser.MobileDeviceModel & ")", ""), "")
                DB.Logs.PostNewLog(EnumUserAction.Login, GetCurrentDateTime)
                If Request.QueryString("ResponsePage") IsNot Nothing Then
                    Response.Redirect(Request.QueryString("ResponsePage"), False)
                Else
                    'If Request.QueryString("Mode") <> "Test" Then
                    Response.Redirect("Master.aspx", False)
                    'Else
                    'Response.Redirect("Master.aspx?Mode=Test", False)
                    'End If
                End If
            Else
                lblError.Visible = True
                txtUserID.Text = ""
                txtPassword.Text = ""
                txtUserID.Focus()
            End If
        Catch ex As Exception
            Server.Transfer("Errors.aspx?Msg=" & ex.Message)
        End Try
    End Sub
End Class