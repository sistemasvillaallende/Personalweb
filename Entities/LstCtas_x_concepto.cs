using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LstCtas_x_concepto
    {

        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public string fecha_alta_registro { get; set; }
        public int cod_tipo_liq { get; set; }
        public string des_tipo_liq { get; set; }
        public int cod_clasif_per { get; set; }
        public string des_clasif_per { get; set; }
        public string nom_cta { get; set; }
        public string nro_cta { get; set; }

        public LstCtas_x_concepto()
        {
            cod_concepto_liq = 0;
            des_concepto_liq = string.Empty;
            fecha_alta_registro = string.Empty;
            cod_tipo_liq = 0;
            des_tipo_liq = string.Empty;
            cod_clasif_per = 0;
            des_concepto_liq = string.Empty;
            nom_cta = string.Empty;
            nro_cta = string.Empty;

        }
    }
}
