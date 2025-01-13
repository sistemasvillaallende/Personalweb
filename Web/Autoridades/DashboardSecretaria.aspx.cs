using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades
{
    public partial class DashboardSecretaria : System.Web.UI.Page
    {
        [WebMethod]
        public static object ResultadoEvaluacion(string idSecretaria)
        {
            return DAL.Fichas.Resultados_x_filtro.getBySecretaria(3,
                int.Parse(idSecretaria));
        }
        [WebMethod]
        public static int[] sueldosPlanta(string idSecretaria)
        {
            try
            {
                List<int> lst = DAL.DiccionarioDonut.readSueldosPlantaSecretaria(
                    DateTime.Now.Month - 1, DateTime.Now.Year,
                    int.Parse(idSecretaria));
                return lst.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public static int[] EstadoEvaluador(string idSecretaria, 
            string idFicha)
        {
            try
            {
                List<Entities.LstEmpleados> lstDirectores =
                    DAL.ConsultaEmpleadoD.GetDirectoresBySecretarias(
                     int.Parse(idSecretaria), DateTime.Now.Year,
                     int.Parse(idFicha));
                List<Entities.LstEmpleados> lstCargoDirecto =
                    DAL.ConsultaEmpleadoD.GetEmpleadosDirectosBySecretarias(
                     int.Parse(idSecretaria), DateTime.Now.Year);

                List<int> lstValores = new List<int>();
                int sinNotificar = 0;
                int notificadas = 0;
                int finalizadas = 0;
                int rechazadas = 0;
                int sinRealizar = 0;
                int total = 0;

                sinNotificar = lstCargoDirecto.Count(n => n.idEstadoEvaluacion == 1);
                sinNotificar += lstDirectores.Count(n => n.idEstadoEvaluacion == 1);

                notificadas = lstCargoDirecto.Count(n => n.idEstadoEvaluacion == 2);
                notificadas += lstDirectores.Count(n => n.idEstadoEvaluacion == 2);

                finalizadas = lstCargoDirecto.Count(n => n.idEstadoEvaluacion == 3);
                finalizadas += lstDirectores.Count(n => n.idEstadoEvaluacion == 3);

                rechazadas = lstCargoDirecto.Count(n => n.idEstadoEvaluacion == 4);
                rechazadas += lstDirectores.Count(n => n.idEstadoEvaluacion == 4);

                total = lstCargoDirecto.Count();
                total += lstDirectores.Count();

                sinRealizar = total - notificadas - finalizadas - rechazadas - sinNotificar;
                lstValores.Add(notificadas);
                lstValores.Add(notificadas);
                lstValores.Add(finalizadas);
                lstValores.Add(rechazadas);
                lstValores.Add(sinRealizar);

                return lstValores.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public static int[] ActualizarEstructuraPersonal(string idSecretaria)
        {
            try
            {

                List<DAL.DiccionarioDonut> lst = DAL.DiccionarioDonut.read(
                    int.Parse(idSecretaria));
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int idSecretaria = 0;
                if (!IsPostBack)
                {
                    idSecretaria =
        Convert.ToInt32(Request.QueryString["idSec"]);
                    hIdSecretaria.Value = idSecretaria.ToString();
                    HtmlAnchor mnuDashboard =
    (HtmlAnchor)Master.FindControl("mnuDashboard");
                    //if (mnuDashboard != null)
                    //{
                    //    mnuDashboard.Attributes.Remove("class");
                    //    mnuDashboard.Attributes.Add("class", "mnuactive");
                    //}

                    List<DAL.Empleados> lstCumpleaños =
                        DAL.Empleados.getCumplesSecretaria(DateTime.Now.Month,
                        DateTime.Now.Day, idSecretaria);
                    lblCumpleaños.InnerHtml = lstCumpleaños.Count().ToString();

                    List<DAL.Ausencias> lstAusencias =
                        DAL.Ausencias.read(DateTime.Now.Month,
                        DateTime.Now.Day, idSecretaria);

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

                    DAL.Secretarias_grilla
                        objSec = DAL.Secretarias_grilla.getByPk(
                            int.Parse(hIdSecretaria.Value), DateTime.Now.Year);
                    lblAusentesAviso.InnerHtml = conAviso.ToString();
                    lblAusentesSinAviso.InnerHtml = sinInformar.ToString();
                    lblLicencia.InnerHtml = licencias.ToString();
                    lblRazones.InnerHtml = razones.ToString();
                    lblSecretaria.InnerHtml = objSec.SECRETARIA;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}