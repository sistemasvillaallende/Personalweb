using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categorias_historial
    {
        public int cod_categoria{ get; set; }
        public string des_categoria { get; set; }
        public int item { get; set; }
        public DateTime fecha_alta_registro { get; set; }
        public decimal sueldo_basico { get; set; }

        public Categorias_historial()
        {
            cod_categoria = 0;
            des_categoria = String.Empty;
            item = 0;
            fecha_alta_registro = DateTime.Now;
            sueldo_basico = 0;

        }
    }
}
