using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class Categorias_historialD
    {

        public static List<Entities.Categorias_historial> GetCategoriaHistorial()
        {
            StringBuilder strSQL = new StringBuilder();
            {
                strSQL.AppendLine("SELECT cod_categoria, item, des_categoria, fecha_alta_registro, sueldo_basico ");
                strSQL.AppendLine("FROM  CATEGORIAS_HIST_BASICOS ");
                strSQL.AppendLine("ORDER BY cod_categoria");
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstCategoriaHist(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        private static List<Entities.Categorias_historial> getLstCategoriaHist(SqlCommand cmd)
        {
            List<Entities.Categorias_historial> lst = new List<Entities.Categorias_historial>();
            Entities.Categorias_historial oCategoriaHist;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int cod_categoria = dr.GetOrdinal("cod_categoria");
                    int item = dr.GetOrdinal("item");
                    int des_categoria = dr.GetOrdinal("des_categoria");
                    int fecha_alta_registro = dr.GetOrdinal("fecha_alta_registro");
                    int sueldo_basico = dr.GetOrdinal("sueldo_basico");
                    while (dr.Read())
                    {
                        oCategoriaHist = new Entities.Categorias_historial();
                        if (!dr.IsDBNull(cod_categoria)) oCategoriaHist.cod_categoria = dr.GetInt32(cod_categoria);
                        if (!dr.IsDBNull(item)) oCategoriaHist.item = dr.GetInt16(item);
                        if (!dr.IsDBNull(des_categoria)) oCategoriaHist.des_categoria = dr.GetString(des_categoria);
                        if (!dr.IsDBNull(fecha_alta_registro)) oCategoriaHist.fecha_alta_registro = dr.GetDateTime(fecha_alta_registro);
                        if (!dr.IsDBNull(sueldo_basico)) oCategoriaHist.sueldo_basico = dr.GetDecimal(sueldo_basico);
                        lst.Add(oCategoriaHist);
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



        public static void AgregarAlHistorial(int cod_categoria, string des_categoria, decimal sueldo_basico)
        {
            try
            {
                var nuevoItem = 0;
                int anioActual = DateTime.Now.Year;


                using (SqlConnection con = DALBase.GetConnection("Siimva"))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        StringBuilder SQL = new StringBuilder();
                        SQL.AppendLine("SELECT isnull(max(item),0) FROM  CATEGORIAS_HIST_BASICOS ");
                        SQL.AppendLine("WHERE cod_categoria = @cod_categoria ");
                        cmd1.Connection = con;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.AddWithValue("@cod_categoria", cod_categoria);
                        cmd1.CommandText = SQL.ToString();

                        nuevoItem = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                    }

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO CATEGORIAS_HIST_BASICOS(");
                    sql.AppendLine("  anio");
                    sql.AppendLine(", cod_categoria");
                    sql.AppendLine(", item");
                    sql.AppendLine(", des_categoria");
                    sql.AppendLine(", fecha_alta_registro");
                    sql.AppendLine(", sueldo_basico");
                    sql.AppendLine(")");
                    sql.AppendLine("VALUES");
                    sql.AppendLine("(");
                    sql.AppendLine("  @anio");
                    sql.AppendLine(", @cod_categoria");
                    sql.AppendLine(", @item");
                    sql.AppendLine(", @des_categoria");
                    sql.AppendLine(", @fecha_alta_registro");
                    sql.AppendLine(", @sueldo_basico");
                    sql.AppendLine(")");

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@anio", anioActual);
                        cmd.Parameters.AddWithValue("@cod_categoria", cod_categoria);
                        cmd.Parameters.AddWithValue("@item", nuevoItem);
                        cmd.Parameters.AddWithValue("@des_categoria", des_categoria);
                        cmd.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Now);
                        cmd.Parameters.AddWithValue("@sueldo_basico", sueldo_basico);
                        cmd.Connection = con;
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
        }

        public static Entities.Categorias_historial GetByPk(int codigo)
        {
            Entities.Categorias_historial obj = new Entities.Categorias_historial();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT cod_categoria, item, des_categoria, fecha_alta_registro, sueldo_basico ");
            strSQL.AppendLine("FROM CATEGORIAS_HIST_BASICOS ");
            strSQL.AppendLine("WHERE cod_categoria = @codigo");

            cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@codigo", codigo));

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
                        if (!dr.IsDBNull(dr.GetOrdinal("cod_categoria")))
                            obj.cod_categoria = dr.GetInt32(dr.GetOrdinal("cod_categoria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("item")))
                            obj.item = dr.GetInt16(dr.GetOrdinal("item"));

                        if (!dr.IsDBNull(dr.GetOrdinal("des_categoria")))
                            obj.des_categoria = dr.GetString(dr.GetOrdinal("des_categoria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                            obj.fecha_alta_registro = dr.GetDateTime(dr.GetOrdinal("fecha_alta_registro"));

                        if (!dr.IsDBNull(dr.GetOrdinal("sueldo_basico")))
                            obj.sueldo_basico = dr.GetDecimal(dr.GetOrdinal("sueldo_basico"));

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
    }
}
