Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Users

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Returns ID of user after verifying for specific UserID and Password
        ''' </summary>
        ''' <param name="UserID">ID of user registered on server</param>
        ''' <param name="password">Password of user registered on server</param>
        ''' <remarks></remarks>
        Public Shared Function Verify(ByVal UserID As String, ByVal Password As String) As DataRow
            Dim query As String = "VerifyUser"

            Dim Param1 As New SqlParameter("@UserID", UserID.Trim())
            Dim Param2 As New SqlParameter("@Password", General.GenerateHash(Password.Trim(), EnumHashMethod.MD5)) '"46f86faa6bbf9ac94a7e459509a20ed0"

            Try
                Using DB As New DBClass(query, True, Param1, Param2)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Returns ID of user after verifying for specific UserID and Password
        ''' </summary>
        ''' <param name="UserID">ID of user registered on server</param>
        ''' <param name="Password">Password of user registered on server</param>
        ''' <remarks></remarks>
        Public Shared Function ChangePassword(ByVal ID As Integer, ByVal Password As String) As Integer
            Dim query As String = "ChangePassword"

            Dim Param1 As New SqlParameter("@ID", ID)
            Dim Param2 As New SqlParameter("@Password", General.GenerateHash(Password.Trim(), EnumHashMethod.MD5))

            Try
                Using DB As New DBClass(query, True, Param1, Param2)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveUser(ByVal ID As Integer) As Integer
            Dim query As String = "RemoveUser"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@UserID", ID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllUserNames() As DataTable
            Dim query As String = "GetAllUserNames"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllUserDetails() As DataTable
            Dim query As String = "GetAllUserDetails"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Returns user details filtered by userID (one record)
        ''' </summary>
        ''' <param name="userID">ID of user</param>
        ''' <remarks></remarks>
        Public Shared Function GetDetails(ByVal UserID As Integer) As DataRow
            Dim query As String = "GetUserDetails"

            Dim param1 As New SqlParameter("@UserID", UserID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataRow()
                End Using
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Creates new User
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function CreateUser(ByVal UserID As String, ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal ApproverID As Integer, ByVal TypeID As Integer, ByVal Station As String, ByVal Country As String, ByVal Region As String, ByVal LastLoginDate As DateTime, ByVal IsEnabled As Boolean) As Integer
            Dim query As String = "CreateUser"

            Dim param(10) As SqlParameter

            param(0) = New SqlParameter("@UserID", UserID.Trim())
            param(1) = New SqlParameter("@Name", UserName.Trim())
            param(2) = New SqlParameter("@Password", General.GenerateHash(Password, EnumHashMethod.MD5))
            param(3) = New SqlParameter("@Email", Email.Trim())
            param(4) = New SqlParameter("@ApproverID", ApproverID)
            param(5) = New SqlParameter("@TypeID", TypeID)
            param(6) = New SqlParameter("@Station", Station.Trim())
            param(7) = New SqlParameter("@Country", Country.Trim())
            param(8) = New SqlParameter("@Region", Region.Trim())
            param(9) = New SqlParameter("@LastLoginDate", LastLoginDate)
            param(10) = New SqlParameter("@IsEnabled", IsEnabled)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetNextLevelUsers(ByVal CurrentUserID As Integer) As DataTable
            Dim query As String = "GetNextLevelUsers"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@CurrentUserID", CurrentUserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetPreviousLevelUsers(ByVal CurrentUserID As Integer) As DataTable
            Dim query As String = "GetPreviousLevelUsers"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@CurrentUserID", CurrentUserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetPrevLevelUsersString(ByVal CurrentUserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            For Each rw As DataRow In GetPreviousLevelUsers(CurrentUserID).Rows
                usersString.Add(CInt(rw("ID")))
            Next

            Return usersString
        End Function

        Public Shared Function GetNextLevelUsersString(ByVal CurrentUserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            For Each rw As DataRow In GetNextLevelUsers(CurrentUserID).Rows
                usersString.Add(CInt(rw("ID")))
            Next

            Return usersString
        End Function

        Public Shared Function GetCurrentLevelUsers(ByVal CurrentUserID As Integer) As DataTable
            Dim query As String = "GetCurrentLevelUsers"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@CurrentUserID", CurrentUserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetCurrentLevelUsersString(ByVal CurrentUserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            For Each rw As DataRow In GetCurrentLevelUsers(CurrentUserID).Rows
                usersString.Add(CInt(rw("ID")))
            Next

            Return usersString
        End Function

        Public Shared Function GetSubordinateUser(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As DataRow
            Dim query As String = "GetSubordinateUser"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@CurrentUserID", CurrentUserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestor(ByVal RateRequestID As Integer, ByVal RateRequestType As String) As DataRow
            Dim query As String = "GetRateRequestor"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateRequestType", RateRequestType)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function


        Public Shared Function GetSubordinateUserString(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            usersString.Add(CInt(GetSubordinateUser(RateRequestID, CurrentUserID)("ID")))

            Return usersString
        End Function

        Public Shared Function GetRelatedUsers(ByVal RateRequestID As Integer, ByVal CurrentUserID As Integer) As DataTable
            Dim query As String = "GetRelatedUsers"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@CurrentUserID", CurrentUserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRelatedUsersString(ByVal RateRequestID As Integer, ByVal currentUserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            For Each rw As DataRow In GetRelatedUsers(RateRequestID, currentUserID).Rows
                usersString.Add(CInt(rw("ID")))
            Next

            Return usersString
        End Function

        Public Shared Function GetSuperiorUser(ByVal UserID As Integer) As DataRow
            Dim query As String = "GetSuperiorUser"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@UserID", UserID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSuperiorUserString(ByVal UserID As Integer) As List(Of Integer)
            Dim usersString As New List(Of Integer)

            usersString.Add(CInt(GetSuperiorUser(UserID)("ID")))

            Return usersString
        End Function

        Public Shared Function UpdateUserDetails(ByVal ID As Integer, ByVal UserID As String, ByVal UserName As String, ByVal Email As String, ByVal ApproverID As Integer, ByVal TypeID As Integer, ByVal Station As String, ByVal Country As String, ByVal Region As String, ByVal IsEnabled As Boolean) As Integer
            Dim query As String = "UpdateUserDetails"

            Dim param(9) As SqlParameter

            param(0) = New SqlParameter("@ID", ID)
            param(1) = New SqlParameter("@UserID", UserID.Trim())
            param(2) = New SqlParameter("@Name", UserName.Trim())
            param(3) = New SqlParameter("@Email", Email.Trim())
            param(4) = New SqlParameter("@ApproverID", ApproverID)
            param(5) = New SqlParameter("@TypeID", TypeID)
            param(6) = New SqlParameter("@Station", Station.Trim())
            param(7) = New SqlParameter("@Country", Country.Trim())
            param(8) = New SqlParameter("@Region", Region.Trim())
            param(9) = New SqlParameter("@IsEnabled", IsEnabled)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestor_15july(ByVal RateRequestID As Integer, ByVal RateRequestType As String) As DataRow
            Dim query As String = "GetRateRequestor_15july"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateRequestType", RateRequestType)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllUsers() As DataTable
            Dim query As String = "GetAllUsers_Combo"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace