﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace web.secure
{
    public partial class certificaciones : System.Web.UI.Page
    {
        int legajo = 0;
        string nombre = "";
        string operacion = "";
        List<Entities.Certificaciones> lstDetalle = new List<Entities.Certificaciones>();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            operacion = Convert.ToString(Request.QueryString["op"]);
            legajo = Convert.ToInt16(Request.QueryString["legajo"]);
            nombre = Convert.ToString(Request.QueryString["nombre"]);

            if (!Page.IsPostBack)
            {
                AsignarDatos();
            }
        }



        protected void AsignarDatos()
        {
            txtNombre.Text = nombre;
            txtLegajo.Text = legajo.ToString();
        }


        private void Movimientos()
        {

            System.Xml.XmlTextReader reader;
            StringReader xmlData =
              new StringReader(BLL.EmpleadoB.Certficaciones_Empleado(Convert.ToInt32(txtLegajo.Text)));
            reader = new XmlTextReader(xmlData);
            XPathDocument xpathDoc = new XPathDocument(reader);
            string strVD = ConfigurationManager.AppSettings["VD"];
            string SiteRoot = Context.Server.MapPath("\\" + strVD);
            string xslPath = SiteRoot + "\\" + ConfigurationManager.AppSettings["XSLPath"] + "\\Certificaciones_empleado.xsl";
            //Xml1.XPathNavigator = xpathDoc.CreateNavigator();
            //Xml1.TransformSource = xslPath;
        }


        private void FillGrid()
        {
            lstDetalle = BLL.EmpleadoB.GetCertificaciones(Convert.ToInt32(txtLegajo.Text));
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
        }



        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            FillGrid();
        }


        private void ExportToExcel(string nameReport, GridView wControl)
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
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=DATA.xls");
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }

        protected void btnExporCtaCte_Click(object sender, EventArgs e)
        {
            ExportToExcel("Novedades", gvDetalle);
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("empleado.aspx?legajo={0}&nombre={1}&op={2}", txtLegajo.Text, txtNombre.Text, operacion));
        }
    }
}