using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace web.secure
{
    public partial class categorias_empleados_monotributo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                CargarGrilla();
            }


            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
            }

        }

        private void CargarGrilla()
        {
            gvCategoriasMono.DataSource = BLL.Categoria_profesional_monotributoB.GetCategoriaProfesionalMono();
            gvCategoriasMono.DataBind();
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            Session["opcion"] = 1;
            txtCodigo.Text = "0";
            lblTituloFormModal.Text = "Nuevo Categoria";
            txtCategoria.Text = "";
            txtMonto.Text = "";
            txtMonto.Focus();
            modalPopupExtender.Show();
        }

        protected void lbtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void gvCategoriasMono_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DADADA'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Categoria_profesional_monotributo oCat = (Entities.Categoria_profesional_monotributo)e.Row.DataItem;
                Label lblIdCateMono = (Label)e.Row.FindControl("lblIdCateMono");
                Label lblFecha_alta = (Label)e.Row.FindControl("lblFecha_alta");
                Label lblCategoria = (Label)e.Row.FindControl("lblCategoria");
                //Label lblMonto = (Label)e.Row.FindControl("lblMonto");
                //TextBox txtMonto = (TextBox)e.Row.FindControl("txtMonto");

                lblIdCateMono.Text = oCat.id_profesional_monotributo.ToString();
                lblFecha_alta.Text = oCat.fecha_alta.ToString();
                lblCategoria.Text = oCat.categoria.ToString();
                // lblMonto.Text = oCat.monto.ToString();

            }
        }

        protected void gvCategoriasMono_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvCategoriasMono.PageSize * gvCategoriasMono.PageIndex);
            int codigo = 0;

            try
            {
                if (e.CommandName == "Page")
                    return;

                codigo = Convert.ToInt32(gvCategoriasMono.DataKeys[indicePaginado].Values["id_profesional_monotributo"]);

                if (e.CommandName == "editar")
                {
                    hID.Value = ID.ToString();
                    lblTituloFormModal.Text = "Editar datos de la Categoria";
                    //
                    Entities.Categoria_profesional_monotributo oCat = BLL.Categoria_profesional_monotributoB.GetByPk(codigo);
                    txtCodigo.Text = oCat.id_profesional_monotributo.ToString();
                    txtCategoria.Text = Convert.ToString(oCat.categoria);
                    txtMonto.Text = oCat.monto.ToString();
                    Session["opcion"] = 2;
                    uPanelCliente.Update();
                    modalPopupExtender.Show();
                }
                if (e.CommandName == "eliminar")
                {
                    //BLL.Plan_CuentasB.deletePlan(id_tipo_cuenta, id_grupo_cuenta, id_cuenta);v
                    //FillPlan();
                    txtCodigo.Text = codigo.ToString();

                    EliminaCategoria();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                txtError.InnerText = ex.Message;
                divError.Visible = true;
            }

        }

        protected void gvCategoriasMono_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategoriasMono.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void btnCloseModal_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int op = 0;
            op = (Convert.ToInt32(Session["opcion"]));
            switch (op)
            {
                case 1:
                    NuevoCategoria();
                    break;
                case 2:
                    ModificaCategoria();
                    break;
                case 3:
                    EliminaCategoria();
                    break;
                default:
                    break;
            }
        }

        private void EliminaCategoria()
        {
            string message = string.Empty;
            try
            {
                int id = Convert.ToInt32(txtCodigo.Text);

                BLL.Categoria_profesional_monotributoB.EliminaCategoria(id);

                message = "Eliminación de la categoría finalizó correctamente...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
            }
            catch (Exception ex)
            {
                modalPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        private void ModificaCategoria()
        {
            Entities.Categoria_profesional_monotributo oCate = new Entities.Categoria_profesional_monotributo();
            string message = string.Empty;
            try
            {
                oCate.id_profesional_monotributo = Convert.ToInt32(txtCodigo.Text);
                oCate.categoria = txtCategoria.Text;
                oCate.monto = Convert.ToDecimal(txtMonto.Text);

                var datosActuales = BLL.Categoria_profesional_monotributoB.GetByPk(oCate.id_profesional_monotributo);

                if (datosActuales != null && datosActuales.monto != oCate.monto)
                {
                    BLL.Categoria_profesional_monotributo_histB.AgregarAlHistorial(oCate.id_profesional_monotributo, oCate.monto);
                }

                BLL.Categoria_profesional_monotributoB.ModificaCategoria(oCate);
                message = "Modificacion de la Categoria Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
            }
            catch (Exception ex)
            {
                modalPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
            ;
        }

        private void NuevoCategoria()
        {
            Entities.Categoria_profesional_monotributo oCate = new Entities.Categoria_profesional_monotributo();
            string message = string.Empty;
            try
            {
                oCate.id_profesional_monotributo = 0;
                oCate.categoria = txtCategoria.Text;
                oCate.monto = Convert.ToDecimal(txtMonto.Text);
                oCate.fecha_alta = DateTime.Today;
                BLL.Categoria_profesional_monotributoB.NuevaCategoria(oCate);
                message = "Alta de la Categoria Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                modalPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            var strCate = txtInput.Value;
            if (txtInput.Value.Length > 0)
            {
                gvCategoriasMono.DataSource = BLL.Categoria_profesional_monotributoB.FindCategoriaByDes(strCate);
                gvCategoriasMono.DataBind();
            }
            else
                CargarGrilla();
        }

        protected void lbtnActualizar_valores_Click(object sender, EventArgs e)
        {
            //popupActualizarMontos.Show();
            foreach (GridViewRow item in gvCategoriasMono.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtMonto = (TextBox)item.FindControl("txtMonto");
                    if (txtMonto != null)
                        txtMonto.Enabled = true;
                }
            }
            divActualiza.Visible = false;
            divAcepta.Visible = true;
        }


        protected void lbtnCancelarBuscador_Click(object sender, EventArgs e)
        {
            popupActualizarMontos.Hide();
        }

        protected void lbtnSalirBuscador_Click(object sender, EventArgs e)
        {
            popupActualizarMontos.Hide();
        }

        protected void gvCategoriasMono2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvCategoriasMono2_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCategoriasMono2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void cmdBuscar2_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnCancelarValores_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in gvCategoriasMono.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtMonto = (TextBox)item.FindControl("txtMonto");
                    if (txtMonto != null)
                        txtMonto.Enabled = false;
                }
            }
            divActualiza.Visible = true;
            divAcepta.Visible = false;
            CargarGrilla();
        }

        protected void btnAceptarValores_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (GridViewRow item in gvCategoriasMono.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtMonto = (TextBox)item.FindControl("txtMonto");
                        if (txtMonto != null)
                        {
                            int id = int.Parse(gvCategoriasMono.DataKeys[item.RowIndex].Values["id_profesional_monotributo"].ToString());
                            Entities.Categoria_profesional_monotributo obj = BLL.Categoria_profesional_monotributoB.GetByPk(id);
                            obj.monto = Convert.ToDecimal(txtMonto.Text);
                            txtMonto.Enabled = false;
                            BLL.Categoria_profesional_monotributoB.ModificaMonto(obj);
                        }
                    }
                }
                scope.Complete();
            }
            divActualiza.Visible = true;
            divAcepta.Visible = false;
            CargarGrilla();

        }

        private void ExportToExcel(string nameReport, GridView wControl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            wControl.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);
            form.Controls.Add(wControl);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=DATA.xls");
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }

        protected void LinkExportar_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                List<Entities.Categoria_profesional_monotributo> lst = new List<Entities.Categoria_profesional_monotributo>();
                lst = BLL.Categoria_profesional_monotributoB.GetCategoriaProfesionalMono();
                gv.DataSource = lst;
                gv.DataBind();
                ExportToExcel("Categorias", gv);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void lbtnHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("categorias_empleados_monotributo_hist.aspx");
        }
    }
}


