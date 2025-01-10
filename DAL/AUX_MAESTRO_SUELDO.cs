using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class AUX_MAESTRO_SUELDO : DALBase
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string des_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public int cod_seccion { get; set; }
        public int cod_categoria { get; set; }
        public string tarea { get; set; }
        public string des_liquidacion { get; set; }
        public DateTime fecha_liquidacion { get; set; }
        public string per_ult_dep { get; set; }
        public string cuil { get; set; }
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public DateTime fecha_pago { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal importe_total { get; set; }
        public string clasificacion_personal { get; set; }
        public int nro_orden { get; set; }
        public List<Entities.AUX_DETALLE_SUELDO> lstDetalle { get; set; }

        public AUX_MAESTRO_SUELDO()
        {
            legajo = 0;
            nombre = string.Empty;
            fecha_ingreso = DateTime.Now;
            des_tipo_documento = string.Empty;
            nro_documento = string.Empty;
            cod_seccion = 0;
            cod_categoria = 0;
            tarea = string.Empty;
            des_liquidacion = string.Empty;
            fecha_liquidacion = DateTime.Now;
            per_ult_dep = string.Empty;
            cuil = string.Empty;
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            fecha_pago = DateTime.Now;
            sueldo_basico = 0;
            importe_total = 0;
            clasificacion_personal = string.Empty;
            nro_orden = 0;
        }

        private static List<Entities.AUX_MAESTRO_SUELDO> mapeo(SqlDataReader dr)
        {
            List<Entities.AUX_MAESTRO_SUELDO> lst = new List<Entities.AUX_MAESTRO_SUELDO>();
            Entities.AUX_MAESTRO_SUELDO obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.AUX_MAESTRO_SUELDO();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.nombre = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.fecha_ingreso = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { obj.des_tipo_documento = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.nro_documento = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.cod_seccion = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { obj.cod_categoria = dr.GetInt32(6); }
                    if (!dr.IsDBNull(7)) { obj.tarea = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.des_liquidacion = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.fecha_liquidacion = dr.GetDateTime(9); }
                    if (!dr.IsDBNull(10)) { obj.per_ult_dep = dr.GetString(10); }
                    if (!dr.IsDBNull(11)) { obj.fecha_ult_dep = dr.GetDateTime(11); }
                    if (!dr.IsDBNull(12)) { obj.cuil = dr.GetString(12); }
                    if (!dr.IsDBNull(13)) { obj.anio = dr.GetInt32(13); }
                    if (!dr.IsDBNull(14)) { obj.cod_tipo_liq = dr.GetInt32(14); }
                    if (!dr.IsDBNull(15)) { obj.nro_liquidacion = dr.GetInt32(15); }
                    if (!dr.IsDBNull(16)) { obj.fecha_pago = dr.GetDateTime(16); }
                    if (!dr.IsDBNull(17)) { obj.sueldo_basico = dr.GetDecimal(17); }
                    if (!dr.IsDBNull(18)) { obj.importe_total = dr.GetDecimal(18); }
                    if (!dr.IsDBNull(19)) { obj.clasificacion_personal = dr.GetString(19); }
                    if (!dr.IsDBNull(20)) { obj.nro_orden = dr.GetInt32(20); }
                    obj.lstDetalle = AUX_DETALLE_SUELDO.read(obj.anio, obj.cod_tipo_liq, obj.nro_liquidacion, obj.legajo);
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Entities.AUX_MAESTRO_SUELDO> read(int desde, int hasta, int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                string sql = @"SELECT e.legajo, e.nombre, e.fecha_ingreso,
                t.des_tipo_documento, e.nro_documento, e.cod_seccion, l1.cod_categoria,
                l1.tarea, l.des_liquidacion, l.fecha_liquidacion, per_ult_dep=l.per_ult_dep,
                l.fecha_ult_dep,
                e.cuil, l.anio, l.cod_tipo_liq, l.nro_liquidacion, l.fecha_pago,
                l1.sueldo_basico, importe_total=l1.sueldo_neto, 
                cp.des_clasif_per as clasificacion_personal,
                l1.nro_orden 
                FROM LIQUIDACIONES l WITH (NOLOCK)
                JOIN LIQ_X_EMPLEADO l1 ON
                l.anio=l1.anio AND
                l.cod_tipo_liq=l1.cod_tipo_liq AND
                l.nro_liquidacion=l1.nro_liquidacion
                JOIN EMPLEADOS e ON
                e.legajo>=@DESDE AND
                e.legajo<=@HASTA AND
                e.legajo=l1.legajo
                JOIN TIPOS_DOCUMENTOS t ON
                e.cod_tipo_documento=t.cod_tipo_documento 
                left JOIN CATEGORIAS c ON
                l1.cod_categoria=c.cod_categoria
                left join det_liq_x_empleado d on
                d.cod_concepto_liq=10 AND
                d.anio=l.anio AND
                d.cod_tipo_liq=l.cod_tipo_liq AND
                d.nro_liquidacion=l.nro_liquidacion AND
                d.legajo=e.legajo
                left join CLASIFICACIONES_PERSONAL cp ON
                cp.cod_clasif_per=l1.cod_clasif_per
                WHERE
                l.anio=@ANIO AND
                l.cod_tipo_liq=@COD_TIPO_LIQ AND
                l.nro_liquidacion=@NRO_LIQ
                ORDER BY e.legajo";

                List<Entities.AUX_MAESTRO_SUELDO> lst = new List<Entities.AUX_MAESTRO_SUELDO>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@DESDE", desde);
                    cmd.Parameters.AddWithValue("@HASTA", hasta);
                    cmd.Parameters.AddWithValue("@ANIO", anio);
                    cmd.Parameters.AddWithValue("@COD_TIPO_LIQ", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@NRO_LIQ", nro_liq);
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

