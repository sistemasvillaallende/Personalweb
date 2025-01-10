using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Categorias
  {
    public int cod_categoria { get; set; }
    public string fecha_alta_registro { get; set; }
    public string des_categoria { get; set; }
    public decimal sueldo_basico { get; set; }

    public Categorias()
    {
      cod_categoria = 0;
      fecha_alta_registro = "";
      des_categoria = "";
      sueldo_basico = 0;
    }

  }
}
