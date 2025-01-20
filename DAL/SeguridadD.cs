using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class SeguridadD
    {
        public SeguridadD()
        {
        }


        #region Vieja Seguridad
        public bool ValidUser(string user, string password, out string nombreusuario)
        {
            string strSQL = string.Empty;
            bool resultado = false;
            string md5Passwd = string.Empty;
            string md5Passwd_ = string.Empty;
            bool mExiste = false;

            user = user.Replace("'", "").Replace(",", "").Replace("=", "");
            strSQL = @"SELECT * 
                       FROM USUARIOS_V2 
                       WHERE nombre=@user";
            MD5Encryption objMD5 = new MD5Encryption();
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add(new SqlParameter("@user", user));
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        mExiste = true;
                        dr.Read();
                        nombreusuario = dr.GetString(dr.GetOrdinal("nombre"));
                        md5Passwd = dr.GetString(dr.GetOrdinal("passwd"));
                        md5Passwd_ = objMD5.EncryptMD5(password.Trim().ToUpper() + user.Trim().ToUpper());
                        if (md5Passwd == md5Passwd_)
                            resultado = true;
                        else
                            resultado = false;
                        dr.Close();
                    }
                    else
                    {
                        nombreusuario = "";
                        mExiste = false;
                        resultado = false;
                    }
                    dr.Close();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autenticación!!!.");
            }
            finally
            {
                objMD5 = null;
            }
        }

        public bool ValidaPermiso(string user, string proceso)
        {
            string strSQL = "";
            strSQL = @"SELECT *
                    FROM PROCESOS_V2 a
                    JOIN PROCESOS_x_USUARIO_V2 b on
                    a.cod_proceso = b.cod_proceso
                    JOIN USUARIOS_V2 c on
                    c.cod_usuario = b.cod_usuario
                    WHERE
                    c.nombre = @user AND
                    a.proceso = @proceso";

            MD5Encryption objMD5 = new MD5Encryption();
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@proceso", proceso);
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        //strSQL = "SELECT * FROM USUARIOS_V2 WHERE ";
                        //strSQL += "administrador=1 AND ";
                        //strSQL += "nombre='" + user + "'";
                        strSQL = @"SELECT *
                                FROM PROCESOS_V2 a
                                JOIN PROCESOS_x_USUARIO_V2 b on
                                a.cod_proceso = b.cod_proceso
                                JOIN USUARIOS_V2 c on
                                c.cod_usuario = b.cod_usuario
                                WHERE
                                c.nombre = @user AND
                                a.proceso = 'S_ADMIN'";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("@user", user);
                        cmd.CommandText = strSQL;
                        reader.Close();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            return true;
                        }
                        else
                        {
                            reader.Close();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autorización de Permiso!!!.");
            }

        }

        public bool ValidaPermiso(string user, string proceso, out int id_oficina)
        {
            string strSQL = "";
            strSQL = @"SELECT *
                    FROM PROCESOS_V2 a
                    JOIN PROCESOS_x_USUARIO_V2 b on
                    a.cod_proceso = b.cod_proceso
                    JOIN USUARIOS_V2 c on
                    c.cod_usuario = b.cod_usuario
                    WHERE
                    c.nombre = @user AND
                    a.proceso = @proceso";
            MD5Encryption objMD5 = new MD5Encryption();
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@proceso", proceso);
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id_oficina = Convert.IsDBNull(reader["cod_oficina"]) ? 0 : Convert.ToInt16(reader["cod_oficina"]);
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        //strSQL = "SELECT * FROM USUARIOS_V2 WHERE ";
                        //strSQL += "administrador=1 AND ";
                        //strSQL += "nombre='" + User + "'";
                        strSQL = @"SELECT *
                                FROM PROCESOS_V2 a
                                JOIN PROCESOS_X_USUARIO_V2 b on
                                a.cod_proceso = b.cod_proceso
                                JOIN USUARIOS_V2 c on
                                c.cod_usuario = b.cod_usuario
                                WHERE
                                c.nombre = @user AND
                                a.proceso = 'S_ADMIN'";
                        reader.Close();
                        cmd.CommandText = strSQL;
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            id_oficina = Convert.IsDBNull(reader["cod_oficina"]) ? 0 : Convert.ToInt16(reader["cod_oficina"]);
                            reader.Close();
                            return true;
                        }
                        else
                        {
                            reader.Close();
                            id_oficina = 0;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autorización de Permiso!!!.");
            }
        }

        #endregion

        #region Seguridad Nueva
        public bool Autenticar(string user, string password, out string nombreUsuario)
        {
            string strSQL = "";
            bool resultado = false;
            string md5Passwd = "";
            string md5Passwd_ = "";
            bool mExiste = false;

            user = user.Replace("'", "").Replace(",", "").Replace("=", "");
            strSQL += "SELECT * From USUARIOS_V2 WHERE login='" + user + "'";


            SqlConnection cn = null;
            cn = DALBase.GetConnection("SIIMVA");
            MD5Encryption objMD5 = new MD5Encryption();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    mExiste = true;
                    reader.Read();
                    nombreUsuario = Convert.IsDBNull(reader["nombres"]) ? "" : Convert.ToString(reader["nombres"]);
                    md5Passwd = Convert.IsDBNull(reader["password"]) ? "" : Convert.ToString(reader["password"]);
                    md5Passwd_ = objMD5.EncryptMD5(password.Trim().ToUpper() + user.Trim().ToUpper());
                    if (md5Passwd == md5Passwd_)
                        resultado = true;
                    else
                        resultado = false;
                    reader.Close();
                }
                else
                {
                    nombreUsuario = "";
                    mExiste = false;
                    resultado = false;
                }
                reader.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autenticación!!!.");
            }
            finally
            {
                cn.Close();
                cmd = null;
                objMD5 = null;
            }
        }

        public bool AutorizaOpcionesMenu(int id_opcion, string login)
        {
            bool autoriza = false;
            string strSQL = " ";
            strSQL += "SELECT  *    ";
            strSQL += "FROM SE_OPCIONES_X_USUARIO a ";
            strSQL += "join SE_USUARIO b on ";
            strSQL += "a.id_usuario=b.id_usuario ";
            strSQL += "WHERE b.login='" + login + "' ";
            strSQL += "AND a.id_opcion=" + id_opcion.ToString();

            SqlConnection cn = null;
            SqlCommand cmd = null;
            cn = DALBase.GetConnection("SIIMVA");
            SqlDataReader reader = null;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                autoriza = true;
            }
            cn.Close();
            return autoriza;
        }

        public DataSet OpcionesMenu()
        {

            string strSQL = " ";

            strSQL += "SELECT  *    ";
            strSQL += "FROM SE_OPCIONES ";
            DataSet ds = new DataSet();
            ds = DALBase.Pagination("Opciones", strSQL, 0, 0, "", "");

            return ds;
        }

        public string MenuFuncion(int id_opcion)
        {
            string strSQL = " ";

            strSQL += "SELECT  *    ";
            strSQL += "FROM SE_OPCIONES ";
            strSQL += "WHERE id_opcion=" + id_opcion.ToString();
            DataSet ds = new DataSet();
            ds = DALBase.Pagination("Opciones", strSQL, 0, 0, "", "");
            string strFuncion = ds.Tables[0].Rows[0]["Funcion"].ToString();
            return strFuncion;
        }
        #endregion


    }
}
