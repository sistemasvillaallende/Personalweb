// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_Preguntas
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Fichas
{
  public class Fichas_Preguntas : DALBase
  {
    public int ID { get; set; }

    public int ID_FICHA { get; set; }

    public string PREGUNTA { get; set; }

    public int TIPO_PREGUNTA { get; set; }

    public bool ACTIVA { get; set; }

    public int PREFERENCE { get; set; }

    public int ID_GRUPO { get; set; }

    public string NOMBRE_GRUPO { get; set; }

    public Fichas_Preguntas()
    {
      this.ID = 0;
      this.ID_FICHA = 0;
      this.PREGUNTA = string.Empty;
      this.TIPO_PREGUNTA = 0;
      this.ACTIVA = false;
      this.PREFERENCE = 0;
      this.ID_GRUPO = 0;
      this.NOMBRE_GRUPO = string.Empty;
    }

    private static List<Fichas_Preguntas> mapeo(SqlDataReader dr)
    {
      List<Fichas_Preguntas> fichasPreguntasList = new List<Fichas_Preguntas>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("ID_FICHA");
        int ordinal3 = dr.GetOrdinal("PREGUNTA");
        int ordinal4 = dr.GetOrdinal("TIPO_PREGUNTA");
        int ordinal5 = dr.GetOrdinal("ACTIVA");
        int ordinal6 = dr.GetOrdinal("PREFERENCE");
        int ordinal7 = dr.GetOrdinal("ID_GRUPO");
        int ordinal8 = dr.GetOrdinal("NOMBRE_GRUPO");
        while (dr.Read())
        {
          Fichas_Preguntas fichasPreguntas = new Fichas_Preguntas();
          if (!dr.IsDBNull(ordinal1))
            fichasPreguntas.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            fichasPreguntas.ID_FICHA = dr.GetInt32(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            fichasPreguntas.PREGUNTA = dr.GetString(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            fichasPreguntas.TIPO_PREGUNTA = dr.GetInt32(ordinal4);
          if (!dr.IsDBNull(ordinal5))
            fichasPreguntas.ACTIVA = dr.GetBoolean(ordinal5);
          if (!dr.IsDBNull(ordinal6))
            fichasPreguntas.PREFERENCE = dr.GetInt32(ordinal6);
          if (!dr.IsDBNull(ordinal7))
            fichasPreguntas.ID_GRUPO = dr.GetInt32(ordinal7);
          if (!dr.IsDBNull(ordinal8))
            fichasPreguntas.NOMBRE_GRUPO = dr.GetString(ordinal8);
          fichasPreguntasList.Add(fichasPreguntas);
        }
      }
      return fichasPreguntasList;
    }

    public static Fichas_Preguntas getByPk(int pk)
    {
      try
      {
        string str = "SELECT A.*, B.NOMBRE_GRUPO \r\n                        FROM Fichas_preguntas A\r\n                        INNER JOIN FICHAS_GRUPO_PREGUNTAS B ON A.ID_GRUPO=B.ID\r\n                        WHERE A.ID = @ID";
        Fichas_Preguntas byPk = (Fichas_Preguntas) null;
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) pk);
          command.Connection.Open();
          List<Fichas_Preguntas> fichasPreguntasList = Fichas_Preguntas.mapeo(command.ExecuteReader());
          if (fichasPreguntasList.Count != 0)
            byPk = fichasPreguntasList[0];
        }
        return byPk;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Preguntas> read(int idFicha)
    {
      try
      {
        List<Fichas_Preguntas> fichasPreguntasList = new List<Fichas_Preguntas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT A.*, B.NOMBRE_GRUPO \r\n                        FROM Fichas_preguntas A\r\n                        INNER JOIN FICHAS_GRUPO_PREGUNTAS B ON A.ID_GRUPO=B.ID\r\n                        WHERE A.ID_FICHA=@ID_FICHA\r\n                        ORDER BY PREFERENCE";
          command.Parameters.AddWithValue("ID_FICHA", (object) idFicha);
          command.Connection.Open();
          return Fichas_Preguntas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Preguntas> readActivas(int idFicha)
    {
      try
      {
        List<Fichas_Preguntas> fichasPreguntasList = new List<Fichas_Preguntas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = 
                        @"SELECT A.*, B.NOMBRE_GRUPO 
                        FROM Fichas_preguntas A
                        INNER JOIN FICHAS_GRUPO_PREGUNTAS B ON A.ID_GRUPO=B.ID
                        WHERE A.ID_FICHA=@ID_FICHA  AND ACTIVA=1";
          command.Parameters.AddWithValue("@ID_FICHA", idFicha);
          command.Connection.Open();
          return Fichas_Preguntas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Preguntas> readSeccionActivas(int idFicha, int idGrupo)
    {
      try
      {
        List<Fichas_Preguntas> fichasPreguntasList = new List<Fichas_Preguntas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT A.*, B.NOMBRE_GRUPO \r\n                        FROM Fichas_preguntas A\r\n                        INNER JOIN FICHAS_GRUPO_PREGUNTAS B ON A.ID_GRUPO=B.ID\r\n                        WHERE A.ID_FICHA=@ID_FICHA AND A.ID_GRUPO=@ID_GRUPO\r\n                        AND ACTIVA=1";
          command.Parameters.AddWithValue("@ID_FICHA", (object) idFicha);
          command.Parameters.AddWithValue("@ID_GRUPO", (object) idGrupo);
          command.Connection.Open();
          return Fichas_Preguntas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Preguntas> readSeccion(int idFicha, int idGrupo)
    {
      try
      {
        List<Fichas_Preguntas> fichasPreguntasList = new List<Fichas_Preguntas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT A.*, B.NOMBRE_GRUPO \r\n                        FROM Fichas_preguntas A\r\n                        INNER JOIN FICHAS_GRUPO_PREGUNTAS B ON A.ID_GRUPO=B.ID\r\n                        WHERE A.ID_FICHA=@ID_FICHA AND ID_GRUPO=@ID_GRUPO";
          command.Parameters.AddWithValue("@ID_FICHA", (object) idFicha);
          command.Parameters.AddWithValue("@ID_GRUPO", (object) idGrupo);
          command.Connection.Open();
          return Fichas_Preguntas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_Preguntas obj)
    {
      try
      {
        string str = "\r\n                INSERT INTO Fichas_preguntas\r\n                    (ID_FICHA, PREGUNTA, TIPO_PREGUNTA, ACTIVA, PREFERENCE, ID_GRUPO)\r\n                VALUES\r\n                    (@ID_FICHA, @PREGUNTA, @TIPO_PREGUNTA, @ACTIVA, @PREFERENCE, @ID_GRUPO)\r\n                SELECT SCOPE_IDENTITY()";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_FICHA", (object) obj.ID_FICHA);
          command.Parameters.AddWithValue("@PREGUNTA", (object) obj.PREGUNTA);
          command.Parameters.AddWithValue("@TIPO_PREGUNTA", (object) obj.TIPO_PREGUNTA);
          command.Parameters.AddWithValue("@ACTIVA", (object) obj.ACTIVA);
          command.Parameters.AddWithValue("@PREFERENCE", (object) obj.PREFERENCE);
          command.Parameters.AddWithValue("@ID_GRUPO", (object) obj.ID_GRUPO);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(int id, string pregunta, int idGrupo)
    {
      try
      {
        string str = "UPDATE  Fichas_preguntas SET\r\n                        PREGUNTA=@PREGUNTA,\r\n                        ID_GRUPO=@ID_GRUPO\r\n                      WHERE ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@PREGUNTA", (object) pregunta);
          command.Parameters.AddWithValue("@ID_GRUPO", (object) idGrupo);
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
        string str = "UPDATE  Fichas_preguntas SET\r\n                        ACTIVA=@ACTIVA\r\n                      WHERE ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@ACTIVA", (object) estado);
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
        string str = "UPDATE  Fichas_preguntas SET\r\n                        PREFERENCE=@PREFERENCE\r\n                      WHERE ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@PREFERENCE", (object) preference);
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
        string str = "DELETE  Fichas_preguntas\r\n                      WHERE ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) pk);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void deleteByFicha(int pk)
    {
      try
      {
        string str = "DELETE  Fichas_preguntas\r\n                      WHERE ID_FICHA=@ID_FICHA";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_FICHA", (object) pk);
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
