using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Valores_x_concepto_liqB
    {

        public static List<Entities.Valores_x_concepto_liq> GetByPk(int cod_concepto_liq, int nro_valor)
        {
            return DAL.Valores_x_concepto_liqD.GetByPk(cod_concepto_liq, nro_valor);
        }

        public static List<Entities.Valores_x_concepto_liq> GetValores(int cod_concepto_liq)
        {
            return DAL.Valores_x_concepto_liqD.GetValores(cod_concepto_liq);
        }

        public static void NuevoValor(Entities.Valores_x_concepto_liq oVal)
        {
            try
            {
                DAL.Valores_x_concepto_liqD.NuevoValor(oVal);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void ModificaValor(Entities.Valores_x_concepto_liq oVal)
        {
            try
            {
                DAL.Valores_x_concepto_liqD.ModificaValor(oVal);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void EliminaValor(Entities.Valores_x_concepto_liq oVal)
        {
            try
            {
                DAL.Valores_x_concepto_liqD.EliminaValor(oVal);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
