using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Valores_x_concepto_liq
  {
    public int cod_concepto_liq { get; set; }
    public int nro_valor { get; set; }
    public string fecha_alta_registro { get; set; }
    public decimal valor { get; set; }
    public string nro_valor_des { get; set; }

    public Valores_x_concepto_liq()
    {
      cod_concepto_liq = 0;
      nro_valor = 0;      
      fecha_alta_registro = string.Empty;
      valor = 0;
      nro_valor_des = string.Empty;
    }
  }
}
