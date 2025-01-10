using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Det_Liq_x_empleado
  {
    public int anio { get; set; }
    public int cod_tipo_liq { get; set; }
    public int nro_liquidacion { get; set; }
    public int legajo { get; set; }
    public int cod_concepto_liq { get; set; }
    public int nro_orden { get; set; }
    public string fecha_alta_registro { get; set; }
    public decimal importe { get; set; }
    public decimal unidades { get; set;}


    public Det_Liq_x_empleado()
    {
      anio = 0;
      cod_tipo_liq = 0;
      nro_liquidacion = 0;
      legajo = 0;
      cod_concepto_liq = 0;
      nro_orden = 0;
      fecha_alta_registro = DateTime.Today.ToString("dd/MM/yyyy");
      importe = 0;
      unidades = 0;
    }


  }
}
