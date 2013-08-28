Public Class infPanel_0001

    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.contact
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.success
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0001 überal Unten"
    End Function

End Class
