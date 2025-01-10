using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Liquidacion
    {
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public string des_tipo_liq { get; set; }
        public string des_liquidacion { get; set; }
        public string periodo { get; set; }
        public string fecha_alta { get; set; }
        public string fecha_liquidacion { get; set; }
        public bool aguinaldo { get; set; }
        public string fecha_pago { get; set; }
        public string per_ult_dep { get; set; }
        public string fecha_ult_dep { get; set; }
        public int cod_semestre = 0;
        public string semestre { get; set; }
        public string usuario { get; set; }
        public string operacion { get; set; }
        public string fecha_modificacion { get; set; }
        public int cod_banco_ult_dep { get; set; }
        public bool publica { get; set; }
        public bool cerrada { get; set; }
        public string fecha_cierre_liq { get; set; }
        public string usuario_cierre { get; set; }
        public short prueba { get; set; }


        public Liquidacion()
        {
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            des_tipo_liq = "";
            periodo = "";
            des_liquidacion = "";
            fecha_alta = "";
            fecha_liquidacion = DateTime.Today.ToString("dd/MM/yyyy");
            aguinaldo = false;
            fecha_pago = "";
            per_ult_dep = "";
            fecha_ult_dep = "";
            cod_semestre = 0;
            semestre = "";
            usuario = "";
            operacion = "";
            fecha_modificacion = DateTime.Today.ToString("dd/MM/yyyy");
            cod_banco_ult_dep = 0;
            publica = false;
            cerrada = false;
            fecha_cierre_liq = "";
            usuario_cierre = "";
            prueba = 0;
        }
    }
}
