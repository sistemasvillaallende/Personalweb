using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PERMISOS_EVALUACION : DALBase
    {
        public int COD_PROCESO { get; set; }
        public int COD_USUARIO { get; set; }
        public int TIPO_PERMISO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string USUARIO { get; set; }
        public int id_secretaria { get; set; }
        public int id_direccion { get; set; }
        public string nombre_permiso { get; set; }
        public int legajo { get; set; }

        public PERMISOS_EVALUACION()
        {
            COD_PROCESO = 0;
            COD_USUARIO = 0;
            TIPO_PERMISO = 0;
            FECHA_ALTA = DateTime.Now;
            USUARIO = string.Empty;
            id_secretaria = 0;
            id_direccion = 0;
            nombre_permiso = string.Empty;
        }

        private static List<PERMISOS_EVALUACION> mapeo(SqlDataReader dr)
        {
            List<PERMISOS_EVALUACION> lst = new List<PERMISOS_EVALUACION>();
            PERMISOS_EVALUACION obj;
            if (dr.HasRows)
            {
                int COD_PROCESO = dr.GetOrdinal("COD_PROCESO");
                int COD_USUARIO = dr.GetOrdinal("COD_USUARIO");
                int TIPO_PERMISO = dr.GetOrdinal("TIPO_PERMISO");
                int FECHA_ALTA = dr.GetOrdinal("FECHA_ALTA");
                int USUARIO = dr.GetOrdinal("USUARIO");
                int id_secretaria = dr.GetOrdinal("id_secretaria");
                int id_direccion = dr.GetOrdinal("id_direccion");
                int nombre_permiso = dr.GetOrdinal("nombre_permiso");

                while (dr.Read())
                {
                    obj = new PERMISOS_EVALUACION();
                    if (!dr.IsDBNull(COD_PROCESO)) { obj.COD_PROCESO = dr.GetInt32(COD_PROCESO); }
                    if (!dr.IsDBNull(COD_USUARIO)) { obj.COD_USUARIO = dr.GetInt32(COD_USUARIO); }
                    if (!dr.IsDBNull(TIPO_PERMISO)) { obj.TIPO_PERMISO = dr.GetInt32(TIPO_PERMISO); }
                    if (!dr.IsDBNull(FECHA_ALTA)) { obj.FECHA_ALTA = dr.GetDateTime(FECHA_ALTA); }
                    if (!dr.IsDBNull(USUARIO)) { obj.USUARIO = dr.GetString(USUARIO); }
                    if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }
                    if (!dr.IsDBNull(id_direccion)) { obj.id_direccion = dr.GetInt32(id_direccion); }
                    if (!dr.IsDBNull(nombre_permiso)) { obj.nombre_permiso = dr.GetString(nombre_permiso); }
                    lst.Add(obj);
                }
            }
            return lst;
        }



        public static void InsertPermisoSecretaria(PERMISOS_EVALUACION obj)
        {
            try
            {
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"
                        DELETE PROCESOS_X_USUARIO_V2
                        WHERE COD_PROCESO=468 AND COD_USUARIO=@COD_USUARIO;

                        INSERT INTO PROCESOS_X_USUARIO_V2
                        (COD_PROCESO, COD_USUARIO, TIPO_PERMISO, FECHA_ALTA, USUARIO)
                        VALUES
                        (468, @COD_USUARIO, 1, GETDATE(), @USUARIO);
                        INSERT INTO PERMISOS_EVALUACION
                        (cod_usuario, id_secretaria, id_direccion, nombre_permiso)
                        VALUES
                        (@legajo, @id_secretaria, 0, 'EVALUACION_SECRETARIOS')";

                    cmd.Parameters.AddWithValue("@COD_USUARIO", obj.COD_USUARIO);
                    cmd.Parameters.AddWithValue("@USUARIO", obj.USUARIO);
                    cmd.Parameters.AddWithValue("@id_secretaria", obj.id_secretaria);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void QuitarPermisoSecretaria(PERMISOS_EVALUACION obj)
        {
            try
            {
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"DELETE PROCESOS_X_USUARIO_V2
                          WHERE COD_PROCESO=468 AND 
                          COD_USUARIO=@COD_USUARIO;

                        DELETE PERMISOS_EVALUACION
                        WHERE COD_USUARIO=@legajo
                        AND id_secretaria=@id_secretaria;";

                    cmd.Parameters.AddWithValue("@COD_USUARIO", obj.COD_USUARIO);
                    cmd.Parameters.AddWithValue("@id_secretaria", obj.id_secretaria);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertPermisoDireccion(PERMISOS_EVALUACION obj)
        {
            try
            {
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"
                        DELETE PROCESOS_X_USUARIO_V2
                        WHERE COD_PROCESO=469 AND COD_USUARIO=@COD_USUARIO;

                        INSERT INTO PROCESOS_X_USUARIO_V2
                        (COD_PROCESO, COD_USUARIO, TIPO_PERMISO, FECHA_ALTA, USUARIO)
                        VALUES
                        (469, @COD_USUARIO, 1, GETDATE(), 'mvelez')

                        INSERT INTO PERMISOS_EVALUACION
                        (cod_usuario, id_secretaria, id_direccion, nombre_permiso)
                        VALUES
                        (@legajo, 0, @id_direccion, 'EVALUACION_DIRECTORES')";

                    cmd.Parameters.AddWithValue("@COD_USUARIO", obj.COD_USUARIO);
                    cmd.Parameters.AddWithValue("@USUARIO", obj.USUARIO);
                    cmd.Parameters.AddWithValue("@id_direccion", obj.id_direccion);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void QuitarPermisoDireccion(PERMISOS_EVALUACION obj)
        {
            try
            {
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"DELETE PROCESOS_X_USUARIO_V2
                          WHERE COD_PROCESO=468 AND 
                          COD_USUARIO=@COD_USUARIO;

                        DELETE PERMISOS_EVALUACION
                        WHERE COD_USUARIO=@legajo
                        AND id_direccion=@id_direccion;";

                    cmd.Parameters.AddWithValue("@COD_USUARIO", obj.COD_USUARIO);
                    cmd.Parameters.AddWithValue("@id_direccion", obj.id_secretaria);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
