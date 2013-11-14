
Partial Class message
    Inherits GeneralWraperPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.messageLbl.Text = MessageReporting.PopMessageFromSession
    End Sub
End Class
