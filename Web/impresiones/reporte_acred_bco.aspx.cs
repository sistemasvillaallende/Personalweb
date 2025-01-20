using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace web.secure
{
    public partial class reporte_acred_bco : System.Web.UI.Page
    {
        private ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crv = new CrystalDecisions.Web.CrystalReportViewer();
        int anio = 0;
        int cod_tipo_liq = 0;
        int nro_liquidacion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["desde"] == null)
            //    Response.Redirect("../secure/Home.aspx");
            //if (Request.QueryString["hasta"] == null)
            //    Response.Redirect("../secure/Home.aspx");
            //if (Request.QueryString["anio"] == null)
            //    Response.Redirect("../secure/Home.aspx");
            //if (Request.QueryString["cod_tipo_liq"] == null)
            //    Response.Redirect("../secure/Home.aspx");
            //if (Request.QueryString["nro_liq"] == null)
            //    Response.Redirect("../secure/Home.aspx");


            List<web.reportes.Acred_bco_cbaRep> lst = new List<web.reportes.Acred_bco_cbaRep>();
            lst = web.reportes.Acred_bco_cbaRep.read(Convert.ToInt32(Request.QueryString["anio"]),
                Convert.ToInt32(Request.QueryString["cod_tipo_liq"]), Convert.ToInt32(Request.QueryString["nro_liq"]),
                Convert.ToDecimal(Request.QueryString["porcentaje"]));

            anio = Convert.ToInt32((Request.QueryString["anio"]));
            cod_tipo_liq = Convert.ToInt32(Request.QueryString["cod_tipo_liq"]);
            nro_liquidacion = Convert.ToInt32(Request.QueryString["nro_liq"]);

            Entities.Liquidacion oLiq = BLL.LiquidacionesB.getByPk(anio, cod_tipo_liq, nro_liquidacion);

            string titulo_liq = oLiq.des_liquidacion;
            ConfigureCrystalReports(lst, titulo_liq);


        }
        private void ConfigureCrystalReports(List<web.reportes.Acred_bco_cbaRep> lst, string titulo_liq)
        {
            try
            {
                reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
                reporte.Load(Server.MapPath("../reportes/racred_bco_cba.rpt"));
                reporte.SetDataSource(lst);
                string usuario = Session["usuario"].ToString();
                reporte.SetParameterValue("strTitulo_liquidacion", "LIQUIDACION CORRESPONDIENTE AL MES " + titulo_liq);
                reporte.SetParameterValue("strUsuario", "Impreso por el Usuario : " + usuario);
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