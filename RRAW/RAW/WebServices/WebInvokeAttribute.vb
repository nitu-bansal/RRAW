
Namespace WebServices
    NotInheritable Class WebInvokeAttribute
        Inherits Attribute

        Private _method As String
        Private _responseFormat As Object

        Property Method As String
            Get
                Return _method
            End Get
            Set(value As String)
                _method = value
            End Set
        End Property

        Property ResponseFormat As Object
            Get
                Return _responseFormat
            End Get
            Set(value As Object)
                _responseFormat = value
            End Set
        End Property

    End Class
End Namespace
