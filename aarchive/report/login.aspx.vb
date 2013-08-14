
Partial Class login
    Inherits GeneralWraperPage


    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not GlobFunctions.RequestVonInnen Then
            Throw New Exception("Warnung: Attempt to access conference management interface from the Internet.")
        End If
    End Sub

    Public ReadOnly Property FromLoginRedirectTo() As String
        Get
            Dim s As String = System.Web.HttpContext.Current.Session("FromLoginRedirectTo")
            If s Is Nothing Then s = "default.aspx"
            Return s
        End Get

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.UserName.Focus()
        If Me.LogedIn Then Response.Redirect(Me.FromLoginRedirectTo)
        Me.InvalidLoginMessage.Visible = False
    End Sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        If GlobFunctions.LogIn(Me.UserName.Text, Me.Password.Text) Then
            Response.Redirect(Me.FromLoginRedirectTo)
        Else
            Me.InvalidLoginMessage.Visible = True
        End If
    End Sub

    Public Function LogedIn() As Boolean
        If GlobFunctions.LogedInUser = "allesOK" Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
