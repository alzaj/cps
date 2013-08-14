
Partial Class Site
    Inherits System.Web.UI.MasterPage

    Private submenus As New ListDictionary 'for storing count of items in repeaters

    Public Sub PrintViewOnLoad(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, HyperLink).Visible = PrintView.printViewEnabled
        CType(sender, HyperLink).NavigateUrl = Me.Request.FilePath + "?" + Q_Names.Q_PrintView + "=ON"
    End Sub

    Public Function RenderStartULForSubmenu(ByVal childsCount As Integer) As String
        Dim ausgabe As String = ""
        If childsCount > 0 Then ausgabe = "<ul>"
        Return ausgabe
    End Function

    Public Function RenderEndULForSubmenu(ByVal childsCount As Integer) As String
        Dim ausgabe As String = ""
        If childsCount > 0 Then ausgabe = "</ul>"
        Return ausgabe
    End Function
End Class

