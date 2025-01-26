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
                int mes = 0;
                int anio = 0;
                if (DateTime.Now.Month == 1)
                {
                    anio = DateTime.Now.Year - 1;
                    mes = 11;
                }
                else
                {
                    if (DateTime.Now.Month == 2)
                    {
                        anio = DateTime.Now.Year - 1;
                        mes = 12;
                    }
                    else
                    {
                        anio = DateTime.Now.Year;
                        mes = DateTime.Now.Month - 2;
                    }

                }
                object lst = DAL.EstadisticaSueldos.readSueldosPlanta(
                    mes, anio);
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

                    gvCumple.DataSource = lstCumpleaños;
                    gvCumple.DataBind();
                    if (gvCumple.Rows.Count > 0)
                    {
                        gvCumple.UseAccessibleHeader = true;
                        gvCumple.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    List <DAL.Ausencias> lstAusencias =
                        DAL.Ausencias.read(DateTime.Now.Month,
                        DateTime.Now.Day, DateTime.Now.Year);

                    int licencias =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Contains("Licencia")).Count();

                    List<DAL.Ausencias> lstLic = lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Contains("Licencia"));

                    gvLicencias.DataSource = lstLic;
                    gvLicencias.DataBind();
                    if (gvLicencias.Rows.Count > 0)
                    {
                        gvLicencias.UseAccessibleHeader = true;
                        gvLicencias.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    int razones =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Contains("Razones")).Count();

                    List<DAL.Ausencias> lstRazones = lstAusencias.FindAll(
        Li => Li.CON_DESCRIP.Contains("Razones"));

                    gvRazones.DataSource = lstRazones;
                    gvRazones.DataBind();
                    if (gvRazones.Rows.Count > 0)
                    {
                        gvRazones.UseAccessibleHeader = true;
                        gvRazones.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    int sinInformar =
                        lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Length == 0).Count();

                    List<DAL.Ausencias> lstSinInformar = lstAusencias.FindAll(
                            Li => Li.CON_DESCRIP.Length == 0);


                    gvSin.DataSource = lstSinInformar;
                    gvSin.DataBind();
                    if (gvSin.Rows.Count > 0)
                    {
                        gvSin.UseAccessibleHeader = true;
                        gvSin.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    int conAviso = lstAusencias.Count() - licencias -
                        razones - sinInformar;

                    List<DAL.Ausencias> pre_resultado = 
                        lstAusencias.Except(lstLic).ToList();
                    List<DAL.Ausencias> pre_resultado2 =
                        pre_resultado.Except(lstRazones).ToList();
                    List<DAL.Ausencias> resultado =
                        pre_resultado2.Except(lstSinInformar).ToList();

                    gvCon.DataSource =  resultado; 
                    gvCon.DataBind();
                    if (gvCon.Rows.Count > 0)
                    {
                        gvCon.UseAccessibleHeader = true;
                        gvCon.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

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