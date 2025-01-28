using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Categoria_profesional_monotributoD
    {
        public static List<Entities.Categoria_profesional_monotributo> GetCategoriaProfesionalMono()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {
                strSQL.AppendLine("SELECT id_profesional_monotributo, categoria, fecha_alta , monto ");
                strSQL.AppendLine("FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO ");
                strSQL.AppendLine("ORDER BY id_profesional_monotributo");
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstCategoriaProfesional(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        private static List<Entities.Categoria_profesional_monotributo> getLstCategoriaProfesional(SqlCommand cmd)
        {
            List<Entities.Categoria_profesional_monotributo> lst = new List<Entities.Categoria_profesional_monotributo>();
            Entities.Categoria_profesional_monotributo oCategoriaProfesional;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int id_profesional = dr.GetOrdinal("id_profesional_monotributo");
                    int categoria = dr.GetOrdinal("categoria");
                    int fecha_alta = dr.GetOrdinal("fecha_alta");
                    int monto = dr.GetOrdinal("monto");
                    while (dr.Read())
                    {
                        oCategoriaProfesional = new Entities.Categoria_profesional_monotributo();
                        if (!dr.IsDBNull(id_profesional)) oCategoriaProfesional.id_profesional_monotributo = dr.GetInt32(id_profesional);
                        if (!dr.IsDBNull(categoria)) oCategoriaProfesional.categoria = dr.GetString(categoria);
                        if (!dr.IsDBNull(fecha_alta)) oCategoriaProfesional.fecha_alta = dr.GetDateTime(fecha_alta);
                        if (!dr.IsDBNull(monto)) oCategoriaProfesional.monto = dr.GetDecimal(monto);
                        lst.Add(oCategoriaProfesional);
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


        public static List<Entities.Categoria_profesional_monotributo> FindCategoriaByDes(string descripcion)
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {

                strSQL.AppendLine("SELECT id_profesional_monotributo ,categoria, fecha_alta, monto ");
                strSQL.AppendLine("FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO");
                strSQL.AppendLine("WHERE categoria LIKE @descripcion");
                strSQL.AppendLine("ORDER BY id_profesional_monotributo");

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Parameters.AddWithValue("@descripcion", "%" + descripcion + "%");
                        cmd.Connection.Open();
                        return getLstCategoriaProfesional(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static Entities.Categoria_profesional_monotributo GetByPk(int codigo)
        {
            Entities.Categoria_profesional_monotributo obj = new Entities.Categoria_profesional_monotributo();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT id_profesional_monotributo ,categoria, fecha_alta, monto ");
            strSQL.AppendLine("FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO");
            strSQL.AppendLine("WHERE id_profesional_monotributo = @codigo");

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
                        if (!dr.IsDBNull(dr.GetOrdinal("id_profesional_monotributo")))
                            obj.id_profesional_monotributo = dr.GetInt32(dr.GetOrdinal("id_profesional_monotributo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("categoria")))
                            obj.categoria = dr.GetString(dr.GetOrdinal("categoria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta")))
                            obj.fecha_alta = dr.GetDateTime(dr.GetOrdinal("fecha_alta"));

                        if (!dr.IsDBNull(dr.GetOrdinal("monto")))
                            obj.monto = dr.GetDecimal(dr.GetOrdinal("monto"));

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


        //public static void NuevaCategoria(Entities.Categoria_profesional_monotributo oCate)
        //{
        //    SqlCommand cmd = null;
        //    SqlCommand cmd1 = null;
        //    SqlConnection cn = DALBase.GetConnection("Siimva");
        //    StringBuilder strSQL = new StringBuilder();
        //    try
        //    {
        //        if (oCate.id_profesional_monotributo == 0)
        //        {
        //            StringBuilder SQL = new StringBuilder();
        //            SQL.AppendLine("SELECT isnull(max(id_profesional_categoria),0) FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO");
        //            cmd1 = new SqlCommand();
        //            cmd1.Connection = cn;
        //            cmd1.CommandType = CommandType.Text;
        //            cmd1.CommandText = SQL.ToString();
        //            oCate.id_profesional_monotributo = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;

        //        }

        //        strSQL.AppendLine("INSERT into  CATEGORIA_PROFESIONAL_MONOTRIBUTO");
        //        strSQL.AppendLine("(id_profesional_monotributo,");
        //        strSQL.AppendLine("categoria,");
        //        strSQL.AppendLine("fecha_alta,");
        //        strSQL.AppendLine("monto)");
        //        strSQL.AppendLine("VALUES");
        //        strSQL.AppendLine("(@id_profesional_monotributo,");
        //        strSQL.AppendLine("@categoria,");
        //        strSQL.AppendLine("@fecha_alta,");
        //        strSQL.AppendLine("@monto)");

        //        cmd = new SqlCommand();
        //        cmd.Parameters.AddWithValue("@id_profesional_monotributo", oCate.id_profesional_monotributo);
        //        cmd.Parameters.AddWithValue("@categoria", oCate.categoria);
        //        cmd.Parameters.AddWithValue("@fecha_alta", oCate.fecha_alta);
        //        cmd.Parameters.AddWithValue("@monto", oCate.monto);
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strSQL.ToString();
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }

        //    finally
        //    {
        //        cmd = null;
        //        cn.Close();
        //    }
        //}

        public static void NuevaCategoria(Entities.Categoria_profesional_monotributo oCate)
        {
            StringBuilder strSQL = new StringBuilder();
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("Siimva"))
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    if (oCate.id_profesional_monotributo == 0)
                    {
                        using (SqlCommand cmd1 = new SqlCommand())
                        {
                            StringBuilder SQL = new StringBuilder();
                            SQL.AppendLine("SELECT isnull(max(id_profesional_monotributo),0) FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO");

                            cmd1.Connection = cn;
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = SQL.ToString();

                            oCate.id_profesional_monotributo = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                        }
                    }

                    strSQL.AppendLine("INSERT INTO CATEGORIA_PROFESIONAL_MONOTRIBUTO");
                    strSQL.AppendLine("(id_profesional_monotributo,");
                    strSQL.AppendLine("categoria,");
                    strSQL.AppendLine("fecha_alta,");
                    strSQL.AppendLine("monto)");
                    strSQL.AppendLine("VALUES");
                    strSQL.AppendLine("(@id_profesional_monotributo,");
                    strSQL.AppendLine("@categoria,");
                    strSQL.AppendLine("@fecha_alta,");
                    strSQL.AppendLine("@monto)");

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@id_profesional_monotributo", oCate.id_profesional_monotributo);
                        cmd.Parameters.AddWithValue("@categoria", oCate.categoria);
                        cmd.Parameters.AddWithValue("@fecha_alta", oCate.fecha_alta);
                        cmd.Parameters.AddWithValue("@monto", oCate.monto);
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void ModificaCategoria(Entities.Categoria_profesional_monotributo oCate)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE CATEGORIA_PROFESIONAL_MONOTRIBUTO set");
                strSQL.AppendLine("categoria=@categoria,");
                strSQL.AppendLine("monto=@monto");
                strSQL.AppendLine("WHERE id_profesional_monotributo=@id_profesional_monotributo");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id_profesional_monotributo", oCate.id_profesional_monotributo);
                cmd.Parameters.AddWithValue("@categoria", oCate.categoria);
                cmd.Parameters.AddWithValue("@monto", oCate.monto);

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

        public static void ModificaMonto(Entities.Categoria_profesional_monotributo oCate)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE CATEGORIA_PROFESIONAL_MONOTRIBUTO set");
                strSQL.AppendLine("monto=@monto");
                strSQL.AppendLine("WHERE id_profesional_monotributo=@id_profesional_monotributo");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id_profesional_monotributo", oCate.id_profesional_monotributo);
                cmd.Parameters.AddWithValue("@monto", oCate.monto);

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

        public static void EliminarCategoria(int id)
        {
            using (SqlConnection cn = DALBase.GetConnection("Siimva"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO WHERE id_profesional_monotributo = @id_profesional_monotributo";
                        cmd.Parameters.AddWithValue("@id_profesional_monotributo", id);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e; 
                    }
                }
            }
        }


    }
}
