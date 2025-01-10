using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;


namespace web.secure
{
    public partial class acreditacion_bancos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usuario"] == null)
            //    Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                txtAnio.Text = DateTime.Now.Year.ToString();
                CargarCombos();
            }
            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
            }
            ddlTipo_liq.Focus();
        }

        private void CargarCombos()
        {
            ddlTipo_liq.DataTextField = "des_tipo_liq";
            ddlTipo_liq.DataValueField = "cod_tipo_liq";
            ddlTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq.DataBind();


            ddlTipo_liq.DataTextField = "des_tipo_liq";
            ddlTipo_liq.DataValueField = "cod_tipo_liq";
            ddlTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq.DataBind();


            ddlOpcionBanco.DataTextField = "nom_banco";
            ddlOpcionBanco.DataValueField = "cod_banco";
            ddlOpcionBanco.DataSource = BLL.ConsultaEmpleadoB.ListBancos(0);
            ddlOpcionBanco.DataBind();
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
                    ddlNro_liq.DataSource = BLL.ConsultaEmpleadoB.PeriodosLiquidados(anio, cod_tipo_liq);
                    ddlNro_liq.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ddlNro_liq.Focus();
        }

        protected void ddlNro_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entities.Liquidacion oLiq = null;
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            try
            {
                oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
                txtAnio.Text = oLiq.anio.ToString();
                ddlTipo_liq.SelectedValue = Convert.ToString(oLiq.cod_tipo_liq);
                ddlTipo_liq_SelectedIndexChanged(ddlTipo_liq, e);
                ddlNro_liq.SelectedValue = Convert.ToString(oLiq.nro_liquidacion);
                txtDes_liquidacion.Text = oLiq.des_liquidacion;
                txtPeriodo.Text = oLiq.periodo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ddlNro_liq.Focus();
        }

        private bool GenerarArchivoBco_Cba(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, decimal porcentaje)
        {

            //string[] archivo = null;
            List<Entities.Acred_bco_cba> lstAcred = new List<Entities.Acred_bco_cba>();
            string message = string.Empty;
            StringBuilder _archivo = new StringBuilder();
            bool loop = false;
            try
            {
                lstAcred = BLL.Acred_bco_cbaB.GetAcred_bco_cba(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
                if (File.Exists(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt"))
                    File.Delete(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt");
                //archivo = new string[lstAcred.Count];
                StreamWriter arch = new StreamWriter(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt", true, Encoding.Default);
                for (int i = 0; i < lstAcred.Count; i++)
                {
                    //if (i != lstAcred.Count - 1)
                    arch.WriteLine(lstAcred[i].campo_1 + lstAcred[i].campo_2 + lstAcred[i].campo_3 + lstAcred[i].campo_4 +
                      lstAcred[i].campo_5 + lstAcred[i].campo_6 + lstAcred[i].campo_7 + lstAcred[i].campo_8 +
                      lstAcred[i].campo_9 + lstAcred[i].campo_10 + lstAcred[i].campo_11 + lstAcred[i].campo_12);
                    //else
                    //  arch.Write(lstAcred[i].campo_1 + lstAcred[i].campo_2 + lstAcred[i].campo_3 + lstAcred[i].campo_4 +
                    //lstAcred[i].campo_5 + lstAcred[i].campo_6 + lstAcred[i].campo_7 + lstAcred[i].campo_8 +
                    //lstAcred[i].campo_9 + lstAcred[i].campo_10 + lstAcred[i].campo_11 + lstAcred[i].campo_12);
                    loop = true;
                }
                if (loop)
                {
                    arch.Close();
                    DescargarDocumento(txtArchivo.Text + ".txt");
                    return true;
                }
                else
                {
                    arch.Close();
                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool GenerarArchivoBco_macro(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, decimal porcentaje)
        {

            //string[] archivo = null;
            List<Entities.Acred_bco_macro> lstAcred = new List<Entities.Acred_bco_macro>();
            string message = string.Empty;
            StringBuilder _archivo = new StringBuilder();
            bool loop = false;
            try
            {
                lstAcred = BLL.Acred_bco_macroB.GetAcred_bco_macro(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
                if (File.Exists(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt"))
                    File.Delete(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt");
                //archivo = new string[lstAcred.Count];
                StreamWriter arch = new StreamWriter(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt", true, Encoding.Default);
                for (int i = 0; i < lstAcred.Count; i++)
                {
                    //if (i != lstAcred.Count - 1)
                    arch.WriteLine(lstAcred[i].codigo_empresa + lstAcred[i].tipo_dni + lstAcred[i].nro_documento + lstAcred[i].apellido_beneficiario +
                    lstAcred[i].nombre_beneficiario + //lstAcred[i].tipo_cuenta + 
                    lstAcred[i].sueldo_neto + lstAcred[i].nro_cbu);
                    //else
                    //    arch.Write(lstAcred[i].codigo_empresa + lstAcred[i].tipo_dni + lstAcred[i].nro_documento + lstAcred[i].apellido_beneficiario +
                    //  lstAcred[i].nombre_beneficiario + //lstAcred[i].tipo_cuenta + 
                    //  lstAcred[i].sueldo_neto + lstAcred[i].nro_cbu);
                    loop = true;
                }
                if (loop)
                {
                    arch.Close();
                    DescargarDocumento(txtArchivo.Text + ".txt");
                    return true;
                }
                else
                {
                    arch.Close();
                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool GenerarArchivoBco_macro_nvo_formato(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, decimal porcentaje)
        {
            //string[] archivo = null;
            List<Entities.Pago_sueldo_macro> lstAcred = new List<Entities.Pago_sueldo_macro>();
            string message = string.Empty;
            StringBuilder _archivo = new StringBuilder();
            string espacio = string.Empty;
            bool loop = false;
            try
            {
                lstAcred = BLL.Acred_bco_macroB.GetAcred_bco_macro_nvo_formato(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
                if (File.Exists(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt"))
                    File.Delete(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt");
                //archivo = new string[lstAcred.Count];
                StreamWriter arch = new StreamWriter(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt", true, Encoding.UTF8);
                for (int i = 0; i < lstAcred.Count; i++)
                {
                    arch.WriteLine(lstAcred[i].legajo.Substring(0, 7).PadRight(7, ' ') +
                                   "\t" +
                                   lstAcred[i].cuil.Substring(0, 11) +
                                   "\t" +
                                   lstAcred[i].apeynom.Trim().PadRight(19, ' ') +
                                   "\t" +
                                   lstAcred[i].cuenta +
                                   "\t" +
                                   lstAcred[i].cbu +
                                   "\t" +
                                   lstAcred[i].importe +
                                   "\t" +
                                   lstAcred[i].comprobante);
                    loop = true;
                }
                if (loop)
                {
                    arch.Close();
                    DescargarDocumento(txtArchivo.Text + ".txt");
                    return true;
                }
                else
                {
                    arch.Close();
                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //private bool GenerarArchivoBco_generico(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo, decimal porcentaje)
        //{
        //    //string[] archivo = null;
        //    List<Entities.Pago_sueldo_macro> lstAcred = new List<Entities.Pago_sueldo_macro>();
        //    string message = string.Empty;
        //    StringBuilder _archivo = new StringBuilder();
        //    string espacio = string.Empty;
        //    bool loop = false;
        //    try
        //    {
        //        lstAcred = BLL.Acred_bco_macroB.GetAcred_bco_macro_nvo_formato(anio, cod_tipo_liq, nro_liquidacion, porcentaje);
        //        if (File.Exists(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt"))
        //            File.Delete(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt");
        //        //archivo = new string[lstAcred.Count];
        //        StreamWriter arch = new StreamWriter(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt", true, Encoding.UTF8);
        //        for (int i = 0; i < lstAcred.Count; i++)
        //        {
        //            arch.WriteLine(lstAcred[i].legajo.Substring(0, 7).PadRight(7, ' ') +
        //                           "\t" +
        //                           lstAcred[i].cuil.Substring(0, 11) +
        //                           "\t" +
        //                           lstAcred[i].apeynom.Trim().PadRight(19, ' ') +
        //                           "\t" +
        //                           lstAcred[i].cuenta +
        //                           "\t" +
        //                           lstAcred[i].cbu +
        //                           "\t" +
        //                           lstAcred[i].importe +
        //                           "\t" +
        //                           lstAcred[i].comprobante);
        //            loop = true;
        //        }
        //        if (loop)
        //        {
        //            arch.Close();
        //            DescargarDocumento(txtArchivo.Text + ".txt");
        //            return true;
        //        }
        //        else
        //        {
        //            arch.Close();
        //            return false;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        private void DescargarDocumento(String fileNme)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileNme);
                HttpContext.Current.Response.WriteFile(fileNme);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void lbtnGenerar_Click(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            string periodo = txtPeriodo.Text;
            int tipo_opcion = Convert.ToInt32(ddlOpcionBanco.SelectedValue);
            int cod_banco = Convert.ToInt32(ddlOpcionBanco.SelectedValue);
            decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
            string message = string.Empty;
            //string tipo_opcion = "";
            try
            {
                if (cod_banco == 20)
                {
                    if (GenerarArchivoBco_Cba(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion, txtArchivo.Text.ToString(), porcentaje))
                    {
                        message = "Se Guardo el Archivo..., Termino Ok ...,Verifique el mismo. Es probable que existan Legajos repetidos, " +
                          "esto se debe a que el Periodo esta repetido en varias Liquidaciones...";
                        msjConfirmar.InnerHtml = message;
                        divConfirma.Visible = true;
                        PanelInfomacion.Update();
                    }
                    else
                    {
                        txtError.InnerText = "No se puedo Generar el Archivo, por pavor verifique la Liquidacion y el campo Periodo...";
                        divError.Visible = true;
                        PanelInfomacion.Update();
                    }
                }
                else
                {
                    if (GenerarArchivoBco_macro_nvo_formato(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion, txtArchivo.Text.ToString(), porcentaje))
                    //if (GenerarArchivoBco_macro(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion, txtArchivo.Text.ToString(), porcentaje))
                    {
                        message = "Se Guardo el Archivo..., Termino Ok ...,Verifique el mismo. Es probable que existan Legajos repetidos, " +
                          "esto se debe a que el Periodo esta repetido en varias Liquidaciones...";
                        msjConfirmar.InnerHtml = message;
                        divConfirma.Visible = true;
                        PanelInfomacion.Update();
                    }
                    else
                    {
                        txtError.InnerText = "No se puedo Generar el Archivo, por pavor verifique la Liquidacion y el campo Periodo...";
                        divError.Visible = true;
                        PanelInfomacion.Update();
                    }

                }
            }
            catch (Exception ex)
            {
                txtError.InnerText = "No se puedo Generar el Archivo, por pavor verifique la carpeta donde se genera el archivo..." + ex.ToString();
                divError.Visible = true;
                PanelInfomacion.Update();
                //throw ex;
            }
        }

        protected void lbtnImprimir_Click(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            string periodo = txtPeriodo.Text;
            int tipo_opcion = Convert.ToInt32(ddlOpcionBanco.SelectedValue);
            int cod_banco = Convert.ToInt32(ddlOpcionBanco.SelectedValue);
            decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
            string message = string.Empty;
            //string tipo_opcion = "";
            try
            {
                if (cod_banco == 20)
                {
                    divReporte.InnerHtml = "<iframe src=\" " +
                    string.Format("../impresiones/reporte_acred_bco.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&porcentaje={3}",
                    txtAnio.Text, ddlTipo_liq.SelectedItem.Value, ddlNro_liq.SelectedItem.Value, txtPorcentaje.Text)
                    + "\"  width=\"100%\" height=\"600\"></iframe>";
                    popUpListado.Show();
                }
                else
                {
                    if (cod_banco == 255)
                    {
                        divReporte.InnerHtml = "<iframe src=\" " +
                        string.Format("../impresiones/reporte_acred_bco_macro.aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&porcentaje={3}",
                        txtAnio.Text, ddlTipo_liq.SelectedItem.Value, ddlNro_liq.SelectedItem.Value, txtPorcentaje.Text)
                        + "\"  width=\"100%\" height=\"600\"></iframe>";
                        popUpListado.Show();
                    }
                    else
                    {
                        divReporte.InnerHtml = "<iframe src=\" " +
                        string.Format("../impresiones/reporte_acred_generico" +
                        ".aspx?anio={0}&cod_tipo_liq={1}&nro_liq={2}&porcentaje={3}&banco={4}",
                        txtAnio.Text, ddlTipo_liq.SelectedItem.Value, ddlNro_liq.SelectedItem.Value, txtPorcentaje.Text,
                        Convert.ToString(ddlOpcionBanco.SelectedItem))
                        + "\"  width=\"100%\" height=\"600\"></iframe>";
                        popUpListado.Show();
                    }
                }
            }
            catch (Exception)
            {
                txtError.InnerText = "No se puedo Imprimir, por pavor verifique los Datos seleccionados...";
                divError.Visible = true;
                PanelInfomacion.Update();
                //throw ex;
            }
        }


        protected bool OtrosBancos()
        {
            //int registros = 0;
            List<web.reportes.Acred_bco_generico> lst = new List<web.reportes.Acred_bco_generico>();
            lst = web.reportes.Acred_bco_generico.read(Convert.ToInt32(Request.QueryString["anio"]),
                           Convert.ToInt32(Request.QueryString["cod_tipo_liq"]),
                           Convert.ToInt32(Request.QueryString["nro_liq"]),
                           Convert.ToDecimal(Request.QueryString["porcentaje"]));

            string message = string.Empty;
            GridView gridExcel = new GridView();
            string txtArchivo = "";
            //
            int anio = int.Parse(txtAnio.Text);
            int tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            int nro_liq = Convert.ToInt32(ddlNro_liq.SelectedValue);

            try
            {
                gridExcel.DataSource = lst;
                gridExcel.DataBind();
                txtArchivo = "ResumenBanco" + anio.ToString() + "_" + tipo_liq.ToString() + "_" + nro_liq.ToString();
                if (lst.Count > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo + ".xls"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo + ".xls");
                    DescargarDocumentoExcel(txtArchivo + ".xls", gridExcel);
                    return true;
                }
                else
                {
                    txtError.InnerText = "No hay datos, por pavor verifique los mismos...";
                    divError.Visible = true;
                    return false;
                }
            }
            catch (Exception e)
            {
                lst = null;
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

        protected void btnCloseListado_ServerClick(object sender, EventArgs e)
        {
            popUpListado.Hide();
        }
    }

}

