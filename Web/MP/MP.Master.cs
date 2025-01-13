using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.MP
{
    public partial class MP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("../index.aspx");
        }
        //OK
        protected void btnCerrarSesion_Click1(object sender, EventArgs e)
        {
            //CIDI.Entrada entrada = new CIDI.Entrada();
            //entrada.IdAplicacion = CIDI.Config.CiDiIdAplicacion;
            //entrada.Contrasenia = CIDI.Config.CiDiPassAplicacion;
            //entrada.HashCookie = Request.Cookies["Va.CiDi"].Value.ToString();
            //entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //entrada.TokenValue = CIDI.Config.ObtenerToken_SHA1(entrada.TimeStamp);

            //CIDI.Respuesta respuesta = CIDI.Config.LlamarWebAPI<CIDI.Entrada,
            //    CIDI.Respuesta>(CIDI.APICuenta.Usuario.Cerrar_Sesion_Usuario_Aplicacion, entrada);

            //if (respuesta.Resultado == CIDI.Config.CiDi_OK)
            //{
            //    Session.Abandon();
            //    Response.Redirect("../index.aspx");
            //}

        }
        //OK

        protected void btnCancelarDisconformidad_Click(object sender, EventArgs e)
        {

        }



        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {
            Response.Cookies["UserVecino"].Expires = DateTime.Now.AddDays(-1d);
            Response.Redirect("../index.aspx");
        }
    }
}