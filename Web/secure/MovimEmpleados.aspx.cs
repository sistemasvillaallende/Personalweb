using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BLL;
using Web.Utils;

namespace web.secure
{
    public partial class MovimEmpleados : System.Web.UI.Page
    {
        protected GridView gvCambios;
        protected GridView gvConceptoMov;
        protected Label lblTarea;
        protected Label lblLiquidacion;
        protected Label lblCategoria;
        protected Label lblClasificacionPersonal;
        protected Label lblSeccion;
        protected Label lblPasoContrato;
        //protected Label lblNombre;
        protected Label lblLegajo;
        protected Label lblCargo;
        protected PlaceHolder phMovimientosInternos;
        protected PlaceHolder phVariacionConceptos;
        int legajo;
        string nombre;

        protected void Page_Load(object sender, EventArgs e)
        {
            legajo = Convert.ToInt32(Request.QueryString["legajo"]);
            //nombre = Convert.ToString(Request.QueryString["NOMBRE"]);
            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                //lblNombre.Text = Utils.CapitalizarPalabras(nombre);
                //lblLegajo.Text = legajo.ToString();
                CargarGrillaCambios(legajo);
                CargarGrillaConceptos(legajo);
                //ActualizarInformacionCabecera(legajo);
                DAL.Temp_empleados objEmpleado = DAL.Temp_empleados.getByPk(legajo);

                this.txtNombre.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.NOMBRE);
                txtNombre.Attributes.Add("title", objEmpleado.NOMBRE);
                this.lblFecha_nacimiento.InnerHtml = objEmpleado.FECHA_NACIMIENTO.ToShortDateString();
                this.lblSexo.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.SEXO);
                this.lblEstadoCivil.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.ESTADO_CIVIL);
                this.lblCuit.InnerHtml = objEmpleado.CUIT;
                lblOS.InnerHtml = objEmpleado.NRO_AFILIADO_OS;
                string direccion = string.Format("{0} {1} B° {2}, {3} {4}",
                    Utils.CapitalizarPalabras(objEmpleado.CALLE.Trim()), objEmpleado.NRO,
                    Utils.CapitalizarPalabras(objEmpleado.BARRIO.Trim()),
                    Utils.CapitalizarPalabras(objEmpleado.CIUDAD.Trim()),
                    Utils.CapitalizarPalabras(objEmpleado.PROVINCIA.Trim()));
                lblDomicilio.InnerHtml = direccion;
                lblDomicilio.Attributes.Add("title", direccion);
                lblCodPostal.InnerHtml = objEmpleado.CP;
                lblTelefono.InnerHtml = objEmpleado.TELEFONOS;
                lblMail.InnerHtml = objEmpleado.EMAIL;


            }
        }

        private void CargarGrillaCambios(int legajo)
        {
            var cambios = BLL.Concepto_Liq_x_EmpB.GetCambiosEmpleadoXLegajo(legajo);
            foreach (var cambio in cambios)
            {
                string[] v = cambio.descripcion_cambio.Split(Convert.ToChar(":"));
                switch (v[0])
                {
                    case "Cambio Clasificacion Personal":
                        v[1] = "Pasa a " + Utils.CapitalizarPalabras(v[1]);
                        break;
                    case "Cambio de Cargo":
                        v[1] = "Cambia a " + Utils.CapitalizarPalabras(v[1]);
                        break;
                    case "Cambio de Categoria":
                        v[1] = "Pasa a categoria " + v[1];
                        break;
                    case "Cambio de Seccion":
                        v[1] = "Cambia a " + Utils.CapitalizarPalabras(v[1]);
                        break;
                    case "Cambio de Tarea":
                        v[1] = v[1];
                        break;
                    case "Cambio Tipo Liquidacion":
                        v[1] = Utils.CapitalizarPalabras(v[1]);
                        break;
                    default:
                        break;
                }
                var divEvent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divEvent.Attributes["class"] = "timeline__event animated fadeInUp delay-2s timeline__event--type2";

                var divIcon = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divIcon.Attributes["class"] = "timeline__event__icon";
                var icon = new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                icon.Attributes["class"] = "lni-burger";
                var divDate = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divDate.Attributes["class"] = "timeline__event__date";
                divDate.InnerText = cambio.fecha_cambio.ToString("dd-MM-yyyy");
                divIcon.Controls.Add(icon);
                divIcon.Controls.Add(divDate);

                var divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divContent.Attributes["class"] = "timeline__event__content";
                var divTitle = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divTitle.Attributes["class"] = "timeline__event__title";
                divTitle.InnerText = v[0];// "Cambio de Tarea";
                var divDescription = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divDescription.Attributes["class"] = "timeline__event__description";
                var pDescription = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                pDescription.Attributes["style"] = "margin-bottom: 0;";
                pDescription.InnerText = v[1];
                divDescription.Controls.Add(pDescription);

                divContent.Controls.Add(divTitle);
                divContent.Controls.Add(divDescription);

                divEvent.Controls.Add(divIcon);
                divEvent.Controls.Add(divContent);

                phMovimientosInternos.Controls.Add(divEvent);
            }
        }

        private void CargarGrillaConceptos(int legajo)
        {
            var conceptos = BLL.Concepto_Liq_x_EmpB.GetHistorial_ConceptosXLegajo(legajo);
            foreach (var concepto in conceptos)
            {
                var divEvent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divEvent.Attributes["class"] = "timeline__event animated fadeInUp delay-2s timeline__event--type3";

                var divIcon = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divIcon.Attributes["class"] = "timeline__event__icon";
                var icon = new System.Web.UI.HtmlControls.HtmlGenericControl("i");
                icon.Attributes["class"] = "lni-burger";
                var divDate = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divDate.Attributes["class"] = "timeline__event__date";
                divDate.InnerText = concepto.Fecha.ToString("dd-MM-yyyy");
                divIcon.Controls.Add(icon);
                divIcon.Controls.Add(divDate);

                var divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divContent.Attributes["class"] = "timeline__event__content";
                var divTitle = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divTitle.Attributes["class"] = "timeline__event__title";
                divTitle.InnerText = Utils.CapitalizarPalabras(
                    concepto.Tipo_movimiento);
                var divDescription = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divDescription.Attributes["class"] = "timeline__event__description";
                var pDescription = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                pDescription.Attributes["style"] = "margin-bottom: 0;";
                pDescription.InnerHtml = $"<strong style=\"font-weight: 500;\">Concepto:</strong> {Utils.CapitalizarPalabras(concepto.Concepto)}<br>" +
                                         $"<strong style=\"font-weight: 500;\">Cod. Concepto:</strong> {concepto.Cod_concepto_liq}<br>" +
                                         $"<strong style=\"font-weight: 500;\">Valor Concepto:</strong> {concepto.Valor_concepto_liq}<br>" +
                                         $"<strong style=\"font-weight: 500;\">Observacion:</strong> {Utils.CapitalizarPalabras(concepto.Observacion)}<br>" +
                                         $"<strong style=\"font-weight: 500;\">Usuario Carga:</strong> {Utils.CapitalizarPalabras(concepto.Usuario_Carga)}";
                divDescription.Controls.Add(pDescription);

                divContent.Controls.Add(divTitle);
                divContent.Controls.Add(divDescription);

                divEvent.Controls.Add(divIcon);
                divEvent.Controls.Add(divContent);

                phVariacionConceptos.Controls.Add(divEvent);
            }
        }

        private void ActualizarInformacionCabecera(int legajo)
        {
            var cambios = BLL.Concepto_Liq_x_EmpB.GetCambiosEmpleadoXLegajo(legajo);

            var primerRegistro = cambios.OrderBy(c => c.fecha_cambio).FirstOrDefault();
            if (primerRegistro != null)
            {
                lblPasoContrato.Text = primerRegistro.fecha_cambio.ToString("dd-MM-yyyy");
            }

            var ultimoCambioTarea = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio de Tarea:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioTarea != null)
            {
                lblTarea.Text = Utils.CapitalizarPalabras(
                    ultimoCambioTarea.descripcion_cambio.Replace("Cambio de Tarea:", "").Trim());
            }

            var ultimoCambioLiquidacion = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio Tipo Liquidacion:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioLiquidacion != null)
            {
                lblLiquidacion.Text = Utils.CapitalizarPalabras(
                    ultimoCambioLiquidacion.descripcion_cambio.Replace("Cambio Tipo Liquidacion:", "")).Trim();
            }

            var ultimoCambioCategoria = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio de Categoria:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioCategoria != null)
            {
                lblCategoria.Text = ultimoCambioCategoria.descripcion_cambio.Replace("Cambio de Categoria:", "").Trim();
            }

            var ultimoCambioClasificacion = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio Clasificacion Personal:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioClasificacion != null)
            {
                lblClasificacionPersonal.Text = Utils.CapitalizarPalabras(
                    ultimoCambioClasificacion.descripcion_cambio.Replace(
                        "Cambio Clasificacion Personal:", "").Trim());
            }

            var ultimoCambioSeccion = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio de Seccion:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioSeccion != null)
            {
                lblSeccion.Text = Utils.CapitalizarPalabras(
                    ultimoCambioSeccion.descripcion_cambio.Replace(
                        "Cambio de Seccion:", "").Trim());
            }

            var ultimoCambioCargo = cambios
                .Where(c => c.descripcion_cambio.StartsWith("Cambio de Cargo:"))
                .OrderByDescending(c => c.fecha_cambio)
                .FirstOrDefault();
            if (ultimoCambioCargo != null)
            {
                lblCargo.Text = Utils.CapitalizarPalabras(
                    ultimoCambioCargo.descripcion_cambio.Replace("Cambio de Cargo:", "").Trim());
            }
        }

        protected void gvCambios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This method is no longer needed for the timeline structure
        }

        protected void gvCambios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvCambios.PageSize * gvCambios.PageIndex);
        }

        protected void gvConceptoMov_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This method is no longer needed for the timeline structure
        }

        protected void gvConceptoMov_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvConceptoMov.PageSize * gvConceptoMov.PageIndex);
        }

        protected void gvCambios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCambios.PageIndex = e.NewPageIndex;
            CargarGrillaCambios(legajo);
        }

        protected void gvConceptoMov_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConceptoMov.PageIndex = e.NewPageIndex;
            CargarGrillaConceptos(legajo);
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
            Response.AddHeader("Content-Disposition", "attachment;filename="+ nameReport +".xls");
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }

        protected void LinkExportar_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                List<Entities.Cambios_empleado> lst = new List<Entities.Cambios_empleado>();
                lst = BLL.Concepto_Liq_x_EmpB.GetCambiosEmpleadoXLegajo(legajo);
                gv.DataSource = lst;
                gv.DataBind();
                ExportToExcel("Mov_cambio_empleado", gv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void LinkExportar_Click_Hist(object sender, EventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                List<Entities.Historial_conceptos> lst = new List<Entities.Historial_conceptos>();
                lst = BLL.Concepto_Liq_x_EmpB.GetHistorial_ConceptosXLegajo(legajo);
                gv.DataSource = lst;
                gv.DataBind();
                ExportToExcel("Mov_cambio_concepto", gv);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}