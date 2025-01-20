using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL
{
    public static class Concepto_Liq_x_EmpD
    {
        private static List<ConceptoLiqxEmp> mapeo(SqlDataReader dr)
        {
            List<ConceptoLiqxEmp> lst = new List<ConceptoLiqxEmp>();
            ConceptoLiqxEmp obj;
            if (dr.HasRows)
            {
                int legajo = dr.GetOrdinal("legajo");
                int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                int fecha_alta_registro = dr.GetOrdinal("fecha_alta_registro");
                int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                int fecha_vto = dr.GetOrdinal("fecha_vto");
                int usuario = dr.GetOrdinal("usuario");
                while (dr.Read())
                {
                    obj = new ConceptoLiqxEmp();
                    if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }
                    if (!dr.IsDBNull(cod_concepto_liq)) { obj.cod_concepto_liq = dr.GetInt32(cod_concepto_liq); }
                    if (!dr.IsDBNull(fecha_alta_registro)) { obj.fecha_alta_registro = Convert.ToDateTime(dr.GetDateTime(fecha_alta_registro)).ToShortDateString(); }
                    if (!dr.IsDBNull(valor_concepto_liq)) { obj.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq); }
                    if (!dr.IsDBNull(fecha_vto)) { obj.fecha_vto = Convert.ToDateTime(dr.GetDateTime(fecha_vto)).ToShortDateString(); }
                    if (!dr.IsDBNull(usuario)) { obj.usuario = dr.GetString(usuario); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Entities.ConceptoLiqxEmp> FillConceptoLiqxEmp(int legajo)
        {
            Entities.ConceptoLiqxEmp oDetalle = null;
            List<Entities.ConceptoLiqxEmp> lst = new List<Entities.ConceptoLiqxEmp>();
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select a.legajo, a.cod_concepto_liq, b.des_concepto_liq as concepto,");
            strSQL.AppendLine("a.valor_concepto_liq, convert(varchar(10), a.fecha_vto, 103) as fecha_vto, ");
            strSQL.AppendLine("convert(varchar(10), a.fecha_alta_registro, 103) as fecha_alta_registro  ");
            strSQL.AppendLine("From CONCEP_LIQUID_X_EMPLEADO a");
            strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
            strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq");
            strSQL.AppendLine("JOIN EMPLEADOS e on");
            strSQL.AppendLine("a.legajo = e.legajo");
            strSQL.AppendLine("Where a.legajo=@legajo");
            strSQL.AppendLine("ORDER BY b.cod_concepto_liq");
            try
            {
                using (SqlConnection con = DALBase.GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            return lst;
        }

        public static void DeleteConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("DELETE CONCEP_LIQUID_X_EMPLEADO");
                strSQL.AppendLine(" WHERE legajo=@legajo and cod_concepto_liq=@cod_concepto_liq");
                using (SqlConnection con = DALBase.GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.Add(new SqlParameter("@legajo", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", SqlDbType.Int));
                    cmd.Connection.Open();
                    for (int i = 0; i < oDetalle.Count; i++)
                    {
                        cmd.Parameters["@legajo"].Value = oDetalle[i].legajo;
                        cmd.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                        cmd.CommandText = strSQL.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void DeleteConceptosxEmp(int legajo)
        {
            string strSQL = @"DELETE FROM CONCEP_LIQUID_X_EMPLEADO
                              WHERE legajo=@legajo";
            try
            {
                using (SqlConnection con = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static void UpdateConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        //{
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            DeleteConceptosxEmp(legajo);
        //            InsertConceptoxEmp(legajo, oDetalle);
        //            AuditaMovimientos(oDetalle);
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void InsertConceptoxEmp(int legajo, List<ConceptoLiqxEmp> oDetalle)
        {
            StringBuilder strSQL = new StringBuilder();
            //bool paso = false;
            DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
            try
            {
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

                using (SqlConnection con = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@legajo", 0);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@fecha_alta_registro", Convert.ToDateTime(DateTime.Now, culturaFecArgentina).ToString());
                    cmd.Parameters.AddWithValue("@valor_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@fecha_vto", Convert.ToDateTime(DateTime.Now, culturaFecArgentina).ToString());
                    cmd.Parameters.AddWithValue("@usuario", string.Empty);
                    cmd.Connection.Open();
                    for (int i = 0; i < oDetalle.Count; i++)
                    {
                        cmd.Parameters["@legajo"].Value = oDetalle[i].legajo;
                        cmd.Parameters["@cod_concepto_liq"].Value = oDetalle[i].cod_concepto_liq;
                        cmd.Parameters["@fecha_alta_registro"].Value = oDetalle[i].fecha_alta_registro;//DateTime.Today.ToShortDateString();
                        cmd.Parameters["@valor_concepto_liq"].Value = oDetalle[i].valor_concepto_liq;
                        if (Convert.ToDateTime(oDetalle[i].fecha_vto) <= Convert.ToDateTime("31/12/1900"))
                            cmd.Parameters["@fecha_vto"].Value = oDetalle[i].fecha_alta_registro;
                        else
                            cmd.Parameters["@fecha_vto"].Value = oDetalle[i].fecha_vto;
                        cmd.Parameters["@usuario"].Value = oDetalle[i].usuario;
                        cmd.CommandText = strSQL.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void AuditaMovimientos(List<Entities.ConceptoLiqxEmp> oDetalle, string obsauditoria)
        {
            try
            {
                string operacion = string.Empty;
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                Concepto_Liq_x_Emp_Mov objMov = new Concepto_Liq_x_Emp_Mov();

                string strSQL = @"INSERT INTO CONCEP_LIQUID_X_EMPLEADO_MOV
                                   (legajo
                                   ,fecha_mov
                                   ,id_tipo_movimiento
                                   ,cod_concepto_liq
                                   ,valor_concepto_liq
                                   ,fecha_vto
                                   ,descripcion
                                   ,observacion
                                   ,usuario)
                                 VALUES
                                   (@legajo
                                   ,@fecha_mov
                                   ,@id_tipo_movimiento
                                   ,@cod_concepto_liq
                                   ,@valor_concepto_liq
                                   ,@fecha_vto 
                                   ,@descripcion
                                   ,@observacion
                                   ,@usuario)";

                using (SqlConnection con = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@legajo", 0);
                    cmd.Parameters.AddWithValue("@fecha_mov", Convert.ToDateTime(DateTime.Now, culturaFecArgentina).ToString());
                    cmd.Parameters.AddWithValue("@id_tipo_movimiento", 0);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@valor_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@fecha_vto", Convert.ToDateTime(DateTime.Now, culturaFecArgentina).ToString());
                    cmd.Parameters.AddWithValue("@descripcion", string.Empty);
                    cmd.Parameters.AddWithValue("@observacion", string.Empty);
                    cmd.Parameters.AddWithValue("@usuario", string.Empty);
                    cmd.Connection.Open();
                    if (oDetalle.Count > 0)
                    {
                        foreach (var item in oDetalle)
                        {
                            if (item.op > 0)
                            {
                                switch (item.op)
                                {
                                    case 1:
                                        operacion = "Alta concepto";
                                        break;
                                    case 2:
                                        operacion = "Modifica concepto";
                                        break;
                                    case 3:
                                        operacion = "Elimina concepto";
                                        break;
                                    default:
                                        break;
                                }
                                cmd.Parameters["@legajo"].Value = item.legajo;
                                cmd.Parameters["@fecha_mov"].Value = Convert.ToDateTime(DateTime.Now, culturaFecArgentina).ToString();
                                cmd.Parameters["@id_tipo_movimiento"].Value = item.op;
                                cmd.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                                cmd.Parameters["@valor_concepto_liq"].Value = item.valor_concepto_liq;
                                cmd.Parameters["@fecha_vto"].Value = item.fecha_vto;
                                cmd.Parameters["@descripcion"].Value = operacion;
                                if (item.op == 3)
                                    cmd.Parameters["@observacion"].Value = obsauditoria;
                                else
                                    cmd.Parameters["@observacion"].Value = item.observacion;
                                cmd.Parameters["@usuario"].Value = item.usuario;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        //491 no remunerativo se calcula con %
        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_3(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;

            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq 
                       FROM 
                        CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), 
                        CONCEPTOS_LIQUIDACION B 
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
        //490 no remunerativo
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


            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq 
                       FROM CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), 
                       CONCEPTOS_LIQUIDACION B
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
        //solo los codigos de las asignaciones fliares 410,500,470

        public static List<Entities.ConceptoLiqxEmp> GetConceptoLiqxEmp_Asignacion_fliar(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.ConceptoLiqxEmp> lstConceptos = new List<ConceptoLiqxEmp>();
            Entities.ConceptoLiqxEmp eConcepto;
            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL = "";

            strSQL = @"SELECT A.cod_concepto_liq, A.valor_concepto_liq 
                       FROM CONCEP_LIQUID_X_EMPLEADO A WITH (NOLOCK), CONCEPTOS_LIQUIDACION B 
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
        /////////////////
        //
        public static void DeleteAsignacionFam(int cod_concepto_liq)
        {
            try
            {
                string strSQL = @"DELETE FROM CONCEP_LIQUID_X_EMPLEADO
                                  WHERE cod_concepto_liq=@cod_concepto_liq";
                using (SqlConnection con = DALBase.GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto_liq);
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ConceptoLiqxEmp> GetTraerFamiliares()
        {
            try
            {
                //sql que me devuele la tabla correspodiente a los legajos en 0
                //y luego en otro procedimiento debo calcular la cantidad de hijos>18 que tiene el legajo.
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                string strSQL = @"SELECT 
                                      a.legajo, count(*)
                                  FROM FAMILIARES a 
                                  JOIN EMPLEADOS b on
                                        a.legajo = b.legajo AND
                                        b.fecha_baja is null
                                  WHERE
                                        a.legajo=b.legajo AND 
                                        a.id_parentezco=1 AND
                                        a.salario_familiar=1 AND
                                        a.incapacitado=0
                                  Group by a.legajo";
                List<ConceptoLiqxEmp> lst = new List<ConceptoLiqxEmp>();
                ConceptoLiqxEmp obj = null;
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int legajo = dr.GetOrdinal("legajo");
                        //int nro_familiar = dr.GetOrdinal("nro_familiar");
                        //int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
                        while (dr.Read())
                        {
                            obj = new ConceptoLiqxEmp();
                            obj.legajo = dr.GetInt32(legajo);
                            obj.valor_concepto_liq = 0;
                            //obj.fecha_nacimiento = Convert.ToDateTime(dr.GetString(fecha_nacimiento), culturaFecArgentina);
                            lst.Add(obj);
                        }
                    }
                    return lst;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ConceptoLiqxEmp> GetTraerHijosDiscapacitados()
        {
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                string strSQL = @"SELECT 
                                      a.legajo, count(*) as cant_hijos
                                  FROM FAMILIARES a 
                                  JOIN EMPLEADOS b on
                                        a.legajo = b.legajo AND
                                        b.fecha_baja is null
                                  WHERE
                                        a.legajo=b.legajo AND 
                                        a.id_parentezco=1 AND
                                        a.salario_familiar=1 AND
                                        a.incapacitado=1
                                  Group by a.legajo";
                List<ConceptoLiqxEmp> lst = new List<ConceptoLiqxEmp>();
                ConceptoLiqxEmp obj = null;
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int legajo = dr.GetOrdinal("legajo");
                        int cant_hijos = dr.GetOrdinal("cant_hijos");
                        //int nro_familiar = dr.GetOrdinal("nro_familiar");
                        //int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
                        while (dr.Read())
                        {
                            obj = new ConceptoLiqxEmp();
                            obj.legajo = dr.GetInt32(legajo);
                            obj.valor_concepto_liq = dr.GetInt32(cant_hijos);
                            //obj.fecha_nacimiento = Convert.ToDateTime(dr.GetString(fecha_nacimiento), culturaFecArgentina);
                            lst.Add(obj);
                        }
                    }
                    return lst;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ConceptoLiqxEmp> GetConceptoLiqxEmp(int cod_concepto)
        {
            try
            {
                //este sql que me devuele la tabla correspodiente a los legajos
                //donde debo modificar los valores de las asignaciones fam, que corresponde al codigo 410.
                string strSQL = @"SELECT a.legajo, a.cod_concepto_liq, a.valor_concepto_liq 
                                  FROM CONCEP_LIQUID_X_EMPLEADO a
                                  JOIN EMPLEADOS b on
                                    b.legajo=a.legajo AND
                                    b.fecha_baja is NULL
                                  WHERE a.cod_concepto_liq=@cod_concepto_liq";
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                List<ConceptoLiqxEmp> lst = new List<ConceptoLiqxEmp>();
                ConceptoLiqxEmp obj = null;
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", cod_concepto);
                    cmd.CommandText = strSQL;
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int legajo = dr.GetOrdinal("legajo");
                        int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                        int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                        while (dr.Read())
                        {
                            obj = new ConceptoLiqxEmp();
                            obj.legajo = dr.GetInt32(legajo);
                            obj.cod_concepto_liq = dr.GetInt32(cod_concepto_liq);
                            obj.valor_concepto_liq = dr.GetDecimal(valor_concepto_liq);
                            lst.Add(obj);
                        }
                    }
                    return lst;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ActualizarConceptoFamiliar(List<ConceptoLiqxEmp> lst)
        {
            var ErrorMessage = string.Empty;
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                string strSQL = @"INSERT INTO CONCEP_LIQUID_X_EMPLEADO
                                  (legajo,cod_concepto_liq, fecha_alta_registro, 
                                   valor_concepto_liq, fecha_vto, usuario)
                                  VALUES
                                  (@legajo, @cod_concepto_liq, @fecha_alta_registro, 
                                   @valor_concepto_liq, @fecha_vto, @usuario)";
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@legajo", 0);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@fecha_alta_registro", string.Empty);
                    cmd.Parameters.AddWithValue("@valor_concepto_liq", 0);
                    cmd.Parameters.AddWithValue("@fecha_vto", string.Empty);
                    cmd.Parameters.AddWithValue("@usuario", string.Empty);
                    foreach (var item in lst)
                    {
                        ErrorMessage = "Legajo : " + item.legajo.ToString();
                        //if (item.legajo == 7023)
                        //{
                        //    Console.Write("...");
                        //    Console.Beep();
                        //}
                        //
                        if (item.valor_concepto_liq > 0)
                        {
                            cmd.Parameters["@legajo"].Value = item.legajo;
                            cmd.Parameters["@cod_concepto_liq"].Value = item.cod_concepto_liq;
                            cmd.Parameters["@fecha_alta_registro"].Value = Convert.ToDateTime(item.fecha_alta_registro, culturaFecArgentina);
                            cmd.Parameters["@valor_concepto_liq"].Value = item.valor_concepto_liq;
                            cmd.Parameters["@fecha_vto"].Value = Convert.ToDateTime(item.fecha_vto, culturaFecArgentina);
                            cmd.Parameters["@usuario"].Value = item.usuario;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                                        "Message: " + ex.Errors[i].Message + "\n" +
                                        "Error Number: " + ex.Errors[i].Number + "\n" +
                                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                        "Source: " + ex.Errors[i].Source + "\n" +
                                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                throw new Exception("Error en el proceso de ActualizarConceptoFamiliar!," + ErrorMessage.ToString(), ex);
            }


        }

    }

}



//if (oDetalle.Count > 0)
//{
//    foreach (var item in oDetalle)
//    {
//        objMov.legajo = item.legajo;
//        objMov.cod_concepto_liq = item.cod_concepto_liq;
//        objMov.valor_concepto_liq = item.valor_concepto_liq;
//        objMov.fecha_mov = DateTime.Now;
//        if (item.fecha_vto.Length > 0)
//            objMov.fecha_vto = Convert.ToDateTime(item.fecha_vto, culturaFecArgentina);
//        objMov.observacion = item.observaciones;
//        objMov.usuario = item.usuario;
//        //cmd.ExecuteNonQuery();

//    }
//}
