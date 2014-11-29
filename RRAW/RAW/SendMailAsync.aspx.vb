Public Class SendMailAsync
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SendMail("Exception thrown to " & CurrentUserName & " by the " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & " (" & CurrentReleaseInfo.Release & ") system at " & Request.ServerVariables("REMOTE_ADDR") & " using " & Request.Browser.Browser & " " & Request.Browser.Version & If(Request.Browser.Beta = True, " (BETA)", "") & If(Request.Browser.IsMobileDevice = True, " (Mobile - " & Request.Browser.MobileDeviceManufacturer & " " & Request.Browser.MobileDeviceModel & ")", ""), _
                     "<strong>Exception Type</strong>: " & Request.Params.Get("ExceptionType") & _
                     "<br /><br /><strong>Message</strong>: " & Request.Params.Get("errMsg") & _
                     "<br /><br /><strong>StackTrace</strong>: " & Request.Params.Get("StackTrace") & _
                     "<br /><br /><strong>TimedOut</strong>: " & Request.Params.Get("TimedOut"))
            lblStatus.Text = "Exception Mail has been sent successfully to admin."
        Catch ex As Exception
            lblStatus.Text = "Error occured in sending mail."
        End Try
        lblStatus.Text += "<br><br>Please click on Navigate Back to continue your work. We will solve the issue shortly."
    End Sub
End Class