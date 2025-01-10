using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using BLL;
using Entities;

namespace web.secure
{
    public partial class Home : System.Web.UI.Page
    {


        SeguridadB objSeguridad;
        Boolean ok = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
        }



        protected void lnbBuscar_legajo_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");


            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "GESTION_EMPLEADO");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("listempleados.aspx");

            objSeguridad = null;
        }

        protected void lnbAdd_legajo_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "NUEVO_EMPLEADO");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("empleado.aspx?op=nuevo");

            objSeguridad = null;
        }

        protected void lnbConceptos_lq_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Liquidacion");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("conceptos_liq.aspx");

            objSeguridad = null;
        }

        protected void lnbInformes_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");
            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "REPORTES_EMPLEADOS");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("listado_pivot.aspx");

            objSeguridad = null;
        }

        protected void lnbPivot_Click(object sender, EventArgs e)
        {
           
        }

        protected void lnbLiquidacion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Liquidacion");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("liquidaciones.aspx");

            objSeguridad = null;
        }

        protected void lnkConceptos_liq_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCategorias_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Categorias_Empleados");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("categorias_empleados.aspx");

            objSeguridad = null;
        }

        protected void lnkSijcor_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Liquidacion");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("sijcor.aspx");

            objSeguridad = null;
        }

        protected void lnkAportes_Jubilatorios_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Liquidacion");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("aportes_jubilatorios.aspx");

            objSeguridad = null;

        }

        protected void lnkAcreditacion_bancos_Click(object sender, EventArgs e)
        {

            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "Acreditacion_bancos");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("acreditacion_bancos.aspx");

            objSeguridad = null;

        }

        protected void lnkReportes_liq_Click(object sender, EventArgs e)
        {
            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "REPORTELIQUIDACIONES");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("reporteliquidacion.aspx");

            objSeguridad = null;
        }

        protected void lnkReportes_sueldos_Click(object sender, EventArgs e)
        {
            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "IMPRIMIR_RECIBOS_SUE");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("reportesueldos.aspx");

            objSeguridad = null;
        }

        protected void LinkConsulta_Click(object sender, EventArgs e)
        {
            objSeguridad = new SeguridadB();
            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "INFORME_CONCEPTOS_LIQ_EMP");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("informeconceptosv2.aspx");

            objSeguridad = null;
        }

        protected void lnbNovedades_liq_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "NOVEDADES_LIQ_EMP");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("cargar_conceptos.aspx");

            objSeguridad = null;
        }

        protected void lnkNovedades_liq2_Click(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
                Response.Redirect("../login.aspx");

            objSeguridad = new SeguridadB();

            ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "NOVEDADES_LIQ_EMP");
            if (ok == false)
                Response.Redirect("accesodenegado.html");
            else
                Response.Redirect("novedades2.aspx");

            objSeguridad = null;
        }
    }


}