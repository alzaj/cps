Public Class infPanel_0006
    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.contact
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.inf
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0006 extra für Page 170"
    End Function

End Class
