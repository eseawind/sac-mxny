using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DBOperations;
using SAC.RealTimeDB;
using System.Collections;
using System.Data;

namespace DAL
{
    public class FactoryDAL
    {
        string sql = "", errMsg = "";
        string[] point = null;
        double val = -1000000;
        object obj = null;
        bool judge = false;
        DataTable dt = new DataTable();
        DBLink db = new DBLink();
        RTDBLink dll = new RTDBLink();

        #region  取值
        /// <summary>
        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double GetPointVal(string points)
        {

            double val = 0;

            dll.OpenPi();

            val = dll.GetRealTimeValue(points);
            val = GetDouble(val, 2);

            return val;
        }

        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double GetHisPointVal(string points, string time)
        {
            double val = 0;
            dll.OpenPi();
            val = dll.GetHisValue(points, time);
            val = GetDouble(val, 2);
            return val;
        }

        /// <summary>
        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double[] GetPointVal(string[] points)
        {
            double[] val = new double[points.Length];
            double v = 0;
            dll.OpenPi();

            for (int i = 0; i < points.Length; i++)
            {
                v = dll.GetRealTimeValue(points[i]);
                v = GetDouble(v, 2);
                val[i] = v;

            }
            return val;
        }

        #region UTC时间转换
        //将一个事件对象转换为UTC格式的时间
        public static int DateTimeToUTC(DateTime DT)
        {
            long a = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            int rtnInt = 0;
            rtnInt = (int)((DT.Ticks - 8 * 3600 * 1e7 - a) / 1e7);
            return rtnInt;
        }
        #endregion

        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="result">要转换的数值</param>
        /// <param name="num">保留位数</param>
        /// <returns></returns>
        public double GetDouble(double result, int num)
        {
            string res = result.ToString();
            string results = "";
            int index = res.IndexOf('.');

            if (res.Length - index == num + 1)
                return Convert.ToDouble(res);
            else
            {
                if (index > 0)
                {
                    index += num;
                    res = res + "000000000000000000";
                    res = res.Remove(0, index + 1);
                    results = result + "000000000000000000";
                    results = results.ToString().Substring(0, index + 1);
                    res = res.Substring(0, 1);

                    string point = "0.";

                    for (int count = 0; count < num - 1; count++)
                    {
                        point += "0";
                    }
                    point += "1";


                    if (Convert.ToInt32(res) > 4)
                    {
                        results = (Convert.ToDouble(results) + Convert.ToDouble(point)).ToString();
                        res = results;
                    }
                    else
                    {
                        res = results;
                    }
                }
                else
                {
                    res += ".";
                    for (int i = 0; i < num; i++)
                    {
                        res += "0";
                    }
                }
                return Convert.ToDouble(res);
            }
        }
        #endregion
        #endregion

        #region 全厂机组负荷曲线
        /// <summary>
        /// 获取全厂机组负荷曲线
        /// </summary>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <param name="count">测点值个数</param>
        /// <returns></returns>
        public IList<Hashtable> GetUnitLine(DateTime st, DateTime et, int count)
        {
            IList<Hashtable> listdata = new List<Hashtable>();
            sql = "SELECT T_UNITID,T_POWERTAG FROM T_BASE_POINT_UNIT;";
            dt = db.RunDataTable(sql, out errMsg);

            if (dt != null && dt.Rows.Count > 0)
            {
                Hashtable ht = new Hashtable();
                ArrayList ld = new ArrayList();
                ArrayList lt = new ArrayList();

                double drv = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ht = new Hashtable();
                    lt = new ArrayList();
                    ht.Add("name", dt.Rows[i][0]);
                    ht.Add("yAxis", i);
                    DateTime _sTime = new DateTime(1970, 1, 1);
                    DateTime dtt = st;
                    dll.OpenPi();
                    while (dtt < et)
                    {
                        ld = new ArrayList();
                        dll.GetHisValue(dt.Rows[i][1].ToString(), dtt.ToString("yyyy-MM-dd HH:mm:ss"), ref drv);
                        if (drv > 0)
                        {
                            drv = GetDouble(drv, 3);
                        }
                        string timeStamp = DateTimeToUTC(dtt).ToString();
                        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        long lTime = long.Parse(timeStamp + "0000000");
                        TimeSpan toNow = new TimeSpan(lTime);
                        DateTime dtResult = dtStart.Add(toNow);
                        ld.Add(Convert.ToInt64((dtResult - _sTime).TotalMilliseconds.ToString()));
                        ld.Add(drv);
                        lt.Add(ld);
                        dtt = dtt.AddSeconds(count);
                    }
                    ht.Add("data", lt);
                    listdata.Add(ht);

                }
            }
            return listdata;
        }
        #endregion

        #region 获取全厂实时负荷
        /// <summary>
        /// 获取机组实时负荷
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetPower()
        {
            sql = "select T_POWERTAG from T_BASE_POINT_UNIT where T_UNITID='0'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion
    }
}
