using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAC.OBJ;

namespace SACSIS.Model
{
    public partial class Web_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ////string a = "FKPC:00CU0018.1Y";
            UnitModel unit = new UnitModel("#1");
            object power = unit.Power;
            unit.time = DateTime.Now.ToString("yyyy-MM-dd 7:00:00");
            object hisPower = unit.HisPower;
            double judge = unit.judge;
            if (judge == -1000000)
                Response.Write("NO");
            else
                Response.Write(hisPower);


        }
    }
}