using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MesaWeb.reportes
{
  public class Expedientes
  {

    public int anio { get; set; }
    public int nro_expediente { get; set; }
    public string fecha_ingreso { get; set; }
    public string nombre { get; set; }
    public string asunto { get; set; }
    public string origen { get; set; }
    public string destino { get; set; }
    public string estado { get; set; }
    public string observaciones { get; set; }


    public Expedientes()
    {
      Clear();
    }

    public void Clear()
    {
      anio = 0;
      nro_expediente = 0;
      fecha_ingreso = "";
      nombre = "";
      asunto = "";
      origen = "";
      destino = "";
      estado = "";
      observaciones = "";
    }

    public static SqlConnection GetConnection(string strDB)
    {
      string connectionString;
      SqlConnection objCon;
      connectionString = ConfigurationManager.ConnectionStrings[strDB].ConnectionString;
      objCon = new SqlConnection(connectionString);
      return objCon;
    }

    public static List<Expedientes> GetExpedienteByDestino(string fecha_desde, string fecha_hasta,
       int cod_oficina_destino_desde, int cod_oficina_destino_hasta)
    {
      Expedientes oExp;
      List<Expedientes> lstExp = new List<Expedientes>();
      SqlCommand cmd;
      SqlDataReader reader;
      SqlConnection cn = null;
      StringBuilder strSQL = new StringBuilder();


      cmd = new SqlCommand();
      try
      {
        cn = GetConnection("local");

        strSQL.AppendLine("SELECT e.Anio as anio, e.Nro_expediente as nro_expediente, ");
        strSQL.AppendLine("e.Fecha_ingreso as fecha_ingreso, ");
        strSQL.AppendLine("e.nombre,tt.Descripcion_tramite as tramite, a.Descripcion_asunto AS asunto,  ");
        strSQL.AppendLine("o1.nombre_oficina AS origen, ");
        strSQL.AppendLine("o2.nombre_oficina AS destino,");
        strSQL.AppendLine("ee.descripcion_estado AS estado, ee.Prefijo, ");
        strSQL.AppendLine("e.Observaciones as observaciones,  ");
        strSQL.AppendLine("e.Fecha_fin_tramite as fecha_fin_tramite, e.Usuario as usuario ");
        strSQL.AppendLine("FROM expediente e ");
        strSQL.AppendLine("left JOIN ASUNTOS a ON ");
        strSQL.AppendLine("e.Cod_asunto=a.Cod_asunto ");

        strSQL.AppendLine("left JOIN TIPOS_TRAMITE tt ON ");
        strSQL.AppendLine("tt.Cod_tipo_tramite=e.Cod_tipo_tramite ");

        strSQL.AppendLine("JOIN OFICINAS o1 ON ");
        strSQL.AppendLine("o1.codigo_oficina=e.Cod_oficina_origen ");
        strSQL.AppendLine("JOIN OFICINAS o2 ON ");
        strSQL.AppendLine("o2.codigo_oficina=e.Cod_oficina_destino ");
        strSQL.AppendLine("left JOIN ESTADOS_EXPEDIENTES ee ON ");
        strSQL.AppendLine("ee.Cod_estado_expediente=e.Cod_estado_expediente ");
        strSQL.AppendLine("where e.fecha_ingreso between @fecha_desde");
        strSQL.AppendLine("and @fecha_hasta");
        strSQL.AppendLine("and e.cod_oficina_origen between @cod_oficina_destino_desde");
        strSQL.AppendLine("and @cod_oficina_destino_hasta");
        strSQL.AppendLine("order by e.anio, e.nro_expediente");

        cmd.Parameters.AddWithValue("@fecha_desde", fecha_desde);
        cmd.Parameters.AddWithValue("@fecha_hasta", fecha_hasta);
        cmd.Parameters.AddWithValue("@cod_oficina_destino_desde", cod_oficina_destino_desde);
        cmd.Parameters.AddWithValue("@cod_oficina_destino_hasta", cod_oficina_destino_hasta);

        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();
        cmd.Connection.Open();

        reader = cmd.ExecuteReader();


        int anio = reader.GetOrdinal("anio");
        int nro_exp = reader.GetOrdinal("nro_expediente");
        int fecha_ingreso = reader.GetOrdinal("fecha_ingreso");
        int nombre = reader.GetOrdinal("nombre");
        int asunto = reader.GetOrdinal("asunto");
        int origen = reader.GetOrdinal("origen");
        int destino = reader.GetOrdinal("destino");
        int estado = reader.GetOrdinal("estado");
        int obs = reader.GetOrdinal("observaciones");


        while (reader.Read())
        {

          oExp = new Expedientes();

          if (!reader.IsDBNull(anio)) oExp.anio = reader.GetInt32(anio);
          if (!reader.IsDBNull(nro_exp)) oExp.nro_expediente = reader.GetInt32(nro_exp);
          if (!reader.IsDBNull(fecha_ingreso)) oExp.fecha_ingreso = reader.GetDateTime(fecha_ingreso).ToShortDateString();
          if (!reader.IsDBNull(nombre)) oExp.nombre = reader.GetString(nombre);
          if (!reader.IsDBNull(asunto)) oExp.asunto = reader.GetString(asunto);
          if (!reader.IsDBNull(origen)) oExp.origen = reader.GetString(origen);
          if (!reader.IsDBNull(destino)) oExp.destino = reader.GetString(destino);
          if (!reader.IsDBNull(estado)) oExp.estado = reader.GetString(estado);
          if (!reader.IsDBNull(obs)) oExp.observaciones = reader.GetString(obs);
          lstExp.Add(oExp);

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
      return lstExp;
    }

    public static List<Expedientes> GetExpedienteByEstado(string fecha_desde, string fecha_hasta,
        int cod_estado_expediente_desde, int cod_estado_expediente_hasta)
    {
      Expedientes oExp;
      List<Expedientes> lstExp = new List<Expedientes>();
      SqlCommand cmd;
      SqlDataReader reader;
      SqlConnection cn = null;
      StringBuilder strSQL = new StringBuilder();


      cmd = new SqlCommand();
      try
      {
        cn = GetConnection("local");

        strSQL.AppendLine("SELECT e.Anio as anio, e.Nro_expediente as nro_expediente, ");
        strSQL.AppendLine("e.Fecha_ingreso AS fecha_ingreso, ");
        strSQL.AppendLine("e.nombre,tt.Descripcion_tramite as tramite, a.Descripcion_asunto AS asunto,  ");
        strSQL.AppendLine("o1.nombre_oficina AS origen, ");
        strSQL.AppendLine("o2.nombre_oficina AS destino,");

        strSQL.AppendLine("ee.descripcion_estado AS estado, ee.Prefijo, ");
        strSQL.AppendLine("e.Observaciones as observaciones,  ");
        strSQL.AppendLine("convert(varchar(10),e.Fecha_fin_tramite ,103) AS fecha_fin_tramite, e.Usuario as usuario ");
        strSQL.AppendLine("FROM expediente e ");
        strSQL.AppendLine("left JOIN ASUNTOS a ON ");
        strSQL.AppendLine("e.Cod_asunto=a.Cod_asunto ");

        strSQL.AppendLine("left JOIN TIPOS_TRAMITE tt ON ");
        strSQL.AppendLine("tt.Cod_tipo_tramite=e.Cod_tipo_tramite ");

        strSQL.AppendLine("JOIN OFICINAS o1 ON ");
        strSQL.AppendLine("o1.codigo_oficina=e.Cod_oficina_origen ");
        strSQL.AppendLine("JOIN OFICINAS o2 ON ");
        strSQL.AppendLine("o2.codigo_oficina=e.Cod_oficina_destino ");
        strSQL.AppendLine("left JOIN ESTADOS_EXPEDIENTES ee ON ");
        strSQL.AppendLine("ee.Cod_estado_expediente=e.Cod_estado_expediente ");
        strSQL.AppendLine("where e.fecha_ingreso between @fecha_desde");
        strSQL.AppendLine("and @fecha_hasta");
        strSQL.AppendLine("and e.cod_estado_expediente between @cod_estado_expediente_desde");
        strSQL.AppendLine("and @cod_estado_expediente_hasta");
        strSQL.AppendLine("order by e.anio, e.nro_expediente");

        cmd.Parameters.AddWithValue("@fecha_desde", fecha_desde);
        cmd.Parameters.AddWithValue("@fecha_hasta", fecha_hasta);
        cmd.Parameters.AddWithValue("@cod_estado_expediente_desde", cod_estado_expediente_desde);
        cmd.Parameters.AddWithValue("@cod_estado_expediente_hasta", cod_estado_expediente_hasta);

        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();
        cmd.Connection.Open();

        reader = cmd.ExecuteReader();


        int anio = reader.GetOrdinal("anio");
        int nro_exp = reader.GetOrdinal("nro_expediente");
        int fecha_ingreso = reader.GetOrdinal("fecha_ingreso");
        int nombre = reader.GetOrdinal("nombre");
        int asunto = reader.GetOrdinal("asunto");
        int origen = reader.GetOrdinal("origen");
        int destino = reader.GetOrdinal("destino");
        int estado = reader.GetOrdinal("estado");
        int obs = reader.GetOrdinal("observaciones");


        while (reader.Read())
        {

          oExp = new Expedientes();
          //oExp.anio = (Convert.IsDBNull(reader.GetInt32(anio))) ? 0 : Convert.ToInt32(reader.GetInt32(anio));
          //oExp.nro_expediente = (Convert.IsDBNull(reader.GetInt32(nro_exp))) ? 0 : Convert.ToInt32(reader.GetInt32(nro_exp));
          //oExp.fecha_ingreso = (Convert.IsDBNull(reader.GetString(fecha_ingreso))) ? "" : Convert.ToString(reader.GetString(fecha_ingreso));
          //oExp.nombre = (Convert.IsDBNull(reader.GetString(nombre))) ? "" : Convert.ToString(reader.GetString(nombre));
          //oExp.asunto = (Convert.IsDBNull(reader.GetString(asunto))) ? "" : Convert.ToString(reader.GetString(asunto));
          //oExp.origen = (Convert.IsDBNull(reader.GetString(origen))) ? "" : Convert.ToString(reader.GetString(origen));
          //oExp.destino = (Convert.IsDBNull(reader.GetString(destino))) ? "" : Convert.ToString(reader.GetString(destino));
          //oExp.estado = (Convert.IsDBNull(reader.GetString(estado))) ? "" : Convert.ToString(reader.GetString(estado));
          //oExp.observaciones = (Convert.IsDBNull(reader.GetString(obs))) ? "" : Convert.ToString(reader.GetString(obs));



          if (!reader.IsDBNull(anio)) oExp.anio = reader.GetInt32(anio);
          if (!reader.IsDBNull(nro_exp)) oExp.nro_expediente = reader.GetInt32(nro_exp);
          if (!reader.IsDBNull(fecha_ingreso)) oExp.fecha_ingreso = reader.GetDateTime(fecha_ingreso).ToShortDateString();
          if (!reader.IsDBNull(nombre)) oExp.nombre = reader.GetString(nombre);
          if (!reader.IsDBNull(asunto)) oExp.asunto = reader.GetString(asunto);
          if (!reader.IsDBNull(origen)) oExp.origen = reader.GetString(origen);
          if (!reader.IsDBNull(destino)) oExp.destino = reader.GetString(destino);
          if (!reader.IsDBNull(estado)) oExp.estado = reader.GetString(estado);
          if (!reader.IsDBNull(obs)) oExp.observaciones = reader.GetString(obs);
          lstExp.Add(oExp);
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
      return lstExp;
    }
    
  }
}