Public Class infPanel_0001

    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.contact
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.success
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0001"
    End Function

    Public Overrides Function BodyText() As String
        Return "<p>This panel together with <b>infPanel_0002</b> will be displayed on all pages that inherit from <b>contentFamily_example</b>.</p><p>It will be shown just <b>below</b> the infPanels declared on each page.</p>"
    End Function

End Class
