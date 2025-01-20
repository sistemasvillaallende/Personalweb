using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Web.reportes
{
  public class clsAlertas
  {

    public int anio { get; set; }
    public int nro_expediente { get; set; }
    public string fecha { get; set; }
    public string tipo { get; set; }
    public string asunto { get; set; }
    public int nro_paso { get; set; }
    public string oficina { get; set; }
    public string estado { get; set; }
    public string observaciones { get; set; }
    public int dias { get; set; }



    public clsAlertas()
    {
      Clear();
    }

    public void Clear()
    {
      anio = 0;
      nro_expediente = 0;
      fecha = "";
      oficina = "";
      nro_paso = 0;
      estado = "";
      observaciones = "";
      dias = 0;

    }

    public static SqlConnection GetConnection(string strDB)
    {
      string connectionString;
      SqlConnection objCon;
      connectionString = ConfigurationManager.ConnectionStrings[strDB].ConnectionString;
      objCon = new SqlConnection(connectionString);
      return objCon;
    }

    public static List<clsAlertas> GetAlertasByOficinaDestino(string fecha_desde, string fecha_hasta,
       int cod_oficina_destino_desde, int cod_oficina_destino_hasta)
    {
      clsAlertas oAlertas;
      List<clsAlertas> lstAlertas = new List<clsAlertas>();
      SqlCommand cmd;
      SqlDataReader reader;
      SqlConnection cn = null;
      StringBuilder strSQL = new StringBuilder();
      int anio_movimiento = Convert.ToInt16(ConfigurationManager.AppSettings["anio_movimiento"]);



      cmd = new SqlCommand();
      try
      {
        cn = GetConnection("local");

        strSQL.AppendLine("select a.anio, a.nro_expediente, a.nro_paso,");
        strSQL.AppendLine("ta.Descripcion_tramite as tipo,");
        strSQL.AppendLine("au.Descripcion_asunto as asunto,");
        strSQL.AppendLine("a.fecha_pase as fecha, o2.nombre_oficina as oficina,");
        strSQL.AppendLine(" b.Descripcion_estado as estado, a.observaciones, a.dias_sin_resolver as dias");
        strSQL.AppendLine("from MOVIMIENTOS_EXPEDIENTE a");
        strSQL.AppendLine("join ESTADOS_EXPEDIENTES b on");
        strSQL.AppendLine("a.cod_estado_expediente = b.Cod_estado_expediente");
        strSQL.AppendLine("join OFICINAS o2 on");
        strSQL.AppendLine("a.codigo_oficina_destino = o2.codigo_oficina");
        strSQL.AppendLine("join EXPEDIENTE e on");
        strSQL.AppendLine("a.nro_expediente = e.Nro_expediente and");
        strSQL.AppendLine("a.anio = e.anio");
        strSQL.AppendLine("join TIPOS_TRAMITE ta on");
        strSQL.AppendLine("ta.Cod_tipo_tramite = e.Cod_tipo_tramite");
        strSQL.AppendLine("join ASUNTOS au on");
        strSQL.AppendLine("au.Cod_tipo_tramite = ta.Cod_tipo_tramite and");
        strSQL.AppendLine("au.Cod_asunto = e.Cod_asunto");
        strSQL.AppendLine("where");
        strSQL.AppendLine("a.anio >= @anio and");
        strSQL.AppendLine("a.atendido = 0 and");
        strSQL.AppendLine("a.fecha_pase between @fecha_desde and @fecha_hasta and");
        strSQL.AppendLine("a.codigo_oficina_destino between @cod_desde and @cod_hasta");


        cmd.Parameters.AddWithValue("@anio", anio_movimiento);
        cmd.Parameters.AddWithValue("@fecha_desde", fecha_desde);
        cmd.Parameters.AddWithValue("@fecha_hasta", fecha_hasta);
        cmd.Parameters.AddWithValue("@cod_desde", cod_oficina_destino_desde);
        cmd.Parameters.AddWithValue("@cod_hasta", cod_oficina_destino_hasta);



        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();
        cmd.Connection.Open();

        reader = cmd.ExecuteReader();


        int anio = reader.GetOrdinal("anio");
        int nro_exp = reader.GetOrdinal("nro_expediente");
        int nro_paso = reader.GetOrdinal("nro_paso");
        int fecha = reader.GetOrdinal("fecha");
        int tipo = reader.GetOrdinal("tipo");
        int asunto = reader.GetOrdinal("asunto");
        int oficina = reader.GetOrdinal("oficina");
        int estado = reader.GetOrdinal("estado");
        int observaciones = reader.GetOrdinal("observaciones");
        int dias = reader.GetOrdinal("dias");


        while (reader.Read())
        {

          oAlertas = new clsAlertas();

          if (!reader.IsDBNull(anio)) oAlertas.anio = reader.GetInt32(anio);
          if (!reader.IsDBNull(nro_exp)) oAlertas.nro_expediente = reader.GetInt32(nro_exp);
          if (!reader.IsDBNull(nro_paso)) oAlertas.nro_paso = reader.GetInt32(nro_paso);
          if (!reader.IsDBNull(tipo)) oAlertas.tipo = reader.GetString(tipo);
          if (!reader.IsDBNull(asunto)) oAlertas.asunto = reader.GetString(asunto);
          if (!reader.IsDBNull(fecha)) oAlertas.fecha = reader.GetDateTime(fecha).ToShortDateString();
          if (!reader.IsDBNull(nro_paso)) oAlertas.nro_paso = reader.GetInt32(nro_paso);
          if (!reader.IsDBNull(oficina)) oAlertas.oficina = reader.GetString(oficina);
          if (!reader.IsDBNull(estado)) oAlertas.estado = reader.GetString(estado);
          if (!reader.IsDBNull(observaciones)) oAlertas.observaciones = reader.GetString(observaciones);
          if (!reader.IsDBNull(dias)) { oAlertas.dias = (reader.GetInt32(dias)); }

          lstAlertas.Add(oAlertas);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Error in query!" + e.ToString());
        throw e;
      }
      finally
      {
        cn.Close();
        cn = null;
        strSQL = null;
      }
      return lstAlertas;
    }





    public static List<clsAlertas> GetAlertasByOficinaOrigen(string fecha_desde, string fecha_hasta,
      int cod_oficina_origen_desde, int cod_oficina_origen_hasta)
    {
      clsAlertas oAlertas;
      List<clsAlertas> lstAlertas = new List<clsAlertas>();
      SqlCommand cmd;
      SqlDataReader reader;
      SqlConnection cn = null;
      StringBuilder strSQL = new StringBuilder();
      int anio_movimiento = Convert.ToInt16(ConfigurationManager.AppSettings["anio_movimiento"]);



      cmd = new SqlCommand();
      try
      {
        cn = GetConnection("local");

        strSQL.AppendLine("select a.anio, a.nro_expediente, a.nro_paso,");
        strSQL.AppendLine("ta.Descripcion_tramite as tipo,");
        strSQL.AppendLine("au.Descripcion_asunto as asunto,");
        strSQL.AppendLine("a.fecha_pase as fecha, o2.nombre_oficina as oficina,");
        strSQL.AppendLine(" b.Descripcion_estado as estado, a.observaciones, a.dias_sin_resolver as dias");
        strSQL.AppendLine("from MOVIMIENTOS_EXPEDIENTE a");
        strSQL.AppendLine("join ESTADOS_EXPEDIENTES b on");
        strSQL.AppendLine("a.cod_estado_expediente = b.Cod_estado_expediente");
        strSQL.AppendLine("join OFICINAS o2 on");
        strSQL.AppendLine("a.codigo_oficina_destino = o2.codigo_oficina");
        strSQL.AppendLine("join EXPEDIENTE e on");
        strSQL.AppendLine("a.nro_expediente = e.Nro_expediente and");
        strSQL.AppendLine("a.anio = e.anio");
        strSQL.AppendLine("join TIPOS_TRAMITE ta on");
        strSQL.AppendLine("ta.Cod_tipo_tramite = e.Cod_tipo_tramite");
        strSQL.AppendLine("join ASUNTOS au on");
        strSQL.AppendLine("au.Cod_tipo_tramite = ta.Cod_tipo_tramite and");
        strSQL.AppendLine("au.Cod_asunto = e.Cod_asunto");
        strSQL.AppendLine("join MOVIMIENTOS_EXPEDIENTE a1 on");
        strSQL.AppendLine("a1.nro_expediente = a.nro_expediente and");
        strSQL.AppendLine("a1.anio = a.anio and");
        strSQL.AppendLine("a1.nro_paso=1");
        strSQL.AppendLine("where");
        strSQL.AppendLine("a.anio >= @anio and");
        //strSQL.AppendLine("a.atendido = 0 and");
        strSQL.AppendLine("a1.fecha_pase between @fecha_desde and @fecha_hasta and");
        strSQL.AppendLine("a1.codigo_oficina_origen between @cod_desde and @cod_hasta");
        strSQL.AppendLine("order by a.anio, a.nro_expediente, a.nro_paso");




        cmd.Parameters.AddWithValue("@anio", anio_movimiento);
        cmd.Parameters.AddWithValue("@fecha_desde", fecha_desde);
        cmd.Parameters.AddWithValue("@fecha_hasta", fecha_hasta);
        cmd.Parameters.AddWithValue("@cod_desde", cod_oficina_origen_desde);
        cmd.Parameters.AddWithValue("@cod_hasta", cod_oficina_origen_hasta);



        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();
        cmd.Connection.Open();

        reader = cmd.ExecuteReader();


        int anio = reader.GetOrdinal("anio");
        int nro_exp = reader.GetOrdinal("nro_expediente");
        int nro_paso = reader.GetOrdinal("nro_paso");
        int fecha = reader.GetOrdinal("fecha");
        int tipo = reader.GetOrdinal("tipo");
        int asunto = reader.GetOrdinal("asunto");
        int oficina = reader.GetOrdinal("oficina");
        int estado = reader.GetOrdinal("estado");
        int observaciones = reader.GetOrdinal("observaciones");
        int dias = reader.GetOrdinal("dias");


        while (reader.Read())
        {

          oAlertas = new clsAlertas();

          if (!reader.IsDBNull(anio)) oAlertas.anio = reader.GetInt32(anio);
          if (!reader.IsDBNull(nro_exp)) oAlertas.nro_expediente = reader.GetInt32(nro_exp);
          if (!reader.IsDBNull(nro_paso)) oAlertas.nro_paso = reader.GetInt32(nro_paso);
          if (!reader.IsDBNull(tipo)) oAlertas.tipo = reader.GetString(tipo);
          if (!reader.IsDBNull(asunto)) oAlertas.asunto = reader.GetString(asunto);
          if (!reader.IsDBNull(fecha)) oAlertas.fecha = reader.GetDateTime(fecha).ToShortDateString();
          if (!reader.IsDBNull(oficina)) oAlertas.oficina = reader.GetString(oficina);
          if (!reader.IsDBNull(estado)) oAlertas.estado = reader.GetString(estado);
          if (!reader.IsDBNull(observaciones)) oAlertas.observaciones = reader.GetString(observaciones);
          if (!reader.IsDBNull(dias)) { oAlertas.dias = (reader.GetInt32(dias)); }

          lstAlertas.Add(oAlertas);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Error in query!" + e.ToString());
        throw e;
      }
      finally
      {
        cn.Close();
        cn = null;
        strSQL = null;
      }
      return lstAlertas;
    }



    public static string XMLAlertasByOficina(List<clsAlertas> alertas)
    {

      //      XElement srcTree = new XElement("Root",
      //    new XElement("Element", 1),
      //    new XElement("Element", 2),
      //    new XElement("Element", 3),
      //    new XElement("Element", 4),
      //    new XElement("Element", 5)
      //);

      // XElement xml = new XElement("alertas",
      //alertas.Select(i => new XElement("alerta",
      //   new XAttribute("anio", i.anio), i.)));




      //  XElement contacts =
      //new XElement("Contacts",
      //    new XElement("Contact",





      //        new XElement("Name", "Patrick Hines"),
      //        new XElement("Phone", "206-555-0144"),
      //        new XElement("Address",
      //            new XElement("Street1", "123 Main St"),
      //            new XElement("City", "Mercer Island"),
      //            new XElement("State", "WA"),
      //            new XElement("Postal", "68042")
      //        )
      //    )
      //);

      XElement valor = null;


      //for (int i = 0; i < length; i++)
      ////{
      //valor = new XElement("alertas", new XElement("alerta", new XElement("anio", item.anio),
      //    new XElement("nro_expediente", item.nro_expediente),
      //    new XElement("fecha_pase", item.fecha_pase.ToString()), new XElement("nro_paso", item.nro_paso),
      //    new XElement("destino", item.destino), new XElement("estado", item.estado),
      //    new XElement("observaciones", item.observaciones), new XElement("dias", item.dias)));
      //}
      string xml = "<alertas>";
      foreach (var item in alertas)
      {

        valor = new XElement("alerta", new XElement("anio", item.anio),
          new XElement("nro_expediente", item.nro_expediente),

          new XElement("tipo", item.tipo),

          new XElement("asunto", item.asunto),

          new XElement("fecha_pase", item.fecha.ToString()), new XElement("nro_paso", item.nro_paso),
          new XElement("oficina", item.oficina), new XElement("estado", item.estado),
          new XElement("observaciones", item.observaciones), new XElement("dias", item.dias));

        xml += valor.ToString();
      }


      if (!string.IsNullOrEmpty(xml))

        xml += "</alertas>";


      return xml.ToString();

    }
  }
}





