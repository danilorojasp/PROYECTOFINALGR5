﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Doctor.aspx.cs" Inherits="Doctor" MasterPageFile="~/Site.master"  %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="js/daypilot-modal-2.1.js"></script>
	<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
<h1>Administrar el Calendario del Medico</h1>
        
<div class="space">
    Doctor: 
    <asp:DropDownList runat="server" ID="DropDownListDoctor" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDoctor_OnSelectedIndexChanged">
            <asp:ListItem Value="NONE">(no doctors specified)</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Height="24px" OnClick="Button1_Click" Text="Importar Calendario Semanal" Width="200px" />
</div>
    
<asp:Panel runat="server" ID="Schedule">
    <div style="float:left; width: 150px;">
        <DayPilot:DayPilotNavigator 
            runat="server" 
            ID="DayPilotNavigator1" 
            ClientIDMode="Static"
            BoundDayPilotID="DayPilotCalendar1"
            ShowMonths="3"    
            SelectMode="Week"
        
            OnBeforeCellRender="DayPilotNavigator1_OnBeforeCellRender"
            />  
    </div>
    
    <div style="margin-left: 150px;">
        <DayPilot:DayPilotCalendar 
            runat="server" 
            ID="DayPilotCalendar1"
            ClientObjectName="dp"
            ClientIDMode="Static"
            ViewType="Week"
        
            OnCommand="DayPilotCalendar1_OnCommand"
            TimeRangeSelectedHandling="CallBack"
            OnTimeRangeSelected="DayPilotCalendar1_OnTimeRangeSelected"
            OnBeforeEventRender="DayPilotCalendar1_OnBeforeEventRender"
            EventClickHandling="JavaScript"
            EventClickJavaScript="edit(e);"
            EventMoveHandling="CallBack"
            OnEventMove="DayPilotCalendar1_OnEventMove"
            EventResizeHandling="CallBack"
            OnEventResize="DayPilotCalendar1_OnEventResize"
            />
    </div>
</asp:Panel>
    
    
<script>
function edit(e) {
    new DayPilot.Modal({
        onClosed: function(args) {
            if (args.result == "OK") {
                dp.commandCallBack('refresh');
            }
        }
    }).showUrl("Edit.aspx?id=" + e.id());
}
</script>
</asp:Content>