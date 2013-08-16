Imports Microsoft.VisualBasic
Imports System
'Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Design
Imports System.Threading
Imports System.Globalization
Imports System.Web.HttpContext

<Assembly: TagPrefix("BlogControl", "BlogControl")> 

Namespace CPFS

    Public Class VDBWaehrung
        Inherits Control
        Implements INamingContainer
        Implements IValidator

        Private isValid_ As Boolean = True
        Public Property IsValid() As Boolean Implements IValidator.IsValid
            Get
                Return isValid_
            End Get
            Set(ByVal value As Boolean)
                isValid_ = value
            End Set
        End Property

        Public Property ErrorMessage() As String Implements IValidator.ErrorMessage
            Get
                Return "Only numbers allowed"
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property Text() As String
            Get
                leftNumber_.Text = leftNumber_.Text.Trim
                rightNumber_.Text = rightNumber_.Text.Trim

                For i As Integer = 0 To leftNumber_.Text.Length - 1
                    If leftNumber_.Text(i) > "9" Or leftNumber_.Text(i) < "0" Then Return ""
                Next
                For i As Integer = 0 To rightNumber_.Text.Length - 1
                    If rightNumber_.Text(i) > "9" Or rightNumber_.Text(i) < "0" Then Return ""
                Next
                If leftNumber_.Text = "" Then leftNumber_.Text = "0"
                If rightNumber_.Text = "" Then rightNumber_.Text = "0"
                Return leftNumber_.Text + "," + rightNumber_.Text
            End Get
            Set(ByVal value As String)
                message.Text = value
                Dim trennZeichen As String = "#"
                Dim leftText, rightText As String

                For i As Integer = value.Length - 1 To 0 Step -1
                    If value(i) = "." Then
                        trennZeichen = "."
                        Exit For
                    End If
                    If value(i) = "," Then
                        trennZeichen = ","
                        Exit For
                    End If
                Next
                If trennZeichen = "#" Then
                    leftText = value
                    rightText = "00"
                Else
                    If value.Split(trennZeichen).Length > 2 Then
                        leftNumber_.Text = ""
                        rightNumber_.Text = ""
                        Exit Property
                    Else
                        leftText = value.Split(trennZeichen)(0)
                        rightText = value.Split(trennZeichen)(1)
                    End If
                End If

                leftText = leftText.Replace(".", "")
                leftText = leftText.Replace(",", "")

                For i As Integer = 0 To leftText.Length - 1
                    If leftText(i) > "9" Or leftText(i) < "0" Then
                        leftNumber_.Text = ""
                        rightNumber_.Text = ""
                        Exit Property
                    End If
                Next
                For i As Integer = 0 To rightText.Length - 1
                    If rightText(i) > "9" Or rightText(i) < "0" Then
                        leftNumber_.Text = ""
                        rightNumber_.Text = ""
                        Exit Property
                    End If
                Next

                If rightText.Length > 2 Then
                    rightText = GlobFunctions.SubstringAnfangEnde(rightText, 0, 1)
                End If

                rightNumber_.Text = rightText.Trim
                leftNumber_.Text = leftText.Trim
                If rightNumber_.Text = "" Then rightNumber_.Text = "0"
                If leftNumber_.Text = "" Then leftNumber_.Text = "0"
            End Set
        End Property

        Public Property Enabled() As Boolean
            Get
                Return Me._enabled
            End Get
            Set(ByVal value As Boolean)
                Me._enabled = value
                Me.leftNumber_.Enabled = value
                Me.rightNumber_.Enabled = value
            End Set
        End Property
        Private _enabled As Boolean = False

        Public Property ForeColor() As System.Drawing.Color
            Get
                Return Me._foreColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                Me._foreColor = value
                Me.leftNumber_.ForeColor = value
                Me.rightNumber_.ForeColor = value
            End Set
        End Property
        Private _foreColor As System.Drawing.Color = Drawing.Color.Black

        Public Property BackColor() As System.Drawing.Color
            Get
                Return Me._backColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                Me._backColor = value
                Me.leftNumber_.BackColor = value
                Me.rightNumber_.BackColor = value
            End Set
        End Property
        Private _backColor As System.Drawing.Color = Drawing.Color.White


        Private fontBold_ As Boolean
        Public Property FontBold() As Boolean
            Get
                Return fontBold_
            End Get
            Set(ByVal value As Boolean)
                fontBold_ = value
                leftNumber_.Font.Bold = value
                rightNumber_.Font.Bold = value
            End Set
        End Property

        Private contentRequired_ As Boolean
        Public Property ContentRequired() As Boolean
            Get
                Return contentRequired_
            End Get
            Set(ByVal value As Boolean)
                contentRequired_ = value
            End Set
        End Property

        Sub Validate() Implements IValidator.Validate
            isValid_ = True
            For i As Integer = 0 To leftNumber_.Text.Length - 1
                If leftNumber_.Text(i) > "9" Or leftNumber_.Text(i) < "0" Then
                    isValid_ = False
                    leftNumber_.BackColor = Drawing.Color.Orange
                    Exit For
                Else
                    leftNumber_.BackColor = Me._backColor
                End If
            Next
            For i As Integer = 0 To rightNumber_.Text.Length - 1
                If rightNumber_.Text(i) > "9" Or rightNumber_.Text(i) < "0" Then
                    isValid_ = False
                    rightNumber_.BackColor = Drawing.Color.Orange
                    Exit For
                Else
                    rightNumber_.BackColor = Me._backColor
                End If
            Next
        End Sub

        Private leftNumber_ As New TextBox
        Private rightNumber_ As New TextBox
        Private message As New Label

        Sub New()
            leftNumber_.EnableViewState = True
            leftNumber_.MaxLength = 5
            leftNumber_.Columns = 5
            leftNumber_.Width = System.Web.UI.WebControls.Unit.Parse("3em")
            rightNumber_.EnableViewState = True
            rightNumber_.MaxLength = 2
            rightNumber_.Columns = 2
            rightNumber_.Width = System.Web.UI.WebControls.Unit.Parse("2em")
        End Sub

        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            MyBase.OnInit(e)
            Me.Page.Validators.Add(Me)
        End Sub

        Protected Overrides Sub CreateChildControls()
            Me.Controls.Add(leftNumber_)
            Me.Controls.Add(New LiteralControl(","))
            Me.Controls.Add(rightNumber_)
            Me.Controls.Add(New LiteralControl(" EUR"))
        End Sub
    End Class

    Public Class VDBDate
        Inherits Control
        Implements INamingContainer
        Implements IPostBackDataHandler
        Implements IValidator

        Private Function loadpostbackdata(ByVal postdatakey As String, ByVal postcollection As Collections.Specialized.NameValueCollection) As Boolean Implements IPostBackDataHandler.LoadPostData
            Return False
        End Function

        Private _needValidation As Boolean = True
        Public Property NeedValidation As Boolean
            Get
                Return Me._needValidation
            End Get
            Set(ByVal value As Boolean)
                Me._needValidation = value
            End Set
        End Property

        Private isValid_ As Boolean = False
        Public Property IsValid() As Boolean Implements IValidator.IsValid
            Get
                If NeedValidation And Me.Page.IsPostBack Then
                    Return isValid_
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                isValid_ = value
            End Set
        End Property

        Public Property ErrorMessage() As String Implements IValidator.ErrorMessage
            Get
                Return "Date not valid"
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Private fontBold_ As Boolean
        Public Property FontBold() As Boolean
            Get
                Return fontBold_
            End Get
            Set(ByVal value As Boolean)
                fontBold_ = value
                ttb_.Font.Bold = value
                mtb_.Font.Bold = value
                jtb_.Font.Bold = value
            End Set
        End Property

        Private ttb_ As New TextBox
        Private mtb_ As New TextBox
        Private jtb_ As New TextBox
        Private ddl_ As New DropDownList

        Sub New()
            'ttb_.Text = ""
            ttb_.EnableViewState = True
            ttb_.MaxLength = 2
            ttb_.Columns = 2
            ttb_.Width = System.Web.UI.WebControls.Unit.Parse("2em")
            ttb_.ID = "day"
            mtb_.Text = ""
            mtb_.MaxLength = 2
            mtb_.Columns = 2
            mtb_.Width = System.Web.UI.WebControls.Unit.Parse("2em")
            mtb_.ID = "month"
            jtb_.Text = ""
            jtb_.MaxLength = 4
            jtb_.Columns = 4
            jtb_.Width = System.Web.UI.WebControls.Unit.Parse("3,5em")
            jtb_.ID = "year"

            Dim ab As New ListItem("Ab", "0")
            Dim bis As New ListItem("Bis", "1")
            Dim am As New ListItem("Am", "2")
            ddl_.Items.Add(ab)
            ddl_.Items.Add(bis)
            ddl_.Items.Add(am)
        End Sub

        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            MyBase.OnInit(e)
            Me.Page.Validators.Add(Me)
        End Sub

        Private text_ As String

        <Browsable(True), _
        Category("Behavior"), _
        Description(" Search Parameter, Day, Month, Year, separated by #-charakter. " & _
        "For Example: Ab#01#03#2008")> _
        Public Property text() As String
            Get
                CompleteJtb()
                Return ddl_.SelectedValue + "#" + ttb_.Text + "#" + mtb_.Text + "#" + jtb_.Text
            End Get

            Set(ByVal value As String)
                Dim str() As String = value.Split("#")
                For i As Integer = 0 To 3
                    If i < str.Length Then
                        Select Case i
                            Case 0
                                ddl_.SelectedValue = str(i)
                            Case 1
                                ttb_.Text = str(i)
                            Case 2
                                mtb_.Text = str(i)
                            Case 3
                                jtb_.Text = str(i)
                        End Select
                    Else
                        Select Case i
                            Case 0
                                ddl_.SelectedValue = "0"
                            Case 1
                                ttb_.Text = ""
                            Case 2
                                mtb_.Text = ""
                            Case 3
                                jtb_.Text = ""
                        End Select
                    End If
                Next
            End Set
        End Property

        Public Property Validated_text() As String
            Get
                Me.CompleteJtb()
                If Not (Me.DateValidate(ttb_.Text, mtb_.Text, jtb_.Text)) Then
                    Validated_text = ""
                ElseIf ttb_.Text = "" Or mtb_.Text = "" Or jtb_.Text = "" Then
                    Validated_text = ""
                Else
                    Validated_text = ttb_.Text + "." + mtb_.Text + "." + jtb_.Text
                End If
            End Get
            Set(ByVal value As String)
                Dim d As Date = Nothing
                Try
                    d = CType(value, Date)
                Catch ex As Exception
                    ttb_.Text = ""
                    'ttb_.BackColor = Drawing.Color.Orange
                    mtb_.Text = ""
                    'mtb_.BackColor = Drawing.Color.Orange
                    jtb_.Text = ""
                    'jtb_.BackColor = Drawing.Color.Orange
                End Try
                If Not (d = Nothing) Then
                    ttb_.Text = d.Day.ToString
                    ttb_.BackColor = Drawing.Color.White
                    mtb_.Text = d.Month.ToString
                    mtb_.BackColor = Drawing.Color.White
                    jtb_.Text = d.Year.ToString
                    jtb_.BackColor = Drawing.Color.White
                End If
            End Set
        End Property

        Private contentRequired_ As Boolean
        Public Property ContentRequired() As Boolean
            Get
                Return contentRequired_
            End Get
            Set(ByVal value As Boolean)
                contentRequired_ = value
            End Set
        End Property

        Private useForSearch_ As Boolean
        Public Property UseForSearch() As Boolean
            Get
                Return useForSearch_
            End Get
            Set(ByVal value As Boolean)
                useForSearch_ = value
                ddl_.Visible = value
            End Set
        End Property

        Private defaultToday_ As Boolean
        Public Property DefaultToday() As Boolean
            Get
                Return defaultToday_
            End Get
            Set(ByVal value As Boolean)
                defaultToday_ = value
                If value = True Then
                    Dim d As Date = Date.Today
                    ttb_.Text = d.Day.ToString
                    mtb_.Text = d.Month.ToString
                    jtb_.Text = d.Year.ToString
                Else
                    ttb_.Text = ""
                    mtb_.Text = ""
                    jtb_.Text = ""
                End If
            End Set
        End Property

        Private searchAb_ As New Date
        Public ReadOnly Property SearchAb() As Date
            Get
                Return Me.getSearchAb
            End Get
        End Property

        Public ReadOnly Property searchAbStr() As String
            Get
                If Not UseForSearch Then
                    searchAb_ = Nothing
                    Return Nothing
                Else
                    Return getSearchAb.ToString("dd.MM.yyyy")
                End If
            End Get
        End Property

        Private searchBis_ As New Date
        Public ReadOnly Property SearchBis() As Date
            Get
                Return Me.getSearchBis
            End Get
        End Property

        Public ReadOnly Property searchBisStr() As String
            Get
                If Not UseForSearch Then
                    searchBis_ = Nothing
                    Return Nothing
                Else
                    Return getSearchBis.ToString("dd.MM.yyyy")
                End If
            End Get
        End Property

        Private datum_ As Date
        Public Property datum() As Date
            Get
                datum = datum_
            End Get
            Set(ByVal value As Date)
                datum_ = value
            End Set
        End Property

        Function getDaysOfMonth(ByVal Month As Integer, ByVal Year As Integer) As Integer
            Dim arrMonth() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

            If Month = "2" Then
                If IsLeapYear(Year) Then
                    getDaysOfMonth = 29
                Else
                    getDaysOfMonth = 28
                End If
            Else
                getDaysOfMonth = arrMonth(Month - 1)
            End If
        End Function

        Function IsLeapYear(ByVal intYear As Integer) As Boolean
            IsLeapYear = False
            If intYear Mod 4 = 0 Then
                If (intYear Mod 100 > 0) Or (intYear Mod 400 = 0) Then
                    IsLeapYear = True
                End If
            End If
        End Function

        Function DateValidate(ByVal tag As String, ByVal monat As String, ByVal jahr As String) As Boolean
            If Not (tag = "" And monat = "" And jahr = "") Then
                Dim tag_i As Integer = 0
                Dim monat_i As Integer = 0
                Dim jahr_i As Integer = 0

                Dim s As String
                Dim ch As Char

                'Jahr-TextBox
                DateValidate = False
                s = jahr
                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        DateValidate = False
                        Exit Function
                    Else : DateValidate = True
                    End If
                Next

                If DateValidate Then
                    If CType(s, Integer) > 1900 And CType(s, Integer) < 2100 Then
                        DateValidate = True
                        jahr_i = CType(s, Integer)
                    Else
                        DateValidate = False
                        Exit Function
                    End If
                End If

                'Monat-TextBox
                DateValidate = False
                s = monat
                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        DateValidate = False
                        Exit Function
                    Else : DateValidate = True
                    End If
                Next

                If DateValidate Then
                    If CType(s, Integer) > 0 And CType(s, Integer) < 13 Then
                        DateValidate = True
                        monat_i = CType(s, Integer)
                    Else
                        DateValidate = False
                        Exit Function
                    End If
                End If

                ' Tag-TextBox
                DateValidate = False
                s = tag

                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        DateValidate = False
                        Exit Function
                    Else : DateValidate = True
                    End If
                Next

                If DateValidate And (monat_i > 0) And (jahr_i > 0) Then
                    If CType(s, Integer) > 0 And CType(s, Integer) <= Me.getDaysOfMonth(monat_i, jahr_i) Then
                        DateValidate = True
                    Else
                        DateValidate = False
                        Exit Function
                    End If
                Else
                    DateValidate = False
                    Exit Function
                End If
            Else
                DateValidate = True
            End If
        End Function

        Sub TTValidate() Implements IValidator.Validate
            Dim ttb, mtb, jtb As String
            ttb = ttb_.Text
            mtb = mtb_.Text
            Me.CompleteJtb()
            jtb = jtb_.Text

            If Me.useForSearch_ Then
                If ttb = "" And mtb = "" And (jtb <> "") Then
                    ttb = "1"
                    mtb = "1"
                ElseIf ttb = "" And (mtb <> "") And (jtb <> "") Then
                    ttb = "1"
                End If
            End If

            If Not (ttb = "" And mtb = "" And jtb = "") Then
                Dim tag As Integer = 0
                Dim monat As Integer = 0
                Dim jahr As Integer = 0

                Dim s As String
                Dim ch As Char

                'Jahr-TextBox
                isValid_ = False
                s = jtb
                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        isValid_ = False
                        Exit For
                    Else : isValid_ = True
                    End If
                Next

                If isValid_ Then
                    If CType(s, Integer) > 1900 And CType(s, Integer) < 2100 Then
                        isValid_ = True
                    Else
                        isValid_ = False
                    End If
                End If

                If Not isValid_ Then
                    jtb_.BackColor = Drawing.Color.Orange
                Else
                    jtb_.BackColor = Drawing.Color.White
                    jahr = s
                End If

                'Monat-TextBox
                isValid_ = False
                s = mtb
                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        isValid_ = False
                        Exit For
                    Else : isValid_ = True
                    End If
                Next

                If isValid_ Then
                    If CType(s, Integer) > 0 And CType(s, Integer) < 13 Then
                        isValid_ = True
                    Else
                        isValid_ = False
                    End If
                End If

                If Not isValid_ Then
                    mtb_.BackColor = Drawing.Color.Orange
                Else
                    mtb_.BackColor = Drawing.Color.White
                    monat = s
                End If

                ' Tag-TextBox
                isValid_ = False
                s = ttb

                For Each ch In s
                    If ch < "0" Or ch > "9" Then
                        isValid_ = False
                        Exit For
                    Else : isValid_ = True
                    End If
                Next

                If isValid_ And Not (monat = 0) Then
                    If CType(s, Integer) > 0 And CType(s, Integer) <= Me.getDaysOfMonth(monat, jahr) Then
                        isValid_ = True
                    Else
                        isValid_ = False
                    End If
                End If

                If Not isValid_ Then
                    ttb_.BackColor = Drawing.Color.Orange
                Else
                    ttb_.BackColor = Drawing.Color.White
                    tag = s
                End If
            Else
                isValid_ = True
            End If

            If Me.DateValidate(ttb, mtb, jtb) Then
                isValid_ = True
            Else
                isValid_ = False
            End If

            If Me.ContentRequired Then
                If ttb_.Text = "" And mtb_.Text = "" And jtb_.Text = "" Then
                    ttb_.BackColor = Drawing.Color.Orange
                    mtb_.BackColor = Drawing.Color.Orange
                    jtb_.BackColor = Drawing.Color.Orange
                    isValid_ = False
                End If
            End If
        End Sub

        Protected Function getSearchBis() As Date
            Me.CompleteJtb()
            If Not UseForSearch Then
                searchBis_ = Nothing
                Return Nothing
            Else
                jtb_.Text = jtb_.Text.Trim
                mtb_.Text = mtb_.Text.Trim
                ttb_.Text = ttb_.Text.Trim
                If jtb_.Text = "" And mtb_.Text = "" And ttb_.Text = "" Then Return DateAndTime.DateSerial(1000, 1, 1)

                If jtb_.Text = "" Then
                    searchBis_ = DateAndTime.DateSerial(9999, 12, 31)
                    Return searchBis_
                ElseIf mtb_.Text = "" Then
                    If DateValidate("31", "12", jtb_.Text) Then
                        searchBis_ = DateAndTime.DateSerial(jtb_.Text, 12, 31)
                        Return searchBis_
                    Else

                        searchBis_ = DateAndTime.DateSerial(9999, 12, 31)
                        Return searchBis_
                    End If
                ElseIf ttb_.Text = "" Then
                    If DateValidate("1", mtb_.Text, jtb_.Text) Then
                        searchBis_ = DateAndTime.DateSerial(jtb_.Text, mtb_.Text, Me.getDaysOfMonth(CInt(mtb_.Text), CInt(jtb_.Text)))
                        Return searchBis_
                    Else
                        searchBis_ = DateAndTime.DateSerial(9999, 12, 31)
                        Return searchBis_
                    End If
                Else
                    If DateValidate(ttb_.Text, mtb_.Text, jtb_.Text) Then
                        If ddl_.SelectedValue = "0" Then
                            searchBis_ = DateAndTime.DateSerial(9999, 12, 31)
                            Return searchBis_
                        Else
                            searchBis_ = DateAndTime.DateSerial(jtb_.Text, mtb_.Text, ttb_.Text)
                            Return searchBis_
                        End If
                    Else
                        searchBis_ = DateAndTime.DateSerial(9999, 12, 31)
                        Return searchBis_
                    End If
                End If
            End If
        End Function

        Protected Function getSearchAb() As Date
            Me.CompleteJtb()
            If Not UseForSearch Then
                searchAb_ = Nothing
                Return Nothing
            Else
                jtb_.Text = jtb_.Text.Trim
                mtb_.Text = mtb_.Text.Trim
                ttb_.Text = ttb_.Text.Trim
                If jtb_.Text = "" And mtb_.Text = "" And ttb_.Text = "" Then Return DateAndTime.DateSerial(1000, 1, 1)

                If jtb_.Text = "" Then
                    searchAb_ = DateAndTime.DateSerial(1800, 1, 1)
                    mtb_.Text = ""
                    ttb_.Text = ""
                    Return searchAb_
                ElseIf mtb_.Text = "" Then
                    If DateValidate("1", "1", jtb_.Text) Then
                        searchAb_ = DateAndTime.DateSerial(jtb_.Text, 1, 1)
                        Return searchAb_
                    Else
                        mtb_.Text = ""
                        ttb_.Text = ""
                        jtb_.Text = ""
                        searchAb_ = DateAndTime.DateSerial(1800, 1, 1)
                        Return searchAb_
                    End If
                ElseIf ttb_.Text = "" Then
                    If DateValidate("1", mtb_.Text, jtb_.Text) Then
                        searchAb_ = DateAndTime.DateSerial(jtb_.Text, mtb_.Text, 1)
                        Return searchAb_
                    Else
                        mtb_.Text = ""
                        ttb_.Text = ""
                        jtb_.Text = ""
                        searchAb_ = DateAndTime.DateSerial(1800, 1, 1)
                        Return searchAb_
                    End If
                Else
                    If DateValidate(ttb_.Text, mtb_.Text, jtb_.Text) Then
                        If ddl_.SelectedValue = "1" Then
                            searchAb_ = DateAndTime.DateSerial(1800, 1, 1)
                            Return searchAb_
                        Else
                            searchAb_ = DateAndTime.DateSerial(jtb_.Text, mtb_.Text, ttb_.Text)
                            Return searchAb_
                        End If
                    Else
                        mtb_.Text = ""
                        ttb_.Text = ""
                        jtb_.Text = ""
                        searchAb_ = DateAndTime.DateSerial(1800, 1, 1)
                        Return searchAb_
                    End If
                End If
            End If
        End Function

        Public Function ComposeSqlStr(ByVal ColumnToQuery As String) As String
            Dim ausgabe As String = ""
            If ColumnToQuery.Trim = "" Then Return ausgabe
            If (SearchAb > DateAndTime.DateSerial(1000, 1, 1)) And (SearchBis > DateAndTime.DateSerial(1000, 1, 1)) Then
                ausgabe = " (((" + ColumnToQuery + " >= CONVERT(DateTime, '" + searchAbStr + "', 104)) AND (" + ColumnToQuery + " <= CONVERT(DateTime, '" + searchBisStr + "', 104))) "
                If (SearchAb > DateAndTime.DateSerial(1800, 1, 1)) And SearchBis = DateAndTime.DateSerial(9999, 12, 31) Then
                    ausgabe += " OR " + ColumnToQuery + " is NULL"
                End If
                ausgabe += ")"
            End If

            Return ausgabe
        End Function

        Protected Overrides Sub CreateChildControls()
            Me.Controls.Add(ddl_)
            Me.Controls.Add(New LiteralControl(" "))
            Me.Controls.Add(ttb_)
            Me.Controls.Add(New LiteralControl(" "))
            Me.Controls.Add(mtb_)
            Me.Controls.Add(New LiteralControl(" "))
            Me.Controls.Add(jtb_)
        End Sub

        Public Sub RaisePostDataChangedEvent() Implements IPostBackDataHandler.RaisePostDataChangedEvent

            ' Part of the IPostBackDataHandler contract.  Invoked if we ever returned true from the
            ' LoadPostData method (indicates that we want a change notification raised).  Since we
            ' always return false, this method is just a no-op.
        End Sub

        Private Sub CompleteJtb()
            Dim jahrvalid As Boolean = False
            Dim ch As Char

            For Each ch In jtb_.Text
                If ch < "0" Or ch > "9" Then
                    jahrvalid = False
                    Exit For
                Else : jahrvalid = True
                End If
            Next

            If jahrvalid Then
                If CType(jtb_.Text, Integer) < 100 Then
                    If CType(jtb_.Text, Integer) < 41 Then
                        jtb_.Text = (CType(jtb_.Text, Integer) + 2000).ToString
                    Else
                        jtb_.Text = (CType(jtb_.Text, Integer) + 1900).ToString
                    End If
                End If
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Methoden und Properties für das Verzeichnis für das Zwischenspeichern der Dateien für die Anwendung
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LokalCache
        Public Const LOCAL_CACHE_KEY As String = "local_cache"

        ''' <summary>
        ''' die Datei liegt im LocalCacheVerzeichnis. Dien zum ermitteln der letzten Löschzeit
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property Last_Clear_File() As String
            Get
                Return LocalCacheDirectory + "\.last_clear"
            End Get
        End Property

        ''' <summary>
        ''' Das Verzeichniss das zum zwischenspeichern von Dateien dient.
        ''' In der Regel befindet es sich lokal auf dem Webserver.
        ''' Die Einstellung kann in der Web.config der Anwendung in AppSettings unter dem Schlüssel "local_cache" geändert werden.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property LocalCacheDirectory() As String
            Get
                Dim ausgabe As String = ConfigurationManager.AppSettings(LOCAL_CACHE_KEY)
                If Not ausgabe Is Nothing Then
                    If Not GlobFunctions.DirectoryExists(ausgabe) Then
                        GlobFunctions.DirectoryCreate(ausgabe)
                    End If

                    Return ausgabe
                Else
                    Return ""
                End If
            End Get
        End Property

        ''' <summary>
        ''' URL zu dem Verzeichnis das zum zwischenspeichern von Dateien dient (ohne Slash am Ende).
        ''' URL wird wie folgt gebildet: ApplicationPath + "\" + "local_cache"
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property LocalCacheURL() As String
            Get
                Dim ap As String = Web.HttpContext.Current.Request.ApplicationPath
                If Not ap.EndsWith("/") Then ap += "/"
                ap += LOCAL_CACHE_KEY
                Return ap
            End Get
        End Property

        ''' <summary>
        ''' Ein virtueller Pfad, der "local_chache" Zeichenfolge enthält
        ''' wird zu dem physischen Pfad in dem virtuellem Verzeichniss umgewandelt
        ''' </summary>
        ''' <param name="url"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CacheURLToCachePhysDir(ByVal url As String) As String
            Dim ausgabe As String = ""
            If url.Contains("/" + LOCAL_CACHE_KEY) Then
                ausgabe = GlobFunctions.SubstringAnfangEnde(url, url.IndexOf(LOCAL_CACHE_KEY) + LOCAL_CACHE_KEY.Length, url.Length - 1)
                ausgabe = ausgabe.Replace("/", "\")
                ausgabe = LocalCacheDirectory + ausgabe
            End If
            Return ausgabe
        End Function

        ''' <summary>
        ''' Löscht im LocalCacheDirectory (rekursiv) ale Dateien und Verzeichnisse die älter sind
        ''' als im Web.config unter dem Schlüssel "cache_files_age" angegebener Wert
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearCache()
            Dim d As String = LocalCacheDirectory
            Dim currentDir As String = d

            Dim clearDuration As Integer = 5
            If lastClear() > clearDuration Then
                If Not GlobFunctions.FileDelete(Last_Clear_File) Then
                    Throw New Exception("CPFS.LocalCache.ClearCache Error: Kann nicht die temporäre Datei löschen " + Last_Clear_File)
                End If
            Else
                Exit Sub
            End If

            If GlobFunctions.DirectoryExists(d) Then
                Dim maxAge As Integer = 30
                Try
                    maxAge = CInt(ConfigurationManager.AppSettings("cache_files_age"))
                Catch ex As Exception
                End Try
                'veraltete Dateien löschen
                For Each f As String In GlobFunctions.DirectoryGetFiles(d, IO.SearchOption.AllDirectories)
                    Dim fi As New System.IO.FileInfo(f)
                    If DateAndTime.DateDiff(DateInterval.Minute, fi.CreationTime, Now) > maxAge Then
                        If Not GlobFunctions.FileDelete(f) Then
                            Throw New Exception("CPFS.LocalCache.ClearCache Error: Kann nicht die temporäre Datei löschen " + f)
                        End If
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Minutenanzahl seit dem die veraltete Dateien zuletzt gelöscht wurden
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function lastClear() As Integer
            If Not GlobFunctions.FileExists(Last_Clear_File) Then
                If Not GlobFunctions.FileWriteText(Last_Clear_File, "") Then
                    Throw New Exception("CPFS.LocalCache.LastClear Error: Kann nicht die temporäre Datei erstellen " + Last_Clear_File)
                End If
                Return 0
            End If

            Dim fi As New System.IO.FileInfo(Last_Clear_File)
            Dim jetzt As Date = Now
            Return DateAndTime.DateDiff(DateInterval.Minute, fi.CreationTime, jetzt)
        End Function
    End Class

    Public Class CSSImageGallery
        Inherits Control

        Private maxThumbWidthOrHeight As Integer = 200

        ''' <summary>
        ''' Subdirectory within the _imageSubDir where the thumbnails are stored.
        ''' </summary>
        ''' <remarks></remarks>
        Private Const _thumbsDirName As String = "thumbs"
        Private Const _divCSSClass As String = "photo"
        Private Const _horizCSSClass As String = "horiz"
        Private Const _vertCSSClass As String = "vert"
        Private Const _clearCSSClass As String = "clear"

        Private _imageSubDir As String = "images"
        ''' <summary>
        ''' Subdirectory within the current directory(where the page liegt) where the images a saved.
        ''' Please use simple name without slashes. Default value images
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImagesSubDir As String
            Get
                Return Me._imageSubDir
            End Get
            Set(ByVal value As String)
                Me._imageSubDir = value
            End Set
        End Property

        Private _pagePhysDirectory As String = ""
        Public ReadOnly Property PagePhysDirectory As String
            Get
                If Me._pagePhysDirectory <> "" Then
                    Return Me._pagePhysDirectory
                Else
                    Dim ausgabe As String = Me.Page.Request.PhysicalPath
                    Dim lastslash As Integer = ausgabe.LastIndexOf("\")
                    If lastslash > 0 Then
                        Me._pagePhysDirectory = ausgabe.Substring(0, lastslash)
                        Return Me._pagePhysDirectory
                    Else
                        Throw New Exception("ToDo CSSImageGallery control.pagePhysDir")
                    End If
                End If
            End Get
        End Property

        Private _pageVirtDirectory As String = ""
        Public ReadOnly Property PageVirtDirectory As String
            Get
                If Me._pageVirtDirectory <> "" Then
                    Return Me._pageVirtDirectory
                Else
                    Dim ausgabe As String = Me.Page.Request.FilePath
                    Dim lastslash As Integer = ausgabe.LastIndexOf("/")
                    If lastslash > 0 Then
                        Me._pageVirtDirectory = ausgabe.Substring(0, lastslash)
                        Return Me._pageVirtDirectory
                    Else
                        Throw New Exception("ToDo CSSImageGallery control.pagePhysDir")
                    End If
                End If
            End Get
        End Property

        Protected Sub registerCSS()
            'if several CSSImageGallery controls - css will be registered only once
            Dim cssregistered As Object = System.Web.HttpContext.Current.Items("CSSImageGallery_cssregistered")
            If Not cssregistered Is Nothing Then Exit Sub
            System.Web.HttpContext.Current.Items("CSSImageGallery_cssregistered") = New Object


            If Me.Page.Header Is Nothing Then
                Throw New System.InvalidOperationException("Using CSSImageGallery WebControl requires a header control on the page or on the masterpage. (e.g. &lt;head runat=""server"" /&gt;).")
            End If

            Dim cssText As String = vbCrLf
            cssText += "<style type=""text/css"">" + vbCrLf
            cssText += "/* ================================================================" + vbCrLf
            cssText += "This copyright notice must be untouched at all times." + vbCrLf
            cssText += vbCrLf
            cssText += "The original version of this stylesheet and the associated (x)html" + vbCrLf
            cssText += "is available at http://www.cssplay.co.uk/menu/lightbox.html" + vbCrLf
            cssText += "Copyright (c) 2005-2007 Stu Nicholls. All rights reserved." + vbCrLf
            cssText += "This stylesheet and the associated (x)html may be modified in any " + vbCrLf
            cssText += "way to fit your requirements." + vbCrLf
            cssText += "=================================================================== */" + vbCrLf
            cssText += vbCrLf
            cssText += "/* slides styling */" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo {width:635px; text-align:left; position:relative; margin:0 auto;float:left;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo(ul)" + vbCrLf
            cssText += "{display:block; position:absolute; left:0; top:31px; list-style:none; padding:0; margin:0; background:#ddd; width:464px; padding:40px 60px; border:20px solid #bbb; z-index:1;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li" + vbCrLf
            cssText += "{display:inline; width:112px; height:112px; float:left; margin:1px;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a" + vbCrLf
            cssText += "{display:block; width:110px; height:110px; float:left; text-decoration:none;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a img.horiz" + vbCrLf
            cssText += "{display:block; width:100px; border:5px solid;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a img.vert" + vbCrLf
            cssText += "{display:block; height:100px; border:5px solid;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a:hover " + vbCrLf
            cssText += "{white-space:normal; position:relative;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a:hover img.horiz" + vbCrLf
            cssText += "{position:absolute; left:-50px; top:-32px; width:200px; border-color:#fff;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".photo ul li a:hover img.vert" + vbCrLf
            cssText += "{position:absolute; left:-50px; top:-32px; height:200px; border-color:#fff;}" + vbCrLf
            cssText += vbCrLf
            cssText += ".clear {clear:both;display:block;height:0;overflow:hidden;}" + vbCrLf

            cssText += "</style>" + vbCrLf
            Me.Page.Header.Controls.Add(New LiteralControl(cssText))
        End Sub

        Private Sub CSSImageGallery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.registerCSS()
        End Sub

        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            Dim msg As String = "DebugMSG: Content of the current directory (" + Me.PagePhysDirectory + "):<br>" + vbCrLf
            msg += "DebugMSG: current virtual directory (" + Me.PageVirtDirectory + "):<br>" + vbCrLf
            Dim ausgabe As String = ""

            Dim imgDir As String = Me.PagePhysDirectory + "\" + Me._imageSubDir
            Dim thumbDir As String = imgDir + "\" + _thumbsDirName

            'validate if image directory exists. if not - create it
            If Not System.IO.Directory.Exists(imgDir) Then
                Try
                    System.IO.Directory.CreateDirectory(imgDir)
                Catch ex As Exception
                    ErrorReporting.ReportErrorToWebmaster("CSSImageCallery: Impossible to create image directory :(" + "<br />" + vbCrLf + ".NET Message: " + ex.Message)
                    Exit Sub
                End Try
            End If

            'validate if thumbs directory exists. if not - create it
            If Not System.IO.Directory.Exists(thumbDir) Then
                Try
                    System.IO.Directory.CreateDirectory(thumbDir)
                Catch ex As Exception
                    ErrorReporting.ReportErrorToWebmaster("CSSImageCallery: Impossible to create thumb directory :(" + "<br />" + vbCrLf + ".NET Message: " + ex.Message)
                    Exit Sub
                End Try
            End If

            'iterate thru all files
            'for each file(if jpg extension)
            For Each f As String In System.IO.Directory.GetFiles(imgDir)
                If Not f.ToLower.EndsWith(".jpg") Then Continue For

                'calculate the file name 
                Dim nStart As Integer = f.LastIndexOf("\") + 1
                Dim filename As String = ""
                If nStart > 0 And nStart < f.Length Then filename = f.Substring(nStart, f.Length - nStart)

                'create thumbnail(if not already exists) and define if the image has horizontal or vertical layout.
                Dim horizontal As Boolean = Me.CreateThumbnailAndCalculateOrientation(filename, maxThumbWidthOrHeight)
                Dim imgClass As String
                If horizontal Then
                    imgClass = _horizCSSClass
                Else
                    imgClass = _vertCSSClass
                End If

                'render the line for this image and add to the output
                'example: <li><a href="http://www.cpfs.mpg.de/cmac/pictures/images/img1.jpg"><img class="horiz" src="http://www.cpfs.mpg.de/cmac/pictures/images/thumbs/img1.jpg" alt="" title=""></a></li>
                ausgabe += vbCrLf + "<li><a href=""" + Me.PageVirtDirectory + "/" + ImagesSubDir + "/" + filename + """>"
                ausgabe += "<img class=""" + imgClass + """ src=""" + Me.PageVirtDirectory + "/" + ImagesSubDir + "/" + _thumbsDirName + "/" + filename
                ausgabe += """ alt="""" title=""""></a></li>"

            Next

            Dim prefix As String = vbCrLf + "<div class=""" + _divCSSClass + """>" + vbCrLf + "<!--[if lte IE 6]><table><tr><td><![endif]-->" + vbCrLf + "<ul>" + vbCrLf
            Dim suffix As String = vbCrLf + "</ul>" + vbCrLf + "<!--[if lte IE 6]></td></tr></table><![endif]-->" + vbCrLf + "</div>" + "<br class=""" + _clearCSSClass + """>" + vbCrLf
            writer.Write(prefix + ausgabe + suffix)

        End Sub

        ''' <summary>
        ''' This function returns true if orientation of the image is horizontal
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <param name="maxThumbWidthOrHeight"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CreateThumbnailAndCalculateOrientation(ByVal filename As String, ByVal maxThumbWidthOrHeight As Integer) As Boolean
            Dim ausgabe As Boolean = True 'default Horizotal
            Dim origImgPath As String = Me.PagePhysDirectory + "\" + Me._imageSubDir + "\" + filename
            Dim thumbImgPath As String = Me.PagePhysDirectory + "\" + Me._imageSubDir + "\" + _thumbsDirName + "\" + filename

            If Not System.IO.File.Exists(origImgPath) Then Return ausgabe

            Using oImage As System.Drawing.Image = System.Drawing.Image.FromFile(origImgPath)
                'find out image orientation 
                'ursprungliche Masse des Bildes ermitteln
                Dim oWidth As Integer = oImage.Width
                Dim oHeight As Integer = oImage.Height

                If oWidth < oHeight Then
                    ausgabe = False 'vertical
                End If

                'do nothing if the thumbnail already exists
                If System.IO.File.Exists(thumbImgPath) Then Return ausgabe

                'calculate width and height for thumbnail
                Dim iWidth As Integer = maxThumbWidthOrHeight
                Dim iHeight As Integer = maxThumbWidthOrHeight

                'wee need to feet the thumb in the quadrat with site length maxThumbWidthOrHeight
                If ausgabe Then 'image orientation is horizontal - the thumb takes full width
                    iHeight = (iWidth / oWidth) * oHeight
                Else 'image orientation is vertical - the thumb takes full height
                    iWidth = (iHeight / oHeight) * oWidth
                End If

                'define stream for saving thumbnail
                Using thumbStream As New System.IO.FileStream(thumbImgPath, System.IO.FileMode.Create)

                    Dim lFilename As String = filename.ToLower
                    If lFilename.EndsWith("jpg") Or lFilename.EndsWith("jpeg") Then
                        If iWidth = oWidth Then 'Convertierungen sind unnötig. This case is already implemented above
                            oImage.Save(thumbStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Else
                            Me.MakeThumbnailAndSave(oImage, iWidth, iHeight, thumbStream)
                        End If
                    ElseIf lFilename.EndsWith("png") Then
                        ' Png's are a special case. Their encoder requires a bi-directional stream.
                        ' The solution is to stream to a memory stream first
                        ' and write the contents of that memory stream to OutputStream afterwards
                        Dim memStream As New System.IO.MemoryStream
                        If iWidth = oWidth Then 'Convertierungen sind unnötig. This case is already implemented above
                            System.IO.File.Copy(origImgPath, thumbImgPath)
                        Else
                            'Me.MakeThumbnailAndSave(oImage, iWidth, iHeight, memStream)
                            oImage.GetThumbnailImage(iWidth, iHeight, Nothing, Nothing).Save(memStream, System.Drawing.Imaging.ImageFormat.Png)
                            memStream.WriteTo(thumbStream)
                        End If
                    ElseIf lFilename.EndsWith("gif") Then 'GIF hat problemme mit dem Erstellen von Thumbnails - transparente Bereiche können nicht umgewandelt werden
                        'oImage.Save(rspns.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)
                        'oThumbnail.Save(rspns.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)
                        If iWidth = oWidth Then 'Convertierungen sind unnötig
                            oImage.Save(thumbStream, System.Drawing.Imaging.ImageFormat.Gif)
                        Else
                            Me.MakeThumbnailAndSave(oImage, iWidth, iHeight, thumbStream)
                        End If
                    End If
                    thumbStream.Close()
                End Using 'thumbStream
            End Using 'oImage
            Return ausgabe
        End Function

        ''' <summary>
        ''' Für JPG-Bilder soll lieber diese Prozedur verwendet werden, da System.Drawing.Image.GetThumbnailImage
        ''' nimt den im Bild gespeicherten Thumbnail(wenn vorhanden), der in der Regel niedrigere Qualität hat.
        ''' </summary>
        ''' <param name="oImage">OriginalImage</param>
        ''' <param name="newWidth"></param>
        ''' <param name="newHeight"></param>
        ''' <param name="outputStream">z.B response.OutputStream oder System.IO.MemoryStream</param>
        ''' <remarks></remarks>
        Private Sub MakeThumbnailAndSave(ByVal oImage As System.Drawing.Image, ByVal newWidth As Integer, ByVal newHeight As Integer, ByRef outputStream As System.IO.Stream)
            Dim imgTmp As System.Drawing.Image
            Dim imgFoto As System.Drawing.Bitmap
            imgTmp = oImage

            If (imgTmp.Width > newWidth) Then
                imgFoto = New System.Drawing.Bitmap(newWidth, newHeight)
                Dim recDest As New System.Drawing.Rectangle(0, 0, newWidth, newHeight)
                Dim gphCrop As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgFoto)
                gphCrop.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality
                gphCrop.CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
                gphCrop.InterpolationMode = Drawing.Drawing2D.InterpolationMode.High

                gphCrop.DrawImage(imgTmp, recDest, 0, 0, imgTmp.Width, imgTmp.Height, Drawing.GraphicsUnit.Pixel)
            Else
                imgFoto = imgTmp
            End If

            'Dim myImageCodecInfo As System.Drawing.Imaging.ImageCodecInfo
            Dim myEncoder As System.Drawing.Imaging.Encoder
            Dim myEncoderParameter As System.Drawing.Imaging.EncoderParameter
            Dim myEncoderParameters As System.Drawing.Imaging.EncoderParameters

            Dim arrayICI() As System.Drawing.Imaging.ImageCodecInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
            Dim jpegICI As System.Drawing.Imaging.ImageCodecInfo = Nothing
            Dim x As Integer = 0
            For x = 0 To arrayICI.Length - 1
                If (arrayICI(x).FormatDescription.Equals("JPEG")) Then
                    jpegICI = arrayICI(x)
                    Exit For
                End If
            Next
            myEncoder = System.Drawing.Imaging.Encoder.Quality
            myEncoderParameters = New System.Drawing.Imaging.EncoderParameters(1)
            myEncoderParameter = New System.Drawing.Imaging.EncoderParameter(myEncoder, 60L)
            myEncoderParameters.Param(0) = myEncoderParameter
            imgFoto.Save(outputStream, jpegICI, myEncoderParameters)
            imgFoto.Dispose()
            imgTmp.Dispose()
        End Sub

    End Class

    Public Class PigmentLabel
        Inherits Label

    End Class 'TemplateFieldNamingContainer
End Namespace