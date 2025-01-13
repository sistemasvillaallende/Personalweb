using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades.Secretarias
{
    public partial class ListaEmpleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HtmlAnchor mnuLstEmpleados =
(HtmlAnchor)Master.FindControl("mnuLstEmpleados");
                    if (mnuLstEmpleados != null)
                    {
                        mnuLstEmpleados.Attributes.Remove("class");
                        mnuLstEmpleados.Attributes.Add("class", "mnuactive");
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