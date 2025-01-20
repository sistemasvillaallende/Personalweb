  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.reportes
{
    public class AUX_MAESTRO_SUELDO
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
        public List<AUX_DETALLE_SUELDO> lstDetalle { get; set; }
    }
}