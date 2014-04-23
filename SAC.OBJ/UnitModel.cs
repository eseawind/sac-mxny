using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace SAC.OBJ
{
    public class UnitModel
    {
        UnitDAL dal = new UnitDAL();
        public UnitModel(string key)
        {
            unitId = key;
        }
        public UnitModel(string key, string st, string et) { unitId = key; sTime = st; eTime = et; }

        public string unitId = "";
        public string time = "";
        public string sTime = "";
        public string eTime = "";

        public double judge = 0;

        #region 负荷
        /// <summary>
        /// 机组实时负荷
        /// </summary>
        public double Power
        {
            get { judge = dal.GetPower(unitId); return judge; }
            set
            {//judge = dal.SetPower(unitId, value);
            }
        }

        /// <summary>
        /// 机组历史负荷
        /// </summary>
        public double HisPower
        {
            get { judge = dal.GetHisPower(unitId, time); return judge; }
            set { }
        }

        /// <summary>
        /// 机组最大负荷
        /// </summary>
        public double PowerMax
        {
            get { judge = dal.GetTJPowerMax(unitId, sTime, eTime); return judge; }
            set { }
        }

        /// <summary>
        /// 机组最小负荷
        /// </summary>
        public double PowerMin
        {
            get { judge = dal.GetTJPowerMin(unitId, sTime, eTime); return judge; }
            set { }
        }

        /// <summary>
        /// 机组平均负荷
        /// </summary>
        public double PowerAvg
        {
            get { judge = dal.GetTJPowerAvg(unitId, sTime, eTime); return judge; }
            set { judge = dal.SetPowerAvg(unitId, value, sTime, eTime); }
        }
        #endregion

        #region 主汽流量
        /// <summary>
        /// 主汽流量
        /// </summary>
        public double Flow
        {
            get { judge = dal.GetFlow(unitId); return judge; }
            set { }
        }
        #endregion

        #region 主汽压力
        /// <summary>
        /// 主汽压力
        /// </summary>
        public double Pressure
        {
            get { judge = dal.GetPressure(unitId); return judge; }
            set { }
        }
        #endregion

        #region 主汽温度
        /// <summary>
        /// 主汽温度
        /// </summary>
        public double Temperature
        {
            get { judge = dal.GetTemperature(unitId); return judge; }
            set { }
        }
        #endregion

        #region 再热温度
        /// <summary>
        /// 再热温度
        /// </summary>
        public double ReheatTemperature
        {
            get { judge = dal.GetReheatTemperature(unitId); return judge; }
            set { }
        }
        #endregion

        #region 真空
        /// <summary>
        /// 真空
        /// </summary>
        public double Vacuum
        {
            get { judge = dal.GetVacuum(unitId); return judge; }
            set { }
        }
        #endregion

        #region 锅炉效率
        /// <summary>
        /// 锅炉效率
        /// </summary>
        public double Efficiency
        {
            get { judge = dal.GetEfficiency(unitId); return judge; }
            set { }
        }
        #endregion

        #region 热耗
        /// <summary>
        /// 热耗
        /// </summary>
        public double Heatconsumption
        {
            get { judge = dal.GetHeatconsumption(unitId); return judge; }
            set { }
        }
        #endregion

        #region 煤耗
        /// <summary>
        /// 煤耗
        /// </summary>
        public double Coalconsumption
        {
            get { judge = dal.GetCoalconsumption(unitId); return judge; }
            set { }
        }
        #endregion

        #region 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值
        /// <summary>
        /// 机组负荷 主汽流量 主汽压力 主汽温度 再热温度 真空 锅炉效率 热耗 煤耗 
        /// </summary>
        public double[] Val
        {
            get { return dal.GetVal(unitId); }
            set { }
        }
        #endregion

        #region 电量
        double dl;

        public double Dl
        {
            get { return dal.GetUnitDL(unitId, sTime, eTime); }
            //set { dl = value; }
        }
        #endregion

        #region 风速
        double wind;

        public double Wind
        {
            get { return dal.GetWind(unitId); }
            //set { wind = value; }
        }
        #endregion
    }
}
