<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            margin-right: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style2">
    
        <br />
        <strong><span class="auto-style1">
        <img alt="" class="auto-style3" height="90" src="https://www.iess.gob.ec/afi-fondos-web/iess-skin-resources?file=/img/plantilla/logo.png" style="margin-top: 15px;" width="182" /><br />
        <br />
        </span></strong>
        <br />
        <asp:Button ID="Button1" runat="server" Height="57px" OnClick="Button1_Click" Text="Ingreso al SISTEMA DE TURNOS" Width="209px" />
    
    </div>
    </form>
</body>
</html>
