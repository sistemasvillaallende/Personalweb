// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Resultados_x_filtro
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Fichas
{
  public class Resultados_x_filtro : DALBase
  {
    public string CONTRATACION { get; set; }

    public int EVALUACIONES_REALIZADAS { get; set; }

    public int RESULTADO_PROMEDIO { get; set; }

    public string SECRETARIA { get; set; }

    public string DIRECCION { get; set; }

    public Resultados_x_filtro()
    {
      this.CONTRATACION = string.Empty;
      this.EVALUACIONES_REALIZADAS = 0;
      this.RESULTADO_PROMEDIO = 0;
      this.SECRETARIA = string.Empty;
      this.DIRECCION = string.Empty;
    }

    private static List<Resultados_x_filtro> mapeo(SqlDataReader dr)
    {
      List<Resultados_x_filtro> resultadosXFiltroList = new List<Resultados_x_filtro>();
      if (dr.HasRows)
      {
        int ordinal1 = dr.GetOrdinal("CONTRATACION");
        int ordinal2 = dr.GetOrdinal("EVALUACIONES_REALIZADAS");
        int ordinal3 = dr.GetOrdinal("RESULTADO_PROMEDIO");
        while (dr.Read())
        {
          Resultados_x_filtro resultadosXFiltro = new Resultados_x_filtro();
          if (!dr.IsDBNull(ordinal1))
            resultadosXFiltro.CONTRATACION = dr.GetString(ordinal1);
          if (!dr.IsDBNull(ordinal2))
            resultadosXFiltro.EVALUACIONES_REALIZADAS = dr.GetInt32(ordinal2);
          if (!dr.IsDBNull(ordinal3))
            resultadosXFiltro.RESULTADO_PROMEDIO = dr.GetInt32(ordinal3);
          resultadosXFiltroList.Add(resultadosXFiltro);
        }
      }
      return resultadosXFiltroList;
    }

    public static object read(int ID_FICHA)
    {
      try
      {
        List<Resultados_x_filtro> resultadosXFiltroList1 = new List<Resultados_x_filtro>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "\r\n                    SELECT \r\n\t                    CLASIFICACION AS CONTRATACION,\r\n\t                    COUNT(*) AS EVALUACIONES_REALIZADAS, \r\n\t                    AVG(RESULTADO) RESULTADO_PROMEDIO\r\n                    FROM VISTA_EVALUACION_CON_RESULTADO\r\n                    WHERE ID_FICHA = @ID_FICHA\r\n                    GROUP BY CLASIFICACION";
          command.Parameters.AddWithValue("@ID_FICHA", (object) ID_FICHA);
          command.Connection.Open();
          List<Resultados_x_filtro> resultadosXFiltroList2 = Resultados_x_filtro.mapeo(command.ExecuteReader());
          ResultadosEvaluacion resultadosEvaluacion = new ResultadosEvaluacion();
          foreach (Resultados_x_filtro resultadosXFiltro in resultadosXFiltroList2)
          {
            resultadosEvaluacion.lstSeries.Add(resultadosXFiltro.CONTRATACION.Trim());
            resultadosEvaluacion.lstValores.Add(resultadosXFiltro.RESULTADO_PROMEDIO.ToString());
          }
          return (object) resultadosEvaluacion;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static object getBySecretaria(int ID_FICHA, int id_secretaria)
    {
      try
      {
        List<Resultados_x_filtro> resultadosXFiltroList1 = new List<Resultados_x_filtro>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "\r\n                    SELECT \r\n\t                    CLASIFICACION AS CONTRATACION,\r\n\t                    COUNT(*) AS EVALUACIONES_REALIZADAS, \r\n\t                    AVG(RESULTADO) RESULTADO_PROMEDIO\r\n                    FROM VISTA_EVALUACION_CON_RESULTADO\r\n                    WHERE ID_FICHA = @ID_FICHA AND id_secretaria=@id_secretaria\r\n                    GROUP BY CLASIFICACION";
          command.Parameters.AddWithValue("@ID_FICHA", (object) ID_FICHA);
          command.Parameters.AddWithValue("@id_secretaria", (object) id_secretaria);
          command.Connection.Open();
          List<Resultados_x_filtro> resultadosXFiltroList2 = Resultados_x_filtro.mapeo(command.ExecuteReader());
          ResultadosEvaluacion bySecretaria = new ResultadosEvaluacion();
          foreach (Resultados_x_filtro resultadosXFiltro in resultadosXFiltroList2)
          {
            bySecretaria.lstSeries.Add(resultadosXFiltro.CONTRATACION.Trim());
            bySecretaria.lstValores.Add(resultadosXFiltro.RESULTADO_PROMEDIO.ToString());
          }
          return (object) bySecretaria;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static object getByDireccion(int ID_FICHA, int id_direccion)
    {
      try
      {
        List<Resultados_x_filtro> resultadosXFiltroList1 = new List<Resultados_x_filtro>();
        using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "\r\n                    SELECT \r\n\t                    CLASIFICACION AS CONTRATACION,\r\n\t                    COUNT(*) AS EVALUACIONES_REALIZADAS, \r\n\t                    AVG(RESULTADO) RESULTADO_PROMEDIO\r\n                    FROM VISTA_EVALUACION_CON_RESULTADO\r\n                    WHERE ID_FICHA = @ID_FICHA AND id_direccion=@id_direccion\r\n                    GROUP BY CLASIFICACION";
          command.Parameters.AddWithValue("@ID_FICHA", (object) ID_FICHA);
          command.Parameters.AddWithValue("@id_direccion", (object) id_direccion);
          command.Connection.Open();
          List<Resultados_x_filtro> resultadosXFiltroList2 = Resultados_x_filtro.mapeo(command.ExecuteReader());
          ResultadosEvaluacion byDireccion = new ResultadosEvaluacion();
          foreach (Resultados_x_filtro resultadosXFiltro in resultadosXFiltroList2)
          {
            byDireccion.lstSeries.Add(resultadosXFiltro.CONTRATACION.Trim());
            byDireccion.lstValores.Add(resultadosXFiltro.RESULTADO_PROMEDIO.ToString());
          }
          return (object) byDireccion;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
