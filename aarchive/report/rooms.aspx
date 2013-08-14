<%@ Page Title="C-MAC 2010 Zimmer" Language="VB" MasterPageFile="~/report/qcnp09.master" AutoEventWireup="false" CodeFile="rooms.aspx.vb" Inherits="report_rooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderOben" Runat="Server">
<br />
<br />

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="roomid" 
        DataSourceID="SqlDataSource1" EnableModelValidation="True" 
        GridLines="Vertical" ShowFooter="True">
        <AlternatingRowStyle BackColor="Gainsboro" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="AddRoom" runat="server" CommandName="Insert" Text="Hizufügen" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="roomid" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="roomid" />
            <asp:BoundField DataField="roomname" HeaderText="Zimmerbezeichnung" 
                SortExpression="roomname" />
            <asp:BoundField DataField="notes" HeaderText="Bemerkungen" />
            <asp:TemplateField HeaderText="Anz. Pl&#228;tze" SortExpression="places">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("places") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# convert.tostring(Eval("places")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="noch frei" SortExpression="free">
                <ItemTemplate>
                    <asp:Label ID="freeLabel" runat="server" Text='<%# convert.toint32(Eval("free")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Personen">
                    <ItemTemplate>
                        <asp:Repeater ID="personenRepeater" runat="server">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/report/default.aspx?memberid=" + Eval("participantsid").ToString %>'><%# Eval("participantsname")%></asp:HyperLink><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
        DeleteCommand="DELETE FROM [temp_rooms] WHERE [roomid] = @roomid" 
        InsertCommand="INSERT INTO [temp_rooms] ([roomname], [places]) VALUES (@roomname, 0)" 
        SelectCommand="SELECT [roomid], [roomname], [places], [places] - (SELECT COUNT(*) FROM temp_participants WHERE (roomid = temp_rooms.roomid)) AS free, [notes] FROM [temp_rooms]" 
        UpdateCommand="UPDATE [temp_rooms] SET [roomname] = @roomname, [places] = @places, [notes] = @notes WHERE [roomid] = @roomid">
        <DeleteParameters>
            <asp:Parameter Name="roomid" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="roomname" Type="String" />
            <asp:Parameter Name="notes" Type="String" />
            <asp:Parameter Name="places" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="roomname" Type="String" />
            <asp:Parameter Name="notes" Type="String" />
            <asp:Parameter Name="places" Type="Int32" />
            <asp:Parameter Name="roomid" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sourceParticipants" 
                           runat="server" 
                           ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
                           ProviderName="System.Data.SqlClient" 
                           SelectCommand="SELECT participantsid, surname + ', ' + firstname AS 'participantsname' FROM temp_participants WHERE roomid=@roomid">
            <SelectParameters>
                <asp:Parameter Name="roomid" Type="Int32" />
            </SelectParameters>
    </asp:SqlDataSource> 
<br />
<asp:HyperLink ID="homeHyperLink" runat="server" NavigateUrl="~/report/default.aspx">zurück zur Teilnehmerübersicht</asp:HyperLink><br />
</asp:Content>

