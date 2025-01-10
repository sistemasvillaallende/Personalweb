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
    public partial class reporte_resumen_cptos : System.Web.UI.Page
    {
        private ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crv = new CrystalDecisions.Web.CrystalReportViewer();
        int anio = 0;
        int cod_tipo_liq = 0;
        int nro_liquidacion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<web.reportes.Conceptos_liqRep> lst = new List<web.reportes.Conceptos_liqRep>();
            lst = web.reportes.Conceptos_liqRep.readResumen_cptos(Convert.ToInt32(Request.QueryString["anio"]),
                Convert.ToInt32(Request.QueryString["cod_tipo_liq"]), Convert.ToInt32(Request.QueryString["nro_liq"]));

            anio = Convert.ToInt32((Request.QueryString["anio"]));
            cod_tipo_liq = Convert.ToInt32(Request.QueryString["cod_tipo_liq"]);
            nro_liquidacion = Convert.ToInt32(Request.QueryString["nro_liq"]);

            Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);


            string titulo_liq = oLiq.des_liquidacion;
            string tipo_liq = oLiq.des_tipo_liq;
            ConfigureCrystalReports(lst, titulo_liq, tipo_liq);

        }

        private void ConfigureCrystalReports(List<web.reportes.Conceptos_liqRep> lst, string titulo_liq, string tipo_liq)
        {
            int total_legajos = 0;
            decimal total_neto = 0;
            try
            {
                foreach (var item in lst)
                {
                    if (item.cod_concepto_liq == 10)
                        total_legajos = item.unidad;
                    total_neto += item.haber_cd + item.haber_sd - item.descuento;

                }
                reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
                reporte.Load(Server.MapPath("../reportes/rresumen_cptos.rpt"));
                reporte.SetDataSource(lst);
                string usuario = Session["usuario"].ToString();
                reporte.SetParameterValue("strMes_liquidacion", "LIQUIDACION CORRESPONDIENTE AL MES " + titulo_liq);
                reporte.SetParameterValue("strTipo_liquidacion", "TIPO LIQUIDACION : " + tipo_liq);
                reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
                reporte.SetParameterValue("strTotal_legajos", Convert.ToString(total_legajos));
                reporte.SetParameterValue("strTotal_neto", Convert.ToDecimal(total_neto));
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