using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Direcciones
{
    public partial class Dashboard : System.Web.UI.Page
    {
        [WebMethod]
        public static object ResultadoEvaluacion(string idDireccion)
        {
            return DAL.Fichas.Resultados_x_filtro.getByDireccion(3,
                int.Parse(idDireccion));
        }
        [WebMethod]
        public static List<DAL.EstadisticaSueldos> sueldosPlanta(string idDireccion)
        {
            try
            {

                List<DAL.EstadisticaSueldos> lst = 
                    DAL.EstadisticaSueldos.readSueldosPlantaDireccion(
                    DateTime.Now.Month - 1, DateTime.Now.Year, int.Parse(idDireccion));
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public static int[] ActualizarEstructuraPersonal(string idDireccion)
        {
            try
            {

                List<DAL.DiccionarioDonut> lst = DAL.DiccionarioDonut.readDireccion(
                    int.Parse(idDireccion));
                List<int> lstValores = new List<int>();
                foreach (var item in lst)
                {
                    lstValores.Add(item.valor);
                }
                return lstValores.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static int[] EstadoEvaluador(string idDireccion)
        {
            try
            {
                List<Entities.LstEmpleados> lst =
                    DAL.ConsultaEmpleadoD.GetEmpleadosByDireccion(
                     int.Parse(idDireccion));

                List<int> lstValores = new List<int>();
                int sinNotificar = 0;
                int notificadas = 0;
                int finalizadas = 0;
                int rechazadas = 0;
                int sinRealizar = 0;
                int total = 0;
                int realizadas = 0;

                sinNotificar = lst.Count(n => n.idEstadoEvaluacion == 1);

                notificadas = lst.Count(n => n.idEstadoEvaluacion == 2);

                finalizadas = lst.Count(n => n.idEstadoEvaluacion == 3);

                rechazadas = lst.Count(n => n.idEstadoEvaluacion == 4);
                realizadas = sinNotificar + notificadas + finalizadas;
                total = lst.Count();

                sinRealizar = total - notificadas - finalizadas - rechazadas - sinNotificar;
                lstValores.Add(realizadas);
                lstValores.Add(sinRealizar);


                //sinRealizar = total - notificadas - finalizadas - rechazadas - sinNotificar;
                //lstValores.Add(sinNotificar);
                //lstValores.Add(notificadas);
                //lstValores.Add(finalizadas);
                //lstValores.Add(rechazadas);
                //lstValores.Add(sinRealizar);

                return lstValores.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int idDireccion = 0;
                if (!IsPostBack)
                {
                    if (Request.Cookies["UserSistema"] != null)
                    {
                        if (Request.Cookies["UserSistema"]["direccion"] != null)
                            lblSecretaria.InnerHtml =
                                Request.Cookies["UserSistema"]["direccion"];
                        if (Request.Cookies["UserSistema"]["id_direccion"] != null)
                            idDireccion =
                                int.Parse(Request.Cookies["UserSistema"]["id_direccion"]);

                    }
                    hIdidDireccion.Value = idDireccion.ToString();
                    HtmlGenericControl liDashboard =
    (HtmlGenericControl)Master.FindControl("liDashboard");
                    if (liDashboard != null)
                        liDashboard.Attributes.Add("class", "active");
                    List<DAL.Empleados> lstCumpleaños =
                        DAL.Empleados.getCumplesDireccion(DateTime.Now.Month,
                        DateTime.Now.Day, idDireccion);
                    lblCumpleaños.InnerHtml = lstCumpleaños.Count().ToString();

                    List<DAL.Ausencias> lstAusencias =
                        DAL.Ausencias.readDireccion(DateTime.Now.Month,
                        DateTime.Now.Day, idDireccion, 2023);

                    int licencias =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Contains("Licencia")).Count();

                    int razones =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Contains("Razones")).Count();

                    int sinInformar =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Length == 0).Count();

                    int conAviso = lstAusencias.Count() - licencias -
                        razones - sinInformar;

                    lblAusentesAviso.InnerHtml = conAviso.ToString();
                    lblAusentesSinAviso.InnerHtml = sinInformar.ToString();
                    lblLicencia.InnerHtml = licencias.ToString();
                    lblRazones.InnerHtml = razones.ToString();

                    //List<DAL.Secretarias_grilla> lst =
                    //    DAL.Secretarias_grilla.read(DateTime.Now.Year);

                    //gvSecretarias.DataSource = lst;
                    //gvSecretarias.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}