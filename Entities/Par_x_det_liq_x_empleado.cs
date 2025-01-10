using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Par_x_det_liq_x_empleado
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public int cod_concepto_liq { get; set; }
        public int nro_parametro { get; set; }
        public string fecha_alta_registro { get; set; }
        public decimal valor_parametro { get; set; }
        public string observacion { get; set; }

        public Par_x_det_liq_x_empleado()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            cod_concepto_liq = 0;
            nro_parametro = 0;
            fecha_alta_registro = DateTime.Today.ToString("dd/MM/yyyy");
            valor_parametro = 0;
            observacion = string.Empty;
        }

    }
}
