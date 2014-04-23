using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DBOperations;
using SAC.RealTimeDB;
using System.Data;
using System.Collections;

namespace DAL
{
    public class UnitDAL
    {
        string sql = "", errMsg = "";
        string[] point = null;
        double val = -1000000;
        object obj = null;
        bool judge = false;
        DBLink db = new DBLink();
        RTDBLink dll = new RTDBLink();

        #region  取值
        /// <summary>
        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double GetPointVal(string points)
        {

            double val = 0;

            dll.OpenPi();

            val = dll.GetRealTimeValue(points);
            val = getDouble(val, 2);

            return val;
        }

        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double GetHisPointVal(string points, string time)
        {
            double val = 0;
            dll.OpenPi();
            val = dll.GetHisValue(points, time);
            val = getDouble(val, 2);
            return val;
        }

        /// <summary>
        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double[] GetPointVal(string[] points)
        {
            double[] val = new double[points.Length];
            double v = 0;
            dll.OpenPi();

            for (int i = 0; i < points.Length; i++)
            {
                v = dll.GetRealTimeValue(points[i]);
                v = getDouble(v, 2);
                val[i] = v;

            }
            return val;
        }

        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="result">要转换的数值</param>
        /// <param name="num">保留位数</param>
        /// <returns></returns>
        public double getDouble(double result, int num)
        {
            string res = result.ToString();
            string results = "";
            int index = res.IndexOf('.');

            if (res.Length - index == num + 1)
                return Convert.ToDouble(res);
            else
            {
                if (index > 0)
                {
                    index += num;
                    res = res + "000000000000000000";
                    res = res.Remove(0, index + 1);
                    results = result + "000000000000000000";
                    results = results.ToString().Substring(0, index + 1);
                    res = res.Substring(0, 1);

                    string point = "0.";

                    for (int count = 0; count < num - 1; count++)
                    {
                        point += "0";
                    }
                    point += "1";


                    if (Convert.ToInt32(res) > 4)
                    {
                        results = (Convert.ToDouble(results) + Convert.ToDouble(point)).ToString();
                        res = results;
                    }
                    else
                    {
                        res = results;
                    }
                }
                else
                {
                    res += ".";
                    for (int i = 0; i < num; i++)
                    {
                        res += "0";
                    }
                }
                return Convert.ToDouble(res);
            }
        }
        #endregion
        #endregion

        #region 机组负荷操作

        #region 获取统计负荷 最大值  最小值  平均值

        #region Max
        /// <summary>
        /// 获取机组实时负荷
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns>最大负荷</returns>
        public double GetTJPowerMax(string unitId, string sTime, string eTime)
        {
            sql = "select max(T_VALUE) from T_INFO_STATISCS where T_UNITID='" + unitId + "' and (T_TIME between '" + sTime + "' and '" + eTime + "')";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = Convert.ToDouble(obj);
            }
            return val;
        }
        #endregion

        #region Min
        /// <summary>
        /// 获取机组实时负荷
        /// <summary>
        /// <param name="unitId">机组编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns>最小负荷</returns>
        public double GetTJPowerMin(string unitId, string sTime, string eTime)
        {
            sql = "select min(T_VALUE) from T_INFO_STATISCS where T_UNITID='" + unitId + "' and (T_TIME between '" + sTime + "' and '" + eTime + "')";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = Convert.ToDouble(obj);
            }
            return val;
        }
        #endregion

        #region Avg
        /// <summary>
        /// 获取机组实时负荷
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns>平均负荷</returns>
        public double GetTJPowerAvg(string unitId, string sTime, string eTime)
        {
            sql = "select avg(T_VALUE) from T_INFO_STATISCS where T_UNITID='" + unitId + "' and (T_TIME between '" + sTime + "' and '" + eTime + "')";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = Convert.ToDouble(obj);
            }
            return val;
        }
        #endregion

        #endregion

        #region 设置   平均负荷
        /// <summary>
        /// 设置平均负荷
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <param name="val">负荷</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public double SetPowerAvg(string unitId, double val, string sTime, string eTime)
        {
            sql = "update T_INFO_STATISCS set T_VALUE='" + val + "' where T_UNITID='" + unitId + "' and (T_TIME between '" + sTime + "' and '" + eTime + "')";
            judge = db.RunNonQuery(sql, out errMsg);
            if (judge)
                val = 1000000;
            return val;
        }
        #endregion

        #region 获取机组实时负荷
        /// <summary>
        /// 获取机组实时负荷
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetPower(string unitId)
        {
            sql = "select T_POWERTAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #region 获取机组历史负荷
        /// <summary>
        /// 获取机组历史负荷
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetHisPower(string unitId, string time)
        {
            sql = "select T_POWERTAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetHisPointVal(obj.ToString(), time);
            }
            return val;
        }
        #endregion

        #region 更改机组负荷测点
        /// <summary>
        /// 更改机组负荷测点
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public bool SetPower(string unitId, string point)
        {
            sql = "update T_BASE_POINT_UNIT set T_POWERTAG='" + point + "' where T_UNITID='" + unitId + "'";
            judge = db.RunNonQuery(sql, out errMsg);
            return judge;
        }
        #endregion

        #endregion

        #region 机组主汽流量操作

        #region 获取机组主汽流量
        /// <summary>
        /// 获取机组主汽流量
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetFlow(string unitId)
        {
            sql = "select T_FLOWTAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组主汽压力操作

        #region 获取机组主汽压力
        /// <summary>
        /// 获取机组主汽压力
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetPressure(string unitId)
        {
            sql = "select T_PRESSURETAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组主汽温度操作

        #region 获取机组主汽温度
        /// <summary>
        /// 获取机组主汽温度
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetTemperature(string unitId)
        {
            sql = "select T_TEMPERATURETAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组再热温度操作

        #region 获取机组再热温度
        /// <summary>
        /// 获取机组再热温度
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetReheatTemperature(string unitId)
        {
            sql = "select T_REHEATTEMPERATURETAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组真空操作

        #region 获取机组真空
        /// <summary>
        /// 获取机组真空
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetVacuum(string unitId)
        {
            sql = "select T_VACUUM from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组锅炉效率操作

        #region 获取机组锅炉效率
        /// <summary>
        /// 获取机组锅炉效率
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetEfficiency(string unitId)
        {
            sql = "select T_EFFICIENCY from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组热耗操作

        #region 获取机组热耗
        /// <summary>
        /// 获取机组热耗
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetHeatconsumption(string unitId)
        {
            sql = "select T_HEATCONSUMPTION from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 机组煤耗操作

        #region 获取机组煤耗
        /// <summary>
        /// 获取机组煤耗
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public double GetCoalconsumption(string unitId)
        {
            sql = "select T_COALCONSUMPTION from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #endregion

        #region 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值

        #region 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值
        /// <summary>
        /// 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <returns></returns>
        public double[] GetVal(string unitId)
        {
            sql = "select T_POWERTAG,T_FLOWTAG,T_PRESSURETAG,T_TEMPERATURETAG,T_REHEATTEMPERATURETAG,T_VACUUM,T_EFFICIENCY,T_HEATCONSUMPTION,T_COALCONSUMPTION from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            DataTable dt = db.RunDataTable(sql, out errMsg);
            string[] _str = new string[9];
            double[] _val = new double[9];
            for (int i = 0; i < 9; i++)
            {
                _str[i] = dt.Rows[0][i].ToString();
            }
            _val = GetPointVal(_str);
            return _val;
        }
        #endregion

        #endregion

        #region 获取机组风速
        /// <summary>
        /// 获取机组风速
        /// </summary>
        /// <param name="unitId">机组编号</param>
        /// <returns>平局风速</returns>
        public double GetWind(string unitId)
        {
            sql = "select T_WINDTAG from T_BASE_POINT_UNIT where T_UNITID='" + unitId + "'";
            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = GetPointVal(obj.ToString());
            }
            return val;
        }
        #endregion

        #region 获取机组电量
        /// <summary>
        /// 获取机组电量
        /// </summary>
        /// <param name="key>机组编码param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns>电量</returns>
        public double GetUnitDL(string key, string st, string et)
        {
            sql += "select sum(D_VALUE) val from T_INFO_STATISCS where T_TJID='DL' and T_ORGID='" + key + "' and (T_TIME between '" + st + "' and '" + et + "')";

            obj = db.RunSingle(sql, out errMsg);

            if (obj != null)
            {
                val = Convert.ToDouble(obj);
            }
            return val;
        }
        #endregion
    }
}
