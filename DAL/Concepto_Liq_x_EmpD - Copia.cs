using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Concepto_Liq_x_EmpDCopia
    {

        public static List<Entities.ConceptoLiqxEmp> FillConceptoLiqxEmp(int legajo)
        {
            Entities.ConceptoLiqxEmp oDetalle = null;
            List<Entities.ConceptoLiqxEmp> lst = new List<Entities.ConceptoLiqxEmp>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select a.legajo, a.cod_concepto_liq, b.des_concepto_liq as concepto,");
            strSQL.AppendLine("a.valor_concepto_liq, convert(varchar(10), a.fecha_vto, 103) as fecha_vto, ");
            strSQL.AppendLine("convert(varchar(10), a.fecha_alta_registro, 103) as fecha_alta_registro  ");
            strSQL.AppendLine("From CONCEP_LIQUID_X_EMPLEADO a");
            strSQL.AppendLine("join CONCEPTOS_LIQUIDACION b on");
            strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq");
            strSQL.AppendLine("join EMPLEADOS e on");
            strSQL.AppendLine("a.legajo = e.legajo");
            strSQL.AppendLine("Where a.legajo=@legajo");
            strSQL.AppendLine("ORDER BY b.cod_concepto_liq");

            cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@legajo", legajo);

            try
            {
                cn = DALBase.GetConnection("Siimva");

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    oDetalle = new ConceptoLiqxEmp();

                    int cod_concepto_liq1 = dr.GetOrdinal("cod_concepto_liq");
                    int concepto = dr.GetOrdinal("concepto");
                    int fecha = dr.GetOrdinal("fecha_vto");
                    int valor = dr.GetOrdinal("valor_concepto_liq");
                    int fecha_alta = dr.GetOrdinal("fecha_alta_registro");

                    oDetalle.legajo = legajo;
                    if (!dr.IsDBNull(cod_concepto_liq1)) oDetalle.cod_concepto_liq = dr.GetInt32(cod_concepto_liq1);
                    if (!dr.IsDBNull(concepto)) oDetalle.des_concepto_liq = dr.GetString(concepto);
                    if (!dr.IsDBNull(valor)) oDetalle.valor_concepto_liq = dr.GetDecimal(valor);
                    if (!dr.IsDBNull(fecha)) oDetalle.fecha_vto = dr.GetString(fecha);
                    if (!dr.IsDBNull(fecha_alta)) oDetalle.fecha_alta_registro = dr.GetString(fecha_alta);

                    lst.Add(oDetalle);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lst;

        }

        public static void DeleteConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        {
            SqlConnection conn = null;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                conn = DALBase.GetConnection("SIIMVA");
                conn.Open();
                StringBuilder strSQL = new StringBuilder();


                strSQL.AppendLine("DELETE CONCEP_LIQUID_X_EMPLEADO");
                strSQL.AppendLine(" WHERE legajo=@legajo and cod_concepto_liq=@cod_concepto_liq");

                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));


                for (int i = 0; i < oDetalle.Count; i++)
                {
                    objCommand.Parameters["@legajo"].Value = oDetalle[i].legajo;
                    objCommand.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                    objCommand.CommandText = strSQL.ToString();
                    objCommand.Connection = conn;
                    objCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void UpdateConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        {
            SqlConnection conn = null;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                conn = DALBase.GetConnection("SIIMVA");
                conn.Open();
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("DELETE FROM CONCEP_LIQUID_X_EMPLEADO");
                strSQL.AppendLine("WHERE legajo=@legajo");
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@legajo", legajo);
                objCommand.CommandText = strSQL.ToString();
                objCommand.Connection = conn;
                objCommand.ExecuteNonQuery();

                //objCommand.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                //objCommand.Parameters.Add(new SqlParameter("@valor_concepto_liq", SqlDbType.Decimal));
                //objCommand.Parameters.Add(new SqlParameter("@fecha_vto", SqlDbType.SmallDateTime));
                //objCommand.Parameters.Add(new SqlParameter("@usuario", SqlDbType.Text));


                //for (int i = 0; i < oDetalle.Count; i++)
                //{
                //  if (oDetalle[i].op == 2)
                //  {

                //    objCommand.Parameters["@legajo"].Value = oDetalle[i].legajo;
                //    objCommand.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                //    objCommand.Parameters["@valor_concepto_liq"].Value = oDetalle[i].valor_concepto_liq;
                //    if (Convert.ToDateTime(oDetalle[i].fecha_vto) >= Convert.ToDateTime("1/1/1900"))
                //      objCommand.Parameters["@fecha_vto"].Value = oDetalle[i].fecha_vto;
                //    objCommand.Parameters["@usuario"].Value = oDetalle[i].usuario;

                //    objCommand.CommandText = strSQL.ToString();
                //    objCommand.Connection = conn;
                //    objCommand.ExecuteNonQuery();

                //  }
                //}
                InsertConceptoxEmp(legajo, oDetalle, conn);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void InsertConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle, SqlConnection conn)
        {

            SqlCommand objCommand = new SqlCommand();
            StringBuilder strSQL = new StringBuilder();
            //SqlConnection conn = null;
            //bool paso = false;
            try
            {

                //conn = DALBase.GetConnection("SIIMVA");
                //conn.Open();
                strSQL.AppendLine("INSERT INTO CONCEP_LIQUID_X_EMPLEADO");
                strSQL.AppendLine("(legajo,");
                strSQL.AppendLine("cod_concepto_liq,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("valor_concepto_liq,");
                strSQL.AppendLine("fecha_vto,");
                strSQL.AppendLine("usuario)");


                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@legajo,");
                strSQL.AppendLine("@cod_concepto_liq,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@valor_concepto_liq,");
                strSQL.AppendLine("@fecha_vto,");
                strSQL.AppendLine("@usuario)");


                objCommand.CommandType = CommandType.Text;

                objCommand.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                objCommand.Parameters.Add(new SqlParameter("@fecha_alta_registro", SqlDbType.SmallDateTime));
                objCommand.Parameters.Add(new SqlParameter("@valor_concepto_liq", SqlDbType.Decimal));
                objCommand.Parameters.Add(new SqlParameter("@fecha_vto", SqlDbType.SmallDateTime));
                objCommand.Parameters.Add(new SqlParameter("@usuario", SqlDbType.Text));

                for (int i = 0; i < oDetalle.Count; i++)
                {
                    objCommand.Parameters["@legajo"].Value = oDetalle[i].legajo;
                    objCommand.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                    objCommand.Parameters["@fecha_alta_registro"].Value = DateTime.Today.ToShortDateString();
                    objCommand.Parameters["@valor_concepto_liq"].Value = oDetalle[i].valor_concepto_liq;

                    if (Convert.ToDateTime(oDetalle[i].fecha_vto) <= Convert.ToDateTime("31/12/1900"))
                        objCommand.Parameters["@fecha_vto"].Value = oDetalle[i].fecha_alta_registro;
                    else
                        objCommand.Parameters["@fecha_vto"].Value = oDetalle[i].fecha_vto;

                    objCommand.Parameters["@usuario"].Value = oDetalle[i].usuario;
                    objCommand.CommandText = strSQL.ToString();
                    objCommand.Connection = conn;
                    objCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_1(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Clear();
            strSQL.AppendLine("SELECT A.cod_concepto_liq, A.valor_concepto_liq ");
            strSQL.AppendLine("FROM CONCEP_LIQUID_X_EMPLEADO A (NOLOCK)");
            strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION B on ");
            strSQL.AppendLine("A.cod_concepto_liq=B.cod_concepto_liq");
            strSQL.AppendLine("AND B.suma<>0 AND B.sujeto_a_desc<>0");
            strSQL.AppendLine("WHERE A.legajo=@legajo");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");


                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            dr.Close();
            cmd = null;
            return lstConceptos;

        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_2(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;
            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), isnull(A.valor_parametro,0) as valor_concepto_liq 
                        FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK),
                        CONCEPTOS_LIQUIDACION B 
                        WHERE A.anio=@anio
                        AND A.cod_tipo_liq=@cod_tipo_liq
                        AND A.nro_liquidacion=@nro_liquidacion
                        AND A.legajo=@legajo
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc<>0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        //seConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }
        //excluye los codigos 410,500,470
        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_3(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq FROM 
                        CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B 
                        WHERE A.legajo=@legajo
                        AND A.cod_concepto_liq not in (410,470,500)
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;

        }
        //solo los codigos 410,500,470
        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_Asignacion_fliar(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq FROM 
                        CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B 
                        WHERE A.legajo=@legajo
                        AND A.cod_concepto_liq
                        in (410,470,500)
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;

        }
        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_4(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), 
                        isnull(A.valor_parametro,0) as valor_concepto_liq 
                        FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK), 
                        CONCEPTOS_LIQUIDACION B
                        WHERE A.anio=@anio
                        AND A.cod_tipo_liq=@cod_tipo_liq
                        AND A.nro_liquidacion=@nro_liquidacion
                        AND A.legajo=@legajo
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = Convert.ToDecimal(dr.GetDecimal(valor_concepto_liq));
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;

        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_5(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;


            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq FROM 
                       CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B
                       WHERE A.legajo=@legajo
                       AND A.cod_concepto_liq=B.cod_concepto_liq
                       AND B.suma=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_6(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), 
                        isnull(A.valor_parametro,0) as valor_concepto_liq
                        FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK),
                        CONCEPTOS_LIQUIDACION B 
                        WHERE A.anio=@anio
                        AND A.cod_tipo_liq=@cod_tipo_liq
                        AND A.nro_liquidacion=@nro_liquidacion
                        AND A.legajo=@legajo
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        //dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;

        }
        
        
        
        
        /// <summary>
        /// //////////////////////////////////////////////////////////////
        //metodos para el calculo de conceptos de aguinaldo
        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_A1(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;

            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.Clear();
            strSQL.AppendLine("SELECT A.cod_concepto_liq, A.valor_concepto_liq FROM ");
            strSQL.AppendLine("CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B  ");
            strSQL.AppendLine("WHERE A.legajo=@legajo");
            strSQL.AppendLine("AND A.cod_concepto_liq=B.cod_concepto_liq");
            strSQL.AppendLine("AND B.suma=0 AND B.sac<>0");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_A11(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;
            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), isnull(A.valor_parametro,0) as valor_concepto_liq 
                        FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK),
                        CONCEPTOS_LIQUIDACION B 
                        WHERE A.anio=@anio
                        AND A.cod_tipo_liq=@cod_tipo_liq
                        AND A.nro_liquidacion=@nro_liquidacion
                        AND A.legajo=@legajo
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        //seConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_A10(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;
            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), isnull(A.valor_parametro,0) as valor_concepto_liq 
                        FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK),
                        CONCEPTOS_LIQUIDACION B 
                        WHERE A.anio=@anio
                        AND A.cod_tipo_liq=@cod_tipo_liq
                        AND A.nro_liquidacion=@nro_liquidacion
                        AND A.legajo=@legajo
                        AND A.cod_concepto_liq=B.cod_concepto_liq
                        AND B.suma<>0 AND B.sujeto_a_desc<>0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        //seConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_A2(int legajo, int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;


            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;
            //strSQL.AppendLine("SELECT DISTINCT(A.cod_concepto_liq), 0 as valor_concepto_liq ");
            strSQL = @"SELECT DISTINCT(A.cod_concepto_liq), isnull(A.valor_parametro,0) as valor_concepto_liq 
            FROM PAR_X_DET_LIQ_X_EMPLEADO A WITH (NOLOCK), 
            CONCEPTOS_LIQUIDACION B 
            WHERE A.anio=@anio
            AND A.cod_tipo_liq=@cod_tipo_liq
            AND A.nro_liquidacion=@nro_liquidacion
            AND A.legajo=@legajo
            AND A.cod_concepto_liq=B.cod_concepto_liq 
            AND B.suma=0";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        //dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;

        }

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_A3(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            //Este Metodo solo trae el codigo 816 que es descuento para los pasivisados 
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;
            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = string.Empty;
            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq FROM 
                       CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B
                       WHERE A.legajo=@legajo
                       AND A.cod_concepto_liq=B.cod_concepto_liq
                       AND B.suma=0 
                       AND A.cod_concepto_liq=816";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                    while (dr.Read())
                    {
                        eConcepto = new Entities.ConceptoLiqxEmp();
                        eConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        eConcepto.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                        lstConceptos.Add(eConcepto);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            dr.Close();
            cmd = null;
            return lstConceptos;
        }

    }




}
