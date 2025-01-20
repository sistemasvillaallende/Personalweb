using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Transactions;
using DAL;

namespace BLL
{
    public class Concepto_liqB
    {

        public static List<Entities.Conceptos_Liq> findConcepto_liqByDescripcion(string descripcion)
        {
            return DAL.Conceptos_liqD.findConcepto_liqByDescripcion(descripcion);
        }

        public static List<Entities.DetalleCptoEmp> FindDetalleCptoEmp(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq)
        {
            return DAL.Conceptos_liqD.FindDetalleCptoEmp(anio, cod_tipo_liq, nro_liq, cod_concepto_liq);
        }

        public static List<Entities.DetalleCptoEmp> FindDetalleCptoEmpByPk(int anio, int cod_tipo_liq, int nro_liq, int cod_concepto_liq, int legajo)
        {
            return DAL.Conceptos_liqD.FindDetalleCptoEmpByPk(anio, cod_tipo_liq, nro_liq, cod_concepto_liq, legajo);
        }


        public static Entities.Conceptos_Liq GetByPk(int cod)
        {
            return DAL.Conceptos_liqD.GetByPk(cod);
        }

        public static List<Entities.Conceptos_Liq> GetConceptos_liq()
        {
            return DAL.Conceptos_liqD.GetConceptos_liq();
        }

        public static void ModificaConcepto(Conceptos_Liq oCon)
        {
            try
            {
                DAL.Conceptos_liqD.ModificaConcepto(oCon);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public static void NuevoConcepto(Conceptos_Liq oCon)
        {
            try
            {
                DAL.Conceptos_liqD.NuevoConcepto(oCon);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
