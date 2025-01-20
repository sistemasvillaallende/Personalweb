using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entities;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace Web.secure
{
    public partial class listempleados : System.Web.UI.Page
    {

        #region Declaracion de Variables y Objetos
        bool ok = false;
        int intPageSize = 6;
        int intPage = 0;
        //int paginas = 0;
        //int registros = 0;
        string BuscarPor = "";
        Decimal CountPage = 0;
        //ConsultaExpedienteB oList;
        //ExpedienteB objExpediente;
        SeguridadB objSeguridad;
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            if (!Page.IsPostBack)
            {
                FillCombo();
                BindList();
                ddFindBy.SelectedIndex = 1;
            }
            //Session.Remove("anio");
            //Session.Remove("nro_expediente");

            //string var = Request.Params["__EVENTARGUMENT"];
            //if (var == "AlertaMSJ")
            //divAlerta.Visible = false;
            //modalMSJ.Hide();

        }


        private void BindList()
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                CargarGrilla();
                txtInput.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillCombo()
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            ds1.ReadXml(Server.MapPath(Request.ApplicationPath + "\\xml\\ListFindEmpleados.xml"));
            ddFindBy.DataSource = ds1.Tables[0];
            ddFindBy.DataTextField = "Name";
            ddFindBy.DataValueField = "IDName";
            ddFindBy.DataBind();
        }

        #region "Eventos Grillas"



        private void CargarGrilla()
        {
            bool verTodo = false;
            string legajo = "";
            string nombre = "", nro_documento = "";


            try
            {
                BuscarPor = (Convert.ToString(ddFindBy.SelectedValue) == "0" ? "" : Convert.ToString(ddFindBy.SelectedValue));
                //-------------------------------------------------------------------//
                //Salvo que el usuario tenga el permiso para ver todos los Empleados
                //como usr admin
                //objSeguridad = new SeguridadB();
                //verTodo = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "ME_GESTION_EXPEDIENTE_ADMIN",
                // out id_oficina_origen);
                //-------------------------------------------------------------------//
                //id_oficina_origen = Convert.ToInt16(Session["id_oficina_usuario"]);
                //oList = new ConsultaExpedienteB();

                switch (BuscarPor)
                {
                    case "e.legajo":
                        legajo = txtInput.Value;
                        grdList.DataSource = BLL.ConsultaEmpleadoB.GetByLegajo(legajo);

                        break;
                    case "e.nombre":
                        nombre = txtInput.Value;
                        grdList.DataSource = BLL.ConsultaEmpleadoB.GetByNombre(nombre);
                        break;
                    case "e.nro_documento":
                        nro_documento = txtInput.Value;
                        //grdList.DataSource = BLL.ConsultaExpedienteB.GetByAsunto(asunto, id_oficina_origen, verTodo);
                        break;
                    default:
                        grdList.DataSource = BLL.ConsultaEmpleadoB.GetEmpleados();
                        break;

                }
                grdList.DataBind();
                if (grdList.Rows.Count > 0)
                {
                    grdList.UseAccessibleHeader = true;
                    grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la Busqueda!" + e.ToString());
                throw e;
            }
            finally
            {
                objSeguridad = null;
            }
        }


        private void CalcPageNumber()
        {
            double result = 0, mod = 0;
            //result = (oList.TotalRows) / intPageSize;
            //mod = oList.TotalRows % intPageSize;

            if (mod > 0)
                CountPage = Decimal.Floor(Convert.ToDecimal(result)) + 1;
        }


        protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            else
            {
            }
        }


        #endregion


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            BindList();

        }

        protected void cmdNuevo_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Nuevo_empleado");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("empleado.aspx?op=nuevo");

            objSeguridad = null;
        }


        protected void cmdModifica_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            int legajo = 0;
            string mensaje = "";
            bool find = false;

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                GridViewRow row = grdList.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if (isChecked)
                {
                    find = true;
                    legajo = Convert.ToInt32(grdList.DataKeys[i].Values["legajo"]);
                    Session["legajo"] = legajo.ToString();
                    break;
                }
            }

            if (find == false)
            {
                mensaje = "Debe seleccionar el Legajo del Empleado...";
                Alerta(mensaje);
            }
            else
            {
                Response.Redirect("empleado.aspx?legajo=" + legajo.ToString());
            }
        }


        private void showAlert(bool trueFalse, string mensaje)
        {
            string alertMsg = "";
            if (trueFalse == true)
            {
                alertMsg += mensaje;
            }
            else
            {
                alertMsg = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "dbAlert", "alert(\'" + alertMsg + "\');", true);
        }

        protected void modalPopupCaratula()
        {
            string strJS = "<script language='javascript'> ";
            //strJS += "PopUp('../impresiones/caratulaexpa4.aspx', '_blank', '900', '800')";
            strJS += "cmdPrintCaratula('../impresiones/caratulaexpediente.aspx')";
            strJS += "</script>";
            ClientScript.RegisterStartupScript(GetType(), "poscript", strJS);
        }

        protected void modalPopupConstancia()
        {
            string strJS = "<script language='javascript'> ";
            //strJS += "PopUp('../impresiones/WebForm1.aspx', '_blank', '900', '800')";
            strJS += "cmdPrintConstancia('../impresiones/constanciaexp.aspx')";
            strJS += "</script>";
            ClientScript.RegisterStartupScript(GetType(), "poscript", strJS);
        }




        protected void Alerta(string mensaje)
        {
            uPanelMSj.Visible = true;
            divAlerta.Visible = true;
            msj.InnerText = mensaje;
            modalMSJ.Show();

        }

        protected void cmdEliminar_Click(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "BAJA_EMPLEADO");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                EliminaExpedienteSeleccionado();


            objSeguridad = null;
        }

        protected void EliminaExpedienteSeleccionado()
        {
            int codigo = 0;
            int anio = 0; int nro_expediente = 0;
            string mensaje = "";
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                GridViewRow row = grdList.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                codigo = Convert.ToInt32(grdList.DataKeys[i].Values["cod_estado_expediente"]);

                if (isChecked)
                {
                    if (codigo == 1)
                    {
                        anio = Convert.ToInt32(grdList.DataKeys[i].Values["anio"]);
                        nro_expediente = Convert.ToInt32(grdList.DataKeys[i].Values["nro_expediente"]);
                        EliminaExpediente(anio, nro_expediente);
                        mensaje = "";
                    }
                    else
                    {
                        mensaje = "No se puede Eliminar el Legajo...";
                        //showAlert(true, mensaje);
                        //Alerta(mensaje);
                    }
                    break;
                }
                else
                    mensaje = "Debe seleccionar un Legajo...";
            }
            if (mensaje.Length > 0)
                Alerta(mensaje);
            BindList();
        }




        protected void EliminaExpediente(int anio, int nro_expediente)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            //objExpediente = new ExpedienteB();
            try
            {
                //objExpediente.BajaExpedienteByID(anio, nro_expediente);
            }
            catch (Exception)
            {
                //objExpediente = null;
                throw;
            }

        }

        protected void cmdRevertir_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "ME_REVERTIR_EXPEDIENTE");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                RevertirExpedienteSeleccionado();
            objSeguridad = null;
        }


        protected void RevertirExpedienteSeleccionado()
        {
            string finalizado = "";
            int anio = 0; int nro_expediente = 0;
            string mensaje = "";
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                GridViewRow row = grdList.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                finalizado = Convert.ToString(grdList.DataKeys[i].Values["fecha_fin_tramite"]);

                if (isChecked)
                {
                    if (!string.IsNullOrEmpty(finalizado))
                    {
                        anio = Convert.ToInt32(grdList.DataKeys[i].Values["anio"]);
                        nro_expediente = Convert.ToInt32(grdList.DataKeys[i].Values["nro_expediente"]);
                        RevertirExpediente(anio, nro_expediente, Session["usuario"].ToString());
                        mensaje = "";
                    }
                    else
                    {
                        mensaje = "No se puede Revertir el Expediente porque no esta Finalizado...";
                        //showAlert(true, mensaje);
                    }
                    break;
                }
                else
                    mensaje = "Debe seleccionar un expediente...";
            }
            if (mensaje.Length > 0)
                Alerta(mensaje);

            BindList();
        }




        protected void RevertirExpediente(int anio, int nro_expediente, string usuario)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            //objExpediente = new ExpedienteB();
            try
            {
                //objExpediente.RevertirExpediente(anio, nro_expediente, usuario);
            }
            catch (Exception)
            {
                //objExpediente = null;
                throw;
            }

        }


        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdList.PageIndex = e.NewPageIndex;
            intPage = grdList.PageIndex;
            BindList();
        }


        private void ExportToExcel(string nameReport, GridView wControl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            wControl.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);
            form.Controls.Add(wControl);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=DATA.xls");
            Response.Charset = "UTF-8";

            Response.Write(sb.ToString());
            Response.End();

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportToExcel("Empleados", grdList);
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("list_personal.aspx");
        }

        protected void cmdRecibos_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            //
            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "VER_RECIBO_EMPLEADO");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
            {
                int legajo = 0;
                string mensaje = "";
                bool find = false;

                for (int i = 0; i < grdList.Rows.Count; i++)
                {
                    GridViewRow row = grdList.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if (isChecked)
                    {
                        find = true;
                        legajo = Convert.ToInt32(grdList.DataKeys[i].Values["legajo"]);
                        Session["legajo"] = legajo.ToString();
                        break;
                    }
                }

                if (find == false)
                {
                    mensaje = "Debe seleccionar el Legajo del Empleado...";
                    Alerta(mensaje);
                }
                else
                {
                    Response.Redirect("recibossueldo.aspx?legajo=" + legajo.ToString());
                }
            }
            objSeguridad = null;
        }
    }
}