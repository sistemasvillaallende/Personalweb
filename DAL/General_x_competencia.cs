using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class General_x_competencia : DALBase
    {
        public string pregunta { get; set; }
        public List<int> Respuestas { get; set; }

        public General_x_competencia()
        {
            pregunta = string.Empty;
            Respuestas = new List<int>();
        }

        private static List<General_x_competencia> mapeo(SqlDataReader dr)
        {
            List<General_x_competencia> lst = new List<General_x_competencia>();
            General_x_competencia obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new General_x_competencia();
                    if (!dr.IsDBNull(dr.GetOrdinal(dr.GetName(0))))
                    {
                        obj.pregunta = dr.GetString(dr.GetOrdinal(dr.GetName(0)));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal(dr.GetName(1))))
                    {
                        obj.Respuestas.Add(dr.GetInt32(dr.GetOrdinal(dr.GetName(1))));

                    }
                    else
                    {
                        obj.Respuestas.Add(0);
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal(dr.GetName(2))))
                    {
                        obj.Respuestas.Add(dr.GetInt32(dr.GetOrdinal(dr.GetName(2))));

                    }
                    else
                    {
                        obj.Respuestas.Add(0);
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal(dr.GetName(3))))
                    {
                        obj.Respuestas.Add(dr.GetInt32(dr.GetOrdinal(dr.GetName(3))));

                    }
                    else
                    {
                        obj.Respuestas.Add(0);
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal(dr.GetName(4))))
                    {
                        obj.Respuestas.Add(dr.GetInt32(dr.GetOrdinal(dr.GetName(4))));

                    }
                    else
                    {
                        obj.Respuestas.Add(0);
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<General_x_competencia> read(int idFicha)
        {
            try
            {
                List<General_x_competencia> lst = new List<General_x_competencia>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_getGenerales";
                    cmd.Parameters.AddWithValue("@ficha", idFicha);
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
        public static List<General_x_competencia> read(int idFicha, string secretaria)
        {
            try
            {
                List<General_x_competencia> lst = new List<General_x_competencia>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_get_competencia_secretaria";
                    cmd.Parameters.AddWithValue("@ficha", idFicha);
                    cmd.Parameters.AddWithValue("@secretaria", secretaria);
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
    }
}
