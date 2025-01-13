// Decompiled with JetBrains decompiler
// Type: BLL.Fichas.fontawesome
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3E2F25D-43DD-4FA5-8132-18DD8F8E997E
// Assembly location: D:\Muni\Dev\rrhh\bin\BLL.dll

using System;
using System.Collections.Generic;


namespace BLL.Fichas
{
  public class fontawesome
  {
    public static List<DAL.Fichas.fontawesome> read()
    {
      try
      {
        return DAL.Fichas.fontawesome.read();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
