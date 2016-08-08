using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection cone1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\daypilot.mdf;Integrated Security=True");
    static SqlDataReader lector;

    protected void Page_Load(object sender, EventArgs e)
    {
        

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        cone1.Open();

        SqlCommand insertarDoctor = new SqlCommand("insert into Doctor(DoctorName)   values('" + TextBox1.Text + "')", cone1);
        System.Diagnostics.Debug.WriteLine("insert into Doctor(DoctorName)   values('" + TextBox1.Text + "')");
        insertarDoctor.ExecuteNonQuery();
        cone1.Close();


        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El medico fue encontrado y registrado existosamente.')", true);



    }
}