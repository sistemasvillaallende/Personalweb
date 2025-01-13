using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HtmlAnchor mnuNotificaciones =
(HtmlAnchor)Master.FindControl("mnuNotificaciones");
                    if (mnuNotificaciones != null)
                    {
                        mnuNotificaciones.Attributes.Remove("class");
                        mnuNotificaciones.Attributes.Add("class", "mnuactive");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}