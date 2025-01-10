using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities
{
    public class AUX_MAESTRO_SUELDO 
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string des_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public int cod_seccion { get; set; }
        public int cod_categoria { get; set; }
        public string tarea { get; set; }
        public string des_liquidacion { get; set; }
        public DateTime fecha_liquidacion { get; set; }
        public string per_ult_dep { get; set; }
        public DateTime fecha_ult_dep { get; set; }
        public string cuil { get; set; }
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }
        public DateTime fecha_pago { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal importe_total { get; set; }
        public string clasificacion_personal { get; set; }
        public int nro_orden { get; set; }        
        public List<AUX_DETALLE_SUELDO> lstDetalle { get; set; }

        public AUX_MAESTRO_SUELDO()
        {
            legajo = 0;
            nombre = string.Empty;
            fecha_ingreso = DateTime.Now;
            des_tipo_documento = string.Empty;
            nro_documento = string.Empty;
            cod_seccion = 0;
            cod_categoria = 0;
            tarea = string.Empty;
            des_liquidacion = string.Empty;
            fecha_liquidacion = DateTime.Now;
            per_ult_dep = string.Empty;
            fecha_ult_dep = DateTime.Now;
            cuil = string.Empty;
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            fecha_pago = DateTime.Now;
            sueldo_basico = 0;
            importe_total = 0;
            clasificacion_personal = string.Empty;
            nro_orden = 0;
        }      
    }
}

