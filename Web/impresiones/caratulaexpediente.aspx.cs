﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;



namespace MesaWeb.impresiones
{
  public partial class caratulaexpediente : System.Web.UI.Page
  {

    ReportDocument customerReport;
    int anio = 0;
    int nro_expediente = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      //if (Request.Cookies["UserSueldos"]["usuario"].ToString() == null)
      //{
      //  Session.Remove("anio");
      //  Session.Remove("nro_expediente");
      //  ClosePage();
      //}
      //else
      //if (!Page.IsPostBack)
      //{
      //  anio = Convert.ToInt16(Session["anio"]);
      //  nro_expediente = Convert.ToInt16(Session["nro_expediente"]);

      //  if (anio > 0 && nro_expediente > 0)
      //    ConfigureCrystalReports();
      //  else
      //    ClosePage();
    }




    //private void ConfigureCrystalReports()
    //{

    //  customerReport = new ReportDocument();
    //  string strBarra = "";
    //  string reportPath = "";


    //  Entities.Expediente oExp = new Entities.Expediente();
    //  BLL.ExpedienteB oExpBLL = new BLL.ExpedienteB();

    //  try
    //  {

    //    oExp = oExpBLL.GetExpedienteByPk(anio, nro_expediente);
    //    reportPath = Server.MapPath("../reportes/rcaratulaexpedienteoficio.rpt");
    //    customerReport.PrintOptions.PaperSize = PaperSize.PaperLegal;

    //    customerReport.Load(reportPath);

    //    //anio = "2010"; nro_expediente = "500005";
    //    strBarra = nro_expediente.ToString() + anio.ToString();
    //    strBarra = "*" + string.Format(strBarra) + "*";



    //    customerReport.SetParameterValue("strAnio", oExp.anio);
    //    customerReport.SetParameterValue("strExpediente", oExp.nro_expediente);
    //    customerReport.SetParameterValue("strFecha_ingreso", oExp.fecha_ingreso);

    //    customerReport.SetParameterValue("strCodigo", strBarra);
    //    customerReport.SetParameterValue("strIniciador", oExp.nombre);
    //    customerReport.SetParameterValue("strDomicilio", (string.IsNullOrEmpty(oExp.domicilio) ? "--" : oExp.domicilio));


    //    customerReport.SetParameterValue("strTelefonos", (string.IsNullOrEmpty(oExp.telefono) ? "--" : oExp.telefono));
    //    customerReport.SetParameterValue("strCelular", (string.IsNullOrEmpty(oExp.celular) ? "--" : oExp.celular));
    //    customerReport.SetParameterValue("strEmail", (string.IsNullOrEmpty(oExp.email) ? "--" : oExp.email));
    //    customerReport.SetParameterValue("strDestinatario", oExp.destino);
    //    customerReport.SetParameterValue("strAsunto", oExp.asunto);
    //    customerReport.SetParameterValue("strObservaciones", oExp.observaciones);
    //    customerReport.SetParameterValue("strFechaIngreso", oExp.fecha_ingreso);
    //    customerReport.SetParameterValue("strTelefono_oficina", ConfigurationManager.AppSettings["Telefonos"]);
    //    customerReport.SetParameterValue("strEmail_oficina", ConfigurationManager.AppSettings["Email"]);
    //    customerReport.SetParameterValue("strUsuario", Convert.ToString(Request.Cookies["UserSueldos"]["usuario"].ToString()));


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
    //  //oStream = (MemoryStream)
    //  //customerReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    //  //Response.Clear();
    //  //Response.Buffer = true;
    //  //Response.ContentType = "application/pdf";
    //  //Response.BinaryWrite(oStream.ToArray());
    //  //Response.End();
    //}

    //private void ClosePage()
    //{
    //  //Response.Write("<SCRIPT id=clientEventHandlersJS LANGUAGE=javascript>");
    //  //Response.Write("window.close();");
    //  //Response.Write("</SCRIPT>");
    //  //Response.End();
    //}


  }
}
