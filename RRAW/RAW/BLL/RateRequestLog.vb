Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class RateRequestLog

        Enum RateRequestOperations
            Approved
            Rejected
            Revised
            Revoked
            Generated
            Forwarded
            Archived
            CommentsRemoved
        End Enum

        Private Sub New()
        End Sub

        Public Shared Function PostRateRequestLog(ByVal RateRequestID As Integer, ByVal RateRequestOperation As RateRequestOperations, ByVal LogDate As DateTime, ByVal OperatorID As Integer) As Integer
            Dim query As String = "PostNewRateRequestLog"

            Dim param(3) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)
            param(1) = New SqlParameter("@RateRequestOperation", RateRequestOperation.ToString)
            param(2) = New SqlParameter("@LogDate", LogDate)
            param(3) = New SqlParameter("@OperatorID", OperatorID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRateRequestLogs(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveRateRequestLogs"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function GetRateRequestLogsByRequestID(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetRateRequestLogsByRequestID"

            Dim param1 As New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param1)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace