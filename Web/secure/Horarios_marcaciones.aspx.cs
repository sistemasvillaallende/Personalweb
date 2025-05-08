using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Web.Utils;

namespace web.secure
{
    public partial class Horarios_marcaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int legajo = Convert.ToInt32(Request.QueryString["legajo"]);

                if (!Page.IsPostBack)
                {
                    fillHorarios(legajo);
                    fillCombos(DateTime.Now.Year);
                    DDLMes.SelectedValue = DateTime.Now.Month.ToString();
                    DDLAnio.SelectedValue = DateTime.Now.Year.ToString();
                    fillGrilla();

                    DAL.Temp_empleados objEmpleado = DAL.Temp_empleados.getByPk(legajo);

                    this.txtNombre.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.NOMBRE);
                    txtNombre.Attributes.Add("title", objEmpleado.NOMBRE);
                    txtLegajo.InnerHtml = objEmpleado.LEGAJO.ToString();
                    this.lblFecha_nacimiento.InnerHtml = objEmpleado.FECHA_NACIMIENTO.ToShortDateString();
                    this.lblSexo.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.SEXO);
                    this.lblEstadoCivil.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.ESTADO_CIVIL);
                    this.lblCuit.InnerHtml = objEmpleado.CUIT;
                    lblOS.InnerHtml = objEmpleado.NRO_AFILIADO_OS;
                    string direccion = string.Format("{0} {1} B° {2}, {3} {4}",
                        Utils.CapitalizarPalabras(objEmpleado.CALLE.Trim()), objEmpleado.NRO,
                        Utils.CapitalizarPalabras(objEmpleado.BARRIO.Trim()),
                        Utils.CapitalizarPalabras(objEmpleado.CIUDAD.Trim()),
                        Utils.CapitalizarPalabras(objEmpleado.PROVINCIA.Trim()));
                    lblDomicilio.InnerHtml = direccion;
                    lblDomicilio.Attributes.Add("title", direccion);
                    lblCodPostal.InnerHtml = objEmpleado.CP;
                    lblTelefono.InnerHtml = objEmpleado.TELEFONOS;
                    lblMail.InnerHtml = objEmpleado.EMAIL;
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }


        }
        private void fillCombos(int year)
        {
            DDLMes.Items.Clear();

            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
            {
                DDLAnio.Items.Add(i.ToString());
            }

            for (int i = 1; i < 13; i++)
            {
                switch (i)
                {
                    case 1:
                        DDLMes.Items.Add(new ListItem("Enero", "1"));
                        break;
                    case 2:
                        DDLMes.Items.Add(new ListItem("Febrero", "2"));
                        break;
                    case 3:
                        DDLMes.Items.Add(new ListItem("Marzo", "3"));
                        break;
                    case 4:
                        DDLMes.Items.Add(new ListItem("Abril", "4"));
                        break;
                    case 5:
                        DDLMes.Items.Add(new ListItem("Mayo", "5"));
                        break;
                    case 6:
                        DDLMes.Items.Add(new ListItem("Junio", "6"));
                        break;
                    case 7:
                        DDLMes.Items.Add(new ListItem("Julio", "7"));
                        break;
                    case 8:
                        DDLMes.Items.Add(new ListItem("Agosto", "8"));
                        break;
                    case 9:
                        DDLMes.Items.Add(new ListItem("Septiembre", "9"));
                        break;
                    case 10:
                        DDLMes.Items.Add(new ListItem("Octubre", "10"));
                        break;
                    case 11:
                        DDLMes.Items.Add(new ListItem("Noviembre", "11"));
                        break;
                    case 12:
                        DDLMes.Items.Add(new ListItem("Diciembre", "12"));
                        break;
                }
            }
            DDLMes.SelectedIndex = DateTime.Now.Month;

        }
        private void fillHorarios(int legajo)
        {
            try
            {
                Entities.Empleado objEmpleado = BLL.EmpleadoB.GetByPkTodos(legajo);
                List<DAL.HORARIOS> lstHorarios = DAL.HORARIOS.getByPk(legajo);
                List<DAL.HORARIOS> lstHorariosLunes = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "LUNES");
                List<DAL.HORARIOS> lstHorariosMartes = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "MARTES");
                List<DAL.HORARIOS> lstHorariosMiercoles = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "MIERCOLES");
                List<DAL.HORARIOS> lstHorariosJueves = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "JUEVES");
                List<DAL.HORARIOS> lstHorariosViernes = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "VIERNES");
                List<DAL.HORARIOS> lstHorariosSabado = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "SABADO");
                List<DAL.HORARIOS> lstHorariosDomingo = lstHorarios.FindAll(h => h.DIA_NOMBRE.Trim() == "DOMINGO");
                HtmlGenericControl tr = new HtmlGenericControl();
                tr.TagName = "tr";
                HtmlGenericControl tdDia = new HtmlGenericControl();
                tdDia.TagName = "td";
                HtmlGenericControl tdHorario = new HtmlGenericControl();
                tr.Controls.Add(tdDia);
                tdHorario = new HtmlGenericControl();
                tdHorario.TagName = "td";
                HtmlGenericControl ul = new HtmlGenericControl();
                ul.TagName = "ul";
                ul.Style.Add("list-style-type", "none");
                ul.Style.Add("padding-left", "0");
                if (lstHorariosLunes.Count > 0)
                {
                    foreach (var item2 in lstHorariosLunes)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "LUNES";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosMartes.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosMartes)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "MARTES";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosMiercoles.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosMiercoles)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "MIERCOLES";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosJueves.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosJueves)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "JUEVES";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosViernes.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosViernes)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "VIERNES";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosSabado.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosSabado)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "SABADO";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                if (lstHorariosDomingo.Count > 0)
                {
                    tr = new HtmlGenericControl();
                    tr.TagName = "tr";
                    tdDia = new HtmlGenericControl();
                    tdDia.TagName = "td";
                    tdHorario = new HtmlGenericControl();
                    tr.Controls.Add(tdDia);
                    tdHorario = new HtmlGenericControl();
                    tdHorario.TagName = "td";
                    ul = new HtmlGenericControl();
                    ul.TagName = "ul";
                    ul.Style.Add("list-style-type", "none");
                    ul.Style.Add("padding-left", "0");
                    foreach (var item2 in lstHorariosDomingo)
                    {
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.TagName = "li";
                        li.InnerHtml = string.Format("{0} - {1}",
                            item2.INT_DESDE, item2.INT_HASTA);
                        ul.Controls.Add(li);
                        tdDia.InnerHtml = "DOMINGO";
                    }

                    tdHorario.Controls.Add(ul);
                    tr.Controls.Add(tdHorario);
                    divTablaHorarios.Controls.Add(tr);
                }
                //lblNombreEmpleado.InnerHtml = objEmpleado.nombre;
                //lblLegajo.InnerHtml = string.Format("Lejago: {0}", objEmpleado.legajo);
                List<DAL.REPORTE_FICHA> lstRazones = DAL.REPORTE_FICHA.getRazones(objEmpleado.legajo,
                DateTime.Now.Year);

                Tbody1.Controls.Add(new LiteralControl(string.Format("<tr><td>{0}</td><td>{1}</td></tr>",
    lstRazones.Count, 6 - lstRazones.Count)));

                foreach (var item in lstRazones)
                {
                    Tbody2.Controls.Add(new LiteralControl(string.Format("<tr><td>{0}</td></tr>", item.FECHA.ToShortDateString())));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DDLAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int legajo = Convert.ToInt32(Request.QueryString["legajo"]);
                fillCombos(int.Parse(DDLAnio.SelectedItem.Value));
                fillGrilla();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }

        }
        protected void DDLMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fillGrilla();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                txtError.InnerText = ex.Message;
            }

        }
        private void fillGrilla()
        {
            try
            {
                int legajo = Convert.ToInt32(Request.QueryString["legajo"]);
                List<DAL.VISTA_FICHADAS> lst = DAL.VISTA_FICHADAS.read(legajo,
                    int.Parse(DDLAnio.SelectedItem.Text), int.Parse(DDLMes.SelectedItem.Value));

                gvFichadas.DataSource = lst;
                gvFichadas.DataBind();
                if (lst.Count() > 0)
                    gvFichadas.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvFichadas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HtmlGenericControl lblNovedades = (HtmlGenericControl)e.Row.FindControl("lblNovedad");
                    DAL.VISTA_FICHADAS obj = (DAL.VISTA_FICHADAS)e.Row.DataItem;
                    if (obj.ENTRADA_TARDE != string.Empty)
                        lblNovedades.InnerHtml = obj.ENTRADA_TARDE;
                    if (obj.SALIDA_ANTES != string.Empty)
                        if (lblNovedades.InnerHtml.Length > 0)
                            lblNovedades.InnerHtml = string.Format("<br/>{0}",
                                obj.SALIDA_ANTES);
                        else
                            lblNovedades.InnerHtml = obj.SALIDA_ANTES;
                    if (obj.SALIDA_INTERMEDIA != string.Empty)
                        if (lblNovedades.InnerHtml.Length > 0)
                            lblNovedades.InnerHtml = string.Format("<br/>{0}",
                                obj.SALIDA_INTERMEDIA);
                        else
                            lblNovedades.InnerHtml = obj.SALIDA_INTERMEDIA;
                    if (obj.AUSENCIAS != string.Empty)
                        if (lblNovedades.InnerHtml.Length > 0)
                            lblNovedades.InnerHtml = string.Format("<br/>{0}",
                                obj.AUSENCIAS);
                        else
                            lblNovedades.InnerHtml = obj.AUSENCIAS;
                    if (obj.FERIADOS != string.Empty)
                        if (lblNovedades.InnerHtml.Length > 0)
                            lblNovedades.InnerHtml = string.Format("<br/>{0}",
                                obj.FERIADOS);
                        else
                            lblNovedades.InnerHtml = obj.FERIADOS;
                    if (obj.HORAS == string.Empty && obj.HORAS_EXTRAS == string.Empty &&
                        obj.AUSENCIAS == string.Empty && obj.FERIADOS == string.Empty &&
                        obj.FECHA < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                        lblNovedades.InnerHtml = "CONSULTAR EN RECURSOS HUMANOS";
                    if (obj.CONTROL > 4)
                        if (lblNovedades.InnerHtml.Length > 0)
                            lblNovedades.InnerHtml = string.Format("<br/>{0}",
                                "Hay mas fichadas de las que se pueden mostrar");
                        else
                            lblNovedades.InnerHtml = "Hay mas fichadas de las que se pueden mostrar";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}