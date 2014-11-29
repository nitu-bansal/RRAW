Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Masters
        Private Sub New()
        End Sub

        Friend Shared Function GetAllOceanShipMethods() As DataTable
            Dim query As String = "GetAllOceanShipMethods"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllRegions() As DataTable
            Dim query As String = "GetAllRegions"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllOceanRatesValidFor() As DataTable
            Dim query As String = "GetAllOceanRatesValidFor"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllRateBasedOn() As DataTable
            Dim query As String = "GetAllRateBasedOn"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllTruckRatesValidFor() As DataTable
            Dim query As String = "GetAllTruckRatesValidFor"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllWarehouseTypes() As DataTable
            Dim query As String = "GetAllWarehouseTypes"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllTonnage() As DataTable
            Dim query As String = "GetAllTonnage"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Friend Shared Function GetAllEuropianZone() As DataTable
            Dim query As String = "GetAllEuropianZone"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Friend Shared Function GetAllDocumentCategory() As DataTable
            Dim query As String = "GetAllDocumentCategory"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetAllParties() As DataTable
            Dim query As String = "GetAllParties"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetAllchargeTypes() As DataTable
            Dim query As String = "GetAllchargeTypes"

            Try
                Using DB As New DBClass(query, True)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetAllUOMs(ByVal RateTransportModeID As Integer) As DataTable
            Dim query As String = "GetAllUOMs"

            Try
                Dim param(0) As SqlParameter
                param(0) = New SqlParameter("@RateTransportModeID", RateTransportModeID)
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function
        Public Shared Function GetAllDecNo(ByVal ClientId As Integer, ByVal TransportModeId As Integer) As DataTable
            Dim query As String = "GetAllDecNo"
            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@ClientId", ClientId)
            param(1) = New SqlParameter("@TransportModeId", TransportModeId)
            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

    End Class
End Namespace