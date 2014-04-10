using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using SAC.Helper;
using DAL;
using System.Web;

namespace BLL
{
    public class BLLGet_chart_Data
    {
        DAL.DAlGet_chart_Data DGD = new DAlGet_chart_Data();

        /// <summary>
        /// 获取T_INFO_CHART_USERTEMPLATE表中USERID
        /// </summary>
        /// <returns></returns>
        public DataSet Get_Userid()
        {
            return DGD.Get_Userid();
        }

        /// <summary>
        /// 根据USERID从T_INFO_CHART_USERTEMPLATE获取模板ID，说明。现用于TendManage页面DataGrid呈现
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns></returns>
        public DataSet Get_Chartid(string user_id)
        {
            return DGD.Get_Chartid(user_id);
        }

        /// <summary>
        /// 根据CHARTID到T_INFO_CHART_CHARTPARA中查询实时测点、测点名称
        /// </summary>
        /// <param name="chart_id"></param>
        /// <returns></returns>
        public DataSet GetPara_id(string chart_id)
        {
            return DGD.GetPara_id(chart_id);
        }

        /// <summary>
        /// 根据LEVEL的集合从数据库里面获取
        /// </summary>
        /// <param name="id">表名，T_BASE_PARAID_XNY_WIND，T_BASE_PARAID_XNY_SUN</param>
        /// <param name="para_id">全部LEVEL的集合</param>
        /// <returns></returns>
        public DataSet Get_ChartidByLEVEL(string id, string[] para_id)
        {
            return DGD.Get_ChartidByLEVEL(id, para_id);
        }

        /// <summary>
        /// 根据测点名称模糊查询结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="para_id"></param>
        /// <returns></returns>
        public DataSet Get_ChartidByFuzzy_Query(string id, string[] para_id)
        {
            return DGD.Get_ChartidByFuzzy_Query(id, para_id);
        }
        /// <summary>
        /// 从表中得出唯一LEVEL
        /// </summary>
        /// <param name="id">表名</param>
        /// <param name="level_id">LEVELID</param>
        /// <param name="para_id">上一级LEVELID</param>
        /// <returns></returns>
        public DataSet Get_Paraid(string id, string level_id, string para_id)
        {
            return DGD.Get_Paraid(id, level_id, para_id);
        }

        /// <summary>
        /// 根据CHARTID删除T_INFO_CHART_CHARTPARA中相关CHARTID信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete_Chart(string id)
        {
            DGD.Delete_Chart(id);
        }

        /// <summary>
        /// 根据模板ID跟测点删除T_INFO_CHART_CHARTPARA信息
        /// </summary>
        /// <param name="chart_id"></param>
        /// <param name="id"></param>
        public void Delete_Paraid(string chart_id, string id)
        {
            DGD.Delete_Paraid(chart_id, id);
        }
        /// <summary>
        /// 根据T_INFO_CHART_USERTEMPLATE最大的CHARTID生成下一个
        /// </summary>
        /// <returns></returns>
        public DataSet Select_ChartId()
        {

            return DGD.Select_ChartId();
        }

        /// <summary>
        /// 向T_INFO_CHART_USERTEMPLATE，T_INFO_CHART_CHARTPARA里面插入测点数据 ，添加趋势模板用
        /// </summary>
        /// <param name="chart_id"></param>
        /// <param name="user_id"></param>
        /// <param name="chart_desc"></param>
        public void Insert_ChartId(string chart_id, string user_id, string chart_desc)
        {
            DGD.Insert_ChartId(chart_id, user_id, chart_desc);
        }
        public void Insert_para_id(string id)
        {
            DGD.Insert_para_id(id);

        }

        /// <summary>
        /// 向T_INFO_CHART_USERTEMPLATE，T_INFO_CHART_CHARTPARA里面插入测点数据 ，编辑趋势模板用
        /// </summary>
        /// <param name="chart_id"></param>
        /// <param name="id"></param>
        public void Insert_paraid_ByChartid(string chart_id, string id)
        {
            DGD.Insert_paraid_ByChartid(chart_id, id);

        }

        /// <summary>
        /// CHARTID,REALTIME获取T_INFO_CHART_CHARTPARA数据
        /// </summary>
        /// <param name="chart_id"></param>
        /// <param name="para_id"></param>
        /// <returns></returns>
        public DataSet Select_Para_id(string chart_id, string para_id)
        {
            return DGD.Select_Para_id(chart_id, para_id);
        }

        /// <summary>
        /// 根据测点ID 删除T_INFO_CHART_CHARTPARA，T_INFO_CHART_USERTEMPLATE表中所有数据
        /// </summary>
        /// <param name="id">测点ID</param>
        public void Delete_Chart_All(string id)
        {
            DGD.Delete_Chart_All(id);
        }


        /// <summary>
        /// 获取拟合趋势X轴信息
        /// </summary>
        /// <param name="id">机组编号</param>
        /// <param name="flag">测点类型</param>
        /// <returns></returns>
        public DataTable GetXPara(string id, int flag)
        {
            return DGD.GetXPara(id, flag);
        }


        /// <summary>
        /// 获取拟合趋势Y轴信息
        /// </summary>
        /// <param name="id">X轴ID</param>
        /// <returns></returns>
        public DataTable GetYPara(string id)
        {
            return DGD.GetYPara(id);
        }
    }
}
