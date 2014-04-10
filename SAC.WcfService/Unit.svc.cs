using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAC.OBJ;

namespace SAC.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Unit”。
    public class Unit : IUnit
    {
        #region 负荷
        /// <summary>
        /// 机组实时负荷
        /// </summary>
        /// <returns></returns>
        public double Power(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Power;
        }

        /// <summary>
        /// 机组历史负荷
        /// </summary>
        /// <returns></returns>
        public double HisPower(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.HisPower;
        }

        /// <summary>
        /// 机组最大负荷
        /// </summary>
        /// <returns></returns>
        public double PowerMax(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.PowerMax;
        }

        /// <summary>
        /// 机组最小负荷
        /// </summary>
        /// <returns></returns>
        public double PowerMin(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.PowerMin;
        }

        /// <summary>
        /// 机组平均负荷
        /// </summary>
        /// <returns></returns>
        public double PowerAvg(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.PowerAvg;
        }
        #endregion

        #region 主汽流量
        /// <summary>
        /// 主汽流量
        /// </summary>
        /// <returns></returns>
        public double Flow(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Flow;
        }
        #endregion

        #region 主汽压力
        /// <summary>
        /// 主汽压力
        /// </summary>
        /// <returns></returns>
        public double Pressure(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Pressure;
        }
        #endregion

        #region 主汽温度
        /// <summary>
        /// 主汽温度
        /// </summary>
        /// <returns></returns>
        public double Temperature(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Temperature;
        }
        #endregion

        #region 再热温度
        /// <summary>
        /// 再热温度
        /// </summary>
        /// <returns></returns>
        public double ReheatTemperature(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.ReheatTemperature;
        }
        #endregion

        #region 真空
        /// <summary>
        /// 真空
        /// </summary>
        /// <returns></returns>
        public double Vacuum(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Vacuum;
        }
        #endregion

        #region 锅炉效率
        /// <summary>
        /// 锅炉效率
        /// </summary>
        /// <returns></returns>
        public double Efficiency(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Efficiency;
        }
        #endregion

        #region 热耗
        /// <summary>
        /// 热耗
        /// </summary>
        /// <returns></returns>
        public double Heatconsumption(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Heatconsumption;
        }
        #endregion

        #region 煤耗
        /// <summary>
        /// 煤耗
        /// </summary>
        /// <returns></returns>
        public double Coalconsumption(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Coalconsumption;
        }
        #endregion

        #region 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值
        /// <summary>
        /// 获取机组负荷、主汽流量、主汽压力、主汽温度、再热温度、真空、锅炉效率、热耗、煤耗的最新值
        /// </summary>
        /// <returns></returns>
        public double[] Val(string key)
        {
            UnitModel um = new UnitModel(key);
            return um.Val;
        }
        #endregion
    }
}
