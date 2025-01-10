using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Direcciones_grilla : DALBase
    {
        public int id_direccion { get; set; }
        public string DIRECCION { get; set; }
        public string DIRECTOR { get; set; }
        public int OFICINAS { get; set; }
        public int PERSONAL { get; set; }
        public int id_secretaria { get; set; }
        public int legajo { get; set; }
        public int COD_USUARIO { get; set; }
        public bool PERMISO { get; set; }

        public Direcciones_grilla()
        {
            id_direccion = 0;
            DIRECCION = string.Empty;
            DIRECTOR = string.Empty;
            OFICINAS = 0;
            PERSONAL = 0;
            id_secretaria = 0;
            legajo = 0;
            COD_USUARIO = 0;
            PERMISO = false;
        }
        static string CapitalizarPalabras(string input)
        {
            // Utilizamos CultureInfo para manejar diferentes culturas
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            // Dividimos la cadena en palabras
            string[] palabras = input.Trim().Split(' ');

            // Creamos un StringBuilder para construir la cadena capitalizada
            StringBuilder resultado = new StringBuilder();

            // Capitalizamos la primera letra de cada palabra y reconstruimos la cadena
            foreach (string palabra in palabras)
            {
                if (resultado.Length > 0)
                {
                    resultado.Append(' '); // Agregamos un espacio entre palabras
                }
                if (palabra.Length > 0)
                {
                    // Capitalizamos la primera letra y añadimos el resto de la palabra en minúsculas
                    resultado.Append(char.ToUpper(palabra[0], cultureInfo));
                    resultado.Append(palabra.Substring(1).ToLower(cultureInfo));
                }
            }

            return resultado.ToString();
        }
        private static List<Direcciones_grilla> mapeo(SqlDataReader dr, bool permiso)
        {
            List<Direcciones_grilla> lst = new List<Direcciones_grilla>();
            Direcciones_grilla obj;
            if (dr.HasRows)
            {
                int id_direccion = dr.GetOrdinal("id_direccion");
                int DIRECCION = dr.GetOrdinal("DIRECCION");
                int DIRECTOR = dr.GetOrdinal("DIRECTOR");
                int PERSONAL = dr.GetOrdinal("PERSONAL");
                int id_secretaria = dr.GetOrdinal("id_secretaria");

                int legajo = dr.GetOrdinal("legajo");
                int COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                int PERMISO = 0;
                if (permiso)
                {
                    PERMISO = dr.GetOrdinal("PERMISO");
                }
                while (dr.Read())
                {
                    obj = new Direcciones_grilla();
                    if (!dr.IsDBNull(id_direccion)) { obj.id_direccion = dr.GetInt32(id_direccion); }
                    if (!dr.IsDBNull(DIRECCION))
                    {
                        obj.DIRECCION =
                            CapitalizarPalabras(dr.GetString(DIRECCION));
                    }
                    if (!dr.IsDBNull(DIRECTOR))
                    {
                        obj.DIRECTOR =
                            CapitalizarPalabras(dr.GetString(DIRECTOR));
                    }
                    if (!dr.IsDBNull(PERSONAL))
                    {
                        obj.PERSONAL = dr.GetInt32(PERSONAL);
                    }
                    if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }

                    if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }

                    if (!dr.IsDBNull(COD_USUARIO)) { obj.COD_USUARIO = dr.GetInt32(COD_USUARIO); }
                    if (permiso)
                    {
                        if (!dr.IsDBNull(PERMISO)) { obj.PERMISO = true; }
                    }

                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Direcciones_grilla> read(int ejercicio, int id_secretaria)
        {
            try
            {
                List<Direcciones_grilla> lst = new List<Direcciones_grilla>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.Id_direccion, A.Descripcion AS Direccion, 
ISNULL(D.nombre, '') AS 'Director',
(SELECT COUNT(*) FROM EMPLEADOS WHERE Id_direccion=A.Id_direccion)
AS 'PERSONAL', B.Id_secretaria, D.legajo, X.COD_USUARIO,
(SELECT z.nombre_permiso FROM PERMISOS_EVALUACION z 
WHERE z.COD_USUARIO=D.legajo 
AND z.nombre_permiso='EVALUACION_DIRECTORES' AND
id_direccion=a.Id_direccion
AND X.COD_USUARIO IN (SELECT COD_USUARIO FROM PROCESOS_X_USUARIO_V2 PRO
WHERE PRO.COD_PROCESO=469)) AS PERMISO
FROM DIRECCION A
INNER JOIN DIRECCION_X_SECRETARIA B ON 
A.Id_direccion=B.Id_direccion AND B.activo=1 AND 
B.ejercicio=@ejercicio AND B.Id_secretaria=@id_secretaria
INNER JOIN DIRECCION C ON B.Id_direccion=C.Id_direccion 
FULL JOIN PROGRAMAS_PUBLICOS E ON B.Id_programa=E.Id_programa
FULL JOIN EMPLEADOS D ON CONVERT(INT,A.cod_subceco)=D.legajo
FULL JOIN USUARIOS_V2 X ON X.LEGAJO=D.legajo
AND C.activa=1 AND C.ejercicio=@ejercicio
WHERE A.activa=1 AND A.ejercicio=@ejercicio
GROUP BY A.Descripcion, D.nombre,A.Id_direccion, 
B.Id_secretaria, D.legajo, X.COD_USUARIO";
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@id_secretaria", id_secretaria);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr, true);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Direcciones_grilla getByPk(int idDireccion, int ejercicio)
        {
            try
            {
                Direcciones_grilla obj = null;
                List<Direcciones_grilla> lst = new List<Direcciones_grilla>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.Id_direccion, A.Descripcion AS DIRECCION, 
                        D.nombre AS 'DIRECTOR',
                        (SELECT COUNT(*) FROM EMPLEADOS WHERE id_direccion=A.id_direccion)
                        AS 'PERSONAL', B.Id_secretaria, D.legajo, X.COD_USUARIO
                        FROM DIRECCION A
                        INNER JOIN DIRECCION_X_SECRETARIA B ON 
                        A.id_direccion=B.id_direccion AND B.activo=1 AND B.ejercicio=2023
                        FULL JOIN PROGRAMAS_PUBLICOS E ON B.id_oficina=E.Id_programa
                        FULL JOIN EMPLEADOS D ON CONVERT(INT,A.cod_subceco)=D.legajo
                        FULL JOIN USUARIOS_V2 X ON X.LEGAJO=D.legajo
                        WHERE A.activa=1 AND A.ejercicio=@ejercicio
                        AND A.id_direccion=@id_direccion
                        GROUP BY A.Descripcion, D.nombre,A.id_direccion, 
                        B.Id_secretaria, D.legajo, X.COD_USUARIO";
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@id_direccion", idDireccion);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr, false);
                    if (lst.Count() > 0)
                        obj = lst[0];
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
