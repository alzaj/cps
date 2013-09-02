
Partial Class sscr_infPanels_demo
    Inherits contentFamily_example

    Public Overrides Sub InitInfoPanels()
        infPanels_Add(New infPanel_0004)
    End Sub

    Public Overrides Sub IndicateNotNeededPanels()
        infPanels_Remove(New infPanel_0003)
        'Me.infPanels.Clear()
    End Sub

    Public Overrides Sub InitRelatedLinks()
        relatedLinks_Add("http://www.domain1.de", "Link added directly to this page")
    End Sub

    Public Overrides Sub IndicateNotNeededLinks()
        relatedLinks_Remove("http://www.domain3.de")
    End Sub

End Class
