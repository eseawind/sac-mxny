using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DAL;

namespace SAC.OBJ
{
    public class FactoryModel
    {
        public FactoryModel() { }


        public DateTime sTime = new DateTime();
        public DateTime eTime = new DateTime();
        public int count = 0;
        FactoryDAL dal = new FactoryDAL();

        #region 机组负荷曲线集合
        /// <summary>
        /// 获取机组负荷曲线集合
        /// </summary>
        public IList<Hashtable> line
        {
            get { return dal.GetUnitLine(sTime, eTime, count); }
            set { }
        }
        #endregion

        #region 全厂负荷
        /// <summary>
        /// 全厂负荷
        /// </summary>
        public double Power
        {
            get { return dal.GetPower(); }
            set
            {//judge = dal.SetPower(unitId, value);
            }
        }
        #endregion
    }
}
