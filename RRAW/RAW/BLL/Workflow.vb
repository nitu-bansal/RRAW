Imports System.Data.SqlClient
Namespace DB
    Public Class Workflow
        Public Shared Function GetDECIValidationExpression(ByVal ClientID As Integer, Optional ByVal TransportMode As Integer = 0, Optional ByVal GetDecNoCount As Integer = 0) As DataTable
            Dim query As String = "GetApprovalFlowRules"
            Dim param(2) As SqlParameter
            Try
                param(0) = New SqlParameter("@ClientID", ClientID)
                param(1) = New SqlParameter("@TransportModeID", TransportMode)
                param(2) = New SqlParameter("@GetDECNOCount", GetDecNoCount)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetQuatesByRateRequest(ByVal RateRequestID As Integer) As DataTable
            Dim query As String = "GetQuatesByRateRequestID"
            Dim param(0) As SqlParameter
            Try
                param(0) = New SqlParameter("@RateRequestID", RateRequestID)

                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace

