Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AirRateRequests
    Inherits System.Web.Services.WebService
    'Web Service scope variable declaration
    Shared errCode As Integer = -1
    Shared successCode As Integer = 1

#Region "Masters"
    ''' <summary>  'this method is used to Get master data.</summary>  
    '''<author>Nitu</author>
    '''<CreatedDate>13.04.2012</CreatedDate>

    <WebMethod()>
    Public Function GetAllMasters(ByVal AuthenticationToken As String) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If

        res.Add("GetAllShipMethods", GetAllShipMethods)
        res.Add("GetAllEuropianZone", GetAllEuropianZone)

        Dim AirTariffRates = DB.AirRateRequests.GetAirTariffRates()
        Dim AirTariffRatesDictionary = DataTableToComplexDictionaryWithColumn(AirTariffRates)
        If AirTariffRatesDictionary.Count > 0 Then
            res.Add("AirTariffRates", AirTariffRatesDictionary)
        Else
            res.Add("AirTariffRates", "Error")
        End If

        Return res
    End Function

    <WebMethod()>
    Public Function CanUserViewBreakDownCharges(ByVal AuthenticationToken As String, ByVal UserID As String) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If

        res.Add("CanUserViewBreakDownCharges", DB.AirRateRequests.BreakDownChargesStatus(UserID))
        Return res
    End Function

    Public Function GetAllEuropianZone() As Dictionary(Of String, String)

        Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
        res = DataTableToDictionary(DB.Masters.GetAllEuropianZone())

        Return res
    End Function

    Public Function GetAllShipMethods() As Dictionary(Of String, String)

        Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
        res = DataTableToDictionary(DB.ShipMethods.GetAllShipMethods_15july())

        Return res
    End Function
#End Region

#Region "Air Rate Request Web Methods"

    <WebMethod()> _
    Public Function PostNewRateRequest(ByVal AuthenticationToken As String, ByVal RequestorID As Integer, ByVal RateRequestDate As DateTime, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date,
                                           ByVal ServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String,
                                           ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String,
                                           ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                            ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String,
                                            ByVal Currency As String,
                                            ByVal Attachments() As Object,
                                            ByVal Rates() As Object,
                                            ByVal NewAddOnCharges() As Object, ByVal updatedAddOnRates() As Object, ByVal removedAddOnRates() As Object, ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode
        'Make sequence of function calls

        Dim NewRateRequestID As Integer = PostNewRateRequest_15July(RequestorID, RateRequestDate, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport, OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity,
                                                     DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate, OtherCharges,
                                                      OriginComment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees)

        If NewRateRequestID = errCode Or NewRateRequestID = 0 Then Return errCode

        If PostNewRates(NewRateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(NewRateRequestID, NewAddOnCharges) = errCode Then Return errCode


        If UpdateAddOnRates(NewRateRequestID, updatedAddOnRates) = errCode Then Return errCode


        If DeleteAddOnRates(removedAddOnRates) = errCode Then Return errCode

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

        If PostNewComment(NewRateRequestID, RequestorID, OriginComment) = errCode Then Return errCode

        If AddAttachments(NewRateRequestID, Attachments) = errCode Then Return errCode

        If PostRateRequestLog(NewRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, RequestorID) = errCode Then Return errCode

        If SendCreateMail(SuperiorUser, RequestorID, NewRateRequestID) = errCode Then Return errCode

        Return NewRateRequestID
    End Function

    <WebMethod()> _
    Public Function SaveNewRateRequest(ByVal AuthenticationToken As String, ByVal RequestorID As Integer, ByVal RateRequestDate As DateTime, ByVal HAWBNumber As String, ByVal ShipMethod As String,
                                           ByVal ShipDate As Date, ByVal ServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String,
                                           ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String,
                                           ByVal DestAirport As String, ByVal DestCity As String, ByVal DestRegion As String,
                                           ByVal DestZipcode As String,
                                           ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                            ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String,
                                            ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                            ByVal Attachments() As Object, ByVal Rates() As Object, ByVal NewAddOnCharges() As Object,
                                            ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        'Make sequence of function calls

        Dim NewRateRequestID As Integer = PostNewRateRequest_15July(RequestorID, RateRequestDate, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport, OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity,
                                                     DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate, OtherCharges,
                                                      OriginComment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees)
        If NewRateRequestID = errCode Or NewRateRequestID = 0 Then Return errCode



        If PostNewRates(NewRateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(NewRateRequestID, NewAddOnCharges) = errCode Then Return errCode

        Dim NewRateRequestHolders As New List(Of Integer)
        Dim CurrentUser As DataRow = DB.Users.GetDetails(RequestorID)
        NewRateRequestHolders.Add(RequestorID)
        If AddRateRequestHolders(NewRateRequestID, NewRateRequestHolders) = errCode Then Return errCode


        If PostNewComment(NewRateRequestID, RequestorID, OriginComment) = errCode Then Return errCode
        If AddAttachments(NewRateRequestID, Attachments) = errCode Then Return errCode

        If PostRateRequestLog(NewRateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, RequestorID) = errCode Then Return errCode

        Return NewRateRequestID
    End Function

    Private Function PostNewRates(ByVal RateRequestID As Integer, ByVal ParamRates() As Object) As Integer
        'Refine and validate input values
        Dim Rates As Array = CType(ParamRates, Array)

        'Return if nothing to process
        If Rates.Length = 0 Then Return 0

        For Each RateRow As Array In Rates
            'Formulate input values
            If RateRow IsNot Nothing Then
                Dim RateId As Integer = Convert.ToInt32(RateRow.GetValue(0))
                Dim MinValue As Double = CDbl(IIf(RateRow.GetValue(1).ToString = "", 0, RateRow.GetValue(1)))
                Dim PerKG As Double = CDbl(IIf(RateRow.GetValue(2).ToString = "", 0, RateRow.GetValue(2)))
                'Call BLL function if valid
                If DB.AirRateRequests.PostNewAirTariffRates(RateRequestID, RateId, MinValue, PerKG) = errCode Then Return errCode
            End If
        Next

        Return successCode
    End Function

    <WebMethod()> _
    Public Function SaveExistingRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer,
                                                ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                                ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String,
                                                ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String,
                                                ByVal DestAirport As String, ByVal DestCity As String,
                                                ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                                ByVal FreightRate As Double, ByVal OtherCharges As String,
                                                ByVal Comment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                                ByVal Attachments() As Object, ByVal Rates() As Object,
                                                ByVal NewAddOnCharges() As Object, ByVal updatedAddOnRates() As Object, ByVal removedAddOnRates() As Object,
                                                ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        'Make sequence of function calls
        If UpdateRateRequest(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport,
                             OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity, DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate,
                             OtherCharges, Comment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees) = errCode Then Return errCode

        If PostNewRates(RateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(RateRequestID, NewAddOnCharges) = errCode Then Return errCode
        If UpdateAddOnRates(RateRequestID, updatedAddOnRates) = errCode Then Return errCode
        If DeleteAddOnRates(removedAddOnRates) = errCode Then Return errCode

        If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, UserID) = errCode Then Return errCode

        If SendMailToRelatedUsers(RateRequestID, UserID) = errCode Then Return errCode

        If PostNewComment(RateRequestID, UserID, Comment) = errCode Then Return errCode

        Return RateRequestID
    End Function

    <WebMethod()> _
    Public Function ApproveRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal UpdateDate As DateTime,
                                           ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                           ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String,
                                           ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String,
                                           ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                           ByVal FreightRate As Double, ByVal OtherCharges As String,
                                           ByVal Comment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                           ByVal Attachments() As Object,
                                           ByVal Rates() As Object,
                                           ByVal NewAddOnCharges() As Object, ByVal updatedAddOnRates() As Object, ByVal removedAddOnRates() As Object,
                                           ByVal TransportMode As String, ByVal GrossAddOnCharges As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        If PostNewAirRateRequestHistory(RateRequestID, UserID, UpdateDate, MinFreightRate, FreightRate, OtherCharges) = errCode Then Return errCode

        'Make sequence of function calls
        If UpdateRateRequest(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport,
                             OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity, DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate,
                             OtherCharges, Comment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnCharges) = errCode Then Return errCode

        If PostNewRates(RateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(RateRequestID, NewAddOnCharges) = errCode Then Return errCode


        If UpdateAddOnRates(RateRequestID, updatedAddOnRates) = errCode Then Return errCode


        If DeleteAddOnRates(removedAddOnRates) = errCode Then Return errCode


        If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

        If PostNewComment(RateRequestID, UserID, Comment) = errCode Then Return errCode

        'operation will be change with generated or approved accoarding to check 
        'weather rate request is new or existing
        If DB.AirRateRequests.IsRateRequestGenerated(RateRequestID) = 0 Then
            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, UserID) = errCode Then Return errCode
        Else
            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, UserID) = errCode Then Return errCode
        End If
        Dim NewRateRequestHolders As New List(Of Integer)
        Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(UserID)
        Dim SuperiorUserID As Integer
        If SuperiorUser IsNot Nothing Then
            SuperiorUserID = CInt(SuperiorUser("ID"))
        Else
            Return errCode
        End If
        NewRateRequestHolders.Add(SuperiorUserID)
        If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

        If ClearRejectedTag(RateRequestID) = errCode Then Return errCode

        If SendApprovalMail(SuperiorUser, UserID, RateRequestID) = errCode Then Return errCode

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


        If DB.AirRateRequests.IsRateRequestGenerated(RateRequestID) = 0 Then
            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Generated, ApproverID) = errCode Then Return errCode
        Else
            If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode
        End If

        If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

        ClearRejectedTag(RateRequestID)

        If SendApprovalMail(SuperiorUser, ApproverID, RateRequestID) = errCode Then Return errCode

        Return successCode
    End Function

    <WebMethod()> _
    Public Function ApproveRateRequestByClient(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ApproverID As Integer,
                                                   ByVal Comment As String, ByVal Attachments() As Object, ByVal ApprovalDate As Date, ByVal isAdhoc As Boolean) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        'Make sequence of function calls
        If ApproveRateRequestByClient(RateRequestID, ApproverID, ApprovalDate, Comment, isAdhoc) = errCode Then Return errCode

        'Dim NewRateRequestHolders As New List(Of Integer)
        'Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(ApproverID)
        'Dim SuperiorUserID As Integer
        'If SuperiorUser IsNot Nothing Then
        '    SuperiorUserID = CInt(SuperiorUser("ID"))
        'Else
        '    Return errCode
        'End If
        'NewRateRequestHolders.Add(SuperiorUserID)
        'If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

        If ClearRejectedTag(RateRequestID) = errCode Then Return errCode


        If PostNewComment(RateRequestID, ApproverID, Comment) = errCode Then Return errCode

        If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

        If SendApproveMailToRelatedUsers(RateRequestID, ApproverID) = errCode Then Return errCode

        Return successCode
    End Function

    <WebMethod()> _
    Public Function ApproveRateRequestPermanent(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal ApprovalDate As DateTime,
                                           ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                           ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String,
                                           ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String,
                                           ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                           ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String,
                                           ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                           ByVal Attachments() As Object,
                                           ByVal Rates() As Object,
                                           ByVal NewAddOnCharges() As Object, ByVal updatedAddOnRates() As Object, ByVal removedAddOnRates() As Object,
                                           ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        If PostNewAirRateRequestHistory(RateRequestID, ApproverID, ApprovalDate, MinFreightRate, FreightRate, OtherCharges) = errCode Then Return errCode

        'Make sequence of function calls
        If UpdateRateRequest(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport,
                             OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity, DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate, OtherCharges, OriginComment,
                             OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees) = errCode Then Return errCode

        If PostNewRates(RateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(RateRequestID, NewAddOnCharges) = errCode Then Return errCode


        If UpdateAddOnRates(RateRequestID, updatedAddOnRates) = errCode Then Return errCode


        If DeleteAddOnRates(removedAddOnRates) = errCode Then Return errCode


        'Make sequence of function calls
        If ApproveRateRequestByClient(RateRequestID, ApproverID, ApprovalDate, OriginComment, False) = errCode Then Return errCode

        Dim NewRateRequestHolders As New List(Of Integer)
        Dim SuperiorUser As DataRow = DB.Users.GetSuperiorUser(ApproverID)
        Dim SuperiorUserID As Integer
        If SuperiorUser IsNot Nothing Then
            SuperiorUserID = CInt(SuperiorUser("ID"))
        Else
            Return errCode
        End If

        If ClearRejectedTag(RateRequestID) = errCode Then Return errCode


        If PostNewComment(RateRequestID, ApproverID, OriginComment) = errCode Then Return errCode

        If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

        'If SendApproveMailToRelatedUsers(RateRequestID, ApproverID) = errCode Then Return errCode

        Return successCode
    End Function

    <WebMethod()> _
    Public Function SendRateRequestBackToRevise(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal UpdatedDate As DateTime,
                                           ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                           ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String,
                                           ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String,
                                           ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                           ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal Comment As String,
                                           ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                           ByVal Attachments() As Object,
                                           ByVal Rates() As Object,
                                           ByVal NewAddOnCharges() As Object, ByVal updatedAddOnRates() As Object, ByVal removedAddOnRates() As Object,
                                           ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode
        If PostNewAirRateRequestHistory(RateRequestID, UserID, UpdatedDate, MinFreightRate, FreightRate, OtherCharges) = errCode Then Return errCode
        'Make sequence of function calls
        If UpdateRateRequest(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport,
                             OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity, DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate, OtherCharges, Comment,
                             OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees) = errCode Then Return errCode

        If PostNewRates(RateRequestID, Rates) = errCode Then Return errCode

        If PostNewAddOnRates(RateRequestID, NewAddOnCharges) = errCode Then Return errCode


        If UpdateAddOnRates(RateRequestID, updatedAddOnRates) = errCode Then Return errCode


        If DeleteAddOnRates(removedAddOnRates) = errCode Then Return errCode

        If PostNewComment(RateRequestID, UserID, Comment) = errCode Then Return errCode

        Dim NewRateRequestHolders As New List(Of Integer)
        Dim SubordinateUser As DataRow = DB.Users.GetSubordinateUser(RateRequestID, UserID)
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

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, UserID) = errCode Then Return errCode

        If SendRevisionMail(SubordinateUser, UserID, RateRequestID) = errCode Then Return errCode



        Return successCode
    End Function

    <WebMethod()> _
    Public Function SendRateRequestBackToReviseWithoutChange(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal Comment As String, ByVal UpdatedDate As Date) As Integer
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
    Public Function RejectRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RejectedDate As DateTime, ByVal MinFreightRate As Double,
                                                      ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal Comment As String, ByVal Attachments() As Object) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        If PostNewAirRateRequestHistory(RateRequestID, RejectorID, RejectedDate, MinFreightRate, FreightRate, OtherCharges) = errCode Then Return errCode
        'Make sequence of function calls

        Dim RelatedUsers As DataTable = DB.Users.GetRelatedUsers(RateRequestID, RejectorID)
        If RelatedUsers.Rows.Count <= 0 Then Return errCode
        Dim NewRateRequestHolders As New List(Of Integer)
        For Each RelatedUserRow As DataRow In RelatedUsers.Rows
            NewRateRequestHolders.Add(CInt(RelatedUserRow("ID")))
        Next
        If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

        If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Rejected, RejectorID) = errCode Then Return errCode

        If RejectRateRequest(RateRequestID, RejectorID, RejectedDate) = errCode Then Return errCode

        If AddAttachments(RateRequestID, Attachments) = errCode Then Return errCode

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
        If PostNewComment(RateRequestID, RevokerID, Comment) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revoked, RevokerID) = errCode Then Return errCode
        'Make sequence of function calls
        If RevokeRateRequest(RateRequestID, RevokerID, Comment) = errCode Then Return errCode

        Dim NewRateRequestHolders As New List(Of Integer)
        NewRateRequestHolders.Add(RevokerID)

        Dim RelatedUsers = DB.Users.GetRelatedUsers(RateRequestID, RevokerID)
        Dim Requestor = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID_15july(RateRequestID)("RequestorID")))
        'RelatedUsers.Rows.Add(Requestor)
        If RelatedUsers.Rows.Count <= 0 And Requestor Is Nothing Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revoked, RevokerID) = errCode Then Return errCode

        If SendRevokeMail(RelatedUsers, RevokerID, RateRequestID) = errCode Then Return errCode
        If SendRevokeMail(Requestor.Table, RevokerID, RateRequestID) = errCode Then Return errCode

        Return successCode
    End Function

    <WebMethod()> _
    Public Function RemoveAllComments(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal CommentsRemoverID As Integer, ByVal Comment As String, ByVal RemovedDate As Date) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        'Make sequence of function calls
        If RemoveAllComments(RateRequestID) = errCode Then Return errCode
        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.CommentsRemoved, CommentsRemoverID) = errCode Then Return errCode
        Return successCode
    End Function
    <WebMethod()> _
    Public Function GetSimilarRateRequests(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If
        Dim RateRequest = DB.AirRateRequests.GetRateRequestByIDTable_15july(RateRequestID)
        Dim SimilarRateRequests = DB.AirRateRequests.GetSimilarRateRequests_15july(RateRequestID, CStr(RateRequest.Rows(0)("Origin Airport")),
                                                                            CStr(RateRequest.Rows(0)("Destination Airport")),
                                                                         CStr(RateRequest.Rows(0)("Destination City")),
                                                                        CStr(RateRequest.Rows(0)("Ship Method")))
        If SimilarRateRequests.Rows.Count > 0 Then
            res.Add("SimilarRateRequests", DataTableToComplexDictionaryWithAllColumn(SimilarRateRequests))
        Else
            res.Add("SimilarRateRequests", "Error")
        End If
        Return res
    End Function

    <WebMethod()> _
    Public Function GetSimilarTariffLanes(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If
        Dim RateRequest = DB.AirRateRequests.GetRateRequestByIDTable_15july(RateRequestID)
        Dim SimilarTariffLanes = DB.Tariffs.GetSimilarTariffLanes_15july(RateRequestID, CStr(RateRequest.Rows(0)("Origin Airport")),
                                                                           CStr(RateRequest.Rows(0)("Destination Airport")),
                                                                    CStr(RateRequest.Rows(0)("Ship Method")))
        If SimilarTariffLanes.Rows.Count > 0 Then
            res.Add("SimilarTariffLanes", DataTableToComplexDictionaryWithAllColumn(SimilarTariffLanes))
        Else
            res.Add("SimilarTariffLanes", "Error")
        End If
        Return res
    End Function


    <WebMethod()> _
    Public Function GetSimilarAdhocLanes(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If
        Dim RateRequest = DB.AirRateRequests.GetRateRequestByIDTable_15july(RateRequestID)
        Dim SimilarAdhocLanes = DB.AirRateRequests.GetSimilarAdhocLanes_15july(RateRequestID, CStr(RateRequest.Rows(0)("Origin Airport")),
                                                                           CStr(RateRequest.Rows(0)("Destination Airport")),
                                                                        CStr(RateRequest.Rows(0)("Destination City")),
                                                                            CStr(RateRequest.Rows(0)("Ship Method")))
        If SimilarAdhocLanes.Rows.Count > 0 Then
            res.Add("SimilarAdhocLanes", DataTableToComplexDictionaryWithAllColumn(SimilarAdhocLanes))
        Else
            res.Add("SimilarAdhocLanes", "Error")
        End If
        Return res
    End Function
    <WebMethod()> _
    Public Function GetRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            res.Add("Error", "AuthenticationFailed.aspx")

            Return res
        End If

        'Make sequence of function calls
        Dim RateRequest = DB.AirRateRequests.GetRateRequestByIDTable_15july(RateRequestID)
        Dim RateRequestDictionary = DataTableToDictionaryWithColumn(RateRequest)
        If RateRequestDictionary.Count > 0 Then
            res.Add("RateRequest", RateRequestDictionary)
        Else
            res.Add("RateRequest", "Error")
        End If

        Dim AirTariffRates = DB.AirRateRequests.GetAirTariffRates(RateRequestID)
        Dim AirTariffRatesDictionary = DataTableToComplexDictionaryWithColumn(AirTariffRates)
        If AirTariffRatesDictionary.Count > 0 Then
            res.Add("AirTariffRates", AirTariffRatesDictionary)
        Else
            res.Add("AirTariffRates", "Error")
        End If

        Dim AirAddOnCharges = DB.AirRateRequests.GetAirAddOnCharges(RateRequestID)
        Dim AirAddOnChargesDictionary = DataTableToComplexDictionaryWithColumn(AirAddOnCharges)
        If AirAddOnChargesDictionary.Count > 0 Then
            res.Add("AirAddOnCharges", AirAddOnChargesDictionary)
        Else
            res.Add("AirAddOnCharges", "Error")
        End If

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

        If IsDBNull(RateRequest.Rows(0)("Approver")) Then
            'Dim CurrentRateRequestApprover = DB.Users.GetDetails(CInt(RateRequest.Rows(0)("Approver"))).Table
            'If CurrentRateRequestApprover.Rows.Count > 0 Then
            res.Add("CurrentRateRequestApprover", "Error")
            ' 
        Else
            res.Add("CurrentRateRequestApprover", RateRequest.Rows(0)("Approver"))
        End If
        'End If

        Dim Attachments = DB.Attachments.GetAttachmentsByReferenceID(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID)
        Attachments.Columns.Remove("Title")
        res.Add("Attachments", DataTableToComplexDictionary(Attachments))

        Dim RateRequestLogs = DB.RateRequestLog.GetRateRequestLogsByRequestID(RateRequestID)
        If RateRequestLogs.Rows.Count > 0 Then
            res.Add("RateRequestLogs", DataTableToDictionaryWithColumn(RateRequestLogs))
        Else
            res.Add("RateRequestLogs", "Error")
        End If


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
    Public Function GetRateRequestHistory(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)

        Dim RateRequestHistory = DataTableToComplexDictionaryWithColumn(DB.AirRateRequests.GetRateRequestHistory(RateRequestID))
        If RateRequestHistory.Count > 0 Then
            res.Add("RateRequestHistory", RateRequestHistory)
        Else
            res.Add("RateRequestHistory", "")
        End If

        Return res
    End Function



    <WebMethod()> _
    Public Function SendRateRequestBackToOriginator(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RateRequestType As String,
                                                        ByVal Comment As String, ByVal ReviseDate As Date) As Integer
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

        If PostNewComment(RateRequestID, RejectorID, Comment) = errCode Then Return errCode

        'If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revised, RejectorID) = errCode Then Return errCode
        If RemoveRateRequestLogs(RateRequestID) = errCode Then Return errCode

        If SendRevisionMail(SubordinateUser, RejectorID, RateRequestID) = errCode Then Return errCode

        Return successCode
    End Function

    <WebMethod()> _
    Public Function DeleteRateRequest(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer, ByVal Comment As String) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode


        If SendDeleteMailToRelatedUsers(RateRequestID, CurrentUserID) = errCode Then Return errCode

        If PostNewComment(RateRequestID, CurrentUserID, Comment) = errCode Then Return errCode

        If RemoveRateRequestLogs(RateRequestID) = errCode Then Return errCode

        If RemoveRateRequestHistory(RateRequestID) = errCode Then Return errCode

        If RemoveRateRequestHolders(RateRequestID) = errCode Then Return errCode


        If RemoveAllComments(RateRequestID) = errCode Then Return errCode

        If RemoveRateRequest(RateRequestID) = errCode Then Return errCode

        If RemoveAttachmentsByReferenceID(RateRequestID) = errCode Then Return errCode
        'Make sequence of function calls

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Revoked, CurrentUserID) = errCode Then Return errCode
        Return successCode
    End Function

    <WebMethod()> _
    Public Function RateRequestTransferToTariff(ByVal AuthenticationToken As String, ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal UpdateDate As DateTime,
                                           ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                           ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String,
                                           ByVal OriginZipcode As String, ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String,
                                           ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String, ByVal MinFreightRate As Double,
                                           ByVal FreightRate As Double, ByVal OtherCharges As String,
                                           ByVal Comment As String,
                                            ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                            ByVal Attachments() As Object, ByVal TransferDate As DateTime, ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then Return errCode

        If PostNewAirRateRequestHistory(RateRequestID, UserID, UpdateDate, MinFreightRate, FreightRate, OtherCharges) = errCode Then Return errCode

        If UpdateRateRequest(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport,
                                     OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity, DestRegion, DestZipcode,
                                     RateDeterMethod, MinFreightRate, FreightRate, OtherCharges, Comment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees) = errCode Then Return errCode


        If PostNewComment(RateRequestID, UserID, Comment) = errCode Then Return errCode

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, UserID) = errCode Then Return errCode

        If RemoveRateRequestHolders(RateRequestID) = errCode Then Return errCode 'Make sequence of function calls

        If RateRequestToTariff(RateRequestID, UpdateDate, TransferDate, UserID) = errCode Then Return errCode

        Return successCode
    End Function

#End Region

#Region "Air Rate Request Background Operations"

    Private Function PostNewRateRequest_15July(ByVal RequestorID As Integer, ByVal RateRequestDate As DateTime, ByVal HAWBNumber As String, ByVal ShipMethod As String, ByVal ShipDate As Date, ByVal ServiceLevel As String, ByVal Weight As Double, ByVal ShipperName As String,
                                               ByVal OriginAirport As String, ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String, ByVal Consignee As String,
                                               ByVal DestAirport As String, ByVal DestCity As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String,
                                               ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal OtherCharges As String, ByVal OriginComment As String, ByVal OriginEuropeanZones As String,
                                               ByVal DestinitionEuropeanZones As String, ByVal Currency As String, ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Refine and validate input values
        If IsValid(RequestorID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.PostNewRateRequest_15July(RequestorID, RateRequestDate, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport, OriginCity, OriginRegion, OriginZipcode, Consignee, DestAirport, DestCity,
                                                     DestRegion, DestZipcode, RateDeterMethod, MinFreightRate, FreightRate, OtherCharges,
                                                      OriginComment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees)
    End Function

    Private Function RemoveAllComments(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.RemoveAllComments_15july(RateRequestID)
    End Function

    Private Function AddRateRequestHolders(ByVal RateRequestID As Integer, ByVal CurrentRateRequestHolders As List(Of Integer)) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If CurrentRateRequestHolders.Count > 0 Then
            For Each NewRateRequestHolder As Integer In CurrentRateRequestHolders
                If IsValid(NewRateRequestHolder) = False Then Return errCode
            Next
        Else
            Return errCode
        End If

        'Call BLL function if valid
        Return DB.RateRequestHolders.AddRateRequestHolders(RateRequestID, CurrentRateRequestHolders)
    End Function

    Private Function PostNewComment(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(UserID) = False Then Return errCode

        If Comment.Trim() <> "" Then
            'Call BLL function if valid
            Return DB.Comments.PostNewAirComment_15july(RateRequestID, UserID, Comment)
        Else
            Return 0
        End If
    End Function


    Private Function PostRateRequestLog(ByVal RateRequestID As Integer, ByVal RateRequestOperation As DB.RateRequestLog.RateRequestOperations, ByVal UserID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(UserID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.RateRequestLog.PostRateRequestLog(RateRequestID, RateRequestOperation, Now, UserID)
    End Function

    Private Function AddAttachments(ByVal RateRequestID As Integer, ByVal Attachments() As Object) As Integer
        For Each Attachment As Array In Attachments
            Dim FileName As String = CStr(Attachment.GetValue(0))
            Dim FilePath As String = CStr(Attachment.GetValue(1))

            If DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID, FileName, FilePath) = errCode Then Return errCode
        Next

        Return successCode
    End Function

    Private Function SendApprovalMail(ByVal Receiver As DataRow, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If Receiver Is Nothing Then Return errCode
        If IsValid(SenderID) = False Then Return errCode
        If IsValid(RateRequestID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

        'Call BLL function if valid
        If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
    End Function


    Private Function SendCreateMail(ByVal Receiver As DataRow, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If Receiver Is Nothing Then Return errCode
        If IsValid(SenderID) = False Then Return errCode
        If IsValid(RateRequestID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

        'Call BLL function if valid
        If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A New Rate request has been generated by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
    End Function


    Private Function UpdateRateRequest(ByVal RateRequestID As Integer, ByVal HAWBNumber As String, ByVal ShipMethod As String,
                                       ByVal ShipDate As Date, ByVal ServiceLevel As String,
                                       ByVal Weight As Double, ByVal ShipperName As String, ByVal OriginAirport As String,
                                       ByVal OriginCity As String, ByVal OriginRegion As String, ByVal OriginZipcode As String,
                                       ByVal Consignee As String, ByVal DestAirport As String, ByVal DestCity As String, ByVal DestRegion As String, ByVal DestZipcode As String, ByVal RateDeterMethod As String,
                                       ByVal MinFreightRate As Double, ByVal FreightRate As Double, ByVal OtherCharges As String,
                                       ByVal OriginComment As String, ByVal OriginEuropeanZones As String, ByVal DestinitionEuropeanZones As String, ByVal Currency As String,
                                       ByVal TransportMode As String, ByVal GrossAddOnFees As Double) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        'If IsValid(ContainerNo) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.UpdateRateRequest_15july(RateRequestID, HAWBNumber, ShipMethod, ShipDate, ServiceLevel, Weight, ShipperName, OriginAirport, OriginCity, OriginRegion, OriginZipcode,
                                                    Consignee, DestAirport, DestCity, DestRegion, DestZipcode,
                                                    RateDeterMethod, MinFreightRate, FreightRate, OtherCharges, OriginComment, OriginEuropeanZones, DestinitionEuropeanZones, Currency, TransportMode, GrossAddOnFees)
    End Function


    Private Function SendMailToRelatedUsers(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(CurrentUserID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(CurrentUserID)

        'Call BLL functions if valid
        'Send mail to all related users except request generator
        For Each dr1 As DataRow In DB.Users.GetRelatedUsers(RateRequestID, CurrentUserID).Rows
            SendMail(dr1("Email").ToString, "Hi " & dr1("Name").ToString & ", A Rate request has been revised by " & Sender("Name").ToString(), "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID)
        Next

        'Send mail to request generator
        Dim dr2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID_15july(RateRequestID)("RequestorID")))
        SendMail(dr2("Email").ToString, "Hi " & dr2("Name").ToString & ", A Rate request has been revised by " & Sender("Name").ToString(), "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID)

        Return successCode
    End Function

    Private Function PostNewAirRateRequestHistory(ByVal RateRequestID As Integer, ByVal UpdatorID As Integer, ByVal UpdateDate As DateTime, ByVal MinFreightRate As Double,
                                                  ByVal FreightRate As Double, ByVal OtherCharges As String) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(UpdatorID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequestHistory.PostNewAirRateRequestHistory_15july(RateRequestID, UpdatorID, UpdateDate, MinFreightRate, FreightRate, OtherCharges)
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

    Private Function ClearRejectedTag(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.ClearRejectedTag_15july(RateRequestID)
    End Function


    Private Function ApproveRateRequestByClient(ByVal RateRequestID As Integer, ByVal ApproverID As Integer, ByVal ApprovalDate As Date, ByVal comment As String, ByVal isAdhoc As Boolean) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(ApproverID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.ApproveRateRequestByClient_15july(RateRequestID, ApproverID, ApprovalDate, comment, isAdhoc)
    End Function

    Private Function SendApproveMailToRelatedUsers(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(CurrentUserID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(CurrentUserID)

        'Call BLL functions if valid
        'Send mail to all related users except request generator
        For Each dr1 As DataRow In DB.Users.GetRelatedUsers(RateRequestID, CurrentUserID).Rows
            If SendMail(dr1("Email").ToString, "Hi " & dr1("Name").ToString & ", A rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & "." & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.") = False Then Return errCode
        Next

        'Send mail to request generator
        Dim dr2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID_15july(RateRequestID)("RequestorID")))
        If SendMail(dr2("Email").ToString, "Hi " & dr2("Name").ToString & ", Your rate request has been approved by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & "." & vbNewLine & "Rate Request will get transferred to Tariff within 24 hours.") = False Then Return errCode

        Return successCode
    End Function

    Private Function SendRevisionMail(ByVal Receiver As DataRow, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If Receiver Is Nothing Then Return errCode
        If IsValid(SenderID) = False Then Return errCode
        If IsValid(RateRequestID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

        If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been sent back to revise by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
    End Function

    Private Function RejectRateRequest(ByVal RateRequestID As Integer, ByVal RejectorID As Integer, ByVal RejectedDate As Date) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(RejectorID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.RejectRateRequestByClient_15july(RateRequestID, RejectorID, RejectedDate)
    End Function


    Private Function SendRejectionMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If Receivers.Rows.Count <= 0 Then Return errCode
        If IsValid(SenderID) = False Then Return errCode
        If IsValid(RateRequestID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

        For Each Receiver As DataRow In Receivers.Rows
            If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been rejected by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
        Next
    End Function

    Private Function RevokeRateRequest(ByVal RateRequestID As Integer, ByVal RevokerID As Integer, ByVal comment As String) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(RevokerID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.RevokeRateRequest_15july(RateRequestID, RevokerID, comment)
    End Function

    Private Function SendRevokeMail(ByVal Receivers As DataTable, ByVal SenderID As Integer, ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If Receivers.Rows.Count <= 0 Then Return errCode
        If IsValid(SenderID) = False Then Return errCode
        If IsValid(RateRequestID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(SenderID)

        For Each Receiver As DataRow In Receivers.Rows
            If SendMail(Receiver("Email").ToString, "Hi " & Receiver("Name").ToString & ", A Rate request has been pulled back by " & Sender("Name").ToString, "Find rate request details at http://rraw.invoize.com/Master.aspx?ChildPage=NewRateRequest.aspx?RateRequestID=" & RateRequestID & ".") = False Then Return errCode
        Next
    End Function

    Private Function RemoveRateRequestLogs(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.RateRequestLog.RemoveRateRequestLogs(RateRequestID)
    End Function

    Private Function RemoveRateRequestHistory(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequestHistory.RemoveRateRequestHistory(RateRequestID)
    End Function

    Private Function RemoveRateRequestHolders(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.RateRequestHolders.RemoveRateRequestHolders(RateRequestID)
    End Function


    Private Function RemoveRateRequest(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.RemoveRateRequest_15july(RateRequestID)
    End Function

    Private Function RemoveAttachmentsByReferenceID(ByVal RateRequestID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.Attachments.RemoveAttachmentsByReferenceID(RateRequestID)
    End Function

    Private Function SendDeleteMailToRelatedUsers(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        If IsValid(CurrentUserID) = False Then Return errCode

        Dim Sender As DataRow = DB.Users.GetDetails(CurrentUserID)

        'Call BLL functions if valid
        'Send mail to all related users except request generator
        For Each dr1 As DataRow In DB.Users.GetRelatedUsers(RateRequestID, CurrentUserID).Rows
            If SendMail(dr1("Email").ToString, "Hi " & dr1("Name").ToString & ", A Rate request has been deleted by " & Sender("Name").ToString) = False Then Return errCode

        Next

        'Send mail to request generator
        Dim dr2 As DataRow = DB.Users.GetDetails(CInt(DB.AirRateRequests.GetRateRequestByID_15july(RateRequestID)("RequestorID")))
        If SendMail(dr2("Email").ToString, "Hi " & dr2("Name").ToString & ", A Rate request has been deleted by " & Sender("Name").ToString) = False Then Return errCode

        Return successCode
    End Function

    Private Function RateRequestToTariff(ByVal RateRequestID As Integer, ByVal ApprovalDate As DateTime, ByVal TransferDate As DateTime, ByVal UserID As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode

        'Call BLL function if valid
        Return DB.Tariffs.RateRequestToTariff_15july(RateRequestID, ApprovalDate, TransferDate, UserID)
    End Function

    Private Function PostNewAddOnRates(ByVal RateRequestID As Integer, ByVal ParamRates() As Object) As Integer
        'Refine and validate input values
        Dim Rates As Array = CType(ParamRates, Array)

        'Return if nothing to process
        If Rates.Length = 0 Then Return 0

        For Each RateRow As Array In Rates
            'Formulate input values
            If RateRow IsNot Nothing Then
                Dim Description As String = Convert.ToString(RateRow.GetValue(0))
                Dim UOM As String = Convert.ToString(IIf(RateRow.GetValue(1).ToString = "", 0, RateRow.GetValue(1)))
                Dim Value As Double = CDbl(IIf(RateRow.GetValue(2).ToString = "", 0, RateRow.GetValue(2)))
                'Call BLL function if valid
                If DB.AirRateRequests.PostNewAddOnRates(RateRequestID, Description, UOM, Value) = errCode Then Return errCode
            End If
        Next

        Return successCode
    End Function

    Private Function UpdateAddOnRates(ByVal RateRequestID As Integer, ByVal ParamRates() As Object) As Integer
        'Refine and validate input values
        Dim Rates As Array = CType(ParamRates, Array)

        'Return if nothing to process
        If Rates.Length = 0 Then Return 0

        For Each RateRow As Array In Rates
            'Formulate input values
            If RateRow IsNot Nothing Then
                Dim ID As Integer = Convert.ToInt32(IIf(RateRow.GetValue(0).ToString = "", 0, RateRow.GetValue(0)))
                Dim Description As String = Convert.ToString(RateRow.GetValue(1))
                Dim UOM As String = Convert.ToString(IIf(RateRow.GetValue(2).ToString = "", 0, RateRow.GetValue(2)))
                Dim Value As Double = CDbl(IIf(RateRow.GetValue(3).ToString = "", 0, RateRow.GetValue(3)))
                'Call BLL function if valid
                If DB.AirRateRequests.UpdateAddOnRates(ID, RateRequestID, Description, UOM, Value) = errCode Then Return errCode
            End If
        Next

        Return successCode
    End Function

    Private Function DeleteAddOnRates(ByVal ParamRates() As Object) As Integer
        'Refine and validate input values
        Dim Rates As Array = CType(ParamRates, Array)


        'Return if nothing to process
        If Rates.Length = 0 Then Return 0

        For Each Rate As Integer In Rates
            'Formulate input values
            Dim ID As Integer = Rate
            'Call BLL function if valid
            If DB.AirRateRequests.DeleteAddOnRates(ID) = errCode Then Return errCode
        Next

        Return successCode
    End Function

    <WebMethod()> _
    Public Function ApproveRateRequestBySystem(ByVal RateRequestID As Integer, ByVal SuperiorUserID As Integer, ByVal ApproverID As Integer, ByVal Comment As String, ByVal StatusId As Integer) As Integer

        'Make sequence of function calls

        UpdateRateRequestTariff(RateRequestID, ApproverID, StatusId)

        If PostNewComment(RateRequestID, ApproverID, Comment) = errCode Then Return errCode

        Dim NewRateRequestHolders As New List(Of Integer)

        NewRateRequestHolders.Add(SuperiorUserID)

        If PostRateRequestLog(RateRequestID, DB.RateRequestLog.RateRequestOperations.Approved, ApproverID) = errCode Then Return errCode

        If UpdateRateRequestHolders(RateRequestID, NewRateRequestHolders) = errCode Then Return errCode

        ClearRejectedTag(RateRequestID)

        Return successCode
    End Function

    Private Function UpdateRateRequestTariff(ByVal RateRequestID As Integer, ByVal ApproverId As Integer, ByVal StatusId As Integer) As Integer
        'Refine and validate input values
        If IsValid(RateRequestID) = False Then Return errCode
        'If IsValid(ContainerNo) = False Then Return errCode

        'Call BLL function if valid
        Return DB.AirRateRequests.UpdateRateRequestTariff(RateRequestID, ApproverId, StatusId)
    End Function




#End Region


    <WebMethod()> _
    Public Function GetTariffRateRequestHistory(ByVal RateRequestId As Integer) As String
        Dim strHTML As New StringBuilder
        'Check for call authentication        
        Dim RateRequest = DB.AirRateRequests.GetTariffRateRequestHistory(RateRequestId)

        If (RateRequest.Rows.Count > 0) Then
            strHTML.Append("<table cellpadding='5' cellspacing='0' class='groupContainer' border='0' style='border-collapse:collapse;'><tr>")
            For Each DC As DataColumn In RateRequest.Columns
                If (DC.ColumnName <> "TransportModeId") Then
                    strHTML.Append("<th style='text-align: left'>" + DC.ColumnName + "</th>")
                End If
            Next
            strHTML.Append("</tr>")
            For Each Dr As DataRow In RateRequest.Rows
                For Each DC As DataColumn In RateRequest.Columns
                    If (DC.ColumnName <> "TransportModeId") Then
                        strHTML.Append("<td style='text-align: left'>" + Dr(DC.ColumnName).ToString() + "</td>")
                    End If


                Next
                strHTML.Append("</tr>")
            Next
            strHTML.Append("</table")

        Else
            strHTML.Append("<table cellpadding='5' cellspacing='0' class='groupContainer' border='0' style='border-collapse:collapse;'><tr>")
            strHTML.Append("<th style='text-align: left'>No History Available.</th>")
            strHTML.Append("</tr>")
            strHTML.Append("</table")

        End If
        Return strHTML.ToString()
    End Function

    <WebMethod()> _
    Public Function GetTariffGlobally(ByVal AuthenticationToken As String, ByVal SearchStr As String) As String

        'Check for call authentication
        If Not IsAuthenticated(AuthenticationToken) Then
            Return "Error"
        End If

        Dim strHTML As New StringBuilder
        'Check for call authentication        
        Dim Tariff = DB.Tariffs_New.GetTariffGlobally(SearchStr)

        If (Tariff.Rows.Count > 0) Then
            strHTML.Append("<table class='tb' cellpadding='5' cellspacing='0' class='groupContainer' border='0' style='width:100%;height:100%; border-collapse:collapse;'><tr style='color: White; border-color: Gray; border-width: 1px; border-style: solid; font-weight: bold; background-color: rgb(51, 102, 153);'>")
            For Each DC As DataColumn In Tariff.Columns
                If (DC.ColumnName <> "addrRateRequestID" And DC.ColumnName <> "TariffRateRequestID" And DC.ColumnName <> "ViewRateRequestID" And DC.ColumnName <> "ServiceRateRequestId") Then
                    strHTML.Append("<th style='text-align: left'>" + DC.ColumnName + "</th>")
                End If
            Next
            strHTML.Append("</tr>")
            For Each Dr As DataRow In Tariff.Rows
                strHTML.Append("<tr style='background-color:#EFF3FB;'>")
                For Each DC As DataColumn In Tariff.Columns
                    If (DC.ColumnName <> "addrRateRequestID" And DC.ColumnName <> "TariffRateRequestID" And DC.ColumnName <> "ViewRateRequestID" And DC.ColumnName <> "ServiceRateRequestId") Then
                        strHTML.Append("<td style='text-align: left;width:100px;'>" + Dr(DC.ColumnName).ToString() + "</td>")
                    End If
                Next
                strHTML.Append("</tr>")
            Next
            strHTML.Append("</table")

        Else
            strHTML.Append("<table cellpadding='5' cellspacing='0' class='groupContainer' border='0' style='border-collapse:collapse;'><tr>")
            strHTML.Append("<th style='text-align: left'>No Tariff Available.</th>")
            strHTML.Append("</tr>")
            strHTML.Append("</table")

        End If
        Return strHTML.ToString()

    End Function
End Class