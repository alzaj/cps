Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO

Public Class ErrorReporting
    Public Shared ReadOnly Property LogDirVirtualPath As String
        Get
            Dim f As String = ConfigurationManager.AppSettings(A_Names.A_LogsFolder)
            If Not String.IsNullOrEmpty(f) Then
                Return f.TrimEnd("/") + "/"
            Else
                Return "~/logs/"
            End If
        End Get
    End Property

    Public Shared ReadOnly Property LogDirPhysicalPath As String
        Get
            Return System.Web.HttpContext.Current.Server.MapPath(LogDirVirtualPath).TrimEnd("\") + "\"
        End Get
    End Property

    Public Shared Sub ReportErrorToWebmaster(ByVal errText As String, Optional ByVal depth As Integer = 10)
        Try
            Dim path As String = LogDirVirtualPath & DateTime.Now.ToString("yyyy'-'MMM'-'dd'_'HH'-'mm'-'ss'_'fffffff") & ".txt"
            If (Not System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))) Then
                System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath(path)).Close()
            End If


            Dim err As String = vbCrLf + "Log Entry :<br />" + vbCrLf
            err += DateTime.Now.ToString("MMMM %d, yyyy HH:mm", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + "<br />" + vbCrLf
            Dim d As String = ""
            d += System.Web.HttpContext.Current.Request.UserHostAddress + ";"
            Dim ip As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If Not String.IsNullOrEmpty(ip) Then
                d += " Forward from " + ip
            End If
            err += "Client Host: " + d + "<br />" + vbCrLf
            err += "Client System: " + System.Web.HttpContext.Current.Request.ServerVariables("http_user_agent") + "<br />" + vbCrLf
            err += "Error in: " & System.Web.HttpContext.Current.Request.Url.ToString() + ".<br />" + vbCrLf
            err += "Error Message:" & errText
            err += "<br />" + vbCrLf
            err += "__________________________" + vbCrLf

            System.IO.File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(path), err, System.Text.Encoding.UTF8)

        Catch ex As Exception
            'protecting against overflow
            If depth > 0 Then
                ReportErrorToWebmaster(ex.Message, depth - 1)
            Else
                If DebugHelper.IsDebugMode Then
                    Throw New Exception("ErrorReporting.ReportErrorToWebmaster Error: " + ex.Message)
                End If
            End If
        End Try
    End Sub

    Public Shared Sub ReportLastErrorToWebmaster()
        Dim br As String = "<br>" + vbCrLf
        Dim ctx As HttpContext = System.Web.HttpContext.Current
        Dim err As Exception = ctx.Server.GetLastError
        Dim ausgabe As String = ""

        If Not err Is Nothing Then
            ausgabe += err.Message
            ausgabe += br + br
            ausgabe += err.StackTrace.Replace(vbCrLf, "<br />" + vbCrLf)
            ReportErrorToWebmaster(ausgabe)
        End If
    End Sub

    Public Shared Function PopErrorFromSession() As String
        Dim ausgabe As String = ""
        If Not String.IsNullOrEmpty(System.Web.HttpContext.Current.Session(S_Names.S_LastError)) Then
            ausgabe = System.Web.HttpContext.Current.Session(S_Names.S_LastError)
            System.Web.HttpContext.Current.Session(S_Names.S_LastError) = ""
        End If
        Return ausgabe
    End Function

    Public Shared Sub PutErrorToSession(ByVal errText As String)
        System.Web.HttpContext.Current.Session(S_Names.S_LastError) = errText
    End Sub

    Public Shared Function GetListOfErrorsAsLinks() As List(Of String)
        Dim ausgabe As New List(Of String)
        Dim physDir As String = LogDirPhysicalPath
        If System.IO.Directory.Exists(physDir) Then
            Dim folder As New System.IO.DirectoryInfo(physDir)
            Dim files As System.IO.FileInfo() = folder.GetFiles
            Array.Sort(files, New reverseCompareFileInfo)
            For Each f As System.IO.FileInfo In files
                Dim l As New HyperLink
                l.NavigateUrl = LogDirVirtualPath + f.Name
                l.Text = f.Name
                ausgabe.Add(GlobFunctions.GetControlHtmlText(l))
            Next
        End If
        Return ausgabe
    End Function

    Public Shared Function GetListOfErrorItems() As List(Of ErrorItem)
        Dim ausgabe As New List(Of ErrorItem)
        Dim physDir As String = LogDirPhysicalPath
        If System.IO.Directory.Exists(physDir) Then
            Dim folder As New System.IO.DirectoryInfo(physDir)
            Dim files As System.IO.FileInfo() = folder.GetFiles
            Array.Sort(files, New reverseCompareFileInfo)
            For Each f As System.IO.FileInfo In files
                If f.Name.ToLower = "empty.txt" Then Continue For

                Dim ei As New ErrorItem
                ei.Datum = f.CreationTimeUtc
                Using r As System.IO.StreamReader = f.OpenText
                    ei.Description = r.ReadToEnd
                    r.Close()
                End Using

                ausgabe.Add(ei)
            Next
        End If
        Return ausgabe
    End Function

    ''' <summary>
    ''' puts the error to session and reports it to webmaster
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub StandardPageErrorHandling()
        Dim lastErr As Exception = System.Web.HttpContext.Current.Server.GetLastError
        Dim mesg As String = ""
        If Not lastErr Is Nothing Then
            If TypeOf lastErr Is HttpRequestValidationException Then
                'cut from error message the place with invalid input
                Dim invalidText As String = GlobFunctions.SubstringAnfangEnde(lastErr.Message, lastErr.Message.IndexOf(""""), lastErr.Message.LastIndexOf(""""))

                mesg += "<br /><br />" + vbCrLf
                mesg += "Please do not input html code"
                If Not String.IsNullOrEmpty(invalidText) Then

                    mesg += "(such as <span style=""color:red;font-weight:bold"">" + System.Web.HttpContext.Current.Server.HtmlEncode(invalidText) + "</span>)"
                End If
                mesg += " in the form fields."
            ElseIf TypeOf lastErr Is DangerousBehavoirException Then
                mesg += "<br /><br />" + vbCrLf
                mesg += "Please do not use dangerous bahavoir"
                mesg += "(such as <span style=""color:red;font-weight:bold"">submitting the registration form multiple times one after other</span>)."
            End If
        End If
        ErrorReporting.ReportLastErrorToWebmaster()
        ErrorReporting.PutErrorToSession(mesg)
    End Sub
End Class

Public Class ErrorItem
    Private _datum As New DateTime(2000, 1, 1)
    Public Property Datum As DateTime 
        Set(ByVal value As DateTime)
            Me._datum = value
        End Set
        Get
            Return Me._datum
        End Get
    End Property

    Private _description As String = ""
    Public Property Description As String 
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
        End Set
    End Property
End Class

''' <summary>
''' For descending sorting the txt files with errors on modified date
''' </summary>
''' <remarks></remarks>
Public Class reverseCompareFileInfo
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim File1 As FileInfo
        Dim File2 As FileInfo
        File1 = DirectCast(x, FileInfo)
        File2 = DirectCast(y, FileInfo)
        'exchange the two arguments to get descending sort order
        Compare = DateTime.Compare(File2.LastWriteTime, File1.LastWriteTime)
    End Function

End Class