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
    Protected relLinksInfPanel As New infPanel_RelatedLinks

    Protected notNeededPanels As New List(Of OpaInfPanel)
    Protected notNeededRelatedLinks As New List(Of String)

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

#Region "InfoPanels"

    Public Overridable Sub InitInfoPanels()

    End Sub

    Public Overridable Sub IndicateNotNeededPanels()

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

    Protected Sub infPanels_Remove(iP As OpaInfPanel)
        Me.notNeededPanels.Add(iP)
    End Sub

    Public Overridable Sub AddAllwaysObenPanels()
        infPanelsSet_Add(New infPanelsSet_AllPagesOben)
    End Sub

    Public Overridable Sub AddAllwaysUntenPanels()
        infPanelsSet_Add(New infPanelsSet_AllPagesUnten)
        If Me.relLinksInfPanel.linksList.Count > 0 Then infPanels_Add(Me.relLinksInfPanel)
    End Sub

    Private Sub RemoveNotNeededPanels()
        For Each nnP As OpaInfPanel In Me.notNeededPanels
            For i As Integer = Me.infPanels.Count - 1 To 0 Step -1
                If Me.infPanels(i).GetType.ToString = nnP.GetType.ToString Then
                    Me.infPanels.RemoveAt(i)
                End If
            Next
        Next
    End Sub

    Private Sub RemoveRedundantPanels()
        Dim pntr As Integer = 0
        While pntr < Me.infPanels.Count - 1
            Dim currentType As String = ""
            For i As Integer = Me.infPanels.Count - 1 - pntr To 0 Step -1
                If currentType = "" Then
                    currentType = Me.infPanels(i).GetType.ToString
                Else
                    If Me.infPanels(i).GetType.ToString = currentType Then
                        Me.infPanels.RemoveAt(i)
                    End If
                End If
            Next
            pntr += 1
        End While
    End Sub

#End Region 'InfoPanels

#Region "RelatedLinks"
    Public Overridable Sub InitRelatedLinks()

    End Sub

    Public Overridable Sub IndicateNotNeededLinks()

    End Sub

    Protected Sub relatedLinks_Add(linkURL As String, linkText As String)
        Me.relLinksInfPanel.linksList.Add(New KeyValuePair(Of String, String)(linkURL.ToLower, linkText.ToLower))
    End Sub

    Protected Sub relatedLinksSet_Add(rLSet As OpaRelatedLinksSet)
        For Each urlAndTitle As KeyValuePair(Of String, String) In rLSet.urlAndTitlePairs
            relatedLinks_Add(urlAndTitle.Key.ToLower, urlAndTitle.Value.ToLower)
        Next
    End Sub

    Protected Sub relatedLinks_Remove(linkUrl As String)
        Me.notNeededRelatedLinks.Add(linkUrl.ToLower)
    End Sub

    Public Overridable Sub AddAllwaysObenRelatedLinks()
        relatedLinksSet_Add(New relLinksSet_AllPagesOben)
    End Sub

    Public Overridable Sub AddAllwaysUntenRelatedLinks()
        relatedLinksSet_Add(New relLinksSet_AllPagesUnten)
    End Sub

    Private Sub RemoveNotNeededRelatedLinks()
        For Each nnL As String In Me.notNeededRelatedLinks
            For i As Integer = Me.relLinksInfPanel.linksList.Count - 1 To 0 Step -1
                If Me.relLinksInfPanel.linksList(i).Key = nnL Then
                    Me.relLinksInfPanel.linksList.RemoveAt(i)
                End If
            Next
        Next
    End Sub

    Private Sub RemoveRedundantRelatedLinks()
        Dim pntr As Integer = 0
        While pntr < Me.relLinksInfPanel.linksList.Count - 1
            Dim currentUrl As String = ""
            For i As Integer = Me.relLinksInfPanel.linksList.Count - 1 - pntr To 0 Step -1
                If currentUrl = "" Then
                    currentUrl = Me.relLinksInfPanel.linksList(i).Key
                Else
                    If Me.relLinksInfPanel.linksList(i).Key = currentUrl Then
                        Me.relLinksInfPanel.linksList.RemoveAt(i)
                    End If
                End If
            Next
            pntr += 1
        End While
    End Sub
#End Region 'RelatedLinks

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Me.AddAllwaysObenRelatedLinks()
        Me.InitRelatedLinks()
        Me.AddAllwaysUntenRelatedLinks()
        Me.IndicateNotNeededLinks()
        Me.RemoveNotNeededRelatedLinks()
        Me.RemoveRedundantRelatedLinks()

        Me.AddAllwaysObenPanels()
        Me.InitInfoPanels()
        Me.AddAllwaysUntenPanels()

        Me.IndicateNotNeededPanels()
        Me.RemoveNotNeededPanels()
        Me.RemoveRedundantPanels()
    End Sub
End Class
