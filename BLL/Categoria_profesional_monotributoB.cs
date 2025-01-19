using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Categoria_profesional_monotributoB
    {
        public static List<Entities.Categoria_profesional_monotributo> GetCategoriaProfesionalMono()
        {
            return DAL.Categoria_profesional_monotributoD.GetCategoriaProfesionalMono();
        }
    }
}
