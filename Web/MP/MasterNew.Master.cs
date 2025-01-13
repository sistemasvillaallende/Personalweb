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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserSistema"] != null)
            {
                lblNombreUsuario.InnerHtml =
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
                }
            }
        }
    }
}