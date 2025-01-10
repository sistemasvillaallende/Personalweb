using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MesaWeb.impresiones
{
  public partial class WebForm1 : System.Web.UI.Page
  {

    ReportDocument customerReport;
    int anio = 0;
    int nro_expediente = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      // if (Request.Cookies["UserSueldos"]["usuario"].ToString() == null)
      //{
      //  Session.Remove("anio");
      //  Session.Remove("nro_expediente");
      //  ClosePage();
      //}
      //else
      // if (!Page.IsPostBack)
      //{
      //  anio = Convert.ToInt16(Session["anio"]);
      //  nro_expediente = Convert.ToInt16(Session["nro_expediente"]);

      //  if (anio > 0 && nro_expediente > 0)
      //    ConfigureCrystalReports();
      //  else
      //    ClosePage();

      //}
    }



    //private void ConfigureCrystalReports()
    //{

    //  customerReport = new ReportDocument();
    //  string strBarra = "";
    //  string reportPath = "";


    //  Expediente oExp = new Expediente();
    //  ExpedienteB oExpBLL = new ExpedienteB();

    //  try
    //  {

    //    oExp = oExpBLL.GetExpedienteByPk(anio, nro_expediente);
    //    reportPath = Server.MapPath("../reportes/rconstancia.rpt");
    //    customerReport.PrintOptions.PaperSize = PaperSize.PaperLegal;

    //    customerReport.Load(reportPath);

    //    //anio = "2010"; nro_expediente = "500005";
    //    strBarra = nro_expediente.ToString() + anio.ToString();
    //    strBarra = "*" + string.Format(strBarra) + "*";



    //    customerReport.SetParameterValue("strCodigo", strBarra);
    //    customerReport.SetParameterValue("strNro_expediente", strBarra);
    //    customerReport.SetParameterValue("strFechaIngreso", oExp.fecha_ingreso);
    //    customerReport.SetParameterValue("strTelefono", ConfigurationManager.AppSettings["Telefonos"]);
    //    customerReport.SetParameterValue("strEmail", ConfigurationManager.AppSettings["Email"]);
    //    //customerReport.SetParameterValue("strUsuario", Convert.ToString(Request.Cookies["UserSueldos"]["usuario"].ToString()));


    //    CrystalReportViewer1.ReportSource = customerReport;

    //    customerReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "");
    //    //PrintPDF();
    //  }



    //  catch (Exception ex)
    //  {
    //    System.Console.WriteLine("Error, no se pudo generar el Reporte " + ex.Message);
    //    Response.Write("Hubo problemas con la Caratula de Expediente, no se pudo generar el Reporte...");
    //  }
    //  finally
    //  {
    //    oExp = null;
    //    oExpBLL = null;
    //  }

    //}



    //private void PrintPDF()
    //{
    //  MemoryStream oStream;
    //  oStream = (MemoryStream)
    //  customerReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    //  Response.Clear();
    //  Response.Buffer = true;
    //  Response.ContentType = "application/pdf";
    //  Response.BinaryWrite(oStream.ToArray());
    //  Response.End();
    //}

    //private void ClosePage()
    //{
    //  Response.Write("<SCRIPT id=clientEventHandlersJS LANGUAGE=javascript>");
    //  Response.Write("window.close();");
    //  Response.Write("</SCRIPT>");
    //  Response.End();
    //}



  }
}