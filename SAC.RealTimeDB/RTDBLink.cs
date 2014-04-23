﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;

namespace SAC.RealTimeDB
{
    public class RTDBLink
    {
        /// <summary>
        /// 数据库操作类，外部调用数据库操作方法时直接调用在此类中定义的方法
        /// </summary>
        string rlDBType = "";
        RTDBHelper rtdbLink;
        public RTDBLink()
        {
            string DBtype = this.init();
            if (DBtype == "PI")
            {
                rtdbLink = new Pi();
            }
            else if (DBtype == "EDNA")
            {
                rtdbLink = new eDna();
            }
            else if (DBtype == "SmartReal")
            {
                rtdbLink = new SamrtReal();
            }
        }
        /// <summary>
        /// 打开pi数据库连接
        /// </summary>
        public int OpenSR(ref int handle)
        {
            return rtdbLink.OpenSR(ref handle);
        }
        /// <summary>
        /// 关闭pi数据库连接
        /// </summary>
        public object ClosePi(int handle)
        {
            return rtdbLink.closeSR(handle);
        }
        /// <summary>
        /// 获取实时值
        /// </summary>
        public double GetRealTimeValue(string tagName, int handle)
        {
            return rtdbLink.GetRealTimeValue(tagName, handle);
        }
        /// <summary>
        /// 获取实时值数组
        /// </summary>
        public double[] GetRealTimeValues(int count, string[] tagName, int handle)
        {
            return rtdbLink.GetRealTimeValues(count, tagName, handle);
        }
        /// <summary>
        /// 获取历史值
        /// </summary>
        public double GetHisValue(string tagName, string hisTime, int handle)
        {
            return rtdbLink.GetHisValue(tagName, hisTime, handle);
        }
        /// <summary>
        /// 写实时值
        /// </summary>
        public int SetRealTimeValue(ref string tagName, ref object val, int handle)
        {
            return rtdbLink.SetRealTimeValue(ref tagName, ref val, handle);
        }
        /// <summary>
        /// 写历史值
        /// </summary>
        public int SetHisValue(ref string tagName, ref string hisTime, ref object val, int handle)
        {
            return rtdbLink.SetHisValue(ref tagName, ref hisTime, ref val, handle);
        }

        public void GetHisValue(string pName, string time, ref double val)
        {
            rtdbLink.GetHisValue(pName, time, ref val);
        }
        public void GetHisValue(string pName, string date, ref int ret, ref double val)
        {
            rtdbLink.GetHisValue(pName, date, ref ret, ref val);
        }
        public void GetHisValueByTime(string pName, string date, ref int ret, ref double val)
        {
            rtdbLink.GetHisValueByTime(pName, date, ref ret, ref val);
        }
        public void GetHisDiffValue(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            rtdbLink.GetHisDiffValue(pName, bTime, eTime, ref ret, ref val);
        }
        public void GetHisDiffValue(string pName, string bTime, ref int ret, ref double val)
        {
            rtdbLink.GetHisDiffValue(pName, bTime, ref ret, ref val);
        }
        public void GetHisDiffValueByTime(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            rtdbLink.GetHisDiffValueByTime(pName, bTime, eTime, ref ret, ref val);
        }
        public string GetPoint()
        {
            return rtdbLink.GetPoint();
        }
        public int DateToUTC()
        {
            return rtdbLink.DateToUTC();
        }
        public string RetPointValueAvg(string tag, int type, string date)
        {
            return rtdbLink.RetPointValueAvg(tag, type, date);
        }
        public string RetPointDiffValue(string tag, string bt, string et)
        {
            return rtdbLink.RetPointDiffValue(tag, bt, et);
        }

        public string init()
        {
            rlDBType = IniHelper.ReadIniData("RTDB", "DBType", null);
            return rlDBType;
        }
    }
}
