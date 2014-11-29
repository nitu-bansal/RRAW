Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Attachments
        Inherits System.Web.Services.WebService

        'Web Service scope variable declaration
        Shared errCode As Integer = -1

        Friend Shared Function AddAttachment(ByVal AttachmentType As DB.Attachments.AttachmentTypes, ByVal ReferenceID As Integer, ByVal Title As String, ByVal Path As String) As Integer
            'Refine and validate input values
            If IsValid(AttachmentType) Then Return -1
            If IsValid(ReferenceID) Then Return -1
            If IsValid(Title) Then Return -1
            If IsValid(Path) Then Return -1

            'Call BLL function if valid
            Return DB.Attachments.AddAttachment(AttachmentType, ReferenceID, Title, Path)
        End Function
    End Class
End Namespace