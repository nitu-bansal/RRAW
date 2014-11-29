Public Class CustomMail
    Private mailBody As StringBuilder
    Private varSubject As String
    Private bodyHeader As String
    Private bodyFooter As String

    Public Sub New(subject As String, bodyHeader As String, bodyFooter As String)
        mailBody = New StringBuilder

        Me.varSubject = subject
        Me.bodyHeader = bodyHeader
        Me.bodyFooter = bodyFooter

        mailBody.Append(bodyHeader)
    End Sub

    Public Sub AddRow(row As String)
        mailBody.Append(row)
    End Sub

    Public Function SendCustomMail(toAddress As String) As Boolean
        SendMail(toAddress, varSubject, mailBody.Append(bodyFooter).ToString)
        'SendMail("dharmesh.mistry@searce.com", subject & " for " & toAddress, mailBody.Append(bodyFooter).ToString)
    End Function

    Public Property Subject() As String
        Get
            Return varSubject
        End Get
        Set(value As String)
            varSubject = value
        End Set
    End Property

End Class
