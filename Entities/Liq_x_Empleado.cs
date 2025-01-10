using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Liq_x_Empleado
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public int legajo { get; set; }
        public int nro_orden { get; set; }
        public string fecha_alta_registro { get; set; }
        public int cod_categoria { get; set; }
        public int cod_cargo { get; set; }
        public string tarea { get; set; }
        public string nro_cta_sb { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal sueldo_neto { get; set; }
        public decimal sueldo_bruto { get; set; }
        public int cod_clasif_per { get; set; }
        public decimal no_remunerativo { get; set; }


        public Liq_x_Empleado()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            legajo = 0;
            fecha_alta_registro = DateTime.Today.ToString("dd/MM/yyyy");
            cod_categoria = 0;
            cod_cargo = 0;
            tarea = string.Empty;
            nro_cta_sb = string.Empty;
            sueldo_basico = 0;
            sueldo_neto = 0;
            sueldo_bruto = 0;
            no_remunerativo = 0;
            cod_clasif_per = 0;
            nro_orden = 0;
        }

    }
}
