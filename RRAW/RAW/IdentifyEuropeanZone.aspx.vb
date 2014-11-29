Public Class IdentifyEuropeanZone
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ApplyFilter()
    End Sub

    Private Sub gvEuropeanZone_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEuropeanZone.PreRender
        If gvEuropeanZone.Rows.Count > 0 Then
            gvEuropeanZone.UseAccessibleHeader = True
            gvEuropeanZone.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Public Sub ApplyFilter()
        Try
            gvEuropeanZone.DataSource = IdentifyEuropeanZones.GetEuropeanZone("F", txtCountryName.Text.Trim(), txtPC2.Text.Trim)
            gvEuropeanZone.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvEuropeanZone_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEuropeanZone.RowDataBound
        If txtCountryName.Text.Trim <> "" Or txtPC2.Text.Trim <> "" Then
            e.Row.Cells(4).BackColor = Drawing.Color.Yellow
        End If

        e.Row.Cells(0).BackColor = Drawing.Color.BurlyWood
        e.Row.Cells(2).Attributes("style") = "background-color:#FFDAB9"
        'If e.Row.RowType <> DataControlRowType.Header AndAlso e.Row.RowType <> DataControlRowType.Footer Then
        '    e.Row.Cells(2)
        'End If
    End Sub
End Class