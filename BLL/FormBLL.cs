using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;
namespace BLL
{
    public class FormBLL
    {
        FormDAL dal = new FormDAL();

        #region 创建数据表
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public string createTable(string tableName)
        {
            return dal.createTable(tableName);
        }
        #endregion

        #region 获取填报数据信息
        /// <summary>
        /// 获取填报数据信息
        /// </summary>
        /// <param name="treeId">菜单树ID</param>
        /// <param name="orgId">组织机构ID</param>
        /// // <param name="formID">表单ID</param>
        /// <returns></returns>
        public DataTable GetCreateInfo(string treeId, string orgId, string formID, string type)
        {
            return dal.GetCreateInfo(treeId, orgId, formID, type);
        }
        #endregion

        #region 为数据库表添加列
        /// <summary>
        /// 为数据库表添加列
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="columns">列集合</param>
        /// <returns></returns>
        public bool CreateColumns(string tableName, string columns)
        {
            return dal.CreateColumns(tableName, columns);
        }
        #endregion

        #region 填报数据
        /// <summary>
        /// 填报数据
        /// </summary>
        /// <param name="table">数据存储表名称</param>
        /// <param name="time">数据填报时间</param>
        /// <param name="timeName">数据存储表  时间字段名称</param>
        /// <param name="treeName">组织机构树名称</param>
        /// <param name="time">组织机构编号</param>
        /// <param name="value">填报的数据集</param>
        /// <param name="formID">表单编号</param>
        /// <returns></returns>
        public bool UpZBData(string table, string time, string timeName, string treeName, string orgId, string value, string formID)
        {
            return dal.UpZBData(table, time, timeName, treeName, orgId, value, formID);
        }
        #endregion

        #region 填报数据  组织机构维度数据
        /// <summary>
        /// 填报数据  组织机构维度数据
        /// </summary>
        /// <param name="table">数据存储表名称</param>
        /// <param name="time">填报时间</param>
        /// <param name="timeName">数据存储表时间字段名称</param>
        /// <param name="id">填报字段名称集合</param>
        /// <param name="orgId">组织维度集合</param>
        /// <param name="value">填报数据集合</param>
        /// <param name="xmlName">组织机构集合名称</param>
        /// <returns></returns>
        public bool UpData(string table, string time, string timeName, string id, string orgId, string value, string xmlName)
        {
            return dal.UpData(table, time, timeName, id, orgId, value, xmlName);
        }
        #endregion

        #region 获取填报参数  数据填报
        /// <summary>
        /// 获取组织机构数据  数据填报
        /// </summary>
        /// <param name="treeID">XML名称</param>
        /// <param name="orgID">组织机构编码</param>
        /// <param name="formId">表单编号</param>
        /// <param name="type">组织维度</param>
        /// <returns></returns>
        public DataTable GetDataParameter(string treeID, string orgID, string formId, string key)
        {
            return dal.GetDataParameter(treeID, orgID, formId, key);
        }
        #endregion

        #region 查询填报数据  指标维度
        /// <summary>
        /// 查询填报数据  指标维度
        /// </summary>
        /// <param name="columns">查询字段</param>
        /// <param name="tableName">查询表名称</param>
        /// <param name="timeType">时间字段名称</param>
        /// <param name="time">查询时间</param>
        /// /// <param name="treeName">组织机构树名称</param>
        /// <param name="orgId">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCreateValueZB(string columns, string tableName, string timeType, string time, string treeName, string orgId)
        {
            return dal.GetCreateValueZB(columns, tableName, timeType, time, treeName, orgId);
        }
        #endregion

        #region 查询填报数据
        /// <summary>
        /// 查询填报数据
        /// </summary>
        /// <param name="columns">查询字段</param>
        /// <param name="tableName">查询表名称</param>
        /// <param name="timeType">时间字段名称</param>
        /// <param name="time">查询时间</param>
        /// /// <param name="treeName">组织机构树名称</param>
        /// <param name="orgId">组织机构ID</param>
        /// <param name="fid">表单编号</param>
        /// <returns></returns>
        public DataTable GetCreateValue(string columns, string tableName, string timeType, string time, string treeName, string orgId, string fid)
        {
            return dal.GetCreateValue(columns, tableName, timeType, time, treeName, orgId, fid);
        }
        #endregion

        #region 获取填报公式等级
        /// <summary>
        /// 获取填报公式等级
        /// </summary>
        /// <param name="treeName">组织机构数名称</param>
        /// <param name="orgId">组织机构编码</param>
        /// <param name="formID">表单编号</param>
        /// <returns></returns>
        public DataTable GetFormGrade(string treeName, string orgId, string formID)
        {
            return dal.GetFormGrade(treeName, orgId, formID);
        }
        #endregion

        #region 搜索公式
        /// <summary>
        /// 搜索公式
        /// </summary>
        /// <param name="treeName">组织机构数名称</param>
        /// <param name="orgId">组织机构编码</param>
        /// <param name="formID">表单编号</param>
        /// <param name="grade">优先级</param>
        /// <returns></returns>
        public DataTable GetFormGradeList(string treeName, string orgId, string formID, string grade)
        {
            return dal.GetFormGradeList(treeName, orgId, formID, grade);
        }
        #endregion

        #region 获取组织机构数据  数据填报
        /// <summary>
        /// 获取组织机构数据  数据填报
        /// </summary>
        /// <param name="treeID">XML名称</param>
        /// <param name="orgID">组织机构编码</param>
        /// <param name="formId">表单编号</param>
        /// <param name="type">组织维度</param>
        /// <returns></returns>
        public DataTable GetDataOrgInfo(string treeID, string orgID, string formId, string type)
        {
            return dal.GetDataOrgInfo(treeID, orgID, formId, type);
        }
        #endregion

        #region 获取表单数据类型
        /// <summary>
        /// 获取表单数据类型
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="xmlName"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable GetDataType(string formID, string xmlName, string orgID)
        {
            return dal.GetDataType(formID, xmlName, orgID);
        }
        #endregion

        #region  生产数据日填报
        /// <summary>
        /// 生产数据日填报[上网电量]
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="orgId"></param>
        /// <param name="time"></param>
        /// <param name="sw">上网电量</param>
        /// <param name="gw">购网电量</param>
        /// <param name="wg">外购电量</param>
        /// <param name="gzqf">故障弃风电量</param>
        /// <param name="dwxd">电网限电弃风电量</param>
        /// <param name="gzqg">故障弃光电量</param>
        /// <param name="dwxdg">电网限电弃光电量</param>
        /// <returns></returns>
        public bool UpData(string treeId, string orgId, string time, string sw, string gw, string wg, string gzqf, string dwxd, string gzqg, string dwxdg,string gzts, string fdl)
        {
            return dal.UpData(treeId, orgId, time, sw, gw, wg, gzqf, dwxd, gzqg, dwxdg,gzts, fdl);
        }
        /// <summary>
        /// 读取手写填报数据信息
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="orgId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public ArrayList SelectInfo(string treeId, string orgId, string time)
        {
            return dal.SelectInfo(treeId, orgId, time);
        }

        #endregion
    }
}
