using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Usuarios
    {
        public int codUsuario { get; set; }

        public string nombre { get; set; }

        public string nombre_completo { get; set; }

        public int id_secretaria { get; set; }

        public int id_direccion { get; set; }

        public string secretaria { get; set; }

        public string direccion { get; set; }
    }
}

