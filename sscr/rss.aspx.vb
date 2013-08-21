
Partial Class rss
    Inherits System.Web.UI.Page

#Region "Strings"
    Const S_ITEMS_TITLE_PREFIX As String = "HäKo Error"
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not String.IsNullOrEmpty(Request.QueryString("action")) AndAlso Request.QueryString("action") = "showerrors" Then
            Me.Response.ContentType = "text/xml"
            Me.ItemsLiteral.Text = Me.renderRssItems
        Else
            Throw New Exception("Somebody is trying to get unauthorized access to this feed")
        End If
    End Sub

    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ErrorReporting.StandardPageErrorHandling()
    End Sub

    Protected Function renderRssItems() As String
        Dim ausgabe As String = ""

        For Each e As ErrorItem In ErrorReporting.GetListOfErrorItems
            ausgabe += vbCrLf + "<item>" + vbCrLf
            ausgabe += "<pubDate>" + e.Datum.ToString("R") + "</pubDate>" + vbCrLf
            ausgabe += "<title>" + S_ITEMS_TITLE_PREFIX + " " + (e.Datum.Hour + 1).ToString.PadLeft(2, "0") + ":" + e.Datum.Minute.ToString.PadLeft(2, "0") + "(" + e.Datum.Second.ToString.PadLeft(2, "0") + "." + e.Datum.Millisecond.ToString.PadLeft(3, "0") + ")</title>" + vbCrLf
            ausgabe += "<description><![CDATA[" + vbCrLf
            ausgabe += e.Description + vbCrLf
            ausgabe += "]]></description> " + vbCrLf
            ausgabe += "</item>" + vbCrLf
        Next

        Return ausgabe
    End Function
End Class
