// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.fontawesome
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Fichas
{
  public class fontawesome : DALBase
  {
    public int ID { get; set; }

    public string CLASE { get; set; }

    public fontawesome()
    {
      this.ID = 0;
      this.CLASE = string.Empty;
    }

    private static List<fontawesome> mapeo(SqlDataReader dr)
    {
      List<fontawesome> fontawesomeList = new List<fontawesome>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("ID");
        int ordinal2 = dr.GetOrdinal("CLASE");
        while (dr.Read())
        {
          fontawesome fontawesome = new fontawesome();
          if (!dr.IsDBNull(ordinal1))
            fontawesome.ID = dr.GetInt32(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            fontawesome.CLASE = dr.GetString(ordinal2);
          fontawesomeList.Add(fontawesome);
        }
      }
      return fontawesomeList;
    }

    public static List<fontawesome> read()
    {
      try
      {
        List<fontawesome> fontawesomeList = new List<fontawesome>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "SELECT *FROM [Font-awesome]";
          command.Connection.Open();
          return fontawesome.mapeo(command.ExecuteReader());
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
