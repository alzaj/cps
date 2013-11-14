
Partial Class sscr_sitemap
    Inherits GeneralWraperPage

    Public Overrides Sub IndicateNotNeededPanels()
        Me.infPanels.Clear()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim t As New StringBuilder

        If SiteMap.RootNode.HasChildNodes Then
            t.AppendLine("<ul class=""list-with-margins"">")
            For Each sn As SiteMapNode In SiteMap.RootNode.ChildNodes
                t.Append(RenderNode(sn))
            Next
            t.AppendLine("</ul>")
        End If
        Me.sitemapLbl.Text = t.ToString
    End Sub

    Public Function RenderNode(n As SiteMapNode) As String
        Dim a As New StringBuilder
        If n.Title.ToLower = "invisible" Then Return ""

        a.Append("<li>" + RenderNodeText(n))
        If n.HasChildNodes Then
            a.AppendLine(vbCrLf + "<ul class=""list-with-margins"">")
            For Each sn As SiteMapNode In n.ChildNodes
                a.Append(RenderNode(sn))
            Next
            a.AppendLine("</ul>")
        Else
            a.AppendLine("</li>")
        End If

        Return a.ToString
    End Function

    Public Function RenderNodeText(n As SiteMapNode) As String
        Dim a As String = ""

        If Not String.IsNullOrEmpty(n.Title) And Not String.IsNullOrEmpty(n.Description) Then
            a = n.Title + ": " + n.Description
        ElseIf String.IsNullOrEmpty(n.Title) Then
            a = n.Description
        ElseIf String.IsNullOrEmpty(n.Description) Then
            a = n.Title
        End If

        a = "<a href=""" + n.Url + """>" + a + "</a>"

        Return a
    End Function
End Class
