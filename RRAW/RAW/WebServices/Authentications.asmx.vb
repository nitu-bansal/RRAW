Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services

Namespace WebServices
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://rraw.invoize.com/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Authentications
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function GetAllUserDetails() As Dictionary(Of String, List(Of String))
            Return DataTableToComplexDictionary(DB.Users.GetAllUserDetails)
        End Function

        <WebMethod()> _
        Public Function GetAllUserTypes() As Dictionary(Of String, String)
            Return DataTableToDictionary(DB.UserTypes.GetAllUserTypes)
        End Function

        <WebMethod()> _
        Public Function GetAllModules() As Dictionary(Of String, List(Of String))
            Return DataTableToComplexDictionary(DB.Modules.GetAllModules)
        End Function

        <WebMethod()> _
        Public Function GetAllUserModuleAccessDetails() As Dictionary(Of String, String())
            Dim dt As DataTable = DB.UserModulesMapping.GetAllUserModuleAccessDetails

            Dim dict As New Dictionary(Of String, String())

            Dim oldKey As String = dt.Rows(0).ToString
            Dim newKey As String

            Dim lst As New List(Of String)
            For Each row As DataRow In dt.Rows
                newKey = row(0).ToString
                If oldKey = newKey Then
                    lst.Add(row(1).ToString)
                Else
                    dict.Add(oldKey, lst.ToArray)
                    lst.Clear()
                    oldKey = row(0).ToString
                    lst.Add(row(1).ToString)
                End If
            Next
            dict.Add(oldKey, lst.ToArray)

            Return dict
        End Function

        <WebMethod()> _
        Public Function GetAllInitialDetails(authenticationToken As String) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            If Not IsAuthenticated(authenticationToken) Then
                res.Add("AuthenticationError", "AuthenticationFailed.aspx")
                Return res
            End If

            With res
                .Add("GetAllUserDetails", GetAllUserDetails)
                .Add("GetAllUserTypes", GetAllUserTypes)
                .Add("GetAllModules", GetAllModules)
                .Add("GetAllUserModuleAccessDetails", GetAllUserModuleAccessDetails)
            End With

            Return res
        End Function

        <WebMethod()> _
        Public Function RemoveUser(userID As Integer) As Integer
            Return DB.Users.RemoveUser(userID)
        End Function

        <WebMethod()> _
        Public Function ResetPassword(userID As Integer, password As String) As Integer
            Return DB.Users.ChangePassword(userID, password)
        End Function

        <WebMethod()> _
        Public Function CreateUser(ByVal UserID As String, ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal ApproverID As Integer, TypeID As Integer, ByVal Station As String, ByVal Country As String, ByVal Region As String, ByVal IsEnabled As Boolean) As Integer
            Return DB.Users.CreateUser(UserID, UserName, Password, Email, ApproverID, TypeID, Station, Country, Region, Now, IsEnabled)
        End Function

        <WebMethod()> _
        Public Function UpdateUserDetails(ByVal ID As Integer, ByVal UserID As String, ByVal UserName As String, ByVal Email As String, ByVal ApproverID As Integer, TypeID As Integer, ByVal Station As String, ByVal Country As String, ByVal Region As String, ByVal IsEnabled As Boolean) As Integer
            Return DB.Users.UpdateUserDetails(ID, UserID, UserName, Email, ApproverID, TypeID, Station, Country, Region, IsEnabled)
        End Function

        <WebMethod()> _
        Public Function UpdateUserAccess(AdminUserID As Integer, ByVal ID As Integer, ModuleIDs As List(Of Integer)) As Integer
            DB.UserModulesMapping.RemoveAllModuleAccessOfUser(ID)

            For Each ModuleID As Integer In ModuleIDs
                DB.UserModulesMapping.AddModuleAccessToUser(AdminUserID, ID, ModuleID, Now, DateAdd(DateInterval.Year, 1, Now))
            Next
        End Function

        <WebMethod(EnableSession:=True)>
        Public Function GetAllMasters(ByVal AuthenticationToken As String) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then
                res.Add("Error", "AuthenticationFailed.aspx")

                Return res
            End If

            res.Add("GetAllClients", GetAllClients(CurrentUserID))
            res.Add("GetAllParties", GetAllParties())
            res.Add("GetAllUsers", GetAllUsers)
            res.Add("GetAllchargeTypes", GetAllchargeTypes)


            Return res
        End Function

        <WebMethod(EnableSession:=True)>
        Public Function GetAllUOMs(ByVal AuthenticationToken As String, ByVal RateTransportModeID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then
                res.Add("Error", "AuthenticationFailed.aspx")

                Return res
            End If
            res.Add("GetAllUOMs", GetAllUOMs(RateTransportModeID))


            Return res
        End Function

        <WebMethod(EnableSession:=True)>
        Public Function GetAllDecNoClient(ByVal AuthenticationToken As String, ByVal ClientId As Integer, ByVal TransportModeId As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)

            'Check for call authentication
            If Not IsAuthenticated(AuthenticationToken) Then
                res.Add("Error", "AuthenticationFailed.aspx")

                Return res
            End If
            res.Add("GetAllDecNo", GetAllDecNo(ClientId, TransportModeId))


            Return res
        End Function

        Public Function GetAllClients(ByVal UserId As Integer) As Dictionary(Of String, String)

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            res = DataTableToDictionary(DB.Dashboard.GetAllClients(UserId))

            Return res
        End Function
        Public Function GetAllParties() As Dictionary(Of String, String)

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            res = DataTableToDictionary(DB.Masters.GetAllParties())

            Return res
        End Function
        Public Function GetAllUOMs(ByVal RateTransportModeID As Integer) As Dictionary(Of String, String)

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            res = DataTableToDictionary(DB.Masters.GetAllUOMs(RateTransportModeID))

            Return res
        End Function
        Public Function GetAllchargeTypes() As Dictionary(Of String, String)

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            res = DataTableToDictionary(DB.Masters.GetAllchargeTypes())

            Return res
        End Function
        Public Function GetAllDecNo(ByVal ClientId As Integer, ByVal TransportModeId As Integer) As Dictionary(Of String, String)

            Dim res As Dictionary(Of String, String) '= GetCachedDictionaryItems(cacheItemName)
            res = DataTableToDictionary(DB.Masters.GetAllDecNo(ClientId, TransportModeId))

            Return res
        End Function

        <WebMethod()> _
        Public Function GetAllUsers() As Dictionary(Of String, List(Of String))
            Return DataTableToComplexDictionary(DB.Users.GetAllUsers)
        End Function

        <WebMethod()> _
        Public Function GetCustomizeUI(ByVal authenticationToken As String, ByVal ClientId As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)
            If Not IsAuthenticated(authenticationToken) Then
                res.Add("AuthenticationError", "AuthenticationFailed.aspx")
                Return res
            End If
            Dim dt As DataTable = DB.UserModulesMapping.GetCustomizeUI(ClientId, True)

            res.Add("CustomizeUI", DataTableToComplexDictionaryWithAllColumn(dt))
            Return res
        End Function

        <WebMethod()> _
        Public Function InsertCustomizeUI(ByVal ClientId As Integer, ByVal IsInsert As Boolean, ByVal FieldID As Integer, ByVal TransportModeID As Integer, ByVal DisplayOrder As Integer) As Integer
            DB.UserModulesMapping.InsertCustomizeUI(ClientId, True, FieldID, TransportModeID, DisplayOrder)
        End Function
        <WebMethod()> _
        Public Function DeleteCustomizeUI(ByVal ClientId As Integer, ByVal IsDelete As Boolean, ByVal FieldID As Integer, ByVal TransportModeID As Integer) As Integer
            DB.UserModulesMapping.DeleteCustomizeUI(ClientId, True, FieldID, TransportModeID)
        End Function
        <WebMethod()> _
        Public Function UpdateCustomizeUISequence(ByVal ClientId As Integer, ByVal IsUpdate As Boolean, ByVal FieldID As Integer, ByVal DisplayOrder As Integer) As Integer
            DB.UserModulesMapping.UpdateCustomizeUISequence(ClientId, IsUpdate, FieldID, DisplayOrder)
        End Function

        <WebMethod()> _
        Public Function GetCustomizeRatesUI(ByVal authenticationToken As String, ByVal ClientId As Integer, ByVal TransportModeID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)
            If Not IsAuthenticated(authenticationToken) Then
                res.Add("AuthenticationError", "AuthenticationFailed.aspx")
                Return res
            End If
            Dim dt As DataTable = DB.UserModulesMapping.GetCustomizeRatesUI(ClientId, TransportModeID, True)

            res.Add("CustomizeRateUI", DataTableToComplexDictionaryWithAllColumn(dt))
            Return res
        End Function


        <WebMethod()> _
        Public Function InsertCustomizeRatesUI(ByVal ClientId As Integer, ByVal IsInsert As Boolean, ByVal RateID As Integer, ByVal TransportModeID As Integer, ByVal DisplayOrder As Integer, ByVal PartyId As Integer,
                                               ByVal UOMID As Integer, ByVal IsRangeTariff As Boolean, ByVal IsCurrency As Boolean, ByVal IsConvertToCurrency As Boolean, ByVal IsCurrencyConversionApply As Boolean,
                                                      ByVal ChargeTypeID As Integer) As Integer
            DB.UserModulesMapping.InsertCustomizeRatesUI(ClientId, True, RateID, TransportModeID, DisplayOrder, PartyId, UOMID, IsRangeTariff, IsCurrency, IsConvertToCurrency, IsCurrencyConversionApply, ChargeTypeID)
        End Function
        <WebMethod()> _
        Public Function DeleteCustomizeRatesUI(ByVal ClientId As Integer, ByVal IsDelete As Boolean, ByVal RateID As Integer, ByVal TransportModeID As Integer) As Integer
            DB.UserModulesMapping.DeleteCustomizeRatesUI(ClientId, True, RateID, TransportModeID)
        End Function
        <WebMethod()> _
        Public Function UpdateCustomizeRatesUISequence(ByVal ClientId As Integer, ByVal IsUpdate As Boolean, ByVal RateID As Integer, ByVal DisplayOrder As Integer) As Integer
            DB.UserModulesMapping.UpdateCustomizeRatesUISequence(ClientId, IsUpdate, RateID, DisplayOrder)
        End Function

        <WebMethod()> _
        Public Function GetCustomizeWorkflowUI(ByVal authenticationToken As String, ByVal ClientId As Integer, ByVal TransportModeID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)
            If Not IsAuthenticated(authenticationToken) Then
                res.Add("AuthenticationError", "AuthenticationFailed.aspx")
                Return res
            End If
            Dim dt As DataTable = DB.UserModulesMapping.GetCustomizeWorkflowUI(ClientId, TransportModeID, True)

            res.Add("CustomizeWorkflowUI", DataTableToComplexDictionaryWithAllColumn(dt))
            Return res
        End Function

        <WebMethod()> _
        Public Function UpdateCustomizeWorkflowUI(ByVal WorkflowID As Integer, ByVal ClientId As Integer, ByVal Update As Boolean, ByVal DECNO As Integer, ByVal UserID As Integer, ByVal TransportModeID As Integer, ByVal ApproverID As Integer, ByVal ApprovalTime As Integer, ByVal AlertTime As Integer, ByVal ApprovingLavel As Integer) As Integer
            Return (DB.UserModulesMapping.UpdateCustomizeWorkflowUI(WorkflowID, ClientId, Update, DECNO, UserID, TransportModeID, ApproverID, ApprovalTime, AlertTime, ApprovingLavel))
        End Function

        <WebMethod()> _
        Public Function InsertCustomizeWorkflowUI(ByVal ClientId As Integer, ByVal Insert As Boolean, ByVal DECNO As Integer, ByVal UserID As Integer, ByVal TransportModeID As Integer, ByVal ApproverID As Integer, ByVal ApprovalTime As Integer, ByVal AlertTime As Integer, ByVal ApprovingLavel As Integer) As Integer
            Return (DB.UserModulesMapping.InsertCustomizeWorkflowUI(ClientId, Insert, DECNO, UserID, TransportModeID, ApproverID, ApprovalTime, AlertTime, ApprovingLavel))
        End Function

        <WebMethod()> _
        Public Function GetAttachments(ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
            Dim res As New Dictionary(Of String, Object)
            Dim Attachments = DB.Attachments.GetAttachmentsByReferenceID(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID)
            Attachments.Columns.Remove("Title")
            res.Add("Attachments", DataTableToComplexDictionary(Attachments))
            Return res
        End Function
        <WebMethod()> _
        Public Function GetSimilarRateRequestsRightPanel(ByVal RateRequestId As Integer, ByVal IsApproved As Integer) As String
            Dim strHTML As New StringBuilder
            'Check for call authentication        
            Dim RateRequest = DB.AirRateRequests.GetSimilarRateRequestsRightPanel(RateRequestId, IsApproved)

            If (RateRequest.Rows.Count > 0) Then
                strHTML.Append("<table cellpadding='5' cellspacing='0' class='SimilarLanes groupContainer' border='1' style='border-collapse:collapse;'><tr>")
                For Each DC As DataColumn In RateRequest.Columns
                    If (DC.ColumnName <> "TransportModeId") Then
                        strHTML.Append("<th style='text-align: left'>" + DC.ColumnName + "</th>")
                    End If
                Next
                strHTML.Append("</tr>")
                For Each Dr As DataRow In RateRequest.Rows
                    Dim TransportModeId As Integer = CInt(Dr("TransportModeId"))
                    For Each DC As DataColumn In RateRequest.Columns
                        If (DC.ColumnName <> "TransportModeId") Then
                            If (DC.ColumnName.ToUpper() = "RATEREQUESTID") Then
                                If (TransportModeId = 1) Then
                                    strHTML.Append("<td style='text-align: left'> <a href='NewAirRateRequest.aspx?RateRequestID=" + Dr(DC.ColumnName).ToString() + "&TransPortModeId=1&rand=" + ((New Random).Next(9999)).ToString() + "'>" + Dr(DC.ColumnName).ToString() + "</a></td>")
                                ElseIf (TransportModeId = 2) Then
                                    strHTML.Append("<td style='text-align: left'> <a href=NewOceanRateRequest.aspx?RateRequestID=" + Dr(DC.ColumnName).ToString() + "&TransPortModeId=2&rand=" + ((New Random).Next(9999)).ToString() + "'>" + Dr(DC.ColumnName).ToString() + "</a></td>")
                                Else
                                    strHTML.Append("<td style='text-align: left'> <a href=NewGroundRateRequest.aspx?RateRequestID=" + Dr(DC.ColumnName).ToString() + "&TransPortModeId=3&rand=" + ((New Random).Next(9999)).ToString() + Dr(DC.ColumnName).ToString() + "</a></td>")
                                End If

                            Else
                                strHTML.Append("<td style='text-align: left'>" + Dr(DC.ColumnName).ToString() + "</td>")
                            End If
                        End If


                    Next
                    strHTML.Append("</tr>")
                Next
                strHTML.Append("</table")

            Else
                If (IsApproved = 1) Then
                    strHTML.Append("No Similar Tariff Lane Available")
                Else
                    strHTML.Append("No Similar Rate Request Available")
                End If

            End If
            Return strHTML.ToString()
        End Function
      
    End Class
End Namespace