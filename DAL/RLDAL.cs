using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAC.DBOperations;
using SAC.Helper;

namespace DAL
{
    public class RLDAL
    {
        private string sql = "", errMsg = "";
        private DataTable dt = new DataTable();
        private DBLink dl = new DBLink();

        /// <summary>
        /// 获取总装机容量
        /// </summary>
        /// <returns></returns>
        public string GetConfigRL()
        {
            return IniHelper.ReadIniData("RelationDBbase", "RL", null);
        }

        #region 获取容量
        object obj = new object();
        double val = -1000000;

        #region 最近容量
        /// <summary>
        /// 最近容量
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <param name="sTime">最近时间</param>
        /// <returns>最近时间点容量</returns>
        public double GetNewestRL(string id, string time)
        {
            sql = "select 容量 from T_INFO_RL where T_ORGID='" + id + "' order by T_TIME desc fetch first 1 rows only";
            obj = dl.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = Convert.ToDouble(obj);
            }
            return val;
        }
        #endregion

        #endregion
    }
}
