/* Copyright © 2015 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/

   If you have purchased a DayPilot Pro license, you are allowed to use this 
   code under the conditions of DayPilot Pro License Agreement:

   http://www.daypilot.org/files/LicenseAgreement.pdf

   Otherwise, you are allowed to use it for evaluation purposes only under 
   the conditions of DayPilot Pro Trial License Agreement:
   
   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
   
*/

using System;
using System.Data;
using DayPilot.Web.Ui.Events.Calendar;
using System.Data;
using System.Web.UI;
using Util;
using System.Web;
using System.Data.SqlClient;
using BeforeCellRenderEventArgs = DayPilot.Web.Ui.Events.Navigator.BeforeCellRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Default : System.Web.UI.Page 
{
    //SqlConnection cone1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True");
    //static SqlDataReader lector;

    //private DataRow dr;
    private DataTable _appointments;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCalendarData();
            LoadNavigatorData();
        }

    }

    private void LoadNavigatorData()
    {
        if (_appointments == null)
        {
            LoadAppointments();
        }

        DayPilotNavigator1.DataSource = _appointments;
        DayPilotNavigator1.DataStartField = "AppointmentStart";
        DayPilotNavigator1.DataEndField = "AppointmentEnd";
        DayPilotNavigator1.DataIdField = "AppointmentId";
        DayPilotNavigator1.DataBind();
    }

    private void LoadCalendarData()
    {
        if (_appointments == null)
        {
            LoadAppointments();
        }

        DayPilotCalendar1.DataSource = _appointments;
        DayPilotCalendar1.DataStartField = "AppointmentStart";
        DayPilotCalendar1.DataEndField = "AppointmentEnd";
        DayPilotCalendar1.DataIdField = "AppointmentId";
        DayPilotCalendar1.DataTextField = "AppointmentPatientName";
        DayPilotCalendar1.DataTagFields = "AppointmentStatus";
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.Update();
    }

    private void LoadAppointments()
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);  // basic validation
        _appointments = Db.LoadFreeAndMyAppointments(DayPilotNavigator1.VisibleStart, DayPilotNavigator1.VisibleEnd, Session.SessionID);
    }


    protected void DayPilotCalendar1_OnCommand(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotCalendar1.StartDate = (DateTime) e.Data["day"];
                LoadCalendarData();
                break;
            case "refresh":
                LoadCalendarData();
                break;
        }
        
    }

    protected void DayPilotNavigator1_OnBeforeCellRender(object sender, BeforeCellRenderEventArgs e)
    {
    }

    protected void DayPilotCalendar1_OnBeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        string status = e.Tag["AppointmentStatus"];


       // dr = Db.LoadAppointment(Convert.ToInt32(Request.QueryString["id"]));
       // if (dr == null)
      //  {
       //     throw new Exception("The slot was not found");
      //  }


       // cone1.Open();
       // string idDoctor = dr["DoctorId"].ToString();

       // string s = "SELECT DoctorName FROM[Doctor] WHERE[DoctorId] = " + idDoctor + "; ";
       // SqlCommand cmd = new SqlCommand(s, cone1);
       // System.Diagnostics.Debug.WriteLine(s);

       
       // lector = cmd.ExecuteReader();
       // lector.Read();
        //TextBoxDoctor.Text = lector[0].ToString();

        switch (status)
        {
            case "free":
                e.DurationBarColor = "green";
                e.Html ="Disponible"; 
                e.ToolTip = "Click to Request This Time Slot";
                break;
            case "waiting":
                e.DurationBarColor = "orange";
                e.Html = "Your appointment, waiting for confirmation";
                break;
            case "confirmed":
                e.DurationBarColor = "#f41616";
                e.Html = "Your appointment, confirmed";
                break;
        }
    }

}
