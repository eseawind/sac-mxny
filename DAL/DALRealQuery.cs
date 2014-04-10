using System;
using SAC.Helper;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SAC.DBOperations;

///创建者：刘海杰
///日期：2013-06-27
namespace DAL
{
    public class DALRealQuery
    {
        DBLink dl = new DBLink();
        Plink pk = new Plink();

        DataTable dt = new DataTable();
        string sql = "", errMsg = "";

        /// <summary>
        /// 获取唯一列
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataTable GetTableColumns(string column, string tableName, string sqlWhere)
        {
            sql = "select distinct " + column + " from " + tableName;
            dt = dl.RunDataTable(sql, out errMsg);
            return dt;
        }

        /// <summary>
        /// 获取数据库中分公司
        /// </summary>
        /// <returns></returns>
        public DataSet Get_Company_Info(out string errMsg)
        {
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_COMPANYID,T_COMPANYDESC from T_BASE_COMPANY";


            DS = dl.RunDataSet(sql, out errMsg);


            return DS;
        }

        /// <summary>
        /// 获取数据库中电厂
        /// </summary>
        /// <param name="company_id">分公司ID</param>
        /// <returns></returns>
        public DataSet Get_Electric_Info(string company_id, out string errMsg)
        {

            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PLANTID,T_PLANTDESC from T_BASE_PLANT where T_COMPANYID='" + company_id + "'";

            DS = dl.RunDataSet(sql, out errMsg);

            return DS;
        }

        /// <summary>
        /// 获取数据库中电厂机组
        /// </summary>
        /// <param name="electric_id">电厂ID</param>
        /// <returns></returns>
        public DataSet Get_Unit_Info(string electric_id, out string errMsg)
        {

            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_UNITID,T_UNITDESC from T_BASE_UNIT where T_PLANTID='" + electric_id + "'";

            DS = dl.RunDataSet(sql, out errMsg);


            return DS;
        }

        /// <summary>
        /// 获取数据库中机组测点
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <returns></returns>
        public DataSet Get_Para_Info(string unit_id, out string errMsg)
        {

            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PARAID,T_PARADESC from T_BASE_PARAID where T_UNITID	='" + unit_id + "'";


            DS = dl.RunDataSet(sql, out errMsg);

            return DS;
        }

        /// <summary>
        /// 获取数据库中机组参数ID
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <returns></returns>
        public DataSet Get_BASE_CRICPARA(string unit_id, out string errMsg)
        {

            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PARAID,T_PARADESC from T_BASE_CRICPARA where T_UNITID	='" + unit_id + "'";


            DS = dl.RunDataSet(sql, out errMsg);


            return DS;
        }

        /// <summary>
        /// 获取实时测点数据
        /// </summary>
        /// <param name="real_data">测点id</param>
        /// <param name="stime">查询的起始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns>返回值数组</returns>
        public IList<Hashtable> GetChartData(string[] real_data, string stime, string etime, out string max_data_total, out string min_data_total)
        {

            int ret = 0, num = 0, num_pin = 1;
            max_data_total = ""; min_data_total = "";
            double drvA = 0;
            double max_data = 0, min_data = 0;
            //dt = new DataTable();
            //if (rtDBType == "EDNA")
            //{

            //}
            //else
            //{
            Plink.OpenPi();
            //}
            IList<Hashtable> listdata = new List<Hashtable>();
            //dt.Columns.Add("序号", typeof(int));
            Hashtable ht = new Hashtable();

            for (int i = 0; i < real_data.Length; i++)
            {
                string[] dv;

                DateTime date;
                if (Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalMinutes) > 20)
                {
                    date = DateTime.Parse(stime);
                    dv = new string[200]; //值
                }
                else
                {
                    date = DateTime.Parse(stime).AddMinutes(-10);
                    dv = new string[Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime).AddMinutes(-10)).TotalSeconds / 1)]; //值
                }

                ht = new Hashtable();
                ArrayList ld = new ArrayList();
                ArrayList lt = new ArrayList();
                ht.Add("name", real_data[i].Split(',')[1]);
                //dt.Columns.Add(real_data[i].Split(',')[1], typeof(double));
                //if (i==real_data.Length-1)
                //{
                //    dt.Columns.Add("时间", typeof(DateTime));
                //}

                while (date < DateTime.Parse(etime))
                {
                    //if (rtDBType == "EDNA")
                    //{
                    //    ek.GetHisValueByTime(real_data[i].Split(',')[0], date.ToString(), ref ret, ref drvA);
                    //}
                    //else
                    //{
                    pk.GetHisValue(real_data[i].Split(',')[0], date.ToString(), ref drvA);
                    //}
                    ld = new ArrayList();
                    ld.Add(DateTimeToUTC(date));
                    ld.Add(Convert.ToDouble(drvA.ToString()));
                    lt.Add(ld);
                    if (num == 0)
                    {
                        max_data = Convert.ToDouble(drvA.ToString());
                        min_data = Convert.ToDouble(drvA.ToString());
                        num++;
                    }
                    else
                    {
                        if (max_data < Convert.ToDouble(drvA.ToString()))
                        {
                            max_data = Convert.ToDouble(drvA.ToString());
                        }
                        if (min_data > Convert.ToDouble(drvA.ToString()))
                        {
                            min_data = Convert.ToDouble(drvA.ToString());
                        }
                    }
                    if (Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalMinutes) > 20)
                    {
                        date = date.AddSeconds(Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalSeconds) / 200);
                    }
                    else
                    {

                        date = date.AddSeconds(1.0);
                    }
                }
                max_data_total += max_data + ",";
                min_data_total += min_data + ",";
                max_data = 0; min_data = 0;
                ht.Add("data", lt);
                ht.Add("yAxis", i);
                listdata.Add(ht);
            }
            return listdata;
        }
        public static int DateTimeToUTC(DateTime DT)
        {
            long a = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            int rtnInt = 0;
            rtnInt = (int)((DT.Ticks - 8 * 3600 * 1e7 - a) / 1e7);
            return rtnInt;
        }

    }
}
