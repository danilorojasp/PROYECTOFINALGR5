

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Util;

public partial class Request : Page
{


    //string connectionString = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True";
    

    SqlConnection cone1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True");
    static SqlDataReader lector;

    private DataRow dr;
    private DataRow drn;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        CajaValidacion.Visible = false;

        dr = Db.LoadAppointment(Convert.ToInt32(Request.QueryString["id"]));
        drn = Db.LoadDoctor(Convert.ToInt32(Request.QueryString["id"]));
        Console.WriteLine("la consola"+Convert.ToInt32(Request.QueryString["id"]));

        if (dr == null)
        {
            throw new Exception("Las citas no fueron encontradas.");
        }

     
        if (!IsPostBack)
        {

            TextBoxStart.Text = Convert.ToDateTime(dr["AppointmentStart"]).ToString();
            TextBoxEnd.Text = Convert.ToDateTime(dr["AppointmentEnd"]).ToString();

        
            string idDoctor = dr["DoctorId"].ToString();


            cone1.Open();

            string s = "SELECT DoctorName FROM[Doctor] WHERE[DoctorId] = " + idDoctor+ "; ";
            SqlCommand cmd = new SqlCommand(s, cone1);
            System.Diagnostics.Debug.WriteLine(s);

            lector = cmd.ExecuteReader();
            lector.Read();
            TextBoxDoctor.Text = lector[0].ToString();

            // SqlDataAdapter da = new SqlDataAdapter("SELECT DoctorName FROM [Doctor] WHERE [DoctorId] =" + idDoctor, ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

            // DataTable dt = new DataTable();
            // da.Fill(dt);


            TextBoxName.Text = dr["AppointmentPatientName"] as string;
          
            //TextBoxDoctor.Text = dr["DoctorId"].ToString();
            //TextBoxDoctor.Text = drn["DoctorName"].ToString();




            TextBoxName.Focus();

           
        }
    }
    protected void ButtonOK_Click(object sender, EventArgs e)
    {

        string cedulaCorrecta = "a";

        cone1.Open();

        string s = "SELECT COUNT(*) FROM Paciente WHERE Paciente.CedulaPaciente = " + Convert.ToInt32( TextBoxName.Text) + "; ";
        SqlCommand cmd = new SqlCommand(s, cone1);
        System.Diagnostics.Debug.WriteLine(s);

        lector = cmd.ExecuteReader();
        lector.Read();
        string cedulaValidator = lector[0].ToString();
        


        string name = TextBoxName.Text;



        if (cedulaValidator == "0")
        {
            CajaValidacion.Visible = true;
        }
        else
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            Db.RequestAppointment(id, name, Session.SessionID);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cita registrada, cuando el medico verifique y confirme la informacion de su cita, el sistema le enviara un correo electronico con la confirmacion.')", true);

            Modal.Close(this, "OK");

        }
        
    }


    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);
        Db.DeleteAppointmentIfFree(id);
        Modal.Close(this, "OK");
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void CustomValidator1_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {

    }
}
