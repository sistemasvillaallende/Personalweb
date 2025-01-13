using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.MP
{
    public partial class Dashnoard : System.Web.UI.MasterPage
    {
        UsuarioLoginCIDI obj = null;
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
            try
            {
                if (!IsPostBack)
                {
                    if (Request.Cookies["VABack.CIDI"] != null)
                    {
                        DAL.UsuarioLoginCIDI obj = CIDI.Utils.ObtenerUsuarioLogueado(
                            Request.Cookies["VABack.CIDI"]["SesionHash"]);
                        if (obj == null)
                            Response.Redirect("../BackEnd/LogIn.aspx");


                        liApellido.InnerHtml =
                            Request.Cookies["VABack.CIDI"]["apellido"];
                        liNombre.InnerHtml =
                            Request.Cookies["VABack.CIDI"]["nombre"];

                        mnuPcApellido.InnerHtml =
                            Request.Cookies["VABack.CIDI"]["apellido"];

                        mnuPcCuit.InnerHtml = Request.Cookies["VABack.CIDI"]["cuit_formateado"];

                        mnuPcNivelCidi.InnerHtml = "2";

                        mnuPcNombre.InnerHtml =
                            Request.Cookies["VABack.CIDI"]["nombre"];

                        SpanOficina.InnerHtml =
                            Request.Cookies["VABack.CIDI"]["nombre_oficina"];


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {
            CIDI.Entrada entrada = new CIDI.Entrada();
            entrada.IdAplicacion = CIDI.Config.CiDiIdAplicacion;
            entrada.Contrasenia = CIDI.Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["VABack.CIDI"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = CIDI.Config.ObtenerToken_SHA1(entrada.TimeStamp);

            CIDI.Respuesta respuesta = CIDI.Config.LlamarWebAPI<CIDI.Entrada,
                CIDI.Respuesta>(CIDI.APICuenta.Usuario.Cerrar_Sesion_Usuario_Aplicacion, entrada);

            if (respuesta.Resultado == CIDI.Config.CiDi_OK)
            {
                HttpCookie _Cookie = new HttpCookie("VABack.CIDI");
                _Cookie.Expires = DateTime.Now.AddMonths(-1);
                Response.Cookies.Add(_Cookie);
            }
            Response.Redirect("/BackEnd/Login.aspx");
        }
    }
}