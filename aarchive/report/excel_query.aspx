<%@ Page Language="VB" AutoEventWireup="false" CodeFile="excel_query.aspx.vb" Inherits="report_excel_query" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
<title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="message" runat="server"></asp:Literal>
        <asp:SqlDataSource ID="ListeSqlDataSource" runat="server"  EnableViewState="false"
                           ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
                           SelectCommand="SELECT [participantsid], 'created' = convert(varchar,registrationdate,104) ,[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[email],[arrival],[departure],[bemerkungen],[listeeintrag],[teilnahmebestatigung],[bemerkungenparticipant] AS 'vortragstitel',[participanttermin],[participantdauer],[checkindate] FROM [dbo].[temp_participants] WHERE [existiert] = 1"
                           >
        </asp:SqlDataSource>
        <asp:GridView ID="ListeGridView" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="participantsid" DataSourceID="ListeSqlDataSource" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" EnableViewState="false">
            <Columns>
                <asp:TemplateField HeaderText="ID" SortExpression="participantsid">
                    <ItemTemplate>
                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("participantsid") %>'></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="created" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Angemeldet am"
                    HtmlEncode="False" SortExpression="registrationdate" ReadOnly="True">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Last Name" SortExpression="surname">
                    <ItemTemplate>
                        <asp:Literal ID="surnameLiteral" runat="server" Text='<%# Eval("surname") %>'></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name" SortExpression="firstname">
                    <ItemTemplate>
                        <asp:Literal ID="firstnameLiteral" runat="server" Text='<%# Eval("firstname") %>'></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="gender" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="titel" HeaderText="Title" SortExpression="titel" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="institut" HeaderText="Institut" SortExpression="institut" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="department" HeaderText="Department" SortExpression="department" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="postalcode" HeaderText="PLZ" SortExpression="postalcode" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="city" HeaderText="Ort" SortExpression="city" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="country" HeaderText="Land" SortExpression="country" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="phone" HeaderText="Telefon" SortExpression="phone" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="arrival" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Ankunft"
                    HtmlEncode="False" SortExpression="arrival" ReadOnly="True">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="departure" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Abreise"
                    HtmlEncode="False" SortExpression="departure" ReadOnly="True">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="bemerkungen" HeaderText="Bemerkungen" SortExpression="bemerkungen" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Teilnehmerliste" SortExpression="listeeintrag">
                    <ItemTemplate>
                        <asp:Label ID="listeeintragLabel" runat="server" Text="+" Visible='<%# cbool(Eval("listeeintrag")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teilnahmebest." SortExpression="teilnahmebestatigung">
                    <ItemTemplate>
                        <asp:Label ID="teilnahmebestatigungLabel" runat="server" Text="+" Visible='<%# cbool(Eval("teilnahmebestatigung")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vortragstitel" SortExpression="bemerkungenparticipant">
                    <ItemTemplate>
                        <asp:Label ID="vortragstitelLabel" runat="server" Text='<%# GlobFunctions.TextConvertTexToHtml(Eval("vortragstitel").ToString) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="participanttermin" HeaderText="Vortragstermin" SortExpression="participanttermin" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="participantdauer" HeaderText="Vortragsdauer" SortExpression="participantdauer" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="checkindate" DataFormatString="{0:dd.MM.yyyy HH:mm}" HeaderText="Eingecheckt"
                    HtmlEncode="False" SortExpression="checkindate" ReadOnly="True">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
