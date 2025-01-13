// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.ResultadosEvaluacion
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System.Collections.Generic;

namespace DAL.Fichas
{
  public class ResultadosEvaluacion
  {
    public List<string> lstSeries { get; set; }

    public List<string> lstValores { get; set; }

    public ResultadosEvaluacion()
    {
      this.lstSeries = new List<string>();
      this.lstValores = new List<string>();
    }
  }
}
