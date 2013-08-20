Imports Microsoft.VisualBasic

Public Class MasterOpa
    Inherits System.Web.UI.MasterPage

    Protected ReadOnly Property PageTitlePrefix As String
        Get
            Dim ausgabe As String = ""
            Try
                ausgabe = CType(Me.Page, GeneralWraperPage).PageTitlePrefix
            Catch ex As Exception
            End Try
            Return ausgabe
        End Get
    End Property


    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim prefix As String = Me.PageTitlePrefix
        If Not String.IsNullOrEmpty(prefix) Then
            Me.Page.Title = prefix + " - " + Me.Page.Title
        End If
    End Sub
End Class
