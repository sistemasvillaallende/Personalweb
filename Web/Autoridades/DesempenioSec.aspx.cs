using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades
{
    public partial class DesempenioSec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                DAL.Secretarias_grilla objSec =
                    DAL.Secretarias_grilla.getByPk(
                        int.Parse(Request.QueryString["idSec"]), DateTime.Now.Year -1);
                lblSec.InnerHtml = objSec.SECRETARIA;
                DDLEvaluaciones.DataTextField = "NOMBRE";
                DDLEvaluaciones.DataValueField = "ID";
                DDLEvaluaciones.DataSource =
                    DAL.Fichas.Ficha.read().FindAll(
                        f => f.EJERCICIO == DateTime.Now.Year-1);
                DDLEvaluaciones.DataBind();


                BindList();
            }
        }
        private void BindList()
        {
            List<DAL.Fichas.Resultado_evaluacion> lst =
DAL.Fichas.Resultado_evaluacion.read(7);
            System.Threading.Thread.Sleep(1000);
            try
            {
                if (Request.QueryString["idSec"] != null)
                {
                    CargarGrilla(
                        int.Parse(Request.QueryString["idSec"]),
                        7);
                    //int.Parse(Request.Cookies["UserSistema"]["id_secretaria"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarGrilla(int id_secretaria, int idFicha)
        {
            try
            {
                grdList.DataSource = DAL.ConsultaEmpleadoD.GetDirectoresBySecretarias(
                    id_secretaria, DateTime.Now.Year, idFicha);
                grdList.DataBind();
                if (grdList.Rows.Count > 0)
                {
                    grdList.UseAccessibleHeader = true;
                    grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                gvDirectos.DataSource = DAL.ConsultaEmpleadoD.GetEmpleadosDirectosBySecretarias(
    id_secretaria, DateTime.Now.Year);
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
                    "EvaluacionSec.aspx?legajo={0}&idFicha={1}&idSec={2}",
                    objEmpleado.legajo, DDLEvaluaciones.SelectedItem.Value,
                    Request.QueryString["idSec"].ToString());
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

        protected void DDLEvaluaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}