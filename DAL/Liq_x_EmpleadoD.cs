using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Liq_x_EmpleadoD
    {

        public static void SaveDet_Liq_x_Empleado(List<Entities.Det_Liq_x_empleado> lstDet_liq_x_empleado, SqlConnection cn, SqlTransaction trx)
        {

            SqlCommand cmd;
            StringBuilder strSQL = new StringBuilder();

            var ErrorMessage = string.Empty;

            strSQL.AppendLine("INSERT INTO DET_LIQ_X_EMPLEADO");
            strSQL.AppendLine("(anio,");
            strSQL.AppendLine("cod_tipo_liq,");
            strSQL.AppendLine("nro_liquidacion,");
            strSQL.AppendLine("legajo,");
            strSQL.AppendLine("cod_concepto_liq,");
            strSQL.AppendLine("nro_orden,");
            strSQL.AppendLine("fecha_alta_registro,");
            strSQL.AppendLine("importe,");
            strSQL.AppendLine("unidades)");

            strSQL.AppendLine("VALUES");
            strSQL.AppendLine("(@anio,");
            strSQL.AppendLine("@cod_tipo_liq,");
            strSQL.AppendLine("@nro_liquidacion,");
            strSQL.AppendLine("@legajo,");
            strSQL.AppendLine("@cod_concepto_liq,");
            strSQL.AppendLine("@nro_orden,");
            strSQL.AppendLine("@fecha_alta_registro,");
            strSQL.AppendLine("@importe,");
            strSQL.AppendLine("@unidades)");


            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", 0);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", 1);
            cmd.Parameters.AddWithValue("@nro_liquidacion", 1);
            cmd.Parameters.AddWithValue("@legajo", 1);
            cmd.Parameters.AddWithValue("@cod_concepto_liq", 1);
            cmd.Parameters.AddWithValue("@nro_orden", 1);
            cmd.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString()); //DateTime.Today.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@importe", 1);
            cmd.Parameters.AddWithValue("@unidades", 1);
            cmd.CommandText = strSQL.ToString();
            cmd.Connection = cn;
            cmd.Transaction = trx;

            try
            {
                foreach (var item in lstDet_liq_x_empleado)
                {
                    ErrorMessage = "Error en : " + item.anio.ToString() + "/ nro_liq " + item.nro_liquidacion.ToString()
                        + "/ legajo " + item.legajo.ToString()
                        + "/ cod_concepto " + item.cod_concepto_liq.ToString();

                    cmd.Parameters["@anio"].Value = item.anio;
                    cmd.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmd.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmd.Parameters["@legajo"].Value = item.legajo;
                    cmd.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmd.Parameters["@nro_orden"].Value = item.nro_orden;
                    cmd.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmd.Parameters["@importe"].Value = item.importe;
                    cmd.Parameters["@unidades"].Value = item.unidades;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ErrorMessage);
                //throw ex + '' + Exception;
                throw new Exception("Error Encontrado: " + ErrorMessage, ex);
            }
        }


        public static void SaveLiq_x_Empleado(List<Entities.Liq_x_Empleado> lstLiq_x_emp, SqlConnection cn, SqlTransaction trx)
        {

            SqlCommand cmd;
            StringBuilder strSQL = new StringBuilder();



            strSQL.AppendLine("INSERT INTO LIQ_X_EMPLEADO");
            strSQL.AppendLine("(anio,");
            strSQL.AppendLine("cod_tipo_liq,");
            strSQL.AppendLine("nro_liquidacion,");
            strSQL.AppendLine("legajo,");
            strSQL.AppendLine("nro_orden,");
            strSQL.AppendLine("fecha_alta_registro,");
            strSQL.AppendLine("sueldo_neto, sueldo_bruto, no_remunerativo, cod_categoria, sueldo_basico, cod_clasif_per, tarea, cod_cargo, nro_cta_sb)");

            strSQL.AppendLine("VALUES");
            strSQL.AppendLine("(@anio,");
            strSQL.AppendLine("@cod_tipo_liq,");
            strSQL.AppendLine("@nro_liquidacion,");
            strSQL.AppendLine("@legajo,");
            strSQL.AppendLine("@nro_orden,");
            strSQL.AppendLine("@fecha_alta_registro,");
            strSQL.AppendLine("@sueldo_neto, @sueldo_bruto, @no_remunerativo, @cod_categoria, @sueldo_basico, @cod_clasif_per, @tarea, @cod_cargo, @nro_cta_sb)");

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", 0);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", 1);
            cmd.Parameters.AddWithValue("@nro_liquidacion", 1);
            cmd.Parameters.AddWithValue("@legajo", 1);
            cmd.Parameters.AddWithValue("@nro_orden", 1);
            cmd.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());//DateTime.Today.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@sueldo_neto", 1);
            cmd.Parameters.AddWithValue("@sueldo_bruto", 1);
            cmd.Parameters.AddWithValue("@no_remunerativo", 1);
            cmd.Parameters.AddWithValue("@cod_categoria", 1);
            cmd.Parameters.AddWithValue("@sueldo_basico", 1);
            cmd.Parameters.AddWithValue("@cod_clasif_per", 0);
            cmd.Parameters.AddWithValue("@tarea", string.Empty);
            cmd.Parameters.AddWithValue("@cod_cargo", 0);
            cmd.Parameters.AddWithValue("@nro_cta_sb", string.Empty);
            cmd.CommandText = strSQL.ToString();
            cmd.Connection = cn;
            cmd.Transaction = trx;

            try
            {
                foreach (var item in lstLiq_x_emp)
                {
                    if (item.legajo == 658)
                        item.legajo = 658;
                    cmd.Parameters["@anio"].Value = item.anio;
                    cmd.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmd.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmd.Parameters["@legajo"].Value = item.legajo;
                    cmd.Parameters["@nro_orden"].Value = item.nro_orden;
                    cmd.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmd.Parameters["@sueldo_neto"].Value = item.sueldo_neto;
                    cmd.Parameters["@sueldo_bruto"].Value = item.sueldo_bruto;
                    cmd.Parameters["@no_remunerativo"].Value = item.no_remunerativo;
                    cmd.Parameters["@cod_categoria"].Value = item.cod_categoria;
                    cmd.Parameters["@sueldo_basico"].Value = item.sueldo_basico;
                    cmd.Parameters["@cod_clasif_per"].Value = item.cod_clasif_per;
                    cmd.Parameters["@tarea"].Value = item.tarea.Substring(0, 50);
                    cmd.Parameters["@cod_cargo"].Value = item.cod_cargo;
                    cmd.Parameters["@nro_cta_sb"].Value = item.nro_cta_sb;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



    }
}
