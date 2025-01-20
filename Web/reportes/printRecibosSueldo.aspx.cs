using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace web.reportes
{
    public partial class printRecibosSueldo : System.Web.UI.Page
    {
        private ReportDocument customerReport = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crview = new CrystalDecisions.Web.CrystalReportViewer();

        #region Declaracion de Variables y Objetos
        bool ok = false;
        string usuario;
        SeguridadB objSeguridad;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../index.aspx");

            usuario = Convert.ToString(Session["usuario"]);            
            objSeguridad = new BLL.SeguridadB();
            ok = objSeguridad.ValidaPermiso(usuario, "VER_RECIBO_EMPLEADO");
            if (ok == false)
                Response.Redirect("~\\secure\\accesodenegado.html");

            if (Request.QueryString["anio"] == null)
                Response.Redirect("../index.aspx");
            if (Request.QueryString["cod_tipo_liq"] == null)
                Response.Redirect("../index.aspx");
            if (Request.QueryString["nro_liq"] == null)
                Response.Redirect("../index.aspx");

            //int legajo = Convert.ToInt32(Request.Cookies["UserSistema"]["Id"]);
            int legajo = Convert.ToInt16(Request.QueryString["legajo"]);
            List<Entities.AUX_MAESTRO_SUELDO> lst = BLL.AUX_MAESTRO_SUELDO.read(legajo, legajo,
                Convert.ToInt32(Request.QueryString["anio"]),
                Convert.ToInt32(Request.QueryString["cod_tipo_liq"]), Convert.ToInt32(Request.QueryString["nro_liq"]));
            //Antes de visualizar el recibo audito
            Entities.Auditoria oAudita = new Entities.Auditoria();
            oAudita.id_auditoria = 0;
            oAudita.fecha_movimiento = DateTime.Now.ToString();
            oAudita.menu = "Empleados";
            oAudita.proceso = "Consulta Recibo Sueldo";
            oAudita.identificacion = legajo.ToString();
            oAudita.autorizaciones = "";
            oAudita.observaciones = "";
            oAudita.detalle = "Consulta de Recibo Legajo " + legajo.ToString();
            oAudita.usuario = Session["usuario"].ToString();
            //DAL.AuditoriaD.Insert_movimiento(oAudita);
            ConfigureCrystalReports(lst);
        }
        private void ConfigureCrystalReports(List<Entities.AUX_MAESTRO_SUELDO> lst)
        {
            try
            {
                string reportPath = Server.MapPath("rptRecibosSueldo.rpt");
                customerReport.PrintOptions.PaperSize = PaperSize.PaperA4;
                customerReport.Load(reportPath);

                List<sueldo_detalle> lstDetalle = new List<sueldo_detalle>();
                sueldo_detalle objDetalle;


                foreach (var item in lst)
                {
                    decimal tot_hab_con_descuento = 0;
                    decimal tot_sin_con_descuento = 0;
                    decimal tot_descuento = 0;
                    foreach (var item2 in item.lstDetalle)
                    {
                        objDetalle = new sueldo_detalle();
                        objDetalle.NRO_ORDEN = item.nro_orden.ToString();
                        objDetalle.FECHA_PAGO = item.fecha_pago.ToShortDateString();
                        objDetalle.LEGAJO = Convert.ToInt32(item.legajo);
                        if (item.cod_categoria > 0)
                            objDetalle.CATEGORIA = item.cod_categoria.ToString();
                        objDetalle.NOMBRE = item.nombre;
                        objDetalle.FECHA_INGRESO = item.fecha_ingreso.ToShortDateString();
                        objDetalle.TIPO_DOC = item.des_tipo_documento;
                        objDetalle.NRO_DOC = item.nro_documento;
                        objDetalle.TIPO_LIQ = item.cod_tipo_liq.ToString();
                        objDetalle.TIPO_CONTRATACION = item.clasificacion_personal;
                        if (item.tarea.Length > 0)
                            objDetalle.CARGO = item.tarea;
                        objDetalle.SECCION = item.cod_seccion.ToString();
                        objDetalle.PERIODO_LIQUIDACION = item.des_liquidacion.Trim();
                        objDetalle.FECHA_ULTIMO_PERIODO = item.fecha_ult_dep.ToShortDateString();
                        objDetalle.PERIODO_ULTIMO = item.per_ult_dep.Trim();
                        objDetalle.CUIT = item.cuil;
                        objDetalle.SUELTO_BASICO = item.sueldo_basico;
                        objDetalle.SUELDO_NETO = item.importe_total;

                        objDetalle.cod_concepto_liq = item2.cod_concepto_liq;
                        if (item2.suma)
                            if (item2.sujeto_a_desc)
                            {
                                objDetalle.hab_con_descuento = item2.importe;
                                tot_hab_con_descuento += item2.importe;
                            }
                            else
                            {
                                objDetalle.hab_sin_descuento = item2.importe;
                                tot_sin_con_descuento += item2.importe;
                            }
                        else
                        {
                            objDetalle.descuentos = item2.importe;
                            tot_descuento += item2.importe;
                        }
                        objDetalle.tot_descuento = tot_descuento;
                        objDetalle.tot_hab_con_descuento = tot_hab_con_descuento;
                        objDetalle.tot_hab_sin_descuento = tot_sin_con_descuento;
                        objDetalle.desc_concepto_liq = item2.des_concepto_liq;
                        objDetalle.unidades = item2.unidades;
                        objDetalle.Coberturamedica = ConfigurationManager.AppSettings["coberturamedica"].ToString();
                        objDetalle.SUELDO_EN_LETRAS = conversiones.enletras(item.importe_total.ToString());
                        lstDetalle.Add(objDetalle);
                    }
                }


                customerReport.SetDataSource(lstDetalle);

                crview.ReportSource = customerReport;

                crview.RefreshReport();
                crview.DataBind();
                //PrintPDF();

                customerReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");
            }

            catch (Exception ex)
            {
                System.Console.WriteLine("Error, no se pudo generar el Reporte " + ex.Message);
                Response.Write("Hubo problemas con el cedulon, no se pudo generar el Reporte...");
            }
            finally
            {
                ;
            }
        }

    }
}