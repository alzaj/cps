
Partial Class styles_change
    Inherits GeneralWraperPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        messageLbl.Text += "<a href=""" + Me.ResolveUrl("~/default.aspx") + "?" + Q_Names.Q_setTheme + "=TabsMenu"">Use TabsMenu Yellow Skin</a><br />" + vbCrLf
        messageLbl.Text += "<a href=""" + Me.ResolveUrl("~/default.aspx") + "?" + Q_Names.Q_setTheme + "=TabsMenuGreen"">Use TabsMenu Green Skin</a><br />" + vbCrLf
        messageLbl.Text += "<a href=""" + Me.ResolveUrl("~/default.aspx") + "?" + Q_Names.Q_setTheme + "=SideMenu"">Use SideMenu Skin</a><br />" + vbCrLf
        messageLbl.Text += "<a href=""" + Me.ResolveUrl("~/default.aspx") + "?" + Q_Names.Q_setTheme + "=SideMenuGreen"">Use SideMenu Green Skin</a><br />" + vbCrLf
        messageLbl.Text += "<a href=""" + Me.ResolveUrl("~/default.aspx") + "?" + Q_Names.Q_setTheme + "=bootstrap"">Use Twitter Bootstrap Skin</a><br />" + vbCrLf
    End Sub
End Class
