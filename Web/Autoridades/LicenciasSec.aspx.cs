using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web.Autoridades
{
    public partial class LicenciasSec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HtmlAnchor mnuLicencias =
(HtmlAnchor)Master.FindControl("mnuLicencias");
                    if (mnuLicencias != null)
                    {
                        mnuLicencias.Attributes.Remove("class");
                        mnuLicencias.Attributes.Add("class", "mnuactive");
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