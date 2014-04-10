using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SACSIS
{
    public partial class paratest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("para1","da");
            this.ReportViewer1.LocalReport.SetParameters(parameters);
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
}