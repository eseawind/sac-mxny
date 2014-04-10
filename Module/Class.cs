/*----------------------------------------------------------------
// Copyright (C) 2014 南京华盾电力信息安全测评有限公司
// 版权所有。 
//
// 文件名：Class

// 文件功能描述：班组业务模块

// 创建标识：李东峰20140311
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
    class Class
    {
        DBLink dl = new DBLink();
        private string ClassId = "";
        private string ClassName = "";
        private string strSql = "";
        public int Flag = 0;
        DataTable dt = null;
        string errMsg = "";

        public Class(string Key)
        {
            ClassId = Key;
        }

        public string T_ClassName
        {
            get
            {
                strSql = "select * from T_SYS_CLASSINFO where T_CLASSID='" + ClassId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_CLASSNAME"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                ClassName = value;
                strSql = "select * from T_SYS_CLASSINFO where T_CLASSID='" + ClassId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此RoleId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_CLASSINFO set T_CLASSNAME='" + ClassName + "' where T_CLASSID='" + ClassId + "'";
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

        /// <summary>
        /// 添加新班组信息
        /// 创建：李东峰 日期：2014-03-12
        /// </summary>
        /// <param name="rName">班组名称</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddClass(string _ClassName)
        {
            errMsg = "";
            bool _flag = false;
            string sql1 = "select * from T_SYS_CLASSINFO where T_CLASSID='" + ClassId + "'";
            DataTable dt = null;
            dt = dl.RunDataTable(sql1, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
            {
                string sql2 = "insert into T_SYS_CLASSINFO (T_CLASSID,T_CLASSNAME) values ('" + ClassId + "','" + _ClassName + "')";
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
        /// 创建：李东峰 日期：2014-03-12
        /// </summary>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteClass()
        {
            errMsg = "";
            bool _flag = true;

            string sql = "delete from T_SYS_CLASSINFO where T_CLASSID='" + ClassId + "'";
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
        /// 根据起始条数和结束条数显示班组信息
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <param name="sCount">起始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns>返回查询结果集</returns>
        public DataTable GetClassByStartToEnd(int sCount, int eCount)
        {
            string[] cName = new string[] { "ID_KEY", "T_CLASSID", "T_CLASSNAME" };

            DataTable _dt = null;
            try
            {
                _dt = dl.GetS2Enotes("T_SYS_CLASSINFO", cName, "ID_KEY", sCount, eCount);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return _dt;
        }

        // <summary>
        /// 返回所有班组的数量
        /// 创建：李东峰 日期：2014-03-13
        /// </summary>
        /// <returns>所有班组的数量</returns>
        public int GetClassCount()
        {
            return dl.GetCount("T_SYS_CLASSINFO");
        }

        /// <summary>
        /// 返回所有班组信息
        /// 创建：李东峰 日期：2014-03-12
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public DataTable GetAllClass()
        {
            DataTable _dt = null;
            string _sql = "select * from T_SYS_CLASSINFO order by ID_KEY asc";
            _dt = dl.RunDataTable(_sql, out errMsg);
            return _dt;
        }
    }
}
