using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace web.reportes
{
    public class Acred_bco_cbaRep
    {
        public string apeynom { get; set; }
        public string cuenta { get; set; }
        public string nro_documento { get; set; }
        public string moneda { get; set; }
        public decimal importe { get; set; }

        public Acred_bco_cbaRep()
        {
            apeynom = string.Empty;
            cuenta = string.Empty;
            nro_documento = string.Empty;
            moneda = string.Empty;
            importe = 0;
        }


        private static List<Acred_bco_cbaRep> mapeo(SqlDataReader dr)
        {
            List<Acred_bco_cbaRep> lst = new List<Acred_bco_cbaRep>();
            Acred_bco_cbaRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Acred_bco_cbaRep();
                    if (!dr.IsDBNull(0)) { obj.apeynom = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.cuenta = Convert.ToString(dr[1]); }
                    if (!dr.IsDBNull(2)) { obj.moneda = dr.GetString(1); }
                    if (!dr.IsDBNull(3)) { obj.nro_documento = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.importe = dr.GetDecimal(4); }
                    //obj.lstDetalle = AUX_DETALLE_SUELDO.read(obj.anio, obj.cod_tipo_liq, obj.nro_liquidacion, obj.legajo);
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Acred_bco_cbaRep> read(int anio, int cod_tipo_liq, int nro_liq, decimal porcentaje)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("Select");
                sql.AppendLine("a.nombre,");
                sql.AppendLine("a.nro_caja_ahorro,");
                sql.AppendLine("'000' as nro_reparto,");
                sql.AppendLine("a.nro_documento,");
                sql.AppendLine("Convert(decimal(15, 2), Round((c.sueldo_neto * @porcentaje / 100), 0, 1)) as sueldo_neto");
                sql.AppendLine("From liq_x_empleado c");
                sql.AppendLine("join empleados a on");
                sql.AppendLine("a.legajo = c.legajo and");
                sql.AppendLine("a.listar = 1 and a.cod_banco = 20");
                //sql.AppendLine("join Acred_bco_prov b on");
                //sql.AppendLine("a.legajo = b.legajo");
                sql.AppendLine("where");
                sql.AppendLine("c.anio = @anio and");
                sql.AppendLine("c.cod_tipo_liq = @cod_tipo_liq and");
                sql.AppendLine("c.nro_liquidacion = @nro_liq");

                List<Acred_bco_cbaRep> lst = new List<Acred_bco_cbaRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    //cmd.Parameters.AddWithValue("@DESDE", desde);
                    //cmd.Parameters.AddWithValue("@HASTA", hasta);
                    cmd.Parameters.AddWithValue("@ANIO", anio);
                    cmd.Parameters.AddWithValue("@COD_TIPO_LIQ", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@NRO_LIQ", nro_liq);
                    cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
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