using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace SACSIS.Trend
{
    public partial class TendManage : System.Web.UI.Page
    {
        public string user_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //user_id = Request.Cookies["ID_KEY"].Value.ToString();
                user_id = "1";
            }

            string param = Request["param"], chart_id = Request["chart_id"];
            if ((param != "") && (param != null))
            {
                if (param == "search")
                {
                    GET_CHARTID();
                }
            }
            if ((chart_id != "") && (chart_id != null))
            {
                Delete_Chart(chart_id);
            }
        }

        /// <summary>
        /// 删除曲线
        /// </summary>
        /// <param name="id">模板ID</param>
        private void Delete_Chart(string id)
        {
            BLL.BLLGet_chart_Data DGD = new BLL.BLLGet_chart_Data();
            DGD.Delete_Chart_All(id);
            GET_CHARTID();
        }

        /// <summary>
        /// datagrid数据获取
        /// </summary>
        private void GET_CHARTID()
        {

            BLL.BLLGet_chart_Data DGD = new BLL.BLLGet_chart_Data();
            DataSet DS = DGD.Get_Chartid(user_id);
            //this.dl_data.DataBind();
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;


            int count = 0;
            if (DS.Tables[0].Rows.Count > 0)
            {
                count = DS.Tables[0].Rows.Count;
            }


            IList<Hashtable> list = new List<Hashtable>();
            //if (list.Count > 0)
            //{
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("CHARTID", row["T_CHARTID"].ToString());
                ht.Add("id", row["id"].ToString());
                ht.Add("CHARTDESC", row["T_CHARTDESC"].ToString());
                list.Add(ht);
            }
            //}
            object obj = new
            {
                total = count,
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();

        }
    }
}