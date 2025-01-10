using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class reporteSueldos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtAnio.Text = DateTime.Now.Year.ToString();
                    CargarCombos();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void CargarCombos()
        {
            txtTipo_liq.DataTextField = "des_tipo_liq";
            txtTipo_liq.DataValueField = "cod_tipo_liq";
            txtTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            txtTipo_liq.DataBind();

        }
        protected void txtTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedValue);
            txtNro_liq.Items.Clear();
            txtNro_liq.DataTextField = "des_liquidacion";
            txtNro_liq.DataValueField = "nro_liquidacion";
            txtNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
            txtNro_liq.DataBind();
            txtNro_liq.Focus();
        }
        protected void txtNro_liq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                /*int desde, int hasta, int anio, int cod_tipo_liq, int nro_liq*/
                //Response.Redirect(string.Format("../reportes/printSueldos.aspx?desde={0}&hasta={1}&anio={2}&cod_tipo_liq={3}&nro_liq={4}",
                //txtDesde.Text, txtHasta.Text, txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value));
                divReporte.InnerHtml = "<iframe src=\" " +
                string.Format("../reportes/printSueldos.aspx?desde={0}&hasta={1}&anio={2}&cod_tipo_liq={3}&nro_liq={4}",
                txtDesde.Text, txtHasta.Text, txtAnio.Text, txtTipo_liq.SelectedItem.Value, txtNro_liq.SelectedItem.Value)
                + "\"  width=\"100%\" height=\"600\"></iframe>";
                popUpListado.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCloseListado_ServerClick(object sender, EventArgs e)
        {
            popUpListado.Hide();
        }
    }
}