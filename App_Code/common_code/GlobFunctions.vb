Imports System.IO

Public Class GlobFunctions
    ''' <summary>
    ''' gibt die Dateinamenerweiterung ohne Punkt und kleingeschrieben zurück. Bsp.: jpg, aspx, ...
    ''' </summary>
    ''' <param name="file">Physikalischer, virtueller oder relativer Pfad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileExtension(ByVal file As String) As String
        Dim ausgabe As String = ""
        Dim pos As Integer = file.LastIndexOf(".") + 1
        If pos >= 0 And pos < file.Length Then
            ausgabe = file.Substring(pos).ToLower
        End If
        Return ausgabe
    End Function

    Public Shared Function Graphics_IsGraphicExtension(ByVal ext As String) As Boolean
        ext = ext.ToLower
        If ext = "jpg" Or ext = "jpeg" Or ext = "gif" Or ext = "png" Then
            Return True
        Else : Return False
        End If
    End Function


    ''' <summary>
    ''' Gibt die Teilzeichenfolge von Text-Parameter ab PosStart(inklusive) bis PosEnd(inklusive) 
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="startPos"></param>
    ''' <param name="endPos"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Function SubstringAnfangEnde(Text as String, PosStart as Integer, PosEnd as Integer) as String
    '''Text :          			|---------------------|
    '''
    '''1)						PosStart        PosEnd
    '''								 |------------|			Output:		PosS...PosE
    '''
    '''2)					   PosS        PosE
    '''							|------------|				Output:		PosS...PosE
    '''
    '''3)							   PosS        PosE
    '''									|------------|		Output:		PosS...PosE
    '''
    '''	Other cases:										Output:		""
    ''' </remarks>
    Public Shared Function SubstringAnfangEnde(ByVal text As String, ByVal startPos As Integer, ByVal endPos As Integer) As String
        'Dim ausgabe As String = ""
        If startPos < 0 Or endPos < 0 Or startPos >= text.Length Or endPos >= text.Length Then Return ""

        '### Diese variante ist langsam: z.B bei 100000 Zeichen wird die Schleife 100000 Mal durchgelaufen
        'For i As Integer = startPos To endPos
        '    ausgabe += text(i)
        'Next
        '###

        '### Dise Variante verbraucht enorm viel Speicher
        'If startPos = endPos Then
        '    ausgabe = text(startPos)
        'ElseIf startPos < endPos Then
        '    ausgabe = text.Substring(startPos, endPos - startPos + 1)
        'End If
        '###

        If startPos = endPos Then
            Return text(startPos)
        ElseIf startPos < endPos Then
            Dim ausgabeChars As Char() = text.ToCharArray(startPos, endPos - startPos + 1)
            Dim ausgabe As New String(ausgabeChars)
            Return ausgabe
        End If

        Return ""
    End Function

#Region "FileSystem procedures"

    Public Shared Function DirectoryExists(ByVal FullPath As String) As Boolean
        If String.IsNullOrEmpty(FullPath) Then Return False

        Dim di As New DirectoryInfo(FullPath)
        Return di.Exists
    End Function

    Public Shared Function FileExists(ByVal FullPath As String) As Boolean
        If FullPath.Trim = "" Then Return False
        Dim fi As New FileInfo(FullPath)
        Return fi.Exists
    End Function

    Public Shared Function DirectoryCreate(ByVal FullPath As String) As Boolean
        Dim di As New DirectoryInfo(FullPath)
        Try
            di.Create()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DirectoryGetFiles(ByVal FullPath As String, Optional ByVal recursive As System.IO.SearchOption = SearchOption.TopDirectoryOnly) As String()
        Dim ausgabe() As String = {}
        Dim di As DirectoryInfo
        Try
            di = New DirectoryInfo(FullPath)
        Catch ex As Exception
            Return ausgabe
        End Try
        If di.Exists Then
            Dim fis() As FileInfo = di.GetFiles("*", recursive)
            ReDim ausgabe(fis.Length - 1)
            For i As Integer = 0 To fis.Length - 1
                ausgabe(i) = fis(i).FullName
            Next
        End If
        Return ausgabe
    End Function

    Public Shared Function FileDelete(ByVal FullPath As String) As Boolean
        Dim fi As New FileInfo(FullPath)
        Try
            fi.Delete()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function FileWriteText(ByVal FullPath As String, ByVal text As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
        If encoding Is Nothing Then encoding = System.Text.Encoding.UTF8
        Try
            Using tr As New StreamWriter(FullPath, False, encoding)
                tr.Write(text)
                tr.Close()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region 'FileSystem procedures

#Region "Text Conversions"

    Public Shared Function TextConvertTexToPlain(ByVal text As String) As String
        Dim ausgabe As String = ""
        ausgabe = ReplaceTexTagsWithHtmlTags(text, True)
        Return ausgabe
    End Function

    Public Shared Function TextConvertTexToHtml(ByVal text As String) As String
        Dim ausgabe As String = ""
        ausgabe = ReplaceTexTagsWithHtmlTags(text)
        ausgabe = ausgabe.Replace(vbCrLf, "VBCRLF") 'note windows linebreacks
        ausgabe = ausgabe.Replace(vbCr, "VBCRLF") ' note unix linebreacks
        ausgabe = ausgabe.Replace("VBCRLF", "<br />" + vbCrLf)
        Return ausgabe
    End Function

    Public Shared Function ReplaceTexTagsWithHtmlTags(ByVal text As String, Optional ByVal onlyRemoveTexTags As Boolean = False) As String
        'Known leaks: if a curly bracket appears between start and end of some tag it mess up all regex
        Dim ausgabe As String = ""
        Dim texStartTag As String = "{"
        Dim texEndTag As String = "}"
        Dim texSUPTagPrefix As String = "\^"
        Dim htmlSUPTag As String = "<sup>"
        Dim htmlEndSUPTag As String = "</sup>"
        Dim texSUBTagPrefix As String = "_"
        Dim htmlSUBTag As String = "<sub>"
        Dim htmlEndSUBTag As String = "</sub>"

        If onlyRemoveTexTags Then
            htmlSUPTag = ""
            htmlEndSUPTag = ""
            htmlSUBTag = ""
            htmlEndSUBTag = ""
        End If
        '[]\^$.|?*+(): this special regex characters must be escaped with \


        Dim regSUPStr As String = "(" + texSUPTagPrefix + texStartTag + ")" + _
                               "[^" + texStartTag + "^" + texEndTag + "]+" + "(" + texEndTag + ")"
        Dim matchSUP As System.Text.RegularExpressions.Match = Regex.Match(text, regSUPStr)
        'the regular expression is: (\^{)[^{^}]+(})

        Dim regSUBStr As String = "(" + texSUBTagPrefix + texStartTag + ")" + _
                       "[^" + texStartTag + "^" + texEndTag + "]+" + "(" + texEndTag + ")"
        Dim matchSUB As System.Text.RegularExpressions.Match = Regex.Match(text, regSUBStr)
        'the regular expression is: (_{)[^{^}]+(})


        Dim count As Integer = 0 'schmelzsicherung
        While matchSUP.Success Or matchSUB.Success
            If matchSUP.Success Then
                'first replacing end tag to remain the position of the match for the start tag
                text = text.Remove(matchSUP.Groups(2).Index, matchSUP.Groups(2).Length)
                text = text.Insert(matchSUP.Groups(2).Index, htmlEndSUPTag)
                'then replacing the start tag
                text = text.Remove(matchSUP.Groups(1).Index, matchSUP.Groups(1).Length)
                text = text.Insert(matchSUP.Groups(1).Index, htmlSUPTag)
            Else 'it means matchSUB.Success
                'first replacing end tag to remain the position of the match for the start tag
                text = text.Remove(matchSUB.Groups(2).Index, matchSUB.Groups(2).Length)
                text = text.Insert(matchSUB.Groups(2).Index, htmlEndSUBTag)
                'then replacing the start tag
                text = text.Remove(matchSUB.Groups(1).Index, matchSUB.Groups(1).Length)
                text = text.Insert(matchSUB.Groups(1).Index, htmlSUBTag)
            End If


            matchSUP = Regex.Match(text, regSUPStr)
            matchSUB = Regex.Match(text, regSUBStr)

            count += 1
            If count > 1000 Then
                Throw New Exception("Closed loop!")
            End If
        End While

        count = 0 'schmelzsicherung

        Return text
    End Function

#End Region 'Text Conversions


    ''' <summary>
    ''' Wandelt den System.Web.UI.Control in HTML-Kode um.
    ''' </summary>
    ''' <param name="ControlToRender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetControlHtmlText(ByVal ControlToRender As Control) As String
        If ControlToRender Is Nothing Then Return ""
        Try
            Using tw As New System.IO.StringWriter

                Dim htw As New HtmlTextWriter(tw)
                ControlToRender.RenderControl(htw)

                Return tw.ToString
            End Using
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function CurrentWorkshopID() As Integer
        Dim ausgabe As Integer = 0

        Dim confID As String = ConfigurationManager.AppSettings(A_Names.A_CurrentWorkshopID)
        If Not String.IsNullOrEmpty(confID) Then
            Try
                ausgabe = CInt(confID)
            Catch ex As Exception
            End Try
        End If

        If ausgabe = 0 Then Throw New Exception("GlobFunctions.CurrentWorkshopID Error: can't retrive current workshopid from configuration file. Make sure you have the entry in the externalAppSettings.config (<add key=""CurrentWorkshopID"" value=""...""/>)")
        Return ausgabe
    End Function

    Public Shared Function LogIn(ByVal name As String, ByVal passwort As String) As Boolean
        If name Is Nothing Then name = ""
        If passwort Is Nothing Then passwort = ""
        Dim originName As String = ConfigurationManager.AppSettings(A_Names.A_agentName)
        Dim originPass As String = ConfigurationManager.AppSettings(A_Names.A_agentPasswort)
        Dim ipOK As Boolean = IPHasPermissionsToLogIn()
        Dim namepassOK As Boolean = (name.ToLower = originName.ToLower) And (passwort = originPass)
        If namepassOK And ipOK Then
            System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser) = "allesOK"
            Return True
        Else
            If Not ipOK Then
                ErrorReporting.ReportErrorToWebmaster("Versuch der Anmeldung mit ungültiger IP. Siehe ApplicationSettings." + A_Names.A_thisIPsHavePermissionsToLogIn)
            End If

            If Not namepassOK Then
                ErrorReporting.ReportErrorToWebmaster("Versuch der Anmeldung mit ungültigen Anmeldedaten")
            End If

            System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser) = ""
            Return False
        End If

    End Function

    Public Shared Function IPHasPermissionsToLogIn() As Boolean
        'die IP hängt der Apache Proxy Server an die Header unter dem Namen "HTTP_X_FORWARDED_FOR" an.
        Dim ip As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(ip) Then
            ip = System.Web.HttpContext.Current.Request.UserHostAddress
        End If

        Dim ips As String = MyAppSettings.thisIPsHavePermissionsToLogIn
        If String.IsNullOrEmpty(ips) Then Return True

        Dim arrayIPs As String() = ips.Replace(",", ";").Split(";")
        For Each s As String In arrayIPs
            If ip.StartsWith(s) Then Return True
        Next
        Return False
    End Function

    Public Shared Function LogedInUser() As String
        'Return "DEBUG"
        If Not RequestVonInnen() Then Return ""
        If System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser) Is Nothing Then
            System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser) = ""
        End If
        Return System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser)
    End Function

    ''' <summary>
    ''' Whenn ein windows-User eingelogt ist, wird er ausgelogt
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub LogOutUser()
        System.Web.HttpContext.Current.Session(S_Names.S_LogedInUser) = ""
    End Sub

    ''' <summary>
    ''' Überprüfung ob die Seite von innerhalb des Institutes angefordert wurde, oder aus dem Internet
    ''' </summary>
    Public Shared Function RequestVonInnen() As Boolean
        'wenn die Datenbank aus dem CPfS-Gebäude umzieht - dann request ist immer von innen
        If MyAppSettings.IsThisAppFeldmaessig Then
            Return True
        End If

        'aufruf über interne name des webservers ist immer von innen
        Dim url As String = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToLower
        If url.StartsWith("http://websrv") Or url.StartsWith("https://websrv") Then
            Return True
        End If


        'aufruf über externe adresse(z.B. http:\\www.cpfs.de) wird nach der IP unterschieden.
        'die IP hängt der Apache Proxy Server an die Header unter dem Namen "HTTP_X_FORWARDED_FOR" an.
        Dim ip As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If ip Is Nothing Then Return False
        Dim octets As String() = ip.Split(".")
        Dim octet3 As Integer
        Try
            octet3 = CInt(octets(2))
        Catch ex As Exception
            Return False
        End Try

        Dim octet4 As Integer
        Try
            octet4 = CInt(octets(3))
        Catch ex As Exception
            Return False
        End Try

        If ip.StartsWith("141.5.12.") Then 'Poweruser
            Return True
        ElseIf ip.StartsWith("141.5.15.") Then 'Apple
            Return True
        ElseIf ip.StartsWith("172.20.1.") Then 'Server
            Return True
        ElseIf ip.StartsWith("172.21.1.") Then 'Remote
            Return True
        ElseIf ip.StartsWith("172.22.1.") Then 'Transfer DFN
            Return True
        ElseIf ip.StartsWith("172.25.1.") Then 'LHS
            Return True
        ElseIf ip.StartsWith("172.30.1.") Then 'Management
            Return True
        ElseIf ip.StartsWith("10.90.24.") Then 'Verwaltung
            Return True
        ElseIf ip.StartsWith("172.23.") Then 'User
            If octet3 >= 0 And octet3 <= 63 Then
                Return True
            End If
        ElseIf ip.StartsWith("172.26.") Then 'Messtechnik
            If octet3 >= 0 And octet3 <= 3 Then
                Return True
            End If
        ElseIf ip.StartsWith("172.27.") Then 'Haustechnik
            If octet3 >= 0 And octet3 <= 3 Then
                Return True
            End If
        ElseIf ip.StartsWith("141.5.13.") Then 'VPN
            If octet4 >= 0 And octet4 <= 31 Then
                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Makes from the mail the mailto-hyperlink with some protection agains the email boots
    ''' </summary>
    ''' <param name="email"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MakeProtectedEmailLink(email As String) As String
        Dim part1 As String = ""
        Dim part2 As String = ""
        Dim parts As String() = email.Split("@")
        If parts.Length > 0 Then
            part1 = parts(0)
        End If
        If parts.Length > 1 Then
            part2 = parts(1)
        Else
            Return part1
        End If

        Dim ausgabe As String = "<nobr><a href=""mailto:" + part1 + """ onmouseover=""this.href='mailto:' + this.innerHTML + '@" + part2 + "';"">" + part1 + "</a>"
        ausgabe += "<a href=""mailto:" + part1 + """ onmouseover=""this.href='mailto:' + '" + part1 + "@' + '" + part2 + "';"">@</a>"
        ausgabe += "<a href=""mailto:" + part1 + """ onmouseover=""this.href='mailto:' + '" + part1 + "@' + this.innerHTML;"">" + part2 + "</a></nobr>"
        Return ausgabe
    End Function

#Region "Error reporting"
    Public Shared Sub WriteToLogFile(ByVal text As String, Optional ByVal append As Boolean = True)
        ErrorReporting.ReportErrorToWebmaster(text)
    End Sub
#End Region 'Error reporting

#Region "ASP.NET extensions"
    ''' <summary>
    ''' Durchsucht den root-control und alle darin geschachtelten Controls nach dem Control mit dem angegebenen Namen. Uses Tree-Traversal algorythms
    ''' </summary>
    Public Shared Function SuperFindControl(ByVal root As Control, ByVal name As String) As Control
        Dim node As Control = root
        Dim i As Integer
        If name = "" Then
            SuperFindControl = Nothing
            Exit Function
        End If

        While True
            While node.HasControls
                node = node.Controls.Item(0)
                If (node.ID = name) Then
                    Return node
                End If
            End While


            While True
                If node.Equals(root) Then
                    Return Nothing
                End If
                For i = 0 To (node.Parent.Controls.Count - 1)
                    If node.Parent.Controls.Item(i).Equals(node) Then
                        node = node.Parent
                        Exit For
                    End If
                Next

                If node.Controls.Count >= (i + 2) Then
                    node = node.Controls.Item(i + 1)
                    If node.ID = name Then
                        Return node
                    End If
                    Exit While
                End If
            End While
        End While
        Return Nothing ' nur dafür, dass VisualStudio nicht mekert
    End Function

    ''' <summary>
    ''' Durchsucht den root-control und alle darin geschachtelten Controls nach dem Control mit dem angegebenen Namen. Uses Recurcive calls
    ''' </summary>
    Public Shared Function SuperFindControlRecursive(ByVal root As Control, ByVal id As String) As Control
        If root.ID = id Then
            Return root
        End If
        If root.HasControls Then
            Dim temp As Control
            For Each subcontrol As Control In root.Controls

                temp = SuperFindControlRecursive(subcontrol, id)
                If Not temp Is Nothing Then
                    Return temp
                End If
            Next
        End If
        Return Nothing
    End Function
#End Region 'ASP.NET extensions
End Class
