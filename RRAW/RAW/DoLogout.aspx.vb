Public Class DoLogout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            DB.Logs.LogDead()
        Catch ex As Exception
        End Try
    End Sub
End Class