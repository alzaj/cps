<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="scientific_program_default" title="HaeKo 2011 - Programm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<h2>Programm</h2>
Das Hemdsärmelkolloquium findet vom 
<%= qcnp09.D_WORKSHOP_START.ToString("%d.", qcnp09.D_DATUM_FORMAT)%>
<%  If qcnp09.D_WORKSHOP_START.Month <> qcnp09.D_WORKSHOP_ENDE.Month Then%>
<%= qcnp09.D_WORKSHOP_START.ToString("MMMM", qcnp09.D_DATUM_FORMAT)%>
<% End If%>
<%  If qcnp09.D_WORKSHOP_START.Year <> qcnp09.D_WORKSHOP_ENDE.Year Then%>
<%= qcnp09.D_WORKSHOP_START.ToString("yyyy", qcnp09.D_DATUM_FORMAT)%>
<% End If%>
bis
<%= qcnp09.D_WORKSHOP_ENDE.ToString("%d. MMMM yyyy", qcnp09.D_DATUM_FORMAT)%>
in der Dreikönigskirche Dresden statt.
<br />
<br />

<table>
<tr>
<td><br /></td>
<td></td>
</tr>
<tr>
<td colspan="2"><b>Donnerstag, 10. März 2011</b></td>
</tr>

<tr>
<td>bis 13:00 Uhr&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td>Anreise</td>
</tr>
<tr>
<td>13:00 - 13:15</td>
<td>Begrüßung</td>
</tr>
<tr>
<td>13:15 - 15:00</td>
<td>Vorträge</td>
</tr>
<tr>
<td>15:00 - 15:30</td>
<td>Kaffeepause</td>
</tr>
<tr>
<td>15:30 - 17:00</td>
<td>Vorträge</td>
</tr>
<tr>
<td>17:00 - 17:30</td>
<td>Kaffeepause</td>
</tr>
<tr>
<td>17:30 - 19:00</td>
<td>Vorträge</td>
</tr>
<tr>
<td>ab 19:30 Uhr</td>
<td>Willkommensimbiss</td>
</tr>
<tr>
<td><br /></td>
<td></td>
</tr>
<tr>
<td colspan="2"><b>Freitag, 11. März 2011</b></td>
</tr>

<tr>
<td>09:00 - 10:45</td>
<td>Vorträge</td>
</tr>
<tr>
<td>10:45 - 11:15</td>
<td>Kaffeepause</td>
</tr>
<tr>
<td>11:15 - 13:00</td>
<td>Vorträge</td>
</tr>
<tr>
<td>13:00 - 15:00</td>
<td>Mittagspause</td>
</tr>
<tr>
<td>14:30 - 16:30</td>
<td>Vorträge</td>
</tr>
<tr>
<td>16:30 - 17:00</td>
<td>Kaffeepause</td>
</tr>
<tr>
<td>17:00 - 18:30</td>
<td>Vorträge</td>
</tr>
<tr>
<td>18:45 - 19:15</td>
<td>Orgelkonzert</td>
</tr>
<tr>
<td>ab 19:30 Uhr</td>
<td>Abendessen (Imbiss)</td>
</tr>

<tr>
<td><br /></td>
<td></td>
</tr>
<tr>
<td colspan="2"><b>Sonnabend, 12. März 2011</b></td>
</tr>

<tr>
<td>09:00 - 10:15</td>
<td>Vorträge</td>
</tr>
<tr>
<td>10:15 - 10:45</td>
<td>Kaffeepause</td>
</tr>
<tr>
<td>10:45 - 12:30</td>
<td>Vorträge</td>
</tr>
<tr>
<td>ab 12:30</td>
<td>Mittagsimbiss (Suppe)</td>
</tr>
<tr>
<td>13:30 Uhr</td>
<td>Ende des Kolloquiums und Abreise</td>
</tr>

</table>


</asp:Content>

