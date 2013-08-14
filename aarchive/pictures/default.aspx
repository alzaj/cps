<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="pictures_default" title="HaeKo 2011 - Pictures" %>
<%@ Register TagPrefix="cmac" Namespace="CPFS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Pictures</h2>
<%-- 
Please put all images to the subdirectory "images" of the directory where this page is stored
to refresch thumbnails please remove the subdirectory "thumbs" from the directory "images".
--%>

<cmac:CSSImageGallery ID="CSSImageGallery1" ImagesSubDir="images" runat="server" EnableViewState="false"></cmac:CSSImageGallery>
<br />
<a href='<%= Me.ResolveUrl(zipFilename) %>'>Download all images as a single ZIP file</a>
<asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>

</asp:Content>

