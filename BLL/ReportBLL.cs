using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;

namespace BLL
{
    public class ReportBLL
    {
        ReportDAL dal = new ReportDAL();
        #region 数据分页
        /// <summary>
        /// 初始化分页数据
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public DataTable GetReportTable(string table, int startIndex, int endIndex)
        {
            return dal.GetReportTable(table, startIndex, endIndex);
        }

        /// <summary>
        /// 初始化分页数据
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public DataTable GetReportTableList(string table, int startIndex, int endIndex)
        {
            return dal.GetReportTableList(table, startIndex, endIndex);
        }

        /// <summary>
        /// 初始化分页数据
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public DataTable GetReportTableList(string table, string where, int startIndex, int endIndex)
        {
            return dal.GetReportTableList(table, where, startIndex, endIndex);
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable GetReportCountList(string table, string where)
        {
            return dal.GetReportCountList(table, where);
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable GetReportCountList(string table)
        {
            return dal.GetReportCountList(table);
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int GetReportCount(string table)
        {
            return dal.GetReportCount(table);
        }

        #endregion

        /// <summary>
        /// 添加报表名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddTable(string name, string id, string model, string tableName)
        {
            return dal.AddTable(name, id, model, tableName);
        }

        /// <summary>
        /// 获取原始测点
        /// </summary>
        /// <returns></returns>
        public DataTable GetPoints(string tableName)
        {
            return dal.GetPoints(tableName);
        }

        /// <summary>
        /// 添加报表属性
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <param name="name3"></param>
        /// <param name="jz"></param>
        /// <param name="order"></param>
        /// <param name="unit"></param>
        /// <param name="remark1"></param>
        /// <param name="remark2"></param>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <param name="tName"></param>
        /// <returns></returns>
        public bool AddSunInfo(string name1, string name2, string name3, string jz, string order, string unit, string remark1, string remark2, string id, string tid, string tName)
        {
            return dal.AddSunInfo(name1, name2, name3, jz, order, unit, remark1, remark2, id, tid, tName);
        }

        /// <summary>
        /// 编辑报表名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EditTable(string id, string name, string ido, string nameo, string tableName)
        {
            return dal.EditTable(id, name, ido, nameo, tableName);
        }

        /// <summary>
        /// 编辑报表属性 
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <param name="name3"></param>
        /// <param name="jz"></param>
        /// <param name="order"></param>
        /// <param name="unit"></param>
        /// <param name="remark1"></param>
        /// <param name="remark2"></param>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool EditsunTableInfo(string tableName, string name1, string name2, string name3, string jz, string order, string unit, string remark1, string remark2, string id, string key)
        {
            return dal.EditsunTableInfo(tableName, name1, name2, name3, jz, order, unit, remark1, remark2, id, key);
        }

        #region 获取模板
        /// <summary>
        /// 获取报表模板信息
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> GetModel()
        {
            return dal.GetModel();
        }
        #endregion

        #region 添加模板
        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddTableModel(string name, string path, string id)
        {
            return dal.AddTableModel(name, path, id);
        }
        #endregion

        #region 编辑模板
        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EddTableModel(string id, string n_id, string n_name, string n_path)
        {
            return dal.EddTableModel(id, n_id, n_name, n_path);
        }
        #endregion

        #region 删除模板
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DellTableModel(string id)
        {
            return dal.DellTableModel(id);
        }
        #endregion

        #region 删除报表
        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DellTable(string id, string tableName)
        {
            return dal.DellTable(id, tableName);
        }
        #endregion


        #region 执行SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        public bool RunSQL(string sql)
        {
            return dal.RunSQL(sql);
        }
        #endregion
        #region 获取参数顺序
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="name"></param>
        public object GetOrder(string name, string tableName)
        {
            return dal.GetOrder(name, tableName);
        }
        #endregion
        #region 获取测点名称
        /// <summary>
        /// 获取测点名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetPointName(string id)
        {
            return dal.GetPointName(id);
        }
        #endregion

        #region 删除数据
        public bool DelTableAttributeByDigital(string key, string table, string column)
        {
            return dal.DelTableAttributeByDigital(key, table, column);
        }
        #endregion

        #region 获取参数顺序
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="name"></param>
        public object GetModelID(string name, string tableName)
        {
            return dal.GetModelID(name, tableName);
        }
        #endregion

        #region 删除测点
        /// <summary>
        /// 删除测点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DellPoint(string id, string tableName)
        {
            return dal.DellPoint(id, tableName);
        }
        #endregion

        #region
        /// <summary>
        ///// 添加属性
        /// </summary>
        /// <param name="paraid"></param>
        /// <param name="paradesc"></param>
        /// <param name="paratype"></param>
        /// <param name="sq"></param>
        /// <param name="realtime"></param>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <param name="level3"></param>
        /// <param name="flag"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool AddTableAttribute(string paraid, string paradesc, string paratype, string sq, string realtime, string level1, string level2, string level3, string flag, string tableName)
        {
            return dal.AddTableAttribute(paraid, paradesc, paratype, sq, realtime, level1, level2, level3, flag, tableName);
        }
        #endregion

        #region 编辑测点
        /// <summary>
        /// 编辑测点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="paraid"></param>
        /// <param name="paradesc"></param>
        /// <param name="paratype"></param>
        /// <param name="sq"></param>
        /// <param name="realtime"></param>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <param name="level3"></param>
        /// <param name="flag"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool EditPoint(string key, string paraid, string paradesc, string paratype, string sq, string realtime, string level1, string level2, string level3, string flag, string tableName)
        {
            return dal.EditPoint(key, paradesc, paradesc, paratype, sq, realtime, level1, level2, level3, flag, tableName);
        }
        #endregion

        #region 添加拟合趋势关系
        /// <summary>
        /// 添加拟合趋势关系
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool AddNHQSMX(string x, string y, string tableName)
        {
            return dal.AddNHQSMX(x, y, tableName);
        }
        #endregion

        #region 编辑拟合趋势关系
        /// <summary>
        /// 编辑拟合趋势关系
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool EditPoint(string key, string x, string y, string tableName)
        {
            return dal.EditPoint(key, x, y, tableName);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Dell(string id, string tableName)
        {
            return dal.Dell(id, tableName);
        }
        #endregion


        #region 添加机组
        /// <summary>
        /// 添加机组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool AddUnit(string id, string name, string tableName)
        {
            return dal.AddUnit(id, name, tableName);
        }
        #endregion


        #region 编辑机组
        /// <summary>
        /// 编辑拟合趋势关系
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool EditUnit(string key, string id, string name, string tableName)
        {
            return dal.EditUnit(key, id, name, tableName);
        }
        #endregion
    }
}
