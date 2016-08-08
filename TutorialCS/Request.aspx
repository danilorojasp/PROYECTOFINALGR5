<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>New event</title>
    <link href='media/modal.css' type="text/css" rel="stylesheet" /> 
    <script src="js/jquery-1.9.1.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            color: #FF3300;
            height: 20px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 20px;
        }
        .auto-style4 {
            margin-left: 0px;
        }
    </style>
</head>
<body class="dialog">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right"></td>
                <td>
                    <div class="header">Solicitud de Cita</div>
                </td>
            </tr>
            <tr>
                <td align="right">Start:</td>
                <td><asp:TextBox ID="TextBoxStart" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">End:</td>
                <td><asp:TextBox ID="TextBoxEnd" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            </tr>
             <tr>
                <td align="right">Doctor:</td>
                <td><asp:TextBox ID="TextBoxDoctor" runat="server" Width="200px" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="auto-style2">Cedula del Paciente:</td>
                <td class="auto-style2"><asp:TextBox ID="TextBoxName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
             <tr>
                <td align="right" class="auto-style3"></td>
                <td align="right" class="auto-style1"> 
                    <asp:TextBox ID="CajaValidacion" runat="server" CssClass="auto-style4" Enabled="False" OnTextChanged="TextBox1_TextChanged" Width="201px" ForeColor="#FF3300">*La cedula no esta registrada</asp:TextBox>
                 </td>
            </tr>
            <tr>
                <td align="right"></td>
                <td>
                    <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="  OK  " />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                </td>
            </tr>
        </table>
        
        </div>
    </form>
    <script>
        $(document).ready(function() {
            $("#TextBoxName").keyup(function() {
                var text = $(this).val();
                $("#CheckBoxScheduled").prop("checked", !!text);
            })
        });
    </script>
</body>
</html>
