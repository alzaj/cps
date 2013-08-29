Public Class infPanel_0004

    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.video
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.inf
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0004"
    End Function

    Public Overrides Function BodyText() As String
        Dim ausgabe As New StringBuilder

        ausgabe.AppendLine("<img src=""/images/video_thumbnail_190x130px.jpg"" alt=""..."">")
        ausgabe.AppendLine("<p>This infPanel is added directly to this page. (see the page's vb-code)</p>")

        ausgabe.AppendLine("<p><a href=""#"" class=""btn btn-primary"">Button</a> <a href=""#"" class=""btn btn-default"">Button</a></p>")

        Return ausgabe.ToString
    End Function

End Class
