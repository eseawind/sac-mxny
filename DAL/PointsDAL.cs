using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAC.DBOperations;

namespace DAL
{
    public class PointsDAL
    {
        private string sql = "", errMsg = "";
        private DataTable dt = new DataTable();
        private object obj = new object();
        private DBLink dl = new DBLink();

        /// <summary>
        /// 获取测点
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPoints()
        {
            sql = "select *From T_BASE_POINT_UNIT";
            dt = dl.RunDataTable(sql, out errMsg);
            return dt;
        }
    }
}
