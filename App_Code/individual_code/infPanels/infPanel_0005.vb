Imports Microsoft.VisualBasic

Public Class infPanel_0005
    Inherits OpaInfPanel

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.danger
    End Function

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.simple
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_005"
    End Function

    Public Overrides Function BodyText() As String
        Return "<p>This panel will be displayed on all pages that inherit from <b>contentFamily_example</b>.</p><p>It will be shown just <b>above</b> the infPanels declared on each page.</p>"

    End Function

End Class
