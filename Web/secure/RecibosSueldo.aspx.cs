using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

namespace web.secure
{
    public partial class RecibosSueldo : System.Web.UI.Page
    {
        #region Declaracion de Variables y Objetos
        bool ok = false;
        SeguridadB objSeguridad;
        #endregion
        //ACTUALIZADO
        override protected void OnInit(EventArgs e)
        {
            try
            {
                for (int i = DateTime.Now.Year - 6; i <= DateTime.Now.Year; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.TagName = "li";
                    HtmlAnchor a = new HtmlAnchor();
                    a.ServerClick += new EventHandler(btnCambiar_Click);
                    a.InnerText = string.Format("Año {0}", i);
                    a.ID = i.ToString();

                    li.Controls.Add(a);
                    ddlAnteriores.Controls.Add(li);
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string usuario = Convert.ToString(Request.Cookies["UserSistema"]["usuario"].ToString());
                if (usuario.Length == 0)
                    Response.Redirect("../index.html");
                objSeguridad = new BLL.SeguridadB();
                ok = objSeguridad.ValidaPermiso(usuario, "VER_RECIBO_EMPLEADO");
                if (ok == false)
                    Response.Redirect("~\\secure\\accesodenegado.html");

                divError.Visible = false;
                txtError.InnerText = string.Empty;
                //int legajo = Convert.ToInt32(Request.Cookies["UserEmpleado"]["Id"]);
                int legajo = Convert.ToInt16(Request.QueryString["legajo"]);   
                if (!Page.IsPostBack)
                {
                    AsignarDatos(BLL.EmpleadoB.GetByPkTodos(legajo));
                    AsignarSueldos(legajo, DateTime.Now.Year);
                    AsignarAguinaldo(legajo, DateTime.Now.Year);
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }
        }

        //ACTUALIZADO
        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                //int legajo = Convert.ToInt32(Request.Cookies["UserEmpleado"]["Id"]);
                int legajo = Convert.ToInt16(Request.QueryString["legajo"]);
                HtmlAnchor a = (HtmlAnchor)sender;
                AsignarSueldos(legajo, int.Parse(a.ID));
                AsignarAguinaldo(legajo, int.Parse(a.ID));
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }

        }

        private void AsignarDatos(Entities.Empleado objEmpleado)
        {
            try
            {
                lblNombreEmpleado.InnerHtml = objEmpleado.nombre;
                lblLegajo.InnerHtml = string.Format("Lejago: {0}", objEmpleado.legajo);
                //List<DAL.HORARIOS> lst = DAL.HORARIOS.getByPk(objEmpleado.legajo);

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }
        }

        private void AsignarSueldos(int legajo, int anio)
        {
            try
            {
                List<DAL.LIQ_X_EMPLEADO> lst = BLL.LIQ_X_EMPLEADO.getLiquidaciones(anio, legajo, false);
                foreach (var item in lst)
                {
                    divSueldos.Controls.Add(new LiteralControl("<div class=\"col-md-2\">"));
                    divSueldos.Controls.Add(new LiteralControl(string.Format(
                        "<a target=\"_blank\" class=\"btn btn-app btn-block pry\" href=\"../reportes/printRecibosSueldo.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&legajo={3}\">",
                        anio, item.cod_tipo_liq, item.nro_liquidacion, legajo)));
                    divSueldos.Controls.Add(new LiteralControl(string.Format("<i class=\"fa fa-download\"></i>{0}</a></div>",
                        item.des_liquidacion)));
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }
        }

        private void AsignarAguinaldo(int legajo, int anio)
        {
            try
            {
                List<DAL.LIQ_X_EMPLEADO> lst = BLL.LIQ_X_EMPLEADO.getLiquidaciones(anio, legajo, true);
                foreach (var item in lst)
                {
                    divAguinaldos.Controls.Add(new LiteralControl("<div class=\"col-md-2\">"));
                    divAguinaldos.Controls.Add(new LiteralControl(string.Format(
                        "<a target=\"_blank\" class=\"btn btn-app btn-block pry\" href=\"../reportes/printRecibosSueldo.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&legajo={3}\">",
                        anio, item.cod_tipo_liq, item.nro_liquidacion, legajo)));
                    divAguinaldos.Controls.Add(new LiteralControl(string.Format("<i class=\"fa fa-download\"></i>{0}</a></div>",
                        item.des_liquidacion)));

                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }
        }

        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }
    }
}