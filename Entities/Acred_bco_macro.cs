using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Acred_bco_macro
    {
        public string codigo_empresa { get; set; }
        public string tipo_dni { get; set; }
        public string nro_documento { get; set; }
        public string nombre_beneficiario { get; set; }
        public string apellido_beneficiario { get; set; }
        public string tipo_cuenta { get; set; }
        public string nro_cbu { get; set; }
        public string sueldo_neto { get; set; }
        public string salto_de_fila { get; set; }

        public Acred_bco_macro()
        {
            codigo_empresa = "20002";
            tipo_dni = string.Empty;
            nro_documento = string.Empty;
            nombre_beneficiario = string.Empty;
            apellido_beneficiario = string.Empty;
            tipo_cuenta = string.Empty;
            nro_cbu = string.Empty;
            sueldo_neto = string.Empty;
            salto_de_fila = string.Empty;
        }
    }
}
