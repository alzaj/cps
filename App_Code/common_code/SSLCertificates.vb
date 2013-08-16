Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class SSLCertificates
    Private Shared Property ClientAlreadyWarnedIfItDontAcceptCPFSCertificate As Boolean
        Get
            If System.Web.HttpContext.Current.Session(S_Names.S_ClientWarnedAboutCertificate) Is Nothing Then
                System.Web.HttpContext.Current.Session(S_Names.S_ClientWarnedAboutCertificate) = False
            End If
            Return System.Web.HttpContext.Current.Session(S_Names.S_ClientWarnedAboutCertificate)
        End Get

        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Session(S_Names.S_ClientWarnedAboutCertificate) = value
        End Set
    End Property

    Public Shared Sub WarnClientIfItDontAcceptCPFSCertificate(ByVal continueToCaption As String, ByVal continueToUrl As String)
        If Not ClientAlreadyWarnedIfItDontAcceptCPFSCertificate Then
            If Not ClientAcceptsOurCertificate() Then
                MessageReporting.TransferToMessage("Certificate not accepted. Continue to " + "<a href=""" + continueToUrl + """>" + continueToCaption + "</a>")
            End If
        End If
    End Sub

    Private Shared Function ClientAcceptsOurCertificate() As Boolean
        Dim ausgabe As Boolean = True

        'Dim badBrowsers As New List(Of String)
        'badBrowsers.Add("Opera")
        'badBrowsers.Add("Konqueror")

        'For Each b As String In badBrowsers
        '    If InStr(System.Web.HttpContext.Current.Request.ServerVariables("http_user_agent"), b) Then
        '        ausgabe = False
        '        Exit For
        '    End If
        'Next

        Return ausgabe
    End Function
End Class
