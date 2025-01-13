// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Fichas_Relevamientos_Personas
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;


namespace BLL.Fichas
{
  public class Fichas_Relevamientos_Personas
  {
    public static DAL.Fichas.Fichas_Relevamientos_Personas getByPk(
      int idRelevamiento,
      int idPregunta,
      int idRespuesta)
    {
      try
      {
        return DAL.Fichas.Fichas_Relevamientos_Personas.getByPk(idRelevamiento, idPregunta, idRespuesta);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<DAL.Fichas.Fichas_Relevamientos_Personas> read(int idRelevamiento)
    {
      try
      {
        return DAL.Fichas.Fichas_Relevamientos_Personas.read(idRelevamiento);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void insert(DAL.Fichas.Fichas_Relevamientos_Personas obj)
    {
      try
      {
        DAL.Fichas.Fichas_Relevamientos_Personas.insert(obj);
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
        DAL.Fichas.Fichas_Relevamientos_Personas.delete(idRelevamiento, idPregunta, idRespuesta);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
