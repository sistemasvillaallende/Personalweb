using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoriasD
    {


        public static List<Entities.Categorias> GetCategorias()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {

                strSQL.AppendLine("SELECT cod_categoria, fecha_alta_registro, des_categoria, sueldo_basico ");
                strSQL.AppendLine("FROM CATEGORIAS");
                strSQL.AppendLine("ORDER BY cod_categoria");

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstCategorias(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        private static List<Entities.Categorias> getLstCategorias(SqlCommand cmd)
        {

            List<Entities.Categorias> lst = new List<Entities.Categorias>();
            Entities.Categorias oCategoria;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                //strSQL.AppendLine("select a.cod_concepto_liq, a.des_concepto_liq as concepto");
                //strSQL.AppendLine("b.des_tipo_concepto, a.suma, a.sujeto_a_desc, a.sac ");
                if (dr.HasRows)
                {

                    int codigo = dr.GetOrdinal("cod_categoria");
                    int fecha = dr.GetOrdinal("fecha_alta_registro");
                    int descripcion = dr.GetOrdinal("des_categoria");
                    int sueldo = dr.GetOrdinal("sueldo_basico");

                    while (dr.Read())
                    {
                        oCategoria = new Entities.Categorias();
                        if (!dr.IsDBNull(codigo)) oCategoria.cod_categoria = dr.GetInt32(codigo);
                        if (!dr.IsDBNull(fecha)) oCategoria.fecha_alta_registro = Convert.ToString(dr.GetDateTime(fecha));
                        if (!dr.IsDBNull(fecha)) oCategoria.des_categoria = Convert.ToString(dr.GetString(descripcion));
                        if (!dr.IsDBNull(sueldo)) oCategoria.sueldo_basico = dr.GetDecimal(sueldo);
                        lst.Add(oCategoria);
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

        public static List<Entities.Categorias> FindCategoriaByDes(string descripcion)
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {

                strSQL.AppendLine("SELECT cod_categoria, fecha_alta_registro, des_categoria, sueldo_basico ");
                strSQL.AppendLine("FROM CATEGORIAS");
                strSQL.AppendLine("WHERE des_categoria LIKE @descripcion");
                strSQL.AppendLine("ORDER BY cod_categoria");

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Parameters.AddWithValue("@descripcion", "%" + descripcion + "%");
                        cmd.Connection.Open();
                        return getLstCategorias(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static Entities.Categorias GetByPk(int codigo)
        {
            Entities.Categorias obj = new Entities.Categorias();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT cod_categoria, fecha_alta_registro, des_categoria, sueldo_basico ");
            strSQL.AppendLine("FROM CATEGORIAS");
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
                            obj.cod_categoria = dr.GetInt32((dr.GetOrdinal("cod_categoria")));

                        //if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                        //  obj.fecha_alta_registro = dr.GetString((dr.GetOrdinal("fecha_alta_registro")));

                        if (!dr.IsDBNull(dr.GetOrdinal("des_categoria")))
                            obj.des_categoria = dr.GetString((dr.GetOrdinal("des_categoria")));

                        if (!dr.IsDBNull(dr.GetOrdinal("sueldo_basico")))
                            obj.sueldo_basico = dr.GetDecimal((dr.GetOrdinal("sueldo_basico")));
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


        public static void NuevaCategoria(Entities.Categorias oCate)
        {
            SqlCommand cmd = null;
            SqlCommand cmd1 = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {
                if (oCate.cod_categoria == 0)
                {
                    StringBuilder SQL = new StringBuilder();
                    SQL.AppendLine("SELECT isnull(max(cod_categoria),0) FROM CATEGORIAS");
                    cmd1.Connection = cn;
                    cmd1.CommandType = CommandType.Text;
                    //cmd1.Transaction = trx;
                    cmd1.CommandText = SQL.ToString();
                    oCate.cod_categoria = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                }

                strSQL.AppendLine("INSERT into CATEGORIAS");
                strSQL.AppendLine("(cod_categoria,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("des_categoria,");
                strSQL.AppendLine("sueldo_basico)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@cod_categoria,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@des_categoria,");
                strSQL.AppendLine("@sueldo_basico)");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_categoria", oCate.cod_categoria);
                cmd.Parameters.AddWithValue("@fecha_alta_registro", oCate.fecha_alta_registro);
                cmd.Parameters.AddWithValue("@des_categoria", oCate.des_categoria);
                cmd.Parameters.AddWithValue("@sueldo_basico", oCate.sueldo_basico);
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

        public static void ModificaCategoria(Entities.Categorias oCate)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE Categorias set");
                strSQL.AppendLine("des_categoria=@des_categoria,");
                strSQL.AppendLine("sueldo_basico=@sueldo_basico");
                strSQL.AppendLine("WHERE cod_categoria=@cod_categoria");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_categoria", oCate.cod_categoria);
                cmd.Parameters.AddWithValue("@des_categoria", oCate.des_categoria);
                cmd.Parameters.AddWithValue("@sueldo_basico", oCate.sueldo_basico);

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

        public static void ModificaSueldoBasico(Entities.Categorias oCate)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE Categorias set");
                strSQL.AppendLine("sueldo_basico=@sueldo_basico");
                strSQL.AppendLine("WHERE cod_categoria=@cod_categoria");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_categoria", oCate.cod_categoria);
                cmd.Parameters.AddWithValue("@sueldo_basico", oCate.sueldo_basico);

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



    }
}
