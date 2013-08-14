
Partial Class pictures_default
    Inherits GeneralWraperPage

    Protected zipFilename As String = "haeko2011_images.zip"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not System.IO.File.Exists(Me.MapPath(Me.zipFilename)) Then
            Literal1.Text = " Dear Webmaster, please kompress all images to a Zip file, name this file """ + zipFilename + """ and place it to the directory where this page is stored. So can our guests download all pics as a single file."
        Else
            Dim fi As New System.IO.FileInfo(Me.MapPath(Me.zipFilename))
            Dim einheiten As String = "K"
            Dim formatstring As String = "F0"
            Dim size As Decimal = fi.Length / 1024
            If size > 1000 Then
                size = fi.Length / 1024 / 1024
                einheiten = "M"
                formatstring = "F1"
            End If

            Literal1.Text = "(" + size.ToString(formatstring, qcnp09.D_DATUM_FORMAT) + " " + einheiten + ")"
        End If
    End Sub
End Class
