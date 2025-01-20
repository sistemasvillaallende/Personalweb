using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LiquidacionD
    {
        //
        private static List<Entities.Liquidacion> getLstLiquidacion(int anio, int cod_tipo_liq, int nro_liquidacion,
          SqlConnection cn, SqlTransaction trx)
        {

            List<Entities.Liquidacion> lst = new List<Entities.Liquidacion>();
            Entities.Liquidacion eLiq;//= new Entities.Liquidacion();
            StringBuilder strSQL = new StringBuilder();
            SqlCommand cmd = null;
            //SqlConnection cn = DALBase.GetConnection("SIIMVA");

            strSQL.AppendLine(" SELECT * FROM LIQUIDACIONES (NOLOCK) ");
            strSQL.AppendLine(" WHERE anio=@anio");
            strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");

            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Transaction = trx;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int anio1 = dr.GetOrdinal("anio");
                    int cod_tipo_liq1 = dr.GetOrdinal("cod_tipo_liq");
                    int nro_liquidacion1 = dr.GetOrdinal("nro_liquidacion");
                    int fecha_liquidacion = dr.GetOrdinal("fecha_liquidacion");

                    while (dr.Read())
                    {
                        eLiq = new Entities.Liquidacion();

                        if (!dr.IsDBNull(anio1)) eLiq.anio = dr.GetInt32(anio1);
                        if (!dr.IsDBNull(cod_tipo_liq1)) eLiq.cod_tipo_liq = dr.GetInt32(cod_tipo_liq1);
                        if (!dr.IsDBNull(nro_liquidacion1)) eLiq.nro_liquidacion = dr.GetInt32(nro_liquidacion1);
                        //if (!dr.IsDBNull(fecha_liquidacion)) eLiq.fecha_liquidacion = dr.GetString(fecha_liquidacion);
                        lst.Add(eLiq);
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; }
            return lst;
        }
        //
        public static void Calcula_Sueldo(int anio, int cod_tipo_liq, int nro_liquidacion, string fecha_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            int nro_orden_liq = 0;
            decimal sneto = 0;
            decimal sasd = 0;
            decimal aux_valor_390 = 0;
            decimal no_remunerativo = 0;
            decimal redondeo = 0;
            int nro_orden = 0;
            decimal aux_sac = 0;
            decimal sueldo_basico = 0;
            decimal dias_trabajados = 0;
            decimal aux_basico = 0;


            StringBuilder strSQL = new StringBuilder();
            List<Entities.Liquidacion> lstLiq = new List<Entities.Liquidacion>();
            lstLiq = getLstLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            List<Entities.LstEmpleados> lstEmp = new List<Entities.LstEmpleados>();
            lstEmp = EmpleadoD.GetLiqEmpleado(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            //Preparo las estructuras colecciones de:
            List<Entities.Det_Liq_x_empleado> lstDetalle = new List<Entities.Det_Liq_x_empleado>();
            Entities.Det_Liq_x_empleado eDetalle;
            List<Entities.Liq_x_Empleado> lstLiq_x_Emp = new List<Entities.Liq_x_Empleado>();
            Entities.Liq_x_Empleado eLiq_x_Emp;
            List<Entities.ConceptoLiqxEmp> lstConceptoLiqxEmp = new List<Entities.ConceptoLiqxEmp>();
            //Set RSAportes = objGral.ObtenerEstructura("APORTES_LIQ_X_EMPLEADO", "anio")
            Entities.Resultado oResultado;
            //24/07/2024
            List<int> codigosnorem = new List<int>(new[] { 17, 18, 167, 481, 486, 487, 489, 490, 491, 499, 493, 494, 499 });
            //Recorro empleados del tipo de liquidación que corresponda
            //y que no estén dados de baja
            //lstEmp = lstEmp.FindAll(a => a.legajo == 814);
            for (int i = 0; i < lstEmp.Count; i++)
            {
                sneto = lstEmp[i].sueldo_basico;
                sasd = lstEmp[i].sueldo_basico;
                //
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                eDetalle.cod_concepto_liq = 10;
                eDetalle.nro_orden = 1;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Now.ToShortDateString();
                eDetalle.importe = lstEmp[i].sueldo_basico;
                eDetalle.unidades = 1;
                aux_valor_390 = lstEmp[i].sueldo_basico;
                lstDetalle.Add(eDetalle);
                nro_orden = 2;
                //
                aux_sac = 0;
                no_remunerativo = 0;
                /////////////////////////////////////////////////////////////////////////////////////
                //Tengo en cuenta en primera instancia los conceptos
                //que suman y sujetos a descuento
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_1(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    if (item.cod_concepto_liq != 498)
                    {
                        oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                          anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, item.valor_concepto_liq, cn, trx);
                        if (oResultado.resultado_1 > 0)
                        {
                            eDetalle = new Entities.Det_Liq_x_empleado();
                            eDetalle.anio = anio;
                            eDetalle.cod_tipo_liq = cod_tipo_liq;
                            eDetalle.nro_liquidacion = nro_liquidacion;
                            eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                            eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                            eDetalle.nro_orden = nro_orden;
                            eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                            eDetalle.importe = oResultado.resultado_1;
                            eDetalle.unidades = oResultado.resultado_2;
                            lstDetalle.Add(eDetalle);
                            //Acumulo aux_valor_390
                            //Siempre y cuando el cod sea <= 390
                            //if (eDetalle.cod_concepto_liq <= 390)
                            //{
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            //}
                            sneto = sneto + eDetalle.importe;
                            sasd = sasd + eDetalle.importe;
                            nro_orden = nro_orden + 1;
                        }
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;

                }
                //
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_2(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString();
                        //En el caso que los codigos de conceptos
                        //sean 300 o 310.
                        //Estos se informan como negativos
                        //y los multiplico -1
                        if (item.cod_concepto_liq == 300 || item.cod_concepto_liq == 310 || item.cod_concepto_liq == 311 || item.cod_concepto_liq == 11)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            lstEmp[i].sueldo_bruto += eDetalle.importe;
                        }
                        //27/12/2022
                        //si pago el sac en el sueldo
                        //para calcular concepto no remunerativos
                        //debo restarlo al sueldo bruto
                        if (eDetalle.cod_concepto_liq == 17 || eDetalle.cod_concepto_liq == 18)
                            aux_sac += eDetalle.importe;
                        sneto = sneto + eDetalle.importe;
                        sasd = sasd + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390 - aux_sac;
                }
                //22/12/2022
                //Ahora tengo en cuenta los conceptos fijos que suman y que
                //no están sujetos a descuentos
                //ACA esta
                //410,500,470
                //491, 499 no remunerativo
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_3(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq, anio, cod_tipo_liq, nro_liquidacion,
                      fecha_liquidacion,
                      item.valor_concepto_liq, cn, trx);

                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        // Acumulo aux_valor_390
                        // Siempre y cuando el cod sea <= 390

                        if (item.cod_concepto_liq <= 390)
                        {
                            ////Sumo todos los codigo <=390
                            ////y excluyo los que estan en el array
                            List<int> codigos = new List<int>(new[] { 153, 155, 158, 164, 167 });
                            if (!codigos.Exists(p => p.Equals(item.cod_concepto_liq)))
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            //25/10/2022
                            //Cambio todo lo re arriba por esta pregunta
                        }
                        //23/12/2022
                        ////////////////////////////////////////////////////////////////////////////////////////
                        ////Sumo todos los codigo no remunerativos                       
                        if (codigosnorem.Exists(p => p.Equals(item.cod_concepto_liq)))
                        {
                            no_remunerativo += eDetalle.importe;
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //
                //Ahora tengo en cuenta los conceptos variables que suman y
                //no son sujetos a descuento
                //490 no remunerativo
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_4(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");

                        if (item.cod_concepto_liq == 481)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        //eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        //if (eDetalle.cod_concepto_liq != 68 || eDetalle.cod_concepto_liq != 69 && eDetalle.cod_concepto_liq <= 390)
                        //{
                        //  aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        //}
                        //Este If lo comento pq no debe acumular el 390 
                        ////////////////////////////////////////////////////////////////////////////////////////
                        // 23/12/2022
                        ////////////////////////////////////////////////////////////////////////////////////////
                        ////Sumo todos los codigo no remunerativos                       
                        if (codigosnorem.Exists(p => p.Equals(item.cod_concepto_liq)))
                        {
                            no_remunerativo += eDetalle.importe;
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                }

                //Ahora tengo en cuenta los conceptos fijos que restan
                //(descuentos/retenciones)
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_5(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        if (item.cod_concepto_liq != 498)
                        {
                            eDetalle = new Entities.Det_Liq_x_empleado();
                            eDetalle.anio = anio;
                            eDetalle.cod_tipo_liq = cod_tipo_liq;
                            eDetalle.nro_liquidacion = nro_liquidacion;
                            eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                            eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                            eDetalle.nro_orden = nro_orden;
                            eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                            eDetalle.importe = oResultado.resultado_1;
                            eDetalle.unidades = oResultado.resultado_2;
                            lstDetalle.Add(eDetalle);
                            //Acumulo aux_valor_390
                            //Siempre y cuando el cod sea <= 390
                            if (eDetalle.cod_concepto_liq <= 390)
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            sneto = sneto - eDetalle.importe;
                            nro_orden = nro_orden + 1;
                        }
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_6(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString();
                        //DateTime.Today.ToString("dd/MM/yyyy");
                        //En el caso que los codigos de conceptos
                        //sean 4811
                        //Estos se informan como negativos
                        //y los multiplico -1
                        //if (item.cod_concepto_liq == 481)
                        //    eDetalle.importe = oResultado.resultado_1 * -1;
                        //else
                        //    eDetalle.importe = oResultado.resultado_1;
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        }
                        sneto = sneto - eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //23/12/2022
                //Ahora tengo en cuenta los conceptos fijos que suman y que
                //no están sujetos a descuentos
                //ACA le calcula el salario flia
                //410,470,500
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_Asignacion_fliar(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], lstEmp[i].sueldo_bruto + no_remunerativo, item.cod_concepto_liq, anio, cod_tipo_liq, nro_liquidacion,
                      fecha_liquidacion,
                      item.valor_concepto_liq, cn, trx);

                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        // Acumulo aux_valor_390
                        // Siempre y cuando el cod sea <= 390

                        if (item.cod_concepto_liq <= 390)
                        {
                            ////Sumo todos los codigo <=390
                            ////y excluyo los que estan en el array
                            List<int> codigos = new List<int>(new[] { 153, 155, 158, 164, 167 });
                            if (!codigos.Exists(p => p.Equals(item.cod_concepto_liq)))
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            //25/10/2022
                            //Cambio todo lo re arriba por esta pregunta
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Verifico si el sueldo neto tiene decimales, si los tiene
                //se los quito y coloco un redondeo negativo
                //redondeo = Round(sneto - Round(sneto), 2)
                redondeo = decimal.Round(sneto - decimal.Round(sneto), 2);
                sneto = decimal.Round(sneto);
                //Si el redondeo es distinto que cero grabo el mismo en el detalle
                if (redondeo != 0)
                {
                    eDetalle = new Entities.Det_Liq_x_empleado();
                    eDetalle.anio = anio;
                    eDetalle.cod_tipo_liq = cod_tipo_liq;
                    eDetalle.nro_liquidacion = nro_liquidacion;
                    eDetalle.legajo = lstEmp[i].legajo;
                    eDetalle.cod_concepto_liq = 885;
                    eDetalle.nro_orden = nro_orden;
                    eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                    eDetalle.importe = redondeo;
                    eDetalle.unidades = 1;
                    lstDetalle.Add(eDetalle);
                    nro_orden = nro_orden + 1;
                }
                //Ahora inserto el importe
                //acumulado de los cod de conceptos <= 390
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = lstEmp[i].legajo;
                eDetalle.cod_concepto_liq = 390;
                eDetalle.nro_orden = nro_orden;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eDetalle.importe = aux_valor_390;
                eDetalle.unidades = 1;
                lstDetalle.Add(eDetalle);
                nro_orden = nro_orden + 1;

                //Ahora cargo el sueldo neto calculado para el legajo
                //en cuestión
                nro_orden_liq = nro_orden_liq + 1;
                eLiq_x_Emp = new Entities.Liq_x_Empleado();
                eLiq_x_Emp.anio = anio;
                eLiq_x_Emp.cod_tipo_liq = cod_tipo_liq;
                eLiq_x_Emp.nro_liquidacion = nro_liquidacion;
                eLiq_x_Emp.legajo = lstEmp[i].legajo;
                eLiq_x_Emp.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eLiq_x_Emp.cod_categoria = lstEmp[i].cod_categoria;
                eLiq_x_Emp.cod_cargo = lstEmp[i].cod_cargo;
                eLiq_x_Emp.tarea = lstEmp[i].tarea;
                eLiq_x_Emp.nro_cta_sb = lstEmp[i].nro_cta_sb;
                eLiq_x_Emp.sueldo_basico = lstEmp[i].sueldo_basico;
                eLiq_x_Emp.sueldo_neto = sneto;
                eLiq_x_Emp.sueldo_bruto = lstEmp[i].sueldo_bruto;
                eLiq_x_Emp.no_remunerativo = no_remunerativo;
                eLiq_x_Emp.cod_clasif_per = lstEmp[i].cod_clasif_per;
                eLiq_x_Emp.nro_orden = nro_orden_liq;
                lstLiq_x_Emp.Add(eLiq_x_Emp);
            }
            //'Ahora hago los updatebatch
            try
            {
                Liq_x_EmpleadoD.SaveDet_Liq_x_Empleado(lstDetalle, cn, trx);
                Liq_x_EmpleadoD.SaveLiq_x_Empleado(lstLiq_x_Emp, cn, trx);
                //Actualizo la Liquidaciones
                //...                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Calcula_SueldoxdiasTrabajados(int anio, int cod_tipo_liq, int nro_liquidacion, string fecha_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            int nro_orden_liq = 0;
            decimal sneto = 0;
            decimal sasd = 0;
            decimal aux_valor_390 = 0;
            decimal no_remunerativo = 0;
            decimal redondeo = 0;
            int nro_orden = 0;
            decimal aux_sac = 0;
            decimal sueldo_basico = 0;
            decimal dias_trabajados = 0;
            decimal aux_basico = 0;


            StringBuilder strSQL = new StringBuilder();
            List<Entities.Liquidacion> lstLiq = new List<Entities.Liquidacion>();
            lstLiq = getLstLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            List<Entities.LstEmpleados> lstEmp = new List<Entities.LstEmpleados>();
            lstEmp = EmpleadoD.GetLiqEmpleado(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            //Preparo las estructuras colecciones de:
            List<Entities.Det_Liq_x_empleado> lstDetalle = new List<Entities.Det_Liq_x_empleado>();
            Entities.Det_Liq_x_empleado eDetalle;
            List<Entities.Liq_x_Empleado> lstLiq_x_Emp = new List<Entities.Liq_x_Empleado>();
            Entities.Liq_x_Empleado eLiq_x_Emp;
            List<Entities.ConceptoLiqxEmp> lstConceptoLiqxEmp = new List<Entities.ConceptoLiqxEmp>();
            //Set RSAportes = objGral.ObtenerEstructura("APORTES_LIQ_X_EMPLEADO", "anio")
            Entities.Resultado oResultado;
            //24/07/2024
            List<int> codigosnorem = new List<int>(new[] { 17, 18, 151, 167, 481, 486, 487, 489, 490, 491, 493, 494, 499 });
            //Recorro empleados del tipo de liquidación que corresponda
            //y que no estén dados de baja
            //lstEmp = lstEmp.FindAll(a => a.legajo == 814);
            for (int i = 0; i < lstEmp.Count; i++)
            {
                //sneto = lstEmp[i].sueldo_basico;
                //sasd = lstEmp[i].sueldo_basico;
                //
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                eDetalle.cod_concepto_liq = 10;
                eDetalle.nro_orden = 1;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Now.ToShortDateString();
                //Busco la cantidad de dias Trabajados para cada empleado
                dias_trabajados = Conceptos_liqD.Buscar_dias_trabajados(lstEmp[i], 10, anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                aux_basico = lstEmp[i].sueldo_basico;
                if (dias_trabajados != 30)
                {
                    sueldo_basico = lstEmp[i].sueldo_basico / 30;
                    sueldo_basico = Decimal.Round(sueldo_basico * dias_trabajados, 2);
                    eDetalle.importe = sueldo_basico;
                    eDetalle.unidades = dias_trabajados;
                    aux_valor_390 = sueldo_basico;
                    lstEmp[i].sueldo_bruto = 0;
                    lstEmp[i].sueldo_basico = sueldo_basico;
                    lstEmp[i].dias_trabajados = dias_trabajados;
                    lstEmp[i].hs_trabajados = 0;
                    lstDetalle.Add(eDetalle);
                    sneto = sueldo_basico;
                    sasd = sueldo_basico;
                }
                else
                {
                    eDetalle.importe = lstEmp[i].sueldo_basico;
                    eDetalle.unidades = dias_trabajados;
                    aux_valor_390 = lstEmp[i].sueldo_basico;
                    lstEmp[i].sueldo_bruto = 0;
                    lstDetalle.Add(eDetalle);
                    sneto = lstEmp[i].sueldo_basico;
                    sasd = lstEmp[i].sueldo_basico;
                    lstEmp[i].dias_trabajados = dias_trabajados;
                    lstEmp[i].hs_trabajados = 0;
                }
                //
                nro_orden = 2;
                aux_sac = 0;
                no_remunerativo = 0;
                /////////////////////////////////////////////////////////////////////////////////////
                //Tengo en cuenta en primera instancia los conceptos
                //que suman y sujetos a descuento
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_1(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    if (item.cod_concepto_liq != 498)
                    {
                        oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                          anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, item.valor_concepto_liq, cn, trx);
                        if (oResultado.resultado_1 > 0)
                        {
                            eDetalle = new Entities.Det_Liq_x_empleado();
                            eDetalle.anio = anio;
                            eDetalle.cod_tipo_liq = cod_tipo_liq;
                            eDetalle.nro_liquidacion = nro_liquidacion;
                            eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                            eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                            eDetalle.nro_orden = nro_orden;
                            eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                            eDetalle.importe = oResultado.resultado_1;
                            eDetalle.unidades = oResultado.resultado_2;
                            lstDetalle.Add(eDetalle);
                            //Acumulo aux_valor_390
                            //Siempre y cuando el cod sea <= 390
                            //if (eDetalle.cod_concepto_liq <= 390)
                            //{
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            //}
                            sneto = sneto + eDetalle.importe;
                            sasd = sasd + eDetalle.importe;
                            nro_orden = nro_orden + 1;
                        }
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;

                }
                //
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_2(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString();
                        //En el caso que los codigos de conceptos
                        //sean 300 o 310.
                        //Estos se informan como negativos
                        //y los multiplico -1
                        if (item.cod_concepto_liq == 300 || item.cod_concepto_liq == 310 || item.cod_concepto_liq == 311 || item.cod_concepto_liq == 11)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            lstEmp[i].sueldo_bruto += eDetalle.importe;
                        }
                        //27/12/2022
                        //si pago el sac en el sueldo
                        //para calcular concepto no remunerativos
                        //debo restarlo al sueldo bruto
                        if (eDetalle.cod_concepto_liq == 17 || eDetalle.cod_concepto_liq == 18)
                            aux_sac += eDetalle.importe;
                        sneto = sneto + eDetalle.importe;
                        sasd = sasd + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390 - aux_sac;
                }
                //22/12/2022
                //Ahora tengo en cuenta los conceptos fijos que suman y que
                //no están sujetos a descuentos
                //ACA esta
                //410,500,470
                //491 no remunerativo
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_3(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq, anio, cod_tipo_liq, nro_liquidacion,
                      fecha_liquidacion,
                      item.valor_concepto_liq, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        // Acumulo aux_valor_390
                        // Siempre y cuando el cod sea <= 390
                        if (item.cod_concepto_liq <= 390)
                        {
                            ////Sumo todos los codigo <=390
                            ////y excluyo los que estan en el array
                            List<int> codigos = new List<int>(new[] { 151, 153, 155, 158, 164, 167 });
                            if (!codigos.Exists(p => p.Equals(item.cod_concepto_liq)))
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            //25/10/2022
                            //Cambio todo lo re arriba por esta pregunta
                        }
                        //23/12/2022
                        ////////////////////////////////////////////////////////////////////////////////////////
                        ////Sumo todos los codigo no remunerativos                       
                        if (codigosnorem.Exists(p => p.Equals(item.cod_concepto_liq)))
                        {
                            no_remunerativo += eDetalle.importe;
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //
                //Ahora tengo en cuenta los conceptos variables que suman y
                //no son sujetos a descuento
                //490 no remunerativo
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_4(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");

                        if (item.cod_concepto_liq == 481)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        //eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        //if (eDetalle.cod_concepto_liq != 68 || eDetalle.cod_concepto_liq != 69 && eDetalle.cod_concepto_liq <= 390)
                        //{
                        //  aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        //}
                        //Este If lo comento pq no debe acumular el 390 
                        ////////////////////////////////////////////////////////////////////////////////////////
                        // 23/12/2022
                        ////////////////////////////////////////////////////////////////////////////////////////
                        ////Sumo todos los codigo no remunerativos                       
                        if (codigosnorem.Exists(p => p.Equals(item.cod_concepto_liq)))
                        {
                            no_remunerativo += eDetalle.importe;
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                }

                //Ahora tengo en cuenta los conceptos fijos que restan
                //(descuentos/retenciones)
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_5(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        if (item.cod_concepto_liq != 498)
                        {
                            eDetalle = new Entities.Det_Liq_x_empleado();
                            eDetalle.anio = anio;
                            eDetalle.cod_tipo_liq = cod_tipo_liq;
                            eDetalle.nro_liquidacion = nro_liquidacion;
                            eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                            eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                            eDetalle.nro_orden = nro_orden;
                            eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                            eDetalle.importe = oResultado.resultado_1;
                            eDetalle.unidades = oResultado.resultado_2;
                            lstDetalle.Add(eDetalle);
                            //Acumulo aux_valor_390
                            //Siempre y cuando el cod sea <= 390
                            if (eDetalle.cod_concepto_liq <= 390)
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            sneto = sneto - eDetalle.importe;
                            nro_orden = nro_orden + 1;
                        }
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_6(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], sasd, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString();
                        //DateTime.Today.ToString("dd/MM/yyyy");
                        //En el caso que los codigos de conceptos
                        //sean 4811
                        //Estos se informan como negativos
                        //y los multiplico -1
                        //if (item.cod_concepto_liq == 481)
                        //    eDetalle.importe = oResultado.resultado_1 * -1;
                        //else
                        //    eDetalle.importe = oResultado.resultado_1;
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        }
                        sneto = sneto - eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //23/12/2022
                //Ahora tengo en cuenta los conceptos fijos que suman y que
                //no están sujetos a descuentos
                //ACA le calcula el salario flia
                //410,470,500
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_Asignacion_fliar(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], lstEmp[i].sueldo_bruto + no_remunerativo, item.cod_concepto_liq, anio, cod_tipo_liq, nro_liquidacion,
                      fecha_liquidacion, item.valor_concepto_liq, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        // Acumulo aux_valor_390
                        // Siempre y cuando el cod sea <= 390

                        if (item.cod_concepto_liq <= 390)
                        {
                            ////Sumo todos los codigo <=390
                            ////y excluyo los que estan en el array
                            List<int> codigos = new List<int>(new[] { 153, 155, 158, 164, 167 });
                            if (!codigos.Exists(p => p.Equals(item.cod_concepto_liq)))
                            {
                                aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            }
                            //25/10/2022
                            //Cambio todo lo re arriba por esta pregunta
                        }
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Verifico si el sueldo neto tiene decimales, si los tiene
                //se los quito y coloco un redondeo negativo
                //redondeo = Round(sneto - Round(sneto), 2)
                redondeo = decimal.Round(sneto - decimal.Round(sneto), 2);
                sneto = decimal.Round(sneto);
                //Si el redondeo es distinto que cero grabo el mismo en el detalle
                if (redondeo != 0)
                {
                    eDetalle = new Entities.Det_Liq_x_empleado();
                    eDetalle.anio = anio;
                    eDetalle.cod_tipo_liq = cod_tipo_liq;
                    eDetalle.nro_liquidacion = nro_liquidacion;
                    eDetalle.legajo = lstEmp[i].legajo;
                    eDetalle.cod_concepto_liq = 885;
                    eDetalle.nro_orden = nro_orden;
                    eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                    eDetalle.importe = redondeo;
                    eDetalle.unidades = 1;
                    lstDetalle.Add(eDetalle);
                    nro_orden = nro_orden + 1;
                }
                //Ahora inserto el importe
                //acumulado de los cod de conceptos <= 390
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = lstEmp[i].legajo;
                eDetalle.cod_concepto_liq = 390;
                eDetalle.nro_orden = nro_orden;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eDetalle.importe = aux_valor_390;
                eDetalle.unidades = 1;
                lstDetalle.Add(eDetalle);
                nro_orden = nro_orden + 1;

                //Ahora cargo el sueldo neto calculado para el legajo
                //en cuestión
                //if (lstEmp[i].legajo == 658)
                //    lstEmp[i].legajo = 658;
                nro_orden_liq = nro_orden_liq + 1;
                eLiq_x_Emp = new Entities.Liq_x_Empleado();
                eLiq_x_Emp.anio = anio;
                eLiq_x_Emp.cod_tipo_liq = cod_tipo_liq;
                eLiq_x_Emp.nro_liquidacion = nro_liquidacion;
                eLiq_x_Emp.legajo = lstEmp[i].legajo;
                eLiq_x_Emp.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eLiq_x_Emp.cod_categoria = lstEmp[i].cod_categoria;
                eLiq_x_Emp.cod_cargo = lstEmp[i].cod_cargo;
                eLiq_x_Emp.tarea = lstEmp[i].tarea;
                eLiq_x_Emp.nro_cta_sb = lstEmp[i].nro_cta_sb;
                eLiq_x_Emp.sueldo_basico = aux_basico;//lstEmp[i].sueldo_basico;
                eLiq_x_Emp.sueldo_neto = sneto;
                eLiq_x_Emp.sueldo_bruto = lstEmp[i].sueldo_bruto;
                eLiq_x_Emp.no_remunerativo = no_remunerativo;
                eLiq_x_Emp.cod_clasif_per = lstEmp[i].cod_clasif_per;
                eLiq_x_Emp.nro_orden = nro_orden_liq;
                lstLiq_x_Emp.Add(eLiq_x_Emp);
            }
            //'Ahora hago los updatebatch
            try
            {
                Liq_x_EmpleadoD.SaveDet_Liq_x_Empleado(lstDetalle, cn, trx);
                Liq_x_EmpleadoD.SaveLiq_x_Empleado(lstLiq_x_Emp, cn, trx);
                //Actualizo la Liquidaciones
                //...                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Calcula_Aguinaldo(int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, string fecha_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            int nro_orden_liq = 0;
            decimal sneto = 0;
            decimal sasd = 0;
            decimal aux_valor_390 = 0;
            decimal aguinaldo = 0;
            decimal redondeo = 0;
            int nro_orden = 0;

            StringBuilder strSQL = new StringBuilder();
            List<Entities.Liquidacion> lstLiq = new List<Entities.Liquidacion>();
            lstLiq = getLstLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            List<Entities.LstEmpleados> lstEmp = new List<Entities.LstEmpleados>();
            lstEmp = EmpleadoD.GetLiqEmpleadoAguinaldo(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
            //Preparo las estructuras colecciones de:
            List<Entities.Det_Liq_x_empleado> lstDetalle = new List<Entities.Det_Liq_x_empleado>();
            Entities.Det_Liq_x_empleado eDetalle;
            List<Entities.Liq_x_Empleado> lstLiq_x_Emp = new List<Entities.Liq_x_Empleado>();
            Entities.Liq_x_Empleado eLiq_x_Emp;
            List<Entities.ConceptoLiqxEmp> lstConceptoLiqxEmp = new List<Entities.ConceptoLiqxEmp>();
            //Set RSAportes = objGral.ObtenerEstructura("APORTES_LIQ_X_EMPLEADO", "anio")
            Entities.Resultado oResultado;
            //Recorro empleados del tipo de liquidación que corresponda
            //y que no estén dados de baja

            for (int i = 0; i < lstEmp.Count; i++)
            {
                //aguinaldo = Conceptos_liqD.Calcula_ConceptoA_Nuevo(lstEmp[i], anio, cod_tipo_liq, nro_liquidacion, cod_semestre, cn, trx);
                //sneto = aguinaldo;
                oResultado = new Entities.Resultado();
                oResultado = Conceptos_liqD.Calcula_ConceptoA_Nuevo(lstEmp[i], anio, cod_tipo_liq, nro_liquidacion, cod_semestre, cn, trx);
                aguinaldo = oResultado.resultado_1;
                sneto = aguinaldo;
                ////
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                if (cod_semestre == 1)
                    eDetalle.cod_concepto_liq = 17; //Codigo que corresponde al sac 1 sem
                else
                    eDetalle.cod_concepto_liq = 18; //Codigo que corresponde al sac 2 sem
                eDetalle.nro_orden = 1;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Now.ToShortDateString();
                eDetalle.importe = aguinaldo;
                //eDetalle.unidades = 1;
                eDetalle.unidades = oResultado.resultado_2;
                aux_valor_390 = aguinaldo;
                lstEmp[i].sueldo_bruto = aux_valor_390;
                lstDetalle.Add(eDetalle);
                nro_orden = 2;
                /////////////////////////////////////////////////////////////////////////////////////
                //Ahora tengo en cuenta los conceptos que restan (descuentos/retenciones)
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_A1(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], aguinaldo, item.cod_concepto_liq,
                      anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, item.valor_concepto_liq, cn, trx);

                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        sneto = sneto - eDetalle.importe;
                        nro_orden = nro_orden + 1;
                        ////15/12/2022////////////////////////////////
                        //aux_valor_390 = aux_valor_390 + eDetalle.importe;
                    }
                    //lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                /////////////////////////////////////////////////////////////////////////////////////
                //Tengo en cuenta en primera instancia los conceptos
                //que suman y sujetos a descuento
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_A10(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], aguinaldo, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString();
                        //En el caso que los codigos de conceptos
                        //sean 300 o 310.
                        //Estos se informan como negativos
                        //y los multiplico -1
                        if (item.cod_concepto_liq == 300 || item.cod_concepto_liq == 310 || item.cod_concepto_liq == 311 || item.cod_concepto_liq == 11)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                            lstEmp[i].sueldo_bruto += eDetalle.importe;
                        }
                        sneto = sneto + eDetalle.importe;
                        sasd = sasd + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Ahora tengo en cuenta los conceptos variables no remunerativos
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_A11(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], aguinaldo, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        if (item.cod_concepto_liq == 481)
                            eDetalle.importe = oResultado.resultado_1 * -1;
                        else
                            eDetalle.importe = oResultado.resultado_1;
                        //eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        sneto = sneto + eDetalle.importe;
                        nro_orden = nro_orden + 1;
                        //15/12/2022////////////////////////////////
                        //if (eDetalle.cod_concepto_liq <= 390)
                        //{
                        //    aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        //}
                    }
                    //15/12/2022////////////////////////////////
                    lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Ahora tengo en cuenta los conceptos variables que restan
                //(descuentos/retenciones)
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_A2(Convert.ToInt16(lstEmp[i].legajo), anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], aguinaldo, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        }
                        sneto = sneto - eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    //lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Ahora tengo en cuenta los conceptos fijos que restan
                //(descuentos/retenciones)
                lstConceptoLiqxEmp = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp_A3(Convert.ToInt16(lstEmp[i].legajo), cn, trx);
                foreach (var item in lstConceptoLiqxEmp)
                {
                    oResultado = new Entities.Resultado();
                    oResultado = Conceptos_liqD.Calcula_Concepto(lstEmp[i], aguinaldo, item.cod_concepto_liq,
                        anio, cod_tipo_liq, nro_liquidacion, fecha_liquidacion, 0, cn, trx);
                    if (oResultado.resultado_1 > 0)
                    {
                        eDetalle = new Entities.Det_Liq_x_empleado();
                        eDetalle.anio = anio;
                        eDetalle.cod_tipo_liq = cod_tipo_liq;
                        eDetalle.nro_liquidacion = nro_liquidacion;
                        eDetalle.legajo = Convert.ToInt16(lstEmp[i].legajo);
                        eDetalle.cod_concepto_liq = item.cod_concepto_liq;
                        eDetalle.nro_orden = nro_orden;
                        eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                        eDetalle.importe = oResultado.resultado_1;
                        eDetalle.unidades = oResultado.resultado_2;
                        lstDetalle.Add(eDetalle);
                        //Acumulo aux_valor_390
                        //Siempre y cuando el cod sea <= 390
                        if (eDetalle.cod_concepto_liq <= 390)
                        {
                            aux_valor_390 = aux_valor_390 + eDetalle.importe;
                        }
                        sneto = sneto - eDetalle.importe;
                        nro_orden = nro_orden + 1;
                    }
                    //lstEmp[i].sueldo_bruto = aux_valor_390;
                }
                //Verifico si el sueldo neto tiene decimales, si los tiene
                //se los quito y coloco un redondeo negativo
                //redondeo = Round(sneto - Round(sneto), 2)
                redondeo = decimal.Round(sneto - decimal.Round(sneto), 2);
                sneto = decimal.Round(sneto);
                //Si el redondeo es distinto que cero grabo el mismo en el detalle
                if (redondeo != 0)
                {
                    eDetalle = new Entities.Det_Liq_x_empleado();
                    eDetalle.anio = anio;
                    eDetalle.cod_tipo_liq = cod_tipo_liq;
                    eDetalle.nro_liquidacion = nro_liquidacion;
                    eDetalle.legajo = lstEmp[i].legajo;
                    eDetalle.cod_concepto_liq = 885;
                    eDetalle.nro_orden = nro_orden;
                    eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                    eDetalle.importe = redondeo;
                    eDetalle.unidades = 1;
                    lstDetalle.Add(eDetalle);
                    nro_orden = nro_orden + 1;
                }
                //Ahora inserto el importe
                //acumulado de los cod de conceptos <= 390
                eDetalle = new Entities.Det_Liq_x_empleado();
                eDetalle.anio = anio;
                eDetalle.cod_tipo_liq = cod_tipo_liq;
                eDetalle.nro_liquidacion = nro_liquidacion;
                eDetalle.legajo = lstEmp[i].legajo;
                eDetalle.cod_concepto_liq = 390;
                eDetalle.nro_orden = nro_orden;
                eDetalle.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eDetalle.importe = aux_valor_390;
                eDetalle.unidades = 1;
                lstDetalle.Add(eDetalle);
                nro_orden = nro_orden + 1;
                //Ahora cargo el sueldo neto calculado para el legajo
                //en cuestión
                nro_orden_liq = nro_orden_liq + 1;
                eLiq_x_Emp = new Entities.Liq_x_Empleado();
                eLiq_x_Emp.anio = anio;
                eLiq_x_Emp.cod_tipo_liq = cod_tipo_liq;
                eLiq_x_Emp.nro_liquidacion = nro_liquidacion;
                eLiq_x_Emp.legajo = lstEmp[i].legajo;
                eLiq_x_Emp.fecha_alta_registro = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyy");
                eLiq_x_Emp.cod_categoria = lstEmp[i].cod_categoria;
                eLiq_x_Emp.cod_cargo = lstEmp[i].cod_cargo;
                eLiq_x_Emp.tarea = lstEmp[i].tarea;
                eLiq_x_Emp.nro_cta_sb = lstEmp[i].nro_cta_sb;
                eLiq_x_Emp.sueldo_basico = lstEmp[i].sueldo_basico;
                eLiq_x_Emp.sueldo_neto = sneto;
                eLiq_x_Emp.sueldo_bruto = lstEmp[i].sueldo_bruto;
                eLiq_x_Emp.no_remunerativo = 0;
                eLiq_x_Emp.cod_clasif_per = lstEmp[i].cod_clasif_per;
                eLiq_x_Emp.nro_orden = nro_orden_liq;
                lstLiq_x_Emp.Add(eLiq_x_Emp);
            }
            //'Ahora hago los updatebatch
            Liq_x_EmpleadoD.SaveDet_Liq_x_Empleado(lstDetalle, cn, trx);
            Liq_x_EmpleadoD.SaveLiq_x_Empleado(lstLiq_x_Emp, cn, trx);
        }
    }
}

