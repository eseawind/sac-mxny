using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web.Services;
using SAC.OBJ;

namespace SACSIS
{
    public partial class Index : System.Web.UI.Page
    {
        private DataTable dtPoints = new DataTable();
        private RLBLL bllRl = new RLBLL();
        private PointsBLL bllPoints = new PointsBLL();
        private PointBLL bllPoint = new PointBLL();
        private DLBLL bllDl = new DLBLL();
        private string strPower = "", strDl = "YZPP:06.D0";
        private DataRow[] drPoits = null;
        private double[] val = null;
        private object objRl = null;
        private StringBuilder sbl = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _param = Request["param"];
            dtPoints = bllPoints.GetAllPoints();
            if (_param != null)
            {
                if (_param == "initTop")
                {
                    InitTop(dtPoints);
                }
                else if (_param == "table")
                {
                    InitTable(dtPoints);
                }
                else if (_param == "line")
                {
                    InitLine();
                }
            }

        }

        #region 获取趋势名称
        private void InitLine()
        {
            string _name = "";
            for (int i = 0; i < dtPoints.Rows.Count; i++)
            {
                if (dtPoints.Rows[i][1].ToString() == "0")
                {
                    _name += "全厂,";
                }
                else
                {
                    _name += dtPoints.Rows[i][1] + ",";
                }

            }
            _name = _name.Substring(0, _name.Length - 1);
            object _obj = new
            {
                list = _name
            };

            string result = JsonConvert.SerializeObject(_obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 加载表格
        private void InitTable(DataTable _dtPoints)
        {

            //drPoits = _dtPoints.Select("T_UNITID<>'0'");
            //string[] _strUnit = new string[drPoits.Length];
            //string[] _strPoints = new string[drPoits.Length * (_dtPoints.Columns.Count - 3)];
            //int j = 0;
            //for (int i = 0; i < drPoits.Length; i++)
            //{
            //    _strUnit[i] = drPoits[i][1].ToString();
            //    for (int k = 2; k < _dtPoints.Columns.Count - 1; k++)
            //    {
            //        _strPoints[j] = drPoits[i][k].ToString();
            //        j++;
            //    }
            //}
            //double[] _val = new double[_strPoints.Length];
            //_val = bllPoint.GetPointVal(_strPoints);

            //sbl.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#cccccc\">");
            //sbl.Append("<tr class=\"td1\">");
            //sbl.Append("<td>机组</td>");
            //sbl.Append("<td>运行状态</td>");
            //sbl.Append("<td>机组负荷</td>");
            //sbl.Append("<td>主汽流量</td>");
            //sbl.Append("<td>主汽压力</td>");
            //sbl.Append("<td>主汽温度</td>");
            //sbl.Append("<td>再热温度</td>");
            //sbl.Append("<td>真空</td>");
            //sbl.Append("<td>锅炉效率</td>");
            //sbl.Append("<td>热耗</td>");
            //sbl.Append("<td>煤耗</td>");
            //sbl.Append("</tr>");
            //for (int i = 0; i < _strUnit.Length; i++)
            //{
            //    if (i % 2 == 0)
            //        sbl.Append("<tr class=\"td2\">");
            //    else
            //        sbl.Append("<tr class=\"td3\">");
            //    sbl.Append("<td>" + _strUnit[i] + "</td>");
            //    for (int k = -1; k < _dtPoints.Columns.Count - 3; k++)
            //    {
            //        if (k == -1)
            //            if (_val[(k + 1) + (k + 1) * i] < 5)
            //                sbl.Append("<td>停机</td>");
            //            else
            //                sbl.Append("<td>运行</td>");
            //        else
            //            if (_val[k + (dtPoints.Columns.Count - 3) * i] == -1000000)
            //                sbl.Append("<td style='color: red;'>bad</td>");
            //            else
            //                sbl.Append("<td>" + _val[k + (dtPoints.Columns.Count - 3) * i] + "</td>");
            //    }
            //    sbl.Append("</tr>");
            //}
            //sbl.Append("</table>");

            drPoits = _dtPoints.Select("T_UNITID<>'0'");
            string[] _strUnit = new string[drPoits.Length];
            string[] _strPoints = new string[drPoits.Length * (_dtPoints.Columns.Count - 3)];
            for (int i = 0; i < drPoits.Length; i++)
            {
                _strUnit[i] = drPoits[i][1].ToString();

            }
            sbl.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#cccccc\">");
            sbl.Append("<tr class=\"td1\">");
            sbl.Append("<td>机组</td>");
            sbl.Append("<td>运行状态</td>");
            sbl.Append("<td>机组负荷</td>");
            sbl.Append("<td>主汽流量</td>");
            sbl.Append("<td>主汽压力</td>");
            sbl.Append("<td>主汽温度</td>");
            sbl.Append("<td>再热温度</td>");
            sbl.Append("<td>真空</td>");
            sbl.Append("<td>锅炉效率</td>");
            sbl.Append("<td>热耗</td>");
            sbl.Append("<td>煤耗</td>");
            sbl.Append("</tr>");
            double[] _vals = new double[36];
            for (int i = 1; i <= drPoits.Length; i++)
            {
                UnitModel unit = new UnitModel(drPoits[i - 1][1].ToString());
                double[] v = unit.Val;
                for (int k = 0; k < 9; k++)
                {
                    _vals[(i - 1) * 9 + k] = v[k];
                }
                //_vals[(i - 1) * 9 + 0] = unit.Power;
                //_vals[(i - 1) * 9 + 1] = unit.Flow;
                //_vals[(i - 1) * 9 + 2] = unit.Pressure;
                //_vals[(i - 1) * 9 + 3] = unit.Temperature;
                //_vals[(i - 1) * 9 + 4] = unit.ReheatTemperature;
                //_vals[(i - 1) * 9 + 5] = unit.Vacuum;
                //_vals[(i - 1) * 9 + 6] = unit.Efficiency;
                //_vals[(i - 1) * 9 + 7] = unit.Heatconsumption;
                //_vals[(i - 1) * 9 + 8] = unit.Coalconsumption;
            }

            for (int i = 0; i < _strUnit.Length; i++)
            {
                if (i % 2 == 0)
                    sbl.Append("<tr class=\"td2\">");
                else
                    sbl.Append("<tr class=\"td3\">");
                sbl.Append("<td>" + _strUnit[i] + "</td>");
                for (int k = -1; k < _dtPoints.Columns.Count - 3; k++)
                {
                    if (k == -1)
                        if (_vals[(k + 1) + (k + 1) * i] < 5)
                            sbl.Append("<td>停机</td>");
                        else
                            sbl.Append("<td>运行</td>");
                    else
                        if (_vals[k + 9 * i] == -1000000)
                            sbl.Append("<td style='color: red;'>bad</td>");
                        else
                            sbl.Append("<td>" + _vals[k + 9 * i] + "</td>");
                }
                sbl.Append("</tr>");
            }
            sbl.Append("</table>");

            object _obj = new
            {
                tbl = sbl.ToString()
            };

            string result = JsonConvert.SerializeObject(_obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化头部数据
        /// <summary>
        /// 初始化头部数据
        /// </summary>
        /// <param name="_dtPoints"></param>
        private void InitTop(DataTable _dtPoints)
        {
            int a = _dtPoints.Columns.Count;
            objRl = bllRl.GetConfigRL();
            drPoits = _dtPoints.Select("T_UNITID='0'");
            object _objDayDl = bllDl.GetDLAll(strDl, DateTime.Now.ToString("yyyy-MM-dd 0:00:00"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            object _objMonthDl = bllDl.GetDLAll(strDl, DateTime.Now.ToString("yyyy-MM-1 0:00:00"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            object _objYearDl = bllDl.GetDLAll(strDl, DateTime.Now.ToString("yyyy-1-1 0:00:00"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string[] _strPoints = new string[1];
            if (drPoits.Length > 0)
            {
                _strPoints[0] = drPoits[0][2].ToString();
            }
            FactoryModel factory = new FactoryModel();
            double _val = factory.Power;
            //val = bllPoint.GetPointVal(_strPoints);
            object _obj = new
            {
                rl = objRl,
                power = _val,
                day = _objDayDl,
                month = _objMonthDl,
                year = _objYearDl
            };

            string result = JsonConvert.SerializeObject(_obj);
            Response.Write(result);
            Response.End();
        }
        #endregion


    }
}