Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Issues
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function GetAllModules(authenticationToken As String) As Dictionary(Of String, String)
            Dim d As New Dictionary(Of String, String)

            If Not IsAuthenticated(authenticationToken) Then
                d.Add("RedirectionPath", "AuthenticationFailed.aspx")

                Return d
            End If

            d = DataTableToDictionary(DB.Issues.GetAllModules)
            Return d
        End Function
    End Class
End Namespace