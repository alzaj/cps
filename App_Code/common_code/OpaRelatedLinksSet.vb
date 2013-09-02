Imports System.Collections.Generic

Public Class OpaRelatedLinksSet
    Public urlAndTitlePairs As New List(Of KeyValuePair(Of String, String))

    Public Sub New()
        Me.InitUrlAndTitlePairs()
    End Sub

    Public Overridable Sub InitUrlAndTitlePairs()
        'Me.AddLink("http://www.example.com", "Link to Example.com")
    End Sub

    Public Sub AddLink(linkUrl As String, linkTitle As String)
        Me.urlAndTitlePairs.Add(New KeyValuePair(Of String, String)(linkUrl.ToLower, linkTitle))
    End Sub
End Class
