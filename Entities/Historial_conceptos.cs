using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Historial_conceptos
    {
        public DateTime Fecha { get; set; }
        public string Usuario_Carga { get; set; }
        public string Tipo_movimiento { get; set; }
        public int Cod_concepto_liq { get; set; }
        public string Concepto { get; set; }
        public decimal Valor_concepto_liq { get; set; }
        public string Observacion { get; set; }


        public Historial_conceptos()
        {
            Fecha = DateTime.Now;
            Usuario_Carga = string.Empty;
            Tipo_movimiento = string.Empty;
            Cod_concepto_liq = 0; ;
            Concepto = string.Empty;
            Valor_concepto_liq = 0;
            Observacion = string.Empty;
        }

    }
}
