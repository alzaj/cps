Public Class contentFamily_example
    Inherits GeneralWraperPage

    Public Overrides Sub AddAllwaysObenPanels()
        MyBase.AddAllwaysObenPanels()
        Me.infPanels_Add(New infPanel_0003)
    End Sub

    Public Overrides Sub AddAllwaysUntenPanels()
        Me.infPanelsSet_Add(New infPanelsSet_example)
        MyBase.AddAllwaysUntenPanels()
    End Sub

    Public Overrides Sub AddAllwaysObenRelatedLinks()
        Me.relatedLinks_Add("http://www.domain2.de", "Added to contentFamily_example Oben")
    End Sub

    Public Overrides Sub AddAllwaysUntenRelatedLinks()
        Me.relatedLinks_Add("http://www.domain3.de", "Added to contentFamily_example Unten")
        Me.relatedLinksSet_Add(New relLinksSet_example)
    End Sub
End Class
