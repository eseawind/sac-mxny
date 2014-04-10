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
    public partial class ManageRepModel : System.Web.UI.Page
    {
        string tableName = "T_BASE_MODEL";
        int count = 0, page = 0, rows = 0;
        ReportBLL bll = new ReportBLL();

        private static string tb = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != null)
            {
                if (param == "Init")
                {
                    InitLoad();
                }
                else if (param == "add")
                {
                    string name = HttpUtility.UrlDecode(Request["name"]);
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    string path = HttpUtility.UrlDecode(Request["path"]);
                    AddTableModel(name, id, path);
                }
                else if (param == "editTable")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    string n_ID = HttpUtility.UrlDecode(Request["ido"]);
                    string n_Name = HttpUtility.UrlDecode(Request["nameo"]);
                    string n_Path = HttpUtility.UrlDecode(Request["patho"]);
                    EditTableModel(id, n_ID, n_Name, n_Path);
                }
                else if (param == "dell")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    DellTableModel(id);
                }
            }
        }

        #region  添加报表名称
        private void DellTableModel(string id)
        {
            bool judge = bll.DellTableModel(id);
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

        #region 编辑报表
        private void EditTableModel(string id, string n_id, string n_name, string n_path)
        {
            bool judge = bll.EddTableModel(id, n_id, n_name, n_path);
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

        #region  添加报表名称
        private void AddTableModel(string name, string id, string path)
        {
            bool judge = bll.AddTableModel(name, path, id);
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
            DataTable dt = bll.GetReportTableList(tableName, "1=1", (page - 1) * rows + 1, page * rows);
            count = bll.GetReportCount(tableName);
            IList<Hashtable> list = new List<Hashtable>();
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                index++;
                ht.Add("IDKEY", index);
                ht.Add("ID", item["T_ID"].ToString());
                ht.Add("NAME", item["T_NAME"].ToString());
                ht.Add("PATH", item["T_PATH"].ToString());

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