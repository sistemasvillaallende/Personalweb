using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Direcciones
{
    public partial class Desempeño : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                HtmlGenericControl liDesempeño =
    (HtmlGenericControl)Master.FindControl("liDesempeño");
                if (liDesempeño != null)
                    liDesempeño.Attributes.Add("class", "active");
                List<DAL.Fichas.Ficha> lstFichas =
                    DAL.Fichas.Ficha.readActivas();
                if (lstFichas.Count() > 0)
                {
                    hIdEvaluacion.Value = lstFichas[0].ID.ToString();
                    lblFichaActiva.InnerHtml = lstFichas[0].NOMBRE;
                }
                BindList();
            }
        }
        private void BindList()
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                if (Request.Cookies["UserSistema"]["id_direccion"] != null)
                {
                    CargarGrilla(
                        int.Parse(Request.Cookies["UserSistema"]["id_direccion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarGrilla(int idDireccion)
        {
            try
            {
                grdList.DataSource = BLL.ConsultaEmpleadoB.GetEmpleadosByDireccion(
                    idDireccion);
                grdList.DataBind();
                if (grdList.Rows.Count > 0)
                {
                    grdList.UseAccessibleHeader = true;
                    grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la Busqueda!" + e.ToString());
                throw e;
            }
            finally
            {

            }
        }
        protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Entities.LstEmpleados objEmpleado =
                        (Entities.LstEmpleados)e.Row.DataItem;
                    HtmlAnchor btnEnviarEvaluacion =
                         (HtmlAnchor)e.Row.FindControl("btnEnviarEvaluacion");
                    HtmlGenericControl lblEstadoEvaluacion =
                        (HtmlGenericControl)e.Row.FindControl("lblEstadoEvaluacion");

                    lblEstadoEvaluacion.InnerHtml = objEmpleado.estadoEvaluacion;
                    lblEstadoEvaluacion.Attributes.Remove("class");
                    switch (objEmpleado.idEstadoEvaluacion)
                    {
                        case 0:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-danger");
                            break;
                        case 1:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-warning");
                            break;
                        case 2:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-info");
                            break;
                        case 3:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-success");
                            break;
                        case 4:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-danger");
                            break;
                        default:
                            break;
                    }

                    btnEnviarEvaluacion.HRef = string.Format(
                    "Evaluacion.aspx?legajo={0}&idFicha={1}",
                    objEmpleado.legajo, hIdEvaluacion.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}