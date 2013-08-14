<%@ Page Language="VB" AutoEventWireup="false" CodeFile="abstract.aspx.vb" Inherits="registration_abstract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Abstracts submission</h2>
Please <a href='<%= Me.ResolveUrl(qcnp09.U_FORMULAR_FILE) %>'>register</a> first. Then use <a href="<%= Me.ResolveURL(qcnp09.U_URL_ABSTRACT_TEMPLATE_DOC) %>">this Microsoft Word document</a> as a template for your abstract. The document also contains information on how to submit your abstract.
</asp:Content> 