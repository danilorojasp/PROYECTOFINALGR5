﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manager.aspx.cs" Inherits="Manager" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div style="float:left; width: 150px;">
        <DayPilot:DayPilotNavigator 
            runat="server" 
            ID="DayPilotNavigator1" 
            ClientIDMode="Static"
            BoundDayPilotID="DayPilotScheduler1"
            ShowMonths="3"    
            SelectMode="Month"
            />  
    </div>
    
    <div style="margin-left: 150px;">
        
        <div class="space">ORDENAR POR: <a href="javascript:scale('shifts')">Por Turno</a> | <a href="javascript:scale('hours')">Por Horas</a></div> 

        <DayPilot:DayPilotScheduler 
            runat="server" 
            ID="DayPilotScheduler1"
            ClientObjectName="dp"
            
            AllowEventOverlap="false"
            UseEventBoxes="Never"
            
            CellWidth ="40"
            DynamicEventRendering="Disabled"
            
            TimeRangeSelectedHandling="CallBack"    
            OnTimeRangeSelected="DayPilotScheduler1_OnTimeRangeSelected"   
            
            EventDeleteHandling="CallBack"
            OnEventDelete="DayPilotScheduler1_OnEventDelete"
            
            OnBeforeEventRender="DayPilotScheduler1_OnBeforeEventRender"
            OnCommand="DayPilotScheduler1_OnCommand"
            >
            <TimeHeaders>
                <DayPilot:TimeHeader GroupBy="Month" />
                <DayPilot:TimeHeader GroupBy="Day" Format="ddd d"/>
                <DayPilot:TimeHeader GroupBy="Hour" Format="ht" />
            </TimeHeaders>            
        </DayPilot:DayPilotScheduler>
        
        <div class="space"><asp:LinkButton runat="server" ID="ButtonClear" OnClick="ButtonClear_OnClick">Borrar citas disponibles.</asp:LinkButton> (Solo del mes seleccionado.)</div>        

    </div>
    
<script>
    function scale(size) {
        dp.clientState.size = size;
        dp.commandCallBack("refresh");
    }

</script>    

</asp:Content>