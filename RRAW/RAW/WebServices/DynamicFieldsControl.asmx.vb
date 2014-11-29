Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports RRAW.DB

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class DynamicFieldsControl
    Inherits System.Web.Services.WebService

    Enum OperationMode
        Post = 1
        Save = 2
        Approve = 3
        Reject = 4
        Sendback = 5
        SendBackToReq = 6

    End Enum
#Region "Dynamic Fields Generation Region"

    ''' <summary>This Function returns JSON Data for dynamic field creation based on containerType and RateRequestID and </summary>  
    '''<author>Sushrut Sawarkar</author>
    '''<CreatedDate>08.02.2013</CreatedDate>
    '''<LastUpdated>08.02.2013</LastUpdated>
    <WebMethod()> _
    Public Function GenerateContainerFields(ByVal ClientID As String, ByVal RateRequestID As String, ByVal ContainerID As String, ByVal TransportModeID As String, ByVal UserID As String) As Dictionary(Of String, Object)

        'Post Comment if valid
        Dim ds As New DataSet
        Dim query As String = "R_GetUIGenerationData"
        Dim param(4) As SqlParameter
        Dim ReturnDict As New Dictionary(Of String, Object)
        Dim TempList As New List(Of Object)

        param(0) = New SqlParameter("@ClientID", ClientID)
        param(1) = New SqlParameter("@RateRequestID", RateRequestID)
        param(2) = New SqlParameter("@ContainerID", ContainerID)
        param(3) = New SqlParameter("@TransportModeID", TransportModeID)
        param(4) = New SqlParameter("@UserID", UserID)

        Try
            Using DB As New DBClass(query, True, param)
                ds = DB.GetDataSet()

                For x As Integer = 0 To ds.Tables.Count - 1
                    For i As Integer = 0 To ds.Tables(x).Rows.Count - 1
                        Dim d As New Dictionary(Of String, Object)
                        For j As Integer = 0 To ds.Tables(x).Columns.Count - 1
                            d.Add(ds.Tables(x).Columns(j).ColumnName, IIf(IsDBNull(ds.Tables(x).Rows(i)(j)), "", ds.Tables(x).Rows(i)(j)))
                        Next
                        TempList.Add(d)
                    Next
                    ReturnDict.Add("Data" + x.ToString(), TempList)
                    TempList = New List(Of Object)
                Next

            End Using

        Catch
            Throw
        End Try
        Return ReturnDict
    End Function

    <WebMethod()> _
    Public Function GenerateRatesContainerFields(ByVal ClientID As String, ByVal RateRequestID As String, ByVal TransportModeID As String) As Dictionary(Of String, Object)

        'Post Comment if valid
        Dim ds As New DataSet
        Dim query As String = "R_GETUIRatesData"
        Dim param(2) As SqlParameter
        Dim ReturnDict As New Dictionary(Of String, Object)
        Dim TempList As New List(Of Object)

        param(0) = New SqlParameter("@ClientID", ClientID)
        param(1) = New SqlParameter("@RateRequestID", RateRequestID)
        param(2) = New SqlParameter("@TransportModeID", TransportModeID)

        Try
            Using DB As New DBClass(query, True, param)
                ds = DB.GetDataSet()

                For x As Integer = 0 To ds.Tables.Count - 1
                    For i As Integer = 0 To ds.Tables(x).Rows.Count - 1
                        Dim d As New Dictionary(Of String, Object)
                        For j As Integer = 0 To ds.Tables(x).Columns.Count - 1
                            d.Add(ds.Tables(x).Columns(j).ColumnName, IIf(IsDBNull(ds.Tables(x).Rows(i)(j)), "", ds.Tables(x).Rows(i)(j)))
                        Next
                        TempList.Add(d)
                    Next
                    ReturnDict.Add("Data" + x.ToString(), TempList)
                    TempList = New List(Of Object)
                Next

            End Using

        Catch
            Throw
        End Try
        Return ReturnDict
    End Function

    <WebMethod()> _
    Public Function GenerateRangeInputFileds(ByVal ScaleCode As String, ByVal RateRequestID As String, ByVal RateID As String) As Dictionary(Of String, Object)

        'Post Comment if valid
        Dim ds As New DataSet
        Dim query As String = "R_GETUIRangeInputs"
        Dim ReturnDict As New Dictionary(Of String, Object)
        Dim TempList As New List(Of Object)
        Dim param(2) As SqlParameter
        param(0) = New SqlParameter("@ScaleCode", ScaleCode)
        param(1) = New SqlParameter("@RateRequestID", RateRequestID)
        param(2) = New SqlParameter("@RateID", RateID)

        Try
            Using DB As New DBClass(query, True, param)
                ds = DB.GetDataSet()

                For x As Integer = 0 To ds.Tables.Count - 1
                    For i As Integer = 0 To ds.Tables(x).Rows.Count - 1
                        Dim d As New Dictionary(Of String, Object)
                        For j As Integer = 0 To ds.Tables(x).Columns.Count - 1
                            d.Add(ds.Tables(x).Columns(j).ColumnName, IIf(IsDBNull(ds.Tables(x).Rows(i)(j)), "", ds.Tables(x).Rows(i)(j)))
                        Next
                        TempList.Add(d)
                    Next
                    ReturnDict.Add("Data" + x.ToString(), TempList)
                    TempList = New List(Of Object)
                Next

            End Using

        Catch
            Throw
        End Try
        Return ReturnDict
    End Function
#End Region

#Region "Saving Fields"

    <WebMethod()> _
    Public Function NewRateRequest(ByVal ClientID As String, ByVal TransPortMode As String, ByVal RequestType As String, ByVal UserID As String) As String

        Dim ds As New DataSet
        Dim query1 As String = "R_SaveNewRateRequest"
        ' Dim query3 As String = "R_AddRateRequestLogs"
        Dim query4 As String = "R_SaveTariffMasterData"
        Dim param1(3) As SqlParameter

        ' Dim param3(1) As SqlParameter
        Dim param4(4) As SqlParameter


        Dim ReturnDict As New Dictionary(Of String, Object)
        Dim TempList As New List(Of Object)

      
        Dim RateRequestID As String = ""

        Try
            param1(0) = New SqlParameter("@ClientID", ClientID)
            param1(1) = New SqlParameter("@RateRequestType", RequestType)
            param1(2) = New SqlParameter("@TransportModeID", TransPortMode)
            param1(3) = New SqlParameter("@UserID", UserID)
            Using DB As New DBClass(query1, True, param1)
                RateRequestID = DB.ExecuteScalar().ToString()
            End Using


            'param3(0) = New SqlParameter("@UserID", UserID)
            'param3(1) = New SqlParameter("@RateRequestID", RateRequestID)
            'Using DB As New DBClass(query3, True, param3)
            '    DB.ExecuteNonQuery()
            'End Using


            'param4(0) = New SqlParameter("@ClientID", ClientID)
            'param4(1) = New SqlParameter("@RateRequestID", RateRequestID)
            'param4(2) = New SqlParameter("@TransportModeID", TransPortMode)
            'param4(3) = New SqlParameter("@StatusID", "1")
            'param4(4) = New SqlParameter("@CreatedBy", UserID)


            'Using DB As New DBClass(query4, True, param4)
            '    DB.ExecuteNonQuery()
            'End Using
            Return RateRequestID
        Catch
            Throw
        End Try


    End Function
    <WebMethod()> _
    Public Function SaveData(ByVal ContainerID As String, ByVal Data As Dictionary(Of String, Object)) As Integer
        Dim ds As New DataSet
        Dim query As String = ""

        'If ContainerID = "2" Then
        '    query = "R_SaveShipmentMeasurementsContainerData"
        'End If
        If ContainerID = "3" Then
            query = "R_SaveCommodityDetailsContainerData"
        End If
        If ContainerID = "5" Then
            query = "R_SaveServiceDetailsContainerData"
        End If
        Dim sqlParams = New List(Of SqlParameter)
        Dim PramName As String = ""
        Dim PramVal As String = ""
        Dim keys As New List(Of String)(Data.Keys)
        Dim innerDict = New Dictionary(Of String, Object)


        Dim Op As String = ""

        Try
            For i As Integer = 0 To keys.Count - 1

                innerDict = TryCast(Data(keys(i)), Dictionary(Of String, Object))
                Dim InnerKeys As New List(Of String)(innerDict.Keys)
                For j As Integer = 0 To InnerKeys.Count - 1
                    PramVal = innerDict(InnerKeys(j)).ToString().Trim()
                    PramName = InnerKeys(j).ToString().Trim()
                    If (PramVal <> "") Then
                        sqlParams.Add(New SqlParameter("@" + PramName, PramVal))
                    End If
                Next

            Next
            Dim result() As SqlParameter
            result = sqlParams.ToArray()
            Using DB As New DBClass(query, True, result)
                Op = DB.ExecuteScalar().ToString()
            End Using

        Catch
            Return 1
        End Try
        If (Op = "Inserted" Or Op = "Updated") Then
            Return 0
        Else
            Return 1
        End If

    End Function
    <WebMethod()> _
    Public Function SaveServicesData(ByVal ContainerID As String, ByVal Data As Dictionary(Of String, Object), ByVal RateRequestID As Integer, ByVal ClientID As String, ByVal TransPortModeID As String) As Integer
        If (Data.Count > 0) Then


            Dim ds As New DataSet
            Dim query As String = ""

            If ContainerID = "5" Then
                query = "R_SaveServiceDetailsContainerData"
            End If
            Dim sqlParams = New List(Of SqlParameter)
            Dim PramName As String = ""
            Dim PramVal As String = ""
            Dim keys As New List(Of String)(Data.Keys)
            Dim innerDict = New Dictionary(Of String, Object)
            Dim Op As String = ""
            Dim ShipmentServicesID As String = ""
            Dim arrSplsrvId As String() = {}

            Try
                For i As Integer = 0 To keys.Count - 1

                    innerDict = TryCast(Data(keys(i)), Dictionary(Of String, Object))
                    Dim InnerKeys As New List(Of String)(innerDict.Keys)
                    For j As Integer = 0 To InnerKeys.Count - 1
                        PramVal = innerDict(InnerKeys(j)).ToString().Trim()
                        PramName = InnerKeys(j).ToString().Trim()
                        If (PramName.ToUpper = "SPECIALSERVICEID") Then
                            arrSplsrvId = PramVal.Split(CType(",", Char))
                        End If

                        If (PramVal <> "") Then
                            sqlParams.Add(New SqlParameter("@" + PramName, PramVal))
                        End If
                    Next

                Next


                Dim result() As SqlParameter
                result = sqlParams.ToArray()
                Using DB As New DBClass(query, True, result)
                    ShipmentServicesID = DB.ExecuteScalar().ToString()
                End Using

                If (CDbl(ShipmentServicesID) > 0) Then
                    query = "R_SaveSpecialServiceDetails"
                    For x As Integer = 0 To arrSplsrvId.Length - 1
                        Dim param1(4) As SqlParameter
                        param1(0) = New SqlParameter("@RateRequestID", RateRequestID)
                        param1(1) = New SqlParameter("@ClientID", ClientID)
                        param1(2) = New SqlParameter("@ShipmentServiceID", ShipmentServicesID)
                        param1(3) = New SqlParameter("@TransportModeID", TransPortModeID)
                        param1(4) = New SqlParameter("@SpecialServiceID", arrSplsrvId(x))
                        Using DB2 As New DBClass(query, True, param1)
                            Op += DB2.ExecuteScalar().ToString() + " "
                        End Using
                    Next
                    Op = "Inserted"
                ElseIf (CDbl(ShipmentServicesID) = -1) Then
                    Op = "Updated"
                End If

            Catch
                Return 1
            End Try
            If (Op = "Inserted" Or Op = "Updated") Then
                Return 0
            Else
                Return 1
            End If

        Else
            Return 0

        End If

    End Function
    <WebMethod()> _
    Public Function SaveAllData(ByVal Data As Dictionary(Of String, Object), ByVal CurrentOperationMode As Integer, ByVal RateRequestID As Integer, ByVal ClientID As String, ByVal TransPortModeID As String, ByVal UserID As String, ByVal UserType As String, ByVal ForceSave As Boolean) As Dictionary(Of String, Object)

        Dim keys As New List(Of String)(Data.Keys)
        Dim Result As Integer = 0
        Dim DecisionNo As String = ""
        Dim Message As String = ""
        Dim Excp As Boolean = False
        Dim Save As Boolean = False
        Dim DC As New DECILineItems
        Try


         
            If (CurrentOperationMode = OperationMode.Post Or CurrentOperationMode = OperationMode.Save Or ForceSave) Then

                For j As Integer = 0 To keys.Count - 1

                    If (keys(j) = "3") Then
                        Result += SaveData(keys(j), CType(Data(keys(j)), Global.System.Collections.Generic.Dictionary(Of String, Object)))
                    End If
                    If (keys(j) = "5") Then
                        Result += SaveServicesData(keys(j), CType(Data(keys(j)), Global.System.Collections.Generic.Dictionary(Of String, Object)), RateRequestID, ClientID, TransPortModeID)
                    End If

                    If (keys(j)) = "0" Or (keys(j)) = "1" Or (keys(j)) = "2" Then
                        Result += SaveShipmentData(keys(j), CType(Data(keys(j)), Global.System.Collections.Generic.Dictionary(Of String, Object)))
                    End If

                Next

            End If
            DecisionNo = DC.ProcessRulesAndGetDecisionNos(RateRequestID, ClientID, TransPortModeID)

            'POST or Approve OPERATION
            If (CurrentOperationMode = OperationMode.Post Or CurrentOperationMode = OperationMode.Approve Or CurrentOperationMode = OperationMode.SendBackToReq Or CurrentOperationMode = OperationMode.Sendback Or CurrentOperationMode = OperationMode.Reject) Then
                'Update Decision number in RateRequestMaster

                If (DecisionNo.Split(CChar(",")).Length > 1) Then
                    Message = "Unable to identify workflow please contact administrator"
                    Excp = True
                Else
                    Excp = False
                    If (Integer.Parse(DecisionNo) <> 0) Then
                        Dim query As String = "R_SaveNewRateRequest"
                        Dim param1(3) As SqlParameter
                        param1(0) = New SqlParameter("@RateRequestID", RateRequestID)
                        param1(1) = New SqlParameter("@DecNo", DecisionNo)
                        param1(2) = New SqlParameter("@ClientID", ClientID)
                        param1(3) = New SqlParameter("@UserID", UserID)
                        Using DB As New DBClass(query, True, param1)
                            DB.ExecuteNonQuery()
                        End Using
                    Else
                        Message = "Unable to identify workflow please contact administrator"
                        Excp = True
                    End If
                End If


                If (Integer.Parse(DecisionNo) <> 0) Then
                    Dim Op As String = ""
                    Dim SendBack As Integer = 0
                    Dim SendBacktoreq As Integer = 0
                    Dim IsLevel1 As Integer = 0
                    Dim FinalApprovedByClient As Integer = 0
                    Dim query As String = "R_SaveCurrentRateRequestHolders"
                    Dim param1(8) As SqlParameter
                    If (CurrentOperationMode = OperationMode.Sendback Or CurrentOperationMode = OperationMode.Reject) Then
                        SendBack = 1
                    ElseIf CurrentOperationMode = OperationMode.SendBackToReq Then
                        SendBacktoreq = 1
                    ElseIf CurrentOperationMode = OperationMode.Post Then
                        IsLevel1 = 1
                    End If

                    If (CurrentOperationMode = OperationMode.Approve And UserType.ToUpper() = "CLIENT") Then
                        FinalApprovedByClient = 1
                    End If

                    param1(0) = New SqlParameter("@RateRequestID", RateRequestID)
                    param1(1) = New SqlParameter("@ClientID", ClientID)
                    param1(2) = New SqlParameter("@DecNo", DecisionNo)
                    param1(3) = New SqlParameter("@TransportModeID", TransPortModeID)
                    param1(4) = New SqlParameter("@UserId", UserID)
                    param1(5) = New SqlParameter("@SendBack", SendBack)
                    param1(6) = New SqlParameter("@IsLevel1", IsLevel1)
                    param1(7) = New SqlParameter("@SendBackToReq", SendBacktoreq)
                    param1(8) = New SqlParameter("@FinalApprovedByClient", FinalApprovedByClient)
                    Using DB As New DBClass(query, True, param1)
                        Op = DB.ExecuteScalar().ToString()
                    End Using

                    If (Op = "Inserted" Or Op = "Corrected" Or Op = "Approved") Then
                        Excp = False
                    Else
                        Excp = True
                        Message = "Request was not posted,please contact administrator"
                    End If
                Else
                    Message = "Unable to identify workflow,please contact administrator"
                    Excp = True
                End If
            End If

            'Approve Operation
            If (CurrentOperationMode = OperationMode.Approve Or CurrentOperationMode = OperationMode.Post Or CurrentOperationMode = OperationMode.Sendback Or CurrentOperationMode = OperationMode.Reject) Then

                Dim query4 As String = "R_AddRateRequestLogs"
                Dim param4(2) As SqlParameter

                param4(0) = New SqlParameter("@UserID", UserID)
                param4(1) = New SqlParameter("@RateRequestID", RateRequestID)


                If CurrentOperationMode = OperationMode.Sendback Or CurrentOperationMode = OperationMode.Post Then
                    param4(2) = New SqlParameter("@StatusID", "3") 'Inprocess 
                ElseIf CurrentOperationMode = OperationMode.Reject Then
                    param4(2) = New SqlParameter("@StatusID", "4") 'Rejected
                ElseIf CurrentOperationMode = OperationMode.Approve Then
                    If (UserType.ToUpper() = "CLIENT") Then
                        param4(2) = New SqlParameter("@StatusID", "1") 'approved by client 
                    Else
                        param4(2) = New SqlParameter("@StatusID", "3") 'approved but in process 
                    End If


                End If

                Using DB As New DBClass(query4, True, param4)
                    DB.ExecuteNonQuery()
                End Using

            End If

            If (Result = 0) Then
                Save = True
            Else
                Save = False
            End If
        Catch ex As Exception
            Save = False
        End Try
        Dim d As New Dictionary(Of String, Object)
        d.Add("Save", Save)
        d.Add("Exp", Excp)
        d.Add("Message", Message)

        Return d

    End Function
    <WebMethod()> _
    Public Function SaveShipmentData(ByVal ContainerID As String, ByVal Data As Dictionary(Of String, Object)) As Integer
        Dim ds As New DataSet
        Dim query As String = ""
        If (Data.Count > 0) Then

            If ContainerID = "1" Then
                query = "R_SaveShipmentAddressesContainerData"
            End If

            If ContainerID = "2" Then
                query = "R_SaveShipmentMeasurementsContainerData"
            End If

            If ContainerID = "0" Then
                query = "R_SaveRatesContainerData"
            End If

            'If ContainerID = "3" Then
            '    query = "R_SaveCommodityDetailsContainerData"
            'End If


            Dim sqlParams = New List(Of SqlParameter)
            Dim PramName As String = ""
            Dim PramVal As String = ""
            Dim keys As New List(Of String)(Data.Keys)
            Dim innerDict = New Dictionary(Of String, Object)
            Dim AllowBlank = False

            Dim Op As String = "NODATA"

            Try
                If (CDbl(ContainerID) = 1) Then
                    AllowBlank = True
                End If
                For i As Integer = 0 To keys.Count - 1
                    sqlParams = New List(Of SqlParameter)
                    innerDict = TryCast(Data(keys(i)), Dictionary(Of String, Object))
                    Dim InnerKeys As New List(Of String)(innerDict.Keys)
                    For j As Integer = 0 To InnerKeys.Count - 1
                        PramVal = innerDict(InnerKeys(j)).ToString().Trim()
                        PramName = InnerKeys(j).ToString().Trim()
                        If PramName = "MasterID" And PramVal = "" Then
                            Continue For
                        End If
                        If (PramVal <> "" Or AllowBlank) Then
                            sqlParams.Add(New SqlParameter("@" + PramName, PramVal))
                        End If
                    Next
                    Dim result() As SqlParameter
                    result = sqlParams.ToArray()
                    Using DB As New DBClass(query, True, result)
                        Op += DB.ExecuteScalar().ToString() + " "
                    End Using

                Next

            Catch
                Return 1
            End Try
            If (Op.Contains("Inserted") Or Op.Contains("Updated") Or Op.Contains("Corrected") Or Op.Contains("NODATA")) Then
                Return 0
            Else
                Return 1
            End If
        Else
            Return 0
        End If
    End Function
    <WebMethod()> _
    Public Function DeleteShipmentMeasurementRow(ByVal MasterID As String) As Boolean
        Dim query As String = "Delete From ShipmentMeasurements where ID=" + MasterID
        Dim x As String
        Using DB As New DBClass(query, False)
            x = DB.ExecuteNonQuery().ToString()
        End Using
        If (CInt(x) > 0) Then
            Return True
        Else
            Return False
        End If

    End Function
    <WebMethod()>
    Public Function AddNewComment(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Boolean

        If Comment.Trim() = "" Then Exit Function

        'Post Comment if valid
        Dim query As String = "R_PostNewComment"

        Dim param(2) As SqlParameter

        param(0) = New SqlParameter("@RateRequestID", RateRequestID)
        param(1) = New SqlParameter("@UserID", UserID)
        param(2) = New SqlParameter("@Comment", Comment)

        Try
            Using DB As New DBClass(query, True, param)
                DB.ExecuteScalar()
                Return True
            End Using
        Catch

            Return False
        End Try


    End Function
    <WebMethod()> _
    Public Function GetAllPreviousComments(ByVal RateRequestID As Integer) As Dictionary(Of String, Object)
        Dim res As New Dictionary(Of String, Object)
        Dim query As String = "R_GetPreviousComments"

        Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

        Try
            Using DB As New DBClass(query, True, param1)
                Dim PreviousComments = DataTableToComplexDictionaryWithColumn(DB.GetDataTable())
                If PreviousComments.Count > 0 Then
                    res.Add("PreviousComments", PreviousComments)
                Else
                    res.Add("PreviousComments", "")
                End If

                Return res
            End Using
        Catch
            Throw
        End Try

    End Function

    <WebMethod()> _
    Public Function AddAttachments(ByVal RateRequestID As Integer, ByVal Attachments() As Object) As Boolean
        For Each Attachment As Array In Attachments
            Dim FileName As String = CStr(Attachment.GetValue(0))
            Dim FilePath As String = CStr(Attachment.GetValue(1))
            If DB.Attachments.AddAttachment(DB.Attachments.AttachmentTypes.RateRequestAttachment, RateRequestID, FileName, FilePath) = -1 Then
                Return False
            End If
        Next
        Return True
    End Function
#End Region
#Region "Verify Details"



    <WebMethod()> _
    Public Function CheckForDuplicateRateRequest(ByVal ClientID As String, ByVal TransPortMode As String, ByVal RequestType As String) As Integer

        'Post Comment if valid

        Dim query As String = "R_CheckDuplicateRateRequest"
        Dim param(2) As SqlParameter
        Dim Rid As Integer

        param(0) = New SqlParameter("@ClientID", ClientID)
        param(1) = New SqlParameter("@RateRequestType", RequestType)
        param(2) = New SqlParameter("@TransportModeID", TransPortMode)

        Try
            Using DB As New DBClass(query, True, param)
                Rid = Integer.Parse(CStr(DB.ExecuteScalar()))
            End Using
        Catch
            Rid = -1
        End Try
        Return Rid
    End Function

    <WebMethod()> _
    Public Function IsUserRateRequestCreator(ByVal RateRequestID As String, ByVal UserID As String) As Boolean

        'Post Comment if valid

        Dim query As String = "R_IsUserRequestCreator"
        Dim param(1) As SqlParameter
        Dim Rid As Integer

        param(0) = New SqlParameter("@RateRequestID", RateRequestID)
        param(1) = New SqlParameter("@UserID", UserID)


        Try
            Using DB As New DBClass(query, True, param)
                Rid = Integer.Parse(CStr(DB.ExecuteScalar()))
            End Using
        Catch
            Rid = 1
        End Try

        If (Rid = 1) Then
            Return True
        Else
            Return False

        End If
    End Function



    <WebMethod()> _
    Public Function IsUserRateRequestHolder(ByVal RateRequestID As String, ByVal UserID As String) As Boolean

        'Post Comment if valid
        'R_CheckIFCurrentRateRequestHolder](1, 452)
        Dim query As String = "R_CheckIFCurrentRateRequestHolder"
        Dim param(1) As SqlParameter
        Dim id As Integer

        param(0) = New SqlParameter("@RateRequestID", RateRequestID)
        param(1) = New SqlParameter("@UserID", UserID)

        Try
            Using DB As New DBClass(query, True, param)
                id = Integer.Parse(CStr(DB.ExecuteScalar()))
            End Using
        Catch
            id = 0
        End Try

        If (id = 1) Then
            Return True
        Else
            Return False
        End If

    End Function


    <WebMethod()> _
    Public Function RateRequestStatus(ByVal RateRequestID As String) As Integer

        'Post Comment if valid
        'R_CheckIFCurrentRateRequestHolder](1, 452)
        Dim query As String = "R_CheckRateRequestStatus"
        Dim param(0) As SqlParameter
        Dim id As Integer

        param(0) = New SqlParameter("@RateRequestID", RateRequestID)


        Try
            Using DB As New DBClass(query, True, param)
                id = Integer.Parse(CStr(DB.ExecuteScalar()))
            End Using
        Catch
            id = 0
        End Try

        Return id

    End Function

    <WebMethod()> _
    Public Function GetCurrentRateRequestLevel(ByVal RateRequestID As String) As Integer

        'Post Comment if valid
        'R_CheckIFCurrentRateRequestHolder](1, 452)
        Dim query As String = "R_GetCurrentRateRequestLevel"
        Dim param(0) As SqlParameter
        Dim id As Integer

        param(0) = New SqlParameter("@RateRequestID", RateRequestID)


        Try
            Using DB As New DBClass(query, True, param)
                id = Integer.Parse(CStr(DB.ExecuteScalar()))
            End Using
        Catch
            id = 0
        End Try

        Return id

    End Function

#End Region
End Class