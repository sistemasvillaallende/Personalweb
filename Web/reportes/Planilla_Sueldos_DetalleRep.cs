using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Planilla_Sueldos_DetalleRep
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
        public int unidades { get; set; }
        public decimal importe { get; set; }
        public int nro_orden { get; set; }


        public Planilla_Sueldos_DetalleRep()
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

        private static List<Planilla_Sueldos_DetalleRep> mapeo(SqlDataReader dr)
        {
            List<Planilla_Sueldos_DetalleRep> lst = new List<Planilla_Sueldos_DetalleRep>();
            Planilla_Sueldos_DetalleRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Planilla_Sueldos_DetalleRep();
                    if (!dr.IsDBNull(0)) { obj.anio = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cod_tipo_liq = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.nro_liquidacion = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.legajo = dr.GetInt32(3); }
                    if (!dr.IsDBNull(4)) { obj.cod_concepto_liq = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.des_concepto_liq = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.suma = dr.GetBoolean(6); }
                    if (!dr.IsDBNull(7)) { obj.sujeto_a_desc = dr.GetBoolean(7); }
                    if (!dr.IsDBNull(8)) { obj.sac = dr.GetBoolean(8); }
                    if (!dr.IsDBNull(9)) { obj.unidades = Convert.ToInt32(dr[9]); }
                    if (!dr.IsDBNull(10)) { obj.importe = dr.GetDecimal(10); }
                    if (!dr.IsDBNull(11)) { obj.nro_orden = dr.GetInt32(11); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Planilla_Sueldos_DetalleRep> read(int anio, int cod_tipo_liq, int nro_liq, int legajo)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("d.anio,");
                strSQL.AppendLine("d.cod_tipo_liq,");
                strSQL.AppendLine("d.nro_liquidacion,");
                strSQL.AppendLine("d.legajo,");
                strSQL.AppendLine("c.cod_concepto_liq,");
                strSQL.AppendLine("c.des_concepto_liq,");
                strSQL.AppendLine("c.suma,");
                strSQL.AppendLine("c.sujeto_a_desc,");
                strSQL.AppendLine("c.sac,");
                strSQL.AppendLine("d.unidades,");
                strSQL.AppendLine("d.importe,");
                strSQL.AppendLine("d.nro_orden");
                strSQL.AppendLine("FROM Det_Liq_x_Empleado d WITH(NOLOCK)");
                strSQL.AppendLine("JOIN Conceptos_Liquidacion c ON");
                strSQL.AppendLine("d.cod_concepto_liq = c.cod_concepto_liq");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("d.cod_concepto_liq <> 390 AND");
                strSQL.AppendLine("d.anio = @anio AND");
                strSQL.AppendLine("d.cod_tipo_liq = @cod_tipo_liq AND");
                strSQL.AppendLine("d.nro_liquidacion = @nro_liq AND");
                strSQL.AppendLine("d.legajo = @legajo");
                strSQL.AppendLine("order by");
                strSQL.AppendLine("d.cod_concepto_liq");
                List<Planilla_Sueldos_DetalleRep> lst = new List<Planilla_Sueldos_DetalleRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    cmd.Parameters.AddWithValue("@legajo", legajo);
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