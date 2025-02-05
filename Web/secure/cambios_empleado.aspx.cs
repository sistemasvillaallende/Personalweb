using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class cambios_empleado : System.Web.UI.Page
    {

        int legajo;
        string nombre;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            legajo = Convert.ToInt32(Request.QueryString["legajo"]);
            nombre = Convert.ToString(Request.QueryString["NOMBRE"]);
            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                CargarGrilla(legajo);
            }

        }

        private void CargarGrilla(int legajo)
        {
            gvCambios.DataSource = BLL.Concepto_Liq_x_EmpB.GetCambiosEmpleadoXLegajo(legajo);
            gvCambios.DataBind();
        }

        protected void gvCambios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DADADA'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Cambios_empleado oCon = (Entities.Cambios_empleado)e.Row.DataItem;
                Label lblFechaMov = (Label)e.Row.FindControl("lblFechaMov");
                Label lblDesCambios = (Label)e.Row.FindControl("lblDesCambios");
                
                lblFechaMov.Text = oCon.fecha_cambio.ToString("d/M/yyyy");
                lblDesCambios.Text = oCon.descripcion_cambio.ToString();
                

            }
        }

        protected void gvCambios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvCambios.PageSize * gvCambios.PageIndex);
        }

        protected void gvCambios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCambios.PageIndex = e.NewPageIndex;
            CargarGrilla(legajo);
        }
    }
}
