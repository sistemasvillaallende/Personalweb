using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Conceptos_Liq
  {

    public int cod_concepto_liq { get; set; }
    public string des_concepto_liq { get; set; }
    public int cod_tipo_concepto { get; set; }
    public string des_tipo_concepto { get; set; }
    public string Fecha_alta_registro { get; set; }
    public bool suma { get; set; }
    public bool sujeto_a_desc { get; set; }
    public bool sac { get; set; }
    public string formula { get; set; }
    public bool aporte { get; set; }
    public bool remunerativo { get; set; }
    public string formula_aporte { get; set; }

    public Conceptos_Liq()
    {
      cod_concepto_liq = 0;
      des_concepto_liq = "";
      cod_tipo_concepto = 0;
      des_tipo_concepto = "";
      Fecha_alta_registro = "";
      suma = false;
      sujeto_a_desc = false;
      sac = false;
      formula = "";
      aporte = false;
      remunerativo = false;
      formula_aporte = "";

    }

  }


}

