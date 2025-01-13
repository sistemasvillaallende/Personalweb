using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Desempeniosecretarias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id_secretaria"] == null)
                    {
                        List<DAL.Secretarias_grilla> lst =
                            DAL.Secretarias_grilla.read(DateTime.Now.Year);
                        gvSecretarias.DataSource = lst;
                        gvSecretarias.DataBind();
                        divSecretarias.Visible = true;
                        divDetalleSecretaria.Visible = false;
                        lblSecretaria.InnerHtml =
                            "RRHH - Control de evaluaciones de desempeño en Secretarias";
                    }
                    else
                    {
                        hIdSecretaria.Value = Request.QueryString["id_secretaria"];

                        DAL.Secretarias_grilla objSec =
                            DAL.Secretarias_grilla.getByPk(
                                int.Parse(hIdSecretaria.Value), DateTime.Now.Year);
                        lblSecretariaConsultada.InnerHtml = objSec.SECRETARIA;
                        divDetalleSecretaria.Visible = true;
                        divSecretarias.Visible = false;
                        List<DAL.Fichas.Resultado_evaluacion> lst =
DAL.Fichas.Resultado_evaluacion.read(
    int.Parse(DDLEvaluaciones.SelectedItem.Value));
                        //gvResultados.DataSource = lst;
                        //gvResultados.DataBind();
                        //if (lst.Count > 0)
                        //{
                        //    gvResultados.UseAccessibleHeader = true;
                        //    gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //}
                        //CargarGrilla(int.Parse(hIdSecretaria.Value));
                    }
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
                            btnEnviarEvaluacion.Visible = false;
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
                            break;
                        case 4:
                            lblEstadoEvaluacion.Attributes.Add("class", "alert alert-danger");
                            btnEnviarEvaluacion.Visible = true;
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
                            btnEnviarEvaluacion.Visible = false;
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

        protected void btnEnviarEvaluacion_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSalir_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id_secretaria"] == null)
                {
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    Response.Redirect(
                        string.Format("Desempeniosecretarias.aspx? id_secretaria =",
                        Request.QueryString["id_secretaria"]));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSecretarias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL.Secretarias_grilla objSecGrilla =
                        (DAL.Secretarias_grilla)e.Row.DataItem;

                    HtmlGenericControl pPermiso =
                        (HtmlGenericControl)e.Row.FindControl("pPermiso");
                    LinkButton btnAsignarPermiso =
                        (LinkButton)e.Row.FindControl("btnAsignarPermiso");
                    LinkButton btnQuitarPermiso =
                        (LinkButton)e.Row.FindControl("btnQuitarPermiso");
                    LinkButton btnCambiarEvaluador =
                        (LinkButton)e.Row.FindControl("btnCambiarEvaluador");

                    if (objSecGrilla.PERMISO)
                    {
                        pPermiso.InnerHtml = "Si";
                        pPermiso.Style.Add("color", "#4DCA88");
                        pPermiso.Style.Add("font-weight", "700");
                        btnAsignarPermiso.Visible = false;
                        btnQuitarPermiso.Visible = true;
                    }
                    else
                    {
                        pPermiso.InnerHtml = "No";
                        pPermiso.Style.Add("color", "#d7263d");
                        pPermiso.Style.Add("font-weight", "700");
                        btnAsignarPermiso.Visible = true;
                        btnQuitarPermiso.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSecretarias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id_secretaria = 0;
            int COD_USUARIO = 0;

            try
            {
                if (e.CommandName == "asignarpermiso")
                {
                    id_secretaria = Convert.ToInt32(
                        gvSecretarias.DataKeys[index].Values["id_secretaria"]);
                    COD_USUARIO = Convert.ToInt32(
                        gvSecretarias.DataKeys[index].Values["COD_USUARIO"]);
                    int legajo = Convert.ToInt32(
                        gvSecretarias.DataKeys[index].Values["legajo"]);

                    DAL.PERMISOS_EVALUACION obj = new DAL.PERMISOS_EVALUACION();
                    obj.COD_USUARIO = COD_USUARIO;
                    obj.id_secretaria = id_secretaria;
                    obj.legajo = legajo;
                    DAL.PERMISOS_EVALUACION.InsertPermisoSecretaria(obj);
                    List<DAL.Secretarias_grilla> lst =
    DAL.Secretarias_grilla.read(DateTime.Now.Year);
                    gvSecretarias.DataSource = lst;
                    gvSecretarias.DataBind();
                    divSecretarias.Visible = true;
                    divDetalleSecretaria.Visible = false;
                }

                if (e.CommandName == "quitarpermiso")
                {
                    id_secretaria = Convert.ToInt32(
    gvSecretarias.DataKeys[index].Values["id_secretaria"]);
                    COD_USUARIO = Convert.ToInt32(
                        gvSecretarias.DataKeys[index].Values["COD_USUARIO"]);
                    DAL.PERMISOS_EVALUACION obj = new DAL.PERMISOS_EVALUACION();
                    obj.COD_USUARIO = COD_USUARIO;
                    obj.id_secretaria = id_secretaria;
                    DAL.PERMISOS_EVALUACION.QuitarPermisoSecretaria(obj);
                    List<DAL.Secretarias_grilla> lst =
    DAL.Secretarias_grilla.read(DateTime.Now.Year);
                    gvSecretarias.DataSource = lst;
                    gvSecretarias.DataBind();
                    divSecretarias.Visible = true;
                    divDetalleSecretaria.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}