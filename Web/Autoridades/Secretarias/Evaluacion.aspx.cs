using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class Evaluacion : System.Web.UI.Page
    {
        //ACTUALIZADO
        override protected void OnInit(EventArgs e)
        {
            try
            {
                string legajo = Request.QueryString["legajo"].ToString();
                int idFicha = int.Parse(Request.QueryString["idFicha"].ToString());
                hIdFicha.Value = idFicha.ToString();
                List<DAL.Fichas.Fichas_Relevamientos> lstR =
                    BLL.Fichas.Fichas_Relevamientos.read(idFicha, legajo);

                if (lstR.Count == 0)
                {
                    fillGrilla();
                    btnNotificar.Visible = false;
                }
                else
                {
                    foreach (var item in lstR)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        HtmlAnchor a = new HtmlAnchor();
                        a.ServerClick += new EventHandler(btnCambiar_Click);
                        a.InnerText = item.FECHA.ToShortDateString();
                        a.ID = item.ID.ToString();
                        li.Controls.Add(a);
                        ddlAnteriores.Controls.Add(li);
                    }
                    btnNotificar.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ACTUALIZADO
        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            HtmlAnchor a = (HtmlAnchor)sender;
            fillResultados(int.Parse(a.ID));
        }
        static string ConvertirALetraCapital(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // Manejar el caso de una cadena vacía o nula, según tus requisitos
                return input;
            }

            // Convertir toda la cadena a minúsculas
            string lowerCaseInput = input.ToLower();

            // Concatenar la primera letra en mayúscula con el resto de la cadena
            return char.ToUpper(lowerCaseInput[0]) + lowerCaseInput.Substring(1);
        }
        //ACTUALIZADO
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    HtmlAnchor liDesempenio =
(HtmlAnchor)Master.FindControl("liDesempenio");
                    if (liDesempenio != null)
                        liDesempenio.Attributes.Add("class", "active");

                    string legajo = Request.QueryString["legajo"].ToString();
                    List<DAL.Fichas.Fichas_Relevamientos> lstR =
                        BLL.Fichas.Fichas_Relevamientos.read(int.Parse(hIdFicha.Value), legajo);

                    DAL.Fichas.Ficha objFicha = DAL.Fichas.Ficha.getByPk(int.Parse(hIdFicha.Value));
                    lblNombreEvaluacion.InnerHtml = objFicha.NOMBRE;

                    List<Entities.LstEmpleados> lst = DAL.EmpleadoD.GetByLegajo(legajo);
                    if (lst.Count() > 0)
                    {
                        Entities.LstEmpleados obj = lst[0];
                        lblCategoria.InnerHtml = obj.cod_categoria.ToString();
                        lblLegajo.InnerHtml = obj.legajo.ToString();
                        lblNombre.InnerHtml = obj.nombre;
                        if (obj.passTemp.Length > 50)
                            imgUser.Src = obj.passTemp;
                        else
                            imgUser.Src = "../../App_Themes/images/usuario.png";
                        lblFechaIngreso.InnerHtml = obj.fecha_ingreso;
                        lblTipoLiq.InnerHtml = obj.des_tipo_liq;

                        lblOficinas.InnerHtml = ConvertirALetraCapital(obj.oficina);


                        lblNombreEvaluador.InnerHtml =
                            Request.Cookies["UserSistema"]["nombreUsuario"].ToString();
                        lblFecha.InnerHtml =
                            DateTime.Now.ToShortDateString();
                    }

                    if (lstR.Count == 0)
                        fillGrilla();
                    else
                    {
                        fillResultados(lstR[0].ID);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ACTUALIZADO
        private void fillResultados(int id)
        {
            DAL.Fichas.Fichas_Relevamientos objRelevamiento =
                DAL.Fichas.Fichas_Relevamientos.getByPk(id);
            hIdEstado.Value = objRelevamiento.NOMBRE_ESTADO;
            evaluador = objRelevamiento.USUARIO_RELEVA;
            lblTitulo.InnerText = string.Format("Realizada el: {0}",
                objRelevamiento.FECHA.ToShortDateString());


            List<DAL.Fichas.Fichas_Relevamientos_Personas> lst =
                BLL.Fichas.Fichas_Relevamientos_Personas.read(id);
            gvPreguntas.DataSource = null;
            gvPreguntas.DataBind();
            gvRespuestas.DataSource = lst;
            gvRespuestas.DataBind();
            divRespuestas.Visible = true;
            hIdRelevamiento.Value = id.ToString();
        }
        //ACTUALIZADO
        private void fillGrilla()
        {
            gvPreguntas.DataSource = BLL.Fichas.Fichas_Preguntas.readActivas(
                int.Parse(hIdFicha.Value));
            gvPreguntas.DataBind();

            gvRespuestas.DataSource = null;
            gvRespuestas.DataBind();
            divRespuestas.Visible = false;
        }
        //ACTUALIZADO
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                leerGrilla();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.InnerText = ex.Message;
            }
        }
        //ACTUALIZADO
        private void leerGrilla()
        {
            try
            {
                StringBuilder error = new StringBuilder();
                List<DAL.Fichas.Fichas_Relevamientos_Personas> lst =
                    new List<DAL.Fichas.Fichas_Relevamientos_Personas>();
                DAL.Fichas.Fichas_Relevamientos_Personas objResp;
                for (int i = 0; i < gvPreguntas.Rows.Count; i++)
                {
                    GridViewRow row = gvPreguntas.Rows[i];
                    DAL.Fichas.Fichas_Preguntas obj = BLL.Fichas.Fichas_Preguntas.getByPk(
                        Convert.ToInt32(gvPreguntas.DataKeys[i].Values["ID"]));
                    objResp = new DAL.Fichas.Fichas_Relevamientos_Personas();
                    switch (obj.TIPO_PREGUNTA)
                    {
                        case 1:
                            RadioButtonList rbtn = (RadioButtonList)row.FindControl("rbtn");
                            if (rbtn.SelectedIndex == -1)
                            {
                                error.AppendLine(string.Format("Debe seleccionar una opcion en la pregunta {0} <br/>", i));
                            }
                            else
                            {
                                objResp.ID_PREGUNTA = obj.ID;
                                objResp.TEXTO_PREGUNTA = obj.PREGUNTA;
                                objResp.ID_RESPUESTA = int.Parse(rbtn.SelectedItem.Value);
                                objResp.TEXTO_RESPUESTA = rbtn.SelectedItem.Text;
                                lst.Add(objResp);
                            }
                            break;
                        case 2:
                            CheckBoxList chkList = (CheckBoxList)row.FindControl("chkList");
                            bool control = false;
                            foreach (ListItem item in chkList.Items)
                            {
                                if (item.Selected)
                                {
                                    objResp = new DAL.Fichas.Fichas_Relevamientos_Personas();
                                    objResp.ID_PREGUNTA = obj.ID;
                                    objResp.TEXTO_PREGUNTA = obj.PREGUNTA;
                                    objResp.ID_RESPUESTA = int.Parse(item.Value);
                                    objResp.TEXTO_RESPUESTA = item.Text;
                                    lst.Add(objResp);
                                    control = true;
                                }
                            }
                            if (!control)
                                error.AppendLine(string.Format("Debe seleccionar al menos una opcion en la pregunta {0} <br/>", i));
                            break;
                        case 3:
                            TextBox txtRespuesta = (TextBox)row.FindControl("txtRespuesta");
                            if (txtRespuesta.Text == string.Empty)
                            {
                                error.AppendLine(string.Format("Debe completar la respuesta de la pregunta {0} <br/>", i));
                            }
                            else
                            {
                                objResp.ID_PREGUNTA = obj.ID;
                                objResp.TEXTO_PREGUNTA = obj.PREGUNTA;
                                objResp.ID_RESPUESTA = 0;
                                objResp.TEXTO_RESPUESTA = txtRespuesta.Text;
                                lst.Add(objResp);
                            }
                            break;
                        case 4:
                            TextBox txtRespuesta2 = (TextBox)row.FindControl("txtRespuesta");
                            if (txtRespuesta2.Text == string.Empty)
                                error.AppendLine(string.Format("Debe completar la respuesta de la pregunta {0} <br/>", i));
                            else
                            {
                                objResp.ID_PREGUNTA = obj.ID;
                                objResp.TEXTO_PREGUNTA = obj.PREGUNTA;
                                objResp.ID_RESPUESTA = 0;
                                objResp.TEXTO_RESPUESTA = txtRespuesta2.Text;
                                lst.Add(objResp);
                            }
                            break;
                        case 5:
                            RadioButtonList rbtn2 = (RadioButtonList)row.FindControl("rbtn");
                            if (rbtn2.SelectedIndex == -1)
                            {
                                error.AppendLine(string.Format("Debe seleccionar una opcion en la pregunta {0} <br/>", i));
                            }
                            else
                            {
                                objResp.ID_PREGUNTA = obj.ID;
                                objResp.TEXTO_PREGUNTA = obj.PREGUNTA;
                                objResp.ID_RESPUESTA = int.Parse(rbtn2.SelectedItem.Value);
                                objResp.TEXTO_RESPUESTA = rbtn2.SelectedItem.Text;
                                lst.Add(objResp);
                            }
                            break;
                        default:
                            break;
                    }

                }
                if (error.Length > 0)
                {
                    lblError.InnerHtml = error.ToString();
                    divError.Visible = true;
                    return;
                }
                else
                {
                    string legajo = Request.QueryString["legajo"].ToString();
                    Entities.LstEmpleados objEmpleado = DAL.EmpleadoD.GetByLegajo(legajo)[0];
                    DAL.Fichas.Fichas_Relevamientos oEdu = new DAL.Fichas.Fichas_Relevamientos();
                    oEdu.CUIT = Request.QueryString["legajo"].ToString();
                    oEdu.FECHA = DateTime.Now;
                    oEdu.USUARIO_RELEVA = Request.Cookies["UserSistema"]["usuario"];
                    oEdu.ID_FICHA = int.Parse(hIdFicha.Value);
                    oEdu.OFICINA = objEmpleado.oficina;
                    oEdu.DIRECCION = objEmpleado.direccion;
                    oEdu.SECRETARIA = objEmpleado.secrectaria;

                    int id = BLL.Fichas.Fichas_Relevamientos.insert(oEdu, lst);
                    if (id != 0)
                    {
                        hIdEstado.Value = "EVALUACION REALIZADA";
                        fillResultados(id);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                divError.Visible = true;
            }

        }
        //ACTUALIZADO
        string grupo = string.Empty;
        protected void gvPreguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL.Fichas.Fichas_Preguntas obj = (DAL.Fichas.Fichas_Preguntas)e.Row.DataItem;
                    RadioButtonList rbtn = (RadioButtonList)e.Row.FindControl("rbtn");
                    CheckBoxList chkList = (CheckBoxList)e.Row.FindControl("chkList");
                    TextBox txtRespuesta = (TextBox)e.Row.FindControl("txtRespuesta");
                    HtmlGenericControl divGrupo = (HtmlGenericControl)e.Row.FindControl("divGrupo");
                    HtmlGenericControl divPorcentual = (HtmlGenericControl)e.Row.FindControl("divPorcentual");
                    HtmlGenericControl divNormal = (HtmlGenericControl)e.Row.FindControl("divNormal");
                    rbtn.Visible = false;
                    chkList.Visible = false;
                    txtRespuesta.Visible = false;
                    if (e.Row.RowIndex == 0)
                    {
                        divGrupo.InnerHtml = string.Format(
                            @"<h4 style=""font-size: 20px; color: var(--primary-color);"">{0}</h4>
                                <hr style=""margin-top: 2px; margin-bottom: 1rem;
                                    border: 0; opacity: 1;
                                    border-top: 2px solid #dcdbdb;""/>",
                            obj.NOMBRE_GRUPO);
                        grupo = obj.ID_GRUPO.ToString();
                    }
                    else
                    {
                        if (obj.ID_GRUPO != int.Parse(grupo))
                        {
                            divGrupo.InnerHtml = string.Format(
                                 @"<h4 style=""font-size: 20px; color: var(--primary-color);"">{0}</h4>
                                <hr style=""margin-top: 2px; margin-bottom: 1rem;
                                    border: 0; opacity: 1;
                                    border-top: 2px solid #dcdbdb;""/>",
                                obj.NOMBRE_GRUPO);
                            grupo = obj.ID_GRUPO.ToString();
                        }
                    }
                    switch (obj.TIPO_PREGUNTA)
                    {
                        case 1:
                            divNormal.Visible = false;
                            divPorcentual.Visible = true;
                            rbtn.Visible = true;
                            rbtn.DataTextField = "TEXTO";
                            rbtn.DataValueField = "ID";
                            rbtn.DataSource = DAL.Fichas.Fichas_Respuestas.read(obj.ID);
                            rbtn.DataBind();

                            chkList.Visible = false;
                            txtRespuesta.Visible = false;
                            break;
                        case 2:
                            divNormal.Visible = false;
                            divPorcentual.Visible = true;
                            chkList.Visible = true;
                            chkList.DataTextField = "TEXTO";
                            chkList.DataValueField = "ID";
                            chkList.DataSource = DAL.Fichas.Fichas_Respuestas.read(obj.ID);
                            chkList.DataBind();

                            rbtn.Visible = false;
                            txtRespuesta.Visible = false;
                            break;
                        case 3:
                            divNormal.Visible = true;
                            divPorcentual.Visible = false;
                            txtRespuesta.Visible = true;
                            txtRespuesta.TextMode = TextBoxMode.Number;
                            break;
                        case 4:
                            txtRespuesta.Visible = true;
                            divNormal.Visible = true;
                            divPorcentual.Visible = false;
                            rbtn.Visible = false;
                            chkList.Visible = false;
                            break;
                        case 5:
                            divNormal.Visible = false;
                            divPorcentual.Visible = true;
                            rbtn.Visible = true;
                            rbtn.DataTextField = "TEXTO";
                            rbtn.DataValueField = "ID";
                            rbtn.DataSource = DAL.Fichas.Fichas_Respuestas.read(obj.ID);
                            rbtn.DataBind();
                            txtRespuesta.Visible = false;

                            txtRespuesta.Visible = false;
                            chkList.Visible = false;
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //ACTUALIZADO
        protected void btnNuevo_ServerClick(object sender, EventArgs e)
        {
            fillGrilla();
        }

        protected void btnEliminar_ServerClick(object sender, EventArgs e)
        {
            List<DAL.Fichas.Fichas_Relevamientos_Personas> lst = BLL.Fichas.Fichas_Relevamientos_Personas.read(
                int.Parse(hIdRelevamiento.Value));
            DAL.Fichas.Fichas_Relevamientos obj = BLL.Fichas.Fichas_Relevamientos.getByPk(int.Parse(hIdRelevamiento.Value));
            BLL.Fichas.Fichas_Relevamientos.delete(int.Parse(hIdRelevamiento.Value));
            List<DAL.Fichas.Fichas_Relevamientos> lstNuevo = BLL.Fichas.Fichas_Relevamientos.read(int.Parse(hIdFicha.Value),
                Request.QueryString["legajo"].ToString());
            if (lstNuevo.Count > 0)
                fillResultados(lstNuevo[0].ID);
            else
                fillGrilla();
        }

        string grupo_respuesta = string.Empty;
        int idPregunta = 0;
        int idRelevamiento = 0;
        string evaluador = string.Empty;
        string estado = string.Empty;

        protected void gvRespuestas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HtmlGenericControl divGrupo = (HtmlGenericControl)e.Row.FindControl("divGrupo");
                    HtmlGenericControl divResultado = (HtmlGenericControl)e.Row.FindControl("divResultado");

                    divGrupo.Visible = false;
                    divResultado.Visible = false;

                    HtmlGenericControl lblPregunta =
                        (HtmlGenericControl)e.Row.FindControl("lblPregunta");
                    DAL.Fichas.Fichas_Relevamientos_Personas obj = (DAL.Fichas.Fichas_Relevamientos_Personas)e.Row.DataItem;
                    List<DAL.Fichas.Fichas_Relevamientos_Personas> lst =
                        DAL.Fichas.Fichas_Relevamientos_Personas.read(obj.ID_RELEVAMIENTO);
                    idRelevamiento = obj.ID_RELEVAMIENTO;

                    if (obj.ID_PREGUNTA == idPregunta)
                    {
                        lblPregunta.Visible = false;
                    }
                    else
                    {
                        lblPregunta.Visible = true;
                        idPregunta = obj.ID_PREGUNTA;
                    }
                    if (e.Row.RowIndex == 0)
                    {
                        divGrupo.InnerHtml = string.Format(
                            @"<h4 style=""font-size: 20px; color: var(--primary-color);"">{0}</h4>
                                <hr style=""margin-top: 2px; margin-bottom: 1rem;
                                    border: 0; opacity: 1;
                                    border-top: 2px solid #dcdbdb;""/>",
                            ConvertirALetraCapital(obj.NOMBRE_GRUPO));
                        divGrupo.Visible = true;
                        grupo_respuesta = obj.ID_GRUPO.ToString();
                    }
                    else
                    {
                        if (obj.ID_GRUPO != int.Parse(grupo_respuesta))
                        {
                            int idGrupo = lst[e.Row.RowIndex - 1].ID_GRUPO;
                            if (lst[e.Row.RowIndex - 1].TIPO == 1)
                            {
                                decimal cant = 0;
                                decimal sum = 0;
                                decimal resultado = 0;
                                foreach (var item in lst)
                                {
                                    if (item.ID_GRUPO == idGrupo)
                                    {
                                        cant++;
                                        sum += item.PUNTUACION;
                                    }
                                }
                                resultado = sum / cant;
                                divResultado.Visible = true;
                                if (resultado >= 75)
                                {
                                    divResultado.InnerHtml = string.Format(
                                        @"Resultado del factor: {0}: 
                                    <strong style=""color: var(--bs-success);;"">{1}%</strong>",
                                        ConvertirALetraCapital(
                                           lst[e.Row.RowIndex - 1].NOMBRE_GRUPO), Math.Round(resultado, 2));
                                }
                                else
                                {
                                    divResultado.InnerHtml = string.Format(
                                        @"Resultado del factor: {0}: 
                                    <strong style=""color: var(--bs-danger);"">{1}%</strong>",
                                      ConvertirALetraCapital(lst[e.Row.RowIndex - 1].NOMBRE_GRUPO), Math.Round(resultado, 2));
                                }
                                divGrupo.InnerHtml = string.Format(
                                     @"<h4 style=""margin-top: 20px; font-size: 20px; color: var(--primary-color);"">{0}</h4>
                                <hr style=""margin-top: 2px; margin-bottom: 1rem;
                                    border: 0; opacity: 1;
                                    border-top: 2px solid #dcdbdb;""/>",
                                    ConvertirALetraCapital(obj.NOMBRE_GRUPO));
                                grupo_respuesta = obj.ID_GRUPO.ToString();
                                divGrupo.Visible = true;
                            }
                        }
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    //HtmlGenericControl divResultadoTotal =
                    //    (HtmlGenericControl)e.Row.FindControl("divResultadoTotal");

                    List<DAL.Fichas.Fichas_Relevamientos_Personas> lst =
    DAL.Fichas.Fichas_Relevamientos_Personas.read(idRelevamiento);

                    decimal cantf = 0;
                    decimal sumf = 0;
                    decimal resultadof = 0;
                    foreach (var item in lst)
                    {
                        if (item.PUNTUACION != 0)
                        {
                            cantf++;
                            sumf += item.PUNTUACION;
                        }
                    }
                    resultadof = sumf / cantf;
                    divResultadoTotal.Visible = true;
                    if (resultadof >= 75)
                    {
                        divResultadoTotal.InnerHtml = string.Format(
                            @"Resultado promedio: 
                                    <strong style=""color: var(--bs-success);"">{0}%</strong>",
                             Math.Round(resultadof, 2));
                    }
                    else
                    {
                        divResultadoTotal.InnerHtml = string.Format(
                            @"Resultado promedio: 
                                    <strong style=""color: var(--bs-danger);"">{0}%</strong>",
                            Math.Round(resultadof, 2));
                    }

                    //HtmlGenericControl lblEvaluadorOriginal =
                    //    (HtmlGenericControl)e.Row.FindControl("lblEvaluadorOriginal");
                    //HtmlGenericControl lblEvaluado =
                    //    (HtmlGenericControl)e.Row.FindControl("lblEvaluado");
                    //HtmlGenericControl lblConformidad =
                    //    (HtmlGenericControl)e.Row.FindControl("lblConformidad");

                    lblEvaluadorOriginal.InnerHtml =
                        string.Format(@"Evaluador: <span style=""color:var(--primary-color)"">
                                      {0}</span>", ConvertirALetraCapital(evaluador));

                    lblEvaluado.InnerHtml =
                        string.Format(@"Evaluado: <span style=""color:var(--primary-color)"">
                                      {0}</span>", ConvertirALetraCapital(lblNombre.InnerHtml));

                    lblConformidad.InnerHtml =
                        string.Format(@"Conformidad: <span style=""color:var(--bs-orange);"">
                                      {0}</span>", ConvertirALetraCapital(hIdEstado.Value));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnNotificar_ServerClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}