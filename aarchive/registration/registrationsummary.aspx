<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registrationsummary.aspx.vb" Inherits="registration_registrationsummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HaeKo 2011 - Zusammenfassung</title>
</head>
<body style="border-top-style:none;">
    <form id="form1" runat="server">
    <div class="printheader">
    Hemdsärmelkolloquium, <%= qcnp09.D_WORKSHOP_START.ToString("%d", qcnp09.D_DATUM_FORMAT)%>-<%= qcnp09.D_WORKSHOP_ENDE.ToString("%d MMMM yyyy")%>, Dresden
    </div>
    <asp:Label ID="message" runat="server" Text=""></asp:Label>
    <div id="printcontent" style="margin-left:2em;">
<asp:Literal ID="summaryLbl" runat="server"></asp:Literal>
    </div>
    <div class="printfooter">
        <%= Now.ToString("yyyy", qcnp09.D_DATUM_FORMAT)%>, Max-Planck-Institut für Chemische Physik fester Stoffe
    </div>
    </form>
</body>
</html>
