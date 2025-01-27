using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categoria_profesional_historialDTO
    {
        public string  categoria { get; set; }
        public int id_movimiento { get; set; }
        public DateTime fecha_movimiento { get; set; }
        public decimal monto { get; set; }
        public Categoria_profesional_historialDTO()
        {
             categoria = string.Empty;
            id_movimiento = 0;
            fecha_movimiento = DateTime.Now;
            monto = 0;
        }
    }
}
