
Partial Class links_default
    Inherits GeneralWraperPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Page.Header Is Nothing Then
            Throw New System.InvalidOperationException("Using CSSImageGallery WebControl requires a header control on the page or on the masterpage. (e.g. &lt;head runat=""server"" /&gt;).")
        End If

        Dim cssText As String = vbCrLf

        cssText = "<style type=""text/css"">" + vbCrLf + _
"/* ================================================================ " + vbCrLf + _
"This copyright notice must be untouched at all times." + vbCrLf + _
"" + vbCrLf + _
"The original version of this stylesheet and the associated (x)html" + vbCrLf + _
"is available at http://www.cssplay.co.uk/menu/cssplay-clickbox.html" + vbCrLf + _
"Copyright (c) 2005-2010 Stu Nicholls. All rights reserved." + vbCrLf + _
"This stylesheet and the associated (x)html may be modified in any " + vbCrLf + _
"way to fit your requirements." + vbCrLf + _
"=================================================================== */" + vbCrLf + _
"a.clickbox, a.clickbox:visited, a.clickbox:hover {text-decoration:none; text-align:center;}" + vbCrLf + _
"a.clickbox img {display:block; border:0;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox b {display:block;}" + vbCrLf + _
"a.clickbox em {font:bold 10px/12px arial,sans-serif; color:#000;}" + vbCrLf + _
"a.clickbox {float:left; margin:0 15px 15px 0; display:inline;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .lightbox {position:absolute; left:-9999px; top:-10000px; cursor:default; z-index:500;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .light {position:absolute; left:0; top:0; width:100%;}" + vbCrLf + _
"a.clickbox .box {position:absolute; left:0; width:100%; text-align:center; height:300px; top:30%; margin-top:-150px;}" + vbCrLf + _
"/* trigger for IE6 */" + vbCrLf + _
"a.clickbox:active {direction:ltr;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox:active .lightbox {left:0; top:0; width:100%; height:100%;}" + vbCrLf + _
"a.clickbox .lightbox:hover," + vbCrLf + _
"a.clickbox:focus .lightbox {position:fixed; left:0; top:0; width:100%; height:100%;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .lightbox:hover .light," + vbCrLf + _
"a.clickbox:active .lightbox .light," + vbCrLf + _
"a.clickbox:focus .lightbox .light {background:#fff; width:100%; height:100%; filter: alpha(opacity=90);" + vbCrLf + _
" filter: progid:DXImageTransform.Microsoft.Alpha(opacity=90); opacity:0.90;" + vbCrLf + _
"}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .lightbox:hover .box img," + vbCrLf + _
"a.clickbox:active .lightbox .box img," + vbCrLf + _
"a.clickbox:focus .lightbox .box img {border:1px solid #ddd; margin:0 auto; padding:30px; background:#fff;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .lightbox:hover .box span," + vbCrLf + _
"a.clickbox:active .lightbox .box span," + vbCrLf + _
"a.clickbox:focus .lightbox .box span {display:block; width:560px; padding:0; margin:10px auto; text-align:center; text-decoration:none; background:#fff; border:1px solid #ddd;}" + vbCrLf + _
"a.clickbox .lightbox .box span.title {font-weight:bold;font-size:140%;color:#666;}" + vbCrLf + _
"a.clickbox .lightbox .box span.text {font:normal 11px/16px verdana, sans-serif; color:#333;}" + vbCrLf + _
"" + vbCrLf + _
".clear {clear:left;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox i {display:block; width:32px; height:32px; position:fixed; right:-100px; top:0; z-index:500;}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .lightbox:hover i," + vbCrLf + _
"a.clickbox:active i," + vbCrLf + _
"a.clickbox:focus i {right:50%; top:30%; background:url(close2.png); margin-right:-345px; margin-top:-165px;}" + vbCrLf + _
"" + vbCrLf + _
"#close {display:block; position:fixed; width:32px; height:32px; right:50%; top:30%; margin-right:-345px; margin-top:-165px; z-index:1000; background:url(trans.gif); cursor:pointer;}" + vbCrLf + _
"" + vbCrLf + _
"" + vbCrLf + _
"</style>" + vbCrLf + _
"<!--[if lte IE 6]>" + vbCrLf + _
"<style type=""text/css"">" + vbCrLf + _
"/* to get IE6 to center the Clickbox - adjust the height to cover the whole page */" + vbCrLf + _
"a.clickbox:active .lightbox {left:50%; margin-left:-2500px; height:2000px; width:5000px;}" + vbCrLf + _
"a.clickbox:active .lightbox .light {height:2000px;}" + vbCrLf + _
"a.clickbox i {display:block; width:32px; height:32px; overflow:hidden; float:right; cursor:pointer; position:static; background:url(close.png);}" + vbCrLf + _
"#close {margin-right:0; margin-top:0; z-index:1000; background:url(trans.gif); cursor:pointer;}" + vbCrLf + _
"a.clickbox .lightbox:hover i," + vbCrLf + _
"a.clickbox:active i {right:32px; top:32px; background:url(close.png); margin-right:0; margin-top:0;  background:url(close.png);}" + vbCrLf + _
"" + vbCrLf + _
"a.clickbox .frame {width:600px; height:500px; background:#fff; border:1px solid #000; padding:10px;}" + vbCrLf + _
"a.clickbox .box {top:5%; margin-top:0;}" + vbCrLf + _
"</style>" + vbCrLf + _
"<![endif]-->"

        Me.Page.Header.Controls.Add(New LiteralControl(cssText))
    End Sub
End Class
