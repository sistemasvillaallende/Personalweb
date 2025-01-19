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
                strSQL.AppendLine("ORDER BY id_profesiona_monotributo");
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
    }
}
