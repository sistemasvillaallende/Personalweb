using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace web.secure
{
    public partial class categoria_empleados_hist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                CargarGrilla();
            }

        }

        private void CargarGrilla()
        {
            gvCategoriasHist.DataSource = BLL.Categorias_historialB.GetCategoriaHistorial();
            gvCategoriasHist.DataBind();
        }



        protected void gvCategoriasHist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFCC80'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Categorias_historial oCat = (Entities.Categorias_historial)e.Row.DataItem;
                Label lblCategoria = (Label)e.Row.FindControl("lblCategoria");
                Label lblDesCategoria = (Label)e.Row.FindControl("lblDesCategoria");
                Label lblMovimiento = (Label)e.Row.FindControl("lblMovimiento");
                Label lblFechaMovimiento = (Label)e.Row.FindControl("lblFechaMovimiento");
                Label lblSueldo = (Label)e.Row.FindControl("lblSueldo");


                lblCategoria.Text = oCat.ToString();
                lblMovimiento.Text = oCat.item.ToString();
                lblFechaMovimiento.Text = oCat.fecha_alta_registro.ToString();
                lblSueldo.Text = oCat.sueldo_basico.ToString();
                lblDesCategoria.Text = oCat.des_categoria.ToString();

            }
        }

      

        protected void gvCategoriasHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategoriasHist.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }


   
    }
}