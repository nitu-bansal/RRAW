Public Class Master
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.ToString, True)

        Select Case CurrentAppVersionExtension
            Case EnumCurrentAppVersionExtension.LOCAL
                lblAppState.Text = "(LOCAL)"
            Case EnumCurrentAppVersionExtension.ALPHA
                lblAppState.Text = "(TEST)"
            Case EnumCurrentAppVersionExtension.BETA
                lblAppState.Text = "(TEST)"
            Case Else
                lblAppState.Text = ""
        End Select


        If Not IsPostBack Then
            GetAllClients()

            'If cmbClients.Items.Count > 1 Then
            '    dvClient.Visible = True
            'Else
            '    dvClient.Visible = False

            'End If
            cmbClients.SelectedValue = Session("ClientId").ToString()
            Session("ClientName") = cmbClients.SelectedItem.Text

        End If

        'If mainFrame.Attributes("src") IsNot Nothing Then
        '    If mainFrame.Attributes("src").ToString().ToUpper() = "DASHBOARD.ASPX" Then
        '        dvClient.Visible = True
        '    Else
        '        dvClient.Visible = False

        '    End If
        'End If

        lblAppVersion.Text = CurrentReleaseInfo.Release & IIf(CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.LIVE, " (" & CurrentAppVersionExtension.ToString & ")", "").ToString
        If Request.QueryString("Mode") = "Test" Then
            lblAppVersion.Text += "|TEST MODE|"
        End If

        lblUserName.Text = CurrentUserName

        If Request.QueryString("ChildPage") IsNot Nothing Then
            mainFrame.Attributes("src") = Request.QueryString("ChildPage")
        End If

        hidAccessibleModules.Value = ""
        hidAccessibleModules.Value = GetAccessibleModules(CurrentUserID, CurrentClientID)
        hidAccessibleModules.Value = hidAccessibleModules.Value.Trim(CChar("""")).Trim(CChar(","))

        'DB.AirRateRequests.FinalizeAirRateRequestBySystem()

        'Send mail & transfer rate requests of users who have not worked on AirRateRequests pending on their side from last 72* hours
        'ProcessNeedToTransferRequests()

        'Send reminder mail to users who have not worked on AirRateRequests pending on their side from last 48* hours
        'ProcessNeedToOperateRequests()

        'Response.Cache.SetCacheability(HttpCacheability.Private)
        'Response.Cache.SetMaxAge(New TimeSpan(7, 1, 0, 0))
    End Sub

    Private Function GetAllClients() As Boolean
        cmbClients.DataSource = DB.Dashboard.GetAllClients(CurrentUserID)
        cmbClients.DataBind()
        Return True
    End Function

    Protected Sub cmbClients_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbClients.SelectedIndexChanged
        Session("ClientId") = cmbClients.SelectedValue
        Session("ClientName") = cmbClients.SelectedItem.Text
        Dim t = New HttpCookie("CurrentClientID", cmbClients.SelectedValue)
        t.Expires = Now.AddHours(authenticationTimeOutHours)
        Response.SetCookie(t)
        hidAccessibleModules.Value = ""
        hidAccessibleModules.Value = GetAccessibleModules(CurrentUserID, CurrentClientID)
        hidAccessibleModules.Value = hidAccessibleModules.Value.Trim(CChar("""")).Trim(CChar(","))
    End Sub

    Private Sub ProcessNeedToOperateRequests()
        Dim mailHeader As New StringBuilder
        With mailHeader
            .Append("<div style='font-family: Verdana, Calibri, Trebuchet MS, Arial; font-size: 11px;'>This is to remind you that some of the rate requests seating on your desk requires your attention. Please visit <a href='http://rraw.invoize.com'>RRAW Portal</a> and clean up your desk.<br /><br />Here is the list of such requests...<br><br>")
            .Append("<style type='text/css'>td,th{border:1px solid gray}</style>")
            .Append("<table cellpadding='2' cellspacing='0' style='border:1px solid gray;font-family: Verdana, Calibri, Trebuchet MS, Arial; font-size: 11px;'>")
        End With

        Dim mailFooter As New StringBuilder
        With mailFooter
            .Append("</table>")
            .Append("</div>")
        End With

        Dim dt As DataTable = DB.AirRateRequests.GetNeedToOperateRequests
        If dt.Rows.Count > 0 Then
            Dim CM As New CustomMail("Pending Rate Requests Reminder from RRAW Portal", mailHeader.ToString, mailFooter.ToString)

            Dim currentEmail As String = dt.Rows(0)("Email").ToString
            Dim oldEmail As String = dt.Rows(0)("Email").ToString

            CM.AddRow(GetMailContentHeader(dt).ToString)

            For Each row As DataRow In dt.Rows
                currentEmail = row("Email").ToString

                If currentEmail = oldEmail Then
                    CM.AddRow(GetMailContentItem(dt, row).ToString)
                Else
                    oldEmail = currentEmail

                    CM.SendCustomMail(currentEmail)

                    CM = New CustomMail("Pending Rate Requests Reminder from RRAW Portal", mailHeader.ToString, mailFooter.ToString)

                    CM.AddRow(GetMailContentHeader(dt).ToString)
                    CM.AddRow(GetMailContentItem(dt, row).ToString)
                End If
            Next

            CM.SendCustomMail(currentEmail)
        End If

        DB.AirRateRequests.UpdateNeedToOperateRequestsForMailOut()
    End Sub

    Private Function GetMailContentHeader(ByRef dt As DataTable) As StringBuilder
        Dim content As New StringBuilder

        With content
            .Append("<tr>")
            For Each column As DataColumn In dt.Columns
                If column.ColumnName <> "Email" Then
                    .Append("<th>" & column.ColumnName & "</th>")
                End If
            Next
            .Append("</tr>")
        End With

        Return content
    End Function

    Private Function GetMailContentItem(ByRef dt As DataTable, ByRef row As DataRow) As StringBuilder
        Dim content As New StringBuilder

        With content
            .Append("<tr>")
            For Each column As DataColumn In dt.Columns
                If column.ColumnName <> "Email" Then
                    .Append("<td>")
                    If column.ColumnName = "ID" Then
                        .Append("<a href='http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & row(column.ColumnName).ToString & "'>" & row(column.ColumnName).ToString & "</a>")
                    Else
                        .Append(row(column.ColumnName))
                    End If
                    .Append("</td>")
                End If
            Next
            .Append("</tr>")
        End With

        Return content
    End Function

    Private Sub ProcessNeedToTransferRequests()
        Dim mailHeader As New StringBuilder
        With mailHeader
            .Append("<div style='font-family: Verdana, Calibri, Trebuchet MS, Arial; font-size: 11px;'>As {SENDER} have not paid attention to some of the rate requests seating on his/her desk from last 72 hours, those rate reqests are now transferred to {RECEIVER}. Here is the list of such requests...<br><br>")
            .Append("<style type='text/css'>td,th{border:1px solid gray}</style>")
            .Append("<table cellpadding='2' cellspacing='0' style='border:1px solid gray;font-family: Verdana, Calibri, Trebuchet MS, Arial; font-size: 11px;'>")
        End With

        Dim mailFooter As New StringBuilder
        With mailFooter
            .Append("</table>")
            .Append("</div>")
        End With

        Dim dt As DataTable = DB.AirRateRequests.GetNeedToTransferRequests
        If dt.Rows.Count > 0 Then
            Dim CM As New CustomMail("Rate Requests Transfer Alert from RRAW Portal", mailHeader.ToString, mailFooter.ToString)

            Dim currentEmail As String = dt.Rows(0)("Email").ToString
            Dim oldEmail As String = dt.Rows(0)("Email").ToString
            Dim nextEmail As String = dt.Rows(0)("Next Operator").ToString

            CM.AddRow(GetMailContentHeader(dt).ToString)

            For Each row As DataRow In dt.Rows
                currentEmail = row("Email").ToString
                nextEmail = row("Next Operator").ToString

                If currentEmail = oldEmail Then
                    CM.AddRow(GetMailContentItem(dt, row).ToString)
                Else
                    oldEmail = currentEmail

                    CM.Subject = CM.Subject.Replace("{SENDER}", currentEmail)
                    CM.Subject = CM.Subject.Replace("{RECEIVER}", nextEmail)
                    CM.SendCustomMail(currentEmail)
                    CM.SendCustomMail(nextEmail)

                    CM = New CustomMail("Rate Requests Transfer Alert from RRAW Portal", mailHeader.ToString, mailFooter.ToString)

                    CM.AddRow(GetMailContentHeader(dt).ToString)
                    CM.AddRow(GetMailContentItem(dt, row).ToString)
                End If
            Next

            CM.Subject = CM.Subject.Replace("{SENDER}", currentEmail)
            CM.Subject = CM.Subject.Replace("{RECEIVER}", nextEmail)
            CM.SendCustomMail(currentEmail)
            CM.SendCustomMail(nextEmail)
        End If

        DB.AirRateRequests.UpdateNeedToTransferRequestsForMailOut()
    End Sub

    Private Sub mainFrame_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles mainFrame.Load
        lblUserName.Text = CurrentUserName
    End Sub
End Class