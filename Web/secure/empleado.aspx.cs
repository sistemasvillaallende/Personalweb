using System;
using BLL;
using Entities;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class empleado : System.Web.UI.Page
    {
        private string operacion = "";
        private int legajo;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("../login.aspx");
            this.operacion = this.Request.QueryString["op"] == null ? "" : Convert.ToString(this.Request.QueryString["op"]);
            this.legajo = (int)Convert.ToInt16(this.Request.QueryString["legajo"]);
            if (!this.Page.IsPostBack)
            {
                this.CargarCombos();
                this.AsignarDatos(EmpleadoB.GetByPkTodos(this.legajo));
            }
            string str = this.Request.Params["__EVENTARGUMENT"];
            if (str == "Confirma")
                this.divConfirma.Visible = false;
            if (str == "Alerta")
                this.divAlerta.Visible = false;
            if (this.operacion == "nuevo")
                this.txtLegajo.Focus();
            else
                this.txtNombre.Focus();
        }

        protected void CargarCombos()
        {
            this.ddRevista.DataTextField = "descripcion";
            this.ddRevista.DataValueField = "id_revista";
            this.ddRevista.DataSource = (object)ConsultaEmpleadoB.ListRevista(0);
            this.ddRevista.DataBind();
            this.ddTipoDNI.DataTextField = "des_tipo_documento";
            this.ddTipoDNI.DataValueField = "cod_tipo_documento";
            this.ddTipoDNI.DataSource = (object)ConsultaEmpleadoB.LisTiposDocumento(0);
            this.ddTipoDNI.DataBind();
            this.ddSecretaria.DataTextField = "descripcion";
            this.ddSecretaria.DataValueField = "id_secretaria";
            this.ddSecretaria.DataSource = (object)ConsultaEmpleadoB.ListSecretarias(0);
            this.ddSecretaria.DataBind();
            this.ddDireccion.DataTextField = "direccion";
            this.ddDireccion.DataValueField = "id_direccion";
            this.ddDireccion.DataSource = (object)ConsultaEmpleadoB.ListDirecciones(0);
            this.ddDireccion.DataBind();
            this.ddCargo.DataTextField = "desc_cargo";
            this.ddCargo.DataValueField = "cod_cargo";
            this.ddCargo.DataSource = (object)ConsultaEmpleadoB.LisCargos(0);
            this.ddCargo.DataBind();
            this.ddCargoCuenta.DataTextField = "desc_cargo";
            this.ddCargoCuenta.DataValueField = "nro_cta";
            this.ddCargoCuenta.DataSource = (object)ConsultaEmpleadoB.ListCargosCuenta("");
            this.ddCargoCuenta.DataBind();
            this.ddSeccion.DataTextField = "des_seccion";
            this.ddSeccion.DataValueField = "cod_seccion";
            this.ddSeccion.DataSource = (object)ConsultaEmpleadoB.ListSecciones(0);
            this.ddSeccion.DataBind();
            this.ddOficina.DataTextField = "nombre_oficina";
            this.ddOficina.DataValueField = "codigo_oficina";
            this.ddOficina.DataSource = (object)ConsultaEmpleadoB.ListOficinas(0);
            this.ddOficina.DataBind();
            this.ddPrograma.DataTextField = "programa";
            this.ddPrograma.DataValueField = "id_programa";
            this.ddPrograma.DataSource = (object)ConsultaEmpleadoB.ListProgramas(0);
            this.ddPrograma.DataBind();
            this.ddCategoria.DataTextField = "des_categoria";
            this.ddCategoria.DataValueField = "cod_categoria";
            this.ddCategoria.DataSource = (object)ConsultaEmpleadoB.ListCategoria(0);
            this.ddCategoria.DataBind();
            this.ddClasificacion_personal.DataTextField = "des_clasif_per";
            this.ddClasificacion_personal.DataValueField = "cod_clasif_per";
            this.ddClasificacion_personal.DataSource = (object)ConsultaEmpleadoB.ListClasificacion_personal(0);
            this.ddClasificacion_personal.DataBind();
            this.ddTipo_liquidacion.DataTextField = "des_tipo_liq";
            this.ddTipo_liquidacion.DataValueField = "cod_tipo_liq";
            this.ddTipo_liquidacion.DataSource = (object)ConsultaEmpleadoB.ListTiposLiquidacion(0);
            this.ddTipo_liquidacion.DataBind();
            this.ddRegimen.DataTextField = "descripcion";
            this.ddRegimen.DataValueField = "cod_regimen_empleado";
            this.ddRegimen.DataSource = (object)ConsultaEmpleadoB.ListRegimen(0);
            this.ddRegimen.DataBind();
            this.ddEscala.DataTextField = "descripcion";
            this.ddEscala.DataValueField = "cod_escala_aumento";
            this.ddEscala.DataSource = (object)ConsultaEmpleadoB.ListEscalaAumentos(0);
            this.ddEscala.DataBind();
            this.ddBanco.DataTextField = "nom_banco";
            this.ddBanco.DataValueField = "cod_banco";
            this.ddBanco.DataSource = (object)ConsultaEmpleadoB.ListBancos(0);
            this.ddBanco.DataBind();
            this.ddTipo_cuenta.DataTextField = "des_tipo_cuenta";
            this.ddTipo_cuenta.DataValueField = "cod_tipo_cuenta";
            this.ddTipo_cuenta.DataSource = (object)ConsultaEmpleadoB.ListTipos_Cuenta(0);
            this.ddTipo_cuenta.DataBind();
            this.ddSexo.DataTextField = "des_sexo";
            this.ddSexo.DataValueField = "cod_sexo";
            this.ddSexo.DataSource = (object)ConsultaEmpleadoB.ListSexos();
            this.ddSexo.DataBind();
            this.ddEstadoCivil.DataTextField = "des_estado_civil";
            this.ddEstadoCivil.DataValueField = "cod_estado_civil";
            this.ddEstadoCivil.DataSource = (object)ConsultaEmpleadoB.ListEstado_Civil();
            this.ddEstadoCivil.DataBind();
        }

        protected void ddSecretaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int32 = Convert.ToInt32(this.ddSecretaria.SelectedValue);
            this.ddDireccion.Items.Clear();
            this.ddDireccion.DataTextField = "direccion";
            this.ddDireccion.DataValueField = "id_direccion";
            this.ddDireccion.DataSource = (object)ConsultaEmpleadoB.ListDirecciones(int32);
            this.ddDireccion.DataBind();
            this.ddDireccion_SelectedIndexChanged((object)null, (EventArgs)null);
        }

        protected void ddDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int32_1 = Convert.ToInt32(this.ddSecretaria.SelectedValue);
            int int32_2 = Convert.ToInt32(this.ddDireccion.SelectedValue);
            this.ddPrograma.Items.Clear();
            this.ddPrograma.DataTextField = "programa";
            this.ddPrograma.DataValueField = "id_programa";
            this.ddPrograma.DataSource = (object)ConsultaEmpleadoB.ListProgramas(int32_1, int32_2);
            this.ddPrograma.DataBind();
        }

        protected void ddCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int int32 = Convert.ToInt32(this.ddCargo.SelectedValue);
            this.ddCargoCuenta.Items.Clear();
            this.ddCargoCuenta.DataTextField = "desc_cargo";
            this.ddCargoCuenta.DataValueField = "nro_cta";
            this.ddCargoCuenta.DataSource = (object)ConsultaEmpleadoB.ListCargosCuenta(int32);
            this.ddCargoCuenta.DataBind();
        }

        private void AsignarDatos(Empleado objEmpleado)
        {
            if (this.legajo > 0)
            {
                this.txtLegajo.Text = objEmpleado.legajo.ToString();
                this.txtNombre.Text = objEmpleado.nombre;
                this.ddTipoDNI.SelectedValue = Convert.ToString(objEmpleado.cod_tipo_documento);
                this.txtNro_documento.Text = objEmpleado.nro_documento;
                this.txtCuil.Text = objEmpleado.cuil;
                this.txtFecha_nacimiento.Text = objEmpleado.fecha_nacimiento;
                this.txtfecha_ingreso.Text = objEmpleado.fecha_ingreso;
                this.txtPais.Text = objEmpleado.pais_domicilio.ToString();
                this.txtProvincia.Text = objEmpleado.provincia_domicilio.ToString();
                this.txtCiudad.Text = objEmpleado.ciudad_domicilio.ToString();
                this.txtBarrio.Text = objEmpleado.barrio_domicilio.ToString();
                this.txtCalle.Text = objEmpleado.calle_domicilio;
                this.txtNro_domicilio.Text = objEmpleado.nro_domicilio;
                this.txtPiso.Text = objEmpleado.piso_domicilio;
                this.txtDpto.Text = objEmpleado.dpto_domicilio;
                this.txtMonoBlock.Text = objEmpleado.monoblock_domicilio;
                this.txtNro_domicilio.Text = objEmpleado.nro_domicilio;
                this.txtCPostal.Text = objEmpleado.cod_postal;
                this.txtTelefono.Text = objEmpleado.telefonos;
                this.txtCelular.Text = objEmpleado.celular;
                this.txtEmail.Text = objEmpleado.email;
                this.ddEstadoCivil.SelectedValue = Convert.ToString(objEmpleado.cod_estado_civil);
                this.ddSexo.SelectedValue = Convert.ToString(objEmpleado.sexo);
                this.txtTarea.Text = objEmpleado.tarea;
                this.ddSeccion.SelectedValue = Convert.ToString(objEmpleado.cod_seccion);
                this.ddCategoria.SelectedValue = Convert.ToString(objEmpleado.cod_categoria);
                this.ddCargo.SelectedValue = Convert.ToString(objEmpleado.cod_cargo);
                this.ddBanco.SelectedValue = Convert.ToString(objEmpleado.cod_banco);
                this.txtNro_sucursal.Text = objEmpleado.nro_sucursal;
                this.ddTipo_cuenta.SelectedValue = Convert.ToString(objEmpleado.tipo_cuenta);
                this.txtNro_caja_ahorro.Text = Convert.ToString(objEmpleado.nro_caja_ahorro);
                this.txtCbu.Text = objEmpleado.nro_cbu;
                this.txtNro_obra_social.Text = objEmpleado.nro_ipam;
                this.txtCuil.Text = objEmpleado.cuil;
                this.txtNro_jubilacion.Text = objEmpleado.nro_jubilacion;
                this.txtAnt_anterior.Text = objEmpleado.antiguedad_ant.ToString();
                this.txtAnt_actual.Text = objEmpleado.antiguedad_actual.ToString();
                this.ddClasificacion_personal.SelectedValue = Convert.ToString(objEmpleado.cod_clasif_per);
                this.ddTipo_liquidacion.SelectedValue = Convert.ToString(objEmpleado.cod_tipo_liq);
                this.ddCargoCuenta.SelectedValue = Convert.ToString(objEmpleado.nro_cta_sb);
                this.txtNro_cta_gastos.Text = objEmpleado.nro_cta_gastos;
                this.txtNro_contrato.Text = objEmpleado.nro_contrato.ToString();
                this.txtFecha_inicio_contrato.Text = objEmpleado.fecha_inicio_contrato;
                this.txtFecha_fin_contrato.Text = objEmpleado.fecha_fin_contrato;
                this.ddSecretaria.SelectedValue = Convert.ToString(objEmpleado.id_secretaria);
                this.ddDireccion.SelectedValue = Convert.ToString(objEmpleado.id_direccion);
                this.ddPrograma.SelectedValue = Convert.ToString(objEmpleado.id_programa);
                this.ddOficina.SelectedValue = Convert.ToString(objEmpleado.id_oficina);
                this.txtNro_nombramiento.Text = objEmpleado.nro_nombramiento;
                this.txtFecha_nombramiento.Text = objEmpleado.fecha_nombramiento;
                this.ddEscala.SelectedValue = Convert.ToString(objEmpleado.cod_escala_aumento);
                this.ddRegimen.SelectedValue = Convert.ToString(objEmpleado.cod_regimen_empleado);
                this.txtFecha_baja.Text = objEmpleado.fecha_baja;
                this.chkImprime.Checked = objEmpleado.imprime_recibo > (short)0;
                this.ddRevista.SelectedValue = Convert.ToString(objEmpleado.id_revista);
                this.txtFecha_revista.Text = objEmpleado.fecha_revista;
                if (objEmpleado.activo)
                    this.ChkActivo.Checked = true;
                else
                    this.ChkActivo.Checked = false;
            }
            else
            {
                this.txtLegajo.Text = "0";
                this.txtfecha_ingreso.Text = DateTime.Today.ToString();
                this.txtPais.Text = "ARGENTINA";
                this.txtProvincia.Text = "CORDOBA";
                this.txtCiudad.Text = "VILLA ALLENDE";
                this.ChkActivo.Checked = true;
            }
        }

        protected void cmdAceptar_tab_empleado_Click(object sender, EventArgs e)
        {
            if (this.legajo == 0)
                this.NuevoEmpleadoTab1();
            else
                this.ModificaEmpleadoTab1();
        }

        protected void NuevoEmpleadoTab1()
        {
            EmpleadoB empleadoB = new EmpleadoB();
            Empleado oEmp = new Empleado();
            oEmp.legajo = Convert.ToInt32(this.txtLegajo.Text);
            oEmp.nombre = this.txtNombre.Text;
            oEmp.fecha_alta_registro = DateTime.Now.ToShortDateString();
            oEmp.fecha_ingreso = this.txtfecha_ingreso.Text;
            oEmp.cod_tipo_documento = Convert.ToInt32(this.ddTipoDNI.SelectedValue);
            oEmp.nro_documento = this.txtNro_documento.Text;
            oEmp.cuil = this.txtCuil.Text;
            oEmp.tarea = this.txtTarea.Text;
            oEmp.cod_cargo = Convert.ToInt32(this.ddCargo.SelectedValue);
            oEmp.cod_seccion = Convert.ToInt32(this.ddSeccion.SelectedValue);
            oEmp.cod_categoria = Convert.ToInt32(this.ddCategoria.SelectedValue);
            oEmp.cod_clasif_per = Convert.ToInt32(this.ddClasificacion_personal.SelectedValue);
            oEmp.cod_tipo_liq = Convert.ToInt32(this.ddTipo_liquidacion.SelectedValue);
            oEmp.id_secretaria = Convert.ToInt32(this.ddSecretaria.SelectedValue);
            oEmp.id_direccion = Convert.ToInt32(this.ddDireccion.SelectedValue);
            oEmp.id_oficina = Convert.ToInt32(this.ddOficina.SelectedValue);
            oEmp.id_programa = Convert.ToInt32(this.ddPrograma.SelectedValue);
            oEmp.cod_regimen_empleado = Convert.ToInt32(this.ddRegimen.SelectedValue);
            oEmp.cod_escala_aumento = Convert.ToInt32(this.ddEscala.SelectedValue);
            oEmp.usuario = Convert.ToString(this.Session["usuario"]);
            oEmp.imprime_recibo = !this.chkImprime.Checked ? (short)0 : (short)1;
            oEmp.nro_cta_sb = Convert.ToString(this.ddCargoCuenta.SelectedValue);
            oEmp.nro_cta_gastos = this.txtNro_cta_gastos.Text;
            oEmp.id_revista = Convert.ToInt32(this.ddRevista.SelectedValue);
            oEmp.fecha_revista = this.txtFecha_revista.Text;
            oEmp.activo = this.ChkActivo.Checked;
            try
            {
                this.txtLegajo.Text = Convert.ToString(EmpleadoB.InsertDatosEmpleado(oEmp));
                this.divConfirma.Visible = true;
                this.msjConfirmar.InnerHtml = "Ok en el Alta del Tab empleado";
                this.PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = ex.ToString() + " Problemas con en el Alta del Tab empleado";
                this.PanelInfomacion.Update();
            }
        }

        protected void ModificaEmpleadoTab1()
        {
            EmpleadoB empleadoB = new EmpleadoB();
            Empleado oEmp = new Empleado();
            oEmp.legajo = Convert.ToInt32(this.txtLegajo.Text);
            oEmp.nombre = this.txtNombre.Text;
            oEmp.fecha_alta_registro = DateTime.Now.ToShortDateString();
            oEmp.fecha_ingreso = this.txtfecha_ingreso.Text;
            oEmp.cod_tipo_documento = Convert.ToInt32(this.ddTipoDNI.SelectedValue);
            oEmp.nro_documento = this.txtNro_documento.Text;
            oEmp.cuil = this.txtCuil.Text;
            oEmp.tarea = this.txtTarea.Text;
            oEmp.cod_cargo = Convert.ToInt32(this.ddCargo.SelectedValue);
            oEmp.cod_seccion = Convert.ToInt32(this.ddSeccion.SelectedValue);
            oEmp.cod_categoria = Convert.ToInt32(this.ddCategoria.SelectedValue);
            oEmp.cod_clasif_per = Convert.ToInt32(this.ddClasificacion_personal.SelectedValue);
            oEmp.cod_tipo_liq = Convert.ToInt32(this.ddTipo_liquidacion.SelectedValue);
            oEmp.id_secretaria = Convert.ToInt32(this.ddSecretaria.SelectedValue);
            oEmp.id_direccion = Convert.ToInt32(this.ddDireccion.SelectedValue);
            oEmp.id_oficina = Convert.ToInt32(this.ddOficina.SelectedValue);
            oEmp.id_programa = Convert.ToInt32(this.ddPrograma.SelectedValue);
            oEmp.cod_regimen_empleado = Convert.ToInt32(this.ddRegimen.SelectedValue);
            oEmp.cod_escala_aumento = Convert.ToInt32(this.ddEscala.SelectedValue);
            oEmp.fecha_baja = this.txtFecha_baja.Text.Length <= 1 ? "" : this.txtFecha_baja.Text;
            this.chkImprime.Checked = oEmp.imprime_recibo == (short)1;
            oEmp.nro_cta_sb = Convert.ToString(this.ddCargoCuenta.SelectedValue);
            oEmp.nro_cta_gastos = this.txtNro_cta_gastos.Text;
            oEmp.id_revista = Convert.ToInt32(this.ddRevista.SelectedValue);
            oEmp.fecha_revista = this.txtFecha_revista.Text;
            oEmp.activo = this.ChkActivo.Checked;
            try
            {
                this.txtLegajo.Text = Convert.ToString(EmpleadoB.UpdateDatosEmpleado(oEmp, Convert.ToString(this.Session["usuario"])));
                this.divConfirma.Visible = true;
                this.msjConfirmar.InnerHtml = "Ok en la Modificacion de los Datos del Empleado...";
                this.PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = " Problemas con  Modificacion de los Datos del Empleado..." + (object)ex;
                this.PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_contrato_Click(object sender, EventArgs e)
        {
            if (this.legajo == 0)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos de Obra Social/Contrato porque no hay un Legajo de Empleado";
                this.PanelInfomacion.Update();
            }
            else
                this.UpdateTab_contrato();
        }

        protected void UpdateTab_contrato()
        {
            EmpleadoB empleadoB = new EmpleadoB();
            Empleado oEmp = new Empleado();
            oEmp.legajo = Convert.ToInt32(this.txtLegajo.Text);
            oEmp.nro_ipam = this.txtNro_obra_social.Text;
            oEmp.nro_jubilacion = this.txtNro_jubilacion.Text;
            oEmp.antiguedad_ant = Convert.ToInt32(this.txtAnt_anterior.Text);
            oEmp.antiguedad_actual = int.Parse(this.txtAnt_actual.Text);
            oEmp.nro_contrato = int.Parse(this.txtNro_contrato.Text);
            oEmp.fecha_inicio_contrato = this.txtFecha_inicio_contrato.Text;
            oEmp.fecha_fin_contrato = this.txtFecha_fin_contrato.Text;
            oEmp.nro_nombramiento = this.txtNro_nombramiento.Text;
            oEmp.fecha_nombramiento = this.txtFecha_nombramiento.Text;
            try
            {
                EmpleadoB.UpdateTab_Datos_Contrato(oEmp, Convert.ToString(this.Session["usuario"]));
                this.divConfirma.Visible = true;
                this.msjConfirmar.InnerHtml = "Ok en el Alta del Tab empleado";
                this.PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = ex.ToString() + " Problemas con en el Alta del Tab empleado";
                this.PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_Datos_Banco_Click(object sender, EventArgs e)
        {
            if (this.legajo == 0)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos de Banco porque no hay un Legajo de Empleado";
                this.PanelInfomacion.Update();
            }
            else
                this.UpdateTab_Datos_Banco();
        }

        protected void UpdateTab_Datos_Banco()
        {
            EmpleadoB empleadoB = new EmpleadoB();
            Empleado oEmp = new Empleado();
            oEmp.legajo = Convert.ToInt32(this.txtLegajo.Text);
            oEmp.cod_banco = Convert.ToInt32(this.ddBanco.SelectedValue);
            oEmp.tipo_cuenta = Convert.ToString(this.ddTipo_cuenta.SelectedValue);
            oEmp.nro_sucursal = this.txtNro_sucursal.Text;
            oEmp.nro_caja_ahorro = this.txtNro_caja_ahorro.Text;
            oEmp.nro_cbu = this.txtCbu.Text;
            try
            {
                EmpleadoB.UpdateTab_Datos_Banco(oEmp, Convert.ToString(this.Session["usuario"]));
                this.divConfirma.Visible = true;
                this.msjConfirmar.InnerHtml = "Ok en el Alta del Tab Datos Bancos";
                this.PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = ex.ToString() + " Problemas con en el Alta del Tab Datos Bancos";
                this.PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_Datos_Particulares_Click(object sender, EventArgs e)
        {
            if (this.legajo == 0)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos Particulares porque no hay un Legajo de Empleado";
                this.PanelInfomacion.Update();
            }
            else
                this.UpdateTab_Datos_Particulares();
        }

        protected void UpdateTab_Datos_Particulares()
        {
            EmpleadoB empleadoB = new EmpleadoB();
            Empleado oEmp = new Empleado();
            oEmp.legajo = Convert.ToInt32(this.txtLegajo.Text);
            oEmp.fecha_nacimiento = this.txtFecha_nacimiento.Text;
            oEmp.sexo = Convert.ToString(this.ddSexo.SelectedValue);
            oEmp.cod_estado_civil = Convert.ToInt32(this.ddEstadoCivil.SelectedValue);
            oEmp.pais_domicilio = this.txtPais.Text;
            oEmp.provincia_domicilio = this.txtProvincia.Text;
            oEmp.ciudad_domicilio = this.txtCiudad.Text;
            oEmp.barrio_domicilio = this.txtBarrio.Text;
            oEmp.calle_domicilio = this.txtCalle.Text;
            oEmp.nro_domicilio = this.txtNro_domicilio.Text;
            oEmp.piso_domicilio = this.txtPiso.Text;
            oEmp.dpto_domicilio = this.txtDpto.Text;
            oEmp.monoblock_domicilio = this.txtMonoBlock.Text;
            oEmp.cod_postal = this.txtCPostal.Text;
            oEmp.telefonos = this.txtTelefono.Text;
            oEmp.celular = this.txtCelular.Text;
            oEmp.email = this.txtEmail.Text;
            try
            {
                EmpleadoB.UpdateTab_Datos_Particulares(oEmp, Convert.ToString(this.Session["usuario"]));
                this.divConfirma.Visible = true;
                this.msjConfirmar.InnerHtml = "Ok en el Alta del Tab Datos Particulares";
                this.PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                this.divAlerta.Visible = true;
                this.msjAlerta.InnerHtml = ex.ToString() + " Problemas con en el Alta del Tab Datos Particulares";
                this.PanelInfomacion.Update();
            }
        }

        protected void btnVolver1_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver2_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver3_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver4_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdConceptos_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("../secure/concepto_emp.aspx?legajo={0}&nombre={1}&op={2}", (object)this.txtLegajo.Text, (object)this.txtNombre.Text, (object)this.operacion));
        }

        protected void cmdCertificaciones_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("../secure/certificaciones.aspx?legajo={0}&nombre={1}&op={2}", (object)this.txtLegajo.Text, (object)this.txtNombre.Text, (object)this.operacion));
        }

        protected void cmdConsLegajo_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("../secure/historial_emp.aspx?legajo={0}&nombre={1}&op={2}", (object)this.txtLegajo.Text, (object)this.txtNombre.Text, (object)this.operacion));
        }

        protected void cmdCancelar_tab_empleado_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_obsocial_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_contrato_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_particulares_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("listempleados.aspx");
        }

        protected void cmdFamiliares_ServerClick(object sender, EventArgs e)
        {
            this.Response.Cookies.Add(new HttpCookie("Empleado")
            {
                ["Legajo"] = this.txtLegajo.Text,
                Expires = DateTime.Now.AddDays(1.0)
            });
            this.Response.Redirect("../secure/familiares.aspx");
        }
    }
}
