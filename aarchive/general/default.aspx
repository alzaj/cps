<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="general_default" title="HaeKo 2011 - Kontakt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Kontakt</h2>
<table>
<tr>
<td>
E-Mail: 
</td>
<td>
<b><%=qcnp09.E_SUPPORT_EMAIL%></b>
</td>
</tr>
<tr>
<td colspan="2">
<br />
<u>Katrin Demian</u>
</td>
</tr>
<tr>
<td>Tel.: </td>
<td> +49 351 4646 4271</td>
</tr>
<tr>
<td>Fax: </td>
<td> +49 351 4646 4641</td>
</tr>
<tr>
<td colspan="2">
<br />
<u>Guido Kreiner</u>
</td>
</tr>
<tr>
<td>Tel.: </td>
<td> +49 351 4646 3323</td>
</tr>
<tr>
<td>Fax: </td>
<td> +49 351 4646 3002</td>
</tr>
<tr>
<td colspan="2" style="white-space:nowrap;">
<br />
Max-Planck-Institut für Chemische Physik fester Stoffe <br />
Nöthnitzer Straße 40<br />
01187 Dresden
</td>
</tr>
</table>
<br />
<img src='<%= Me.ResolveUrl("~/images/mpi_cpfs.jpg") %>' alt="MPI CPfS" />


</asp:Content>

