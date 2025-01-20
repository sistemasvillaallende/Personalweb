

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DAL
{
    public class SijcorDCopia
    {

        public static List<Entities.Sijcor> GetSijcor(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int opcion)
        {
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            Entities.Sijcor eSijcor;
            StringBuilder strSQL = new StringBuilder();
            SqlCommand cmd;
            SqlConnection cn = DALBase.GetConnection("Siimva");

            strSQL.AppendLine("select e.legajo, cuil=REPLACE(e.cuil,'-',''), ");
            strSQL.AppendLine(" e.nombre, e.tarea, categoria = convert(varchar(2), c1.cod_categoria), ");
            strSQL.AppendLine(" e.cod_regimen_empleado, ");
            strSQL.AppendLine(" cod_modalidad_contratacion = ");
            strSQL.AppendLine(" CASE ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=1 THEN '0003' ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=2 THEN '0004' ");
            strSQL.AppendLine("  ELSE '0004' ");
            strSQL.AppendLine(" END, ");
            //
            strSQL.AppendLine(" cant_dias_trabajados= (SELECT isnull(SUM(unidades),0) ");
            strSQL.AppendLine("  FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine("  WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine("  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine("  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine("  dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine("  dlxe2.cod_concepto_liq BETWEEN 300 AND 310), ");
            //
            strSQL.AppendLine(" sueldo390 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390)), ");
            //ok
            //strSQL.AppendLine(" dlxe2.cod_concepto_liq in (10, 12, 17, 20, 50, 60, 63, 65, 66, 110, 120, 121, 149, 150, 151, 152, 157, 159, 160,");
            //strSQL.AppendLine(" 161, 162, 163, 164, 166, 170, 173, 175, 176, 300, 310, 499, 831, 174, 183, 496, 300, 162, 831)),");
            strSQL.AppendLine(" sueldo390BIS = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390,499)), ");//ok
            //--------------------------------------------------------------------------------//
            //'    strSQl = strSQl & " otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'    "  FROM DET_LIQ_X_EMPLEADO dlxe2  " & _
            //'    "  WHERE dlxe2.anio=dlxe.anio AND " & _
            //'    "  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'    "  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'    "  dlxe2.legajo=dlxe.legajo AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 16 and  19) AND " & _
            //'    " (dlxe2.cod_concepto_liq=65) AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 153 and  155)), "
            //--------------------------------------------------------------------------------//

            strSQL.AppendLine(" otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2  ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (12,154)), ");
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos1 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND (dlxe2.cod_concepto_liq=62)),");
            //--------------------------------------------------------------------------------//
            //'strSQl = strSQl & " otroscodigos2 = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'" FROM DET_LIQ_X_EMPLEADO dlxe2 " & _
            //'" WHERE dlxe2.anio=dlxe.anio AND " & _
            //'" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq=65), "
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos3 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq = 153 ), ");
            //////////////////////////////////////////////////////////////////////////////////////
            //' conceptos No remunerativos
            //////////////////////////////////////////////////////////////////////////////////////
            strSQL.AppendLine(" conceptos_no_remunerativos_1=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.suma <> 0 AND B.sujeto_a_desc = 0");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo and dlxe2.cod_concepto_liq not in (410,420,421,430,440,450,470,480,500,510,520,540,570,580,590)),");//ok
            //////////////////////////////////////////////////////////////////////////////////////                                                                                                                                                                                                                                                 //--------------------------------------------------------------------------------//
            strSQL.AppendLine(" aguinaldo=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.sac=1");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq between 16 and 19), ");
            //
            strSQL.AppendLine(" valor_codigo_800 =(SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 800),  ");
            //
            strSQL.AppendLine("  importe_adherente_voluntario =(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine("  FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine("  WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine("  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine("  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine("  dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine("  dlxe2.cod_concepto_liq=800), ");
            //
            strSQL.AppendLine(" valor_codigo_624 = (SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 624)  ");
            //
            //1 = byNroliquidacion
            //2 = byPeriodo
            if (opcion == 1)
            {
                strSQL.AppendLine(" FROM LIQ_X_EMPLEADO dlxe ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where ");
                strSQL.AppendLine(" dlxe.anio =@vAnio");
                strSQL.AppendLine(" AND dlxe.cod_tipo_liq = @vCod_tipo_liq");
                strSQL.AppendLine(" AND dlxe.nro_liquidacion =@vNro_liquidacion");
                strSQL.AppendLine(" ORDER BY dlxe.legajo ");
            }
            else
            {
                strSQL.AppendLine(" FROM LIQUIDACIONES L ");
                strSQL.AppendLine(" JOIN LIQ_X_EMPLEADO dlxe ON ");
                strSQL.AppendLine(" L.anio = dlxe.anio AND ");
                strSQL.AppendLine(" L.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
                strSQL.AppendLine(" L.nro_liquidacion = dlxe.nro_liquidacion ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where L.anio =@vAnio");
                strSQL.AppendLine(" AND L.periodo=@vPeriodo");
                strSQL.AppendLine(" ORDER BY dlxe.legajo ");
            }

            SqlDataReader dr;
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@vAnio", anio);
                //1 = byNroliquidacion
                //2 = byPeriodo
                if (opcion == 1)
                {
                    cmd.Parameters.AddWithValue("@vCod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@vNro_liquidacion", nro_liquidacion);
                }
                else
                    cmd.Parameters.AddWithValue("@vPeriodo", periodo);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int cuil = dr.GetOrdinal("cuil");
                    int nombre = dr.GetOrdinal("nombre");
                    int cargo = dr.GetOrdinal("tarea");
                    int categoria = dr.GetOrdinal("categoria");
                    //int adherente_vol = 
                    //int adherente_obl =
                    //int cod_situacion =
                    //int cod_condicion =
                    int cod_actividad = dr.GetOrdinal("cod_regimen_empleado");
                    int afiliado = 0;
                    int valor_codigo_624 = dr.GetOrdinal("valor_codigo_624");
                    int valor_codigo_800 = dr.GetOrdinal("valor_codigo_800");
                    int cod_modalidad_contratacion = dr.GetOrdinal("cod_modalidad_contratacion");
                    //int cod_siniestro =
                    //int cod_departamento =
                    //int cod_delegacion =
                    //int cod_obra_social =
                    //int cod_situacion_1er_tramo =
                    //int cant_dias_1er_tramo =
                    //int cod_situacion_2do_tramo =
                    //int cant_dias_2do_tramo =
                    //int cod_situacion_3er_tramo =
                    //int cant_dias_3er_tramo =
                    int cant_dias_trabajados = dr.GetOrdinal("cant_dias_trabajados");
                    int sueldo390 = dr.GetOrdinal("sueldo390");
                    int otroscodigos = dr.GetOrdinal("otroscodigos");
                    //int otroscodigos1 = dr.GetOrdinal("otroscodigos1");
                    int otroscodigos3 = 0; //dr.GetOrdinal("otroscodigos3");
                                           //int importe_hs_extra =
                                           //int zona_desfavorable =
                    int conceptos_no_remunerativos_1 = dr.GetOrdinal("conceptos_no_remunerativos_1");
                    //int conceptos_no_remunerativos_497 = 0; //dr.GetOrdinal("conceptos_no_remunerativos_497");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_2 = dr.GetOrdinal("conceptos_no_remunerativos_2");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_3 = 0;// dr.GetOrdinal("conceptos_no_remunerativos_3");

                    //int retroactividades =
                    int aguinaldo = dr.GetOrdinal("aguinaldo");
                    //int remuneracion_2 =
                    //int tipo_adicional_seg_vida =
                    //15/04/2013
                    // Se agregaron 3 campos nuevos
                    //int secuencia_cuil =
                    //int diferencia_x_jerarquia =
                    int importe_adherente_voluntario = dr.GetOrdinal("importe_adherente_voluntario");
                    //
                    decimal sueldo = 0;
                    decimal dias = 0;

                    while (dr.Read())
                    {
                        eSijcor = new Entities.Sijcor();
                        //
                        if (!dr.IsDBNull(cuil))
                            eSijcor.cuil = dr.GetString(cuil).Substring(0, 11);
                        if (!dr.IsDBNull(nombre))
                            eSijcor.apeynom = dr.GetString(nombre).ToUpper().PadRight(30).Substring(0, 30);
                        if (!dr.IsDBNull(cargo))
                            eSijcor.cargo = dr.GetString(cargo).Trim().ToUpper().PadRight(30).Substring(0, 30);
                        if (!dr.IsDBNull(categoria))
                            eSijcor.categoria = dr.GetString(categoria).PadRight(30);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (!dr.IsDBNull(cod_actividad))
                            eSijcor.cod_actividad = string.Format("{0:0000}", dr.GetInt32(cod_actividad));//.PadLeft(4, '0');
                        if (!dr.IsDBNull(valor_codigo_800))
                            eSijcor.adherente_vol = string.Format("{0:00}", dr.GetDecimal(valor_codigo_800)).Replace(".", "");//.PadLeft(2, '0');
                        if (!dr.IsDBNull(valor_codigo_624) && !dr.IsDBNull(valor_codigo_800))
                        {
                            afiliado = Convert.ToInt32(dr.GetDecimal(valor_codigo_624) - 1 - dr.GetDecimal(valor_codigo_800));
                            if (afiliado < 0)
                                eSijcor.adherente_obl = "00";
                            else
                                eSijcor.adherente_obl = string.Format("{0:00}", afiliado.ToString());//.PadLeft(2, '0');
                        }
                        else
                            eSijcor.adherente_obl = "00";
                        if (!dr.IsDBNull(cod_modalidad_contratacion))
                            eSijcor.cod_modalidad_contratacion = dr.GetString(cod_modalidad_contratacion);
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (!dr.IsDBNull(cant_dias_trabajados))
                        {
                            dias = 30 - dr.GetDecimal(cant_dias_trabajados);
                            eSijcor.cant_dias_trabajados = string.Format("{0:00}", dias);
                        }
                        if (!dr.IsDBNull(sueldo390))
                        {
                            sueldo = dr.GetDecimal(sueldo390);
                            //3/5/2019 no debe sumar el codigo 12 & 154 pq esta incluido el 390
                            //sueldo = dr.GetDecimal(sueldo390) + dr.GetDecimal(otroscodigos);
                            eSijcor.sueldo = string.Format("{0:000000.00}", sueldo).Replace(".", ",");
                        }
                        else
                            eSijcor.sueldo = "000000,00";

                        if (!dr.IsDBNull(conceptos_no_remunerativos_1))
                            eSijcor.conceptos_no_remunerativos = string.Format("{0:000000.00}", dr.GetDecimal(conceptos_no_remunerativos_1)).Replace(".", ",");
                        //2019
                        //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_2)).Replace(".", ",");
                        //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_497) +
                        //dr.GetDecimal(conceptos_no_remunerativos_2) + dr.GetDecimal(conceptos_no_remunerativos_3)).Replace(".", ",");
                        else
                            eSijcor.conceptos_no_remunerativos = "000000,00";

                        //.conceptos_no_remunerativos = Replace(Format$(rs("conceptos_no_remunerativos_1").Value + _
                        //rs("conceptos_no_remunerativos_497").Value + _
                        //rs("conceptos_no_remunerativos_2").Value + rs("conceptos_no_remunerativos_3").Value, "000000.00"), ".", ",")

                        eSijcor.retroactividades = "000000,00";
                        if (!dr.IsDBNull(aguinaldo))
                            eSijcor.aguinaldo = string.Format("{0:000000.00}", dr.GetDecimal(aguinaldo)).Replace(".", ",");
                        else
                            eSijcor.aguinaldo = "000000,00";
                        eSijcor.secuencia_cuil = "01";
                        eSijcor.diferencia_x_jerarquia = "000000,00";
                        if (!dr.IsDBNull(importe_adherente_voluntario))
                            eSijcor.importe_adherente_voluntario = string.Format("{0:000000.00}", dr.GetDecimal(importe_adherente_voluntario)).Replace(".", ",");
                        else
                            eSijcor.importe_adherente_voluntario = "000000,00";
                        lstSijcor.Add(eSijcor);
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            return lstSijcor;
        }

        public static List<Entities.Sijcor> GetSijcorConAguilucho(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int opcion)
        {
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            Entities.Sijcor eSijcor;
            StringBuilder strSQL = new StringBuilder();
            SqlCommand cmd;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            int legajo_ant = 0;
            decimal monto_no_remunerativo = 0;
            decimal monto_aguinaldo = 0;

            strSQL.AppendLine("select e.legajo, cuil=REPLACE(e.cuil,'-',''), ");
            strSQL.AppendLine(" e.nombre, e.tarea, categoria = convert(varchar(2), c1.cod_categoria), ");
            strSQL.AppendLine(" e.cod_regimen_empleado, ");
            strSQL.AppendLine(" cod_modalidad_contratacion = ");
            strSQL.AppendLine(" CASE ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=1 THEN '0003' ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=2 THEN '0004' ");
            strSQL.AppendLine("  ELSE '0004' ");
            strSQL.AppendLine(" END, ");
            //
            strSQL.AppendLine(" cant_dias_trabajados= (SELECT isnull(SUM(unidades),0) ");
            strSQL.AppendLine("  FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine("  WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine("  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine("  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine("  dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine("  dlxe2.cod_concepto_liq BETWEEN 300 AND 310), ");
            //
            strSQL.AppendLine(" sueldo390 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390)), ");
            //ok
            //strSQL.AppendLine(" dlxe2.cod_concepto_liq in (10, 12, 17, 20, 50, 60, 63, 65, 66, 110, 120, 121, 149, 150, 151, 152, 157, 159, 160,");
            //strSQL.AppendLine(" 161, 162, 163, 164, 166, 170, 173, 175, 176, 300, 310, 499, 831, 174, 183, 496, 300, 162, 831)),");
            strSQL.AppendLine(" sueldo390BIS = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390,499)), ");//ok
            //--------------------------------------------------------------------------------//
            //'    strSQl = strSQl & " otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'    "  FROM DET_LIQ_X_EMPLEADO dlxe2  " & _
            //'    "  WHERE dlxe2.anio=dlxe.anio AND " & _
            //'    "  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'    "  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'    "  dlxe2.legajo=dlxe.legajo AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 16 and  19) AND " & _
            //'    " (dlxe2.cod_concepto_liq=65) AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 153 and  155)), "
            //--------------------------------------------------------------------------------//

            strSQL.AppendLine(" otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2  ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (12,154)), ");
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos1 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND (dlxe2.cod_concepto_liq=62)),");
            //--------------------------------------------------------------------------------//
            //'strSQl = strSQl & " otroscodigos2 = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'" FROM DET_LIQ_X_EMPLEADO dlxe2 " & _
            //'" WHERE dlxe2.anio=dlxe.anio AND " & _
            //'" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq=65), "
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos3 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq = 153 ), ");
            //////////////////////////////////////////////////////////////////////////////////////
            //' conceptos No remunerativos
            //////////////////////////////////////////////////////////////////////////////////////
            strSQL.AppendLine(" conceptos_no_remunerativos_1=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.suma <> 0 AND B.sujeto_a_desc = 0");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo and dlxe2.cod_concepto_liq not in (410,420,421,430,440,450,470,480,500,510,520,540,570,580,590)),");//ok
            //////////////////////////////////////////////////////////////////////////////////////                                                                                                                                                                                                                                                 //--------------------------------------------------------------------------------//
            strSQL.AppendLine(" aguinaldo=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.sac=1");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq between 16 and 19), ");
            //
            strSQL.AppendLine(" valor_codigo_800 =(SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 800),  ");
            //
            strSQL.AppendLine("  importe_adherente_voluntario =(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine("  FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine("  WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine("  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine("  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine("  dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine("  dlxe2.cod_concepto_liq=800), ");
            //
            strSQL.AppendLine(" valor_codigo_624 = (SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 624)  ");
            //
            //1 = byNroliquidacion
            //2 = byPeriodo
            if (opcion == 1)
            {
                strSQL.AppendLine(" FROM LIQ_X_EMPLEADO dlxe ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where ");
                strSQL.AppendLine(" dlxe.anio =@vAnio");
                strSQL.AppendLine(" AND dlxe.cod_tipo_liq = @vCod_tipo_liq");
                strSQL.AppendLine(" AND dlxe.nro_liquidacion =@vNro_liquidacion");
                strSQL.AppendLine(" ORDER BY dlxe.legajo, dlxe.nro_liquidacion  ");
            }
            else
            {
                strSQL.AppendLine(" FROM LIQUIDACIONES L ");
                strSQL.AppendLine(" JOIN LIQ_X_EMPLEADO dlxe ON ");
                strSQL.AppendLine(" L.anio = dlxe.anio AND ");
                strSQL.AppendLine(" L.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
                strSQL.AppendLine(" L.nro_liquidacion = dlxe.nro_liquidacion ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where L.anio =@vAnio");
                strSQL.AppendLine(" AND L.periodo=@vPeriodo");
                //strSQL.AppendLine(" AND L.cod_tipo_liq = @vCod_tipo_liq");
                strSQL.AppendLine(" ORDER BY dlxe.legajo, dlxe.nro_liquidacion  ");
            }

            SqlDataReader dr;
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@vAnio", anio);
                //1 = byNroliquidacion
                //2 = byPeriodo
                if (opcion == 1)
                {
                    cmd.Parameters.AddWithValue("@vCod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@vNro_liquidacion", nro_liquidacion);
                }
                else
                {
                    //cmd.Parameters.AddWithValue("@vCod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@vPeriodo", periodo);
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int cuil = dr.GetOrdinal("cuil");
                    int nombre = dr.GetOrdinal("nombre");
                    int cargo = dr.GetOrdinal("tarea");
                    int categoria = dr.GetOrdinal("categoria");
                    //int adherente_vol = 
                    //int adherente_obl =
                    //int cod_situacion =
                    //int cod_condicion =
                    int cod_actividad = dr.GetOrdinal("cod_regimen_empleado");
                    int afiliado = 0;
                    int valor_codigo_624 = dr.GetOrdinal("valor_codigo_624");
                    int valor_codigo_800 = dr.GetOrdinal("valor_codigo_800");
                    int cod_modalidad_contratacion = dr.GetOrdinal("cod_modalidad_contratacion");
                    //int cod_siniestro =
                    //int cod_departamento =
                    //int cod_delegacion =
                    //int cod_obra_social =
                    //int cod_situacion_1er_tramo =
                    //int cant_dias_1er_tramo =
                    //int cod_situacion_2do_tramo =
                    //int cant_dias_2do_tramo =
                    //int cod_situacion_3er_tramo =
                    //int cant_dias_3er_tramo =
                    int cant_dias_trabajados = dr.GetOrdinal("cant_dias_trabajados");
                    int sueldo390 = dr.GetOrdinal("sueldo390");
                    int otroscodigos = dr.GetOrdinal("otroscodigos");
                    //int otroscodigos1 = dr.GetOrdinal("otroscodigos1");
                    int otroscodigos3 = 0; //dr.GetOrdinal("otroscodigos3");
                                           //int importe_hs_extra =
                                           //int zona_desfavorable =
                    int conceptos_no_remunerativos_1 = dr.GetOrdinal("conceptos_no_remunerativos_1");
                    //int conceptos_no_remunerativos_497 = 0; //dr.GetOrdinal("conceptos_no_remunerativos_497");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_2 = dr.GetOrdinal("conceptos_no_remunerativos_2");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_3 = 0;// dr.GetOrdinal("conceptos_no_remunerativos_3");

                    //int retroactividades =
                    int aguinaldo = dr.GetOrdinal("aguinaldo");
                    //int remuneracion_2 =
                    //int tipo_adicional_seg_vida =
                    //15/04/2013
                    // Se agregaron 3 campos nuevos
                    //int secuencia_cuil =
                    //int diferencia_x_jerarquia =
                    int importe_adherente_voluntario = dr.GetOrdinal("importe_adherente_voluntario");
                    //
                    int legajo = dr.GetOrdinal("legajo");
                    decimal sueldo = 0;
                    decimal dias = 0;

                    eSijcor = new Entities.Sijcor();
                    while (dr.Read())
                    {

                        //eSijcor = new Entities.Sijcor();
                        if (legajo_ant != dr.GetInt32(legajo))
                        {

                            legajo_ant = dr.GetInt32(legajo);

                            if (!dr.IsDBNull(cuil))
                                eSijcor.cuil = dr.GetString(cuil).Substring(0, 11);
                            if (!dr.IsDBNull(nombre))
                                eSijcor.apeynom = dr.GetString(nombre).ToUpper().PadRight(30).Substring(0, 30);
                            if (!dr.IsDBNull(cargo))
                                eSijcor.cargo = dr.GetString(cargo).Trim().ToUpper().PadRight(30).Substring(0, 30);
                            if (!dr.IsDBNull(categoria))
                                eSijcor.categoria = dr.GetString(categoria).PadRight(30);
                            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            if (!dr.IsDBNull(cod_actividad))
                                eSijcor.cod_actividad = string.Format("{0:0000}", dr.GetInt32(cod_actividad));//.PadLeft(4, '0');
                            if (!dr.IsDBNull(valor_codigo_800))
                                eSijcor.adherente_vol = string.Format("{0:00}", dr.GetDecimal(valor_codigo_800)).Replace(".", "");//.PadLeft(2, '0');
                            if (!dr.IsDBNull(valor_codigo_624) && !dr.IsDBNull(valor_codigo_800))
                            {
                                afiliado = Convert.ToInt32(dr.GetDecimal(valor_codigo_624) - 1 - dr.GetDecimal(valor_codigo_800));
                                if (afiliado < 0)
                                    eSijcor.adherente_obl = "00";
                                else
                                    eSijcor.adherente_obl = string.Format("{0:00}", afiliado.ToString());//.PadLeft(2, '0');
                            }
                            else
                                eSijcor.adherente_obl = "00";
                            if (!dr.IsDBNull(cod_modalidad_contratacion))
                                eSijcor.cod_modalidad_contratacion = dr.GetString(cod_modalidad_contratacion);
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            if (!dr.IsDBNull(cant_dias_trabajados))
                            {
                                dias = 30 - dr.GetDecimal(cant_dias_trabajados);
                                eSijcor.cant_dias_trabajados = string.Format("{0:00}", dias);
                            }
                            if (!dr.IsDBNull(sueldo390))
                            {
                                sueldo = dr.GetDecimal(sueldo390);
                                //3/5/2019 no debe sumar el codigo 12 & 154 pq esta incluido el 390
                                //sueldo = dr.GetDecimal(sueldo390) + dr.GetDecimal(otroscodigos);
                                eSijcor.sueldo = string.Format("{0:000000.00}", sueldo).Replace(".", ",");
                            }
                            else
                                eSijcor.sueldo = "000000,00";

                            if (!dr.IsDBNull(conceptos_no_remunerativos_1))
                            {
                                monto_no_remunerativo = dr.GetDecimal(conceptos_no_remunerativos_1);
                                eSijcor.conceptos_no_remunerativos = string.Format("{0:000000.00}", dr.GetDecimal(conceptos_no_remunerativos_1)).Replace(".", ",");
                                //2019
                                //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_2)).Replace(".", ",");
                                //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_497) +
                                //dr.GetDecimal(conceptos_no_remunerativos_2) + dr.GetDecimal(conceptos_no_remunerativos_3)).Replace(".", ",");
                            }
                            else
                                eSijcor.conceptos_no_remunerativos = "000000,00";

                            //.conceptos_no_remunerativos = Replace(Format$(rs("conceptos_no_remunerativos_1").Value + _
                            //rs("conceptos_no_remunerativos_497").Value + _
                            //rs("conceptos_no_remunerativos_2").Value + rs("conceptos_no_remunerativos_3").Value, "000000.00"), ".", ",")

                            eSijcor.retroactividades = "000000,00";
                            if (!dr.IsDBNull(aguinaldo))
                            {
                                monto_aguinaldo = dr.GetDecimal(aguinaldo);
                                eSijcor.aguinaldo = string.Format("{0:000000.00}", dr.GetDecimal(aguinaldo)).Replace(".", ",");
                            }
                            else
                                eSijcor.aguinaldo = "000000,00";
                            eSijcor.secuencia_cuil = "01";
                            eSijcor.diferencia_x_jerarquia = "000000,00";
                            if (!dr.IsDBNull(importe_adherente_voluntario))
                                eSijcor.importe_adherente_voluntario = string.Format("{0:000000.00}", dr.GetDecimal(importe_adherente_voluntario)).Replace(".", ",");
                            else
                                eSijcor.importe_adherente_voluntario = "000000,00";
                        }
                        else
                        {
                            monto_aguinaldo += dr.GetDecimal(aguinaldo);
                            monto_no_remunerativo += dr.GetDecimal(conceptos_no_remunerativos_1);
                            eSijcor.conceptos_no_remunerativos =
                                string.Format("{0:000000.00}", monto_no_remunerativo).Replace(".", ",");
                            eSijcor.aguinaldo = string.Format("{0:000000.00}", monto_aguinaldo).Replace(".", ",");
                            lstSijcor.Add(eSijcor);
                            monto_no_remunerativo = 0;
                            monto_aguinaldo = 0;
                            eSijcor = new Entities.Sijcor();
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            return lstSijcor;
        }
        public static List<Entities.Sijcor> GetSijcorSinAguilucho(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int opcion)
        {
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            Entities.Sijcor eSijcor;
            StringBuilder strSQL = new StringBuilder();
            SqlCommand cmd;
            SqlConnection cn = DALBase.GetConnection("Siimva");

            strSQL.AppendLine("select e.legajo, cuil=REPLACE(e.cuil,'-',''), ");
            strSQL.AppendLine(" e.nombre, e.tarea, categoria = convert(varchar(2), c1.cod_categoria), ");
            strSQL.AppendLine(" e.cod_regimen_empleado, ");
            strSQL.AppendLine(" cod_modalidad_contratacion = ");
            strSQL.AppendLine(" CASE ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=1 THEN '0003' ");
            strSQL.AppendLine("  WHEN e.cod_clasif_per=2 THEN '0004' ");
            strSQL.AppendLine("  ELSE '0004' ");
            strSQL.AppendLine(" END, ");
            //
            strSQL.AppendLine(" cant_dias_trabajados= (SELECT isnull(SUM(unidades),0) ");
            strSQL.AppendLine("  FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine("  WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine("  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine("  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine("  dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine("  dlxe2.cod_concepto_liq BETWEEN 300 AND 310), ");
            //
            strSQL.AppendLine(" sueldo390 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390) AND dlxe2.cod_concepto_liq<>17 ), ");
            //
            strSQL.AppendLine(" aguinaldo390 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq=17 ), ");
            //ok
            //strSQL.AppendLine(" dlxe2.cod_concepto_liq in (10, 12, 17, 20, 50, 60, 63, 65, 66, 110, 120, 121, 149, 150, 151, 152, 157, 159, 160,");
            //strSQL.AppendLine(" 161, 162, 163, 164, 166, 170, 173, 175, 176, 300, 310, 499, 831, 174, 183, 496, 300, 162, 831)),");
            strSQL.AppendLine(" sueldo390BIS = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (390, 499)), ");//ok
            //--------------------------------------------------------------------------------//
            //'    strSQl = strSQl & " otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'    "  FROM DET_LIQ_X_EMPLEADO dlxe2  " & _
            //'    "  WHERE dlxe2.anio=dlxe.anio AND " & _
            //'    "  dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'    "  dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'    "  dlxe2.legajo=dlxe.legajo AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 16 and  19) AND " & _
            //'    " (dlxe2.cod_concepto_liq=65) AND " & _
            //'    " (dlxe2.cod_concepto_liq BETWEEN 153 and  155)), "
            //--------------------------------------------------------------------------------//

            strSQL.AppendLine(" otroscodigos = (SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2  ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq in (12,154)), ");
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos1 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND (dlxe2.cod_concepto_liq=62)),");
            //--------------------------------------------------------------------------------//
            //'strSQl = strSQl & " otroscodigos2 = (SELECT isnull(SUM(dlxe2.importe),0) " & _
            //'" FROM DET_LIQ_X_EMPLEADO dlxe2 " & _
            //'" WHERE dlxe2.anio=dlxe.anio AND " & _
            //'" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND " & _
            //'" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND " & _
            //'" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq=65), "
            //--------------------------------------------------------------------------------//
            //strSQL.AppendLine(" otroscodigos3 = (SELECT isnull(SUM(dlxe2.importe),0) ");
            //strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            //strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            //strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            //strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            //strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND dlxe2.cod_concepto_liq = 153 ), ");
            //////////////////////////////////////////////////////////////////////////////////////
            //' conceptos No remunerativos
            //////////////////////////////////////////////////////////////////////////////////////
            strSQL.AppendLine(" conceptos_no_remunerativos_1=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.suma <> 0 AND B.sujeto_a_desc = 0");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo and dlxe2.cod_concepto_liq not in (410,420,421,430,440,450,470,480,500,510,520,540,570,580,590)),");//ok
            //////////////////////////////////////////////////////////////////////////////////////                                                                                                                                                                                                                                                 //--------------------------------------------------------------------------------//
            strSQL.AppendLine(" aguinaldo=(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" JOIN CONCEPTOS_LIQUIDACION B on");
            strSQL.AppendLine(" B.cod_concepto_liq<>390 AND");
            strSQL.AppendLine(" B.cod_concepto_liq = dlxe2.cod_concepto_liq AND");
            strSQL.AppendLine(" B.sac=1");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq between 16 and 19), ");
            //
            strSQL.AppendLine(" valor_codigo_800 =(SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 800),  ");
            //
            strSQL.AppendLine(" importe_adherente_voluntario =(SELECT isnull(SUM(dlxe2.importe),0) ");
            strSQL.AppendLine(" FROM DET_LIQ_X_EMPLEADO dlxe2 ");
            strSQL.AppendLine(" WHERE dlxe2.anio=dlxe.anio AND ");
            strSQL.AppendLine(" dlxe2.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
            strSQL.AppendLine(" dlxe2.nro_liquidacion=dlxe.nro_liquidacion AND ");
            strSQL.AppendLine(" dlxe2.legajo=dlxe.legajo AND ");
            strSQL.AppendLine(" dlxe2.cod_concepto_liq=800), ");
            //
            strSQL.AppendLine(" valor_codigo_624 = (SELECT isnull(sum(valor_concepto_liq),0) ");
            strSQL.AppendLine(" FROM CONCEP_LIQUID_X_EMPLEADO clxe  ");
            strSQL.AppendLine(" Where clxe.legajo = dlxe.legajo And clxe.cod_concepto_liq = 624)  ");
            //
            //1 = byNroliquidacion
            //2 = byPeriodo
            if (opcion == 1)
            {
                strSQL.AppendLine(" FROM LIQ_X_EMPLEADO dlxe ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where ");
                strSQL.AppendLine(" dlxe.anio =@vAnio");
                strSQL.AppendLine(" AND dlxe.cod_tipo_liq = @vCod_tipo_liq");
                strSQL.AppendLine(" AND dlxe.nro_liquidacion =@vNro_liquidacion");
                strSQL.AppendLine(" ORDER BY dlxe.legajo ");
            }
            else
            {
                strSQL.AppendLine(" FROM LIQUIDACIONES L ");
                strSQL.AppendLine(" JOIN LIQ_X_EMPLEADO dlxe ON ");
                strSQL.AppendLine(" L.anio = dlxe.anio AND ");
                strSQL.AppendLine(" L.cod_tipo_liq=dlxe.cod_tipo_liq AND ");
                strSQL.AppendLine(" L.nro_liquidacion = dlxe.nro_liquidacion ");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON ");
                strSQL.AppendLine(" e.legajo = dlxe.legajo ");
                strSQL.AppendLine(" JOIN CATEGORIAS c1 on ");
                strSQL.AppendLine(" e.cod_categoria = c1.cod_categoria ");
                strSQL.AppendLine(" LEFT JOIN CARGOS c ON ");
                strSQL.AppendLine(" c.cod_cargo = e.cod_cargo ");
                strSQL.AppendLine(" Where L.anio =@vAnio");
                strSQL.AppendLine(" AND dlxe.cod_tipo_liq = @vCod_tipo_liq");
                strSQL.AppendLine(" AND L.periodo=@vPeriodo");
                strSQL.AppendLine(" ORDER BY dlxe.legajo ");
            }

            SqlDataReader dr;
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@vAnio", anio);
                //1 = byNroliquidacion
                //2 = byPeriodo
                if (opcion == 1)
                {
                    cmd.Parameters.AddWithValue("@vCod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@vNro_liquidacion", nro_liquidacion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@vCod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@vPeriodo", periodo);
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int cuil = dr.GetOrdinal("cuil");
                    int nombre = dr.GetOrdinal("nombre");
                    int cargo = dr.GetOrdinal("tarea");
                    int categoria = dr.GetOrdinal("categoria");
                    //int adherente_vol = 
                    //int adherente_obl =
                    //int cod_situacion =
                    //int cod_condicion =
                    int cod_actividad = dr.GetOrdinal("cod_regimen_empleado");
                    int afiliado = 0;
                    int valor_codigo_624 = dr.GetOrdinal("valor_codigo_624");
                    int valor_codigo_800 = dr.GetOrdinal("valor_codigo_800");
                    int cod_modalidad_contratacion = dr.GetOrdinal("cod_modalidad_contratacion");
                    //int cod_siniestro =
                    //int cod_departamento =
                    //int cod_delegacion =
                    //int cod_obra_social =
                    //int cod_situacion_1er_tramo =
                    //int cant_dias_1er_tramo =
                    //int cod_situacion_2do_tramo =
                    //int cant_dias_2do_tramo =
                    //int cod_situacion_3er_tramo =
                    //int cant_dias_3er_tramo =
                    int cant_dias_trabajados = dr.GetOrdinal("cant_dias_trabajados");
                    int sueldo390 = dr.GetOrdinal("sueldo390");
                    int aguinaldo390 = dr.GetOrdinal("aguinaldo390");
                    int otroscodigos = dr.GetOrdinal("otroscodigos");
                    //int otroscodigos1 = dr.GetOrdinal("otroscodigos1");
                    int otroscodigos3 = 0; //dr.GetOrdinal("otroscodigos3");
                                           //int importe_hs_extra =
                                           //int zona_desfavorable =
                    int conceptos_no_remunerativos_1 = dr.GetOrdinal("conceptos_no_remunerativos_1");
                    //int conceptos_no_remunerativos_497 = 0; //dr.GetOrdinal("conceptos_no_remunerativos_497");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_2 = dr.GetOrdinal("conceptos_no_remunerativos_2");
                    ////////////////////////////////////////////////////////////////////////////////
                    //int conceptos_no_remunerativos_3 = 0;// dr.GetOrdinal("conceptos_no_remunerativos_3");

                    //int retroactividades =
                    int aguinaldo = dr.GetOrdinal("aguinaldo");
                    //int remuneracion_2 =
                    //int tipo_adicional_seg_vida =
                    //15/04/2013
                    // Se agregaron 3 campos nuevos
                    //int secuencia_cuil =
                    //int diferencia_x_jerarquia =
                    int importe_adherente_voluntario = dr.GetOrdinal("importe_adherente_voluntario");
                    //
                    decimal sueldo = 0;
                    decimal dias = 0;

                    while (dr.Read())
                    {
                        eSijcor = new Entities.Sijcor();
                        //
                        if (!dr.IsDBNull(cuil))
                            eSijcor.cuil = dr.GetString(cuil).Substring(0, 11);
                        if (!dr.IsDBNull(nombre))
                            eSijcor.apeynom = dr.GetString(nombre).ToUpper().PadRight(30).Substring(0, 30);
                        if (!dr.IsDBNull(cargo))
                            eSijcor.cargo = dr.GetString(cargo).Trim().ToUpper().PadRight(30).Substring(0, 30);
                        if (!dr.IsDBNull(categoria))
                            eSijcor.categoria = dr.GetString(categoria).PadRight(30);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (!dr.IsDBNull(cod_actividad))
                            eSijcor.cod_actividad = string.Format("{0:0000}", dr.GetInt32(cod_actividad));//.PadLeft(4, '0');
                        if (!dr.IsDBNull(valor_codigo_800))
                            eSijcor.adherente_vol = string.Format("{0:00}", dr.GetDecimal(valor_codigo_800)).Replace(".", "");//.PadLeft(2, '0');
                        if (!dr.IsDBNull(valor_codigo_624) && !dr.IsDBNull(valor_codigo_800))
                        {
                            afiliado = Convert.ToInt32(dr.GetDecimal(valor_codigo_624) - 1 - dr.GetDecimal(valor_codigo_800));
                            if (afiliado < 0)
                                eSijcor.adherente_obl = "00";
                            else
                                eSijcor.adherente_obl = string.Format("{0:00}", afiliado.ToString());//.PadLeft(2, '0');
                        }
                        else
                            eSijcor.adherente_obl = "00";
                        if (!dr.IsDBNull(cod_modalidad_contratacion))
                            eSijcor.cod_modalidad_contratacion = dr.GetString(cod_modalidad_contratacion);
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (!dr.IsDBNull(cant_dias_trabajados))
                        {
                            dias = 30 - dr.GetDecimal(cant_dias_trabajados);
                            eSijcor.cant_dias_trabajados = string.Format("{0:00}", dias);
                        }
                        if (!dr.IsDBNull(sueldo390))
                        {
                            sueldo = dr.GetDecimal(sueldo390) - dr.GetDecimal(aguinaldo390);
                            //3/5/2019 no debe sumar el codigo 12 & 154 pq esta incluido el 390
                            //sueldo = dr.GetDecimal(sueldo390) + dr.GetDecimal(otroscodigos);
                            eSijcor.sueldo = string.Format("{0:000000.00}", sueldo).Replace(".", ",");
                        }
                        else
                            eSijcor.sueldo = "000000,00";

                        if (!dr.IsDBNull(conceptos_no_remunerativos_1))
                            eSijcor.conceptos_no_remunerativos = string.Format("{0:000000.00}", dr.GetDecimal(conceptos_no_remunerativos_1)).Replace(".", ",");
                        //2019
                        //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_2)).Replace(".", ",");
                        //dr.GetDecimal(conceptos_no_remunerativos_1) + dr.GetDecimal(conceptos_no_remunerativos_497) +
                        //dr.GetDecimal(conceptos_no_remunerativos_2) + dr.GetDecimal(conceptos_no_remunerativos_3)).Replace(".", ",");
                        else
                            eSijcor.conceptos_no_remunerativos = "000000,00";

                        //.conceptos_no_remunerativos = Replace(Format$(rs("conceptos_no_remunerativos_1").Value + _
                        //rs("conceptos_no_remunerativos_497").Value + _
                        //rs("conceptos_no_remunerativos_2").Value + rs("conceptos_no_remunerativos_3").Value, "000000.00"), ".", ",")

                        eSijcor.retroactividades = "000000,00";
                        if (!dr.IsDBNull(aguinaldo))
                            eSijcor.aguinaldo = string.Format("{0:000000.00}", dr.GetDecimal(aguinaldo)).Replace(".", ",");
                        else
                            eSijcor.aguinaldo = "000000,00";
                        eSijcor.secuencia_cuil = "01";
                        eSijcor.diferencia_x_jerarquia = "000000,00";
                        if (!dr.IsDBNull(importe_adherente_voluntario))
                            eSijcor.importe_adherente_voluntario = string.Format("{0:000000.00}", dr.GetDecimal(importe_adherente_voluntario)).Replace(".", ",");
                        else
                            eSijcor.importe_adherente_voluntario = "000000,00";
                        lstSijcor.Add(eSijcor);
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            return lstSijcor;
        }

        public static string[] GenerarArchivo(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, string carpeta)
        {
            //string campo = "";
            //string strField = "";
            //string strRow = "";
            string[] archivo = null;

            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            //FileStream stream = new FileStream(@"c:\prueba_archivo.txt", FileMode.Create, FileAccess.ReadWrite);
            //BinaryFormatter b = new BinaryFormatter();

            try
            {
                lstSijcor = GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                archivo = new string[lstSijcor.Count];

                for (int i = 0; i < lstSijcor.Count; i++)
                {
                    archivo[i] = lstSijcor[i].cuil + lstSijcor[i].apeynom + lstSijcor[i].cargo + lstSijcor[i].categoria + lstSijcor[i].adherente_vol +
                      lstSijcor[i].adherente_obl + lstSijcor[i].cod_situacion + lstSijcor[i].cod_condicion + lstSijcor[i].cod_actividad + lstSijcor[i].cod_modalidad_contratacion +
                      lstSijcor[i].cod_siniestro + lstSijcor[i].cod_departamento + lstSijcor[i].cod_delegacion + lstSijcor[i].cod_obra_social + lstSijcor[i].cod_situacion_1er_tramo +
                      lstSijcor[i].cant_dias_1er_tramo + lstSijcor[i].cod_situacion_2do_tramo + lstSijcor[i].cant_dias_2do_tramo + lstSijcor[i].cod_situacion_3er_tramo +
                      lstSijcor[i].cant_dias_3er_tramo + lstSijcor[i].cant_dias_trabajados + lstSijcor[i].sueldo + lstSijcor[i].importe_hs_extra + lstSijcor[i].conceptos_no_remunerativos +
                      lstSijcor[i].zona_desfavorable + lstSijcor[i].retroactividades + lstSijcor[i].aguinaldo + lstSijcor[i].remuneracion_2 + lstSijcor[i].tipo_adicional_seg_vida +
                      lstSijcor[i].secuencia_cuil + lstSijcor[i].diferencia_x_jerarquia + lstSijcor[i].importe_adherente_voluntario;
                }
                //File. WriteAllLines(@"d:\prueba.txt", archivo);
                return archivo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void GuardarArchivo(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, string carpeta)
        {

            //string campo = "";
            //string strField = "";
            //string strRow = "";
            string[] archivo = null;

            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            FileStream stream = new FileStream(@"c:\prueba_archivo.txt", FileMode.Create, FileAccess.ReadWrite);
            //BinaryFormatter b = new BinaryFormatter();

            try
            {
                lstSijcor = GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                archivo = new string[lstSijcor.Count];

                for (int i = 0; i < lstSijcor.Count; i++)
                {
                    archivo[i] = lstSijcor[i].cuil + lstSijcor[i].apeynom + lstSijcor[i].cargo + lstSijcor[i].categoria + lstSijcor[i].adherente_vol +
                      lstSijcor[i].adherente_obl + lstSijcor[i].cod_situacion + lstSijcor[i].cod_condicion + lstSijcor[i].cod_actividad + lstSijcor[i].cod_modalidad_contratacion +
                      lstSijcor[i].cod_siniestro + lstSijcor[i].cod_departamento + lstSijcor[i].cod_delegacion + lstSijcor[i].cod_obra_social + lstSijcor[i].cod_situacion_1er_tramo +
                      lstSijcor[i].cant_dias_1er_tramo + lstSijcor[i].cod_situacion_2do_tramo + lstSijcor[i].cant_dias_2do_tramo + lstSijcor[i].cod_situacion_3er_tramo +
                      lstSijcor[i].cant_dias_3er_tramo + lstSijcor[i].cant_dias_trabajados + lstSijcor[i].sueldo + lstSijcor[i].importe_hs_extra + lstSijcor[i].conceptos_no_remunerativos +
                      lstSijcor[i].zona_desfavorable + lstSijcor[i].retroactividades + lstSijcor[i].aguinaldo + lstSijcor[i].remuneracion_2 + lstSijcor[i].tipo_adicional_seg_vida +
                      lstSijcor[i].secuencia_cuil + lstSijcor[i].diferencia_x_jerarquia + lstSijcor[i].importe_adherente_voluntario;
                }

                File.WriteAllLines(@"d:\prueba.txt", archivo);
                //b.Serialize(stream, lstSijcor, null);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
