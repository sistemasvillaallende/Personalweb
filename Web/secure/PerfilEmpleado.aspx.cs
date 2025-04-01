using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Utils;

namespace web.secure
{
    public partial class PerfilEmpleado : System.Web.UI.Page
    {
        private int legajo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.legajo = (int)Convert.ToInt16(this.Request.QueryString["legajo"]);
                this.AsignarDatos(DAL.Temp_empleados.getByPk(this.legajo));
            }
        }
        private void AsignarDatos(DAL.Temp_empleados objEmpleado)
        {
            if (this.legajo > 0)
            {
                this.txtNombre.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.NOMBRE);
                txtNombre.Attributes.Add("title", objEmpleado.NOMBRE);
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

                txtfecha_ingreso.InnerHtml = objEmpleado.FECHA_INGRESO.ToShortDateString();
                txtLegajo.InnerHtml = objEmpleado.LEGAJO.ToString();
                txtTarea.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.TAREA);
                txtCargo.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.CARGO);
                txtSeccion.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.SECCION);
                txtCategoria.InnerHtml = objEmpleado.CATEGORIA.ToString();
                txtClasificacionPersonal.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.CLASIFICACION_PERSONAL);
                txtTipoLiq.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.TIPO_LIQUIDACION);
                txtOficina.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.OFICINA);
                txtPrograma.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.PROGRAMA);
                txtDireccion.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.DIRECCION);
                txtSecretaria.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.SECRETARIA);
                txtRegimen.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.REGIMEN);
                txtEscalaAumento.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.ESCALA_AUMENTO);
                txtSituacionRevista.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.REVISTA);
                txtFechaRevista.InnerHtml = objEmpleado.FECHA_REVISTA.ToShortDateString();
                txtActivo.InnerHtml = objEmpleado.ACTIVO;
                txtImprimeResivo.InnerHtml = objEmpleado.IMPRIME_RECIBO;
                txtCtaBasico.InnerHtml = objEmpleado.NRO_SUELDO_BASICO;
                txtNroCtaGastos.InnerHtml = objEmpleado.NRO_CUENTA_GASTOS;
                if (objEmpleado.NRO_CONTRATO == 0)
                    divContrato.Visible = false;
                else
                    divContrato.Visible = true;
                lblContrato.InnerHtml = objEmpleado.NRO_CONTRATO.ToString();
                lblFecInicio.InnerHtml = objEmpleado.FECHA_INICIO_CONTRATO.ToShortDateString();
                lblFecFin.InnerHtml = objEmpleado.FECHA_FIN_CONTRATO.ToShortDateString();
                lblAntiguedadActual.InnerHtml = objEmpleado.ANTIGUEDD_ACTUAL.ToString();
                lblAntiguedadAnterior.InnerHtml = objEmpleado.ANTIGUEDAD_ANTERIOR.ToString();
                lblNroNombramiento.InnerHtml = objEmpleado.NRO_NOMRAMIENTO.ToString();
                lblFechaNombramiento.InnerHtml = objEmpleado.FECHA_NOMBRAMIENTO.ToShortDateString();
                lblBanco.InnerHtml = Utils.CapitalizarPalabras(objEmpleado.BANCO);
                lblSucursal.InnerHtml = objEmpleado.SUCURSAL;
                lblCuenta.InnerHtml = objEmpleado.CAJA_AHORRO;
                lblCbu.InnerHtml = objEmpleado.CBU;
            }
            //else
            //{
            //    this.txtLegajo.Text = "0";
            //    this.txtfecha_ingreso.Text = DateTime.Today.ToString();
            //    this.txtPais.Text = "ARGENTINA";
            //    this.txtProvincia.Text = "CORDOBA";
            //    this.txtCiudad.Text = "VILLA ALLENDE";
            //    this.ChkActivo.Checked = true;
            //}
        }
    }
}