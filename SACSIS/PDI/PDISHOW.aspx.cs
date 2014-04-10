using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SACSIS.PDI
{
    public partial class PDISHOW : System.Web.UI.Page
    {
        public string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = Request.QueryString["url"];
            //url = "http://172.18.135.128/PDI/全厂首页图.pdi";
        }
    }
}