using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public
        class Auditoria
    {
        public int id_auditoria { get; set; }
        public string fecha_movimiento { get; set; }
        public string usuario { get; set; }
        public string menu { get; set; }
        public string proceso { get; set; }
        public string identificacion { get; set; }
        public string autorizaciones { get; set; }
        public string observaciones { get; set; }
        public string detalle { get; set; }
        public Auditoria()
        {
            id_auditoria = 0;
            fecha_movimiento = DateTime.Today.ToString();
            usuario = string.Empty;
            menu = string.Empty;
            proceso = string.Empty;
            identificacion = string.Empty;
            autorizaciones = string.Empty;
            observaciones = string.Empty;
            detalle = string.Empty;           
        }
    }
}
