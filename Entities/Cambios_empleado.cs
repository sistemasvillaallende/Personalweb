using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cambios_empleado
    {
        public DateTime fecha_cambio { get; set; }
        public string descripcion_cambio { get; set; }
       

        public Cambios_empleado()
        {
            fecha_cambio = DateTime.Now;
            descripcion_cambio = string.Empty;
        }

    }
}
