using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sueldos
    {
        public int nro_liquidacion { get; set; }
        public decimal sueldo_bruto { get; set; }
        public decimal dias_trabajados { get; set; }

        public Sueldos()
        {
            nro_liquidacion = 0;
            sueldo_bruto = 0;
            dias_trabajados = 0;
        }

    }
}

