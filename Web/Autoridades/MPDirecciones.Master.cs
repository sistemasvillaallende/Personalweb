using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Autoridades
{
    public partial class MPDirecciones : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserSistema"] != null)
            {
                if (Request.Cookies["UserSistema"]["nombreUsuario"] != null)
                {
                    if (Request.Cookies["UserSistema"]["id_direccion"] != string.Empty)
                    {
                        lblNombreUsuario.InnerHtml =
                        Request.Cookies["UserSistema"]["nombreUsuario"];
                        if(Request.Cookies["UserSistema"]["rrhh"] != null)
                        {
                            if(Request.Cookies["UserSistema"]["rrhh"] == "SI")
                            {
                                btnSalir.Visible = true;
                            }
                            else
                            {
                                btnSalir.Visible = false;
                            }
                        }
                    }
                    else
                        Response.Redirect("../../index.aspx");
                }
                else
                    Response.Redirect("../../index.aspx");
            }
            else
                Response.Redirect("../../index.aspx");
        }
        protected void btnSalir_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../index.aspx");
        }
    }
}