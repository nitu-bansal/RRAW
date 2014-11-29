Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class AutoComplete
        Inherits System.Web.Services.WebService

        <WebMethod(EnableSession:=True)> _
        Public Function GetTariffAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()

            Dim temp As String() = contextKey.Split("-"c)
            Dim FieldName As String = temp(0)
            Dim TransportModeId As String = temp(1)

            Dim Qry = "select DISTINCT  LTRIM(RTRIM(a.Data)) from ShipmentAddresses a join FieldMaster b on a.FieldMasterID=b.ID where b.Name=ltrim(rtrim('" & FieldName & "')) and LTRIM(RTRIM(a.Data)) like '" & prefixText.Trim & "%' and a.ClientId= " & CurrentClientID & " and  a.TransportModeId=" & TransportModeId

            Try
                Using DB As New RRAW.DB.DBClass(Qry)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetServiceLevelsAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim temp As String() = contextKey.Split("-"c)
            Dim FieldName As String = temp(0)
            Dim TransportModeId As String = temp(1)
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & FieldName & ")) FROM ServiceLevels WHERE ltrim(rtrim(" & FieldName & ")) LIKE '" & prefixText.Trim & "%' and TransportModeId=" & TransportModeId

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        <WebMethod()> _
        Public Function GetServiceTypesAutoComplete(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
            Dim query As String = "SELECT DISTINCT LTRIM(RTRIM(" & contextKey & ")) FROM ServiceTypes WHERE ltrim(rtrim(" & contextKey & ")) LIKE '" & prefixText.Trim & "%'"

            Try
                Using DB As New RRAW.DB.DBClass(query)
                    Dim dt As DataTable = DB.GetDataTable
                    Dim dataArray As New Generic.List(Of String)
                    For Each row As DataRow In dt.Rows
                        dataArray.Add(row(0).ToString)
                    Next
                    Return dataArray.ToArray
                End Using
            Catch
                Throw
            End Try
        End Function

        
    End Class
End Namespace