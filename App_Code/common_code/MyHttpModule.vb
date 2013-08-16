Public Class MyHttpModule
    Implements IHttpModule
    Implements IRequiresSessionState


    Private needSessionState As Boolean = False

    Public Sub Init(ByVal app As HttpApplication) _
       Implements IHttpModule.Init
        AddHandler app.BeginRequest, AddressOf MyBeginRequest
        AddHandler app.EndRequest, AddressOf MyEndRequest
    End Sub

    Public Sub Dispose() Implements IHttpModule.Dispose
        ' add clean-up code here if required
    End Sub

    Public Sub MyBeginRequest(ByVal s As Object, ByVal e As EventArgs)

        Dim filePfad As String = System.Web.HttpContext.Current.Request.FilePath
        'note the original request path
        System.Web.HttpContext.Current.Items.Item(C_Names.C_originalRequestedFile) = filePfad

        Dim physPfad As String = System.Web.HttpContext.Current.Server.MapPath(filePfad)
        Dim ext As String = GlobFunctions.FileExtension(filePfad)

        'If ext = "aspx" AndAlso Not GlobFunctions.FileExists(physPfad) Then
        '    Dim lastIndexOfBackSlash As Integer = physPfad.LastIndexOf("\")
        '    Dim fileName As String = GlobFunctions.SubstringAnfangEnde(physPfad, lastIndexOfBackSlash + 1, physPfad.Length - 1)
        '    Dim physDir As String = GlobFunctions.SubstringAnfangEnde(physPfad, 0, lastIndexOfBackSlash)


        '    If GlobFunctions.FileExists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.TrimEnd("\") + "\content\" + fileName) Then
        '        System.Web.HttpContext.Current.Server.Transfer(System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd("/") + "/content/" + fileName)
        '    End If

        'End If

        If ext = "html" Then
            Handle_html()
        ElseIf filePfad.EndsWith("CaptchaImage.aspx") Then
            Me.handle_captcha()
        ElseIf GlobFunctions.Graphics_IsGraphicExtension(ext) Then
            handle_graphic()
            'ElseIf ext.ToLower = "csv" Then
            '    Handle_csv()
        Else
            handle_other_files()
        End If
    End Sub


    Public Sub MyEndRequest(ByVal s As Object, ByVal e As EventArgs)
    End Sub

    ''' <summary>
    ''' Wenn eine html seite augerufen wird, die nicht existiert. Wird zu der jeweiligen aspx seite umgeleitet.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Handle_html()
        Dim rqst As HttpRequest = System.Web.HttpContext.Current.Request
        'note the original request path
        Dim origRequestedFile As String = System.Web.HttpContext.Current.Items.Item(C_Names.C_originalRequestedFile)

        Dim rspns As HttpResponse = System.Web.HttpContext.Current.Response
        Dim physPath As String
        Try
            physPath = rqst.PhysicalPath
        Catch ex As Exception
            physPath = ""
        End Try

        'transfering request to dispatcher.aspx, but maintaining session and querystring 
        '(we can't do it with ServerTransfer, we are doing it with RewritePath)
        If Not GlobFunctions.FileExists(physPath) Then
            If physPath.ToLower.EndsWith(".html") Then
                Dim aspxPhysFile As String = GlobFunctions.SubstringAnfangEnde(physPath, 0, physPath.Length - 5) + "aspx"
                If GlobFunctions.FileExists(aspxPhysFile) Then
                    Dim newPath As String = GlobFunctions.SubstringAnfangEnde(origRequestedFile, 0, origRequestedFile.Length - 5) + "aspx"
                    System.Web.HttpContext.Current.RewritePath(newPath, "", System.Web.HttpContext.Current.Request.Url.Query.TrimStart("?"))
                End If
            End If
        End If
    End Sub

    Private Sub handle_graphic()
        'it's all to avoid flackern on IE6
        Dim rqst As HttpRequest = System.Web.HttpContext.Current.Request
        Dim rspns As HttpResponse = System.Web.HttpContext.Current.Response

        Dim file As String = ""
        Try
            file = rqst.MapPath(rqst.Path)
        Catch ex As Exception
        End Try

        If GlobFunctions.FileExists(file) Then
            rspns.Clear()
            rspns.Cache.SetExpires(DateAndTime.DateAdd(DateInterval.Month, 1, Now))
            rspns.Cache.SetCacheability(HttpCacheability.Public)
            rspns.ContentType = "image/" + GlobFunctions.FileExtension(file)
            rspns.WriteFile(file)
            rspns.End()
        End If
    End Sub


    ''' <summary>
    ''' Retrieves CAPTCHA objects from cache, renders them to memory, 
    ''' and streams them to the browser.
    ''' </summary>
    ''' <remarks>
    ''' Jeff Atwood
    ''' http://www.codinghorror.com/
    '''</remarks>
    Private Sub handle_captcha()
        Dim app As HttpApplication = System.Web.HttpContext.Current.ApplicationInstance

        '-- get the unique GUID of the captcha; this must be passed in via the querystring
        Dim guid As String = app.Request.QueryString("guid")
        Dim ci As WebControlCaptcha.CaptchaImage = Nothing

        If guid <> "" Then
            If String.IsNullOrEmpty(app.Request.QueryString("s")) Then
                ci = CType(HttpRuntime.Cache.Get(guid), WebControlCaptcha.CaptchaImage)
            Else
                ci = CType(HttpContext.Current.Session.Item(guid), WebControlCaptcha.CaptchaImage)
            End If

        End If

        If ci Is Nothing Then
            app.Response.StatusCode = 404
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest()
            Return
        End If

        '-- write the image to the HTTP output stream as an array of bytes
        Dim b As System.Drawing.Bitmap = ci.RenderImage
        b.Save(app.Context.Response.OutputStream, Drawing.Imaging.ImageFormat.Jpeg)
        b.Dispose()
        app.Response.ContentType = "image/jpeg"
        app.Response.StatusCode = 200
        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub


    Private Sub handle_other_files()

    End Sub

End Class
