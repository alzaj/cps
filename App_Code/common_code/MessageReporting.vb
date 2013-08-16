Imports Microsoft.VisualBasic

Public Class MessageReporting
    Public Shared Function PopMessageFromSession() As String
        Dim ausgabe As String = ""
        If Not String.IsNullOrEmpty(System.Web.HttpContext.Current.Session(S_Names.S_LastMessage)) Then
            ausgabe = System.Web.HttpContext.Current.Session(S_Names.S_LastMessage)
            System.Web.HttpContext.Current.Session(S_Names.S_LastMessage) = ""
        End If
        Return ausgabe
    End Function

    Public Shared Sub PutMessageToSession(ByVal errText As String)
        System.Web.HttpContext.Current.Session(S_Names.S_LastMessage) = errText
    End Sub

    Public Shared Sub TransferToMessage(ByVal messageText As String)
        MessageReporting.PutMessageToSession(messageText)
        System.Web.HttpContext.Current.Server.Transfer(F_NAMES.F_URL_MESSAGE_FILE)
    End Sub

End Class
