using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Procesos
    {
        public int cod_proceso { get; set; }
        public string proceso { get; set; }

        public Procesos()
        {
            cod_proceso = 0;
            proceso = string.Empty;
        }
        public static List<Procesos> read(int cod_usuario)
        {
            try
            {
                Procesos obj = null;
                List<Procesos> lst = new List<Procesos>();
                using (SqlConnection con = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                    SELECT 
                        A.COD_PROCESO,
                        B.PROCESO
                    FROM PROCESOS_X_USUARIO_V2 A
	                    INNER JOIN PROCESOS_V2 B ON A.COD_PROCESO=B.COD_PROCESO
                    WHERE A.COD_USUARIO=@COD_USUARIO";

                    cmd.Parameters.AddWithValue("@COD_USUARIO", cod_usuario);
                    cmd.Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    int cod_proceso = sqlDataReader.GetOrdinal("cod_proceso");
                    int proceso = sqlDataReader.GetOrdinal("proceso");
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            obj = new Procesos();
                            if (!sqlDataReader.IsDBNull(cod_proceso))
                                obj.cod_proceso = sqlDataReader.GetInt32(cod_proceso);
                            if (!sqlDataReader.IsDBNull(proceso))
                                obj.proceso = sqlDataReader.GetString(proceso);
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*SELECT 
 A.COD_PROCESO,
 B.PROCESO
FROM PROCESOS_X_USUARIO_V2 A
	INNER JOIN PROCESOS_V2 B ON A.COD_PROCESO=B.COD_PROCESO
WHERE A.COD_USUARIO=181*/
    }
}
