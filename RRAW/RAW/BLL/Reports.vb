Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Reports

        Private Sub New()
        End Sub

        Public Shared Function GetApprovalTimelineForPendingRequests() As DataTable
            Dim query As String = "GetApprovalTimelineForPendingRequests"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetSummaryOfRequestsByMonth(monthNumber As Integer) As DataTable
            Dim query As String = "GetSummaryOfRequestsByMonth"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@MonthNumber", monthNumber)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetNewLanesAddedInMonth(ByVal monthNumber As Integer, ByVal RequestType As String) As DataTable
            Dim query As String
            If RequestType = "AIR" Then
                query = "GetNewLanesAddedInMonth"
            Else
                query = "GetOceanNewLanesAddedInMonth"
            End If

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@MonthNumber", monthNumber)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetMonthlyApprovalTimeline(monthNumber As Integer) As DataTable
            Dim query As String = "GetMonthlyApprovalTimeline"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@MonthNumber", monthNumber)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetPendingApprovalTimeline() As DataTable
            Dim query As String = "GetPendingApprovalTimeline"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetMonthlyRequestFrequency(ByVal RequestType As String) As DataTable
            Dim query As String
            If RequestType = "AIR" Then
                query = "GetMonthlyRequestFrequency"
            Else
                query = "GetOceanMonthlyRequestFrequency"
            End If
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
