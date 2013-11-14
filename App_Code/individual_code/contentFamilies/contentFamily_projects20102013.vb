Public Class contentFamily_projects20102013
    Inherits GeneralWraperPage

    Public Overrides Sub AddAllwaysObenPanels()
        MyBase.AddAllwaysObenPanels()
    End Sub

    Public Overrides Sub AddAllwaysUntenPanels()
        MyBase.AddAllwaysUntenPanels()
    End Sub

    Public Overrides Sub AddAllwaysObenRelatedLinks()
        
    End Sub

    Public Overrides Sub AddAllwaysUntenRelatedLinks()
        Me.relatedLinksSet_Add(new relLinksSet_projects20102013)
    End Sub
End Class
