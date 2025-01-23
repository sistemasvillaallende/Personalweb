using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL
{
    public class Categoria_profesional_monotributoB
    {
        public static List<Entities.Categoria_profesional_monotributo> GetCategoriaProfesionalMono()
        {
            return DAL.Categoria_profesional_monotributoD.GetCategoriaProfesionalMono();
        }

        public static List<Entities.Categoria_profesional_monotributo> FindCategoriaByDes(string descripcion)
        {
            return DAL.Categoria_profesional_monotributoD.FindCategoriaByDes(descripcion);
        }

        public static Entities.Categoria_profesional_monotributo GetByPk(int cod)
        {
            return DAL.Categoria_profesional_monotributoD.GetByPk(cod);
        }


        // public static List<Entities.Conceptos_Liq> GetConceptos_liq()
        // {
        //     return DAL.Conceptos_liqD.GetConceptos_liq();
        // }

        public static void ModificaCategoria(Categoria_profesional_monotributo oCate)
        {
            try
            {
                DAL.Categoria_profesional_monotributoD.ModificaCategoria(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void ModificaMonto(Categoria_profesional_monotributo oCate)
        {
            try
            {
                DAL.Categoria_profesional_monotributoD.ModificaMonto(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void NuevaCategoria(Categoria_profesional_monotributo oCate)
        {
            try
            {
                DAL.Categoria_profesional_monotributoD.NuevaCategoria(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void EliminaCategoria(int id)
        {
           
            try
            {
                DAL.Categoria_profesional_monotributoD.EliminarCategoria(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


    }
}
