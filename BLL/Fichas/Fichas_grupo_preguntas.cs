// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Fichas_grupo_preguntas
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;

namespace BLL.Fichas
{
  public class Fichas_grupo_preguntas
  {
    public static List<DAL.Fichas.Fichas_grupo_preguntas> read(int idFicha)
    {
      try
      {
        return DAL.Fichas.Fichas_grupo_preguntas.read(idFicha);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static DAL.Fichas.Fichas_grupo_preguntas getByPk(int ID)
    {
      try
      {
        return DAL.Fichas.Fichas_grupo_preguntas.getByPk(ID);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(DAL.Fichas.Fichas_grupo_preguntas obj)
    {
      try
      {
        return DAL.Fichas.Fichas_grupo_preguntas.insert(obj);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(DAL.Fichas.Fichas_grupo_preguntas obj)
    {
      try
      {
        DAL.Fichas.Fichas_grupo_preguntas.update(obj);
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
        DAL.Fichas.Fichas_grupo_preguntas.setOrden(id, orden);
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
        DAL.Fichas.Fichas_grupo_preguntas.activaDesactiva(id, activa);
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
        DAL.Fichas.Fichas_grupo_preguntas.delete(id);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
