using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.impresiones
{
    public partial class reporte_planilla_sueldos : System.Web.UI.Page
    {
        private ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crv = new CrystalDecisions.Web.CrystalReportViewer();
        int anio = 0;
        int cod_tipo_liq = 0;
        int nro_liquidacion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<web.reportes.Planilla_SueldosRep> lst = new List<web.reportes.Planilla_SueldosRep>();
            lst = web.reportes.Planilla_SueldosRep.read(Convert.ToInt32(Request.QueryString["anio"]),
                Convert.ToInt32(Request.QueryString["cod_tipo_liq"]), Convert.ToInt32(Request.QueryString["nro_liq"]));

            anio = Convert.ToInt32((Request.QueryString["anio"]));
            cod_tipo_liq = Convert.ToInt32(Request.QueryString["cod_tipo_liq"]);
            nro_liquidacion = Convert.ToInt32(Request.QueryString["nro_liq"]);

            Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);


            string titulo_liq = oLiq.des_liquidacion;
            string tipo_liq = oLiq.des_tipo_liq;
            ConfigureCrystalReports(lst, titulo_liq, tipo_liq);

        }

        private void ConfigureCrystalReports(List<web.reportes.Planilla_SueldosRep> lst, string titulo_liq, string tipo_liq)
        {
            //int total_legajos = 0;
            //decimal total_neto = 0;

            //List<web.reportes.Planilla_Sueldos_DetalleRep> lstDetallle = new List<web.reportes.Planilla_Sueldos_DetalleRep>();
            //web.reportes.Planilla_Sueldos_DetalleRep objDetalle;
            try
            {
                //foreach (var item in lst)
                //{
                //    foreach (var item2 in item.lstDetalle)
                //    {
                //        objDetalle = new reportes.Planilla_Sueldos_DetalleRep();
                //        objDetalle.anio = item2.anio;
                //        objDetalle.cod_tipo_liq = item2.cod_tipo_liq;
                //        objDetalle.nro_liquidacion = item2.nro_liquidacion;
                //        objDetalle.legajo = item2.legajo;
                //        objDetalle.des_concepto_liq = item2.des_concepto_liq;
                //        objDetalle.cod_concepto_liq = item2.cod_concepto_liq;
                //        objDetalle.suma = item2.suma;
                //        objDetalle.sujeto_a_desc = item2.sujeto_a_desc;
                //        objDetalle.sac = item2.sac;
                //        objDetalle.unidades = item2.unidades;
                //        objDetalle.importe = item2.importe;
                //        objDetalle.nro_orden = item2.nro_orden;
                //        lstDetallle.Add(objDetalle);
                //    }
                //}

                reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
                reporte.Load(Server.MapPath("../reportes/rplanilla_sueldos.rpt"));
                //reporte.Subreports[0].SetDataSource(lstDetallle);
                reporte.SetDataSource(lst);
                string usuario = Session["usuario"].ToString();
                //reporte.SetParameterValue("strTitulo", "PLANILLA DE SUELDOS Y JORNALES");
                reporte.SetParameterValue("strMes_liquidacion", "LIQUIDACION CORRESPONDIENTE AL MES " + titulo_liq);
                reporte.SetParameterValue("strTipo_liquidacion", "TIPO LIQUIDACION : " + tipo_liq);
                reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
                crv.ReportSource = reporte;
                crv.RefreshReport();
                crv.DataBind();
                reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");

            }

            catch (Exception ex)
            {
                System.Console.WriteLine("Error, no se pudo generar el Reporte " + ex.Message);
                Response.Write(ex.Message);
            }
            finally
            {
                ;
            }
        }




    }
}