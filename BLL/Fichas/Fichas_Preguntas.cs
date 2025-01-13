// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Fichas_Preguntas
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;
using System.Transactions;


namespace BLL.Fichas
{
  public class Fichas_Preguntas
  {
    public static DAL.Fichas.Fichas_Preguntas getByPk(int pk)
    {
      try
      {
        return DAL.Fichas.Fichas_Preguntas.getByPk(pk);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<DAL.Fichas.Fichas_Preguntas> read(int idFicha)
    {
      try
      {
        return DAL.Fichas.Fichas_Preguntas.read(idFicha);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static List<DAL.Fichas.Fichas_Preguntas> readActivas(int idFicha)
    {
      try
      {
        return DAL.Fichas.Fichas_Preguntas.readActivas(idFicha);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insert(DAL.Fichas.Fichas_Preguntas obj)
    {
      try
      {
        return DAL.Fichas.Fichas_Preguntas.insert(obj);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int insertRespuestasPorcentuales(DAL.Fichas.Fichas_Preguntas obj)
    {
      try
      {
        int idPregunta = 0;
        using (TransactionScope transactionScope = new TransactionScope())
        {
          idPregunta = DAL.Fichas.Fichas_Preguntas.insert(obj);
          DAL.Fichas.Fichas_Respuestas.insert(idPregunta);
          transactionScope.Complete();
        }
        return idPregunta;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(int id, string pregunta, int idGrupo)
    {
      try
      {
        DAL.Fichas.Fichas_Preguntas.update(id, pregunta, idGrupo);
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
        DAL.Fichas.Fichas_Preguntas.updateActiva(id, estado);
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
        DAL.Fichas.Fichas_Preguntas.delete(pk);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void deleteByFicha(int pk)
    {
      try
      {
        DAL.Fichas.Fichas_Preguntas.deleteByFicha(pk);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
