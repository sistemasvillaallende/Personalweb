using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Fichas
{
    public class Fichas_Resultados
    {
        public static List<decimal> lstResultados { get; set; }

        private static List<decimal> mapeo(SqlDataReader dr)
        {
            List<decimal> lstResultados = new List<decimal>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        lstResultados.Add(Convert.ToDecimal(dr.GetString(0)));
                }
            }
            lstResultados.Sort();
            return lstResultados;
        }

        public static List<decimal> read(int idFicha)
        {
            try
            {
                List<decimal> estadosEvaluacionList = new List<decimal>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                            @"SELECT 
	                            FORMAT(ROUND(A.RESULTADO * 100 / (COUNT(B.ID_PREGUNTA) * 4), 2),
                                'N2') AS 'RESULTADOS'
                            FROM FICHAS_RELEVAMIENTOS A
                            INNER JOIN FICHAS_RELEVAMIENTOS_PERSONAS B ON A.ID=B.ID_RELEVAMIENTO
                            WHERE A.ID_FICHA=@idFicha AND RESULTADO IS NOT NULL
                            GROUP BY A.SECRETARIA, A.RESULTADO, A.CUIT, A.DIRECCION, A.OFICINA
                            ORDER BY SECRETARIA";
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
        public static List<decimal> read(int idFicha, string secretaria)
        {
            try
            {
                List<decimal> estadosEvaluacionList = new List<decimal>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        @"SELECT 
                            FORMAT(ROUND(A.RESULTADO * 100 / (COUNT(B.ID_PREGUNTA) * 4), 2),
                            'N2') AS 'RESULTADOS'
                        FROM FICHAS_RELEVAMIENTOS A
                            INNER JOIN FICHAS_RELEVAMIENTOS_PERSONAS B ON 
                            A.ID=B.ID_RELEVAMIENTO
                        WHERE A.ID_FICHA=@idFicha AND RESULTADO IS NOT NULL 
                            AND A.SECRETARIA = @secretaria
                        GROUP BY A.SECRETARIA, A.RESULTADO, A.CUIT, A.DIRECCION, A.OFICINA
                        ORDER BY 1";
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Parameters.AddWithValue("@secretaria", secretaria);
                    command.Connection.Open();
                    return mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<decimal> readConFiltros(int idFicha, string secretaria = null, string direccion = null, string oficina = null)
        {
            try
            {
                List<decimal> estadosEvaluacionList = new List<decimal>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    // Query base
                    string query = @"SELECT 
                            FORMAT(ROUND(A.RESULTADO * 100 / (COUNT(B.ID_PREGUNTA) * 4), 2),
                            'N2') AS 'RESULTADOS'
                        FROM FICHAS_RELEVAMIENTOS A
                            INNER JOIN FICHAS_RELEVAMIENTOS_PERSONAS B ON 
                            A.ID=B.ID_RELEVAMIENTO
                        WHERE A.ID_FICHA=@idFicha AND RESULTADO IS NOT NULL";

                    // Agregar filtros dinámicamente
                    if (!string.IsNullOrEmpty(secretaria))
                    {
                        query += " AND A.SECRETARIA = @secretaria";
                        command.Parameters.AddWithValue("@secretaria", secretaria);
                    }

                    if (!string.IsNullOrEmpty(direccion))
                    {
                        query += " AND A.DIRECCION = @direccion";
                        command.Parameters.AddWithValue("@direccion", direccion);
                    }

                    if (!string.IsNullOrEmpty(oficina))
                    {
                        query += " AND A.OFICINA = @oficina";
                        command.Parameters.AddWithValue("@oficina", oficina);
                    }

                    query += " GROUP BY A.SECRETARIA, A.RESULTADO, A.CUIT, A.DIRECCION, A.OFICINA ORDER BY 1";

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



        public static List<string> getSecretarias(int idFicha)
        {
            try
            {
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"SELECT DISTINCT SECRETARIA
                                  FROM FICHAS_RELEVAMIENTOS
                                  WHERE ID_FICHA=@idFicha 
                                  AND SECRETARIA IS NOT NULL
                                  ORDER BY SECRETARIA";
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Connection.Open();
                    return MapearLista(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> getDirecciones(int idFicha, string secretaria)
        {
            try
            {
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"SELECT DISTINCT DIRECCION
                                  FROM FICHAS_RELEVAMIENTOS
                                  WHERE ID_FICHA=@idFicha 
                                  AND SECRETARIA=@secretaria
                                  AND DIRECCION IS NOT NULL
                                  ORDER BY DIRECCION";
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Parameters.AddWithValue("@secretaria", secretaria);
                    command.Connection.Open();
                    return MapearLista(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> getOficinas(int idFicha, string secretaria, string direccion)
        {
            try
            {
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"SELECT DISTINCT OFICINA
                                  FROM FICHAS_RELEVAMIENTOS
                                  WHERE ID_FICHA=@idFicha 
                                  AND SECRETARIA=@secretaria
                                  AND DIRECCION=@direccion
                                  AND OFICINA IS NOT NULL
                                  ORDER BY OFICINA";
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Parameters.AddWithValue("@secretaria", secretaria);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Connection.Open();
                    return MapearLista(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<string> MapearLista(SqlDataReader dr)
        {
            List<string> lstResultados = new List<string>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        lstResultados.Add(dr.GetString(0));
                }
            }
            return lstResultados;
        }
    }
}
