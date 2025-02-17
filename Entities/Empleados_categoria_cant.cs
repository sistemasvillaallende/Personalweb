using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Empleados_categoria_cant
    {
        public List<Empleado> Empleados { get; set; }
        public int cantidad_empleados { get; set; }
      
        public Empleados_categoria_cant()
        {
            Empleados = new List<Empleado>();
            cantidad_empleados = 0;
        }
    }
}
