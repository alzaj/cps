Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class qcnp09
#Region "Constants"
    Public Shared ConferenceState As ConferenceStatus.ConferenceStates = ConferenceStatus.ConferenceStates.registration_opened
    'Public Shared ConferenceState As ConferenceStatus.ConferenceStates = ConferenceStatus.ConferenceStates.registration_closed

    Public Shared D_GRENZ_DATUM As New Date(2009, 5, 31)
    Public Shared P_REGISTR_FEE_BEFORE_GRENZ_DATUM As Decimal = 250
    Public Shared P_REGISTR_FEE_AFTER_GRENZ_DATUM As Decimal = 250

    Public Shared P_CONFERENCE_DINNER_PRISE As Decimal = 50
    Public Shared P_TICKET_TRANSPORT_PRISE As Decimal = 12
    Public Shared P_TICKET_VAULT_PRISE As Decimal = 12.3

    Public Shared P_CURRENCY_FORMAT As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")
    'Public Shared D_DATUM_FORMAT As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
    Public Shared D_DATUM_FORMAT As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")


    Public Shared D_DATUM_FORMAT_STRING As String = "MMMM %d, yyyy"

    Public Shared D_WORKSHOP_START As New Date(2011, 3, 10)
    Public Shared D_WORKSHOP_ENDE As New Date(2011, 3, 12)

    Public Shared D_DEADLINE_REGISTRATION As New Date(2011, 2, 11)
    Public Shared D_DEADLINE_ABSTRACT_SUBMISSION As New Date(2011, 2, 11)
    Public Shared D_NOTIFICATION_OF_ACCEPTANCE As New Date(2011, 2, 11)
    Public Shared D_DEADLINE_CHEQUE As New Date(2011, 2, 11)
    Public Shared D_DEADLINE_PAYMENT_CANCELATION As New Date(2011, 2, 11)

    Public Shared E_AGENT_EMAILS As String = MyAppSettings.E_AGENT_EMAILS  '"Alexander.Zajcev@cpfs.mpg.de" 'durch semikolon getrennte e-mails von bearbeiterinnen
    Public Const E_SUPPORT_EMAIL As String = "haeko2011@cpfs.mpg.de"
    'Public Const E_SUPPORT_EMAIL_HTML_TEXT As String = "support e-mail: <a href=""mailto:" + qcnp09.E_SUPPORT_EMAIL + """>" + qcnp09.E_SUPPORT_EMAIL + "</a>"
    Public Const E_SUPPORT_EMAIL_PREFIX As String = "Weitere Informationen: "
    Public Const E_SUPPORT_EMAIL_HTML_TEXT As String = E_SUPPORT_EMAIL_PREFIX + "<a href=""mailto:" + qcnp09.E_SUPPORT_EMAIL + """>" + qcnp09.E_SUPPORT_EMAIL + "</a>"
    Public Shared C_EXTENDED_ERROR_DESCRIPTION As Boolean = True  'Ob in den Fehlermeldungen auf den Registrierungsseiten detalierte informationen erscheinen sollen

    Public Const C_BASIC_DATA_SESSION_NAME As String = "basicdata"
    Public Const C_ACC_PERSONS_SESSION_NAME As String = "accpersons"
    Public Const C_MEMBER_ID_QUERY_STRING As String = "member"
    Public Const C_SUBDIRECTORY_IN_CACHE As String = "qcnp09"

    Public Const U_HOME_ABSOLUTE_URL As String = "http://www.cpfs.mpg.de/haeko2011/"
    Public Const U_PRINT_METHODS_ABSOLUTE_URL As String = U_HOME_ABSOLUTE_URL + "general/payment.html"
    Public Const U_INTRO_FILE As String = "~/registration/intro.aspx"
    Public Const U_LOGIN_FILE As String = "~/registration/login.aspx"
    Public Const U_FORMULAR_FILE As String = "~/registration/default.aspx"
    Public Const U_REGISTRATIONSUMMARY_FILE As String = "~/registration/registrationsummary.aspx"
    Public Const U_COMPLETE_REGISTRATION_FILE As String = "~/registration/complete_registration.aspx"
    Public Const U_CHANGE_REGISTRATION_FILE As String = "~/registration/change_registration.aspx"

    Public Const U_URL_TO_CSV_ASPX_FILE As String = "~/registration/qcnp09_members.aspx"
    Public Const U_URL_TO_ABSTRACTS_UPLOAD_FILE As String = "~/registration"
    Public Const U_URL_TO_QCNP09_REGISTRATION_ROOT As String = "~/registration"
    Public Const U_URL_TO_DUMMY_PDF As String = "~/registration/dummy.pdf"
    Public Const U_URL_TO_SORRY_PDF As String = "~/registration/sorry.pdf"
    Public Const U_URL_ABSTRACT_TEMPLATE_DOC As String = "~/registration/Firstname_Name_Abstract_CMACDays2010.doc"

    Public Const F_DOWNLOAD_PATH As String = "C:\Inetpub\cmac\registration\downloads"

    'strings
    Public Const STR_FAILURE_NOTIFICATION_RECEIVED As String = "Wir haben die Fehlermeldung bekommen und versuchen das Problem schnellstmöglich zu beseitigen." '"We received a failure notification and will try to solve the problem."
    Public Const STR_GENERIC_DB_ERROR As String = "Ihre Daten konnten nicht gespeichert werden. " + STR_FAILURE_NOTIFICATION_RECEIVED + " Versuchen Sie es bitte später noch einmal." '"The system was unable to save your data. " + STR_FAILURE_NOTIFICATION_RECEIVED + " Please try again later."
#End Region 'Constants

#Region "DB_Functions"
    Public Shared Function IdIstVergeben(ByVal id As String) As Boolean
        Dim sql As String = "EXEC pinexists '" + id + _
                                 "', @exists OUT"
        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)

            Dim befehl As New System.Data.SqlClient.SqlCommand()
            Dim existsParam As New System.Data.SqlClient.SqlParameter("exists", Data.SqlDbType.Bit, 100)
            existsParam.Direction = Data.ParameterDirection.Output
            befehl.Parameters.Add(existsParam)

            befehl.Connection = conn
            befehl.CommandText = sql
            conn.Open()
            befehl.ExecuteScalar()
            If Not befehl.Parameters.Item("exists").Value Is Nothing And (Not TypeOf befehl.Parameters.Item("exists").Value Is System.DBNull) Then
                Return CType(befehl.Parameters.Item("exists").Value, Boolean)
            End If
            Return True
            conn.Close()
        End Using
    End Function

    Public Shared Function IdIstValid(ByVal id As String) As Boolean
        Dim sql As String = "EXEC member_newpinvalid '" + id + _
                          "', @valid OUT"
        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("VDBConnectionString").ConnectionString)

            Dim befehl As New System.Data.SqlClient.SqlCommand()
            Dim validParam As New System.Data.SqlClient.SqlParameter("valid", Data.SqlDbType.Bit, 100)
            validParam.Direction = Data.ParameterDirection.Output
            befehl.Parameters.Add(validParam)

            befehl.Connection = conn
            befehl.CommandText = sql
            conn.Open()
            befehl.ExecuteScalar()
            If Not befehl.Parameters.Item("valid").Value Is Nothing And (Not TypeOf befehl.Parameters.Item("valid").Value Is System.DBNull) Then
                Return CType(befehl.Parameters.Item("valid").Value, Boolean)
            End If
            Return True
            conn.Close()
        End Using
    End Function

    Public Shared Function GetPinForID(ByVal memberid As Integer) As String
        Dim ausgabe As String = ""

        Dim sql As String = "SELECT memberpin FROM temp_participants " + _
                          " WHERE participantsid = " + memberid
        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)

            Dim befehl As New System.Data.SqlClient.SqlCommand()

            befehl.Connection = conn
            befehl.CommandText = sql
            conn.Open()
            ausgabe = befehl.ExecuteScalar().ToString
            conn.Close()
        End Using

        Return ausgabe
    End Function

    Public Shared Function GetIDForPin(ByVal memberpin As String) As Integer
        Dim ausgabe As Integer = 0

        Dim sql As String = "SELECT participantsid FROM temp_participants " + _
                          " WHERE memberpin = '" + memberpin + "'"
        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)

            Dim befehl As New System.Data.SqlClient.SqlCommand()

            befehl.Connection = conn
            befehl.CommandText = sql
            conn.Open()
            Try
                ausgabe = CInt(befehl.ExecuteScalar())
            Catch ex As Exception
            End Try
            conn.Close()
        End Using

        Return ausgabe
    End Function

    Public Overloads Shared Function GetBasicDataFromDatabase(ByVal memberpin As String) As BasicData
        Dim ausgabe As New BasicData
        Return ausgabe.GetFromDatabase("memberpin", memberpin)
    End Function

    Public Overloads Shared Function GetBasicDataFromDatabase(ByVal memberid As Integer) As BasicData
        Dim ausgabe As New BasicData
        Return ausgabe.GetFromDatabase("memberid", memberid.ToString)
    End Function
#End Region 'DB_Functions

#Region "Procedures"
    Public Shared Function TranslateRequestedAbstractToAspx(ByVal requestedFile As String) As String
        Dim ausgabe As String = ""

        requestedFile = requestedFile.Replace("/", "\")
        If requestedFile.Contains("\") Then
            requestedFile = GlobFunctions.SubstringAnfangEnde(requestedFile, requestedFile.LastIndexOf("\"), requestedFile.Length - 1).Trim("\")
        End If

        Dim ext As String = ""
        Dim basis As String = ""
        If requestedFile.Contains(".") Then
            ext = GlobFunctions.SubstringAnfangEnde(requestedFile, requestedFile.LastIndexOf("."), requestedFile.Length - 1).Trim(".")
            basis = GlobFunctions.SubstringAnfangEnde(requestedFile, 0, requestedFile.LastIndexOf(".")).Trim(".")
        Else
            basis = requestedFile
        End If

        'benötigte schema vom basis: <memberid>_<abstractid>  z.B 543_3
        Dim teile As String() = basis.Split("_")
        Dim valid As Boolean = True
        Dim mid As Integer = 0
        Dim aid As Integer = 0
        If teile.Length <> 2 Then
            valid = False
        Else
            Try
                mid = CInt(teile(0))
                aid = CInt(teile(1))
            Catch ex As Exception
                valid = False
            End Try
        End If

        ausgabe = U_URL_TO_ABSTRACTS_UPLOAD_FILE.Trim + "?m=" + mid.ToString + "&a=" + aid.ToString
        If ext <> "" Then
            ausgabe += "&e=" + ext
        End If

        Return ausgabe
    End Function

    Public Shared Function GetInfosForAbstractUploadFromRequestedFile(ByVal requestedFile As String) As InfosForAbstractUpload
        requestedFile = requestedFile.Replace("/", "\")
        If requestedFile.Contains("\") Then
            requestedFile = GlobFunctions.SubstringAnfangEnde(requestedFile, requestedFile.LastIndexOf("\"), requestedFile.Length - 1).Trim("\")
        End If

        Dim ext As String = ""
        Dim basis As String = ""
        If requestedFile.Contains(".") Then
            ext = GlobFunctions.SubstringAnfangEnde(requestedFile, requestedFile.LastIndexOf("."), requestedFile.Length - 1).Trim(".")
            basis = GlobFunctions.SubstringAnfangEnde(requestedFile, 0, requestedFile.LastIndexOf(".")).Trim(".")
        Else
            basis = requestedFile
        End If

        'benötigte schema vom basis: <memberid>_<abstractid>  z.B 543_3
        Dim teile As String() = basis.Split("_")
        Dim valid As Boolean = True
        Dim mid As Integer = 0
        Dim aid As Integer = 0
        If teile.Length <> 2 Then
            valid = False
        Else
            Try
                mid = CInt(teile(0))
                aid = CInt(teile(1))
            Catch ex As Exception
                valid = False
            End Try
        End If

        Return New InfosForAbstractUpload(mid, aid, ext)

    End Function

    ''' <summary>
    ''' Physikalischer Pfad für den angegebenen Abstrakt. Wenn dieser Abstract nicht existiert, wird leere Zeichenfolge zurückgegeben
    ''' </summary>
    ''' <param name="memberid"></param>
    ''' <param name="abstractid"></param>
    ''' <param name="extension">Dateinamenerweiterung</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFileNameForAbstractID(ByVal memberid As Integer, ByVal abstractid As Integer, ByVal extension As String) As String
        Dim ausgabe As String = ""
        If memberid = 0 Then Return ausgabe
        Dim nameZusatz As String = ""


        Dim copyNr As Integer = 0
        Dim tmpInt As Integer = 0

        If Not GlobFunctions.DirectoryExists(F_DOWNLOAD_PATH + "\" + memberid.ToString) Then
            GlobFunctions.DirectoryCreate(F_DOWNLOAD_PATH + "\" + memberid.ToString)
        End If

        If Not GlobFunctions.DirectoryExists(F_DOWNLOAD_PATH + "\" + memberid.ToString) Then
            Throw New Exception("abstracts.aspx_CalculateNameForFileToSave Error: Das Verzeichnis " + _
                                F_DOWNLOAD_PATH + "\" + memberid.ToString + " can not be created.")
            Return ausgabe
        End If

        Dim downloadZiel As String = F_DOWNLOAD_PATH + "\" + memberid.ToString

        For Each f As String In GlobFunctions.DirectoryGetFiles(downloadZiel)
            If f.StartsWith(downloadZiel + "\" + abstractid.ToString + "_") And f.EndsWith("." + extension) Then
                Return f
            End If
        Next
        Return ausgabe
    End Function
#End Region 'Procedures

    Public Class BasicData
        Public memberid As Integer = 0
        Public memberpin As String = ""
        Public title As String = ""
        Public gender As String = ""
        Public firstname As String = ""
        Public surname As String = ""
        Public institut As String = ""
        Public department As String = ""
        Public street As String = ""
        Public city As String = ""
        Public postalcode As String = ""
        Public contry As String = ""
        Public phone As String = ""
        Public fax As String = ""
        Public email As String = ""
        Public thema As String = ""
        Public coauthors As String = ""
        Public registrationfee As Decimal = 0
        Public isinvited As Boolean = False
        Public dinner_a As Decimal = 0
        Public dinner_p As Decimal = 0
        Public transport_a As Decimal = 0
        Public transport_p As Decimal = 0
        Public vault_a As Decimal = 0
        Public vault_p As Decimal = 0
        Public initiallyHadVault As Boolean = False
        Public isstudent_p As Boolean = False
        Public isstudent_a As Boolean = False
        Public payment_a As String = ""
        Public payment_p As String = ""
        Public exists_p As Boolean = False
        Public exists_a As Boolean = False

        Public Function GetFromDatabase(ByVal filterColumnName As String, ByVal filterColumnValue As String) As BasicData
            Dim ausgabe As New BasicData
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("VDBConnectionString").ConnectionString)
                Dim befehl As New SqlCommand()
                befehl.Connection = conn
                Dim SqlStr As String = "SELECT " + _
                                       "[memberid]" + _
                                       ",[memberpin]" + _
                                       ",[title]" + _
                                       ",[gender]" + _
                                       ",[firstname]" + _
                                       ",[surname]" + _
                                       ",[institut]" + _
                                       ",[department]" + _
                                       ",[street]" + _
                                       ",[postalcode]" + _
                                       ",[city]" + _
                                       ",[country]" + _
                                       ",[phone]" + _
                                       ",[fax]" + _
                                       ",[email]" + _
                                       ",[thema]" + _
                                       ",[coauthors]" + _
                                       ",[registrationfee]" + _
                                       ",[isinvited]" + _
                                       ",[dinner_a]" + _
                                       ",[dinner_p]" + _
                                       ",[transport_a]" + _
                                       ",[transport_p]" + _
                                       ",[vault_a]" + _
                                       ",[vault_p]" + _
                                       ",[isstudent_a]" + _
                                       ",[isstudent_p]" + _
                                       ",[payment_a]" + _
                                       ",[payment_p]" + _
                                       ",[exists_a]" + _
                                       ",[exists_p]" + _
                                       "FROM [member_new] WHERE " + filterColumnName + " = '" + filterColumnValue + "'"
                befehl.CommandText = SqlStr

                Dim dr As SqlDataReader
                Dim zeile As String = ""
                Try
                    conn.Open()
                    dr = befehl.ExecuteReader
                    If (dr.Read) Then
                        ausgabe.memberid = CInt(dr("memberid"))
                        ausgabe.memberpin = dr("memberpin").ToString
                        ausgabe.title = dr("title").ToString
                        ausgabe.gender = dr("gender").ToString
                        ausgabe.firstname = dr("firstname").ToString
                        ausgabe.surname = dr("surname").ToString
                        ausgabe.institut = dr("institut").ToString
                        ausgabe.department = dr("department").ToString
                        ausgabe.street = dr("street").ToString
                        ausgabe.postalcode = dr("postalcode").ToString
                        ausgabe.city = dr("city").ToString
                        ausgabe.contry = dr("country").ToString
                        ausgabe.phone = dr("phone").ToString
                        ausgabe.fax = dr("fax").ToString
                        ausgabe.email = dr("email").ToString
                        ausgabe.thema = dr("thema").ToString
                        ausgabe.coauthors = dr("coauthors").ToString
                        ausgabe.registrationfee = CDec(dr("registrationfee"))
                        ausgabe.isinvited = CDec(dr("isinvited"))
                        ausgabe.dinner_a = CDec(dr("dinner_a"))
                        ausgabe.dinner_p = CDec(dr("dinner_p"))
                        ausgabe.transport_a = CDec(dr("transport_a"))
                        ausgabe.transport_p = CDec(dr("transport_p"))
                        ausgabe.vault_a = CDec(dr("vault_a"))
                        ausgabe.vault_p = CDec(dr("vault_p"))
                        ausgabe.initiallyHadVault = ausgabe.vault_p > 0
                        ausgabe.isstudent_a = CBool(dr("isstudent_a"))
                        ausgabe.isstudent_p = CBool(dr("isstudent_p"))
                        ausgabe.payment_a = dr("payment_a").ToString
                        ausgabe.payment_p = dr("payment_p").ToString
                        ausgabe.exists_a = CBool(dr("exists_a"))
                        ausgabe.exists_p = CBool(dr("exists_p"))
                    Else
                        'Throw New Exception("qcnp09.vb(BasicData.GetFromDatabase) Error: Meber with " + filterColumnName + "= '" + filterColumnValue + "' not found")
                    End If
                    dr.Close()
                Catch ex As Exception
                    Throw New Exception("qcnp09.vb(BasicData.GetFromDatabase) Error: " + ex.Message)
                End Try
                conn.Close()
            End Using
            Return ausgabe
        End Function

        Public Sub New()

        End Sub

        Public Sub SaveToSession()
            System.Web.HttpContext.Current.Session(C_BASIC_DATA_SESSION_NAME) = Me
        End Sub

        Public Overloads Function GetFromSession(ByVal memberid As Integer) As BasicData
            Dim ausgabe As BasicData = System.Web.HttpContext.Current.Session(C_BASIC_DATA_SESSION_NAME)
            If Not ausgabe Is Nothing Then
                Return ausgabe
            Else
                Return GetFromDatabase("memberid", memberid.ToString)
            End If
        End Function

        Public Overloads Function GetFromSession(ByVal memberpin As String) As BasicData
            Dim ausgabe As BasicData = System.Web.HttpContext.Current.Session(C_BASIC_DATA_SESSION_NAME)
            If Not ausgabe Is Nothing Then
                Return ausgabe
            Else
                Return GetFromDatabase("memberpin", memberpin)
            End If
        End Function

    End Class

    Public Class DataForAccompany
        Public dbID As Integer = 0
        Public memberid As Integer = 0
        Public firstname_a As String = ""
        Public firstname_p As String = ""
        Public surname_a As String = ""
        Public surname_p As String = ""
        Public dinner_a As Decimal = 0
        Public dinner_p As Decimal = 0
        Public transport_a As Decimal = 0
        Public transport_p As Decimal = 0
        Public vault_a As Decimal = 0
        Public vault_p As Decimal = 0
        Public initiallyHadVault As Boolean = False
        Public exists_a As Boolean = False
        Public exists_p As Boolean = False

        'Public exists As Boolean = False
        Public existsInDatabase As Boolean = False

        Public Sub New()

        End Sub

        Public Function GetArrayOfAccompPersonsFromSession(Optional ByVal memberid As Integer = 0) As ArrayList
            Dim ausgabe As New ArrayList
            If Not System.Web.HttpContext.Current.Session(C_ACC_PERSONS_SESSION_NAME) Is Nothing Then
                ausgabe = System.Web.HttpContext.Current.Session(C_ACC_PERSONS_SESSION_NAME)
            Else
                ausgabe = Me.GetArrayOfAccompPersonsFromDatabase(memberid)
            End If
            Return ausgabe
        End Function

        Public Function GetArrayOfAccompPersonsFromDatabase(ByVal memberNr As Integer) As ArrayList
            Dim ausgabe As New ArrayList
            If memberNr = 0 Then Return ausgabe
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("VDBConnectionString").ConnectionString)
                Dim befehl As New SqlCommand()
                befehl.Connection = conn
                Dim SqlStr As String = "Select " + _
                                        "[id]" + _
                                        ",firstname_a " + _
                                        ",firstname_p " + _
                                        ",surname_a " + _
                                        ",surname_p " + _
                                        ",dinner_a " + _
                                        ",dinner_p " + _
                                        ",transport_a " + _
                                        ",transport_p " + _
                                        ",vault_a " + _
                                        ",vault_p " + _
                                        ", exists_a " + _
                                        ", exists_p " + _
                                       "FROM [accompperson_new] WHERE memberid = " + memberNr.ToString + _
                                                                " AND exists_p = 1"
                befehl.CommandText = SqlStr

                Dim dr As SqlDataReader
                Dim zeile As String = ""
                Try
                    conn.Open()

                    dr = befehl.ExecuteReader
                    While (dr.Read)
                        Dim person As New DataForAccompany
                        person.dbID = dr("id")
                        person.memberid = memberNr
                        person.firstname_a = dr("firstname_a").ToString
                        person.firstname_p = dr("firstname_p").ToString
                        person.surname_a = dr("surname_a").ToString
                        person.surname_p = dr("surname_p").ToString
                        person.dinner_a = CDec(dr("dinner_a"))
                        person.dinner_p = CDec(dr("dinner_p"))
                        person.transport_a = CDec(dr("transport_a"))
                        person.transport_p = CDec(dr("transport_p"))
                        person.vault_a = CDec(dr("vault_a"))
                        person.vault_p = CDec(dr("vault_p"))
                        person.initiallyHadVault = person.vault_p > 0
                        person.exists_a = CBool(dr("exists_a"))
                        person.exists_p = CBool(dr("exists_p"))
                        person.existsInDatabase = True
                        ausgabe.Add(person)
                    End While
                    dr.Close()
                Catch ex As Exception
                    Throw New Exception("qcnp09.vb(DataForAccompany.GetAccompPersonsFromDatabase) Error: " + ex.Message)
                End Try
                conn.Close()
            End Using
            Return ausgabe
        End Function

        Public Sub SaveArrayOfAccompPersonsToSession(ByVal persons As ArrayList)
            System.Web.HttpContext.Current.Session(C_ACC_PERSONS_SESSION_NAME) = persons
        End Sub


    End Class

    Public Class InfosForAbstractUpload
        Public memberid As Integer
        Public abstractid As Integer
        Public fileExtension As String
        Public urlForAbstractUpload As String
        Public Sub New(ByVal memberid As Integer, ByVal abstractid As Integer, ByVal fileextension As String)
            Me.memberid = memberid
            Me.abstractid = abstractid
            Me.fileExtension = fileextension
            Me.urlForAbstractUpload = U_URL_TO_ABSTRACTS_UPLOAD_FILE.Trim("/") + "/abstract_upload.aspx"
        End Sub
    End Class
End Class
