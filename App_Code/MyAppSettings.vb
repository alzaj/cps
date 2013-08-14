Imports Microsoft.VisualBasic

Public Class MyAppSettings

#Region "strings"
    Private Shared emailException As String = "Information for the web developer: for the application to be able to send e-mails " + _
                            "in the application settings the following sections should be properly configured:<br>" + vbCrLf + _
                            "Section SmtpServerName should contain the alias of the Smtp Server<br>" + vbCrLf + _
                            "Section FromEmailForThisApplication should contain a valid e-mail address that will be placed in the from field of the e-mail<br>" + vbCrLf + _
                            "Section FromEmailUserName should contain the name of the user (only name without domain name) that has permissions to send from this e-mail<br>" + vbCrLf + _
                            "Section FromEmailPassword shoud contain the password for this user<br>" + vbCrLf
#End Region 'strings

    ''' <summary>
    ''' Some settings and messages in the application output more detailed information, if this flag is set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property IsDebugMode() As Boolean
        Get
            Return GetBooleanFromAppSettings(A_Names.A_IsDebugMode, False)
        End Get
    End Property

    ''' <summary>
    ''' In the last wizard step the user will be asked to confirm his agreement about saving his data in the database.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property AskUserForAgreement() As Boolean
        Get
            Return GetBooleanFromAppSettings(A_Names.A_AskUserForAgreement, False)
        End Get
    End Property

    Public Shared ReadOnly Property IsThisAppFeldmaessig() As Boolean
        Get
            Return GetBooleanFromAppSettings(A_Names.A_IsThisAppFeldmaessig, False)
        End Get
    End Property

    'Email addresses who wants to receive new registration notifications
    Public Shared ReadOnly Property E_AGENT_EMAILS As String
        Get
            Return GetStringFromAppSettings(A_Names.A_AgentEmails)
        End Get
    End Property

    Public Shared ReadOnly Property thisIPsHavePermissionsToLogIn As String
        Get
            Return GetStringFromAppSettings(A_Names.A_thisIPsHavePermissionsToLogIn)
        End Get
    End Property

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
End Class
