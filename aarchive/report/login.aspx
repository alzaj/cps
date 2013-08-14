<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HaeKo 2011 - Anmeldung</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <h1>Bitte melden Sie sich an</h1>
                  <table>
                    <tr>
                        <td>
                            Benutzername: 
                        </td>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" Width="10em"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Kennwort: 
                        </td>
                        <td>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="10em"></asp:TextBox>
                        </td>
                    </tr>
                  </table>

                  <%-- <p><asp:CheckBox ID="RememberMe" runat="server" Text="Remember Me" />
                  </p>--%>
                  <p><asp:Button ID="LoginButton" runat="server" Text="Anmelden" />
                  </p>
                  <p>
                    <asp:Label ID="InvalidLoginMessage" runat="server" ForeColor="Red" Text="Benutzername oder Kennwort ist ungültig. Versuchen Sie bitte noch ein Mal." Visible="False"></asp:Label>
                  </p> 
    </div>
    </form>
</body>
</html>
