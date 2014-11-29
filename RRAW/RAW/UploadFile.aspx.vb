Imports System.Data.SqlClient

Public Class UploadFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim res As String = ""

        Try
            'Handling Request
            Dim uploadedFile As IO.FileInfo
            Dim newFileName As String
            Dim serverFilePath As String = Server.MapPath("Attachments/RateRequestAttachment/")

            If Request.Files.Count > 0 Then
                'For IE
                uploadedFile = My.Computer.FileSystem.GetFileInfo(Request.Files(0).FileName)
                newFileName = uploadedFile.Name
                Dim i As Integer = 0
                While My.Computer.FileSystem.FileExists(serverFilePath & newFileName)
                    newFileName = uploadedFile.Name.Substring(0, uploadedFile.Name.IndexOf(uploadedFile.Extension)) & "_" & i & uploadedFile.Extension
                    i += 1
                End While
                Request.Files(0).SaveAs(serverFilePath & newFileName)
            Else
                'For Chrome & FF
                uploadedFile = My.Computer.FileSystem.GetFileInfo(Request.QueryString("qqfile"))
                newFileName = uploadedFile.Name
                Dim i As Integer = 0
                While My.Computer.FileSystem.FileExists(serverFilePath & newFileName)
                    newFileName = uploadedFile.Name.Substring(0, uploadedFile.Name.IndexOf(uploadedFile.Extension)) & "_" & i & uploadedFile.Extension
                    i += 1
                End While
                Dim stream = Request.InputStream
                Dim buf(CInt(stream.Length)) As Byte
                stream.Read(buf, 0, CInt(stream.Length))

                IO.File.WriteAllBytes(serverFilePath & newFileName, buf)
            End If

            res = "{""success"":""true"",""filename"":""" & newFileName & """}"
        Catch ex As Exception
            res = "{err:" & ex.Message & "}"
        End Try

        'Handling Response
        Response.ClearContent()
        Response.Clear()
        Response.ContentType = "application/json; charset=utf-8"
        Response.AddHeader("Content-Length", res.Length.ToString)
        Response.Write(res)
    End Sub
End Class