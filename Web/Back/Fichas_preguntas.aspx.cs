using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Fichas_preguntas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.divError.Visible = false;
                if (this.Request.QueryString["id"] == null)
                    this.Response.Redirect("Fichas.aspx");
                this.hIdFicha.Value = this.Request.QueryString["id"].ToString();
                if (this.IsPostBack)
                    return;
                DAL.Fichas.Ficha byPk = BLL.Fichas.Ficha.getByPk(int.Parse(this.hIdFicha.Value));
                this.txtIcono.Text = byPk.NOMBRE;
                this.iIcono2.Attributes.Add("class", byPk.ICONO);
                this.txtIcono3.Text = byPk.NOMBRE;
                this.iIcono3.Attributes.Add("class", byPk.ICONO);
                this.fillSecciones();
                this.btnSecciones_Click((object)null, (EventArgs)null);
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        private void fillPreguntas()
        {
            try
            {
                this.gvPreguntas.DataSource = (object)DAL.Fichas.Fichas_Preguntas.readSeccionActivas(int.Parse(this.hIdFicha.Value), int.Parse(this.hIdSeccion.Value));
                this.gvPreguntas.DataBind();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        private void fillSecciones()
        {
            try
            {
                List<DAL.Fichas.Fichas_grupo_preguntas> fichasGrupoPreguntasList = BLL.Fichas.Fichas_grupo_preguntas.read(int.Parse(this.hIdFicha.Value));
                this.gvSecciones.DataSource = (object)fichasGrupoPreguntasList;
                this.gvSecciones.DataBind();
                if (fichasGrupoPreguntasList.Count == 0)
                    this.btnAddPregunta.Enabled = false;
                else
                    this.btnAddPregunta.Enabled = true;
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        private void fillRespuestas()
        {
            try
            {
                this.gvRespuestas.DataSource = (object)BLL.Fichas.Fichas_Respuestas.readActivas(int.Parse(this.hIdPregunta.Value));
                this.gvRespuestas.DataBind();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "configurar")
                {
                    DAL.Fichas.Fichas_Preguntas byPk = BLL.Fichas.Fichas_Preguntas.getByPk(Convert.ToInt32(e.CommandArgument));
                    this.hEdita.Value = "1";
                    this.DDLTipo.Enabled = false;
                    this.divList.Visible = false;
                    this.divPreguntas.Visible = true;
                    this.txtTextoPregunta.Text = byPk.PREGUNTA;
                    HiddenField hIdPregunta1 = this.hIdPregunta;
                    int id = byPk.ID;
                    string str1 = id.ToString();
                    hIdPregunta1.Value = str1;
                    if (byPk.TIPO_PREGUNTA <= 2)
                    {
                        this.divRespuestas.Visible = true;
                        HiddenField hIdPregunta2 = this.hIdPregunta;
                        id = byPk.ID;
                        string str2 = id.ToString();
                        hIdPregunta2.Value = str2;
                        this.fillRespuestas();
                    }
                    if (byPk.TIPO_PREGUNTA == 5)
                    {
                        this.divRespuestas.Visible = true;
                        HiddenField hIdPregunta3 = this.hIdPregunta;
                        id = byPk.ID;
                        string str3 = id.ToString();
                        hIdPregunta3.Value = str3;
                        this.fillRespuestas();
                    }
                }
                if (e.CommandName == "activar")
                    BLL.Fichas.Fichas_Preguntas.updateActiva(Convert.ToInt32(e.CommandArgument), true);
                if (e.CommandName == "desactivar")
                    BLL.Fichas.Fichas_Preguntas.updateActiva(Convert.ToInt32(e.CommandArgument), false);
                if (e.CommandName == "eliminar")
                    BLL.Fichas.Fichas_Preguntas.delete(Convert.ToInt32(e.CommandArgument));
                this.fillPreguntas();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvPreguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                DAL.Fichas.Fichas_Preguntas dataItem = (DAL.Fichas.Fichas_Preguntas)e.Row.DataItem;
                LinkButton control1 = (LinkButton)e.Row.FindControl("btnActivar");
                LinkButton control2 = (LinkButton)e.Row.FindControl("btnDesactivar");
                LinkButton control3 = (LinkButton)e.Row.FindControl("btnConfigurar");
                Label control4 = (Label)e.Row.FindControl("lblTipoPregunta");
                switch (dataItem.TIPO_PREGUNTA)
                {
                    case 1:
                        control4.Text = "Multiple Opcion - Respuesta Unica";
                        break;
                    case 2:
                        control4.Text = "Multiple Opcion - Respuesta Multiple";
                        break;
                    case 3:
                        control4.Text = "Respuesta Numerica";
                        break;
                    case 4:
                        control4.Text = "Respuesta de Texto";
                        break;
                    case 5:
                        control4.Text = "Calificacion 100-75-50-25";
                        break;
                }
                if (dataItem.ACTIVA)
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
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                this.txtTextoPregunta.Text = string.Empty;
                this.divPreguntas.Visible = false;
                this.divRespuestas.Visible = false;
                this.divList.Visible = true;
                this.divSecciones.Visible = false;
                this.hEdita.Value = "0";
                this.fillPreguntas();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void btnAddPregunta_Click(object sender, EventArgs e)
        {
            try
            {
                this.divList.Visible = false;
                this.divPreguntas.Visible = true;
                this.divRespuestas.Visible = false;
                this.divSecciones.Visible = false;
                this.hEdita.Value = "0";
                this.DDLTipo.Enabled = true;
                if (DAL.Fichas.Fichas_grupo_preguntas.getByPk(int.Parse(this.hIdSeccion.Value.ToString())).TIPO != 1)
                    return;
                this.DDLTipo.SelectedValue = "5";
                this.DDLTipo.Enabled = false;
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvRespuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "activar")
                    BLL.Fichas.Fichas_Respuestas.updateActiva(Convert.ToInt32(e.CommandArgument), true);
                if (e.CommandName == "desactivar")
                    BLL.Fichas.Fichas_Respuestas.updateActiva(Convert.ToInt32(e.CommandArgument), false);
                if (e.CommandName == "eliminar")
                    BLL.Fichas.Fichas_Respuestas.delete(Convert.ToInt32(e.CommandArgument));
                this.fillRespuestas();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvRespuestas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                DAL.Fichas.Fichas_Respuestas dataItem = (DAL.Fichas.Fichas_Respuestas)e.Row.DataItem;
                LinkButton control1 = (LinkButton)e.Row.FindControl("btnActivar");
                LinkButton control2 = (LinkButton)e.Row.FindControl("btnDesactivar");
                if (dataItem.ACTIVA)
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
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void btnAceptaPregunta_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.Fichas.Fichas_grupo_preguntas byPk1 = DAL.Fichas.Fichas_grupo_preguntas.getByPk(int.Parse(this.hIdSeccion.Value.ToString()));
                DAL.Fichas.Fichas_Preguntas fichasPreguntas = new DAL.Fichas.Fichas_Preguntas();
                if (this.hEdita.Value == "1")
                {
                    DAL.Fichas.Fichas_Preguntas byPk2 = BLL.Fichas.Fichas_Preguntas.getByPk(int.Parse(this.hIdPregunta.Value));
                    byPk2.PREGUNTA = this.txtTextoPregunta.Text;
                    byPk2.TIPO_PREGUNTA = byPk1.TIPO == 1 ? 5 : int.Parse(this.DDLSeccion.SelectedItem.Value);
                    BLL.Fichas.Fichas_Preguntas.update(byPk2.ID, byPk2.PREGUNTA, byPk2.ID_GRUPO);
                }
                else if (byPk1.TIPO != 1)
                {
                    fichasPreguntas.ACTIVA = true;
                    fichasPreguntas.ID_FICHA = int.Parse(this.hIdFicha.Value);
                    fichasPreguntas.TIPO_PREGUNTA = Convert.ToInt32(this.DDLTipo.SelectedItem.Value);
                    fichasPreguntas.PREGUNTA = this.txtTextoPregunta.Text;
                    fichasPreguntas.ID_GRUPO = int.Parse(this.hIdSeccion.Value);
                    fichasPreguntas.ID = BLL.Fichas.Fichas_Preguntas.insert(fichasPreguntas);
                    if (fichasPreguntas.TIPO_PREGUNTA <= 2)
                    {
                        this.divRespuestas.Visible = true;
                        this.hIdPregunta.Value = fichasPreguntas.ID.ToString();
                        this.fillRespuestas();
                    }
                    else
                    {
                        this.txtTextoPregunta.Text = string.Empty;
                        this.divPreguntas.Visible = false;
                        this.divRespuestas.Visible = false;
                        this.divList.Visible = true;
                        this.divSecciones.Visible = false;
                        this.fillPreguntas();
                    }
                }
                else
                {
                    fichasPreguntas.ACTIVA = true;
                    fichasPreguntas.ID_FICHA = int.Parse(this.hIdFicha.Value);
                    fichasPreguntas.TIPO_PREGUNTA = Convert.ToInt32(this.DDLTipo.SelectedItem.Value);
                    fichasPreguntas.PREGUNTA = this.txtTextoPregunta.Text;
                    fichasPreguntas.ID_GRUPO = int.Parse(this.hIdSeccion.Value);
                    fichasPreguntas.ID = BLL.Fichas.Fichas_Preguntas.insertRespuestasPorcentuales(fichasPreguntas);
                    this.fillPreguntas();
                }
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void btnAceptarRespuesta_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DAL.Fichas.Fichas_Respuestas fichasRespuestas = new DAL.Fichas.Fichas_Respuestas();
                if (this.hIdRespuesta.Value == string.Empty)
                {
                    fichasRespuestas.ACTIVA = true;
                    fichasRespuestas.ID_PREGUNTA = int.Parse(this.hIdPregunta.Value);
                    fichasRespuestas.TEXTO = this.txtRespuesta.Text;
                    BLL.Fichas.Fichas_Respuestas.insert(fichasRespuestas);
                }
                else
                {
                    DAL.Fichas.Fichas_Respuestas byPk = BLL.Fichas.Fichas_Respuestas.getByPk(int.Parse(this.hIdRespuesta.Value));
                    byPk.TEXTO = this.txtRespuesta.Text;
                    BLL.Fichas.Fichas_Respuestas.update(byPk.ID, byPk.TEXTO, 0);
                }
                this.txtRespuesta.Text = string.Empty;
                this.fillRespuestas();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
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
                DAL.Fichas.Fichas_Preguntas.updatePreference(locationId, preference);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ActualizarReferencia2_Click(object sender, EventArgs e)
        {
            int[] array = ((IEnumerable<string>)this.Request.Form["LocationId2"].Split(',')).Select<string, int>((Func<string, int>)(p => int.Parse(p))).ToArray<int>();
            int preference = 1;
            foreach (int locationId in array)
            {
                this.UpdatePreference2(locationId, preference);
                ++preference;
            }
            this.Response.Redirect(this.Request.Url.AbsoluteUri);
        }

        private void UpdatePreference2(int locationId, int preference)
        {
            try
            {
                DAL.Fichas.Fichas_Respuestas.updateReference(locationId, preference);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAgregarSeccion_Click(object sender, EventArgs e)
        {
        }

        protected void btnSecciones_Click(object sender, EventArgs e)
        {
            try
            {
                this.divList.Visible = false;
                this.divPreguntas.Visible = false;
                this.divRespuestas.Visible = false;
                this.divSecciones.Visible = true;
                this.hEdita.Value = "0";
                this.DDLTipo.Enabled = true;
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvSecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "activar")
                {
                    BLL.Fichas.Fichas_grupo_preguntas.activaDesactiva(Convert.ToInt32(e.CommandArgument), true);
                    this.fillSecciones();
                }
                if (e.CommandName == "desactivar")
                {
                    BLL.Fichas.Fichas_grupo_preguntas.activaDesactiva(Convert.ToInt32(e.CommandArgument), false);
                    this.fillSecciones();
                }
                if (e.CommandName == "eliminar")
                {
                    BLL.Fichas.Fichas_grupo_preguntas.delete(Convert.ToInt32(e.CommandArgument));
                    this.fillSecciones();
                }
                if (!(e.CommandName == "preguntas"))
                    return;
                try
                {
                    if (DAL.Fichas.Fichas_grupo_preguntas.getByPk(int.Parse(e.CommandArgument.ToString())).TIPO == 1)
                    {
                        this.DDLTipo.SelectedValue = "5";
                        this.DDLTipo.Enabled = false;
                    }
                    this.divList.Visible = true;
                    this.divPreguntas.Visible = false;
                    this.divRespuestas.Visible = false;
                    this.divSecciones.Visible = false;
                    this.hEdita.Value = "0";
                    this.DDLTipo.Enabled = true;
                    this.hIdSeccion.Value = e.CommandArgument.ToString();
                    this.fillPreguntas();
                }
                catch (Exception ex)
                {
                    this.divError.Visible = true;
                    this.lblError.InnerText = ex.Message;
                }
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void gvSecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                DAL.Fichas.Fichas_grupo_preguntas dataItem = (DAL.Fichas.Fichas_grupo_preguntas)e.Row.DataItem;
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
                    e.Row.BackColor = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.lblError.InnerText = ex.Message;
            }
        }

        protected void btnCrearSeccion_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (this.hIdSeccion.Value == "")
                {
                    BLL.Fichas.Fichas_grupo_preguntas.insert(new DAL.Fichas.Fichas_grupo_preguntas()
                    {
                        ACTIVO = true,
                        ID_FICHA = int.Parse(this.Request.QueryString["id"].ToString()),
                        NOMBRE_GRUPO = this.txtNombreSeccion.Text,
                        TIPO = int.Parse(this.DDLTipoSeccion.SelectedItem.Value)
                    });
                }
                else
                {
                    DAL.Fichas.Fichas_grupo_preguntas byPk = BLL.Fichas.Fichas_grupo_preguntas.getByPk(int.Parse(this.hIdSeccion.Value));
                    byPk.NOMBRE_GRUPO = this.txtNombreSeccion.Text;
                    BLL.Fichas.Fichas_grupo_preguntas.update(byPk);
                }
                this.fillSecciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSalirSecciones_Click(object sender, EventArgs e)
        {
            try
            {
                this.divList.Visible = false;
                this.divPreguntas.Visible = false;
                this.divRespuestas.Visible = false;
                this.divSecciones.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
