using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DAL
{
    public class EmpxFamiliar
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string cuil { get; set; }
        public string sexo { get; set; }
        public string fecha_ingreso { get; set; }
        public Familiares objFam { get; set; }

        public EmpxFamiliar()
        {
            legajo = 0;
            nombre = string.Empty;
            cuil = string.Empty;
            sexo = string.Empty;
            fecha_ingreso = string.Empty;

            objFam = new Familiares();
        }

        //public static string GetFamiliarxLegajo()
        //{
        //    try
        //    {
        //        DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
        //        string strSQL = @"SELECT a.*
        //                          FROM FAMILIARES a 
        //                          JOIN EMPLEADOS b on 
        //                             a.legajo = b.legajo AND
        //                             b.fecha_baja is null
        //                          WHERE
        //                             a.legajo=@legajo AND
        //                             a.id_parentezco=1";
        //        List<Familiares> lst = new List<Familiares>();
        //        //Familiares obj;
        //        using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
        //        {
        //            SqlCommand cmd = cn.CreateCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = strSQL;
        //            //cmd.Parameters.AddWithValue("@legajo", legajo);
        //            cmd.Connection.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            //lst = mapeo(dr);
        //            //return lst;
        //            //SqlDataReader dr = cmd.ExecuteReader();
        //            //if (dr.HasRows)
        //            //{
        //            //    int legajo2 = dr.GetOrdinal("legajo");
        //            //    int nro_familiar = dr.GetOrdinal("nro_familiar");
        //            //    int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
        //            //    while (dr.Read())
        //            //    {
        //            //        obj = new Familiares();
        //            //        obj.legajo = dr.GetInt32(legajo2);
        //            //        obj.nro_familiar = dr.GetInt32(nro_familiar);
        //            //        obj.fecha_nacimiento = Convert.ToDateTime(dr.GetDateTime(fecha_nacimiento), culturaFecArgentina);
        //            //        lst.Add(obj);
        //            //    }
        //            //}
        //            //return lst;
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

    }
}
