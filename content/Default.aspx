<%@ Page Title="ASPIMATT - Advanced spintronic materials and transport phenomena" Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="sixcol editorcontent" id="spaltemitte">
                  <div class="content">
        <div class="page-header">
          <h1>ASPIMATT - Advanced spintronic materials and transport phenomena</h1>
        </div>
					 


					 <div class="panel clearfix">
					 	<div class="panel-heading">03.11.2011:  EU-Förderung von €2,4 Mio. für Claudia Felser</div>
												
						<a href="http://www.uni-mainz.de/presse/49102.php" title="Bildlink" class="pull-left">
							<img src="/images/personal_09_felser_145x100.jpg" />
						</a>
						
                           <p>Mainzer Chemikerin erhält ERC Advanced Grant für den Ausbau der Materialforschung auf Basis von Heusler-Verbindungen
                           <a href="http://www.uni-mainz.de/presse/49102.php" title="Link">... <img src="Bilder_zentral/pfeil_news_blau.gif" /></a></p>
					 
					 </div>
					 

					 <div class="panel">
					 	<div class="panel-heading">22.08.2011 - 23.08.2011: Annual Meeting of ASPIMATT JST-DFG Research Unit</div>
												
							 <p>
                             Annual Meeting of ASPIMATT JST-DFG Research Unit, <br />
                             Villa Denis, Diemerstein/Kaiserslautern 							 
							 </p>
                             <p>with the ASPIMATT school on "Advanced Spintronic Materials and Transport Phenomena" August 24-27, 2011</p>
							 <p><a  target="_blank" href="pdfs/ASPIMATTMeetingSchool2011Program170811Hb.pdf">Program</a> 
							    <a href="pdfs/ASPIMATTMeetingSchool2011Program170811Hb.pdf" title="Link">... <img src="Bilder_zentral/pfeil_news_blau.gif" /></a></p>
					 </div>
					 
                     <% If Not SiteMap.CurrentNode Is Nothing AndAlso SiteMap.CurrentNode.Equals(SiteMap.RootNode) Then%>
                        This is rootnode
                     <% Else %>
                        This is NOT rootnode
                     <% End If%>

 
                     <br />
                                       </div>
               </div>               
</asp:Content>