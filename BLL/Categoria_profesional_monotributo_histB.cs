using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Categoria_profesional_monotributo_histB
    {
         public static List<Entities.Categoria_profesional_monotributo_hist> GetCategoriaProfesionalMonoHist()
        {
            return DAL.Categoria_profesional_monotributo_histD.GetCategoriaProfesionalHistorial();
        }

    }
}
