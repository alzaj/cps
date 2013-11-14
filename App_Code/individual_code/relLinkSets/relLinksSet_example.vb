Public Class relLinksSet_example
    Inherits OpaRelatedLinksSet

    Public Overrides Sub InitUrlAndTitlePairs()
        Me.AddLink("http://www.web.de", "WEB.DE")
    End Sub

End Class
