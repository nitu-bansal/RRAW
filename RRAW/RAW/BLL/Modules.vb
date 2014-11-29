Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Modules

        Private Sub New()
        End Sub

        Public Shared Function GetNavigationSequence(ByVal UserID As Integer) As DataTable
            Dim query As String = "GetNavigationSequence"

            Try
                Dim param(0) As SqlParameter

                param(0) = New SqlParameter("@UserID", UserID)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetAllModules() As DataTable
            Dim query As String = "GetAllModules"

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