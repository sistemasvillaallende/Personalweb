using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConceptoLiqxEmpMov
    {
        public int id { get; set; }
        public int legajo { get; set; }
        public DateTime fecha_mov { get; set; }
        public int id_tipo_movimiento { get; set; }
        public int cod_concepto_liq { get; set; }
        public decimal valor_concepto_liq { get; set; }
        public DateTime fecha_vto { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
       

        public ConceptoLiqxEmpMov()
        {
            legajo = 0;
            fecha_mov = DateTime.Now;
            id_tipo_movimiento = 0;
            cod_concepto_liq = 0;
            descripcion = string.Empty; ;
            observacion = string.Empty;
            usuario = string.Empty; ;
            valor_concepto_liq = 0;
            fecha_vto = DateTime.Now;
            id = 0;
        }

    }
}

