using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entities;


namespace web.secure
{
    public partial class Empleado_categoria_detalle : System.Web.UI.Page
    {

        int codCategoria;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            codCategoria = Convert.ToInt32(Request.QueryString["cod_categoria"]);
            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                CargarGrilla(codCategoria);
            }

            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
            }
        }

        private void CargarGrilla(int codCategoria)
        {
            gvCategoriasEmple.DataSource = BLL.Empleados_categoria_cantB.GetEmpleadosByCategoria(codCategoria).Empleados;
            gvCategoriasEmple.DataBind();
        }


        protected void gvCategoriasEmple_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DADADA'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Empleado oCat = (Entities.Empleado)e.Row.DataItem;
                Label lblLegajo = (Label)e.Row.FindControl("lblLegajo");
                Label lblNombre = (Label)e.Row.FindControl("lblNombre");
                Label lblNroDocumento = (Label)e.Row.FindControl("lblNroDocumento");

                lblLegajo.Text = oCat.legajo.ToString();
                lblNombre.Text = oCat.nombre.ToString();
                lblNroDocumento.Text = oCat.nro_documento.ToString();
            }
        }

        protected void gvCategoriasEmple_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvCategoriasEmple.PageSize * gvCategoriasEmple.PageIndex);

        }

        protected void gvCategoriasEmple_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategoriasEmple.PageIndex = e.NewPageIndex;
            CargarGrilla(codCategoria);
        }

    }
}