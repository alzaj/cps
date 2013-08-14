<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="registr_draft_default" Title="HaeKo 2011 - Anmeldung"%>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Anmeldung</h2>
<b>Anmeldeschluss: <%= qcnp09.D_DEADLINE_REGISTRATION.ToString("%d. MMMM yyyy", qcnp09.D_DATUM_FORMAT)%></b>
<br /><br />
<asp:Wizard ID="RegistrationWizard" runat="server" DisplaySideBar="False" OnNextButtonClick="CheckNextStep" NavigationStyle-HorizontalAlign="Left">
<WizardSteps>
<asp:WizardStep ID="MainDataStep" runat="server" Title="Main Data">
<asp:Label ID="MainDataStepMsg" runat="server" EnableViewState="false"></asp:Label>
<table class="toptable">
<tr>
<td style="white-space:nowrap;text-align:right;">
Titel: </td>
<td>  </td>
<td>
<asp:DropDownList DataTextField="" ID="gendertextbox" runat="server">
<asp:ListItem></asp:ListItem>
<asp:ListItem>Fr.</asp:ListItem>
<asp:ListItem>Hr.</asp:ListItem>
</asp:DropDownList>&nbsp;
<asp:DropDownList DataTextField="" ID="titleDropDown" runat="server">
<asp:ListItem></asp:ListItem>                            
                            <asp:ListItem>Dr.</asp:ListItem>
                            <asp:ListItem>Prof.</asp:ListItem>
                            <asp:ListItem>Prof. Dr.</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                </tr>
                <tr>
                    <td style="text-align:right;white-space:nowrap;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="! " ControlToValidate="surnameTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
                        Nachname: 
                    </td>
                    <td> * </td>
                    <td style="white-space:nowrap;">
                        <asp:TextBox ID="surnameTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Vorname: 
                    </td>
                    <td>  </td>
                    <td>
                        <asp:TextBox ID="firstnameTextBox" runat="server" Wrap="true" Width="395px" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td style="white-space:nowrap;text-align:right;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="! " ControlToValidate="organizationtextbox" EnableClientScript="False"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label39" runat="server" Text="Organization:" CssClass="Label"></asp:Label>
                    </td>
                    <td> * </td>
                    <td style="white-space:nowrap;">
                        <asp:TextBox ID="organizationtextbox" runat="server" Width="395px" MaxLength="500" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Institution: 
                    </td>
                    <td>  </td>
                    <td style="white-space:nowrap;">
                        <asp:TextBox ID="institutTextBox" runat="server" Width="395px" MaxLength="500" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="! " ControlToValidate="departmenttextbox" EnableClientScript="False"></asp:RequiredFieldValidator>--%>
                        Abteilung<br />/Institut: 
                    </td>
                    <td></td>
                    <td style="white-space:nowrap;">
                        <asp:TextBox ID="departmenttextbox" runat="server" Width="395px" MaxLength="500" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Straße, Hausnr.<br />/Postfach: </td>
                    <td style="">  </td>
                    <td style="">
                        <asp:TextBox ID="streetTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Postleitzahl: </td>
                    <td>  </td>
                    <td>
                        <asp:TextBox ID="postalcodeTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Ort: </td>
                    <td>  </td>
                    <td>
                        <asp:TextBox ID="cityTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Land: </td>
                    <td> </td>
                    <td>
                        <asp:TextBox ID="countryTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        <asp:CustomValidator ID="emailCustomValidator" runat="server" ErrorMessage="! " OnServerValidate="validateEmail" EnableClientScript="False"></asp:CustomValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="! " ControlToValidate="emailTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>                    
                        E-Mail: </td>
                    <td> * </td>
                    <td>
                        <asp:TextBox ID="emailTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Telefon: </td>
                    <td>  </td>
                    <td>
                        <asp:TextBox ID="phoneTextBox" runat="server" Width="395px" MaxLength="500"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Eintrag in<br />Teilnehmerliste: </td>
                    <td>  </td>
                    <td>
                        <asp:CheckBox ID="listeeintragCheckBox" runat="server" Text="" Checked="true" />
                    </td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        Teilnahmebestätigung<br />erwünscht: </td>
                    <td>  </td>
                    <td>
                        <asp:CheckBox ID="teilnahmebestatigungCheckBox" runat="server" Text="" Checked="true" />
                    </td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        <asp:CustomValidator ID="arrivalCustomValidator" runat="server" ErrorMessage="! " OnServerValidate="validateArrivalDeparture"  EnableClientScript="False"></asp:CustomValidator>
                        <%--<asp:RequiredFieldValidator ID="ArrivalValidator" runat="server" ErrorMessage="! " ControlToValidate="arrivalDropDown" EnableClientScript="False"></asp:RequiredFieldValidator>--%>
                        Ankunft: </td>
                    <td>  </td>
                    <td>
                        <asp:DropDownList DataTextField="" ID="arrivalDropDown" runat="server">
                            <asp:ListItem value=""></asp:ListItem>
                            <asp:ListItem value="8.03.2011">8 März</asp:ListItem>
                            <asp:ListItem value="9.03.2011">9 März</asp:ListItem>
                            <asp:ListItem value="10.03.2011">10 März</asp:ListItem>
                            <asp:ListItem value="11.03.2011">11 März</asp:ListItem>
                            <asp:ListItem value="12.03.2011">12 März</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                </tr>
                <tr>
                    <td style="white-space:nowrap;text-align:right;">
                        <asp:CustomValidator ID="departureCustomValidator" runat="server" ErrorMessage="! " OnServerValidate="validateArrivalDeparture" EnableClientScript="False"></asp:CustomValidator>
                        <%-- <asp:RequiredFieldValidator ID="departureValidator" runat="server" ErrorMessage="! " ControlToValidate="departureDropDown" EnableClientScript="False"></asp:RequiredFieldValidator>--%>
                        Abreise: </td>
                    <td>  </td>
                    <td>
                        <asp:DropDownList DataTextField="" ID="departureDropDown" runat="server">
                            <asp:ListItem value=""></asp:ListItem>
                            <asp:ListItem value="10.03.2011">10 März</asp:ListItem>
                            <asp:ListItem value="11.03.2011">11 März</asp:ListItem>
                            <asp:ListItem value="12.03.2011">12 März</asp:ListItem>
                            <asp:ListItem value="13.03.2011">13 März</asp:ListItem>
                            <asp:ListItem value="14.03.2011">14 März</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                </tr>
<tr>
<td style="white-space:nowrap;text-align:right;">
Vortragstitel: 
</td>
<td>  </td>
<td>
<asp:TextBox ID="bemerkungenparticipantTextBox" runat="server" Height="50px" TextMode="MultiLine" Width="395px"></asp:TextBox>
</td> 
</tr>
<tr>
<td style="vertical-align:top;text-align:right;">
<br />
</td>
<td> </td>
<td>
Um eine Zeichenfolge tiefzusetzen verwenden Sie bitte Unterstrich und geschweifte Klammern "_{ }",<br />
um eine Zeichenfolge hochzustellen verwenden Sie bitte Zirkumflex und geschweifte Klammern "^{ }",<br /> 
jeweils ohne Anführungszeichen,<br />
z.B. H<sub>2</sub>O = H_{2}O
<br />
<br />
</td> 
</tr>
<tr>
<td style="white-space:nowrap;text-align:right;">
<asp:CustomValidator ID="participantdauerCustomValidator" runat="server" ErrorMessage="! " OnServerValidate="validateParticipantdauer" EnableClientScript="False"></asp:CustomValidator>
Vortragsdauer: 
</td>
<td>  </td>
<td>
<asp:TextBox ID="participantdauerTextBox" runat="server" Width="2em" MaxLength="2"></asp:TextBox> Min.
</td> 
</tr>
<tr>
<td style="white-space:nowrap;text-align:right;">
bevorzugter<br />Vortragstermin: 
</td>
<td>  </td>
<td>
<asp:DropDownList DataTextField="" ID="participantterminDropDown" runat="server">
    <asp:ListItem value=""></asp:ListItem>
    <asp:ListItem value="10.03.2011 Nachmittag">10.03.2011 Nachmittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Vormittag">11.03.2011 Vormittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Nachmittag">11.03.2011 Nachmittag</asp:ListItem>
    <asp:ListItem value="11.03.2011 Vormittag">12.03.2011 Vormittag</asp:ListItem>
</asp:DropDownList>
</td> 
</tr>
<tr>
<td colspan="3"><br /></td>
</tr>
<%--                 <tr>
                <td colspan="3">* <i>Please forward changes of your itinerary by e-mail to Mrs. Claudia Strohbach <a href='<%= "mailto:" + qcnp09.E_SUPPORT_EMAIL %>'><%= qcnp09.E_SUPPORT_EMAIL %></a></i></td>
                </tr>
--%>
                <tr>
                <td colspan="3"><br /></td>
                </tr>
            </table>
            
          </asp:WizardStep> 

<asp:WizardStep ID="PaymentStep" runat="server" Title="Payment Options">
<br />
<asp:Literal ID="PaymentStepMsg" runat="server"></asp:Literal>
Payment of the registration fee is requested by one of the following methods, without any charge to the beneficiary.<br />
<b>Please refer to "C-MAC/ Participant’s Name"</b>
<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" OnServerValidate="validatePayment" EnableClientScript="false"></asp:CustomValidator>
<br /><br />
<asp:RadioButton ID="Payment_transferRadioButton" runat="server" GroupName="PaymentMethod" Text="by bank transfer to the following account:" Checked="true" />
<br />
<b>Remittee:</b>&nbsp;<i>Max-Planck-Institut für Chemische Physik fester Stoffe Dresden</i><br />
<b>Name of the bank:</b>&nbsp;<i>Deutsche Bank</i><br />
<b>Account number:</b>&nbsp;<i>911 53 46 01</i><br />
<b>Bank identifier code:</b>&nbsp;<i>870 700 00</i><br />
<b>IBAN:</b>&nbsp;<i>DE61 8707 0000 0911 5346 01</i><br />
<b>SWIFT (BIC):</b>&nbsp;<i>DEUTDE8CXXX</i><br />
<br />

<asp:RadioButton ID="Payment_chequeRadioButton" runat="server" GroupName="PaymentMethod" Text="by cheque*. Cheques should be issued and made payable to:" /><br />
<i>Max-Planck-Institut für Chemische Physik fester Stoffe</i><br />
<i>Nöthnitzerstraße 40</i><br />
<i>D-01187 Dresden</i><br />
* cheques need to be sent to MPI CPfS before <b><%= qcnp09.D_DEADLINE_CHEQUE.ToString(qcnp09.D_DATUM_FORMAT_STRING, qcnp09.D_DATUM_FORMAT)%>.</b><br />
<br />
<asp:RadioButton ID="Payment_creditcardRadioButton" runat="server" GroupName="PaymentMethod" Text="<b>On Site</b> by credit card (Mastercard, Visacard and American Express will be accepted)" /><br />
<br />
<asp:RadioButton ID="Payment_casheRadioButton" runat="server" GroupName="PaymentMethod" Text="<b>On Site</b> in cash" /><br /><br />
(use <a href='<%= qcnp09.U_PRINT_METHODS_ABSOLUTE_URL  + "?" + Q_Names.Q_PrintView + "=ON" %>' target="_blank">the print view</a> to print the payment methods.)<br />
</asp:WizardStep>

<asp:WizardStep ID="SummaryStep" runat="server" Title="Summary">
<asp:Literal ID="SummaryStepMsg" runat="server" EnableViewState="false"></asp:Literal>
<br />
Zusammenfassung (<a href="<%= Me.ResolveURL(qcnp09.U_REGISTRATIONSUMMARY_FILE) %>" target="_blank">Zusammenfassung drucken</a>):<br />
<asp:Literal ID="SummaryLbl" runat="server" EnableViewState="false"></asp:Literal>
<br />
<div runat="server" id="AgreementPanel">
<table class="toptable">
<tr>
<td style="white-space:nowrap;">
<asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="! " OnServerValidate="validateAllowSaveData" EnableClientScript="False"></asp:CustomValidator>
* 
</td>
<td>
<asp:CheckBox ID="allowCheckBox" runat="server" />
</td>
<td>
Hiermit stimme ich einer Verwendung meiner oben aufgeführten persönlichen Daten für die Organisation des HaeKo 2011 zu.
<br />
</td>
 </tr>
 </table>
</div>
<br />
<cc1:captchacontrol id="CaptchaControl1" runat="server" CaptchaMaxTimeout="1800" Text="Geben Sie den angezeigten Code ein:" ></cc1:captchacontrol>&nbsp;
</asp:WizardStep>

<asp:WizardStep ID="Complete" runat="server" StepType="Complete" Title="Complete">
    <asp:Literal ID="completeLbl" runat="server"></asp:Literal>
</asp:WizardStep>

</WizardSteps>

        <StartNavigationTemplate>
            <br />
            <asp:Button ID="ClearButton" CausesValidation="false" runat="server" OnClick="ClearRegistrationForm" Text="Zurücksetzen" Width="89px" />
            <asp:Button ID="NextButton" CommandName="MoveNext" CausesValidation="true" runat="server" Text="Weiter" Width="89px" />
            <asp:Button ID="AbortButton" CausesValidation="false" CommandName="Cancel" runat="server" Text="Abbrechen" Width="89px" /><br />
        </StartNavigationTemplate>

        <StepNavigationTemplate>
            <br />
            <asp:Button ID="PreviousButton" CommandName="MovePrevious" CausesValidation="false" runat="server" Text="Zurück" Width="89px" />
            <asp:Button ID="NextButton" CommandName="MoveNext" runat="server" Text="Weiter" Width="89px" />
            <asp:Button ID="AbortButton" CausesValidation="false" CommandName="Cancel" runat="server" Text="Abbrechen" Width="89px" /><br />
        </StepNavigationTemplate>

        <FinishNavigationTemplate>
            <br />
            <asp:Button ID="PreviousButton" CommandName="MovePrevious" CausesValidation="false" runat="server" Text="Zurück" Width="89px" />
            <asp:Button ID="FinishButton" CommandName="MoveComplete" runat="server" Text="Absenden" Width="89px" />
            <asp:Button ID="AbortButton" CausesValidation="false" CommandName="Cancel" runat="server" Text="Abbrechen" Width="89px" /><br />
        </FinishNavigationTemplate>

</asp:Wizard>
</asp:Content>

