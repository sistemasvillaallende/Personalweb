using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Resultado_evaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<DAL.Fichas.Ficha> lstEval = DAL.Fichas.Ficha.read();
                    DDLEvaluaciones.DataTextField = "NOMBRE";
                    DDLEvaluaciones.DataValueField = "ID";
                    DDLEvaluaciones.DataSource = lstEval;
                    DDLEvaluaciones.DataBind();
                    fillResultados();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillResultados()
        {
            List<DAL.Fichas.Resultado_evaluacion> lst =
            DAL.Fichas.Resultado_evaluacion.read(
                int.Parse(DDLEvaluaciones.SelectedItem.Value));
            List<string> lstSec = lst.Select(r => r.SECRETARIA).Distinct().ToList();
            lstSec.Insert(0, "TODAS");
            DDLSecretarias.DataSource = lstSec;
            DDLSecretarias.DataBind();

            DDLSecretarias.DataBind();
            gvResultados.DataSource = lst;
            gvResultados.DataBind();
            if (lst.Count > 0)
            {
                gvResultados.UseAccessibleHeader = true;
                gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            DDLDirecciones.DataSource = null;
            DDLDirecciones.DataBind();
            DDLOficinas.DataSource = null;
            DDLOficinas.DataBind();
            DDLProgramas.DataSource = null;
            DDLProgramas.DataBind();

        }
        private void fillResultadosSecreatria(string secretaria)
        {
            List<DAL.Fichas.Resultado_evaluacion> lst =
            DAL.Fichas.Resultado_evaluacion.read(
                int.Parse(DDLEvaluaciones.SelectedItem.Value));

            var resultados = lst
                .Where(r => secretaria == "TODAS" ||
                            r.SECRETARIA.Trim().ToLower() == secretaria.Trim().ToLower())
                .ToList();

            if (secretaria == "TODAS")
            {
                DDLDirecciones.DataSource = null;
                DDLDirecciones.DataBind();
                DDLOficinas.DataSource = null;
                DDLOficinas.DataBind();
                DDLProgramas.DataSource = null;
                DDLProgramas.DataBind();
            }
            else
            {
                List<string> lstDir = resultados.Select(r => r.DIRECCION).Distinct().ToList();
                lstDir.Insert(0, "TODAS");
                DDLDirecciones.DataSource = lstDir;
                DDLDirecciones.DataBind();
            }

            gvResultados.DataSource = resultados;
            gvResultados.DataBind();
            if (lst.Count > 0)
            {
                gvResultados.UseAccessibleHeader = true;
                gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void fillResultadosDireccion(string direccion)
        {
            List<DAL.Fichas.Resultado_evaluacion> lst =
            DAL.Fichas.Resultado_evaluacion.read(
                int.Parse(DDLEvaluaciones.SelectedItem.Value));

            var resultados = lst
                .Where(r => direccion == "TODAS" ||
                            r.DIRECCION.Trim().ToLower() == direccion.Trim().ToLower())
                .ToList();

            if (direccion == "TODAS")
            {
                DDLOficinas.DataSource = null;
                DDLOficinas.DataBind();
                DDLProgramas.DataSource = null;
                DDLProgramas.DataBind();
            }
            else
            {
                List<string> lstOficina = resultados.Select(r => r.OFICINA).Distinct().ToList();
                lstOficina.Insert(0, "TODAS");
                DDLOficinas.DataSource = lstOficina;
                DDLOficinas.DataBind();

                List<string> lstProgramas = resultados.Select(r => r.PROGRAMA).Distinct().ToList();
                lstProgramas.Insert(0, "TODAS");
                DDLProgramas.DataSource = lstProgramas;
                DDLProgramas.DataBind();
            }

            gvResultados.DataSource = resultados;
            gvResultados.DataBind();
            if (lst.Count > 0)
            {
                gvResultados.UseAccessibleHeader = true;
                gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void fillResultadosOficina(string oficina)
        {
            DDLProgramas.SelectedIndex = 0;
            List<DAL.Fichas.Resultado_evaluacion> lst =
            DAL.Fichas.Resultado_evaluacion.read(
                int.Parse(DDLEvaluaciones.SelectedItem.Value));
            string direccion = DDLDirecciones.SelectedValue;
            var resultados = lst
                .Where(r => oficina == "TODAS" ||
                            r.OFICINA.Trim().ToLower() == oficina.Trim().ToLower())
                .ToList();

            if (oficina == "TODAS")
            {
                resultados = lst
                                .Where(r => oficina == "TODAS" ||
                                            r.DIRECCION.Trim().ToLower() == direccion.Trim().ToLower())
                                .ToList();
            }

            gvResultados.DataSource = resultados;
            gvResultados.DataBind();
            if (lst.Count > 0)
            {
                gvResultados.UseAccessibleHeader = true;
                gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void fillResultadosPrograma(string programa)
        {
            DDLOficinas.SelectedIndex = 0;
            List<DAL.Fichas.Resultado_evaluacion> lst =
            DAL.Fichas.Resultado_evaluacion.read(
                int.Parse(DDLEvaluaciones.SelectedItem.Value));
            string direccion = DDLDirecciones.SelectedValue;
            var resultados = lst
                .Where(r => programa == "TODAS" ||
                            r.OFICINA.Trim().ToLower() == programa.Trim().ToLower())
                .ToList();

            if (programa == "TODAS")
            {
                resultados = lst
                                .Where(r => programa == "TODAS" ||
                                            r.DIRECCION.Trim().ToLower() == direccion.Trim().ToLower())
                                .ToList();
            }

            gvResultados.DataSource = resultados;
            gvResultados.DataBind();
            if (lst.Count > 0)
            {
                gvResultados.UseAccessibleHeader = true;
                gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void gvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL.Fichas.Resultado_evaluacion obj =
                        (DAL.Fichas.Resultado_evaluacion)e.Row.DataItem;
                    HtmlGenericControl divLink =
                        (HtmlGenericControl)e.Row.FindControl("divLink");

                    HtmlGenericControl divLinkEval =
                        (HtmlGenericControl)e.Row.FindControl("divLinkEval");
                    if (obj.ID_FICHA != 0)
                    {
                        divLink.Visible = true;
                        divLinkEval.Visible = false;
                    }
                    else
                    {
                        HtmlAnchor anchor = new HtmlAnchor();
                        HtmlGenericControl span = new HtmlGenericControl();
                        span.Attributes.Add("class", "fa fa-pencil-square-o");
                        span.Style.Add("font-size", "30px;");
                        anchor.HRef = string.Format(
                            "Personas_fichas.aspx?idFicha={0}&legajo={1}",
                            DDLEvaluaciones.SelectedItem.Value,
                            obj.LEGAJO);
                        anchor.Controls.Add(span);
                        divLinkEval.Controls.Add(anchor);
                        /*
                         <a href="Personas_fichas.aspx?idFicha=&legajo=<%#Eval("LEGAGO")%>">
                                            <span class="fa fa-search-plus"></span>
                                        </a>
                         */
                        divLink.Visible = false;
                        divLinkEval.Visible = true;
                    }
                }

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
                fillResultados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DDLSecretarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<DAL.Fichas.Resultado_evaluacion> lst =
                DAL.Fichas.Resultado_evaluacion.read(
                    int.Parse(DDLEvaluaciones.SelectedItem.Value));
                string secretaria = DDLSecretarias.SelectedValue;
                DDLDirecciones.DataSource = lst
                    .Where(r => r.SECRETARIA == secretaria)
                    .Select(r => r.DIRECCION)
                    .Distinct()
                    .ToList();
                DDLDirecciones.DataBind();

                fillResultadosSecreatria(DDLSecretarias.SelectedItem.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DDLDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<DAL.Fichas.Resultado_evaluacion> lst =
                DAL.Fichas.Resultado_evaluacion.read(
                    int.Parse(DDLEvaluaciones.SelectedItem.Value));

                string secretaria = DDLSecretarias.SelectedValue;
                string direccion = DDLDirecciones.SelectedValue;

                List<DAL.Fichas.Resultado_evaluacion> lstSec = lst
                    .FindAll(r => r.SECRETARIA == secretaria).ToList();

                List<DAL.Fichas.Resultado_evaluacion> lstDir = lstSec
    .FindAll(r => r.DIRECCION == direccion).ToList();

                fillResultadosDireccion(direccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DDLOficinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string oficina = DDLOficinas.SelectedValue;
                fillResultadosOficina(oficina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DDLProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string programa = DDLProgramas.SelectedValue;

                fillResultadosPrograma(programa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}