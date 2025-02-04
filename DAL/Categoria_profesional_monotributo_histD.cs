using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Categoria_profesional_monotributo_histD
    {
        public static List<Entities.Categoria_profesional_monotributo_hist> GetCategoriaProfesionalHistorial()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {
                strSQL.AppendLine("SELECT id_profesional_monotributo, id_movimiento, fecha_movimiento , monto ");
                strSQL.AppendLine("FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO_HIST ");
                strSQL.AppendLine("ORDER BY id_profesional_monotributo");
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstCategoriaProfesionalHist(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        private static List<Entities.Categoria_profesional_monotributo_hist> getLstCategoriaProfesionalHist(SqlCommand cmd)
        {
            List<Entities.Categoria_profesional_monotributo_hist> lst = new List<Entities.Categoria_profesional_monotributo_hist>();
            Entities.Categoria_profesional_monotributo_hist oCategoriaProfesionalHist;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int id_profesional = dr.GetOrdinal("id_profesional_monotributo");
                    int id_movimiento = dr.GetOrdinal("id_movimiento");
                    int fecha_movimiento = dr.GetOrdinal("fecha_movimiento");
                    int monto = dr.GetOrdinal("monto");
                    while (dr.Read())
                    {
                        oCategoriaProfesionalHist = new Entities.Categoria_profesional_monotributo_hist();
                        if (!dr.IsDBNull(id_profesional)) oCategoriaProfesionalHist.id_profesional_monotributo = dr.GetInt32(id_profesional);
                        if (!dr.IsDBNull(id_movimiento)) oCategoriaProfesionalHist.id_movimiento = dr.GetInt32(id_movimiento);
                        if (!dr.IsDBNull(fecha_movimiento)) oCategoriaProfesionalHist.fecha_movimiento = dr.GetDateTime(fecha_movimiento);
                        if (!dr.IsDBNull(monto)) oCategoriaProfesionalHist.monto = dr.GetDecimal(monto);
                        lst.Add(oCategoriaProfesionalHist);
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

        public static List<Entities.Categoria_profesional_historialDTO> GetHistorialDetalle()
        {

            StringBuilder strSQL = new StringBuilder();
            strSQL.AppendLine("SELECT cat.categoria, hist.id_movimiento, hist.fecha_movimiento, hist.monto");
            strSQL.AppendLine("FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO_HIST hist");
            strSQL.AppendLine(" LEFT JOIN CATEGORIA_PROFESIONAL_MONOTRIBUTO cat");
            strSQL.AppendLine(" ON hist.id_profesional_monotributo = cat.id_profesional_monotributo");
            strSQL.AppendLine(" ORDER BY hist.id_profesional_monotributo");

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Entities.Categoria_profesional_historialDTO> lst = new List<Entities.Categoria_profesional_historialDTO>();
                    Entities.Categoria_profesional_historialDTO obj;
                    if (dr.HasRows)
                    {
                        int categoria = dr.GetOrdinal("categoria");
                        int id_movimiento = dr.GetOrdinal("id_movimiento");
                        int fecha_movimiento = dr.GetOrdinal("fecha_movimiento");
                        int monto = dr.GetOrdinal("monto");
                        while (dr.Read())
                        {
                            obj = new Entities.Categoria_profesional_historialDTO();
                            if (!dr.IsDBNull(categoria)) obj.categoria = dr.GetString(categoria);
                            if (!dr.IsDBNull(id_movimiento)) obj.id_movimiento = dr.GetInt32(id_movimiento);
                            if (!dr.IsDBNull(fecha_movimiento)) obj.fecha_movimiento = dr.GetDateTime(fecha_movimiento);
                            if (!dr.IsDBNull(monto)) obj.monto = dr.GetDecimal(monto);
                            lst.Add(obj);
                        }
                        dr.Close();
                    }
                    return lst;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in query!" + e.ToString());
                    throw e;
                }
            }
        }

        public static void AgregarAlHistorial(int id_profesional_monotributo, decimal monto)
        {
            try
            {
                 var nuevoIdMovimiento = 0;

                using (SqlConnection con = DALBase.GetConnection("Siimva"))
                {
                     if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        StringBuilder SQL = new StringBuilder();
                        SQL.AppendLine("SELECT isnull(max(id_movimiento),0) FROM CATEGORIA_PROFESIONAL_MONOTRIBUTO_HIST ");
                        SQL.AppendLine("WHERE id_profesional_monotributo = @id_profesional_monotributo ");
                        cmd1.Connection = con;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.AddWithValue("@id_profesional_monotributo", id_profesional_monotributo);
                        cmd1.CommandText = SQL.ToString();

                        nuevoIdMovimiento = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                    }

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO CATEGORIA_PROFESIONAL_MONOTRIBUTO_HIST(");
                    sql.AppendLine(" id_profesional_monotributo");
                    sql.AppendLine(", id_movimiento");
                    sql.AppendLine(", fecha_movimiento");
                    sql.AppendLine(", monto");
                    sql.AppendLine(")");
                    sql.AppendLine("VALUES");
                    sql.AppendLine("(");
                    sql.AppendLine("@id_profesional_monotributo");
                    sql.AppendLine(", @id_movimiento");
                    sql.AppendLine(", @fecha_movimiento");
                    sql.AppendLine(", @monto");
                    sql.AppendLine(")");

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_profesional_monotributo", id_profesional_monotributo);
                        cmd.Parameters.AddWithValue("@id_movimiento", nuevoIdMovimiento);
                        cmd.Parameters.AddWithValue("@fecha_movimiento", DateTime.Now);
                        cmd.Parameters.AddWithValue("@monto", monto);
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
    }
}








