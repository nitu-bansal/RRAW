Option Strict Off

Imports System.Security.Cryptography
Imports System.Text
Imports System.Configuration

Friend Module General
    Friend Const NullString As String = ""
    Friend globalExpiryDate As Date = "2012-06-30"
    Friend Const pageExpiresInDays As Integer = 30
    Friend Const authenticationTimeOutHours As Integer = 24
    Friend databaseEntryName As String

    Friend Structure structReleaseInfo
        Private Shared varRelease As String
        Private Shared varTitle As String
        Private Shared varFilePath As String
        Private Shared varPostedOn As Date
        Private Shared varPostedBy As String

        Friend Sub New(ByVal release As String, ByVal title As String, ByVal filePath As String, ByVal postedOn As Date, ByVal postedBy As String)
            varRelease = release
            varTitle = title
            varFilePath = filePath
            varPostedOn = postedOn
            varPostedBy = postedBy
        End Sub

        Friend ReadOnly Property Release As String
            Get
                Return varRelease
            End Get
        End Property

        Friend ReadOnly Property Title As String
            Get
                Return varTitle
            End Get
        End Property

        Friend ReadOnly Property FilePath As String
            Get
                Return varFilePath
            End Get
        End Property

        Friend ReadOnly Property PostedOn As Date
            Get
                Return varPostedOn
            End Get
        End Property

        Friend ReadOnly Property PostedBy As String
            Get
                Return varPostedBy
            End Get
        End Property

    End Structure

    Private Sub TemporaryConnnectionStorage()
        Dim RRAWDatabaseConnectionLOCAL As String = "Data Source=.\sqlexpress;Initial Catalog=RAWDB;Persist Security Info=True;User ID=searce;Password=searce"
        Dim RRAWDatabaseConnectionTEST As String = "Data Source=67.228.4.188,2897\INVOIZEDB;Initial Catalog=RAWDB_Test;Persist Security Info=True;User ID=searce;Password=searce"
        Dim RRAWDatabaseConnectionLIVE As String = "Data Source=SV2657\INVOIZEDB;Initial Catalog=RAWDB;Persist Security Info=True;User ID=searce;Password=searce"
        Dim RRAWDatabaseConnectionDEV_OLD As String = "Data Source=192.168.200.220;Initial Catalog=RAWDB;Persist Security Info=True;User ID=SearceDeveloperRRAW;Password=de2U#cefkW7"
        Dim RRAWDatabaseConnectionDEV As String = "Data Source=192.168.50.220;Initial Catalog=RAWDB_TEST;Persist Security Info=True;User ID=SearceDeveloperRRAW;Password=de2U#cefkW7"
    End Sub

    Friend Function CurrentReleaseInfo() As structReleaseInfo
        Dim dr As DataRow = DB.Blogs.GetLatestBlogEntry

        Return New structReleaseInfo(dr("Release"), dr("Title"), dr("FilePath"), dr("PostedOn"), dr("PostedBy"))

        'Return New structReleaseInfo("", "", "", "", "")
    End Function

    Friend Enum EnumCurrentAppVersionExtension
        LOCAL
        ALPHA
        BETA
        LIVE
    End Enum


    Friend Enum EnumStatus As Integer
        Approved = 1
        NewRateRequest
        InProcess
        Rejected
        EscalatedToNextLevel
    End Enum

    Friend Enum EnumHashMethod
        MD5
        SHA1
        SHA384
    End Enum

    Friend Enum EnumUserTypes
        StationUser = 1
        KeyAccountManager
        RegionalManager
        GlobalAccountManager
        Client
    End Enum

    Friend Enum EnumUserAction
        Login = 1
        Logout = 2
        NewRateRequest = 3
        ReplyRateRequest = 4
        SearchingRates = 5
        ExportingRates = 6
        ApplicationError = 7
        RemoveAllComments = 8
        RevokeRateRequest = 9
    End Enum

    Friend Function GetScreenResolution() As String
        Return HttpContext.Current.Session("ScreenRes").ToString
    End Function

    ''' <summary>
    ''' Returns the hash value for inputed data using the algorithm provided
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="algorithm"></param>
    ''' <remarks></remarks>
    Friend Function GenerateHash(ByVal source As String, ByVal algorithm As EnumHashMethod) As String
        Dim hashAlgorithm As HashAlgorithm = Nothing
        Select Case algorithm
            Case EnumHashMethod.MD5
                hashAlgorithm = New MD5CryptoServiceProvider
            Case EnumHashMethod.SHA1
                hashAlgorithm = New SHA1CryptoServiceProvider
            Case EnumHashMethod.SHA384
                hashAlgorithm = New SHA384Managed
        End Select

        Try
            Dim byteValue() As Byte = Encoding.UTF8.GetBytes(source)
            Dim hashValue() As Byte = hashAlgorithm.ComputeHash(byteValue)

            Dim sb As New StringBuilder()
            For i As Integer = 0 To hashValue.Length - 1
                sb.AppendFormat("{0:x2}", hashValue(i))
            Next

            Return sb.ToString
        Catch
            Throw
        End Try
    End Function

    Friend Function DBStrValue(ByVal fieldValue As String) As String
        Return If(fieldValue <> Nothing, fieldValue.Trim, "")
    End Function

    Friend Function GetCSVFromQuery(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal filterExpression As String, ByVal fieldSeparator As String, ByVal ParamArray params() As SqlClient.SqlParameter) As String
        Dim csvContent As New StringBuilder

        Try
            Using DB As New DB.DBClass(query, isStoredProcedure, params)
                Dim dt As DataTable = DB.GetDataTable
                For Each col As DataColumn In dt.Columns
                    csvContent.Append(col.ColumnName & fieldSeparator)
                Next
                csvContent.Remove(csvContent.Length - 1, 1)
                csvContent.AppendLine()
                If filterExpression <> "" Then
                    For Each row In dt.Select(filterExpression)
                        For i = 0 To dt.Columns.Count - 1
                            csvContent.Append("""" & row(i).ToString & """" & fieldSeparator)
                        Next
                        csvContent.Remove(csvContent.Length - 1, 1)
                        csvContent.AppendLine()
                    Next
                Else
                    For Each row In dt.Rows
                        For i = 0 To dt.Columns.Count - 1
                            csvContent.Append("""" & row(i).ToString & """" & fieldSeparator)
                        Next
                        csvContent.Remove(csvContent.Length - 1, 1)
                        csvContent.AppendLine()
                    Next
                End If
            End Using

            Return csvContent.ToString
        Catch
            Throw
        End Try
    End Function

    Friend Function CreateCSV(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal filePath As String, ByVal fileNamePrefix As String, ByVal attachTimeStampAsSuffix As Boolean, ByVal ParamArray params() As SqlClient.SqlParameter) As String
        Try
            Dim fileName As String = fileNamePrefix & If(attachTimeStampAsSuffix = True, "_" & Now.ToString("yyyyMMddHHmmss"), "") & ".csv"

            My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.GetParentPath(My.Request.PhysicalPath) & "\" & filePath & "\" & fileName, GetCSVFromQuery(query, isStoredProcedure, ",", "", params), False)

            Return filePath & "\" & fileName
        Catch
            Throw
        End Try
    End Function

    Friend Function CreateCSV(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal fileNamePrefix As String, ByVal attachTimeStampAsSuffix As Boolean, ByVal ParamArray params() As SqlClient.SqlParameter) As String
        Dim filePath As String = "CSVFiles"

        My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(My.Request.PhysicalPath) & "\" & filePath)

        Return CreateCSV(query, isStoredProcedure, filePath, fileNamePrefix, attachTimeStampAsSuffix, params)
    End Function

    Friend Function CreateCSV(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal fileNamePrefix As String, ByVal ParamArray params() As SqlClient.SqlParameter) As String
        Return CreateCSV(query, isStoredProcedure, fileNamePrefix, True, params)
    End Function

    'Friend Function CreateExcel(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal filePath As String, ByVal fileNamePrefix As String, ByVal attachTimeStampAsSuffix As String, ByVal ParamArray params() As SqlClient.SqlParameter) As String
    '    Dim exportContent As New StringBuilder(GetCSVFromQuery(query, isStoredProcedure, vbTab, params))

    '    Try

    '        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

    '        response.Clear()

    '        response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)
    '        response.ContentType = "application/ms-excel"

    '        Dim Encoding As New System.Text.UnicodeEncoding

    '        response.AddHeader("Content-Length", Encoding.GetByteCount(exportContent.ToString).ToString())
    '        response.BinaryWrite(Encoding.GetBytes(exportContent.ToString))
    '        response.Charset = "iso-8859-2"

    '        response.End()

    '        'My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.GetParentPath(My.Request.PhysicalPath) & "\" & filePath & "\" & fileName, csvContent.ToString, False)
    '        'Return filePath & "\" & fileName
    '    Catch
    '        Throw
    '    End Try
    'End Function

    'Friend Function CreateExcel(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal filePath As String, ByVal fileNamePrefix As String, ByVal attachTimeStampAsSuffix As String, ByVal ParamArray params() As SqlClient.SqlParameter) As String
    '    Dim xlApp As Object = Nothing
    '    Try
    '        Using DB As New DB.DBClass(query, isStoredProcedure, params)
    '            Dim dt As DataTable = DB.GetDataTable

    '            xlApp = CreateObject("Excel.Application")
    '            xlApp.workbooks.add()

    '            Dim oSheet = xlApp.worksheets("Sheet1")
    '            Dim colIndex As Integer = 0
    '            Dim rowIndex As Integer = 0
    '            For Each dc As DataColumn In dt.Columns
    '                colIndex = colIndex + 1
    '                oSheet.Cells(1, colIndex) = dc.ColumnName
    '                oSheet.Cells(1, colIndex).Interior.ColorIndex = 6
    '                oSheet.Cells(1, colIndex).Font.Bold = True
    '                oSheet.Cells(1, colIndex).Font.Size = 10
    '                'For as many columns as you have            
    '            Next
    '            For Each dr As DataRow In dt.Rows
    '                rowIndex = rowIndex + 1
    '                colIndex = 0
    '                For Each dc As DataColumn In dt.Columns
    '                    colIndex = colIndex + 1
    '                    oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
    '                Next
    '            Next
    '            rowIndex += 2
    '            oSheet.Columns.AutoFit()
    '            Dim blnFileOpen As Boolean = False

    '            Dim fileName As String = fileNamePrefix & If(attachTimeStampAsSuffix = True, "_" & Now.ToString("yyyyMMddHHmmss"), "") & ".xlsx"

    '            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(fileName)
    '            fileTemp.Close()

    '            blnFileOpen = False
    '            If System.IO.File.Exists(fileName) Then
    '                System.IO.File.Delete(fileName)
    '            End If
    '            oSheet.Name = "Sheet1"
    '            xlApp.Workbooks(1).SaveAs(My.Computer.FileSystem.GetParentPath(My.Request.PhysicalPath) & "\" & filePath & "\" & fileName)
    '            xlApp.Workbooks(1).Close()
    '            xlApp.Quit()
    '            oSheet = Nothing
    '            xlApp = Nothing

    '            GC.Collect()

    '            Return filePath & "\" & fileName
    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Friend Function CreateExcel(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal fileNamePrefix As String, ByVal attachTimeStampAsSuffix As Boolean, ByVal ParamArray params() As SqlClient.SqlParameter) As String
    '    Dim filePath As String = "ExcelFiles"

    '    My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetParentPath(My.Request.PhysicalPath) & "\" & filePath)

    '    Return CreateExcel(query, isStoredProcedure, filePath, fileNamePrefix, attachTimeStampAsSuffix, params)
    'End Function

    'Friend Function CreateExcel(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal fileNamePrefix As String, ByVal ParamArray params() As SqlClient.SqlParameter) As String
    '    Return CreateExcel(query, isStoredProcedure, fileNamePrefix, True, params)
    'End Function

    'Friend Function SendMail(ByVal subject As String, ByVal body As String) As Integer
    '    Return SendMail("dharmesh.mistry@searce.com", subject, body)
    'End Function

    Friend Function SendMail(ByVal subject As String, ByVal body As String) As Boolean
        Return SendMail("chetankumar.makadia@searce.com", subject, body)
    End Function

    'Friend Function SendMail(ByVal toAddress As String, ByVal subject As String, ByVal body As String) As Boolean
    '    Dim fromAddress As String = "rraw.searce@gmail.com"
    '    Dim fromPassword As String = "searcerrawsearce"

    '    'toAddress = "dharmesh.mistry@searce.com"

    '    Try
    '        Dim m As New System.Web.Mail.MailMessage()
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", fromAddress)
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", fromPassword)
    '        m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
    '        m.From = fromAddress
    '        m.To = toAddress
    '        m.Subject = subject
    '        m.Body = body
    '        System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465"

    '        If CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.LOCAL And CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.ALPHA And CurrentAppVersionExtension <> EnumCurrentAppVersionExtension.BETA Then
    '            System.Web.Mail.SmtpMail.Send(m)
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Friend Function SendMail(ByVal toAddress As String, ByVal subject As String, ByVal body As String) As Boolean
        If CurrentAppVersionExtension = EnumCurrentAppVersionExtension.LOCAL Then Return True

        Try
            Dim fromAddress As String = "rraw@searce.com"
            Dim fromPassword As String = "searcewarr"
            Dim m As New System.Net.Mail.MailMessage()

            Dim fromMail As New System.Net.Mail.MailAddress(fromAddress)

            If Not fromMail Is Nothing Then
                m.From = fromMail
                m.ReplyTo = fromMail
            End If
            If CurrentAppVersionExtension = EnumCurrentAppVersionExtension.ALPHA Or CurrentAppVersionExtension = EnumCurrentAppVersionExtension.BETA Then
                toAddress = "vimal.santoki@searce.com"
            End If
            m.To.Add(New System.Net.Mail.MailAddress(toAddress))
            m.IsBodyHtml = True
            m.BodyEncoding = System.Text.Encoding.UTF8
            m.Subject = subject
            m.Body = body
            m.Priority = System.Net.Mail.MailPriority.High
            m.Bcc.Add(New System.Net.Mail.MailAddress("rraw.searce@gmail.com"))
            Dim Client As New System.Net.Mail.SmtpClient()
            With Client
                .Host = "smtp.gmail.com"
                .Port = 587
                .EnableSsl = True
                .Credentials = New Net.NetworkCredential(fromAddress, fromPassword)
                Client.Send(m)
            End With
            Return True
        Catch ex As Exception
            DB.Logs.PostNewLog(EnumUserAction.ApplicationError, ex.Message)

            Return False
        End Try
    End Function

    'Friend Function SendMail() As Integer
    '    Dim m As New Web.Mail.MailMessage()
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "eglsxch17.egl.corp")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "rraw.searce@gmail.com")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "searcerrawsearce")
    '    m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
    '    m.From = "rraw.searce@gmail.com"
    '    m.To = "dharmesh.mistry@searce.com"
    '    m.Subject = "Testing"
    '    m.Body = "Hi Testing"
    '    Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465"
    '    Web.Mail.SmtpMail.Send(m)
    'End Function

    Friend Sub SendTestMailToAllUsers()
        Try
            Dim dt As New DataTable
            Using DB As New DB.DBClass("SELECT Users.Name, Users.UserID, Users.Email, Approvers.Name AS Approver FROM Users JOIN Users AS Approvers ON Users.ApproverID = Approvers.ID ORDER BY Users.ID")
                dt = DB.GetDataTable
            End Using

            For Each row In dt.Rows
                Dim body As String

                body = "This mail was sent to you by RRAW Portal Admin for acknowledgement purpose.<br /><br />"
                body += "Please confirm your identity in RRAW Poral as below.<br />"
                body += "Name: " & row("Name") & "<br />"
                body += "User ID: " & row("UserID") & "<br />"
                body += "Email Address: " & row("Email") & "<br />"
                body += "Approver: " & row("Approver") & "<br />"
                body += "If any of the above information is incorrect or missing, please contact RRAW Admin at rraw@searce.com"

                Try
                    SendMail(row("Email").ToString, "RRAW TEST MAIL", body)
                Catch
                    Throw
                End Try

                Exit For
            Next
        Catch
            Throw
        End Try
    End Sub

    Public ReadOnly Property CurrentUserID() As Integer
        Get
            Return CInt(HttpContext.Current.Session("UserID"))
        End Get
    End Property


    Public ReadOnly Property AccessibleModules() As String
        Get
            Return CInt(HttpContext.Current.Session("AccessibleModules"))
        End Get
    End Property

    

     
    Public ReadOnly Property CurrentClientID() As Integer
        Get
            Return CInt(HttpContext.Current.Session("ClientId"))
        End Get
    End Property

    Public ReadOnly Property CurrentClientName() As String
        Get
            Return CStr(HttpContext.Current.Session("ClientName"))
        End Get
    End Property

    Public ReadOnly Property CurrentUserName() As String
        Get
            Return HttpContext.Current.Session("UserName").ToString.Trim
        End Get
    End Property

    Public ReadOnly Property CurrentUserType() As String
        Get
            Return HttpContext.Current.Session("UserType").ToString.Trim
        End Get
    End Property

    Public ReadOnly Property CurrentApproversEmail() As String
        Get
            Return HttpContext.Current.Session("ApproversEmail").ToString.Trim
        End Get
    End Property

    Public Property CurrentSelectCommand() As String
        Get
            Return HttpContext.Current.Session("CurrentSelectCommand").ToString
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("CurrentSelectCommand") = value
        End Set
    End Property

    Public Property IsLocalCurrency() As Integer
        Get
            Return HttpContext.Current.Session("IsLocalCurrency")
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("IsLocalCurrency") = value
        End Set
    End Property

    Public Property IsAdhocLocalCurrency() As Integer
        Get
            Return HttpContext.Current.Session("IsAdhocLocalCurrency")
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("IsAdhocLocalCurrency") = value
        End Set
    End Property

    Public Property CurrentFilterExpression() As String
        Get
            Return HttpContext.Current.Session("CurrentFilterExpression").ToString
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("CurrentFilterExpression") = value
        End Set
    End Property

    Friend ReadOnly Property CurrentAppVersionExtension() As EnumCurrentAppVersionExtension
        Get
            Try
                Return HttpContext.Current.Session("AppVersionExtension")
            Catch ex As Exception
                Try
                    If HttpContext.Current.Request.Url.AbsoluteUri.ToLower.Contains("172.16") Or HttpContext.Current.Request.Url.AbsoluteUri.ToLower.Contains("local") Then
                        Return EnumCurrentAppVersionExtension.LOCAL
                    ElseIf HttpContext.Current.Request.Url.AbsoluteUri.ToLower.Contains("alpha") Then
                        Return EnumCurrentAppVersionExtension.ALPHA
                    ElseIf HttpContext.Current.Request.Url.AbsoluteUri.ToLower.Contains("beta") Then
                        Return EnumCurrentAppVersionExtension.BETA
                    Else
                        Return EnumCurrentAppVersionExtension.LIVE
                    End If
                Catch e As Exception
                End Try
            End Try
        End Get
    End Property

    Friend ReadOnly Property CurrentDBConnection() As String
        Get
            Try
                Select Case CurrentAppVersionExtension
                    Case EnumCurrentAppVersionExtension.LOCAL
                        databaseEntryName = "RRAWDatabaseConnectionLOCAL"
                        Return My.Settings.RRAWDatabaseConnectionLOCAL

                        'databaseEntryName = "RRAWDatabaseConnectionLIVE"
                        'Return My.Settings.RRAWDatabaseConnectionLIVE

                    Case EnumCurrentAppVersionExtension.ALPHA
                        databaseEntryName = "RRAWDatabaseConnectionTEST"
                        Return My.Settings.RRAWDatabaseConnectionTEST
                    Case EnumCurrentAppVersionExtension.BETA
                        databaseEntryName = "RRAWDatabaseConnectionTEST"
                        Return My.Settings.RRAWDatabaseConnectionTEST
                    Case Else
                        databaseEntryName = "RRAWDatabaseConnectionLIVE"
                        Return My.Settings.RRAWDatabaseConnectionLIVE
                End Select
            Catch ex As Exception
                Throw
            End Try
        End Get
    End Property

    Friend Function GetAccessibleModules(ByVal UserID As Integer, ByVal ClientId As Integer) As String
        Dim result As String
        result = ""
        For Each row As DataRow In DB.UserModulesMapping.GetAccesibleModulesOfUser(UserID, ClientId).Rows
            result += row("ControlID").ToString.Trim & ","
        Next
        HttpContext.Current.Session("AccessibleModules") = result
        Return result
    End Function


    Friend Function ConvertDataTableToListOfInt(ByVal dt As DataTable, ByVal columnName As String) As List(Of Integer)
        Dim result As New List(Of Integer)

        For Each row As DataRow In dt.Rows
            result.Add(row(columnName).ToString)
        Next

        Return result
    End Function

    Friend Function StringToDouble(ByVal value As String) As Double
        Return Val(value.Replace(",", ""))
    End Function

    Friend Property LiveLogIDs As String
        Get
            Return HttpContext.Current.Session("LogIDs").ToString.Trim(","c)
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("LogIDs") &= "," & value
        End Set
    End Property

    Friend Sub ClearSession()
        With HttpContext.Current
            .Session("UserID") = Nothing
            .Session("UserName") = Nothing
            .Session("UserType") = Nothing
            .Session("ApproversEmail") = Nothing
            .Session("AppVersionExtension") = 3
            .Session("ScreenRes") = Nothing
            .Session("CurrentSelectCommand") = Nothing
            .Session("LogIDs") = Nothing
        End With
    End Sub

    Friend Function DataTableToComplexDictionary(ByVal dt As DataTable) As Dictionary(Of String, List(Of String))
        Dim dict As New Dictionary(Of String, List(Of String))

        For Each row As DataRow In dt.Rows
            Dim lst As New List(Of String)
            For i As Integer = 1 To dt.Columns.Count - 1
                lst.Add(row(i).ToString)
            Next
            dict.Add(row(0).ToString, lst)
        Next

        Return dict
    End Function

    Friend Function DataTableToComplexDictionaryWithColumn(ByVal dt As DataTable) As Dictionary(Of String, Dictionary(Of String, String))
        Dim dict As New Dictionary(Of String, Dictionary(Of String, String))

        For Each row As DataRow In dt.Rows
            Dim innerDict As New Dictionary(Of String, String)
            For i As Integer = 1 To dt.Columns.Count - 1
                innerDict.Add(dt.Columns(i).ColumnName.Replace(" ", ""), row(i).ToString)
            Next
            Try
                dict.Add(row(0).ToString, innerDict)
            Catch ex As Exception
            End Try
        Next

        Return dict
    End Function

    Friend Function DataTableToListDictionary(ByVal dt As DataTable) As List(Of Dictionary(Of String, String))
        Dim dict As New List(Of Dictionary(Of String, String))



        For Each row As DataRow In dt.Rows
            Dim innerDict As New Dictionary(Of String, String)
            For i As Integer = 0 To dt.Columns.Count - 1
                innerDict.Add(dt.Columns(i).ColumnName.Replace(" ", ""), row(i).ToString)
            Next
            Try
                dict.Add(innerDict)
            Catch ex As Exception
            End Try
        Next

        Return dict
    End Function

    Friend Function DataTableColumns(ByVal dt As DataTable) As List(Of String)
        Dim lst As New List(Of String)

        For Each col As DataColumn In dt.Columns
            lst.Add(col.ColumnName)
        Next

        Return lst
    End Function

    Friend Function DataTableToDictionary(ByVal dt As DataTable) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)

        For Each row As DataRow In dt.Rows
            dict.Add(row(0).ToString, row(1).ToString)
        Next

        Return dict
    End Function

    Friend Function DataTableToDictionaryWithColumn(ByVal dt As DataTable) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)

        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Columns.Count - 1
                dict.Add(dt.Columns(i).ColumnName, dt.Rows(0)(i).ToString)
            Next
        End If

        Return dict
    End Function

    Friend Function DataTableToCSV(ByVal reportTitle As String, ByVal dt As DataTable) As String
        Dim csvContent As New StringBuilder

        Try
            If reportTitle <> "" Then
                csvContent.AppendLine(reportTitle)
            End If
            For Each col As DataColumn In dt.Columns
                csvContent.Append(col.ColumnName & ",")
            Next
            csvContent.Remove(csvContent.Length - 1, 1)
            csvContent.AppendLine()
            For Each dr As DataRow In dt.Rows
                For i = 0 To dt.Columns.Count - 1
                    csvContent.Append(dr(i).ToString.Replace("""", """""").Replace(",", " ").Trim & ",")
                Next
                csvContent.Remove(csvContent.Length - 1, 1)
                csvContent.AppendLine()
            Next

            Return csvContent.ToString
        Catch
            Throw
        End Try
    End Function

    Function IsAuthenticated(ByVal authenticationToken As String) As Boolean
        If DB.AuthenticationTokens.GetAuthenticationToken(authenticationToken) = authenticationToken Then
            Return True
        Else
            Return 0
        End If
    End Function

    Friend Function RefineInput(ByRef val As String) As String
        Return val.Trim()
    End Function

    Friend Function RefineInput(ByRef val As Integer) As Integer
        Return val
    End Function

    Friend Function RefineInput(ByRef val As Date) As Date
        Return val
    End Function

    Friend Function IsValid(ByVal val As String) As Boolean
        If RefineInput(val) <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Friend Function IsValid(ByVal val As Integer) As Boolean
        If RefineInput(val) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Friend Function IsValid(ByVal val As Date) As Boolean
        Return True
    End Function

    Friend Sub CacheDictionaryItems(ByVal cacheName As String, ByVal tableName As String, ByVal obj As Dictionary(Of String, String))
        Dim sqlDependency As New SqlCacheDependency(databaseEntryName, tableName)
        HttpContext.Current.Cache.Insert(cacheName, obj, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration)
    End Sub

    Friend Function GetCachedDictionaryItems(ByVal cacheItemName As String) As Dictionary(Of String, String)
        Return TryCast(HttpContext.Current.Cache(cacheItemName), Dictionary(Of String, String))
    End Function

    Friend Sub CacheDataTable(ByVal cacheName As String, ByVal tableName As String, ByVal obj As DataTable)
        Dim sqlDependency As New SqlCacheDependency(databaseEntryName, tableName)
        HttpContext.Current.Cache.Insert(cacheName, obj, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration)
    End Sub

    Friend Function GetCachedDataTable(ByVal cacheItemName As String) As DataTable
        Return TryCast(HttpContext.Current.Cache(cacheItemName), DataTable)
    End Function
    Friend Function DataTableToComplexDictionaryWithAllColumn(ByVal dt As DataTable) As Dictionary(Of String, Dictionary(Of String, String))
        Dim dict As New Dictionary(Of String, Dictionary(Of String, String))

        For Each row As DataRow In dt.Rows
            Dim innerDict As New Dictionary(Of String, String)
            For i As Integer = 0 To dt.Columns.Count - 1
                innerDict.Add(dt.Columns(i).ColumnName.Replace(" ", ""), row(i).ToString)
            Next
            Try
                dict.Add(row(0).ToString, innerDict)
            Catch ex As Exception
            End Try
        Next

        Return dict
    End Function
End Module
