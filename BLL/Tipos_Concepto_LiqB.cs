using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class Tipos_Concepto_LiqB
  {

    public static DataSet ListTipo_Concep(int id)
    {
      return DAL.Tipos_Conceptos_LiqD.ListTipo_Concep(id);
    }



  }




}
