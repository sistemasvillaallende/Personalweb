using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pago_sueldo_macro
    {
        public string legajo { get; set; }
        public string cuil { get; set; }
        public string apeynom { get; set; }
        public string cuenta { get; set; }
        public string cbu { get; set; }
        public string importe { get; set; }
        //Recordar que el impore debe ser
        //parte entera un punto y parte decimal
        //11 Digitos para entera y 2 decimales
        public string comprobante { get; set; }

        public Pago_sueldo_macro()
        {
            legajo = "0".ToString().PadLeft(7, '0');
            cuil = "0".PadLeft(11, '0');
            apeynom = " ".ToString().PadRight(19, ' ');
            cuenta = " ".ToString().PadRight(15, ' ');
            cbu = " ".ToString().PadRight(22, ' ');
            importe = " ".ToString().PadRight(7, ' ');
            comprobante = " ".ToString().PadRight(7, ' ');
        }
    }

}

