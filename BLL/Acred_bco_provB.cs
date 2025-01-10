using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Acred_bco_provB
    {
        public static List<Entities.Acred_bco_prov> GetAcred_bco_prov(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            return DAL.Acred_bco_provD.GetAcred_bco_prov(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
        }
        //
    }
}
