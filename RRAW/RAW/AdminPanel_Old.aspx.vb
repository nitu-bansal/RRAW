﻿Public Class AdminPanel_Old
    Inherits System.Web.UI.Page

    Dim ApproverID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Server.Transfer("Errors.aspx?Msg=You have tried to open an unauthorized page (" & Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5) & ".aspx).")

        hidCurrentUserID.Value = CurrentUserID.ToString

        Response.Cache.SetCacheability(HttpCacheability.Private)
        Response.Cache.SetMaxAge(New TimeSpan(7, 0, 0, 0))
    End Sub
End Class