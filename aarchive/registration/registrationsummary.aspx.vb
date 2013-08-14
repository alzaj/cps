
Partial Class registration_registrationsummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Request.IsSecureConnection Or String.IsNullOrEmpty(Me.Session(S_Names.S_RegistrationSummary)) Then
            Me.summaryLbl.Text = "Summary not available."
        Else
            Me.summaryLbl.Text = "<h3>Registration for Hemdsärmelkolloquium 2011</h3>"

            Me.summaryLbl.Text += Me.Session(S_Names.S_RegistrationSummary)

            ''The user has submited the form. we need to reset the session
            'Me.Session.Abandon()
            'Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    End Sub


End Class
