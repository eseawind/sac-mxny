using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using BLL;

namespace SACSIS.Report
{
    public partial class ManagePoints : System.Web.UI.Page
    {
        string tableName = "T_BASE_POINT_UNIT";
        int count = 0, page = 0, rows = 0;
        ReportBLL bll = new ReportBLL();

        private static string tb = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != null)
            {
                if (param == "InitTable")
                {
                    InitLoad();
                }
                else if (param == "add")
                {
                    string radio = Request["radio"];
                    string pointType = HttpUtility.UrlDecode(Request["pointType"]);
                    string zbType = HttpUtility.UrlDecode(Request["zbType"]);
                    string point = HttpUtility.UrlDecode(Request["point"]);
                    string jz = HttpUtility.UrlDecode(Request["jz"]);
                    string SQL = HttpUtility.UrlDecode(Request["SQL"]);
                    string cType = HttpUtility.UrlDecode(Request["cType"]);
                    string ID = Request["ID"];
                    string desc = HttpUtility.UrlDecode(Request["desc"]);

                    AddPoint(radio, pointType, zbType, point, jz, SQL, cType, ID, desc);
                }
                else if (param == "dellPoint")
                {
                    string id = Request["id"];
                    DellTable(id);
                }
                else if (param == "edit")
                {
                    string key = Request["key"];
                    string paraid = HttpUtility.UrlDecode(Request["paraid"]);
                    string desc = HttpUtility.UrlDecode(Request["desc"]);
                    string cslx = HttpUtility.UrlDecode(Request["cslx"]);
                    string sql = HttpUtility.UrlDecode(Request["sql"]);
                    string point = HttpUtility.UrlDecode(Request["point"]);
                    string jz = HttpUtility.UrlDecode(Request["jz"]);
                    string zblx = HttpUtility.UrlDecode(Request["zblx"]);
                    string cdlx = HttpUtility.UrlDecode(Request["cdlx"]);
                    string radio = HttpUtility.UrlDecode(Request["radio"]);

                    EditPoint(key, radio, cdlx, zblx, point, jz, sql, cslx, paraid, desc);
                }

            }
        }

        #region  编辑测点
        private void EditPoint(string key, string radio, string pointType, string zbType, string point, string jz, string SQL, string cType, string ID, string desc)
        {
            bool judge = bll.EditPoint(key, ID, desc, pointType, SQL, point, jz, zbType, cType, radio, tableName);
            if (judge)
                count = 1;
            object obj = new
            {
                count = count
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 删除报表
        public void DellTable(string id)
        {
            bool judge = bll.DellPoint(id, tableName);
            if (judge)
                count = 1;
            object obj = new
            {
                count = count
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region  添加测点
        private void AddPoint(string radio, string pointType, string zbType, string point, string jz, string SQL, string cType, string ID, string desc)
        {
            bool judge = bll.AddTableAttribute(ID, desc, pointType, SQL, point, jz, zbType, cType, radio, tableName);
            if (judge)
                count = 1;
            object obj = new
            {
                count = count
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化报表信息
        public void InitLoad()
        {
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            DataTable dt = bll.GetReportTableList(tableName, " 1=1", (page - 1) * rows + 1, page * rows);
            count = bll.GetReportCount(tableName);
            IList<Hashtable> list = new List<Hashtable>();
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                index++;
                ht.Add("KEY", index);
                ht.Add("ID", item["ID_KEY"].ToString());
                ht.Add("T_UNITID", item["T_UNITID"].ToString());
                ht.Add("T_POWERTAG", item["T_POWERTAG"].ToString());
                ht.Add("T_FLOWTAG", item["T_FLOWTAG"].ToString());
                ht.Add("T_PRESSURETAG", item["T_PRESSURETAG"].ToString());
                ht.Add("T_TEMPERATURETAG", item["T_TEMPERATURETAG"].ToString());
                ht.Add("T_REHEATTEMPERATURETAG", item["T_REHEATTEMPERATURETAG"].ToString());
                ht.Add("T_VACUUM", item["T_VACUUM"].ToString());
                ht.Add("T_EFFICIENCY", item["T_EFFICIENCY"].ToString());
                ht.Add("T_HEATCONSUMPTION", item["T_HEATCONSUMPTION"].ToString());
                ht.Add("T_COALCONSUMPTION", item["T_COALCONSUMPTION"].ToString());
                ht.Add("T_INFO_RL", item["T_INFO_RL"].ToString());

                list.Add(ht);
            }
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}