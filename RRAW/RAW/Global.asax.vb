Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    'Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
    'DB.Logs.PostNewLog(0, Now, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, AppVersion, EnumUserAction.ApplicationStart, Request.ServerVariables("REMOTE_ADDR"), Request.Browser.Browser, Request.Browser.Version)
    'End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Session("UserID") = 0
        Session("UserName") = ""
        Session("UserType") = ""
        Session("ApproversEmail") = ""
        Session("AppVersionExtension") = 3
        Session("ScreenRes") = ""
        Session("CurrentSelectCommand") = ""
        Session("CurrentFilterExpression") = ""
        Session("LogIDs") = ""
        Session("IsLocalCurrency") = 1
        Session("IsAdhocLocalCurrency") = 1

        'DB.Logs.PostNewLog(0, Now, System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, AppVersion, EnumUserAction.SessionStart, Request.ServerVariables("REMOTE_ADDR"), Request.Browser.Browser, Request.Browser.Version)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try
            DB.Logs.PostNewLog(EnumUserAction.ApplicationError)

            Dim ex As Exception = Server.GetLastError

            Server.Transfer("Errors.aspx?Msg=" & ex.InnerException.Message)

            Server.ClearError()
        Catch
        End Try
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        DB.AuthenticationTokens.RemoveAuthenticationToken((New HttpCookieCollection).Get("AuthenticationToken").Value)

        DB.Logs.LogDead()

        Response.Redirect("Login.aspx")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        DB.AuthenticationTokens.RemoveAuthenticationToken((New HttpCookieCollection).Get("AuthenticationToken").Value)
    End Sub
End Class