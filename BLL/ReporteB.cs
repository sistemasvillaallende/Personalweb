using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
  public class ReporteB
  {

    DataSet dsDatos;
    ReporteD objReporte = null;

    //struct ListerProps
    //{
    //  public long TotalRows;
    //  //public decimal  TotalDeuda;
    //}
    //ListerProps o;

    public ReporteB()
    {
      objReporte = new ReporteD();
      dsDatos = new DataSet();
      //o = new ListerProps();
    }

    //public long TotalRows
    //{
    //  get { return o.TotalRows; }
    //}

    public DataSet ListDatosConstancia(string anio, string nro_expediente)
    {
      dsDatos = objReporte.ListDatosConstancia(anio, nro_expediente);
      //o.TotalRows = objReporte.TotalRows;
      return dsDatos;
    }

    public DataSet ListDatosExpedientes(string anio, string nro_expediente)
    {
      dsDatos = objReporte.ListDatosExpedientes(anio, nro_expediente);
      //o.TotalRows = objReporte.TotalRows;
      return dsDatos;
    }

  }
}
