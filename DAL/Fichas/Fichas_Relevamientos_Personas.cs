// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_Relevamientos_Personas
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL.Fichas
{
  public class Fichas_Relevamientos_Personas : DALBase
  {
    public int ID_RELEVAMIENTO { get; set; }

    public int ID_PREGUNTA { get; set; }

    public int ID_RESPUESTA { get; set; }

    public string TEXTO_PREGUNTA { get; set; }

    public string TEXTO_RESPUESTA { get; set; }

    public string NOMBRE_GRUPO { get; set; }

    public int ID_GRUPO { get; set; }

    public int TIPO { get; set; }

    public int PUNTUACION { get; set; }

    public string USUARIO_RELEVA { get; set; }

    public Fichas_Relevamientos_Personas()
    {
      this.ID_RELEVAMIENTO = 0;
      this.ID_PREGUNTA = 0;
      this.ID_RESPUESTA = 0;
      this.TEXTO_PREGUNTA = string.Empty;
      this.TEXTO_RESPUESTA = string.Empty;
      this.NOMBRE_GRUPO = string.Empty;
      this.ID_GRUPO = 0;
      this.TIPO = 0;
      this.PUNTUACION = 0;
      this.USUARIO_RELEVA = "";
    }

    private static List<Fichas_Relevamientos_Personas> mapeo(SqlDataReader dr)
    {
      List<Fichas_Relevamientos_Personas> relevamientosPersonasList = new List<Fichas_Relevamientos_Personas>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID_RELEVAMIENTO");
        int ordinal2 = dr.GetOrdinal("ID_PREGUNTA");
        int ordinal3 = dr.GetOrdinal("ID_RESPUESTA");
        int ordinal4 = dr.GetOrdinal("TEXTO_PREGUNTA");
        int ordinal5 = dr.GetOrdinal("TEXTO_RESPUESTA");
        int ordinal6 = dr.GetOrdinal("NOMBRE_GRUPO");
        int ordinal7 = dr.GetOrdinal("ID_GRUPO");
        int ordinal8 = dr.GetOrdinal("TIPO");
        int ordinal9 = dr.GetOrdinal("PUNTUACION");
        int ordinal10 = dr.GetOrdinal("USUARIO_RELEVA");
        while (dr.Read())
        {
          Fichas_Relevamientos_Personas relevamientosPersonas = new Fichas_Relevamientos_Personas();
          if (!dr.IsDBNull(ordinal1))
            relevamientosPersonas.ID_RELEVAMIENTO = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            relevamientosPersonas.ID_PREGUNTA = dr.GetInt32(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            relevamientosPersonas.ID_RESPUESTA = dr.GetInt32(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            relevamientosPersonas.TEXTO_PREGUNTA = dr.GetString(ordinal4);
          if (!dr.IsDBNull(ordinal5))
            relevamientosPersonas.TEXTO_RESPUESTA = dr.GetString(ordinal5);
          if (!dr.IsDBNull(ordinal6))
            relevamientosPersonas.NOMBRE_GRUPO = dr.GetString(ordinal6);
          if (!dr.IsDBNull(ordinal7))
            relevamientosPersonas.ID_GRUPO = dr.GetInt32(ordinal7);
          if (!dr.IsDBNull(ordinal8))
            relevamientosPersonas.TIPO = dr.GetInt32(ordinal8);
          if (!dr.IsDBNull(ordinal9))
            relevamientosPersonas.PUNTUACION = dr.GetInt32(ordinal9);
          if (!dr.IsDBNull(ordinal10))
            relevamientosPersonas.USUARIO_RELEVA = dr.GetString(ordinal10);
          relevamientosPersonasList.Add(relevamientosPersonas);
        }
      }
      return relevamientosPersonasList;
    }

    public static Fichas_Relevamientos_Personas getByPk(
      int ID_RELEVAMIENTO,
      int ID_PREGUNTA,
      int ID_RESPUESTA)
    {
      try
      {
        string str = "\r\n                    SELECT A.*, C.NOMBRE_GRUPO, B.ID_GRUPO, C.TIPO, \r\n                    D.PUNTUACION, E.USUARIO_RELEVA\r\n                    FROM Fichas_relevamientos_personas A\r\n                    INNER JOIN FICHAS_PREGUNTAS B ON A.ID_PREGUNTA=B.ID\r\n                    INNER JOIN FICHAS_GRUPO_PREGUNTAS C ON B.ID_GRUPO = C.ID\r\n                    INNER JOIN FICHAS_RESPUESTAS D ON A.ID_RESPUESTA=D.ID\r\n                    INNER JOIN FICHAS_RELEVAMIENTOS E ON A.ID_RELEVAMIENTO=E.ID\r\n                    WHERE ID_RELEVAMIENTO = @ID_RELEVAMIENTO AND \r\n                    ID_PREGUNTA = @ID_PREGUNTA AND \r\n                    ID_RESPUESTA = @ID_RESPUESTA";
        Fichas_Relevamientos_Personas byPk = (Fichas_Relevamientos_Personas) null;
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_RELEVAMIENTO", (object) ID_RELEVAMIENTO);
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) ID_PREGUNTA);
          command.Parameters.AddWithValue("@ID_RESPUESTA", (object) ID_RESPUESTA);
          command.Connection.Open();
          List<Fichas_Relevamientos_Personas> relevamientosPersonasList = Fichas_Relevamientos_Personas.mapeo(command.ExecuteReader());
          if (relevamientosPersonasList.Count != 0)
            byPk = relevamientosPersonasList[0];
        }
        return byPk;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Relevamientos_Personas> read(int idRelevamiento)
    {
      try
      {
        try
        {
          List<Fichas_Relevamientos_Personas> relevamientosPersonasList = new List<Fichas_Relevamientos_Personas>();
          using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
          {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "\r\n                                SELECT A.*, C.NOMBRE_GRUPO, B.ID_GRUPO, C.TIPO, \r\n                                D.PUNTUACION, E.USUARIO_RELEVA\r\n                                FROM Fichas_relevamientos_personas A\r\n                                INNER JOIN FICHAS_PREGUNTAS B ON A.ID_PREGUNTA=B.ID\r\n                                LEFT JOIN FICHAS_GRUPO_PREGUNTAS C ON B.ID_GRUPO = C.ID\r\n                                LEFT JOIN FICHAS_RESPUESTAS D ON A.ID_RESPUESTA=D.ID\r\n                                INNER JOIN FICHAS_RELEVAMIENTOS E ON A.ID_RELEVAMIENTO=E.ID\r\n                                WHERE ID_RELEVAMIENTO=@ID_RELEVAMIENTO";
            command.Parameters.AddWithValue("@ID_RELEVAMIENTO", (object) idRelevamiento);
            command.Connection.Open();
            return Fichas_Relevamientos_Personas.mapeo(command.ExecuteReader());
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_Relevamientos_Personas obj)
    {
      try
      {
        string str = "\r\n                INSERT INTO Fichas_relevamientos_personas\r\n                    (ID_RELEVAMIENTO, ID_PREGUNTA, ID_RESPUESTA, TEXTO_PREGUNTA, \r\n                    TEXTO_RESPUESTA)\r\n                VALUES\r\n                    (@ID_RELEVAMIENTO, @ID_PREGUNTA, @ID_RESPUESTA, @TEXTO_PREGUNTA\r\n                    , @TEXTO_RESPUESTA)";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_RELEVAMIENTO", (object) obj.ID_RELEVAMIENTO);
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) obj.ID_PREGUNTA);
          command.Parameters.AddWithValue("@ID_RESPUESTA", (object) obj.ID_RESPUESTA);
          command.Parameters.AddWithValue("@TEXTO_PREGUNTA", (object) obj.TEXTO_PREGUNTA);
          command.Parameters.AddWithValue("@TEXTO_RESPUESTA", (object) obj.TEXTO_RESPUESTA);
          command.Connection.Open();
          return command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void insert(List<Fichas_Relevamientos_Personas> lst)
    {
      try
      {
        string str = "\r\n                INSERT INTO Fichas_relevamientos_personas\r\n                    (ID_RELEVAMIENTO, ID_PREGUNTA, ID_RESPUESTA, TEXTO_PREGUNTA, \r\n                    TEXTO_RESPUESTA)\r\n                VALUES\r\n                    (@ID_RELEVAMIENTO, @ID_PREGUNTA, @ID_RESPUESTA, @TEXTO_PREGUNTA\r\n                    , @TEXTO_RESPUESTA)";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str;
          command.Parameters.Add("@ID_RELEVAMIENTO", SqlDbType.Int);
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) SqlDbType.Int);
          command.Parameters.AddWithValue("@ID_RESPUESTA", (object) SqlDbType.Int);
          command.Parameters.AddWithValue("@TEXTO_PREGUNTA", (object) SqlDbType.VarChar);
          command.Parameters.AddWithValue("@TEXTO_RESPUESTA", (object) SqlDbType.VarChar);
          command.Connection.Open();
          foreach (Fichas_Relevamientos_Personas relevamientosPersonas in lst)
          {
            command.Parameters["@ID_RELEVAMIENTO"].Value = (object) relevamientosPersonas.ID_RELEVAMIENTO;
            command.Parameters["@ID_PREGUNTA"].Value = (object) relevamientosPersonas.ID_PREGUNTA;
            command.Parameters["@ID_RESPUESTA"].Value = (object) relevamientosPersonas.ID_RESPUESTA;
            command.Parameters["@TEXTO_PREGUNTA"].Value = (object) relevamientosPersonas.TEXTO_PREGUNTA;
            command.Parameters["@TEXTO_RESPUESTA"].Value = (object) relevamientosPersonas.TEXTO_RESPUESTA;
            command.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void delete(int idRelevamiento, int idPregunta, int idRespuesta)
    {
      try
      {
        string str = "\r\n                    DELETE Fichas_relevamientos_personas\r\n                    WHERE ID_RELEVAMIENTO=@ID_RELEVAMIENTO\r\n                        AND ID_PREGUNTA=@ID_PREGUNTA\r\n                        AND ID_RESPUESTA=@ID_RESPUESTA";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_RELEVAMIENTO", (object) idRelevamiento);
          command.Parameters.AddWithValue("@ID_PREGUNTA", (object) idPregunta);
          command.Parameters.AddWithValue("@ID_RESPUESTA", (object) idRespuesta);
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
