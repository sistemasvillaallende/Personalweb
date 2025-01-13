// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Fichas_Respuestas
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;


namespace BLL.Fichas
{
  public class Fichas_Respuestas
  {
    public static List<DAL.Fichas.Fichas_Respuestas> read(int idPregunta)
    {
      try
      {
        return DAL.Fichas.Fichas_Respuestas.read(idPregunta);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static DAL.Fichas.Fichas_Respuestas getByPk(int id)
    {
      try
      {
        return DAL.Fichas.Fichas_Respuestas.getByPk(id);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<DAL.Fichas.Fichas_Respuestas> readActivas(int idPregunta)
    {
      try
      {
        return DAL.Fichas.Fichas_Respuestas.readActivas(idPregunta);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void insert(DAL.Fichas.Fichas_Respuestas obj)
    {
      try
      {
        DAL.Fichas.Fichas_Respuestas.insert(obj);
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
        DAL.Fichas.Fichas_Respuestas.update(id, respuesta, puntuacion);
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
        DAL.Fichas.Fichas_Respuestas.updateActiva(id, estado);
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
        DAL.Fichas.Fichas_Respuestas.delete(pk);
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
        DAL.Fichas.Fichas_Respuestas.deleteByPregunta(pk);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
