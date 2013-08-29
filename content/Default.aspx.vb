Partial Class _default
    Inherits GeneralWraperPage

    Public Overrides Sub InitInfoPanels()
        Me.infPanels_Add(New infPanel_0010)
    End Sub

    Public Overrides Sub IndicateNotNeededPanels()
        Me.infPanels_Remove(New infPanel_0004)
    End Sub

    Public Overrides Sub InitRelatedLinks()
        Me.relatedLinks_Add("http://www.google.de", "Google")
        Me.relatedLinks_Add("http://www.yahoo.com", "Yahoo")
    End Sub

    Public Overrides Sub IndicateNotNeededLinks()
        'Me.relatedLinks_Remove("http://www.yahoo.com")
    End Sub

End Class

