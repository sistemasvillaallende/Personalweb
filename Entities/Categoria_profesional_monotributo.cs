using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categoria_profesional_monotributo
    {
        public int id_profesional_monotributo { get; set; }
        public string categoria { get; set; }
        public DateTime fecha_alta { get; set; }
        public decimal monto { get; set; }

        public Categoria_profesional_monotributo()
        {
            id_profesional_monotributo = 0;
            categoria = String.Empty;
            fecha_alta = DateTime.Now;
            monto = 0;

        }
    }
}
