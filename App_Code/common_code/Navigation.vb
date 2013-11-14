Imports Microsoft.VisualBasic

Public Class Navigation

#Region "functions for Site.Master"
    ''' <summary>
    ''' If li has subitems than "hasSubs" else "hasNoSubs"
    ''' </summary>
    ''' <param name="SiteNode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLiClass(ByVal SiteNode As System.Web.SiteMapNode) As String
        Dim linkActive As Boolean = GetLinkClass(SiteNode) = "linkAktive"

        If SiteNode.ChildNodes.Count > 0 Then
            Return "hasSubs"
        Else
            Return "hasNoSubs"
        End If

    End Function

    ''' <summary>
    ''' If link target shows to the active page then "linkActive" else ""
    ''' </summary>
    ''' <param name="SiteNode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetLinkClass(ByVal SiteNode As System.Web.SiteMapNode, Optional ByVal setActiveIfChildIsActive As Boolean = False) As String
        Dim ausgabe As String = ""
        Dim act As String = "linkActive"
        Dim reqFileAspx As String = System.Web.HttpContext.Current.Request.FilePath.ToLower
        Dim reqFileHtml As String = GlobFunctions.SubstringAnfangEnde(reqFileAspx, 0, reqFileAspx.LastIndexOf(".")).ToLower + "html"
        Dim reqPhysAspx As String = System.Web.HttpContext.Current.Server.MapPath(reqFileAspx).ToLower
        Dim reqPhysHtml As String = System.Web.HttpContext.Current.Server.MapPath(reqFileHtml).ToLower
        Dim reqPhysDir As String = GlobFunctions.SubstringAnfangEnde(reqPhysAspx, 0, reqPhysAspx.LastIndexOf("\"))

        Dim nodePhysPath As String = System.Web.HttpContext.Current.Server.MapPath(SiteNode.Url).ToLower
        Dim nodePhysDir As String = GlobFunctions.SubstringAnfangEnde(nodePhysPath, 0, nodePhysPath.LastIndexOf("\")).ToLower

        If nodePhysDir = reqPhysDir _
           And (nodePhysPath.EndsWith("default.html") Or nodePhysPath.EndsWith("default.aspx")) Then
            Dim needMakeActive As Boolean = True
            'searching if a neighbour with requested filename exists
            For Each nn As System.Web.SiteMapNode In SiteNode.ParentNode.ChildNodes
                Dim nPhysPath As String = System.Web.HttpContext.Current.Server.MapPath(nn.Url).ToLower
                Dim nPhysDir As String = GlobFunctions.SubstringAnfangEnde(nPhysPath, 0, nPhysPath.LastIndexOf("\")).ToLower
                If (nPhysPath = reqPhysAspx Or nPhysPath = reqPhysHtml) And Not (nPhysPath = nodePhysPath) Then
                    'neighbour with requested filename exists. it marks himself as active if needed
                    needMakeActive = False
                    Exit For
                End If
            Next

            If needMakeActive Then
                'if some childnode exists with requested filename then it marks himself as active,
                'else this node will be set as active
                For Each sn As System.Web.SiteMapNode In SiteNode.ChildNodes
                    Dim nPhysPath As String = System.Web.HttpContext.Current.Server.MapPath(sn.Url).ToLower
                    Dim nPhysDir As String = GlobFunctions.SubstringAnfangEnde(nPhysPath, 0, nPhysPath.LastIndexOf("\")).ToLower
                    If nPhysPath = reqPhysAspx Or nPhysPath = reqPhysHtml Then
                        If setActiveIfChildIsActive Then
                            needMakeActive = True
                        Else
                            needMakeActive = False
                        End If
                        Exit For
                    End If
                Next
            End If

            If needMakeActive Then ausgabe = act
        ElseIf nodePhysPath = reqPhysAspx Or nodePhysPath = reqPhysHtml Then
            ausgabe = act
        End If

        Return ausgabe
    End Function

    Public Overloads Shared Function GetLinkClass(ByVal linkUrl As String) As String
        Dim ausgabe As String = ""
        Dim act As String = "linkActive"
        Dim reqFileAspx As String = System.Web.HttpContext.Current.Request.FilePath.ToLower
        Dim reqFileHtml As String = GlobFunctions.SubstringAnfangEnde(reqFileAspx, 0, reqFileAspx.LastIndexOf(".")).ToLower + "html"
        Dim reqPhysAspx As String = System.Web.HttpContext.Current.Server.MapPath(reqFileAspx).ToLower
        Dim reqPhysHtml As String = System.Web.HttpContext.Current.Server.MapPath(reqFileHtml).ToLower
        Dim reqPhysDir As String = GlobFunctions.SubstringAnfangEnde(reqPhysAspx, 0, reqPhysAspx.LastIndexOf("\"))

        Dim nodePhysPath As String = System.Web.HttpContext.Current.Server.MapPath(linkUrl).ToLower
        Dim nodePhysDir As String = GlobFunctions.SubstringAnfangEnde(nodePhysPath, 0, nodePhysPath.LastIndexOf("\")).ToLower

        If nodePhysPath = reqPhysAspx Or nodePhysPath = reqPhysHtml Then ausgabe = act

        Return ausgabe
    End Function

#End Region 'functions for Site.Master

#Region "functions for Tabs.Master"
    Public Shared Function BuildPrimaryListItem(ByVal siteNode As System.Web.SiteMapNode) As String
        Dim act As String = "linkActive"
        Dim noWrap As String = "" '" style=""white-space:nowrap"" "
        Dim ausgabe As String = "<li>"
        Dim urlClass As String = GetLinkClass(siteNode.Url)
        If urlClass = act Then
            ausgabe += "<span" + noWrap + ">" + siteNode.Title + "</span>"
            If siteNode.ChildNodes.Count > 0 Then
                ausgabe += BuildSecondaryUL(siteNode)
            End If
            ausgabe += "</li>"
        ElseIf GetLinkClass(siteNode, True) = act Then
            Dim l As New HyperLink
            l.NavigateUrl = siteNode.Url
            l.Text = siteNode.Title
            'l.Style("white-space") = "nowrap"
            l.CssClass = act
            ausgabe += GlobFunctions.GetControlHtmlText(l)
            If siteNode.ChildNodes.Count > 0 Then
                ausgabe += BuildSecondaryUL(siteNode)
            End If
            ausgabe += "</li>"
        Else
            Dim l As New HyperLink
            l.NavigateUrl = siteNode.Url
            l.Text = siteNode.Title
            'l.Style("white-space") = "nowrap"
            ausgabe += GlobFunctions.GetControlHtmlText(l)
            ausgabe += "</li>"
        End If
        Return ausgabe
    End Function

    Public Shared Function BuildSecondaryUL(ByVal siteNode As System.Web.SiteMapNode) As String
        Dim ausgabe As String = ""

        Dim act As String = "linkActive"
        Dim noWrap As String = "" '" style=""white-space:nowrap"" "
        ausgabe += vbCrLf + "<ul id=""secondary"">" + vbCrLf
        For Each sn As System.Web.SiteMapNode In siteNode.ChildNodes
            ausgabe += "<li>"
            If GetLinkClass(sn.Url) = act Then
                ausgabe += "<span" + noWrap + ">" + sn.Title + "</span>"
            Else
                Dim ll As New HyperLink
                ll.NavigateUrl = sn.Url
                ll.Text = sn.Title
                'll.Style("white-space") = "nowrap"
                ausgabe += GlobFunctions.GetControlHtmlText(ll)
            End If
            ausgabe += "</li>" + vbCrLf
        Next
        ausgabe += "</ul>"

        Return ausgabe
    End Function
#End Region 'functions for Tabs.Master
End Class
