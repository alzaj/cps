
Partial Class qcnp09_qcnp09
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ErrorReporting.StandardPageErrorHandling()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not GlobFunctions.LogedInUser = "allesOK" Then
            Server.Transfer(F_NAMES.F_URL_AGENT_LOGIN)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If GlobFunctions.LogedInUser = "allesOK" Then
            Me.abmeldenHyperLink.Visible = True
        Else
            Me.abmeldenHyperLink.Visible = False
        End If
    End Sub


End Class

