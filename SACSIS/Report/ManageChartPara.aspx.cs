using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json;
using System.Data;
using System.Collections;

namespace SACSIS.Report
{
    public partial class ManageChartPara : System.Web.UI.Page
    {
        string tableName = "T_BASE_FITTINGPARA";
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
                    string x = HttpUtility.UrlDecode(Request["x"]);
                    string y = HttpUtility.UrlDecode(Request["y"]);

                    AddPoint(x, y);
                }
                else if (param == "dell")
                {
                    string id = Request["id"];
                    DellTable(id);
                }
                else if (param == "edit")
                {
                    string key = Request["key"];
                    string x = HttpUtility.UrlDecode(Request["x"]);
                    string y = HttpUtility.UrlDecode(Request["y"]);

                    EditPoint(key, x, y);
                }

            }
        }

        #region  编辑
        private void EditPoint(string key, string x, string y)
        {
            bool judge = bll.EditPoint(key, x, y, tableName);
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


        #region 删除
        public void DellTable(string id)
        {
            bool judge = bll.Dell(id, tableName);
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

        #region  添加
        private void AddPoint(string x, string y)
        {
            bool judge = bll.AddNHQSMX(x, y, tableName);
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
                ht.Add("X", item["T_XID"].ToString());
                ht.Add("Y", item["T_YID"].ToString());

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