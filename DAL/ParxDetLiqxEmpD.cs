using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{

    public class ParxDetLiqxEmpD
    {


        public static void InsertParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<DetalleCptoEmp> oDetalle)
        {
            StringBuilder strSQL = new StringBuilder();
            int legajo;
            //bool paso = false;
            int nro_param;

            try
            {

                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    strSQL.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
                    strSQL.AppendLine("(anio,");
                    strSQL.AppendLine("cod_tipo_liq,");
                    strSQL.AppendLine("nro_liquidacion,");
                    strSQL.AppendLine("legajo,");
                    strSQL.AppendLine("cod_concepto_liq,");
                    strSQL.AppendLine("nro_parametro,");
                    strSQL.AppendLine("fecha_alta_registro,");
                    strSQL.AppendLine("valor_parametro, observacion)");

                    strSQL.AppendLine("VALUES");
                    strSQL.AppendLine("(@anio,");
                    strSQL.AppendLine("@cod_tipo_liq,");
                    strSQL.AppendLine("@nro_liquidacion,");
                    strSQL.AppendLine("@legajo,");
                    strSQL.AppendLine("@cod_concepto_liq,");
                    strSQL.AppendLine("@nro_parametro,");
                    strSQL.AppendLine("@fecha_alta_registro,");
                    strSQL.AppendLine("@valor_parametro, @observacion)");
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.Add(new SqlParameter("@anio", anio));
                    cmd.Parameters.Add(new SqlParameter("@cod_tipo_liq", cod_tipo_liq));
                    cmd.Parameters.Add(new SqlParameter("@nro_liquidacion", nro_liq));
                    cmd.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@nro_parametro", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@fecha_alta_registro", SqlDbType.SmallDateTime));
                    cmd.Parameters.Add(new SqlParameter("@valor_parametro", SqlDbType.Decimal));
                    cmd.Parameters.Add(new SqlParameter("@observacion", string.Empty));

                    legajo = 0;
                    nro_param = 0;
                    cmd.Connection.Open();
                    //using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                    //{
                    for (int i = 0; i < oDetalle.Count; i++)
                    {
                        if (legajo != oDetalle[i].legajo)
                        {
                            legajo = oDetalle[i].legajo;
                            nro_param = Calc_nro_parametro(anio, cod_tipo_liq, nro_liq, oDetalle[i].legajo, oDetalle[i].cod_concepto_liq, cn);
                            nro_param += 1;
                        }
                        else
                        {
                            nro_param += 1;
                        }
                        cmd.Parameters["@legajo"].Value = oDetalle[i].legajo;
                        cmd.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                        cmd.Parameters["@fecha_alta_registro"].Value = DateTime.Today.ToShortDateString();
                        cmd.Parameters["@nro_parametro"].Value = nro_param;
                        cmd.Parameters["@valor_parametro"].Value = oDetalle[i].valor_parametro;
                        //cmd.Parameters.AddWithValue("@observacion", oDetalle[i].observacion);
                        if (oDetalle[i].observacion != null)
                            cmd.Parameters["@observacion"].Value = oDetalle[i].observacion;
                        else
                            cmd.Parameters["@observacion"].Value = string.Empty;
                        cmd.CommandText = strSQL.ToString();
                        cmd.ExecuteNonQuery();
                        //}
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Asistencia(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdInsert = new SqlCommand();
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strSQLI = new StringBuilder();
            SqlDataReader dr = null;
            List<Entities.Par_x_det_liq_x_empleado> oListPar = new List<Par_x_det_liq_x_empleado>();
            Entities.Par_x_det_liq_x_empleado oPar = null;
            int legajo = 0;
            int cod_concepto_liq = Convert.ToInt32(ConfigurationManager.AppSettings["Asistencia"]);

            strSQL.AppendLine(" SELECT * FROM empleados ");
            strSQL.AppendLine(" WHERE fecha_baja is null and ");
            strSQL.AppendLine(" cod_seccion<>50 and cod_categoria<=18");
            strSQL.AppendLine(" and cod_tipo_liq=@cod_tipo_liq");
            //
            strSQLI.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
            strSQLI.AppendLine("(anio,");
            strSQLI.AppendLine("cod_tipo_liq,");
            strSQLI.AppendLine("nro_liquidacion,");
            strSQLI.AppendLine("legajo,");
            strSQLI.AppendLine("cod_concepto_liq,");
            strSQLI.AppendLine("nro_parametro,");
            strSQLI.AppendLine("fecha_alta_registro,");
            strSQLI.AppendLine("valor_parametro, observacion)");

            strSQLI.AppendLine("VALUES");
            strSQLI.AppendLine("(@anio,");
            strSQLI.AppendLine("@cod_tipo_liq,");
            strSQLI.AppendLine("@nro_liquidacion,");
            strSQLI.AppendLine("@legajo,");
            strSQLI.AppendLine("@cod_concepto_liq,");
            strSQLI.AppendLine("@nro_parametro,");
            strSQLI.AppendLine("@fecha_alta_registro,");
            strSQLI.AppendLine("@valor_parametro,");
            strSQLI.AppendLine("@observacion)");

            try
            {
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        legajo = dr.GetInt32((dr.GetOrdinal("legajo")));
                        oPar = new Par_x_det_liq_x_empleado();
                        oPar.anio = anio;
                        oPar.cod_tipo_liq = cod_tipo_liq;
                        oPar.nro_liquidacion = nro_liquidacion;
                        oPar.legajo = legajo;
                        oPar.cod_concepto_liq = cod_concepto_liq;
                        oPar.fecha_alta_registro = DateTime.Now.ToString();
                        oPar.nro_parametro = 1;
                        oPar.valor_parametro = 1;
                        oPar.observacion = "Carga Masiva.";
                        oListPar.Add(oPar);
                    }
                }
                dr.Close();
                cmdInsert.Parameters.AddWithValue("@anio", anio);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmdInsert.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmdInsert.Parameters.AddWithValue("@legajo", 0);
                cmdInsert.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmdInsert.Parameters.AddWithValue("@nro_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());
                cmdInsert.Parameters.AddWithValue("@valor_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@observacion", string.Empty);
                cmdInsert.CommandText = strSQLI.ToString();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                foreach (var item in oListPar)
                {
                    cmdInsert.Parameters["@anio"].Value = item.anio;
                    cmdInsert.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmdInsert.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmdInsert.Parameters["@legajo"].Value = item.legajo;
                    cmdInsert.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmdInsert.Parameters["@nro_parametro"].Value = item.nro_parametro;
                    cmdInsert.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmdInsert.Parameters["@valor_parametro"].Value = item.valor_parametro;
                    cmdInsert.Parameters["@observacion"].Value = item.observacion;
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                cmd = null;
                cmdInsert = null;
                strSQL = null;
                strSQLI = null;
            }
        }

        public static void DiasTrabajados(int anio, int cod_tipo_liq, int nro_liquidacion, int dias, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdInsert = new SqlCommand();
            string strSQL = "";
            StringBuilder strSQLI = new StringBuilder();
            SqlDataReader dr = null;
            List<Entities.Par_x_det_liq_x_empleado> oListPar = new List<Par_x_det_liq_x_empleado>();
            Entities.Par_x_det_liq_x_empleado oPar = null;
            int legajo = 0;
            //int dias = Convert.ToInt32(ConfigurationManager.AppSettings["diastrabajados"]);
            int cod_concepto_liq = 10;
            strSQL = @"SELECT * 
                       FROM Empleados
                       WHERE fecha_baja is null
                       And cod_tipo_liq=@cod_tipo_liq";
            //
            strSQLI.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
            strSQLI.AppendLine("(anio,");
            strSQLI.AppendLine("cod_tipo_liq,");
            strSQLI.AppendLine("nro_liquidacion,");
            strSQLI.AppendLine("legajo,");
            strSQLI.AppendLine("cod_concepto_liq,");
            strSQLI.AppendLine("nro_parametro,");
            strSQLI.AppendLine("fecha_alta_registro,");
            strSQLI.AppendLine("valor_parametro, observacion)");
            strSQLI.AppendLine("VALUES");
            strSQLI.AppendLine("(@anio,");
            strSQLI.AppendLine("@cod_tipo_liq,");
            strSQLI.AppendLine("@nro_liquidacion,");
            strSQLI.AppendLine("@legajo,");
            strSQLI.AppendLine("@cod_concepto_liq,");
            strSQLI.AppendLine("@nro_parametro,");
            strSQLI.AppendLine("@fecha_alta_registro,");
            strSQLI.AppendLine("@valor_parametro, @observacion)");

            try
            {
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        legajo = dr.GetInt32((dr.GetOrdinal("legajo")));
                        oPar = new Par_x_det_liq_x_empleado();
                        oPar.anio = anio;
                        oPar.cod_tipo_liq = cod_tipo_liq;
                        oPar.nro_liquidacion = nro_liquidacion;
                        oPar.legajo = legajo;
                        oPar.cod_concepto_liq = cod_concepto_liq;
                        oPar.fecha_alta_registro = DateTime.Now.ToString();
                        oPar.nro_parametro = 1;
                        oPar.valor_parametro = dias;
                        oPar.observacion = "Carga Masiva.";
                        oListPar.Add(oPar);
                    }
                }
                dr.Close();
                cmdInsert.Parameters.AddWithValue("@anio", anio);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmdInsert.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmdInsert.Parameters.AddWithValue("@legajo", 0);
                cmdInsert.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmdInsert.Parameters.AddWithValue("@nro_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());
                cmdInsert.Parameters.AddWithValue("@valor_parametro", dias);
                cmdInsert.Parameters.AddWithValue("@observacion", string.Empty);
                cmdInsert.CommandText = strSQLI.ToString();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                foreach (var item in oListPar)
                {
                    cmdInsert.Parameters["@anio"].Value = item.anio;
                    cmdInsert.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmdInsert.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmdInsert.Parameters["@legajo"].Value = item.legajo;
                    cmdInsert.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmdInsert.Parameters["@nro_parametro"].Value = item.nro_parametro;
                    cmdInsert.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmdInsert.Parameters["@valor_parametro"].Value = item.valor_parametro;
                    cmdInsert.Parameters["@observacion"].Value = item.observacion;
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                cmd = null;
                cmdInsert = null;
                strSQL = null;
                strSQLI = null;
            }
        }

        public static void Puntualidad(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdInsert = new SqlCommand();
            string strSQL = "";
            StringBuilder strSQLI = new StringBuilder();
            SqlDataReader dr = null;
            List<Entities.Par_x_det_liq_x_empleado> oListPar = new List<Par_x_det_liq_x_empleado>();
            Entities.Par_x_det_liq_x_empleado oPar = null;
            int legajo = 0;
            int cod_concepto_liq = Convert.ToInt32(ConfigurationManager.AppSettings["Puntualidad"]);

            strSQL = @"SELECT * 
                       FROM Empleados
                       WHERE fecha_baja is null
                         And cod_seccion<>50 and cod_categoria<=18
                         And cod_tipo_liq=@cod_tipo_liq";
            //
            strSQLI.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
            strSQLI.AppendLine("(anio,");
            strSQLI.AppendLine("cod_tipo_liq,");
            strSQLI.AppendLine("nro_liquidacion,");
            strSQLI.AppendLine("legajo,");
            strSQLI.AppendLine("cod_concepto_liq,");
            strSQLI.AppendLine("nro_parametro,");
            strSQLI.AppendLine("fecha_alta_registro,");
            strSQLI.AppendLine("valor_parametro, observacion)");

            strSQLI.AppendLine("VALUES");
            strSQLI.AppendLine("(@anio,");
            strSQLI.AppendLine("@cod_tipo_liq,");
            strSQLI.AppendLine("@nro_liquidacion,");
            strSQLI.AppendLine("@legajo,");
            strSQLI.AppendLine("@cod_concepto_liq,");
            strSQLI.AppendLine("@nro_parametro,");
            strSQLI.AppendLine("@fecha_alta_registro,");
            strSQLI.AppendLine("@valor_parametro, @observacion)");

            try
            {
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        legajo = dr.GetInt32((dr.GetOrdinal("legajo")));
                        oPar = new Par_x_det_liq_x_empleado();
                        oPar.anio = anio;
                        oPar.cod_tipo_liq = cod_tipo_liq;
                        oPar.nro_liquidacion = nro_liquidacion;
                        oPar.legajo = legajo;
                        oPar.cod_concepto_liq = cod_concepto_liq;
                        oPar.fecha_alta_registro = DateTime.Now.ToString();
                        oPar.nro_parametro = 1;
                        oPar.valor_parametro = 1;
                        oPar.observacion = "Carga Masiva.";
                        oListPar.Add(oPar);
                    }
                }
                dr.Close();
                cmdInsert.Parameters.AddWithValue("@anio", anio);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmdInsert.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmdInsert.Parameters.AddWithValue("@legajo", 0);
                cmdInsert.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmdInsert.Parameters.AddWithValue("@nro_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());
                cmdInsert.Parameters.AddWithValue("@valor_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@observacion", string.Empty);
                cmdInsert.CommandText = strSQLI.ToString();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                foreach (var item in oListPar)
                {
                    cmdInsert.Parameters["@anio"].Value = item.anio;
                    cmdInsert.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmdInsert.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmdInsert.Parameters["@legajo"].Value = item.legajo;
                    cmdInsert.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmdInsert.Parameters["@nro_parametro"].Value = item.nro_parametro;
                    cmdInsert.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmdInsert.Parameters["@valor_parametro"].Value = item.valor_parametro;
                    cmdInsert.Parameters["@observacion"].Value = item.observacion;
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                cmd = null;
                cmdInsert = null;
                strSQL = null;
                strSQLI = null;
            }
        }

        public static int Calc_nro_parametro(int anio, int cod_tipo_liq, int nro_liq, int legajo, int cod_concepto_liq, SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder strSQL = new StringBuilder();
            int nro_paramentro = 0;

            strSQL.AppendLine("Select isnull(max(nro_parametro),0)");
            strSQL.AppendLine("From PAR_X_DET_LIQ_X_EMPLEADO");
            strSQL.AppendLine("Where anio=@anio");
            strSQL.AppendLine("and cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine("and nro_liquidacion=@nro_liquidacion");
            strSQL.AppendLine("and cod_concepto_liq=@cod_concepto_liq");
            strSQL.AppendLine("and legajo=@legajo");

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmd.Parameters.AddWithValue("@legajo", legajo);
                cmd.Connection = cn;
                nro_paramentro = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                strSQL = null;
                cmd = null;
            }

            return nro_paramentro;
        }

        public static int Calc_nro_parametro(int anio, int cod_tipo_liq, int nro_liq, int legajo, int cod_concepto_liq, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSQL = new StringBuilder();
            int nro_paramentro = 0;

            strSQL.AppendLine("Select isnull(max(nro_parametro),0)");
            strSQL.AppendLine("From PAR_X_DET_LIQ_X_EMPLEADO");
            strSQL.AppendLine("Where anio=@anio");
            strSQL.AppendLine("and cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine("and nro_liquidacion=@nro_liquidacion");
            strSQL.AppendLine("and cod_concepto_liq=@cod_concepto_liq");
            strSQL.AppendLine("and legajo=@legajo");

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmd.Parameters.AddWithValue("@legajo", legajo);
                nro_paramentro = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                strSQL = null;
                cmd = null;
            }

            return nro_paramentro;
        }

        public static void DeleteParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, List<DetalleCptoEmp> oDetalle)
        {
            SqlConnection conn = null;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                conn = DALBase.GetConnection("SIIMVA");
                conn.Open();
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("DELETE PAR_X_DET_LIQ_X_EMPLEADO");
                strSQL.AppendLine("WHERE anio = @anio");
                strSQL.AppendLine("AND cod_tipo_liq = @cod_tipo_liq");
                strSQL.AppendLine("AND nro_liquidacion = @nro_liquidacion");
                strSQL.AppendLine("AND cod_concepto_liq = @cod_concepto_liq");


                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@anio", anio);
                objCommand.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                objCommand.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                objCommand.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                objCommand.CommandText = strSQL.ToString();
                objCommand.Connection = conn;
                objCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, List<DetalleCptoEmp> oDetalle)
        {
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendLine("DELETE PAR_X_DET_LIQ_X_EMPLEADO");
                    strSQL.AppendLine("WHERE anio = @anio");
                    strSQL.AppendLine("AND cod_tipo_liq = @cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion = @nro_liquidacion");
                    strSQL.AppendLine("AND cod_concepto_liq = @cod_concepto_liq");
                    //strSQL.AppendLine("AND legajo = @legajo");
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    //objCommand.Parameters.AddWithValue("@legajo", 0);
                    //for (int i = 0; i < oDetalle.Count; i++)
                    //{
                    //objCommand.Parameters["@anio"].Value = anio;
                    //objCommand.Parameters["@cod_tipo_liq"].Value =  cod_tipo_liq;
                    //objCommand.Parameters["@nro_liquidacion"].Value = nro_liq;
                    //objCommand.Parameters["@cod_concepto_liq"].Value = cod_concepto_liq;
                    //objCommand.Parameters["@legajo"].Value = oDetalle[i].legajo;
                    //objCommand.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    //}
                    InsertParxDetLiqxEmp(anio, cod_tipo_liq, nro_liq, oDetalle, cn);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void InsertParxDetLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<DetalleCptoEmp> oDetalle, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            StringBuilder strSQL = new StringBuilder();

            int legajo;
            //bool paso = false;
            int nro_param;
            try
            {
                strSQL.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
                strSQL.AppendLine("(anio,");
                strSQL.AppendLine("cod_tipo_liq,");
                strSQL.AppendLine("nro_liquidacion,");
                strSQL.AppendLine("legajo,");
                strSQL.AppendLine("cod_concepto_liq,");
                strSQL.AppendLine("nro_parametro,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("valor_parametro, observacion)");

                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@anio,");
                strSQL.AppendLine("@cod_tipo_liq,");
                strSQL.AppendLine("@nro_liquidacion,");
                strSQL.AppendLine("@legajo,");
                strSQL.AppendLine("@cod_concepto_liq,");
                strSQL.AppendLine("@nro_parametro,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@valor_parametro, @observacion)");

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@anio", anio));
                cmd.Parameters.Add(new SqlParameter("@cod_tipo_liq", cod_tipo_liq));
                cmd.Parameters.Add(new SqlParameter("@nro_liquidacion", nro_liq));
                cmd.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@nro_parametro", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@fecha_alta_registro", SqlDbType.SmallDateTime));
                cmd.Parameters.Add(new SqlParameter("@valor_parametro", SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@observacion", string.Empty));

                legajo = 0;
                nro_param = 0;

                //using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                //{
                for (int i = 0; i < oDetalle.Count; i++)
                {
                    if (legajo != oDetalle[i].legajo)
                    {
                        legajo = oDetalle[i].legajo;
                        nro_param = Calc_nro_parametro(anio, cod_tipo_liq, nro_liq, oDetalle[i].legajo, oDetalle[i].cod_concepto_liq, conn);
                        nro_param += 1;
                    }
                    else
                    {
                        nro_param += 1;
                    }
                    cmd.Parameters["@legajo"].Value = oDetalle[i].legajo;
                    cmd.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                    cmd.Parameters["@fecha_alta_registro"].Value = DateTime.Now.ToString(); //DateTime.Today.ToShortDateString();
                    cmd.Parameters["@nro_parametro"].Value = nro_param;
                    cmd.Parameters["@valor_parametro"].Value = oDetalle[i].valor_parametro;

                    if (oDetalle[i].observacion != null)
                        cmd.Parameters["@observacion"].Value = oDetalle[i].observacion;
                    else
                        cmd.Parameters["@observacion"].Value = string.Empty;

                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    //}
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void UpdateCptoLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<Entities.Conceptos> lstConceptos)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    strSQL.AppendLine("DELETE PAR_X_DET_LIQ_X_EMPLEADO");
                    strSQL.AppendLine("WHERE anio = @anio");
                    strSQL.AppendLine("AND cod_tipo_liq = @cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion = @nro_liquidacion");
                    //strSQL.AppendLine("AND cod_concepto_liq = @cod_concepto_liq");
                    //strSQL.AppendLine("AND legajo = @legajo");
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                    //objCommand.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    //objCommand.Parameters.AddWithValue("@legajo", 0);
                    //for (int i = 0; i < oDetalle.Count; i++)
                    //{
                    //objCommand.Parameters["@anio"].Value = anio;
                    //objCommand.Parameters["@cod_tipo_liq"].Value =  cod_tipo_liq;
                    //objCommand.Parameters["@nro_liquidacion"].Value = nro_liq;
                    //objCommand.Parameters["@cod_concepto_liq"].Value = cod_concepto_liq;
                    //objCommand.Parameters["@legajo"].Value = oDetalle[i].legajo;
                    //objCommand.CommandText = strSQL.ToString();
                    cmd.ExecuteNonQuery();
                    InsertParCptoLiqxEmp(anio, cod_tipo_liq, nro_liq, lstConceptos);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void InsertParCptoLiqxEmp(int anio, int cod_tipo_liq, int nro_liq, List<Entities.Conceptos> lstConceptos)
        {
            StringBuilder strSQL = new StringBuilder();
            int legajo;
            //bool paso = false;
            int nro_param;

            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    strSQL.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
                    strSQL.AppendLine("(anio,");
                    strSQL.AppendLine("cod_tipo_liq,");
                    strSQL.AppendLine("nro_liquidacion,");
                    strSQL.AppendLine("legajo,");
                    strSQL.AppendLine("cod_concepto_liq,");
                    strSQL.AppendLine("nro_parametro,");
                    strSQL.AppendLine("fecha_alta_registro,");
                    strSQL.AppendLine("valor_parametro, observacion)");

                    strSQL.AppendLine("VALUES");
                    strSQL.AppendLine("(@anio,");
                    strSQL.AppendLine("@cod_tipo_liq,");
                    strSQL.AppendLine("@nro_liquidacion,");
                    strSQL.AppendLine("@legajo,");
                    strSQL.AppendLine("@cod_concepto_liq,");
                    strSQL.AppendLine("@nro_parametro,");
                    strSQL.AppendLine("@fecha_alta_registro,");
                    strSQL.AppendLine("@valor_parametro, @observacion)");

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@anio", anio));
                    cmd.Parameters.Add(new SqlParameter("@cod_tipo_liq", cod_tipo_liq));
                    cmd.Parameters.Add(new SqlParameter("@nro_liquidacion", nro_liq));
                    cmd.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@nro_parametro", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@fecha_alta_registro", SqlDbType.SmallDateTime));
                    cmd.Parameters.Add(new SqlParameter("@valor_parametro", SqlDbType.Decimal));
                    cmd.Parameters.Add(new SqlParameter("@observacion", string.Empty));

                    legajo = 0;
                    nro_param = 0;
                    for (int i = 0; i < lstConceptos.Count; i++)
                    {
                        if (legajo != lstConceptos[i].legajo)
                        {
                            legajo = lstConceptos[i].legajo;
                            nro_param = Calc_nro_parametro(anio, cod_tipo_liq, nro_liq, lstConceptos[i].legajo, lstConceptos[i].codigo, cn);
                            nro_param += 1;
                        }
                        else
                        {
                            nro_param += 1;
                        }
                        cmd.Parameters["@legajo"].Value = lstConceptos[i].legajo;
                        cmd.Parameters["@cod_concepto_liq"].Value = lstConceptos[i].codigo;
                        cmd.Parameters["@fecha_alta_registro"].Value = DateTime.Today.ToShortDateString();
                        cmd.Parameters["@nro_parametro"].Value = nro_param;
                        cmd.Parameters["@valor_parametro"].Value = lstConceptos[i].importe;
                        //cmd.Parameters["@observacion"].Value = lstConceptos[i].observacion;
                        if (lstConceptos[i].observacion != null)
                            cmd.Parameters["@observacion"].Value = lstConceptos[i].observacion;
                        else
                            cmd.Parameters["@observacion"].Value = string.Empty;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Traspaso_Concepto(int anio_actual, int cod_tipo_liq_actual, int nro_liq_actual,
       int anio_desde, int cod_tipo_liq_desde, int nro_liq_desde, int cod_concepto_liq, SqlConnection cn, SqlTransaction trx)

        {

            int nro_param = 0;
            int legajo = 0;
            List<Entities.Par_x_det_liq_x_empleado> oListPar = new List<Par_x_det_liq_x_empleado>();
            Entities.Par_x_det_liq_x_empleado oPar = null;
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strSQLInsert = new StringBuilder();

            SqlCommand cmd = null;
            SqlCommand cmdInsert = null;
            SqlDataReader dr;

            strSQL.AppendLine("SELECT legajo, cod_concepto_liq, valor_parametro, nro_parametro");
            strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO");
            strSQL.AppendLine("WHERE anio = @anio");
            strSQL.AppendLine("AND cod_tipo_liq = @cod_tipo_liq");
            strSQL.AppendLine("AND nro_liquidacion = @nro_liquidacion");
            strSQL.AppendLine("AND cod_concepto_liq = @cod_concepto_liq");
            //strSQL.AppendLine("GROUP BY legajo, cod_concepto_liq, valor_parametro");
            //
            strSQLInsert.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
            strSQLInsert.AppendLine("(anio,");
            strSQLInsert.AppendLine("cod_tipo_liq,");
            strSQLInsert.AppendLine("nro_liquidacion,");
            strSQLInsert.AppendLine("legajo,");
            strSQLInsert.AppendLine("cod_concepto_liq,");
            strSQLInsert.AppendLine("nro_parametro,");
            strSQLInsert.AppendLine("fecha_alta_registro,");
            strSQLInsert.AppendLine("valor_parametro, observacion)");

            strSQLInsert.AppendLine("VALUES");
            strSQLInsert.AppendLine("(@anio,");
            strSQLInsert.AppendLine("@cod_tipo_liq,");
            strSQLInsert.AppendLine("@nro_liquidacion,");
            strSQLInsert.AppendLine("@legajo,");
            strSQLInsert.AppendLine("@cod_concepto_liq,");
            strSQLInsert.AppendLine("@nro_parametro,");
            strSQLInsert.AppendLine("@fecha_alta_registro,");
            strSQLInsert.AppendLine("@valor_parametro, @observacion)");


            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@anio", anio_desde);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq_desde);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq_desde);
                cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmd.Parameters.AddWithValue("@observacion", string.Empty);

                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        legajo = dr.GetInt32((dr.GetOrdinal("legajo")));
                        oPar = new Par_x_det_liq_x_empleado();
                        oPar.anio = anio_actual;
                        oPar.cod_tipo_liq = cod_tipo_liq_actual;
                        oPar.nro_liquidacion = nro_liq_actual;
                        oPar.legajo = legajo;
                        oPar.cod_concepto_liq = dr.GetInt32(dr.GetOrdinal("cod_concepto_liq"));
                        oPar.fecha_alta_registro = DateTime.Now.ToString();
                        nro_param = 1;
                        //nro_param = Calc_nro_parametro(anio_actual, cod_tipo_liq_actual, nro_liq_actual, legajo, cod_concepto_liq, cn, trx);
                        //nro_param += 1;
                        oPar.nro_parametro = nro_param;
                        oPar.valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        oPar.observacion = "Traspaso de Concepto de Liq. Anterior.";
                        oListPar.Add(oPar);
                    }
                }
                dr.Close();
                //
                cmdInsert = new SqlCommand();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Parameters.AddWithValue("@anio", 0);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", 0);
                cmdInsert.Parameters.AddWithValue("@nro_liquidacion", 0);
                cmdInsert.Parameters.AddWithValue("@legajo", 0);
                cmdInsert.Parameters.AddWithValue("@cod_concepto_liq", 0);
                cmdInsert.Parameters.AddWithValue("@nro_parametro", 0);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());
                cmdInsert.Parameters.AddWithValue("@valor_parametro", 0);
                cmdInsert.Parameters.AddWithValue("@observacion", string.Empty);
                cmdInsert.CommandText = strSQLInsert.ToString();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                //
                foreach (var item in oListPar)
                {
                    cmdInsert.Parameters["@anio"].Value = anio_actual;
                    cmdInsert.Parameters["@cod_tipo_liq"].Value = cod_tipo_liq_actual;
                    cmdInsert.Parameters["@nro_liquidacion"].Value = nro_liq_actual;
                    cmdInsert.Parameters["@legajo"].Value = item.legajo;
                    cmdInsert.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmdInsert.Parameters["@nro_parametro"].Value = item.nro_parametro; ;
                    cmdInsert.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmdInsert.Parameters["@valor_parametro"].Value = item.valor_parametro;
                    cmdInsert.Parameters["@observacion"].Value = item.observacion;
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                strSQL = null;
                strSQLInsert = null;
                cmd = null;
                cmdInsert = null;
            }

        }

        public static void ActualizarHijos(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdInsert = new SqlCommand();
            string strSQL = String.Empty;
            StringBuilder strSQLI = new StringBuilder();
            SqlDataReader dr = null;
            List<Entities.Par_x_det_liq_x_empleado> oListPar = new List<Par_x_det_liq_x_empleado>();
            Entities.Par_x_det_liq_x_empleado oPar = null;
            int legajo = 0;
            int cod_concepto_liq = Convert.ToInt32(ConfigurationManager.AppSettings["Asistencia"]);

            strSQL = @" SELECT * From empleados
                        WHERE fecha_baja is null 
                          And cod_seccion<>50 and cod_categoria<=18
                          And cod_tipo_liq=@cod_tipo_liq";
            //
            strSQLI.AppendLine("INSERT INTO PAR_X_DET_LIQ_X_EMPLEADO");
            strSQLI.AppendLine("(anio,");
            strSQLI.AppendLine("cod_tipo_liq,");
            strSQLI.AppendLine("nro_liquidacion,");
            strSQLI.AppendLine("legajo,");
            strSQLI.AppendLine("cod_concepto_liq,");
            strSQLI.AppendLine("nro_parametro,");
            strSQLI.AppendLine("fecha_alta_registro,");
            strSQLI.AppendLine("valor_parametro, observacion)");

            strSQLI.AppendLine("VALUES");
            strSQLI.AppendLine("(@anio,");
            strSQLI.AppendLine("@cod_tipo_liq,");
            strSQLI.AppendLine("@nro_liquidacion,");
            strSQLI.AppendLine("@legajo,");
            strSQLI.AppendLine("@cod_concepto_liq,");
            strSQLI.AppendLine("@nro_parametro,");
            strSQLI.AppendLine("@fecha_alta_registro,");
            strSQLI.AppendLine("@valor_parametro, @observacion)");

            try
            {
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        legajo = dr.GetInt32((dr.GetOrdinal("legajo")));
                        oPar = new Par_x_det_liq_x_empleado();
                        oPar.anio = anio;
                        oPar.cod_tipo_liq = cod_tipo_liq;
                        oPar.nro_liquidacion = nro_liquidacion;
                        oPar.legajo = legajo;
                        oPar.cod_concepto_liq = cod_concepto_liq;
                        oPar.fecha_alta_registro = DateTime.Now.ToString();
                        oPar.nro_parametro = 1;
                        oPar.valor_parametro = 1;
                        oPar.observacion = "Actualizacion de Hijos.";
                        oListPar.Add(oPar);
                    }
                }
                dr.Close();
                cmdInsert.Parameters.AddWithValue("@anio", anio);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmdInsert.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmdInsert.Parameters.AddWithValue("@legajo", 0);
                cmdInsert.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                cmdInsert.Parameters.AddWithValue("@nro_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now.ToString());
                cmdInsert.Parameters.AddWithValue("@valor_parametro", 1);
                cmdInsert.Parameters.AddWithValue("@observacion", string.Empty);
                cmdInsert.CommandText = strSQLI.ToString();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                foreach (var item in oListPar)
                {
                    cmdInsert.Parameters["@anio"].Value = item.anio;
                    cmdInsert.Parameters["@cod_tipo_liq"].Value = item.cod_tipo_liq;
                    cmdInsert.Parameters["@nro_liquidacion"].Value = item.nro_liquidacion;
                    cmdInsert.Parameters["@legajo"].Value = item.legajo;
                    cmdInsert.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                    cmdInsert.Parameters["@nro_parametro"].Value = item.nro_parametro;
                    cmdInsert.Parameters["@fecha_alta_registro"].Value = item.fecha_alta_registro;
                    cmdInsert.Parameters["@valor_parametro"].Value = item.valor_parametro;
                    cmdInsert.Parameters["@observacion"].Value = item.observacion;
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error !!!");
            }
            finally
            {
                cmd = null;
                cmdInsert = null;
                strSQL = null;
                strSQLI = null;
            }
        }


    }



}

