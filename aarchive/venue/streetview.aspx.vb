
Partial Class venue_streetview
    Inherits GeneralWraperPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Header Is Nothing Then

            Dim scriptStr As String = ""
            scriptStr = "<script src=""http://maps.google.com/maps?file=api&amp;v=2.x&amp;key=ABQIAAAAH1ysRhEj1oS8TDJCCaxXnBSr-kz-8xPycHWtTcxZQ1eZFHSw9BQuxFWAzHYVawe8pAxl5uQSUUXPCw"" type=""text/javascript""></script>" + vbCrLf
            scriptStr += "<script type=""text/javascript"">" + vbCrLf
            scriptStr += "var myPano;" + vbCrLf


            scriptStr += "function initialize() {" + vbCrLf
            scriptStr += "var fenwayPark = new GLatLng(51.053819,13.736469);" + vbCrLf 'Coordinates 
            scriptStr += "panoramaOptions = { latlng:fenwayPark, pov:{heading: 90, pitch: 5, zoom: 1} };" + vbCrLf
            scriptStr += "myPano = new GStreetviewPanorama(document.getElementById(""pano""), panoramaOptions);" + vbCrLf
            scriptStr += "GEvent.addListener(myPano, ""error"", handleNoFlash);" + vbCrLf
            scriptStr += "}" + vbCrLf

            scriptStr += "function handleNoFlash(errorCode) {" + vbCrLf
            scriptStr += " if (errorCode == FLASH_UNAVAILABLE) {" + vbCrLf
            scriptStr += " alert(""Error: Flash doesn't appear to be supported by your browser"");" + vbCrLf
            scriptStr += " return;" + vbCrLf
            scriptStr += "}" + vbCrLf
            scriptStr += "}  " + vbCrLf
            scriptStr += "</script>"

            Me.Header.Controls.Add(New LiteralControl(scriptStr))
        End If
    End Sub
End Class
