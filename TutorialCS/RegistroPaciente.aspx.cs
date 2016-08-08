using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class RegistroPaciente : System.Web.UI.Page
{
    string nombreCompleto;
    string telefono;
    string correo;
    SqlConnection cone1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True");
    static SqlDataReader lector;

    SqlConnection coneIESS = new SqlConnection(@"Data Source=(LocalDB)\v11.0;Initial Catalog=IESS;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    static SqlDataReader lectorIESS;




    
    protected void Page_Load(object sender, EventArgs e)
    {
        div1.Visible = false;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {




         cone1.Open();

         SqlCommand insertarPaciente = new SqlCommand("insert into PACIENTE(NOMBREPACIENTE,CEDULAPACIENTE,TELEFONO,CORREO)   values('" + TextNombre.Text + "','" + Convert.ToInt32(TextBox1.Text) + "','" + TextTelefono.Text + "','" + TextCorreo.Text + "')", cone1);
         System.Diagnostics.Debug.WriteLine("insert into PACIENTE(NOMBREPACIENTE,TELEFONO,CORREO)   values('" + TextNombre.Text + "','" + TextTelefono.Text + "','" + TextCorreo.Text + "')");
         insertarPaciente.ExecuteNonQuery();
         cone1.Close();












        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Paciente Registrado Exitosamente.')", true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        string datos = "SELECT * FROM PacienteIESS WHERE PacienteIESS.Cedula = " + TextBox1.Text + "; ";
        SqlCommand traer= new SqlCommand(datos, coneIESS);

        cone1.Open();
        coneIESS.Open();


        //base de la aplicacion
        string s = "SELECT COUNT(*) FROM Paciente WHERE Paciente.CedulaPaciente = " + Convert.ToInt32(TextBox1.Text) + "; ";
        SqlCommand cmd = new SqlCommand(s, cone1);


        //base del iess
        string sIESS = "SELECT COUNT(*) FROM PacienteIESS WHERE PacienteIESS.Cedula = " + TextBox1.Text + "; ";
        SqlCommand cmdIESS = new SqlCommand(sIESS, coneIESS);


        System.Diagnostics.Debug.WriteLine(s);
        System.Diagnostics.Debug.WriteLine(sIESS);


        lectorIESS = cmdIESS.ExecuteReader();
        lectorIESS.Read();

        //base del IESS
        string controlCedulaIESS = lectorIESS[0].ToString();

        lector = cmd.ExecuteReader();
        lector.Read();


        //base de la aplicacion
        string controlCedula = lector[0].ToString();


        cone1.Close();
        coneIESS.Close();





        if (controlCedulaIESS == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EL PACIENTE NO ESTA REGISTRADO EN EL SISTEMA, FAVOR CONTACTE AL 1800-100-000, EXT-140 ')", true);
  
        }
        else
        {
            if (controlCedula == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El paciente ya se encuentra registrado, ingrese a la seccion pacientes para solicitar una cita. ')", true);

            }
            else
            {

                coneIESS.Open();

                System.Diagnostics.Debug.WriteLine(datos);

                //base del IESS
                lectorIESS = traer.ExecuteReader();
                lectorIESS.Read();
                nombreCompleto = lectorIESS[1].ToString();
                telefono = lectorIESS[5].ToString();
                correo = lectorIESS[9].ToString();

                TextNombre.Text = nombreCompleto;
                TextTelefono.Text = telefono;
                TextCorreo.Text = correo;

                coneIESS.Close();
               




                div1.Visible = true;
            }


        }
        
    }

    protected void EntityDataSource1_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
    {

    }

    protected void IESS_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}