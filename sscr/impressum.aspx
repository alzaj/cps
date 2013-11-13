<%@ Page Title="Imprint" Language="VB" AutoEventWireup="false" CodeFile="impressum.aspx.vb" Inherits="_impressum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="sixcol editorcontent" id="spaltemitte">
                  <div class="content">
                     <h1>
                        Imprint
                     </h1><!-- Indexüberschrift:  -->
                      
                     <h5>
                        Editor
                     </h5>
                     <h6>
                        ASPIMATT
                     </h6>
                     <p>
                        Prof. Dr. Claudia Felser
                           <br />
                           Nothnitzer Strasse 40
                           <br />
                           D-01187 Dresden
                           <br />
                           Phone: +49 351 4646-3004
                           <br />
                           Fax: +49 351 4646-3001
                           <br />
                           E-Mail: <%= GlobFunctions.MakeProtectedEmailLink("Claudia.Felser@cpfs.mpg.de")%>
                     </p>
                     <p>
                         
                     </p>
                     <p>
                        Technical questions and suggestions concerning operating this Web site should be directed to <%= GlobFunctions.MakeProtectedEmailLink(MyAppSettings.WebMasterEmail)%>.
                     </p>
                     <p>
                        Es gelten die allgemeinen Regelungen zu den Internetseiten des Max-Planck-Instituts für Chemische Physik fester Stoffe 
                     </p>
                     <p>
                        <a href="http://www.cpfs.mpg.de/web/impressum/">http://www.cpfs.mpg.de/web/impressum/</a>
                     </p>
                     <p>
                         
                     </p>
                  </div>
               </div>               
</asp:Content>