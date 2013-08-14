
Partial Class Default2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not String.IsNullOrEmpty(Request.QueryString("action")) AndAlso Request.QueryString("action") = "showerrors" Then
            Dim urlStr As String = Me.Request.Url.AbsoluteUri.Substring(0, Me.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + "rss.aspx"
            Me.rsslinkLiteral.Text = "<link rel=""alternate"" type=""application/rss+xml"" title=""RSS"" href=""" + urlStr + """ />"

            '<link rel="alternate" type="application/rss xml" title="RSS" href="<%= Me.ResolveURL("~/rss.aspx") %>" />
            'admin wants to see errors
            Me.captionLbl.Text = "Errors list"
            messageLbl.Text = ""
            For Each l As String In ErrorReporting.GetListOfErrorsAsLinks
                messageLbl.Text += l + "<br />" + vbCrLf
            Next
            messageLbl.Text += "<br />" + vbCrLf + "<br />" + vbCrLf
            messageLbl.Text += System.Web.HttpContext.Current.Request.ServerVariables("http_user_agent") + "<br />" + vbCrLf

            messageLbl.Text += "<a href=""" + Me.ResolveUrl("default.aspx") + "?" + Q_Names.Q_setTheme + "=TabsMenu"">Use TabsMenu Yellow Skin</a><br />" + vbCrLf
            messageLbl.Text += "<a href=""" + Me.ResolveUrl("default.aspx") + "?" + Q_Names.Q_setTheme + "=TabsMenuGreen"">Use TabsMenu Green Skin</a><br />" + vbCrLf
            messageLbl.Text += "<a href=""" + Me.ResolveUrl("default.aspx") + "?" + Q_Names.Q_setTheme + "=SideMenu"">Use SideMenu Skin</a><br />" + vbCrLf
            messageLbl.Text += "<a href=""" + Me.ResolveUrl("default.aspx") + "?" + Q_Names.Q_setTheme + "=SideMenuGreen"">Use SideMenu Green Skin</a><br />" + vbCrLf

        Else 'simple show the last error to the user and write it to logs
            Me.captionLbl.Text = "Internal Error. Please try again later."
            ErrorReporting.ReportLastErrorToWebmaster()
            Me.messageLbl.Text = qcnp09.STR_FAILURE_NOTIFICATION_RECEIVED

            Dim errMsg As String = ErrorReporting.PopErrorFromSession
            If errMsg <> "" Then
                messageLbl.Text += "<br />" + vbCrLf + errMsg
            End If
        End If
    End Sub
End Class
