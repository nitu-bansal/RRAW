Imports RRAW.AirRateRequests
Public Class Dashboard
    Inherits System.Web.UI.Page

    Shared getRateRequestSelectCommand As String
    Shared showSummarySelectCommand As String
    Shared statusTitle As String

    Private Function PeriodFrom() As Date
        Return Convert.ToDateTime(txtPeriodFrom.Text)
    End Function

    Private Function PeriodTo() As Date
        Return Convert.ToDateTime(hidPeriodTo.Value)
    End Function

    

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtPeriodTo.Text = Now.ToString("MM/dd/yyyy")
        hidPeriodTo.Value = Convert.ToDateTime(txtPeriodTo.Text).AddDays(1).ToString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SendAlertEmailToApprover()
            SendAlertEmailToAccountManager()
            SendEmailToApproverAndUpdateStatus()


            gridRateRequestGroupings.Columns(2).HeaderText = "In_Discussion (" & CurrentClientName & ")"
            If CurrentUserID <= 0 Then Response.Redirect("Login.aspx?ResponsePage=" & Request.Url.OriginalString, True)

            If DB.UserModulesMapping.IsAccessAllowed(CurrentUserID, Me.Page.ToString().Substring(4, Me.Page.ToString().Substring(4).Length - 5), CurrentClientID) = False Then Response.Redirect("Tariff.aspx")

          

            If CurrentUserType <> "Admin" Then
                getRateRequestSelectCommand = "GetOpenToMeRequests"
            Else
                getRateRequestSelectCommand = "GetRecentRateRequests"
                statusTitle = "Recent activity on requests"
                lblStatusTitle.Text = statusTitle
            End If
            showSummarySelectCommand = "GetCountryWiseRateRequestStatusCounts"

            Dim curMM As String = Now.ToString("MM")
            Dim curYYYY As String = Now.ToString("yyyy")

            If (Convert.ToInt32(curMM) >= 1 And Convert.ToInt32(curMM) < 4) Then
                curMM = "01"
            ElseIf (Convert.ToInt32(curMM) >= 4 And Convert.ToInt32(curMM) < 7) Then
                curMM = "04"
            ElseIf (Convert.ToInt32(curMM) >= 7 And Convert.ToInt32(curMM) < 10) Then
                curMM = "07"
            ElseIf (Convert.ToInt32(curMM) >= 10) Then
                curMM = "10"
            End If

            txtPeriodFrom.Text = curMM & "/01/" & curYYYY
            'txtPeriodFrom.Text = DB.AirRateRequests.GetMinRequestDate
            txtPeriodTo.Text = Now.ToString("MM/dd/yyyy") 'DB.AirRateRequests.GetMaxRequestDate

            BrowserAdjustments()
        End If

        linkInDiscussionClientRequests.Text = "In_Discussion (" & CurrentClientName & ")"
        linkInDiscussionInLoopClientRequests.Text = "In_Discussion (" & CurrentClientName & ")"

        linkInDiscussionClientRequests.ToolTip = "Click here to see In Discussion (" & CurrentClientName & ") requests"
        linkInDiscussionInLoopClientRequests.ToolTip = "Click here to see In Discussion (" & CurrentClientName & ") requests"

        gridRateRequestGroupings.Columns(2).HeaderText = "In_Discussion (" & CurrentClientName & ")"

        If Not Page.IsPostBack Then
            FillupStats()
        ElseIf Request("__EVENTTARGET").Contains("txtPeriodFrom") Or Request("__EVENTTARGET").Contains("txtPeriodTo") Then
            FillupStats()
        End If

        SqlDataSource1.ConnectionString = CurrentDBConnection
        SqlDataSource1.SelectCommand = showSummarySelectCommand
        SqlDataSource1.DataBind()

        SqlDataSource3.ConnectionString = CurrentDBConnection
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        SqlDataSource3.DataBind()

        ApplyFilter()

        If Request("__EVENTTARGET") <> Nothing Then
            If Request("__EVENTTARGET").Contains("linkExportToCSV") Then
                linkExportToCSV_Click(sender, e)
            ElseIf Request("__EVENTTARGET").Contains("linkExportToExcel") Then
                linkExportToExcel_Click(sender, e)
            ElseIf Request("__EVENTTARGET").Contains("btnClearFilter") Then
                ClearFilterValues()
            End If
        End If
    End Sub

    Private Sub FillupStats()
        lblOpenToAllRequests.Text = DB.AirRateRequests.GetOpenRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblInDiscussionStationRequests.Text = DB.AirRateRequests.GetInDiscussionStationRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblInDiscussionClientRequests.Text = DB.AirRateRequests.GetInDiscussionClientRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblApprovedRequests.Text = DB.AirRateRequests.GetApprovedRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblRejectedRequests.Text = DB.AirRateRequests.GetRejectedRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString

        lblOpenToMeRequests.Text = DB.AirRateRequests.GetOpenToMeRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblInDiscussionInLoopStationRequests.Text = DB.AirRateRequests.GetInDiscussionInLoopStationRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblInDiscussionInLoopClientRequests.Text = DB.AirRateRequests.GetInDiscussionInLoopClientRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblApprovedInLoopRequests.Text = DB.AirRateRequests.GetApprovedInLoopRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString
        lblRejectedInLoopRequests.Text = DB.AirRateRequests.GetRejectedInLoopRequestsCount(CurrentUserID, CurrentClientID, PeriodFrom, PeriodTo).ToString

        lblOpenToMeRequests.ForeColor = FigureColors(CInt(lblOpenToMeRequests.Text), 0, 5)
    End Sub

    Private Sub ApplyFilter()
        Dim filter As New StringBuilder
        If hidID.Value <> "" Then
            filter.Append("RateRequestID = " & Val(hidID.Value).ToString)
        End If

        If hidRequestDate.Value <> "" Then
            hidRequestDate.Value = ToDate(hidRequestDate.Value)

            filter.Append(" AND [Request Date] LIKE '%" & hidRequestDate.Value & "%'")
        End If

        If hidRequestor.Value <> "" Then
            filter.Append(" AND Requestor LIKE '%" & hidRequestor.Value & "%'")
        End If

        If hidEffectiveDate.Value <> "" Then
            filter.Append(" AND [Effective Date] LIKE '%" & hidEffectiveDate.Value & "%'")
        End If

        If hidExpiryDate.Value <> "" Then
            filter.Append(" AND [Expiry Date] LIKE '%" & hidExpiryDate.Value & "%'")
        End If

        If hidStatus.Value <> "" Then
            filter.Append(" AND [Status] LIKE '%" & hidStatus.Value & "%'")
        End If

        If hidCurrentApprover.Value <> "" Then
            filter.Append(" AND [Current Approver] LIKE '%" & hidCurrentApprover.Value & "%'")
        End If

        If hidOriginComment.Value <> "" Then
            filter.Append(" AND [Origin Comment] LIKE '%" & hidOriginComment.Value & "%'")
        End If

        SqlDataSource3.FilterExpression = If(filter.ToString.StartsWith(" AND"), filter.ToString.Substring(5), filter.ToString)

        lblStatusTitle.Text = statusTitle
    End Sub

    Private Function ToDate(ByVal dateString As String) As String
        If IsDate(hidRequestDate.Value) = True Then
            Return CDate(dateString).ToString("MM/dd/yyyy")
        Else
            Dim temp As String() = hidRequestDate.Value.Split("-"c)
            If temp.Length = 3 Then
                Dim temp1 As String = temp(1) & "/" & temp(0) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
            End If
            temp = hidRequestDate.Value.Split("/"c)
            If temp.Length = 3 Then
                Dim temp1 As String = temp(1) & "/" & temp(0) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
            End If
            temp = hidRequestDate.Value.Split("."c)
            If temp.Length = 3 Then
                Dim temp1 As String = temp(0) & "/" & temp(1) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
                temp1 = temp(1) & "/" & temp(0) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
            End If
            temp = hidRequestDate.Value.Split("_"c)
            If temp.Length = 3 Then
                Dim temp1 As String = temp(0) & "/" & temp(1) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
                temp1 = temp(1) & "/" & temp(0) & "/" & temp(2)
                If IsDate(temp1) Then
                    Return CDate(temp1).ToString("MM/dd/yyyy")
                End If
            End If
            Return dateString
        End If
    End Function

    Private Function FigureColors(ByVal value As Integer, ByVal greenLevelValue As Integer, ByVal yellowLevelValue As Integer) As Drawing.Color
        Select Case value
            Case Is <= greenLevelValue
                Return Drawing.Color.Green
            Case Is <= yellowLevelValue
                Return Drawing.Color.Red
            Case Else
                Return Drawing.Color.DarkRed
        End Select
    End Function

    Private Sub BrowserAdjustments()
        Select Case Request.Browser.Browser
            Case "IE"
                Select Case Request.Browser.Version
                    Case "7.0"
                        mainDiv2.Height = 495
                        tb1.Width = "98%"
                        tb2.Width = "98%"
                    Case Else
                        mainDiv2.Height = 512
                End Select
            Case Else
                mainDiv2.Height = 600
        End Select
    End Sub

    Private Sub gridRateRequests_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridRateRequests.DataBound
        For i As Integer = 0 To gridRateRequests.Rows.Count - 1

            Dim dtRequestDate As Date

            If IsDate(gridRateRequests.Rows(i).Cells(1).Text) = True Then
                dtRequestDate = CDate(gridRateRequests.Rows(i).Cells(1).Text)
            End If
            'line added by sushrut
            gridRateRequests.Rows(i).Cells(0).Text = If(gridRateRequests.Rows(i).Cells(9).Text = "1", "<img class='attachmentImage' src='Images/cleardot.gif'> ", "") & If(gridRateRequests.Rows(i).Cells(9).Text = "AIR", "<a href='NewAirRateRequest.aspx?RateRequestID=" & gridRateRequests.Rows(i).Cells(0).Text & "&TransPortModeId=1&rand=" & (New Random).Next(9999) & "'>" & gridRateRequests.Rows(i).Cells(0).Text & "</a>", If(gridRateRequests.Rows(i).Cells(9).Text = "GROUND", "<a href='NewGroundRateRequest.aspx?RateRequestID=" & gridRateRequests.Rows(i).Cells(0).Text & "&TransPortModeId=3&rand=" & (New Random).Next(9999) & "'>" & gridRateRequests.Rows(i).Cells(0).Text & "</a>", "<a href='NewOceanRateRequest.aspx?RateRequestID=" & gridRateRequests.Rows(i).Cells(0).Text & "&TransPortModeId=2&rand=" & (New Random).Next(9999) & "'>" & gridRateRequests.Rows(i).Cells(0).Text & "</a>"))




            If (gridRateRequests.Rows(i).Cells(9).Text = "AIR") Then
                gridRateRequests.Rows(i).BackColor = Drawing.Color.White
                gridRateRequests.Rows(i).ToolTip = "Air Rate Request"
            ElseIf (gridRateRequests.Rows(i).Cells(9).Text = "OCEAN") Then
                gridRateRequests.Rows(i).BackColor = Drawing.Color.FromArgb(185, 190, 245)
                gridRateRequests.Rows(i).ToolTip = "Ocean Rate Request"
            ElseIf (gridRateRequests.Rows(i).Cells(9).Text = "GROUND") Then
                gridRateRequests.Rows(i).BackColor = Drawing.Color.FromArgb(183, 194, 205)
                gridRateRequests.Rows(i).ToolTip = "Ground Rate Request"
            End If
        Next
        If gridRateRequests.Rows.Count > 0 Then
            With gridRateRequests
                .Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                .Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Center
                .Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                .Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                .Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                .Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                .Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                .Columns(7).ItemStyle.HorizontalAlign = HorizontalAlign.Left

                .Columns(0).ItemStyle.CssClass = "colID"
                .Columns(1).ItemStyle.CssClass = "colRequestDate"
                .Columns(2).ItemStyle.CssClass = "colRequestor"
                .Columns(3).ItemStyle.CssClass = "colCurrentApprover"
                .Columns(4).ItemStyle.CssClass = "colEffectiveDate"
                .Columns(5).ItemStyle.CssClass = "colExpiryDate"
                .Columns(6).ItemStyle.CssClass = "colStatus"

                .Columns(7).ItemStyle.CssClass = "colOriginComment"

            End With
        End If

        gridRateRequests.Columns(8).HeaderStyle.CssClass = "hideControl"
        gridRateRequests.Columns(8).ItemStyle.CssClass = "hideControl"
        gridRateRequests.Columns(8).FooterStyle.CssClass = "hideControl"
        gridRateRequests.Columns(9).HeaderStyle.CssClass = "hideControl"
        gridRateRequests.Columns(9).ItemStyle.CssClass = "hideControl"
        gridRateRequests.Columns(9).FooterStyle.CssClass = "hideControl"
    End Sub

    Private Sub ClearFilterValues()
        hidID.Value = ""
        hidRequestDate.Value = ""
        hidRequestor.Value = ""
        hidEffectiveDate.Value = ""
        hidExpiryDate.Value = ""
        hidStatus.Value = ""

        hidCurrentApprover.Value = ""
        hidOriginComment.Value = ""

        SqlDataSource3.FilterExpression = ""

    End Sub

    Protected Sub linkOpenToMeRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkOpenToMeRequests.Click
        getRateRequestSelectCommand = "GetOpenToMeRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on requests that are Open To Me"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkOpenRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkOpenRequests.Click
        getRateRequestSelectCommand = "GetOpenRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on requests that are Open for Others"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkInDiscussionStationRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkInDiscussionStationRequests.Click
        getRateRequestSelectCommand = "GetInDiscussionStationRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on my In Discussion requests"
        lblStatusTitle.Text = statusTitle
    End Sub

    'Protected Sub cmbClients_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbClients.SelectedIndexChanged
    '    Session("ClientId") = cmbClients.SelectedValue
    '    FillupStats()
    '    hidAccessibleModules.Value = ""
    '    hidAccessibleModules.Value = GetAccessibleModules(CurrentUserID, CurrentClientID)
    '    hidAccessibleModules.Value = hidAccessibleModules.Value.Trim(CChar("""")).Trim(CChar(","))
    'End Sub

    Protected Sub linkApprovedRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkApprovedRequests.Click
        getRateRequestSelectCommand = "GetApprovedRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on my Approved requests"
        lblStatusTitle.Text = statusTitle
    End Sub

    Private Sub gridRateRequests_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridRateRequests.PageIndexChanging
        If e.NewPageIndex = -1 Then
            gridRateRequests.PageIndex = Integer.MaxValue
        Else
            gridRateRequests.PageIndex = e.NewPageIndex
        End If
        gridRateRequests.DataBind()
    End Sub

    Private Sub gridRateRequests_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRateRequests.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            With e.Row
                CType(.Cells(0).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(1).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(2).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(3).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(4).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(5).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(6).Controls(0), LinkButton).TabIndex = -1
                CType(.Cells(7).Controls(0), LinkButton).TabIndex = -1

                .Cells(0).Width = 55

                Dim ltrSpace0 As New Literal
                ltrSpace0.Text = "<br />"
                .Cells(0).Controls.Add(ltrSpace0)

                Dim txtID As New TextBox
                With txtID
                    .ID = "txtID"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidID').value = this.value")
                    .Width = Unit.Percentage(80)
                    .Style.Add("margin-top", "5px")
                    .Style.Add("text-align", "right")
                    .Text = hidID.Value
                    'AddHandler(txtID.TextChanged,
                End With
                .Cells(0).Controls.Add(txtID)
                .Cells(0).Style.Add("padding-left", "0")
                .Cells(0).Style.Add("padding-right", "0")

                Dim ltrSpace As New Literal
                ltrSpace.Text = "<br />"
                .Cells(0).Controls.Add(ltrSpace)
                .Cells(1).Controls.Add(ltrSpace)

                Dim txtRequestDate As New TextBox
                With txtRequestDate
                    .ID = "txtRequestDate"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidRequestDate').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Style.Add("text-align", "center")
                    .Text = hidRequestDate.Value
                End With
                .Cells(1).Controls.Add(txtRequestDate)
                .Cells(1).Style.Add("padding-right", "10px")

                .Cells(2).Controls.Add(ltrSpace)

                Dim txtRequestor As New TextBox
                With txtRequestor
                    .ID = "txtRequestor"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidRequestor').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Text = hidRequestor.Value
                End With
                .Cells(2).Controls.Add(txtRequestor)
                .Cells(2).Style.Add("padding-right", "10px")

                .Cells(3).Controls.Add(ltrSpace)

                Dim txtCurrentApprover As New TextBox
                With txtCurrentApprover
                    .ID = "txtCurrentApprover"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidCurrentApprover').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Text = hidCurrentApprover.Value
                End With
                .Cells(3).Controls.Add(txtCurrentApprover)
                .Cells(3).Style.Add("padding-right", "10px")

                Dim txtEffectiveDate As New TextBox
                With txtEffectiveDate
                    .ID = "txtEffectiveDate"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidEffectiveDate').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Text = hidEffectiveDate.Value
                End With
                .Cells(4).Controls.Add(txtEffectiveDate)
                .Cells(4).Style.Add("padding-right", "10px")

                .Cells(5).Controls.Add(ltrSpace)

                Dim txtExpiryDate As New TextBox
                With txtExpiryDate
                    .ID = "txtExpiryDate"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidExpiryDate').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Text = hidExpiryDate.Value
                End With
                .Cells(5).Controls.Add(txtExpiryDate)
                .Cells(5).Style.Add("padding-right", "10px")

                .Cells(6).Controls.Add(ltrSpace)

                Dim txtStatus As New TextBox
                With txtStatus
                    .ID = "txtStatus"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidStatus').value = this.value")
                    .Width = Unit.Percentage(100)
                    .Style.Add("margin-top", "5px")
                    .Text = hidStatus.Value
                End With
                .Cells(6).Controls.Add(txtStatus)


                .Cells(7).Controls.Add(ltrSpace)

                Dim txtOriginComment As New TextBox
                With txtOriginComment
                    .ID = "txtOriginComment"
                    .EnableViewState = True
                    .Attributes.Add("onChange", "document.getElementById('hidOriginComment').value = this.value")
                    .Width = Unit.Percentage(46)
                    .Style.Add("margin-top", "5px")
                    .Text = hidOriginComment.Value
                End With
                .Cells(7).Controls.Add(txtOriginComment)

                Dim btnApplyFilter As New Button
                With btnApplyFilter
                    .ID = "btnApplyFilter"
                    .Style.Add("margin", "0 5px")
                    .Style.Add("padding", "0 5px")
                    .Style.Add("height", "20px")
                    If (Request.Browser.Browser <> "IE") Then
                        .Style.Add("line-height", "0")
                        .Style.Add("min-width", "10px")
                        .Style.Add("min-height", "20px")
                    End If
                    .Text = "Filter"
                End With
                .Cells(7).Controls.Add(btnApplyFilter)

                Dim btnClearFilter As New Button
                With btnClearFilter
                    .ID = "btnClearFilter"
                    .OnClientClick = "ClearFilterValues()"
                    '.Style.Add("margin", "0 0 -5px 5px")
                    .Style.Add("padding", "0 5px")
                    .Style.Add("height", "20px")
                    If (Request.Browser.Browser <> "IE") Then
                        .Style.Add("line-height", "0")
                        .Style.Add("min-width", "10px")
                        .Style.Add("min-height", "20px")
                    End If
                    .Text = "Clear"
                End With
                .Cells(7).Controls.Add(btnClearFilter)
            End With
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).ColumnSpan = 2
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(0).Text = "Pages: " & (gridRateRequests.PageIndex + 1).ToString & "/" & gridRateRequests.PageCount.ToString
            e.Row.Cells(1).Visible = False
            e.Row.Cells(6).ColumnSpan = 2
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right

            Dim linkExportToExcel As New LinkButton
            linkExportToExcel.ID = "linkExportToExcel"
            linkExportToExcel.Text = "<b>Export to Excel</b>"
            AddHandler linkExportToExcel.Click, AddressOf linkExportToExcel_Click
            linkExportToExcel.ForeColor = Drawing.Color.White
            e.Row.Cells(6).Controls.Add(linkExportToExcel)

            Dim lblSeparator As New Label
            lblSeparator.Text = " | "
            lblSeparator.ForeColor = Drawing.Color.White
            e.Row.Cells(6).Controls.Add(lblSeparator)

            Dim linkExportToCSV As New LinkButton
            linkExportToCSV.ID = "linkExportToCSV"
            linkExportToCSV.Text = "<b>Export to CSV</b>"
            AddHandler linkExportToCSV.Click, AddressOf linkExportToCSV_Click
            linkExportToCSV.ForeColor = Drawing.Color.White
            e.Row.Cells(6).Controls.Add(linkExportToCSV)
            e.Row.Cells(7).Visible = False
        End If

    End Sub

    Private Sub linkExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            CurrentSelectCommand = SqlDataSource3.SelectCommand
            CurrentFilterExpression = SqlDataSource3.FilterExpression
            Response.Redirect("ExportFile.aspx?isStoredProcedure=TRUE&fileNamePrefix=Request&fileType=Excel&UserID=" & CurrentUserID & "&FromDate=" & txtPeriodFrom.Text & "&ToDate=" & txtPeriodTo.Text & "&ClientId=" & CurrentClientID, False)
        Catch ex As Exception
            Server.Transfer("Errors.aspx?Msg=" & ex.Message)
        End Try
    End Sub

    Private Sub linkExportToCSV_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            CurrentSelectCommand = SqlDataSource3.SelectCommand
            CurrentFilterExpression = SqlDataSource3.FilterExpression
            Response.Redirect("ExportFile.aspx?isStoredProcedure=TRUE&fileNamePrefix=Request&UserID=" & CurrentUserID & "&FromDate=" & txtPeriodFrom.Text & "&ToDate=" & txtPeriodTo.Text & "&ClientId=" & CurrentClientID, False)
            'hidCallback.Text = (New Random).Next.ToString
        Catch ex As Exception
            Server.Transfer("Errors.aspx?Msg=" & ex.Message)
        End Try
    End Sub

    Private Sub gridRateRequestGroupings_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridRateRequestGroupings.DataBound
        Dim comp As String = ""
        For Each row As GridViewRow In gridRateRequestGroupings.Rows
            If comp = row.Cells(0).Text Then row.Cells(0).Text = ""
            If row.Cells(0).Text <> "" Then comp = row.Cells(0).Text
        Next
    End Sub

    Private Sub gridRateRequestGroupings_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridRateRequestGroupings.PageIndexChanging
        gridRateRequestGroupings.PageIndex = e.NewPageIndex
        gridRateRequestGroupings.DataBind()
    End Sub

    Private Sub gridRateRequestGroupings_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRateRequestGroupings.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).ToolTip = DB.AirRateRequests.GetAllOpenRequestsCount(PeriodFrom, PeriodTo, CurrentClientID).ToString & " request(s) are Open" & vbNewLine & "Click on link to see all"
            e.Row.Cells(1).ToolTip = DB.AirRateRequests.GetAllInDiscussionStationRequestsCount(PeriodFrom, PeriodTo, CurrentClientID).ToString & " request(s) are In Discussion with CEVA" & vbNewLine & "Click on link to see all"
            e.Row.Cells(2).ToolTip = DB.AirRateRequests.GetAllInDiscussionClientRequestsCount(PeriodFrom, PeriodTo, CurrentClientID).ToString & " request(s) are In Discussion with WD" & vbNewLine & "Click on link to see all"
            e.Row.Cells(3).ToolTip = DB.AirRateRequests.GetAllApprovedRequestsCount(PeriodFrom, PeriodTo, CurrentClientID).ToString & " request(s) were Approved" & vbNewLine & "Click on link to see all"
            e.Row.Cells(4).ToolTip = DB.AirRateRequests.GetAllRejectedRequestsCount(PeriodFrom, PeriodTo, CurrentClientID).ToString & " request(s) were Rejected" & vbNewLine & "Click on link to see all"
            e.Row.Cells(2).ToolTip = e.Row.Cells(4).ToolTip.Replace("WD", CurrentClientName.ToString())


        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).ColumnSpan = 2
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(0).Text = "Pages: " & (gridRateRequestGroupings.PageIndex + 1).ToString & "/" & gridRateRequestGroupings.PageCount.ToString
            e.Row.Cells(1).Visible = False
        End If
    End Sub


    Protected Sub linkRejectedRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkRejectedRequests.Click
        getRateRequestSelectCommand = "GetRejectedRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on my Rejected requests"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkInDiscussionRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkInDiscussionClientRequests.Click
        getRateRequestSelectCommand = "GetInDiscussionClientRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on my In Discussion requests"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkInDiscussionInLoopStationRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkInDiscussionInLoopStationRequests.Click
        getRateRequestSelectCommand = "GetInDiscussionInLoopStationRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on Station In Discussion requests looped to me"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkInDiscussionInLoopClientRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkInDiscussionInLoopClientRequests.Click
        getRateRequestSelectCommand = "GetInDiscussionInLoopClientRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on Client In Discussion requests looped to me"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkApprovedInLoopRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkApprovedInLoopRequests.Click
        getRateRequestSelectCommand = "GetApprovedInLoopRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on Approved requests looped to me"
        lblStatusTitle.Text = statusTitle
    End Sub

    Protected Sub linkRejectedInLoopRequests_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkRejectedInLoopRequests.Click
        getRateRequestSelectCommand = "GetRejectedInLoopRequests"
        SqlDataSource3.SelectCommand = getRateRequestSelectCommand
        ClearFilterValues()
        gridRateRequests.DataBind()
        statusTitle = "Recent activity on Rejected requests looped to me"
        lblStatusTitle.Text = statusTitle
    End Sub

    Private Sub gridRateRequestGroupings_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gridRateRequestGroupings.Sorting
        If e.SortExpression <> "Origin Region" And e.SortExpression <> "Origin Port" Then
            e.Cancel = True
            Select Case e.SortExpression
                Case "Open"
                    getRateRequestSelectCommand = "GetAllOpenRequests"
                    statusTitle = "Recent activity on all Open requests"
                Case "In_Discussion (Station)"
                    getRateRequestSelectCommand = "GetAllInDiscussionStationRequests"
                    statusTitle = "Recent activity on all In Discussion with Station requests"
                Case "In_Discussion (Client)"
                    getRateRequestSelectCommand = "GetAllInDiscussionClientRequests"
                    statusTitle = "Recent activity on all In Discussion with " & CurrentClientName.ToString() & " requests"
                Case "Approved"
                    getRateRequestSelectCommand = "GetAllApprovedRequests"
                    statusTitle = "Recent activity on all Approved requests"
                Case "Rejected"
                    getRateRequestSelectCommand = "GetAllRejectedRequests"
                    statusTitle = "Recent activity on all Rejected requests"
            End Select
            SqlDataSource3.SelectCommand = getRateRequestSelectCommand
            ClearFilterValues()
            gridRateRequests.DataBind()

            lblStatusTitle.Text = statusTitle
        End If
    End Sub

    '''module to send email alert to approver

    Private Sub SendAlertEmailToApprover()
        Dim dtApprover As DataTable = DB.Dashboard.GetAllApproverToSendEmail(1)
        For Each row As DataRow In dtApprover.Rows
            Dim strBody As New StringBuilder
            Dim temp As String() = row("RateRequestIds").ToString.Split(","c)
            strBody.Append("Hi " & row("ApproverName").ToString & ", " & row("PendingRequests").ToString() & " Rate requests which are pending will be approved by system and sent to next approver " & row("NextApprover").ToString() & ", so please take action on them within one hour, Find rate request details at")
            For Each raterequestid As String In temp
                If row("TransportMode").ToString.ToUpper = "AIR" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                ElseIf row("TransportMode").ToString.ToUpper = "OCEAN" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                ElseIf row("TransportMode").ToString.ToUpper = "GROUND" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=TruckRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                End If
            Next
            SendMail(row("ApproverEmail").ToString, "Auto approval alert mail", strBody.ToString)
        Next
    End Sub

    Private Sub SendAlertEmailToAccountManager()
        Dim dtApprover As DataTable = DB.Dashboard.GetAllAccountManagerToSendEmail(1)
        For Each row As DataRow In dtApprover.Rows
            Dim strBody As New StringBuilder
            Dim temp As String() = row("RateRequestIds").ToString.Split(","c)
            strBody.Append("Hi " & row("ApproverName").ToString & ", " & row("PendingRequests").ToString() & " Rate requests are pending, so please take action on them , Find rate request details at")
            For Each raterequestid As String In temp
                If row("TransportMode").ToString.ToUpper = "AIR" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                ElseIf row("TransportMode").ToString.ToUpper = "OCEAN" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                ElseIf row("TransportMode").ToString.ToUpper = "GROUND" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=TruckRateRequest.aspx?RateRequestID=" & raterequestid & ",\n.")
                End If
            Next
            SendMail(row("ApproverEmail").ToString, "Auto approval alert mail", strBody.ToString)
        Next
    End Sub

    Private Sub SendEmailToApproverAndUpdateStatus()
        '''email sent to current approver
        Dim dtApprover As DataTable = DB.Dashboard.GetAllApproverToSendEmail(0)
        Dim obj As New RRAW.AirRateRequests


        For Each row As DataRow In dtApprover.Rows
            Dim strBody As New StringBuilder
            Dim temp As String() = row("RateRequestIds").ToString.Split(","c)
            For Each raterequestid As String In temp
                obj.ApproveRateRequestBySystem(Convert.ToInt32(raterequestid), Convert.ToInt32(row("NextApproverId")), Convert.ToInt32(row("ApproverId")), "Auto approved by system", EnumStatus.EscalatedToNextLevel)
            Next

            strBody.Append("Hi " & row("ApproverName").ToString & ", " & row("PendingRequests").ToString() & " Rate requests which are pending have been approved by system and sent to next approver " & row("NextApprover").ToString() & ", Find rate request details at")
            For Each raterequestid As String In temp
                If row("TransportMode").ToString.ToUpper = "AIR" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                ElseIf row("TransportMode").ToString.ToUpper = "OCEAN" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                ElseIf row("TransportMode").ToString.ToUpper = "GROUND" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=TruckRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                End If
            Next
            SendMail(row("ApproverEmail").ToString, "Auto approval mail", strBody.ToString)

        Next
        '''email sent to next approver
        For Each row As DataRow In dtApprover.Rows
            Dim strBody As New StringBuilder
            Dim temp As String() = row("RateRequestIds").ToString.Split(","c)
            strBody.Append("Hi " & row("NextApprover").ToString & ", " & row("PendingRequests").ToString() & " Rate requests which were pending for " & row("ApproverName").ToString & " have been moved to your bucket by system, Find rate request details at")
            For Each raterequestid As String In temp
                If row("TransportMode").ToString.ToUpper = "AIR" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                ElseIf row("TransportMode").ToString.ToUpper = "OCEAN" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                ElseIf row("TransportMode").ToString.ToUpper = "GROUND" Then
                    strBody.Append(" http://rraw.invoize.com/Master.aspx?ChildPage=TruckRateRequest.aspx?RateRequestID=" & raterequestid & ",</BR>")
                End If
            Next
            SendMail(row("NextApproverEmail").ToString, "Auto approval mail", strBody.ToString)
        Next
    End Sub



End Class
