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
    public partial class categorias_empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                CargarGrilla();
            }
            //CargarGrilla();


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
            gvCategorias.DataSource = BLL.CategoriasB.GetCategorias();
            gvCategorias.DataBind();
        }

        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            Session["opcion"] = 1;
            txtCodigo.Text = "0";
            lblTituloFormModal.Text = "Nuevo Categoria";
            txtDes_categoria.Text = "";
            txtSueldo_basico.Text = "";
            txtDes_categoria.Focus();
            modalPopupExtender.Show();
        }

        protected void lbtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void gvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFCC80'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Categorias oCat = (Entities.Categorias)e.Row.DataItem;
                Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                Label lblFecha_alta = (Label)e.Row.FindControl("lblFecha_alta");
                Label lblDes_categoria = (Label)e.Row.FindControl("lblDes_categoria");
                //Label lblSueldo_basico = (Label)e.Row.FindControl("lblSueldo_basico");
                //
                lblCodigo.Text = oCat.cod_categoria.ToString();
                lblFecha_alta.Text = oCat.fecha_alta_registro.ToString();
                lblDes_categoria.Text = oCat.des_categoria.ToString();
                //lblSueldo_basico.Text = oCat.sueldo_basico.ToString();

            }
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvCategorias.PageSize * gvCategorias.PageIndex);
            int codigo = 0;

            try
            {
                if (e.CommandName == "Page")
                    return;

                codigo = Convert.ToInt32(gvCategorias.DataKeys[indicePaginado].Values["cod_categoria"]);

                if (e.CommandName == "editar")
                {
                    hID.Value = ID.ToString();
                    lblTituloFormModal.Text = "Editar datos de la Categoria";
                    //
                    Entities.Categorias oCat = BLL.CategoriasB.GetByPk(codigo);
                    txtCodigo.Text = oCat.cod_categoria.ToString();
                    txtDes_categoria.Text = Convert.ToString(oCat.des_categoria);
                    txtSueldo_basico.Text = oCat.sueldo_basico.ToString();
                    Session["opcion"] = 2;
                    uPanelCliente.Update();
                    modalPopupExtender.Show();
                }
                if (e.CommandName == "eliminar")
                {
                    //BLL.Plan_CuentasB.deletePlan(id_tipo_cuenta, id_grupo_cuenta, id_cuenta);v
                    //FillPlan();
                }
            }
            catch (Exception ex)
            {
                txtError.InnerText = ex.Message;
                divError.Visible = true;
            }

        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorias.PageIndex = e.NewPageIndex;
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
            throw new NotImplementedException();
        }

        private void ModificaCategoria()
        {
            Entities.Categorias oCate = new Entities.Categorias();
            string message = string.Empty;
            try
            {
                oCate.cod_categoria = Convert.ToInt32(txtCodigo.Text);
                oCate.des_categoria = (txtDes_categoria.Text);
                oCate.sueldo_basico = Convert.ToDecimal(txtSueldo_basico.Text);
                BLL.CategoriasB.ModificaCategoria(oCate);
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
            };
        }

        private void NuevoCategoria()
        {
            Entities.Categorias oCate = new Entities.Categorias();
            string message = string.Empty;
            try
            {
                oCate.cod_categoria = 0;
                oCate.des_categoria = txtDes_categoria.Text;
                oCate.sueldo_basico = Convert.ToDecimal(txtSueldo_basico.Text);
                oCate.fecha_alta_registro = DateTime.Today.ToString();
                BLL.CategoriasB.NuevaCategoria(oCate);
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
                gvCategorias.DataSource = BLL.CategoriasB.FindCategoriaByDes(strCate);
                gvCategorias.DataBind();
            }
            else
                CargarGrilla();
        }

        protected void lbtnActualizar_valores_Click(object sender, EventArgs e)
        {
            //popupActualizarMontos.Show();
            foreach (GridViewRow item in gvCategorias.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtSueldo_basico = (TextBox)item.FindControl("txtSueldo_basico");
                    if (txtSueldo_basico != null)
                        txtSueldo_basico.Enabled = true;
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

        protected void gvCategorias2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvCategorias2_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCategorias2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void cmdBuscar2_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnCancelarValores_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in gvCategorias.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtSueldo_basico = (TextBox)item.FindControl("txtSueldo_basico");
                    if (txtSueldo_basico != null)
                        txtSueldo_basico.Enabled = false;
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
                foreach (GridViewRow item in gvCategorias.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtSueldo_basico = (TextBox)item.FindControl("txtSueldo_basico");
                        if (txtSueldo_basico != null)
                        {
                            int id = int.Parse(gvCategorias.DataKeys[item.RowIndex].Values["cod_categoria"].ToString());
                            Entities.Categorias obj = BLL.CategoriasB.GetByPk(id);
                            obj.sueldo_basico = Convert.ToDecimal(txtSueldo_basico.Text);
                            txtSueldo_basico.Enabled = false;
                            BLL.CategoriasB.ModificaSueldoBasico(obj);
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
                List<Entities.Categorias> lst = new List<Entities.Categorias>();
                lst = BLL.CategoriasB.GetCategorias();
                gv.DataSource = lst;
                gv.DataBind();
                ExportToExcel("Categorias", gv);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}