using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tipo_auditoria
    {
        public int id { get; set; }
        public string des_tipo_auditoria { get; set; }
        public string fecha_alta_registro { get; set; }
        public int activo { get; set; }

        public Tipo_auditoria()
        {
            id = 0;
            des_tipo_auditoria = "";
            fecha_alta_registro = "";
            activo = 1;

        }
    }
}
