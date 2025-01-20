using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.reportes
{
    public class sueldo_detalle
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
        public string FECHA_PAGO { get; set; }
        public int LEGAJO { get; set; }
        public string CATEGORIA { get; set; }
        public string NOMBRE { get; set; }
        public string FECHA_INGRESO { get; set; }
        public string TIPO_DOC { get; set; }
        public string NRO_DOC { get; set; }
        public string TIPO_LIQ { get; set; }
        public string TIPO_CONTRATACION { get; set; }
        public string CARGO { get; set; }
        public string SECCION { get; set; }
        public string PERIODO_LIQUIDACION { get; set; }
        public string FECHA_ULTIMO_PERIODO { get; set; }
        public string PERIODO_ULTIMO { get; set; }
        
        public string BANCO { get; set; }
        public string CUIT { get; set; }
        public decimal SUELTO_BASICO { get; set; }
        public decimal SUELDO_NETO { get; set; }
        public string SUELDO_EN_LETRAS { get; set; }
        public string Coberturamedica { get; set; }

    }
}