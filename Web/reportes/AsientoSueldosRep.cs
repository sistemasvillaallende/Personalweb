using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class AsientoSueldosRep
    {
        public string nro_cta_contable { get; set; }
        public string nom_cuenta { get; set; }
        public int id_secretaria { get; set; }
        public string des_secretaria { get; set; }
        public int id_direccion { get; set; }
        public string des_direccion { get; set; }
        public int id_oficina { get; set; }
        public string nom_oficina { get; set; }
        public int suma { get; set; }
        public decimal importe { get; set; }
        public int cantidad { get; set; }

        public AsientoSueldosRep()
        {
            nro_cta_contable = "";
            nom_cuenta = "";
            suma = 0;
            importe = 0;
            cantidad = 0;
            id_secretaria = 0;
            des_secretaria = "";
            id_direccion = 0;
            des_direccion = "";
            id_oficina = 0;
            nom_oficina = "";
        }
         
        private static List<AsientoSueldosRep> mapeo(SqlDataReader dr)
        {
            List<AsientoSueldosRep> lst = new List<AsientoSueldosRep>();
            AsientoSueldosRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new AsientoSueldosRep();
                    if (!dr.IsDBNull(0)) { obj.nro_cta_contable = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.nom_cuenta = dr.GetString(1).Trim(); }
                    if (!dr.IsDBNull(2)) { obj.id_secretaria = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.des_secretaria = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.id_direccion = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.des_direccion = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.id_oficina = dr.GetInt32(6); }
                    if (!dr.IsDBNull(7)) { obj.nom_oficina = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.suma = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.importe = Convert.ToDecimal(dr[9]); }
                    if (!dr.IsDBNull(10)) { obj.cantidad = dr.GetInt32(10); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<AsientoSueldosRep> readAsientoSueldos(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                List<AsientoSueldosRep> lst = new List<AsientoSueldosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Asiento_sueldos_x_sec";
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