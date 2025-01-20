using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.reportes
{
    public class sueldo_maestro
    {
        public string NRO_ORDEN { get; set; }
        public string FECHA_PAGO { get; set; }
        public string LEGAJO { get; set; }
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
        public string SUELTO_BASICO { get; set; }
        public string SUELDO_NETO { get; set; }
        public string SUELDO_EN_LETRAS { get; set; }
        public List<sueldo_detalle> lstDetalle { get; set; }

        public sueldo_maestro()
        {
            NRO_ORDEN = "";
            FECHA_PAGO = "";
            LEGAJO = "";
            CATEGORIA = "";
            NOMBRE = "";
            FECHA_INGRESO = "";
            TIPO_DOC = "";
            NRO_DOC = "";
            TIPO_LIQ = "";
            TIPO_CONTRATACION = "";
            CARGO = "";
            SECCION = "";
            PERIODO_LIQUIDACION = "";
            FECHA_ULTIMO_PERIODO = "";
            PERIODO_ULTIMO = "";
            BANCO = "";
            CUIT = "";
            SUELTO_BASICO = "";
            SUELDO_NETO = "";
            SUELDO_EN_LETRAS = "";
            lstDetalle = new List<sueldo_detalle>();
        }

    }
}