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
    public partial class concepto_emp : System.Web.UI.Page
    {

        int legajo = 0;
        string nombre = "";
        string operacion = "";
        List<Entities.ConceptoLiqxEmp> lstDetalle = new List<Entities.ConceptoLiqxEmp>();
        List<Entities.ConceptoLiqxEmp> lstDetalleBorrar = new List<Entities.ConceptoLiqxEmp>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            operacion = Convert.ToString(Request.QueryString["op"]);
            legajo = Convert.ToInt16(Request.QueryString["legajo"]);
            nombre = Convert.ToString(Request.QueryString["nombre"]);

            if (!Page.IsPostBack)
            {
                Session.Add("Detalle", lstDetalle);
                Session.Add("Borrar", lstDetalleBorrar);
                Session.Add("opcion", 0);
                Session.Add("index", 0);
                AsignarDatos();
            }


            string var = Request.Params["__EVENTARGUMENT"];

            if (var == "Error")
                divError.Visible = false;

            if (var == "Informacion")
                divInformacion.Visible = false;

            if (var == "Alerta")
                divMSJDetalleLegajos.Visible = false;

        }


        protected void AsignarDatos()
        {
            txtNombre.Text = nombre;
            txtLegajo.Text = legajo.ToString();
            FillDetalleGridConceptos();
        }

        protected void btnExporCtaCte_Click(object sender, EventArgs e)
        {
            ExportToExcel("Conceptos", gvDetalle);
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

        protected void FillDetalle()
        {
            FillDetalleGridConceptos();
            int cantidad = 0;
            foreach (Entities.ConceptoLiqxEmp det in lstDetalle)
            {
                cantidad += 1;
            }
            lblCantidad.InnerText = "CANTIDAD : " + cantidad.ToString();
            //popUpConcepto.Hide();
        }

        private void FillDetalleGridConceptos()
        {
            int cantidad = 0;
            lstDetalle = BLL.Concepto_Liq_x_EmpB.FillConceptoLiqxEmp(Convert.ToInt32(txtLegajo.Text));

            foreach (Entities.ConceptoLiqxEmp det in lstDetalle)
            {
                cantidad += 1;
            }
            lblCantidad.InnerText = "CANTIDAD : " + cantidad.ToString();
            Session.Add("Detalle", lstDetalle);
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            //if (gvDetalle.Rows.Count > 0)
            //{
            //    gvDetalle.UseAccessibleHeader = true;
            //    gvDetalle.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
            //PanelDetalle.Update();
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index + (gvDetalle.PageSize * gvDetalle.PageIndex);
            int legajo = 0;
            int cod_concepto_liq = 0;
            List<Entities.ConceptoLiqxEmp> lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];

            if (e.CommandName == "Page")
                return;
            legajo = Convert.ToInt32(gvDetalle.DataKeys[index].Values["legajo"]);
            cod_concepto_liq = Convert.ToInt32(gvDetalle.DataKeys[index].Values["cod_concepto_liq"]);

            if (e.CommandName == "editrow")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //List<Entities.ConceptoLiqxEmp> lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
                //txtCod_concepto_liq.Text = lstDetalle[index].cod_concepto_liq.ToString();
                //txtConcepto.Text = lstDetalle[index].des_concepto_liq;
                //txtFecha_vto.Text = lstDetalle[index].fecha_vto;
                //txtValor.Text = lstDetalle[index].valor_concepto_liq.ToString();
                ////txtValor.Text = lstDetalle[index].valor_parametro.ToString();
                //Session["opcion"] = 1;
                //Session["index"] = index;
                //UpdatePanelConcepto.Update();
                //modalPopupDetalle.Show();

                Entities.ConceptoLiqxEmp oVal = new Entities.ConceptoLiqxEmp();
                oVal = lstDetalle.Find(ent => ent.legajo == legajo && ent.cod_concepto_liq == cod_concepto_liq);
                txtCod_concepto_liq.Text = oVal.cod_concepto_liq.ToString();
                txtConcepto.Text = oVal.des_concepto_liq;
                txtFecha_vto.Text = oVal.fecha_vto.ToString();
                txtValor.Text = oVal.valor_concepto_liq.ToString();
                Session["opcion"] = 2;
                Session["index"] = indicePaginado;
                UpdatePanelConcepto.Update();
                modalPopupDetalle.Show();
            }

            if (e.CommandName == "deleterow")
            {
                ////int index = Convert.ToInt32(e.CommandArgument);
                ////List<Entities.ConceptoLiqxEmp> lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
                ////List<Entities.ConceptoLiqxEmp> lstDetalleDel = (List<Entities.ConceptoLiqxEmp>)Session["DetalleDel"];
                ////lstDetalleDel.Add(lstDetalle[index]);
                ////lstDetalle.RemoveAt(index);
                //lstDetalle.RemoveAll(ent => ent.legajo == legajo && ent.cod_concepto_liq == cod_concepto_liq);
                //Session["Detalle"] = lstDetalle;
                ////Session["DetalleDel"] = lstDetalleDel;
                //Session["opcion"] = 2;
                //gvDetalle.DataSource = lstDetalle;
                //gvDetalle.DataBind(); 
                ////lstDetalle.RemoveAll(ent => ent.legajo == legajo);
                ////Session["Detalle"] = lstDetalle;
                ////Session["Total"] = total;
                ////Session["opcion"] = 1;
                ////fillDetalle(lstDetalle);
                if (lstDetalle.Exists(ent => ent.legajo == legajo && ent.cod_concepto_liq == cod_concepto_liq))
                {
                    for (int i = 0; i < lstDetalle.Count; i++)
                    {
                        if (lstDetalle[i].legajo == legajo && lstDetalle[i].cod_concepto_liq==cod_concepto_liq)
                        {
                            lstDetalle[i].op = 3;
                            lstDetalleBorrar.Add(lstDetalle[i]);
                            break;
                        }
                    }
                    Session["Borrar"] = lstDetalleBorrar;
                    lstDetalle.RemoveAll(ent => ent.cod_concepto_liq == cod_concepto_liq);
                }
                Session["Detalle"] = lstDetalle;
                Session["opcion"] = 3;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
                //fillDetalle(lstDetalle);
            }
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
                LinkButton imgbtn;
                imgbtn = (LinkButton)e.Row.FindControl("btnDelete");
                if (imgbtn != null)
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                LinkButton imgbtnEdit;
                imgbtnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                if (imgbtnEdit != null)
                    imgbtnEdit.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("empleado.aspx?legajo={0}&nombre={1}&op={2}", txtLegajo.Text, txtNombre.Text, operacion));
        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
            modalPopupDetalle.Hide();
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            int op = 0;
            op = (int)Session["opcion"];
            if (txtValor.Text.Trim() == string.Empty ||
               txtLegajo.Text.Trim() == string.Empty || txtNombre.Text == string.Empty)
            {
                //string script =
                //@"<script type='text/javascript'> apprise('Complete los datos Solicitados',{'animate':true}); </script>";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                return;
            }
            else
            {
                lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
                Entities.ConceptoLiqxEmp detalle = new Entities.ConceptoLiqxEmp();
                //
                switch (op)
                {
                    case 1:
                        //lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
                        //Entities.ConceptoLiqxEmp detalle = new Entities.ConceptoLiqxEmp();
                        //detalle.legajo = Convert.ToInt16(txtLegajo.Text);
                        detalle.legajo = Convert.ToInt32(txtLegajo.Text);
                        detalle.cod_concepto_liq = Convert.ToInt16(txtCod_concepto_liq.Text);
                        detalle.des_concepto_liq = txtConcepto.Text;
                        detalle.valor_concepto_liq = Convert.ToDecimal(txtValor.Text);
                        detalle.fecha_vto = txtFecha_vto.Text;
                        detalle.fecha_alta_registro = DateTime.Today.ToShortDateString();
                        detalle.usuario = Convert.ToString(Session["usuario"]);
                        detalle.observacion = txtObs.Text;
                        detalle.op = 1;
                        if (lstDetalle.Count > 0)
                        {
                            if (!lstDetalle.Exists(ent => ent.cod_concepto_liq == detalle.cod_concepto_liq && ent.legajo == detalle.legajo))
                                lstDetalle.Add(detalle);
                            else
                            {
                                //MENSAJE DE QUE YA EXISTE
                                divMSJDetalleLegajos.Visible = true;
                                msjDetalleLegajo.InnerHtml = "Ya Existe el Concepto " + detalle.cod_concepto_liq.ToString() + " para este Legajo:" + detalle.legajo.ToString();
                                UpdatePanelConcepto.Update();
                            }
                        }
                        else
                            lstDetalle.Add(detalle);
                        break;
                    case 2:
                        lstDetalle[(int)Session["index"]].legajo = Convert.ToInt32(txtLegajo.Text);
                        lstDetalle[(int)Session["index"]].cod_concepto_liq = Convert.ToInt16(txtCod_concepto_liq.Text);
                        lstDetalle[(int)Session["index"]].des_concepto_liq = txtConcepto.Text;
                        lstDetalle[(int)Session["index"]].valor_concepto_liq = Convert.ToDecimal(txtValor.Text);
                        lstDetalle[(int)Session["index"]].fecha_vto = txtFecha_vto.Text;
                        lstDetalle[(int)Session["index"]].usuario = Convert.ToString(Session["usuario"]);
                        lstDetalle[(int)Session["index"]].observacion = txtObs.Text;
                        lstDetalle[(int)Session["index"]].op = 2;
                        break;
                    case 3:
                        lstDetalle[(int)Session["index"]].legajo = Convert.ToInt32(txtLegajo.Text);
                        lstDetalle[(int)Session["index"]].cod_concepto_liq = Convert.ToInt16(txtCod_concepto_liq.Text);
                        lstDetalle[(int)Session["index"]].des_concepto_liq = txtConcepto.Text;
                        lstDetalle[(int)Session["index"]].valor_concepto_liq = Convert.ToDecimal(txtValor.Text);
                        lstDetalle[(int)Session["index"]].fecha_vto = txtFecha_vto.Text;
                        lstDetalle[(int)Session["index"]].usuario = Convert.ToString(Session["usuario"]);
                        lstDetalle[(int)Session["index"]].observacion = txtObs.Text;
                        lstDetalle[(int)Session["index"]].op = 3;
                        break;
                    default:
                        break;
                }
                //
                Session["Detalle"] = lstDetalle;
                //Session["opcion"] = 0;
                fillDetalle(lstDetalle);
                modalPopupDetalle.Hide();
                txtObs.Text = string.Empty;
                btnCargar_concepto_ServerClick(null, null);
            }
        }

        protected void fillDetalle(List<Entities.ConceptoLiqxEmp> lstDetalle)
        {
            int cantidad = 0;
            foreach (Entities.ConceptoLiqxEmp det in lstDetalle)
            {
                cantidad += 1;
            }
            lblCantidad.InnerText = string.Format("Cantidad : {0}", cantidad.ToString());
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            PanelDetalle.Update();
        }

        protected void txtCod_concepto_liq_TextChanged(object sender, EventArgs e)
        {
            //
            Entities.Conceptos_Liq oConcepto = new Entities.Conceptos_Liq();
            int cod;
            try
            {
                cod = int.Parse(txtCod_concepto_liq.Text);
                oConcepto = BLL.Concepto_liqB.GetByPk(cod);
                if (oConcepto != null)
                {
                    txtCod_concepto_liq.Text = oConcepto.cod_concepto_liq.ToString();
                    txtConcepto.Text = oConcepto.des_concepto_liq;
                    txtValor.Focus();
                }
            }
            catch (Exception ex)
            {
                txtCod_concepto_liq.Text = string.Empty;
                txtConcepto.Text = string.Empty; ;
                UpdatePanelConcepto.Update();
                txtValor.Focus();
            }
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            List<Entities.ConceptoLiqxEmp> lstDetalle;
            lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
            lstDetalleBorrar = (List<Entities.ConceptoLiqxEmp>)Session["Borrar"];

            if (lstDetalle.Count == 0)
            {
                divError.Visible = true;
                msjError.InnerHtml = "Debe agregar al menos un Item/s al detalle!!!";
                PanelError.Update();
                return;
            }

            int op = 0;

            if (Session["opcion"] != null)
                op = Convert.ToInt32(Session["opcion"]);

            try
            {
                switch (op)
                {
                    case 1://New
                        {
                            BLL.Concepto_Liq_x_EmpB.UpdateConceptoxEmp(Convert.ToInt32(txtLegajo.Text), lstDetalle);
                            divInformacion.Visible = true;
                            msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                            PanelInfomacion.Update();
                            //btnAgregarConceptos.Visible = true;
                            //btnConfirma.Visible = false;
                            break;
                        }
                    case 2://Edit
                        {
                            BLL.Concepto_Liq_x_EmpB.UpdateConceptoxEmp(Convert.ToInt32(txtLegajo.Text), lstDetalle);
                            divInformacion.Visible = true;
                            msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                            PanelInfomacion.Update();
                            // btnAgregarConceptos.Visible = true;
                            break;
                        }
                    case 3://Delete
                        {
                            BLL.Concepto_Liq_x_EmpB.DeleteConceptoxEmp(Convert.ToInt32(txtLegajo.Text), lstDetalle, lstDetalleBorrar);
                            divInformacion.Visible = true;
                            msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                            PanelInfomacion.Update();
                            //btnAgregarConceptos.Visible = true;
                            break;
                        }

                    default:
                        break;
                }

                //txtOP.InnerText = oOrden.nroOrden.ToString();
            }
            catch
            {
                divError.Visible = true;
                msjError.InnerHtml = "Problemas con el Alta de los Novedades, Revise la Grilla, se cargo varias veces el mismo Concepto!!!";
                PanelError.Update();
            }
        }

        protected void lnkAgrega_conceptos_Click(object sender, EventArgs e)
        {
            operacion = "carga";
            Session["opcion"] = 1;
            txtCod_concepto_liq.Text = "";
            txtConcepto.Text = "";
            txtValor.Text = "";
            txtFecha_vto.Text = "";
            txtObs.Text = string.Empty;
            txtCod_concepto_liq.Focus();
            UpdatePanelConcepto.Update();
            modalPopupDetalle.Show();
        }

        protected void btnCargar_concepto_ServerClick(object sender, EventArgs e)
        {
            operacion = "carga";
            txtCod_concepto_liq.Text = "";
            txtConcepto.Text = "";
            txtValor.Text = "";
            txtFecha_vto.Text = "";
            txtCod_concepto_liq.Focus();
            UpdatePanelConcepto.Update();
            modalPopupDetalle.Show();
        }

        protected void btnCloseModal_ServerClick(object sender, EventArgs e)
        {
            modalPopupDetalle.Hide();
        }
    }
}


//protected void btnAceptar_ServerClick(object sender, EventArgs e)
//{
//    int op = 0;
//    op = (int)Session["opcion"];
//    if (txtValor.Text.Trim() == string.Empty ||
//       txtLegajo.Text.Trim() == string.Empty || txtNombre.Text == string.Empty)
//    {
//        //string script =
//        //@"<script type='text/javascript'> apprise('Complete los datos Solicitados',{'animate':true}); </script>";
//        //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
//        return;
//    }
//    else
//    {
//        lstDetalle = (List<Entities.ConceptoLiqxEmp>)Session["Detalle"];
//        Entities.ConceptoLiqxEmp detalle = new Entities.ConceptoLiqxEmp();
//        //detalle.legajo = Convert.ToInt16(txtLegajo.Text);
//        detalle.legajo = Convert.ToInt32(txtLegajo.Text);
//        detalle.cod_concepto_liq = Convert.ToInt16(txtCod_concepto_liq.Text);
//        detalle.des_concepto_liq = txtConcepto.Text;
//        detalle.valor_concepto_liq = Convert.ToDecimal(txtValor.Text);
//        detalle.fecha_vto = txtFecha_vto.Text;
//        detalle.fecha_alta_registro = DateTime.Today.ToShortDateString();
//        detalle.usuario = Convert.ToString(Session["usuario"]);
//        detalle.observaciones = txtObs.Text;

//        //if (Convert.ToInt32(Session["opcion"]) == 0)
//        //{
//        //    detalle.op = 1;
//        //    lstDetalle.Add(detalle);
//        //}
//        if ((int)Session["opcion"] == 0)
//        {
//            if (lstDetalle.Count > 0)
//            {
//                if (!lstDetalle.Exists(ent => ent.cod_concepto_liq == detalle.cod_concepto_liq && ent.legajo == detalle.legajo))
//                    lstDetalle.Add(detalle);
//                else
//                {
//                    //MENSAJE DE QUE YA EXISTE
//                    divMSJDetalleLegajos.Visible = true;
//                    msjDetalleLegajo.InnerHtml = "Ya Existe el Concepto " + detalle.cod_concepto_liq.ToString() + " para este Legajo:" + detalle.legajo.ToString();
//                    UpdatePanelConcepto.Update();
//                }
//            }
//            else
//                lstDetalle.Add(detalle);
//        }
//        else
//        {
//            lstDetalle[(int)Session["index"]].legajo = detalle.legajo;
//            lstDetalle[(int)Session["index"]].cod_concepto_liq = detalle.cod_concepto_liq;
//            lstDetalle[(int)Session["index"]].valor_concepto_liq = detalle.valor_concepto_liq;
//            lstDetalle[(int)Session["index"]].fecha_vto = detalle.fecha_vto;
//            lstDetalle[(int)Session["index"]].usuario = Convert.ToString(Session["usuario"]);
//            lstDetalle[(int)Session["index"]].op = 2;
//        }
//        Session["Detalle"] = lstDetalle;
//        //Session["opcion"] = 0;
//        fillDetalle(lstDetalle);
//        modalPopupDetalle.Hide();
//        btnCargar_concepto_ServerClick(null, null);
//    }
//}