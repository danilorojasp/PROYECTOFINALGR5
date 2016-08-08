<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="RegistroPaciente.aspx.cs" Inherits="RegistroPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .auto-style1 {
            font-size: large;
        }
        .auto-style7 {
        color: #000000;
    }
        .auto-style11 {
        width: 472px;
    }
    .auto-style13 {
        width: 185px;
    }
    .auto-style14 {
        width: 609px;
    }
    .auto-style16 {
        width: 89%;
            margin-right: 0px;
        }
        .auto-style17 {
            width: 776px;
        }
        .auto-style18 {
            width: 208px;
        }
        .auto-style19 {
            width: 189px;
        }
        .auto-style20 {
            width: 100%;
            height: 130px;
        }
        .auto-style21 {
            margin-left: 0px;
        }
        .auto-style22 {
            color: #FF3300;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <table class="auto-style16">
        <tr>
            <td class="auto-style18"><span class="auto-style7">Cedula del Paciente:</span></td>
            <td class="auto-style17"><span class="auto-style7">

    

        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" Width="112px"></asp:TextBox>

                </span>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar Paciente" Width="125px" CssClass="auto-style11" />
            </td>
            <td class="auto-style14">
                &nbsp;</td>
        </tr>
        </table>
        <strong><span class="auto-style22">*NOTA IMPORTANTE:</span> </strong><span class="auto-style7">Solo pacientes registrados en el sistema de atencion del IESS podran acceder al sistema.</span><br />
    <br />

    <div id="div1" runat="server">


    <table class="auto-style20">
        <tr>
            <td class="auto-style13">Nombre:</td>
            <td class="auto-style19">
                <asp:TextBox ID="TextNombre" runat="server" Width="198px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
         <tr>
            <td class="auto-style13">Telefono:</td>
            <td class="auto-style19">
                <asp:TextBox ID="TextTelefono" runat="server" CssClass="auto-style21" Width="196px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
         <tr>
            <td class="auto-style13">Correo Electronico:</td>
            <td class="auto-style19">
                <asp:TextBox ID="TextCorreo" runat="server" Width="196px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style13"><span class="auto-style7">

        <asp:Button ID="Button1" runat="server" Text="Registrar Paciente" OnClick="Button1_Click" Height="31px" Width="188px" />
                </span></td>
            <td class="auto-style19">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
     
    </table>
        </div>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
        <span class="auto-style7"><br />
    <br />
&nbsp;<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:daypilot %>" SelectCommand="SELECT [IdPaciente], [NombrePaciente], [CedulaPaciente], [Telefono], [e-mail] AS column1 FROM [Paciente] WHERE ([CedulaPaciente] = @CedulaPaciente)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBox1" Name="CedulaPaciente" PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                <br />
    <br />
    </span>


</asp:Content>

