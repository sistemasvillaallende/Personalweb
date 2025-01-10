using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Acred_bco_cbaB
    {
        public static List<Entities.Acred_bco_cba> GetAcred_bco_cba(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            return DAL.Acred_bco_cbaD.GetAcred_bco_cba(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
        }
      
    }


}
