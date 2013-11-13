<%@ Page Title="Contact" Language="VB" AutoEventWireup="false" CodeFile="088.aspx.vb" Inherits="_088" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="sixcol editorcontent" id="spaltemitte">
                  <div class="content">
                     <h1>
                        Contact
                     </h1><!-- Indexüberschrift:  -->
                     <div class="label">
                        <h3>
                           Coordinator
                        </h3>
                     </div>                     <div style="font-weight: bold" class="boxaktuell">
                        Prof. Dr. Claudia Felser
                     </div>
                     <div class="textboxaktuell" style="min-height:100px;">
                        <div style="float: left; padding-right: 10px">
                           <img src="Illustrationen/felser_201_rdax_146x100.png" alt="Illustration Prof. Dr. Claudia Felser" />
                        </div>
                        <div >
                           Max-Planck-Institute for Chemical Physics of Solids
                           <br />
                           Nothnitzer Strasse 40
                           <br />
                           D-01187 Dresden
                           <br />
                           Phone +49 351 4646-3004
                           <br />
                           Fax +49 351 4646-3001
                           <br />
                           <%= GlobFunctions.MakeProtectedEmailLink("Claudia.Felser@cpfs.mpg.de")%>
                           <br />
                           <a target="_blank" href="http://www.superconductivity.de/">Website</a>
                        </div>
                        <div style="clear: left"></div>
                     </div>
                     <br />
                                          <div style="font-weight: bold" class="boxaktuell">
                        Prof. Dr. Burkard Hillebrands
                     </div>
                     <div class="textboxaktuell" style="min-height:108px;">
                        <div style="float: left; padding-right: 10px">
                           <img src="Illustrationen/hillebrands_kl.jpg" alt="Illustration Prof. Dr. Burkard Hillebrands" />
                        </div>
                        <div >
                           University of Kaiserslautern
                           <br />
                           Dep. of Physics
                           <br />
                           Erwin Schrödinger Str. 56
                           <br />
                           D-67663 Kaiserslautern
                           <br />
                           Phone +49 631 205-4228
                           <br />
                           Fax +49 631 205-4096 
                           <br />
						   <%= GlobFunctions.MakeProtectedEmailLink("hilleb@physik.uni-kl.de")%>
                           <br />
                           <a target="_blank" href="http://www.physik.uni-kl.de/hillebrands/home/">Website  </a>
                        </div>
                        <div style="clear: left"></div>
                     </div>
                     <br />
                     <br />
                  </div>
               </div>               
</asp:Content>