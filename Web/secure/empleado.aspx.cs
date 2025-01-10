using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace web.secure
{
    public partial class empleado : System.Web.UI.Page
    {

        string operacion = "";
        int legajo = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            if (Request.QueryString["op"] != null)
                operacion = Convert.ToString(Request.QueryString["op"]);
            else
                operacion = "";

            legajo = Convert.ToInt16(Request.QueryString["legajo"]);
            if (!Page.IsPostBack)
            {
                CargarCombos();
                AsignarDatos(BLL.EmpleadoB.GetByPkTodos(legajo));
            }
            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Confirma")
                divConfirma.Visible = false;
            if (var == "Alerta")
                divAlerta.Visible = false;
            //
            if (operacion == "nuevo")
                txtLegajo.Focus();
            else
                txtNombre.Focus();
        }


        protected void CargarCombos()
        {
            ddRevista.DataTextField = "descripcion";
            ddRevista.DataValueField = "id_revista";
            ddRevista.DataSource = BLL.ConsultaEmpleadoB.ListRevista(0);
            ddRevista.DataBind();

            ddTipoDNI.DataTextField = "des_tipo_documento";
            ddTipoDNI.DataValueField = "cod_tipo_documento";
            ddTipoDNI.DataSource = BLL.ConsultaEmpleadoB.LisTiposDocumento(0);
            ddTipoDNI.DataBind();

            ddSecretaria.DataTextField = "descripcion";
            ddSecretaria.DataValueField = "id_secretaria";
            ddSecretaria.DataSource = BLL.ConsultaEmpleadoB.ListSecretarias(0);
            ddSecretaria.DataBind();

            ddDireccion.DataTextField = "direccion";
            ddDireccion.DataValueField = "id_direccion";
            ddDireccion.DataSource = BLL.ConsultaEmpleadoB.ListDirecciones(0);
            ddDireccion.DataBind();

            ddCargo.DataTextField = "desc_cargo";
            ddCargo.DataValueField = "cod_cargo";
            ddCargo.DataSource = BLL.ConsultaEmpleadoB.LisCargos(0);
            ddCargo.DataBind();

            ddCargoCuenta.DataTextField = "desc_cargo";
            ddCargoCuenta.DataValueField = "nro_cta";
            ddCargoCuenta.DataSource = BLL.ConsultaEmpleadoB.ListCargosCuenta("");
            ddCargoCuenta.DataBind();

            ddSeccion.DataTextField = "des_seccion";
            ddSeccion.DataValueField = "cod_seccion";
            ddSeccion.DataSource = BLL.ConsultaEmpleadoB.ListSecciones(0);
            ddSeccion.DataBind();

            ddOficina.DataTextField = "nombre_oficina";
            ddOficina.DataValueField = "codigo_oficina";
            ddOficina.DataSource = BLL.ConsultaEmpleadoB.ListOficinas(0);
            ddOficina.DataBind();


            ddPrograma.DataTextField = "programa";
            ddPrograma.DataValueField = "id_programa";
            ddPrograma.DataSource = BLL.ConsultaEmpleadoB.ListProgramas(0);
            ddPrograma.DataBind();

            ddCategoria.DataTextField = "des_categoria";
            ddCategoria.DataValueField = "cod_categoria";
            ddCategoria.DataSource = BLL.ConsultaEmpleadoB.ListCategoria(0);
            ddCategoria.DataBind();

            ddClasificacion_personal.DataTextField = "des_clasif_per";
            ddClasificacion_personal.DataValueField = "cod_clasif_per";
            ddClasificacion_personal.DataSource = BLL.ConsultaEmpleadoB.ListClasificacion_personal(0);
            ddClasificacion_personal.DataBind();

            ddTipo_liquidacion.DataTextField = "des_tipo_liq";
            ddTipo_liquidacion.DataValueField = "cod_tipo_liq";
            ddTipo_liquidacion.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            ddTipo_liquidacion.DataBind();

            ddRegimen.DataTextField = "descripcion";
            ddRegimen.DataValueField = "cod_regimen_empleado";
            ddRegimen.DataSource = BLL.ConsultaEmpleadoB.ListRegimen(0);
            ddRegimen.DataBind();

            ddEscala.DataTextField = "descripcion";
            ddEscala.DataValueField = "cod_escala_aumento";
            ddEscala.DataSource = BLL.ConsultaEmpleadoB.ListEscalaAumentos(0);
            ddEscala.DataBind();

            ddBanco.DataTextField = "nom_banco";
            ddBanco.DataValueField = "cod_banco";
            ddBanco.DataSource = BLL.ConsultaEmpleadoB.ListBancos(0);
            ddBanco.DataBind();

            ddTipo_cuenta.DataTextField = "des_tipo_cuenta";
            ddTipo_cuenta.DataValueField = "cod_tipo_cuenta";
            ddTipo_cuenta.DataSource = BLL.ConsultaEmpleadoB.ListTipos_Cuenta(0);
            ddTipo_cuenta.DataBind();

            ddSexo.DataTextField = "des_sexo";
            ddSexo.DataValueField = "cod_sexo";
            ddSexo.DataSource = BLL.ConsultaEmpleadoB.ListSexos();
            ddSexo.DataBind();

            ddEstadoCivil.DataTextField = "des_estado_civil";
            ddEstadoCivil.DataValueField = "cod_estado_civil";
            ddEstadoCivil.DataSource = BLL.ConsultaEmpleadoB.ListEstado_Civil();
            ddEstadoCivil.DataBind();

            ddTipoauditoria.DataTextField = "des_tipo_auditoria";
            ddTipoauditoria.DataValueField = "id";
            ddTipoauditoria.DataSource = BLL.EmpleadoB.GetTipo_auditoria();
            ddTipoauditoria.DataBind();

        }

        protected void ddSecretaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddSecretaria.SelectedValue);
            ddDireccion.Items.Clear();
            ddDireccion.DataTextField = "direccion";
            ddDireccion.DataValueField = "id_direccion";
            ddDireccion.DataSource = BLL.ConsultaEmpleadoB.ListDirecciones(id);
            ddDireccion.DataBind();
            ddDireccion_SelectedIndexChanged(null, null);
        }

        protected void ddDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_secretaria = Convert.ToInt32(ddSecretaria.SelectedValue);
            int id_direccion = Convert.ToInt32(ddDireccion.SelectedValue);
            ddPrograma.Items.Clear();
            ddPrograma.DataTextField = "programa";
            ddPrograma.DataValueField = "id_programa";
            ddPrograma.DataSource = BLL.ConsultaEmpleadoB.ListProgramas(id_secretaria, id_direccion);
            ddPrograma.DataBind();
        }

        protected void ddCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_cargo = Convert.ToInt32(ddCargo.SelectedValue);
            ddCargoCuenta.Items.Clear();
            ddCargoCuenta.DataTextField = "desc_cargo";
            ddCargoCuenta.DataValueField = "nro_cta";
            ddCargoCuenta.DataSource = BLL.ConsultaEmpleadoB.ListCargosCuenta(id_cargo);
            ddCargoCuenta.DataBind();
        }

        private void AsignarDatos(Entities.Empleado objEmpleado)
        {
            if (legajo > 0)
            {
                txtLegajo.Text = objEmpleado.legajo.ToString();
                txtNombre.Text = objEmpleado.nombre;
                ddTipoDNI.SelectedValue = Convert.ToString(objEmpleado.cod_tipo_documento);
                txtNro_documento.Text = objEmpleado.nro_documento;
                txtCuil.Text = objEmpleado.cuil;
                txtFecha_nacimiento.Text = objEmpleado.fecha_nacimiento;
                txtfecha_ingreso.Text = objEmpleado.fecha_ingreso;
                //Domicilio
                txtPais.Text = objEmpleado.pais_domicilio.ToString();
                txtProvincia.Text = objEmpleado.provincia_domicilio.ToString();
                txtCiudad.Text = objEmpleado.ciudad_domicilio.ToString();
                txtBarrio.Text = objEmpleado.barrio_domicilio.ToString();
                txtCalle.Text = objEmpleado.calle_domicilio;
                txtNro_domicilio.Text = objEmpleado.nro_domicilio;
                txtPiso.Text = objEmpleado.piso_domicilio;
                txtDpto.Text = objEmpleado.dpto_domicilio;
                txtMonoBlock.Text = objEmpleado.monoblock_domicilio;
                txtNro_domicilio.Text = objEmpleado.nro_domicilio;
                txtCPostal.Text = objEmpleado.cod_postal;
                txtTelefono.Text = objEmpleado.telefonos;
                txtCelular.Text = objEmpleado.celular;
                txtEmail.Text = objEmpleado.email;
                ddEstadoCivil.SelectedValue = Convert.ToString(objEmpleado.cod_estado_civil);
                ddSexo.SelectedValue = Convert.ToString(objEmpleado.sexo);
                txtTarea.Text = objEmpleado.tarea.Trim();
                ddSeccion.SelectedValue = Convert.ToString(objEmpleado.cod_seccion);
                ddCategoria.SelectedValue = Convert.ToString(objEmpleado.cod_categoria);
                ddCargo.SelectedValue = Convert.ToString(objEmpleado.cod_cargo);
                ddBanco.SelectedValue = Convert.ToString(objEmpleado.cod_banco);
                txtNro_sucursal.Text = objEmpleado.nro_sucursal;
                ddTipo_cuenta.SelectedValue = Convert.ToString(objEmpleado.tipo_cuenta);
                txtNro_caja_ahorro.Text = Convert.ToString(objEmpleado.nro_caja_ahorro);
                txtCbu.Text = objEmpleado.nro_cbu;
                txtNro_obra_social.Text = objEmpleado.nro_ipam;
                txtCuil.Text = objEmpleado.cuil;
                txtNro_jubilacion.Text = objEmpleado.nro_jubilacion;
                txtAnt_anterior.Text = objEmpleado.antiguedad_ant.ToString();
                txtAnt_actual.Text = objEmpleado.antiguedad_actual.ToString();
                ddClasificacion_personal.SelectedValue = Convert.ToString(objEmpleado.cod_clasif_per);
                ddTipo_liquidacion.SelectedValue = Convert.ToString(objEmpleado.cod_tipo_liq);
                //txtNro_cta_sueldo_basico.Text = objEmpleado.nro_cta_sb;
                ddCargoCuenta.SelectedValue = Convert.ToString(objEmpleado.nro_cta_sb);
                txtNro_cta_gastos.Text = objEmpleado.nro_cta_gastos;
                txtNro_contrato.Text = objEmpleado.nro_contrato.ToString();
                txtFecha_inicio_contrato.Text = objEmpleado.fecha_inicio_contrato;
                txtFecha_fin_contrato.Text = objEmpleado.fecha_fin_contrato;
                ddSecretaria.SelectedValue = Convert.ToString(objEmpleado.id_secretaria);
                ddDireccion.SelectedValue = Convert.ToString(objEmpleado.id_direccion);
                ddPrograma.SelectedValue = Convert.ToString(objEmpleado.id_programa);
                ddOficina.SelectedValue = Convert.ToString(objEmpleado.id_oficina);
                txtNro_nombramiento.Text = objEmpleado.nro_nombramiento;
                txtFecha_nombramiento.Text = objEmpleado.fecha_nombramiento;
                ddEscala.SelectedValue = Convert.ToString(objEmpleado.cod_escala_aumento);
                ddRegimen.SelectedValue = Convert.ToString(objEmpleado.cod_regimen_empleado);
                txtFecha_baja.Text = objEmpleado.fecha_baja;

                if (objEmpleado.imprime_recibo > 0)
                    chkImprime.Checked = true;
                else
                    chkImprime.Checked = false;
                //txtFecha_ingreso.Focus();
                ddRevista.SelectedValue = Convert.ToString(objEmpleado.id_revista);
                txtFecha_revista.Text = objEmpleado.fecha_revista;
                if (objEmpleado.activo == true)
                    ChkActivo.Checked = true;
                else
                    ChkActivo.Checked = false;
            }
            else
            {
                txtLegajo.Text = "0";
                txtfecha_ingreso.Text = DateTime.Today.ToString();
                //Domicilio
                txtPais.Text = "ARGENTINA";
                txtProvincia.Text = "CORDOBA";
                txtCiudad.Text = "VILLA ALLENDE";
                ChkActivo.Checked = true;
            }
        }

        protected void cmdAceptar_tab_empleado_Click(object sender, EventArgs e)
        {
            if (legajo == 0)
            {
                NuevoEmpleadoTab1();
            }
            else
            {
                //popUpAuditoria.Show();
                txtObservAuditoria.Focus();
                txtObservAuditoria.Text = string.Empty;
                popUpAuditoria.Show();
                //txtObservAuditoria.Focus();
                //ModificaEmpleadoTab1();
            }
        }

        protected void NuevoEmpleadoTab1()
        {
            BLL.EmpleadoB obj = new BLL.EmpleadoB();
            Entities.Empleado oEmp = new Entities.Empleado();
            int legajo = 0;

            oEmp.legajo = Convert.ToInt32(txtLegajo.Text);
            oEmp.nombre = txtNombre.Text;
            oEmp.fecha_alta_registro = DateTime.Now.ToShortDateString();
            oEmp.fecha_ingreso = txtfecha_ingreso.Text;
            oEmp.cod_tipo_documento = Convert.ToInt32(ddTipoDNI.SelectedValue);
            oEmp.nro_documento = txtNro_documento.Text;
            oEmp.cuil = txtCuil.Text;
            oEmp.tarea = txtTarea.Text;
            oEmp.cod_cargo = Convert.ToInt32(ddCargo.SelectedValue);
            oEmp.cod_seccion = Convert.ToInt32(ddSeccion.SelectedValue);
            oEmp.cod_categoria = Convert.ToInt32(ddCategoria.SelectedValue);
            oEmp.cod_clasif_per = Convert.ToInt32(ddClasificacion_personal.SelectedValue);
            oEmp.cod_tipo_liq = Convert.ToInt32(ddTipo_liquidacion.SelectedValue);
            oEmp.id_secretaria = Convert.ToInt32(ddSecretaria.SelectedValue);
            oEmp.id_direccion = Convert.ToInt32(ddDireccion.SelectedValue);
            oEmp.id_oficina = Convert.ToInt32(ddOficina.SelectedValue);
            oEmp.id_programa = Convert.ToInt32(ddPrograma.SelectedValue);
            oEmp.cod_regimen_empleado = Convert.ToInt32(ddRegimen.SelectedValue);
            oEmp.cod_escala_aumento = Convert.ToInt32(ddEscala.SelectedValue);
            oEmp.usuario = Convert.ToString(Session["usuario"]);
            if (chkImprime.Checked)
                oEmp.imprime_recibo = 1;
            else
                oEmp.imprime_recibo = 0;
            oEmp.nro_cta_sb = Convert.ToString(ddCargoCuenta.SelectedValue);//txtNro_cta_sueldo_basico.Text;
            oEmp.nro_cta_gastos = txtNro_cta_gastos.Text;
            oEmp.id_revista = Convert.ToInt32(ddRevista.SelectedValue);
            oEmp.fecha_revista = txtFecha_revista.Text;
            oEmp.activo = ChkActivo.Checked;
            try
            {
                legajo = BLL.EmpleadoB.InsertDatosEmpleado(oEmp);
                txtLegajo.Text = Convert.ToString(legajo);
                divConfirma.Visible = true;
                msjConfirmar.InnerHtml = "Ok en el Alta del Tab empleado";
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = ex + " Problemas con en el Alta del Tab empleado";
                PanelInfomacion.Update();
            }
        }

        protected void ModificaEmpleadoTab1()
        {
            BLL.EmpleadoB obj = new BLL.EmpleadoB();
            Entities.Empleado oEmp = new Entities.Empleado();
            int legajo = 0;
            int id_tipo_auditoria = 0;
            string des_tipo_auditoria = string.Empty;
            string obsauditoria = string.Empty;
            oEmp.legajo = Convert.ToInt32(txtLegajo.Text);
            oEmp.nombre = txtNombre.Text;
            oEmp.fecha_alta_registro = DateTime.Now.ToShortDateString();
            oEmp.fecha_ingreso = txtfecha_ingreso.Text;
            oEmp.cod_tipo_documento = Convert.ToInt32(ddTipoDNI.SelectedValue);
            oEmp.nro_documento = txtNro_documento.Text;
            oEmp.cuil = txtCuil.Text;
            oEmp.tarea = txtTarea.Text;
            oEmp.cod_cargo = Convert.ToInt32(ddCargo.SelectedValue);
            oEmp.cod_seccion = Convert.ToInt32(ddSeccion.SelectedValue);
            oEmp.cod_categoria = Convert.ToInt32(ddCategoria.SelectedValue);
            oEmp.cod_clasif_per = Convert.ToInt32(ddClasificacion_personal.SelectedValue);
            oEmp.cod_tipo_liq = Convert.ToInt32(ddTipo_liquidacion.SelectedValue);
            oEmp.id_secretaria = Convert.ToInt32(ddSecretaria.SelectedValue);
            oEmp.id_direccion = Convert.ToInt32(ddDireccion.SelectedValue);
            oEmp.id_oficina = Convert.ToInt32(ddOficina.SelectedValue);
            oEmp.id_programa = Convert.ToInt32(ddPrograma.SelectedValue);
            oEmp.cod_regimen_empleado = Convert.ToInt32(ddRegimen.SelectedValue);
            oEmp.cod_escala_aumento = Convert.ToInt32(ddEscala.SelectedValue);
            if (txtFecha_baja.Text.Length > 1)
                oEmp.fecha_baja = txtFecha_baja.Text;
            else
                oEmp.fecha_baja = "";
            if (oEmp.imprime_recibo == 1)
                chkImprime.Checked = true;
            else
                chkImprime.Checked = false;
            oEmp.nro_cta_sb = Convert.ToString(ddCargoCuenta.SelectedValue);//txtNro_cta_sueldo_basico.Text;
            oEmp.nro_cta_gastos = txtNro_cta_gastos.Text;
            oEmp.id_revista = Convert.ToInt32(ddRevista.SelectedValue);
            oEmp.fecha_revista = txtFecha_revista.Text;
            oEmp.activo = ChkActivo.Checked;
            try
            {
                id_tipo_auditoria = Convert.ToInt32(ddTipoauditoria.SelectedValue);
                des_tipo_auditoria = ddTipoauditoria.Text;
                obsauditoria = txtObservAuditoria.Text;
                legajo = BLL.EmpleadoB.UpdateDatosEmpleado(oEmp, Convert.ToString(Session["usuario"]), id_tipo_auditoria, des_tipo_auditoria, obsauditoria);
                txtLegajo.Text = Convert.ToString(legajo);
                divConfirma.Visible = true;
                msjConfirmar.InnerHtml = "Ok en la Modificacion de los Datos del Empleado...";
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = " Problemas con  Modificacion de los Datos del Empleado..." + ex;
                PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_contrato_Click(object sender, EventArgs e)
        {
            if (legajo == 0)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos de Obra Social/Contrato porque no hay un Legajo de Empleado";
                PanelInfomacion.Update();
            }
            else
            {
                UpdateTab_contrato();
            }
        }

        protected void UpdateTab_contrato()
        {
            BLL.EmpleadoB obj = new BLL.EmpleadoB();
            Entities.Empleado oEmp = new Entities.Empleado();
            //
            int id_tipo_auditoria = 0;
            string des_tipo_auditoria = string.Empty;
            string obsauditoria = string.Empty;

            oEmp.legajo = Convert.ToInt32(txtLegajo.Text);
            //oEmp.nro_cta_sb = Convert.ToString(ddCargoCuenta.SelectedValue);//txtNro_cta_sueldo_basico.Text;
            //oEmp.nro_cta_gastos = txtNro_cta_gastos.Text;
            oEmp.nro_ipam = txtNro_obra_social.Text;
            oEmp.nro_jubilacion = txtNro_jubilacion.Text;
            oEmp.antiguedad_ant = Convert.ToInt32(txtAnt_anterior.Text);
            oEmp.antiguedad_actual = Int32.Parse(txtAnt_actual.Text);
            oEmp.nro_contrato = Int32.Parse(txtNro_contrato.Text);
            oEmp.fecha_inicio_contrato = txtFecha_inicio_contrato.Text;
            oEmp.fecha_fin_contrato = txtFecha_fin_contrato.Text;
            oEmp.nro_nombramiento = txtNro_nombramiento.Text;
            oEmp.fecha_nombramiento = txtFecha_nombramiento.Text;
            try
            {
                //id_tipo_auditoria = Convert.ToInt32(ddTipoauditoria.SelectedValue);
                //des_tipo_auditoria = ddTipoauditoria.Text;
                //obsauditoria = txtObservAuditoria.Text;
                BLL.EmpleadoB.UpdateTab_Datos_Contrato(oEmp, Convert.ToString(Session["usuario"]), id_tipo_auditoria, des_tipo_auditoria, obsauditoria);
                divConfirma.Visible = true;
                msjConfirmar.InnerHtml = "Ok en el Alta del Tab empleado";
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = ex + " Problemas con en el Alta del Tab empleado";
                PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_Datos_Banco_Click(object sender, EventArgs e)
        {
            if (legajo == 0)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos de Banco porque no hay un Legajo de Empleado";
                PanelInfomacion.Update();
            }
            else
            {
                UpdateTab_Datos_Banco();
            }
        }

        protected void UpdateTab_Datos_Banco()
        {
            BLL.EmpleadoB obj = new BLL.EmpleadoB();
            Entities.Empleado oEmp = new Entities.Empleado();

            int id_tipo_auditoria = 0;
            string des_tipo_auditoria = string.Empty;
            string obsauditoria = string.Empty;

            oEmp.legajo = Convert.ToInt32(txtLegajo.Text);
            oEmp.cod_banco = Convert.ToInt32(ddBanco.SelectedValue);
            oEmp.tipo_cuenta = Convert.ToString(ddTipo_cuenta.SelectedValue);
            oEmp.nro_sucursal = txtNro_sucursal.Text;
            oEmp.nro_caja_ahorro = txtNro_caja_ahorro.Text;
            oEmp.nro_cbu = txtCbu.Text;

            try
            {
                //id_tipo_auditoria = Convert.ToInt32(ddTipoauditoria.SelectedValue);
                //des_tipo_auditoria = ddTipoauditoria.Text;
                //obsauditoria = txtObservAuditoria.Text;
                BLL.EmpleadoB.UpdateTab_Datos_Banco(oEmp, Convert.ToString(Session["usuario"]), id_tipo_auditoria, des_tipo_auditoria, obsauditoria);
                divConfirma.Visible = true;
                msjConfirmar.InnerHtml = "Ok en el Alta del Tab Datos Bancos";
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = ex + " Problemas con en el Alta del Tab Datos Bancos";
                PanelInfomacion.Update();
            }
        }

        protected void cmdAceptar_tab_Datos_Particulares_Click(object sender, EventArgs e)
        {
            if (legajo == 0)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = "No se Puede Actualizar la Pestaña Datos Particulares porque no hay un Legajo de Empleado";
                PanelInfomacion.Update();
            }
            else
            {
                UpdateTab_Datos_Particulares();
            }
        }

        protected void UpdateTab_Datos_Particulares()
        {
            BLL.EmpleadoB obj = new BLL.EmpleadoB();
            Entities.Empleado oEmp = new Entities.Empleado();

            int id_tipo_auditoria = 0;
            string des_tipo_auditoria = string.Empty;
            string obsauditoria = string.Empty;

            oEmp.legajo = Convert.ToInt32(txtLegajo.Text);
            oEmp.fecha_nacimiento = txtFecha_nacimiento.Text;
            oEmp.sexo = Convert.ToString(ddSexo.SelectedValue);
            oEmp.cod_estado_civil = Convert.ToInt32(ddEstadoCivil.SelectedValue);
            oEmp.pais_domicilio = txtPais.Text;
            oEmp.provincia_domicilio = txtProvincia.Text;
            oEmp.ciudad_domicilio = txtCiudad.Text;
            oEmp.barrio_domicilio = txtBarrio.Text;
            oEmp.calle_domicilio = txtCalle.Text;
            oEmp.nro_domicilio = txtNro_domicilio.Text;
            oEmp.piso_domicilio = txtPiso.Text;
            oEmp.dpto_domicilio = txtDpto.Text;
            oEmp.monoblock_domicilio = txtMonoBlock.Text;
            oEmp.cod_postal = txtCPostal.Text;
            oEmp.telefonos = txtTelefono.Text;
            oEmp.celular = txtCelular.Text;
            oEmp.email = txtEmail.Text;

            try
            {
                //id_tipo_auditoria = Convert.ToInt32(ddTipoauditoria.SelectedValue);
                //des_tipo_auditoria = ddTipoauditoria.Text;
                //obsauditoria = txtObservAuditoria.Text;
                BLL.EmpleadoB.UpdateTab_Datos_Particulares(oEmp, Convert.ToString(Session["usuario"]), id_tipo_auditoria, des_tipo_auditoria, obsauditoria);
                divConfirma.Visible = true;
                msjConfirmar.InnerHtml = "Ok en el Alta del Tab Datos Particulares";
                PanelInfomacion.Update();
            }
            catch (Exception ex)
            {
                divAlerta.Visible = true;
                msjAlerta.InnerHtml = ex + " Problemas con en el Alta del Tab Datos Particulares";
                PanelInfomacion.Update();

            }
        }

        protected void btnVolver1_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver2_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver3_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdVolver4_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdConceptos_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../secure/concepto_emp.aspx?legajo={0}&nombre={1}&op={2}", txtLegajo.Text, txtNombre.Text, operacion));
        }

        protected void cmdCertificaciones_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../secure/certificaciones.aspx?legajo={0}&nombre={1}&op={2}", txtLegajo.Text, txtNombre.Text, operacion));
        }

        protected void cmdConsLegajo_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("../secure/historial_emp.aspx?legajo={0}&nombre={1}&op={2}", txtLegajo.Text, txtNombre.Text, operacion));
        }

        protected void cmdCancelar_tab_empleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_obsocial_Click(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_contrato_Click(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdCancelar_tab_particulares_Click(object sender, EventArgs e)
        {
            Response.Redirect("listempleados.aspx");
        }

        protected void cmdFamiliares_ServerClick(object sender, EventArgs e)
        {
            //
            this.Response.Cookies.Add(new System.Web.HttpCookie("Empleado")
            {
                ["Legajo"] = txtLegajo.Text,
                Expires = DateTime.Now.AddDays(1.0)
            });
            Response.Redirect("../secure/familiares.aspx");
        }

        protected void btnCloseModalAuditoria_ServerClick(object sender, EventArgs e)
        {
            //
        }

        protected void btnCancelarAuditoria_Click(object sender, EventArgs e)
        {
            //
        }

        protected void btnAceptarAuditoria_Click(object sender, EventArgs e)
        {
            ModificaEmpleadoTab1();
        }


    }
}