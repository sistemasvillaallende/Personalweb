using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.GraficoEvaluaciones
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDatos()
        {
            int idFicha = DAL.Fichas.Fichas_Relevamientos.getUltimaFicha();
            List<decimal> datos = DAL.Fichas.Fichas_Resultados.read(idFicha);
            return JsonConvert.SerializeObject(datos);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDatosSecretaria(string idFicha, string valor)
        {
            int idF = DAL.Fichas.Fichas_Relevamientos.getUltimaFicha();
            List<decimal> datos = DAL.Fichas.Fichas_Resultados.read(idF, valor);
            return JsonConvert.SerializeObject(datos);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerOpciones(string idFicha)
        {
            if (string.IsNullOrEmpty(idFicha))
            {
                return "Error: El parámetro idFicha está vacío o es nulo.";
            }

            List<string> datos = DAL.Fichas.Fichas_Resultados.getSecretaria(int.Parse(idFicha));
            return JsonConvert.SerializeObject(datos);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}