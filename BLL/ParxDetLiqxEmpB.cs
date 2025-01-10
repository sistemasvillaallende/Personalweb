using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Transactions;
using DAL;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BLL
{
    public class ParxDetLiqxEmpB
    {


        //public static void InsertParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<DetalleCptoEmp> oDetalle, string usuario)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            ParxDetLiqxEmpD.InsertParxDetLiqxEmp(anio, cod_tipo_liq, nro_liq, oDetalle);
        //            Dsp audito el proceso
        //            Entities.Auditoria oAudita = new Auditoria();
        //            oAudita.id_auditoria = 0;
        //            oAudita.fecha_movimiento = DateTime.Now.ToString();
        //            oAudita.menu = "CARGA DE NOVEDADES ";
        //            oAudita.proceso = "Alta de Conceptos del/los empleado";
        //            oAudita.identificacion = string.Format("{0}{1}{2}", anio, cod_tipo_liq, nro_liq);
        //            oAudita.autorizaciones = "";
        //            oAudita.observaciones = string.Format("Alta masiva de conceptos variables de la liquidacion {0},{1},{2}", anio, cod_tipo_liq, nro_liq);
        //            string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
        //            oAudita.detalle = oAudita.detalle = JsonConvert.SerializeObject(oDetalle);
        //            oAudita.usuario = usuario;
        //            DAL.AuditoriaD.Insert_movimiento(oAudita);
        //            scope.Complete();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public static void UpdateParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, List<DetalleCptoEmp> oDetalle, string usuario)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ParxDetLiqxEmpD.UpdateParxDetLiqxEmp(anio, cod_tipo_liq, nro_liq, cod_concepto_liq, oDetalle);
                    //Dsp audito el proceso
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "CARGA DE NOVEDADES ";
                    oAudita.proceso = "Actualiza Conceptos del/los empleado";
                    oAudita.identificacion = string.Format("{0}{1}{2}", anio, cod_tipo_liq, nro_liq);
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = string.Format("Actualiza conceptos variables de la liquidacion {0},{1},{2}", anio, cod_tipo_liq, nro_liq);
                    //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
                    oAudita.detalle = JsonConvert.SerializeObject(oDetalle);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public static void UpdateCptoLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<Entities.Conceptos> lstConceptos, string usuario)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            ParxDetLiqxEmpD.UpdateCptoLiqxEmp(anio, cod_tipo_liq, nro_liq, lstConceptos);
        //            //Dsp audito el proceso
        //            Entities.Auditoria oAudita = new Auditoria();
        //            oAudita.id_auditoria = 0;
        //            oAudita.fecha_movimiento = DateTime.Now.ToString();
        //            oAudita.menu = "CARGA DE NOVEDADES ";
        //            oAudita.proceso = "Actualiza Conceptos del/los empleado";
        //            oAudita.identificacion = string.Format("{0}{1}{2}", anio, cod_tipo_liq, nro_liq);
        //            oAudita.autorizaciones = "";
        //            oAudita.observaciones = string.Format("Actualiza conceptos variables de la liquidacion {0},{1},{2}", anio, cod_tipo_liq, nro_liq);
        //            //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
        //            oAudita.detalle = oAudita.detalle = JsonConvert.SerializeObject(lstConceptos);
        //            oAudita.usuario = usuario;
        //            DAL.AuditoriaD.Insert_movimiento(oAudita);
        //            scope.Complete();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public static void InsertParCptoLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<Entities.Conceptos> lstConceptos, string usuario)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ParxDetLiqxEmpD.InsertParCptoLiqxEmp(anio, cod_tipo_liq, nro_liq, lstConceptos);
                    //Dsp audito el proceso
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "CARGA DE NOVEDADES ";
                    oAudita.proceso = "Alta de Conceptos del/los empleado";
                    oAudita.identificacion = string.Format("{0}{1}{2}", anio, cod_tipo_liq,nro_liq);
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = string.Format("Alta masiva de conceptos variables de la liquidacion {0},{1},{2}", anio, cod_tipo_liq, nro_liq);
                    //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
                    oAudita.detalle = JsonConvert.SerializeObject(lstConceptos);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void DeleteParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, List<DetalleCptoEmp> oDetalle, string usuario)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ParxDetLiqxEmpD.DeleteParxDetLiqxEmp(anio, cod_tipo_liq, nro_liq, cod_concepto_liq, oDetalle);
                    //Dsp audito el proceso
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "CARGA DE NOVEDADES ";
                    oAudita.proceso = "Eliminar Conceptos del/los empleado";
                    oAudita.identificacion = cod_concepto_liq.ToString();
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = string.Format("Elimina conceptos Variables de la liquidacion {0},{1},{2}", anio, cod_tipo_liq, nro_liq);
                    //string.Format("Fecha auditoria: {0}, {1}", DateTime.Now, "Se Elimino el/los concepto/s del empleado");
                    oAudita.detalle = JsonConvert.SerializeObject(oDetalle);
                    oAudita.usuario = usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Traspaso_Concepto(int anio_actual, int cod_tipo_liq_actual, int nro_liq_actual,
            int anio_desde, int cod_tipo_liq_desde, int nro_liq_desde, int cod_concepto_liq)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            cn.Open();
            try
            {
                trx = cn.BeginTransaction();
                DAL.ParxDetLiqxEmpD.Traspaso_Concepto(anio_actual, cod_tipo_liq_actual, nro_liq_actual,
                  anio_desde, cod_tipo_liq_desde, nro_liq_desde, cod_concepto_liq, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            { cn = null; }
        }

        public static void Asistencia(int anio, int cod_tipo_liq, int nro_liquidacion)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            cn.Open();
            try
            {
                trx = cn.BeginTransaction();
                DAL.ParxDetLiqxEmpD.Asistencia(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            {
                cn = null;
            }
        }

        public static void Puntualidad(int anio, int cod_tipo_liq, int nro_liquidacion)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;

            cn.Open();
            try
            {
                trx = cn.BeginTransaction();
                DAL.ParxDetLiqxEmpD.Puntualidad(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            {
                cn = null;
            }
        }

        public static void DiasTrabajados(int anio, int cod_tipo_liq, int nro_liquidacion, int dias)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;

            cn.Open();
            try
            {
                trx = cn.BeginTransaction();
                DAL.ParxDetLiqxEmpD.DiasTrabajados(anio, cod_tipo_liq, nro_liquidacion, dias, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            {
                cn = null;
            }
        }
    }
}
