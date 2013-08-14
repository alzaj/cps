<%--CPFS_ID:000001--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="intro.aspx.vb" Inherits="qcnp09_registration_intro" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<%  If qcnp09.ConferenceState = ConferenceStatus.ConferenceStates.preparation Then%>
Registration is not opened jet... please try later.
<%  ElseIf qcnp09.ConferenceState >= ConferenceStatus.ConferenceStates.registration_closed Then%>
    <h3>Die Online-Anmeldung für das Hemdsärmelkolloquium 2011 ist nicht mehr möglich.</h3>
<%  End If%>
<p>        
Für weitere Informationen wenden Sie sich bitte an <a href='<%= "mailto:" + qcnp09.E_SUPPORT_EMAIL %>'><%= qcnp09.E_SUPPORT_EMAIL %></a>.
</p>

</asp:Content>

