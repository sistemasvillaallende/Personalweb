using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UsuariosDAL
    {
        public static List<Usuarios> getUserByOffice(int codOffice)
        {
            Usuarios oUsu = new Usuarios();
            List<Usuarios> lstUser = new List<Usuarios>();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT cod_usuario, nombre, nombre_completo FROM usuarios_v2");
            strSQL.AppendLine("WHERE cod_oficina = @cod");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod", codOffice));

            try
            {
                cn = DALBase.GetConnection("Siimva");

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oUsu = new Usuarios();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_usuario")))
                        oUsu.codUsuario = dr.GetInt32((dr.GetOrdinal("cod_usuario")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) oUsu.nombre =
                        dr.GetString((dr.GetOrdinal("nombre")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_completo")))
                        oUsu.nombre_completo = dr.GetString(dr.GetOrdinal("nombre_completo"));
                    lstUser.Add(oUsu);
                    //if (!dr.IsDBNull(dr.GetOrdinal("activo"))) oOficina.activo = dr.GetByte(dr.GetOrdinal("activo"));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstUser;
        }
        public static Usuarios getUserByNombre(string nombre)
        {
            Usuarios oUsu = new Usuarios();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT cod_usuario, nombre, nombre_completo FROM usuarios_v2");
            strSQL.AppendLine("WHERE nombre = @nombre");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));

            try
            {
                cn = DALBase.GetConnection("Siimva");

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oUsu = new Usuarios();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_usuario")))
                        oUsu.codUsuario = dr.GetInt32((dr.GetOrdinal("cod_usuario")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) oUsu.nombre =
                        dr.GetString((dr.GetOrdinal("nombre")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_completo")))
                        oUsu.nombre_completo = dr.GetString(dr.GetOrdinal("nombre_completo"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("activo"))) oOficina.activo = dr.GetByte(dr.GetOrdinal("activo"));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oUsu;
        }
        public static Usuarios getPermisoEvaluacion(string nombre)
        {
            Usuarios oUsu = new Usuarios();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;

            cmd = new SqlCommand();

            try
            {
                cn = DALBase.GetConnection("Siimva");

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =
                    @"SELECT A.*, B.ID_SECRETARIA, B.ID_DIRECCION, 
                        C.Descripcion AS SECRETARIA, D.Descripcion AS DIRECCION 
                        FROM USUARIOS_V2 A
                        INNER JOIN PERMISOS_EVALUACION B ON A.LEGAJO=B.cod_usuario
                        RIGHT JOIN SECRETARIA C ON B.id_secretaria=C.Id_secretaria
                        RIGHT JOIN DIRECCION D ON B.Id_direccion=D.Id_direccion
                        WHERE nombre = @nombre";


                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oUsu = new Usuarios();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_usuario")))
                        oUsu.codUsuario = dr.GetInt32((dr.GetOrdinal("cod_usuario")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) oUsu.nombre =
                        dr.GetString((dr.GetOrdinal("nombre")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_completo")))
                        oUsu.nombre_completo = dr.GetString(dr.GetOrdinal("nombre_completo"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_SECRETARIA")))
                        oUsu.id_secretaria = dr.GetInt32((dr.GetOrdinal("ID_SECRETARIA")));
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_DIRECCION")))
                        oUsu.id_direccion = dr.GetInt32((dr.GetOrdinal("ID_DIRECCION")));

                    if (!dr.IsDBNull(dr.GetOrdinal("SECRETARIA")))
                        oUsu.secretaria = dr.GetString((dr.GetOrdinal("SECRETARIA")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                        oUsu.direccion = dr.GetString((dr.GetOrdinal("DIRECCION")));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oUsu;
        }
    }
}
