using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.reportes
{
    public class sueldo_detalle2
    {
        public int id_liquidacion { get; set; }

        public int cod_concepto_liq { get; set; }
        public string desc_concepto_liq { get; set; }
        public decimal unidades { get; set; }
        public decimal hab_con_descuento { get; set; }
        public decimal hab_sin_descuento { get; set; }
        public decimal descuentos { get; set; }

        public decimal tot_hab_con_descuento { get; set; }
        public decimal tot_hab_sin_descuento { get; set; }
        public decimal tot_descuento { get; set; }

        public string NRO_ORDEN { get; set; }
    }
}