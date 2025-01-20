using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Aportes_obra_social_x_sec
    {
        public string descripcion { get; set; }
        public int orden { get; set; }
        public decimal importe { get; set; }

        public Aportes_obra_social_x_sec()
        {
            descripcion = "";
            orden = 0;
            importe = 0;
        }


        private static List<Aportes_obra_social_x_sec> mapeo(SqlDataReader dr)
        {
            List<Aportes_obra_social_x_sec> lst = new List<Aportes_obra_social_x_sec>();
            Aportes_obra_social_x_sec obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Aportes_obra_social_x_sec();
                    if (!dr.IsDBNull(0)) { obj.descripcion = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.orden = Convert.ToInt32(dr[1]); }
                    if (!dr.IsDBNull(2)) { obj.importe = Convert.ToDecimal(dr[2]); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Aportes_obra_social_x_sec> readAportes(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                List<Aportes_obra_social_x_sec> lst = new List<Aportes_obra_social_x_sec>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Aportes_obra_social";
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}