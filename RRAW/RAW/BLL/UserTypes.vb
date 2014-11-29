Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class UserTypes

        Private Sub New()
        End Sub

        Public Shared Function GetAllUserTypes(ByVal filter As String) As DataTable
            Dim query As String = "SELECT DISTINCT Name, ID FROM UserTypes WHERE Name LIKE '%" & filter & "%'"

            Try
                Using DB As New DBClass(query)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllUserTypes() As DataTable
            Dim query As String = "GetAllUserTypes"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetUserTypeByUserID(ByVal UserID As Integer) As String
            Dim query As String = "GetUserTypeByUserID"

            Try
                Dim param(0) As SqlParameter
                param(0) = New SqlParameter("@UserID", UserID)

                Using DB As New DBClass(query, True, param)
                    Return DB.ExecuteScalar.ToString
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace