Imports System.Collections.Generic

Partial Class bootstrap
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.masterStylePlaceHolder.Controls.Clear()
        Dim jsText As String = "<script type=""text/javascript"" src=""" + ResolveUrl("~/App_Themes/bootstrap/jquery.js") + """></script>" + vbCrLf
        jsText += "<script type=""text/javascript"" src=""" + ResolveUrl("~/App_Themes/bootstrap/bootstrap.js") + """></script>" + vbCrLf
        jsText += "<script type=""text/javascript"" src=""" + ResolveUrl("~/App_Themes/bootstrap/respond.js") + """></script>" + vbCrLf
        Me.masterStylePlaceHolder.Controls.Add(New LiteralControl(jsText))
    End Sub

    Public Function RenderNaviItem(nod As SiteMapNode, level As Integer) As String
        Dim ausgabe As String = ""

        Dim isCurrent As Boolean = False
        Dim isDescCurrent As Boolean = False
        If Not SiteMap.CurrentNode Is Nothing Then
            isCurrent = SiteMap.CurrentNode.Equals(nod)
            isDescCurrent = SiteMap.CurrentNode.IsDescendantOf(nod)
        End If
        Dim expanded As Boolean = (isCurrent Or isDescCurrent) And nod.HasChildNodes

        If expanded Then
            ausgabe += RenderExpandedNodeGroupBeginn(nod, level)
            For Each nd As SiteMapNode In nod.ChildNodes
                ausgabe += RenderNaviItem(nd, level + 1)
            Next
            ausgabe += RenderExpandedNodeGroupEnd(level)
        Else
            ausgabe += RenderNaviNode(nod, level)
        End If

        Return ausgabe
    End Function

    Private Function RenderNaviNode(nod As SiteMapNode, menuLevel As Integer) As String
        Dim ausgabe As String = ""

        Dim activeStr As String = ""
        If Not SiteMap.CurrentNode Is Nothing AndAlso SiteMap.CurrentNode.Equals(nod) Then
            activeStr = " active"
        End If

        ausgabe += "<a href=""" + ResolveUrl(nod.Url) + """ class=""list-group-item" + activeStr + """>" + vbCrLf

        If Not String.IsNullOrEmpty(nod.Title) Then
            ausgabe += "<h4 class=""list-group-item-heading"">" + nod.Title + "</h4>" + vbCrLf
        End If

        If Not String.IsNullOrEmpty(nod.Description) Then
            ausgabe += "<p class=""list-group-item-text"">"
            ausgabe += nod.Description
            ausgabe += "</p>" + vbCrLf
        End If
        ausgabe += "</a>" + vbCrLf
        Return ausgabe
    End Function

    Private Function RenderExpandedNodeGroupBeginn(expandedNode As SiteMapNode, menuLevel As Integer) As String
        Dim ausgabe As String = ""
        Select Case menuLevel
            Case 1
                ausgabe += "<div class=""panel nestedNavGroup"">" + vbCrLf
                ausgabe += "<div class=""panel-heading""><h4>" + expandedNode.Title + "</h4></div>" + vbCrLf
                ausgabe += "<div class=""list-group"">" + vbCrLf
            Case 2
                ausgabe += RenderNaviNode(expandedNode, menuLevel)
            Case Else
                Throw New Exception("The sitemap can't be deeper than 3 levels.")
        End Select
        Return ausgabe
    End Function

    Private Function RenderExpandedNodeGroupEnd(menuLevel As Integer) As String
        Dim ausgabe As String = ""
        Select Case menuLevel
            Case 1
                ausgabe += "</div>" + vbCrLf 'close list-group
                ausgabe += "</div>" + vbCrLf 'close panel nestedNavGroup
            Case 2
                Return ausgabe
            Case Else
                Throw New Exception("The sitemap can't be deeper than 3 levels.")
        End Select
        Return ausgabe
    End Function

    Protected Function RenderTopNaviListItems() As String
        Dim ausgabe As String = ""

        For Each kvp As KeyValuePair(Of String, String) In MyAppSettings.GetTopNavigationEntries
            Dim activeStr As String = ""
            If Not SiteMap.RootNode Is Nothing AndAlso ResolveUrl(kvp.Key) = ResolveUrl(SiteMap.RootNode.Url) Then
                If Not SiteMap.CurrentNode Is Nothing AndAlso SiteMap.CurrentNode.Equals(SiteMap.RootNode) Then activeStr = " class=""active"""
            ElseIf isAscendantOrEqualsToCurrentNode(kvp.Key) Then
                activeStr = " class=""active"""
            End If
            ausgabe += "<li" + activeStr + "><a href=""" + ResolveUrl(kvp.Key) + """>" + kvp.Value + "</a></li>" + vbCrLf
        Next

        Return ausgabe
    End Function

    ''' <summary>
    ''' returns true in the case if the current sitemapNode or some of it's ancestors in the sitemap has url that equals to the urlToCheck.
    ''' 
    ''' </summary>
    ''' <param name="urlToCheck"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isAscendantOrEqualsToCurrentNode(urlToCheck As String) As Boolean
        Dim ausgabe As Boolean = False
        Dim cnt As Integer = 0
        Dim smcn As SiteMapNode = SiteMap.CurrentNode
        While Not smcn Is Nothing
            If ResolveUrl(smcn.Url) = ResolveUrl(urlToCheck) Then
                ausgabe = True
                Exit While
            End If
            smcn = smcn.ParentNode
        End While
        Return ausgabe
    End Function

    Protected Function RenderTopNaviListItem(linkTitle As String, linkUrl As String, isActive As Boolean) As String
        Dim ausgabe As String = ""
        '<li class="active"><a href="#">Projects 2010-2012</a></li>
        Dim liClass As String = ""
        If isActive Then liClass = " class=""active"""

        ausgabe += "<li" + liClass + "><a href=""" + ResolveUrl(linkUrl) + """>" + linkTitle + "</a></li>"

        Return ausgabe
    End Function
End Class

