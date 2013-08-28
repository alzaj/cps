Public Class infPanel_0004

    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.video
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.success
    End Function

    Public Overrides Function HeaderText() As String
        Return "infPanel_0004 überal Oben"
    End Function

    Public Overrides Function BodyText() As String
        Dim ausgabe As New StringBuilder

        ausgabe.AppendLine("<img src=""/images/video_thumbnail_190x130px.jpg"" alt=""..."">")
        ausgabe.AppendLine("<p><a target=""_blank"" title=""Videolink"" href=""http://www.uni-mainz.de//presse/18858.php"">Prof. Claudia Felser erhält SUR Grant der IBM</a></p>")
        ausgabe.AppendLine("<p><a href=""#"" class=""btn btn-primary"">Button</a> <a href=""#"" class=""btn btn-default"">Button</a></p>")

        Return ausgabe.ToString
    End Function

End Class
