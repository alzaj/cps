<%@ Page Language="VB" MasterPageFile="qcnp09.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_index" title="" MaintainScrollPositionOnPostback="false" %>
<%@ Register TagPrefix="cpfs" Namespace="CPFS" %>


<asp:Content ID="ObenContent" ContentPlaceHolderID="ContentPlaceHolderOben" runat="server">
    <asp:Label ID="message" runat="server" Text="" EnableViewState="false"></asp:Label>
    <asp:Panel ID="FilterPanel" runat="server">
        ID: <asp:TextBox ID="Filter_IDTextBox" runat="server" Width="3em" MaxLength="3"></asp:TextBox>&nbsp;
        Name: <asp:TextBox ID="Filter_NameTextBox" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="ListShowButton" runat="server" Text="Show" />
        <asp:Button ID="DetailsShowButton" runat="server" Text="Show" />
        Adresse: <asp:TextBox ID="Filter_AdressTextBox" runat="server"></asp:TextBox>&nbsp;
        Anwesenheit:
            <asp:DropDownList ID="anwesendDropDownList" runat="server">
                <asp:ListItem Value="3">Komplette Liste</asp:ListItem>
                <asp:ListItem Value="1">Nur anwesende</asp:ListItem>
                <asp:ListItem Value="2">Nur abwesende</asp:ListItem>
            </asp:DropDownList>
        Vorträge hervorheben:
            <asp:DropDownList ID="vortraghighlightDropDownList" runat="server">
                <asp:ListItem Value="10.03">10.03</asp:ListItem>
                <asp:ListItem Value="11.03">11.03</asp:ListItem>
                <asp:ListItem Value="11.03.2011 Vormittag">11.03.2011 Vormittag</asp:ListItem>
                <asp:ListItem Value="11.03.2011 Nachmittag">11.03.2011 Nachmittag</asp:ListItem>
                <asp:ListItem Value="12.03">12.03</asp:ListItem>
                <asp:ListItem Value="">Keine Hervorhebung</asp:ListItem>
            </asp:DropDownList>
        <asp:Literal ID="filtermsg" runat="server" EnableViewState="false"></asp:Literal>
    </asp:Panel>
    <asp:Panel ID="DetailsPanel" runat="server">
        <br /><br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:SqlDataSource ID="Details_MemberSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
                            SelectCommand="SELECT [participantsid], 'created' = convert(varchar,registrationdate,104) ,[workshopsid],[memberpin],[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[email],[registrationdate],[arrival],[departure],[paymenttype],[anwesend],[bemerkungen],[listeeintrag],[teilnahmebestatigung],[bemerkungenparticipant],[participanttermin],[participantdauer],[warning],[checkindate] FROM [dbo].[temp_participants]"
                            ConflictDetection="CompareAllValues" 
                            DeleteCommand="DELETE FROM FROM [dbo].[temp_participants] WHERE [participantsid] = @original_participantsid" 
                            OldValuesParameterFormatString="original_{0}" 
                            UpdateCommand="UPDATE [dbo].[temp_participants] SET [firstname] = @firstname,[surname] = @surname,[gender] = @gender,[titel] = @titel,[institut] = @institut,[department] = @department,[street] = @street,[postalcode] = @postalcode,[city] = @city,[country] = @country,[phone] = @phone,[email] = @email,[listeeintrag] = @listeeintrag,[teilnahmebestatigung] = @teilnahmebestatigung,[arrival] = @arrival,[departure] = @departure,[bemerkungen] = @bemerkungen,[bemerkungenparticipant]=@bemerkungenparticipant, [participanttermin]=@participanttermin, [participantdauer]=@participantdauer, [warning]=@warning WHERE [participantsid] = @original_participantsid">
            <DeleteParameters>
                <asp:Parameter Name="original_participantsid" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>

                <asp:Parameter Name="workshopsid" Type="Int32" />
                <asp:Parameter Name="firstname" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="surname" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="gender" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="titel" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="institut" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="department" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="street" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="postalcode" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="city" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="country" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="phone" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="email" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="registrationdate" Type="DateTime" />
                <asp:Parameter Name="arrival" Type="DateTime" />
                <asp:Parameter Name="departure" Type="DateTime" />

                <asp:Parameter Name="bemerkungenparticipant" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="participantdauer" Type="Int32" />
                <asp:Parameter Name="participanttermin" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="listeeintrag" Type="Boolean" />
                <asp:Parameter Name="teilnahmebestatigung" Type="Boolean" />
                <asp:Parameter Name="bemerkungen" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="warning" Type="String" ConvertEmptyStringToNull="false" />

                <asp:Parameter Name="original_workshopsid" Type="Int32" />
                <asp:Parameter Name="original_firstname" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_surname" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_gender" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_titel" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_institut" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_department" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_street" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_postalcode" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_city" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_country" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_phone" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_email" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_registrationdate" Type="DateTime" />
                <asp:Parameter Name="original_arrival" Type="DateTime" />
                <asp:Parameter Name="original_departure" Type="DateTime" />
                <asp:Parameter Name="original_participantsid" Type="Int32" />
                
                <asp:Parameter Name="original_bemerkungenparticipant" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_participantdauer" Type="Int32" />
                <asp:Parameter Name="original_participanttermin" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_listeeintrag" Type="Boolean" />
                <asp:Parameter Name="original_teilnahmebestatigung" Type="Boolean" />
                <asp:Parameter Name="original_bemerkungen" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="original_warning" Type="String" ConvertEmptyStringToNull="false" />
            </UpdateParameters>
        </asp:SqlDataSource>
        &nbsp;
        <asp:FormView ID="Details_MemberFormView" runat="server" CellPadding="4" DataKeyNames="participantsid" DataSourceID="Details_MemberSqlDataSource" DefaultMode="Edit" ForeColor="#333333">
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <EditItemTemplate>
                <table style="color:#663399; font-weight:bold;">
            <tr>
                <td colspan="2">
                <asp:Label ID="teilnehmer" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Black" Text='<%# Eval("surname").ToString + " " + Eval("firstname").ToString  %>'></asp:Label>
                <span style="color:#663399; font-weight:bold;">(anwesend?</span>
                <asp:ImageButton ID="detailsPresentImageButton" runat="server" ToolTip="click to mark as absent" CommandName="uncheck" CommandArgument='<%# CStr(Eval("participantsid"))%>' ImageUrl="~/images/present.gif" Visible='<%# Eval("anwesend")%>' ImageAlign="baseline" OnClick="detailsPresentImageButton_Click" ></asp:ImageButton>
                <asp:ImageButton ID="detailsAbsentImageButton" runat="server" ToolTip="click to mark as present" CommandName="check" CommandArgument='<%# CStr(Eval("participantsid"))%>' ImageUrl="~/images/absent.gif" Visible='<%# Not cbool(Eval("anwesend"))%>' OnClick="detailsAbsentImageButton_Click"></asp:ImageButton>
                <span style="color:#663399; font-weight:bold;">)</span>
                </td>
                <td style="text-align:right;">
                    <asp:Label ID="Label30" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red" Text='<%# Eval("warning").ToString %>'></asp:Label>
                </td>
            </tr>
<%--
                    <tr>
                        <td>
                            PIN:</td>
                        <td colspan="2">
                <asp:Label ID="memberpinLabel" runat="server" Text='<%# Eval("memberpin") %>'></asp:Label></td>
                    </tr>
--%>

<tr>
<td>
ID:
</td>
<td colspan="2">
<asp:Label ID="participantsidLabel" runat="server" Text='<%# Eval("participantsid") %>'></asp:Label>
</td>
</tr>

<tr>
<td>
Erstellt:</td>
<td colspan="2" style="height: 18px">
<asp:Label ID="createdLabel" runat="server" Text='<%# Eval("created") %>'></asp:Label>
</td>
</tr>

<tr>
<td>
Eingecheckt:</td>
<td colspan="2" style="height: 18px">
<asp:Label ID="checkindateLabel" runat="server" Text='<%# Eval("checkindate", "{0:dd.MM.yyyy HH:mm}") %>'></asp:Label>
</td>
</tr>


                    <tr>
                        <td>
                            Surname</td>
                        <td colspan="2">
                <asp:TextBox ID="surnameTextBox" runat="server" Text='<%# Bind("surname") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Firstname</td>
                        <td colspan="2">
                <asp:TextBox ID="firstnameTextBox" runat="server" Text='<%# Bind("firstname") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Titel</td>
                        <td colspan="2">
                        <asp:DropDownList ID="genderDropDownList" runat="server" SelectedValue='<%# Bind("gender") %>'>
                            <asp:ListItem>&nbsp;</asp:ListItem>
                            <asp:ListItem>Fr.</asp:ListItem>
                            <asp:ListItem>Hr.</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:DropDownList ID="titelDropDownList" runat="server" SelectedValue='<%# Bind("titel") %>'>
                            <asp:ListItem></asp:ListItem>                            
                            <asp:ListItem>Dr.</asp:ListItem>
                            <asp:ListItem>Prof.</asp:ListItem>
                            <asp:ListItem>Prof. Dr.</asp:ListItem>
                        </asp:DropDownList>                
                </td>
                    </tr>
                    <tr>
                        <td>
                            Institut</td>
                        <td colspan="2">
                <asp:TextBox ID="institutTextBox" runat="server" Text='<%# Bind("institut") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Department</td>
                        <td colspan="2">
                <asp:TextBox ID="departmentTextBox" runat="server" Text='<%# Bind("department") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Street</td>
                        <td colspan="2">
                <asp:TextBox ID="streetTextBox" runat="server" Text='<%# Bind("street") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Postal code</td>
                        <td colspan="2">
                <asp:TextBox ID="postalcodeTextBox" runat="server" Text='<%# Bind("postalcode") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            City</td>
                        <td colspan="2">
                <asp:TextBox ID="cityTextBox" runat="server" Text='<%# Bind("city") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Country</td>
                        <td colspan="2">
                <asp:TextBox ID="countryTextBox" runat="server" Text='<%# Bind("country") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Phone</td>
                        <td colspan="2">
                <asp:TextBox ID="phoneTextBox" runat="server" Text='<%# Bind("phone") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            E-Mail</td>
                        <td colspan="2">
                <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Eintrag in Teilnehmerliste</td>
                        <td  colspan="2">
                            <asp:CheckBox ID="listeeintragCheckBox" runat="server" Checked='<%# Bind("listeeintrag") %>' /></td>
                    </tr>
                    <tr>
                        <td>
                            Teilnahmebestätigung</td>
                        <td  colspan="2">
                            <asp:CheckBox ID="teilnahmebestatigungCheckBox" runat="server" Checked='<%# Bind("teilnahmebestatigung") %>' /></td>
                    </tr>
                    <tr>
                        <td>
                            Arrival<br /><br /></td>
                        <td><cpfs:VDBDate ID="arrivalVDBDate" runat="server" UseForSearch="False" ContentRequired="true" Validated_text='<%# Bind("arrival") %>' NeedValidation="false" /></td>
                        <td>Departure <cpfs:VDBDate ID="departureVDBDate" runat="server" UseForSearch="False" ContentRequired="true" Validated_text='<%# Bind("departure") %>' NeedValidation="false" /></td>
                    </tr>
<tr>
<td style="vertical-align:top">
Vortragstitel
</td>
<td colspan="2">
    <asp:Label ID="bemerkungenparticipantLabel" runat="server" Text='<%# GlobFunctions.TextConvertTexToHtml(Convert.ToString(Eval("bemerkungenparticipant"))) %>'></asp:Label>
</td>
</tr>
<tr>
<td>
</td>
<td colspan="2">
<asp:TextBox ID="bemerkungenparticipantTextBox" runat="server" Height="80px" Text='<%# Bind("bemerkungenparticipant") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
</td>
</tr>
<tr>
<td>
geplannte Dauer
</td>
<td colspan="2">
<asp:TextBox ID="participantdauerTextBox" runat="server" Width="2em" Text='<%# Bind("participantdauer") %>' MaxLength="2"></asp:TextBox> Min.
<asp:CustomValidator ID="participantdauerValidator" runat="server" ControlToValidate="participantdauerTextBox" OnServerValidate="validateParticipantdauer" ErrorMessage="Bitte als Vortragsdauer eine ganze, positive Zahl eintragen." EnableClientScript="false"></asp:CustomValidator>
</td>
</tr>
                    <tr>
                        <td>
                            geplanter Termin</td>
                        <td colspan="2">
<asp:DropDownList ID="participantterminDropDownList" runat="server" SelectedValue='<%# Bind("participanttermin") %>'>
    <asp:ListItem value=""></asp:ListItem>
    <asp:ListItem value="10.03.2011 Nachmittag">10.03.2011 Nachmittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Vormittag">11.03.2011 Vormittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Nachmittag">11.03.2011 Nachmittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Vormittag">12.03.2011 Vormittag</asp:ListItem>
</asp:DropDownList>                
                        <td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="TextBox4" runat="server" Height="152px" Text='<%# Bind("bemerkungen") %>'
                                TextMode="MultiLine" Width="100%"></asp:TextBox></td>
</tr>

<tr>
<td>
Warnung</td>
<td colspan="2">
<asp:TextBox ID="warnungTextBox" runat="server" Text='<%# Bind("warning") %>' Width="100%"></asp:TextBox></td>
</tr>

                </table>
                <br />
                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                
                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>

                <asp:Button ID="UpdateUnlockButton" runat="server" CausesValidation="False" CommandName="Unlock" Text="Unlock" Visible="false" EnableViewState="false"  BorderColor="Red"/>
                
            </EditItemTemplate>
       
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        </asp:FormView>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="ListePanel" runat="server">
        <br />
        <asp:SqlDataSource ID="ListeSqlDataSource" runat="server"  
                           ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
                           SelectCommand="SELECT [participantsid],[workshopsid],[memberpin],[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[fax],[email],[vegetarier],[registrationdate],[arrival],[departure],[paymenttype],[poster],[posterexists],[postertitle],[posterauthors],[status],[external],[roomneighbour],[fee],[anwesend],[feepayed],[shulden],[needsroom],CASE WHEN [roomid] IS NULL THEN 0 ELSE [roomid] END AS 'roomid',[bemerkungenparticipant], CASE WHEN DATALENGTH(bemerkungenparticipant) > 0 THEN 1 ELSE 0 END AS 'hasvortrag', [listeeintrag],[teilnahmebestatigung],[participanttermin],[warning],[checkindate]  FROM [dbo].[temp_participantsstatistik]"
                           EnableViewState="false">
                           
        </asp:SqlDataSource>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" />        
        <asp:GridView ID="ListeGridView" runat="server" AutoGenerateColumns="False" 
            CellPadding="3" DataKeyNames="participantsid" DataSourceID="ListeSqlDataSource" 
            GridLines="Vertical" AllowSorting="True" AllowPaging="True" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" PageSize="30" 
            SelectedIndex="200" ShowFooter="True" EnableModelValidation="True">
            <FooterStyle BackColor="#CCCCCC" ForeColor="#000084" Font-Bold="True" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Columns>
                <asp:CommandField ShowEditButton="True" Visible="False" ButtonType="Button" />
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Label ID="RowNumberLabel" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
<%-- 
                <asp:BoundField DataField="memberpin" HeaderText="PIN" ReadOnly="True" >
                    <HeaderStyle Wrap="False" ForeColor="White" />
                </asp:BoundField>
--%>
<%-- 
                 <asp:HyperLinkField DataNavigateUrlFields="participantsid" DataNavigateUrlFormatString="registration/qcnp09_members.aspx?memberid={0}"
                    DataTextField="participantsid" HeaderText="ID" SortExpression="participantsid" Target="_blank" >
                    <HeaderStyle Wrap="False" />
                </asp:HyperLinkField>
--%>
                <asp:TemplateField HeaderText="ID" SortExpression="participantsid">
                    <ItemTemplate>
                        <cpfs:PigmentLabel  runat="server" BackColor='<%# GetColorForWarning(Eval("warning")) %>' Visible="false"></cpfs:PigmentLabel>
                        <asp:Label ID="participantsidLabel" runat="server" Text='<%# Eval("participantsid") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="anzGesamtLabel" runat="server" Text='<%# Me._footerStatistics.AnzahlGesamt %>'></asp:Label>
                    </FooterTemplate>
                    <HeaderStyle ForeColor="White" Wrap="False" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="A" SortExpression="anwesend">
                    <ItemTemplate>
                        <asp:ImageButton ID="presentImageButton" runat="server" CommandName="uncheck" CommandArgument='<%# CStr(Eval("participantsid"))%>' ImageUrl="~/images/present.gif" Visible='<%# Eval("anwesend")%>'></asp:ImageButton>
                        <asp:ImageButton ID="absentImageButton" runat="server" CommandName="check" CommandArgument='<%# CStr(Eval("participantsid"))%>' ImageUrl="~/images/absent.gif" Visible='<%# Not cbool(Eval("anwesend"))%>'></asp:ImageButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="anzAnwesendLabel" runat="server" Text='<%# Me._footerStatistics.AnzahlAnwesend %>'></asp:Label>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Eingecheckt" SortExpression="checkindate">
                    <ItemTemplate>
                        <asp:Label ID="checkindateLabel" runat="server" 
                            Text='<%# Bind("checkindate", "{0:ddd. HH:mm}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Font-Names="Courier" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" SortExpression="surname">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("participantsid", "~/report/default.aspx?memberid={0}") %>'
                            Text='<%# Eval("surname") + " " + Eval("firstname") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="gender" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="postalcode" HeaderText="PLZ" SortExpression="postalcode" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="city" HeaderText="Ort" SortExpression="city" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="institut" HeaderText="Institut" SortExpression="institut" ReadOnly="True" >
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="registrationdate" DataFormatString="{0:dd.MM.yyyy}" HeaderText="registered"
                    HtmlEncode="False" SortExpression="registrationdate" ReadOnly="True" Visible="False">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Vortrag" SortExpression="hasvortrag">
                    <ItemTemplate >
                        <cpfs:PigmentLabel runat="server" BackColor='<%# GetColorForWunschtermin(Eval("participanttermin")) %>' Visible="false"></cpfs:PigmentLabel>
                        <asp:Label ID="wunschterminLabel" runat="server" Text="+" Visible='<%# Not cbool(String.IsNullOrEmpty(Convert.ToString(Eval("bemerkungenparticipant")))) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="anzVortragLabel" runat="server" Text='<%# Me._footerStatistics.AnzahlVortrag %>'></asp:Label>
                    </FooterTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wunschtermin" SortExpression="hasvortrag DESC, participanttermin" >
                    <ItemTemplate>
                        <asp:Label ID="Labelparticipanttermin" runat="server" Text='<%# Eval("participanttermin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teilnehmerliste" SortExpression="listeeintrag">
                    <ItemTemplate>
                        <asp:Label ID="Labellisteeintrag" runat="server" Text="+" Visible='<%# cbool(Eval("listeeintrag")) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="anzListeeintragLabel" runat="server" Text='<%# Me._footerStatistics.AnzahlTeilnehmerliste %>'></asp:Label>
                    </FooterTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teilnahmebest." SortExpression="teilnahmebestatigung">
                    <ItemTemplate>
                        <asp:Label ID="Labelteilnahmebestatigung" runat="server" Text="+" Visible='<%# cbool(Eval("teilnahmebestatigung")) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="anzBestaetigungLabel" runat="server" Text='<%# Me._footerStatistics.AnzahlTeilnehmerbestaetigung %>'></asp:Label>
                    </FooterTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="White" ForeColor="#999999" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="gridViewSelected" BackColor="#008A8C" Font-Bold="False" ForeColor="White" />
            <EditRowStyle CssClass="gridViewSelected" BackColor="#008A8C" Font-Bold="False" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" />
            <AlternatingRowStyle BackColor="Gainsboro" />
            <PagerSettings Mode="NumericFirstLast" />
        </asp:GridView>
        <br />
        <asp:HyperLink ID="newParticipantHyperLink" runat="server" NavigateUrl="~/report/newparticipant.aspx">Register New Participant</asp:HyperLink><br />
        <br />
<%-- 
        <asp:HyperLink ID="csvHyperLink" runat="server" Target="_blank"><b>ToDo:</b>Participants list (for printing)</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="roomsHyperLink" runat="server" NavigateUrl="~/report/rooms.aspx">Zimmer und Gruppen</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/report/daystatistics.aspx">Teilnehmer pro Tag Statistik</asp:HyperLink><br />
        <br />
--%>
    </asp:Panel>
</asp:Content>