Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Blogs

        Private Sub New()
        End Sub

        Public Shared Function GetBlogEntries() As DataTable
            Dim query As String = "GetBlogEntries"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetDeveloperBlogEntries() As DataTable
            Dim query As String = "GetDeveloperBlogEntries"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetBlogEntryByTitle(ByVal Title As String) As DataRow
            Dim query As String = "GetBlogEntryByTitle"

            Try
                Dim param(0) As SqlParameter

                param(0) = New SqlParameter("@Title", Title)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetLatestBlogEntry() As DataRow
            Dim query As String = "GetLatestBlogEntry"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataRow
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace
