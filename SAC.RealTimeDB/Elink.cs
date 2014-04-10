using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SAC.Helper;

namespace SAC.RealTimeDB
{
    public class Elink
    {
        int nRet;

        /// <summary>
        /// 获得实时值
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns>数值[double]</returns>
        public double GetRTValue(string pName)
        {
            double dValue = 0;
            if (pName != "")
            {
                nRet = eDnaApiNet.RealTime.LowSpeed.DNAGetRTValue(pName, ref dValue);
            }
            return dValue;
        }

        /// <summary>
        /// 获得实时值
        /// </summary>
        /// <param name="pName"></param>
        /// <returns>数值[string]</returns>
        public string GetStrRTValue(string pName)
        {
            double dValue = 0;
            if (pName != "")
            {
                nRet = eDnaApiNet.RealTime.LowSpeed.DNAGetRTValue(pName, ref dValue);
            }
            return dValue.ToString();
        }

        /// <summary>
        /// 获取历史值
        /// </summary>
        /// <param name="pName">点名</param>
        /// <param name="date">时间</param>
        /// <param name="ret">是否正常取数</param>
        /// <param name="val">返回值</param>
        public double GetHisValue(string pName, string date)
        {
            DateTime st = DateTime.Parse(date);
            DateTime et = DateTime.Parse(date);
            double val = 0;
            int ret = 0;
            uint key = 1;
            double value = -1;
            string status;

            DateTime utcTime;
            if (pName != "")
            {
                if (et < DateTime.Parse("2011-09-30"))
                { val = 0; }
                else
                {
                    //int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistSnap(pName, st, et, new TimeSpan(0, 0, 0, 1, 0), out key);
                    int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistRaw(pName, st, et, out key);
                    if (nRet == 0)
                    {
                        while (0 == eDnaApiNet.History.LowSpeed.Reading.DnaGetNextHist(key, ref value, out utcTime, out status))
                        {
                            //Console.WriteLine("time : " + utcTime + " value:" + value + " status :" + status);
                            val = value;
                        }
                    }
                    else
                    {
                        ret = nRet;
                    }
                }
            }
            else { val = 0; }
            return val;
        }

        /// <summary>
        /// 获取历史值
        /// </summary>
        /// <param name="pName">点名</param>
        /// <param name="date">时间</param>
        /// <param name="ret">是否正常取数</param>
        /// <param name="val">返回值</param>
        public void GetHisValue(string pName, string date, ref int ret, ref double val)
        {
            DateTime st = DateTime.Parse(date);
            DateTime et = DateTime.Parse(date);

            uint key = 1;
            double value = -1;
            string status;

            DateTime utcTime;
            if (pName != "")
            {
                if (et < DateTime.Parse("2011-09-30"))
                { val = 0; }
                else
                {
                    //int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistSnap(pName, st, et, new TimeSpan(0, 0, 0, 1, 0), out key);
                    int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistRaw(pName, st, et, out key);
                    if (nRet == 0)
                    {
                        while (0 == eDnaApiNet.History.LowSpeed.Reading.DnaGetNextHist(key, ref value, out utcTime, out status))
                        {
                            //Console.WriteLine("time : " + utcTime + " value:" + value + " status :" + status);
                            val = value;
                        }
                    }
                    else
                    {
                        ret = nRet;
                    }
                }
            }
            else { val = 0; }
        }

        /// <summary>
        /// 获取历史值(无时间限制,可取将来值)
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="date"></param>
        /// <param name="ret"></param>
        /// <param name="val"></param>
        public void GetHisValueByTime(string pName, string date, ref int ret, ref double val)
        {
            DateTime st = DateTime.Parse(date);
            DateTime et = DateTime.Parse(date);

            uint key = 1;
            double value = 0;
            string status;
            val = 0;

            DateTime utcTime;
            if (pName != "")
            {
                if (DateTime.Parse(date) > DateTime.Parse("2011-09-30"))
                {
                    //int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistRaw(pName, st, et, new TimeSpan(0, 0, 0, 1, 0), out key);
                    int nRet = eDnaApiNet.History.LowSpeed.Reading.DnaGetHistRaw(pName, st, et, out key);

                    if (nRet == 0)
                    {
                        eDnaApiNet.History.LowSpeed.Reading.DnaGetNextHist(key, ref value, out utcTime, out status);

                        val = value;

                    }
                    else
                    {
                        ret = nRet;
                    }
                }
                else { val = 0; }
            }
            else { val = 0; }

            //object value = null;

            //ELink.declare ee = new ELink.declare();

            //ee.GetHistRawValue(ref pName, ref date, ref value);

            //if (value == null || value.ToString() == "")
            //    val = 0;
            //else
            //    val = double.Parse(value.ToString());


        }

        /// <summary>
        /// 获取历史时间内间隔值
        /// </summary>
        /// <param name="pName">点名</param>
        /// <param name="bTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="ret">正常取数状态</param>
        /// <param name="val">值</param>
        public void GetHisDiffValue(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            double bVal = 0;
            double eVal = 0;
            if (pName != "")
            {
                if (DateTime.Parse(bTime) > DateTime.Parse("2011-09-30"))
                {
                    this.GetHisValue(pName, bTime, ref ret, ref bVal);
                    this.GetHisValue(pName, eTime, ref ret, ref eVal);

                    val = eVal - bVal;
                }
                else { val = 0; }
            }
            else { val = 0; }

        }

        /// <summary>
        /// 后去历史时间内间隔值
        /// </summary>
        /// <param name="pName">点名</param>
        /// <param name="bTime">开始时间</param>
        /// <param name="ret">正常取数状态</param>
        /// <param name="val">值</param>
        public void GetHisDiffValue(string pName, string bTime, ref int ret, ref double val)
        {
            if (pName != "")
            {
                double bVal = 0;
                double eVal = 0;

                this.GetHisValue(pName, bTime, ref ret, ref bVal);
                eVal = this.GetRTValue(pName);

                val = eVal - bVal;
            }
            else { val = 0; }
        }

        /// <summary>
        /// 后去历史时间内间隔值(无时间限制,可取将来值)
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="bTime"></param>
        /// <param name="ret"></param>
        /// <param name="val"></param>
        public void GetHisDiffValueByTime(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            if (pName != "")
            {
                double bVal = 0;
                double eVal = 0;

                if (DateTime.Parse(bTime) > DateTime.Parse("2011-09-30"))
                {
                    this.GetHisValueByTime(pName, bTime, ref ret, ref bVal);
                    this.GetHisValueByTime(pName, eTime, ref ret, ref eVal);
                }
                val = eVal - bVal;
            }
            else { val = 0; }
        }

        /// <summary>
        /// 写历史值
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="time"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int SetHisValue(ref string pName, ref string time, ref object val)
        {
            string hisName = IniHelper.ReadIniData("RTDB", "DBHisName", null); //"WHSIS.HISUNIV";

            ELink.declare ee = new ELink.declare();

            int i = ee.SetHistValue(ref hisName, ref pName, ref val, ref time); //ee.SetHistValue(ref hisName, ref pName, ref time, ref val);

            return i;
        }

        /// <summary>
        /// 写实时值
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int SetCurValue(ref string pName, ref object val)
        {
            ELink.declare ee = new ELink.declare();

            int i = ee.SetCurValue(ref pName, ref val);

            return i;
        }

        /// <summary>
        /// 打开点表选择器
        /// </summary>
        /// <returns></returns>
        public string GetPoint()
        {
            string point;
            int nRet = eDnaApiNet.Configuration.Point.DnaSelectPoint(out point);
            return point;
        }

        /// <summary>
        /// 将现在时刻转换为EDNA时间
        /// </summary>
        /// <returns></returns>
        public static int DateToUTC()
        {
            DateTime TimeFormat = DateTime.Parse("1970-1-1 08:00:00");
            DateTime time = DateTime.Now;
            int lBeg = (int)time.Subtract(TimeFormat).TotalSeconds;
            return lBeg;
        }

        /// <summary>
        /// 根据测点和时间求平均值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="type">1,2,3</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string RetPointValueAvg(string tag, int type, string date)
        {
            int ret = -1;
            double value = 0;
            string mon = "";
            string res = "";

            ArrayList list = new ArrayList();

            if (type == 1)
            {
                list.Add(date + " 00:59:59");
                list.Add(date + " 01:59:59");
                list.Add(date + " 02:59:59");
                list.Add(date + " 03:59:59");
                list.Add(date + " 04:59:59");
                list.Add(date + " 05:59:59");
                list.Add(date + " 06:59:59");
                list.Add(date + " 07:59:59");
            }
            else if (type == 2)
            {
                list.Add(date + " 08:59:59");
                list.Add(date + " 09:59:59");
                list.Add(date + " 10:59:59");
                list.Add(date + " 11:59:59");
                list.Add(date + " 12:59:59");
                list.Add(date + " 13:59:59");
                list.Add(date + " 14:59:59");
                list.Add(date + " 15:59:59");
            }
            else if (type == 3)
            {
                list.Add(date + " 16:59:59");
                list.Add(date + " 17:59:59");
                list.Add(date + " 18:59:59");
                list.Add(date + " 19:59:59");
                list.Add(date + " 20:59:59");
                list.Add(date + " 21:59:59");
                list.Add(date + " 22:59:59");
                list.Add(date + " 23:59:59");
            }
            else
            { }

            if (list.Count > 0)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    GetHisValue(tag, list[i].ToString(), ref ret, ref value);

                    mon += "(" + value + ")+";
                }

                mon = "(" + mon.TrimEnd('+') + ")/" + list.Count.ToString();

                res = StrHelper.Cale(mon);
            }
            else { res = "0"; }

            return res;
        }

        /// <summary>
        /// 计算差值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string RetPointDiffValue(string tag, string bt, string et)
        {
            int ret = -1;
            double value = 0;

            if (tag != "")
            {
                if (Convert.ToDateTime(et) > DateTime.Now)
                    value = 0;
                else
                    GetHisDiffValue(tag, bt, et, ref ret, ref value);
            }

            return value.ToString();
        }
    }
}
