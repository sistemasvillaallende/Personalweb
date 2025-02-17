using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Concepto_Liq_x_Emp_movB
    {

        public static List<Entities.ConceptoLiqxEmpMov> GetByLegajo(int legajo)
        {
            return DAL.Concepto_Liq_x_Emp_Mov.getAllByLegajo(legajo);
        }
    }
}
