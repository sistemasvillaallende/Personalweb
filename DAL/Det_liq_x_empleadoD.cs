using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
  public class Det_liq_x_empleadoD
  {

    public static List<Entities.Det_Liq_x_empleado> GetDet_Liq_X_Empleados(short anio, short cod_tipo_liq, short nro_liquidacion,
      SqlConnection cn, SqlTransaction trx)
    {
      List<Entities.Det_Liq_x_empleado> lstDetalle = new List<Entities.Det_Liq_x_empleado>();
      Entities.Det_Liq_x_empleado eDetalle;
      StringBuilder strSQL = new StringBuilder();
      SqlCommand cmd;
      strSQL.AppendLine("SELECT * FROM DET_LIQ_X_EMPLEADO");
      strSQL.AppendLine("WHERE anio=@anio and cod_tipo_liq=@cod_tipo_liq and nro_liquidacion=@nro_liquidacion");
      try
      {

        cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@anio", anio);
        cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
        cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
        cmd.Connection = cn;
        cmd.Transaction = trx;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();



        cmd.Connection.Open();

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
          int anio1 = dr.GetOrdinal("anio");
          int cod_tipo_liq1 = dr.GetOrdinal("cod_tipo_liq");
          int nro_liquidacion1 = dr.GetOrdinal("nro_liquidacion");
          int legajo = dr.GetOrdinal("legajo");
          int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
          int nro_orden = dr.GetOrdinal("nro_orden");
          int fecha_alta_registro = dr.GetOrdinal("fecha_alta_registro");
          int importe = dr.GetOrdinal("importe");
          int unidades = dr.GetOrdinal("unidades");

          while (dr.Read())
          {
            eDetalle = new Entities.Det_Liq_x_empleado();
            eDetalle.anio = anio;
            eDetalle.cod_tipo_liq = cod_tipo_liq;
            eDetalle.nro_liquidacion = nro_liquidacion;
            if (!dr.IsDBNull(legajo)) eDetalle.legajo = dr.GetInt16(legajo);
            if (!dr.IsDBNull(cod_tipo_liq1)) eDetalle.cod_concepto_liq = dr.GetInt16(cod_concepto_liq);
            if (!dr.IsDBNull(nro_liquidacion1)) eDetalle.nro_orden = dr.GetInt16(nro_orden);
            lstDetalle.Add(eDetalle);
          }
        }
      }

      catch (Exception ex)
      {
        cmd = null;
        throw ex;
      }
      return lstDetalle;

    }

  }
}
