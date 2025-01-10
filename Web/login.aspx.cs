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
    public partial class login : System.Web.UI.Page
    {


        BLL.SeguridadB objSeguridad;
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
            bool ok = false;
            int id_oficina = 0;
            objSeguridad = new BLL.SeguridadB();


            ok = objSeguridad.ValidUser(txtUsuario.Value, txtPass.Value);

            if (ok == true)
            {
                Session["usuario"] = txtUsuario.Value.ToLower();
                ok = false;
                ok = objSeguridad.ValidaPermiso(Session["usuario"].ToString(), "SUELDOS", out id_oficina);
                //"ME_GESTION_EXPEDIENTE", 
                if (ok == true)
                {
                    //Entities.Usuarios objUsu = DAL.UsuariosDAL.getUserByNombre(txtUsuario.Value.ToLower());
                    Session["id_oficina_usuario"] = id_oficina;
                    //
                    HttpCookie cookie = new HttpCookie("UserSistema");
                    cookie["id_oficina_usuario"] = id_oficina.ToString();
                    cookie["usuario"] = txtUsuario.Value;
                    //cookie["nombreUsuario"] = objUsu.nombre_completo;
                    cookie.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie);
                    //
                    FormsAuthentication.RedirectFromLoginPage(Session["usuario"].ToString().Replace("%", ""), false);
                    Response.Redirect("~\\secure\\home.aspx");
                }
                else
                {
                    //Response.Redirect("~\\secure\\accesodenegado.html");
                    divError.Visible = true;
                    lblError.InnerHtml = "Ud. no está autorizado a Visualizar esta Página,<br/>Por favor Solicite Permiso a la Oficina de RRHH.<br/>";
                                          //+ "Oficina de Sistemas al Interno 255/256.";
                    divLogIn.Visible = false;
                    txtUsuario.Focus();

                }
            }
            else
            {
                //ImageOutput.Visible = true;
                //lblOutput.Text = "Usuario o Password Incorrecta...";

                if (ok == false)
                {
                    divError.Visible = true;
                    lblError.InnerHtml = "No se puede iniciar la Sesión. <br/>Por favor verifique los datos ingresados.";
                    divLogIn.Visible = false;
                }
                txtUsuario.Focus();
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