Imports System.IO
Imports Microsoft.Win32

Public Class DownloadFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

        If Request.QueryString("FileName") <> "" Then
            Dim f As New FileInfo(Server.MapPath(Request.QueryString("FileName")))
            DownloadFile(f)
        End If
    End Sub

    Private Sub DownloadFile(ByVal f As FileInfo)
        Using st As Stream = New IO.FileStream(f.FullName, FileMode.Open)
            Dim blockSize As Integer

            If st.Length < (100 * 1024) Then
                blockSize = CInt(st.Length)
            Else
                blockSize = 100 * 1024
            End If

            Dim dataLengthToRead As Long = st.Length
            Dim buffer(CInt(dataLengthToRead)) As Byte

            Response.ContentType = GetContentType(f)
            Response.AddHeader("Accept-Ranges", "bytes")
            Response.AddHeader("Connection", "Keep-Alive")
            Response.AddHeader("Content-Length", dataLengthToRead.ToString)

            Response.AddHeader("Content-Disposition", "attachment; filename=" + f.Name)

            While (dataLengthToRead > 0 AndAlso Response.IsClientConnected)
                Dim lengthRead As Integer = st.Read(buffer, 0, blockSize)

                Response.OutputStream.Write(buffer, 0, lengthRead)
                Response.Flush()

                dataLengthToRead = dataLengthToRead - lengthRead
            End While

            Response.Flush()
            Response.Close()
        End Using

        Response.End()
    End Sub

    Private Function GetContentType(ByVal f As FileInfo) As String
        Dim mimeType As String = "application/unknown"

        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(f.Extension.ToLower())

        If (regKey IsNot Nothing) Then

            Dim contentType As Object = regKey.GetValue("Content Type")

            If (contentType IsNot Nothing) Then
                mimeType = contentType.ToString()
            End If
        End If

        Return mimeType
    End Function
End Class