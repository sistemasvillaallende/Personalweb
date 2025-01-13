using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class DesempenioSec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.Cookies["UserSistema"] != null)
                {
                    if (Request.Cookies["UserSistema"]["secretaria"] != null)
                        lblSecretaria.InnerHtml =
                            Request.Cookies["UserSistema"]["secretaria"];
                }
                HtmlAnchor liDesempenio =
    (HtmlAnchor)Master.FindControl("liDesempenio");
                if (liDesempenio != null)
                    liDesempenio.Attributes.Add("class", "active");
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
                if (Request.Cookies["UserSistema"]["id_secretaria"] != null)
                {
                    CargarGrilla(
                        int.Parse(Request.Cookies["UserSistema"]["id_secretaria"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarGrilla(int id_secretaria)
        {
            try
            {
                grdList.DataSource = DAL.ConsultaEmpleadoD.GetDirectoresBySecretarias(
                    id_secretaria, 2023);
                grdList.DataBind();
                if (grdList.Rows.Count > 0)
                {
                    grdList.UseAccessibleHeader = true;
                    grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                gvDirectos.DataSource = DAL.ConsultaEmpleadoD.GetEmpleadosDirectosBySecretarias(
    id_secretaria, 2023);
                gvDirectos.DataBind();
                if (gvDirectos.Rows.Count > 0)
                {
                    gvDirectos.UseAccessibleHeader = true;
                    gvDirectos.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void gvDirectos_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnDirecciones_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format(
                    "DesempenioDirecciones.aspx?id_secretaria={0}",
                    Request.Cookies["UserSistema"]["id_secretaria"]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}