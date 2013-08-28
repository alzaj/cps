Imports Microsoft.VisualBasic

Public Class infPanel_0003
    Inherits OpaInfPanel

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.success
    End Function

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.publications
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_003 überal Oben"
    End Function

End Class
