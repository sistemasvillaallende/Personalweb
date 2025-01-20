using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Certificaciones
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string anio { get; set; }
        public string periodo { get; set; }
        public string des_liquidacion { get; set; }
        public string cargo { get; set; }
        public string tarea { get; set; }
        public decimal importe { get; set; }


        public Certificaciones()
        {
            legajo = 0;
            nombre = "";
            anio = "";
            periodo = "";
            des_liquidacion = string.Empty;
            cargo = "";
            tarea = "";
            importe = 0;

        }
    }
}
