using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

namespace web.secure
{

    public partial class reporteLiquidacion : System.Web.UI.Page
    {

        SeguridadB objSeguridad;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtAnio.Text = DateTime.Now.Year.ToString();
                    CargarCombos();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
            }
        }
        protected void CargarCombos()
        {
            txtTipo_liq.DataTextField = "des_tipo_liq";
            txtTipo_liq.DataValueField = "cod_tipo_liq";
            txtTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            txtTipo_liq.DataBind();

            DataSet ds1 = new DataSet();
            ds1.ReadXml(Server.MapPath(Request.ApplicationPath + "\\xml\\ListReportesLiquidacion.xml"));
            ddlReportes.DataSource = ds1.Tables[0];
            ddlReportes.DataTextField = "Name";
            ddlReportes.DataValueField = "IDName";
            ddlReportes.DataBind();

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
        protected void txtNro_liq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Int32 reporte = 0;
            string message = "";
            bool ok = false;

            /*int desde, int hasta, int anio, int cod_tipo_liq, int nro_liq*/
            //Response.Redirect(string.Format("../reportes/printSueldos.aspx?desde={0}&hasta={1}&anio={2}&cod_tipo_liq={3}&nro_liq={4}",
            //txtDesde.Text, txtHasta.Text, txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value));
            //divReporte.InnerHtml = "<iframe src=\" " +
            //string.Format("../reportes/printSueldos.aspx?desde={0}&hasta={1}&anio={2}&cod_tipo_liq={3}&nro_liq={4}",
            //txtDesde.Text, txtHasta.Text, txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
            //+ "\"  width=\"100%\" height=\"600\"></iframe>";
            //popUpListado.Show();

            try
            {
                reporte = Convert.ToInt32(ddlReportes.SelectedValue);
                switch (reporte)
                {
                    case 1:
                        ResumenConceptos();
                        break;
                    case 2:
                        if (ResumenConceptosExcel())
                        {
                            message = "Se Guardo el Archivo..., Termino Ok ...,Verifique el mismo";
                            msjConfirmar.InnerHtml = message;
                            divConfirma.Visible = true;
                            PanelInfomacion.Update();
                        }
                        else
                        {
                            txtError.InnerText = "No se puedo Generar el Archivo, por pavor verifique Datos, posiblemente tengo campos nulos, verificar Vista...";
                            divError.Visible = true;
                            PanelInfomacion.Update();
                        }
                        break;
                    case 3:
                        ResumenConceptosFijos();
                        break;
                    case 4:
                        ResumenConceptosVariable();
                        break;

                    case 5:
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_AsientoSueldos");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                            AsientoSueldos();
                        objSeguridad = null;
                        break;
                    case 6:
                        //
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_AsientoSueldos");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                        {
                            if (AsientoSueldosExcel())
                            {
                                message = "Se Guardo el Archivo..., Termino Ok ...,Verifique el mismo";
                                msjConfirmar.InnerHtml = message;
                                divConfirma.Visible = true;
                                PanelInfomacion.Update();
                            }
                            else
                            {
                                txtError.InnerText = "No se puedo Generar el Archivo, por pavor verifique Datos, posiblemente tengo campos nulos, verificar Vista...";
                                divError.Visible = true;
                                PanelInfomacion.Update();
                            }
                            break;
                        }
                        objSeguridad = null;
                        break;
                    //                        
                    case 7:
                        //
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_PlanillaSueldos");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                            PlanillaSueldos();
                        objSeguridad = null;
                        break;
                    case 8:
                        //
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_PlanillaSueldos");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                            PlanillaSueldosxSecretaria();
                        objSeguridad = null;
                        break;
                    case 9:
                        //
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_PlanillaSueldos");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                            PlanillaSueldosExcel();
                        objSeguridad = null;
                        break;
                    case 10:
                        //
                        objSeguridad = new SeguridadB();
                        ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Ver_PlanillaCheques");
                        if (ok == false)
                            Response.Redirect("accesodenegado.html");
                        else
                            PlanillaCheques();
                        objSeguridad = null;

                        break;
                    case 11:
                        ResumenAportesJubilatorios();
                        break;
                    case 12:
                        ResumenAportesJubilatoriosxSec();
                        break;
                    case 13:
                        ResumenAportesObraSocial();
                        break;
                    case 14:
                        ResumenAportesObraSocialxSec();
                        break;
                    case 15:
                        Etiquetas();
                        break;
                    case 16:
                        ResumenConceptosFijosParaOPedido();
                        break;
                    case 17:
                        ResumenConceptosVariableParaOPedido();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool PlanillaSueldosExcel()
        {
            //int registros = 0;
            List<web.reportes.Planilla_Sueldos_ExcelRep> lstPlanilla = new List<web.reportes.Planilla_Sueldos_ExcelRep>();
            string message = string.Empty;
            GridView gridExcel = new GridView();
            string txtArchivo = "";
            //
            int anio = int.Parse(txtAnio.Text);
            int tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedItem.Value);
            int nro_liq = Convert.ToInt32(txtNro_liq.SelectedItem.Value);
            try
            {
                lstPlanilla = web.reportes.Planilla_Sueldos_ExcelRep.read(anio, tipo_liq, nro_liq);
                gridExcel.DataSource = lstPlanilla;
                gridExcel.DataBind();

                txtArchivo = "PlanillaSueldos_" + anio.ToString() + "_" + tipo_liq.ToString() + "_" + nro_liq.ToString();
                //if (archivo.Length > 0)
                if (lstPlanilla.Count > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo + ".xls"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo + ".xls");
                    DescargarDocumentoExcel(txtArchivo + ".xls", gridExcel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                lstPlanilla = null;
                gridExcel = null;
                throw e;
            }
        }

        private void PlanillaSueldos()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_planilla_sueldos.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void PlanillaSueldosxSecretaria()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_planilla_sueldos_x_secretaria.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void PlanillaCheques()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_planilla_cheques.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void Etiquetas()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_etiquetas.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void ResumenAportesJubilatorios()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_aportes_jubilatorios.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void ResumenAportesJubilatoriosxSec()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_aportes_jubilatorios_x_sec.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void ResumenAportesObraSocial()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_aportes_obra_social.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private void ResumenAportesObraSocialxSec()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_aportes_obra_social_x_sec.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        private bool AsientoSueldosExcel()
        {
            //int registros = 0;
            List<web.reportes.AsientoSueldosRep> lstConceptos = new List<web.reportes.AsientoSueldosRep>();
            string message = string.Empty;
            GridView gridExcel = new GridView();
            string txtArchivo = "";
            //
            int anio = int.Parse(txtAnio.Text);
            int tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedItem.Value);
            int nro_liq = Convert.ToInt32(txtNro_liq.SelectedItem.Value);
            try
            {
                lstConceptos = web.reportes.AsientoSueldosRep.readAsientoSueldos(anio, tipo_liq, nro_liq);
                gridExcel.DataSource = lstConceptos;
                gridExcel.DataBind();

                txtArchivo = "AsientoSueldos_" + anio.ToString() + "_" + tipo_liq.ToString() + "_" + nro_liq.ToString();
                //if (archivo.Length > 0)
                if (lstConceptos.Count > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo + ".xls"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo + ".xls");
                    DescargarDocumentoExcel(txtArchivo + ".xls", gridExcel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                lstConceptos = null;
                gridExcel = null;
                throw e;
            }
        }

        private void AsientoSueldos()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
               string.Format("../impresiones/reporte_asiento_sueldos.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
               txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
               + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void ResumenConceptos()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_resumen_cptos.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected bool ResumenConceptosExcel()
        {

            //int registros = 0;
            List<web.reportes.Conceptos_liqRep> lstConceptos = new List<web.reportes.Conceptos_liqRep>();
            string message = string.Empty;
            GridView gridExcel = new GridView();
            string txtArchivo = "";
            //
            int anio = int.Parse(txtAnio.Text);
            int tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedItem.Value);
            int nro_liq = Convert.ToInt32(txtNro_liq.SelectedItem.Value);
            try
            {
                lstConceptos = web.reportes.Conceptos_liqRep.readResumen_cptos(anio, tipo_liq, nro_liq);
                gridExcel.DataSource = lstConceptos;
                gridExcel.DataBind();

                txtArchivo = "Resumenconceptos_" +
                    anio.ToString() + "_" + tipo_liq.ToString() + "_" + nro_liq.ToString();
                //if (archivo.Length > 0)
                if (lstConceptos.Count > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo + ".xls"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo + ".xls");
                    DescargarDocumentoExcel(txtArchivo + ".xls", gridExcel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                lstConceptos = null;
                gridExcel = null;
                throw e;
            }
        }

        protected void ResumenConceptosFijos()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_conceptos_fijos.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&fijo={3}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value, 0)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void ResumenConceptosFijosParaOPedido()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_conceptos_fijos_opedido.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&fijo={3}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value, 0)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void ResumenConceptosVariable()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
            string.Format("../impresiones/reporte_conceptos_fijos.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&fijo={3}",
            txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value, 1)
            + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void ResumenConceptosVariableParaOPedido()
        {
            divReporte.InnerHtml = "<iframe src=\" " +
              string.Format("../impresiones/reporte_conceptos_fijos_opedido.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&fijo={3}",
              txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value, 1)
              + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void btnCloseListado_ServerClick(object sender, EventArgs e)
        {
            popUpListado.Hide();
        }

        protected void ddlReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
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