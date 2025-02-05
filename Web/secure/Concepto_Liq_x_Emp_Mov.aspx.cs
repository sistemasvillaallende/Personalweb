using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace web.secure
{
    public partial class Concepto_Liq_x_Emp_Mov : System.Web.UI.Page
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
            gvConceptoMov.DataSource = BLL.Concepto_Liq_x_EmpB.GetHistorial_ConceptosXLegajo(legajo);
            gvConceptoMov.DataBind();
        }

        protected void gvConceptoMov_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DADADA'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Historial_conceptos oCon = (Entities.Historial_conceptos)e.Row.DataItem;
                Label lblFechaMov = (Label)e.Row.FindControl("lblFechaMov");
                Label lblUsuarioCarga = (Label)e.Row.FindControl("lblUsuarioCarga");
                Label lblTipoMov = (Label)e.Row.FindControl("lblTipoMov");
                Label lblCodConcepto = (Label)e.Row.FindControl("lblCodConcepto");
                Label lblConcepto = (Label)e.Row.FindControl("lblConcepto");
                Label lblValorConcepto = (Label)e.Row.FindControl("lblValorConcepto");
                Label lblObservacion = (Label)e.Row.FindControl("lblObservacion");


                lblFechaMov.Text = oCon.Fecha.ToString("d/M/yyyy");
                lblUsuarioCarga.Text = oCon.Usuario_Carga.ToString();
                lblTipoMov.Text = oCon.Tipo_movimiento.ToString();
                lblCodConcepto.Text = oCon.Cod_concepto_liq.ToString();
                lblConcepto.Text = oCon.Concepto.ToString();
                lblValorConcepto.Text = oCon.Valor_concepto_liq.ToString();
                lblObservacion.Text = oCon.Observacion.ToString();

            }
        }

        protected void gvConceptoMov_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvConceptoMov.PageSize * gvConceptoMov.PageIndex);
        }

        protected void gvConceptoMov_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConceptoMov.PageIndex = e.NewPageIndex;
            CargarGrilla(legajo);
        }
    }
}