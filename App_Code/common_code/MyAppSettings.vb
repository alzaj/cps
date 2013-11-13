Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Configuration

Public Class MyAppSettings

#Region "strings"
    Private Shared emailException As String = "Information for the web developer: for the application to be able to send e-mails " + _
                            "in the application settings the following sections should be properly configured:<br>" + vbCrLf + _
                            "Section SmtpServerName should contain the alias of the Smtp Server<br>" + vbCrLf + _
                            "Section FromEmailForThisApplication should contain a valid e-mail address that will be placed in the from field of the e-mail<br>" + vbCrLf + _
                            "Section FromEmailUserName should contain the name of the user (only name without domain name) that has permissions to send from this e-mail<br>" + vbCrLf + _
                            "Section FromEmailPassword shoud contain the password for this user<br>" + vbCrLf
#End Region 'strings

#Region "Settings - String"

    Public Shared ReadOnly Property HomePageUrl As String
        Get
            Return GetStringFromAppSettings("HomePageUrl")
        End Get
    End Property

    Public Shared ReadOnly Property LogoUrl As String
        Get
            Return GetStringFromAppSettings("LogoUrl")
        End Get
    End Property

    Public Shared ReadOnly Property ImpressumUrl As String
        Get
            Return GetStringFromAppSettings("ImpressumUrl")
        End Get
    End Property

    'Email addresses who wants to receive new registration notifications
    Public Shared ReadOnly Property E_AGENT_EMAILS As String
        Get
            Return GetStringFromAppSettings("AgentEmails")
        End Get
    End Property

    Public Shared ReadOnly Property SupportEmail As String
        Get
            Return GetStringFromAppSettings("SupportEmail")
        End Get
    End Property

    Public Shared ReadOnly Property WebMasterEmail As String
        Get
            Return GetStringFromAppSettings("WebMasterEmail")
        End Get
    End Property

    Public Shared ReadOnly Property SupportPhone As String
        Get
            Return GetStringFromAppSettings("SupportPhone")
        End Get
    End Property

    Public Shared ReadOnly Property SupportFax As String
        Get
            Return GetStringFromAppSettings("SupportFax")
        End Get
    End Property

    Public Shared ReadOnly Property thisIPsHavePermissionsToLogIn As String
        Get
            Return GetStringFromAppSettings("thisIPsHavePermissionsToLogIn")
        End Get
    End Property

    Public Shared ReadOnly Property SmtpServerName As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("SmtpServerName", "")
            If ausgabe = "" Then
                Throw New Exception(emailException)
            End If
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property FromEmailForThisApplication As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("FromEmailForThisApplication", "")
            If ausgabe = "" Then
                Throw New Exception(emailException)
            End If
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property FromEmailUserName As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("FromEmailUserName", "")
            If ausgabe = "" Then
                Throw New Exception(emailException)
            End If
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property FromEmailPassword As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("FromEmailPassword", "")
            If ausgabe = "" Then
                Throw New Exception(emailException)
            End If
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property PageTitlePrefix As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("PageTitlePrefix", "")
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property PageBannerHTML As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("PageBannerHTML", "")
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property WebsiteMiddleName As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("WebsiteMiddleName", "")
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property WebsiteLongName As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("WebsiteLongName", "")
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property forceRegistrationFor As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("forceRegistrationFor", "")
            Return ausgabe
        End Get
    End Property

    Public Shared Property AppSettingsTemplate As String
        Get
            Dim needSave As Boolean = False
            Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Dim templSection As AppSettingsTemplateConfSection = myConfiguration.Sections("AppSettingsTemplate")
            If templSection Is Nothing Then
                templSection = New AppSettingsTemplateConfSection
                myConfiguration.Sections.Add("AppSettingsTemplate", templSection)
                needSave = True
            End If
            Dim ausgabe As String = templSection.settingKeys
            If String.IsNullOrEmpty(ausgabe) Then 'creating AppSettingsTemplate key
                Dim val As String = ""
                Dim separator As String = ""
                For Each s As String In MyAppSettings.EnumerateAppSettingKeys
                    val += separator + s
                    separator = ";"
                Next

                templSection.settingKeys = val
                ausgabe = templSection.settingKeys
                needSave = True
            End If
            If needSave Then
                myConfiguration.Save()
                System.Configuration.ConfigurationManager.RefreshSection("AppSettingsTemplate")
            End If
            Return ausgabe
        End Get
        Set(ByVal value As String)
            Dim a As String = AppSettingsTemplate 'becouse Get initiates the ConfigSection if it absent
            Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Dim templSection As AppSettingsTemplateConfSection = myConfiguration.Sections("AppSettingsTemplate")
            templSection.settingKeys = value
            myConfiguration.Save()
            System.Configuration.ConfigurationManager.RefreshSection("AppSettingsTemplate")
        End Set
    End Property

    Public Shared ReadOnly Property ISO639_MainLanguage As String
        Get
            Return GetStringFromAppSettings("ISO639-1_MainLanguage", "de")
        End Get
    End Property

    Public Shared ReadOnly Property MainCulture As System.Globalization.CultureInfo
        Get
            Dim ausgabe As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")
            If ISO639_MainLanguage = "en" Then
                ausgabe = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN")
            End If
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property IPAliases As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("IPAliases", "")
            Return ausgabe
        End Get
    End Property

    Public Shared ReadOnly Property TopNavigationEntries As String
        Get
            Dim ausgabe As String = GetStringFromAppSettings("TopNavigationEntries", "")
            Return ausgabe
        End Get
    End Property
#End Region 'Settings - String

#Region "Settings - Boolean"

    ''' <summary>
    ''' Some settings and messages in the application output more detailed information, if this flag is set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property IsDebugMode() As Boolean
        Get
            Return GetBooleanFromAppSettings("IsDebugMode", False)
        End Get
    End Property

    ''' <summary>
    ''' In the last wizard step the user will be asked to confirm his agreement about saving his data in the database.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property AskUserForAgreement() As Boolean
        Get
            Return GetBooleanFromAppSettings("AskUserForAgreement", False)
        End Get
    End Property

    Public Shared ReadOnly Property IsThisAppFeldmaessig() As Boolean
        Get
            Return GetBooleanFromAppSettings("IsThisAppFeldmaessig", False)
        End Get
    End Property

#End Region 'Settings - Boolean

#Region "Settings - Integer"
    Public Shared ReadOnly Property maxNumberOfParticipants As Integer
        Get
            Return GetIntegerFromAppSettings("maxNumberOfParticipants", 0)
        End Get
    End Property
#End Region 'Settings - Integer

#Region "Settings - Decimal"
    Public Shared ReadOnly Property Amount_CancelationServiceCharge As Decimal
        Get
            Return GetDecimalFromAppSettings("Amount_CancelationServiceCharge", 0)
        End Get
    End Property

    Public Shared ReadOnly Property Amount_GreenVaultTicket As Decimal
        Get
            Return GetDecimalFromAppSettings("Amount_GreenVaultTicket", 0)
        End Get
    End Property

    Public Shared ReadOnly Property Amount_Zusatzdinner As Decimal
        Get
            Return GetDecimalFromAppSettings("Amount_Zusatzdinner", 0)
        End Get
    End Property

#End Region 'Settings - Decimal


#Region "Settings - DateTime"
    Public Shared ReadOnly Property ISO8601_WorkshopStart As DateTime
        Get
            Return GetDateTimeFromAppSettings("ISO8601_WorkshopStart", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property ISO8601_WorkshopEnd As DateTime
        Get
            Return GetDateTimeFromAppSettings("ISO8601_WorkshopEnd", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property ISO8601_PosterSessionStart As DateTime
        Get
            Return GetDateTimeFromAppSettings("ISO8601_PosterSessionStart", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property ISO8601_PosterSessionEnde As DateTime
        Get
            Return GetDateTimeFromAppSettings("ISO8601_PosterSessionEnde", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property Deadline_Registration As DateTime
        Get
            Return GetDateTimeFromAppSettings("Deadline_Registration", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property Deadline_Cancelations As DateTime
        Get
            Return GetDateTimeFromAppSettings("Deadline_Cancelations", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property Deadline_Accommodation As DateTime
        Get
            Return GetDateTimeFromAppSettings("Deadline_Accommodation", New DateTime(2001, 1, 1))
        End Get
    End Property

    Public Shared ReadOnly Property Deadline_Abstracts As DateTime
        Get
            Return GetDateTimeFromAppSettings("Deadline_Abstracts", New DateTime(2001, 1, 1))
        End Get
    End Property
#End Region 'Settings -Datetime

#Region "other types"
    Public Shared ReadOnly Property workshopStatus As ConferenceStatus.ConferenceStates
        Get
            Dim ausgabe As ConferenceStatus.ConferenceStates = ConferenceStatus.ConferenceStates.registration_closed
            Dim val As String = GetStringFromAppSettings("workshopStatus")
            For Each c As ConferenceStatus.ConferenceStates In [Enum].GetValues(GetType(ConferenceStatus.ConferenceStates))
                If val = [Enum].GetName(GetType(ConferenceStatus.ConferenceStates), c) Then
                    ausgabe = c
                    Exit For
                End If
            Next
            Return ausgabe
        End Get
    End Property

    Public Shared Function GetIPAlias(ipAddress As String) As String
        Dim ausgabe As String = ipAddress
        Dim entries As String() = IPAliases.Split(";")
        For Each e As String In entries
            If e.Length > 0 Then
                Dim recordParts As String() = e.Split(":")
                If recordParts.Length > 1 AndAlso recordParts(0) = ipAddress Then
                    ausgabe = recordParts(1)
                    Exit For
                End If
            End If
        Next
        Return ausgabe
    End Function

    ''' <summary>
    ''' Returns list of items for the top navigation
    ''' </summary>
    ''' <returns>Eeach key -  items url, value - items Title</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTopNavigationEntries() As List(Of KeyValuePair(Of String, String))
        Dim ausgabe As New List(Of KeyValuePair(Of String, String))
        Dim entries As String() = TopNavigationEntries.TrimEnd(";").Split(";")
        For Each e As String In entries
            If e.Length > 0 Then
                Dim recordParts As String() = e.Split(":")
                If recordParts.Length > 1 Then
                    Dim kvp As New KeyValuePair(Of String, String)(recordParts(0), recordParts(1))
                    ausgabe.Add(kvp)
                End If
            End If
        Next
        Return ausgabe
    End Function
#End Region 'other types

#Region "Procedures"
    ''' <summary>
    ''' Converts the value stored in ConfigurationManager.AppSettings section to boolean.
    ''' If the key doesn't exist - returns standardValue from second parameter.
    ''' </summary>
    ''' <param name="settingName">AppSettings key name</param>
    ''' <param name="standardValue">Value to use if the AppSettings key doesn't exist</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetBooleanFromAppSettings(ByVal settingName As String, ByVal standardValue As Boolean) As Boolean
        Dim ausgabe As Boolean = standardValue
        Dim settingValue As String = ConfigurationManager.AppSettings(settingName)
        If Not String.IsNullOrEmpty(settingValue) Then
            If settingValue = "1" Or _
               settingValue.ToLower = "yes" Or _
               settingValue.ToLower = "ja" Or _
               settingValue.ToLower = "true" Then

                ausgabe = True
            Else
                ausgabe = False
            End If
        End If

        Return ausgabe
    End Function

    ''' <summary>
    ''' Converts the value stored in ConfigurationManager.AppSettings section to integer.
    ''' If the key doesn't exist - returns standardValue from second parameter.
    ''' </summary>
    ''' <param name="settingName">AppSettings key name</param>
    ''' <param name="standardValue">Value to use if the AppSettings key doesn't exist</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetIntegerFromAppSettings(ByVal settingName As String, ByVal standardValue As Integer) As Integer
        Dim ausgabe As Integer = standardValue
        Dim settingValue As String = ConfigurationManager.AppSettings(settingName)
        If Not String.IsNullOrEmpty(settingValue) Then
            Try
                ausgabe = CType(settingValue, Integer)
            Catch ex As Exception
            End Try
        End If
        Return ausgabe
    End Function

    ''' <summary>
    ''' Converts the value stored in ConfigurationManager.AppSettings section to datetime.
    ''' If the key doesn't exist - returns standardValue from second parameter.
    ''' </summary>
    ''' <param name="settingName">AppSettings key name</param>
    ''' <param name="standardValue">Value to use if the AppSettings key doesn't exist</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetDateTimeFromAppSettings(ByVal settingName As String, ByVal standardValue As DateTime) As DateTime
        Dim ausgabe As DateTime = standardValue
        Dim settingValue As String = ConfigurationManager.AppSettings(settingName)
        If Not String.IsNullOrEmpty(settingValue) Then
            Try
                Dim y As Integer = CInt(settingValue.Substring(0, 4))
                Dim m As Integer = CInt(settingValue.Substring(5, 2))
                Dim d As Integer = CInt(settingValue.Substring(8, 2))
                Dim h As Integer = 0
                Dim minute As Integer = 0
                Dim s As Integer = 0
                Try
                    h = CInt(settingValue.Substring(11, 2))
                    minute = CInt(settingValue.Substring(14, 2))
                    s = CInt(settingValue.Substring(17, 2))
                Catch ex As Exception
                End Try

                ausgabe = New DateTime(y, m, d, h, minute, s)
            Catch ex As Exception
                Throw New SettingsPropertyWrongTypeException("The key """ + settingName + _
                        """ in the ""appSettings"" section of the ""web.config"" must contain a date string in the ISO 8601 format (YYYY-MM-DD hh:mm:ss)." + _
                        " If not - you will get this error." + vbCrLf + "error message: " + ex.Message)
            End Try
        End If
        Return ausgabe
    End Function

    ''' <summary>
    ''' Converts the value stored in ConfigurationManager.AppSettings section to decimal.
    ''' If the key doesn't exist - returns standardValue from second parameter.
    ''' </summary>
    ''' <param name="settingName">AppSettings key name</param>
    ''' <param name="standardValue">Value to use if the AppSettings key doesn't exist</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetDecimalFromAppSettings(ByVal settingName As String, ByVal standardValue As Decimal) As Decimal
        Dim ausgabe As Decimal = standardValue
        Dim settingValue As String = ConfigurationManager.AppSettings(settingName)
        If Not String.IsNullOrEmpty(settingValue) Then
            Try
                ausgabe = CDec(settingValue)
            Catch ex As Exception
                Throw New SettingsPropertyWrongTypeException("The key """ + settingName + _
                        """ in the ""appSettings"" section of the ""web.config"" must contain a string representing a decimal number." + _
                        " If not - you will get this error." + vbCrLf + "error message: " + ex.Message)
            End Try
        End If
        Return ausgabe
    End Function


    ''' <summary>
    ''' Returns the value stored in ConfigurationManager.AppSettings section.
    ''' </summary>
    ''' <param name="settingName">AppSettings key name</param>
    ''' <param name="standardValue">Value to use if the AppSettings key doesn't exist.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetStringFromAppSettings(ByVal settingName As String, Optional ByVal standardValue As String = "") As String
        Dim ausgabe As String = ConfigurationManager.AppSettings(settingName)
        If ausgabe Is Nothing Then ausgabe = standardValue
        Return ausgabe
    End Function

    Public Shared Function EnumerateAppSettingKeys() As List(Of String)
        Dim ausgabe As New List(Of String)
        For Each k As String In ConfigurationManager.AppSettings.Keys
            If Not k = "AppSettingsTemplate" Then
                ausgabe.Add(k)
            End If
        Next
        Return ausgabe
    End Function

    Public Shared Function EnumerateAppSettingsTemplate() As List(Of String)
        Dim ausgabe As New List(Of String)
        For Each s As String In AppSettingsTemplate.Split(";")
            If Not String.IsNullOrEmpty(s.Trim()) Then ausgabe.Add(s)
        Next
        Return ausgabe
    End Function

    Public Shared Sub CheckConsistensyOfAppSettings()
        Dim origS As List(Of String) = EnumerateAppSettingKeys()
        Dim templS As List(Of String) = EnumerateAppSettingsTemplate()

        Dim entriesToAdd As String = ""
        Dim separator As String = ""
        If templS.Count > 0 Then separator = ";"

        Dim alarmText As String = ""

        Dim needSave As Boolean = False

        'removing from both arrays entries that match each other
        For i As Integer = origS.Count - 1 To 0 Step -1
            For j As Integer = templS.Count - 1 To 0 Step -1
                If origS(i).ToLower = templS(j).ToLower Then
                    origS.RemoveAt(i)
                    templS.RemoveAt(j)
                    Exit For
                End If
            Next
        Next

        For Each s As String In origS
            entriesToAdd += separator + s
            separator = ";"
            needSave = True
        Next

        If needSave Then
            AppSettingsTemplate += entriesToAdd
        End If

        For Each s As String In templS
            alarmText += s + vbCrLf
        Next

        If Not String.IsNullOrEmpty(alarmText) Then
            alarmText = "Some AppSettings entries are missing. Please add following AppSettings entries to your web configuration, or remove them from AppSettingsTemplate section of Web.config:" + vbCrLf + alarmText
            Throw New Exception(alarmText)
        End If

        'analize remaining entries.
    End Sub

#End Region 'Procedures

End Class

Public Class AppSettingsTemplateConfSection
    Inherits System.Configuration.ConfigurationSection

    <ConfigurationProperty("settingKeys", DefaultValue:="", IsRequired:=True)> _
    Public Property settingKeys() As String
        Get
            Return CType(Me("settingKeys"), String)
        End Get
        Set(ByVal value As String)
            Me("settingKeys") = value
        End Set
    End Property

End Class
