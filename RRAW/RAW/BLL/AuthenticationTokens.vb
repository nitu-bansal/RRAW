Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class AuthenticationTokens
        Private Sub New()
        End Sub


        Public Shared Function AddNewAuthenticationToken(AuthenticationToken As String, ExpiresOn As Date) As Integer
            Dim query As String = "AddNewAuthenticationToken"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@AuthenticationToken", AuthenticationToken)
            param(1) = New SqlParameter("@ExpiresOn", ExpiresOn)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAuthenticationToken(AuthenticationToken As String) As String
            Dim query As String = "GetAuthenticationToken"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@AuthenticationToken", AuthenticationToken)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CStr(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAuthenticationToken(AuthenticationToken As String) As Integer
            Dim query As String = "RemoveAuthenticationToken"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@AuthenticationToken", AuthenticationToken)

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