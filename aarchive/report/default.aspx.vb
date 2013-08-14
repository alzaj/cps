Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class _index
    Inherits System.Web.UI.Page

#Region "PageConstants"
    Const FILTER_ID_SESSION_NAME As String = "filter_id"
    Const FILTER_NAME_SESSION_NAME As String = "filter_name"
    Const FILTER_ADDRESS_SESSION_NAME As String = "filter_address"
    Const FILTER_BEITRAG_TYP_SESSION_NAME As String = "filter_beitragtyp"
    Const FILTER_ANWESEND_SESSION_NAME As String = "filter_anwesend"
    Const FILTER_VORTRAGHIGHLIGHT_SESSION_NAME As String = "filter_vortraghighlight"
    Const FILTER_DAY_SESSION_NAME As String = "filter_day"

    Const SORT_EXPRESSION_SESSION_NAME As String = "sort_expression"
    Const SORT_DIRECTION_SESSION_NAME As String = "sort_direction"

    Const URL_ACCENDING_SORT_IMAGE As String = "~/Images/ascending_white.gif"
    Const URL_DECCENDING_SORT_IMAGE As String = "~/Images/descending_white.gif"
    Const URL_NOTIFICATION_FOR_CHANGED_DATA As String = "member.aspx"

    Const NAME_TAGE_DDL As String = "tageDLL"
    Const NAME_TYP_DDL As String = "typDDL"
    Const NAME_NUMMER_DDL As String = "nummerDDL"
    Const NAME_PLACEHOLDER_LABEL As String = "placeholderLabel"
    Const NAME_SUFFIX_INSERT_BEITRAG As String = "_insertBeitrag"
    Const NAME_SUFFIX_DELETE_BEITRAG As String = "_deleteBeitrag"

    Public Shared ARRAY_CONFERENCE_TAGE As Char() = {"M", "T", "W"}
    Public Shared ARRAY_BEITRAG_TYPES As Char() = {"O", "P"}
#End Region 'PageConstants

#Region "PageVariables"
    Private _postBackData As New PostBackData

    ''' <summary>
    '''einige Funktionen wollen wissen ob der ViewState schon geladen ist oder nicht.
    '''ViewState ist geladen zwischen PageInit und PageLoad
    '''Ich setze diese Variable am Anfang von PageLoad auf True
    ''' </summary>
    ''' <remarks></remarks>
    Private _viewStateIsLoaded As Boolean = False
    Private _listeGridViewPageIndex As Integer = -1
    Private _listeGridViewRowNr As Integer = -1
    Private _messageText As String = ""
    Private _listeGridViewDataView As System.Data.DataView

    Private _uniqueIDForListGridview As Integer = 1

    Private _abstracts As New ArrayList

    Private summeMissingAbstracts As Integer = 0

    Private Class PostBackData
        Public eventArgument As String = ""

        Public memberid As Integer = 0
        Public filterName As String = ""
        Public filterAddress As String = ""
        Public filterID As Integer = 0
        Public filterDinner As Boolean = False
        Public filterTransport As Boolean = False
        Public filterVault As Boolean = False
        Public filterStudent As Boolean = False
        Public filterPayment As String = ""

        Public detailsMemberNeedUpdate As Boolean = False
        Public detailsUnlockPressed As Boolean = False
        Public detailsCancelPressed As Boolean = False
        Public detailsShowPressed As Boolean = False
        'Public detailsAccompNeedUpdate As Boolean = False
        Public listeNeedUpdate As Boolean = False
        Public updatedPosteranz As Integer = -1
        Public updatedVortraganz As Integer = -1

        Public memberToSelect As Integer = 0

        Public beitragType As Char = "X"

        Public filterAnwesend As Integer = 0
        Public filterVortragHighlight As String = ""

        'Public insertAccomp As Boolean = False

        Public anwesendInDerListePressed As Boolean = False 'Ob eine der Anwesend-Imagebuttons in der liste gedrückt wurde

        'Public additionalDinnerPressed As Boolean = False
        'Public additionalDVBPressed As Boolean = False
        'Public additionalVaultPressed As Boolean = False

        Public Sub New()

        End Sub
    End Class

    Protected _footerStatistics As New FooterStatistics

    Protected Class FooterStatistics
        Public _anzahlGesamt As Integer = 0
        Public Property AnzahlGesamt As Integer 
            Get
                Return Me._anzahlGesamt
            End Get
            Set(ByVal value As Integer)
                Me._anzahlGesamt = value
            End Set
        End Property
        Public AnzahlAnwesend As Integer = 0
        Public AnzahlVortrag As Integer = 0
        Public AnzahlTeilnehmerliste As Integer = 0
        Public AnzahlTeilnehmerbestaetigung As Integer = 0
    End Class

    Private ItemsCount As Integer = 0

    Public Class Beitrag
        Public beitragid As Integer = 0
        Public memberid As Integer
        Public tag As Char
        Public art As Char
        Public nummer As Integer
        Public start As Long
        Public ende As Long
        Public Sub New(ByVal beitragid As Integer, ByVal memberid As Integer, ByVal tag As Char, ByVal art As Char, ByVal nummer As Integer, Optional ByVal startTiks As Long = 0, Optional ByVal endeTiks As Long = 0)
            tag = Char.ToUpper(tag)
            Dim tagOK As Boolean = False
            For Each t As Char In ARRAY_CONFERENCE_TAGE
                If t = tag Then
                    tagOK = True
                    Exit For
                End If
            Next

            art = Char.ToUpper(art)
            Dim artOK As Boolean = False
            For Each t As Char In ARRAY_BEITRAG_TYPES
                If t = art Then
                    artOK = True
                    Exit For
                End If
            Next

            If Not tagOK Or Not artOK Or nummer <= 0 Then
                Throw New Exception("Üngültige Beitragsbezeichnung: " + tag + art + nummer.ToString)
            End If

            Me.beitragid = beitragid
            Me.memberid = memberid
            Me.tag = tag
            Me.art = art
            Me.nummer = nummer
            Me.start = start
            Me.ende = ende
        End Sub
    End Class
    Private _beitraege As New List(Of Beitrag)
    Private WithEvents _beitragInsertValidator As New CustomValidator

    Private whereClause As String = ""
    Protected orderbyClause As String = " ORDER BY [surname], [firstname]"
    Protected filterDescriptions As New ArrayList

#End Region 'PageVariables

#Region "PageProperties"
    ''' <summary>
    ''' First - gesperrte RecordID(integer), second - Bezeichnung des Bearbeites (string), third - letzter Zugriff (date)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected ReadOnly Property LockedRecords As List(Of Triplet)
        Get
            If System.Web.HttpContext.Current.Application(App_NAMES.App_Report_LockedRecords) Is Nothing _
                OrElse Not TypeOf System.Web.HttpContext.Current.Application(App_NAMES.App_Report_LockedRecords) Is List(Of Triplet) Then
                System.Web.HttpContext.Current.Application(App_NAMES.App_Report_LockedRecords) = New List(Of Triplet)
            End If
            Return System.Web.HttpContext.Current.Application(App_NAMES.App_Report_LockedRecords)
        End Get
    End Property

    ''' <summary>
    ''' The String should be unique for each who edits the records
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BearbeiterID As String
        Get
            Dim forwardIP As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If Not String.IsNullOrEmpty(forwardIP) Then forwardIP = ","
            Dim ip As String = System.Web.HttpContext.Current.Request.UserHostAddress

            Return forwardIP + ip + ";" + System.Web.HttpContext.Current.Session.SessionID
        End Get
    End Property
#End Region 'PageProperties



#Region "PageLiveCycle"

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ErrorReporting.StandardPageErrorHandling()
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Request.QueryString("action") = "logout" Then GlobFunctions.LogOutUser()

        Dim q As System.Collections.Specialized.NameValueCollection = System.Web.HttpContext.Current.Request.QueryString
        If Not q(Q_Names.Q_STRING_FOR_MEMBERID) Is Nothing Then
            Try
                Me._postBackData.memberid = CInt(q(Q_Names.Q_STRING_FOR_MEMBERID))
            Catch ex As Exception
            End Try
        End If

        'example of use: when created new participant on newparticipant.aspx and then redirected to this page(default.aspx)
        'the query string parameter Q_STRING_MEMBER_TO_SELECT contains id of just created member
        If Not q(Q_Names.Q_STRING_MEMBER_TO_SELECT) Is Nothing Then
            Try
                Me._postBackData.memberToSelect = CInt(q(Q_Names.Q_STRING_MEMBER_TO_SELECT))
                Me._postBackData.memberid = 0
            Catch ex As Exception
            End Try
        End If

        Dim lunchPressed As Boolean = False
        Dim lunchMemberid As Integer = -1

        For Each k As String In System.Web.HttpContext.Current.Request.Form.Keys
            Dim kValue As String = System.Web.HttpContext.Current.Request.Form(k)

            If k.EndsWith("$Filter_NameTextBox") Then 'Wert des Name-Filter-TextBoxes speichern
                Me._postBackData.filterName = kValue.Trim
            ElseIf k.EndsWith("$Filter_AdressTextBox") Then
                Me._postBackData.filterAddress = kValue.Trim
            ElseIf k.EndsWith("$Filter_IDTextBox") Then 'Wert des ID-Filter-TextBoxes speichern
                If kValue.Trim <> "" Then
                    Dim i As Integer = 0
                    Try
                        i = CInt(kValue)
                    Catch ex As Exception
                        i = 999999
                    End Try
                    Me._postBackData.filterID = i
                End If

            ElseIf k.EndsWith("$posterTextBox") And k.Contains("$ListeGridView$") Then 'Wert des posteranz-TextBoxes zwischenspeichern
                Try
                    If kValue.Trim = "" Then
                        Me._postBackData.updatedPosteranz = 0
                    Else
                        Me._postBackData.updatedPosteranz = CInt(kValue.Trim)
                    End If
                Catch ex As Exception
                End Try
            ElseIf k.EndsWith("$vortragTextBox") And k.Contains("$ListeGridView$") Then 'Wert des vortraganz-TextBoxes zwischenspeichern
                Try
                    If kValue.Trim = "" Then
                        Me._postBackData.updatedVortraganz = 0
                    Else
                        Me._postBackData.updatedVortraganz = CInt(kValue.Trim)
                    End If

                Catch ex As Exception
                End Try
            ElseIf k.EndsWith("$ListShowButton") Then 'Show-Button im Liste-Modus wurde gedrückt
                Me._postBackData.memberid = 0
            ElseIf k.EndsWith("presentImageButton.x") Or k.EndsWith("presentImageButton.y") Then 'Button zum Makieren als anwesend wurde gedrückt
                Me._postBackData.memberid = 0
                Me._postBackData.anwesendInDerListePressed = True
            ElseIf k.EndsWith("absentImageButton.x") Or k.EndsWith("absentImageButton.y") Then 'Button zum Makieren als abwesend wurde gedrückt
                Me._postBackData.memberid = 0
                Me._postBackData.anwesendInDerListePressed = True
            ElseIf k.EndsWith("$DetailsShowButton") Then 'Show-Button im Details-Modus wurde gedrückt
                Me._postBackData.detailsShowPressed = True
                Me.UnlockRecordForEditing(Me._postBackData.memberid, True) 'Den aktuellen datensatz ensperren, aber nur wenn er nicht von anderen gesperrt ist
                Me._postBackData.memberToSelect = Me._postBackData.memberid
                Me._postBackData.memberid = 0
            ElseIf k.EndsWith("$anwesendDropDownList") Then
                Try
                    Me._postBackData.filterAnwesend = CInt(Me.Request.Form(k).Trim)
                Catch ex As Exception
                End Try
            ElseIf k.EndsWith("$vortraghighlightDropDownList") Then
                Try
                    Me._postBackData.filterVortragHighlight = Me.Request.Form(k).Trim
                Catch ex As Exception
                End Try
            ElseIf k.EndsWith("$" + NAME_TYP_DDL) And kValue.Length > 0 Then
                Me._postBackData.beitragType = kValue(0)
            ElseIf (k = "__EVENTTARGET" And kValue.EndsWith("$Details_MemberFormView$UpdateButton")) Or k.EndsWith("$UpdateButton") Then
                Me._postBackData.detailsMemberNeedUpdate = True
                If k.EndsWith("$UpdateButton") Then
                    'Me._postBackData.detailsUpdatePressed = True

                    'Der Datensatz kann nur geändert werden wenn kein anderer benutzer gerade mit ihm arbeitet.
                    'siehe auch Details_MemberFormView_ItemUpdating
                    If Not Me.IsRecordLockedForEditing(Me._postBackData.memberid).Second <> -1 Then
                        Me._postBackData.memberToSelect = Me._postBackData.memberid
                        Me._postBackData.memberid = 0
                    End If

                End If
            ElseIf k.EndsWith("$UpdateCancelButton") Then
                Me._postBackData.detailsCancelPressed = True
                Me.UnlockRecordForEditing(Me._postBackData.memberid, True) 'Den aktuellen datensatz ensperren, aber nur wenn er nicht von anderen gesperrt ist
                Me._postBackData.memberToSelect = Me._postBackData.memberid
                Me._postBackData.memberid = 0
            ElseIf k.EndsWith("$UpdateUnlockButton") Then
                Me._postBackData.detailsUnlockPressed = True
            ElseIf k = "__EVENTTARGET" And kValue.EndsWith("$ListeGridView") Then
                Me._postBackData.memberid = 0
            ElseIf k = "__EVENTTARGET" And kValue.Contains("$ListeGridView$") Then ' In der ListeGridView wurde ein Cancel- oder Update-Knopf gedrückt
                Me._postBackData.memberid = 0
            ElseIf k = "__EVENTTARGET" And kValue.Contains("$lunchPartDropDownList") Then ' für den Teilnehmer wurde anzahl der Lunchkarten geändert
                lunchMemberid = Me._postBackData.memberid
                'Me._postBackData.memberid = 0
                lunchPressed = True
            End If
        Next
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not Me.IsPostBack Or Me._postBackData.anwesendInDerListePressed Then
            Me.RestoreFilterToPostBackDataFromSession()
            'Filter form fields wiederherstellen
            Me.InitFilterFieldsFromPostBackData()
        End If

        Me.GetBeitraegeFromDatabase()
        Me.RestoreSortingFromSession()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me._postBackData.memberid = 0) Or (Not Me.IsPostBack) Then
            'Me.RestoreFilterFromSession()
        End If

        Me.LoadListePanel()
        Me.LoadDetailsPanel()

        Me.HighlightNotEmptyFilterFields()
    End Sub

#End Region 'PageLiveCycle

#Region "Control Events"

    Protected Sub ListeGridView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListeGridView.DataBinding
        Dim fehler As String = Me.InitFooterStatistics(Me.whereClause)
        If fehler <> "" Then
            GlobFunctions.WriteToLogFile(fehler)
        End If
    End Sub

    ''' <summary>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' handling if the user checks/unchecks the "anwesenheit" in the GridView.
    ''' </remarks>
    Protected Sub ListeGridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ListeGridView.RowCommand

        Dim id As Integer = 0
        Try
            id = CInt(e.CommandArgument)
        Catch ex As Exception
        End Try
        If id = 0 Then Exit Sub

        Dim fehler As String = ""
        Dim anwesend As Boolean = False
        If e.CommandName = "check" Then
            anwesend = True
        ElseIf e.CommandName = "uncheck" Then
            anwesend = False
        Else
            Exit Sub
        End If

        fehler = Me.SetAnwesenheitForMember(id, anwesend)
        If fehler <> "" Then
            GlobFunctions.WriteToLogFile(fehler)
            Exit Sub
        Else
            Dim thisButton As ImageButton = CType(e.CommandSource, ImageButton)
            Dim anotherButton As ImageButton
            If anwesend Then
                anotherButton = thisButton.Parent.FindControl("presentImageButton")
            Else
                anotherButton = thisButton.Parent.FindControl("absentImageButton")
            End If
            thisButton.Visible = False
            anotherButton.Visible = True

            'Angezeigte Eincheck-Datum muss auch synchron mit dem imabebutton sein.
            'Deswegen wenn "checked" wurde gedrückt - wir setzen angezeigte Eincheck-Datum auf Now
            'wenn "unchecked" wurde gedrückt - wir blenden die angezeigte Eincheck-Datum aus
            Dim eincheckLbl As Label = thisButton.Parent.FindControl("checkindateLabel")
            If Not eincheckLbl Is Nothing Then
                If anwesend Then
                    eincheckLbl.Text = Now.ToString("ddd. HH:mm")
                Else
                    eincheckLbl.Text = ""
                End If
            End If
        End If

        'Die zeile mit anderer Formatierung herforheben
        CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow).RowState = DataControlRowState.Selected

        'die Zeile ausblenden, wenn der Filter für Anwesenheit auf "nur anwesend" oder "nur abwesend" steht
        If Me._postBackData.filterAnwesend = 1 And Not anwesend Or _
           Me._postBackData.filterAnwesend = 2 And anwesend Then
            CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow).Visible = False
        End If

    End Sub

    Protected Sub detailsPresentImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim thisButton As ImageButton = CType(sender, ImageButton).Parent.FindControl("detailsPresentImageButton")

        Dim id As Integer = 0
        Try
            id = CInt(thisButton.CommandArgument)
        Catch ex As Exception
        End Try
        If id = 0 Then Exit Sub

        Dim fehler As String = ""
        Dim anwesend As Boolean = False


        fehler = Me.SetAnwesenheitForMember(id, anwesend)
        If fehler <> "" Then
            GlobFunctions.WriteToLogFile(fehler)
            Exit Sub
        Else
            Dim anotherButton As ImageButton = thisButton.Parent.FindControl("detailsAbsentImageButton")
            thisButton.Visible = False
            anotherButton.Visible = True

            'Angezeigte Eincheck-Datum muss auch synchron mit dem imabebutton sein.
            'Deswegen wenn "checked" wurde gedrückt - wir setzen angezeigte Eincheck-Datum auf Now
            Dim eincheckLbl As Label = thisButton.Parent.FindControl("checkindateLabel")
            If Not eincheckLbl Is Nothing Then
                eincheckLbl.Text = ""
            End If
        End If

    End Sub

    Protected Sub detailsAbsentImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim thisButton As ImageButton = CType(sender, ImageButton).Parent.FindControl("detailsAbsentImageButton")

        Dim id As Integer = 0
        Try
            id = CInt(thisButton.CommandArgument)
        Catch ex As Exception
        End Try
        If id = 0 Then Exit Sub

        Dim fehler As String = ""
        Dim anwesend As Boolean = True


        fehler = Me.SetAnwesenheitForMember(id, anwesend)
        If fehler <> "" Then
            GlobFunctions.WriteToLogFile(fehler)
            Exit Sub
        Else
            Dim anotherButton As ImageButton = thisButton.Parent.FindControl("detailsPresentImageButton")
            thisButton.Visible = False
            anotherButton.Visible = True

            'Angezeigte Eincheck-Datum muss auch synchron mit dem imabebutton sein.
            'Deswegen wenn "unchecked" wurde gedrückt - wir blenden die angezeigte Eincheck-Datum aus
            Dim eincheckLbl As Label = thisButton.Parent.FindControl("checkindateLabel")
            If Not eincheckLbl Is Nothing Then
                eincheckLbl.Text = Now.ToString("dd.MM.yyyy HH:mm")
            End If
        End If
    End Sub

    Protected Sub ListeGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ListeGridView.RowCreated
        ' Use the RowType property to determine whether the 
        ' row being created is the header row. 
        If e.Row.RowType = DataControlRowType.Header Then

            ' Call the GetSortColumnIndex helper method to determine
            ' the index of the column being sorted.
            Dim sortColumnIndex As Integer = GetSortColumnIndex()
            If sortColumnIndex <> -1 Then

                ' Call the AddSortImage helper method to add
                ' a sort direction image to the appropriate
                ' column header. 
                AddSortImage(sortColumnIndex, e.Row)
            End If
        End If
    End Sub

    Protected Sub ListeGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ListeGridView.RowDataBound
        'Workaround for the ImageButtons. If not assigned the ListGridView throws the exception "Invalid postback or callback argument"
        e.Row.ID = Me._uniqueIDForListGridview
        Me._uniqueIDForListGridview += 1

        Dim nrLabel As Label = CType(GlobFunctions.SuperFindControl(e.Row, "RowNumberLabel"), Label)
        If Not nrLabel Is Nothing Then
            nrLabel.Text = ((Me.ListeGridView.PageIndex * Me.ListeGridView.PageSize) + e.Row.RowIndex + 1).ToString

            'If Me._listeGridViewRowNr = e.Row.RowIndex And Me._listeGridViewPageIndex = Me.ListeGridView.PageIndex Then
            '    e.Row.RowState = DataControlRowState.Selected
            'End If

            Dim memberNr As Integer = CType(CType(e.Row.DataItem, System.Data.DataRowView).Item("participantsid"), Integer)
            If memberNr = Me._postBackData.memberToSelect Then 'die Zeile als selected Markieren und PageIndex berechnen
                e.Row.RowState = DataControlRowState.Selected
            End If

            If Me._listeGridViewDataView Is Nothing And Me._postBackData.memberToSelect > 0 Then 'Zwischenspeichern Kompletten DataView
                Me._listeGridViewDataView = CType(e.Row.DataItem, System.Data.DataRowView).DataView
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            'searching each cell for the pigment control. If found - color the cels background
            For Each tc As TableCell In e.Row.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is CPFS.PigmentLabel Then
                        tc.BackColor = CType(c, Label).BackColor
                        Exit For
                    End If
                Next
            Next

            'If e.Row.Cells.Item(0).ba = System.Drawing.Color.Red Then e.Row.Cells.Item(11).BackColor = System.Drawing.Color.Green
        End If

    End Sub

    Protected Sub ListeGridView_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles ListeGridView.Sorting
        Me.SaveSortingToSession(e.SortExpression, e.SortDirection)
    End Sub

    Protected Sub ShowButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListShowButton.Click, DetailsShowButton.Click
        Me.ListeGridView.EditIndex = -1
        Me.SaveFilterToSession()
        'Me.ClearFilterMaske()
    End Sub

    Protected Sub ListeSqlDataSource_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListeSqlDataSource.DataBinding
        Me.summeMissingAbstracts = 0
    End Sub

    Protected Sub ListeSqlDataSource_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles ListeSqlDataSource.Updating
        If Me._postBackData.updatedPosteranz > -1 Then
            e.Command.Parameters.Item("@posteranz").Value = Me._postBackData.updatedPosteranz
        End If
        If Me._postBackData.updatedVortraganz > -1 Then
            e.Command.Parameters.Item("@vortraganz").Value = Me._postBackData.updatedVortraganz
        End If
        Try
            Me._postBackData.memberToSelect = CInt(e.Command.Parameters.Item("@memberid").Value)
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub Details_MemberFormView_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles Details_MemberFormView.ItemUpdated
        'reloading ListePanel
        Me.LoadListePanel(False)
        Me.message.Font.Bold = True
        Me.message.ForeColor = Drawing.Color.Lime
        Me.message.Text = "Änderungen wurden erfolgreich gespeichert<br /><br />"
        Me.UnlockRecordForEditing(e.Keys("participantsid"))
    End Sub

    Protected Sub Details_MemberFormView_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles Details_MemberFormView.ItemUpdating
        Dim concur As Pair = Me.IsRecordLockedForEditing(e.Keys("participantsid"))
        If Convert.ToInt64(concur.Second) >= 0 Then
            Me.ShowConcurency(Convert.ToString(concur.First), Convert.ToInt64(concur.Second))
            Me.message.Text += "Ihre Ändernungen wurden NICHT gespeichert!<br /><br />"
            e.Cancel = True
        End If
    End Sub


#End Region 'Control Events

#Region "Procedures"
    Protected Sub LoadDetailsPanel()
        If Me._postBackData.memberid > 0 Then
            Me.DetailsPanel.Visible = True
            Me.DetailsShowButton.Visible = True

            'Filtereinstellungen für die DataSources
            Me.Details_MemberSqlDataSource.FilterExpression = "participantsid = " + Me._postBackData.memberid.ToString

            'weitere 2 zeilen sind nicht gut, while die Daten werden extra abgerufen um die Anzahl der Datensätze von der Filtered SqlDataSource zu bekommen
            'andere lösung habe ich nicht gefunden

            If Not Me._postBackData.detailsMemberNeedUpdate Then
                Me.Details_MemberFormView.DataBind()
                'überprüfen of ein anderer Benutzer gerade den selben Datensatz bearbeitet.
                'Wenn ja, dann die Bearbeitungsmöglichkeiten ausblenden (siehe Me.CheckConcurency).
                'Wenn nicht, dann den Datensatz sperren
                Dim lockingEditor As Pair = Me.IsRecordLockedForEditing(Me._postBackData.memberid)
                If Me._postBackData.detailsUnlockPressed Then
                    Me.UnlockRecordForEditing(Me._postBackData.memberid)
                End If

                'nach den oberen manupulationen editor konnte geändert werden
                lockingEditor = Me.IsRecordLockedForEditing(Me._postBackData.memberid)
                If Convert.ToInt64(lockingEditor.Second) >= 0 Then
                    Me.ShowConcurency(lockingEditor.First, lockingEditor.Second)
                Else
                    Me.LockRecordForEditing(Me._postBackData.memberid)
                End If
            End If
        Else
            Me.DetailsPanel.Visible = False
            Me.DetailsShowButton.Visible = False
        End If
    End Sub

    Protected Sub LoadListePanel(Optional ByVal needGeneratingSqlCommand As Boolean = True)
        If Me._postBackData.memberid > 0 Then
            Me.ListePanel.Visible = False
            Me.ListShowButton.Visible = False
        Else
            If needGeneratingSqlCommand Then

                Me.ListePanel.Visible = True
                Me.ListShowButton.Visible = True

                Dim transfDir As String = GlobFunctions.SubstringAnfangEnde(qcnp09.U_URL_TO_CSV_ASPX_FILE, 0, qcnp09.U_URL_TO_CSV_ASPX_FILE.LastIndexOf("/")).TrimEnd("/")
                Dim transfAspx As String = GlobFunctions.SubstringAnfangEnde(qcnp09.U_URL_TO_CSV_ASPX_FILE, qcnp09.U_URL_TO_CSV_ASPX_FILE.LastIndexOf("/"), qcnp09.U_URL_TO_CSV_ASPX_FILE.Length - 1).Trim("/")
                Dim transfCsv As String = GlobFunctions.SubstringAnfangEnde(transfAspx, 0, transfAspx.ToLower.LastIndexOf(".aspx")) + "csv"
                'Me.csvHyperLink.NavigateUrl = transfDir + "/" + transfCsv
                'Me.csvHyperLink.NavigateUrl = transfDir + "/" + transfAspx + "?" + Q_STRING_FOR_MEMBERID + "=0"

                Dim delim As String = ""
                'If Not String.IsNullOrEmpty(Me.ListeSqlDataSource.FilterExpression) Then delim = " AND "
                If Not String.IsNullOrEmpty(Me.whereClause) Then delim = " AND "

                If Me._postBackData.filterAnwesend > 0 Then
                    If Me._postBackData.filterAnwesend < 3 Then
                        Dim s As String = delim + " (anwesend = "
                        If Me._postBackData.filterAnwesend = 1 Then
                            s += "1) "
                            Me.filterDescriptions.Add("Nur anwesende Teilnehmer.")
                        ElseIf Me._postBackData.filterAnwesend = 2 Then
                            s += "0) "
                            Me.filterDescriptions.Add("Nur abwesende Teilnehmer.")
                        End If
                        'Me.ListeSqlDataSource.FilterExpression += s
                        Me.whereClause += s
                    End If

                End If

                'If Not String.IsNullOrEmpty(Me.ListeSqlDataSource.FilterExpression) Then delim = " AND "
                If Not String.IsNullOrEmpty(Me.whereClause) Then delim = " AND "

                If Me._postBackData.filterName <> "" Then
                    Dim parts() As String = Me._postBackData.filterName.Split("/")


                    'Me.ListeSqlDataSource.FilterExpression += delim + " (firstname LIKE '" + Me._postBackData.filterName + "%' OR " + _
                    '                                                "surname LIKE '" + Me._postBackData.filterName + "%' OR " + _
                    '                                                "roomneighbour LIKE '%" + Me._postBackData.filterName + "%')"
                    Me.whereClause += delim + " (firstname LIKE '%" + Me._postBackData.filterName + "%' OR " + _
                                                                    "surname LIKE '%" + Me._postBackData.filterName + "%')"

                    Me.filterDescriptions.Add("Name, Vorname oder Nachbarname enthält """ + Me._postBackData.filterName + """")
                End If

                If Not String.IsNullOrEmpty(Me.whereClause) Then delim = " AND "

                If Me._postBackData.filterAddress <> "" Then
                    Me.whereClause += delim + " (institut LIKE '%" + Me._postBackData.filterAddress + "%' OR " + _
                                                                    "department LIKE '%" + Me._postBackData.filterAddress + "%' OR " + _
                                                                    "street LIKE '%" + Me._postBackData.filterAddress + "%' OR " + _
                                                                    "city LIKE '%" + Me._postBackData.filterAddress + "%' OR " + _
                                                                    "country LIKE '%" + Me._postBackData.filterAddress + "%')"
                    Me.filterDescriptions.Add("Institut, Department, Strasse, Stadt or Land enthält """ + Me._postBackData.filterAddress + """")
                End If

                If Not String.IsNullOrEmpty(Me.whereClause) Then delim = " AND "

                If Me._postBackData.filterID <> 0 Then
                    'Me.ListeSqlDataSource.FilterExpression += delim + " (participantsid = " + Me._postBackData.filterID.ToString + ")"
                    Me.whereClause += delim + " (participantsid = " + Me._postBackData.filterID.ToString + ")"
                    Me.filterDescriptions.Add("TeilnehmerID ist gleich """ + Me._postBackData.filterID.ToString + """")
                End If


                If Not String.IsNullOrEmpty(whereClause) Then
                    Me.ListeSqlDataSource.SelectCommand = Me.ListeSqlDataSource.SelectCommand + " WHERE " + Me.whereClause + Me.orderbyClause
                    'Me.filtermsg.Text = vbCrLf + "<br><br><span style=""color:orange;font-size:10px;"">Ergebnisse sind gefiltert. Folgende Filter wurden angewendet:</span><br>" + vbCrLf
                    'Me.filtermsg.Text += "<ul style=""color:orange;font-size:10px;margin-top:0px;"">" + vbCrLf
                    'For Each s As String In Me.filterDescriptions
                    '    Me.filtermsg.Text += "<li>" + s + "</li>" + vbCrLf
                    'Next
                    'Me.filtermsg.Text += "</ul>" + vbCrLf
                End If
            End If

            Me.ListeGridView.DataBind()


            'Widerherstellen die Page von Gridview, die Der Benutzer verlassen hat
            If Not Me._listeGridViewDataView Is Nothing And Me._postBackData.memberToSelect > 0 Then
                For i As Integer = 0 To Me._listeGridViewDataView.Count - 1
                    Dim memberNr As Integer = CType(Me._listeGridViewDataView.Item(i).Item("participantsid"), Integer)
                    If memberNr = Me._postBackData.memberToSelect Then 'die Zeile als selected Markieren und PageIndex berechnen
                        Dim pageNr As String = i \ Me.ListeGridView.PageSize
                        'Dim rowNr As String = i Mod Me.ListeGridView.PageSize
                        Me.ListeGridView.PageIndex = pageNr
                        Exit For
                    End If
                Next
            End If
            End If
    End Sub

    Protected Sub ClearFilterMaske()
        'Me.Session(FILTER_ID_SESSION_NAME) = ""
        Me.Filter_IDTextBox.Text = ""
        'Me.Session(FILTER_NAME_SESSION_NAME) = ""
        Me.Filter_NameTextBox.Text = ""

        Me.Filter_AdressTextBox.Text = ""

        'Me.Session(FILTER_ANWESEND_SESSION_NAME) = "3"
        Me.anwesendDropDownList.SelectedValue = "3"

        Me.vortraghighlightDropDownList.SelectedIndex = 0
    End Sub

    Protected Sub SaveFilterToSession()
        Me.Session(FILTER_ID_SESSION_NAME) = Me.Filter_IDTextBox.Text
        Me.Session(FILTER_NAME_SESSION_NAME) = Me.Filter_NameTextBox.Text
        Me.Session(FILTER_ADDRESS_SESSION_NAME) = Me.Filter_AdressTextBox.Text
        Me.Session(FILTER_ANWESEND_SESSION_NAME) = Me.anwesendDropDownList.SelectedValue
        Me.Session(FILTER_VORTRAGHIGHLIGHT_SESSION_NAME) = Me.vortraghighlightDropDownList.SelectedValue

    End Sub

    Protected Sub RestoreFilterFromSession()
        If Me.Session(FILTER_ID_SESSION_NAME) Is Nothing _
            Or Me.Session(FILTER_NAME_SESSION_NAME) Is Nothing _
            Or Me.Session(FILTER_BEITRAG_TYP_SESSION_NAME) Is Nothing _
            Or Me.Session(FILTER_DAY_SESSION_NAME) Is Nothing _
            Or Me.Session(FILTER_ANWESEND_SESSION_NAME) Is Nothing Then Exit Sub
        Me.Filter_IDTextBox.Text = Me.Session(FILTER_ID_SESSION_NAME)
        Me.Filter_NameTextBox.Text = Me.Session(FILTER_NAME_SESSION_NAME)
        Me.Filter_AdressTextBox.Text = Me.Session(FILTER_ADDRESS_SESSION_NAME)
        Me.anwesendDropDownList.SelectedValue = Me.Session(FILTER_ANWESEND_SESSION_NAME)

        Dim s As String = Me.Session(FILTER_VORTRAGHIGHLIGHT_SESSION_NAME)
        If s Is Nothing Then
            Me.vortraghighlightDropDownList.SelectedIndex = 0
        Else
            Me.vortraghighlightDropDownList.SelectedValue = s
        End If

        If Not (Me.IsPostBack) Then
            Me.RestoreFilterToPostBackDataFromSession()
        End If

    End Sub

    Public Sub RestoreFilterToPostBackDataFromSession()
        Try
            Me._postBackData.filterID = CInt(Me.Session(FILTER_ID_SESSION_NAME))
        Catch ex As Exception
        End Try

        Try
            Me._postBackData.filterName = Me.Session(FILTER_NAME_SESSION_NAME)
        Catch ex As Exception
        End Try

        Try
            Me._postBackData.filterAddress = Me.Session(FILTER_ADDRESS_SESSION_NAME)
        Catch ex As Exception

        End Try

        Try
            Me._postBackData.filterAnwesend = CInt(Me.Session(FILTER_ANWESEND_SESSION_NAME))
        Catch ex As Exception
        End Try

        Try
            Me._postBackData.filterVortragHighlight = Me.Session(FILTER_VORTRAGHIGHLIGHT_SESSION_NAME)
        Catch ex As Exception
        End Try

    End Sub

    Public Sub InitFilterFieldsFromPostBackData()
        If Not Me._postBackData Is Nothing Then
            Me.Filter_IDTextBox.Text = Me._postBackData.filterID
            Me.Filter_NameTextBox.Text = Me._postBackData.filterName
            Me.Filter_AdressTextBox.Text = Me._postBackData.filterAddress
            Me.anwesendDropDownList.SelectedValue = Me._postBackData.filterAnwesend.ToString
            Me.vortraghighlightDropDownList.SelectedValue = Me._postBackData.filterVortragHighlight
        End If
    End Sub

    Public Sub HighlightNotEmptyFilterFields()
        If Not String.IsNullOrEmpty(Me.Filter_IDTextBox.Text) AndAlso Not Me.Filter_IDTextBox.Text = "0" Then
            Me.Filter_IDTextBox.BackColor = Drawing.Color.Orange
        Else
            Me.Filter_IDTextBox.BackColor = Drawing.Color.White
        End If

        If Not String.IsNullOrEmpty(Me.Filter_NameTextBox.Text) Then
            Me.Filter_NameTextBox.BackColor = Drawing.Color.Orange
        Else
            Me.Filter_NameTextBox.BackColor = Drawing.Color.White
        End If

        If Not String.IsNullOrEmpty(Me.Filter_AdressTextBox.Text) Then
            Me.Filter_AdressTextBox.BackColor = Drawing.Color.Orange
        Else
            Me.Filter_AdressTextBox.BackColor = Drawing.Color.White
        End If

        If Not (Me.anwesendDropDownList.SelectedValue = "3") Then
            Me.anwesendDropDownList.BackColor = Drawing.Color.Orange
        Else
            Me.anwesendDropDownList.BackColor = Drawing.Color.White
        End If

    End Sub


    Protected Sub SaveSortingToSession(ByVal expression As String, ByVal direction As SortDirection)
        Me.Session(SORT_DIRECTION_SESSION_NAME) = direction
        Me.Session(SORT_EXPRESSION_SESSION_NAME) = expression
    End Sub

    Protected Sub RestoreSortingFromSession()
        If Me.Session(SORT_EXPRESSION_SESSION_NAME) Is Nothing Then
            Me.Session(SORT_EXPRESSION_SESSION_NAME) = ""
        End If
        If Me.Session(SORT_DIRECTION_SESSION_NAME) Is Nothing Then
            Me.Session(SORT_DIRECTION_SESSION_NAME) = SortDirection.Ascending
        End If
        Me.ListeGridView.Sort(Me.Session(SORT_EXPRESSION_SESSION_NAME), Me.Session(SORT_DIRECTION_SESSION_NAME))
    End Sub

    ''' <summary>
    ''' This is a helper method used to determine the index of the
    ''' column being sorted. If no column is being sorted, -1 is returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSortColumnIndex() As Integer
        ' Iterate through the Columns collection to determine the index
        ' of the column being sorted.
        Dim field As DataControlField
        For Each field In ListeGridView.Columns
            If field.SortExpression = ListeGridView.SortExpression Then
                Return ListeGridView.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' This is a helper method used to add a sort direction
    ''' image to the header of the column being sorted.
    ''' </summary>
    ''' <param name="columnIndex"></param>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Sub AddSortImage(ByVal columnIndex As Integer, ByVal row As GridViewRow)
        ' Create the sorting image based on the sort direction.
        Dim sortImage As New Image()
        If ListeGridView.SortDirection = SortDirection.Ascending Then
            sortImage.ImageUrl = URL_ACCENDING_SORT_IMAGE
            sortImage.AlternateText = "" '"Ascending Order"
        Else
            sortImage.ImageUrl = URL_DECCENDING_SORT_IMAGE
            sortImage.AlternateText = "" '"Descending Order"
        End If
        ' Add the image to the appropriate header cell.
        row.Cells(columnIndex).Controls.Add(sortImage)

    End Sub

    Public Function GetAbstractsAnzahl(ByVal memberid As Integer) As Integer
        For Each i As Integer() In Me._abstracts
            If i(0) = memberid Then
                Return i(1)
            End If
        Next
        Return 0
    End Function

    Public Function GetBeitraege(ByVal memberid As Integer) As List(Of Beitrag)
        Dim ausgabe As New List(Of Beitrag)
        For Each b As Beitrag In Me._beitraege
            If b.memberid = memberid Then
                ausgabe.Add(b)
            End If
        Next
        Return ausgabe
    End Function

    Public Sub GetBeitraegeFromDatabase()
        Dim sql As String = "SELECT * FROM beitrag"

        Dim filterExp As String = ""
        Dim concat As String = ""

        If filterExp <> "" Then
            sql += " WHERE " + filterExp
        End If

        sql += " ORDER BY tag, art, nummer"

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)
            conn.Open()
            Dim comm As New SqlCommand(sql, conn)
            Dim dr As SqlDataReader = comm.ExecuteReader
            While dr.Read
                Me._beitraege.Add(New Beitrag(dr("id"), dr("memberid"), dr("tag"), dr("art"), dr("nummer"), dr("start"), dr("ende")))
            End While
            conn.Close()
        End Using
    End Sub

    Public Function CreateTagDDL() As DropDownList
        Dim ddl As New DropDownList
        ddl.ID = NAME_TAGE_DDL

        ddl.Items.Add(New ListItem("", ""))
        For Each c As Char In ARRAY_CONFERENCE_TAGE
            Dim li As New ListItem(c, c)
            ddl.Items.Add(li)
        Next
        Return ddl
    End Function

    Public Function CreateTypDDL() As DropDownList
        Dim ddl As New DropDownList
        ddl.ID = NAME_TYP_DDL

        ddl.Items.Add(New ListItem("", ""))
        For Each c As Char In ARRAY_BEITRAG_TYPES
            Dim li As New ListItem(c, c)
            ddl.Items.Add(li)
        Next
        Return ddl
    End Function

    Public Function CreateNummerDDL() As DropDownList
        Dim ddl As New DropDownList
        ddl.ID = NAME_NUMMER_DDL
        ddl.Items.Add(New ListItem("", ""))
        For i As Integer = 1 To 50
            ddl.Items.Add(New ListItem(i.ToString, i.ToString))
        Next
        Return ddl
    End Function
#End Region 'Procedures

#Region "DML functions"
    Public Function SetAnwesenheitForMember(ByVal memberid As Integer, ByVal anwesend As Boolean) As String
        'Dim ausgabe As String = "Testfehler beim Markieren des Members " + memberid.ToString + " als anwesend"
        Dim ausgabe As String = ""

        Dim check As String = ""
        If anwesend Then
            check = "1"
        Else
            check = "0"
        End If

        Dim checkInDate As String = "NULL"
        If anwesend Then
            checkInDate = "convert(datetime, '" _
                            + Now.Year.ToString _
                            + Now.Month.ToString.PadLeft(2, "0") _
                            + Now.Day.ToString.PadLeft(2, "0") _
                            + " " + Now.Hour.ToString.PadLeft(2, "0") _
                            + ":" + Now.Minute.ToString.PadLeft(2, "0") _
                            + ":" + Now.Second.ToString.PadLeft(2, "0") _
                            + "." + Now.Millisecond.ToString.PadLeft(3, "0") _
                            + "', 112)"

        End If

        Dim sql As String = "UPDATE temp_participants SET anwesend = " + check + ", checkindate = " + checkInDate + " WHERE participantsid = " + memberid.ToString

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)
            conn.Open()
            Dim comm As New SqlCommand(sql, conn)
            Try
                comm.ExecuteNonQuery()
            Catch ex As Exception
                ausgabe = "Fehler beim Markieren des Teilnehmers " + memberid.ToString + " als "
                If anwesend Then
                    ausgabe += "anwesend. "
                Else
                    ausgabe += "abwesend. "
                End If

                ausgabe += "Fehlerbeschreibung: " + ex.Message
            End Try
            conn.Close()
        End Using

        Return ausgabe
    End Function

    Public Function InitFooterStatistics(ByVal filterExpression As String) As String
        Dim ausgabe As String = ""

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConfsConnectionString").ConnectionString)
            Dim befehl As New SqlCommand()
            befehl.Connection = conn

            'If Me.ListeSqlDataSource.FilterExpression.Trim <> "" Then posterCountFilter += " AND (" + Me.ListeSqlDataSource.FilterExpression + ")"
            Dim countFilter As String = ""
            'If Me.ListeSqlDataSource.FilterExpression.Trim <> "" Then posterCountFilter += " AND (" + Me.ListeSqlDataSource.FilterExpression + ")"
            If Me.whereClause.Trim <> "" Then countFilter += " AND (" + Me.whereClause + ")"

            Dim SqlStr As String = "SELECT " + _
                                    "COUNT(*) AS anzgesamt, " + _
                                    "(SELECT COUNT(*) FROM [temp_participantsstatistik] WHERE ([anwesend] = 1) " + countFilter + ") AS anzanwesend, " + _
                                    "(SELECT COUNT(*) FROM [temp_participantsstatistik] WHERE (DATALENGTH(bemerkungenparticipant) > 0) " + countFilter + ") AS anzvortrag, " + _
                                    "(SELECT COUNT(*) FROM [temp_participantsstatistik] WHERE ([listeeintrag] = 1) " + countFilter + ") AS anzlisteeintrag, " + _
                                    "(SELECT COUNT(*) FROM [temp_participantsstatistik] WHERE ([teilnahmebestatigung] = 1) " + countFilter + ") AS anzteilnahmebestatigung " + _
                                    "FROM [temp_participantsstatistik]"

            'If Me.ListeSqlDataSource.FilterExpression.Trim <> "" Then SqlStr += " WHERE " + Me.ListeSqlDataSource.FilterExpression
            If Me.whereClause.Trim <> "" Then SqlStr += " WHERE " + Me.whereClause

            befehl.CommandText = SqlStr

            Dim dr As SqlDataReader
            Dim zeile As String = ""
            Try
                conn.Open()
                dr = befehl.ExecuteReader
                If (dr.Read) Then

                    If Not TypeOf dr("anzgesamt") Is DBNull Then
                        Me._footerStatistics.AnzahlGesamt = CInt(dr("anzgesamt"))
                    End If

                    If Not TypeOf dr("anzanwesend") Is DBNull Then
                        Me._footerStatistics.AnzahlAnwesend = CInt(dr("anzanwesend"))
                    End If

                    If Not TypeOf dr("anzvortrag") Is DBNull Then
                        Me._footerStatistics.AnzahlVortrag = CInt(dr("anzvortrag"))
                    End If

                    If Not TypeOf dr("anzlisteeintrag") Is DBNull Then
                        Me._footerStatistics.AnzahlTeilnehmerliste = CInt(dr("anzlisteeintrag"))
                    End If
                    If Not TypeOf dr("anzteilnahmebestatigung") Is DBNull Then
                        Me._footerStatistics.AnzahlTeilnehmerbestaetigung = CInt(dr("anzteilnahmebestatigung"))
                    End If
                    'e.Row.Cells(12).Text = dr_anzlisteeintrag.ToString
                    'e.Row.Cells(13).Text = dr_teilnamebestatigung.ToString
                Else
                    ausgabe += "Fehler beim Berechnet von Statistiken.  " + vbCrLf
                End If
                dr.Close()
            Catch ex As Exception
                ausgabe += "Berechnen von Statistiken fehlgeschlagen. .NET sagt: " + ex.Message
            End Try
            conn.Close()
        End Using

        Return ausgabe
    End Function

#End Region 'DML functions

#Region "DEBUG"

    Protected Sub _index_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Me.message.Text = Me._messageText  'DEBUG
    End Sub

#End Region 'DEBUG

#Region "Overriden Procedures"

#End Region

#Region "Helper Functions"
    Protected Function ContribShortText(ByVal langText As String) As String
        Dim ausgabe As String = ""
        If langText.ToLower.StartsWith("p") Then
            ausgabe = "poster"
        ElseIf langText.ToLower.StartsWith("o") Then
            ausgabe = "oral"
        End If

        Return ausgabe
    End Function

    Protected Function BackgroundOrTransparent(ByVal needHighlight As Boolean, ByVal color As String) As System.Drawing.Color
        If needHighlight Then
            Return System.Drawing.Color.FromName(color)
        Else
            'Return Drawing.Color.Transparent
            Return System.Drawing.Color.FromName("white")
        End If

    End Function



#End Region 'Helper Functions

#Region "Validators"
    Public Sub validateParticipantdauer(ByVal source As Object, ByVal args As ServerValidateEventArgs)

        If String.IsNullOrEmpty(args.Value) Then
            args.IsValid = True
        Else
            Try
                Dim i As Integer = CInt(args.Value)
                If i >= 0 Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            Catch ex As Exception
                args.IsValid = False
            End Try
        End If
    End Sub
#End Region

#Region "Highlighting"
    Protected Function GetColorForWunschtermin(ByVal dbValue As Object) As System.Drawing.Color
        Dim ausgabe As System.Drawing.Color = Nothing 'System.Drawing.Color.Transparent
        If Not TypeOf (dbValue) Is DBNull Then
            Dim datum As String = (Convert.ToString(dbValue))
            'If Not String.IsNullOrEmpty(Me._postBackData.filterVortragHighlight) AndAlso Not String.IsNullOrEmpty(datum) AndAlso datum.StartsWith(Me._postBackData.filterVortragHighlight) Then
            If Not String.IsNullOrEmpty(Me.vortraghighlightDropDownList.SelectedValue) AndAlso Not String.IsNullOrEmpty(datum) AndAlso datum.StartsWith(Me.vortraghighlightDropDownList.SelectedValue) Then
                ausgabe = System.Drawing.Color.Orange
            End If
        End If

        Return ausgabe
    End Function

    Protected Function GetColorForWarning(ByVal dbWarning As Object) As System.Drawing.Color
        Dim ausgabe As System.Drawing.Color = Nothing 'System.Drawing.Color.Transparent
        If Not TypeOf (dbWarning) Is DBNull AndAlso Not String.IsNullOrEmpty(Convert.ToString(dbWarning)) Then
            ausgabe = System.Drawing.Color.FromArgb(252, 120, 140)
        End If
        Return ausgabe
    End Function
#End Region

#Region "Concurency"

    ''' <summary>
    ''' Zeigt die Mitteilung, dass der aktuelle Datensatz von einem anderen Benutzer bearbeitet wird.
    ''' Blendet die Schaltfläche "Update" aus, blendet die Schaltfläche "Unlock" ein.
    ''' </summary>
    ''' <param name="bearbeiter_ID">Bezeichnung des Bearbeiters der den Datendatz gesperrt hat (wird für die Mitteilung gebraucht)</param>
    ''' <param name="lastAccessDiffSec">Anzahl der Sekunden seit dem letzten Zugriff (wird für die Mitteilung gebraucht)</param>
    ''' <remarks></remarks>
    Public Sub ShowConcurency(ByVal bearbeiter_ID As String, ByVal lastAccessDiffSec As Long)
        Me.message.Font.Bold = True
        Me.message.ForeColor = Drawing.Color.Red
        Me.message.Text = "Dieser Datensatz wird von [" + bearbeiter_ID.Split(";")(0) + "] bearbeitet" _
                        + " (letzter Zugriff vor " + (lastAccessDiffSec \ 60).ToString + " Minuten)<br /><br />"


        Dim updateBtn As Button = Me.Details_MemberFormView.FindControl("UpdateButton")
        'Dim updateBtn As Button = GlobFunctions.SuperFindControl(Me.Details_MemberFormView.Row, "UpdateButton")
        If Not updateBtn Is Nothing Then
            updateBtn.Enabled = False
        End If
        Dim unlockBtn As Button = Me.Details_MemberFormView.FindControl("UpdateUnlockButton")
        If Not unlockBtn Is Nothing Then
            unlockBtn.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' Der aktuelle Benutzer sperrt den Datensatz. Gleichzeitig werden andere vom aktuellen Benutzer gesperrte Datensätze ensperrt.
    ''' </summary>
    ''' <param name="recordID"></param>
    ''' <remarks></remarks>
    Public Sub LockRecordForEditing(ByVal recordID As Integer)
        Dim found As Boolean = False

        'Da wir manche records löschen möchten, iterieren wir rückwards.
        For i As Integer = Me.LockedRecords.Count - 1 To 0 Step -1
            Dim t As Triplet = Me.LockedRecords(i)
            If CInt(t.First = recordID) Then 'sperren
                t.Second = Me.BearbeiterID
                t.Third = Now
                found = True
            ElseIf t.Second = Me.BearbeiterID Then 'entsperren
                Me.LockedRecords.RemoveAt(i)
            End If
        Next

        If Not found Then
            Me.LockedRecords.Add(New Triplet(recordID, Me.BearbeiterID, Now))
        End If
    End Sub

    Public Sub UnlockRecordForEditing(ByVal recordID As Integer, Optional ByVal doNotTouchLocksFromOthers As Boolean = False)
        For i As Integer = 0 To Me.LockedRecords.Count - 1
            If CInt(Me.LockedRecords(i).First = recordID) Then
                Dim allowedRemove As Boolean = (Not doNotTouchLocksFromOthers) Or _
                                            CStr(Me.LockedRecords(i).Second) = Me.BearbeiterID
                If allowedRemove Then Me.LockedRecords.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' wenn von anderem Bearbeiter gesperrt - Rückgabe bearbeitername und sekunden. wenn von sich selbst gesperrt Rückgabe "" und -1
    ''' </summary>
    ''' <param name="recordID"></param>
    ''' <returns>
    ''' first - Bearbeiter des gesperrten Records (leer wenn der Record ist nicht gesperrt). 
    ''' second - sekunden seit dem letzten Zugriff (-1 wenn Record nicht gesperrt)
    ''' </returns>
    ''' <remarks></remarks>
    Public Function IsRecordLockedForEditing(ByVal recordID As Integer) As Pair
        Dim ausgabe As New Pair("", -1)
        For i As Integer = 0 To Me.LockedRecords.Count - 1
            If CInt(Me.LockedRecords(i).First) = recordID AndAlso Not CStr(Me.LockedRecords(i).Second) = Me.BearbeiterID Then
                ausgabe.First = Me.LockedRecords(i).Second
                ausgabe.Second = DateDiff(DateInterval.Second, CDate(Me.LockedRecords(i).Third), Now)
                Exit For
            End If
        Next

        Return ausgabe
    End Function
#End Region 'Concurency

End Class
