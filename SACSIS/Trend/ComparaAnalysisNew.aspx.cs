/*********************************************
 * 创建人：胡进财
 * 创建日期：2014-1-7
 * 页面功能：拟合 趋势 
 **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Collections;
using Newtonsoft.Json;

namespace SACSIS.Trend
{
    public partial class ComparaAnalysisNew : System.Web.UI.Page
    {
        BLLGet_chart_Data g_bll_chart = new BLLGet_chart_Data();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _param_in = Request["param"];
            if (_param_in != "")
            {
                if (_param_in == "Init")
                {
                    InitPage();
                }
                else if (_param_in == "unit")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    GetElectric(id);
                }
                else if (_param_in == "X")
                {
                    string id = HttpUtility.UrlDecode(Request["id"]);
                    GetY(id);
                }
                else if (_param_in == "line")
                {
                    string str = HttpUtility.UrlDecode(Request["id"]);
                    get_data(str);
                }
            }
        }

        #region 获取Y 集合
        private void GetY(string id)
        {
            IList<Hashtable> l_list_Y = new List<Hashtable>();       /*Y轴集合*/

            DataTable l_dt_T_BASE_PARAID_WIND = new DataTable();

            l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetYPara(id);
            if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
            {
                l_list_Y = DataTableToList(l_dt_T_BASE_PARAID_WIND);
            }

            object obj = new
            {
                y = l_list_Y
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取X Y 集合
        private void GetX(string id)
        {
            IList<Hashtable> l_list_X = new List<Hashtable>();       /*X轴集合*/
            IList<Hashtable> l_list_Y = new List<Hashtable>();       /*Y轴集合*/


            DataTable l_dt_T_BASE_PARAID_WIND = new DataTable();

            l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetXPara(id, 1);
            l_list_X = DataTableToList(l_dt_T_BASE_PARAID_WIND);
            if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
            {
                l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetYPara(l_dt_T_BASE_PARAID_WIND.Rows[0][0].ToString());
                if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
                {
                    l_list_Y = DataTableToList(l_dt_T_BASE_PARAID_WIND);
                }
            }

            object obj = new
            {
                x = l_list_X,
                y = l_list_Y
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取电厂及其集合
        private void GetElectric(string id)
        {
            IList<Hashtable> l_list_X = new List<Hashtable>();           /*X轴集合*/
            IList<Hashtable> l_list_Y = new List<Hashtable>();           /*Y轴集合*/

            DataSet l_ds_T_BASE_PARAID_WIND = new DataSet();
            DataTable l_dt_T_BASE_PARAID_WIND = new DataTable();


            l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetXPara(id, 1);
            l_list_X = DataTableToList(l_dt_T_BASE_PARAID_WIND);
            if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
            {
                l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetYPara(l_dt_T_BASE_PARAID_WIND.Rows[0][0].ToString());
                if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
                {
                    l_list_Y = DataTableToList(l_dt_T_BASE_PARAID_WIND);
                }
            }


            object obj = new
            {
                x = l_list_X,
                y = l_list_Y
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化页面
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            IList<Hashtable> l_list_company = new List<Hashtable>();    /*公司集合*/
            IList<Hashtable> l_list_X = new List<Hashtable>();       /*X轴集合*/
            IList<Hashtable> l_list_Y = new List<Hashtable>();           /*Y轴集合*/


            DataSet l_ds_T_BASE_PARAID_WIND = new DataSet();
            DataTable l_dt_T_BASE_PARAID_WIND = new DataTable();

            l_ds_T_BASE_PARAID_WIND = g_bll_chart.Get_Paraid("administrator.T_BASE_CHARTPARAID", "T_LEVEL1", "");/*公司集合*/
            if (l_ds_T_BASE_PARAID_WIND != null && l_ds_T_BASE_PARAID_WIND.Tables[0].Rows.Count > 0)
            {
                l_list_company = DataTableToList(l_ds_T_BASE_PARAID_WIND.Tables[0]);

                l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetXPara(l_ds_T_BASE_PARAID_WIND.Tables[0].Rows[0][0].ToString(), 1);
                l_list_X = DataTableToList(l_dt_T_BASE_PARAID_WIND);
                if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
                {
                    l_dt_T_BASE_PARAID_WIND = g_bll_chart.GetYPara(l_dt_T_BASE_PARAID_WIND.Rows[0][0].ToString());
                    if (l_dt_T_BASE_PARAID_WIND != null && l_dt_T_BASE_PARAID_WIND.Rows.Count > 0)
                    {
                        l_list_Y = DataTableToList(l_dt_T_BASE_PARAID_WIND);
                    }
                }
            }

            object obj = new
            {
                company = l_list_company,
                x = l_list_X,
                y = l_list_Y
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 从DataTable转化为List 胡进财 2013/02/27
        /// <summary>
        /// 从DataTable转化为List
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <returns>List集合</returns>
        public IList<Hashtable> DataTableToList(DataTable dt)
        {
            IList<Hashtable> list = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<Hashtable>();
                Hashtable ht = null;
                foreach (DataRow row in dt.Rows)
                {
                    ht = new Hashtable();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (row[col.ColumnName] != null && !string.IsNullOrEmpty(Convert.ToString(row[col.ColumnName])))
                        {
                            ht.Add(col.ColumnName.ToUpper(), row[col.ColumnName]);
                        }
                        else
                        {
                            ht.Add(col.ColumnName.ToUpper(), "");
                        }
                    }
                    list.Add(ht);
                }
            }
            return list;
        }
        #endregion

        #region 趋势
        private void get_data(string rating)
        {
            //3200406;60,70,80,;2013-08-01 00:00:00,2013-08-13 08:39:46;q_fd,Eta_H;多项式 ,2;
            string rating_data = rating;
            IList<Hashtable> list = new List<Hashtable>();
            string errMsg = "";
            string stime = rating_data.Split(';')[2].Split(',')[0], etime = rating_data.Split(';')[2].Split(',')[1];
            string[] per = new string[rating_data.Split(';')[1].TrimEnd(',').Split(',').Length];
            string[] para_id = new string[2];
            string hanshu = rating_data.Split(';')[4];
            for (int i = 0; i < rating_data.Split(';')[1].TrimEnd(',').Split(',').Length; i++)
            {
                per[i] = rating_data.Split(';')[1].TrimEnd(',').Split(',')[i];
            }
            for (int i = 0; i < 2; i++)
            {
                para_id[i] = rating_data.Split(';')[3].Split(',')[i];
            }
            string unit_id = rating_data.Split(';')[0];
            BLL.BLLComparaAnalysis BCA = new BLL.BLLComparaAnalysis();
            string[] gongshi = new string[per.Length];
            list = BCA.Get_Required_data(unit_id, para_id, per, hanshu, stime, etime, out gongshi, out errMsg);
            object obj = new
            {
                gongshi = gongshi,
                title = "趋势呈现数据图",
                list = list
            };

            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}