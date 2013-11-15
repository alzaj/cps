<%@ Page Language="VB" Title="Imprint" AutoEventWireup="false" CodeFile="impressum.aspx.vb" Inherits="content_impressum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="sixcol editorcontent" id="spaltemitte">
                  <div class="content">
                     <h1>
                        Impressum
                     </h1>
                     Editor: <%= GlobFunctions.MakeProtectedEmailLink(MyAppSettings.WebMasterEmail)%>

                  </div>
               </div>               
</asp:Content>
