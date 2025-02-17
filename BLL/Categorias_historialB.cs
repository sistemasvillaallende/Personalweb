using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Categorias_historialB
    {
        public static List<Entities.Categorias_historial> GetCategoriaHistorial()
        {
            return DAL.Categorias_historialD.GetCategoriaHistorial();
        }


        public static void AgregarAlHistorial(int cod_categoria, string des_categoria, decimal sueldo_basico)
        {
            try
            {
                DAL.Categorias_historialD.AgregarAlHistorial(cod_categoria, des_categoria, sueldo_basico);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static Entities.Categorias_historial GetByPk(int cod)
        {
            return DAL.Categorias_historialD.GetByPk(cod);
        }
    }
}
