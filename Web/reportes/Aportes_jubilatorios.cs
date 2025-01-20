using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Aportes_jubilatorios
    {
        public string descripcion { get; set; }
        public int orden { get; set;
        }
        public decimal importe { get; set; }

        public Aportes_jubilatorios()
        {
            descripcion = "";
            orden = 0;
            importe = 0;
        }


        private static List<Aportes_jubilatorios> mapeo(SqlDataReader dr)
        {
            List<Aportes_jubilatorios> lst = new List<Aportes_jubilatorios>();
            Aportes_jubilatorios obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Aportes_jubilatorios();
                    if (!dr.IsDBNull(0)) { obj.descripcion = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.orden = Convert.ToInt32(dr[1]); }
                    if (!dr.IsDBNull(2)) { obj.importe = Convert.ToDecimal(dr[2]); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Aportes_jubilatorios> readAportes(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                List<Aportes_jubilatorios> lst = new List<Aportes_jubilatorios>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_APORTES_JUBILATORIOS";
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
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