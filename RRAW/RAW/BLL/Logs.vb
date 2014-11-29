Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Logs

        Private Sub New()
        End Sub

        Friend Shared Function PostNewLog(ByVal UserActionID As EnumUserAction, ByVal Description As String) As Integer
            PostNewLog(UserActionID, CDate(SqlTypes.SqlDateTime.MinValue), Description)
        End Function

        Friend Shared Function PostNewLog(ByVal UserID As Integer, ByVal UserActionID As EnumUserAction) As Integer
            PostNewLog(UserID, Now(), System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, CurrentReleaseInfo.Release, UserActionID, CDate(SqlTypes.SqlDateTime.MinValue), HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"), HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.Version, GetScreenResolution, DBNull.Value.ToString)
        End Function

        Friend Shared Function PostNewLog(ByVal UserActionID As EnumUserAction) As Integer
            PostNewLog(UserActionID, CDate(SqlTypes.SqlDateTime.MinValue), DBNull.Value.ToString)
        End Function

        Friend Shared Function PostNewLog(ByVal UserActionID As EnumUserAction, ByVal ClientTime As DateTime) As Integer
            PostNewLog(UserActionID, ClientTime, DBNull.Value.ToString)
        End Function

        Friend Shared Function PostNewLog(ByVal UserActionID As EnumUserAction, ByVal ClientTime As DateTime, ByVal Description As String) As Integer
            PostNewLog(CurrentUserID, Now(), System.Reflection.Assembly.GetExecutingAssembly.GetName.Name, CurrentReleaseInfo.Release, UserActionID, ClientTime, HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"), HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.Version, GetScreenResolution, Description)
        End Function

        Friend Shared Function PostNewLog(ByVal UserID As Integer, ByVal LogTime As DateTime, ByVal ApplicationName As String, ByVal ApplicationVersion As String, ByVal UserActionID As EnumUserAction, ByVal ClientTime As DateTime, ByVal ClientIP As String, ByVal ClientBrowser As String, ByVal ClientBrowserVersion As String, ByVal ClientScreenResolution As String, ByVal Description As String) As Integer
            Dim query As String = "PostNewLog"

            Dim param(10) As SqlParameter

            param(0) = New SqlParameter("@UserID", UserID)
            param(1) = New SqlParameter("@LogTime", LogTime)
            param(2) = New SqlParameter("@ApplicationName", ApplicationName)
            param(3) = New SqlParameter("@ApplicationVersion", ApplicationVersion)
            param(4) = New SqlParameter("@UserActionID", UserActionID)
            param(5) = New SqlParameter("@ClientTime", ClientTime)
            param(6) = New SqlParameter("@ClientIP", ClientIP.Trim())
            param(7) = New SqlParameter("@ClientBrowser", ClientBrowser)
            param(8) = New SqlParameter("@ClientBrowserVersion", ClientBrowserVersion)
            param(9) = New SqlParameter("@ClientScreenResolution", ClientScreenResolution)
            param(10) = New SqlParameter("@Description", Description)

            Try
                Using DB As New DBClass(query, True, param)
                    Dim result As Integer = CInt(DB.ExecuteScalar())
                    LiveLogIDs = result.ToString

                    Return result
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Sub LogDead()
            Dim query As String = "LogDead"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@LogIDs", LiveLogIDs)

            Try
                Using DB As New DBClass(query, True, param)
                    DB.ExecuteNonQuery()

                    ClearSession()
                End Using
            Catch
                Throw
            End Try
        End Sub
    End Class
End Namespace
