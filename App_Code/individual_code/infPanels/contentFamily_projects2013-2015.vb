Public Class contentFamily_projects2013_2015
    Inherits GeneralWraperPage

    Public Overrides Sub AddAllwaysObenPanels()
        MyBase.AddAllwaysObenPanels()
        Me.infPanelsSet_Add(New infPanelsSet_Projects20132015_Oben)
    End Sub

    Public Overrides Sub AddAllwaysUntenPanels()
        Me.infPanelsSet_Add(New infPanelsSet_Projects20132015_Unten)
        MyBase.AddAllwaysUntenPanels()
    End Sub

End Class
