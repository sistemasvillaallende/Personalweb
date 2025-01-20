using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Direcciones
{
    public partial class ListaEmpleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HtmlGenericControl liLstEmpleados =
                    (HtmlGenericControl)Master.FindControl("liLstEmpleados");
                if (liLstEmpleados != null)
                    liLstEmpleados.Attributes.Add("class", "active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}