using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.RealTimeDB;

namespace SAC.RealTimeDB
{
    public class RTDBHelper
    {
        /// <summary>
        /// 实时数据库操作父类，在此类定义数据库操作要用到的方法
        /// </summary>
        public virtual int OpenPi()
        {
            return 0;
        }
        public virtual object closePi()
        {
            object obj = null;
            return obj;
        }
        public virtual double GetRealTimeValue(string tagName)//返回实时值
        {
            return 0;
        }
        public virtual double GetHisValue(string tagName, string hisTime)//返回历史值
        {
            return 0;
        }
        public virtual int SetRealTimeValue(ref string tagName, ref object val)//写实时值
        {
            return 0;
        }
        public virtual int SetHisValue(ref string tagName, ref string hisTime, ref object val)//写历史值
        {
            return 0;
        }
        public virtual void GetHisValue(string pName, string time, ref double val) { }
        public virtual void GetHisValue(string pName, string date, ref int ret, ref double val) { }
        public virtual void GetHisValueByTime(string pName, string date, ref int ret, ref double val) { }
        public virtual void GetHisDiffValue(string pName, string bTime, string eTime, ref int ret, ref double val) { }
        public virtual void GetHisDiffValue(string pName, string bTime, ref int ret, ref double val) { }
        public virtual void GetHisDiffValueByTime(string pName, string bTime, string eTime, ref int ret, ref double val) { }
        public virtual string GetPoint()
        {
            return "";
        }
        public virtual int DateToUTC()
        {
            return 0;
        }
        public virtual string RetPointValueAvg(string tag, int type, string date)
        {
            return "";
        }
        public virtual string RetPointDiffValue(string tag, string bt, string et)
        {
            return "";
        }
    }

    public class Pi : RTDBHelper
    {
        SAC.RealTimeDB.Plink pl = new Plink();
        public override int OpenPi()
        {
            return SAC.RealTimeDB.Plink.OpenPi();
        }
        public override object closePi()
        {
            return SAC.RealTimeDB.Plink.closePi();
        }
        public override double GetRealTimeValue(string tagName)
        {
            return SAC.RealTimeDB.Plink.returnValueByTagName(tagName);
        }
        public override double GetHisValue(string tagName, string hisTime)
        {
            return SAC.RealTimeDB.Plink.returnValueByTagName(tagName, hisTime);
        }
        public override int SetRealTimeValue(ref string tagName, ref object val)
        {
            return pl.SetCurValue(ref tagName, ref val);
        }
        public override int SetHisValue(ref string tagName, ref string hisTime, ref object val)
        {
            return pl.SetHisValue(ref tagName, ref hisTime, ref val);
        }
        public override void GetHisValue(string pName, string time, ref double val)
        {
            pl.GetHisValue(pName, time, ref val);
        }
    }
    public class eDna : RTDBHelper
    {
        SAC.RealTimeDB.Elink el = new Elink();

        public override int OpenPi()
        {
            return 0;
        }
        public override object closePi()
        {
            object obj =new object();
            return obj;
        }

        public override double GetRealTimeValue(string tagName)
        {
            return el.GetRTValue(tagName);
        }
        public override double GetHisValue(string tagName, string hisTime)
        {
            return el.GetHisValue(tagName, hisTime);
        }
        public override int SetRealTimeValue(ref string tagName, ref object val)
        {
            return el.SetCurValue(ref tagName, ref val);
        }
        public override int SetHisValue(ref string tagName, ref string hisTime, ref object val)
        {
            return el.SetHisValue(ref tagName, ref hisTime, ref val);
        }
        public override void GetHisValue(string pName, string date, ref int ret, ref double val)
        {
            el.GetHisValue(pName, date, ref ret, ref val);
        }
        public override void GetHisValueByTime(string pName, string date, ref int ret, ref double val)
        {
            el.GetHisValueByTime(pName, date, ref ret, ref val);
        }
        public override void GetHisDiffValue(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            el.GetHisDiffValue(pName, bTime, eTime, ref ret, ref val);
        }
        public override void GetHisDiffValue(string pName, string bTime, ref int ret, ref double val)
        {
            el.GetHisDiffValue(pName, bTime, ref ret, ref val);
        }
        public override void GetHisDiffValueByTime(string pName, string bTime, string eTime, ref int ret, ref double val)
        {
            el.GetHisDiffValueByTime(pName, bTime, eTime, ref ret, ref val);
        }
        public override string GetPoint()
        {
            return el.GetPoint();
        }
        public override int DateToUTC()
        {
            return SAC.RealTimeDB.Elink.DateToUTC();
        }
        public override string RetPointValueAvg(string tag, int type, string date)
        {
            return el.RetPointValueAvg(tag, type, date);
        }
        public override string RetPointDiffValue(string tag, string bt, string et)
        {
            return el.RetPointDiffValue(tag, bt, et);
        }
    }
}
