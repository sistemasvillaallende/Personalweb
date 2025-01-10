using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConceptoLiqxEmp
    {
        public int legajo { get; set; }
        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public decimal valor_concepto_liq { get; set; }
        public string fecha_alta_registro { get; set; }
        public string fecha_vto { get; set; }
        public string usuario { get; set; }
        //public string cod_usuario { get; set; }
        public int op { get; set; }
        public string observacion { get; set; }

        public ConceptoLiqxEmp()
        {
            legajo = 0;
            cod_concepto_liq = 0;
            des_concepto_liq = "";
            fecha_alta_registro = "";
            fecha_vto = "";
            usuario = "";
            //cod_usuario = "";
            op = 0;
            observacion = string.Empty;

        }

    }

}
