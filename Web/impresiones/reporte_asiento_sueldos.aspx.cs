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
    public partial class reporte_asiento_sueldos : System.Web.UI.Page
    {
        private ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crv = new CrystalDecisions.Web.CrystalReportViewer();
        int anio = 0;
        int cod_tipo_liq = 0;
        int nro_liquidacion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            List<web.reportes.AsientoSueldosRep> lst = new List<web.reportes.AsientoSueldosRep>();
            lst = web.reportes.AsientoSueldosRep.readAsientoSueldos(Convert.ToInt32(Request.QueryString["anio"]),
                Convert.ToInt32(Request.QueryString["cod_tipo_liq"]), Convert.ToInt32(Request.QueryString["nro_liq"]));

            anio = Convert.ToInt32((Request.QueryString["anio"]));
            cod_tipo_liq = Convert.ToInt32(Request.QueryString["cod_tipo_liq"]);
            nro_liquidacion = Convert.ToInt32(Request.QueryString["nro_liq"]);

            Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);


            string titulo_liq = oLiq.des_liquidacion;
            string tipo_liq = oLiq.des_tipo_liq;
            ConfigureCrystalReports(lst, titulo_liq, tipo_liq);
        }

        private void ConfigureCrystalReports(List<web.reportes.AsientoSueldosRep> lst, string titulo_liq, string tipo_liq)
        {
            decimal egresos = 0;
            decimal ingresos = 0;
            decimal neto = 0;
            try
            {
                foreach (var item in lst)
                {
                    if (item.suma == 0)
                        egresos += item.importe;
                    else
                        ingresos += item.importe;

                }
                neto = egresos - ingresos;
                reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
                reporte.Load(Server.MapPath("../reportes/rasientosueldos.rpt"));
                reporte.SetDataSource(lst);
                string usuario = Session["usuario"].ToString();
                reporte.SetParameterValue("strMes_liquidacion", "LIQUIDACION CORRESPONDIENTE AL MES " + titulo_liq);
                reporte.SetParameterValue("strTipo_liquidacion", "TIPO LIQUIDACION : " + tipo_liq);
                reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
                reporte.SetParameterValue("strEgresos", Convert.ToString(egresos));
                reporte.SetParameterValue("strIngresos", Convert.ToDecimal(ingresos));
                reporte.SetParameterValue("strNeto", Convert.ToDecimal(neto));
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