Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class UserModulesMapping

        Private Sub New()
        End Sub

        Public Shared Function IsAccessAllowed(ByVal UserID As Integer, ByVal ModuleName As String, ByVal ClientID As Integer) As Boolean
            Dim query As String = "IsAccessAllowed"

            Try
                Dim param(2) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ModuleName", ModuleName)
                param(2) = New SqlParameter("@ClientID", ClientID)

                Using DB As New DBClass(query, True, param)
                    If Convert.ToInt32(DB.ExecuteScalar) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAccesibleModulesOfUser(ByVal UserID As Integer, ByVal ClientID As Integer) As DataTable
            Dim query As String = "GetAccesibleModulesOfUser"

            Try
                Dim param(1) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)
                param(1) = New SqlParameter("@ClientID", ClientID)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAllModuleAccessOfUser(ByVal UserID As Integer) As Integer
            Dim query As String = "RemoveAllModuleAccessOfUser"

            Try
                Dim param(0) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)

                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function AddModuleAccessToUser(ByVal AdminUserID As Integer, ByVal AccessToUserID As Integer, ByVal ModuleID As Integer, ByVal AccessGivenAt As Date, ByVal AccessExpiresAt As Date) As Integer
            Dim query As String = "AddModuleAccessToUser"

            Try
                Dim param(4) As SqlParameter

                param(0) = New SqlParameter("@AdminUserID", AdminUserID)
                param(1) = New SqlParameter("@AccessToUserID", AccessToUserID)
                param(2) = New SqlParameter("@ModuleID", ModuleID)
                param(3) = New SqlParameter("@AccessGivenAt", AccessGivenAt)
                param(4) = New SqlParameter("@AccessExpiresAt", AccessExpiresAt)

                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllUserModuleAccessDetails() As DataTable
            Dim query As String = "GetAllUserModuleAccessDetails"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetCustomizeUI(ByVal ClientId As Integer, ByVal IsSelect As Boolean) As DataTable
            Dim query As String = "CustomizeUI"
            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Select", IsSelect)
            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function InsertCustomizeUI(ByVal ClientId As Integer, ByVal IsInsert As Boolean, ByVal FieldID As Integer, ByVal TransportModeID As Integer, ByVal DisplayOrder As Integer) As Integer
            Dim query As String = "CustomizeUI"
            Dim param(4) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Insert", IsInsert)
            param(2) = New SqlParameter("@FieldID", FieldID)
            param(3) = New SqlParameter("@TransportModeID", TransportModeID)
            param(4) = New SqlParameter("@DisplayOrder", DisplayOrder)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function DeleteCustomizeUI(ByVal ClientId As Integer, ByVal IsDelete As Boolean, ByVal FieldID As Integer, ByVal TransportModeID As Integer) As Integer
            Dim query As String = "CustomizeUI"
            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Delete", IsDelete)
            param(2) = New SqlParameter("@FieldID", FieldID)
            param(3) = New SqlParameter("@TransportModeID", TransportModeID)


            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateCustomizeUISequence(ByVal ClientId As Integer, ByVal IsUpdate As Boolean, ByVal FieldID As Integer, ByVal DisplayOrder As Integer) As Integer
            Dim query As String = "CustomizeUI"
            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Update", IsUpdate)
            param(2) = New SqlParameter("@FieldID", FieldID)
            param(3) = New SqlParameter("@DisplayOrder", DisplayOrder)


            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetCustomizeRatesUI(ByVal ClientId As Integer, ByVal TransportModeID As Integer, ByVal IsSelect As Boolean) As DataTable
            Dim query As String = "CustomizeRatesUI"
            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@TransportModeID", TransportModeID)
            param(2) = New SqlParameter("@Select", IsSelect)
            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function InsertCustomizeRatesUI(ByVal ClientId As Integer, ByVal IsInsert As Boolean, ByVal RateID As Integer, ByVal TransportModeID As Integer, ByVal DisplayOrder As Integer, ByVal PartyId As Integer, ByVal UOMID As Integer,
                                                      ByVal IsRangeTariff As Boolean, ByVal IsCurrency As Boolean, ByVal IsConvertToCurrency As Boolean, ByVal IsCurrencyConversionApply As Boolean,
                                                      ByVal ChargeTypeID As Integer) As Integer
            Dim query As String = "CustomizeRatesUI"
            Dim param(11) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Insert", IsInsert)
            param(2) = New SqlParameter("@RateID", RateID)
            param(3) = New SqlParameter("@TransportModeID", TransportModeID)
            param(4) = New SqlParameter("@DisplayOrder", DisplayOrder)
            param(5) = New SqlParameter("@PartyID", PartyId)
            param(6) = New SqlParameter("@UOMID", UOMID)
            param(7) = New SqlParameter("@IsRangeTariff", IsRangeTariff)
            param(8) = New SqlParameter("@IsCurrency", IsCurrency)
            param(9) = New SqlParameter("@IsConvertToCurrency", IsConvertToCurrency)
            param(10) = New SqlParameter("@IsCurrencyConversionApply", IsCurrencyConversionApply)
            param(11) = New SqlParameter("@ChargeTypeID", ChargeTypeID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function DeleteCustomizeRatesUI(ByVal ClientId As Integer, ByVal IsDelete As Boolean, ByVal RateID As Integer, ByVal TransportModeID As Integer) As Integer
            Dim query As String = "CustomizeRatesUI"
            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Delete", IsDelete)
            param(2) = New SqlParameter("@RateID", RateID)
            param(3) = New SqlParameter("@TransportModeID", TransportModeID)


            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function UpdateCustomizeRatesUISequence(ByVal ClientId As Integer, ByVal IsUpdate As Boolean, ByVal RateID As Integer, ByVal DisplayOrder As Integer) As Integer
            Dim query As String = "CustomizeRatesUI"
            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@Update", IsUpdate)
            param(2) = New SqlParameter("@RateID", RateID)
            param(3) = New SqlParameter("@DisplayOrder", DisplayOrder)


            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteNonQuery
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetCustomizeWorkflowUI(ByVal ClientId As Integer, ByVal TransportModeID As Integer, ByVal IsSelect As Boolean) As DataTable
            Dim query As String = "CustomizeWorkflowUI"
            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@ClientID", ClientId)
            param(1) = New SqlParameter("@TransportModeID", TransportModeID)
            param(2) = New SqlParameter("@Select", IsSelect)
            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function UpdateCustomizeWorkflowUI(ByVal WorkflowID As Integer, ByVal ClientId As Integer, ByVal Update As Boolean, ByVal DECNO As Integer, ByVal UserID As Integer, ByVal TransportModeID As Integer, ByVal ApproverID As Integer, ByVal ApprovalTime As Integer, ByVal AlertTime As Integer, ByVal ApprovingLavel As Integer) As Integer
            Dim query As String = "CustomizeWorkflowUI"
            Dim param(9) As SqlParameter

            param(0) = New SqlParameter("@WorkflowID", WorkflowID)
            param(1) = New SqlParameter("@ClientId", ClientId)
            param(2) = New SqlParameter("@Update", Update)
            param(3) = New SqlParameter("@DECNO", DECNO)
            param(4) = New SqlParameter("@UserID", UserID)
            param(5) = New SqlParameter("@TransportModeID", TransportModeID)
            param(6) = New SqlParameter("@ApproverID", ApproverID)
            param(7) = New SqlParameter("@ApprovalTime", ApprovalTime)
            param(8) = New SqlParameter("@AlertTime", AlertTime)
            param(9) = New SqlParameter("@ApprovingLavel", ApprovingLavel)


            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function InsertCustomizeWorkflowUI(ByVal ClientId As Integer, ByVal Insert As Boolean, ByVal DECNO As Integer, ByVal UserID As Integer, ByVal TransportModeID As Integer, ByVal ApproverID As Integer, ByVal ApprovalTime As Integer, ByVal AlertTime As Integer, ByVal ApprovingLavel As Integer) As Integer
            Dim query As String = "CustomizeWorkflowUI"
            Dim param(8) As SqlParameter
            param(0) = New SqlParameter("@ClientId", ClientId)
            param(1) = New SqlParameter("@Insert", Insert)
            param(2) = New SqlParameter("@DECNO", DECNO)
            param(3) = New SqlParameter("@UserID", UserID)
            param(4) = New SqlParameter("@TransportModeID", TransportModeID)
            param(5) = New SqlParameter("@ApproverID", ApproverID)
            param(6) = New SqlParameter("@ApprovalTime", ApprovalTime)
            param(7) = New SqlParameter("@AlertTime", AlertTime)
            param(8) = New SqlParameter("@ApprovingLavel", ApprovingLavel)


            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

    End Class
End Namespace