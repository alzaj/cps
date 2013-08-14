<%@ Page Title="" Language="VB" MasterPageFile="qcnp09.master" AutoEventWireup="false" CodeFile="sendmail.aspx.vb" Inherits="report_sendmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="Button1" runat="server" Text="Send mails" /><br />
    <asp:Literal ID="message" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>

