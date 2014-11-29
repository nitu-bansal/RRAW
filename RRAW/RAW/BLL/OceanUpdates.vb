Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class OceanUpdates

        Private Sub New()
        End Sub
      
        Public Shared Function AddOceanUpdate(ByVal Title As String, ByVal Path As String, ByVal UpdateDate As DateTime) As Integer
            'Validate Inputs
            If Title.Trim = "" Or Path.Trim = "" Then Exit Function

            'Post New Rate Request if valid
            Dim query As String = "AddOceanUpdate"

            Dim param(2) As SqlParameter
            param(0) = New SqlParameter("@Title", Title)
            param(1) = New SqlParameter("@Path", Path)
            param(2) = New SqlParameter("@UpdateDate", UpdateDate)

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