using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Aportes_jubilatorios_x_sec
    {
        public string descripcion { get; set; }
        public int id_secretaria { get; set; }
        public string des_secretaria { get; set; }
        public int id_direccion { get; set; }
        public string des_direccion { get; set; }
        public int id_oficina { get; set; }
        public string oficina { get; set; }
        public int orden { get; set; }
        public decimal importe { get; set; }
        

        public Aportes_jubilatorios_x_sec()
        {
            descripcion = string.Empty;
            id_secretaria = 0;
            des_secretaria = string.Empty;
            id_direccion = 0;
            des_direccion = string.Empty;
            id_oficina = 0;
            oficina = string.Empty;
            orden = 0;
            importe = 0;
        }


        private static List<Aportes_jubilatorios_x_sec> mapeo(SqlDataReader dr)
        {
            List<Aportes_jubilatorios_x_sec> lst = new List<Aportes_jubilatorios_x_sec>();
            Aportes_jubilatorios_x_sec obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Aportes_jubilatorios_x_sec();
                    if (!dr.IsDBNull(0)) { obj.descripcion = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.id_secretaria = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.des_secretaria = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.id_direccion = dr.GetInt32(3); }
                    if (!dr.IsDBNull(4)) { obj.des_direccion = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.id_oficina = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { obj.oficina = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.orden = Convert.ToInt32(dr[7]); }
                    if (!dr.IsDBNull(8)) { obj.importe = Convert.ToDecimal(dr[8]); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Aportes_jubilatorios_x_sec> readAportes(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                List<Aportes_jubilatorios_x_sec> lst = new List<Aportes_jubilatorios_x_sec>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_APORTES_JUBILATORIOS_X_SEC";
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