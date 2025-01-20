using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class AUX_DETALLE_SUELDO : DALBase
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public bool suma { get; set; }
        public bool sujeto_a_desc { get; set; }
        public bool sac { get; set; }
        public decimal unidades { get; set; }
        public decimal importe { get; set; }
        public int nro_orden { get; set; }

        public AUX_DETALLE_SUELDO()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            cod_concepto_liq = 0;
            des_concepto_liq = string.Empty;
            suma = false;
            sujeto_a_desc = false;
            sac = false;
            unidades = 0;
            importe = 0;
            nro_orden = 0;
        }

        private static List<Entities.AUX_DETALLE_SUELDO> mapeo(SqlDataReader dr)
        {
            List<Entities.AUX_DETALLE_SUELDO> lst = new List<Entities.AUX_DETALLE_SUELDO>();
            Entities.AUX_DETALLE_SUELDO obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.AUX_DETALLE_SUELDO();
                    if (!dr.IsDBNull(0)) { obj.anio = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cod_tipo_liq = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.nro_liquidacion = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.legajo = dr.GetInt32(3); }
                    if (!dr.IsDBNull(4)) { obj.cod_concepto_liq = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.des_concepto_liq = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.suma = dr.GetBoolean(6); }
                    if (!dr.IsDBNull(7)) { obj.sujeto_a_desc = dr.GetBoolean(7); }
                    if (!dr.IsDBNull(8)) { obj.sac = dr.GetBoolean(8); }
                    if (!dr.IsDBNull(9)) { obj.unidades = dr.GetDecimal(9); }
                    if (!dr.IsDBNull(10)) { obj.importe = dr.GetDecimal(10); }
                    if (!dr.IsDBNull(11)) { obj.nro_orden = dr.GetInt32(11); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Entities.AUX_DETALLE_SUELDO> read(int anio, int cod_tipo_liq, int nro_liq, int leg)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT");
                sql.AppendLine("d.anio,");
                sql.AppendLine("d.cod_tipo_liq,");
                sql.AppendLine("d.nro_liquidacion,");
                sql.AppendLine("d.legajo,");
                sql.AppendLine("c.cod_concepto_liq,");
                sql.AppendLine("c.des_concepto_liq,");
                sql.AppendLine("c.suma,");
                sql.AppendLine("c.sujeto_a_desc,");
                sql.AppendLine("c.sac,");
                sql.AppendLine("d.unidades,");
                sql.AppendLine("d.importe,");
                sql.AppendLine("d.nro_orden");
                sql.AppendLine("FROM Det_Liq_x_Empleado d WITH (NOLOCK)");
                sql.AppendLine("JOIN Conceptos_Liquidacion c ON");
                sql.AppendLine("d.cod_concepto_liq=c.cod_concepto_liq");
                sql.AppendLine("WHERE");
                sql.AppendLine("d.cod_concepto_liq<>390 AND");
                sql.AppendLine("d.anio=@ANIO AND");
                sql.AppendLine("d.cod_tipo_liq=@COD_TIPO_LIQ AND");
                sql.AppendLine("d.nro_liquidacion=@NRO_LIQ AND");
                sql.AppendLine("d.legajo=@LEG");
                sql.AppendLine("order by  d.cod_concepto_liq");
                List<Entities.AUX_DETALLE_SUELDO> lst = new List<Entities.AUX_DETALLE_SUELDO>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@ANIO", anio);
                    cmd.Parameters.AddWithValue("@COD_TIPO_LIQ", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@NRO_LIQ", nro_liq);
                    cmd.Parameters.AddWithValue("@LEG", leg);
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

