Public Class UploadFilePage
    Inherits System.Web.UI.Page

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim uploadedFile As IO.FileInfo
        Dim newFileName As String
        Dim serverFilePath As String = Server.MapPath("Attachments/RateRequestAttachment/")
        Dim uploadedFileCount As Integer
        Dim res As New Dictionary(Of String, String)

        'Tested with IE6, IE7, IE8, Chrome12 & FF5
        For i = 0 To Request.Files.Count - 1
            If Request.Files(i).FileName = "" Then Continue For

            uploadedFile = My.Computer.FileSystem.GetFileInfo(Request.Files(i).FileName)
            newFileName = uploadedFile.Name
            Dim j As Integer = 0
            While My.Computer.FileSystem.FileExists(serverFilePath & newFileName)
                newFileName = uploadedFile.Name.Substring(0, uploadedFile.Name.IndexOf(uploadedFile.Extension)) & "_" & j & uploadedFile.Extension
                j += 1
            End While
            Request.Files(i).SaveAs(serverFilePath & newFileName)
            uploadedFileCount += 1

            res.Add(newFileName, serverFilePath & newFileName)
        Next

        Response.ClearContent()
        Response.Clear()
        Response.ContentType = "text/plain; charset=UTF-8"
        'Response.AddHeader("Content-Length", res.Length.ToString)
        Response.Write("<pre>" & ComplexDictionaryToString(res) & "</pre>")
    End Sub

    Private Function ComplexDictionaryToString(ByVal dict As Dictionary(Of String, String)) As String
        If dict.Count = 0 Then Return "[]"

        Dim res As New StringBuilder("[")

        For Each dictItem In dict
            res.Append("[")
            res.Append("""" & dictItem.Key & """")
            res.Append(",")
            res.Append("""" & dictItem.Value & """")
            res.Append("],")
        Next
        res = res.Remove(res.Length - 1, 1)
        res.Append("]")

        Return res.ToString
    End Function
End Class