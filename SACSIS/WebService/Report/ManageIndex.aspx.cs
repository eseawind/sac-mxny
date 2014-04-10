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
using System.Data.OleDb;

namespace SACSIS.Report
{
    public partial class Manageindex : System.Web.UI.Page
    {
        string tableName = "T_BASE_CHARTPARAID";
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
                else if (param == "excel")
                {
                    string path = HttpUtility.UrlDecode(Request["path"]);
                    UpExcel(path, "table");
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

        #region 上传Excel
        public void UpExcel(string strExcelFileName, string strSheetName)
        {
            string sql = "";
            DataTable dtExcel = ExcelToDataTable(strExcelFileName, strSheetName);
            if (dtExcel != null && dtExcel.Rows.Count > 0)
            {

                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    sql += "insert into " + tableName + "(T_PARAID,T_PARADESC,T_PARATYPE,T_SQL,T_REALTIME,T_LEVEL1,T_LEVEL2,T_LEVEL3,I_FLAG) values";
                    sql += "('" + dtExcel.Rows[i][1] + "','" + dtExcel.Rows[i][2] + "','" + dtExcel.Rows[i][3] + "','" + dtExcel.Rows[i][4] + "','" + dtExcel.Rows[i][5] + "','" + dtExcel.Rows[i][6] + "','" + dtExcel.Rows[i][7] + "','" + dtExcel.Rows[i][8] + "'," + dtExcel.Rows[i][9] + ");";
                }
            }
            bool judge = false;
            judge = bll.RunSQL(sql);
            sql = "";
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

        #region Excel转换成DataTable
        public DataTable ExcelToDataTable(string strExcelFileName, string strSheetName)
        {
            //源的定义
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strExcelFileName + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

            //Sql语句
            //string strExcel = string.Format("select * from [{0}$]", strSheetName); 这是一种方法
            string strExcel = "select * from   [Sheet1$]";

            //定义存放的数据表
            DataSet ds = new DataSet();

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);

            conn.Open();

            //适配到数据源
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
            adapter.Fill(ds, strSheetName);

            conn.Close();

            return ds.Tables[strSheetName];
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
                ht.Add("ID", item["T_PARAID"].ToString());
                ht.Add("DESC", item["T_PARADESC"].ToString());
     
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