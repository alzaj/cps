Imports System.Collections.Generic

Partial Class sponsors_default
    Inherits GeneralWraperPage

    Class SponsorElement
        Public FirmenName As String = ""
        Public wwwURL As String = ""
        Public imgURL As String = ""
        Public Sub New(ByVal name As String, ByVal www As String, ByVal img As String)
            If String.IsNullOrEmpty(name) Then Throw New Exception("Firmenname ist Pflichtfeld")
            Me.FirmenName = name
            Me.wwwURL = www
            Me.imgURL = img
        End Sub
    End Class

    Public largestImageWidthEM As Integer = 25
    Public largestImageHeightEM As Integer = 13
    Public sponsors As New List(Of SponsorElement)

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Me.sponsors.Add(New SponsorElement("Ametek GmbH", "", ""))
        Me.sponsors.Add(New SponsorElement("Applichem", "http://www.applichem.com", "applichem_logo.png"))
        Me.sponsors.Add(New SponsorElement("BASF", "http://www.basf.com", "basf_logo.png"))
        Me.sponsors.Add(New SponsorElement("Berghof Products + Instruments GmbH", "http://www.berghof.com", "berghof_logo.png"))
        Me.sponsors.Add(New SponsorElement("Carl Zeiss MicroImaging GmbH", "", ""))
        Me.sponsors.Add(New SponsorElement("Chempur Karlsruhe", "", ""))
        Me.sponsors.Add(New SponsorElement("De Gruyter", "", ""))
        Me.sponsors.Add(New SponsorElement("FEI Deutschland GmbH", "", ""))
        Me.sponsors.Add(New SponsorElement("Fonds der Chemischen Industrie", "http://www.vci.de", "fci_logo.jpg"))
        Me.sponsors.Add(New SponsorElement("Gatan GmbH", "http://www.gatan.com", "gatan_logo.png"))
        Me.sponsors.Add(New SponsorElement("Goodfellow GmbH", "", ""))
        Me.sponsors.Add(New SponsorElement("HTM Reetz GmbH", "http://www.htm-reetz.de", "htm_logo.png"))
        Me.sponsors.Add(New SponsorElement("LECO Instrumente GmbH", "http://www.leco.de", "leco_logo.png"))
        Me.sponsors.Add(New SponsorElement("Max-Planck-Gesellschaft", "http://www.mpg.de", "mpg_logo.png"))
        Me.sponsors.Add(New SponsorElement("MBraun", "", ""))
        Me.sponsors.Add(New SponsorElement("Merck KGaA", "", ""))
        Me.sponsors.Add(New SponsorElement("NETZSCH-Gerätebau GmbH", "http://www.netzsch-thermal-analysis.com", "netzsch_logo.png"))
        Me.sponsors.Add(New SponsorElement("Oerlikon Vacuum Germany", "", ""))
        Me.sponsors.Add(New SponsorElement("PerkinElmer", "", ""))
        Me.sponsors.Add(New SponsorElement("PRAXAIR Deutschland GmbH & Co. KG", "http://www.praxair.de", "praxair_logo.png"))
        Me.sponsors.Add(New SponsorElement("STOE & Cie GmbH", "http://www.stoe.com", "stoe_logo.png"))
        Me.sponsors.Add(New SponsorElement("succidia AG", "", ""))
        Me.sponsors.Add(New SponsorElement("Südzucker", "", ""))
        Me.sponsors.Add(New SponsorElement("Thermo Fisher Scientific", "http://www.thermo.com/com/cda/landingpage/0,10255,388,00.html", "thermo_logo.png"))
        Me.sponsors.Add(New SponsorElement("Waagen-Kissling GmbH", "", ""))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SponsorsLbl.Text = ""
        Me.SponsorsLbl.Text += Me.RenderPrefix()

        Dim count As Integer = 0
        For Each se As SponsorElement In Me.sponsors
            count += 1
            Dim float As Boolean = False
            If count > 1 Then float = True
            Me.SponsorsLbl.Text += Me.RenderSponsor(se, float)
        Next

        Me.SponsorsLbl.Text += Me.RenderSuffix()

    End Sub

    Public Function RenderPrefix() As String
        Dim ausgabe As String = "<table><tr><td>" + vbCrLf

        Return ausgabe
    End Function

    Public Function RenderSponsor(ByVal se As SponsorElement, ByVal float As Boolean) As String
        Dim ausgabe As String = ""
        '<a href="http://www.netzsch-thermal-analysis.com" target="_blank" title="NETZSCH-Gerätebau GmbH"><img src="netzsch_logo.png" align="middle" alt="NETZSCH-Gerätebau GmbH" /></a></td>



        ausgabe += "<div style=""float:left;width:" + Me.largestImageWidthEM.ToString + "em;"
        'End If
        ausgabe += """>"
        ausgabe += "<table><tr><td style=""text-align:center;width:" + Me.largestImageWidthEM.ToString + "em;height:" + Me.largestImageHeightEM.ToString + "em;"">" + vbCrLf
        Dim imgText As String = se.FirmenName
        If Not String.IsNullOrEmpty(se.imgURL) Then
            imgText = "<span style=""""><img src=""" + se.imgURL + """ style=""border-style:none; border-width:0;"" align=""middle"" alt=""" + se.FirmenName + """ /></span>"
        End If

        If Not String.IsNullOrEmpty(se.wwwURL) Then
            ausgabe += "<a href=""" + se.wwwURL + """ target=""_blank"" title=""" + se.FirmenName + """>"
        End If

        ausgabe += imgText

        If Not String.IsNullOrEmpty(se.wwwURL) Then
            ausgabe += "</a>" + vbCrLf
        End If
        ausgabe += "</td></tr></table>"
        ausgabe += "</div>"

        Return ausgabe
    End Function

    Public Function RenderSuffix() As String
        Dim ausgabe As String = "</td></tr></table>"

        Return ausgabe
    End Function
End Class


