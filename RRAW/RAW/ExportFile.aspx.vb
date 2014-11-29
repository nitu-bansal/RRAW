Imports System.Data.SqlClient

Public Class ExportFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ExportFile(CurrentSelectCommand, CBool(Request.QueryString("isStoredProcedure")), CurrentFilterExpression, Request.QueryString("fileNamePrefix"), Request.QueryString("fileType"), Convert.ToInt32(Request.QueryString("UserID")), Convert.ToDateTime(Request.QueryString("FromDate")), Convert.ToDateTime(Request.QueryString("ToDate")), Convert.ToInt32(Request.QueryString("ClientId")))
    End Sub

    Private Sub ExportFile(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal CurrentFilterExpression As String, ByVal fileNamePrefix As String, ByVal fileType As String, ByVal userID As Integer, ByVal periodStartDate As Date, ByVal periodEndDate As Date, ByVal ClientId As Integer)
        Dim fileName As String = fileNamePrefix & "_" & Now.ToString("yyyyMMddHHmmss") & If(fileType = "Excel", ".xls", ".csv")

        Dim params(3) As SqlParameter
        params(0) = New SqlParameter("@UserID", userID)
        params(1) = New SqlParameter("@FromDate", periodStartDate)
        params(2) = New SqlParameter("@ToDate", periodEndDate)
        params(3) = New SqlParameter("@ClientId", ClientId)

        Dim fileContent As String = GetCSVFromQuery(query, isStoredProcedure, CurrentFilterExpression, If(fileType = "Excel", vbTab, ","), params)

        Response.Clear()

        Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)

        If fileType = "Excel" Then
            Response.ContentType = "application/ms-excel"
        Else
            Response.ContentType = "application/octet-stream"
        End If

        Dim Encoding As New System.Text.UnicodeEncoding

        Response.AddHeader("Content-Length", Encoding.GetByteCount(fileContent).ToString())
        Response.BinaryWrite(Encoding.GetBytes(fileContent))
        Response.Charset = "iso-8859-2"

        'Response.Flush()
    End Sub
End Class