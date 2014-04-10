using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class RLBLL
    {
        RLDAL dal = new RLDAL();
        /// <summary>
        /// 获取总装机容量
        /// </summary>
        /// <returns></returns>
        public string GetConfigRL()
        {
            return dal.GetConfigRL();
        }
    }
}
