using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Entities;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL
{
    public class Conceptos_liqD
    {

        #region Constructor
        /// <summary>
        /// Contructor de la clase sin parametro.
        /// </summary>



        public static List<Entities.Conceptos_Liq> findConcepto_liqByDescripcion(string descripcion)
        {
            Entities.Conceptos_Liq oConcepto = null;
            List<Entities.Conceptos_Liq> lst = new List<Entities.Conceptos_Liq>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();


            strSQL.AppendLine("select a.cod_concepto_liq, a.fecha_alta_registro, a.des_concepto_liq as concepto,");
            strSQL.AppendLine("b.des_tipo_concepto, a.cod_tipo_concepto, a.suma, a.sujeto_a_desc, a.sac, a.aporte, a.remunerativo ");
            strSQL.AppendLine("from CONCEPTOS_LIQUIDACION a");
            strSQL.AppendLine("join TIPOS_CONCEPTOS_LIQ b on");
            strSQL.AppendLine("a.cod_tipo_concepto = b.cod_tipo_concepto");
            strSQL.AppendLine("WHERE a.des_concepto_liq LIKE @descripcion");
            strSQL.AppendLine("ORDER BY a.cod_concepto_liq");

            cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@descripcion", "%" + descripcion + "%");


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

                    oConcepto = new Conceptos_Liq();

                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int cod_tipo_concepto = dr.GetOrdinal("cod_tipo_concepto");
                    int concepto = dr.GetOrdinal("concepto");
                    int fecha = dr.GetOrdinal("fecha_alta_registro");
                    int des_tipo_concepto = dr.GetOrdinal("des_tipo_concepto");
                    int suma = dr.GetOrdinal("suma");
                    int sujeto_a_desc = dr.GetOrdinal("sujeto_a_desc");
                    int sac = dr.GetOrdinal("sac");
                    int aporte = dr.GetOrdinal("aporte");
                    int remunerativo = dr.GetOrdinal("remunerativo");

                    if (!dr.IsDBNull(cod_concepto_liq)) oConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                    if (!dr.IsDBNull(concepto)) oConcepto.des_concepto_liq = dr.GetString(concepto);
                    if (!dr.IsDBNull(fecha)) oConcepto.Fecha_alta_registro = Convert.ToString(dr.GetDateTime(fecha));
                    if (!dr.IsDBNull(cod_tipo_concepto)) oConcepto.cod_tipo_concepto = dr.GetInt32(cod_tipo_concepto);
                    if (!dr.IsDBNull(des_tipo_concepto)) oConcepto.des_tipo_concepto = dr.GetString(des_tipo_concepto);
                    if (!dr.IsDBNull(suma)) oConcepto.suma = dr.GetBoolean(suma);
                    if (!dr.IsDBNull(sujeto_a_desc)) oConcepto.sujeto_a_desc = dr.GetBoolean(sujeto_a_desc);
                    if (!dr.IsDBNull(sac)) oConcepto.sac = dr.GetBoolean(sac);
                    if (!dr.IsDBNull(aporte)) oConcepto.aporte = dr.GetBoolean(aporte);
                    if (!dr.IsDBNull(remunerativo)) oConcepto.remunerativo = dr.GetBoolean(remunerativo);

                    lst.Add(oConcepto);
                }
                dr.Close();
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

        public static void NuevoConcepto(Conceptos_Liq oCon)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("INSERT into Conceptos_liquidacion");
                strSQL.AppendLine("(cod_concepto_liq,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("des_concepto_liq,");
                strSQL.AppendLine("cod_tipo_concepto,");
                strSQL.AppendLine("suma, sujeto_a_desc, sac, aporte, remunerativo)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@cod_concepto_liq,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@des_concepto_liq,");
                strSQL.AppendLine("@cod_tipo_concepto,");
                strSQL.AppendLine("@suma, @sujeto_a_desc, @sac, @aporte, @remunerativo)");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oCon.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@fecha_alta_registro", oCon.Fecha_alta_registro);
                cmd.Parameters.AddWithValue("@des_concepto_liq", oCon.des_concepto_liq);
                cmd.Parameters.AddWithValue("@cod_tipo_concepto", oCon.cod_tipo_concepto);
                cmd.Parameters.AddWithValue("@suma", oCon.suma);
                cmd.Parameters.AddWithValue("@sujeto_a_desc", oCon.sujeto_a_desc);
                cmd.Parameters.AddWithValue("@sac", oCon.sac);
                cmd.Parameters.AddWithValue("@aporte", oCon.aporte);
                cmd.Parameters.AddWithValue("@remunerativo", oCon.remunerativo);

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                cmd = null;
                cn.Close();
            }
        }

        public static void ModificaConcepto(Conceptos_Liq oCon)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("update Conceptos_liquidacion set");
                strSQL.AppendLine("des_concepto_liq=@des_concepto_liq,");
                strSQL.AppendLine("cod_tipo_concepto=@cod_tipo_concepto,");
                strSQL.AppendLine("suma=@suma, sujeto_a_desc=@sujeto_a_desc, sac=@sac, aporte=@aporte, remunerativo=@remunerativo");
                strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oCon.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@des_concepto_liq", oCon.des_concepto_liq);
                cmd.Parameters.AddWithValue("@cod_tipo_concepto", oCon.cod_tipo_concepto);
                cmd.Parameters.AddWithValue("@suma", oCon.suma);
                cmd.Parameters.AddWithValue("@sujeto_a_desc", oCon.sujeto_a_desc);
                cmd.Parameters.AddWithValue("@sac", oCon.sac);
                cmd.Parameters.AddWithValue("@aporte", oCon.aporte);
                cmd.Parameters.AddWithValue("@remunerativo", oCon.remunerativo);

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                cmd = null;
                cn.Close();
            }
        }

        private static List<Entities.Conceptos_Liq> getLstConceptos_liq(SqlCommand cmd)
        {
            List<Entities.Conceptos_Liq> lst = new List<Entities.Conceptos_Liq>();
            Entities.Conceptos_Liq oConcepto;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                //strSQL.AppendLine("select a.cod_concepto_liq, a.des_concepto_liq as concepto");
                //strSQL.AppendLine("b.des_tipo_concepto, a.suma, a.sujeto_a_desc, a.sac ");
                if (dr.HasRows)
                {
                    int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                    int concepto = dr.GetOrdinal("concepto");
                    int fecha = dr.GetOrdinal("fecha_alta_registro");
                    int des_tipo_concepto = dr.GetOrdinal("des_tipo_concepto");
                    int suma = dr.GetOrdinal("suma");
                    int sujeto_a_desc = dr.GetOrdinal("sujeto_a_desc");
                    int sac = dr.GetOrdinal("sac");
                    int aporte = dr.GetOrdinal("aporte");
                    int remunerativo = dr.GetOrdinal("remunerativo");
                    while (dr.Read())
                    {
                        oConcepto = new Conceptos_Liq();
                        if (!dr.IsDBNull(cod_concepto_liq)) oConcepto.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                        if (!dr.IsDBNull(concepto)) oConcepto.des_concepto_liq = dr.GetString(concepto);
                        if (!dr.IsDBNull(fecha)) oConcepto.Fecha_alta_registro = Convert.ToString(dr.GetDateTime(fecha));
                        if (!dr.IsDBNull(des_tipo_concepto)) oConcepto.des_tipo_concepto = dr.GetString(des_tipo_concepto);
                        if (!dr.IsDBNull(suma)) oConcepto.suma = dr.GetBoolean(suma);
                        if (!dr.IsDBNull(sujeto_a_desc)) oConcepto.sujeto_a_desc = dr.GetBoolean(sujeto_a_desc);
                        if (!dr.IsDBNull(sac)) oConcepto.sac = dr.GetBoolean(sac);
                        if (!dr.IsDBNull(aporte)) oConcepto.aporte = dr.GetBoolean(aporte);
                        if (!dr.IsDBNull(remunerativo)) oConcepto.remunerativo = dr.GetBoolean(remunerativo);
                        lst.Add(oConcepto);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            return lst;
        }

        public static List<Entities.Conceptos_Liq> GetConceptos_liq()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {
                strSQL.AppendLine("select a.cod_concepto_liq, a.fecha_alta_registro, a.des_concepto_liq as concepto,");
                strSQL.AppendLine("b.des_tipo_concepto, a.suma, a.sujeto_a_desc, a.sac, a.aporte, a.remunerativo ");
                strSQL.AppendLine("from CONCEPTOS_LIQUIDACION a");
                strSQL.AppendLine("join TIPOS_CONCEPTOS_LIQ b on");
                strSQL.AppendLine("a.cod_tipo_concepto = b.cod_tipo_concepto");
                strSQL.AppendLine("ORDER BY a.cod_concepto_liq");

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstConceptos_liq(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static List<Entities.DetalleCptoEmp> FindDetalleCptoEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq)
        {
            Entities.DetalleCptoEmp oDetalle = null;
            List<Entities.DetalleCptoEmp> lst = new List<Entities.DetalleCptoEmp>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select a.cod_concepto_liq, a.des_concepto_liq as concepto,");
            strSQL.AppendLine("p.legajo, e.nombre, p.nro_parametro, p.valor_parametro ");
            strSQL.AppendLine("From PAR_X_DET_LIQ_X_EMPLEADO p");
            strSQL.AppendLine("join CONCEPTOS_LIQUIDACION a on");
            strSQL.AppendLine("p.cod_concepto_liq = a.cod_concepto_liq");
            strSQL.AppendLine("join EMPLEADOS e on");
            strSQL.AppendLine("p.legajo = e.legajo");
            strSQL.AppendLine("Where p.anio=@anio");
            strSQL.AppendLine("and p.cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine("and p.nro_liquidacion=@nro_liquidacion");
            strSQL.AppendLine("and p.cod_concepto_liq=@cod_concepto_liq");
            strSQL.AppendLine("ORDER BY e.legajo");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
            cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
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
                    oDetalle = new DetalleCptoEmp();
                    int cod_concepto_liq1 = dr.GetOrdinal("cod_concepto_liq");
                    int concepto = dr.GetOrdinal("concepto");
                    int legajo = dr.GetOrdinal("legajo");
                    int nombre = dr.GetOrdinal("nombre");
                    int nro = dr.GetOrdinal("nro_parametro");
                    int valor = dr.GetOrdinal("valor_parametro");

                    if (!dr.IsDBNull(cod_concepto_liq1)) oDetalle.cod_concepto_liq = dr.GetInt32(cod_concepto_liq1);
                    if (!dr.IsDBNull(concepto)) oDetalle.concepto = dr.GetString(concepto);
                    if (!dr.IsDBNull(legajo)) oDetalle.legajo = dr.GetInt32(legajo);
                    if (!dr.IsDBNull(nombre)) oDetalle.nombre = dr.GetString(nombre);
                    if (!dr.IsDBNull(nro)) oDetalle.nro_parametro = dr.GetInt32(nro);
                    if (!dr.IsDBNull(valor)) oDetalle.valor_parametro = dr.GetDecimal(valor);
                    lst.Add(oDetalle);
                }
                dr.Close();
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

        public static List<Entities.DetalleCptoEmp> FindDetalleCptoEmpByPk(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, int legajo)
        {
            Entities.DetalleCptoEmp oDetalle = null;
            List<Entities.DetalleCptoEmp> lst = new List<Entities.DetalleCptoEmp>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            string strSQL = @"SELECT a.cod_concepto_liq, a.des_concepto_liq as concepto,
                              p.legajo, e.nombre, p.nro_parametro, p.valor_parametro
                              From PAR_X_DET_LIQ_X_EMPLEADO p
                              join CONCEPTOS_LIQUIDACION a on
                              p.cod_concepto_liq = a.cod_concepto_liq
                              join EMPLEADOS e on
                              p.legajo = e.legajo
                              Where p.anio = @anio
                              and p.cod_tipo_liq = @cod_tipo_liq
                              and p.nro_liquidacion = @nro_liquidacion
                              and p.cod_concepto_liq = @cod_concepto_liq
                              and p.legajo = @legajo
                              ORDER BY e.legajo";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
            cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
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

                    oDetalle = new DetalleCptoEmp();

                    int cod_concepto_liq1 = dr.GetOrdinal("cod_concepto_liq");
                    int concepto = dr.GetOrdinal("concepto");
                    int legajo_ = dr.GetOrdinal("legajo");
                    int nombre = dr.GetOrdinal("nombre");
                    int nro = dr.GetOrdinal("nro_parametro");
                    int valor = dr.GetOrdinal("valor_parametro");


                    if (!dr.IsDBNull(cod_concepto_liq1)) oDetalle.cod_concepto_liq = dr.GetInt32(cod_concepto_liq1);
                    if (!dr.IsDBNull(concepto)) oDetalle.concepto = dr.GetString(concepto);
                    if (!dr.IsDBNull(legajo_)) oDetalle.legajo = dr.GetInt32(legajo_);
                    if (!dr.IsDBNull(nombre)) oDetalle.nombre = dr.GetString(nombre);
                    if (!dr.IsDBNull(nro)) oDetalle.nro_parametro = dr.GetInt32(nro);
                    if (!dr.IsDBNull(valor)) oDetalle.valor_parametro = dr.GetDecimal(valor);
                    lst.Add(oDetalle);
                }
                dr.Close();
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

        public static Entities.Conceptos_Liq GetByPk(int cod_concepto_liq)
        {
            Entities.Conceptos_Liq obj = new Conceptos_Liq();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();



            strSQL.AppendLine("select a.cod_concepto_liq, a.fecha_alta_registro, a.des_concepto_liq,");
            strSQL.AppendLine("b.des_tipo_concepto, a.cod_tipo_concepto, a.suma, a.sujeto_a_desc, a.sac, a.aporte, a.remunerativo ");
            strSQL.AppendLine("from CONCEPTOS_LIQUIDACION a");
            strSQL.AppendLine("join TIPOS_CONCEPTOS_LIQ b on");
            strSQL.AppendLine("a.cod_tipo_concepto = b.cod_tipo_concepto");
            strSQL.AppendLine("WHERE a.cod_concepto_liq = @cod_concepto_liq");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", cod_concepto_liq));

            try
            {
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    while (dr.Read())
                    {
                        obj = new Conceptos_Liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_concepto_liq")))
                            obj.cod_concepto_liq = dr.GetInt32((dr.GetOrdinal("cod_concepto_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("des_concepto_liq")))
                            obj.des_concepto_liq = dr.GetString((dr.GetOrdinal("des_concepto_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_concepto")))
                            obj.cod_tipo_concepto = dr.GetInt32((dr.GetOrdinal("cod_tipo_concepto")));

                        if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_concepto")))
                            obj.des_tipo_concepto = dr.GetString((dr.GetOrdinal("des_tipo_concepto")));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                            obj.Fecha_alta_registro = Convert.ToString((dr.GetOrdinal("fecha_alta_registro")));

                        if (!dr.IsDBNull(dr.GetOrdinal("suma")))
                            obj.suma = dr.GetBoolean((dr.GetOrdinal("suma")));

                        if (!dr.IsDBNull(dr.GetOrdinal("sujeto_a_desc")))
                            obj.sujeto_a_desc = dr.GetBoolean((dr.GetOrdinal("sujeto_a_desc")));

                        if (!dr.IsDBNull(dr.GetOrdinal("sac")))
                            obj.sac = dr.GetBoolean((dr.GetOrdinal("sac")));

                        if (!dr.IsDBNull(dr.GetOrdinal("aporte")))
                            obj.aporte = dr.GetBoolean((dr.GetOrdinal("aporte")));

                        if (!dr.IsDBNull(dr.GetOrdinal("remunerativo")))
                            obj.remunerativo = dr.GetBoolean((dr.GetOrdinal("remunerativo")));
                    }
                    dr.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; strSQL = null; }

            return obj;
        }

        public static decimal Buscar_dias_trabajados(Entities.LstEmpleados oEmpleado, int cod_concepto_liq, int anio, int cod_tipo_liq,
            int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            Entities.Resultado eResultado = new Resultado();
            decimal dias = 0;
            string strSQL;
            SqlCommand cmd;
            SqlDataReader dr;
            try
            {
                //10-SUELDO BASICO
                if (cod_concepto_liq == 10)
                {
                    strSQL = @"SELECT * 
                                FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)
                                WHERE anio=@anio
                                AND cod_tipo_liq=@cod_tipo_liq
                                AND nro_liquidacion=@nro_liquidacion
                                AND legajo=@legajo
                                AND cod_concepto_liq=@cod_concepto_liq";
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dias = Decimal.Round(dr.GetDecimal(dr.GetOrdinal("valor_parametro")), 2);
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dias;
        }

        public static Entities.Resultado Calcula_Concepto(Entities.LstEmpleados oEmpleado, decimal sasd,
          int cod_concepto_liq, int anio, int cod_tipo_liq, int nro_liquidacion, string fecha_liquidacion,
          decimal valor_concepto_liq, SqlConnection cn, SqlTransaction trx)
        {

            decimal importe_concepto = 0;
            decimal imp_cat_agente = 0;
            decimal imp_cat_8 = 0;
            decimal basico_cat_8 = 0;
            decimal antiguedad = 0;
            decimal valor_1 = 0;
            decimal valor_2 = 0;
            decimal valor = 0;
            int nro_valor = 0;
            decimal sasd_aux = 0; //Corresponde al sasd pero sin el aguinaldo
            decimal valor_parametro = 0;
            decimal sbruto = 0;
            //
            Entities.Resultado eResultado = new Resultado();
            //
            StringBuilder strSQL = new StringBuilder();
            //
            SqlCommand cmd;
            SqlCommand cmd1;
            SqlCommand cmd2;
            //SqlCommand cmd3;
            SqlDataReader dr;

            try
            {
                //11-DESCUENTO SUELDO BASICO
                if (cod_concepto_liq == 11)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine(" WHERE anio=@anio");
                    strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                            eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                            eResultado.resultado_2 = 1;
                        }
                    }
                    dr.Close();
                }
                ///////////////////////////////////////////////////////////////////////////
                //20-ANTIGUEDAD
                //
                if (cod_concepto_liq == 20)
                {
                    valor_1 = 0;
                    valor_2 = 0;
                    imp_cat_8 = 0;
                    importe_concepto = 0;
                    imp_cat_agente = 0;

                    //1•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE cod_concepto_liq=20 AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_1 = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    //2•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE cod_concepto_liq=20 AND nro_valor=2");
                    cmd = new SqlCommand();
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_2 = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    //3•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM CATEGORIAS WITH (NOLOCK) WHERE cod_categoria=8");
                    cmd = new SqlCommand();
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (oEmpleado.dias_trabajados != 30)
                            {
                                basico_cat_8 = dr.GetDecimal(dr.GetOrdinal("sueldo_basico"));
                                basico_cat_8 = Decimal.Round(basico_cat_8 / 30 * oEmpleado.dias_trabajados, 2);
                            }
                            else
                                basico_cat_8 = dr.GetDecimal(dr.GetOrdinal("sueldo_basico"));
                        }
                    }
                    dr.Close();
                    imp_cat_agente = oEmpleado.sueldo_basico * valor_1 / 100;
                    imp_cat_8 = basico_cat_8 * valor_2 / 100;
                    importe_concepto = imp_cat_agente + imp_cat_8;

                    antiguedad = DALBase.Antiguedad(Convert.ToDateTime(oEmpleado.fecha_ingreso), Convert.ToDateTime(fecha_liquidacion)) + oEmpleado.antiguedad_ant;
                    importe_concepto = Decimal.Round(importe_concepto * antiguedad, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = antiguedad;
                }
                /////////////////////////////////////////////////////////////////////////////////
                //16-AGUINALDO PERIODO ANTERIOR
                //17-AGUINALDO PROPORCIONAL
                //18-Pago Anquinaldo Proporcional (1/6)
                //19-Aguinaldo proporcional segundo semestre 2002
                //151-Adicional x guardia y se comporta igual que estos codigos
                //se paga pero no se tiene en cta p/el aguinaldo
                //177-Guardias Medicas


                if (cod_concepto_liq == 16 || cod_concepto_liq == 17 || cod_concepto_liq == 18 ||
                  cod_concepto_liq == 19 || cod_concepto_liq == 151 || cod_concepto_liq == 177)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                            eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                            eResultado.resultado_2 = 1;
                        }
                    }
                    dr.Close();
                }
                //////////////////////////////////////////////////////////////////////////
                //158-pago extraordinario
                //164-Bonificacion x Manejo de Valores
                //no lleva aporte, y no se tiene en cta p/el aguinaldo
                if (cod_concepto_liq == 158 || cod_concepto_liq == 164)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=@nro_valor");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Parameters.AddWithValue("@nro_valor", valor_parametro);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;

                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = valor;
                    eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_parametro;
                }
                ///////////////////////////////////////////////////////////////////////////

                //163-Adicional / Area Servicios

                if (cod_concepto_liq == 163)
                {
                    nro_valor = 0;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor = A.valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO A with (nolock) ");
                    strSQL.AppendLine("WHERE A.anio=@anio");
                    strSQL.AppendLine(" AND A.cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND A.nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND A.legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            nro_valor = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("valor")));
                        }
                    }
                    dr.Close();
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=@nro_valor");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Parameters.AddWithValue("@nro_valor", nro_valor);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;

                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = nro_valor;
                }
                ////////////////////////////////////////////////////////////////////////////////
                //165-Pago Festival
                //561 Reintegro IMP GANANCIA
                if (cod_concepto_liq == 165 || cod_concepto_liq == 561)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    importe_concepto = valor_parametro;
                    eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = 1;
                }
                /////////////////////////////////////////////////////////////////////////////////////
                //60-responsabilidad jerárquica
                //140-tarea insalubre
                if (cod_concepto_liq == 60 || cod_concepto_liq == 140)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = (oEmpleado.sueldo_basico * valor) / 100;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor;
                }
                /////////////////////////////////////////////////////////////////
                //50-título,
                //70-,
                //77-Adicional Esp Servicio Publico,
                //149 RSU
                //150 Funcion tecnica
                //154 Adicional Juzgado de Faltas
                //156
                //157
                //159 Adicional Area Alumbrado Publico
                //161 Adicional Mesa de Entrada
                //162 Adicional Area Demva
                //160-quebranto caja.
                //170 agregado 28/05/2014
                //173
                //183
                //184
                //175 Adicional Tarea Docente
                //178 Ad Direcc Ad y Finan
                //179 Ad Cocinero
                //180 Ad Chofer

                //176-Adic Remunerativo                
                if (cod_concepto_liq == 50 || cod_concepto_liq == 70 || cod_concepto_liq == 77 || cod_concepto_liq == 149 || cod_concepto_liq == 150 ||
                    cod_concepto_liq == 154 || cod_concepto_liq == 156 || cod_concepto_liq == 157 || cod_concepto_liq == 160 || cod_concepto_liq == 173 ||
                    cod_concepto_liq == 161 || cod_concepto_liq == 162 || cod_concepto_liq == 159 || cod_concepto_liq == 166 || cod_concepto_liq == 170 ||
                    cod_concepto_liq == 175 || cod_concepto_liq == 176 || cod_concepto_liq == 178 || cod_concepto_liq == 179 || cod_concepto_liq == 180 ||
                    cod_concepto_liq == 183 || cod_concepto_liq == 184)
                {
                    importe_concepto = (oEmpleado.sueldo_basico * valor_concepto_liq) / 100;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_concepto_liq;
                }

                //491-Adic No Remunerativo
                //if (cod_concepto_liq == 491)
                //{
                //    sql = @"SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) 
                //            WHERE anio=@anio
                //            AND cod_tipo_liq=@cod_tipo_liq
                //            AND nro_liquidacion=@nro_liquidacion
                //            AND legajo=@legajo
                //            AND cod_concepto_liq=@cod_concepto_liq
                //            AND nro_parametro=1";
                //    cmd = new SqlCommand();
                //    cmd.Parameters.AddWithValue("@anio", anio);
                //    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                //    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                //    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                //    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                //    cmd.CommandText = sql;
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Connection = cn;


                //    cmd.Transaction = trx;
                //    dr = cmd.ExecuteReader();

                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                //        }
                //    }
                //    dr.Close();
                //    importe_concepto = decimal.Round(valor_parametro);
                //    eResultado.resultado_1 = importe_concepto;
                //    eResultado.resultado_2 = 1;
                //}

                //Octubre 2022 cuando se fue Nora
                //491-Adic No Remunerativo                
                //20/04/2024
                //71 a cuenta de futuros aumentos, a este concepto lo saco de aca
                //Julio 2024
                //499-Adicional no remunerativo
                //se calculo con procentaje del sueldo bruto
                if (cod_concepto_liq == 491 ||
                    cod_concepto_liq == 499)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK)  ");
                    strSQL.AppendLine("WHERE cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = (oEmpleado.sueldo_bruto * dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq")) / 100);
                            valor_concepto_liq = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto);
                    eResultado.resultado_2 = valor_concepto_liq;
                }
                //21/04/2024
                //71 a cuenta de futuros aumentos
                //pruebo aca
                if (cod_concepto_liq == 71)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK)  ");
                    strSQL.AppendLine("WHERE cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = (oEmpleado.sueldo_basico * dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq")) / 100);
                            valor_concepto_liq = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto);
                    eResultado.resultado_2 = valor_concepto_liq;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //174 Responsabilidad de Firma
                if (cod_concepto_liq == 174)
                {
                    importe_concepto = (oEmpleado.sueldo_basico * valor_concepto_liq) / 100;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_concepto_liq;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //176 Adic Remunerativo
                //21/11/2018
                //if (cod_concepto_liq == 176)
                //{
                //  strSQL.Clear();
                //  strSQL.AppendLine("SELECT * FROM CONCEP_LIQUID_X_EMPLEADO WITH (NOLOCK) ");
                //  strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                //  strSQL.AppendLine("AND legajo=@legajo");
                //  cmd = new SqlCommand();
                //  cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                //  cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                //  cmd.CommandText = strSQL.ToString();
                //  cmd.CommandType = CommandType.Text;
                //  cmd.Connection = cn;
                //  cmd.Transaction = trx;

                //  dr = cmd.ExecuteReader();

                //  if (dr.HasRows)
                //  {
                //    while (dr.Read())
                //    {
                //      valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                //    }
                //  }
                //  dr.Close();
                //  importe_concepto = Decimal.Round(valor_parametro, 2);
                //  eResultado.resultado_1 = importe_concepto;
                //  eResultado.resultado_2 = 1;
                //}
                /////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////

                //61 Diferencia de Antiguedad Ant.
                //63 SUBROGANCIA
                //64 Diferencia Licencia x Imp
                //65 Diferencia de meses anteriores
                //72
                //73
                //75 Diferencia Sueldo por cambio de Cargo
                //76
                //73 Diferencia Sueldo por cambio de Cargo Consejo Deliberante
                //74 Diferencia Sueldo por cambio de Cargo Tribunal de Cuenta

                //489-Adic No Remunerativo
                //490-Adic No Remunerativo Paritaria 2022
                //487-Bono extraordinario Noviembre 2022 de 20000 pesos
                //486-MONTO ADICIONAL
                //69-
                if (cod_concepto_liq == 61 || cod_concepto_liq == 62 || cod_concepto_liq == 64 || cod_concepto_liq == 65 || cod_concepto_liq == 69 ||
                   cod_concepto_liq == 72 || cod_concepto_liq == 73 || cod_concepto_liq == 74 || cod_concepto_liq == 75 ||
                  cod_concepto_liq == 76 || cod_concepto_liq == 486 || cod_concepto_liq == 487 || cod_concepto_liq == 489 || cod_concepto_liq == 490)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor_parametro);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = 1;

                }
                /////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////
                //63 SUBROGANCIA
                //Es un porcentaje del Basico del Empleado
                //71 A cta de Futuros aumentos aca tampoco va
                if (cod_concepto_liq == 63)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    importe_concepto = (oEmpleado.sueldo_basico * valor_parametro) / 100;
                    eResultado.resultado_1 = Decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_parametro;
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                //66-diferencia de licencia
                //67-diferencia
                if (cod_concepto_liq == 66 || cod_concepto_liq == 67)
                {
                    //1•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=10");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@anio", anio);
                    cmd1.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd1.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd1.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    //2•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    //importe_concepto = ((oEmpleado.sueldo_basico / 20) - (oEmpleado.sueldo_basico / 30)) * valor_parametro;
                    ///////////////////////////////////////////////////////////////////////////////
                    /////15/04/2024
                    //basico del empleado/ cant.dias.informados
                    //importe_concepto = (((valor * 30) / 20) - ((valor * 30) / 30)) * valor_parametro;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    //11/06/2024
                    valor_1 = oEmpleado.sueldo_basico / valor * 30;
                    importe_concepto = ((valor_1 / 20) - (valor_1 / 30)) * valor_parametro;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_parametro;
                }
                ///////////////////////////////////////////////////////////////////////
                //68-Licencia no tomada
                if (cod_concepto_liq == 68)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    //importe_concepto = (oEmpleado.sueldo_basico / 30) * valor_parametro;
                    importe_concepto = (sasd / 20) * valor_parametro;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor_parametro;
                }
                ///////////////////////////////////////////////////////////////////////////
                /////12 Adic Basico 2013
                /////153-ADICIONAL ESP.VIATICO
                /////155-Adicional Referente de Area
                /////167-Adicional Protocolo
                if (cod_concepto_liq == 12 || cod_concepto_liq == 153 || cod_concepto_liq == 155 || cod_concepto_liq == 167)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK) WHERE ");
                    strSQL.AppendLine("cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                            //18/05/2023
                            if (oEmpleado.dias_trabajados != 30)
                            {
                                importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                                importe_concepto = Decimal.Round(importe_concepto / 30 * oEmpleado.dias_trabajados, 2);
                            }
                            else
                                importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto);
                    eResultado.resultado_2 = 1;
                }
                //300-DIAS DE DESCUENTO
                //310-días de suspensión
                //311-descuento decreto remunerativo 63/20 
                //320-descuento decreto remunerativo 63/20 (descuenta monto_fijo)
                //535-Licecnia no gozadas
                if (cod_concepto_liq == 300 || cod_concepto_liq == 310 || cod_concepto_liq == 311 || cod_concepto_liq == 535)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    importe_concepto = valor_parametro;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = 1;
                }
                ////////////////////////////////////////////////////////////////////////////////////
                //400-salario familiar.
                //410-asig.hijos
                //470-asig.hijos discapacitado
                //500-asig.prenatal
                //decimal otrosConceptos = 0;
                //if (cod_concepto_liq == 491)
                //{
                //    otrosConceptos += eResultado.resultado_1;
                //}
                //if (cod_concepto_liq == 482)
                //{
                //    otrosConceptos += eResultado.resultado_1;
                //}
                if (cod_concepto_liq == 400 || cod_concepto_liq == 410 || cod_concepto_liq == 470 || cod_concepto_liq == 500)
                {
                    nro_valor = 0;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq between 16 and 19");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();

                    if (valor_parametro > 0)
                    {
                        sasd_aux = sasd - valor_parametro;
                    }
                    else
                        sasd_aux = sasd;

                    /***************************************************************************/
                    //Aca sumo los conceptos no remunerativos hs extras etc los que estan cargados en el 
                    //Formulario 931 para Afip.
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq in(151,153,155,490)");
                    strSQL.AppendLine("UNION");
                    strSQL.AppendLine("SELECT ISNULL(valor_concepto_liq * " + oEmpleado.sueldo_basico + " / 100, 0)");
                    strSQL.AppendLine("FROM CONCEP_LIQUID_X_EMPLEADO");
                    strSQL.AppendLine("WHERE legajo=@legajo");
                    //24/07/2024
                    strSQL.AppendLine("AND cod_concepto_liq in(167,482,491,499)");

                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);

                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro += dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    if (valor_parametro > 0)
                    {
                        sbruto = valor_parametro;
                    }
                    else
                        sbruto = 0;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq in(151,153,155,482,490, 491, 499)");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();

                    /*importe_concepto = (oEmpleado.sueldo_basico * valor_concepto_liq) / 100;*/
                    /*22/10/2021*/
                    /*Nuevo Sueldo bruto mas codigos 151/153/155/167/482/491, 499*/
                    sasd_aux += sbruto;
                    //--------------------------------------------------------------------------
                    //    'Busco el si el sueldobruto esta dentro de las escalas de las
                    //    'asignaciones fliares
                    //    'si es asi cobra asignacion sino a llorar
                    //--------------------------------------------------------------------------
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    //23/12/2022
                    //Ahora lo saca en forma dinamica
                    //
                    sasd_aux = sasd;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(nro_valor,0) as nro_valor ");
                    strSQL.AppendLine("FROM escalas_asignaciones_fliar");
                    strSQL.AppendLine(" WHERE activo=1 AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND anio=@anio");
                    strSQL.AppendLine(" AND @sasd_aux");
                    strSQL.AppendLine(" BETWEEN valor_desde AND valor_hasta");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Parameters.AddWithValue("@anio", anio);
                    cmd1.Parameters.AddWithValue("@sasd_aux", sasd_aux);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            nro_valor = dr.GetInt16(dr.GetOrdinal(("nro_valor")));
                        }
                    }
                    dr.Close();
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=@nro_valor");
                    cmd2 = new SqlCommand();
                    cmd2.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd2.Parameters.AddWithValue("@nro_valor", nro_valor);
                    cmd2.CommandText = strSQL.ToString();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = cn;
                    cmd2.Transaction = trx;
                    dr = cmd2.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor * valor_concepto_liq, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = valor_concepto_liq;
                }

                ///////////////////////////////////////////////////////////////////////     
                //17-pagar Sac1 en el sueldo
                //18-pagar Sac2 en el sueldo
                //386-Sueldo Anual Complementario
                if (cod_concepto_liq == 17 || cod_concepto_liq == 18 || cod_concepto_liq == 386)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro ");
                    strSQL.AppendLine("FROM par_x_det_liq_x_empleado WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor_parametro, 2);
                    eResultado.resultado_2 = 1;
                }

                //'420-salario familiar.
                //'430-salario familiar.
                //'431-salario familiar.
                //'440-salario familiar.
                //'460-salario familiar.
                //'471-ASIG.HIJO INCAP.
                //'490-AYUDA ESC.PRIMARIA 4-5-6 ==> ahora es adic no remuner paritaria 2022
                //'510-ASIG.ANUAL COMPLEMENTARIA
                //'520-LICENCIA MATERNIDAD
                //'540-SAL.FAM.IMPAGO MES ANT.
                //'545-BONIF.P/RETIRO VOL/
                //'550-INDEMN.POR ANT.
                //'560-VIATICOS
                //'570-ASIG.POR CASAMIENTO.
                //'580-salario familiar nacimiento
                //'590-ASIGNACION POR NACIMIENTO.
                //'625-626
                //'628-APROSS APORTES ATRASADOS
                //'496-Reintegro de mas
                //'482-Reintegro Capacitacion
                if (cod_concepto_liq == 420 || cod_concepto_liq == 430 || cod_concepto_liq == 431 || cod_concepto_liq == 440 ||
                  cod_concepto_liq == 460 || cod_concepto_liq == 471 || cod_concepto_liq == 510 || cod_concepto_liq == 482 ||
                  cod_concepto_liq == 520 || cod_concepto_liq == 540 || cod_concepto_liq == 545 || cod_concepto_liq == 550 ||
                  cod_concepto_liq == 560 || cod_concepto_liq == 570 || cod_concepto_liq == 590 || cod_concepto_liq == 580 ||
                  cod_concepto_liq == 625 || cod_concepto_liq == 626 || cod_concepto_liq == 628 ||
                  cod_concepto_liq == 496 || cod_concepto_liq == 541)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("select valor_parametro from par_x_det_liq_x_empleado WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor_parametro, 2);
                    eResultado.resultado_2 = 1;

                }

                //605-retención de jubilación
                //606-Aporte fondo de actividad recreativas
                //607-JUB.APORTES PER.s/SIPA                                                          
                //608-JUB.APORTE PER.COMP.s/REG.CBA.                                                  
                //621-retención ipam, 622 retención ipam,
                //635-soemva, 636-soemva, 650-soemva

                if (cod_concepto_liq == 605 || cod_concepto_liq == 606 || cod_concepto_liq == 607 || cod_concepto_liq == 608 ||
                    cod_concepto_liq == 621 || cod_concepto_liq == 622 || cod_concepto_liq == 635 ||
                    cod_concepto_liq == 636 || cod_concepto_liq == 650)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) WHERE ");
                    strSQL.AppendLine(" cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(sasd * valor / 100, 2);
                    eResultado.resultado_2 = valor;
                }

                //609-JUB.Aporte Obligatorio (Inc e) Art 9 Ley 10724
                if (cod_concepto_liq == 609)
                {
                    valor = 0;
                    ////////////////////////////////////////////////////
                    ///
                    sasd_aux = sasd;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(porcentaje,0) as porcentaje ");
                    strSQL.AppendLine("FROM JUBILACION_APORTE_OBLIGATORIO");
                    strSQL.AppendLine(" WHERE activo=1");
                    strSQL.AppendLine(" AND anio=@anio");
                    strSQL.AppendLine(" AND @sasd_aux");
                    strSQL.AppendLine(" BETWEEN valor_desde AND valor_hasta");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@anio", anio);
                    cmd1.Parameters.AddWithValue("@sasd_aux", sasd_aux);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal(("porcentaje")));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(sasd_aux * valor / 100, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = valor;
                }

                //620-OBRA SOCIAL APROSS
                if (cod_concepto_liq == 620)
                {
                    valor = 0;
                    valor_1 = 0;
                    valor_2 = 0;
                    importe_concepto = 0;
                    valor_parametro = 0;
                    //1•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) WHERE ");
                    strSQL.AppendLine(" cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    //2•
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq AND nro_valor=2");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_2 = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    //////////////////////////////////////////////////////////////////////////////
                    valor_1 = sasd * valor / 100;

                    if (valor_1 > valor_2)
                        importe_concepto = valor_1;
                    else
                        importe_concepto = valor_2;
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = valor;
                }

                //691-Seguro itt
                if (cod_concepto_liq == 691)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) WHERE ");
                    strSQL.AppendLine(" cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor, 2);
                    eResultado.resultado_2 = 1;
                }

                //694-Seguros carusso
                if (cod_concepto_liq == 694)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT * FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK) WHERE ");
                    strSQL.AppendLine("cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto, 2);
                    eResultado.resultado_2 = 1;
                }
                //611-JUB.APORTES ATRASADOS
                //693-Seguros galicia
                if (cod_concepto_liq == 611 || cod_concepto_liq == 693)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro from par_x_det_liq_x_empleado WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor_parametro, 2);
                    eResultado.resultado_2 = 1;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////
                //'Se comento 05/04/2011
                //'18/10/2022
                //488 ADICIONAL EXTRAOD COVID19
                //492 Incentivo Plan Sumar
                //493
                //494
                //497-Adicional no remunerativo mes anterior
                if (cod_concepto_liq == 488 ||
                    cod_concepto_liq == 492 || cod_concepto_liq == 493 ||
                    cod_concepto_liq == 494 || cod_concepto_liq == 497)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK) WHERE ");
                    strSQL.AppendLine("cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto);
                    eResultado.resultado_2 = 1;
                }


                //481 Dto no remunerativo decreto 63/20
                //495 no remunerativo
                //411 Complemento Salarial Familiar
                if (cod_concepto_liq == 411 || cod_concepto_liq == 481 || cod_concepto_liq == 495)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine(" WHERE anio=@anio");
                    strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor_parametro);
                    eResultado.resultado_2 = 1;
                }

                //110-asistencia perfecta, 120-Puntualidad, 121-Media puntualidad
                //152-Tarea Riesgosa
                if (cod_concepto_liq == 110 || cod_concepto_liq == 120 || cod_concepto_liq == 121 || cod_concepto_liq == 152)
                {
                    strSQL.Clear();
                    //22/05/2019 Comento este query pq ahora lo calvular <>
                    //strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) WHERE ");
                    //strSQL.AppendLine(" cod_concepto_liq=@cod_concepto_liq");
                    //strSQL.AppendLine(" AND nro_valor=1");
                    if (cod_concepto_liq == 110 || cod_concepto_liq == 120 || cod_concepto_liq == 121)
                    {
                        strSQL.AppendLine("SELECT sueldo_basico as valor ");
                        strSQL.AppendLine("FROM categorias WITH (NOLOCK) ");
                        strSQL.AppendLine("WHERE cod_categoria=1");
                    }
                    else
                    {
                        strSQL.AppendLine("SELECT sueldo_basico as valor ");
                        strSQL.AppendLine("FROM categorias WITH (NOLOCK) ");
                        strSQL.AppendLine("WHERE cod_categoria=8");
                    }
                    cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //************************17/05/2023******************************
                            //Antes calculaba sin dividir los dias trabajados
                            //valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                            if (oEmpleado.dias_trabajados != 30)
                            {
                                valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                                valor = Decimal.Round(valor / 30 * oEmpleado.dias_trabajados, 2);
                            }
                            else
                                valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    switch (cod_concepto_liq)
                    {
                        case 110:
                            valor = valor * 12 / 100;
                            break;
                        case 120:
                            valor = valor * 12 / 100;
                            break;
                        case 121:
                            valor = valor * 6 / 100;
                            break;
                        case 152:
                            valor = valor * 15 / 100;
                            break;
                        default:
                            break;
                    }

                    importe_concepto = decimal.Round(valor, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = 1;
                }


                //692-seguro itt, 695-seguro obligatorio,
                if (cod_concepto_liq == 692 || cod_concepto_liq == 695)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) WHERE ");
                    strSQL.AppendLine(" cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_valor=1");
                    cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = 1;

                }

                //800-ipam esposos,
                //802-IPAM NIETOS
                //826 Descuento Aporte Nacion

                //906 Grupo Extra Nieto
                if (cod_concepto_liq == 800 || cod_concepto_liq == 802 || cod_concepto_liq == 826 || cod_concepto_liq == 906)
                {
                    nro_valor = 0;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK) WHERE ");
                    strSQL.AppendLine("cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            nro_valor = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq")));
                        }
                        dr.Close();
                    }
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=@nro_valor");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Parameters.AddWithValue("@nro_valor", nro_valor);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = nro_valor;
                }


                //624-Fondo Enfermedades
                //if (cod_concepto_liq == 624)
                //{
                //    valor_parametro = 0;
                //    strSQL.Clear();
                //    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK) WHERE ");
                //    strSQL.AppendLine("cod_concepto_liq =@cod_concepto_liq");
                //    strSQL.AppendLine(" AND legajo=@legajo");
                //    cmd = new SqlCommand();
                //    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                //    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                //    cmd.Connection = cn;
                //    cmd.Transaction = trx;
                //    cmd.CommandType = CommandType.Text;
                //    cmd.CommandText = strSQL.ToString();
                //    dr = cmd.ExecuteReader();
                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                //        }
                //    }
                //    dr.Close();
                //    strSQL.Clear();
                //    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ (NOLOCK) ");
                //    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                //    strSQL.AppendLine(" AND nro_valor=1");
                //    cmd1 = new SqlCommand();
                //    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                //    cmd1.CommandText = strSQL.ToString();
                //    cmd1.CommandType = CommandType.Text;
                //    cmd1.Connection = cn;
                //    cmd1.Transaction = trx;
                //    dr = cmd.ExecuteReader();
                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                //        }
                //    }
                //    dr.Close();
                //    importe_concepto = decimal.Round(valor_parametro * valor, 2);
                //    eResultado.resultado_1 = importe_concepto;
                //    eResultado.resultado_2 = nro_valor;
                //}

                //624-Fondo Enfermedades
                //ahora funciona como variable
                if (cod_concepto_liq == 624)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine(" WHERE anio=@anio");
                    strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(valor_parametro);
                    eResultado.resultado_2 = 1;
                }


                //480-AYUDA ESC.PRIMARIA
                if (cod_concepto_liq == 480)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine(" WHERE anio=@anio");
                    strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_valor=1");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor_parametro * valor, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = 1;
                }
                //////////////////////////////////////////////////////////////////////////////////////////////
                //627-APROSS  - APORTE ADICIONAL FAMILIARES                                           
                //660-retención embargo, 
                //680 -retención judicial,
                //681 -Cuota Alimentaria,
                //685-descuentos ley,
                //700-descuentos diferencia seguro años 2006-2007,
                //819-Asociacion Mutual MAS                
                //820-dto. proveeduria,
                //821-Descuento 100 pesos x vales
                //822-Descuento pago de vacaciones
                //823-Desc Imp a La Ganancia
                //824-S.O.E.M.V.A. - PROVEEDURIA,
                //825-Dif Antiguedad
                //827-Caruso Seguros
                //828-Cocheria Sn Jose
                //831-descuento anticipo sac
                //834-dto. bco, 835-dto. bco,
                //836-dto. proveeduria, 837-dto. varios
                //833 AGL Capital
                //826 AMPARO Seguros
                if (cod_concepto_liq == 660 || cod_concepto_liq == 680 || cod_concepto_liq == 681 || cod_concepto_liq == 685 ||
                  cod_concepto_liq == 700 || cod_concepto_liq == 819 ||
                  cod_concepto_liq == 820 || cod_concepto_liq == 821 || cod_concepto_liq == 822 ||
                  cod_concepto_liq == 823 || cod_concepto_liq == 824 || cod_concepto_liq == 825 ||
                  cod_concepto_liq == 831 || cod_concepto_liq == 834 || cod_concepto_liq == 835 ||
                  cod_concepto_liq == 836 || cod_concepto_liq == 837 || cod_concepto_liq == 838 ||
                  cod_concepto_liq == 839 || cod_concepto_liq == 833 || cod_concepto_liq == 627 ||
                  cod_concepto_liq == 826 || cod_concepto_liq == 827 || cod_concepto_liq == 828)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_parametro FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK) ");
                    strSQL.AppendLine(" WHERE anio=@anio");
                    strSQL.AppendLine(" AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine(" AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine(" AND legajo=@legajo");
                    strSQL.AppendLine(" AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND nro_parametro=1");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor_parametro, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = 1;
                }

                //816-Descuento por Retiro Anticipado
                if (cod_concepto_liq == 816)
                {
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor_concepto_liq FROM CONCEP_LIQUID_X_EMPLEADO (NOLOCK)  ");
                    strSQL.AppendLine("WHERE cod_concepto_liq =@cod_concepto_liq");
                    strSQL.AppendLine("AND legajo=@legajo");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            importe_concepto = (oEmpleado.sueldo_bruto * dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq")) / 100);
                            valor_concepto_liq = dr.GetDecimal(dr.GetOrdinal("valor_concepto_liq"));
                        }
                    }
                    dr.Close();
                    eResultado.resultado_1 = decimal.Round(importe_concepto);
                    eResultado.resultado_2 = valor_concepto_liq;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cmd = null;
            cmd1 = null;
            cmd2 = null;
            return eResultado;
        }

        public static decimal Calcula_ConceptoA(Entities.LstEmpleados oEmpleado, int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, SqlConnection cn, SqlTransaction trx)
        {
            decimal importe_concepto = 0;
            decimal max_sueldo = 0;
            decimal sbruto = 0;
            //
            string strSQL = string.Empty;
            SqlCommand cmd;
            SqlDataReader dr;
            Entities.Sueldos oSueldos = null;
            List<Entities.Sueldos> lstSueldos = new List<Sueldos>();

            try
            {
                //El 390 que ya esta sin los no remunerativos
                importe_concepto = 0;
                strSQL = @"SELECT 
                           A.nro_liquidacion,
                           sueldo_bruto=(isnull(sum(b.importe),0)-
                            (SELECT isnull(SUM(importe),0)
                            FROM DET_LIQ_X_EMPLEADO WITH (NOLOCK) 
                            WHERE anio=a.anio and cod_tipo_liq=a.cod_tipo_liq AND
                            nro_liquidacion=a.nro_liquidacion and
                            legajo=b.legajo AND 
                            cod_concepto_liq in (17,18,19,65,71,72,73,74))),
                           dias_trabajados=(select isnull(SUM(unidades),0)
                             from DET_LIQ_X_EMPLEADO WITH (NOLOCK) 
                             where
                               anio=a.anio AND cod_tipo_liq=a.cod_tipo_liq AND
                               nro_liquidacion=a.nro_liquidacion AND
	                           legajo=b.legajo AND
	                           cod_concepto_liq=10)
                           FROM LIQUIDACIONES A WITH (NOLOCK)
                           JOIN DET_LIQ_X_EMPLEADO B on
                           B.anio=A.anio
                           AND B.cod_tipo_liq=A.cod_tipo_liq
                           AND B.nro_liquidacion=A.nro_liquidacion
                           AND B.legajo=@legajo
                           AND (B.cod_concepto_liq = 390 OR B.cod_concepto_liq = 491 OR B.cod_concepto_liq = 499)
                           JOIN CONCEPTOS_LIQUIDACION C on
                           B.cod_concepto_liq = C.cod_concepto_liq 
                           AND C.suma<>0 AND C.sac<>0
                           WHERE A.anio=@anio
                           AND A.cod_tipo_liq=@cod_tipo_liq
                           AND A.cod_semestre=@cod_semestre
                           AND A.aguinaldo<>1
                           AND A.prueba=0
                           GROUP BY A.anio,B.legajo,A.cod_tipo_liq,A.nro_liquidacion
                           ORDER BY A.nro_liquidacion DESC";
                //
                //strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a1 WITH(NOLOCK)");
                //strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION cl on");
                //strSQL.AppendLine("cl.cod_concepto_liq <> 390 AND");
                //strSQL.AppendLine("cl.cod_concepto_liq = a1.cod_concepto_liq AND");
                //strSQL.AppendLine("cl.suma <> 0 AND cl.sujeto_a_desc = 0 and cl.sac = 1 and");
                //strSQL.AppendLine("a1.anio = a.anio and");
                //strSQL.AppendLine("a1.cod_tipo_liq = a.cod_tipo_liq AND");
                //strSQL.AppendLine("a1.nro_liquidacion = a.nro_liquidacion and");
                //strSQL.AppendLine("a1.legajo = b.legajo ))");
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.Parameters.AddWithValue("@cod_semestre", cod_semestre);
                cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int nro_liq = dr.GetOrdinal("nro_liquidacion");
                    int sueldo_bruto = dr.GetOrdinal("sueldo_bruto");
                    int dias_trabajados = dr.GetOrdinal("dias_trabajados");
                    while (dr.Read())
                    {
                        oSueldos = new Sueldos();
                        if (!dr.IsDBNull(nro_liq)) oSueldos.nro_liquidacion = dr.GetInt32(nro_liq);
                        if (!dr.IsDBNull(sueldo_bruto)) oSueldos.sueldo_bruto = dr.GetDecimal(sueldo_bruto);
                        if (!dr.IsDBNull(dias_trabajados)) oSueldos.dias_trabajados = dr.GetDecimal(dias_trabajados);
                        lstSueldos.Add(oSueldos);
                    }
                }
                dr.Close();
                max_sueldo = 0;
                sbruto = 0;
                int i = 0;
                foreach (var item in lstSueldos)
                {
                    sbruto = item.sueldo_bruto;
                    if (Decimal.Round(sbruto, 2) > (Decimal.Round(max_sueldo, 2)))
                    {
                        max_sueldo = Decimal.Round(sbruto, 2);
                    }
                    i = i + 1;
                }

                if (max_sueldo > 0)
                    importe_concepto = Decimal.Round((max_sueldo / 2 / 6) * i, 2);
            }
            catch (Exception e)
            {
                throw e;
            }
            cmd = null;
            return importe_concepto;
        }

        public static Entities.Resultado Calcula_ConceptoA_Nuevo(Entities.LstEmpleados oEmpleado, int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, SqlConnection cn, SqlTransaction trx)
        {
            decimal importe_concepto = 0;
            decimal max_sueldo = 0;
            decimal sbruto = 0;
            decimal saguinaldo = 0;
            decimal dias_trabajados = 0;
            decimal importe_sueldo = 0;
            //
            string strSQL = string.Empty;
            SqlCommand cmd;
            SqlDataReader dr;
            Entities.Sueldos oSueldos = null;
            List<Entities.Sueldos> lstSueldos = new List<Sueldos>();
            Entities.Resultado eResultado = new Resultado();

            //dias_trabajados = (select isnull(SUM(valor_parametro), 0)
            //                from PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)
            //                where
            //                anio = a.anio AND cod_tipo_liq = A.cod_tipo_liq AND
            //                    nro_liquidacion = @nro_liquidacion AND
            //                      legajo = b.legajo AND
            //                        cod_concepto_liq = 10)
            try
            {
                //El 390 que ya esta sin los no remunerativos
                importe_concepto = 0;
                strSQL = @"SELECT 
                           A.nro_liquidacion,
                           sueldo_bruto=(isnull(sum(b.importe),0)-
                                        (SELECT isnull(SUM(importe),0)
                                        FROM DET_LIQ_X_EMPLEADO WITH (NOLOCK) 
                                        WHERE anio=a.anio and cod_tipo_liq=a.cod_tipo_liq AND
                                        nro_liquidacion=a.nro_liquidacion and
                                        legajo=b.legajo AND 
                                        cod_concepto_liq in (17,18,19,65,72,73,74))),
                            dias_trabajados=(SELECT isnull(SUM(unidades),0)
										FROM DET_LIQ_X_EMPLEADO WITH (NOLOCK) 
										WHERE
										anio=A.anio AND 
										cod_tipo_liq=A.cod_tipo_liq AND
										nro_liquidacion=A.nro_liquidacion AND
										legajo=B.legajo AND
										cod_concepto_liq=10)
                           FROM LIQUIDACIONES A WITH (NOLOCK)
                           JOIN DET_LIQ_X_EMPLEADO B on
                           B.anio=A.anio
                           AND B.cod_tipo_liq=A.cod_tipo_liq
                           AND B.nro_liquidacion=A.nro_liquidacion
                           AND B.legajo=@legajo
                           AND (B.cod_concepto_liq = 390 OR B.cod_concepto_liq = 491 OR B.cod_concepto_liq = 499)
                           JOIN CONCEPTOS_LIQUIDACION C on
                           B.cod_concepto_liq = C.cod_concepto_liq 
                           AND C.suma<>0 AND C.sac<>0
                           WHERE A.anio=@anio
                           AND A.cod_tipo_liq=@cod_tipo_liq
                           AND A.cod_semestre=@cod_semestre
                           --AND A.nro_liquidacion=@nro_liquidacion
                           AND A.aguinaldo<>1
                           AND A.prueba=0
                           GROUP BY A.anio,B.legajo,A.cod_tipo_liq, A.nro_liquidacion
                           ORDER BY A.nro_liquidacion DESC";
                //dias_trabajados = (select isnull(SUM(unidades), 0)
                //from DET_LIQ_X_EMPLEADO WITH (NOLOCK)
                //where
                //  anio = a.anio AND cod_tipo_liq = a.cod_tipo_liq AND
                //  nro_liquidacion = a.nro_liquidacion AND
                //  legajo = b.legajo AND
                //  cod_concepto_liq = 10)
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.Parameters.AddWithValue("@cod_semestre", cod_semestre);
                cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int nro_liq = dr.GetOrdinal("nro_liquidacion");
                    int sueldo_bruto = dr.GetOrdinal("sueldo_bruto");
                    int dias = dr.GetOrdinal("dias_trabajados");
                    while (dr.Read())
                    {
                        oSueldos = new Sueldos();
                        if (!dr.IsDBNull(nro_liq)) oSueldos.nro_liquidacion = dr.GetInt32(nro_liq);
                        if (!dr.IsDBNull(sueldo_bruto)) oSueldos.sueldo_bruto = dr.GetDecimal(sueldo_bruto);
                        if (!dr.IsDBNull(dias)) oSueldos.dias_trabajados = dr.GetDecimal(dias);
                        lstSueldos.Add(oSueldos);
                    }
                }
                dr.Close();
                max_sueldo = 0;
                importe_sueldo = 0;
                sbruto = 0;
                int i = 0;
                foreach (var item in lstSueldos)
                {

                    if (item.sueldo_bruto > 0)
                    {
                        importe_sueldo = (item.sueldo_bruto / item.dias_trabajados) * 30;
                        sbruto = importe_sueldo;
                        if (Decimal.Round(sbruto, 2) > (Decimal.Round(max_sueldo, 2)))
                        {
                            max_sueldo = Decimal.Round(sbruto, 2);
                        }
                        dias_trabajados += item.dias_trabajados;
                        i = i + 1;
                    }
                }
                //if (max_sueldo > 0)
                //importe_concepto = Decimal.Round((max_sueldo / 2 / 6) * i, 2);
                if (max_sueldo > 0)
                {
                    importe_concepto = Decimal.Round((max_sueldo / 360), 2);
                    saguinaldo = importe_concepto * dias_trabajados;
                }
                eResultado.resultado_1 = saguinaldo;
                eResultado.resultado_2 = dias_trabajados;
            }
            catch (Exception e)
            {
                throw e;
            }
            cmd = null;
            return eResultado;
            //saguinaldo;
        }

        public static decimal Calcula_ConceptoA17(Entities.LstEmpleados oEmpleado, int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, SqlConnection cn, SqlTransaction trx)
        {
            decimal importe_concepto = 0;
            decimal max_sueldo = 0;
            decimal sbruto = 0;
            //
            string strSQL;
            //
            SqlCommand cmd;
            //
            SqlDataReader dr;
            //
            Entities.Sueldos oSueldos = null;
            List<Entities.Sueldos> lstSueldos = new List<Sueldos>();
            try
            {
                importe_concepto = 0;

                strSQL = @"SELECT 
                             --nro_liquidacion, 
                             valor_parametro as sueldo_bruto 
                           FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)
                           WHERE anio = @anio
                           AND cod_tipo_liq=@cod_tipo_liq
                           AND nro_liquidacion=@nro_liquidacion
                           AND legajo=@legajo
                           AND cod_concepto_liq=17
                           AND nro_parametro=1";
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                //cmd.Parameters.AddWithValue("@cod_semestre", cod_semestre);
                cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    //int nro_liq = dr.GetOrdinal("nro_liquidacion");
                    int sueldo_bruto = dr.GetOrdinal("sueldo_bruto");
                    int dias = dr.GetOrdinal("dias_trabajados");
                    while (dr.Read())
                    {
                        oSueldos = new Sueldos();
                        //if (!dr.IsDBNull(nro_liq)) oSueldos.nro_liquidacion = dr.GetInt32(nro_liq);
                        if (!dr.IsDBNull(sueldo_bruto)) oSueldos.sueldo_bruto = dr.GetDecimal(sueldo_bruto);
                        if (!dr.IsDBNull(dias)) oSueldos.dias_trabajados = dr.GetDecimal(dias);
                        lstSueldos.Add(oSueldos);
                    }
                }
                dr.Close();
                importe_concepto = lstSueldos.ElementAt(0).sueldo_bruto;
                //max_sueldo = 0;
                //sbruto = 0;
                //int i = 0;
                //foreach (var item in lstSueldos)
                //{
                //    sbruto = item.sueldo_bruto;
                //    if (Decimal.Round(sbruto, 2) > (Decimal.Round(max_sueldo, 2)))
                //    {
                //        max_sueldo = Decimal.Round(sbruto, 2);
                //    }
                //    i = i + 1;
                //}

                //if (max_sueldo > 0)
                //    importe_concepto = Decimal.Round((max_sueldo / 2 / 6) * i, 2);
            }
            catch (Exception e)
            {
                throw e;
            }
            cmd = null;
            return importe_concepto;
        }

        public static Entities.Resultado Calcula_Conceptos_familia(Entities.LstEmpleados oEmpleado, decimal sasd,
          int cod_concepto_liq, int anio, int cod_tipo_liq, int nro_liquidacion, string fecha_liquidacion,
          decimal valor_concepto_liq, SqlConnection cn, SqlTransaction trx)
        {

            decimal importe_concepto = 0;
            //decimal imp_cat_agente = 0;
            //decimal imp_cat_8 = 0;
            //decimal basico_cat_8 = 0;
            //decimal antiguedad = 0;
            //decimal valor_1 = 0;
            //decimal valor_2 = 0;
            decimal valor = 0;
            int nro_valor = 0;
            decimal sasd_aux = 0; //Corresponde al sasd pero sin el aguinaldo
            decimal valor_parametro = 0;
            decimal sbruto = 0;
            //
            Entities.Resultado eResultado = new Resultado();
            //
            StringBuilder strSQL = new StringBuilder();
            string sql;
            //
            SqlCommand cmd;
            SqlCommand cmd1;
            SqlCommand cmd2;
            SqlDataReader dr;

            try
            {
                ////////////////////////////////////////////////////////////////////////////////////
                //400-salario familiar.
                //410-asig.hijos
                //470-asig.hijos discapacitado
                //500-asig.prenatal
                //decimal otrosConceptos = 0;

                sql = @"";

                if (cod_concepto_liq == 400 || cod_concepto_liq == 410 || cod_concepto_liq == 470 || cod_concepto_liq == 500)
                {
                    nro_valor = 0;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq between 16 and 19");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();

                    if (valor_parametro > 0)
                    {
                        sasd_aux = sasd - valor_parametro;
                    }
                    else
                        sasd_aux = sasd;

                    /***************************************************************************/
                    //Aca sumo los conceptos no remunerativos hs extras etc los que estan cargados en el 
                    //Formulario 931 para Afip.
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq in(151,153,155,167,490)");
                    strSQL.AppendLine("UNION");
                    strSQL.AppendLine("SELECT ISNULL(valor_concepto_liq * " + oEmpleado.sueldo_basico + " / 100, 0)");
                    strSQL.AppendLine("FROM CONCEP_LIQUID_X_EMPLEADO");
                    strSQL.AppendLine("WHERE legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq in(482, 491, 499)");

                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);

                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro += dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();
                    if (valor_parametro > 0)
                    {
                        sbruto = valor_parametro;
                    }
                    else
                        sbruto = 0;


                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(sum(valor_parametro),0)  as valor_parametro ");
                    strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO WITH (NOLOCK)");
                    strSQL.AppendLine("WHERE anio=@anio");
                    strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                    strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                    strSQL.AppendLine("AND legajo=@legajo");
                    strSQL.AppendLine("AND cod_concepto_liq in(151,153,155,482,490,491,499)");
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", oEmpleado.legajo);
                    cmd.CommandText = strSQL.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Transaction = trx;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor_parametro = dr.GetDecimal(dr.GetOrdinal("valor_parametro"));
                        }
                    }
                    dr.Close();

                    /*importe_concepto = (oEmpleado.sueldo_basico * valor_concepto_liq) / 100;*/
                    /*22/10/2021*/
                    /*Nuevo Sueldo bruto mas codigos 151/153/155/482/491/499*/
                    sasd_aux += sbruto;
                    //--------------------------------------------------------------------------
                    //    'Busco el si el sueldobruto esta dentro de las escalas de las
                    //    'asignaciones fliares
                    //    'si es asi cobra asignacion sino a llorar
                    //--------------------------------------------------------------------------
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    //23/12/2022
                    //Ahora lo saca en forma dinamica
                    //
                    sasd_aux = sasd;
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT isnull(nro_valor,0) as nro_valor ");
                    strSQL.AppendLine("FROM escalas_asignaciones_fliar");
                    strSQL.AppendLine(" WHERE activo=1 AND cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine(" AND anio=@anio");
                    strSQL.AppendLine(" AND @sasd_aux");
                    strSQL.AppendLine(" BETWEEN valor_desde AND valor_hasta");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd1.Parameters.AddWithValue("@anio", anio);
                    cmd1.Parameters.AddWithValue("@sasd_aux", sasd_aux);
                    cmd1.CommandText = strSQL.ToString();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = cn;
                    cmd1.Transaction = trx;
                    dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            nro_valor = dr.GetInt16(dr.GetOrdinal(("nro_valor")));
                        }
                    }
                    dr.Close();
                    strSQL.Clear();
                    strSQL.AppendLine("SELECT valor FROM VALORES_X_CONCEPTO_LIQ WITH (NOLOCK) ");
                    strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq");
                    strSQL.AppendLine("AND nro_valor=@nro_valor");
                    cmd2 = new SqlCommand();
                    cmd2.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd2.Parameters.AddWithValue("@nro_valor", nro_valor);
                    cmd2.CommandText = strSQL.ToString();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = cn;
                    cmd2.Transaction = trx;
                    dr = cmd2.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            valor = dr.GetDecimal(dr.GetOrdinal("valor"));
                        }
                    }
                    dr.Close();
                    importe_concepto = decimal.Round(valor * valor_concepto_liq, 2);
                    eResultado.resultado_1 = importe_concepto;
                    eResultado.resultado_2 = valor_concepto_liq;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cmd = null;
            cmd1 = null;
            cmd2 = null;
            return eResultado;
        }

    }

}



#endregion


