// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_grupo_preguntas
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
  public class Fichas_grupo_preguntas : DALBase
  {
    public int ID { get; set; }

    public string NOMBRE_GRUPO { get; set; }

    public int ID_FICHA { get; set; }

    public int ORDEN { get; set; }

    public bool ACTIVO { get; set; }

    public int TIPO { get; set; }

    public Fichas_grupo_preguntas()
    {
      this.ID = 0;
      this.NOMBRE_GRUPO = string.Empty;
      this.ID_FICHA = 0;
      this.ORDEN = 0;
      this.ACTIVO = false;
      this.TIPO = 0;
    }

    private static List<Fichas_grupo_preguntas> mapeo(SqlDataReader dr)
    {
      List<Fichas_grupo_preguntas> fichasGrupoPreguntasList = new List<Fichas_grupo_preguntas>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("NOMBRE_GRUPO");
        int ordinal3 = dr.GetOrdinal("ID_FICHA");
        int ordinal4 = dr.GetOrdinal("ORDEN");
        int ordinal5 = dr.GetOrdinal("ACTIVO");
        int ordinal6 = dr.GetOrdinal("TIPO");
        while (dr.Read())
        {
          Fichas_grupo_preguntas fichasGrupoPreguntas = new Fichas_grupo_preguntas();
          if (!dr.IsDBNull(ordinal1))
            fichasGrupoPreguntas.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            fichasGrupoPreguntas.NOMBRE_GRUPO = dr.GetString(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            fichasGrupoPreguntas.ID_FICHA = dr.GetInt32(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            fichasGrupoPreguntas.ORDEN = dr.GetInt32(ordinal4);
          if (!dr.IsDBNull(ordinal5))
            fichasGrupoPreguntas.ACTIVO = dr.GetBoolean(ordinal5);
          if (!dr.IsDBNull(ordinal6))
            fichasGrupoPreguntas.TIPO = dr.GetInt32(ordinal6);
          fichasGrupoPreguntasList.Add(fichasGrupoPreguntas);
        }
      }
      return fichasGrupoPreguntasList;
    }

    public static List<Fichas_grupo_preguntas> read(int idFicha)
    {
      try
      {
        List<Fichas_grupo_preguntas> fichasGrupoPreguntasList = new List<Fichas_grupo_preguntas>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM Fichas_grupo_preguntas\r\n                                        WHERE ID_FICHA=@ID_FICHA\r\n                                        ORDER BY ORDEN";
          command.Parameters.AddWithValue("@ID_FICHA", (object) idFicha);
          command.Connection.Open();
          return Fichas_grupo_preguntas.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Fichas_grupo_preguntas getByPk(int ID)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("SELECT *FROM Fichas_grupo_preguntas WHERE");
        stringBuilder.AppendLine("ID = @ID");
        Fichas_grupo_preguntas byPk = (Fichas_grupo_preguntas) null;
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@ID", (object) ID);
          command.Connection.Open();
          List<Fichas_grupo_preguntas> fichasGrupoPreguntasList = Fichas_grupo_preguntas.mapeo(command.ExecuteReader());
          if (fichasGrupoPreguntasList.Count != 0)
            byPk = fichasGrupoPreguntasList[0];
        }
        return byPk;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_grupo_preguntas obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("INSERT INTO Fichas_grupo_preguntas(");
        stringBuilder.AppendLine("NOMBRE_GRUPO");
        stringBuilder.AppendLine(", ID_FICHA");
        stringBuilder.AppendLine(", ORDEN");
        stringBuilder.AppendLine(", ACTIVO");
        stringBuilder.AppendLine(", TIPO");
        stringBuilder.AppendLine(")");
        stringBuilder.AppendLine("VALUES");
        stringBuilder.AppendLine("(");
        stringBuilder.AppendLine("@NOMBRE_GRUPO");
        stringBuilder.AppendLine(", @ID_FICHA");
        stringBuilder.AppendLine(", @ORDEN");
        stringBuilder.AppendLine(", @ACTIVO");
        stringBuilder.AppendLine(", @TIPO");
        stringBuilder.AppendLine(")");
        stringBuilder.AppendLine("SELECT SCOPE_IDENTITY()");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@NOMBRE_GRUPO", (object) obj.NOMBRE_GRUPO);
          command.Parameters.AddWithValue("@ID_FICHA", (object) obj.ID_FICHA);
          command.Parameters.AddWithValue("@ORDEN", (object) obj.ORDEN);
          command.Parameters.AddWithValue("@ACTIVO", (object) obj.ACTIVO);
          command.Parameters.AddWithValue("@TIPO", (object) obj.TIPO);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(Fichas_grupo_preguntas obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("UPDATE  Fichas_grupo_preguntas SET");
        stringBuilder.AppendLine("NOMBRE_GRUPO=@NOMBRE_GRUPO");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@NOMBRE_GRUPO", (object) obj.NOMBRE_GRUPO);
          command.Parameters.AddWithValue("@ID", (object) obj.ID);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void setOrden(int id, int orden)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("UPDATE  Fichas_grupo_preguntas SET");
        stringBuilder.AppendLine("ORDEN=@ORDEN");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@ORDEN", (object) orden);
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

    public static void activaDesactiva(int id, bool activa)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("UPDATE  Fichas_grupo_preguntas SET");
        stringBuilder.AppendLine("ACTIVO=@ACTIVO");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@ACTIVO", (object) activa);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void delete(int id)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("DELETE  Fichas_grupo_preguntas ");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
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
  }
}
