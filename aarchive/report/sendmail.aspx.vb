Imports System.Net.Mail

Partial Class report_sendmail
    Inherits GeneralWraperPage

#Region "Strings"
    Public Const E_TITLE As String = "Hemdsärmelkolloquium 2011 in Dresden"
    Public Const E_VARIABLE_BR As String = "[*br*]"

    'durch komma getrennte email adressen
    '"ehp.abicht@gmx.de,albert@ac.chemie.tu-darmstadt.de,baernighausen@kit.edu,j.beck@uni-bonn.de,hp.beck@mx.uni-saarland.de,behrens@fhi-berlin.mpg.de,peter.behrens@acb.uni-hannover.de,wbensch@ac.uni-kiel.de,michael.binnewies@aca.uni-hannover.de,michael.braeu@basf.com,josef.breu@uni-bayreuth.de,wolfgang.buettner@bruker.axs.de,dehnen@chemie.uni-marburg.de,deiseroth@chemie.uni-siegen.de,wd@min.uni-kiel.de,r.dinnebier@fkf.mpg.de,thomas.doert@chemie.tu-dresden.de,k.doll@fkf.mpg.de,drons@HAL9000.ac.rwth-aachen.de,stefan.ebbinghaus@chemie.uni-halle.de,Manuela.Donaubauer@lrz.tum.de,claus.feldmann@kit.edu,felser@uni-mainz.de,dieter.fenske@kit.edu,boniface.fokwa@ac.rwth-aachen.de,froeba@chemie.uni-hamburg.de,ekkehard.fueglein@netzsch.com,gerhard.gille@hcstarck.com,rglaum@uni-bonn.de,grin@cpfs.mpg.de,guetlich@uni-mainz.de,frank.haarmann@ac.rwth-aachen.de,ZAAC@wiley-vch.de,harbrecht@chemie.uni-marburg.de,hartenbach@iac.uni-stuttgart.de,bernhard.hettich@cht.com,hey@uni-leipzig.de,Harald.hillebrecht@ac.uni-freiburg.de,hoenle@cpfs.mpg.de,hoppe@anorg.chemie-giessen.de,hubert.huppertz@uibk.ac.at,herbert.jacobs@uni-dortmund.de,juergen.janek@phys.chemie.uni-giessen.de,m.jansen@fkf.mpg.de,jeitsch@uni-muenster.de,johrendt@lmu.de,ac013@uni-koeln.de,stefan.kaskel@chemie.tu-dresden.de,hans-lothar.keller@tu-dortmund.de,lk@tf.uni-kiel.de,kleinke@uwaterloo.ca,kniep@cpfs.mpg.de,Martin.Koeckerling@uni-rostock.de,paul.koegerler@ac.rwth-aachen.de,j.koehler@fkf.mpg.de,h.kohlmann@mx.uni-saarland.de,nikolaus.korber@chemie.uni-regensburg.de,krautscheid@rz.uni-leipzig.de,krebs@uni-muenster.de,kreiner@cpfs.mpg.de,rekre@fkf.mpg.de,edwin.kroke@chemie.tu-freiberg.de,krumeich@inorg.chem.ethz.ch,a.leineweber@mf.mpg.de,stefano.leoni@chemie.tu-dresden.de,lerch@chem.tu-berlin.de,mader@uni-bonn.de,s.weiglein@fkf.mpg.de,hj.mattausch@fkf.mpg.de,mattes@uni-muenster.de,gerd.meyer@uni-koeln.de,juergen.meyer@uni-tuebingen.de,amoeller@uh.edu,anja.mudring@rub.de,mueller@chemie.uni-marburg.de,mbsenior@t-online.de,k.mueller-buschbaum@uni-wuerzburg.de,cnaether@ac.uni-kiel.de,nesper@inorg.chem.ethz.ch,rainer.niewa@iac.uni-stuttgart.de,tom.nilges@lrz.tu-muenchen.de,j.nuss@fkf.mpg.de,oliver.oeckler@gmx.de,panthoef@uni-mainz.de,greta.patzke@aci.uzh.ch,beate@chemie.fu-berlin.de,arno.pfitzner@chemie.uni-regensburg.de,pottgen@uni-muenster.de,armin.reller@physik.uni-augsburg.de,rentschler@uni-mainz.de,riedel@materials.tu-darmstadt.de,caroline@ruby.chemie.uni-freiburg.de,frank.rosowski@basf.com,michael.ruck@tu-dresden.de,uwe.ruschewitz@uni-koeln.de,sabine.schlecht@anorg.chemie.uni-giessen.de,Schleid@iac.uni-stuttgart.de,peer.schmidt@chemie.tu-dresden.de,wolfgang.schnick@uni-muenchen.de,christoph.schnitter@hcstarck.com,marcos.schoeneborn@de.sasol.com,schoen@fkf.mpg.de,thorbjoern.schoenbeck@panalytical.com,schwarz@cpfs.mpg.de,juergen.senker@uni-bayreuth.de,sieler@uni-leipzig.de,a.simon@fkf.mpg.de,k.stoewe@mx.uni-saarland.de,tremel@uni-mainz.de,sturba@wiley.com,G.Vajenine@fkf.mpg.de,Hansbernhard.wagner@t-online.de,u.wedig@fkf.mpg.de,anke.weidenkaff@empa.ch,richard.weihrich@uni-ulm.de,mweil@mail.zserv.tuwien.ac.at,mathias.wickleder@uni-oldenburg.de,wickleder@chemie.uni-siegen.de,wosylus@mpi-muelheim.mpg.de,zahn@cpfs.mpg.de,Katrin.Demian@cpfs.mpg.de,Kay.Pollex@cpfs.mpg.de,Alexander.Zajcev@cpfs.mpg.de"
    Public Const E_MAILS As String = "antipov@icr.chem.msu.ru,Sven.Lidin@polymat.lth.se,gnolas@cas.usf.edu,sponou@gmail.com,ssevov@nd.edu,Katrin.Demian@cpfs.mpg.de"

    '  Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1 As String = _
    '      "Liebe Kolleginnen und Kollegen," + E_VARIABLE_BR + _
    '"liebe Freunde des Hemdsärmelkolloquiums," + E_VARIABLE_BR + E_VARIABLE_BR + _
    '      "wie auch in den vergangenen Jahren findet das Hemdsärmelkolloquium 2011 unmittelbar vor der Chemiedozententagung statt. Als Veranstaltungsort wurde Dresden ausgewählt." + E_VARIABLE_BR + E_VARIABLE_BR + _
    '"Die Organisatoren (Juri Grin, Stefan Kaskel, Rüdiger Kniep und Michael Ruck) haben nach anfänglichen Problemen schließlich doch noch geeignete Räumlichkeiten gefunden, so dass Sie nun und auf diesem Wege unsere herzliche Einladung zur Teilnahme erhalten." + E_VARIABLE_BR + E_VARIABLE_BR

    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1 As String = _
    "Dear colleagues," + E_VARIABLE_BR + _
    "Dear friends of the Hemdsärmelkolloquium," + E_VARIABLE_BR + E_VARIABLE_BR + _
    "As in the years before the Hemdsärmelkolloquium 2011 takes place immediately prior to the Chemiedozententagung. Dresden was chosen as venue." + E_VARIABLE_BR + E_VARIABLE_BR + _
    "The organizers (Juri Grin, Stefan Kaskel, Rüdiger Kniep and Michael Ruck) are happy to invite you to participate in the Colloquium. Please note that the conference language is German." + E_VARIABLE_BR + E_VARIABLE_BR

    '  Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2 As String = _
    '      E_VARIABLE_BR + "Beginn: Do, 10. März um 13:00 Uhr" + E_VARIABLE_BR + _
    '"Ende: Sa, 12. März gegen 13:00 Uhr" + E_VARIABLE_BR + _
    '"Ort: Dreikönigskirche Dresden" + E_VARIABLE_BR + E_VARIABLE_BR + E_VARIABLE_BR + _
    '"Für das Kolloquium haben wir eine Homepage eingerichtet:" + E_VARIABLE_BR + E_VARIABLE_BR

    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2 As String = _
    E_VARIABLE_BR + "Begin: Thursday, March 10, 2011 at 13:00 p.m." + E_VARIABLE_BR + _
 "End: Saturday, March 12, 2011 at 13:00 p.m." + E_VARIABLE_BR + _
 "Venue: Dreikönigskirche Dresden" + E_VARIABLE_BR + E_VARIABLE_BR + E_VARIABLE_BR + _
 "We have set up a website for the Colloquium:" + E_VARIABLE_BR + E_VARIABLE_BR

    'Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART3 As String = _
    '       E_VARIABLE_BR + E_VARIABLE_BR + "Dort finden Sie alle notwendigen Links (Anmeldung, Übernachtungsmöglichkeiten, Anreise, vorläufiger Zeitplan, Sponsoren etc.). Wir bitten Sie, Ihre Anmeldungen bis zum 11. Februar 2011 vorzunehmen. Nach Möglichkeit sollte Ihre Redezeit auf maximal 10 Minuten beschränkt sein, damit auch ausreichend diskutiert werden kann." + E_VARIABLE_BR + E_VARIABLE_BR + _
    ' "Wir freuen uns auf Ihr Kommen und verbleiben mit allen guten Wünschen für das bevorstehende Weihnachtsfest und das neue Jahr" + E_VARIABLE_BR + _
    ' "Ihr" + E_VARIABLE_BR + _
    ' "Dresdner Organisationskollegium" + E_VARIABLE_BR + E_VARIABLE_BR

    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART3 As String = _
     E_VARIABLE_BR + E_VARIABLE_BR + "You will find all necessary information (Registration, Accommodation, Directions, Program, Sponsors etc.) there. Please register until February 11, 2011. Please limit your presentation time to 10 minutes if possible, so that there is enough time for discussion." + E_VARIABLE_BR + E_VARIABLE_BR + _
     "We look forward to meet you in Dresden und remain with all good wishes for the upcoming Christmas season and the New Year," + E_VARIABLE_BR + _
     "The organizers" + E_VARIABLE_BR + E_VARIABLE_BR
#End Region



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.message.Text = "" ' Me.CreateMailForParticipant
        Dim mails As String() = E_MAILS.Split(",")
        For Each em As String In mails
            If Not Me.EmailIsValid(em.Trim) Then
                Me.message.Text += vbCrLf + "<br>E-Mail invalid: " + em
                Continue For
            End If
            Dim err As String = Me.SendMail(em.Trim, Me.CreateMailForParticipant, Me.CreateMailForParticipant(True))
            If String.IsNullOrEmpty(err) Then
                Me.message.Text += "<br>" + vbCrLf + "OK an: " + em
            Else
                Me.message.Text += "<br>" + vbCrLf + "Error Sending an " + em + ": " + err
            End If

        Next
    End Sub


#Region "E-Mail"
    ''' <summary>
    ''' Sends e-mail an on address. If error occured - returns error description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SendMail(ByVal toMail As String, ByVal htmltext As String, ByVal plaintext As String) As String
        Dim ausgabe As String = ""

        Try
            Dim msgtitel As String = E_TITLE

            Dim _mail As New MailMessage
            _mail.From = New MailAddress(MyAppSettings.FromEmailForThisApplication)
            _mail.To.Add(toMail)
            _mail.ReplyTo = New MailAddress(MyAppSettings.FromEmailForThisApplication)
            _mail.Subject = msgtitel
            _mail.BodyEncoding = System.Text.Encoding.UTF8
            _mail.IsBodyHtml = True

            'HTML version
            If htmltext <> "" Then
                Dim htmlType As New System.Net.Mime.ContentType("text/html")
                Dim htmlBody As AlternateView = AlternateView.CreateAlternateViewFromString(htmltext, htmlType)
                _mail.AlternateViews.Add(htmlBody)
            End If

            'Text version
            If plaintext <> "" Then
                Dim txtBody As AlternateView = AlternateView.CreateAlternateViewFromString(plaintext, New System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain))
                _mail.AlternateViews.Add(txtBody)
            End If

            Dim smtp As New SmtpClient(MyAppSettings.SmtpServerName, 587)
            smtp.UseDefaultCredentials = False
            Dim creds As New System.Net.NetworkCredential(MyAppSettings.FromEmailUserName, MyAppSettings.FromEmailPassword)
            smtp.Credentials = creds
            smtp.Send(_mail)
        Catch ex As Exception
            ausgabe = vbCrLf + "<br />" + vbCrLf + "Error: " + ex.Message
        End Try

        Return ausgabe
    End Function

    Private Function CreateMailForParticipant(Optional ByVal needPlainText As Boolean = False) As String
        Dim ausgabe As String = ""
        Dim br As String = "<br>" + vbCrLf
        If needPlainText Then br = vbCrLf

        If Not needPlainText Then
            ausgabe += "<!DOCTYPE html PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">" + vbcrlf
            ausgabe += "<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">" + vbcrlf
            ausgabe += "</HEAD><BODY><FONT style=""font-size:11.0pt;font-family:Arial"">" + vbCrLf
        End If

        ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1.Replace(E_VARIABLE_BR, br)

        If Not needPlainText Then
            ausgabe += "<u>"
        End If

        ausgabe += "Details"

        If Not needPlainText Then
            ausgabe += "</u>"
        End If

        ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2.Replace(E_VARIABLE_BR, br)

        If Not needPlainText Then
            ausgabe += "<a href=""" + qcnp09.U_HOME_ABSOLUTE_URL + """>"
        End If

        ausgabe += qcnp09.U_HOME_ABSOLUTE_URL

        If Not needPlainText Then
            ausgabe += "</a>"
        End If

        ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART3.Replace(E_VARIABLE_BR, br)

        If Not needPlainText Then
            ausgabe += "</FONT></BODY></HTML>"
        End If

        Return ausgabe
    End Function

    Public Function EmailIsValid(ByVal emailStr As String) As Boolean
        Dim ausgabe As Boolean = False
        If Not String.IsNullOrEmpty(emailStr) Then
            Try
                Dim addr As New System.Net.Mail.MailAddress(emailStr)
                ausgabe = True
            Catch ex As Exception
                ausgabe = False
            End Try
        End If
        Return ausgabe
    End Function

#End Region
End Class
