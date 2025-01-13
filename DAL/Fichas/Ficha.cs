// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Ficha
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Fichas
{
    public class Ficha : DALBase
    {
        public int ID { get; set; }

        public string NOMBRE { get; set; }

        public string ICONO { get; set; }

        public bool ACTIVO { get; set; }

        public int PREFERENCE { get; set; }

        public string NOTIFICACION { get; set; }
        public int EJERCICIO { get; set; }

        public Ficha()
        {
            this.ID = 0;
            this.NOMBRE = string.Empty;
            this.ICONO = string.Empty;
            this.ACTIVO = false;
            this.NOTIFICACION = string.Empty;
            EJERCICIO = 0;
        }

        private static List<Ficha> mapeo(SqlDataReader dr)
        {
            List<Ficha> fichaList = new List<Ficha>();
            if (dr.HasRows)
            {
                int ID = dr.GetOrdinal("ID");
                int NOMBRE = dr.GetOrdinal("NOMBRE");
                int ICONO = dr.GetOrdinal("ICONO");
                int ACTIVO = dr.GetOrdinal("ACTIVO");
                int PREFERENCE = dr.GetOrdinal("PREFERENCE");
                int NOTIFICACION = dr.GetOrdinal("NOTIFICACION");
                int EJERCICIO = dr.GetOrdinal("EJERCICIO");
                while (dr.Read())
                {
                    Ficha ficha = new Ficha();
                    if (!dr.IsDBNull(ID))
                        ficha.ID = dr.GetInt32(ID);
                    if (!dr.IsDBNull(NOMBRE))
                        ficha.NOMBRE = dr.GetString(NOMBRE);
                    if (!dr.IsDBNull(ICONO))
                        ficha.ICONO = dr.GetString(ICONO);
                    if (!dr.IsDBNull(ACTIVO))
                        ficha.ACTIVO = dr.GetBoolean(ACTIVO);
                    if (!dr.IsDBNull(PREFERENCE))
                        ficha.PREFERENCE = dr.GetInt32(PREFERENCE);
                    if (!dr.IsDBNull(NOTIFICACION))
                        ficha.NOTIFICACION = dr.GetString(NOTIFICACION);
                    if (!dr.IsDBNull(EJERCICIO))
                        ficha.EJERCICIO = dr.GetInt32(EJERCICIO);
                    fichaList.Add(ficha);
                }
            }
            return fichaList;
        }

        public static Ficha getByPk(int ID)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT *FROM Fichas WHERE");
                stringBuilder.AppendLine("ID = @ID");
                Ficha byPk = (Ficha)null;
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@ID", (object)ID);
                    command.Connection.Open();
                    List<Ficha> fichaList = Ficha.mapeo(command.ExecuteReader());
                    if (fichaList.Count != 0)
                        byPk = fichaList[0];
                }
                return byPk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Ficha> read()
        {
            try
            {
                List<Ficha> fichaList = new List<Ficha>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT *FROM Fichas ORDER BY ID DESC";
                    command.Connection.Open();
                    return Ficha.mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Ficha> readActivas()
        {
            try
            {
                List<Ficha> fichaList = new List<Ficha>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT *FROM Fichas WHERE ACTIVO=1";
                    command.Connection.Open();
                    return Ficha.mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(Ficha obj)
        {
            try
            {
                string str =
                    @"INSERT INTO Fichas
                      (NOMBRE, ICONO, ACTIVO, PREFERENCE, NOTIFICACION, EJERCICIO)
                      VALUES
                      (@NOMBRE, @ICONO, @ACTIVO, @PREFERENCE, @NOTIFICACION, YEAR(GETDATE()));
                      SELECT SCOPE_IDENTITY()";
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@NOMBRE", (object)obj.NOMBRE);
                    command.Parameters.AddWithValue("@ICONO", (object)obj.ICONO);
                    command.Parameters.AddWithValue("@ACTIVO", (object)obj.ACTIVO);
                    command.Parameters.AddWithValue("@PREFERENCE", (object)obj.PREFERENCE);
                    command.Parameters.AddWithValue("@NOTIFICACION", (object)obj.NOTIFICACION);
                    command.Connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Ficha obj)
        {
            try
            {
                string str = "\r\n                    UPDATE  Fichas SET\r\n                        NOMBRE=@NOMBRE\r\n                        ICONO=@ICONO\r\n                        ACTIVO=@ACTIVO\r\n                        PREFERENCE=@PREFERENCE\r\n                    WHERE ID=@ID";
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@NOMBRE", (object)obj.NOMBRE);
                    command.Parameters.AddWithValue("@ICONO", (object)obj.ICONO);
                    command.Parameters.AddWithValue("@ACTIVO", (object)obj.ACTIVO);
                    command.Parameters.AddWithValue("@PREFERENCE", (object)obj.PREFERENCE);
                    command.Parameters.AddWithValue("@ID", (object)obj.ID);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateActiva(int id, bool estado)
        {
            try
            {
                string str = "\r\n                    UPDATE  Fichas SET\r\n                        ACTIVO=@ACTIVO\r\n                    WHERE ID=@ID";
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@ACTIVO", (object)estado);
                    command.Parameters.AddWithValue("@ID", (object)id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updatePreference(int id, int preference)
        {
            try
            {
                string str = "\r\n                    UPDATE  Fichas SET\r\n                        PREFERENCE=@PREFERENCE\r\n                    WHERE ID=@ID";
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@PREFERENCE", (object)preference);
                    command.Parameters.AddWithValue("@ID", (object)id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateNotificacion(int id, string notificacion)
        {
            try
            {
                string str = "\r\n                    UPDATE  Fichas SET\r\n                        NOTIFICACION=@NOTIFICACION\r\n                    WHERE ID=@ID";
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@NOTIFICACION", (object)notificacion);
                    command.Parameters.AddWithValue("@ID", (object)id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int pk)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("DELETE  Fichas ");
                stringBuilder.AppendLine("WHERE");
                stringBuilder.AppendLine("ID=@ID");
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@ID", (object)pk);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
