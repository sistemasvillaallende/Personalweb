using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DiccionarioDonut : DALBase
    {
        public string serie { get; set; }
        public int valor { get; set; }

        public DiccionarioDonut()
        {
            serie = string.Empty;
            valor = 0;
        }
        public DiccionarioDonut(string _serie, int _valor)
        {
            serie = _serie;
            valor = _valor;
        }
        private static List<DiccionarioDonut> mapeo(SqlDataReader dr)
        {
            List<DiccionarioDonut> lst = new List<DiccionarioDonut>();
            DiccionarioDonut obj;
            if (dr.HasRows)
            {
                int serie = dr.GetOrdinal("serie");
                int valor = dr.GetOrdinal("valor");

                while (dr.Read())
                {
                    obj = new DiccionarioDonut();
                    if (!dr.IsDBNull(serie)) { obj.serie = dr.GetString(serie); }
                    if (!dr.IsDBNull(valor)) { obj.valor = dr.GetInt32(valor); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<DiccionarioDonut> read()
        {
            try
            {
                List<DiccionarioDonut> lst = new List<DiccionarioDonut>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT C.des_clasif_per AS serie, COUNT(*) AS valor
                            FROM EMPLEADOS A
                            INNER JOIN CLASIFICACIONES_PERSONAL C ON 
                            C.cod_clasif_per=A.cod_clasif_per
                            WHERE activo=1
                            GROUP BY C.des_clasif_per";

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

        public static List<int> readSueldosPlantaSecretaria(
            int mes, int anio, int idSecretaria)
        {
            try
            {
                string m = string.Empty;
                if (mes < 10)
                    m = string.Format("0{0}", mes);
                else
                    m = mes.ToString();

                string periodo = string.Format("{0}{1}", anio, m);
                List<int> lstBase = new List<int>();
                List<int> lstReturn = new List<int>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT CONVERT(INT, ROUND(sueldo_bruto, 0, 1)) 
                        FROM LIQ_X_EMPLEADO A
                        INNER JOIN EMPLEADOS B ON A.legajo=B.legajo
                        WHERE A.anio=@anio AND A.nro_liquidacion=(
                        SELECT nro_liquidacion FROM LIQUIDACIONES
                        WHERE anio=@anio AND periodo=@periodo
                        AND publica=1 AND cod_tipo_liq=1)
                        AND B.id_secretaria=@id_secretaria
                        ORDER BY sueldo_bruto ASC";

                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@periodo", periodo);
                    cmd.Parameters.AddWithValue("@id_secretaria", idSecretaria);

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstBase.Add(dr.GetInt32(0));
                        }
                    }
                    int max = lstBase.Count() - 1;
                    lstReturn.Add(lstBase[0]);
                    lstReturn.Add(Convert.ToInt32(lstBase.Average()));
                    lstReturn.Add(mediana(lstBase));
                    lstReturn.Add(moda(lstBase));
                    lstReturn.Add(lstBase[max]);

                    return lstReturn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int mediana(List<int> lst)
        {
            int n = lst.Count;
            List<int> lstRound = new List<int>();
            double mediana = n % 2 == 0
                ? (lst[(n / 2) - 1] + lst[n / 2]) / 2.0
                : lst[n / 2];

            return Convert.ToInt32(mediana);
        }
        private static int RedondearA50K(int numero)
        {
            // Dividir el número por 50,000, redondear al entero más cercano y luego multiplicar por 50,000
            return (int)Math.Round(numero / 50000.0) * 50000;
        }
        private static int moda(List<int> lst)
        {
            //List<int> lstRound = new List<int>();
            //foreach (var item in lst)
            //{
            //    lstRound.Add(RedondearA50K(item));
            //}
            var grupos = lst.GroupBy(x => x);
            var moda = grupos.OrderByDescending(g => g.Count()).First().Key;
            return Convert.ToInt32(moda);
        }


        public static List<DiccionarioDonut> read(int idSecretaria)
        {
            try
            {
                List<DiccionarioDonut> lst = new List<DiccionarioDonut>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT C.des_clasif_per AS serie, COUNT(*) AS valor
                            FROM EMPLEADOS A
                            INNER JOIN CLASIFICACIONES_PERSONAL C ON 
                            C.cod_clasif_per=A.cod_clasif_per
                            WHERE activo=1 AND A.id_secretaria=@id_secretaria
                            GROUP BY C.des_clasif_per";
                    cmd.Parameters.AddWithValue("@id_secretaria", idSecretaria);
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
        public static List<DiccionarioDonut> readDireccion(int idDireccion)
        {
            try
            {
                List<DiccionarioDonut> lst = new List<DiccionarioDonut>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT C.des_clasif_per AS serie, COUNT(*) AS valor
                            FROM EMPLEADOS A
                            INNER JOIN CLASIFICACIONES_PERSONAL C ON 
                            C.cod_clasif_per=A.cod_clasif_per
                            WHERE activo=1 AND A.id_direccion=@id_direccion
                            GROUP BY C.des_clasif_per";
                    cmd.Parameters.AddWithValue("@id_direccion", idDireccion);
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
        public static List<DiccionarioDonut> estadoEvaluadorSecretaria(int idSecretaria,
            int ejercicio)
        {
            try
            {
                List<DiccionarioDonut> lst = new List<DiccionarioDonut>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT Z.Id_secretaria, Z.Descripcion,
                (SELECT COUNT(*) --A.Id_direccion--, A.Descripcion, B.* 
                FROM DIRECCION A
                INNER JOIN EMPLEADOS B ON CONVERT(int, A.cod_subceco)=B.legajo
                INNER JOIN SECRETARIA C ON C.Id_secretaria=B.id_secretaria
                WHERE A.ejercicio = @ejercicio AND B.id_secretaria=@Id_secretaria
                AND CONVERT(int, C.cod_ceco) <> B.legajo) AS TOTAL,
                (SELECT COUNT(*) 
                FROM FICHAS_RELEVAMIENTOS A
                WHERE CUIT IN 
	                (SELECT legajo FROM EMPLEADOS A
	                 WHERE legajo IN 
		                (SELECT A.cod_subceco
		                 FROM DIRECCION A
		                 INNER JOIN EMPLEADOS B ON CONVERT(int, A.cod_subceco)=B.legajo
		                 INNER JOIN SECRETARIA C ON C.Id_secretaria=B.id_secretaria
		                 WHERE A.ejercicio = @ejercicio AND B.id_secretaria=@Id_secretaria
		                 AND CONVERT(int, C.cod_ceco) <> B.legajo))
                AND A.ID_ESTADO = 2) AS NOTIFICADAS,
                (SELECT COUNT(*) 
                FROM FICHAS_RELEVAMIENTOS A
                WHERE CUIT IN 
	                (SELECT legajo FROM EMPLEADOS A
	                 WHERE legajo IN 
		                (SELECT A.cod_subceco
		                 FROM DIRECCION A
		                 INNER JOIN EMPLEADOS B ON CONVERT(int, A.cod_subceco)=B.legajo
		                 INNER JOIN SECRETARIA C ON C.Id_secretaria=B.id_secretaria
		                 WHERE A.ejercicio = @ejercicio AND B.id_secretaria=@Id_secretaria
		                 AND CONVERT(int, C.cod_ceco) <> B.legajo))
                AND A.ID_ESTADO = 3) AS FINALIZADAS,
                (SELECT COUNT(*) 
                FROM FICHAS_RELEVAMIENTOS A
                WHERE CUIT IN 
	                (SELECT legajo FROM EMPLEADOS A
	                 WHERE legajo IN 
		                (SELECT A.cod_subceco
		                 FROM DIRECCION A
		                 INNER JOIN EMPLEADOS B ON CONVERT(int, A.cod_subceco)=B.legajo
		                 INNER JOIN SECRETARIA C ON C.Id_secretaria=B.id_secretaria
		                 WHERE A.ejercicio = 2023 AND B.id_secretaria=@Id_secretaria
		                 AND CONVERT(int, C.cod_ceco) <> B.legajo))
                AND A.ID_ESTADO = 4) AS RECHAZADAS
                FROM SECRETARIA Z
                WHERE Z.ejercicio = @ejercicio AND Z.id_secretaria=@Id_secretaria AND Z.activa=1";
                    cmd.Parameters.AddWithValue("@Id_secretaria", idSecretaria);
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
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
