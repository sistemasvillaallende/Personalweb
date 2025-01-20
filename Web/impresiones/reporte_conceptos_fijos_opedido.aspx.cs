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
    public partial class reporte_conceptos_fijos_opedido : System.Web.UI.Page
    {
        //
        private ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crv = new CrystalDecisions.Web.CrystalReportViewer();
        int anio = 0;
        int cod_tipo_liq = 0;
        int nro_liquidacion = 0;
        int fijo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<web.reportes.Conceptos_fijosRep> lst = new List<web.reportes.Conceptos_fijosRep>();
            anio = Convert.ToInt32((Request.QueryString["anio"]));
            cod_tipo_liq = Convert.ToInt32(Request.QueryString["cod_tipo_liq"]);
            nro_liquidacion = Convert.ToInt32(Request.QueryString["nro_liq"]);
            fijo = Convert.ToInt32(Request.QueryString["fijo"]);
            lst = web.reportes.Conceptos_fijosRep.readResumen_cptos_opedido(anio, cod_tipo_liq, nro_liquidacion, fijo);
            Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);
            string titulo_liq = oLiq.des_liquidacion;
            string tipo_liq = oLiq.des_tipo_liq;
            ConfigureCrystalReports(lst, titulo_liq, tipo_liq, fijo);
        }


        private void ConfigureCrystalReports(List<web.reportes.Conceptos_fijosRep> lst, string titulo_liq, string tipo_liq, int fijo)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
                reporte.Load(Server.MapPath("../reportes/rconceptos_fijos.rpt"));
                reporte.SetDataSource(lst);
                reporte.SetParameterValue("strMes_liquidacion", "LIQUIDACION CORRESPONDIENTE AL MES " + titulo_liq);
                reporte.SetParameterValue("strTipo_liquidacion", "TIPO LIQUIDACION : " + tipo_liq);
                reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
                if (fijo == 1)
                    reporte.SetParameterValue("strTitulo", "CONCEPTOS VARIABLES - O.PEDIDO");
                else
                    reporte.SetParameterValue("strTitulo", "CONCEPTOS FIJOS - O.PEDIDO");
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
        //
    }
}