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
    public partial class sijcor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                chkConAquinaldo.Checked = false;
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
            txtAnio.Focus();
        }

        private void CargarCombos()
        {
            ddlTipo_liq.DataTextField = "des_tipo_liq";
            ddlTipo_liq.DataValueField = "cod_tipo_liq";
            ddlTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddlTipo_liq.DataBind();
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

        private bool GenerarArchivo(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo)
        {
            string strVD = ConfigurationManager.AppSettings["VD"]; //Aca va el nombre de la carpeta donde esta corriendo el sitio
            string SiteRoot = Server.MapPath("\\" + strVD);
            string Carpeta = ConfigurationManager.AppSettings["archivos"];
            //string Path = "";
            string[] archivo = null;
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            string message = string.Empty;
            StringBuilder _archivo = new StringBuilder();

            try
            {
                if (!chkConAquinaldo.Checked)
                    lstSijcor = BLL.SijcorB.GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                else
                    lstSijcor = BLL.SijcorB.GetSijcorConAguilucho(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                archivo = new string[lstSijcor.Count];
                for (int i = 0; i < lstSijcor.Count; i++)
                {
                    _archivo.AppendLine(lstSijcor[i].cuil + lstSijcor[i].apeynom + lstSijcor[i].cargo + lstSijcor[i].categoria +
                      //lstSijcor[i].cod_actividad +
                      lstSijcor[i].adherente_vol +
                      lstSijcor[i].adherente_obl + lstSijcor[i].cod_situacion + lstSijcor[i].cod_condicion + lstSijcor[i].cod_actividad + lstSijcor[i].cod_modalidad_contratacion +
                      lstSijcor[i].cod_siniestro + lstSijcor[i].cod_departamento + lstSijcor[i].cod_delegacion + lstSijcor[i].cod_obra_social + lstSijcor[i].cod_situacion_1er_tramo +
                      lstSijcor[i].cant_dias_1er_tramo + lstSijcor[i].cod_situacion_2do_tramo + lstSijcor[i].cant_dias_2do_tramo + lstSijcor[i].cod_situacion_3er_tramo +
                      lstSijcor[i].cant_dias_3er_tramo + lstSijcor[i].cant_dias_trabajados + lstSijcor[i].sueldo + lstSijcor[i].importe_hs_extra + lstSijcor[i].zona_desfavorable +
                      lstSijcor[i].conceptos_no_remunerativos + lstSijcor[i].retroactividades + lstSijcor[i].aguinaldo + lstSijcor[i].remuneracion_2 + lstSijcor[i].tipo_adicional_seg_vida +
                      lstSijcor[i].secuencia_cuil + lstSijcor[i].diferencia_x_jerarquia + lstSijcor[i].importe_adherente_voluntario);

                }
                //File.WriteAllLines(@"d:\prueba.txt", archivo);
                if (_archivo.Length > 0)
                {
                    if (File.Exists(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt"))
                        File.Delete(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt");

                    StreamWriter arch = new StreamWriter(Server.MapPath(".") + "/" + txtArchivo.Text + ".txt", true, Encoding.Default);

                    arch.WriteLine(_archivo);
                    arch.Close();
                    DescargarDocumento(txtArchivo.Text + ".txt");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected List<Entities.Sijcor> GenerarExcel(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion)
        {
            //string strVD = ConfigurationManager.AppSettings["VD"]; //Aca va el nombre de la carpeta donde esta corriendo el sitio
            //string SiteRoot = Server.MapPath("\\" + strVD);
            //string Carpeta = ConfigurationManager.AppSettings["archivos"];
            ////string Path = "";
            //string[] archivo = null;
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            string message = string.Empty;
            StringBuilder _archivo = new StringBuilder();
            try
            {
                if (!chkConAquinaldo.Checked)
                    lstSijcor = BLL.SijcorB.GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                else
                    lstSijcor = BLL.SijcorB.GetSijcorConAguilucho(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
                return lstSijcor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        //private string GenerarArchivo_1(int anio, int cod_tipo_liq, int nro_liquidacion, string periodo, int tipo_opcion, string nombre_archivo)
        //{

        //  string strVD = ConfigurationManager.AppSettings["VD"]; //Aca va el nombre de la carpeta donde esta corriendo el sitio
        //  string SiteRoot = Server.MapPath("\\" + strVD);
        //  string Carpeta = ConfigurationManager.AppSettings["archivos"];
        //  string Path = "";
        //  string[] archivo = null;
        //  List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
        //  string message = string.Empty;
        //  try
        //  {
        //    lstSijcor = BLL.SijcorB.GetSijcor(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
        //    archivo = new string[lstSijcor.Count];
        //    for (int i = 0; i < lstSijcor.Count; i++)
        //    {
        //      archivo[i] = lstSijcor[i].cuil + lstSijcor[i].apeynom + lstSijcor[i].cargo + lstSijcor[i].categoria +
        //        //lstSijcor[i].cod_actividad +
        //        lstSijcor[i].adherente_vol +
        //        lstSijcor[i].adherente_obl + lstSijcor[i].cod_situacion + lstSijcor[i].cod_condicion + lstSijcor[i].cod_actividad + lstSijcor[i].cod_modalidad_contratacion +
        //        lstSijcor[i].cod_siniestro + lstSijcor[i].cod_departamento + lstSijcor[i].cod_delegacion + lstSijcor[i].cod_obra_social + lstSijcor[i].cod_situacion_1er_tramo +
        //        lstSijcor[i].cant_dias_1er_tramo + lstSijcor[i].cod_situacion_2do_tramo + lstSijcor[i].cant_dias_2do_tramo + lstSijcor[i].cod_situacion_3er_tramo +
        //        lstSijcor[i].cant_dias_3er_tramo + lstSijcor[i].cant_dias_trabajados + lstSijcor[i].sueldo + lstSijcor[i].importe_hs_extra + lstSijcor[i].zona_desfavorable +
        //        lstSijcor[i].conceptos_no_remunerativos + lstSijcor[i].retroactividades + lstSijcor[i].aguinaldo + lstSijcor[i].remuneracion_2 + lstSijcor[i].tipo_adicional_seg_vida +
        //        lstSijcor[i].secuencia_cuil + lstSijcor[i].diferencia_x_jerarquia + lstSijcor[i].importe_adherente_voluntario;
        //    }
        //    //File.WriteAllLines(@"d:\prueba.txt", archivo);
        //    if (archivo.Length > 0)
        //    {
        //      Path = Server.MapPath("~/archivos/" + txtArchivo.Text);
        //      File.WriteAllLines(@Path, archivo);
        //      lbtnGenerar.Visible = false;
        //      lbtnGuardar.Visible = true;
        //      return archivo.ToString();
        //    }
        //    else
        //    {
        //      return archivo.ToString();
        //    }
        //  }
        //  catch (Exception e)
        //  {
        //    throw e;
        //  }

        //}

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
            int tipo_opcion = Convert.ToInt32(ddlOpcionArchivo.SelectedValue);
            string message = string.Empty;
            //string tipo_opcion = "";
            try
            {
                if (GenerarArchivo(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion, txtArchivo.Text.ToString()))
                {
                    message = "Se Guardo el Archivo..., Termino Ok ...,Verifique el mismo. Es probable que existan Legajos repetidos, " +
                      "esto se debe a que el Periodo esta repetido en varias Liquidaciones...";
                    msjConfirmar.InnerHtml = message;
                    divConfirma.Visible = true;
                    PanelInfomacion.Update();
                }
                else
                {
                    txtError.InnerText = "No se pudo Generar el Archivo, por pavor verifique la Liquidacion y el campo Periodo...";
                    divError.Visible = true;
                    PanelInfomacion.Update();
                }
            }
            catch (Exception)
            {
                txtError.InnerText = "No se pudo Generar el Archivo, por pavor verifique la carpeta donde se genera el archivo...";
                divError.Visible = true;
                PanelInfomacion.Update();
                //throw ex;
            }
        }

        protected void lbtnExcel_Click(object sender, EventArgs e)
        {

            //List<web.reportes.NominaArt> lst = new List<reportes.NominaArt>();
            //GridView GridtoExcel = new GridView();
            //lst = web.reportes.NominaArt.read();
            //    GridtoExcel.DataSource = lst;
            //    GridtoExcel.DataBind();
            //    DescargarDocumentoExcel("ReporteNominaArt.xls", GridtoExcel);
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(ddlTipo_liq.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(ddlNro_liq.SelectedValue);
            string periodo = txtPeriodo.Text;
            int tipo_opcion = Convert.ToInt32(ddlOpcionArchivo.SelectedValue);
            string message = string.Empty;
            List<Entities.Sijcor> lstSijcor = new List<Entities.Sijcor>();
            lstSijcor = GenerarExcel(anio, cod_tipo_liq, nro_liquidacion, periodo, tipo_opcion);
            GridView GridToExcel = new GridView();
            GridToExcel.DataSource = lstSijcor;
            GridToExcel.DataBind();
            ExportToExcel("Sijcor.xls", GridToExcel);
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

    }

}

