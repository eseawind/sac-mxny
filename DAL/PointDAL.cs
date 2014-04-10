using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SAC.DBOperations;
using SAC.Helper;
using SAC.RealTimeDB;

namespace DAL
{
    public class PointDAL
    {
        double drv = 0;

        string val = "";
        IList<Hashtable> list = new List<Hashtable>();
        DBLink dl = new DBLink();

        /// <summary>
        /// 查询某段时间测点集合的历史值 -刘海杰
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme1(string[] points, DateTime st, DateTime et)
        {
            ArrayList list = new ArrayList();
            Plink pk = new Plink();
            IList<Hashtable> listdata = new List<Hashtable>();

            Hashtable ht = new Hashtable();
            ArrayList ld = new ArrayList();
            ArrayList lt = new ArrayList();
            for (int i = 0; i < points.Length; i++)
            {
                ht = new Hashtable();
                lt = new ArrayList();
                ht.Add("name", points[i].Split('|')[1]);
                ht.Add("yAxis", i);
                DateTime _sTime = new DateTime(1970, 1, 1);
                int seconds = Convert.ToInt32((et - st).TotalSeconds) / 600;
                DateTime dtt = st;
                Plink.OpenPi();
                while (dtt < et)
                {
                    ld = new ArrayList();
                    pk.GetHisValue(points[i].Split('|')[0], dtt.ToString("yyyy-MM-dd HH:mm:ss"), ref drv);
                    if (drv > 0)
                    {
                        drv = getDouble(drv, 3);
                    }
                    string timeStamp = DateTimeToUTC(dtt).ToString();
                    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    long lTime = long.Parse(timeStamp + "0000000");
                    TimeSpan toNow = new TimeSpan(lTime);
                    DateTime dtResult = dtStart.Add(toNow);
                    ld.Add(Convert.ToInt64((dtResult - _sTime).TotalMilliseconds.ToString()));
                    ld.Add(drv);
                    lt.Add(ld);
                    // TimeSpan toNow1 = new TimeSpan(seconds);
                    dtt = dtt.AddSeconds(seconds);
                }
                ht.Add("data", lt);
                listdata.Add(ht);
                //lt = new ArrayList();
                //ht = new Hashtable();
            }

            return listdata;
        }
        /// <summary>
        /// 查询某段时间测点集合的历史值，去一定数量点。 
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme(string[] points, DateTime st, DateTime et, int count)
        {
            ArrayList list = new ArrayList();
            Plink pk = new Plink();
            IList<Hashtable> listdata = new List<Hashtable>();

            Hashtable ht = new Hashtable();
            ArrayList ld = new ArrayList();
            ArrayList lt = new ArrayList();
            for (int i = 0; i < points.Length; i++)
            {
                ht = new Hashtable();
                lt = new ArrayList();
                ht.Add("name", points[i].Split('|')[1]);
                ht.Add("yAxis", i);
                DateTime _sTime = new DateTime(1970, 1, 1);
                DateTime dtt = st;
                Plink.OpenPi();
                while (dtt < et)
                {
                    ld = new ArrayList();
                    pk.GetHisValue(points[i].Split('|')[0], dtt.ToString("yyyy-MM-dd HH:mm:ss"), ref drv);
                    if (drv > 0)
                    {
                        drv = getDouble(drv, 3);
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

            return listdata;
        }
        //将一个事件对象转换为UTC格式的时间
        public static int DateTimeToUTC(DateTime DT)
        {
            long a = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            int rtnInt = 0;
            rtnInt = (int)((DT.Ticks - 8 * 3600 * 1e7 - a) / 1e7);
            return rtnInt;
        }
        /// <summary>
        /// 查询某段时间测点集合的历史值 -刘海杰
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme2(string[] points, DateTime st, DateTime et, int jiange)
        {
            ArrayList list = new ArrayList();
            Plink pk = new Plink();
            IList<Hashtable> listdata = new List<Hashtable>();

            Hashtable ht = new Hashtable();
            ArrayList ld = new ArrayList();
            ArrayList lt = new ArrayList();
            for (int i = 0; i < points.Length; i++)
            {
                ht = new Hashtable();
                ht.Add("name", points[i].Split('|')[1]);
                ht.Add("yAxis", i);
                DateTime _sTime = new DateTime(1970, 1, 1);
                // int seconds = Convert.ToInt32((et - st).TotalSeconds) / 600;
                DateTime dtt = st;
                Plink.OpenPi();
                while (dtt < et)
                {
                    ld = new ArrayList();
                    pk.GetHisValue(points[i].Split('|')[0], dtt.ToString("yyyy-MM-dd HH:mm:ss"), ref drv);
                    if (drv > 0)
                    {
                        drv = getDouble(drv, 3);
                    }
                    string timeStamp = DateTimeToUTC(dtt).ToString();
                    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    long lTime = long.Parse(timeStamp + "0000000");
                    TimeSpan toNow = new TimeSpan(lTime);
                    DateTime dtResult = dtStart.Add(toNow);
                    ld.Add(Convert.ToInt64((dtResult - _sTime).TotalMilliseconds.ToString()));
                    ld.Add(drv);
                    lt.Add(ld);
                    // TimeSpan toNow1 = new TimeSpan(seconds);
                    dtt = dtt.AddSeconds(jiange);
                }
                ht.Add("data", lt);
                listdata.Add(ht);
                //lt = new ArrayList();
                //ht = new Hashtable();
            }

            return listdata;
        }

        /// <summary>
        /// 获取测点历史值
        /// </summary>
        /// <param name="points"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double[] GetPointVal(string[] points, string time)
        {
            double[] val = new double[points.Length];
            double v = 0;
            Plink pk = new Plink();
            Plink.OpenPi();
            for (int i = 0; i < points.Length; i++)
            {
                pk.GetHisValue(points[i], time, ref v);

                v = getDouble(v, 2);
                val[i] = v;
            }
            //Plink.closePi();
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
            RTDBLink dll = new RTDBLink();
            dll.OpenPi();

            for (int i = 0; i < points.Length; i++)
            {
                v = dll.GetRealTimeValue(points[i]);
                v = getDouble(v, 2);
                val[i] = v;

            }
            //Plink.closePi();
            return val;
        }

        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="result">要转换的数值</param>
        /// <param name="num">保留位数</param>
        /// <returns></returns>
        public double getDouble(double result, int num)
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
    }
}
