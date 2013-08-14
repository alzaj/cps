<%@ Page Language="VB" AutoEventWireup="false" CodeFile="newparticipant.aspx.vb" Inherits="newparticipant" %>
<%@ Register TagPrefix="cpfs" Namespace="CPFS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Register New Participant</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Register New Participant</h2>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label ID="message" ForeColor="red" runat="server" Text=""></asp:Label>
        <asp:SqlDataSource ID="Insert_MemberSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConfsConnectionString %>" 
                            SelectCommand="SELECT [participantsid], 'created' = convert(varchar,registrationdate,104) ,[workshopsid],[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[fax],[email],[vegetarier],[registrationdate],[arrival],[departure],[paymenttype],[poster],[posterexists],[postertitle],[posterauthors],[status],[external],[roomneighbour],[fee],[anwesend],[feepayed],[bemerkungen],[needsroom],[roomid],[warning] FROM [dbo].[temp_participants]"
                            ConflictDetection="CompareAllValues" 
                            OldValuesParameterFormatString="original_{0}" 
                            InsertCommand="Insert INTO [dbo].[temp_participants]  ([workshopsid],[anwesend],[checkindate],[registrationdate],[firstname],[surname],[gender],[titel],[institut],[department],[street],[postalcode],[city],[country],[phone],[email],[listeeintrag],[teilnahmebestatigung],[arrival],[departure],[bemerkungen],[bemerkungenparticipant],[participanttermin],[participantdauer],[vegetarier],[warning]) VALUES (2,1,GETDATE(),GETDATE(),@firstname,@surname,@gender,@titel,@institut,@department,@street,@postalcode,@city,@country,@phone,@email,@listeeintrag,@teilnahmebestatigung,@arrival,@departure,@bemerkungen,@bemerkungenparticipant,@participanttermin,@participantdauer,0,@warning) SELECT @newID = SCOPE_IDENTITY()">
                            
            <InsertParameters>
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
                <asp:Parameter Name="checkindate" Type="DateTime" />
                <asp:Parameter Name="arrival" Type="DateTime" />
                <asp:Parameter Name="departure" Type="DateTime" />
                <asp:Parameter Name="bemerkungenparticipant" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="participantdauer" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="participanttermin" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="listeeintrag" Type="Boolean" />
                <asp:Parameter Name="teilnahmebestatigung" Type="Boolean" />
                <asp:Parameter Name="bemerkungen" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="warning" Type="String" ConvertEmptyStringToNull="false" />
                <asp:Parameter Name="newID" Type="Int32" Direction="output" />

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
                <asp:Parameter Name="original_checkindate" Type="DateTime" />
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
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:FormView ID="Insert_MemberFormView" runat="server" CellPadding="4" DataKeyNames="participantsid" DataSourceID="Insert_MemberSqlDataSource" DefaultMode="Insert" ForeColor="#333333">
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
<InsertItemTemplate>

                <table style="color:#663399; font-weight:bold;">

<tr>
<td style="vertical-align:top;">
Surname</td>
<td colspan="2">
<asp:TextBox ID="surnameTextBox" runat="server" Text='<%# Bind("surname") %>' Width="100%"></asp:TextBox>
<asp:RequiredFieldValidator ID="surnameRequiredFieldValidator" runat="server" ErrorMessage="content required" ControlToValidate="surnameTextBox"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td style="vertical-align:top;">
Firstname</td>
<td colspan="2">
<asp:TextBox ID="firstnameTextBox" runat="server" Text='<%# Bind("firstname") %>' Width="100%"></asp:TextBox>
<asp:RequiredFieldValidator ID="firstnameRequiredFieldValidator" runat="server" ErrorMessage="content required" ControlToValidate="firstnameTextBox"></asp:RequiredFieldValidator>
</td>
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
                        <td><cpfs:VDBDate ID="arrivalVDBDate" runat="server" UseForSearch="False" ContentRequired="true" DefaultToday="true" Validated_text='<%# Bind("arrival") %>' NeedValidation="false" /></td>
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
<asp:TextBox ID="TextBox4" runat="server" Height="152px" Text='<%# Bind("bemerkungen") %>' TextMode="MultiLine" Width="100%"></asp:TextBox></td>
</tr>

<tr>
<td>
Warnung</td>
<td colspan="2">
<asp:TextBox ID="warnungTextBox" runat="server" Text='<%# Bind("warning") %>' Width="100%"></asp:TextBox></td>
</tr>

                </table>
                <br />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                
                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>
                
                        
</InsertItemTemplate>
       
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        </asp:FormView>
    </div>
    </form>
</body>
</html>
