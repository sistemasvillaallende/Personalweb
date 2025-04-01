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
        protected void OnInit()
        {
            try
            {
                
                DAL.Fichas.Fichas_Relevamientos.getUltimaFicha();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}