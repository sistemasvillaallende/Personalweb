using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.reportes
{
    public class AUX_DETALLE_SUELDO
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public bool suma { get; set; }
        public bool sujeto_a_desc { get; set; }
        public bool sac { get; set; }
        public decimal unidades { get; set; }
        public decimal importe { get; set; }
        public int nro_orden { get; set; }
    }
}