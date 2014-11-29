Imports System.Data.SqlClient

Namespace DB
    Public NotInheritable Class Attachments
        Public Enum AttachmentTypes
            FSCUpdateMalaysia = 1
            FSCUpdateThailand = 2
            OceanRates = 3
            TruckRates = 4
            OceanRateRequestTemplate = 5
            TruckRateRequestTemplate = 6
            RateRequestAttachment = 7
        End Enum

        Private Sub New()
        End Sub

        'Friend Shared Function GetAttachments(ByVal AttachmentType As AttachmentTypes) As DataTable
        '    Return GetAttachments(AttachmentType, 0)
        'End Function

        Friend Shared Function GetAttachmentsByReferenceID(ByVal AttachmentTypeID As AttachmentTypes, ByVal ReferenceID As Integer) As DataTable
            Dim query As String = "GetAttachmentsByReferenceID"

            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@AttachmentTypeID", AttachmentTypeID)
            param(1) = New SqlParameter("@ReferenceID", ReferenceID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return DB.GetDataTable
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function AddAttachment(ByVal AttachmentType As AttachmentTypes, ReferenceID As Integer, ByVal Title As String, ByVal Path As String) As Integer
            If Title.Trim = "" Or Path.Trim = "" Then Exit Function

            Dim query As String = "AddAttachment"

            Dim param(3) As SqlParameter
            param(0) = New SqlParameter("@AttachmentTypeID", AttachmentType)
            param(1) = New SqlParameter("@ReferenceID", ReferenceID)
            param(2) = New SqlParameter("@Title", Title)
            param(3) = New SqlParameter("@Path", Path)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAttachment(ByVal ID As Integer) As Integer
            Dim query As String = "RemoveAttachment"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@ID", ID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function RemoveAttachmentsByReferenceID(ByVal ReferenceID As Integer) As Integer
            Dim query As String = "RemoveAttachmentsByReferenceID"

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@ReferenceID", ReferenceID)

            Try
                Using DB As New DBClass(query, True, param)
                    Return CInt(DB.ExecuteScalar())
                End Using
            Catch
                Throw
            End Try
        End Function

        Public Shared Function AddDocument(ByVal ClientID As Integer, ByVal UserId As Integer, ByVal CategoryID As Integer, ByVal Name As String, ByVal Title As String, ByVal EffectiveDate As String, ByVal ExpiryDate As String) As Integer

            Dim query As String = "AddDocument"

            Dim param(6) As SqlParameter
            param(0) = New SqlParameter("@ClientID", ClientID)
            param(1) = New SqlParameter("@UserId", UserId)
            param(2) = New SqlParameter("@CategoryID", CategoryID)
            param(3) = New SqlParameter("@Name", Name)
            param(4) = New SqlParameter("@Title", Title)
            param(5) = New SqlParameter("@EffectiveDate", EffectiveDate)
            param(6) = New SqlParameter("@ExpiryDate", ExpiryDate)

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