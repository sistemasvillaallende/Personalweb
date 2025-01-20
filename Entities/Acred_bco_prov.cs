using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Acred_bco_prov
    {
        public string tipo_reg { get; set; }
        public string nro_suc { get; set; }
        public string nro_reparto { get; set; }
        public string legajo { get; set; }
        public string tipo_doc { get; set; }
        public string nro_doc { get; set; }
        public string tipo_cuenta { get; set; }
        public string nro_cuenta { get; set; }
        public string monto_sueldo { get; set; }
        public string importe_pesos { get; set; }
        public string importe_cecor { get; set; }
        public string fecha_acreditacion { get; set; }
        public string nro_empresa { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string cod_postal { get; set; }
        public string telefono { get; set; }
        public string fecha_nacimiento { get; set; }
        public string salto_de_fila { get; set; }

        public Acred_bco_prov()
        {
            tipo_reg = string.Empty;
            nro_suc = string.Empty;
            nro_reparto = string.Empty;
            legajo = string.Empty;
            tipo_doc = string.Empty;
            nro_doc = string.Empty;
            tipo_cuenta = string.Empty;
            nro_cuenta = string.Empty;
            monto_sueldo = "";
            importe_pesos = "";
            importe_cecor = "";
            fecha_acreditacion = string.Empty;
            nro_empresa = string.Empty;
            nombre = string.Empty;
            direccion = string.Empty;
            localidad = string.Empty;
            cod_postal = string.Empty;
            telefono = string.Empty;
            fecha_nacimiento = string.Empty;
            salto_de_fila = string.Empty;
        }
    }
}
