Imports Microsoft.VisualBasic

Public Class OpaInfPanel
    Public Enum ColorTypes
        inf
        success
        warning
        danger
    End Enum

    Public Enum HeaderTypes
        simple
        video
        contact
        publications
    End Enum

    Public Overridable Function PanelColorType() As ColorTypes
        Return ColorTypes.inf
    End Function

    Public Overridable Function PanelHeaderType() As HeaderTypes
        Return HeaderTypes.simple
    End Function

    Public Overridable Function HeaderText() As String
        Return "Web developer, override the function <b>HeaderText</b> in the class """ + Me.GetType.ToString + """"
    End Function

    Public Overridable Function BodyText() As String
        Return "Web developer, override the function <b>BodyText</b> in the class """ + Me.GetType.ToString + """"
    End Function

    Public Overridable Function RenderPanel() As String
        Dim ausgabe As New StringBuilder
        Dim colorClass As String = "panel-info"
        Select Case PanelColorType()
            Case ColorTypes.success
                colorClass = "panel-success"
            Case ColorTypes.warning
                colorClass = "panel-warning"
            Case ColorTypes.danger
                colorClass = "panel-danger"
        End Select
        ausgabe.AppendLine("<div class=""panel " + colorClass + """>")
        ausgabe.Append(Me.RenderHeading)
        ausgabe.AppendLine()
        ausgabe.Append(Me.RenderBody)
        ausgabe.AppendLine()
        ausgabe.AppendLine("</div>")
        Return ausgabe.ToString
    End Function

    Public Overridable Function RenderHeading() As String
        Dim glyphicon As String = ""
        Select Case PanelHeaderType()
            Case HeaderTypes.video
                glyphicon = "<span class=""glyphicon glyphicon-film""></span>"
            Case HeaderTypes.contact
                glyphicon = "<span class=""glyphicon glyphicon-envelope""></span>"
            Case HeaderTypes.publications
                glyphicon = "<span class=""glyphicon glyphicon-book""></span>"
        End Select
        Return "<div class=""panel-heading"">" + glyphicon + "&nbsp;&nbsp;" + HeaderText() + "</div>"
    End Function

    Public Overridable Function RenderBody() As String
        Dim ausgabe As New StringBuilder

        ausgabe.AppendLine("<div class=""panel-body"">")
        ausgabe.Append(BodyText)
        ausgabe.AppendLine("</div>")

        Return ausgabe.ToString
    End Function

End Class
