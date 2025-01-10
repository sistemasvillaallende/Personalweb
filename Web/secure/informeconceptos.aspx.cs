using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace web.secure
{
    public partial class informeconceptos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtAnio.Text = DateTime.Now.Year.ToString();
                clean();
                CargarCombos();
                txtAnio.Focus();
            }
        }
        private void clean()
        {
            TimeSpan hora;
            DateTime h;
            h = DateTime.Now;
            hora = new TimeSpan(0, 0, 0);
            //txtHora_desde.Text = hora.ToString();
            hora = new TimeSpan(h.Hour, h.Minute, 0);
            //txtHora_hasta.Text = hora.ToString();
            //lblCantReg.Text = "0";
            //lblTotal.Text = " 0";
        }
        protected void CargarCombos()
        {
            cmbTipo_liq.DataTextField = "des_tipo_liq";
            cmbTipo_liq.DataValueField = "cod_tipo_liq";
            cmbTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            cmbTipo_liq.DataBind();

            ddConcepto.DataTextField = "des_concepto_liq";
            ddConcepto.DataValueField = "cod_concepto_liq";
            ddConcepto.DataSource = BLL.Concepto_liqB.GetConceptos_liq();
            ddConcepto.DataBind();

            ddLegajos.DataTextField = "nombre";
            ddLegajos.DataValueField = "legajo";
            ddLegajos.DataSource = BLL.EmpleadoB.GetEmpleadosAll();
            ddLegajos.DataBind();
        }
        protected void cmbTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem itemInicial = new ListItem();
            itemInicial.Text = "Seleccione Liquidacion";
            itemInicial.Value = "0";
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(cmbTipo_liq.SelectedValue);
            cmbNro_liq.Items.Clear();
            cmbNro_liq.Items.Add(itemInicial);
            cmbNro_liq.DataTextField = "des_liquidacion";
            cmbNro_liq.DataValueField = "nro_liquidacion";
            cmbNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
            cmbNro_liq.DataBind();
            cmbNro_liq.Focus();
        }
        //
        protected void cmdConsulta_Click(object sender, EventArgs e)
        {
            string switch_on = "";
            int anio = Convert.ToInt32(txtAnio.Text);
            int id_tipo_liq = Convert.ToInt32(cmbTipo_liq.SelectedValue) == 0 ? 1 : Convert.ToInt32(cmbTipo_liq.SelectedValue);
            int nro_liquidacion = Convert.ToInt32(cmbNro_liq.SelectedValue) == 0 ? 0 : Convert.ToInt32(cmbNro_liq.SelectedValue);
            int cod_concepto = Convert.ToInt32(ddConcepto.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddConcepto.SelectedValue);
            int legajo = Convert.ToInt32(ddLegajos.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddLegajos.SelectedValue);

            try
            {

                if (string.IsNullOrEmpty(txtAnio.Text))
                    return;
                if (id_tipo_liq > 0 && nro_liquidacion > 0 && cod_concepto > 0 && legajo > 0)
                    switch_on = "1";
                if (id_tipo_liq > 0 && nro_liquidacion > 0 && cod_concepto > 0 && switch_on == string.Empty)
                    switch_on = "2";
                if (id_tipo_liq > 0 && nro_liquidacion > 0 && legajo > 0 && switch_on == string.Empty)
                    switch_on = "3";
                if (id_tipo_liq > 0 && cod_concepto > 0 && legajo > 0 && switch_on == string.Empty)
                    switch_on = "4";
                if (id_tipo_liq > 0 && cod_concepto > 0 && switch_on == string.Empty)
                    switch_on = "5";
                if (id_tipo_liq > 0 && legajo > 0 && switch_on == string.Empty)
                    switch_on = "6";
                switch (switch_on)
                {
                    case "1":
                        InformexLiqxConceptoxLegajo(anio, id_tipo_liq, nro_liquidacion, cod_concepto, legajo);
                        break;
                    case "2":
                        InformexLiqxConcepto(anio, id_tipo_liq, nro_liquidacion, cod_concepto);
                        break;
                    case "3":
                        InformexLiqxLegajo(anio, id_tipo_liq, nro_liquidacion, legajo);
                        break;
                    case "4":
                        InformexConceptoxLegajo(anio, id_tipo_liq, cod_concepto, legajo);
                        break;
                    case "5":
                        InformexConcepto(anio, id_tipo_liq, cod_concepto);
                        break;
                    case "6":
                        InformexLegajo(anio, id_tipo_liq, legajo);
                        break;
                    default:
                        Informe(anio, id_tipo_liq, nro_liquidacion);
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void InformexConceptoxLegajo(int anio, int id_tipo_liq, int cod_concepto, int legajo)
        {
            List<reportes.Conceptos_fijosRep> lst = new List<reportes.Conceptos_fijosRep>();
            lst = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_liq(anio, id_tipo_liq);
            if (cod_concepto > 0)
            {
                GridView1.DataSource = lst.Where(ent => ent.cod_concepto_liq == cod_concepto && ent.legajo == legajo);
            }
            else
                GridView1.DataSource = lst;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void InformexConcepto(int anio, int id_tipo_liq, int cod_concepto)
        {
            List<reportes.Conceptos_fijosRep> lst = new List<reportes.Conceptos_fijosRep>();
            lst = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_liq(anio, id_tipo_liq);
            if (cod_concepto > 0)
            {
                GridView1.DataSource = lst.Where(ent => ent.cod_concepto_liq == cod_concepto);
            }
            else
                GridView1.DataSource = lst;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void InformexLegajo(int anio, int id_tipo_liq, int legajo)
        {
            List<reportes.Conceptos_fijosRep> lst = new List<reportes.Conceptos_fijosRep>();
            lst = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_liq(anio, id_tipo_liq);
            if (legajo > 0)
            {
                GridView1.DataSource = lst.Where(ent => ent.legajo == legajo);
            }
            else
                GridView1.DataSource = lst;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void InformexLiqxLegajo(int anio, int id_tipo_liq, int nro_liquidacion, int legajo)
        {
            List<reportes.Conceptos_fijosRep> lst = new List<reportes.Conceptos_fijosRep>();
            lst = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_Nro_liq(anio, id_tipo_liq, nro_liquidacion);
            if (legajo > 0)
            {
                GridView1.DataSource = lst.Where(ent => ent.legajo == legajo);
            }
            else
                GridView1.DataSource = lst;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void InformexLiqxConcepto(int anio, int id_tipo_liq, int nro_liquidacion, int cod_concepto)
        {
            List<reportes.Conceptos_fijosRep> lstConceptos = new List<reportes.Conceptos_fijosRep>();
            lstConceptos = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_Nro_liq(anio, id_tipo_liq, nro_liquidacion);
            if (cod_concepto > 0)
            {
                GridView1.DataSource = lstConceptos.Where(ent => ent.cod_concepto_liq == cod_concepto);
            }
            else
                GridView1.DataSource = lstConceptos;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        private void Informe(int anio, int id_tipo_liq, int nro_liquidacion)
        {
            List<reportes.Conceptos_fijosRep> lstConceptos = new List<reportes.Conceptos_fijosRep>();
            lstConceptos = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_Nro_liq(anio, id_tipo_liq, nro_liquidacion);
            GridView1.DataSource = lstConceptos;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void InformexLiqxConceptoxLegajo(int anio, int id_tipo_liq, int nro_liquidacion, int cod_concepto, int legajo)
        {
            List<reportes.Conceptos_fijosRep> lstConceptos = new List<reportes.Conceptos_fijosRep>();
            //List<reportes.Conceptos_fijosRep> lstResultado = new List<reportes.Conceptos_fijosRep>();
            lstConceptos = reportes.Conceptos_fijosRep.readResumen_cptosAnio_Tipo_Nro_liq(anio, id_tipo_liq, nro_liquidacion);
            if (cod_concepto > 0)
            {
                GridView1.DataSource = lstConceptos.Where(ent => ent.cod_concepto_liq == cod_concepto && ent.legajo == legajo);
            }
            else
                GridView1.DataSource = lstConceptos;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("../secure/home.aspx");
        }

        protected void cmdExportar_Click(object sender, EventArgs e)
        {

        }

        protected void txtLegajo_TextChanged(object sender, EventArgs e)
        {
            int legajo = 0;
            if (!string.IsNullOrEmpty(txtLegajo.Text))
            {
                legajo = Convert.ToInt32(txtLegajo.Text);
                ddLegajos.SelectedIndex =
                    ddLegajos.Items.IndexOf(ddLegajos.Items.FindByValue(txtLegajo.Text));
            }
        }
        protected void ddLegajos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int legajo = Convert.ToInt32(ddLegajos.SelectedValue);
            txtLegajo.Text = Convert.ToString(legajo);
        }
        protected void txtCod_concepto_TextChanged(object sender, EventArgs e)
        {
            int cod_concepto = 0;
            if (!string.IsNullOrEmpty(txtCod_concepto.Text))
            {
                cod_concepto = Convert.ToInt32(txtCod_concepto.Text);
                ddConcepto.Items.FindByValue(txtCod_concepto.Text);
                ddConcepto.SelectedIndex =
                  ddConcepto.Items.IndexOf(ddConcepto.Items.FindByValue(txtCod_concepto.Text));
            }
        }
        protected void ddConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod_concepto = Convert.ToInt32(ddConcepto.SelectedValue);
            txtCod_concepto.Text = Convert.ToString(cod_concepto);
        }
    }
}











//var informe = nav.Select("/Productos/Producto");
//while (informe.MoveNext())
//{
//    var location = informe.Current;
//    //var book = new Book();
//    //book.Id = location.GetAttribute("id", "");

//    //if (!parseChildNodeValues) continue;

//    //book.Title = location.SelectSingleNode("title").Value;
//    //book.Genre = location.SelectSingleNode("genre").Value;
//    //book.Price = location.SelectSingleNode("price").Value;
//    //book.PublishDate = location.SelectSingleNode("publish_date").Value;
//    //book.Author = location.SelectSingleNode("author").Value;

//    totalnav += Convert.ToDouble(location.SelectSingleNode("total").Value);
//    Console.WriteLine(totalnav.ToString());
//}
//Console.WriteLine(totalnav.ToString());