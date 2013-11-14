Imports System
Imports System.ComponentModel
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections
Imports System.Collections.Specialized

Namespace WebControlCaptcha

    ''' <summary>
    ''' CAPTCHA image generation class
    ''' </summary>
    ''' <remarks>
    ''' Adapted from the excellent code at 
    ''' http://www.codeproject.com/aspnet/CaptchaImage.asp
    '''
    ''' Jeff Atwood
    ''' http://www.codinghorror.com/
    ''' </remarks>
    Public Class CaptchaImage

        Private _height As Integer
        Private _width As Integer
        Private _rand As Random
        Private _generatedAt As DateTime
        Private _randomText As String
        Private _randomTextLength As Integer
        Private _randomTextChars As String
        Private _fontFamilyName As String
        Private _fontWarp As FontWarpFactor
        Private _backgroundNoise As BackgroundNoiseLevel
        Private _lineNoise As LineNoiseLevel
        Private _guid As String
        Private _fontWhitelist As String

#Region "  Public Enums"

        ''' <summary>
        ''' Amount of random font warping to apply to rendered text
        ''' </summary>
        Public Enum FontWarpFactor
            None
            Low
            Medium
            High
            Extreme
        End Enum

        ''' <summary>
        ''' Amount of background noise to add to rendered image
        ''' </summary>
        Public Enum BackgroundNoiseLevel
            None
            Low
            Medium
            High
            Extreme
        End Enum

        ''' <summary>
        ''' Amount of curved line noise to add to rendered image
        ''' </summary>
        Public Enum LineNoiseLevel
            None
            Low
            Medium
            High
            Extreme
        End Enum

#End Region

#Region "  Public Properties"

        ''' <summary>
        ''' Returns a GUID that uniquely identifies this Captcha
        ''' </summary>
        Public ReadOnly Property UniqueId() As String
            Get
                Return _guid
            End Get
        End Property

        ''' <summary>
        ''' Returns the date and time this image was last rendered
        ''' </summary>
        Public ReadOnly Property RenderedAt() As DateTime
            Get
                Return _generatedAt
            End Get
        End Property

        ''' <summary>
        ''' Font family to use when drawing the Captcha text. If no font is provided, a random font will be chosen from the font whitelist for each character.
        ''' </summary>
        Public Property Font() As String
            Get
                Return _fontFamilyName
            End Get
            Set(ByVal Value As String)
                Try
                    Dim font1 As Font = New Font(Value, 12.0!)
                    _fontFamilyName = Value
                    font1.Dispose()
                Catch ex As Exception
                    _fontFamilyName = Drawing.FontFamily.GenericSerif.Name
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Amount of random warping to apply to the Captcha text.
        ''' </summary>
        Public Property FontWarp() As FontWarpFactor
            Get
                Return _fontWarp
            End Get
            Set(ByVal Value As FontWarpFactor)
                _fontWarp = Value
            End Set
        End Property

        ''' <summary>
        ''' Amount of background noise to apply to the Captcha image.
        ''' </summary>
        Public Property BackgroundNoise() As BackgroundNoiseLevel
            Get
                Return _backgroundNoise
            End Get
            Set(ByVal Value As BackgroundNoiseLevel)
                _backgroundNoise = Value
            End Set
        End Property

        Public Property LineNoise() As LineNoiseLevel
            Get
                Return _lineNoise
            End Get
            Set(ByVal value As LineNoiseLevel)
                _lineNoise = value
            End Set
        End Property

        ''' <summary>
        ''' A string of valid characters to use in the Captcha text. 
        ''' A random character will be selected from this string for each character.
        ''' </summary>
        Public Property TextChars() As String
            Get
                Return _randomTextChars
            End Get
            Set(ByVal Value As String)
                _randomTextChars = Value
                _randomText = GenerateRandomText()
            End Set
        End Property

        ''' <summary>
        ''' Number of characters to use in the Captcha text. 
        ''' </summary>
        Public Property TextLength() As Integer
            Get
                Return _randomTextLength
            End Get
            Set(ByVal Value As Integer)
                _randomTextLength = Value
                _randomText = GenerateRandomText()
            End Set
        End Property

        ''' <summary>
        ''' Returns the randomly generated Captcha text.
        ''' </summary>
        Public ReadOnly Property [Text]() As String
            Get
                Return _randomText
            End Get
        End Property

        ''' <summary>
        ''' Width of Captcha image to generate, in pixels 
        ''' </summary>
        Public Property Width() As Integer
            Get
                Return _width
            End Get
            Set(ByVal Value As Integer)
                If (Value <= 60) Then
                    Throw New ArgumentOutOfRangeException("width", Value, "width must be greater than 60.")
                End If
                _width = Value
            End Set
        End Property

        ''' <summary>
        ''' Height of Captcha image to generate, in pixels 
        ''' </summary>
        Public Property Height() As Integer
            Get
                Return _height
            End Get
            Set(ByVal Value As Integer)
                If Value <= 30 Then
                    Throw New ArgumentOutOfRangeException("height", Value, "height must be greater than 30.")
                End If
                _height = Value
            End Set
        End Property

        ''' <summary>
        ''' A semicolon-delimited list of valid fonts to use when no font is provided.
        ''' </summary>
        Public Property FontWhitelist() As String
            Get
                Return _fontWhitelist
            End Get
            Set(ByVal value As String)
                _fontWhitelist = value
            End Set
        End Property

#End Region

        Public Sub New()
            _rand = New Random
            _fontWarp = FontWarpFactor.Low
            _backgroundNoise = BackgroundNoiseLevel.Low
            _lineNoise = LineNoiseLevel.None
            _width = 180
            _height = 50
            _randomTextLength = 5
            _randomTextChars = "ACDEFGHJKLNPQRTUVXYZ2346789"
            _fontFamilyName = ""
            ' -- a list of known good fonts in on both Windows XP and Windows Server 2003
            _fontWhitelist = _
                "arial;arial black;comic sans ms;courier new;estrangelo edessa;franklin gothic medium;" & _
                "georgia;lucida console;lucida sans unicode;mangal;microsoft sans serif;palatino linotype;" & _
                "sylfaen;tahoma;times new roman;trebuchet ms;verdana"
            _randomText = GenerateRandomText()
            _generatedAt = DateTime.Now
            _guid = Guid.NewGuid.ToString()
        End Sub

        ''' <summary>
        ''' Forces a new Captcha image to be generated using current property value settings.
        ''' </summary>
        Public Function RenderImage() As Bitmap
            Return GenerateImagePrivate()
        End Function

        ''' <summary>
        ''' Returns a random font family from the font whitelist
        ''' </summary>
        Private Function RandomFontFamily() As String
            Static ff() As String
            '-- small optimization so we don't have to split for each char
            If ff Is Nothing Then
                ff = _fontWhitelist.Split(";"c)
            End If
            Return ff(_rand.Next(0, ff.Length))
        End Function

        ''' <summary>
        ''' generate random text for the CAPTCHA
        ''' </summary>
        Private Function GenerateRandomText() As String
            Dim sb As New System.Text.StringBuilder(_randomTextLength)
            Dim maxLength As Integer = _randomTextChars.Length
            For n As Integer = 0 To _randomTextLength - 1
                sb.Append(_randomTextChars.Substring(_rand.Next(maxLength), 1))
            Next
            Return sb.ToString
        End Function

        ''' <summary>
        ''' Returns a random point within the specified x and y ranges
        ''' </summary>
        Private Function RandomPoint(ByVal xmin As Integer, ByVal xmax As Integer, ByRef ymin As Integer, ByRef ymax As Integer) As PointF
            Return New PointF(_rand.Next(xmin, xmax), _rand.Next(ymin, ymax))
        End Function

        ''' <summary>
        ''' Returns a random point within the specified rectangle
        ''' </summary>
        Private Function RandomPoint(ByVal rect As Rectangle) As PointF
            Return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom)
        End Function

        ''' <summary>
        ''' Returns a GraphicsPath containing the specified string and font
        ''' </summary>
        Private Function TextPath(ByVal s As String, ByVal f As Font, ByVal r As Rectangle) As GraphicsPath
            Dim sf As StringFormat = New StringFormat
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Near
            Dim gp As GraphicsPath = New GraphicsPath
            gp.AddString(s, f.FontFamily, CType(f.Style, Integer), f.Size, r, sf)
            Return gp
        End Function

        ''' <summary>
        ''' Returns the CAPTCHA font in an appropriate size 
        ''' </summary>
        Private Function GetFont() As Font
            Dim fsize As Single
            Dim fname As String = _fontFamilyName
            If fname = "" Then
                fname = RandomFontFamily()
            End If
            Select Case Me.FontWarp
                Case FontWarpFactor.None
                    fsize = Convert.ToInt32(_height * 0.7)
                Case FontWarpFactor.Low
                    fsize = Convert.ToInt32(_height * 0.8)
                Case FontWarpFactor.Medium
                    fsize = Convert.ToInt32(_height * 0.85)
                Case FontWarpFactor.High
                    fsize = Convert.ToInt32(_height * 0.9)
                Case FontWarpFactor.Extreme
                    fsize = Convert.ToInt32(_height * 0.95)
            End Select
            Return New Font(fname, fsize, FontStyle.Bold)
        End Function

        ''' <summary>
        ''' Renders the CAPTCHA image
        ''' </summary>
        Private Function GenerateImagePrivate() As Bitmap
            Dim fnt As Font = Nothing
            Dim rect As Rectangle
            Dim br As Brush
            Dim bmp As Bitmap = New Bitmap(_width, _height, PixelFormat.Format32bppArgb)
            Dim gr As Graphics = Graphics.FromImage(bmp)
            gr.SmoothingMode = SmoothingMode.AntiAlias

            '-- fill an empty white rectangle
            rect = New Rectangle(0, 0, _width, _height)
            br = New SolidBrush(Color.White)
            gr.FillRectangle(br, rect)

            Dim charOffset As Integer = 0
            Dim charWidth As Double = _width / _randomTextLength
            Dim rectChar As Rectangle

            For Each c As Char In _randomText
                '-- establish font and draw area
                fnt = GetFont()
                rectChar = New Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), _height)

                '-- warp the character
                Dim gp As GraphicsPath = TextPath(c, fnt, rectChar)
                WarpText(gp, rectChar)

                '-- draw the character
                br = New SolidBrush(Color.Black)
                gr.FillPath(br, gp)

                charOffset += 1
            Next

            AddNoise(gr, rect)
            AddLine(gr, rect)

            '-- clean up unmanaged resources
            fnt.Dispose()
            br.Dispose()
            gr.Dispose()

            Return bmp
        End Function

        ''' <summary>
        ''' Warp the provided text GraphicsPath by a variable amount
        ''' </summary>
        Private Sub WarpText(ByVal textPath As GraphicsPath, ByVal rect As Rectangle)
            Dim WarpDivisor As Single
            Dim RangeModifier As Single

            Select Case _fontWarp
                Case FontWarpFactor.None
                    Return
                Case FontWarpFactor.Low
                    WarpDivisor = 6
                    RangeModifier = 1
                Case FontWarpFactor.Medium
                    WarpDivisor = 5
                    RangeModifier = 1.3
                Case FontWarpFactor.High
                    WarpDivisor = 4.5
                    RangeModifier = 1.4
                Case FontWarpFactor.Extreme
                    WarpDivisor = 4
                    RangeModifier = 1.5
            End Select

            Dim rectF As RectangleF
            rectF = New RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height)

            Dim hrange As Integer = Convert.ToInt32(rect.Height / WarpDivisor)
            Dim wrange As Integer = Convert.ToInt32(rect.Width / WarpDivisor)
            Dim left As Integer = rect.Left - Convert.ToInt32(wrange * RangeModifier)
            Dim top As Integer = rect.Top - Convert.ToInt32(hrange * RangeModifier)
            Dim width As Integer = rect.Left + rect.Width + Convert.ToInt32(wrange * RangeModifier)
            Dim height As Integer = rect.Top + rect.Height + Convert.ToInt32(hrange * RangeModifier)

            If left < 0 Then left = 0
            If top < 0 Then top = 0
            If width > Me.Width Then width = Me.Width
            If height > Me.Height Then height = Me.Height

            Dim leftTop As PointF = RandomPoint(left, left + wrange, top, top + hrange)
            Dim rightTop As PointF = RandomPoint(width - wrange, width, top, top + hrange)
            Dim leftBottom As PointF = RandomPoint(left, left + wrange, height - hrange, height)
            Dim rightBottom As PointF = RandomPoint(width - wrange, width, height - hrange, height)

            Dim points As PointF() = New PointF() {leftTop, rightTop, leftBottom, rightBottom}
            Dim m As New Matrix
            m.Translate(0, 0)
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0)
        End Sub


        ''' <summary>
        ''' Add a variable level of graphic noise to the image
        ''' </summary>
        Private Sub AddNoise(ByVal graphics1 As Graphics, ByVal rect As Rectangle)
            Dim density As Integer
            Dim size As Integer

            Select Case _backgroundNoise
                Case BackgroundNoiseLevel.None
                    Return
                Case BackgroundNoiseLevel.Low
                    density = 30
                    size = 40
                Case BackgroundNoiseLevel.Medium
                    density = 18
                    size = 40
                Case BackgroundNoiseLevel.High
                    density = 16
                    size = 39
                Case BackgroundNoiseLevel.Extreme
                    density = 12
                    size = 38
            End Select

            Dim br As New SolidBrush(Color.Black)
            Dim max As Integer = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size)

            For i As Integer = 0 To Convert.ToInt32((rect.Width * rect.Height) / density)
                graphics1.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height), _
                    _rand.Next(max), _rand.Next(max))
            Next
            br.Dispose()
        End Sub

        ''' <summary>
        ''' Add variable level of curved lines to the image
        ''' </summary>
        Private Sub AddLine(ByVal graphics1 As Graphics, ByVal rect As Rectangle)

            Dim length As Integer
            Dim width As Single
            Dim linecount As Integer

            Select Case _lineNoise
                Case LineNoiseLevel.None
                    Return
                Case LineNoiseLevel.Low
                    length = 4
                    width = Convert.ToSingle(_height / 31.25) ' 1.6
                    linecount = 1
                Case LineNoiseLevel.Medium
                    length = 5
                    width = Convert.ToSingle(_height / 27.7777) ' 1.8
                    linecount = 1
                Case LineNoiseLevel.High
                    length = 3
                    width = Convert.ToSingle(_height / 25) ' 2.0
                    linecount = 2
                Case LineNoiseLevel.Extreme
                    length = 3
                    width = Convert.ToSingle(_height / 22.7272) ' 2.2
                    linecount = 3
            End Select

            Dim pf(length) As PointF
            Dim p As New Pen(Color.Black, width)

            For l As Integer = 1 To linecount
                For i As Integer = 0 To length
                    pf(i) = RandomPoint(rect)
                Next
                graphics1.DrawCurve(p, pf, 1.75)
            Next

            p.Dispose()
        End Sub

    End Class 'CaptcheImage

    ''' <summary>
    ''' Captcha image stream HttpModule. Retrieves CAPTCHA objects from cache, renders them to memory, 
    ''' and streams them to the browser.
    ''' </summary>
    ''' <remarks>
    ''' You *MUST* enable this HttpHandler in your web.config, like so:
    '''
    '''	  &lt;httpHandlers&gt;
    '''		  &lt;add verb="GET" path="CaptchaImage.aspx" type="WebControlCaptcha.CaptchaImageHandler, WebControlCaptcha" /&gt;
    '''	  &lt;/httpHandlers&gt;
    '''
    ''' Jeff Atwood
    ''' http://www.codinghorror.com/
    '''</remarks>
    Public Class CaptchaImageHandler
        Implements IHttpHandler

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
            Dim app As HttpApplication = context.ApplicationInstance

            '-- get the unique GUID of the captcha; this must be passed in via the querystring
            Dim guid As String = app.Request.QueryString("guid")
            Dim ci As CaptchaImage = Nothing

            If guid <> "" Then
                If String.IsNullOrEmpty(app.Request.QueryString("s")) Then
                    ci = CType(HttpRuntime.Cache.Get(guid), CaptchaImage)
                Else
                    ci = CType(HttpContext.Current.Session.Item(guid), CaptchaImage)
                End If

            End If

            If ci Is Nothing Then
                app.Response.StatusCode = 404
                context.ApplicationInstance.CompleteRequest()
                Return
            End If

            '-- write the image to the HTTP output stream as an array of bytes
            Dim b As Bitmap = ci.RenderImage
            b.Save(app.Context.Response.OutputStream, Drawing.Imaging.ImageFormat.Jpeg)
            b.Dispose()
            app.Response.ContentType = "image/jpeg"
            app.Response.StatusCode = 200
            context.ApplicationInstance.CompleteRequest()
        End Sub

        Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return True
            End Get
        End Property

    End Class 'CaptchaImageHandler

    ''' <summary>
    ''' CAPTCHA ASP.NET 2.0 user control
    ''' </summary>
    ''' <remarks>
    ''' add a reference to this DLL and add the CaptchaControl to your toolbox;
    ''' then just drag and drop the control on a web form and set properties on it.
    '''
    ''' Jeff Atwood
    ''' http://www.codinghorror.com/
    ''' </remarks>
    <DefaultProperty("Text")> _
    Public Class CaptchaControl
        Inherits System.Web.UI.WebControls.WebControl
        Implements INamingContainer
        Implements IPostBackDataHandler
        Implements IValidator

#Region "Strings"
        'Public Const S_DOESNT_MATCH As String = "The code you typed does not match the code in the image."
        Public Const S_DOESNT_MATCH As String = "Die eingegebenen Zeichen stimmen nicht mit dem Bild überein. Bitte versuchen Sie es erneut."
        'Public Const S_CODE_EXPIRED As String = "The code you typed has expired. Please try again."
        Public Const S_CODE_EXPIRED As String = "Der Code auf dem Bild ist abgelaufen. Bitte versuchen Sie es erneut."
        'Public Const S_TOO_QUICKLY_1 As String = "Code was typed too quickly. Wait at least"
        'Public Const S_TOO_QUICKLY_2 As String = "seconds"
        Public Const S_TOO_QUICKLY_1 As String = "Sie haben den Code zu schnell eingegeben. Bitte warten Sie mindestens"
        Public Const S_TOO_QUICKLY_2 As String = "Sekunden."

#End Region

        Public Enum Layout
            Horizontal
            Vertical
        End Enum

        Public Enum CacheType
            HttpRuntime
            Session
        End Enum

        Private _timeoutSecondsMax As Integer = 90
        Private _timeoutSecondsMin As Integer = 3
        Private _userValidated As Boolean = True
        Private _text As String = "Enter the code shown:"
        Private _font As String = ""
        Private _captcha As CaptchaImage = New CaptchaImage
        Private _layoutStyle As Layout = Layout.Horizontal
        Private _prevguid As String
        Private _errorMessage As String = ""
        Private _cacheStrategy As CacheType = CacheType.HttpRuntime

#Region "  Public Properties"

        <Browsable(False), _
        Bindable(True), _
        Category("Appearance"), _
        DefaultValue("The text you typed does not match the text in the image."), _
        Description("Message to display in a Validation Summary when the CAPTCHA fails to validate.")> _
        Public Property ErrorMessage() As String Implements System.Web.UI.IValidator.ErrorMessage
            Get
                If Not _userValidated Then
                    Return _errorMessage
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                _errorMessage = value
            End Set
        End Property

        <Browsable(False), _
        Category("Behavior"), _
        DefaultValue(True), _
        Description("Is Valid"), _
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property IsValid() As Boolean Implements System.Web.UI.IValidator.IsValid
            Get
                Return _userValidated
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property

        Public Overrides Property Enabled() As Boolean
            Get
                Return MyBase.Enabled
            End Get
            Set(ByVal value As Boolean)
                MyBase.Enabled = value
                ' When a validator is disabled, generally, the intent is not to
                ' make the page invalid for that round trip.
                If Not value Then
                    _userValidated = True
                End If
            End Set
        End Property


        <DefaultValue("Enter the code shown above:"), _
        Description("Instructional text displayed next to CAPTCHA image."), _
        Category("Appearance")> _
        Public Property [Text]() As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property

        <DefaultValue(GetType(CaptchaControl.Layout), "Horizontal"), _
        Description("Determines if image and input area are displayed horizontally, or vertically."), _
        Category("Captcha")> _
        Public Property LayoutStyle() As Layout
            Get
                Return _layoutStyle
            End Get
            Set(ByVal Value As Layout)
                _layoutStyle = Value
            End Set
        End Property

        <DefaultValue(GetType(CaptchaControl.CacheType), "HttpRuntime"), _
        Description("Determines if CAPTCHA codes are stored in HttpRuntime (fast, but local to current server) or Session (more portable across web farms)."), _
        Category("Captcha")> _
        Public Property CacheStrategy() As CacheType
            Get
                Return _cacheStrategy
            End Get
            Set(ByVal value As CacheType)
                _cacheStrategy = value
            End Set
        End Property

        <Description("Returns True if the user was CAPTCHA validated after a postback."), _
        Category("Captcha")> _
        Public ReadOnly Property UserValidated() As Boolean
            Get
                Return _userValidated
            End Get
        End Property


        <DefaultValue(""), _
        Description("Font used to render CAPTCHA text. If font name is blank, a random font will be chosen."), _
        Category("Captcha")> _
        Public Property CaptchaFont() As String
            Get
                Return _font
            End Get
            Set(ByVal Value As String)
                _font = Value
                _captcha.Font = _font
            End Set
        End Property

        <DefaultValue(""), _
        Description("Characters used to render CAPTCHA text. A character will be picked randomly from the string."), _
        Category("Captcha")> _
        Public Property CaptchaChars() As String
            Get
                Return _captcha.TextChars
            End Get
            Set(ByVal Value As String)
                _captcha.TextChars = Value
            End Set
        End Property

        <DefaultValue(5), _
        Description("Number of CaptchaChars used in the CAPTCHA text"), _
        Category("Captcha")> _
        Public Property CaptchaLength() As Integer
            Get
                Return _captcha.TextLength
            End Get
            Set(ByVal Value As Integer)
                _captcha.TextLength = Value
            End Set
        End Property

        <DefaultValue(2), _
        Description("Minimum number of seconds CAPTCHA must be displayed before it is valid. If you're too fast, you must be a robot. Set to zero to disable."), _
        Category("Captcha")> _
        Public Property CaptchaMinTimeout() As Integer
            Get
                Return _timeoutSecondsMin
            End Get
            Set(ByVal Value As Integer)
                If Value > 15 Then
                    Throw New ArgumentOutOfRangeException("CaptchaTimeout", "Timeout must be less than 15 seconds. Humans aren't that slow!")
                End If
                _timeoutSecondsMin = Value
            End Set
        End Property

        <DefaultValue(90), _
        Description("Maximum number of seconds CAPTCHA will be cached and valid. If you're too slow, you may be a CAPTCHA hack attempt. Set to zero to disable."), _
        Category("Captcha")> _
        Public Property CaptchaMaxTimeout() As Integer
            Get
                Return _timeoutSecondsMax
            End Get
            Set(ByVal Value As Integer)
                If Value < 15 And Value <> 0 Then
                    Throw New ArgumentOutOfRangeException("CaptchaTimeout", "Timeout must be greater than 15 seconds. Humans can't type that fast!")
                End If
                _timeoutSecondsMax = Value
            End Set
        End Property

        <DefaultValue(50), _
        Description("Height of generated CAPTCHA image."), _
        Category("Captcha")> _
        Public Property CaptchaHeight() As Integer
            Get
                Return _captcha.Height
            End Get
            Set(ByVal Value As Integer)
                _captcha.Height = Value
            End Set
        End Property

        <DefaultValue(180), _
        Description("Width of generated CAPTCHA image."), _
        Category("Captcha")> _
        Public Property CaptchaWidth() As Integer
            Get
                Return _captcha.Width
            End Get
            Set(ByVal Value As Integer)
                _captcha.Width = Value
            End Set
        End Property

        <DefaultValue(GetType(CaptchaImage.FontWarpFactor), "Low"), _
        Description("Amount of random font warping used on the CAPTCHA text"), _
        Category("Captcha")> _
        Public Property CaptchaFontWarping() As CaptchaImage.FontWarpFactor
            Get
                Return _captcha.FontWarp
            End Get
            Set(ByVal Value As CaptchaImage.FontWarpFactor)
                _captcha.FontWarp = Value
            End Set
        End Property

        <DefaultValue(GetType(CaptchaImage.BackgroundNoiseLevel), "Low"), _
        Description("Amount of background noise to generate in the CAPTCHA image"), _
        Category("Captcha")> _
        Public Property CaptchaBackgroundNoise() As CaptchaImage.BackgroundNoiseLevel
            Get
                Return _captcha.BackgroundNoise
            End Get
            Set(ByVal Value As CaptchaImage.BackgroundNoiseLevel)
                _captcha.BackgroundNoise = Value
            End Set
        End Property

        <DefaultValue(GetType(CaptchaImage.LineNoiseLevel), "None"), _
        Description("Add line noise to the CAPTCHA image"), _
        Category("Captcha")> _
        Public Property CaptchaLineNoise() As CaptchaImage.LineNoiseLevel
            Get
                Return _captcha.LineNoise
            End Get
            Set(ByVal Value As CaptchaImage.LineNoiseLevel)
                _captcha.LineNoise = Value
            End Set
        End Property
#End Region

        Public Sub Validate() Implements System.Web.UI.IValidator.Validate
            '-- a no-op, since we validate in LoadPostData
        End Sub

        Private Function GetCachedCaptcha(ByVal guid As String) As CaptchaImage
            If _cacheStrategy = CacheType.HttpRuntime Then
                Return CType(HttpRuntime.Cache.Get(guid), CaptchaImage)
            Else
                Return CType(HttpContext.Current.Session.Item(guid), CaptchaImage)
            End If
        End Function

        Private Sub RemoveCachedCaptcha(ByVal guid As String)
            If _cacheStrategy = CacheType.HttpRuntime Then
                HttpRuntime.Cache.Remove(guid)
            Else
                HttpContext.Current.Session.Remove(guid)
            End If
        End Sub

        ''' <summary>
        ''' are we in design mode?
        ''' </summary>
        Private ReadOnly Property IsDesignMode() As Boolean
            Get
                Return HttpContext.Current Is Nothing
            End Get
        End Property

        ''' <summary>
        ''' Validate the user's text against the CAPTCHA text
        ''' </summary>
        Private Sub ValidateCaptcha(ByVal userEntry As String)

            If Not Visible Or Not Enabled Then
                _userValidated = True
                Return
            End If

            '-- retrieve the previous captcha from the cache to inspect its properties
            Dim ci As CaptchaImage = GetCachedCaptcha(_prevguid)
            If ci Is Nothing Then
                Me.ErrorMessage = "* " + S_CODE_EXPIRED
                _userValidated = False
                Return
            End If

            '--  was it entered too quickly?
            If Me.CaptchaMinTimeout > 0 Then
                If (ci.RenderedAt.AddSeconds(Me.CaptchaMinTimeout) > Now) Then
                    _userValidated = False
                    Me.ErrorMessage = "* " + S_TOO_QUICKLY_1 & Me.CaptchaMinTimeout & S_TOO_QUICKLY_2
                    RemoveCachedCaptcha(_prevguid)
                    Return
                End If
            End If

            If String.Compare(userEntry, ci.Text, True) <> 0 Then
                Me.ErrorMessage = "* " + S_DOESNT_MATCH
                _userValidated = False
                RemoveCachedCaptcha(_prevguid)
                Return
            End If

            _userValidated = True
            RemoveCachedCaptcha(_prevguid)
        End Sub

        ''' <summary>
        ''' returns HTML-ized color strings
        ''' </summary>
        Private Function HtmlColor(ByVal color As Drawing.Color) As String
            If color.IsEmpty Then Return ""
            If color.IsNamedColor Then
                Return color.ToKnownColor.ToString
            End If
            If color.IsSystemColor Then
                Return color.ToString
            End If
            Return "#" & color.ToArgb.ToString("x").Substring(2)
        End Function

        ''' <summary>
        ''' returns css "style=" tag for this control
        ''' based on standard control visual properties
        ''' </summary>
        Private Function CssStyle() As String
            Dim sb As New System.Text.StringBuilder
            Dim strColor As String

            With sb
                .Append(" style='")

                If BorderWidth.ToString.Length > 0 Then
                    .Append("border-width:")
                    .Append(BorderWidth.ToString)
                    .Append(";")
                End If
                If BorderStyle <> WebControls.BorderStyle.NotSet Then
                    .Append("border-style:")
                    .Append(BorderStyle.ToString)
                    .Append(";")
                End If
                strColor = HtmlColor(BorderColor)
                If strColor.Length > 0 Then
                    .Append("border-color:")
                    .Append(strColor)
                    .Append(";")
                End If

                strColor = HtmlColor(BackColor)
                If strColor.Length > 0 Then
                    .Append("background-color:" & strColor & ";")
                End If

                strColor = HtmlColor(ForeColor)
                If strColor.Length > 0 Then
                    .Append("color:" & strColor & ";")
                End If

                If Font.Bold Then
                    .Append("font-weight:bold;")
                End If

                If Font.Italic Then
                    .Append("font-style:italic;")
                End If

                If Font.Underline Then
                    .Append("text-decoration:underline;")
                End If

                If Font.Strikeout Then
                    .Append("text-decoration:line-through;")
                End If

                If Font.Overline Then
                    .Append("text-decoration:overline;")
                End If

                If Font.Size.ToString.Length > 0 Then
                    .Append("font-size:" & Font.Size.ToString & ";")
                End If

                If Font.Names.Length > 0 Then
                    Dim strFontFamily As String
                    .Append("font-family:")
                    For Each strFontFamily In Font.Names
                        .Append(strFontFamily)
                        .Append(",")
                    Next
                    .Length = .Length - 1
                    .Append(";")
                End If

                If Height.ToString <> "" Then
                    .Append("height:" & Height.ToString & ";")
                End If
                If Width.ToString <> "" Then
                    .Append("width:" & Width.ToString & ";")
                End If

                .Append("'")
            End With
            If sb.ToString = " style=''" Then
                Return ""
            Else
                Return sb.ToString
            End If
        End Function

        ''' <summary>
        ''' render raw control HTML to the page
        ''' </summary>
        Protected Overrides Sub Render(ByVal Output As HtmlTextWriter)
            With Output
                '-- master DIV
                .Write("<div")
                If CssClass <> "" Then
                    .Write(" class='" & CssClass & "'")
                End If
                .Write(CssStyle)
                .Write(">")

                '-- image DIV/SPAN
                If Me.LayoutStyle = Layout.Vertical Then
                    .Write("<div style='text-align:center;margin:5px;'>")
                Else
                    .Write("<span style='margin:5px;float:left;'>")
                End If
                '-- this is the URL that triggers the CaptchaImageHandler
                .Write("<img src=""CaptchaImage.aspx")
                If Not IsDesignMode Then
                    .Write("?guid=" & Convert.ToString(_captcha.UniqueId))
                End If
                If Me.CacheStrategy = CacheType.Session Then
                    .Write("&s=1")
                End If
                .Write(""" border='0'")
                If ToolTip.Length > 0 Then
                    .Write(" alt='" & ToolTip & "'")
                End If
                .Write(" width=" & _captcha.Width)
                .Write(" height=" & _captcha.Height)
                .Write(">")
                If Me.LayoutStyle = Layout.Vertical Then
                    .Write("</div>")
                Else
                    .Write("</span>")
                End If

                '-- text input and submit button DIV/SPAN
                If Me.LayoutStyle = Layout.Vertical Then
                    .Write("<div style='text-align:center;margin:5px;'>")
                Else
                    .Write("<span style='margin:5px;float:left;'>")
                End If
                If _text.Length > 0 Then
                    .Write(_text)
                    .Write("<br>")
                End If
                .Write("<input name=" & UniqueID & " type=text size=")
                .Write(_captcha.TextLength.ToString)
                .Write(" maxlength=")
                .Write(_captcha.TextLength.ToString)
                If AccessKey.Length > 0 Then
                    .Write(" accesskey=" & AccessKey)
                End If
                If Not Enabled Then
                    .Write(" disabled=""disabled""")
                End If
                If TabIndex > 0 Then
                    .Write(" tabindex=" & TabIndex.ToString)
                End If
                .Write(" value=''>")
                If Me.LayoutStyle = Layout.Vertical Then
                    .Write("</div>")
                Else
                    .Write("</span>")
                    .Write("<br clear='all'>")
                End If

                '-- closing tag for master DIV
                .Write("</div>")
            End With
        End Sub

        ''' <summary>
        ''' generate a new captcha and store it in the ASP.NET Cache by unique GUID
        ''' </summary>
        Private Sub GenerateNewCaptcha()
            If Not IsDesignMode Then
                If _cacheStrategy = CacheType.HttpRuntime Then
                    HttpRuntime.Cache.Add(_captcha.UniqueId, _captcha, Nothing, _
                        DateTime.Now.AddSeconds(Convert.ToDouble(IIf(Me.CaptchaMaxTimeout = 0, 90, Me.CaptchaMaxTimeout))), _
                        TimeSpan.Zero, Caching.CacheItemPriority.NotRemovable, Nothing)
                Else
                    HttpContext.Current.Session.Add(_captcha.UniqueId, _captcha)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Retrieve the user's CAPTCHA input from the posted data
        ''' </summary>
        Public Function LoadPostData(ByVal PostDataKey As String, ByVal Values As NameValueCollection) As Boolean Implements IPostBackDataHandler.LoadPostData
            ValidateCaptcha(Convert.ToString(Values(Me.UniqueID)))
            Return False
        End Function

        Public Sub RaisePostDataChangedEvent() Implements IPostBackDataHandler.RaisePostDataChangedEvent
        End Sub

        Protected Overrides Function SaveControlState() As Object
            Return CType(_captcha.UniqueId, Object)
        End Function

        Protected Overrides Sub LoadControlState(ByVal state As Object)
            If state IsNot Nothing Then
                _prevguid = CType(state, String)
            End If
        End Sub

        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            MyBase.OnInit(e)
            Page.RegisterRequiresControlState(Me)
            Page.Validators.Add(Me)

        End Sub

        Protected Overrides Sub OnUnload(ByVal e As System.EventArgs)
            If Not (Page Is Nothing) Then
                Page.Validators.Remove(Me)
            End If
            MyBase.OnUnload(e)
        End Sub

        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
            If Me.Visible Then
                GenerateNewCaptcha()
            End If
            MyBase.OnPreRender(e)
        End Sub

    End Class 'CaptchaControl

End Namespace