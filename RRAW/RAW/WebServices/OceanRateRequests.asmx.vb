Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class OceanRateRequests
        Inherits System.Web.Services.WebService

        'Web Service scope variable declaration
        Shared errCode As Integer = -1
        Shared successCode As Integer = 1
#Region "Masters"
        <WebMethod()>
        Public Function GetAllMasters(ByVal AuthenticationToken As String) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then
                res.Add("Error", "AuthenticationFailed.aspx")

                Return res
            End If

            res.Add("GetAllOceanShipMethods", GetAllShipMethods)
            res.Add("GetAllRegions", GetAllRegions)
            res.Add("GetAllRatesValidFor", GetAllRatesValidFor)
            res.Add("GetAllRateBasedOn", GetAllRateBasedOn)

            Return res
        End Function

        Public Function GetAllRegions() As Dictionary(Of String, String)
            'Dim cacheItemName As String = "RegionsList"
            'Dim tableName As String = "Regions"

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            'If res Is Nothing Then
            res = DataTableToDictionary(DB.Masters.GetAllRegions())

            'CacheDictionaryItems(cacheItemName, tableName, res)
            'End If

            Return res
        End Function

        Public Function GetAllRatesValidFor() As Dictionary(Of String, String)
            'Dim cacheItemName As String = "OceanRatesValidForList"
            'Dim tableName As String = "OceanRateValidFor"

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            'If res Is Nothing Then
            res = DataTableToDictionary(DB.Masters.GetAllOceanRatesValidFor())

            'CacheDictionaryItems(cacheItemName, tableName, res)
            'End If

            Return res
        End Function

        Public Function GetAllRateBasedOn() As Dictionary(Of String, String)
            'Dim cacheItemName As String = "RatesBasedOnList"
            'Dim tableName As String = "OceanRateBaseOn"

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            'If res Is Nothing Then
            res = DataTableToDictionary(DB.Masters.GetAllRateBasedOn())

            'CacheDictionaryItems(cacheItemName, tableName, res)
            'End If

            Return res
        End Function

        Public Function GetAllShipMethods() As Dictionary(Of String, String)
            'Dim cacheItemName As String = "OceanShipMethodsList"
            'Dim tableName As String = "OceanShipMethods"

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            'If res Is Nothing Then
            res = DataTableToDictionary(DB.Masters.GetAllOceanShipMethods())

            'CacheDictionaryItems(cacheItemName, tableName, res)
            'End If

            Return res
        End Function
#End Region

#Region "Ocean Rate Request Web Methods"
        <WebMethod()> _
        Public Function PostNewRateRequest(ByVal AuthenticationToken As String, ByVal RequestorID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String, ByVal Rates() As Object, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            Dim NewRateRequestID As Integer = DB.RateRequestMaster.PostNewRateRequestID("O"c)
            If NewRateRequestID = errCode Or NewRateRequestID = 0 Then Return errCode

            If PostNewRateRequest(NewRateRequestID, RequestorID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment) = errCode Then Return errCode

            If PostNewRates(NewRateRequestID, Rates) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(RequestorID)
            Dim SuperiorUserID As Integer
            If SuperiorUser IsNot Nothing Then
                SuperiorUserID = CInt(SuperiorUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SuperiorUserID)
            If AddRateRequestHolders(NewRateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If PostNewComment(NewRateRequestID, RequestorID, Comment) = errCode Then Return errCode

            If AddAttachments(NewRateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(NewRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, RequestorID) = errCode Then Return errCode

            If SendApprovalMail(SuperiorUser, RequestorID, NewRateRequestID) = errCode Then Return errCode

            Return NewRateRequestID
        End Function

        <WebMethod()> _
        Public Function SaveNewRateRequest(ByVal AuthenticationToken As String, ByVal RequestorID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String, ByVal Rates() As Object, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            Dim NewRateRequestID As Integer = DB.RateRequestMaster.PostNewRateRequestID("O"c)
            If NewRateRequestID = errCode Or NewRateRequestID = 0 Then Return errCode

            If PostNewRateRequest(NewRateRequestID, RequestorID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment) = errCode Then Return errCode

            If PostNewRates(NewRateRequestID, Rates) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim CurrentUser As DataRow = DB.Users.GetDetails(RequestorID)
            NewRateRequestHolders.Add(RequestorID)
            If AddRateRequestHolders(NewRateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If PostNewComment(NewRateRequestID, RequestorID, Comment) = errCode Then Return errCode

            If AddAttachments(NewRateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(NewRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, RequestorID) = errCode Then Return errCode

            Return NewRateRequestID
        End Function

        <WebMethod()> _
        Public Function SaveExistingRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String, ByVal NewRates() As Object, ByVal UpdatedRates() As Object, ByVal RemovedRates As Object, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If UpdateRateRequest(RateRequestID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment) = errCode Then Return errCode

            If PostNewRates(RateRequestID, NewRates) = errCode Then Return errCode

            If UpdateRates(UpdatedRates) = errCode Then Return errCode

            If RemoveRates(RemovedRates) = errCode Then Return errCode

            If PostNewComment(RateRequestID, UserID, Comment) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, UserID) = errCode Then Return errCode

            Return RateRequestID
        End Function

        <WebMethod()> _
        Public Function ApproveRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String, ByVal NewRates() As Object, ByVal UpdatedRates() As Object, ByVal RemovedRates As Object, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If UpdateRateRequest(RateRequestID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment) = errCode Then Return errCode

            If PostNewRates(RateRequestID, NewRates) = errCode Then Return errCode

            If UpdateRates(UpdatedRates) = errCode Then Return errCode

            If RemoveRates(RemovedRates) = errCode Then Return errCode

            If PostNewComment(RateRequestID, ApproverID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(ApproverID)
            Dim SuperiorUserID As Integer
            If SuperiorUser IsNot Nothing Then
                SuperiorUserID = CInt(SuperiorUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SuperiorUserID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

            If SendApprovalMail(SuperiorUser, ApproverID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function ApproveRateRequestWithoutChange(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If PostNewComment(RateRequestID, ApproverID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(ApproverID)
            Dim SuperiorUserID As Integer
            If SuperiorUser IsNot Nothing Then
                SuperiorUserID = CInt(SuperiorUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SuperiorUserID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

            If SendApprovalMail(SuperiorUser, ApproverID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function ApproveRateRequestByClient(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal Comment As String, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If ApproveRateRequestByClient(RateRequestID, ApproverID) = errCode Then Return errCode

            If PostNewComment(RateRequestID, ApproverID, Comment) = errCode Then Return errCode

            If RemoveRateRequestHolders(RateRequestID) = errCode Then Return errCode

            If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

            If SendMailToRelatedUsers(RateRequestID, ApproverID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function SendRateRequestBackToRevise(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String, ByVal NewRates() As Object, ByVal UpdatedRates() As Object, ByVal RemovedRates As Object, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If UpdateRateRequest(RateRequestID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment) = errCode Then Return errCode

            If PostNewRates(RateRequestID, NewRates) = errCode Then Return errCode

            If UpdateRates(UpdatedRates) = errCode Then Return errCode

            If RemoveRates(RemovedRates) = errCode Then Return errCode

            If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SubordinateUser As DataRow = DB.Users.GetSubordinateUser(RateRequestID, RejectorID)
            Dim SubordinateUserID As Integer
            If SubordinateUser IsNot Nothing Then
                SubordinateUserID = CInt(SubordinateUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SubordinateUserID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, RejectorID) = errCode Then Return errCode

            If SendRevisionMail(SubordinateUser, RejectorID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function SendRateRequestBackToReviseWithoutChange(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SubordinateUser As DataRow = DB.Users.GetSubordinateUser(RateRequestID, RejectorID)
            Dim SubordinateUserID As Integer
            If SubordinateUser IsNot Nothing Then
                SubordinateUserID = CInt(SubordinateUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SubordinateUserID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, RejectorID) = errCode Then Return errCode

            If SendRevisionMail(SubordinateUser, RejectorID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function RejectRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal Comment As String, ByVal Attachments() As Object) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If RejectRateRequest(RateRequestID, RejectorID) = errCode Then Return errCode

            If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

            Dim RelatedUsers As DataTable = DB.Users.GetRelatedUsers(RateRequestID, RejectorID)
            If RelatedUsers.Rows.Count <= 0 Then Return errCode
            Dim NewRateRequestHolders As New List(Of Integer)
            For Each RelatedUserRow As DataRow In RelatedUsers.Rows
                NewRateRequestHolders.Add(CInt(RelatedUserRow("ID")))
            Next
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, RejectorID) = errCode Then Return errCode

            If SendRejectionMail(RelatedUsers, RejectorID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()>
        Public Function PostNewComment(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            PostNewComment(RateRequestID, UserID, Comment)

            'Confirm that whom to send mail (either to all for a rate request or just to the next approver)

            Return successCode
        End Function

        <WebMethod()> _
        Public Function RevokeRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RevokerID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If RevokeRateRequest(RateRequestID, RevokerID) = errCode Then Return errCode

            If PostNewComment(RateRequestID, RevokerID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            NewRateRequestHolders.Add(RevokerID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            Dim RelatedUsers = DB.Users.GetRelatedUsers(RateRequestID, RevokerID)
            Dim Requestor = DB.Users.GetDetails(CInt(DB.OceanRateRequests.GetRateRequest(RateRequestID).Rows(0)("RequestorID")))
            'RelatedUsers.Rows.Add(Requestor)
            If RelatedUsers.Rows.Count <= 0 And Requestor Is Nothing Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revoked, RevokerID) = errCode Then Return errCode

            If SendRevokeMail(RelatedUsers, RevokerID, RateRequestID) = errCode Then Return errCode
            If SendRevokeMail(Requestor.Table, RevokerID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function ArchiveRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ArchiverID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If ArchiveRateRequest(RateRequestID, ArchiverID) = errCode Then Return errCode

            If PostNewComment(RateRequestID, ArchiverID, Comment) = errCode Then Return errCode

            If RemoveRateRequestHolders(RateRequestID) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Archived, ArchiverID) = errCode Then Return errCode

            Dim RelatedUsers As DataTable = DB.Users.GetRelatedUsers(RateRequestID, ArchiverID)

            If SendArchiveMail(RelatedUsers, ArchiverID, RateRequestID) = errCode Then Return errCode

            Dim Requestor As DataRow = DB.Users.GetDetails(CInt(DB.OceanRateRequests.GetRateRequest(RateRequestID).Rows(0)("RequestorID")))
            If Requestor Is Nothing Then Return errCode

            If SendArchiveMail(Requestor.Table, ArchiverID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function RemoveAllComments(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal CommentsRemoverID As Integer, ByVal Comment As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            If RemoveAllComments(RateRequestID) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.CommentsRemoved, CommentsRemoverID) = errCode Then Return errCode

            Dim RelatedUsers As DataTable = DB.Users.GetRelatedUsers(RateRequestID, CommentsRemoverID)
            If SendCommentRemovalMail(RelatedUsers, CommentsRemoverID, RateRequestID) = errCode Then Return errCode

            Dim Requestor As DataRow = DB.Users.GetDetails(CInt(DB.OceanRateRequests.GetRateRequest(RateRequestID).Rows(0)("RequestorID")))
            If Requestor Is Nothing Then Return errCode
            If SendCommentRemovalMail(Requestor.Table, CommentsRemoverID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        <WebMethod()> _
        Public Function GetRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then
                res.Add("Error", "AuthenticationFailed.aspx")

                Return res
            End If

            'Make sequence of function calls
            Dim RateRequest = DB.OceanRateRequests.GetRateRequest(RateRequestID)
            Dim RateRequestDictionary = DataTableToDictionaryWithColumn(RateRequest)
            If RateRequestDictionary.Count > 0 Then
                res.Add("RateRequest", RateRequestDictionary)
            Else
                res.Add("RateRequest", "Error")
            End If

            Dim Rates = DataTableToComplexDictionary(DB.OceanRates.GetRates(RateRequestID))
            res.Add("Rates", Rates)

            Dim CurrentRateRequestHolders = DB.RateRequestHolders.GetRateRequestHolders(RateRequestID)
            If CurrentRateRequestHolders.Count > 0 Then
                res.Add("CurrentRateRequestHolders", CurrentRateRequestHolders)
            Else
                res.Add("CurrentRateRequestHolders", "Error")
            End If

            Dim CurrentRateRequestGenerator = DB.Users.GetDetails(CInt(RateRequest.Rows(0)("RequestorID"))).Table
            If CurrentRateRequestGenerator.Rows.Count > 0 Then
                res.Add("CurrentRateRequestGenerator", DataTableToDictionaryWithColumn(CurrentRateRequestGenerator))
            Else
                res.Add("CurrentRateRequestGenerator", "Error")
            End If

            If IsNumeric(RateRequest.Rows(0)("ApproverID")) Then
                Dim CurrentRateRequestApprover = DB.Users.GetDetails(CInt(RateRequest.Rows(0)("ApproverID"))).Table
                If CurrentRateRequestApprover.Rows.Count > 0 Then
                    res.Add("CurrentRateRequestApprover", DataTableToDictionaryWithColumn(CurrentRateRequestApprover))
                Else
                    res.Add("CurrentRateRequestApprover", "Error")
                End If
            End If

            Dim Attachments = DB.Attachments.GetAttachmentsByReferenceID(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID)
            Attachments.Columns.Remove("Title")
            res.Add("Attachments", DataTableToComplexDictionary(Attachments))

            Dim DBServerDate = DB.Utilities.GetCurrentDate()
            If IsDate(DBServerDate) Then
                res.Add("DBServerDate", DBServerDate.ToString)
            Else
                res.Add("DBServerDate", "Error")
            End If

            Return res
        End Function

        <WebMethod()> _
        Public Function GetPreviousComments(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            Dim PreviousComments = DataTableToComplexDictionaryWithColumn(DB.Comments.GetRateRequestComments(RateRequestID))
            If PreviousComments.Count > 0 Then
                res.Add("PreviousComments", PreviousComments)
            Else
                res.Add("PreviousComments", "")
            End If

            Return res
        End Function

        <WebMethod()> _
        Public Function SendRateRequestBackToOriginator(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RateRequestType As String) As Integer
            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then Return errCode

            'Make sequence of function calls
            'If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

            Dim NewRateRequestHolders As New List(Of Integer)
            Dim SubordinateUser As DataRow = DB.Users.GetRateRequestor(RateRequestID, RateRequestType)
            Dim SubordinateUserID As Integer
            If SubordinateUser IsNot Nothing Then
                SubordinateUserID = CInt(SubordinateUser("ID"))
            Else
                Return errCode
            End If
            NewRateRequestHolders.Add(SubordinateUserID)
            If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

            If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, RejectorID) = errCode Then Return errCode

            If SendRevisionMail(SubordinateUser, RejectorID, RateRequestID) = errCode Then Return errCode

            Return successCode
        End Function

        '<WebMethod()> _
        'Public Function GetRateRequestWithMasters(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        '    Dim res As New Dictionary(Of String, Object)
        '    res.Add("Masters", (New WebServices.Masters).GetAllOceanMasters(AuthenticationToken))
        '    res.Add("RateRequest", GetRateRequest(AuthenticationToken, RateRequestID))

        '    Return res
        'End Function
#End Region

#Region "Ocean Rate Request Background Operations"
        Private Function PostNewComment(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(UserID) = False Then Return errCode

            If Comment.Trim() <> "" Then
                'Call BLL function if valid
                Return DB.OceanRateRequests.PostNewComment(RateRequestID, UserID, Comment)
            Else
                Return 0
            End If
        End Function

        Private Function RemoveAllComments(ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.RemoveAllComments(RateRequestID)
        End Function

        Private Function PostNewRateRequest(ByVal RateRequestID As Integer, ByVal RequestorID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String,CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(RequestorID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.PostNewRateRequest(RateRequestID, RequestorID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment)
        End Function

        Private Function ApproveRateRequestByClient(ByVal RateRequestID As Integer, ByVal ApproverID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(ApproverID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.ApproveRateRequestByClient(RateRequestID, ApproverID)
        End Function

        Private Function RejectRateRequest(ByVal RateRequestID As Integer, ByVal RejectorID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(RejectorID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.RejectRateRequest(RateRequestID, RejectorID)
        End Function

        Private Function RevokeRateRequest(ByVal RateRequestID As Integer, ByVal RevokerID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(RevokerID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.RevokeRateRequest(RateRequestID, RevokerID)
        End Function

        Private Function ArchiveRateRequest(ByVal RateRequestID As Integer, ByVal ArchiverID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(ArchiverID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.ArchiveRateRequest(RateRequestID, ArchiverID)
        End Function

        Private Function UpdateRateRequest(ByVal RateRequestID As Integer, ByVal ContainerNo As String, ByVal OceanHBL As String, ByVal ShipDate As String, ByVal FreightTerm As String, ByVal WDShipMethod As String, ByVal CarrierName As String, ByVal ShipperName As String, ByVal OriginCity As String, ByVal OriginPort As String, ByVal OriginZipcode As String, ByVal OriginRegion As String, ByVal ConsigneeName As String, ByVal DestCity As String, ByVal DestPort As String, ByVal DestZipcode As String, ByVal DestRegion As String, ByVal RatesValidFor As String, ByVal RatesValidTill As String, ByVal Comment As String) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            'If IsValid(ContainerNo) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.UpdateRateRequest(RateRequestID, ContainerNo, OceanHBL, ShipDate, FreightTerm, WDShipMethod, CarrierName, ShipperName, OriginCity, OriginPort, OriginZipcode, OriginRegion, ConsigneeName, DestCity, DestPort, DestZipcode, DestRegion, RatesValidFor, RatesValidTill, Comment)
        End Function

        Private Function PostNewRates(ByVal RateRequestID As Integer, ByVal ParamRates() As Object) As Integer
            'Refine and validate input values
            Dim Rates As Array = CType(ParamRates, Array)

            'Return if nothing to process
            If Rates.Length = 0 Then Return 0

            For Each RateRow As Array In Rates
                'Formulate input values
                If RateRow IsNot Nothing Then
                    Dim RateTitle As String = RateRow.GetValue(0).ToString
                    Dim BasedOn As String = RateRow.GetValue(1).ToString
                    Dim PerCBM As String = RateRow.GetValue(2).ToString
                    Dim Per20GP As String = RateRow.GetValue(3).ToString
                    Dim Per40GP As String = RateRow.GetValue(4).ToString
                    Dim Per40HC As String = RateRow.GetValue(5).ToString

                    'Call BLL function if valid
                    If DB.OceanRates.PostNewRates(RateRequestID, RateTitle, BasedOn, PerCBM, Per20GP, Per40GP, Per40HC) = errCode Then Return errCode
                End If
            Next

            Return successCode
        End Function

        Private Function UpdateRates(ByVal ParamRates() As Object) As Integer
            'Refine and validate input values
            Dim Rates As Array = CType(ParamRates, Array)

            'Return if nothing to process
            If Rates.Length = 0 Then Return 0

            For Each RateRow As Array In Rates
                'Formulate input values
                Dim RateID As Integer = CInt(RateRow.GetValue(0))
                Dim RateTitle As String = RateRow.GetValue(1).ToString
                Dim BasedOn As String = RateRow.GetValue(2).ToString
                Dim PerCBM As String = RateRow.GetValue(3).ToString
                Dim Per20GP As String = RateRow.GetValue(4).ToString
                Dim Per40GP As String = RateRow.GetValue(5).ToString
                Dim Per40HC As String = RateRow.GetValue(6).ToString

                'Call BLL function if valid
                If DB.OceanRates.UpdateRates(RateID, RateTitle, BasedOn, PerCBM, Per20GP, Per40GP, Per40HC) = errCode Then Return errCode
            Next

            Return successCode
        End Function

        Private Function RemoveRates(ByVal ParamRateIDs As Object) As Integer
            'Refine and validate input values
            Dim RateIDs As Array = CType(ParamRateIDs, Array)

            'Return if nothing to process
            If RateIDs.Length = 0 Then Return 0

            For Each RateID As Integer In RateIDs
                'Call BLL function if valid
                If DB.OceanRates.RemoveRates(RateID) = errCode Then Return errCode
            Next

            Return successCode
        End Function

        Private Function AddRateRequestHolders(ByVal RateRequestID As Integer, ByVal NewRateRequestHolders As List(Of Integer)) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If NewRateRequestHolders.Count > 0 Then
                For Each NewRateRequestHolder As Integer In NewRateRequestHolders
                    If IsValid(NewRateRequestHolder) = False Then Return errCode
                Next
            Else
                Return errCode
            End If

            'Call BLL function if valid
            Return DB.RateRequestHolders.AddRateRequestHolders(RateRequestID, NewRateRequestHolders)
        End Function

        Private Function UpdateRateRequestHolders(ByVal RateRequestID As Integer, ByVal NewRateRequestHolders As List(Of Integer)) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If NewRateRequestHolders.Count > 0 Then
                For Each NewRateRequestHolder As Integer In NewRateRequestHolders
                    If IsValid(NewRateRequestHolder) = False Then Return errCode
                Next
            Else
                Return errCode
            End If

            'Call BLL function if valid
            Return DB.RateRequestHolders.UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders)
        End Function

        Private Function RemoveRateRequestHolders(ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.RateRequestHolders.RemoveRateRequestHolders(RateRequestID)
        End Function

        Private Function ClearRejectedTag(ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.OceanRateRequests.ClearRejectedTag(RateRequestID)
        End Function

        Private Function SendApprovalMail(ByVal Receiver As DataRow, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If Receiver Is Nothing Then Return errCode
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            'Call BLL function if valid
            If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
        End Function

        Private Function SendRevisionMail(ByVal Receiver As DataRow, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If Receiver Is Nothing Then Return errCode
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been sent back to revise by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
        End Function

        Private Function SendRejectionMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If Receivers.Rows.Count <= 0 Then Return errCode
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            For Each Receiver As DataRow In Receivers.Rows
                If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been rejected by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
            Next
        End Function

        Private Function SendRevokeMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If Receivers.Rows.Count <= 0 Then Return errCode
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            For Each Receiver As DataRow In Receivers.Rows
                If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been pulled back by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
            Next
        End Function

        Private Function SendArchiveMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            For Each Receiver As DataRow In Receivers.Rows
                If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been archived by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
            Next
        End Function

        Private Function SendCommentRemovalMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
            'Refine and validate input values
            If Receivers.Rows.Count <= 0 Then Return errCode
            If IsValid(SenderID) = False Then Return errCode
            If IsValid(RateRequestID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

            For Each Receiver As DataRow In Receivers.Rows
                If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", All comments has been removed from a Rate request by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
            Next
        End Function

        Private Function SendMailToRelatedUsers(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(CurrentUserID) = False Then Return errCode

            Dim Sender As DataRow = DB.Users.GetDetails(CurrentUserID)

            'Call BLL functions if valid
            'Send mail to all related users except request generator
            For Each dr1 As DataRow In DB.Users.GetRelatedUsers(RateRequestID, CurrentUserID).Rows
                If SendMail(dr1("Email").ToString, "Hi " & dr1("Name").ToString & ", A rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & "." & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.") = False Then Return errCode
            Next

            'Send mail to request generator
            Dim dr2 As DataRow = DB.Users.GetDetails(CInt(DB.OceanRateRequests.GetRateRequest(RateRequestID).Rows(0)("RequestorID")))
            If SendMail(dr2("Email").ToString, "Hi " & dr2("Name").ToString & ", Your rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=OceanRateRequest.aspx?RateRequestID=" & RateRequestID & "." & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.") = False Then Return errCode

            Return successCode
        End Function

        Private Function AddAttachments(ByVal RateRequestID As Integer, ByVal Attachments() As Object) As Integer
            For Each Attachment As Array In Attachments
                Dim FileName As String = CStr(Attachment.GetValue(0))
                Dim FilePath As String = CStr(Attachment.GetValue(1))

                If DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID, FileName, FilePath) = errCode Then Return errCode
            Next

            Return successCode
        End Function

        Private Function PostRateRequestLog(ByVal RateRequestID As Integer, ByVal RateRequestOperation As DB.RateRequestLog.RateRequestOperations, ByVal UserID As Integer) As Integer
            'Refine and validate input values
            If IsValid(RateRequestID) = False Then Return errCode
            If IsValid(UserID) = False Then Return errCode

            'Call BLL function if valid
            Return DB.RateRequestLog.PostRateRequestLog(RateRequestID, RateRequestOperation, Now, UserID)
        End Function

        'Private Function PostNewLog(ByVal UserID As Integer, ByVal UserAction As EnumUserAction) As Integer
        '    'Refine and validate input values
        '    If IsValid(UserAction) = False Then Return errCode

        '    'Call BLL function if valid
        '    DB.Logs.PostNewLog(UserID, UserAction)
        'End Function
#End Region
    End Class
End Namespace