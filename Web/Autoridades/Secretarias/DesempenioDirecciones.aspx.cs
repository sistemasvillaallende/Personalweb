using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class DesempenioDirecciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id_secretaria"] != null)
                {
                    DAL.Secretarias_grilla objSec =
                        DAL.Secretarias_grilla.getByPk(
                            int.Parse(Request.QueryString["id_secretaria"]),
                            2023);
                    List<DAL.Direcciones_grilla> lst =
                    DAL.Direcciones_grilla.read(DateTime.Now.Year,
                    int.Parse(Request.QueryString["id_secretaria"]));
                    gvSecretarias.DataSource = lst;
                    gvSecretarias.DataBind();
                    divSecretarias.Visible = true;
                    divDetalleSecretaria.Visible = false;
                    hIdSecretaria.Value = Request.QueryString["id_secretaria"];
                    lblSecretaria.InnerHtml =
                        string.Format(
                            "Evaluaciones de desempeño en {0}",
                            objSec.SECRETARIA);

                }
                else
                {
                    if (Request.QueryString["id_direccion"] != null)
                    {
                        hIdDireccion.Value = Request.QueryString["id_direccion"];

                        DAL.Direcciones_grilla objDir =
                            DAL.Direcciones_grilla.getByPk(
                                int.Parse(hIdDireccion.Value), DateTime.Now.Year);
                        hIdSecretaria.Value = objDir.id_secretaria.ToString();
                        lblSecretaria.InnerHtml = string.Format(
                            "Evaluaciones de desempeño en {0}",
                            objDir.DIRECCION);
                        lblDireccionConsultada.InnerHtml = objDir.DIRECCION;
                        divDetalleSecretaria.Visible = true;
                        divSecretarias.Visible = false;

                        CargarGrilla(int.Parse(hIdDireccion.Value));
                    }
                }
            }
        }

        private void CargarGrilla(int idDireccion)
        {
            try
            {
                gvDirectos.DataSource = BLL.ConsultaEmpleadoB.GetEmpleadosByDireccion(
                    idDireccion);
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
                    "Personas_fichas.aspx?legajo={0}&idFicha={1}",
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
                            btnEnviarEvaluacion.Visible = true;
                            break;
                        case 1:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-warning");
                            btnEnviarEvaluacion.Visible = true;
                            break;
                        case 2:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-info");
                            btnEnviarEvaluacion.Visible = true;
                            break;
                        case 3:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-success");
                            btnEnviarEvaluacion.Visible = true;
                            break;
                        case 4:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-danger");
                            btnEnviarEvaluacion.Visible = true;
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

        protected void btnSalir_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["id_secretaria"] != null)
            {
                Response.Redirect(
                    "Desempeniosecretarias.aspx");
            }
            else
            {
                if (Request.QueryString["id_direccion"] != null)
                {
                    Response.Redirect(string.Format(
                        "DesempenioDirecciones.aspx?id_secretaria={0}",
                        hIdSecretaria.Value));
                }
            }
        }

    }
}