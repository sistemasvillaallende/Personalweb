using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SijcorB
    {
        //public static void GuardarArchivo(int anio, int cod_tipo_liq, int nro_liquidacion, string opcion, string nombre_archivo, string carpeta)
        //{
        //  DAL.SijcorD.GuardarArchivo(anio, cod_tipo_liq, nro_liquidacion, opcion, nombre_archivo, carpeta);
        //}


        //public static string[] GenerarArchivo(int anio, int cod_tipo_liq, int nro_liquidacion, string opcion, string nombre_archivo, string carpeta)
        //{
        //  return DAL.SijcorD.GenerarArchivo(anio, cod_tipo_liq, nro_liquidacion, opcion, nombre_archivo, carpeta);
        //}


        public static List<Entities.Sijcor> GetSijcor(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion)
        {
            return DAL.SijcorD.GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
        }

        public static List<Entities.Sijcor> GetSijcorSinAguilucho(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion)
        {
            return DAL.SijcorD.GetSijcorSinAguilucho(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
        }

        public static List<Entities.Sijcor> GetSijcorConAguilucho(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion)
        {
            return DAL.SijcorD.GetSijcorConAguilucho(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
        }


    }
}
