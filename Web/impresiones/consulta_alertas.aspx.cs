using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web.impresiones
{
  public partial class consulta_alertas : System.Web.UI.Page
  {
    ReportDocument reporte = new ReportDocument();
    CrystalReportViewer crv = new CrystalReportViewer();

    string fecha_desde = "";
    string fecha_hasta = "";
    string usuario = "";
    int cod_desde = 0;
    int cod_hasta = 0;
    string tipo = String.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Request.QueryString["fecha_desde"] != null)
          fecha_desde = Convert.ToString(Request.QueryString["fecha_desde"]);


        if (Request.QueryString["fecha_hasta"] != null)
          fecha_hasta = Convert.ToString(Request.QueryString["fecha_hasta"]);

        if (Request.QueryString["cod_desde"] != null)
          cod_desde = Convert.ToInt16(Request.QueryString["cod_desde"]);

        if (Request.QueryString["cod_hasta"] != null)
          cod_hasta = Convert.ToInt16(Request.QueryString["cod_hasta"]);

        if (Request.QueryString["tipo"] != null)
          tipo = Convert.ToString(Request.QueryString["tipo"]);


      }


      reporte.Load(Server.MapPath("~/reportes/ralertas.rpt"));
      List<reportes.clsAlertas> lstExp = new List<reportes.clsAlertas>();


      if (tipo == "origen")
        lstExp = reportes.clsAlertas.GetAlertasByOficinaOrigen(fecha_desde, fecha_hasta, cod_desde, cod_hasta);
      else
        lstExp = reportes.clsAlertas.GetAlertasByOficinaDestino(fecha_desde, fecha_hasta, cod_desde, cod_hasta);

      reporte.SetDataSource(lstExp);

      usuario = Request.Cookies["UserSueldos"]["usuario"].ToString().ToString();
      reporte.SetParameterValue("strFechas", "Fechas Desde : " + fecha_desde + " Hasta : " + fecha_hasta);
      reporte.SetParameterValue("strUsuario", "Impreso por el Usuario : " + usuario);


      crv.ReportSource = reporte;
      crv.RefreshReport();
      crv.DataBind();

      reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "PersonDetails");


    }


    private void ClosePage()
    {
      Response.Write("<SCRIPT id=clientEventHandlersJS LANGUAGE=javascript>");
      Response.Write("window.close();");
      Response.Write("</SCRIPT>");
      Response.End();
    }

  }
}