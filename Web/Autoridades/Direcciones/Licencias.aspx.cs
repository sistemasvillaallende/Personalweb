using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Direcciones
{
    public partial class Licencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HtmlGenericControl liLicencias =
                    (HtmlGenericControl)Master.FindControl("liLicencias");
                if (liLicencias != null)
                    liLicencias.Attributes.Add("class", "active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}