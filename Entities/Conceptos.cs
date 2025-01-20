using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Conceptos
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public int codigo { get; set; }
        public decimal importe { get; set; }
        public int nro_parametro { get; set; }
        public string observacion { get; set; }


        public Conceptos()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            codigo = 0;
            importe = 0;
            nro_parametro = 0;
            observacion = string.Empty;
        }

    }
}
