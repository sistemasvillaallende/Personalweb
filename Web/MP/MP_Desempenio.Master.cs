using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Utils;

namespace web.MP
{
    public partial class MP_Desempenio : System.Web.UI.MasterPage
    {
        int chances = 0;
        bool control = false;
        private CIDI.Usuario usuario;
        protected void ObtenerUsuario(string hash)
        {
            CIDI.Entrada entrada = new CIDI.Entrada();
            entrada.IdAplicacion = CIDI.Config.CiDiIdAplicacion;
            entrada.Contrasenia = CIDI.Config.CiDiPassAplicacion;
            entrada.HashCookie = hash;
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = CIDI.Config.ObtenerToken_SHA1(entrada.TimeStamp);

            usuario = CIDI.Config.LlamarWebAPI<CIDI.Entrada,
                CIDI.Usuario>(CIDI.APICuenta.Usuario.Obtener_Usuario_Basicos_Domicilio, entrada);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["VABack.CIDI"] == null)
                {
                   // DAL.UsuarioLoginCIDI obj = CIDI.Utils.ObtenerUsuarioLogueado(
                   //     Request.Cookies["VABack.CIDI"]["SesionHash"]);
                   // if (obj == null)
                        Response.Redirect("../login.aspx");
                }

            }
            
        }

        protected void btnDirecciones_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format(
                    "DesempenioDirecciones.aspx?id_secretaria={0}",
                    Request.Cookies["UserSistema"]["id_secretaria"]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void aEvaluar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format(
                    "DesempenioSec.aspx?idSec={0}",
                    Request.QueryString["idSec"].ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}