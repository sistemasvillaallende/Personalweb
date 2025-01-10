using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Transactions;
using DAL;

namespace BLL
{
    public class CategoriasB
    {

        public static List<Entities.Categorias> GetCategorias()
        {
            return DAL.CategoriasD.GetCategorias();
        }


        public static List<Entities.Categorias> FindCategoriaByDes(string descripcion)
        {
            return DAL.CategoriasD.FindCategoriaByDes(descripcion);
        }

        public static Entities.Categorias GetByPk(int cod)
        {
            return DAL.CategoriasD.GetByPk(cod);
        }


        public static List<Entities.Conceptos_Liq> GetConceptos_liq()
        {
            return DAL.Conceptos_liqD.GetConceptos_liq();
        }

        public static void ModificaCategoria(Categorias oCate)
        {
            try
            {
                DAL.CategoriasD.ModificaCategoria(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void ModificaSueldoBasico(Categorias oCate)
        {
            try
            {
                DAL.CategoriasD.ModificaSueldoBasico(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void NuevaCategoria(Categorias oCate)
        {
            try
            {
                DAL.CategoriasD.NuevaCategoria(oCate);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

}