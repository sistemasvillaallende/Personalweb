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
        public static string ObtenerDatosSecretaria(int idFicha, string valor)
        {
            List<decimal> datos = DAL.Fichas.Fichas_Resultados.read(idFicha, valor);
            return JsonConvert.SerializeObject(datos);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerDatosFiltrados(int idFicha, string secretaria = null, string direccion = null, string oficina = null, string programa = null)
        {
            List<decimal> datos = DAL.Fichas.Fichas_Resultados.readConFiltros(idFicha, secretaria, direccion, oficina,programa);
            return JsonConvert.SerializeObject(datos);
        }

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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ObtenerProgramas(int idFicha, string secretaria, string direccion)
        {
            if (idFicha == 0)
            {
                return "Error: El parámetro idFicha está vacío o es nulo.";
            }
            List<string> datos = DAL.Fichas.Fichas_Resultados.getProgramas(idFicha, secretaria, direccion);
            return JsonConvert.SerializeObject(datos);
        }


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
    
    }
}