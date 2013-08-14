Imports System.Data.SqlClient

Public Class RegistrationData

#Region "Constants"

#End Region 'Constants

    Public participantsID As Integer = 0
    Public workshopid As String = "2"
    Public memberpin As String = ""

    Public firstname As String = ""
    Public surname As String = ""
    Public gender As String = "" 'anrede
    Public titel As String = ""

    'Address
    Public institut As String = "" 'post_pref1
    Public department As String = "" 'post_pref2
    Public street As String = ""
    Public postalcode As String = ""
    Public city As String = ""
    Public country As String = ""
    Public phone As String = ""
    'Public fax As String = ""
    Public email As String = ""

    'Conference context
    'Public vegetarier As Boolean = False
    Public registrationDate As New Nullable(Of Date)

    Public arrival As New Nullable(Of Date)
    Public departure As New Nullable(Of Date)
    'Public paymenttype As String = ""
    Public listeeintrag As Boolean = True
    Public teilnahmebestatigung As Boolean = True
    Public bemerkungenparticipant As String = ""
    Public participantdauer As Integer = 0
    Public participanttermin As String = ""

    'Contributions table
    'Public poster As String = ""

    'status replacements
    'Public status As String = ""
    'Public external As String = ""
    'Public roomneighbour As String = ""

    'services
    'Public fee As Decimal = 0


#Region "Constants"
    Public Const EXTERNAL_EXTERNAL As String = "external"
    Public Const EXTERNAL_CMAC As String = "cmac"
    Public Const EXTERNAL_KEYNOTE As String = "keynote"
    Public Const EXTERNAL_INVITED As String = "invited"

    Public Const PAYMENT_BANK_TRANSFER As String = "transfer"
    Public Const PAYMENT_CHEQUE As String = "cheque"
    Public Const PAYMENT_CREDITCARD As String = "creditcard"
    Public Const PAYMENT_CACHE As String = "cache"

#End Region 'Constants

#Region "Rendering"
    Public Function RenderSummaryForSummaryStep(Optional ByVal renderPlainText As Boolean = False) As String
        Dim ausgabe As String = ""

        If Not renderPlainText Then
            ausgabe = "<table class=""toptable"">" + vbCrLf
        End If

        If Me.registrationDate.HasValue Then
            ausgabe += RenderRowForSummaryStep("Anmeldedatum", Me.registrationDate.Value.ToString("dd. MMMM yyyy", qcnp09.D_DATUM_FORMAT), , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.titel) Or Not String.IsNullOrEmpty(Me.gender) Then
            If Not String.IsNullOrEmpty(Me.gender) Then
                Me.gender += "&nbsp;"
            End If
            ausgabe += Me.RenderRowForSummaryStep("Titel", Me.gender.ToString + Me.titel.ToString, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.surname) Then
            ausgabe += Me.RenderRowForSummaryStep("Nachname", Me.surname, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.firstname) Then
            ausgabe += Me.RenderRowForSummaryStep("Vorname", Me.firstname, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.institut) Then
            ausgabe += Me.RenderRowForSummaryStep("Institution", Me.institut, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.department) Then
            ausgabe += Me.RenderRowForSummaryStep("Abteilung/Institut", Me.department, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.street) Then
            ausgabe += Me.RenderRowForSummaryStep("Straße", Me.street, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.postalcode) Then
            ausgabe += Me.RenderRowForSummaryStep("Postleitzahl", Me.postalcode, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.city) Then
            ausgabe += Me.RenderRowForSummaryStep("Ort", Me.city, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.country) Then
            ausgabe += Me.RenderRowForSummaryStep("Land", Me.country, , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.phone) Then
            ausgabe += Me.RenderRowForSummaryStep("Telefon", Me.phone, , renderPlainText)
        End If

        'If Not String.IsNullOrEmpty(Me.fax) Then
        '    ausgabe += Me.RenderRowForSummaryStep("Fax", Me.fax, , renderPlainText)
        'End If

        If Not String.IsNullOrEmpty(Me.email) Then
            ausgabe += Me.RenderRowForSummaryStep("E-Mail", Me.email, , renderPlainText)
        End If

        'If Not String.IsNullOrEmpty(Me.status) Then
        '    ausgabe += Me.RenderRowForSummaryStep("Status", Me.status, , renderPlainText)
        'Else
        '    ausgabe += Me.RenderRowForSummaryStep("Status", "", True, renderPlainText)
        'End If

        'If Me.vegetarier Then
        '    ausgabe += Me.RenderRowForSummaryStep("Vegetarian", "Yes", , renderPlainText)
        'Else

        If Me.listeeintrag Then
            ausgabe += Me.RenderRowForSummaryStep("Eintrag in Teilnehmerliste", "Ja", , renderPlainText)
        Else
            ausgabe += Me.RenderRowForSummaryStep("Eintrag in Teilnehmerliste", "Nein", , renderPlainText)
        End If

        If Me.teilnahmebestatigung Then
            ausgabe += Me.RenderRowForSummaryStep("Teilnahmebestätigung Erwünscht", "Ja", , renderPlainText)
        Else
            ausgabe += Me.RenderRowForSummaryStep("Teilnahmebestätigung Erwünscht", "Nein", , renderPlainText)
        End If

        'If Not String.IsNullOrEmpty(Me.poster) Then
        '    ausgabe += Me.RenderRowForSummaryStep("Contribution", Me.poster, , renderPlainText)
        'Else
        '    ausgabe += Me.RenderRowForSummaryStep("Contribution", "", True, renderPlainText)
        'End If

        If Me.arrival.HasValue Then
            ausgabe += Me.RenderRowForSummaryStep("Ankunft", Me.arrival.Value.ToString("dd. MMMM yyyy", qcnp09.D_DATUM_FORMAT), , renderPlainText)
        End If

        If Me.departure.HasValue Then
            ausgabe += Me.RenderRowForSummaryStep("Abreise", Me.departure.Value.ToString("dd. MMMM yyyy", qcnp09.D_DATUM_FORMAT), , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.bemerkungenparticipant) Then
            'render the text
            If renderPlainText Then
                ausgabe += Me.RenderRowForSummaryStep("geplanter Vortragstitel", GlobFunctions.TextConvertTexToPlain(Me.bemerkungenparticipant), , renderPlainText)
            Else
                ausgabe += Me.RenderRowForSummaryStep("geplanter Vortragstitel", GlobFunctions.TextConvertTexToHtml(Me.bemerkungenparticipant), , renderPlainText)
            End If
        End If

        If Me.participantdauer > 0 Then
            ausgabe += Me.RenderRowForSummaryStep("geplante Vortragsdauer", Me.participantdauer.ToString + " Min.", , renderPlainText)
        End If

        If Not String.IsNullOrEmpty(Me.participanttermin) Then
            ausgabe += Me.RenderRowForSummaryStep("geplanter Vortragstermin", Me.participanttermin, , renderPlainText)
        End If

        'Dim externl As String = ""
        'Select Case external
        '    Case RegistrationData.EXTERNAL_CMAC
        '        externl = "Member of C-MAC"
        '    Case RegistrationData.EXTERNAL_EXTERNAL
        '        externl = "External participant"
        '    Case RegistrationData.EXTERNAL_KEYNOTE
        '        externl = "Keynote Speaker"
        '    Case RegistrationData.EXTERNAL_INVITED
        '        externl = "Invited Speaker"
        'End Select
        'ausgabe += Me.RenderRowForSummaryStep("Reference to C-MAC", externl, , renderPlainText)


        'If Me.fee > 0 Then
        '    ausgabe += Me.RenderRowForSummaryStep("Registration fee", Me.fee.ToString("F2", qcnp09.P_CURRENCY_FORMAT) + "€", , renderPlainText)
        'End If

        'Dim paymnt As String = ""
        'Select Case paymenttype
        '    Case RegistrationData.PAYMENT_BANK_TRANSFER
        '        paymnt = "Bank transfer"
        '    Case RegistrationData.PAYMENT_CACHE
        '        paymnt = "Cache"
        '    Case RegistrationData.PAYMENT_CHEQUE
        '        paymnt = "Cheque"
        '    Case RegistrationData.PAYMENT_CREDITCARD
        '        paymnt = "Credit card"
        'End Select
        'If Not String.IsNullOrEmpty(paymnt) Then ausgabe += RenderRowForSummaryStep("Payment method", paymnt, , renderPlainText)

        If Not renderPlainText Then
            ausgabe += "</table>" + vbCrLf
        End If

        Return ausgabe
    End Function

    Public Function RenderRowForSummaryStep(ByVal description As String, ByVal value As String, Optional ByVal renderEmptyValue As Boolean = False, Optional ByVal renderPlainText As Boolean = False) As String
        If Not renderEmptyValue And String.IsNullOrEmpty(value) Then Return ""
        Dim ausgabe As String = ""

        If Not renderPlainText Then
            ausgabe = "<tr><td>" + description + "&nbsp;</td><td><b>" + value + "</b></td></tr>" + vbCrLf
        Else
            ausgabe = description + ": " + value + vbCrLf + vbCrLf
        End If

        Return ausgabe
    End Function
#End Region 'Rendering

#Region "DBOperations"
    ''' <summary>
    ''' Saves the registration Data to your store(f.e Database) and sends confirmation e-mail to participant and notification e-mail to agent.
    ''' If saving failed - returns error description as string.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRegistrationData() As String
        Dim ausgabe As String = ""

        Dim randomNr As String = "o00oo00"
        Dim nrOk As Boolean = False
        Dim c As Integer = 0
        Do
            randomNr = Me.GenerateRandomNumber
            Try
                nrOk = Not qcnp09.IdIstVergeben(randomNr)
            Catch ex As Exception
                Dim errReport As String = Now.ToString("MMMM dd, yyyy HH:mm:ss", New System.Globalization.CultureInfo("de-DE")) + "<br />" + vbCrLf
                errReport += qcnp09.STR_GENERIC_DB_ERROR + "<br />" + vbCrLf
                errReport += ex.Message + "<br />" + vbCrLf

                If Not DebugHelper.IsDebugMode Then
                    ausgabe = qcnp09.STR_GENERIC_DB_ERROR
                Else
                    ausgabe = errReport
                End If
                ErrorReporting.ReportErrorToWebmaster(errReport)
                Return ausgabe
            End Try
            c += 1
        Loop Until nrOk Or c > 1000
        If c = 1001 Then
            Dim errReport As String = Now.ToString("MMMM dd, yyyy HH:mm:ss", New System.Globalization.CultureInfo("de-DE")) + "<br />" + vbCrLf
            errReport += qcnp09.STR_GENERIC_DB_ERROR + "<br />" + vbCrLf
            errReport += "Error: file " + System.Web.HttpContext.Current.Request.PhysicalPath + ", Function SaveFormular() - endlose Schleife" + "<br />" + vbCrLf

            If Not DebugHelper.IsDebugMode Then
                ausgabe = qcnp09.STR_GENERIC_DB_ERROR
            Else
                ausgabe = errReport
            End If
            ErrorReporting.ReportErrorToWebmaster(errReport)
            Return ausgabe
        End If

        Dim sqlStr As String
        sqlStr = "INSERT INTO temp_participants (" _
                                + "[memberpin],[workshopsid],[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[email],[listeeintrag],[teilnahmebestatigung],[registrationdate],[arrival],[departure],[vegetarier],[bemerkungenparticipant],[participantdauer],[participanttermin]) " + _
                                "VALUES ('" + randomNr + "',@workshopsid,@firstname,@surname,@gender,@titel,@institut,@department,@street,@postalcode,@city,@country,@phone,@email,@listeeintrag,@teilnahmebestatigung,@registrationdate,@arrival,@departure,1,@bemerkungenparticipant,@participantdauer,@participanttermin) SELECT SCOPE_IDENTITY()"

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)
            Try
                conn.Open()
                Dim befehl As New SqlCommand(sqlStr)
                befehl.Parameters.Add("@parparticipantsID", System.Data.SqlDbType.Int)
                befehl.Parameters("@parparticipantsID").Direction = System.Data.ParameterDirection.Output

                befehl.Parameters.Add("@workshopsid", System.Data.SqlDbType.Int)
                befehl.Parameters("@workshopsid").Value = GlobFunctions.CurrentWorkshopID
                befehl.Parameters.Add("@firstname", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@firstname").Value = Me.firstname
                befehl.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@surname").Value = Me.surname
                befehl.Parameters.Add("@gender", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@gender").Value = Me.gender
                befehl.Parameters.Add("@titel", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@titel").Value = Me.titel
                befehl.Parameters.Add("@institut", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@institut").Value = Me.institut
                befehl.Parameters.Add("@department", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@department").Value = Me.department
                befehl.Parameters.Add("@street", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@street").Value = Me.street
                befehl.Parameters.Add("@postalcode", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@postalcode").Value = Me.postalcode
                befehl.Parameters.Add("@city", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@city").Value = Me.city
                befehl.Parameters.Add("@country", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@country").Value = Me.country
                befehl.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@phone").Value = Me.phone
                'befehl.Parameters.Add("@fax", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@fax").Value = Me.fax
                befehl.Parameters.Add("@email", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@email").Value = Me.email

                'befehl.Parameters.Add("@vegetarier", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@vegetarier").Value = Me.vegetarier

                befehl.Parameters.Add("@listeeintrag", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@listeeintrag").Value = Me.listeeintrag

                befehl.Parameters.Add("@teilnahmebestatigung", System.Data.SqlDbType.NVarChar)
                befehl.Parameters("@teilnahmebestatigung").Value = Me.teilnahmebestatigung

                befehl.Parameters.Add("@registrationdate", System.Data.SqlDbType.DateTime)
                If Me.registrationDate.HasValue Then
                    befehl.Parameters("@registrationdate").Value = Me.registrationDate.Value
                Else
                    befehl.Parameters("@registrationdate").Value = System.DBNull.Value
                End If


                befehl.Parameters.Add("@arrival", System.Data.SqlDbType.DateTime)
                If Me.arrival.HasValue Then
                    befehl.Parameters("@arrival").Value = Me.arrival.Value
                Else
                    befehl.Parameters("@arrival").Value = System.DBNull.Value
                End If


                befehl.Parameters.Add("@departure", System.Data.SqlDbType.DateTime)
                If Me.departure.HasValue Then
                    befehl.Parameters("@departure").Value = Me.departure.Value
                Else
                    befehl.Parameters("@departure").Value = System.DBNull.Value
                End If


                'befehl.Parameters.Add("@paymenttype", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@paymenttype").Value = Me.paymenttype

                'befehl.Parameters.Add("@poster", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@poster").Value = Me.poster

                'befehl.Parameters.Add("@status", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@status").Value = Me.status

                'befehl.Parameters.Add("@external", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@external").Value = Me.external

                'befehl.Parameters.Add("@roomneighbour", System.Data.SqlDbType.NVarChar)
                'befehl.Parameters("@roomneighbour").Value = Me.roomneighbour

                'befehl.Parameters.Add("@fee", System.Data.SqlDbType.Money)
                'befehl.Parameters("@fee").Value = Me.fee

                befehl.Parameters.Add("@bemerkungenparticipant", System.Data.SqlDbType.NText)
                befehl.Parameters("@bemerkungenparticipant").Value = Me.bemerkungenparticipant

                befehl.Parameters.Add("@participantdauer", System.Data.SqlDbType.Int)
                befehl.Parameters("@participantdauer").Value = Me.participantdauer

                befehl.Parameters.Add("@participanttermin", System.Data.SqlDbType.NText)
                befehl.Parameters("@participanttermin").Value = Me.participanttermin

                befehl.Connection = conn

                Me.participantsID = CInt(befehl.ExecuteScalar) 'DEBUG
                Me.memberpin = randomNr
            Catch ex As Exception
                Dim errReport As String = Now.ToString("MMMM dd, yyyy HH:mm:ss", New System.Globalization.CultureInfo("de-DE")) + "<br />" + vbCrLf
                errReport += qcnp09.STR_GENERIC_DB_ERROR + "<br />" + vbCrLf
                errReport += ex.Message + "<br />" + vbCrLf

                If Not DebugHelper.IsDebugMode Then
                    ausgabe = qcnp09.STR_GENERIC_DB_ERROR
                Else
                    ausgabe = errReport
                End If
                ErrorReporting.ReportErrorToWebmaster(errReport)
            End Try
            conn.Close()
        End Using

        Return ausgabe
    End Function

    Private Function GenerateRandomNumber() As String
        Dim ausgabe As String = ""
        Dim numbers(5) As Integer
        Dim chars(3) As Integer

        Randomize()
        For i As Integer = 0 To numbers.Length - 1
            numbers(i) = Int((9 * Rnd()) + 1)   ' Ganze Zufallszahl im Bereich von 1 bis 9 (von 'A' bis 'Z') erzeugen.
        Next

        For i As Integer = 0 To chars.Length - 1
            Do
                chars(i) = Int((26 * Rnd()) + 1) + 64   ' Ganze Zufallszahl im Bereich von 65 bis 90 (von 'A' bis 'Z') erzeugen.
            Loop Until chars(i) <> 73 And chars(i) <> 79
        Next

        Return Chr(chars(0)) + _
               numbers(0).ToString + _
               numbers(1).ToString + _
               Chr(chars(1)) + _
               Chr(chars(2)) + _
               numbers(3).ToString + _
               numbers(4).ToString

    End Function
#End Region 'DBOperations
End Class