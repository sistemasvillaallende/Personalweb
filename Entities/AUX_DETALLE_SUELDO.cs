using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities
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

        public AUX_DETALLE_SUELDO()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            cod_concepto_liq = 0;
            des_concepto_liq = string.Empty;
            suma = false;
            sujeto_a_desc = false;
            sac = false;
            unidades = 0;
            importe = 0;
            nro_orden = 0;
        }   
    }
}

