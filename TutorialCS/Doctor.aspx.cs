using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Calendar;
using System.Data.SqlClient;
using BeforeCellRenderEventArgs = DayPilot.Web.Ui.Events.Navigator.BeforeCellRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class Doctor : System.Web.UI.Page
{

    SqlConnection cone1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True");
    static SqlDataReader lector;

    private DataTable _appointments;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                //Schedule.Visible = false;
                //return;
                DataRow first = Db.LoadFirstDoctor();
                if (first != null)
                {
                    Response.Redirect("Doctor.aspx?id=" + first["DoctorId"], true);
                    return;
                }
            }

            int id = Convert.ToInt32(Request.QueryString["id"]);  // basic validation
            DropDownListDoctor.SelectedValue = id.ToString();

            DataRow doctor = Db.LoadDoctor(id);
            if (doctor == null)
            {
                Schedule.Visible = false;
                return;
            }

            DropDownListDoctor.AppendDataBoundItems = false;

            LoadDoctors();
            LoadNavigatorData();
            LoadCalendarData();
        }
    }

    private void LoadDoctors()
    {
        DropDownListDoctor.DataSource = Db.LoadDoctors();
        DropDownListDoctor.DataTextField = "DoctorName";
        DropDownListDoctor.DataValueField = "DoctorId";
        DropDownListDoctor.DataBind();        
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
        _appointments = Db.LoadAppointmentsForDoctor(id, DayPilotNavigator1.VisibleStart, DayPilotNavigator1.VisibleEnd);
    }

    protected void DayPilotCalendar1_OnCommand(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotCalendar1.StartDate = (DateTime)e.Data["day"];
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

    protected void DropDownListDoctor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string selected = DropDownListDoctor.SelectedValue;
        if (selected == "NONE")
        {
            Response.Redirect("Doctor.aspx", true);
            return;
        }

        Response.Redirect("Doctor.aspx?id=" + selected, true);
    }

    protected void DayPilotCalendar1_OnTimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
        int doctor = Convert.ToInt32(Request.QueryString["id"]);

        Db.CreateAppointment(doctor, e.Start, e.End);

        LoadCalendarData();

    }

    protected void DayPilotCalendar1_OnBeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        string status = e.Tag["AppointmentStatus"];
        switch (status)
        {
            case "free":
                e.DurationBarColor = "green";
                break;
            case "waiting":
                e.DurationBarColor = "orange";
                break;
            case "confirmed":
                e.DurationBarColor = "#f41616";  // red            
                break;
        }
    }

    protected void DayPilotCalendar1_OnEventMove(object sender, EventMoveEventArgs e)
    {
        Db.MoveAppointment(e.Id, e.NewStart, e.NewEnd);
        LoadCalendarData();
    }

    protected void DayPilotCalendar1_OnEventResize(object sender, EventResizeEventArgs e)
    {
        Db.MoveAppointment(e.Id, e.NewStart, e.NewEnd);
        LoadCalendarData();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        int i = 0;
        int j;
        int id = Convert.ToInt32(Request.QueryString["id"]);



        cone1.Open();
        string s = "SELECT COUNT(*) FROM Appointment WHERE Appointment.DoctorId = " + id + "; ";
        SqlCommand cmd = new SqlCommand(s, cone1);
        System.Diagnostics.Debug.WriteLine(s);

        lector = cmd.ExecuteReader();
        lector.Read();
        string importarExiste = lector[0].ToString();

        cone1.Close();


        if (importarExiste != "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El registro semanal ya fue importado o ya se ha editado el calendario de "+DropDownListDoctor.SelectedItem+", favor edite los registros manualmente.')", true);

        } else
        {

            cone1.Open();

            for (i= 0; i < 6; i++)
                    {
            
 
                        string c1 = "insert into Appointment(AppointmentStart, AppointmentEnd, DoctorId)   values(SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 9, 00), SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 9, 30), " + id + ");";
                        string c2 = "insert into Appointment(AppointmentStart, AppointmentEnd, DoctorId)   values(SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 9, 30), SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 10, 00), " + id + ");";
                        string c3 = "insert into Appointment(AppointmentStart, AppointmentEnd, DoctorId)   values(SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 10, 00), SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "),10, 30), " + id + ");";
                        string c4 = "insert into Appointment(AppointmentStart, AppointmentEnd, DoctorId)   values(SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "), 10, 30), SMALLDATETIMEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()+" + i + "),11, 00), " + id + ");";



                        SqlCommand insertarHora1 = new SqlCommand(c1, cone1);
                        SqlCommand insertarHora2 = new SqlCommand(c2, cone1);
                        SqlCommand insertarHora3 = new SqlCommand(c3, cone1);
                        SqlCommand insertarHora4 = new SqlCommand(c4, cone1);

                        System.Diagnostics.Debug.WriteLine(c1);
                        insertarHora1.ExecuteNonQuery();
                        insertarHora2.ExecuteNonQuery();
                        insertarHora3.ExecuteNonQuery();
                        insertarHora4.ExecuteNonQuery();

                    }

        }


        cone1.Close();


    }
}