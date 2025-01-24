using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categoria_profesional_monotributo_hist
    {
        public int id_profesional_monotributo { get; set; }
        public int id_movimiento { get; set; }
        public DateTime fecha_movimiento { get; set; }
        public decimal monto { get; set; }
        public Categoria_profesional_monotributo_hist()
        {
            id_profesional_monotributo = 0;
            id_movimiento = 0;
            fecha_movimiento = DateTime.Now;
            monto = 0;
        }
    }
}
