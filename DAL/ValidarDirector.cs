using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ValidarDirector : DALBase
    {
        public static bool ValidaDireccion(string cod_usuario)
        {
            bool permiso = false;
            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection cn = null;

            cn = DALBase.GetConnection("SIIMVA");
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT *FROM DIRECCION
                                WHERE cod_subceco = @cod_usuario";

            cmd.Parameters.AddWithValue("@legcod_usuariojo", cod_usuario);
            cmd.Connection.Open();

            try
            {
                //cn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    permiso = true;
                    reader.Close();
                }
                return permiso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autorización de Permiso!!!.");
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
