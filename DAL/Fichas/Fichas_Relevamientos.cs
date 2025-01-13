// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Fichas_Relevamientos
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Fichas
{
  public class Fichas_Relevamientos : DALBase
  {
    public int ID { get; set; }

    public int ID_FICHA { get; set; }

    public string CUIT { get; set; }

    public DateTime FECHA { get; set; }

    public string USUARIO_RELEVA { get; set; }

    public string OFICINA { get; set; }

    public string DIRECCION { get; set; }

    public string SECRETARIA { get; set; }

    public int ESTADO { get; set; }

    public string NOMBRE_ESTADO { get; set; }

    public Decimal RESULTADO { get; set; }

    public Fichas_Relevamientos()
    {
      this.ID = 0;
      this.ID_FICHA = 0;
      this.CUIT = string.Empty;
      this.FECHA = DateTime.Now;
      this.USUARIO_RELEVA = string.Empty;
      this.OFICINA = string.Empty;
      this.DIRECCION = string.Empty;
      this.SECRETARIA = string.Empty;
      this.ESTADO = 0;
      this.NOMBRE_ESTADO = string.Empty;
      this.RESULTADO = 0M;
    }

    private static List<Fichas_Relevamientos> mapeo(SqlDataReader dr)
    {
      List<Fichas_Relevamientos> fichasRelevamientosList = new List<Fichas_Relevamientos>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("ID_FICHA");
        int ordinal3 = dr.GetOrdinal("CUIT");
        int ordinal4 = dr.GetOrdinal("FECHA");
        int ordinal5 = dr.GetOrdinal("USUARIO_RELEVA");
        int ordinal6 = dr.GetOrdinal("OFICINA");
        int ordinal7 = dr.GetOrdinal("DIRECCION");
        int ordinal8 = dr.GetOrdinal("SECRETARIA");
        int ordinal9 = dr.GetOrdinal("ID_ESTADO");
        int ordinal10 = dr.GetOrdinal("NOMBRE_ESTADO");
        int ordinal11 = dr.GetOrdinal("RESULTADO");
        while (dr.Read())
        {
          Fichas_Relevamientos fichasRelevamientos = new Fichas_Relevamientos();
          if (!dr.IsDBNull(ordinal1))
            fichasRelevamientos.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            fichasRelevamientos.ID_FICHA = dr.GetInt32(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            fichasRelevamientos.CUIT = dr.GetString(ordinal3);
          if (!dr.IsDBNull(ordinal4))
            fichasRelevamientos.FECHA = dr.GetDateTime(ordinal4);
          if (!dr.IsDBNull(ordinal5))
            fichasRelevamientos.USUARIO_RELEVA = dr.GetString(ordinal5);
          if (!dr.IsDBNull(ordinal6))
            fichasRelevamientos.OFICINA = dr.GetString(ordinal6);
          if (!dr.IsDBNull(ordinal7))
            fichasRelevamientos.DIRECCION = dr.GetString(ordinal7);
          if (!dr.IsDBNull(ordinal8))
            fichasRelevamientos.SECRETARIA = dr.GetString(ordinal8);
          if (!dr.IsDBNull(ordinal9))
            fichasRelevamientos.ESTADO = dr.GetInt32(ordinal9);
          if (!dr.IsDBNull(ordinal10))
            fichasRelevamientos.NOMBRE_ESTADO = dr.GetString(ordinal10);
          if (!dr.IsDBNull(ordinal11))
            fichasRelevamientos.RESULTADO = dr.GetDecimal(ordinal11);
          fichasRelevamientosList.Add(fichasRelevamientos);
        }
      }
      return fichasRelevamientosList;
    }

    public static Fichas_Relevamientos getByPk(int pk)
    {
      try
      {
        string str = "SELECT A.*, B.NOMBRE AS NOMBRE_ESTADO \r\n                               FROM Fichas_relevamientos A\r\n                               INNER JOIN FICHAS_ESTADOS_EVALUACION B ON A.ID_ESTADO=B.ID\r\n                               WHERE A.ID = @ID";
        Fichas_Relevamientos byPk = (Fichas_Relevamientos) null;
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID", (object) pk);
          command.Connection.Open();
          List<Fichas_Relevamientos> fichasRelevamientosList = Fichas_Relevamientos.mapeo(command.ExecuteReader());
          if (fichasRelevamientosList.Count != 0)
            byPk = fichasRelevamientosList[0];
        }
        return byPk;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<Fichas_Relevamientos> read(int id, string cuit)
    {
      try
      {
        List<Fichas_Relevamientos> fichasRelevamientosList = new List<Fichas_Relevamientos>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT A.*, B.NOMBRE AS NOMBRE_ESTADO \r\n   " +
                        "FROM Fichas_relevamientos A\r\n                         " +
                        " INNER JOIN FICHAS_ESTADOS_EVALUACION B ON A.ID_ESTADO=B.ID\r\n " +
                        "  WHERE A.ID=@ID AND A.CUIT=@CUIT";
          command.Parameters.AddWithValue("@ID", (object) id);
          command.Parameters.AddWithValue("@CUIT", (object) cuit);
          command.Connection.Open();
          return Fichas_Relevamientos.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(Fichas_Relevamientos obj)
    {
      try
      {
        string str = "INSERT INTO Fichas_relevamientos\r\n             (ID_FICHA, CUIT, FECHA, USUARIO_RELEVA, OFICINA, DIRECCION, \r\n                SECRETARIA, ID_ESTADO, RESULTADO)\r\n             VALUES\r\n             (@ID_FICHA, @CUIT, @FECHA, @USUARIO_RELEVA, @OFICINA, @DIRECCION, \r\n                @SECRETARIA, 1, @RESULTADO);\r\n             SELECT SCOPE_IDENTITY()";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_FICHA", (object) obj.ID_FICHA);
          command.Parameters.AddWithValue("@CUIT", (object) obj.CUIT);
          command.Parameters.AddWithValue("@FECHA", (object) obj.FECHA);
          command.Parameters.AddWithValue("@USUARIO_RELEVA", (object) obj.USUARIO_RELEVA);
          command.Parameters.AddWithValue("@OFICINA", (object) obj.OFICINA);
          command.Parameters.AddWithValue("@DIRECCION", (object) obj.DIRECCION);
          command.Parameters.AddWithValue("@SECRETARIA", (object) obj.SECRETARIA);
          command.Parameters.AddWithValue("@RESULTADO", (object) obj.RESULTADO);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int actualizaEstado(int id, int estado, string cuit)
    {
      try
      {
        string str = "UPDATE Fichas_relevamientos\r\n                          SET ID_ESTADO=@ESTADO\r\n                          WHERE ID_FICHA=@ID_FICHA AND CUIT=@CUIT";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@ID_FICHA", (object) id);
          command.Parameters.AddWithValue("@ESTADO", (object) estado);
          command.Parameters.AddWithValue("@CUIT", (object) cuit);
          command.Connection.Open();
          return Convert.ToInt32(command.ExecuteScalar());
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
        string str = "\r\n                    DELETE  Fichas_relevamientos\r\n                    WHERE ID=@ID";
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

    public static void delete(string cuit)
    {
      try
      {
        string str = "\r\n                    DELETE  Fichas_relevamientos\r\n                    WHERE CUIT=@CUIT";
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = str.ToString();
          command.Parameters.AddWithValue("@CUIT", (object) cuit);
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
