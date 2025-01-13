// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Fichas_Relevamientos
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL.Fichas
{
    public class Fichas_Relevamientos
    {
        public static DAL.Fichas.Fichas_Relevamientos getByPk(int pk)
        {
            try
            {
                return DAL.Fichas.Fichas_Relevamientos.getByPk(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.Fichas.Fichas_Relevamientos> read(int idFicha, string cuit)
        {
            try
            {
                return DAL.Fichas.Fichas_Relevamientos.read(idFicha, cuit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(DAL.Fichas.Fichas_Relevamientos obj, List<DAL.Fichas.Fichas_Relevamientos_Personas> lst)
        {
            try
            {
                try
                {
                    int num = 0;
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        num = obj.ID = DAL.Fichas.Fichas_Relevamientos.insert(obj);
                        foreach (DAL.Fichas.Fichas_Relevamientos_Personas relevamientosPersonas in lst)
                            relevamientosPersonas.ID_RELEVAMIENTO = obj.ID;
                        DAL.Fichas.Fichas_Relevamientos_Personas.insert(lst);
                        transactionScope.Complete();
                    }
                    return num;
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

        public static void delete(int pk)
        {
            try
            {
                DAL.Fichas.Fichas_Relevamientos.delete(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
