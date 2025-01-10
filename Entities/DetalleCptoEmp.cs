using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DetalleCptoEmp
    {
        public int legajo { get; set; }
        public int cod_concepto_liq { get; set; }
        public string concepto { get; set; }
        public string nombre { get; set; }
        public int nro_parametro { get; set; }
        public decimal valor_parametro { get; set; }
        public string observacion { get; set; }

        public DetalleCptoEmp()
        {
        }



    }
}
