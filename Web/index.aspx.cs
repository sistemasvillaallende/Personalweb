using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;


namespace Web
{
    public partial class index : System.Web.UI.Page
    {


        BLL.SecurityBLL objSeguridad;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPass.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) __doPostBack('" + btnIngresar.UniqueID + "','')");
                if (Request.QueryString["cerrar"] != null)
                {
                    Session.Abandon();
                }
                txtUsuario.Focus();
            }
        }

        protected int ValidaLicencia()
        {
            string mensaje = string.Empty;
            int licencia = 0;
            licencia = Library.Util.Licencia(out mensaje);

            switch (licencia)
            {
                case 0:
                    break;
                case 1:
                    divError.Visible = true;
                    lblError.InnerHtml = mensaje;
                    divLogIn.Visible = false;
                    txtUsuario.Focus();
                    break;
                case 2:
                    divError.Visible = true;
                    lblError.InnerHtml = mensaje;
                    divLogIn.Visible = false;
                    //txtUsuario.Focus();
                    break;
                default:
                    break;
            }
            return licencia;
        }

        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            if (Request.Cookies["UserSistema"] != null)
            {
                // Eliminar la cookie configurándola con una fecha de expiración en el pasado
                HttpCookie miCookie = new HttpCookie("UserSistema");
                miCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(miCookie);
            }
            Entities.Usuarios objUsu = new Entities.Usuarios();
            bool ok = false;
            int id_oficina = 0;
            objSeguridad = new BLL.SecurityBLL();

            ok = objSeguridad.ValidUser(txtUsuario.Value, txtPass.Value);

            if (ok == true)
            {
                Session["usuario"] = txtUsuario.Value.ToLower();
                ok = false;
                ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "SUELDOS", out id_oficina);
                //"ME_GESTION_EXPEDIENTE", 
                if (ok == true)
                {
                    objUsu = DAL.UsuariosDAL.getUserByNombre(
                        txtUsuario.Value.ToLower());
                    Session["id_oficina_usuario"] = id_oficina;
                    ok = false;
                    ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "SUELDOS", out id_oficina);

                    objUsu = DAL.UsuariosDAL.getPermisoEvaluacion(
                        txtUsuario.Value.ToLower());
                    if (objUsu.id_secretaria != 0)
                    {
                        HttpCookie cookie1 = new HttpCookie("UserSistema");
                        cookie1["id_oficina_usuario"] = id_oficina.ToString();
                        cookie1["id_direccion"] = "";
                        cookie1["id_secretaria"] = objUsu.id_secretaria.ToString();
                        cookie1["direccion"] = "";
                        cookie1["secretaria"] = objUsu.secretaria.ToString();
                        cookie1["usuario"] = txtUsuario.Value;
                        cookie1["nombreUsuario"] = objUsu.nombre_completo;
                        cookie1["rrhh"] = "SI";
                        cookie1.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie1);
                        FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);

                        Response.Redirect("~\\secure\\Dashboard.aspx");
                    }
                    if (objUsu.id_direccion != 0)
                    {
                        HttpCookie cookie2 = new HttpCookie("UserSistema");
                        cookie2["id_oficina_usuario"] = id_oficina.ToString();
                        cookie2["id_direccion"] = objUsu.id_direccion.ToString();
                        cookie2["id_secretaria"] = "";
                        cookie2["direccion"] = objUsu.direccion;
                        cookie2["secretaria"] = "";
                        cookie2["usuario"] = txtUsuario.Value;
                        cookie2["nombreUsuario"] = objUsu.nombre_completo;
                        cookie2["rrhh"] = "SI";
                        cookie2.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie2);
                        FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);

                        Response.Redirect("~\\secure\\Dashboard.aspx");
                    }
                    HttpCookie cookie = new HttpCookie("UserSistema");
                    cookie["id_oficina_usuario"] = id_oficina.ToString();
                    cookie["id_direccion"] = "";
                    cookie["id_secretaria"] = "";
                    cookie["direccion"] = "";
                    cookie["secretaria"] = "";
                    cookie["rrhh"] = "SI";
                    cookie["usuario"] = txtUsuario.Value;
                    cookie["nombreUsuario"] = objUsu.nombre_completo;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);

                    Response.Redirect("~\\secure\\Dashboard.aspx");
                }

                else
                {
                    ok = false;
                    ok = objSeguridad.ValidaPermiso(txtUsuario.Value, "EVALUACION_SECRETARIOS", out id_oficina);
                    if (ok == true)
                    {
                        Session["usuario"] = txtUsuario.Value.ToLower();
                        objUsu = DAL.UsuariosDAL.getPermisoEvaluacion(
                            txtUsuario.Value.ToLower());
                        if (objUsu.id_secretaria != 0)
                        {
                            Session["id_oficina_usuario"] = id_oficina;
                            //
                            HttpCookie cookie = new HttpCookie("UserSistema");
                            cookie["id_oficina_usuario"] = id_oficina.ToString();
                            cookie["id_direccion"] = "";
                            cookie["id_secretaria"] = objUsu.id_secretaria.ToString();
                            cookie["direccion"] = "";
                            cookie["secretaria"] = objUsu.secretaria.ToString();
                            cookie["usuario"] = txtUsuario.Value;
                            cookie["rrhh"] = "NO";
                            cookie["nombreUsuario"] = objUsu.nombre_completo;
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(cookie);

                            FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);
                            Response.Redirect("~\\Autoridades\\Secretarias\\DashboardSecretaria.aspx");
                        }
                    }
                    ok = false;
                    ok = objSeguridad.ValidaPermiso(txtUsuario.Value, "EVALUACION_DIRECTORES", out id_oficina);
                    if (ok == true)
                    {
                        objUsu = DAL.UsuariosDAL.getPermisoEvaluacion(
                            txtUsuario.Value.ToLower());
                        if (objUsu.id_direccion != 0)
                        {
                            Session["id_oficina_usuario"] = id_oficina;
                            //
                            HttpCookie cookie = new HttpCookie("UserSistema");
                            cookie["id_oficina_usuario"] = id_oficina.ToString();
                            cookie["id_direccion"] = objUsu.id_direccion.ToString();
                            cookie["id_secretaria"] = "0";
                            cookie["direccion"] = objUsu.direccion.ToString();
                            cookie["secretaria"] = "";
                            cookie["rrhh"] = "NO";
                            cookie["usuario"] = txtUsuario.Value;
                            cookie["nombreUsuario"] = objUsu.nombre_completo;
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(cookie);

                            FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);
                            Response.Redirect("~\\Autoridades\\Direcciones\\Dashboard.aspx");
                        }
                    }
                    else
                    {
                        //Response.Redirect("~\\secure\\accesodenegado.html");
                        divError.Visible = true;
                        lblError.InnerHtml = "Ud. no está autorizado a Visualizar esta Página,<br/>Por favor comuníquese con nuestra<br/>" +
                                              "Oficina de Sistemas al Interno 255/256.";
                        divLogIn.Visible = false;
                        txtUsuario.Focus();

                    }
                }
            }
            objSeguridad = null;
        }

        protected void btnOkError_Click(object sender, EventArgs e)
        {
            divError.Visible = false;
            lblError.InnerHtml = string.Empty;
            divLogIn.Visible = true;
            txtUsuario.Focus();
        }
    }
}