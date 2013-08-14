
Partial Class CPFSMenuGreen
    Inherits System.Web.UI.MasterPage

    Public Sub PrintViewOnLoad(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, HyperLink).Visible = PrintView.printViewEnabled
        CType(sender, HyperLink).NavigateUrl = Me.Request.FilePath + "?" + Q_Names.Q_PrintView + "=ON"
    End Sub

End Class

