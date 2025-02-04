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
                    DDLEvaluaciones.DataTextField = "NOMBRE";
                    DDLEvaluaciones.DataValueField = "ID";
                    DDLEvaluaciones.DataSource = DAL.Fichas.Ficha.read();
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
            gvResultados.DataSource = lst;
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
                            obj.LEGAGO);
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
    }
}