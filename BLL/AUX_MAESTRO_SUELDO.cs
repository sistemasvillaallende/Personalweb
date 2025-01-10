using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AUX_MAESTRO_SUELDO
    {
        public static List<Entities.AUX_MAESTRO_SUELDO> read(int desde, int hasta, int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                return DAL.AUX_MAESTRO_SUELDO.read(desde, hasta, anio, cod_tipo_liq, nro_liq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
