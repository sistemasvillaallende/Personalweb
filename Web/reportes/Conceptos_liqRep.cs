using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Conceptos_liqRep
    {
        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public int unidad { get; set; }
        public bool suma { get; set; }
        public bool sujeto_a_desc { get; set; }
        public decimal haber_cd { get; set; }
        public decimal haber_sd { get; set; }
        public decimal descuento { get; set; }
        public decimal importe { get; set; }

        public Conceptos_liqRep()
        {
            cod_concepto_liq = 0;
            des_concepto_liq = "";
            unidad = 0;
            suma = true;
            sujeto_a_desc = true;
            haber_cd = 0;
            haber_sd = 0;
            descuento = 0;
            importe = 0;

        }


        private static List<Conceptos_liqRep> mapeo(SqlDataReader dr)
        {
            List<Conceptos_liqRep> lst = new List<Conceptos_liqRep>();
            Conceptos_liqRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Conceptos_liqRep();
                    if (!dr.IsDBNull(0)) { obj.cod_concepto_liq = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.des_concepto_liq = dr.GetString(1).Trim(); }
                    if (!dr.IsDBNull(2)) { obj.unidad = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.suma = dr.GetBoolean(3); }
                    if (obj.suma)
                    {
                        if (!dr.IsDBNull(4))
                        {
                            obj.sujeto_a_desc = dr.GetBoolean(4);
                            if (obj.sujeto_a_desc)
                                obj.haber_cd = dr.GetDecimal(8);
                            else
                                obj.haber_sd = dr.GetDecimal(8);
                        }
                    }
                    else
                    {
                        obj.descuento = dr.GetDecimal(8);
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Conceptos_liqRep> readResumen_cptos(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("c.cod_concepto_liq,");
                strSQL.AppendLine("des_concepto = CONVERT(CHAR(30), c.des_concepto_liq),");
                strSQL.AppendLine("--des_liquidacion = RTrim(l.des_liquidacion),");
                strSQL.AppendLine("unidades = count(*),");
                strSQL.AppendLine("c.suma,");
                strSQL.AppendLine("c.sujeto_a_desc,");
                strSQL.AppendLine("0 as haber_cd,");
                strSQL.AppendLine("0 as haber_sd,");
                strSQL.AppendLine("0 as decuento,");
                strSQL.AppendLine("importe = sum(d.importe)");
                strSQL.AppendLine("from det_liq_x_empleado d WITH(NOLOCK)");
                strSQL.AppendLine("join conceptos_liquidacion c on");
                strSQL.AppendLine("d.cod_concepto_liq <> 390 AND");
                strSQL.AppendLine("d.cod_concepto_liq = c.cod_concepto_liq");
                strSQL.AppendLine("join liquidaciones l on");
                strSQL.AppendLine("d.anio = l.anio AND");
                strSQL.AppendLine("d.cod_tipo_liq = l.cod_tipo_liq AND");
                strSQL.AppendLine("d.nro_liquidacion = l.nro_liquidacion");
                strSQL.AppendLine("where");
                strSQL.AppendLine("d.anio = @anio AND");
                strSQL.AppendLine("d.cod_tipo_liq = @cod_tipo_liq AND");
                strSQL.AppendLine("d.nro_liquidacion = @nro_liq");
                strSQL.AppendLine("group by");
                strSQL.AppendLine("c.cod_concepto_liq,");
                strSQL.AppendLine("c.des_concepto_liq,");
                strSQL.AppendLine("c.suma,");
                strSQL.AppendLine("c.sujeto_a_desc");
                strSQL.AppendLine("--l.des_liquidacion");
                strSQL.AppendLine("order by");
                strSQL.AppendLine("c.cod_concepto_liq");

                List<Conceptos_liqRep> lst = new List<Conceptos_liqRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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