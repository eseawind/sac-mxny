using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DBOperations;
using System.Data;
using System.Collections;

namespace DAL
{
    public class ReportDAL
    {
        DBLink dl = new DBLink();

        private string sql = "";
        private string errMsg = "";
        private int count = 0;
        private bool judge = false;
        private DataTable dt = null;
        private IList<Hashtable> list = new List<Hashtable>();

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
            string errMsg = "";
            DataTable dt = null;
            sql = "select * from (select a.*,rownumber() over() as rowid from (select * from Administrator." + table + ") a) tmp where tmp.rowid >=" + startIndex + " and tmp.rowid <= " + endIndex;
            dt = dl.RunDataTable(sql, out errMsg);
            sql = "";
            return dt;
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
            string errMsg = "";

            sql = "select * from (select a.*,rownumber() over() as rowid from (select distinct h.T_REPORTDESC,h.T_DCNAME,m.T_NAME,T_PATH From " + table + " h left join T_BASE_MODEL m on h.T_DCNAME=m.T_ID) a) tmp where tmp.rowid >=" + startIndex + " and tmp.rowid <= " + endIndex;
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return dt;
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
            string errMsg = "";

            sql = "select * from (select a.*,rownumber() over() as rowid from (select * from Administrator." + table + " where " + where + ") a) tmp where tmp.rowid >=" + startIndex + " and tmp.rowid <= " + endIndex;
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return dt;
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable GetReportCountList(string table)
        {
            sql = "select distinct T_REPORTDESC from " + table;
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return dt;
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable GetReportCountList(string table, string where)
        {
            sql = "select * from " + table + " where " + where;
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return dt;
        }

        /// <summary>
        /// 获取数据表记录条数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int GetReportCount(string table)
        {
            count = dl.GetCount(table); sql = "";
            return count;
        }
        #endregion

        /// <summary>
        /// 添加报表名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddTable(string name, string id, string model, string tableName)
        {
            sql = "insert into " + tableName + "(T_REPORTDESC,T_DCNAME,T_MODELID) values('" + name + "','" + id + "','" + model + "')";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }

        /// <summary>
        /// 获取原始测点
        /// </summary>
        /// <returns></returns>
        public DataTable GetPoints(string tableName)
        {
            sql = "select distinct h.PARAID,s.PARADESC From " + tableName + " h inner join STATPARA s on h.PARAID=s.PARAID";
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return dt;
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
            sql = "insert into T_BASE_HOURREPORT(T_PARAID,T_DESCRIP,I_JZ,I_SHOWID,T_UNIT,T_DCNAME,T_REPORTDESC,T_FENDESC,T_FENDESC2,T_REMARK,T_REMARK2) values(";
            sql += "'" + id + "','" + name1 + "'," + jz + "," + order + ",'" + unit + "','" + tid + "','" + tName + "','" + name2 + "','" + name3 + "','" + remark1 + "','" + remark2 + "'";
            sql += ")";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }

        /// <summary>
        /// 编辑报表名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EditTable(string id, string name, string ido, string nameo, string tableName)
        {
            sql = "update " + tableName + " set T_DCNAME='" + id + "',T_REPORTDESC='" + name + "' where T_DCNAME='" + ido + "' and T_REPORTDESC='" + nameo + "'";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "update " + tableName + " set T_PARAID='" + id + "',T_DESCRIP='" + name1 + "',I_JZ=" + jz + ",I_SHOWID=" + order + ",T_UNIT='" + unit + "',";
            sql += "T_FENDESC='" + name2 + "',T_FENDESC2='" + name3 + "',T_REMARK='" + remark1 + "',T_REMARK2='" + remark2 + "'";
            sql += " where ID_KEY =" + key;
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }

        #region 获取模板
        /// <summary>
        /// 获取报表模板信息
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> GetModel()
        {
            sql = "select T_ID,T_NAME,T_PATH from T_BASE_MODEL";
            dt = dl.RunDataTable(sql, out errMsg); sql = "";
            return DataTableToList(dt);
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
            sql = "insert into T_BASE_MODEL(T_NAME,T_PATH,T_ID) values('" + name + "','" + path + "','" + id + "')";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "update T_BASE_MODEL set T_ID='" + n_id + "',T_NAME='" + n_name + "',T_PATH='" + n_path + "'";
            sql += " where T_ID ='" + id + "'";
            judge = dl.RunNonQuery(sql, out errMsg);
            return judge;
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
            sql = "delete from  T_BASE_MODEL";
            sql += " where T_ID ='" + id + "'";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "delete from  " + tableName + "";
            sql += " where T_DCNAME ='" + id + "'";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }
        #endregion

        #region 执行SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        public bool RunSQL(string sql)
        {
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }
        #endregion

        #region 获取参数顺序
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="name"></param>
        public object GetOrder(string name, string tableName)
        {
            sql = "select max(I_SHOWID) from " + tableName + " where T_DCNAME='" + name + "'";
            object obj = dl.RunSingle(sql, out errMsg); sql = "";
            return obj;
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
            sql = "select PARADESC from STATPARA where PARAID='" + id + "'";
            object obj = dl.RunSingle(sql, out errMsg); sql = "";
            return obj;
        }
        #endregion

        #region 删除数据
        public bool DelTableAttributeByDigital(string key, string table, string column)
        {
            sql = "delete from  " + table + "";
            sql += " where " + column + " =" + key + "";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }
        #endregion


        #region 获取参数顺序
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="name"></param>
        public object GetModelID(string name, string tableName)
        {
            sql = "select T_MODELID from " + tableName + " where T_DCNAME='" + name + "'";
            object obj = dl.RunSingle(sql, out errMsg); sql = "";
            return obj;
        }
        #endregion

        #region 从DataTable转化为List
        /// <summary>
        /// 从DataTable转化为List
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <returns>List集合</returns>
        public IList<Hashtable> DataTableToList(DataTable dt)
        {
            IList<Hashtable> list = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<Hashtable>();
                Hashtable ht = null;
                foreach (DataRow row in dt.Rows)
                {
                    ht = new Hashtable();
                    foreach (DataColumn col in dt.Columns)
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

        #region 删除测点
        /// <summary>
        /// 删除测点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DellPoint(string id, string tableName)
        {
            sql = "delete from  " + tableName + "";
            sql += " where T_PARAID ='" + id + "'";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }
        #endregion

        #region 添加测点
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
            sql = "insert into " + tableName + "(T_PARAID,T_PARADESC,T_PARATYPE,T_SQL,T_REALTIME,T_LEVEL1,T_LEVEL2,T_LEVEL3,I_FLAG) values";
            sql += "('" + paraid + "','" + paradesc + "','" + paratype + "','" + sq + "','" + realtime + "','" + level1 + "','" + level2 + "','" + level3 + "'," + flag + ")";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "update " + tableName + " set T_PARAID='" + paraid + "',T_PARADESC='" + paradesc + "',T_PARATYPE='" + paratype + "',T_SQL='" + sq + "',T_REALTIME='" + realtime + "',";
            sql += "T_LEVEL1='" + level1 + "',T_LEVEL2='" + level2 + "',T_LEVEL3='" + level3 + "',I_FLAG=" + flag;
            sql += " where ID_KEY=" + key;
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "insert into " + tableName + "(T_XID,T_YID) values";
            sql += "('" + x + "','" + y + "')";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "update " + tableName + " set T_XID='" + x + "',T_YID='" + y + "'";
            sql += " where ID_KEY=" + key;
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "delete from  " + tableName + "";
            sql += " where ID_KEY =" + id;
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "insert into " + tableName + "(T_UNITID,T_UNITDESC) values";
            sql += "('" + id + "','" + name + "')";
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
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
            sql = "update " + tableName + " set T_UNITID='" + id + "',T_UNITDESC='" + name + "'";
            sql += " where ID_KEY=" + key;
            judge = dl.RunNonQuery(sql, out errMsg); sql = "";
            return judge;
        }
        #endregion
    }
}
