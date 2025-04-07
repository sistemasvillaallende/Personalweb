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
        public static List<string> getSecretaria(int idFicha)
        {
            try
            {
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                            @"SELECT DISTINCT SECRETARIA
                              FROM FICHAS_RELEVAMIENTOS
                              WHERE ID_FICHA=@idFicha";
                    command.Parameters.AddWithValue("@idFicha", idFicha);
                    command.Connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    List<string> lstResultados = new List<string>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(0))
                                lstResultados.Add(dr.GetString(0));
                        }
                    }
                    lstResultados.Sort();
                    return lstResultados;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
