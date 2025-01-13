// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.Ficha
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;


namespace BLL.Fichas
{
    public class Ficha
    {
        public static DAL.Fichas.Ficha getByPk(int pk)
        {
            try
            {
                return DAL.Fichas.Ficha.getByPk(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.Fichas.Ficha> read()
        {
            try
            {
                return DAL.Fichas.Ficha.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.Fichas.Ficha> readActivas()
        {
            try
            {
                return DAL.Fichas.Ficha.readActivas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insert(DAL.Fichas.Ficha obj)
        {
            try
            {
                DAL.Fichas.Ficha.insert(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(DAL.Fichas.Ficha obj)
        {
            try
            {
                DAL.Fichas.Ficha.update(obj);
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
                DAL.Fichas.Ficha.updateActiva(id, estado);
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
                DAL.Fichas.Ficha.delete(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
