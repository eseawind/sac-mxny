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
    public partial class MangeShiftRep : System.Web.UI.Page
    {
        string tableName = "T_BASE_SHIFTREPORT";
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
                    string name = HttpUtility.UrlDecode(Request["name"]);
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    string model = Request["model"];
                    AddTableName(name, id, model);
                }
                else if (param == "edittable")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    string name = HttpUtility.UrlDecode(Request["name"]);
                    string ido = HttpUtility.UrlDecode(Request["ido"]);
                    string nameo = HttpUtility.UrlDecode(Request["nameo"]);
                    EditTable(id, name, ido, nameo);
                }
                else if (param == "dellTable")
                {
                    string id = Request["id"];
                    DellTable(id);
                }
                else if (param == "InitSun")
                {
                    if (HttpUtility.UrlDecode(Request["name"]) != null && HttpUtility.UrlDecode(Request["name"]) != "")
                        tb = HttpUtility.UrlDecode(Request["name"]);
                    InitSunLoad(tb);
                }
                else if (param == "point")
                {
                    InitPoints();
                }
                else if (param == "addSun")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    string tid = HttpUtility.UrlDecode(Request["tid"]);
                    string tName = HttpUtility.UrlDecode(Request["tName"]);
                    AddSunInfo(id, tid, tName);
                }
                else if (param == "pointEdit")
                {
                    InitPoints();
                }
                else if (param == "sunEdit")
                {
                    string name1 = HttpUtility.UrlDecode(Request["name1"]);
                    string name2 = HttpUtility.UrlDecode(Request["name2"]);
                    string name3 = HttpUtility.UrlDecode(Request["name3"]);
                    string jz = Request["jz"];
                    string order = Request["order"];
                    string unit = HttpUtility.UrlDecode(Request["unit"]);
                    string remark1 = HttpUtility.UrlDecode(Request["remark1"]);
                    string remark2 = HttpUtility.UrlDecode(Request["remark2"]);
                    string id = Request["id"];
                    string key = Request["key"];
                    EditSunTable(name1, name2, name3, jz, order, unit, remark1, remark2, id, key);
                }
                else if (param == "name")
                {
                    string id = Request["id"];
                    GetPointName(id);
                }
                else if (param == "Attribute")
                {
                    string key = Request["key"];
                    DelTableAttribute(key);
                }
            }
        }

        #region 删除报表属性
        public void DelTableAttribute(string key)
        {
            bool judge = bll.DelTableAttributeByDigital(key, tableName, "ID_KEY");
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

        #region 获取测点名称
        private void GetPointName(string id)
        {
            object _obj = bll.GetPointName(id);

            object obj = new
            {
                name = _obj
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 编辑报表
        private void EditSunTable(string name1, string name2, string name3, string jz, string order, string unit, string remark1, string remark2, string id, string key)
        {
            bool judge = bll.EditsunTableInfo(tableName, name1, name2, name3, jz, order, unit, remark1, remark2, id, key);
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

        #region 添加报表属性
        private void AddSunInfo(string id, string tid, string tName)
        {
            string[] _ponts = id.Split('*');
            string _sql = "";
            int _count = 1;
            object _obj = bll.GetOrder(tid, tableName);

            if (_obj != null)
                _count = Convert.ToInt32(_obj.ToString()) + 1;
            _sql = "";
            _obj = bll.GetModelID(tid, tableName);
            for (int i = 0; i < _ponts.Length; i++)
            {
                if (_ponts[i] != "")
                    _sql += "insert into " + tableName + "(T_PARAID,T_DCNAME,T_REPORTDESC,I_SHOWID,T_MODELID) values ('" + _ponts[i] + "','" + tid + "','" + tName + "'," + (i + _count) + ",'" + _obj + "');";
            }
            _sql += "delete from " + tableName + " where T_PARAID is null or T_PARAID ='';";
            bool judge = false;
            judge = bll.RunSQL(_sql);

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

        #region 初始化报表名称
        /// <summary>
        /// 加载测点集合
        /// </summary>
        public void InitPoints()
        {
            DataTable dt = new DataTable();
            IList<Hashtable> list = new List<Hashtable>();
            dt = bll.GetPoints("DAYDATA");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ID", dt.Rows[i]["PARAID"]);
                    ht.Add("NAME", dt.Rows[i]["PARADESC"]);
                    list.Add(ht);
                }
            }

            object obj = new
            {
                list = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();

        }
        #endregion

        #region 初始化报表属性
        public void InitSunLoad(string table)
        {
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());

            DataTable dt = bll.GetReportTableList(tableName, "T_REPORTDESC='" + table + "'", (page - 1) * rows + 1, page * rows);
            count = bll.GetReportCountList(tableName, "T_REPORTDESC='" + table + "'").Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                index++;
                ht.Add("ID", index);
                ht.Add("ID_KEY", item["ID_KEY"].ToString());
                ht.Add("T_PARAID", item["T_PARAID"].ToString());
                ht.Add("T_DESCRIP", item["T_DESCRIP"].ToString());
                ht.Add("I_JZ", item["I_JZ"].ToString());
                ht.Add("I_SHOWID", item["I_SHOWID"].ToString());
                ht.Add("T_UNIT", item["T_UNIT"].ToString());
                ht.Add("T_FENDESC", item["T_FENDESC"].ToString());
                ht.Add("T_FENDESC2", item["T_FENDESC2"].ToString());
                ht.Add("T_REMARK", item["T_REMARK"].ToString());
                ht.Add("T_REMARK2", item["T_REMARK2"].ToString());
                ht.Add("ROWID", item["ROWID"].ToString());

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

        #region 删除报表
        public void DellTable(string id)
        {
            bool judge = bll.DellTable(id, tableName);
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
        private void EditTable(string id, string name, string ido, string nameo)
        {
            bool judge = bll.EditTable(id, name, ido, nameo, tableName);
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
        private void AddTableName(string name, string id, string model)
        {
            bool judge = bll.AddTable(name, id, model, tableName);
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
            DataTable dt = bll.GetReportTableList(tableName, (page - 1) * rows + 1, page * rows);
            count = bll.GetReportCountList(tableName).Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();
            int index = 0;
            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                index++;
                ht.Add("ID", index);
                ht.Add("T_DCNAME", item["T_DCNAME"].ToString());
                ht.Add("NAME", item["T_REPORTDESC"].ToString());
                ht.Add("MNAME", item["T_NAME"].ToString());
                ht.Add("MPATH", item["T_PATH"].ToString());

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