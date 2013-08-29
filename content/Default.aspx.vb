Partial Class _default
    Inherits GeneralWraperPage

    Public Overrides Sub InitInfoPanels()
        Me.infPanels_Add(New infPanel_0010)
    End Sub

    Public Overrides Sub IndicateNotNeededPanels()
        Me.infPanels_Remove(New infPanel_0004)
    End Sub

End Class

