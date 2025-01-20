using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Fichas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.fillIconos();
                if (this.IsPostBack)
                    return;
                this.fillGrilla();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillGrilla()
        {
            try
            {
                List<DAL.Fichas.Ficha> fichaList = BLL.Fichas.Ficha.read();
                this.gvPreguntas.DataSource = (object)fichaList;
                this.gvPreguntas.DataBind();
                if (fichaList.Count <= 0)
                    return;
                this.gvPreguntas.UseAccessibleHeader = true;
                this.gvPreguntas.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillIconos()
        {
            try
            {
                foreach (DAL.Fichas.fontawesome fontawesome in BLL.Fichas.fontawesome.read())
                {
                    HtmlGenericControl child1 = new HtmlGenericControl();
                    HtmlAnchor child2 = new HtmlAnchor();
                    child1.TagName = "div";
                    child1.Attributes.Add("class", "col-1");
                    child1.Style.Add("padding", "15px");
                    child2.HRef = "javascript:";
                    child2.Attributes.Add("onclick", string.Format("cambiarIcono('fa {0}')", (object)fontawesome.CLASE));
                    child2.InnerHtml = string.Format("<span class=\"fa {0}\"></span>", (object)fontawesome.CLASE);
                    child1.Controls.Add((Control)child2);
                    this.divIconos.Controls.Add((Control)child1);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "activar")
                    BLL.Fichas.Ficha.updateActiva(Convert.ToInt32(e.CommandArgument), true);
                if (e.CommandName == "desactivar")
                    BLL.Fichas.Ficha.updateActiva(Convert.ToInt32(e.CommandArgument), false);
                if (e.CommandName == "eliminar")
                    BLL.Fichas.Ficha.delete(Convert.ToInt32(e.CommandArgument));
                this.fillGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPreguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                DAL.Fichas.Ficha dataItem = (DAL.Fichas.Ficha)e.Row.DataItem;
                LinkButton control1 = (LinkButton)e.Row.FindControl("btnActivar");
                LinkButton control2 = (LinkButton)e.Row.FindControl("btnDesactivar");
                if (dataItem.ACTIVO)
                {
                    control1.Visible = false;
                    control2.Visible = true;
                }
                else
                {
                    control1.Visible = true;
                    control2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DAL.Fichas.Ficha ficha = new DAL.Fichas.Ficha();
                ficha.ACTIVO = true;
                if (this.hIdFicha.Value != string.Empty)
                    ficha = BLL.Fichas.Ficha.getByPk(int.Parse(this.hIdFicha.Value));
                ficha.ICONO = this.hIdIcono.Value;
                ficha.NOMBRE = this.txtActualizaPregunta.Text;
                if (ficha.ID != 0)
                    BLL.Fichas.Ficha.update(ficha);
                else
                    BLL.Fichas.Ficha.insert(ficha);
                this.fillGrilla();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
        }

        protected void ActualizarReferencia_Click(object sender, EventArgs e)
        {
            int[] array = ((IEnumerable<string>)this.Request.Form["LocationId"].Split(',')).Select<string, int>((Func<string, int>)(p => int.Parse(p))).ToArray<int>();
            int preference = 1;
            foreach (int locationId in array)
            {
                this.UpdatePreference(locationId, preference);
                ++preference;
            }
            this.Response.Redirect(this.Request.Url.AbsoluteUri);
        }

        private void UpdatePreference(int locationId, int preference)
        {
            try
            {
                DAL.Fichas.Ficha.updatePreference(locationId, preference);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAceptarNotificacion_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DAL.Fichas.Ficha.updateNotificacion(int.Parse(this.hIdFicha.Value), this.txtTextoNtificacion.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
