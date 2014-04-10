/*----------------------------------------------------------------
// Copyright (C) 2014 南京华盾电力信息安全测评有限公司
// 版权所有。 
//
// 文件名：Role

// 文件功能描述：角色业务模块

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
    class Role
    {
        DBLink dl = new DBLink();
        private string RoleId = "";
        private string RoleName = "";
        private string strSql = "";
        public int Flag = 0;
        DataTable dt = null;
        string errMsg = "";

        public Role(string Key)
        {
            RoleId = Key;
        }

        public string T_RoleName
        {
            get
            {
                strSql = "select * from T_SYS_GROUP where T_GRPID='" + RoleId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_USERNAME"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                RoleName = value;
                strSql = "select * from T_SYS_GROUP where T_GRPID='" + RoleId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此RoleId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_GROUP set T_GRPDESC='" + RoleName + "' where T_GRPID='" + RoleId + "'";
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
        /// 根据用户所属的角色ID和每页显示多少条数据返回用户信息
        /// 创建：李东峰 日期：2014-03-10
        /// </summary>
        /// <param name="_grpId">用户所属的角色ID</param>
        /// <param name="sCount">起始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns>返回查询结果集</returns>
        public DataTable GetUserByRoleId(int sCount, int eCount)
        {
            int t1 = eCount - sCount + 1;
            int t2 = sCount - 1;
            string rlDBType = dl.init();
            string sqlSQL = "select top " + t1 + " * from (select ID_KEY,T_USERID,T_USERNAME from(select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID)as a where a.T_GRPID='" + RoleId + "')as b where (b.ID_KEY not in ( select top " + t2 + " ID_KEY from(select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID)as a where a.T_GRPID='" + RoleId + "'))";
            string sqlDB2 = "select * from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME,rownumber() over(order by ID_KEY asc ) as rowid from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_GRPID='" + RoleId + "') as b where b.rowid between " + sCount + " and " + eCount + "";
            string sqlORC = "select * from(select ID_KEY,T_USERID,T_USERNAME,ROWNUM rn from(select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where T_GRPID='" + RoleId + "' and ROWNUM <= " + eCount + ")WHERE rn >= " + sCount + "";

            string errMsg;
            DataTable _dt = null;
            if (rlDBType == "SQL")
            {
                try
                {
                    _dt = dl.RunDataTable(sqlSQL, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "DB2")
            {
                try
                {
                    _dt = dl.RunDataTable(sqlDB2, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "ORACLE")
            {
                try
                {
                    _dt = dl.RunDataTable(sqlORC, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return _dt;
        }

        /// <summary>
        /// 根据角色ID返回所有用户信息的条数
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <returns>返回该角色下用户的数量</returns>
        public int GetUserCountByRole()
        {
            string rlDBType = dl.init();
            string sql = "select COUNT(*) from(select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID)as a where a.T_GRPID='" + RoleId + "'";
            string sql1 = "select count(*) from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_GRPID='" + RoleId + "')as b";
            string sql2 = "select count(*) from ( select ID_KEY,T_USERID,T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where T_GRPID='" + RoleId + "')";
            string errMsg;
            int count = 0;
            if (rlDBType == "SQL")
            {
                try
                {
                    count = dl.RunRowCount(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "DB2")
            {
                try
                {
                    count = dl.RunRowCount(sql1, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "ORACLE")
            {
                try
                {
                    DataTable dt = dl.RunDataTable(sql, out errMsg);
                    count = int.Parse(dt.Rows[0]["COUNT(*)"].ToString());
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return count;
        }

        /// <summary>
        /// 添加新角色信息
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <param name="rName">角色名称</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddRole(string rName)
        {
            errMsg = "";
            bool _flag = false;
            string sql1 = "select * from T_SYS_GROUP where T_GRPID='" + RoleId + "'";
            DataTable dt = null;
            dt = dl.RunDataTable(sql1, out errMsg);
            if (dt == null || dt.Rows.Count == 0)
            {
                string sql2 = "insert into T_SYS_GROUP (T_GRPID,T_GRPDESC) values ('" + RoleId + "','" + rName + "')";
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
        /// 删除角色
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteRole()
        {
            errMsg = "";
            bool _flag = true;

            string sql = "delete from T_SYS_GROUP where T_GRPID='" + RoleId + "'";
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
        /// 返回所有角色信息的datatable
        /// 创建：李东峰 日期：2014-03-10
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public DataTable GetAllRole()
        {
            DataTable _dt = null;
            string _sql = "select * from T_SYS_GROUP order by ID_KEY asc";
            _dt = dl.RunDataTable(_sql, out errMsg);
            return _dt;
        }

        /// <summary>
        /// 根据每页显示多少条数据返回角色信息
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <param name="sCount">起始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns>返回查询结果集</returns>
        public DataTable GetRoleByStartToEnd(int sCount, int eCount)
        {
            DataTable _dt = null;
            string[] cName = new string[] { "ID_KEY", "T_GRPID", "T_GRPDESC" };
            _dt = dl.GetS2Enotes("T_SYS_GROUP", cName, "ID_KEY", sCount, eCount);
            return _dt;
        }

        /// <summary>
        /// 返回所有角色的数量
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <returns>所有角色的数量</returns>
        public int GetRoleCount()
        {
            return dl.GetCount("T_SYS_GROUP");
        }

        /// <summary>
        /// 根据角色ID判断该角色下是否有人员
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <returns>返回是否有人员</returns>
        public bool JudgeMemberByRoleId()
        {
            bool _result = false;
            string sql = "select count(*) from T_SYS_MEMBERGRP where T_GRPID='" + RoleId + "'";
            int count = dl.RunRowCount(sql, out errMsg);
            if (count > 0)
                _result = true;
            else
                _result = false;
            return _result;
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
