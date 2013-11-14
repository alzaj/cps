Imports Microsoft.VisualBasic

Public Class infPanel_0002
    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.publications
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.warning
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0002"
    End Function

    Public Overrides Function BodyText() As String
        Return "<p>This panel together with <b>infPanel_0001</b> belongs to the <b>infPanelsSet_example</b>.</p>"
    End Function
End Class
