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

        public static List<Entities.Categoria_profesional_historialDTO> GetHistorialDetalle()
        {
            return DAL.Categoria_profesional_monotributo_histD.GetHistorialDetalle();
        }


        public static void AgregarAlHistorial(int id_profesional_monotributo, decimal monto)
        {
            try
            {
            DAL.Categoria_profesional_monotributo_histD.AgregarAlHistorial(id_profesional_monotributo, monto);
            }
            catch (System.Exception)
            { 
                throw;
            }
        }


    }
}
