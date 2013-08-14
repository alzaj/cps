<%@ Page Title="C-MAC Days - Deadlines" Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="deadlines_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Important Dates and Deadlines</h2>
<ul class="largeGap">
<li>Deadline for registration and guesthouse reservation:<br />
<b><%= qcnp09.D_DEADLINE_REGISTRATION.ToString(qcnp09.D_DATUM_FORMAT_STRING,qcnp09.D_DATUM_FORMAT) %></b>
</li>
<li>Deadline for abstract submission:<br />
<b><%= qcnp09.D_DEADLINE_ABSTRACT_SUBMISSION.ToString(qcnp09.D_DATUM_FORMAT_STRING, qcnp09.D_DATUM_FORMAT)%></b>
</li>
<li>Notification of acceptance (oral or poster presentation):<br />
<b><%= qcnp09.D_NOTIFICATION_OF_ACCEPTANCE.ToString(qcnp09.D_DATUM_FORMAT_STRING, qcnp09.D_DATUM_FORMAT)%></b>
</li>
</ul>
</asp:Content>

