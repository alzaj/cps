Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class registr_draft_default
    Inherits GeneralWraperPage

#Region "Constants"

    'error messages
    Const C_ERROR_CHECK_REQUIRED_FIELDS As String = "* Bitte füllen Sie alle mit einem Stern (*) markierten Felder aus.<br />" + vbCrLf
    Const C_PAYMENT_STEP_ERROR_SELECT_PAYMENT As String = "* Please select one payment method<br />" + vbCrLf
    Const C_ERROR_DEPARTURE_LESS_AS_ARRIVAL As String = "* Ankunft muss vor der Abreise liegen<br />" + vbCrLf
    Const C_ERROR_EMAIL_STRING_INVALID As String = "* Die angegebene Zeichenfolge besitzt nicht das für eine E-Mail-Adresse erforderliche Format.<br />" + vbCrLf
    Const C_ERROR_VORTRAGSDAUER_INVALID As String = "* Bitte geben Sie als geplannte Vortragsdauer eine positive ganze Zahl ein.<br />" + vbCrLf
    Const C_ERROR_DEPARTURE_OR_ARRIVAL_INVALID_VALUE As String = "* Ankunft oder Abreise ungültig."

    Const C_ERROR_EMAIL_CONFIRMATION As String = "Ihre Daten wurden gespeichert, aber die Bestätigungsmail konnte nicht an die von Ihnen angegebene E-Mail-Adresse gesendet werden." '"You data was saved, but the system was unable to send confirmation e-mail to the address you specified"

    'MainStep strings
    Const C_MAIN_STEP_MESSAGE As String = "Alle mit einem Stern (*) markierten Felder sind Pflichtfelder.<br /><br />" + vbCrLf

    'PaymentStep strings
    Const C_PAYMENT_STEP_MESSAGE As String = "Select your payment method<br /><br />" + vbCrLf

    'SummaryStep strings
    Const C_SUMMARY_STEP_MESSAGE_PART1 As String = "Bitte überprüfen Sie die eingegebenen Informationen und bestätigen Sie mit der Schaltfläche ""Absenden"". <br /><br />" + vbCrLf
    Const C_SUMMARY_STEP_MESSAGE_PART2 As String = "Nach erfolgreicher Registrierung erhalten Sie eine Bestätigung mit Registrierungsnummer an die von Ihnen angegebene E-mail-Adresse.<br /><br />" + vbCrLf '"Your agreement is needed to save your data in the database created for the conference organization.<br /><br />" + vbCrLf

    Const C_SUMMARY_ERROR_NEED_AGREEMENT As String = "* Um die Anmeldeinformationen zu speichern benötigen wir Ihre Zustimmung.<br />" + vbCrLf ' "* Your agreement is needed to save your data<br />" + vbCrLf

    'E-Mail
    Const E_AGENT_NOTIFICATION_TITLE As String = "HaeKo 2011. Neuer Teilnehmer" '"HaeKo 2011 New Participant"
    Public Const E_AGENT_ERROR_NOTIFICATION As String = "<span style=""color:red;font-weight:bold;"">Bei der Anmeldung sind fehler aufgetretten!</span>"

    Public Const E_PARTICIPANT_CONFIRMATION_TITLE As String = "Ihre Registrierung für das Hemdsärmelkolloquium 2011"

    Public Const E_VARIABLE_BR As String = "[*br*]"
    'Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1 As String = "Dear participant of the Hemdsärmelkolloquium 2011," + E_VARIABLE_BR + E_VARIABLE_BR + _
    '                                                                "Your registration data was successfully saved. If you have questions please contact the organisation committee." + E_VARIABLE_BR + E_VARIABLE_BR + _
    '                                                                 "Sincerely yours," + E_VARIABLE_BR + _
    '                                                                 "The workshop organisers"
    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1 As String = "Sehr geehrte/r Teilnehmer/in des Hemdsärmelkolloquiums 2011," + E_VARIABLE_BR + E_VARIABLE_BR + _
                                                                "vielen Dank für Ihre Registrierung. Wir haben Ihre Daten mit der Registrierungsnummer #### gespeichert. Wir bitten die Sprecher, ihre Präsentationen frühzeitig auf dem Konferenzlaptop zu speichern." + E_VARIABLE_BR + E_VARIABLE_BR

    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1_1 As String = "Mit freundlichen Grüßen," + E_VARIABLE_BR + _
                                                             "Die Organisatoren des Hemdsärmelkolloquiums 2011"

    'Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2 As String = "Your e-mail address was used to register for the Hemdsärmelkolloquium 2011." + E_VARIABLE_BR + _
    '                                                                "If you don't want to register please ignore this e-mail."
    Public Const E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2 As String = "Ihre E-Mail-Adresse wurde bei der Anmeldung für das Hemdsärmelkolloquium 2011 angegeben." + E_VARIABLE_BR + _
                                                                "Wenn Sie nicht vorhatten, sich anzumelden, betrachten Sie bitte diese Nachricht als gegenstandslos."

    'Public Const C_SUCCESS_MESSAGE_PART1 As String = "You completed the registration successfully.<br /><br />A confirmation e-mail will be forwarded to the e-mail address specified in the registration form"
    'Public Const C_SUCCESS_MESSAGE_PART2 As String = "<br /><br />" + vbCrLf + _
    '                                                "<b>Note:</b> The e-mail will be sent from the domain 'cpfs.mpg.de'. Please verify that your mail box accepts such messages.<br /><br />" + vbCrLf + _
    '                                                qcnp09.E_SUPPORT_EMAIL_HTML_TEXT + vbCrLf
    Public Const C_SUCCESS_MESSAGE_PART1 As String = "Vielen Dank für Ihre Registrierung.<br /><br />" + _
                                                    "Ihre Daten wurden unter der Registrierungsnummer #### gespeichert.<br /><br />" + _
                                                    "Sie erhalten eine automatisch generierte Nachricht an die angegebene E-Mail-Adresse "
    Public Const C_SUCCESS_MESSAGE_PART2 As String = " zur Bestätigung.<br /><br />" + vbCrLf + _
                                                    "<b>Bemerkung:</b> Wenn Sie nicht innerhalb eines Arbeitstages nach der Anmeldung eine Bestätigungsmail bekommen, kontaktieren Sie bitte die Organisatoren.<br /><br />" + vbCrLf + _
                                                    qcnp09.E_SUPPORT_EMAIL_HTML_TEXT + vbCrLf



    Public Const C_REGISTRATION_CANCELED As String = "Anmeldung abgebrochen" '"Registration canceled"
    Public Const C_REGISTRATION_FAILED As String = "<b>Anmeldung fehlgeschlagen</b><br />" + vbCrLf '"<b>Registration failed</b><br />" + vbCrLf
#End Region 'Constants

#Region "Variables"
    Private _abortedClicked As Boolean = False
#End Region 'Variables

#Region "ErrorMessages"
    'Step 0
    Private RequiredFieldsErrorMsg As String = ""
    Private ArrivalDepartureErrorMsg As String = ""
    Private EmailErrorMsg As String = ""
    Private VortragsDauerErrorMsg As String = ""
#End Region 'ErrorMessages

#Region "Properties"

#End Region 'Properties

#Region "Initializations"

#End Region 'Initializations

#Region "Page Life Cycle"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Me.thisPageMustBeSecure = True
        PrintView.printViewEnabled = False

        'Check if the registration is opened
        If qcnp09.ConferenceState <> ConferenceStatus.ConferenceStates.registration_opened Then Me.Server.Transfer(qcnp09.U_INTRO_FILE)

        'Warn client about our certificate
        If Not Request.IsSecureConnection Then
            Dim urlStr As String = qcnp09.U_FORMULAR_FILE
            urlStr = urlStr.TrimStart("~")
            urlStr = urlStr.TrimStart("/")
            SSLCertificates.WarnClientIfItDontAcceptCPFSCertificate("Registration form", qcnp09.U_HOME_ABSOLUTE_URL.Replace("http://", "https://") + urlStr)
        End If
    End Sub

    Protected Sub Page_Init1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'reseting the wizard if requested (f.e. after submission or after user pressed "cancel")
        If Not Session(S_Names.S_ResertRegistrationWizard) Is Nothing AndAlso Session(S_Names.S_ResertRegistrationWizard) Then
            Session(S_Names.S_ResertRegistrationWizard) = False
            'Me.RegistrationWizard.doNotLoadViewState = True
            Response.Redirect(Me.Context.Items(C_Names.C_originalRequestedFile))
        End If

        Me.InitLabels()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set agreement request visible or not
        If MyAppSettings.AskUserForAgreement Then
            Me.AgreementPanel.Visible = True
        Else
            Me.AgreementPanel.Visible = False
        End If
    End Sub
#End Region 'Page Life Cycle

#Region "Control events"
    Sub CheckNextStep(ByVal sender As Object, ByVal e As WizardNavigationEventArgs) Handles RegistrationWizard.NextButtonClick
        If Not Page.IsValid Then
            Me.ShowError(RegistrationWizard.ActiveStepIndex)
            e.Cancel = True
            Return
        End If

        'deactivate the payment step.
        If e.CurrentStepIndex = 0 Then
            RegistrationWizard.ActiveStepIndex = 2
        End If
    End Sub

    Sub FinishWizardClicked(ByVal sender As Object, ByVal e As WizardNavigationEventArgs) Handles RegistrationWizard.FinishButtonClick
        If Not Page.IsValid OrElse Not Me.CaptchaControl1.IsValid Then
            Dim summ As String = (Me.CollectRegistrationDataFromWizard).RenderSummaryForSummaryStep
            Me.Session(S_Names.S_RegistrationSummary) = summ
            Me.SummaryLbl.Text = summ
            Me.ShowError(RegistrationWizard.ActiveStepIndex)
            e.Cancel = True
            Return
        End If
    End Sub

    Sub CancelWizardClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegistrationWizard.CancelButtonClick
        Me.allowCheckBox.Checked = False
        Me._abortedClicked = True
        RegistrationWizard.ActiveStepIndex = 3
    End Sub

    Protected Sub RegistrationWizard_ActiveStepChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegistrationWizard.ActiveStepChanged
        If RegistrationWizard.ActiveStepIndex = 2 Then
            'saving current summary for printing (see registrationsummary.aspx)
            Dim summ As String = (Me.CollectRegistrationDataFromWizard).RenderSummaryForSummaryStep
            Me.Session(S_Names.S_RegistrationSummary) = summ
            Me.SummaryLbl.Text = summ
        ElseIf RegistrationWizard.ActiveStepIndex = 3 Then
            'displaying the result
            If (Me.allowCheckBox.Checked Or Not MyAppSettings.AskUserForAgreement) And Not Me._abortedClicked Then
                Dim regdata As RegistrationData = Me.CollectRegistrationDataFromWizard

                DangerousBehavoirControl.ClientSubmitedTheRegistration()

                Dim err As String = regdata.SaveRegistrationData()
                If String.IsNullOrEmpty(err) Then
                    err = Me.EMailConfirmationToParticipant(regdata.email, Me.CreateMailForParticipant(regdata), Me.CreateMailForParticipant(regdata, True))
                End If

                If String.IsNullOrEmpty(err) Then
                    Me.ShowResultSuccess(regdata)
                Else
                    Me.ShowResultFailed(err)
                End If
                Me.EMailNotificationToAgent(Me.CreateMailForAgent(regdata, err), "HaeKo 2011: " + regdata.surname + ", " + regdata.firstname)
            Else
                Me.ShowResultAborted()
            End If
            'resetting the wizard
            'Setting the sign the wizard should be reserted
            Me.Session(S_Names.S_ResertRegistrationWizard) = True
            'disable wizards SaveViewState
            'Me.RegistrationWizard.doNotSaveViewState = True
        End If

    End Sub

    Protected Sub ClearRegistrationForm(ByVal sender As Object, ByVal e As System.EventArgs)
        'We are simply redirecting to this form
        Response.Redirect(Context.Items(C_Names.C_originalRequestedFile))
    End Sub

#End Region 'Control events

#Region "Validations"
    'Public Sub validateExternalValue(ByVal source As Object, ByVal args As ServerValidateEventArgs)
    '    args.IsValid = Not (externalDropDown.Text = "notallowed")
    'End Sub

    Public Sub validatePayment(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        args.IsValid = Me.Payment_transferRadioButton.Checked Or _
                        Me.Payment_casheRadioButton.Checked Or _
                        Me.Payment_chequeRadioButton.Checked Or _
                        Me.Payment_creditcardRadioButton.Checked
    End Sub

    Public Sub validateEmail(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        If Not String.IsNullOrEmpty(Me.emailTextBox.Text) Then
            Try
                Dim addr As New System.Net.Mail.MailAddress(Me.emailTextBox.Text)
            Catch ex As Exception
                args.IsValid = False
                Me.EmailErrorMsg = C_ERROR_EMAIL_STRING_INVALID
            End Try
        End If
    End Sub

    Public Sub validateAllowSaveData(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        If MyAppSettings.AskUserForAgreement Then
            args.IsValid = allowCheckBox.Checked
        Else
            args.IsValid = True
        End If
    End Sub

    Public Function emailValid(ByVal emailaddress As String) As Boolean
        If emailaddress.Split("@").Length = 2 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub validateArrivalDeparture(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        Dim fehler As String = ""
        If String.IsNullOrEmpty(Me.arrivalDropDown.SelectedValue) Or String.IsNullOrEmpty(Me.departureDropDown.SelectedValue) Then
            'no need to do anything: appropriate RequiredFieldValidator makes this job

            'fehler = C_ERROR_CHECK_REQUIRED_FIELDS
            'Me.RequiredFieldsErrorMsg = C_ERROR_CHECK_REQUIRED_FIELDS
        Else
            Dim arrivalParts As String() = Me.arrivalDropDown.SelectedValue.Split(".")
            Dim departureParts As String() = Me.departureDropDown.SelectedValue.Split(".")
            If arrivalParts.Length <> 3 Or departureParts.Length <> 3 Then
                fehler = C_ERROR_DEPARTURE_OR_ARRIVAL_INVALID_VALUE
            Else
                Dim arr As Date
                Dim dep As Date
                Try
                    arr = New Date(CInt(arrivalParts(2)), CInt(arrivalParts(1)), CInt(arrivalParts(0)))
                    dep = New Date(CInt(departureParts(2)), CInt(departureParts(1)), CInt(departureParts(0)))
                Catch ex As Exception
                    fehler = C_ERROR_DEPARTURE_OR_ARRIVAL_INVALID_VALUE
                End Try
                If String.IsNullOrEmpty(fehler) Then
                    If dep < arr Then fehler = C_ERROR_DEPARTURE_LESS_AS_ARRIVAL
                End If
            End If
            If Not String.IsNullOrEmpty(fehler) Then Me.ArrivalDepartureErrorMsg = fehler
        End If
        args.IsValid = (fehler = "")
    End Sub

    Public Sub validateParticipantdauer(ByVal source As Object, ByVal args As ServerValidateEventArgs)
        If String.IsNullOrEmpty(Me.participantdauerTextBox.Text) Then
            args.IsValid = True
        Else
            Try
                Dim i As Integer = CInt(Me.participantdauerTextBox.Text)
                If i >= 0 Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                    Me.VortragsDauerErrorMsg = C_ERROR_VORTRAGSDAUER_INVALID
                End If
            Catch ex As Exception
                args.IsValid = False
                Me.VortragsDauerErrorMsg = C_ERROR_VORTRAGSDAUER_INVALID
            End Try
        End If
    End Sub

    Protected Function ValidationAllRequiredFieldsHaveText() As Boolean
        Dim ausgabe As Boolean = True
        For Each v As IValidator In Me.Validators
            If TypeOf v Is System.Web.UI.WebControls.RequiredFieldValidator AndAlso Not v.IsValid Then
                ausgabe = False
                Exit For
            End If
        Next
        Return ausgabe
    End Function
#End Region 'Validations

#Region "Initializations"

    Public Sub InitLabels()
        Me.MainDataStepMsg.Text = C_MAIN_STEP_MESSAGE
        Me.PaymentStepMsg.Text = C_PAYMENT_STEP_MESSAGE
        Me.SummaryStepMsg.Text = C_SUMMARY_STEP_MESSAGE_PART1
        If MyAppSettings.AskUserForAgreement Then
            Me.SummaryStepMsg.Text += C_SUMMARY_STEP_MESSAGE_PART2
        End If
    End Sub

#End Region 'Initializations

#Region "Registration Data"
    Public Function CollectRegistrationDataFromWizard() As RegistrationData
        Dim ausgabe As New RegistrationData

        ausgabe.registrationDate = Now
        ausgabe.firstname = Me.firstnameTextBox.Text
        ausgabe.surname = Me.surnameTextBox.Text
        ausgabe.email = Me.emailTextBox.Text
        'thema = Me.themeTextBox.Text
        ausgabe.institut = Me.institutTextBox.Text
        ausgabe.titel = Me.titleDropDown.Text
        'ausgabe.status = Me.statusDropDown.Text
        'ausgabe.vegetarier = Me.vegetarienCheckBox.Checked
        ausgabe.listeeintrag = Me.listeeintragCheckBox.Checked
        ausgabe.teilnahmebestatigung = Me.teilnahmebestatigungCheckBox.Checked

        ausgabe.street = Me.streetTextBox.Text
        ausgabe.city = Me.cityTextBox.Text
        ausgabe.postalcode = Me.postalcodeTextBox.Text
        ausgabe.country = Me.countryTextBox.Text
        ausgabe.phone = Me.phoneTextBox.Text
        'ausgabe.fax = Me.faxTextBox.Text
        ausgabe.department = Me.departmenttextbox.Text
        ausgabe.gender = Me.gendertextbox.Text

        'ausgabe.poster = Me.posterDropDown.Text
        ausgabe.bemerkungenparticipant = Me.bemerkungenparticipantTextBox.Text

        Try
            ausgabe.participantdauer = CInt(Me.participantdauerTextBox.Text)
        Catch ex As Exception
        End Try

        ausgabe.participanttermin = Me.participantterminDropDown.SelectedValue

        Try
            ausgabe.arrival = CDate(Me.arrivalDropDown.SelectedValue)
        Catch ex As Exception
        End Try

        Try
            ausgabe.departure = CDate(Me.departureDropDown.SelectedValue)
        Catch ex As Exception
        End Try

        'ausgabe.roomneighbour = Me.roomneighbourTextBox.Text
        'ausgabe.external = Me.externalDropDown.Text
        'If ausgabe.external = RegistrationData.EXTERNAL_EXTERNAL Then
        '    ausgabe.fee = qcnp09.P_REGISTR_FEE_BEFORE_GRENZ_DATUM

        '    If Me.Payment_casheRadioButton.Checked Then
        '        ausgabe.paymenttype = "cache"
        '    ElseIf Me.Payment_chequeRadioButton.Checked Then
        '        ausgabe.paymenttype = "cheque"
        '    ElseIf Me.Payment_creditcardRadioButton.Checked Then
        '        ausgabe.paymenttype = "creditcard"
        '    ElseIf Me.Payment_transferRadioButton.Checked Then
        '        ausgabe.paymenttype = "transfer"
        '    Else
        '        ausgabe.paymenttype = ""
        '    End If
        'End If

        Return ausgabe
    End Function
#End Region 'Registration Data

#Region "Helper Functions"
    Function makeRedLabel(ByVal text As String) As String
        Return "<span style=""color:red;"">" + text + "</span>"
    End Function

    Public Sub ShowError(ByVal stepIndex As Integer)
        If stepIndex = 0 Then
            Me.MainDataStepMsg.Text = ""
            If Not ValidationAllRequiredFieldsHaveText() Then
                Me.MainDataStepMsg.Text = C_ERROR_CHECK_REQUIRED_FIELDS
            End If
            If Not Me.arrivalCustomValidator.IsValid Or Not Me.departureCustomValidator.IsValid Then Me.MainDataStepMsg.Text += ArrivalDepartureErrorMsg
            If Not Me.emailCustomValidator.IsValid Then Me.MainDataStepMsg.Text += EmailErrorMsg
            If Not Me.participantdauerCustomValidator.IsValid Then Me.MainDataStepMsg.Text += Me.VortragsDauerErrorMsg
            Me.MainDataStepMsg.Text = Me.makeRedLabel(Me.MainDataStepMsg.Text + "<br />" + vbCrLf)
        ElseIf stepIndex = 1 Then
            Me.PaymentStepMsg.Text = C_PAYMENT_STEP_ERROR_SELECT_PAYMENT
            'If Not Me.CustomValidator2.IsValid Then Me.PaymentStepMsg.Text += C_ERROR_DONT_SELECT
            Me.PaymentStepMsg.Text = Me.makeRedLabel(Me.PaymentStepMsg.Text + "<br />" + vbCrLf)
        ElseIf stepIndex = 2 Then
            Me.SummaryStepMsg.Text = ""

            If Not Me.CustomValidator3.IsValid Then
                Me.SummaryStepMsg.Text += C_SUMMARY_ERROR_NEED_AGREEMENT
            End If

            If Not Me.CaptchaControl1.IsValid Then
                Me.SummaryStepMsg.Text += CaptchaControl1.ErrorMessage + "<br />" + vbCrLf
            End If

            If Me.SummaryStepMsg.Text <> "" Then
                Me.SummaryStepMsg.Text = Me.makeRedLabel(Me.SummaryStepMsg.Text + "<br />" + vbCrLf)
            End If
        End If
    End Sub

    Public Sub ShowResultSuccess(ByVal regdata As RegistrationData)
        Me.completeLbl.Text = C_SUCCESS_MESSAGE_PART1.Replace("####", regdata.participantsID.ToString)
        Me.completeLbl.Text += "(" + regdata.email + ")"
        Me.completeLbl.Text += C_SUCCESS_MESSAGE_PART2
    End Sub

    Public Sub ShowResultFailed(ByVal errors As String)
        Me.completeLbl.Text = C_REGISTRATION_FAILED
        If Not String.IsNullOrEmpty(errors) Then
            Me.completeLbl.Text += "Error description:<br />" + vbCrLf + errors + "<br />" + vbCrLf
        End If
        Me.completeLbl.Text += "<br />" + vbCrLf + qcnp09.E_SUPPORT_EMAIL_HTML_TEXT
    End Sub

    Public Sub ShowResultAborted()
        Me.completeLbl.Text = C_REGISTRATION_CANCELED
    End Sub
#End Region 'Helper Functions

#Region "E-Mail"
    ''' <summary>
    ''' Sends confirmation to the participants e-mail. If error occured - returns error description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function EMailConfirmationToParticipant(ByVal participantemail As String, ByVal htmltext As String, ByVal plaintext As String) As String
        Dim ausgabe As String = ""

        Try
            Dim msgtitel As String = E_PARTICIPANT_CONFIRMATION_TITLE

            Dim _mail As New MailMessage
            _mail.From = New MailAddress(MyAppSettings.FromEmailForThisApplication)
            _mail.To.Add(participantemail)
            _mail.ReplyTo = New MailAddress(qcnp09.E_SUPPORT_EMAIL)
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
            Dim errReport As String = Now.ToString("MMMM dd, yyyy HH:mm:ss", New System.Globalization.CultureInfo("de-DE")) + "<br />" + vbCrLf
            errReport += C_ERROR_EMAIL_CONFIRMATION + "(" + participantemail + ").<br />" + vbCrLf
            errReport += ex.Message + "<br />" + vbCrLf

            If Not DebugHelper.IsDebugMode Then
                ausgabe = C_ERROR_EMAIL_CONFIRMATION + "(" + participantemail + ")."
            Else
                ausgabe = errReport
            End If
            ErrorReporting.ReportErrorToWebmaster(errReport)
        End Try

        Return ausgabe
    End Function

    Private Function CreateMailForParticipant(ByVal regdata As RegistrationData, Optional ByVal needPlainText As Boolean = False) As String
        Dim ausgabe As String = ""
        Dim br As String = "<br />" + vbCrLf
        If needPlainText Then br = vbCrLf

        Dim idStr As String = regdata.memberpin
        If Not needPlainText Then
            idStr = "<b>" + idStr + "</b>"
        End If

        ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1.Replace(E_VARIABLE_BR, br).Replace("####", regdata.participantsID.ToString)

        'ausgabe += regdata.RenderSummaryForSummaryStep(needPlainText)

        'ausgabe += br

        ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART1_1.Replace(E_VARIABLE_BR, br)

        ausgabe += br + br

        ausgabe += "Web: "
        If Not needPlainText Then
            ausgabe += "<a href=""" + qcnp09.U_HOME_ABSOLUTE_URL + """>"
        End If

        ausgabe += qcnp09.U_HOME_ABSOLUTE_URL

        If Not needPlainText Then
            ausgabe += "</a>"
        End If


        ausgabe += br + qcnp09.E_SUPPORT_EMAIL_PREFIX '"support e-mail: "
        If Not needPlainText Then
            ausgabe += "<a href=""mailto:" + qcnp09.E_SUPPORT_EMAIL + """>"
        End If

        ausgabe += qcnp09.E_SUPPORT_EMAIL

        If Not needPlainText Then
            ausgabe += "</a>"
        End If

        'ausgabe += br + br

        'ausgabe += E_PARTICIPANT_CONFIRMATION_EMAIL_INTRO_PART2.Replace(E_VARIABLE_BR, br)

        Return ausgabe
    End Function

    ''' <summary>
    ''' Sends notification of registering new participants to all agent e-mails. If error occured - returns error description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function EMailNotificationToAgent(ByVal emailtext As String, Optional ByVal emailCaption As String = "") As String
        Dim ausgabe As String = ""

        Dim msgtitel As String = emailCaption
        If msgtitel = "" Then msgtitel = E_AGENT_NOTIFICATION_TITLE

        Dim targetEmails As String = qcnp09.E_AGENT_EMAILS.Replace(",", ";")
        Dim emails As String() = targetEmails.Split(";")
        For Each e As String In emails
            'überprüfen die targetEmail auf gültigkeit
            Dim targetTeile As String() = e.Split("@")
            If targetTeile.Length <> 2 Then
                ErrorReporting.ReportErrorToWebmaster("Agent-Email '" + e + "' is invalid")
                Continue For
            End If

            Dim _mail As New MailMessage
            _mail.From = New MailAddress(MyAppSettings.FromEmailForThisApplication)
            _mail.To.Add(e)
            _mail.Subject = msgtitel
            _mail.BodyEncoding = System.Text.Encoding.UTF8
            _mail.Body = emailtext
            _mail.IsBodyHtml = True

            Dim smtp As New SmtpClient(MyAppSettings.SmtpServerName, 587)
            smtp.UseDefaultCredentials = False
            Dim creds As New System.Net.NetworkCredential(MyAppSettings.FromEmailUserName, MyAppSettings.FromEmailPassword)
            smtp.Credentials = creds
            smtp.Send(_mail)
        Next
        Return True

        Return ausgabe
    End Function

    Private Function CreateMailForAgent(ByVal regdata As RegistrationData, ByVal errors As String) As String

        Dim ausgabe As String = "<h2>" + E_AGENT_NOTIFICATION_TITLE + "</h2>" + vbCrLf

        If Not String.IsNullOrEmpty(errors) Then
            ausgabe += E_AGENT_ERROR_NOTIFICATION + "<br />" + vbCrLf
            ausgabe += errors + "<br /><br />" + vbCrLf
        End If

        ausgabe += regdata.RenderSummaryForSummaryStep

        ausgabe += "<br />" + vbCrLf

        Return ausgabe
    End Function


#End Region
End Class


