/*----------------------------------------------------------------
// Copyright (C) 2014 南京华盾电力信息安全测评有限公司
// 版权所有。 
//
// 文件名：Member

// 文件功能描述：人员业务模块

// 创建标识：李东峰20140306
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
    public class Member
    {
        DBLink dl = new DBLink();
        #region 全局变量
        private string UserId = "";
        private string UserName = "";
        private string PassWord = "";
        private string strSql = "";
        private string ClassId = "";
        private string PosId = "";
        DataTable dt = null;
        string errMsg = "";
        public int Flag=0;
        IList<Hashtable> listMembers = null;
        Hashtable htb = new Hashtable();
        byte[] AttachMent = null;
        #endregion
        public Member(string Key) 
        {
            UserId = Key;
        }
        public string T_UserName
        {
            get
            {
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
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
                UserName = value;
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此UserId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_MEMBERINFO set T_USERNAME='" + UserName + "' where T_USERID='" + UserId + "'";
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

        public string T_PassWord
        {
            get
            {
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_PASSWD"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                PassWord = value;
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此UserId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_MEMBERINFO set T_PASSWD='" + PassWord + "' where T_USERID='" + UserId + "'";
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

        public byte[] B_AttachMent
        {
            get
            {
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                if (dt != null & dt.Rows.Count != 0)
                {
                    dt = dl.RunDataTable(strSql, out errMsg);
                    listMembers = DataTableToList(dt);
                    htb = listMembers[0];
                    AttachMent = (byte[])(htb["B_ATTACHMENT"]);
                    return AttachMent;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Flag = 0;
                AttachMent = value;
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此UserId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    dl.RetBoolUpFile("T_SYS_MEMBERINFO", "T_USERID", UserId, "B_ATTACHMENT", AttachMent,out errMsg);
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

        public string T_ClassId
        {
            get
            {
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_CLASSID"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                ClassId = value;
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此UserId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_MEMBERINFO set T_CLASSID='" + ClassId + "' where T_USERID='" + UserId + "'";
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

        public string T_PosId
        {
            get
            {
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt != null & dt.Rows.Count != 0)
                {
                    return dt.Rows[0]["T_POSID"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                Flag = 0;
                PosId = value;
                strSql = "select * from T_SYS_MEMBERINFO where T_USERID='" + UserId + "'";
                dt = dl.RunDataTable(strSql, out errMsg);
                if (dt == null || dt.Rows.Count == 0)//不存在此UserId，返回错误信息
                {
                    Flag = 0;
                }
                else
                {
                    strSql = "update T_SYS_MEMBERINFO set T_POSID='" + PosId + "' where T_USERID='" + UserId + "'";
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
        /// 添加人员
        /// 创建：李东峰 日期：2014-03-10
        /// </summary>
        /// <param name="_userName">用户真实姓名</param>
        /// <param name="_passWord">用户密码</param>
        /// <param name="_attachMent">用户照片</param>
        /// <param name="_classId">用户班组</param>
        /// <param name="_posId">用户岗位</param>
        /// <param name="_posId">用户所属角色ID</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddMember(string _userName, string _passWord, byte[] _attachMent, string _classId, string _posId, string _grpId)
        {
            string rlDBType = dl.init();
            string _errMsg;
            string sql1 = "";
            string sql2 = "";
            bool result = false;
            if (rlDBType == "SQL")
            {
                if (_attachMent != null && _attachMent.Length > 0)
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,B_ATTACHMENT,T_CLASSID,T_POSID) values(@T_USERID,@T_USERNAME,@T_PASSWD,@B_ATTACHMENT,@T_CLASSID,@T_POSID);";
                }
                else
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,T_CLASSID,T_POSID) values(@T_USERID,@T_USERNAME,@T_PASSWD,@T_CLASSID,@T_POSID);";
                }
                sql2 = "insert into T_SYS_MEMBERGRP(T_USERID,T_GRPID) values('" + UserId + "','" + _grpId + "')";
                try
                {
                    SqlConnection sqlconn = SAC.DBOperations.DBsql.GetConnection();
                    SqlCommand sqlcmd = new SqlCommand(sql1, sqlconn);
                    if (_attachMent != null && _attachMent.Length > 0)
                    {
                        sqlcmd.Parameters.Add("@T_USERID", UserId);
                        sqlcmd.Parameters.Add("@T_USERNAME", _userName);
                        sqlcmd.Parameters.Add("@T_PASSWD", _passWord);
                        sqlcmd.Parameters.Add("@B_ATTACHMENT", _attachMent);
                        sqlcmd.Parameters.Add("@T_PASSWD", _classId);
                        sqlcmd.Parameters.Add("@T_POSID", _posId);
                    }
                    else
                    {
                        sqlcmd.Parameters.Add("@T_USERID", UserId);
                        sqlcmd.Parameters.Add("@T_USERNAME", _userName);
                        sqlcmd.Parameters.Add("@T_PASSWD", _passWord);
                        sqlcmd.Parameters.Add("@T_PASSWD", _classId);
                        sqlcmd.Parameters.Add("@T_POSID", _posId);
                    }
                    if (sqlcmd.ExecuteNonQuery() > 0)
                        result = true;
                    sqlconn.Close();
                    dl.RunNonQuery(sql2, out _errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                    result = false;
                }
            }
            else if (rlDBType == "DB2")
            {
                if (_attachMent != null && _attachMent.Length > 0)
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,B_ATTACHMENT,T_CLASSIS,T_POSID) values(?,?,?,?,?,?);";
                }
                else
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,T_CLASSIS,T_POSID) values(?,?,?,?,?);";
                }
                sql2 = "insert into T_SYS_MEMBERGRP(T_USERID,T_GRPID) values('" + UserId + "','" + _grpId + "')";
                try
                {
                    OleDbConnection con = new OleDbConnection(SAC.DBOperations.DBdb2.SetConString());
                    con.Open();
                    OleDbCommand oledbcom = new OleDbCommand(sql1, con);
                    if (_attachMent != null && _attachMent.Length > 0)
                    {
                        oledbcom.Parameters.Add("?", UserId);
                        oledbcom.Parameters.Add("?", _userName);
                        oledbcom.Parameters.Add("?", _passWord);
                        oledbcom.Parameters.Add("?", _attachMent);
                        oledbcom.Parameters.Add("?", _classId);
                        oledbcom.Parameters.Add("?", _posId);
                    }
                    else
                    {
                        oledbcom.Parameters.Add("?", UserId);
                        oledbcom.Parameters.Add("?", _userName);
                        oledbcom.Parameters.Add("?", _passWord);
                        oledbcom.Parameters.Add("?", _classId);
                        oledbcom.Parameters.Add("?", _posId);
                    }
                    if (oledbcom.ExecuteNonQuery() > 0)
                        result = true;
                    con.Close();
                    dl.RunNonQuery(sql2, out _errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                    result = false;
                }
            }
            else if (rlDBType == "ORACLE")
            {
                if (_attachMent != null && _attachMent.Length > 0)
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,B_ATTACHMENT,T_CLASSIS,T_POSID) values(:blobtodb,:blobtodb,:blobtodb,:blobtodb,:blobtodb,:blobtodb);";
                }
                else
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,T_CLASSIS,T_POSID) values(:blobtodb,:blobtodb,:blobtodb,:blobtodb,:blobtodb);";
                }
                sql2 = "insert into T_SYS_MEMBERGRP(T_USERID,T_GRPID) values('" + UserId + "','" + _grpId + "')";
                try
                {
                    OracleConnection con = new OracleConnection(SAC.DBOperations.OracleHelper.retStr());
                    con.Open();
                    OracleCommand oledbcom = new OracleCommand(sql1, con);
                    if (_attachMent != null && _attachMent.Length > 0)
                    {
                        oledbcom.Parameters.Add("blobtodb", UserId);
                        oledbcom.Parameters.Add("blobtodb", _userName);
                        oledbcom.Parameters.Add("blobtodb", _passWord);
                        oledbcom.Parameters.Add("blobtodb", _attachMent);
                        oledbcom.Parameters.Add("blobtodb", _classId);
                        oledbcom.Parameters.Add("blobtodb", _posId);
                    }
                    else
                    {
                        oledbcom.Parameters.Add("blobtodb", UserId);
                        oledbcom.Parameters.Add("blobtodb", _userName);
                        oledbcom.Parameters.Add("blobtodb", _passWord);
                        oledbcom.Parameters.Add("blobtodb", _classId);
                        oledbcom.Parameters.Add("blobtodb", _posId);
                    }
                    if (oledbcom.ExecuteNonQuery() > 0)
                        result = true;
                    con.Close();
                    dl.RunNonQuery(sql2, out _errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除人员
        /// 创建：李东峰 日期：2014-03-10
        /// </summary>
        /// <returns>返回是否删除成功</returns>
        public bool RemoveMember()
        {
            string _errMsg;
            bool result = false;
            string sql = "delete from T_SYS_MEMBERINFO where T_USERID in (" + UserId + ");delete from T_SYS_MEMBERGRP where T_USERID in (" + UserId + ");";
            try
            {
                result = dl.RunNonQuery(sql, out _errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 返回所有人员信息
        /// 创建：李东峰 日期：2014-03-11
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public DataTable GetAllMembers()
        {
            DataTable _dt = null;
            string sql = "select T_USERID,T_USERNAME from T_SYS_MEMBERINFO order by ID_KEY asc";
            try
            {
                _dt = dl.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {

            }

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

        #region 从DataTable转化为List 胡进财 2013/02/27
        /// <summary>
        /// 从DataTable转化为List
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <returns>List集合</returns>
        public IList<Hashtable> DataTableToList(DataTable _dt)
        {
            IList<Hashtable> list = null;
            if (_dt != null && _dt.Rows.Count > 0)
            {
                list = new List<Hashtable>();
                Hashtable ht = null;
                foreach (DataRow row in _dt.Rows)
                {
                    ht = new Hashtable();
                    foreach (DataColumn col in _dt.Columns)
                    {
                        if (row[col.ColumnName] != null && !string.IsNullOrEmpty(Convert.ToString(row[col.ColumnName])))
                        {
                            ht.Add(col.ColumnName.ToUpper(), row[col.ColumnName]);
                        }
                        else
                        {
                            ht.Add(col.ColumnName.ToUpper(), "");
                        }
                    }
                    list.Add(ht);
                }
            }
            return list;
        }
        #endregion
    }

    
}
