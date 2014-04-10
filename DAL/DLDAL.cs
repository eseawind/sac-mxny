using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DBOperations;
using System.Data;

namespace DAL
{
    public class DLDAL
    {
        private string sql = "", errMsg = "";
        private DataTable dt = new DataTable();
        private object obj = new object();
        private DBLink dl = new DBLink();

        /// <summary>
        /// 获取电量
        /// </summary>
        /// <param name="point"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <returns></returns>
        public object GetDLAll(string point, string st, string et)
        {
            sql = "select sum(VALUE) value from DAYDATA where PARAID='" + point + "' and (STATCALTIME between '" + st + "' and '" + et + "')";
            obj = dl.RunSingle(sql, out errMsg);
            return obj;
        }
    }
}
