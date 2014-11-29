Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Comments

        Private Sub New()
        End Sub

        Public Shared Function PostNewAirComment(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
            'Validate Inputs
            If Comment.Trim() = "" Then Exit Function

            'Post Comment if valid
            Dim query As String = "PostNewAirComment"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@UserID", UserID)
            param(2) = New SqlParameter("@Comment", Comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestComments(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetRateRequestComments"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function PostNewAirComment_15july(ByVal RateRequestID As Integer, ByVal UserID As Integer, ByVal Comment As String) As Integer
            'Validate Inputs
            If Comment.Trim() = "" Then Exit Function

            'Post Comment if valid
            Dim query As String = "PostNewAirComment_15july"

            Dim param(2) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@UserID", UserID)
            param(2) = New SqlParameter("@Comment", Comment)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

    End Class
End Namespace
