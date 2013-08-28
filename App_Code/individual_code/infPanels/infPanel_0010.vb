Public Class infPanel_0010
    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.contact
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.inf
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0010 extra für Page default.aspx"
    End Function

End Class
