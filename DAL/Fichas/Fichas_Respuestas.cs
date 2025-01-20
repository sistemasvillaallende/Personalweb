// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_Respuestas
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
  public class Fichas_Respuestas : DALBase
  {
    public int ID { get; set; }

    public int ID_PREGUNTA { get; set; }

    public string TEXTO { get; set; }

    public bool ACTIVA { get; set; }

    public int REFERENCE { get; set; }

    public int PUNTUACION { get; set; }

    public Fichas_Respuestas()
    {
      this.ID = 0;
      this.ID_PREGUNTA = 0;
      this.TEXTO = string.Empty;
      this.ACTIVA = false;
      this.REFERENCE = 0;
      this.PUNTUACION = 0;
    }

    private static List<Fichas_Respuestas> mapeo(SqlDataReader dr)
    {
      List<Fichas_Respuestas> fichasRespuestasList = new List<Fichas_Respuestas>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("ID_PREGUNTA");
        int ordinal3 = dr.GetOrdinal("TEXTO");
        int ordinal4 = dr.GetOrdinal("ACTIVA");
        int ordinal5 = dr.GetOrdinal("REFERENCE");
        int ordinal6 = dr.GetOrdinal("PUNTUACION");
        while (dr.Read())
        {
          Fichas_Respuestas fichasRespuestas = new Fichas_Respuestas();
          if (!dr.IsDBNull(ordinal1))
            fichasRespuestas.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            fichasRespuestas.ID_PREGUNTA = dr.GetInt32(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            fichasRespuestas.TEXTO = dr.GetString(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            fichasRespuestas.ACTIVA = dr.GetBoolean(ordinal4);
          if (!dr.IsDBNull(ordinal5))
            fichasRespuestas.REFERENCE = dr.GetInt32(ordinal5);
          if (!dr.IsDBNull(ordinal6))
            fichasRespuestas.PUNTUACION = dr.GetInt32(ordinal6);
          fichasRespuestasList.Add(fichasRespuestas);
        }
      }
      return fichasRespuestasList;
    }

    public static Fichas_Respuestas getByPk(int id)
    {
      try
      {
        Fichas_Respuestas byPk = (Fichas_Respuestas) null;
        List<Fichas_Respuestas> fichasRespuestasList1 = new List<Fichas_Respuestas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM Fichas_respuestas\r\n                                        WHERE ID=@ID";
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Connection.Open();
          List<Fichas_Respuestas> fichasRespuestasList2 = Fichas_Respuestas.mapeo(command.ExecuteReader());
          if (fichasRespuestasList2.Count != 0)
            byPk = fichasRespuestasList2[0];
          return byPk;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Respuestas> read(int idPregunta)
    {
      try
      {
        List<Fichas_Respuestas> fichasRespuestasList = new List<Fichas_Respuestas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM Fichas_respuestas\r\n                                        WHERE ID_PREGUNTA=@ID_PREGUNTA";
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) idPregunta);
          command.Connection.Open();
          return Fichas_Respuestas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Respuestas> readActivas(int idPregunta)
    {
      try
      {
        List<Fichas_Respuestas> fichasRespuestasList = new List<Fichas_Respuestas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM Fichas_respuestas\r\n                                        WHERE ID_PREGUNTA=@ID_PREGUNTA\r\n                                        AND ACTIVA=1";
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) idPregunta);
          command.Connection.Open();
          return Fichas_Respuestas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_Respuestas obj)
    {
      try
      {
        string str = "\r\n                    INSERT INTO Fichas_respuestas\r\n                        (ID_PREGUNTA, TEXTO, ACTIVA, REFERENCE, PUNTUACION)\r\n                    VALUES\r\n                        (@ID_PREGUNTA, @TEXTO, @ACTIVA, @REFERENCE, @PUNTUACION);\r\n                    SELECT SCOPE_IDENTITY()";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) obj.ID_PREGUNTA);
          command.Parameters.AddWithValue("@TEXTO", (object) obj.TEXTO);
          command.Parameters.AddWithValue("@ACTIVA", (object) obj.ACTIVA);
          command.Parameters.AddWithValue("@REFERENCE", (object) obj.REFERENCE);
          command.Parameters.AddWithValue("@PUNTUACION", (object) obj.PUNTUACION);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void insert(int idPregunta)
    {
      try
      {
        string str = "\r\n                    INSERT INTO Fichas_respuestas\r\n                        (ID_PREGUNTA, TEXTO, ACTIVA, REFERENCE, PUNTUACION)\r\n                    VALUES\r\n                        (@ID_PREGUNTA, 'Sobresaliente 100%', 1, 1, 100),\r\n                        (@ID_PREGUNTA, 'Bueno 75%', 1, 2, 75),\r\n                        (@ID_PREGUNTA, 'Regular 50%', 1, 3, 50),\r\n                        (@ID_PREGUNTA, 'Deficiente 25%', 1, 4, 25)";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) idPregunta);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(int id, string respuesta, int puntuacion)
    {
      try
      {
        string str = "\r\n                    UPDATE  Fichas_respuestas SET\r\n                        TEXTO=@TEXTO,\r\n                        PUNTUACION=@PUNTUACION\r\n                    WHERE\r\n                        ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@TEXTO", (object) respuesta);
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@PUNTUACION", (object) puntuacion);
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
        string str = "\r\n                    UPDATE  Fichas_respuestas SET\r\n                        ACTIVA=@ACTIVA,\r\n                    WHERE\r\n                        ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ACTIVA", (object) estado);
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void updateReference(int id, int reference)
    {
      try
      {
        string str = "\r\n                    UPDATE  Fichas_respuestas SET\r\n                        REFERENCE=@REFERENCE,\r\n                    WHERE\r\n                        ID=@ID";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@REFERENCE", (object) reference);
          command.Parameters.AddWithValue("@ID", (object) id);
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
        stringBuilder.AppendLine("DELETE Fichas_respuestas ");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
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

    public static void deleteByPregunta(int pk)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("DELETE Fichas_respuestas ");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID_PREGUNTA=@ID_PREGUNTA");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) pk);
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
