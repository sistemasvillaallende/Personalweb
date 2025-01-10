using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Cuentas_x_cpto
    {
        public int cod_concepto { get; set; }
        public int cod_tipo_liq { get; set; }
        public string nro_cta_contable { get; set; }
        public string des_tipo_liq { get; set; }
        public string des_clasif_per { get; set; }
        public string des_concepto_liq { get; set; }

        public Cuentas_x_cpto()
        {
            cod_concepto = 0;
            cod_tipo_liq = 0;
            nro_cta_contable = "";
            des_tipo_liq = "";
            des_clasif_per = "";
            des_concepto_liq = "";
        }

        private static List<Cuentas_x_cpto> mapeo(SqlDataReader dr)
        {
            List<Cuentas_x_cpto> lst = new List<Cuentas_x_cpto>();
            Cuentas_x_cpto obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Cuentas_x_cpto();
                    if (!dr.IsDBNull(0)) { obj.cod_concepto = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cod_tipo_liq = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.nro_cta_contable = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.des_tipo_liq = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.des_clasif_per = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.des_concepto_liq = dr.GetString(5); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Cuentas_x_cpto> read()
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("Select ");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("a.cod_tipo_liq,");
                strSQL.AppendLine("a.nro_cta_contable,");
                strSQL.AppendLine("t.des_tipo_liq,");
                strSQL.AppendLine("c.des_clasif_per,");
                strSQL.AppendLine("b.des_concepto_liq");
                strSQL.AppendLine("From Ctas_x_Concepto_Liq a");
                strSQL.AppendLine("join Conceptos_Liquidacion b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq");
                strSQL.AppendLine("left join Clasificaciones_personal c on");
                strSQL.AppendLine("a.cod_clasif_per = c.cod_clasif_per");
                strSQL.AppendLine("join Tipos_liquidacion t on");
                strSQL.AppendLine("a.cod_tipo_liq = t.cod_tipo_liq");
                strSQL.AppendLine("ORDER BY  a.cod_concepto_liq");
                List<Cuentas_x_cpto> lst = new List<Cuentas_x_cpto>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Parameters.AddWithValue("@anio", anio);
                    //cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    //cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
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