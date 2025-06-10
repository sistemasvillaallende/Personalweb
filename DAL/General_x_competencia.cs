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


        public static List<General_x_competencia> readConFiltros(int idFicha, string secretaria = null, string direccion = null, string oficina = null)
        {
            try
            {
                List<General_x_competencia> list = new List<General_x_competencia>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    string query = @"WITH Datos AS (
                                    SELECT 
                                        CASE A.ID_PREGUNTA
                                            WHEN 53 THEN 'ORIENTACION A RESULTADOS'
                                            WHEN 54 THEN 'ORIENTACION A LA CALIDAD'
                                            WHEN 55 THEN 'ORIENTACION AL VECINO'
                                            WHEN 56 THEN 'TRABAJO EN EQUIPO'
                                            WHEN 57 THEN 'ORGANIZACION Y PLANIFICACION'
                                            WHEN 58 THEN 'ACTITUD COMPROMETIDA'
                                            WHEN 59 THEN 'FLEXIBILIDAD Y ADAPTABILIDAD'
                                            WHEN 60 THEN 'LIDERAZGO'
                                            WHEN 61 THEN 'ANALISIS Y RESOLUCION DE PROBLEMAS'
                                            WHEN 62 THEN 'COMUNICACION'
                                            WHEN 63 THEN 'COMPETENCIA TECNICA'
                                        END AS Pregunta,
                                        A.TEXTO_RESPUESTA AS Respuesta,
                                        COUNT(*) AS Ocurrencias
                                    FROM FICHAS_RELEVAMIENTOS_PERSONAS A
                                    INNER JOIN FICHAS_RELEVAMIENTOS B ON A.ID_RELEVAMIENTO = B.ID
                                    WHERE B.ID_FICHA = @idFicha ";

                    if (!string.IsNullOrEmpty(secretaria))
                    {
                        query += " AND B.SECRETARIA = @secretaria";
                        command.Parameters.AddWithValue("@secretaria", secretaria);
                    }

                    if (!string.IsNullOrEmpty(direccion))
                    {
                        query += " AND B.DIRECCION = @direccion";
                        command.Parameters.AddWithValue("@direccion", direccion);
                    }

                    if (!string.IsNullOrEmpty(oficina))
                    {
                        query += " AND B.OFICINA = @oficina";
                        command.Parameters.AddWithValue("@oficina", oficina);
                    }

                    query += @"  GROUP BY A.ID_PREGUNTA, A.TEXTO_RESPUESTA
                                    )
                                    SELECT 
                                        Pregunta,
                                        COALESCE([No Cubre Expectativas], NULL) AS [No_Cubre_Expectativas],
                                        COALESCE([Sólido], NULL) AS [Sólido],  
                                        COALESCE([Alcanza Plenamente], NULL) AS [Alcanza_Plenamente],  
                                        COALESCE([Supera Expectativas], NULL) AS [Supera_Expectativas]
                                    FROM Datos
                                    PIVOT (
                                        SUM(Ocurrencias) 
                                        FOR Respuesta IN (
                                            [No Cubre Expectativas], 
                                            [Sólido], 
                                            [Alcanza Plenamente], 
                                            [Supera Expectativas]
                                        )
                                    ) AS TablaPivote
                                    ORDER BY Pregunta;";

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Connection.Open();
                    return mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}
