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
    public class Secretarias_grilla : DALBase
    {
        public int id_secretaria { get; set; }
        public string SECRETARIA { get; set; }
        public string SECRETARIO { get; set; }
        public int DIRECCIONES { get; set; }
        public int PERSONAL { get; set; }
        public bool PERMISO { get; set; }
        public int legajo { get; set; }
        public int COD_USUARIO { get; set; }

        public Secretarias_grilla()
        {
            id_secretaria = 0;
            SECRETARIA = string.Empty;
            SECRETARIO = string.Empty;
            DIRECCIONES = 0;
            PERSONAL = 0;
            PERMISO = false;
            legajo = 0;
            COD_USUARIO = 0;
        }
        static string CapitalizarPalabras(string input)
        {
            // Utilizamos CultureInfo para manejar diferentes culturas
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            // Dividimos la cadena en palabras
            string[] palabras = input.Split(' ');

            // Creamos un StringBuilder para construir la cadena capitalizada
            StringBuilder resultado = new StringBuilder();

            // Capitalizamos la primera letra de cada palabra y reconstruimos la cadena
            foreach (string palabra in palabras)
            {
                if (resultado.Length > 0)
                {
                    resultado.Append(' '); // Agregamos un espacio entre palabras
                }

                // Capitalizamos la primera letra y añadimos el resto de la palabra en minúsculas
                resultado.Append(char.ToUpper(palabra[0], cultureInfo));
                resultado.Append(palabra.Substring(1).ToLower(cultureInfo));
            }

            return resultado.ToString();
        }
        private static List<Secretarias_grilla> mapeo(SqlDataReader dr, bool permiso)
        {
            List<Secretarias_grilla> lst = new List<Secretarias_grilla>();
            Secretarias_grilla obj;
            if (dr.HasRows)
            {
                int id_secretaria = dr.GetOrdinal("id_secretaria");
                int SECRETARIA = dr.GetOrdinal("SECRETARIA");
                int SECRETARIO = dr.GetOrdinal("SECRETARIO");
                int DIRECCIONES = dr.GetOrdinal("DIRECCIONES");
                int PERSONAL = dr.GetOrdinal("PERSONAL");
                int legajo = dr.GetOrdinal("legajo");
                int COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                int PERMISO = 0;
                if (permiso)
                {
                    PERMISO = dr.GetOrdinal("PERMISO");
                }
                while (dr.Read())
                {
                    obj = new Secretarias_grilla();
                    if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }
                    if (!dr.IsDBNull(SECRETARIA)) { obj.SECRETARIA =
                            CapitalizarPalabras(dr.GetString(SECRETARIA)); }
                    if (!dr.IsDBNull(SECRETARIO)) { obj.SECRETARIO =
                            CapitalizarPalabras(dr.GetString(SECRETARIO)); }
                    if (!dr.IsDBNull(DIRECCIONES)) { obj.DIRECCIONES = dr.GetInt32(DIRECCIONES); }
                    if (!dr.IsDBNull(PERSONAL)) { obj.PERSONAL = dr.GetInt32(PERSONAL); }
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

        public static List<Secretarias_grilla> read(int ejercicio)
        {
            try
            {
                List<Secretarias_grilla> lst = new List<Secretarias_grilla>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.id_secretaria, A.Descripcion AS SECRETARIA, 
D.nombre AS 'SECRETARIO', COUNT(*) AS 'DIRECCIONES',
(SELECT COUNT(*) FROM EMPLEADOS WHERE id_secretaria=A.id_secretaria)
AS 'PERSONAL', D.legajo, X.COD_USUARIO,
(SELECT z.nombre_permiso FROM PERMISOS_EVALUACION z 
                        WHERE z.COD_USUARIO=D.legajo 
                        AND z.nombre_permiso='EVALUACION_SECRETARIOS' AND
                        id_secretaria=a.id_secretaria
						AND X.COD_USUARIO IN (SELECT COD_USUARIO FROM PROCESOS_X_USUARIO_V2 PRO
						WHERE PRO.COD_PROCESO=468)) AS PERMISO
FROM SECRETARIA A
INNER JOIN DIRECCION_X_SECRETARIA B ON 
A.Id_secretaria=B.Id_secretaria AND B.activo=1 AND B.ejercicio=@ejercicio
INNER JOIN DIRECCION C ON B.Id_direccion=C.Id_direccion 
FULL JOIN PROGRAMAS_PUBLICOS E ON B.id_oficina=E.Id_programa
FULL JOIN EMPLEADOS D ON CONVERT(INT,A.cod_ceco)=D.legajo
FULL JOIN USUARIOS_V2 X ON X.LEGAJO=D.legajo
AND C.activa=1 AND C.ejercicio=@ejercicio
WHERE A.activa=1 AND A.ejercicio=@ejercicio
AND A.id_secretaria NOT IN (33, 45)
GROUP BY A.Descripcion, D.nombre,A.id_secretaria, D.legajo, X.COD_USUARIO";


                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
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
        public static Secretarias_grilla getByPk(int idSecretaria, int ejercicio)
        {
            try
            {
                List<Secretarias_grilla> lst = new List<Secretarias_grilla>();
                Secretarias_grilla obj = null;
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.id_secretaria, A.Descripcion AS SECRETARIA, 
                        D.nombre AS 'SECRETARIO', COUNT(*) AS 'DIRECCIONES',
                        (SELECT COUNT(*) FROM EMPLEADOS WHERE id_secretaria=A.id_secretaria)
                        AS 'PERSONAL', D.legajo, X.COD_USUARIO
                        FROM SECRETARIA A
                        INNER JOIN DIRECCION_X_SECRETARIA B ON 
                        A.Id_secretaria=B.Id_secretaria AND B.activo=1 AND B.ejercicio=@ejercicio
                        INNER JOIN DIRECCION C ON B.Id_direccion=C.Id_direccion 
                        FULL JOIN PROGRAMAS_PUBLICOS E ON B.id_oficina=E.Id_programa
                        FULL JOIN EMPLEADOS D ON CONVERT(INT,A.cod_ceco)=D.legajo
                        FULL JOIN USUARIOS_V2 X ON X.LEGAJO=D.legajo
                        AND C.activa=1 AND C.ejercicio=@ejercicio
                        WHERE A.activa=1 AND A.ejercicio=@ejercicio
                        AND A.id_secretaria=@id_secretaria
                        GROUP BY A.Descripcion, D.nombre,A.id_secretaria, D.legajo, X.COD_USUARIO";
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@id_secretaria", idSecretaria);
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

