Imports System.Data.SqlClient

<Assembly: CLSCompliant(True)> 
Namespace DB
    ''' <summary>
    ''' Performs database related operations for RRAW
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DBClass
        Implements IDisposable

        Private connection As Data.SqlClient.SqlConnection

        Private varQuery As String
        Private varParam() As SqlParameter
        Private varIsStoredProcedure As Boolean

#Region " IDisposable Support "
        Private disposedValue As Boolean        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    MyBase.Finalize()
                    'free other state (managed objects).
                    connection.Dispose()
                End If

                'free your own state (unmanaged objects).
                'set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        ''' <summary>
        ''' Creates a new object of DBClass with Query to execute and Parameters
        ''' </summary>
        ''' <param name="query">Query to be executed</param>
        ''' <param name="param">Parameters to be passed on query</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal query As String, ByVal ParamArray param() As SqlParameter)
            Try
                connection = New Data.SqlClient.SqlConnection(CurrentDBConnection)
            Catch ex As SqlException
                Throw ex
            End Try
            varQuery = query
            varParam = param
            varIsStoredProcedure = False
        End Sub

        ''' <summary>
        ''' Creates a new object of DBClass with Query to execute and Parameters
        ''' </summary>
        ''' <param name="query">Query to be executed</param>
        ''' <param name="param">Parameters to be passed on query</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal query As String, ByVal isStoredProcedure As Boolean, ByVal ParamArray param() As SqlParameter)
            connection = New Data.SqlClient.SqlConnection(CurrentDBConnection)
            varQuery = query
            varParam = param
            varIsStoredProcedure = isStoredProcedure
        End Sub

        ''' <summary>
        ''' Returns current connection string being used for the execution
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property GetConnectionString() As String
            Get
                Using DB As New DBClass(Nothing)
                    Return DB.connection.ConnectionString
                End Using
            End Get
        End Property


        Friend Function HasNewAirRateRequestAccess(ByVal userid As String) As Boolean
            Dim str As String = "select * from Rawdb..usermodulesmapping where moduleid=28 and userid=@UserID"
            Dim param(0) As SqlParameter

            param(0) = New SqlParameter("@UserID", userid)
            Using DB As New DBClass(str, param)
                Return CBool((DB.GetDataTable()).Rows.Count > 0)
            End Using
        End Function
        ''' <summary>
        ''' Executes query with parameters and Using DB As New DBClass(query, param1) ''' Returns data table
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function GetDataSet() As DataSet
            Try
                Using cmd = New SqlCommand(varQuery, connection)
                    cmd.Parameters.AddRange(varParam)

                    If varIsStoredProcedure = True Then
                        cmd.CommandType = CommandType.StoredProcedure
                    Else
                        cmd.CommandType = CommandType.Text
                    End If

                    Using da = New SqlDataAdapter(cmd)
                        Using ds = New DataSet
                            ds.Locale = System.Globalization.CultureInfo.InvariantCulture

                            da.Fill(ds)

                            Return ds
                        End Using
                    End Using
                End Using
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Executes query with parameters and Using DB As New DBClass(query, param1) ''' Returns data table
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function GetDataTable() As DataTable
            Try
                Using cmd = New SqlCommand(varQuery, connection)
                    cmd.Parameters.AddRange(varParam)

                    If varIsStoredProcedure = True Then
                        cmd.CommandType = CommandType.StoredProcedure
                    Else
                        cmd.CommandType = CommandType.Text
                    End If

                    Using da = New SqlDataAdapter(cmd)
                        Using dt = New DataTable
                            dt.Locale = System.Globalization.CultureInfo.InvariantCulture

                            da.Fill(dt)

                            Return dt
                        End Using
                    End Using
                End Using
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Executes query with parameters and Using DB As New DBClass(query, param1) ''' Returns data row
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function GetDataRow() As DataRow
            Dim dt As DataTable = GetDataTable()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Executes query with parameters and Using DB As New DBClass(query, param1) ''' Returns number of rows affected
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function ExecuteNonQuery() As Integer
            Dim result As Integer

            Try
                If Not connection.State = ConnectionState.Open Then
                    connection.Open()
                End If

                Using cmd = New SqlCommand(varQuery, connection)
                    cmd.Parameters.AddRange(varParam)

                    If varIsStoredProcedure = True Then
                        cmd.CommandType = CommandType.StoredProcedure
                    Else
                        cmd.CommandType = CommandType.Text
                    End If

                    result = cmd.ExecuteNonQuery()
                End Using

                If Not connection.State = ConnectionState.Closed Then
                    connection.Close()
                End If

                Return result
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Executes query with parameters and Using DB As New DBClass(query, param1) ''' Returns object
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function ExecuteScalar() As Object
            Dim result As Object

            Try
                If Not connection.State = ConnectionState.Open Then
                    connection.Open()
                End If

                Using cmd As New SqlCommand(varQuery, connection)
                    cmd.Parameters.AddRange(varParam)

                    If varIsStoredProcedure = True Then
                        cmd.CommandType = CommandType.StoredProcedure
                    Else
                        cmd.CommandType = CommandType.Text
                    End If

                    result = cmd.ExecuteScalar()
                End Using

                If Not connection.State = ConnectionState.Closed Then
                    connection.Close()
                End If

                Return result
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace