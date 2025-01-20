using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class familiares : System.Web.UI.Page
    {
        int intPage = 0;
        int legajo = 0;
        string nombre = "";
        string operacion = "";
        DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            legajo = Convert.ToInt32(Request.Cookies["Empleado"]["Legajo"]);
            operacion = Convert.ToString(Request.QueryString["op"]);
            nombre = Convert.ToString(Request.QueryString["nombre"]);

            if (!Page.IsPostBack)
            {
                Session.Add("opcion", 0);
                Session.Add("opcion", 0);
                Session.Add("nro_fam", 0);
                HiddenLegajo.Value = Convert.ToString(legajo);
                AsignarDatos(BLL.EmpleadoB.GetByPkTodos(legajo));
                fillFamiliares(legajo);
                CargarDDL();
            }
            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
            {
                divError.Visible = false;
                //divMsjTraspaso.Visible = false;
                //divMsjAsistencia.Visible = false;
                //divMsjPublicar.Visible = false;
            }

        }

        public void CargarDDL()
        {
            ddTipoDNI.DataTextField = "des_tipo_documento";
            ddTipoDNI.DataValueField = "cod_tipo_documento";
            ddTipoDNI.DataSource = BLL.ConsultaEmpleadoB.LisTiposDocumento(0);
            ddTipoDNI.DataBind();
        }

        public void fillFamiliares(int legajo)
        {
            try
            {
                gvFamiliares.DataSource = DAL.Familiares.read(legajo);
                gvFamiliares.DataBind();
            }
            catch (Exception ex)
            {
                //    divError.Visible = true;
                //    txtError.InnerText = ex.Message;
            }
        }

        public void fillFamiliares(List<DAL.Familiares> lst)
        {
            try
            {
                gvFamiliares.DataSource = lst;
                gvFamiliares.DataBind();
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //txtError.InnerText = ex.Message;
            }
        }

        private void AsignarDatos(Entities.Empleado objEmpleado)
        {
            try
            {
                lblNombreEmpleado.InnerHtml = objEmpleado.nombre;
                lblLegajo.InnerHtml = string.Format("Lejago: {0}", objEmpleado.legajo);
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //txtError.InnerText = ex.Message;
            }
        }


        protected void lbtnNuevo_Click(object sender, EventArgs e)
        {
            lblTituloFormModal.Text = "Nuevo Familiar";
            Clean();
            Session["opcion"] = "1";//Alta
            txtNombre.Focus();

        }

        protected void gvFamiliares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL.Familiares obj = (DAL.Familiares)e.Row.DataItem;
                    HtmlGenericControl lblEdad = (HtmlGenericControl)e.Row.FindControl("lblEdad");
                    HtmlGenericControl lblSexo = (HtmlGenericControl)e.Row.FindControl("lblSexo");
                    if (obj.sexo == 1)
                        lblSexo.InnerHtml = "<span style=\"color:#ff64b5; font-size:24px;\" class=\"fa fa-female\"></span>";
                    else
                        lblSexo.InnerHtml = "<span style=\"color:#29aae3; font-size:24px;\" class=\"fa fa-male\"></span>";
                    //CheckBox chkSalario = (CheckBox)e.Row.FindControl("chkSalario");
                    //chkSalario.Checked = Convert.ToBoolean(obj.salario_familiar);
                    //CheckBox chkIncapacitado = (CheckBox)e.Row.FindControl("chkIncapacitado");
                    //chkIncapacitado.Checked = Convert.ToBoolean(obj.incapacitado);
                    lblEdad.InnerText = string.Format("{0} Años", Edad(Convert.ToDateTime(obj.fecha_nacimiento, culturaFecArgentina)).ToString());
                }
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //txtError.InnerText = ex.Message;
            }
        }

        protected void gvFamiliares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int indicePaginado = index - (gvFamiliares.PageSize * gvFamiliares.PageIndex);
            int legajo = 0;
            int nro_familiar = 0;
            string familiar = "";

            try
            {
                if (e.CommandName == "Page")
                    return;
                legajo = Convert.ToInt32(gvFamiliares.DataKeys[indicePaginado].Values["legajo"]);
                nro_familiar = Convert.ToInt32(gvFamiliares.DataKeys[indicePaginado].Values["nro_familiar"]);
                HiddenNro_fam.Value = nro_familiar.ToString();
                Session["nro_fam"] = HiddenNro_fam.Value;
                familiar = Convert.ToString(gvFamiliares.DataKeys[indicePaginado].Values["nombre"]);
                if (e.CommandName == "editar")
                {
                    DAL.Familiares oFam = DAL.Familiares.getByPk(legajo, nro_familiar);
                    txtNombre.Text = oFam.nombre;
                    ddTipoDNI.SelectedValue = Convert.ToString(oFam.cod_tipo_documento);
                    txtNrodocumento.Text = oFam.nro_documento;
                    txtFecha_nacimiento.Text = oFam.fecha_nacimiento.ToString();
                    ddParentezco.SelectedValue = Convert.ToString(oFam.id_parentezco);
                    ddSexo.SelectedValue = Convert.ToString(oFam.sexo);
                    chkSalario.Checked = oFam.salario_familiar;
                    chkIncapacitado.Checked = oFam.incapacitado;
                    oFam.opcion = 2;//Editar
                    Session["opcion"] = "2";//Editar                    
                    Datos_ModalPopupExtender.Show();
                    txtNombre.Focus();
                }
                if (e.CommandName == "elimina")
                {
                    lblPregunta.Text = "Desea Borrar al Familiar " + familiar + " ?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#myModalMessage').modal('show');", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void gvFamiliares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFamiliares.PageIndex = e.NewPageIndex;
            fillFamiliares(Convert.ToInt32(HiddenLegajo.Value));
        }

        //public int Edad(DateTime fechaNacimiento)
        //{
        //    try
        //    {
        //        //Obtengo la diferencia en años.
        //        int edad = DateTime.Now.Year - fechaNacimiento.Year;

        //        //Obtengo la fecha de cumpleaños de este año.
        //        DateTime nacimientoAhora = fechaNacimiento.AddYears(edad);

        //        //Le resto un año si la fecha actual es anterior 
        //        //al día de nacimiento.
        //        if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
        //        {
        //            edad--;
        //        }

        //        return edad;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static int Edad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:
            DateTime fechaActual = DateTime.Today;

            // Comprueba que la se haya introducido una fecha válida; si 
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje 
            // de advertencia:
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                // Comprueba que el mes de la fecha de nacimiento es mayor 
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
            }
        }


        public static double Edad2(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:

            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaActual - fechaNacimiento;
            double dias = diferencia.TotalDays;
            double edad = Math.Floor(dias / 365);
            return edad;
        }


        private List<DAL.Familiares> leerGrillaFamiliares()
        {
            //decimal total = 0;
            List<DAL.Familiares> lst = new List<DAL.Familiares>();
            DAL.Familiares obj;
            for (int i = 0; i < gvFamiliares.Rows.Count; i++)
            {
                GridViewRow row = gvFamiliares.Rows[i];
                obj = new DAL.Familiares();
                obj.legajo = Convert.ToInt32(gvFamiliares.DataKeys[i].Values["legajo"]);
                obj.nro_familiar = Convert.ToInt32(gvFamiliares.DataKeys[i].Values["nro_familiar"]);
                obj.fecha_alta_registro = Convert.ToDateTime(gvFamiliares.DataKeys[i].Values["fecha_alta_registro"]);
                obj.nombre = Convert.ToString(gvFamiliares.DataKeys[i].Values["nombre"]);
                obj.cod_tipo_documento = Convert.ToInt16(gvFamiliares.DataKeys[i].Values["cod_tipo_documento"]);
                obj.nro_documento = Convert.ToString(gvFamiliares.DataKeys[i].Values["nro_documento"]);
                obj.fecha_nacimiento = Convert.ToDateTime(gvFamiliares.DataKeys[i].Values["fecha_nacimiento"]);
                obj.parentezco = Convert.ToString(gvFamiliares.DataKeys[i].Values["parentezco"]);
                obj.sexo = Convert.ToInt16(gvFamiliares.DataKeys[i].Values["sexo"]);
                obj.salario_familiar = Convert.ToBoolean(gvFamiliares.DataKeys[i].Values["salario_familiar"]);
                obj.incapacitado = Convert.ToBoolean(gvFamiliares.DataKeys[i].Values["incapacitado"]);
                obj.opcion = 0;
                lst.Add(obj);
            }
            return lst;
        }

        protected void Clean()
        {
            txtNombre.Text = "";
            txtNrodocumento.Text = "";
            txtFecha_nacimiento.Text = "";
            ddParentezco.SelectedIndex = 0;
            ddSexo.SelectedIndex = 0;
            chkSalario.Checked = false;
            chkIncapacitado.Checked = false;
        }

        protected void CancelarFam_Click(object sender, EventArgs e)
        {

        }

        protected void AceptarFam_Click(object sender, EventArgs e)
        {

            int op = 0;
            if (Session["opcion"] != null)
                op = Convert.ToInt32(Session["opcion"]);
            //Por el alta
            if (op == 1)
            {
                AddRowFamilia();
                Datos_ModalPopupExtender.Hide();
                lbtnAddFamilia_Click(sender, e);
            }
            if (op == 2)
            {
                UpdateFamilia();
                Datos_ModalPopupExtender.Hide();
                //fillFamiliares(Convert.ToInt32(HiddenLegajo.Value));
            }
        }
        private void UpdateFamilia()
        {
            try
            {
                List<DAL.Familiares> lst = leerGrillaFamiliares();
                DAL.Familiares obj = new DAL.Familiares();
                obj.legajo = int.Parse(HiddenLegajo.Value);
                obj.nro_familiar = Convert.ToInt32(Session["nro_fam"]);
                obj.fecha_alta_registro = DateTime.Now;
                obj.nombre = txtNombre.Text.ToUpper();
                obj.cod_tipo_documento = 1;
                obj.nro_documento = txtNrodocumento.Text;
                obj.fecha_nacimiento = Convert.ToDateTime(txtFecha_nacimiento.Text);
                obj.parentezco = Convert.ToString(ddParentezco.SelectedItem.ToString());
                obj.sexo = Convert.ToInt32(ddSexo.SelectedValue);
                obj.salario_familiar = Convert.ToBoolean(chkSalario.Checked);
                obj.incapacitado = Convert.ToBoolean(chkIncapacitado.Checked);
                obj.id_parentezco = Convert.ToInt32(ddParentezco.SelectedValue);
                obj.opcion = 2;
                if (obj != null)
                {
                    DAL.Familiares.update(obj);
                    fillFamiliares(obj.legajo);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void AddRowFamilia()
        {
            try
            {
                List<DAL.Familiares> lst = leerGrillaFamiliares();
                DAL.Familiares obj = new DAL.Familiares();
                obj.legajo = int.Parse(HiddenLegajo.Value);
                obj.nro_familiar = 0;
                obj.fecha_alta_registro = DateTime.Now;
                obj.nombre = txtNombre.Text.ToUpper();
                obj.cod_tipo_documento = 1;
                obj.nro_documento = txtNrodocumento.Text;
                obj.fecha_nacimiento = Convert.ToDateTime(txtFecha_nacimiento.Text);
                obj.parentezco = Convert.ToString(ddParentezco.SelectedItem.ToString());
                obj.sexo = Convert.ToInt32(ddSexo.SelectedValue);
                obj.salario_familiar = Convert.ToBoolean(chkSalario.Checked);
                obj.incapacitado = Convert.ToBoolean(chkIncapacitado.Checked);
                obj.id_parentezco = Convert.ToInt32(ddParentezco.SelectedValue);
                obj.opcion = 1;
                if (obj != null)
                {
                    lst.Add(obj);
                    DAL.Familiares.insert(obj);
                    fillFamiliares(obj.legajo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LinkButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("empleado.aspx?legajo={0}&nombre={1}&op={2}", HiddenLegajo.Value, nombre, operacion));
        }

        protected void lbtnAddFamilia_Click(object sender, EventArgs e)
        {
            Clean();
            Session["opcion"] = "1";//Alta
            Datos_ModalPopupExtender.Show();
            txtNombre.Focus();
        }

        protected void btnModalYes_ServerClick(object sender, EventArgs e)
        {
            //Alerta("Legajo " + HiddenLegajo.Value +
            //" Nro Familia : " + HiddenNro_fam.Value);
            try
            {
                DAL.Familiares obj = new DAL.Familiares();
                obj.legajo = int.Parse(HiddenLegajo.Value);
                obj.nro_familiar = int.Parse(HiddenNro_fam.Value);
                DAL.Familiares.delete(obj);
                fillFamiliares(obj.legajo);
                //popUpMSJ.Hide();
                Alerta("Baja Ok!!!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            uPanelCliente.Update();
        }

        protected void Alerta(string mensaje)
        {
            uPanelAlerta.Visible = true;
            divAlerta.Visible = true;
            msj.InnerText = mensaje;
            popUpAlerta.Show();
        }

        protected void btnCloseModal_ServerClick(object sender, EventArgs e)
        {

        }

    }
}