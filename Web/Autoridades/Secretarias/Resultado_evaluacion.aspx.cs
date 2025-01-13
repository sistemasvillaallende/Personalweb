using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class Resultado_evaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int idSecretaria = 0;
                if (!IsPostBack)
                {
                    if (Request.Cookies["UserSistema"] != null)
                    {
                        if (Request.Cookies["UserSistema"]["id_secretaria"] != null)
                            idSecretaria =
                                int.Parse(Request.Cookies["UserSistema"]["id_secretaria"]);

                    }
                    List<DAL.Fichas.Resultado_evaluacion> lst =
                        DAL.Fichas.Resultado_evaluacion.getBySecretaria(idSecretaria);
                    gvResultados.DataSource = lst;
                    gvResultados.DataBind();
                    if (lst.Count > 0)
                    {
                        gvResultados.UseAccessibleHeader = true;
                        gvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}