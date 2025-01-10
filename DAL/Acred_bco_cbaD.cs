using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Acred_bco_cbaD : DALBase
    {

        private static List<Entities.Acred_bco_cba> mapeo(SqlDataReader dr)
        {
            List<Entities.Acred_bco_cba> lst = new List<Entities.Acred_bco_cba>();
            Entities.Acred_bco_cba obj;
            bool aguinaldo = false;
            string nro_caja_ahorro = "";
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.Acred_bco_cba();
                    //if (!dr.IsDBNull(0)) { obj.campo_1 = dr.GetString(0); }
                    if (!dr.IsDBNull(12)) { aguinaldo = dr.GetBoolean(12); }
                    //if (dr.IsDBNull(12))
                    if (aguinaldo == true )
                        obj.campo_1 = "044";
                    else
                        obj.campo_1 = "001";
                    if (!dr.IsDBNull(1)) { obj.campo_2 = dr.GetString(1).PadLeft(5, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(2)) { obj.campo_3 = dr.GetString(2).PadLeft(2, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(3)) { obj.campo_4 = dr.GetString(3); }
                    if (!dr.IsDBNull(4))
                    {
                        //if (Convert.ToString(dr.GetString(4)).Trim().Length == 8)
                        //    obj.campo_5 = dr.GetString(4).Replace("/", "0").Substring(0, 9).Trim().PadLeft(9, Convert.ToChar("0"));
                        //else
                        //    obj.campo_5 = dr.GetString(4).Replace("/", "0").Substring(1, 9).Trim().PadLeft(9, Convert.ToChar("0"));
                        nro_caja_ahorro = dr.GetString(4).Trim().Replace("/", "0");
                        if (nro_caja_ahorro.Length <= 8)
                            obj.campo_5 = nro_caja_ahorro.Trim().PadLeft(9, Convert.ToChar("0"));
                        else
                            obj.campo_5 = nro_caja_ahorro.Substring(1, nro_caja_ahorro.Length-1).Trim().PadLeft(9, Convert.ToChar("0"));

                    }

                    if (!dr.IsDBNull(5)) { obj.campo_6 = Convert.ToString(dr.GetDecimal(5)).Replace(".", "").PadLeft(18, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(6)) { obj.campo_7 = dr.GetString(6).PadLeft(8, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(7)) { obj.campo_8 = dr.GetString(7).PadLeft(5, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(8)) { obj.campo_9 = dr.GetString(8).PadLeft(6, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(9)) { obj.campo_10 = dr.GetString(9).PadLeft(22, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(10)) { obj.campo_11 = dr.GetString(10).PadLeft(2, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(11)) { obj.campo_12 = dr.GetString(11).PadRight(22, Convert.ToChar(" ")); }
                    //if (!dr.IsDBNull(12)) { obj.aguinaldo = dr.GetBoolean(12); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Entities.Acred_bco_cba> GetAcred_bco_cba(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            try
            {
                List<Entities.Acred_bco_cba> lst = new List<Entities.Acred_bco_cba>();
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT '0' as campo_1, '00392' as campo_2, '01' as campo_3, ");
                strSQL.AppendLine("'3' as campo_4, e.nro_caja_ahorro as campo_5, ");
                //strSQL.AppendLine("l.sueldo_neto as campo_6,");
                strSQL.AppendLine("Convert(decimal(15,2),Round((l.sueldo_neto * @porcentaje / 100 ),0,1)) as campo_6,");
                strSQL.AppendLine("convert(varchar(10),l1.fecha_pago,112)  as campo_7, '02173' as campo_8, ");
                strSQL.AppendLine("'0' as campo_9, '0' as campo_10, '00' as campo_11, ' ' as campo_12, l1.aguinaldo ");
                strSQL.AppendLine("FROM liq_x_empleado l with (nolock) ");
                //strSQL.AppendLine("JOIN Empleados e ON e.listar = 1 and l.legajo = e.legajo ");
                strSQL.AppendLine("JOIN Empleados e ON l.legajo = e.legajo ");
                //strSQL.AppendLine("AND e.cod_clasif_per<>10 "); 
                strSQL.AppendLine("AND e.cod_banco = 20 ");
                strSQL.AppendLine("JOIN liquidaciones l1 ON l.anio=l1.anio ");
                strSQL.AppendLine("AND l.cod_tipo_liq=l1.cod_tipo_liq ");
                strSQL.AppendLine("AND l.nro_liquidacion = l1.nro_liquidacion ");
                strSQL.AppendLine("WHERE e.listar=1 and");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liquidacion");
                strSQL.AppendLine("ORDER by l.legajo");


                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
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

