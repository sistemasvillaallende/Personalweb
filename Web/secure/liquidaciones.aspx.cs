using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class liquidaciones : System.Web.UI.Page
    {
        int intPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                CargarCombos();
                Session.Add("opcion", 0);
                /*CargarGrilla*/
                CargarGrilla();
            }



            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
                divMsjTraspaso.Visible = false;
                divMsjAsistencia.Visible = false;
                divMsjPublicar.Visible = false;
            }

        }

        protected void CargarGrilla()
        {
            gvLiquidaciones.DataSource = BLL.LiquidacionesB.getLstLiq();
            gvLiquidaciones.DataBind();
        }

        protected void CargarCombos()
        {
            ddlTipo_liq.DataTextField = "des_tipo_liq";
            ddlTipo_liq.DataValueField = "cod_tipo_liq";
            ddlTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq.DataBind();


            ddlTipo_liq_1.DataTextField = "des_tipo_liq";
            ddlTipo_liq_1.DataValueField = "cod_tipo_liq";
            ddlTipo_liq_1.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq_1.DataBind();

            ddlTipo_liq_2.DataTextField = "des_tipo_liq";
            ddlTipo_liq_2.DataValueField = "cod_tipo_liq";
            ddlTipo_liq_2.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq_2.DataBind();

            ddlTipo_liq_3.DataTextField = "des_tipo_liq";
            ddlTipo_liq_3.DataValueField = "cod_tipo_liq";
            ddlTipo_liq_3.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq_3.DataBind();

            ddLSemestre.DataTextField = "descripcion";
            ddLSemestre.DataValueField = "cod_semestre";
            ddLSemestre.DataSource = BLL.ConsultaEmpleadoB.ListSemestres(0);
            ddLSemestre.DataBind();

            ddlBanco.DataTextField = "nom_banco";
            ddlBanco.DataValueField = "cod_banco";
            ddlBanco.DataSource = BLL.ConsultaEmpleadoB.ListBancos(0);
            ddlBanco.DataBind();
        }

        protected void ddlTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(txtAnio.Text);
                int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
                if (anio != 0)
                {
                    ddlNro_liq.Items.Clear();
                    ddlNro_liq.DataTextField = "des_liquidacion";
                    ddlNro_liq.DataValueField = "nro_liquidacion";
                    ddlNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
                    ddlNro_liq.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ddlNro_liq.Focus();
        }



        protected void ddlTipo_liq_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(txtAnio_1.Text);
                int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_1.SelectedValue);
                if (anio != 0)
                {
                    ddlNro_liq_1.Items.Clear();
                    ddlNro_liq_1.DataTextField = "des_liquidacion";
                    ddlNro_liq_1.DataValueField = "nro_liquidacion";
                    ddlNro_liq_1.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
                    ddlNro_liq_1.DataBind();
                }
                uPanelCliente.Update();
                lbtnmodalTraspasoExtender.Show();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            txtAnio_1.Focus();

        }

        protected void ddlTipo_liq_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(txtAnio_2.Text);
                int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_2.SelectedValue);
                if (anio != 0)
                {
                    ddlNro_liq_2.Items.Clear();
                    ddlNro_liq_2.DataTextField = "des_liquidacion";
                    ddlNro_liq_2.DataValueField = "nro_liquidacion";
                    ddlNro_liq_2.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
                    ddlNro_liq_2.DataBind();
                }
                uPanelCliente.Update();
                lbtnmodalAsistenciaExtender.Show();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            txtAnio_2.Focus();

        }


        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            Session["opcion"] = 1;
            lblTituloFormModal.Text = "Nueva Liquidacion";
            txtAnio.Text = ""; ;
            ddlTipo_liq.SelectedIndex = 0;
            ddlNro_liq.Items.Clear();
            txtDes_liquidacion.Text = "";
            txtPeriodo.Text = "";
            ddLSemestre.SelectedIndex = 0;
            chkAguinaldo.Checked = false;
            txtFecha_liquidacion.Text = "";
            txtFecha_pago.Text = "";
            txtPer_ult_dep.Text = "";
            txtFecha_ult_deposito.Text = "";
            chkPublicar.Checked = false;
            chkPrueba.Checked = false;
            ddlBanco.SelectedIndex = 0;
            txtAnio.Focus();
            uPanelCliente.Update();
            lbtnNuevo_ModalPopupExtender.Show();
        }

        protected void gvLiquidaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvLiquidaciones.PageSize * gvLiquidaciones.PageIndex);
            int anio = 0;
            int cod_tipo_liq = 0;
            int nro_liquidacion = 0;
            try
            {
                if (e.CommandName == "Page")
                    return;

                anio = Convert.ToInt32(gvLiquidaciones.DataKeys[indicePaginado].Values["anio"]);
                cod_tipo_liq = Convert.ToInt32(gvLiquidaciones.DataKeys[indicePaginado].Values["cod_tipo_liq"]);
                nro_liquidacion = Convert.ToInt32(gvLiquidaciones.DataKeys[indicePaginado].Values["nro_liquidacion"]);

                if (e.CommandName == "editar")
                {
                    hID.Value = ID.ToString();
                    lblTituloFormModal.Text = "Editar datos de la Liquidación";
                    //BLL.LiquidacionB.
                    Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                    txtAnio.Text = oLiq.anio.ToString();
                    ddlTipo_liq.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                    ddlTipo_liq_SelectedIndexChanged(ddlTipo_liq, e);
                    ddlNro_liq.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                    txtDes_liquidacion.Text = oLiq.des_liquidacion;
                    txtPeriodo.Text = oLiq.periodo;
                    ddLSemestre.SelectedValue = Convert.ToString(oLiq.cod_semestre);
                    if (oLiq.aguinaldo == true)
                        chkAguinaldo.Checked = true;
                    else
                        chkAguinaldo.Checked = false;
                    txtFecha_liquidacion.Text = oLiq.fecha_liquidacion;
                    txtFecha_pago.Text = oLiq.fecha_pago;
                    txtPer_ult_dep.Text = oLiq.per_ult_dep;
                    txtFecha_ult_deposito.Text = oLiq.fecha_ult_dep;
                    if (oLiq.cod_banco_ult_dep != 0)
                        ddlBanco.SelectedValue = Convert.ToString(oLiq.cod_banco_ult_dep);

                    chkPublicar.Checked = Convert.ToBoolean(oLiq.publica);
                    chkCerrada.Checked = Convert.ToBoolean(oLiq.cerrada);
                    chkPrueba.Checked = Convert.ToBoolean(oLiq.prueba);

                    Session["opcion"] = 2;
                    uPanelCliente.Update();
                    lbtnNuevo_ModalPopupExtender.Show();
                }
                if (e.CommandName == "eliminar")
                {
                    //BLL.Plan_CuentasB.deletePlan(id_tipo_cuenta, id_grupo_cuenta, id_cuenta);v
                    //FillPlan();
                }
                if (e.CommandName == "liquidar")
                {
                    Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                    if (oLiq.cerrada == true)
                    {
                        string message = "No puede Re Liquidar esta Liquidación, porque la misma esta Cerrada...!";
                        msjConfirmar.InnerHtml = message;
                        divConfirma.Visible = true;
                        PanelInfomacion.Update();
                    }
                    else
                    {
                        lblTituloFormModal.Text = "Liquidar Sueldos";
                        //Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                        txtAnio.Text = oLiq.anio.ToString();
                        ddlTipo_liq.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                        ddlTipo_liq_SelectedIndexChanged(ddlTipo_liq, e);
                        ddlNro_liq.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                        txtDes_liquidacion.Text = oLiq.des_liquidacion;
                        txtPeriodo.Text = oLiq.periodo;
                        ddLSemestre.SelectedValue = Convert.ToString(oLiq.cod_semestre);
                        if (oLiq.aguinaldo == true)
                            chkAguinaldo.Checked = true;
                        else
                            chkAguinaldo.Checked = false;
                        txtFecha_liquidacion.Text = oLiq.fecha_liquidacion;
                        txtFecha_pago.Text = oLiq.fecha_pago;
                        txtPer_ult_dep.Text = oLiq.per_ult_dep;
                        txtFecha_ult_deposito.Text = oLiq.fecha_ult_dep;
                        
                        chkPublicar.Checked = Convert.ToBoolean(oLiq.publica);
                        chkCerrada.Checked = Convert.ToBoolean(oLiq.cerrada);
                        chkPrueba.Checked = Convert.ToBoolean(oLiq.prueba);

                        if (oLiq.cod_banco_ult_dep != 0)
                            ddlBanco.SelectedValue = Convert.ToString(oLiq.cod_banco_ult_dep);
                        Session["opcion"] = 3;
                        chkSalarioFam.Checked = false;
                        uPanelCliente.Update();
                        lbtnNuevo_ModalPopupExtender.Show();
                    }
                }

                if (e.CommandName == "traspaso")
                {
                    Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                    hAño.Value = anio.ToString();
                    hCod_tipo_liq.Value = cod_tipo_liq.ToString();
                    hNro_liq.Value = oLiq.nro_liquidacion.ToString();
                    hDes_tipo_liq.Value = oLiq.des_tipo_liq;
                    hDes_liquidacion.Value = oLiq.des_liquidacion;
                    txtLiquidacion.Text = hAño.Value + " / " + hDes_tipo_liq.Value.Trim() + " / " + hDes_liquidacion.Value;
                    lblTraspaso.Text = "Traspaso de Conceptos de una Liquidacion Anterior a la Actual";
                    txtAnio_1.Text = oLiq.anio.ToString();
                    ddlTipo_liq_1.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                    ddlTipo_liq_1_SelectedIndexChanged(ddlTipo_liq_1, e);
                    ddlNro_liq_1.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                    divMsjTraspaso.Visible = false;
                    txtCod_concepto.Text = string.Empty;
                    txtConcepto.Text = string.Empty;
                    Session["opcion"] = 4;
                    uPanelCliente.Update();
                    lbtnmodalTraspasoExtender.Show();
                }

                if (e.CommandName == "asistencia")
                {
                    lblTitulo_asistencia.Text = "Carga Masiva de Dias Trabajados, Asistencia Perfecta y Puntualidad";
                    Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                    txtAnio_2.Text = oLiq.anio.ToString();
                    ddlTipo_liq_2.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                    ddlTipo_liq_2_SelectedIndexChanged(ddlTipo_liq_2, e);
                    ddlNro_liq_2.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                    txtDes_liquidacion_2.Text = oLiq.des_liquidacion;
                    txtPeriodo_2.Text = oLiq.periodo;
                    chkPuntualidad.Checked = false;
                    chkAsistencia.Checked = false;
                    chkDias.Checked = false;
                    Session["opcion"] = 5;
                    uPanelCliente.Update();
                    lbtnmodalAsistenciaExtender.Show();
                }

                if (e.CommandName == "publicar")
                {
                    lblTitulo_publicar.Text = "Publicar Liquidación";
                    Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                    txtAnio_3.Text = oLiq.anio.ToString();
                    ddlTipo_liq_3.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                    ddlTipo_liq_3_SelectedIndexChanged(ddlTipo_liq_2, e);
                    ddlNro_liq_3.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                    txtDes_liquidacion_3.Text = oLiq.des_liquidacion;
                    txtPeriodo_3.Text = oLiq.periodo;
                    chkPublicar_liquidacion.Checked = oLiq.publica;
                    //Session["opcion"] = 6;
                    uPanelCliente.Update();
                    lbtnmodalPublicarExtender.Show();
                }

                //if (e.CommandName == "salariofam")
                //{
                //    string usuario = Convert.ToString(Session["usuario"]);
                //    string message = string.Empty;
                //    try
                //    {
                //        BLL.Concepto_Liq_x_EmpB.ActualizarSalarioFamiliar(usuario);
                //        message = "Salario Familiar Actualizado...!";

                //    }
                //    catch (Exception ex)
                //    {
                //        message = "Hubo Problemas con la Actualizacion del Salario Familiar...!, Error: " + ex.Message;
                //        throw;
                //    }
                //    msjConfirmar.InnerHtml = message;
                //    divConfirma.Visible = true;
                //    PanelInfomacion.Update();
                //}

            }
            catch (Exception ex)
            {
                txtError.InnerText = ex.Message;
                divError.Visible = true;
            }
        }

        protected void gvLiquidaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFCC80'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                Entities.Liquidacion oLiq = (Entities.Liquidacion)e.Row.DataItem;
                Label lblAnio = (Label)e.Row.FindControl("lblAnio");
                Label lblTipo_liq = (Label)e.Row.FindControl("lblTipo_liq");
                Label lblNro_liquidacion = (Label)e.Row.FindControl("lblNro_liquidacion");
                Label lblDes_liquidacion = (Label)e.Row.FindControl("lblDes_liquidacion");
                CheckBox chkAguinaldo = (CheckBox)e.Row.FindControl("chkAguinaldo");
                Label lblPeriodo = (Label)e.Row.FindControl("lblPeriodo");
                Label lblSemestre = (Label)e.Row.FindControl("lblSemestre");
                Label lblFecha_pago = (Label)e.Row.FindControl("lblFecha_pago");
                //CheckBox chkPublicar = (CheckBox)e.Row.FindControl("chkPublicar");
                //CheckBox chkCerrada = (CheckBox)e.Row.FindControl("chkCerrada");
                //
                lblAnio.Text = oLiq.anio.ToString();
                lblTipo_liq.Text = oLiq.des_tipo_liq.ToString();
                lblNro_liquidacion.Text = oLiq.nro_liquidacion.ToString();
                lblDes_liquidacion.Text = oLiq.des_liquidacion.ToString();
                chkAguinaldo.Checked = Convert.ToBoolean(oLiq.aguinaldo);
                lblPeriodo.Text = oLiq.periodo.ToString();
                lblSemestre.Text = oLiq.semestre.ToString();
                lblFecha_pago.Text = oLiq.fecha_pago.ToString();
                //chkPublicar.Checked = Convert.ToBoolean(oLiq.publica);
                //chkCerrada.Checked = Convert.ToBoolean(oLiq.cerrada);
            }

        }

        protected void gvLiquidaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLiquidaciones.PageIndex = e.NewPageIndex;
            intPage = gvLiquidaciones.PageIndex;
            BindList();
        }

        protected void BindList()
        {
            CargarGrilla();
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {

        }

        protected void btnCrearLiq_Click(object sender, EventArgs e)
        {

            int op = 0;
            op = (Convert.ToInt32(Session["opcion"]));

            switch (op)
            {
                case 1:
                    NuevaLiquidacion();
                    break;
                case 2:
                    ModificaLiquidacion();
                    break;
                case 3:
                    Liquidar();
                    break;
                default:
                    break;
            }
        }

        protected void NuevaLiquidacion()
        {
            Entities.Liquidacion oLiq = new Entities.Liquidacion();
            string message = string.Empty;
            oLiq.anio = Convert.ToInt32(txtAnio.Text);
            oLiq.cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            oLiq.nro_liquidacion = 0;
            oLiq.des_liquidacion = txtDes_liquidacion.Text;
            oLiq.periodo = txtPeriodo.Text;
            oLiq.aguinaldo = chkAguinaldo.Checked;
            oLiq.cod_semestre = Convert.ToInt32(ddLSemestre.SelectedValue);
            oLiq.fecha_alta = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyyy");
            oLiq.per_ult_dep = txtPer_ult_dep.Text;
            oLiq.fecha_ult_dep = txtFecha_ult_deposito.Text;
            oLiq.fecha_pago = txtFecha_pago.Text;
            oLiq.fecha_liquidacion = txtFecha_liquidacion.Text;
            oLiq.usuario = Convert.ToString(Session["usuario"]);
            oLiq.cod_banco_ult_dep = Convert.ToInt32(ddlBanco.SelectedValue);
            oLiq.publica = false;
            oLiq.prueba = Convert.ToInt16(chkPrueba.Checked);
            try
            {
                string usuario = Convert.ToString(Session["usuario"]);
                BLL.LiquidacionesB.insert(oLiq);
                lbtnNuevo_ModalPopupExtender.Hide();
                message = "Alta de los Datos de la Liquidación Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
                PanelInfomacion.Update();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                lbtnNuevo_ModalPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
                throw ex;
            }
        }

        protected void ModificaLiquidacion()
        {
            Entities.Liquidacion oLiq = new Entities.Liquidacion();
            string message = string.Empty;
            oLiq.anio = Convert.ToInt32(txtAnio.Text);
            oLiq.cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            oLiq.nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            oLiq.des_liquidacion = txtDes_liquidacion.Text;
            oLiq.periodo = txtPeriodo.Text;
            oLiq.aguinaldo = chkAguinaldo.Checked;
            oLiq.cod_semestre = Convert.ToInt32(ddLSemestre.SelectedValue);
            oLiq.fecha_alta = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyyy");
            oLiq.per_ult_dep = txtPer_ult_dep.Text;
            oLiq.fecha_ult_dep = txtFecha_ult_deposito.Text;
            oLiq.fecha_pago = txtFecha_pago.Text;
            oLiq.fecha_liquidacion = txtFecha_liquidacion.Text;
            oLiq.usuario = Convert.ToString(Session["usuario"]);
            oLiq.cod_banco_ult_dep = Convert.ToInt32(ddlBanco.SelectedValue);
            oLiq.prueba = Convert.ToInt16(chkPrueba.Checked);
            try
            {
                BLL.LiquidacionesB.update(oLiq);
                lbtnNuevo_ModalPopupExtender.Hide();
                message = "Alta de los Datos de la Liquidacion Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
                PanelInfomacion.Update();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                lbtnNuevo_ModalPopupExtender.Hide();
                if (string.IsNullOrEmpty(message))
                    txtError.InnerText = ex.Message;
                else
                    txtError.InnerText = message;
                divError.Visible = true;
                throw ex;
            }
        }

        protected void Liquidar()
        {
            Entities.Liquidacion oLiq = new Entities.Liquidacion();
            string message = string.Empty;
            oLiq.anio = Convert.ToInt32(txtAnio.Text);
            oLiq.cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            oLiq.nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            oLiq.des_liquidacion = txtDes_liquidacion.Text;
            oLiq.periodo = txtPeriodo.Text;
            oLiq.aguinaldo = chkAguinaldo.Checked;
            oLiq.cod_semestre = Convert.ToInt32(ddLSemestre.SelectedValue);
            oLiq.fecha_alta = DateTime.Now.ToString(); //DateTime.Today.ToString("dd/MM/yyyyy");
            oLiq.per_ult_dep = txtPer_ult_dep.Text;
            oLiq.fecha_ult_dep = txtFecha_ult_deposito.Text;
            oLiq.fecha_pago = txtFecha_pago.Text;
            oLiq.usuario = Convert.ToString(Session["usuario"]);
            oLiq.fecha_liquidacion = txtFecha_liquidacion.Text;
            try
            {
                if (oLiq.aguinaldo == false)
                    BLL.LiquidacionesB.Liquidar(oLiq.anio, oLiq.cod_tipo_liq, oLiq.nro_liquidacion, oLiq, chkSalarioFam.Checked);
                else
                    BLL.LiquidacionesB.Aguinaldo(oLiq.anio, oLiq.cod_tipo_liq, oLiq.nro_liquidacion, oLiq.cod_semestre, oLiq);
                lbtnNuevo_ModalPopupExtender.Hide();
                message = "La Liquidacion Termino Ok ...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                lbtnNuevo_ModalPopupExtender.Hide();
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

        protected void lbtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void btnCloseTraspaso_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnBuscarConcepto_ServerClick(object sender, EventArgs e)
        {
            //grdConceptos.DataSource = null;
            //grdConceptos.DataBind();
            //txtInput.Focus();
            //popUpConcepto.Show();
        }

        protected void txtCod_concepto_TextChanged(object sender, EventArgs e)
        {
            Entities.Conceptos_Liq oConcepto = new Entities.Conceptos_Liq();
            int cod;
            try
            {
                cod = int.Parse(txtCod_concepto.Text);
                oConcepto = BLL.Concepto_liqB.GetByPk(cod);
                if (oConcepto != null)
                {
                    txtCod_concepto.Text = oConcepto.cod_concepto_liq.ToString();
                    txtConcepto.Text = oConcepto.des_concepto_liq.ToString();
                    txtConcepto.Focus();
                    if (txtAnio_1.Text.Trim() != string.Empty &&
                      Convert.ToString(ddlTipo_liq_1.SelectedValue) != string.Empty &&
                      Convert.ToString(ddlNro_liq_1.SelectedValue) != string.Empty)
                    {
                        //FillDetalle();
                    }
                }
                lbtnmodalTraspasoExtender.Show();
            }
            catch (Exception ex)
            {
                txtCod_concepto.Text = string.Empty;
                txtConcepto.Text = string.Empty;
                //uPanelCliente.Update();
                throw ex;
            }
        }

        protected void txtAnio_1_TextChanged(object sender, EventArgs e)
        {
            ddlTipo_liq_1.SelectedIndex = 0;
            ddlNro_liq_1.Items.Clear();
            uPanelCliente.Update();
            lbtnmodalTraspasoExtender.Show();
        }

        protected void btnCancelaTraspaso_Click(object sender, EventArgs e)
        {
            lbtnmodalTraspasoExtender.Hide();
        }

        protected void btnAceptaTraspaso_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            int op = 0;
            int anio = Convert.ToInt32(txtAnio_1.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_1.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq_1.SelectedValue);
            int cod_concepto_liq = Convert.ToInt32(txtCod_concepto.Text);

            op = (Convert.ToInt32(Session["opcion"]));
            try
            {
                if (op == 4)
                {
                    BLL.ParxDetLiqxEmpB.Traspaso_Concepto(Convert.ToInt32(hAño.Value),
                      Convert.ToInt32(hCod_tipo_liq.Value),
                      Convert.ToInt32(hNro_liq.Value),
                      anio, cod_tipo_liq, nro_liquidacion, cod_concepto_liq);
                }

                message = "El Concepto " + txtCod_concepto.Text + " se cargo con exito...";
                msjTraspaso.InnerHtml = message;
                divMsjTraspaso.Visible = true;
                lbtnmodalTraspasoExtender.Show();
                uPanelCliente.Update();
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(message))
                    msjTraspaso.InnerText = ex.Message;
                else
                    msjTraspaso.InnerText = message;
                divMsjTraspaso.Visible = true;
                lbtnmodalTraspasoExtender.Show();
                uPanelCliente.Update();
                //throw ex;
            }
        }

        protected void btnCloseAsistenciaPuntualidad_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnCancelaAsistencia_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmaAsistencia_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            int op = 0;
            int anio = Convert.ToInt32(txtAnio_2.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_2.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq_2.SelectedValue);

            op = (Convert.ToInt32(Session["opcion"]));
            try
            {
                if (op == 5)
                {
                    message = "No ha Selecciono ninguna opcion...";
                    if (chkAsistencia.Checked == true)
                    {
                        BLL.ParxDetLiqxEmpB.Asistencia(anio, cod_tipo_liq, nro_liquidacion);
                        message = "La Asistencia se cargo con exito...";
                    }
                    if (chkPuntualidad.Checked == true)
                    {
                        BLL.ParxDetLiqxEmpB.Puntualidad(anio, cod_tipo_liq, nro_liquidacion);
                        message = "La Puntualidad se cargo con exito...";
                    }
                    if (chkDias.Checked == true)
                    {
                        BLL.ParxDetLiqxEmpB.DiasTrabajados(anio, cod_tipo_liq, nro_liquidacion, 30);
                        message = "Las Dias de Trabajo de c/legajo se cargo con exito...";
                    }
                    if (chkDiasAguinaldo.Checked)
                    {
                        BLL.ParxDetLiqxEmpB.DiasTrabajados(anio, cod_tipo_liq, nro_liquidacion, 180);
                        message = "Las Dias de Aguinaldo de c/legajo se cargo con exito...";
                    }
                }
                msjAsistencia.InnerHtml = message;
                divMsjAsistencia.Visible = true;
                lbtnmodalAsistenciaExtender.Show();
                uPanelCliente.Update();
            }
            catch (Exception ex)
            {
                msjAsistencia.InnerText = ex.Message;
                divMsjAsistencia.Visible = true;
                lbtnmodalAsistenciaExtender.Show();
                uPanelCliente.Update();
                //throw ex;
            }
        }

        protected void ddlTipo_liq_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int anio = Convert.ToInt32(txtAnio_3.Text);
                int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_3.SelectedValue);
                if (anio != 0)
                {
                    ddlNro_liq_3.Items.Clear();
                    ddlNro_liq_3.DataTextField = "des_liquidacion";
                    ddlNro_liq_3.DataValueField = "nro_liquidacion";
                    ddlNro_liq_3.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
                    ddlNro_liq_3.DataBind();
                }
                uPanelCliente.Update();
                lbtnmodalPublicarExtender.Show();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            txtAnio_3.Focus();
        }

        protected void btnCancelaPublicar_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmaPublicar_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            int op = 0;
            int anio = Convert.ToInt32(txtAnio_3.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq_3.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq_3.SelectedValue);

            try
            {
                message = "No ha Selecciono ninguna opcion...";
                BLL.LiquidacionesB.Publicar_liquidacion(anio, cod_tipo_liq, nro_liquidacion,
                    Convert.ToString(Session["usuario"]), "Modifica campo Publicar",
                    chkPublicar_liquidacion.Checked);
                message = "Se Realizo la Modificacion con éxito...";
                msjPublicar.InnerHtml = message;
                divMsjPublicar.Visible = true;
                lbtnmodalPublicarExtender.Show();
                uPanelCliente.Update();

            }
            catch (Exception ex)
            {
                msjPublicar.InnerText = ex.Message;
                divMsjPublicar.Visible = true;
                lbtnmodalPublicarExtender.Show();
                uPanelCliente.Update();
                //throw ex;
            }

        }

        protected void btnClosePublicar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lbtnPublicar_liq_Click(object sender, EventArgs e)
        {
            //popupActualizarMontos.Show();
            foreach (GridViewRow item in gvLiquidaciones.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPublica = (CheckBox)item.FindControl("chkPublica");
                    if (chkPublica != null)
                        chkPublica.Enabled = true;
                }
            }
            divActualiza.Visible = false;
            divAcepta.Visible = true;
        }

        protected void btnAceptarPublicar_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow item in gvLiquidaciones.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPublica = (CheckBox)item.FindControl("chkPublica");
                    if (chkPublica != null)
                    {
                        int anio = int.Parse(gvLiquidaciones.DataKeys[item.RowIndex].Values["anio"].ToString());
                        int cod_tipo_liq = int.Parse(gvLiquidaciones.DataKeys[item.RowIndex].Values["cod_tipo_liq"].ToString());
                        int nro_liquidacion = int.Parse(gvLiquidaciones.DataKeys[item.RowIndex].Values["nro_liquidacion"].ToString());
                        //chkPublica.Enabled = false;
                        BLL.LiquidacionesB.Publicar_liquidacion(anio, cod_tipo_liq, nro_liquidacion,
                          Convert.ToString(Session["usuario"]), "Modifica el campo Publicar", chkPublica.Checked);
                    }
                }
            }
            divActualiza.Visible = true;
            divAcepta.Visible = false;
            CargarGrilla();

        }

        protected void btnCancelarPublicar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in gvLiquidaciones.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPublica = (CheckBox)item.FindControl("chkPublica");
                    if (chkPublica.Checked)
                        chkPublica.Enabled = false;
                }
            }
            divActualiza.Visible = true;
            divAcepta.Visible = false;
            CargarGrilla();

        }

        protected void btnCierrarLiq_Click(object sender, EventArgs e)
        {
            //
            string message = string.Empty;
            int anio = Convert.ToInt32(HFAnio1.Value);
            int cod_tipo_liq = Convert.ToInt32(HFCod_tipo_liq1.Value);
            int nro_liquidacion = Convert.ToInt32(HFNro_liquidacion1.Value);
            string usuario_cierre = Convert.ToString(Session["usuario"]);
            int currentPage = gvLiquidaciones.PageIndex;
            string operacion = "Cierre de Liquidacion";
            try
            {
                message = "";
                BLL.LiquidacionesB.Cerrar_liquidacion(anio, cod_tipo_liq, nro_liquidacion, usuario_cierre, operacion,
                    chkCerrada.Checked, txtFecha_cierre.Text);
                message = "Se Realizo el Cierre de Liquidacion con éxito...";
                msjConfirmar.InnerHtml = message;
                divConfirma.Visible = true;
                //PanelInfomacion.Update();
                //uPanelCliente.Update();
                CargarGrilla();
                gvLiquidaciones.PageIndex = currentPage;
            }
            catch (Exception ex)
            {
                msjConfirmar.InnerHtml = ex.ToString();
                divConfirma.Visible = true;
                PanelInfomacion.Update();
                //throw ex;
            }
        }
    }
}