using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Acred_bco_macroB
    {
        public static List<Entities.Acred_bco_macro> GetAcred_bco_macro(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            return DAL.Acred_bco_macroD.GetAcred_bco_macro(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
        }

        public static List<Entities.Pago_sueldo_macro> GetAcred_bco_macro_nvo_formato(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            return DAL.Acred_bco_macroD.GetAcred_bco_macro_nvo_formato(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
        }

    }


}
