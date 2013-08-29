Imports System.Collections.Generic
Public Class infPanel_RelatedLinks
    Inherits OpaInfPanel

#Region "RelatedLinks"
    Public linksList As New List(Of KeyValuePair(Of String, String))
#End Region 'RelatedLinks

    Public Overrides Function PanelHeaderType() As OpaInfPanel.HeaderTypes
        Return HeaderTypes.link
    End Function

    Public Overrides Function PanelColorType() As OpaInfPanel.ColorTypes
        Return ColorTypes.danger
    End Function

    Public Overrides Function HeaderText() As String
        Return "Related Links"
    End Function

    Public Overrides Function RenderBody() As String
        Dim ausgabe As New StringBuilder

        ausgabe.AppendLine("<ul class=""list-group"">")
        '<li class="list-group-item"><a href="#rellink1">DFG Research Unit 559</a></li>
        For Each kvp As KeyValuePair(Of String, String) In linksList
            ausgabe.AppendLine("<li class=""list-group-item""><a href=""" + kvp.Key + """ target=""_blank"">" + kvp.Value + "</a></li>")
        Next
        ausgabe.AppendLine("</ul>")
        Return ausgabe.ToString
    End Function

End Class
