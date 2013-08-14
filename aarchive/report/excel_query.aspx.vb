
Partial Class report_excel_query
    Inherits System.Web.UI.Page

    Dim showData As Boolean = False 'if the current user can see the database data

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        showData = True

        'only secure this page if the web application is on the stable server and not traveling
        If Not MyAppSettings.IsThisAppFeldmaessig Then
            If Not Me.Request.IsSecureConnection Then
                showData = False
            End If
        End If

        'always hide date if the user is not logged in
        If String.IsNullOrEmpty(GlobFunctions.LogedInUser) Then
            If Not GlobFunctions.LogIn(Me.Request.QueryString("n"), Me.Request.QueryString("p")) Then
                showData = False
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.showData Then
            Me.message.Text = "<table><tr><td>Access Denied :(</td></tr></table>"
            Me.ListeGridView.Visible = False
        Else
            Me.message.Text = ""
            Me.ListeGridView.Visible = True
        End If
    End Sub

End Class
