using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class list_personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //
        protected void toSettings_ServerClick(object sender, EventArgs e)
        {

        }

        protected void toNominaPersonal_ServerClick(object sender, EventArgs e)
        {
            divReporte.InnerHtml = "<iframe src=\" " +
                string.Format("../reportes/reportes.aspx?reporte={0}", "NominaPersonal") + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void toPersonalExcel_ServerClick(object sender, EventArgs e)
        {
            //List<web.reportes.NominaEmpleados> lst = new List<reportes.NominaEmpleados>();
            //GridView GridtoExcel = new GridView();
            //lst = web.reportes.NominaEmpleados.readNominaEmpleado();
            //GridtoExcel.DataSource = lst;
            //GridtoExcel.DataBind();
            //DescargarDocumentoExcel("ReporteNominaEmpleadoActivos.xls", GridtoExcel);
            List<web.reportes.NominaEmpleados> lst = web.reportes.NominaEmpleados.readNominaEmpleado();
            if (lst != null && lst.Count > 0)
            {
                GridView GridtoExcel = new GridView();
                GridtoExcel.DataSource = lst;
                GridtoExcel.DataBind();
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                string nombreArchivo = $"ReporteNominaEmpleadoActivos_{fecha}.xls";
                ExportarAExcel(nombreArchivo, GridtoExcel);
            }
        }

        protected void toPersonalExcelTodos_ServerClick(object sender, EventArgs e)
        {
            //List<web.reportes.NominaEmpleados> lst = new List<reportes.NominaEmpleados>();
            //GridView GridtoExcel = new GridView();
            //lst = web.reportes.NominaEmpleados.readNominaEmpleadoTodos();
            //GridtoExcel.DataSource = lst;
            //GridtoExcel.DataBind();
            //DescargarDocumentoExcel("ReporteNominaEmpleadoTodos.xls", GridtoExcel);

            List<web.reportes.NominaEmpleados> lst = web.reportes.NominaEmpleados.readNominaEmpleadoTodos();
            if (lst != null && lst.Count > 0)
            {
                GridView GridtoExcel = new GridView();
                GridtoExcel.DataSource = lst;
                GridtoExcel.DataBind();
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                string nombreArchivo = $"ReporteNominaEmpleadoTodos_{fecha}.xls";
                ExportarAExcel(nombreArchivo, GridtoExcel);
            }

        }

        protected void toPersonalxSeccion_ServerClick(object sender, EventArgs e)
        {
            divReporte.InnerHtml = "<iframe src=\" " +
             string.Format("../reportes/reportes.aspx?reporte={0}", "NominaEmpxSeccion") + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }

        protected void toPersonalxSeccionExcel_ServerClick(object sender, EventArgs e)
        {
            //List<web.reportes.NominaEmpxSeccion> lst = new List<reportes.NominaEmpxSeccion>();
            //GridView GridtoExcel = new GridView();
            //lst = web.reportes.NominaEmpxSeccion.readNominaEmpleado();
            //GridtoExcel.DataSource = lst;
            //GridtoExcel.DataBind();
            //DescargarDocumentoExcel("ReporteNominaEmpxSeccion.xls", GridtoExcel);

            List<web.reportes.NominaEmpxSeccion> lst = web.reportes.NominaEmpxSeccion.readNominaEmpleado();
            if (lst != null && lst.Count > 0)
            {
                GridView GridtoExcel = new GridView();
                GridtoExcel.DataSource = lst;
                GridtoExcel.DataBind();
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                string nombreArchivo = $"ReporteNominaEmpxSeccion_{fecha}.xls";
                ExportarAExcel(nombreArchivo, GridtoExcel);
            }
        }

        protected void btnCloseListado_ServerClick(object sender, EventArgs e)
        {
            popUpListado.Hide();
        }
        //
        //
        private void DescargarDocumentoExcel(string nameReport, GridView wControl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            wControl.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);
            form.Controls.Add(wControl);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport);

            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }

        protected void ToCuentaporConceptos_ServerClick(object sender, EventArgs e)
        {
            divReporte.InnerHtml = "<iframe src=\" " +
               string.Format("../reportes/reportes.aspx?reporte={0}", "CuentaporConceptos") + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();

        }

        protected void toCuentaSueldoyGtos_ServerClick(object sender, EventArgs e)
        {
            divReporte.InnerHtml = "<iframe src=\" " +
               string.Format("../reportes/reportes.aspx?reporte={0}", "CuentaSueldoyGastos") + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();

        }

        //protected void toNominaArt_ServerClick(object sender, EventArgs e)
        //{
        //    List<web.reportes.NominaArt> lst = new List<reportes.NominaArt>();
        //    GridView GridtoExcel = new GridView();
        //    lst = web.reportes.NominaArt.read();
        //    GridtoExcel.DataSource = lst;
        //    GridtoExcel.DataBind();
        //    DescargarDocumentoExcel("ReporteNominaArt.xls", GridtoExcel);
        //}

        protected void toNominaArt_ServerClick(object sender, EventArgs e)
        {
            List<web.reportes.NominaArt> lst = web.reportes.NominaArt.read();

            if (lst != null && lst.Count > 0)
            {
                GridView GridtoExcel = new GridView();
                GridtoExcel.DataSource = lst;
                GridtoExcel.DataBind();
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                string nombreArchivo = $"ReporteNominaArt_{fecha}.xls";
                ExportarAExcel(nombreArchivo, GridtoExcel);
            }
        }

        private void ExportarAExcel(string nombreArchivo, GridView grid)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // Si usás estilos de bootstrap, estos podrían no verse bien en Excel, por eso limpiar.
                grid.AllowPaging = false;
                grid.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }




    }
}