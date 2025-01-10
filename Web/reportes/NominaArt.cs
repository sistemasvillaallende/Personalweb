using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class NominaArt
    {
        public string nro_documento { get; set; }
        public string des_tipo_documento { get; set; }
        public string nombre { get; set; }
        public string fecha_nacimiento { get; set; }
        public string des_estado_civil { get; set; }
        public string sexo { get; set; }
        public string telefonos { get; set; }
        public string domicilio { get; set; }
        public string cod_postal { get; set; }
        public string ciudad_domicilio { get; set; }
        public string fecha_ingreso { get; set; }
        public string tarea { get; set; }
        public string des_clasif_per { get; set; }

        public NominaArt()
        {
            nro_documento = "";
            des_tipo_documento = "";
            nombre = "";
            fecha_nacimiento = "";
            des_estado_civil = "";
            sexo = "";
            telefonos = "";
            domicilio = "";
            cod_postal = "";
            ciudad_domicilio = "";
            fecha_ingreso = "";
            tarea = "";
            des_clasif_per = "";
        }

        private static List<NominaArt> mapeo(SqlDataReader dr)
        {
            List<NominaArt> lst = new List<NominaArt>();
            NominaArt obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new NominaArt();
                    if (!dr.IsDBNull(0)) { obj.nro_documento = dr.GetString(0); }
                    if (!dr.IsDBNull(1)) { obj.des_tipo_documento = dr.GetString(1).Trim(); }
                    if (!dr.IsDBNull(2)) { obj.nombre = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.fecha_nacimiento = dr.GetDateTime(3).ToShortDateString();}
                    if (!dr.IsDBNull(4)) { obj.des_estado_civil = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.sexo = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.telefonos = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.domicilio = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.cod_postal = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.ciudad_domicilio = dr.GetString(9); }
                    if (!dr.IsDBNull(10)) { obj.fecha_ingreso = dr.GetDateTime(10).ToShortDateString(); }
                    if (!dr.IsDBNull(11)) { obj.tarea = dr.GetString(11); }
                    if (!dr.IsDBNull(12)) { obj.des_clasif_per = dr.GetString(12); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<NominaArt> read()
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("a.nro_documento,");
                strSQL.AppendLine("b.des_tipo_documento,");
                strSQL.AppendLine("a.nombre,");
                strSQL.AppendLine("a.fecha_nacimiento,");
                strSQL.AppendLine("ec.des_estado_civil,");
                strSQL.AppendLine("a.sexo,");
                strSQL.AppendLine("a.telefonos,");
                strSQL.AppendLine("domicilio = rtrim(ltrim(a.calle_domicilio)) + ' ' + CONVERT(CHAR(5), a.nro_domicilio) + ' ' + ltrim(rtrim(a.ciudad_domicilio)),");
                strSQL.AppendLine("a.cod_postal,");
                strSQL.AppendLine("a.ciudad_domicilio,");
                strSQL.AppendLine("a.fecha_ingreso,");
                strSQL.AppendLine("a.tarea,");
                strSQL.AppendLine("t.des_clasif_per");
                strSQL.AppendLine("FROM EMPLEADOS a, TIPOS_DOCUMENTOS b, CLASIFICACIONES_PERSONAL t, ESTADOS_CIVILES ec");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.fecha_baja IS NULL AND");
                strSQL.AppendLine("a.cod_tipo_documento = b.cod_tipo_documento AND");
                strSQL.AppendLine("a.cod_clasif_per = t.cod_clasif_per  AND");
                strSQL.AppendLine("a.cod_estado_civil = ec.cod_estado_civil");
                strSQL.AppendLine("ORDER BY a.legajo");
                List<NominaArt> lst = new List<NominaArt>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Parameters.AddWithValue("@anio", anio);
                    //cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    //cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}