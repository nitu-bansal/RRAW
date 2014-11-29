Public Class GetBlogEntries
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then
            lblTitle.Text = "LoginRequired" & "{EndTitle}"
        Else
            Dim dt As DataTable
            If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, "developerblogs", CurrentClientID) = True Then
                dt = DB.Blogs.GetDeveloperBlogEntries
            Else
                dt = DB.Blogs.GetBlogEntries
            End If

            Dim specialRow As DataRow
            If Request.QueryString("Title") = "" Then
                specialRow = dt.Rows(0)
            Else
                specialRow = dt.Select("Title='" & Server.UrlDecode(Request.QueryString("Title")) & "'")(0)
            End If

            lblTitle.Text = specialRow("Title").ToString & "{EndTitle}"
            lblFilePath.Text = specialRow("FilePath").ToString & "{EndFilePath}"
            lblPostedOn.Text = CDate(specialRow("PostedOn")).ToString("MMMM dd, yyyy") & "{EndPostedOn}"
            lblPostedBy.Text = specialRow("PostedBy").ToString & "{EndPostedBy}"

            For Each dr As DataRow In dt.Rows
                Dim a As New HtmlAnchor
                With a
                    a.InnerText = dr("Title").ToString
                    a.HRef = "#"
                    a.Attributes.Add("onClick", "window['LoadBlog']('" & dr("Title").ToString & "', '" & dr("FilePath").ToString & "', '" & CDate(dr("PostedOn")).ToString("MMMM dd, yyyy") & "', '" & dr("PostedBy").ToString & "');")
                End With

                container.Controls.Add(a)
            Next
        End If
    End Sub
End Class