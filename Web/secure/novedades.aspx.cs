using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;

namespace web.secure
{
    public partial class novedades : System.Web.UI.Page
    {

        string operacion = "";
        List<Entities.DetalleCptoEmp> lstDetalle = new List<DetalleCptoEmp>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../index.aspx");

            lbtnAgregarConceptos.Visible = false;
            lbtnConfirma.Visible = true;

            if (Request.QueryString["op"] != null)
                operacion = Convert.ToString(Request.QueryString["op"]);
            else
                operacion = "";

            if (!Page.IsPostBack)
            {
                lblCantReg.Text = "  0";
                lblTotal.Text = "  $ 0";
                txtAnio.Text = DateTime.Now.Year.ToString();
                CargarCombos();
                Session.Add("Detalle", lstDetalle);
                Session.Add("Total", 0);
                Session.Add("opcion", 0);
                Session.Add("index", 0);
                txtAnio.Focus();

            }


            string var = Request.Params["__EVENTARGUMENT"];

            if (var == "Error")
                divError.Visible = false;

            if (var == "Informacion")
                divInformacion.Visible = false;

            if (var == "Alerta")
                divMSJDetalleLegajos.Visible = false;

        }

        protected void CargarCombos()
        {
            txtTipo_liq.DataTextField = "des_tipo_liq";
            txtTipo_liq.DataValueField = "cod_tipo_liq";
            txtTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            txtTipo_liq.DataBind();
        }

        protected void txtTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedValue);
            txtNro_liq.Items.Clear();
            txtNro_liq.DataTextField = "des_liquidacion";
            txtNro_liq.DataValueField = "nro_liquidacion";
            txtNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
            txtNro_liq.DataBind();
            txtNro_liq.Focus();
        }

        protected void cmdCarga_Click(object sender, EventArgs e)
        {

        }

        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargar_conceptos.aspx");
        }

        protected void btnBuscarConcepto_ServerClick(object sender, EventArgs e)
        {
            grdConceptos.DataSource = null;
            grdConceptos.DataBind();
            txtInput.Focus();
            popUpConcepto.Show();
        }

        protected void FillDetalle()
        {
            FillDetalleGrid();
            decimal total = 0;
            int cantidad = 0;
            foreach (DetalleCptoEmp det in lstDetalle)
            {
                total += det.valor_parametro;
                cantidad += 1;
            }
            //lblTotal.InnerText = "TOTAL: $ " + total.ToString();
            //lblCantidad.InnerText = "CANTIDAD : " + cantidad.ToString();
            lblCantReg.Text = string.Format("   {0:N}", cantidad);
            lblTotal.Text = string.Format("$   {0:N}", total);
            Session["Total"] = total;
            //popUpConcepto.Hide();
        }

        protected void FillDetalleGrid()
        {
            lstDetalle = BLL.Concepto_liqB.FindDetalleCptoEmp(Convert.ToInt32(txtAnio.Text),
              Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text), Convert.ToInt32(txtCod_concepto.Text));
            Session.Add("Detalle", lstDetalle);
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            PanelDetalle.Update();
        }



        private List<Entities.DetalleCptoEmp> leerGrilla()
        {
            List<Entities.DetalleCptoEmp> lst = new List<Entities.DetalleCptoEmp>();
            for (int i = 0; i < gvDetalle.Rows.Count; i++)
            {
                GridViewRow row = gvDetalle.Rows[i];
                Entities.DetalleCptoEmp obj = new Entities.DetalleCptoEmp();
                obj.legajo = int.Parse(gvDetalle.DataKeys[i].Values["legajo"].ToString());
                obj.cod_concepto_liq = int.Parse(gvDetalle.DataKeys[i].Values["cod_concepto_liq"].ToString());
                obj.nro_parametro = int.Parse(gvDetalle.DataKeys[i].Values["nro_parametro"].ToString());
                lst.Add(obj);
            }
            //txtTot.Text = tot.ToString();
            return lst;
        }


        protected void cmdCancelar_Click(object sender, EventArgs e)
        {
            popUpConcepto.Hide();
        }


        protected void txtNro_liq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmdBuscar_ServerClick(object sender, EventArgs e)
        {
            List<Conceptos_Liq> lstConcepto_liq = BLL.Concepto_liqB.findConcepto_liqByDescripcion(txtInput.Value);
            grdConceptos.DataSource = lstConcepto_liq;
            grdConceptos.DataBind();

        }

        protected void grdConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtCod_concepto.Text = grdConceptos.DataKeys[i].Values["cod_concepto"].ToString();
                txtConcepto.Text = grdConceptos.DataKeys[i].Values["des_concepto_liq"].ToString();
                //UpdatePanelDatos.Update();

                if (txtAnio.Text.Trim() != string.Empty && txtTipo_liq.SelectedValue.ToString() != string.Empty &&
                  txtNro_liq.SelectedValue.ToString() != string.Empty)
                {
                    FillDetalle();
                }
                popUpConcepto.Hide();
            }
        }


        protected void grdConceptos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn;

                imgbtn = (ImageButton)e.Row.FindControl("imgbSeleccionar");
                if (imgbtn != null)
                {
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void btnAddDetalle_ServerClick(object sender, EventArgs e)
        {

        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<Entities.DetalleCptoEmp> lstDetalle = (List<Entities.DetalleCptoEmp>)Session["Detalle"];

            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index + (gvDetalle.PageSize * gvDetalle.PageIndex);
            int legajo = 0;

            if (e.CommandName == "Page")
                return;

            legajo = Convert.ToInt32(gvDetalle.DataKeys[index].Values["legajo"]);


            if (e.CommandName == "deleterow")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                decimal total = Convert.ToDecimal(Session["Total"]);

                if (!lstDetalle.Exists(ent => ent.legajo == legajo))
                    total -= lstDetalle[index].valor_parametro;
                //lblTotal.InnerText = "TOTAL: $ " + total.ToString();

                lblCantReg.Text = string.Format("   {0:D}", lstDetalle.Count);
                lblTotal.Text = string.Format("$   {0:N}", total);

                //lstDetalle.RemoveAt(index);
                //lstDetalle.RemoveAt(indicePaginado);
                lstDetalle.RemoveAll(ent => ent.legajo == legajo);
                Session["Detalle"] = lstDetalle;
                Session["Total"] = total;
                Session["opcion"] = 1;
                fillDetalle(lstDetalle);
            }
            if (e.CommandName == "editrow")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //txtLegajo.Text = lstDetalle[index].legajo.ToString();
                //txtNombre.Text = lstDetalle[index].nombre;
                //txtValor.Text = lstDetalle[index].valor_parametro.ToString();
                //Session["opcion"] = 1;
                //Session["index"] = index;
                Entities.DetalleCptoEmp oVal = new Entities.DetalleCptoEmp();
                //oVal = BLL.Concepto_liqB.FindDetalleCptoEmpByPk(Convert.ToInt32(txtAnio.Text), Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text), Convert.ToInt32(txtCod_concepto.Text),legajo);
                oVal = lstDetalle.Find(ent => ent.legajo == legajo);
                //txtLegajo.Text = lstDetalle[indicePaginado].legajo.ToString();
                //txtNombre.Text = lstDetalle[indicePaginado].nombre;
                //txtValor.Text = lstDetalle[indicePaginado].valor_parametro.ToString();
                txtLegajo.Text = oVal.legajo.ToString();
                txtNombre.Text = oVal.nombre;
                txtValor.Text = oVal.valor_parametro.ToString();
                Session["opcion"] = 1;
                Session["index"] = indicePaginado;
                UpdatePanelLegajo.Update();
                popUpDetalleLegajos.Show();
            }
            Session["Detalle"] = lstDetalle;
            //btnConfirma_Click(sender, e);
            //Session["opcion"] = 0;
            //Esto no va andar de cargar y actualizar la base
            //cada vez que modifique o elimine
        }

        protected void fillDetalle(List<Entities.DetalleCptoEmp> lstDetalle)
        {
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            PanelDetalle.Update();
        }


        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //  e.Row.BackColor = System.Drawing.Color.FromArgb(0, 205, 248, 241);
            //  e.Row.Height = Unit.Pixel(34);
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn;
                imgbtn = (ImageButton)e.Row.FindControl("imgbDelete");
                if (imgbtn != null)
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                ImageButton imgbtnEdit;
                imgbtnEdit = (ImageButton)e.Row.FindControl("imgbEdit");
                if (imgbtnEdit != null)
                    imgbtnEdit.CommandArgument = e.Row.RowIndex.ToString();
            }

        }


        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            List<Entities.DetalleCptoEmp> lstDetalle;
            lstDetalle = (List<Entities.DetalleCptoEmp>)Session["Detalle"];
            int op = 0;

            if (Session["opcion"] != null)
                op = (Convert.ToInt32(Session["opcion"]) == 0 ? 0 : 1);
            try
            {

                if (txtAnio.Text.Length == 0 || Convert.ToInt32(txtTipo_liq.SelectedValue) == 0 || Convert.ToInt32(txtNro_liq.SelectedValue) == 0)
                {
                    divError.Visible = true;
                    msjError.InnerHtml = "Problemas con el Alta, Ingrese nuevamente el Año, Tipo de liquidacion y el Mes de la Liquidacion!!!";
                    PanelError.Update();
                    return;
                }

                if (lstDetalle.Count == 0)
                {

                    divError.Visible = true;
                    msjError.InnerHtml = "Debe agregar al menos un Item/s al detalle!!!";
                    PanelError.Update();
                    return;
                }

                if (op == 0)
                {

                    BLL.ParxDetLiqxEmpB.UpdateParxDetLiqxEmp(Convert.ToInt32(txtAnio.Text),
                      Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text),
                      Convert.ToInt32(txtCod_concepto.Text), lstDetalle, Convert.ToString(Session["usuario"]));

                    divInformacion.Visible = true;
                    msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                    PanelInfomacion.Update();


                    lbtnAgregarConceptos.Visible = true;
                    lbtnConfirma.Visible = false;
                }
                else
                {
                    BLL.ParxDetLiqxEmpB.UpdateParxDetLiqxEmp(Convert.ToInt32(txtAnio.Text),
                   Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text),
                   Convert.ToInt32(txtCod_concepto.Text), lstDetalle, Convert.ToString(Session["usuario"]));

                    divInformacion.Visible = true;
                    msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                    PanelInfomacion.Update();

                    lbtnAgregarConceptos.Visible = true;
                    lbtnConfirma.Visible = false;

                }
                FillDetalle();
                //txtOP.InnerText = oOrden.nroOrden.ToString();
            }
            catch
            {
                divError.Visible = true;
                msjError.InnerHtml = "Problemas con el Alta de los Novedades, Revise la Grilla, revise datos del Legajo!!!";
                PanelError.Update();
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnAddDetalle_ServerClick1(object sender, EventArgs e)
        {
            popUpDetalleLegajos.Show();
        }

        protected void txtLegajo_TextChanged(object sender, EventArgs e)
        {
            //
            Empleado oEmp = new Empleado();
            int cod;
            try
            {
                cod = int.Parse(txtLegajo.Text);
                oEmp = BLL.EmpleadoB.GetByPk(cod);
                if (oEmp != null)
                {
                    txtLegajo.Text = oEmp.legajo.ToString();
                    txtNombre.Text = oEmp.nombre;
                    txtValor.Focus();
                }
                else
                    txtLegajo.Focus();
            }
            catch (Exception)
            {
                txtLegajo.Text = string.Empty;
                txtNombre.Text = string.Empty;
                UpdatePanelLegajo.Update();
                txtValor.Focus();
            }
        }

        protected void txtCod_concepto_TextChanged(object sender, EventArgs e)
        {
            //
            Conceptos_Liq oConcepto = new Conceptos_Liq();
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
                    if (txtAnio.Text.Trim() != string.Empty &&
                      txtTipo_liq.SelectedValue.ToString() != string.Empty &&
                      txtNro_liq.SelectedValue.ToString() != string.Empty)
                    {
                        FillDetalle();
                    }
                }

            }
            catch (Exception ex)
            {
                txtCod_concepto.Text = string.Empty;
                txtConcepto.Text = string.Empty;
                UpdatePanelLegajo.Update();
                throw ex;
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            /*if (txtValor.Text.Trim() == string.Empty ||
               txtLegajo.Text.Trim() == string.Empty || txtNombre.Text == string.Empty)*/
            if (txtAnio.Text.Length == 0 || Convert.ToInt32(txtTipo_liq.SelectedValue) == 0 || Convert.ToInt32(txtNro_liq.SelectedValue) == 0)
            {
                //string script =
                //@"<script type='text/javascript'> apprise('Complete los datos Solicitados',{'animate':true}); </script>";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                return;
            }
            else
            {
                lstDetalle = (List<Entities.DetalleCptoEmp>)Session["Detalle"];
                Entities.DetalleCptoEmp detalle = new DetalleCptoEmp();
                detalle.legajo = Convert.ToInt16(txtLegajo.Text);
                detalle.nombre = txtNombre.Text;
                detalle.cod_concepto_liq = Convert.ToInt16(txtCod_concepto.Text);
                detalle.concepto = txtConcepto.Text;
                detalle.nro_parametro = 0;
                detalle.valor_parametro = Convert.ToDecimal(txtValor.Text);
                detalle.observacion = txtObs.Text;

                if ((int)Session["opcion"] == 0)
                {
                    if (lstDetalle.Count > 0)
                    {
                        if (!lstDetalle.Exists(ent => ent.cod_concepto_liq == detalle.cod_concepto_liq && ent.legajo == detalle.legajo))
                            lstDetalle.Add(detalle);
                        else
                        {
                            //MENSAJE DE QUE YA EXISTE
                            divMSJDetalleLegajos.Visible = true;
                            msjDetalleLegajo.InnerHtml = "Ya Existe este Concepto para este Legajo:" + detalle.legajo.ToString();
                            UpdatePanelLegajo.Update();
                        }
                    }
                    else
                        lstDetalle.Add(detalle);
                }
                else
                {
                    lstDetalle[(int)Session["index"]].legajo = detalle.legajo;
                    lstDetalle[(int)Session["index"]].cod_concepto_liq = detalle.cod_concepto_liq;
                    lstDetalle[(int)Session["index"]].concepto = detalle.concepto;
                    lstDetalle[(int)Session["index"]].nro_parametro = detalle.nro_parametro;
                    lstDetalle[(int)Session["index"]].valor_parametro = detalle.valor_parametro;
                    lstDetalle[(int)Session["index"]].observacion = detalle.observacion;
                }
                decimal total = 0;
                foreach (DetalleCptoEmp det in lstDetalle)
                {
                    total += det.valor_parametro;
                }
                Session["Detalle"] = lstDetalle;
                Session["Total"] = total;
                Session["opcion"] = 0;

                //lblTotal.InnerText = "TOTAL: $ " + total.ToString();
                lblCantReg.Text = string.Format("   {0:N}", lstDetalle.Count);
                lblTotal.Text = string.Format("  $ {0:N}", total);

                fillDetalle(lstDetalle);
                popUpDetalleLegajos.Hide();
                btnCargar_Legajos_ServerClick(null, null);
            }
        }


        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
            popUpDetalleLegajos.Hide();
        }


        protected void btnCargar_Legajos_ServerClick(object sender, EventArgs e)
        {
            if (txtAnio.Text.Length == 0 || Convert.ToInt32(txtTipo_liq.SelectedValue) == 0 || Convert.ToInt32(txtNro_liq.SelectedValue) == 0)
            {
                divError.Visible = true;
                msjError.InnerHtml = "Ingrese el Año, Tipo y Mes de la Liquidacion!!!";
                PanelError.Update();
                return;
            }
            Session["opcion"] = 0;
            operacion = "carga";
            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtValor.Text = "";
            txtObs.Text = string.Empty;
            UpdatePanelLegajo.Update();
            txtLegajo.Focus();
            popUpDetalleLegajos.Show();
        }


        protected void btnAgregarConceptos_Click(object sender, EventArgs e)
        {
            txtConcepto.Text = "";
            txtCod_concepto.Text = "";
            //UpdatePanelDatos.Update();

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();
            PanelDetalle.Update();

            Session.Add("Detalle", null);
            Session.Add("Total", 0);
            Session.Add("opcion", 0);
            Session.Add("index", 0);

            divInformacion.Visible = false;
            divError.Visible = false;
            txtCod_concepto.Focus();

        }



        protected void btnExporCtaCte_Click(object sender, EventArgs e)
        {
            GridView gv = new GridView();
            BindGrid(gv);
            ExportToExcel("Novedades", gv);
        }

        protected void BindGrid(GridView gv)
        {
            lstDetalle = BLL.Concepto_liqB.FindDetalleCptoEmp(Convert.ToInt32(txtAnio.Text),
              Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text), Convert.ToInt32(txtCod_concepto.Text));
            Session.Add("Detalle", lstDetalle);
            gv.DataSource = lstDetalle;
            gv.DataBind();
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



        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargar_conceptos.aspx");
        }

        protected void btnCloseModal_ServerClick(object sender, EventArgs e)
        {
            popUpDetalleLegajos.Hide();
        }

        protected void btnCloseModalConcepto_ServerClick(object sender, EventArgs e)
        {
            popUpConcepto.Hide();
        }

        protected void lbtnEliminaTodos_Click(object sender, EventArgs e)
        {
            List<Entities.DetalleCptoEmp> lstDetalle;
            lstDetalle = (List<Entities.DetalleCptoEmp>)Session["Detalle"];

            int op = 3;

            try
            {
                if (txtAnio.Text.Length == 0 || Convert.ToInt32(txtTipo_liq.SelectedValue) == 0 || Convert.ToInt32(txtNro_liq.SelectedValue) == 0)
                {
                    divError.Visible = true;
                    msjError.InnerHtml = "Problemas con la Eliminación, Ingrese nuevamente el Año, Tipo de liquidacion y el Mes de la Liquidacion!!!";
                    PanelError.Update();
                    return;
                }

                if (lstDetalle.Count == 0)
                {

                    divError.Visible = true;
                    msjError.InnerHtml = "Debe agregar al menos un Item/s al detalle!!!";
                    PanelError.Update();
                    return;
                }

                if (op == 3)
                {
                    BLL.ParxDetLiqxEmpB.DeleteParxDetLiqxEmp(Convert.ToInt32(txtAnio.Text),
                      Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text),
                      Convert.ToInt32(txtCod_concepto.Text), lstDetalle, Convert.ToString(Session["usuario"]));

                    divInformacion.Visible = true;
                    msjInformacion.InnerHtml = "La Eliminación Masiva fue Procesada Correctamente!!!";
                    PanelInfomacion.Update();

                    lbtnAgregarConceptos.Visible = true;
                    lbtnConfirma.Visible = true;

                }
                FillDetalle();
                //txtOP.InnerText = oOrden.nroOrden.ToString();
            }
            catch
            {
                divError.Visible = true;
                msjError.InnerHtml = "Problemas con la Eliminación de los Novedades, Revise Datos!!!";
                PanelError.Update();
            }
        }

        protected void lbtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
            FillDetalleGrid();
            //intPage = gvDetalle.PageIndex;
            //BindList();
        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            Conceptos_Liq oConcepto = new Conceptos_Liq();
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
                    if (txtAnio.Text.Trim() != string.Empty &&
                      txtTipo_liq.SelectedValue.ToString() != string.Empty &&
                      txtNro_liq.SelectedValue.ToString() != string.Empty)
                    {
                        FillDetalle();
                    }
                }

            }
            catch (Exception ex)
            {
                txtCod_concepto.Text = string.Empty;
                txtConcepto.Text = string.Empty;
                UpdatePanelLegajo.Update();
                throw ex;
            }
        }
    }
}

