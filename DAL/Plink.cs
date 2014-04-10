using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;

namespace DAL
{
    public class Plink
    {
        /// <summary>
        /// 打开PI连接
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public static int OpenPi()
        {
            PiLink.declare pk = new PiLink.declare();

            int i = 200;

            string serverName = IniHelper.ReadIniData("RTDB", "DBIP", null);// System.Configuration.ConfigurationSettings.AppSettings["piserver"].ToString();
            string userName = IniHelper.ReadIniData("RTDB", "DBUser", null);// System.Configuration.ConfigurationSettings.AppSettings["piuser"].ToString();
            string userPwd = IniHelper.ReadIniData("RTDB", "DBPwd", null); //System.Configuration.ConfigurationSettings.AppSettings["pipwd"].ToString();

            i = pk.ConnectToServer(ref serverName, ref userName, ref userPwd);

            return i;
        }

        /// <summary>
        /// 释放Pi数据库的连接
        /// </summary>
        /// <returns></returns>
        public static object closePi()
        {
            PiLink.declare pk = new PiLink.declare();
            return pk.DisConnect();
        }

        /// <summary>
        /// 返回实时测点值
        /// </summary>
        /// <param name="tagName">测点名</param>
        /// <returns>测点值</returns>
        static public double returnValueByTagName(string tagName)
        {
            PiLink.declare pk = new PiLink.declare();

            object drval = null;
            object bval = null;
            object bsize = null;
            object istat = null;
            object flags = null;
            object time = null;

            int j = pk.WebGetCurValue(ref tagName, ref drval, ref bval, ref bsize, ref istat, ref flags, ref time);

            if (j == 0)
            { return Convert.ToDouble(drval.ToString()); }
            else
            { return Convert.ToDouble(drval.ToString()); }
        }

        /// <summary>
        /// 返回历史值
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        static public double returnValueByTagName(string tagName, string time)
        {
            PiLink.declare pk = new PiLink.declare();

            object drval = null;
            object bval = null;
            object bsize = null;
            object istat = null;
            object flags = null;

            int j = pk.GetHistValue(ref tagName, ref  drval, ref bval, ref bsize, ref istat, ref flags, ref time);

            if (j == 0)
            { return Convert.ToDouble(drval.ToString()); }
            else
            { return Convert.ToDouble(drval.ToString()); }
        }

        /// <summary>
        /// 取历史值
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="time"></param>
        /// <param name="val"></param>
        public void GetHisValue(string pName, string time, ref double val)
        {
            PiLink.declare pk = new PiLink.declare();

            object drval = null;
            object bval = null;
            object bsize = null;
            object istat = null;
            object flags = null;

            int j = pk.GetHistValue(ref pName, ref  drval, ref bval, ref bsize, ref istat, ref flags, ref time);

            if (j == 0)
                val = double.Parse(drval.ToString());
            else
                val = 0;
        }

        public int SetHisValue(string pName, string time, ref object val)
        {
            PiLink.declare pk = new PiLink.declare();

            object drval = null;
            object bval = null;
            object bsize = null;
            object istat = null;
            object flags = null;

            int j = pk.SetHistValue(ref pName, ref val, ref time);

            return j;
        }

        public int SetCurValue(string pName, ref object val)
        {
            int j;

            PiLink.declare pk = new PiLink.declare();

            j = pk.SetCurValue(ref pName, ref val);

            return j;
        }
    }
}
