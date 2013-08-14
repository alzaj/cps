<%@ Page Title="" Language="VB" MasterPageFile="~/report/qcnp09.master" AutoEventWireup="false" CodeFile="daystatistics.aspx.vb" Inherits="report_daystatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderOben" Runat="Server">
<h3>Geplannte Anzahl der Teilnehmer pro Tag</h3>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        EnableModelValidation="True" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="konfDay" HeaderText="Tag" DataFormatString="{0:dd.MM ddd}" />
            <asp:BoundField DataField="anzahl" HeaderText="Anzahl">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="countArrival" HeaderText="kommen an">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="countDeparture" HeaderText="reisen ab">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
        SelectCommand="daysstatistics" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <br />
<asp:HyperLink ID="homeHyperLink" runat="server" NavigateUrl="~/report/default.aspx">zurück zur Teilnehmerübersicht</asp:HyperLink><br />
</asp:Content>

