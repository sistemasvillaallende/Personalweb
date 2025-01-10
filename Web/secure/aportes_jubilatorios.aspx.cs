using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class aportes_jubilatorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtAnio.Text = DateTime.Now.Year.ToString();
                CargarCombos();
                txtAnio.Focus();
            }
        }

        public static SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["siimva"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(cmbTipo_liq.SelectedValue);
            cmbNro_liq.Items.Clear();
            cmbNro_liq.DataTextField = "des_liquidacion";
            cmbNro_liq.DataValueField = "nro_liquidacion";
            cmbNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
            cmbNro_liq.DataBind();
            cmbNro_liq.Focus();
        }

        protected void CargarCombos()
        {
            cmbTipo_liq.DataTextField = "des_tipo_liq";
            cmbTipo_liq.DataValueField = "cod_tipo_liq";
            cmbTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            cmbTipo_liq.DataBind();
        }

        protected void cmdConsulta_Click(object sender, EventArgs e)
        {

            switch (rbOpcion.SelectedIndex)
            {
                case 0:
                    {
                        Aportes_Jubilatorio(GridView1);
                        if (GridView1.Rows.Count > 0)
                        {
                            GridView1.UseAccessibleHeader = true;
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                case 1:
                    {
                        Aportes_Jubilatorio_x_sec(GridView1);
                        if (GridView1.Rows.Count > 0)
                        {
                            GridView1.UseAccessibleHeader = true;
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;
                case 2:
                    {
                        Aporte_Tarea_Ins(GridView1);
                        if (GridView1.Rows.Count > 0)
                        {
                            GridView1.UseAccessibleHeader = true;
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    break;

                default:
                    break;
            }

        }

        protected void Aportes_Jubilatorio(GridView gvPivot)
        {
            try
            {
                SqlDataReader dr;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_APORTES_JUBILATORIOS";
                    cmd.Parameters.AddWithValue("@anio", txtAnio.Text);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", Convert.ToInt32(cmbTipo_liq.SelectedValue));
                    cmd.Parameters.AddWithValue("@nro_liquidacion", Convert.ToInt32(cmbNro_liq.SelectedValue));
                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();
                    gvPivot.DataSource = dr;
                    gvPivot.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Aportes_Jubilatorio_x_sec(GridView gvPivot)
        {
            try
            {
                SqlDataReader dr;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_APORTES_JUBILATORIOS_x_sec";
                    cmd.Parameters.AddWithValue("@anio", txtAnio.Text);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", Convert.ToInt32(cmbTipo_liq.SelectedValue));
                    cmd.Parameters.AddWithValue("@nro_liquidacion", Convert.ToInt32(cmbNro_liq.SelectedValue));
                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();
                    gvPivot.DataSource = dr;
                    gvPivot.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Aporte_Tarea_Ins(GridView gv)
        {
            try
            {
                SqlDataReader dr;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_APORTES_TAREA_INSALUBRE";
                    cmd.Parameters.AddWithValue("@anio", txtAnio.Text);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", Convert.ToInt32(cmbTipo_liq.SelectedValue));
                    cmd.Parameters.AddWithValue("@nro_liquidacion", Convert.ToInt32(cmbNro_liq.SelectedValue));
                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();
                    gv.DataSource = dr;
                    gv.DataBind();
                    //if (GridView1.Rows.Count > 0)
                    //{
                    //  GridView1.UseAccessibleHeader = true;
                    //  GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            GridView gv = new GridView();
            //Aportes_Jubilatorio(gv);
            //ExportToExcel("Pivot", gv);
            switch (rbOpcion.SelectedIndex)
            {
                case 0:
                    {
                        Aportes_Jubilatorio(gv);
                        ExportToExcel("Pivot", gv);
                    }
                    break;
                case 1:
                    {
                        Aportes_Jubilatorio_x_sec(gv);
                        ExportToExcel("Pivot", gv);
                    }
                    break;
                case 2:
                    {
                        Aporte_Tarea_Ins(gv);
                        ExportToExcel("Pivot", gv);
                    }
                    break;

                default:
                    break;
            }
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

        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }


    }
}