using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class LIQ_X_EMPLEADO : DALBase
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public DateTime fecha_alta_registro { get; set; }
        public int cod_categoria { get; set; }
        public int cod_cargo { get; set; }
        public string tarea { get; set; }
        public string nro_cta_sb { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal sueldo_neto { get; set; }
        public decimal sueldo_bruto { get; set; }
        public decimal no_remunerativo { get; set; }
        public int nro_orden { get; set; }
        public string des_liquidacion { get; set; }
        public int cod_clasif_per { get; set; }
        public string des_clasif_per { get; set; }


        public LIQ_X_EMPLEADO()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            fecha_alta_registro = DateTime.Now;
            cod_categoria = 0;
            cod_cargo = 0;
            tarea = string.Empty;
            nro_cta_sb = string.Empty;
            sueldo_basico = 0;
            sueldo_neto = 0;
            sueldo_bruto = 0;
            no_remunerativo = 0;
            nro_orden = 0;
            des_liquidacion = string.Empty;
            cod_clasif_per = 0;
            des_clasif_per = string.Empty;
        }

        private static List<LIQ_X_EMPLEADO> mapeo(SqlDataReader dr)
        {
            List<LIQ_X_EMPLEADO> lst = new List<LIQ_X_EMPLEADO>();
            LIQ_X_EMPLEADO obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new LIQ_X_EMPLEADO();
                    if (!dr.IsDBNull(0)) { obj.anio = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cod_tipo_liq = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.nro_liquidacion = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.legajo = dr.GetInt32(3); }
                    if (!dr.IsDBNull(4)) { obj.fecha_alta_registro = dr.GetDateTime(4); }
                    if (!dr.IsDBNull(5)) { obj.cod_categoria = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { obj.cod_cargo = dr.GetInt32(6); }
                    if (!dr.IsDBNull(7)) { obj.tarea = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.nro_cta_sb = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.sueldo_basico = dr.GetDecimal(9); }
                    if (!dr.IsDBNull(10)) { obj.sueldo_neto = dr.GetDecimal(10); }
                    if (!dr.IsDBNull(11)) { obj.sueldo_bruto = dr.GetDecimal(11); }
                    if (!dr.IsDBNull(12)) { obj.no_remunerativo = dr.GetDecimal(12); }
                    if (!dr.IsDBNull(13)) { obj.nro_orden = dr.GetInt32(13); }
                    if (!dr.IsDBNull(14)) { obj.cod_clasif_per = dr.GetInt32(14); }
                    if (!dr.IsDBNull(15)) { obj.des_liquidacion = dr.GetString(15); }                   
                    if (!dr.IsDBNull(16)) { obj.des_clasif_per = dr.GetString(16); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<LIQ_X_EMPLEADO> read()
        {
            try
            {
                List<LIQ_X_EMPLEADO> lst = new List<LIQ_X_EMPLEADO>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM LIQ_X_EMPLEADO";
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static LIQ_X_EMPLEADO getUltimoHaber(int legajo, int anio)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT L.*, B.des_liquidacion");
                sql.AppendLine("FROM LIQ_X_EMPLEADO L");
                sql.AppendLine("WHERE L.legajo = @LEGAJO AND L.anio = @ANIO AND L.nro_liquidacion = ");
                sql.AppendLine("(SELECT MAX(A.nro_liquidacion) FROM LIQ_X_EMPLEADO A");
                sql.AppendLine("INNER JOIN LIQUIDACIONES B ON A.anio = B.anio AND A.nro_liquidacion = B.nro_liquidacion AND B.publica = 1");
                sql.AppendLine("WHERE A.anio = @ANIO AND A.legajo = @LEGAJO)");

                LIQ_X_EMPLEADO obj = null;
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@ANIO", anio);
                    cmd.Parameters.AddWithValue("@LEGAJO", legajo);

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<LIQ_X_EMPLEADO> lst = new List<LIQ_X_EMPLEADO>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            obj = new LIQ_X_EMPLEADO();
                            if (!dr.IsDBNull(0)) { obj.anio = dr.GetInt32(0); }
                            if (!dr.IsDBNull(1)) { obj.cod_tipo_liq = dr.GetInt32(1); }
                            if (!dr.IsDBNull(2)) { obj.nro_liquidacion = dr.GetInt32(2); }
                            if (!dr.IsDBNull(3)) { obj.legajo = dr.GetInt32(3); }
                            if (!dr.IsDBNull(4)) { obj.fecha_alta_registro = dr.GetDateTime(4); }
                            if (!dr.IsDBNull(5)) { obj.cod_categoria = dr.GetInt32(5); }
                            if (!dr.IsDBNull(6)) { obj.cod_cargo = dr.GetInt32(6); }
                            if (!dr.IsDBNull(7)) { obj.tarea = dr.GetString(7); }
                            if (!dr.IsDBNull(8)) { obj.nro_cta_sb = dr.GetString(8); }
                            if (!dr.IsDBNull(9)) { obj.sueldo_basico = dr.GetDecimal(9); }
                            if (!dr.IsDBNull(10)) { obj.sueldo_neto = dr.GetDecimal(10); }
                            if (!dr.IsDBNull(11)) { obj.sueldo_bruto = dr.GetDecimal(11); }
                            if (!dr.IsDBNull(12)) { obj.no_remunerativo = dr.GetDecimal(12); }
                            if (!dr.IsDBNull(13)) { obj.nro_orden = dr.GetInt32(13); }
                            if (!dr.IsDBNull(14)) { obj.des_liquidacion = dr.GetString(14); }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<LIQ_X_EMPLEADO> getLiquidaciones(int anio, int legajo, bool aguinaldo)
        {
            try
            {
                string sql = @"SELECT A.*, B.des_liquidacion, cp.des_clasif_per
                            FROM LIQ_X_EMPLEADO A
                            LEFT JOIN CLASIFICACIONES_PERSONAL cp on
                                cp.cod_clasif_per=A.cod_clasif_Per   
                            INNER JOIN LIQUIDACIONES B ON 
                            A.anio = B.anio AND
                            A.nro_liquidacion = B.nro_liquidacion AND
                            A.cod_tipo_liq=B.cod_tipo_liq
                            WHERE legajo=@legajo AND A.anio=@anio AND B.aguinaldo=@aguinaldo
                            ORDER BY b.anio, b.cod_tipo_liq, b.nro_liquidacion";
                //if (aguinaldo)
                //    sql += "AND B.aguinaldo = 1";
                //else
                //    sql += "AND B.aguinaldo = 0";
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@aguinaldo", aguinaldo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    return mapeo(dr);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static LIQ_X_EMPLEADO getByPk(int anio, int cod_tipo_liq, int nro_liquidacion, int legajo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM LIQ_X_EMPLEADO WHERE");
                sql.AppendLine("anio = @anio");
                sql.AppendLine("AND cod_tipo_liq = @cod_tipo_liq");
                sql.AppendLine("AND nro_liquidacion = @nro_liquidacion");
                sql.AppendLine("AND legajo = @legajo");
                LIQ_X_EMPLEADO obj = null;
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<LIQ_X_EMPLEADO> lst = mapeo(dr);
                    if (lst.Count != 0)
                        obj = lst[0];
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(LIQ_X_EMPLEADO obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO LIQ_X_EMPLEADO(");
                sql.AppendLine("anio");
                sql.AppendLine(", cod_tipo_liq");
                sql.AppendLine(", nro_liquidacion");
                sql.AppendLine(", legajo");
                sql.AppendLine(", fecha_alta_registro");
                sql.AppendLine(", sueldo_neto");
                sql.AppendLine(", nro_orden");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@anio");
                sql.AppendLine(", @cod_tipo_liq");
                sql.AppendLine(", @nro_liquidacion");
                sql.AppendLine(", @legajo");
                sql.AppendLine(", @fecha_alta_registro");
                sql.AppendLine(", @sueldo_neto");
                sql.AppendLine(", @nro_orden");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@anio", obj.anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", obj.cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", obj.nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@fecha_alta_registro", obj.fecha_alta_registro);
                    cmd.Parameters.AddWithValue("@sueldo_neto", obj.sueldo_neto);
                    cmd.Parameters.AddWithValue("@nro_orden", obj.nro_orden);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(LIQ_X_EMPLEADO obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  LIQ_X_EMPLEADO SET");
                sql.AppendLine("fecha_alta_registro=@fecha_alta_registro");
                sql.AppendLine(", sueldo_neto=@sueldo_neto");
                sql.AppendLine(", nro_orden=@nro_orden");
                sql.AppendLine("WHERE");
                sql.AppendLine("anio=@anio");
                sql.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                sql.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                sql.AppendLine("AND legajo=@legajo");
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@anio", obj.anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", obj.cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", obj.nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@fecha_alta_registro", obj.fecha_alta_registro);
                    cmd.Parameters.AddWithValue("@sueldo_neto", obj.sueldo_neto);
                    cmd.Parameters.AddWithValue("@nro_orden", obj.nro_orden);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(LIQ_X_EMPLEADO obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  LIQ_X_EMPLEADO ");
                sql.AppendLine("WHERE");
                sql.AppendLine("anio=@anio");
                sql.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                sql.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                sql.AppendLine("AND legajo=@legajo");
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@anio", obj.anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", obj.cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", obj.nro_liquidacion);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

