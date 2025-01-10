using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
  public partial class ReporteD
  {

    public DataSet dsDatos = new DataSet();

    //struct ListerProps
    //{
    //  public long TotalRows;

    //}
    //ListerProps o;
    //public ReporteD()
    //{
    //  //
    //  // TODO: agregar aquí la lógica del constructor
    //  //
    //  o = new ListerProps();
    //}

    //public long TotalRows
    //{
    //  get { return o.TotalRows; }
    //}

    #region Listas --> Retornan DataSet


    public DataSet ListDatosConstancia(string anio, string nro_expediente)
    {

      string strSQL = "";
      string strCondicion = "";

      strSQL += "Select  anio, nro_expediente, Fecha_ingreso  ";
      strSQL += "FROM expediente ";

      if (!string.IsNullOrEmpty(anio) && (!string.IsNullOrEmpty(nro_expediente)))
        strCondicion = " Where anio=" + anio + " AND nro_expediente=" + nro_expediente;


      strSQL += strCondicion;
      DataSet ds = new DataSet();
      ds = DALBase.Pagination("Expediente", strSQL, 0, 0, "", "");
      //o.TotalRows = DALBase.TotalRows;
      return ds;

    }

    #endregion

    #region Expedientes


    public DataSet ListDatosExpedientes(string anio, string nro_expediente)
    {
      string strSQL = "";
      string strCondicion = "";


      strSQL = "SELECT e.nro_expediente, e.anio, ";
      strSQL += "convert(varchar(10),e.Fecha_ingreso,103) AS fecha_ingreso, ";
      strSQL += "e.nombre, a.Descripcion_asunto AS asunto,  ";
      strSQL += "o1.nombre_oficina AS origen, ";
      strSQL += "o2.nombre_oficina AS destino, ee.Descripcion_estado AS Estado, ";
      strSQL += "ee.Prefijo, e.Observaciones,  ";
      strSQL += "convert(varchar(10),e.Fecha_fin_tramite ,103) AS fecha_fin_tramite, ";
      strSQL += "e.Usuario, e.cod_estado_expediente,  ";
      strSQL += "e.email, e.telefono, e.domicilio  ";
      strSQL += "FROM expediente e ";
      strSQL += "left JOIN ASUNTOS a ON ";
      strSQL += "e.Cod_asunto=a.Cod_asunto ";
      strSQL += "JOIN OFICINAS o1 ON ";
      strSQL += "o1.codigo_oficina=e.Cod_oficina_origen ";
      strSQL += "JOIN OFICINAS o2 ON ";
      strSQL += "o2.codigo_oficina=e.Cod_oficina_destino ";
      strSQL += "left JOIN ESTADOS_EXPEDIENTES ee ON ";
      strSQL += "ee.Cod_estado_expediente=e.Cod_estado_expediente ";

      if (!string.IsNullOrEmpty(anio) && (!string.IsNullOrEmpty(nro_expediente)))
        strCondicion = " Where e.anio=" + anio + " AND e.nro_expediente=" + nro_expediente;


      strSQL += strCondicion;
      DataSet ds = new DataSet();
      ds = DALBase.Pagination("Expediente", strSQL, 0, 0, "", "");
      //o.TotalRows = oLibDB.TotalRows;
      return ds;
    }



    #endregion

  }
}
