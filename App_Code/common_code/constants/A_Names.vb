Imports Microsoft.VisualBasic

'Names of appsettings in web.config
Public Class A_Names

    Public Const A_IsDebugMode As String = S_Names.S_IsDebugMode  'DebugHelper.vb
    Public Const A_CurrentWorkshopID As String = "CurrentWorkshopID" 'GlobFunctions.vb
    Public Const A_LogsFolder As String = "LogsFolder" 'ErrorReporting.vb
    Public Const A_AskUserForAgreement As String = "AskUserForAgreement"
    Public Const A_AgentEmails As String = "AgentEmails" 'MyAppSettings.vb
    Public Const A_IsThisAppFeldmaessig As String = "IsThisAppFeldmaessig" 'MyAppSettings.vb
    Public Const A_agentName As String = "agentName" 'login.vb
    Public Const A_agentPasswort As String = "agentPasswort" 'login.vb
    Public Const A_thisIPsHavePermissionsToLogIn As String = "thisIPsHavePermissionsToLogIn" 'GlobFunctions.vb
End Class
