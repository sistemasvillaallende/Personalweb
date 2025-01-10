using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL
{
  public class Ctas_x_concepto_liqB
  {

    public static List<Entities.Ctas_x_concepto_liq> GetByPk(int cod_concepto_liq)
    {
      return DAL.Ctas_x_concepto_liqD.GetByPk(cod_concepto_liq);
    }

    //public static Entities.Ctas_x_concepto_liq GetPlan_cta_egreso(int cod_concepto_liq, int cod_clasif_per, int cod_tipo_liq)
    //{
    //  return DAL.Ctas_x_concepto_liqD.GetPlan_cta_egreso(cod_concepto_liq, cod_clasif_per, cod_tipo_liq);
    //}

    public static List<Entities.Ctas_x_concepto_liq> GetPlan_cta_egreso(int cod_concepto_liq, int cod_clasif_per, int cod_tipo_liq)
    {
      return DAL.Ctas_x_concepto_liqD.GetPlan_cta_egreso(cod_concepto_liq, cod_clasif_per, cod_tipo_liq);
    }


    public static List<Entities.Ctas_x_concepto_liq> GetPlan_cta_egreso()
    {
      return DAL.Ctas_x_concepto_liqD.GetPlan_cta_egreso();
    }

    public static void NuevaCuenta(Ctas_x_concepto_liq oCta)
    {
      try
      {
        DAL.Ctas_x_concepto_liqD.NuevaCuenta(oCta);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public static void ModificaCuenta(Ctas_x_concepto_liq oCta)
    {
      try
      {
        DAL.Ctas_x_concepto_liqD.ModificaCuenta(oCta);
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public static void EliminaCuenta(Ctas_x_concepto_liq oCta)
    {
      try
      {
        DAL.Ctas_x_concepto_liqD.EliminaCuenta(oCta);
      }
      catch (Exception e)
      {

        throw e;
      }
    }


  }
}
