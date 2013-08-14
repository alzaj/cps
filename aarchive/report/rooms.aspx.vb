
Partial Class report_rooms
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Insert" Then
            'TODO: Insert new record...
            Me.SqlDataSource1.Insert()
        End If
    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'personenRepeater
            Dim repeaterChild As Repeater = GlobFunctions.SuperFindControl(e.Row, "personenRepeater")
            If repeaterChild Is Nothing Then Exit Sub
            sourceParticipants.SelectParameters(0).DefaultValue = GridView1.DataKeys(e.Row.DataItemIndex).Value.ToString
            Dim data As Object = sourceParticipants.Select(DataSourceSelectArguments.Empty)

            repeaterChild.DataSource = data
            repeaterChild.DataBind()
        End If
    End Sub
End Class
