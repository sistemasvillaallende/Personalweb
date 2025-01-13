using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class MPSec : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idSec"] != null)
                {
                    hIdSec.Value = Request.QueryString["idSec"].ToString();
                    mnuDashboard.HRef = string.Format(
                        "DashboardSecretaria.aspx?idSec={0}",
                         hIdSec.Value);

                    aEvaluar.HRef = string.Format("DesempenioSec.aspx?idSec={0}",
                         hIdSec.Value);

                    aDirecciones.HRef = string.Format(
                        "DesempenioDirecciones.aspx?id_secretaria={0}",
             hIdSec.Value);

     //               mnuAusentismo.HRef = string.Format("AusentismoSec.aspx?idSec={0}",
     //                    hIdSec.Value);
     //               mnuLicencias.HRef = string.Format("LicenciasSec.aspx?idSec={0}",
     //                    hIdSec.Value);
     //               mnuNotificaciones.HRef = string.Format("NotificacionesSec.aspx?idSec={0}",
     //hIdSec.Value);
                }

            }



            //if (Request.Cookies["UserSistema"] != null)
            //{
            //    if (Request.Cookies["UserSistema"]["nombreUsuario"] != null)
            //    {
            //        if (Request.Cookies["UserSistema"]["id_secretaria"] != string.Empty)
            //        {
            //            lblNombreUsuario.InnerHtml =
            //            Request.Cookies["UserSistema"]["nombreUsuario"];
            //            if (Request.Cookies["UserSistema"]["rrhh"] != null)
            //            {
            //                if (Request.Cookies["UserSistema"]["rrhh"] == "SI")
            //                {
            //                    btnSalir.Visible = true;
            //                }
            //                else
            //                {
            //                    btnSalir.Visible = false;
            //                }
            //            }
            //        }
            //        else
            //            Response.Redirect("../../index.aspx");
            //    }
            //    else
            //        Response.Redirect("../../index.aspx");
            //}
            //else
            //    Response.Redirect("../../index.aspx");
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