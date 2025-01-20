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
            int num = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return num;
        }

        public static int InsertDatosEmpleado(Empleado oEmp)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = (SqlTransaction)null;
            int num;
            try
            {
                connection.Open();
                trx = connection.BeginTransaction();
                num = EmpleadoD.InsertDatosEmpleado(oEmp, connection, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw;
            }
            return num;
        }

        public static int UpdateDatosEmpleado(Empleado oEmp, string usuario)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = (SqlTransaction)null;
            int num;
            try
            {
                connection.Open();
                trx = connection.BeginTransaction();
                num = EmpleadoD.UpdateDatosEmpleado(oEmp, usuario, connection, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw;
            }
            return num;
        }

        public static int UpdateTab_Datos_Contrato(Empleado oEmp, string usuario)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = (SqlTransaction)null;
            int num;
            try
            {
                connection.Open();
                trx = connection.BeginTransaction();
                num = EmpleadoD.UpdateTab_Datos_Contrato(oEmp, usuario, connection, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw;
            }
            return num;
        }

        public static int UpdateTab_Datos_Banco(Empleado oEmp, string usuario)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = (SqlTransaction)null;
            int num;
            try
            {
                connection.Open();
                trx = connection.BeginTransaction();
                num = EmpleadoD.UpdateTab_Datos_Banco(oEmp, usuario, connection, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw;
            }
            return num;
        }

        public static int UpdateTab_Datos_Particulares(Empleado oEmp, string usuario)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = (SqlTransaction)null;
            int num;
            try
            {
                connection.Open();
                trx = connection.BeginTransaction();
                num = EmpleadoD.UpdateTab_Datos_Particulares(oEmp, usuario, connection, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw;
            }
            return num;
        }

        public static Empleado GetByPk(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            return EmpleadoD.GetByPk(legajo, cn, trx);
        }

        public static Empleado GetByPkTodos(int legajo) => EmpleadoD.GetByPkTodos(legajo);

        public static Empleado GetByPk(int legajo) => EmpleadoD.GetByPk(legajo);

        public static List<LstEmpleados> GetEmpleadosAll() => EmpleadoD.GetEmpleadosAll();

        public static List<Certificaciones> GetCertificaciones(int legajo)
        {
            return EmpleadoD.GetCertificaciones(legajo);
        }

        public static string Certficaciones_Empleado(int legajo)
        {
            return EmpleadoD.Certficaciones_Empleado(legajo);
        }

        public static List<HistorialEmpleado> GetHistCambiosPersonal(int legajo)
        {
            SqlConnection connection = DALBase.GetConnection("SIIMVA");
            try
            {
                connection.Open();
                return EmpleadoD.GetHistCambiosPersonal(legajo, connection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
