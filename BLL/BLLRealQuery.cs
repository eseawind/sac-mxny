using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

/////创建者：刘海杰
/////创建日期：2013-06-27
namespace BLL
{
    public class BLLRealQuery
    {
        DAL.DALRealQuery DLQ = new DALRealQuery();

        /// <summary>
        /// 获取分公司信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Get_Company_Info(out string errMsg)
        {
            return DLQ.Get_Company_Info(out errMsg);
        }
        /// <summary>
        /// 获取电厂信息
        /// </summary>
        /// <param name="company_id">分公司ID</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Get_Electric_Info(string company_id, out string errMsg)
        {
            return DLQ.Get_Electric_Info(company_id,out errMsg);
        }
        /// <summary>
        /// 获取机组信息
        /// </summary>
        /// <param name="electric_id">电厂id</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Get_Unit_Info(string electric_id, out string errMsg)
        {
            return DLQ.Get_Unit_Info(electric_id, out errMsg);
        }
        /// <summary>
        /// 获取测点信息
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Get_Para_Info(string unit_id, out string errMsg)
        {
            return DLQ.Get_Para_Info(unit_id, out errMsg);
        }

        /// <summary>
        /// 获取参数ID
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Get_BASE_CRICPARA(string unit_id, out string errMsg)
        {
            return DLQ.Get_BASE_CRICPARA(unit_id, out errMsg);
        }

        /// <summary>
        /// 获取实时测点数据
        /// </summary>
        /// <param name="real_data">测点id</param>
        /// <param name="stime">查询的起始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns>返回值数组</returns>
        public IList<Hashtable> GetChartData(string[] real_data, string stime, string etime, out string max_data, out string min_data)
        {
            return DLQ.GetChartData(real_data, stime, etime, out max_data, out min_data);
        }
    }
}
