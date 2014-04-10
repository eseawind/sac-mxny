using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace SAC.OBJ
{
    public class RLModel
    {
        string unitId = "";
        public RLModel(string id)
        {
            unitId = id;
        }

        RLDAL dal = new RLDAL();
        public double judge = 0;
        public string time = "";

        /// <summary>
        /// 获取最近容量
        /// </summary>
        public double RL
        {
            get { judge = dal.GetNewestRL(unitId, time); return judge; }
            set { }
        }
    }
}
