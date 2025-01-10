using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.reportes
{
    public partial class reportes : System.Web.UI.Page
    {
        ReportDocument reporte = new ReportDocument();
        CrystalDecisions.Web.CrystalReportViewer crview = new CrystalDecisions.Web.CrystalReportViewer();
        string nomreporte = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["reporte"] != null)
                    nomreporte = Convert.ToString(Request.QueryString["reporte"]);
            }
            switch (nomreporte)
            {
                case "NominaPersonal":
                    NominaPersonal();
                    break;
                case "NominaEmpxSeccion":
                    NominaEmpxSeccion();
                    break;
                case "CuentaporConceptos":
                    CuentaporConceptos();
                    break;
                case "CuentaSueldoyGtos":
                    CuentaSueldoyGtos();
                    break;
                default:
                    break;
            }
        }

        private void CuentaSueldoyGtos()
        {
            List<Cuentas_Sueldo_Gto> lst = new List<Cuentas_Sueldo_Gto>();
            lst = web.reportes.Cuentas_Sueldo_Gto.read();
            reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
            reporte.Load(Server.MapPath("../reportes/repCuenta_Sueldo_Bco_Gtos.rpt"));
            reporte.SetDataSource(lst);
            string usuario = Session["usuario"].ToString();
            //Request.Cookies["UserSueldos"]["usuario"].ToString().ToString();
            reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
            crview.ReportSource = reporte;
            crview.RefreshReport();
            crview.DataBind();
            reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");
        }

        private void CuentaporConceptos()
        {
            List<Cuentas_x_cpto> lst = new List<Cuentas_x_cpto>();
            lst = web.reportes.Cuentas_x_cpto.read();
            reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
            reporte.Load(Server.MapPath("../reportes/repCuentas_x_cpto.rpt"));
            reporte.SetDataSource(lst);
            string usuario = Session["usuario"].ToString();
            //Request.Cookies["UserSueldos"]["usuario"].ToString().ToString();
            reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
            crview.ReportSource = reporte;
            crview.RefreshReport();
            crview.DataBind();
            reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");
        }

        private void NominaEmpxSeccion()
        {
            List<NominaEmpxSeccion> lst = new List<NominaEmpxSeccion>();
            lst = web.reportes.NominaEmpxSeccion.readNominaEmpleado();
            reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
            reporte.Load(Server.MapPath("../reportes/repNominaEmpxSeccion.rpt"));
            reporte.SetDataSource(lst);
            string usuario = Session["usuario"].ToString();
            //Request.Cookies["UserSueldos"]["usuario"].ToString().ToString();
            reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
            crview.ReportSource = reporte;
            crview.RefreshReport();
            crview.DataBind();
            reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");
        }

        //
        private void NominaPersonal()
        {
            List<NominaEmpleados> lst = new List<NominaEmpleados>();
            lst = web.reportes.NominaEmpleados.readNominaEmpleado();
            reporte.PrintOptions.PaperSize = PaperSize.PaperA4;
            reporte.Load(Server.MapPath("../reportes/repNominaEmpleados.rpt"));
            reporte.SetDataSource(lst);
            string usuario = Session["usuario"].ToString();
            //Request.Cookies["UserSueldos"]["usuario"].ToString().ToString();
            reporte.SetParameterValue("strUsuario", "Impreso por el Usuario :" + usuario);
            crview.ReportSource = reporte;
            crview.RefreshReport();
            crview.DataBind();
            reporte.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DirectAccessReport");
        }




    }

}