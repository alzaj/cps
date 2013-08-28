Imports System.Collections.Generic

''' <summary>
''' Allows setting MasterPage and Theme dynamycally. Redirects to https:\\ if the page must be secure.
''' </summary>
''' <remarks></remarks>
Public Class GeneralWraperPage
    Inherits System.Web.UI.Page

    Private _currThemeInitiated As Boolean = False


    Public Overridable ReadOnly Property PageTitlePrefix As String
        Get
            Return MyAppSettings.PageTitlePrefix
        End Get
    End Property

    Private _currPageTheme As String = ""
    Public Overrides Property StyleSheetTheme() As String
        Get
            Me.InitCurrentThema()
            Return Me._currPageTheme
        End Get
        Set(ByVal value As String)
            Me._currPageTheme = value
            MyBase.StyleSheetTheme = value
        End Set
    End Property

    Private _currPageMaster As String = ""
    Public Overrides Property MasterPageFile() As String
        Get
            Me.InitCurrentThema()
            If PrintView.needPrintView Then
                Me._currPageMaster = "~/masterpages/Print.master"
            End If
            Return Me._currPageMaster
        End Get
        Set(ByVal value As String)
            If Not _currThemeInitiated Or String.IsNullOrEmpty(Me._currPageMaster) Then
                Dim master As String = value
                If PrintView.needPrintView Then
                    master = "~/masterpages/Print.master"
                End If

                Me._currPageMaster = master
                MyBase.MasterPageFile = master
            Else
                Dim master As String = Me._currPageMaster
                If PrintView.needPrintView Then
                    master = "~/masterpages/Print.master"
                End If

                MyBase.MasterPageFile = master
            End If
        End Set
    End Property

    Protected thisPageMustBeSecure As Boolean = False

    Protected useTitlePrefix As Boolean = True

    Protected infPanels As New List(Of OpaInfPanel)

    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ErrorReporting.StandardPageErrorHandling()
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim portBase As Integer = Request.Url.Port / 1000
        portBase = portBase * 1000

        If thisPageMustBeSecure And Not Me.Request.IsSecureConnection Then
            Response.Redirect("https://" + Request.Url.Host + ":" + (portBase + 443).ToString + Request.FilePath)
        ElseIf Not thisPageMustBeSecure And Me.Request.IsSecureConnection Then
            Response.Redirect("http://" + Request.Url.Host + ":" + (portBase + 80).ToString + Request.FilePath)
        End If
    End Sub


    Private Sub InitCurrentThema()
        If Me._currThemeInitiated _
           And Not String.IsNullOrEmpty(Me._currPageTheme) _
           And Not String.IsNullOrEmpty(Me._currPageMaster) Then Exit Sub

        'note current Theme if the user changed it
        Dim newTheme As String = Me.Request.Form(P_Names.P_ThemeDropDown)
        If String.IsNullOrEmpty(newTheme) Then newTheme = Me.Request.QueryString(Q_Names.Q_setTheme)

        If Not String.IsNullOrEmpty(newTheme) Then
            Me.Session(S_Names.S_CurrentTheme) = newTheme
        End If


            Dim currTheme As String = Me.Session(S_Names.S_CurrentTheme)
            If Not String.IsNullOrEmpty(currTheme) Then
                'take Theme name from session
                Me._currPageTheme = currTheme
            Me._currPageMaster = "~/masterpages/" + currTheme + ".master"
            Else
                'if current Theme on the Session is empty set it from Web.config
                Me.Session(S_Names.S_CurrentTheme) = MyBase.StyleSheetTheme
                Me._currPageTheme = MyBase.StyleSheetTheme
                Me._currPageMaster = MyBase.MasterPageFile
            End If

            Me._currThemeInitiated = True

    End Sub

    Public Overridable Sub InitInfoPanels()

    End Sub

    Public Overridable Function RenderInfoPanels() As String
        Dim ausgabe As New StringBuilder()
        For Each iP As OpaInfPanel In Me.infPanels
            ausgabe.Append(iP.RenderPanel)
        Next
        Return ausgabe.ToString
    End Function

    Protected Sub infPanels_Add(iP As OpaInfPanel)
        Me.infPanels.Add(iP)
    End Sub

    Protected Sub infPanelsSet_Add(iPSet As OpaInfPanelsSet)
        For Each opaIP As OpaInfPanel In iPSet.infPanels
            infPanels_Add(opaIP)
        Next
    End Sub

    Public Overridable Sub AddAllwaysObenPanels()
        infPanelsSet_Add(New infPanelsSet_AllPagesOben)
    End Sub

    Public Overridable Sub AddAllwaysUntenPanels()
        infPanelsSet_Add(New infPanelsSet_AllPagesUnten)
    End Sub

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Me.AddAllwaysObenPanels()
        Me.InitInfoPanels()
        Me.AddAllwaysUntenPanels()
    End Sub
End Class
