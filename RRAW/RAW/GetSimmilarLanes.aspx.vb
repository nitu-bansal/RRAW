Public Class GetSimilarLanes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private ReadOnly Property qStrOriginAirport() As String
        Get
            Return Request.QueryString("OriginAirport")
        End Get
    End Property

    Private ReadOnly Property qStrDestAirport() As String
        Get
            Return Request.QueryString("DestAirport")
        End Get
    End Property

    Private ReadOnly Property qStrDestCity() As String
        Get
            Return Request.QueryString("DestCity")
        End Get
    End Property

    Private ReadOnly Property qStrCEVATransitMode() As String
        Get
            Return Request.QueryString("CEVATransitMode")
        End Get
    End Property

    Private ReadOnly Property qStrShipMethod() As String
        Get
            Return Request.QueryString("ShipMethod")
        End Get
    End Property
End Class