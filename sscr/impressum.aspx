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
                        Staudinger Weg 9
                        <br />
                        Room 01-230
                        <br />
                        D-55128 Mainz
                        <br />
                        Phone Office  +49 (0)6131 39-22184
                        <br />
                        Fax               +49 (0)6131 39 26267
                        <br />
                        E-Mail felser-office(at)uni-mainz.de
                     </p>
                     <p>
                         
                     </p>
                     <p>
                        Technical questions and suggestions concerning operating this Web site should be directed to <%= GlobFunctions.MakeProtectedEmailLink(MyAppSettings.WebMasterEmail)%>.
                     </p>
                     <p>
                        Es gelten die allgemeinen Regelungen zu den Internetseiten der Johannes Gutenberg-Universität Mainz 
                     </p>
                     <p>
                        <a href="http://www.uni-mainz.de/zentral/impressum.php">http://www.uni-mainz.de/zentral/impressum.php</a>
                     </p>
                     <p>
                         
                     </p>
                  </div>
               </div>               
</asp:Content>