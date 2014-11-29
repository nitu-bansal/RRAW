Imports System.Data.SqlClient

Namespace DB
    ''' <summary>
    ''' Handles User activities for entire application
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class RateRequestHolders

        Private Sub New()
        End Sub

        Public Shared Function GetRateRequestHolders(ByVal RateRequestID As Integer) As List(Of Integer)
            Dim query As String = "GetRateRequestHolders"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return General.ConvertDataTableToListOfInt(DB.GetDataTable(), "RateRequestHolderID")
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveRateRequestHolders(ByVal RateRequestID As Integer) As Integer
            Dim query As String = "RemoveRateRequestHolders"

            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@RateRequestID", RateRequestID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar)
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function AddRateRequestHolders(ByVal RateRequestID As Integer, ByVal CurrentRateRequestHolders As List(Of Integer)) As Integer
            Dim query As String = "AddRateRequestHolder"
            Dim successCount As Integer = 0

            For Each RateRequestHolderID As Integer In CurrentRateRequestHolders
                Dim param(1) As SqlParameter
                param(0) = New SqlParameter("@RateRequestID", RateRequestID)
                param(1) = New SqlParameter("@RateRequestHolderID", RateRequestHolderID)

                Try
                    Using DB As New DBClass(query, True, param)
                        DB.ExecuteScalar()
                    End Using
                    successCount += 1
                Catch
                    Throw
                End Try
            Next

            Return successCount
        End Function

        Public Shared Function UpdateRateRequestHolders(ByVal RateRequestID As Integer, ByVal NewRateRequestHolders As List(Of Integer)) As Integer
            RemoveRateRequestHolders(RateRequestID)
            AddRateRequestHolders(RateRequestID, NewRateRequestHolders)
        End Function
    End Class
End Namespace