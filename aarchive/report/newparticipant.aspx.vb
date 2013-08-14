
Partial Class newparticipant
    Inherits System.Web.UI.Page

    Protected Sub newparticipant_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        message.Text = ""
    End Sub

    Protected Sub Insert_MemberSqlDataSource_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles Insert_MemberSqlDataSource.Inserted
        If e.AffectedRows = 1 Then
            Dim memberid As String = ""
            If Not (e.Command.Parameters("@newID").Value Is Nothing) Then
                memberid = e.Command.Parameters("@newID").Value.ToString
                memberid = "?" + Q_Names.Q_STRING_MEMBER_TO_SELECT + "=" + memberid
            End If
            Response.Redirect(F_NAMES.F_URL_AGENT_DEFAULT + memberid)
        Else
            Dim msg As String = "Inserting failed." + vbCrLf
            If Not e.Exception Is Nothing Then
                msg += " Details: " + e.Exception.Message
            End If
            e.ExceptionHandled = True
            Me.message.Text = msg + "<br><br>"
            GlobFunctions.WriteToLogFile("newparticipant.aspx - " + msg)
        End If
    End Sub


    Protected Sub Insert_MemberFormView_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewCommandEventArgs) Handles Insert_MemberFormView.ItemCommand
        If e.CommandName.ToLower = "cancel" Then Response.Redirect(F_NAMES.F_URL_AGENT_DEFAULT)
    End Sub

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
End Class
