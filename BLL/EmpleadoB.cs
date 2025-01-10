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
    public class EmpleadoB
    {
        public static int Insert(Empleado oEmp)
        {
            int legajo = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //legajo = EmpleadoD.insert(op);
                    scope.Complete();
                }
                catch (Exception ex) { throw ex; }
            }
            return legajo;
        }

        public static int InsertDatosEmpleado(Entities.Empleado oEmp)
        {
            int legajo = 0;
            //using (TransactionScope scope = new TransactionScope())
            //{
            //  try
            //  {
            //    legajo = EmpleadoD.InsertDatosEmpleado(oEmp);
            //    scope.Complete();
            //  }
            //  catch (Exception ex) { throw ex; }
            //}
            //return legajo;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                legajo = EmpleadoD.InsertDatosEmpleado(oEmp, cn, trx);
                trx.Commit();
            }
            catch (Exception)
            {
                trx.Rollback();
                throw;
            }
            return legajo;
        }

        public static int UpdateDatosEmpleado(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria)
        {
            int legajo = 0;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                legajo = EmpleadoD.UpdateDatosEmpleado(oEmp, usuario, id_tipo_auditoria, des_tipo_auditoria, obsauditoria, cn, trx);
                trx.Commit();
            }
            catch (Exception)
            {
                trx.Rollback();
                throw;
            }
            return legajo;
        }

        public static int UpdateTab_Datos_Contrato(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria)
        {
            int legajo = 0;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                legajo = EmpleadoD.UpdateTab_Datos_Contrato(oEmp, usuario, id_tipo_auditoria, des_tipo_auditoria, obsauditoria, cn, trx);
                trx.Commit();
            }
            catch (Exception)
            {
                trx.Rollback();
                throw;
            }
            return legajo;
        }

        public static int UpdateTab_Datos_Banco(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria)
        {
            int legajo = 0;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                legajo = EmpleadoD.UpdateTab_Datos_Banco(oEmp, usuario, id_tipo_auditoria, des_tipo_auditoria, obsauditoria, cn, trx);

                trx.Commit();

            }
            catch (Exception)
            {
                trx.Rollback();
                throw;
            }
            return legajo;
        }

        public static int UpdateTab_Datos_Particulares(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria)
        {
            int legajo = 0;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                legajo = EmpleadoD.UpdateTab_Datos_Particulares(oEmp, usuario, id_tipo_auditoria, des_tipo_auditoria, obsauditoria, cn, trx);
                trx.Commit();
            }
            catch (Exception)
            {
                trx.Rollback();
                throw;
            }
            return legajo;
        }

        public static Entities.Empleado GetByPk(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            return DAL.EmpleadoD.GetByPk(legajo, cn, trx);
        }

        public static Entities.Empleado GetByPkTodos(int legajo)
        {
            return DAL.EmpleadoD.GetByPkTodos(legajo);
        }

        public static Entities.Empleado GetByPk(int legajo)
        {
            return DAL.EmpleadoD.GetByPk(legajo);
        }

        public static List<Entities.LstEmpleados> GetEmpleadosAll()
        {

            return DAL.EmpleadoD.GetEmpleadosAll();
        }

        public static List<Entities.Certificaciones> GetCertificaciones(int legajo)
        {
            return DAL.EmpleadoD.GetCertificaciones(legajo);
        }


        public static List<Entities.Tipo_auditoria> GetTipo_auditoria()
        {
            return DAL.EmpleadoD.GetTipo_auditoria();
        }


        public static string Certficaciones_Empleado(int legajo)
        {
            string strXML = "";
            strXML = DAL.EmpleadoD.Certficaciones_Empleado(legajo);
            return strXML;
        }

        public static List<Entities.HistorialEmpleado> GetHistCambiosPersonal(int legajo)
        {
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            try
            {
                cn.Open();
                return DAL.EmpleadoD.GetHistCambiosPersonal(legajo, cn);
            }
            catch (Exception e)
            {
                throw e;
            }

        }



    }
}
