<%@ Page Language="VB" AutoEventWireup="false" CodeFile="streetview.aspx.vb" Inherits="venue_streetview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MPI CPfS - Google Streetview</title>
</head>
  <body onload="initialize()" onunload="GUnload()">
    <div name="pano" id="pano" style="width: 700px; height: 700px"></div>
  </body>
</html>
