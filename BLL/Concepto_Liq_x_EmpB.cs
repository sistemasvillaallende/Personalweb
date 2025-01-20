using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Transactions;
using Newtonsoft.Json;
using DAL;
using System.Globalization;

namespace BLL
{
    public static class Concepto_Liq_x_EmpB
    {

        public static List<Entities.ConceptoLiqxEmp> FillConceptoLiqxEmp(int legajo)
        {
            return DAL.Concepto_Liq_x_EmpD.FillConceptoLiqxEmp(legajo);
        }

        public static void UpdateConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle, string obsauditoria, string usuario)
        {
            try
            {
                string opercion = string.Empty;
                using (TransactionScope scope = new TransactionScope())
                {
                    Concepto_Liq_x_EmpD.DeleteConceptosxEmp(legajo);
                    Concepto_Liq_x_EmpD.InsertConceptoxEmp(legajo, oDetalle);
                    Concepto_Liq_x_EmpD.AuditaMovimientos(oDetalle, obsauditoria);
                    //Dsp audito el proceso
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "EMPLEADOS";
                    oAudita.proceso = "Actualizar Conceptos del empleado";
                    oAudita.identificacion = legajo.ToString();
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = obsauditoria;
                    //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se hicierón cambios en los conceptos del empleado");
                    oAudita.detalle = JsonConvert.SerializeObject(oDetalle.FindAll(ent => ent.op != 0));
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle, List<ConceptoLiqxEmp> oBorrar, string obsauditoria, string usuario)
        {

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Concepto_Liq_x_EmpD.InsertConceptoxEmp(legajo, oDetalle);
                    Concepto_Liq_x_EmpD.DeleteConceptoxEmp(legajo, oBorrar);
                    Concepto_Liq_x_EmpD.AuditaMovimientos(oBorrar, obsauditoria);
                    //Dsp audito el proceso
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "EMPLEADOS";
                    oAudita.proceso = "Elimina conceptos del empleado";
                    oAudita.identificacion = legajo.ToString();
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = obsauditoria;
                    //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
                    oAudita.detalle = oAudita.detalle = JsonConvert.SerializeObject(oBorrar);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void DeleteConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Concepto_Liq_x_EmpD.DeleteConceptoxEmp(legajo, oDetalle);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /////
        ///UpdateAsignacionFamiliares
        public static void CalculoSalarioFamiliar(string usuario)
        {
            var ErrorMessage = string.Empty;
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                List<ConceptoLiqxEmp> lstEmpconFamiliares = new List<ConceptoLiqxEmp>();
                List<ConceptoLiqxEmp> lstAsignacionFam410ParaAuditar = new List<ConceptoLiqxEmp>();
                List<Familiares> lstFam = new List<Familiares>();
                using (TransactionScope scope = new TransactionScope())
                {
                    lstAsignacionFam410ParaAuditar = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp(410);
                    lstEmpconFamiliares = Concepto_Liq_x_EmpD.GetTraerFamiliares();
                    Concepto_Liq_x_EmpD.DeleteAsignacionFam(410);
                    int edad = 0;
                    int contador = 0;
                    foreach (var item in lstEmpconFamiliares)
                    {
                        lstFam = Familiares.GetHijos(item.legajo);
                        ErrorMessage = "Legajo: " + item.legajo.ToString();
                        edad = 0;
                        contador = 0;
                        if (lstFam.Count > 0)
                        {
                            foreach (var itemFam in lstFam)
                            {
                                if (itemFam.fecha_nacimiento != null)
                                {
                                    edad = DBHelper.CalcularEdad(Convert.ToDateTime(itemFam.fecha_nacimiento));
                                    if (edad <= 17)
                                    {
                                        contador++;
                                    }
                                }
                            }
                            item.cod_concepto_liq = 410;
                            item.valor_concepto_liq = contador;
                            item.fecha_alta_registro = Convert.ToString(DateTime.Now, culturaFecArgentina);
                            item.fecha_vto = Convert.ToString(DateTime.Now.AddDays(30), culturaFecArgentina);
                            item.usuario = usuario;

                        }
                        else
                        {
                            item.cod_concepto_liq = 410;
                            item.valor_concepto_liq = contador;
                            item.fecha_alta_registro = Convert.ToString(DateTime.Now, culturaFecArgentina);
                            item.fecha_vto = Convert.ToString(DateTime.Now.AddDays(30), culturaFecArgentina);
                            item.usuario = usuario;
                        }
                    }
                    ErrorMessage = string.Empty;
                    Concepto_Liq_x_EmpD.ActualizarConceptoFamiliar(lstEmpconFamiliares);
                    //
                    //Audito el concepto 410
                    //para tener un foto de los conceptos
                    //ante cualquier duda de como 
                    //estaba antes de la liquidacion
                    //                    
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "Liquidaciones";
                    oAudita.proceso = "Actualizacion_Salario_Familiar";
                    oAudita.identificacion = "410";
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = "Se Actualizo las Asignaciones familiares, fecha: " +
                        DateTime.Now.ToString() + " guardo las asignaciones anteriores para tener referencia del codigo 410.";
                    oAudita.detalle = JsonConvert.SerializeObject(lstAsignacionFam410ParaAuditar);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ErrorMessage);
                throw new Exception("Error en proceso de CalculoSalarioFamiliar!, " + ErrorMessage, ex);
            }

        }

        public static void CalculoSalarioHijoDiscapcitado(string usuario)
        {
            var ErrorMessage = string.Empty;
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                List<ConceptoLiqxEmp> lstEmpconFamiliares = new List<ConceptoLiqxEmp>();
                List<ConceptoLiqxEmp> lstAsignacionFam470ParaAuditar = new List<ConceptoLiqxEmp>();
                List<Familiares> lstFam = new List<Familiares>();
                using (TransactionScope scope = new TransactionScope())
                {
                    lstAsignacionFam470ParaAuditar = Concepto_Liq_x_EmpD.GetConceptoLiqxEmp(470);
                    lstEmpconFamiliares = Concepto_Liq_x_EmpD.GetTraerHijosDiscapacitados();
                    Concepto_Liq_x_EmpD.DeleteAsignacionFam(470);
                    foreach (var item in lstEmpconFamiliares)
                    {

                        item.cod_concepto_liq = 470;
                        //item.valor_concepto_liq = item.valor_concepto_liq;
                        item.fecha_alta_registro = Convert.ToString(DateTime.Now, culturaFecArgentina);
                        item.fecha_vto = Convert.ToString(DateTime.Now.AddDays(30), culturaFecArgentina);
                        item.usuario = usuario;

                    }
                    ErrorMessage = string.Empty;
                    Concepto_Liq_x_EmpD.ActualizarConceptoFamiliar(lstEmpconFamiliares);
                    //
                    //Audito el concepto 470
                    //para tener un foto de los conceptos
                    //ante cualquier duda de como 
                    //estaba antes de la liquidacion
                    //                    
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "Liquidaciones";
                    oAudita.proceso = "Actualizacion_Hijos_discapacitado";
                    oAudita.identificacion = "470";
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = "Se Actualizo los hijos discapacitado, fecha: " +
                        DateTime.Now.ToString() + " guardo las asignaciones anteriores para tener referencia del codigo 470.";
                    oAudita.detalle = JsonConvert.SerializeObject(lstAsignacionFam470ParaAuditar);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ErrorMessage);
                throw new Exception("Error en proceso de CalculoSalarioFamiliar!, " + ErrorMessage, ex);
            }

        }




    }
}
