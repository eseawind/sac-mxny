using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;

namespace SACSIS.Trend
{
    public partial class ChartManage : System.Web.UI.Page
    {
        string rating = "", rating_time = "", chart_id, chart_name, jiange = "";
        private IList<Hashtable> list = new List<Hashtable>();
        object obj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"];
            chart_id = Request["chart"];
            chart_name = Request["chart_name"];
            rating_time = Request["rating_time"];
            jiange = Request["jiange"];
            //画曲线。
            if ((rating != "") && (rating != null))
            {
                GET_PARANAME(rating);
            }
            //保存测点
            if ((chart_id != "") && (chart_id != null))
            {
                GetPara_id(chart_id);
            }
            if ((chart_name != "") && (chart_name != null))
            {
                ChangePara(chart_name);
            }
            if (!IsPostBack)
            {
                string userid = Request["user_id"];
                //防止页面直接跳转到该页面
                if ((userid == "") || (userid == null))
                {
                    Response.Redirect("TendManage.aspx", false);
                }
                BindData();
                this.stime.Value = DateTime.Now.ToShortDateString().ToString() + " 00:00:00";
                this.etime.Value = DateTime.Now.ToString();
            }


        }
        /// <summary>
        /// 保存测点，趋势模板
        /// </summary>
        /// <param name="id">测点集合</param>
        private void ChangePara(string id)
        {
            BLL.BLLGet_chart_Data DGD = new BLL.BLLGet_chart_Data();
            if (id.Split('&')[0] == "1")
            {
                if (id.Split('&')[2] == "")
                {
                    DGD.Delete_Chart_All(id.Split('&')[1]);
                }
                else
                {
                    DGD.Delete_Chart(id.Split('&')[1]);
                    for (int i = 0; i < id.Split('&')[2].Split(',').Length; i++)
                    {
                        DGD.Insert_paraid_ByChartid(id.Split('&')[1], id.Split('&')[2].Split(',')[i].Split('|')[0]);
                    }
                }
            }
            else if (id.Split('&')[0] == "2")
            {
                string num = "";
                if (DGD.Select_ChartId().Tables[0].Rows.Count > 0)
                {
                    num = DGD.Select_ChartId().Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    num = "1";
                }
                DGD.Insert_ChartId(num, id.Split('&')[2].ToString(), id.Split('&')[1].ToString());

                for (int i = 0; i < id.Split('&')[3].Split(',').Length; i++)
                {
                    if (DGD.Select_Para_id(num, id.Split('&')[2].ToString()).Tables[0].Rows.Count <= 0)
                    {
                        DGD.Insert_para_id(id.Split('&')[3].Split(',')[i].Split('|')[0]);
                    }
                }

            }
        }

        /// <summary>
        /// 解析测点并画曲线
        /// </summary>
        /// <param name="id"></param>
        private void GetPara_id(string id)
        {
            BLL.BLLGet_chart_Data DGD = new BLL.BLLGet_chart_Data();
            DataSet DS = DGD.GetPara_id(chart_id);
            string str = "";
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                str += DS.Tables[0].Rows[i]["T_REALTIME"].ToString() + "|" + DS.Tables[0].Rows[i]["T_PARADESC"].ToString();
                if ((DS.Tables[0].Rows.Count != 1) || (i != DS.Tables[0].Rows.Count - 1))
                {
                    str += ",";
                }
            }
            if (str != "")
            {
                GET_PARANAME(str.TrimEnd(','));
            }
        }


        //绑定DropDownList默认数据
        private void BindData()
        {
            BLL.BLLGet_chart_Data DGD = new BLL.BLLGet_chart_Data();

            //if (this.sec_type.Items[this.sec_type.SelectedIndex].Value == "T_BASE_PARAID_WIND")
            //{
            DataSet DS = DGD.Get_Paraid("administrator.T_BASE_CHARTPARAID", "T_LEVEL1", "11");
            if (DS != null && DS.Tables[0].Rows.Count > 0)
            {
                DataSet DDS = DGD.Get_Paraid("administrator.T_BASE_CHARTPARAID", "T_LEVEL2", DS.Tables[0].Rows[0][0].ToString());
                DataSet DDDS = DGD.Get_Paraid("administrator.T_BASE_CHARTPARAID", "T_LEVEL3", DDS.Tables[0].Rows[0][0].ToString());
                this.ddl_level1.DataSource = DS;
                this.ddl_level1.DataTextField = "T_LEVEL1";
                this.ddl_level1.DataValueField = "T_LEVEL1";
                this.ddl_level1.DataBind();
                this.ddl_level2.DataSource = DDS;
                this.ddl_level2.DataTextField = "T_LEVEL2";
                this.ddl_level2.DataValueField = "T_LEVEL2";
                this.ddl_level2.DataBind();
                this.ddl_level3.DataSource = DDDS;
                this.ddl_level3.DataTextField = "T_LEVEL3";
                this.ddl_level3.DataValueField = "T_LEVEL3";
                this.ddl_level3.DataBind();
            }
        }

        /// <summary>
        /// 根据测点获取数据
        /// </summary>
        /// <param name="str1">测点集合</param>
        private void GET_PARANAME(string str1)
        {

            BLL.PointBLL PB = new BLL.PointBLL();
            DateTime dtt, DTs;
            string[] str = new string[str1.Split(',').Length];
            for (int i = 0; i < str1.Split(',').Length; i++)
            {
                str[i] = str1.Split(',')[i];
            }
            if ((rating_time != "") && (rating_time != null))
            {
                dtt = DateTime.Parse(rating_time.Split(',')[0]);
                DTs = DateTime.Parse(rating_time.Split(',')[1]);
            }
            else
            {
                dtt = DateTime.Parse(DateTime.Now.ToShortDateString().ToString() + " 00:00:00");
                DTs = DateTime.Now;
            }
            if ((jiange != "") && (jiange != null))
            {
                int seconds = 0;
                if (jiange.Split('|')[1] == "分")
                {
                    seconds = Convert.ToInt32(jiange.Split('|')[0]) * 60;
                }
                else if (jiange.Split('|')[1] == "小时")
                {
                    seconds = Convert.ToInt32(jiange.Split('|')[0]) * 360;
                }
                else
                {
                    seconds = Convert.ToInt32(jiange.Split('|')[0]);
                }
                list = PB.GetHistValAndTIme2(str, dtt, DTs, seconds);
            }
            else
            {
                list = PB.GetHistValAndTIme1(str, dtt, DTs);
            }
            ArrayList list1 = new ArrayList();
            string[] str2 = new string[9] { "#058DC7", "#50B432", "#ED561B", "#DDDF00", "#24CBE5", "#64E572", "#FF9655", "#FFF263", "#6AF9C4" };
            int num1 = 0;
            foreach (Hashtable _ht in list)
            {

                ArrayList _data = (ArrayList)_ht["data"];
                Hashtable _dv1 = new Hashtable();
                _dv1.Add("lineColor", str2[num1]);
                _dv1.Add("title", "{text:''}");
                //_dv.Add("opposite", true);//Y轴右端显示
                _dv1.Add("lineWidth", 1);
                list1.Add(_dv1);
                num1++;

            }
            object obj = new
            {
                str_para_id = str,
                title = "趋势呈现数据图",
                y_data = list1,
                list = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
    }
}