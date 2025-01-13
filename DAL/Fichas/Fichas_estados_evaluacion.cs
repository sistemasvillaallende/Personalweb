// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_estados_evaluacion
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
  public class Fichas_estados_evaluacion : DALBase
  {
    public int ID { get; set; }

    public string NOMBRE { get; set; }

    public int TOLERANCIA_EN_DIAS { get; set; }

    public string TIPO_DIAS { get; set; }

    public Fichas_estados_evaluacion()
    {
      this.ID = 0;
      this.NOMBRE = string.Empty;
      this.TOLERANCIA_EN_DIAS = 0;
      this.TIPO_DIAS = string.Empty;
    }

    private static List<Fichas_estados_evaluacion> mapeo(SqlDataReader dr)
    {
      List<Fichas_estados_evaluacion> estadosEvaluacionList = new List<Fichas_estados_evaluacion>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("NOMBRE");
        int ordinal3 = dr.GetOrdinal("TOLERANCIA_EN_DIAS");
        int ordinal4 = dr.GetOrdinal("TIPO_DIAS");
        while (dr.Read())
        {
          Fichas_estados_evaluacion estadosEvaluacion = new Fichas_estados_evaluacion();
          if (!dr.IsDBNull(ordinal1))
            estadosEvaluacion.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            estadosEvaluacion.NOMBRE = dr.GetString(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            estadosEvaluacion.TOLERANCIA_EN_DIAS = dr.GetInt32(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            estadosEvaluacion.TIPO_DIAS = dr.GetString(ordinal4);
          estadosEvaluacionList.Add(estadosEvaluacion);
        }
      }
      return estadosEvaluacionList;
    }

    public static List<Fichas_estados_evaluacion> read()
    {
      try
      {
        List<Fichas_estados_evaluacion> estadosEvaluacionList = new List<Fichas_estados_evaluacion>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM Fichas_estados_evaluacion";
          command.Connection.Open();
          return Fichas_estados_evaluacion.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Fichas_estados_evaluacion getByPk(int ID)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("SELECT *FROM Fichas_estados_evaluacion WHERE");
        stringBuilder.AppendLine("ID = @ID");
        Fichas_estados_evaluacion byPk = (Fichas_estados_evaluacion) null;
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@ID", (object) ID);
          command.Connection.Open();
          List<Fichas_estados_evaluacion> estadosEvaluacionList = Fichas_estados_evaluacion.mapeo(command.ExecuteReader());
          if (estadosEvaluacionList.Count != 0)
            byPk = estadosEvaluacionList[0];
        }
        return byPk;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_estados_evaluacion obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("INSERT INTO Fichas_estados_evaluacion(");
        stringBuilder.AppendLine("NOMBRE");
        stringBuilder.AppendLine(", TOLERANCIA_EN_DIAS");
        stringBuilder.AppendLine(", TIPO_DIAS");
        stringBuilder.AppendLine(")");
        stringBuilder.AppendLine("VALUES");
        stringBuilder.AppendLine("(");
        stringBuilder.AppendLine("@NOMBRE");
        stringBuilder.AppendLine(", @TOLERANCIA_EN_DIAS");
        stringBuilder.AppendLine(", @TIPO_DIAS");
        stringBuilder.AppendLine(")");
        stringBuilder.AppendLine("SELECT SCOPE_IDENTITY()");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@NOMBRE", (object) obj.NOMBRE);
          command.Parameters.AddWithValue("@TOLERANCIA_EN_DIAS", (object) obj.TOLERANCIA_EN_DIAS);
          command.Parameters.AddWithValue("@TIPO_DIAS", (object) obj.TIPO_DIAS);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(Fichas_estados_evaluacion obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("UPDATE  Fichas_estados_evaluacion SET");
        stringBuilder.AppendLine("NOMBRE=@NOMBRE");
        stringBuilder.AppendLine(", TOLERANCIA_EN_DIAS=@TOLERANCIA_EN_DIAS");
        stringBuilder.AppendLine(", TIPO_DIAS=@TIPO_DIAS");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@NOMBRE", (object) obj.NOMBRE);
          command.Parameters.AddWithValue("@TOLERANCIA_EN_DIAS", (object) obj.TOLERANCIA_EN_DIAS);
          command.Parameters.AddWithValue("@TIPO_DIAS", (object) obj.TIPO_DIAS);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void delete(Fichas_estados_evaluacion obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("DELETE  Fichas_estados_evaluacion ");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("ID=@ID");
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
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
  }
}
