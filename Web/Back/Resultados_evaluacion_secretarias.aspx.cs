using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Resultados_evaluacion_secretarias : System.Web.UI.Page
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