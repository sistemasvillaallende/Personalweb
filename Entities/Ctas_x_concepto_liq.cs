using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Ctas_x_concepto_liq
  {
    public int cod_concepto_liq { get; set; }
    public string des_concepto_liq { get; set; }
    public int cod_tipo_liq { get; set; }
    public string des_tipo_liq { get; set; }
    public int cod_clasif_per { get; set; }
    public string des_clasif_per { get; set; }
    public string nro_cta { get; set; }
    public string nom_cta { get; set; }
    public string fecha_alta_registro { get; set; }
    public string nro_con_des { get; set; }

    public Ctas_x_concepto_liq()
    {
      cod_concepto_liq = 0;
      cod_tipo_liq = 0;
      cod_clasif_per = 0;
      des_concepto_liq = string.Empty;
      des_clasif_per = string.Empty;
      nro_cta = string.Empty;
      nom_cta = string.Empty;
      fecha_alta_registro = string.Empty;
      nro_con_des = string.Empty;
    }
  }
}
