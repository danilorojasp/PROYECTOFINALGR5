using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public static class Global
{
    /// <summary>
    /// Global variable storing important stuff.
    /// </summary>
    static string _FechaCita;
    static string _DoctorCita;

    /// <summary>
    /// Get or set the static important data.
    /// </summary>
    public static string FechaCita
    {
        get
        {
            return _FechaCita;
        }
        set
        {
            _FechaCita = value;
        }
    }



    public static string DoctorCita
    {
        get
        {
            return _DoctorCita;
        }
        set
        {
            _DoctorCita = value;
        }
    }

}