Imports Microsoft.VisualBasic

Public Class infPanel_0002
    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.video
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.success
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0002 überal Unten"
    End Function
End Class
