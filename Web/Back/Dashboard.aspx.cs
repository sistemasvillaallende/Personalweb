using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.secure
{
    public partial class Dashboard : System.Web.UI.Page
    {
        /*[WebMethod]
        public static int[] resultadoEvaluacion()
        {
            try
            {

                List<int> lst = DAL.DiccionarioDonut.(
                    DateTime.Now.Month - 1, DateTime.Now.Year);
                return lst.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        [WebMethod]
        public static object sueldosPlanta()
        {
            try
            {

                object lst = DAL.EstadisticaSueldos.readSueldosPlanta(
                    DateTime.Now.Month - 2, DateTime.Now.Year);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public static object ResultadoEvaluacion()
        {
                
            return DAL.Fichas.Resultados_x_filtro.read(3);
        }
        [WebMethod]
        public static int[] EstadoEvaluador()
        {
            try
            {
                List<Entities.LstEmpleados> lst =
                    DAL.ConsultaEmpleadoD.GetEmpleadosEvaluados();

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
                if (!IsPostBack)
                {
                    HtmlAnchor mnuDashboard =
(HtmlAnchor)Master.FindControl("mnuDashboard");
                    if (mnuDashboard != null)
                    {
                        mnuDashboard.Attributes.Remove("class");
                        mnuDashboard.Attributes.Add("class", "mnuactive");
                    }

                    List<DAL.Empleados> lstCumpleaños =
                        DAL.Empleados.getCumples(DateTime.Now.Month,
                        DateTime.Now.Day);
                    lblCumpleaños.InnerHtml = lstCumpleaños.Count().ToString();

                    List<DAL.Ausencias> lstAusencias =
                        DAL.Ausencias.read(DateTime.Now.Month,
                        DateTime.Now.Day, DateTime.Now.Year);

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
        [WebMethod]
        public static int[] ActualizarEstructuraPersonal()
        {
            try
            {
                List<DAL.DiccionarioDonut> lst = DAL.DiccionarioDonut.read();
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
    }
}