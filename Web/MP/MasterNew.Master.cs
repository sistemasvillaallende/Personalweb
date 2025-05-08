using DAL;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.MP
{
    public partial class MasterNew : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Request.Url.IsLoopback ||
                Request.Url.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                this.Response.Cookies.Add(new HttpCookie("VABack.CIDI")
                {
                    ["administrador"] = "1",
                    ["apellido"] = "VELEZ SPITALE",
                    ["cod_oficina"] = "19",
                    ["cod_usuario"] = "181",
                    ["cuit"] = "23271734999",
                    ["cuit_formateado"] = "23-27.173.499.9",
                    ["legajo"] = "710",
                    ["nombre"] = "IGNACIO MARTIN",
                    ["nombre_completo"] = "VELEZ SPITALE, IGNACIO MARTIN",
                    ["nombre_oficina"] = "SISTEMAS",
                    ["nombre_usuario"] = "mvelez",
                    ["SesionHash"] = "6A6764776575367953635830595561576F747536664351783243673D",
                    ["lstPermisos"] = "SOY ADMIN NO ME CALIENTA",
                    Expires = DateTime.Now.AddDays(1000.0)
                });
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            int ficha = DAL.Fichas.Fichas_Relevamientos.getUltimaFicha();
            if (Request.Cookies["VABack.CIDI"] == null)
                Response.Redirect("http://10.0.0.24/siimva/login.aspx");

            UsuarioLoginCIDI usuario = null;
            string baseApi =
                System.Configuration.ConfigurationManager.AppSettings["BaseApi"];

            var options = new RestClientOptions(baseApi)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(string.Format(
                "/CiDiLogin/Usuario/GetUsuarioLogueado?hash={0}",
                Request.Cookies["VABack.CIDI"]["SesionHash"], Method.Get));

            RestResponse response = client.Execute(request);
            usuario =
                JsonConvert.DeserializeObject<UsuarioLoginCIDI>(response.Content);
            liApellido.InnerHtml = usuario.apellido;
            liNombre.InnerHtml = usuario.nombre;
            mnuPcApellido.InnerHtml = usuario.apellido;
            mnuPcCuit.InnerHtml = usuario.cuit;
            mnuPcNivelCidi.InnerHtml = "2";
            mnuPcNombre.InnerHtml = usuario.nombre;




            //SpanOficina.InnerHtml = usuario.nombre_oficina;
            /*lblNombreUsuario.InnerHtml =
                 Request.Cookies["UserSistema"]["nombreUsuario"];
             btnEvaluar.Visible = false;
             if (Request.Cookies["UserSistema"]["id_secretaria"] != null)
             {
                 if (Request.Cookies["UserSistema"]["id_secretaria"] != "0"
                     && Request.Cookies["UserSistema"]["id_secretaria"] != "")
                 {
                     string valor = Request.Cookies["UserSistema"]["id_secretaria"];
                     btnEvaluar.Visible = true;
                     btnEvaluar.HRef =
                         "~/Autoridades/Secretarias/DashboardSecretaria.aspx";
                 }
             }
             if (Request.Cookies["UserSistema"]["id_direccion"] != null)
             {
                 if (Request.Cookies["UserSistema"]["id_direccion"] != "0"
                     && Request.Cookies["UserSistema"]["id_direccion"] != "")
                 {
                     string dir = Request.Cookies["UserSistema"]["id_direccion"];
                     btnEvaluar.Visible = true;
                     btnEvaluar.HRef =
                         "~/Autoridades/Direcciones/Dashboard.aspx";
                 }
             }*/

        }

        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {
            if (Request.Cookies["VABack.CIDI"] != null)
            {
                // Crear una nueva cookie con el mismo nombre pero vacía
                HttpCookie cookie = new HttpCookie("VABack.CIDI")
                {
                    Expires = DateTime.Now.AddDays(-1), // Fecha de expiración en el pasado
                    Value = string.Empty // Dejar el valor vacío
                };

                // Agregarla a la respuesta para sobrescribir la existente
                Response.Cookies.Add(cookie);

                // También eliminarla del lado del servidor
                Request.Cookies.Remove("VABack.CIDI");
                Response.Redirect("http://10.0.0.24/siimva/login.aspx");
            }
        }
    }
}