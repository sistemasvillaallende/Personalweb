using System;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Services;

namespace web.GraficoEvaluaciones
{
    public partial class General_x_Competencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<DAL.Fichas.Ficha> lstEval = DAL.Fichas.Ficha.read();
                DDLEvaluaciones.DataTextField = "NOMBRE";
                DDLEvaluaciones.DataValueField = "ID";
                DDLEvaluaciones.DataSource = lstEval;
                DDLEvaluaciones.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDatos()
        {
            int idFicha = DAL.Fichas.Fichas_Relevamientos.getUltimaFicha();
            List<DAL.General_x_competencia> datos = DAL.General_x_competencia.read(idFicha);
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod]
        public static string ObtenerDatosSecretaria(string idFicha, string nombreSecretaria)
        {
            List<DAL.General_x_competencia> datos = DAL.General_x_competencia.read(7);
            var json = JsonConvert.SerializeObject(datos);
            return json;
        }


  /// <summary>
  /// ///////////////////////////
  /// </summary>
  /// <returns></returns>

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerSecretarias(int idFicha)
        {
            if (idFicha == 0)
            {
                return "Error: El parámetro idFicha está vacío o es nulo.";
            }
            List<string> datos = DAL.Fichas.Fichas_Resultados.getSecretarias(idFicha);
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDirecciones(int idFicha, string secretaria)
        {
            if (idFicha == 0)
            {
                return "Error: El parámetro idFicha está vacío o es nulo.";
            }
            List<string> datos = DAL.Fichas.Fichas_Resultados.getDirecciones(idFicha, secretaria);
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerOficinas(int idFicha, string secretaria, string direccion)
        {
            if (idFicha == 0)
            {
                return "Error: El parámetro idFicha está vacío o es nulo.";
            }
            List<string> datos = DAL.Fichas.Fichas_Resultados.getOficinas(idFicha, secretaria, direccion);
            return JsonConvert.SerializeObject(datos);
        }



        /////////////////////
        ///


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDatosFiltrados(int idFicha, string secretaria = null, string direccion = null, string oficina = null)
        {
          
            List<DAL.General_x_competencia> datos = DAL.General_x_competencia.readConFiltros(idFicha, secretaria, direccion, oficina);
            var json = JsonConvert.SerializeObject(datos);
            return json;
        }









    }
}