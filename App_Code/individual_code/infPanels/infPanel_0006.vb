Public Class infPanel_0006

    Inherits OpaInfPanel

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
		Return HeaderTypes.simple
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.inf
    End Function

    Public Overrides Function RenderPanel() As String
        Dim ausgabe As New StringBuilder

        ausgabe.AppendLine("<div>")
        ausgabe.AppendLine("<ul class=""list-group"">")
        ausgabe.AppendLine("<li class=""list-group-item"" style=""padding-top:0;border:none 0;""><strong>With deep appreciation to:</strong></li>")
        ausgabe.AppendLine("<li class=""list-group-item"" style=""border:none 0;""><img src=""" + System.Web.VirtualPathUtility.ToAbsolute("~/content/images/myteachers.png") + """ alt=""My Teachers"" /></li>")
        ausgabe.AppendLine("<li class=""list-group-item"" style=""border:none 0;""><a href=""http://www.google.com/"" target=""_blank""><img src=""" + System.Web.VirtualPathUtility.ToAbsolute("~/content/images/google_logo.png") + """ alt=""Google"" /></a></li>")
        ausgabe.AppendLine("<li class=""list-group-item"" style=""border:none 0;""><a href=""http://www.wikipedia.org/"" target=""_blank""><img src=""" + System.Web.VirtualPathUtility.ToAbsolute("~/content/images/wikipedia_logo.png") + """ alt=""Wikipedia"" /></a></li>")
        ausgabe.AppendLine("</ul>")
        ausgabe.AppendLine("</div>")

        Return ausgabe.ToString
    End Function
	
End Class
