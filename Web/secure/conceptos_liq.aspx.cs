using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class conceptos_liq : System.Web.UI.Page
    {
        int intPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                CargarCombos();
                //CargarGrilla();
                Session.Add("opcion", 0);
                Session.Add("opcionCuenta", 0);
                Session.Add("opcionValores", 0);
                CargarCombos2();
                CargarGrilla();
            }

            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
                //divMsjTraspaso.Visible = false;
                //divMsjAsistencia.Visible = false;
            }

        }

        protected void CargarGrilla()
        {

            gvConceptos.DataSource = BLL.Concepto_liqB.GetConceptos_liq();
            gvConceptos.DataBind();
            //if (gvConceptos.Rows.Count > 0)
            //{
            //  gvConceptos.UseAccessibleHeader = true;
            //  gvConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
            //PanelInfomacion.Update();
        }

        protected void CargarCombos()
        {
            ddlTipo_concepto.DataTextField = "des_tipo_concepto";
            ddlTipo_concepto.DataValueField = "cod_tipo_concepto";
            ddlTipo_concepto.DataSource = BLL.Tipos_Concepto_LiqB.ListTipo_Concep(0);
            ddlTipo_concepto.DataBind();
        }

        protected void CargarCombos2()
        {
            ddlClasifPersonal.DataTextField = "des_clasif_per";
            ddlClasifPersonal.DataValueField = "cod_clasif_per";
            ddlClasifPersonal.DataSource = BLL.ConsultaEmpleadoB.ListClasificacion_personal(0);
            ddlClasifPersonal.DataBind();

            ddlTipoLiq.DataTextField = "des_tipo_liq";
            ddlTipoLiq.DataValueField = "cod_tipo_liq";
            ddlTipoLiq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipoLiq.DataBind();
        }

        protected void CargarCombosCuentas(int cod_concepto_liq, int cod_clasif_per, int cod_tipo_liq)
        {
            ddlNro_cuenta.Items.Clear();
            ddlNro_cuenta.Items.Clear();
            ddlNro_cuenta.DataTextField = "nro_con_des";
            ddlNro_cuenta.DataValueField = "nro_cta";
            ddlNro_cuenta.DataSource = BLL.Ctas_x_concepto_liqB.GetPlan_cta_egreso(cod_concepto_liq, cod_clasif_per, cod_tipo_liq);
            ddlNro_cuenta.DataBind();
            ddlNro_cuenta.Focus();
            //ddlNro_cuenta.SelectedValue = Convert.ToString(oCta.nro_cta_contable);
            txtNro_Cuenta.Text = Convert.ToString(ddlNro_cuenta.SelectedValue);
        }

        protected void gvConceptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFCC80'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Conceptos_Liq oConcepto = (Entities.Conceptos_Liq)e.Row.DataItem;
                Label lblCod_concepto_liq = (Label)e.Row.FindControl("lblCod_concepto_liq");

                lblCod_concepto_liq.Text = oConcepto.cod_concepto_liq.ToString();

                Label lblFecha_alta = (Label)e.Row.FindControl("lblFecha_alta");
                lblFecha_alta.Text = Convert.ToDateTime(oConcepto.Fecha_alta_registro).ToShortDateString();

                Label lblDescripcion = (Label)e.Row.FindControl("lblDescripcion");
                lblDescripcion.Text = oConcepto.des_concepto_liq;


                Label lblTipo_concepto = (Label)e.Row.FindControl("lblTipo_concepto");
                lblTipo_concepto.Text = oConcepto.des_tipo_concepto;

                CheckBox chkSuma = (CheckBox)e.Row.FindControl("chkSuma");
                chkSuma.Checked = oConcepto.suma;

                CheckBox chkAporte = (CheckBox)e.Row.FindControl("chkAporte");
                chkAporte.Checked = oConcepto.aporte;

                CheckBox chkSujeto_a_desc = (CheckBox)e.Row.FindControl("chkSujeto_a_desc");
                chkSujeto_a_desc.Checked = oConcepto.sujeto_a_desc;

                CheckBox chkSac = (CheckBox)e.Row.FindControl("chkSac");
                chkSac.Checked = oConcepto.sac;

                CheckBox chkRemunerativo = (CheckBox)e.Row.FindControl("chkRemunerativo");
                chkRemunerativo.Checked = oConcepto.remunerativo;

            }
            //PanelInfomacion.Update();
        }

        protected void clean()
        {
            txtCod_concepto_liq.Text = "0";
            txtConcepto.Text = string.Empty;
            ddlTipo_concepto.SelectedIndex = -1;
            chkSac_.Checked = false;
            chkSuma_.Checked = false;
            chkSuma_.Checked = false;
            chkAporte_.Checked = false;
            chkRemunerativo_.Checked = false;
        }

        protected void gvConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvConceptos.PageSize * gvConceptos.PageIndex);
            int codigo = 0;
            try
            {
                if (e.CommandName == "Page")
                    return;

                codigo = Convert.ToInt32(gvConceptos.DataKeys[indicePaginado].Values["cod_concepto_liq"]);

                if (e.CommandName == "editar")
                {
                    hID.Value = ID.ToString();
                    lblTituloFormModal.Text = "Editar Concepto";
                    Entities.Conceptos_Liq oCon = BLL.Concepto_liqB.GetByPk(codigo);
                    txtCod_concepto_liq.Text = oCon.cod_concepto_liq.ToString();
                    txtConcepto.Text = oCon.des_concepto_liq;
                    ddlTipo_concepto.SelectedValue = Convert.ToString(oCon.cod_tipo_concepto);
                    if (oCon.sac == true)
                        chkSac_.Checked = true;
                    else
                        chkSac_.Checked = false;
                    //
                    if (oCon.sujeto_a_desc == true)
                        chkSujeto_a_desc_.Checked = true;
                    else
                        chkSujeto_a_desc_.Checked = false;
                    //
                    if (oCon.suma == true)
                        chkSuma_.Checked = true;
                    else
                        chkSuma_.Checked = false;
                    //
                    if (oCon.remunerativo == true)
                        chkRemunerativo_.Checked = true;
                    else
                        chkRemunerativo_.Checked = false;

                    if (oCon.aporte == true)
                        chkAporte_.Checked = true;
                    else
                        chkAporte_.Checked = false;
                    Session["opcion"] = 2;
                    txtConcepto.Focus();
                    //uPanelCliente.Update();
                    modalConceptoExtender.Show();
                }
                if (e.CommandName == "eliminar")
                {
                    //BLL.Plan_CuentasB.deletePlan(id_tipo_cuenta, id_grupo_cuenta, id_cuenta);v
                    //FillPlan();
                }

                if (e.CommandName == "cuentas")
                {
                    List<Entities.Ctas_x_concepto_liq> oList = new List<Entities.Ctas_x_concepto_liq>();
                    Entities.Ctas_x_concepto_liq oCon = new Entities.Ctas_x_concepto_liq();
                    oList = BLL.Ctas_x_concepto_liqB.GetByPk(codigo);

                    if (oList.Count > 0)
                    {
                        oCon.cod_concepto_liq = oList[0].cod_concepto_liq;
                        oCon.des_concepto_liq = oList[0].des_concepto_liq.ToString();

                        txtCod_concepto_1.Text = oCon.cod_concepto_liq.ToString();
                        txtConcepto_1.Text = oCon.des_concepto_liq;
                        CargarCombosCuentas(oList[0].cod_concepto_liq, oList[0].cod_clasif_per, oList[0].cod_tipo_liq);

                        ddlClasifPersonal.SelectedValue = Convert.ToString(oList[0].cod_clasif_per);
                        ddlTipoLiq.SelectedValue = Convert.ToString(oList[0].cod_tipo_liq);
                    }

                    else
                    {
                        Entities.Conceptos_Liq oConl = BLL.Concepto_liqB.GetByPk(codigo);
                        txtCod_concepto_1.Text = oConl.cod_concepto_liq.ToString();

                        txtConcepto_1.Text = oConl.des_concepto_liq;
                        ddlClasifPersonal.SelectedIndex = -1;
                        ddlTipoLiq.SelectedIndex = -1;
                    }

                    cmdNvacuenta.Enabled = true;
                    cmdModcuenta.Enabled = true;
                    cmdDelcta.Enabled = true;
                    CuentaPopupExtender.Show();

                }


                if (e.CommandName == "valores")
                {
                    Entities.Conceptos_Liq oCon = BLL.Concepto_liqB.GetByPk(codigo);
                    txtCod_concepto_2.Text = oCon.cod_concepto_liq.ToString();
                    txtConcepto_2.Text = oCon.des_concepto_liq;

                    ddlValores.Items.Clear();
                    ddlValores.DataTextField = "nro_valor_des";
                    ddlValores.DataValueField = "valor";
                    ddlValores.DataSource = BLL.Valores_x_concepto_liqB.GetValores(codigo);
                    ddlValores.DataBind();
                    txtValor.Text = Convert.ToString(ddlValores.SelectedValue);
                    cmdNva_valor.Enabled = true;
                    cmdMod_valor.Enabled = true;
                    cmdDel_valor.Enabled = true;
                    ValoresPopupExtender.Show();
                }

                if (e.CommandName == "asistencia")
                {
                    //  
                }
                //uPanelCliente.Update();
                //lbtnNuevo_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                txtError.InnerText = ex.Message;
                divError.Visible = true;
            }
        }

        protected void gvConceptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConceptos.PageIndex = e.NewPageIndex;
            //intPage = gvConceptos.PageIndex;
            CargarGrilla();
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            modalConceptoExtender.Hide();
            //UpdatePanel5.Update();

        }

        protected void btnCrearConcepto_Click(object sender, EventArgs e)
        {

            int op = 0;
            op = (Convert.ToInt32(Session["opcion"]));

            switch (op)
            {
                case 1:
                    NuevoConcepto();
                    break;
                case 2:
                    ModificaConcepto();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            CargarGrilla();
        }


        protected void NuevoConcepto()
        {
            Entities.Conceptos_Liq oCon = new Entities.Conceptos_Liq();
            string message = string.Empty;
            try
            {
                oCon.cod_concepto_liq = int.Parse(txtCod_concepto_liq.Text);
                oCon.des_concepto_liq = txtConcepto.Text;
                oCon.Fecha_alta_registro = DateTime.Today.ToString();
                oCon.cod_tipo_concepto = Convert.ToInt32(ddlTipo_concepto.SelectedValue);
                oCon.suma = chkSuma_.Checked;
                oCon.sac = chkSac_.Checked;
                oCon.sujeto_a_desc = chkSujeto_a_desc_.Checked;
                oCon.aporte = chkAporte_.Checked;
                oCon.remunerativo = chkRemunerativo_.Checked;
                oCon.Fecha_alta_registro = DateTime.Today.ToString();
                BLL.Concepto_liqB.NuevoConcepto(oCon);
                message = "Alta del Concepto Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                modalConceptoExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }

        }

        protected void ModificaConcepto()
        {
            Entities.Conceptos_Liq oCon = new Entities.Conceptos_Liq();
            string message = string.Empty;
            try
            {
                oCon.cod_concepto_liq = int.Parse(txtCod_concepto_liq.Text);
                oCon.des_concepto_liq = txtConcepto.Text;
                oCon.cod_tipo_concepto = Convert.ToInt32(ddlTipo_concepto.SelectedValue);
                oCon.suma = chkSuma_.Checked;
                oCon.sac = chkSac_.Checked;
                oCon.sujeto_a_desc = chkSujeto_a_desc_.Checked;
                oCon.aporte = chkAporte_.Checked;
                oCon.remunerativo = chkRemunerativo_.Checked;
                BLL.Concepto_liqB.ModificaConcepto(oCon);
                message = "Modificacion del Concepto Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
            }

            catch (Exception ex)
            {
                modalConceptoExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
                //throw ex;
            }
        }

        protected void btnCloseModal_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lbtnNuevo_concepto_Click(object sender, EventArgs e)
        {
            Session["opcion"] = 1;
            clean();
            lblTituloFormModal.Text = "Nuevo Concepto";
            txtCod_concepto_liq.Text = "0";

            txtConcepto.Focus();
            modalConceptoExtender.Show();

        }

        protected void lbtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            var strConcepto = txtInput.Value;
            if (txtInput.Value.Length > 0)
            {
                gvConceptos.DataSource = BLL.Concepto_liqB.findConcepto_liqByDescripcion(strConcepto);
                gvConceptos.DataBind();
            }
            else
                CargarGrilla();
        }

        protected void btnCloseModalCuenta_ServerClick(object sender, EventArgs e)
        {
        }

        protected void btnCancela_cuenta_Click(object sender, EventArgs e)
        {
        }

        protected void btnAceptar_cuenta_Click(object sender, EventArgs e)
        {
            int op = 0;
            op = (Convert.ToInt32(Session["opcionCuenta"]));
            switch (op)
            {
                case 1:
                    NuevoCuenta();
                    break;
                case 2:
                    ModificaCuenta();
                    break;
                case 3:
                    EliminaCuenta();
                    break;
                default:
                    break;
            }
        }

        private void EliminaCuenta()
        {
            Entities.Ctas_x_concepto_liq oCta = new Entities.Ctas_x_concepto_liq();
            string message = string.Empty;
            try
            {
                oCta.cod_concepto_liq = Convert.ToInt32(txtCod_concepto_1.Text);
                oCta.cod_tipo_liq = Convert.ToInt32(ddlTipoLiq.SelectedValue);
                oCta.cod_clasif_per = Convert.ToInt32(ddlClasifPersonal.SelectedValue);
                oCta.nro_cta = txtNro_Cuenta.Text;
                BLL.Ctas_x_concepto_liqB.EliminaCuenta(oCta);
                message = "Eliminacion de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                CuentaPopupExtender.Show();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        private void ModificaCuenta()
        {
            Entities.Ctas_x_concepto_liq oCta = new Entities.Ctas_x_concepto_liq();
            string message = string.Empty;
            try
            {
                oCta.cod_concepto_liq = Convert.ToInt32(txtCod_concepto_1.Text);
                oCta.cod_tipo_liq = Convert.ToInt32(ddlTipoLiq.SelectedValue);
                oCta.cod_clasif_per = Convert.ToInt32(ddlClasifPersonal.SelectedValue);
                oCta.nro_cta = txtNro_Cuenta.Text;
                BLL.Ctas_x_concepto_liqB.ModificaCuenta(oCta);
                message = "Modificacion de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
            }
            catch (Exception ex)
            {
                CuentaPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        private void NuevoCuenta()
        {
            Entities.Ctas_x_concepto_liq oCta = new Entities.Ctas_x_concepto_liq();
            string message = string.Empty;
            try
            {
                oCta.cod_concepto_liq = Convert.ToInt32(txtCod_concepto_1.Text);
                oCta.cod_tipo_liq = Convert.ToInt32(ddlTipoLiq.SelectedValue);
                oCta.cod_clasif_per = Convert.ToInt32(ddlClasifPersonal.SelectedValue);
                oCta.nro_cta = txtNro_Cuenta.Text;
                oCta.fecha_alta_registro = DateTime.Today.ToString();
                BLL.Ctas_x_concepto_liqB.NuevaCuenta(oCta);
                message = "Alta de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                CuentaPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        protected void ddlNro_cuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNro_Cuenta.Text = Convert.ToString(ddlNro_cuenta.SelectedValue);
            CuentaPopupExtender.Show();
        }


        protected void ddlTipoLiq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int op = 0;
            op = (Convert.ToInt32(Session["opcionCuenta"]));
            if (op != 1)
            {
                int cod_concepto_liq = Convert.ToInt32(txtCod_concepto_1.Text);
                int cod_clasif_per = Convert.ToInt32(ddlClasifPersonal.SelectedValue);
                int cod_tipo_liq = Convert.ToInt32(ddlTipoLiq.SelectedValue);
                //List<Entities.Ctas_x_concepto_liq> oCta = new List<Entities.Ctas_x_concepto_liq>();
                ddlNro_cuenta.Items.Clear();
                ddlNro_cuenta.DataTextField = "nro_con_des";
                ddlNro_cuenta.DataValueField = "nro_cta";
                ddlNro_cuenta.DataSource = BLL.Ctas_x_concepto_liqB.GetPlan_cta_egreso(cod_concepto_liq, cod_clasif_per, cod_tipo_liq);
                ddlNro_cuenta.DataBind();
                ddlNro_cuenta.Focus();
                //ddlNro_cuenta.SelectedValue = Convert.ToString(oCta.nro_cta_contable);
                txtNro_Cuenta.Text = Convert.ToString(ddlNro_cuenta.SelectedValue);
            }
            CuentaPopupExtender.Show();

        }

        protected void ddlClasifPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int op = 0;
            op = (Convert.ToInt32(Session["opcionCuenta"]));
            if (op != 1)
            {
                //ddlNro_cuenta.Items.Clear();
                //txtNro_Cuenta.Text = string.Empty;
                int cod_concepto_liq = Convert.ToInt32(txtCod_concepto_1.Text);
                int cod_clasif_per = Convert.ToInt32(ddlClasifPersonal.SelectedValue);
                int cod_tipo_liq = Convert.ToInt32(ddlTipoLiq.SelectedValue);
                //List<Entities.Ctas_x_concepto_liq> oCta = new List<Entities.Ctas_x_concepto_liq>();
                ddlNro_cuenta.Items.Clear();
                ddlNro_cuenta.DataTextField = "nro_con_des";
                ddlNro_cuenta.DataValueField = "nro_cta";
                ddlNro_cuenta.DataSource = BLL.Ctas_x_concepto_liqB.GetPlan_cta_egreso(cod_concepto_liq, cod_clasif_per, cod_tipo_liq);
                ddlNro_cuenta.DataBind();
                ddlNro_cuenta.Focus();
                //ddlNro_cuenta.SelectedValue = Convert.ToString(oCta.nro_cta_contable);
                txtNro_Cuenta.Text = Convert.ToString(ddlNro_cuenta.SelectedValue);
            }
            CuentaPopupExtender.Show();
        }

        protected void cmdNvacuenta_Click(object sender, EventArgs e)
        {
            ddlNro_cuenta.Items.Clear();
            txtNro_Cuenta.Text = string.Empty;
            Session["opcionCuenta"] = 1;
            ddlNro_cuenta.DataTextField = "nro_con_des";
            ddlNro_cuenta.DataValueField = "nro_cta";
            ddlNro_cuenta.DataSource = BLL.Ctas_x_concepto_liqB.GetPlan_cta_egreso();
            ddlNro_cuenta.DataBind();
            cmdNvacuenta.Enabled = false;
            cmdModcuenta.Enabled = false;
            cmdDelcta.Enabled = false;
            CuentaPopupExtender.Show();
        }


        protected void cmdModcuenta_Click(object sender, EventArgs e)
        {
            Session["opcionCuenta"] = 2;
            ddlNro_cuenta.DataTextField = "nro_con_des";
            ddlNro_cuenta.DataValueField = "nro_cta";
            ddlNro_cuenta.DataSource = BLL.Ctas_x_concepto_liqB.GetPlan_cta_egreso();
            ddlNro_cuenta.DataBind();
            ddlNro_cuenta.SelectedValue = Convert.ToString(txtNro_Cuenta.Text);
            cmdNvacuenta.Enabled = false;
            cmdModcuenta.Enabled = false;
            cmdDelcta.Enabled = false;
            CuentaPopupExtender.Show();
        }

        protected void cmdDelcta_Click(object sender, EventArgs e)
        {
            Session["opcionCuenta"] = 3;
            cmdNvacuenta.Enabled = false;
            cmdModcuenta.Enabled = false;
            cmdDelcta.Enabled = false;
            CuentaPopupExtender.Show();
        }

        protected void btnCloseModalValor_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnCancela_valor_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_valor_Click(object sender, EventArgs e)
        {
            int op = 0;
            op = (Convert.ToInt32(Session["opcionValores"]));
            switch (op)
            {
                case 1:
                    NuevoValor();
                    break;
                case 2:
                    ModificaValor();
                    break;
                case 3:
                    EliminaValor();
                    break;
                default:
                    break;
            }
        }

        private void EliminaValor()
        {
            Entities.Valores_x_concepto_liq oVal = new Entities.Valores_x_concepto_liq();
            string message = string.Empty;

            try
            {
                string[] nro_valor = ddlValores.Text.Split(Convert.ToChar("-"));
                oVal.cod_concepto_liq = int.Parse(txtCod_concepto_2.Text);
                oVal.nro_valor = Convert.ToInt32(nro_valor[0]);
                oVal.valor = decimal.Parse(txtValor.Text);
                BLL.Valores_x_concepto_liqB.EliminaValor(oVal);
                message = "Elimina el Valor de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                ValoresPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }


        private void ModificaValor()
        {
            Entities.Valores_x_concepto_liq oVal = new Entities.Valores_x_concepto_liq();
            string message = string.Empty;
            try
            {
                string[] nro_valor = ddlValores.SelectedItem.Text.Split(Convert.ToChar("-"));
                oVal.cod_concepto_liq = int.Parse(txtCod_concepto_2.Text);
                oVal.nro_valor = Convert.ToInt32(nro_valor[0]);
                oVal.valor = Convert.ToDecimal(txtValor.Text);
                BLL.Valores_x_concepto_liqB.ModificaValor(oVal);
                message = "Modifica el Valor de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                ValoresPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;

            }
        }

        private void NuevoValor()
        {
            Entities.Valores_x_concepto_liq oVal = new Entities.Valores_x_concepto_liq();
            string message = string.Empty;
            try
            {
                oVal.cod_concepto_liq = int.Parse(txtCod_concepto_2.Text);
                oVal.fecha_alta_registro = DateTime.Today.ToString();
                oVal.nro_valor = 0;
                oVal.valor = decimal.Parse(txtValor.Text);
                BLL.Valores_x_concepto_liqB.NuevoValor(oVal);
                message = "Alta del el Valor de la Cuenta Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;

            }
            catch (Exception ex)
            {
                ValoresPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
            }
        }

        protected void cmdNva_valor_Click(object sender, EventArgs e)
        {
            //ddlValores.Items.Clear();
            txtValor.Text = string.Empty;
            ddlValores.SelectedIndex = -1;
            ddlValores.Items.Clear();
            Session["opcionValores"] = 1;
            txtValor.Text = "0";
            txtValor.Focus();
            cmdNva_valor.Enabled = false;
            cmdMod_valor.Enabled = false;
            cmdDel_valor.Enabled = false;
            ValoresPopupExtender.Show();
        }

        protected void cmdMod_valor_Click(object sender, EventArgs e)
        {
            Session["opcionValores"] = 2;
            cmdNva_valor.Enabled = false;
            cmdMod_valor.Enabled = false;
            cmdDel_valor.Enabled = false;
            ValoresPopupExtender.Show();
        }

        protected void cmdDel_valor_Click(object sender, EventArgs e)
        {
            Session["opcionValores"] = 3;
            cmdNva_valor.Enabled = false;
            cmdMod_valor.Enabled = false;
            cmdDel_valor.Enabled = false;
            ValoresPopupExtender.Show();
        }

        protected void ddlValores_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValor.Text = Convert.ToString(ddlValores.SelectedValue);
            ValoresPopupExtender.Show();
        }

        protected void lbtnListado_concepto_Click(object sender, EventArgs e)
        {
            //
            Ctas_x_conceptosExcel();

        }



        private void Ctas_x_conceptosExcel()
        {
            List<Entities.LstCtas_x_concepto> lstConceptos = new List<Entities.LstCtas_x_concepto>();
            string message = string.Empty;
            GridView gridExcel = new GridView();
            string txtArchivo = "";
            //
            try
            {
                lstConceptos = DAL.Ctas_x_concepto_liqD.GetCtas_x_conceptos();
                gridExcel.DataSource = lstConceptos;
                gridExcel.DataBind();
                if (lstConceptos.Count > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo + ".xls"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo + ".xls");
                    DescargarDocumentoExcel("Cuentas_x_conceptos" + ".xls", gridExcel);
                }
            }
            catch (Exception e)
            {
                lstConceptos = null;
                gridExcel = null;
                throw e;
            }
        }

        private void DescargarDocumentoExcel(string nameReport, GridView wControl)
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
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport);

            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }
    }
}


