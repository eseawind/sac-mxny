/*----------------------------------------------------------------
// Copyright (C) 2014 南京华盾电力信息安全测评有限公司
// 版权所有。 
//
// 文件名：Class

// 文件功能描述：班值业务模块

// 创建标识：李东峰20140312
 * 
// 修改标识：

// 修改描述：

//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAC.DBOperations;
using SAC.Helper;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace Module
{
    class Shift
    {
        DBLink dl = new DBLink();
        private string ShfitId = "";
        private string ShfitName = "";
        private string StartTime = "";
        private string EndTime = "";
        private string strSql = "";
        public int Flag = 0;
        DataTable dt = null;
        string errMsg = "";

        public Shift(string Key)
        {
            ShfitId = Key;
        }

        public string T_ShiftName
        {
            get
            {
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_SHIFTNAME"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                ShfitName = value;
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此RoleId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_SHIFTINFO set T_SHIFTNAME='" + ShfitName + "' where T_SHIFTID='" + ShfitId + "'";
                    dl.RunNonQuery(strSql, out errMsg);
                    if (errMsg == "")
                    {
                        Flag = 1;
                    }
                    else
                    {
                        Flag = 0;
                    }
                }
            }
        }

        public string T_StratTime
        {
            get
            {
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    StartTime = dt.Rows[0]["D_STARTTIME"].ToString().Substring(9);
                    return StartTime;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                StartTime = value;
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此RoleId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_SHIFTINFO set D_STARTTIME='2014-01-01 " + StartTime + "' where T_SHIFTID='" + ShfitId + "'";
                    dl.RunNonQuery(strSql, out errMsg);
                    if (errMsg == "")
                    {
                        Flag = 1;
                    }
                    else
                    {
                        Flag = 0;
                    }
                }
            }
        }

        public string T_EndTime
        {
            get
            {
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    EndTime = dt.Rows[0]["D_ENDTIME"].ToString().Substring(9);
                    return EndTime;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                EndTime = value;
                strSql = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此RoleId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_SHIFTINFO set D_ENDTIME='2014-01-01 " + EndTime + "' where T_SHIFTID='" + ShfitId + "'";
                    dl.RunNonQuery(strSql, out errMsg);
                    if (errMsg == "")
                    {
                        Flag = 1;
                    }
                    else
                    {
                        Flag = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 添加新班次信息
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <param name="_shiftName">班次名称</param>
        /// <param name="_startTime">班次名称</param>
        /// <param name="_endTime">班次名称</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddShift(string _shiftName, string _startTime, string _endTime)
        {
            errMsg = "";
            bool _flag = false;
            string sql1 = "select * from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
            DataTable dt = null;
            dt = dl.RunDataTable(sql1, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
            {
                string sql2 = "insert into T_SYS_SHIFTINFO (T_SHIFTID,T_SHIFTNAME,D_STRATTIME,D_ENDTIME) values ('" + ShfitId + "','" + _shiftName + "','2014-01-01 " + _startTime + "','2014-01-01 " + _endTime + "')";
                dl.RunNonQuery(sql2, out errMsg);
                if (errMsg == "")
                {
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            else
            {
                _flag = false;
            }
            return _flag;
        }

        /// <summary>
        /// 删除班组
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteShift()
        {
            errMsg = "";
            bool _flag = true;

            string sql = "delete from T_SYS_SHIFTINFO where T_SHIFTID='" + ShfitId + "'";
            dl.RunNonQuery(sql, out errMsg);

            if (errMsg == "")
            {
                _flag = true;
            }
            else
            {
                _flag = false;
            }
            return _flag;
        }

        /// <summary>
        /// 根据起始条数和结束条数显示班次信息
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <param name="sCount">起始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns>返回查询结果集</returns>
        public DataTable GetShiftByStartToEnd(int sCount, int eCount)
        {
            string[] cName = new string[] { "ID_KEY", "T_SHIFTNAME", "T_SHIFTID", "D_STARTTIME", "D_ENDTIME" };
            DataTable _dt = null;
            try
            {
                _dt = dl.GetS2Enotes("T_SYS_SHIFTINFO", cName, "ID_KEY", sCount, eCount);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return _dt;
        }

        /// <summary>
        /// 返回所有班次的数量
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <returns>所有班次的数量</returns>
        public int GetShiftCount()
        {
            return dl.GetCount("T_SYS_SHIFTINFO");
        }

        /// <summary>
        /// 返回所有班次信息的datatable
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public DataTable GetAllShift()
        {
            DataTable _dt = null;
            string _sql = "select * from T_SYS_SHIFTINFO order by ID_KEY asc";
            _dt = dl.RunDataTable(_sql, out errMsg);
            return _dt;
        }

        public int flag
        {
            get
            {
                return Flag;
            }
            set
            {
                Flag = value;
            }
        }
    }
}
