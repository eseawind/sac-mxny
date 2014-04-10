using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class DLBLL
    {
        DLDAL dal = new DLDAL();
        /// <summary>
        /// 获取电量
        /// </summary>
        /// <param name="point"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <returns></returns>
        public object GetDLAll(string point, string st, string et)
        {
            return dal.GetDLAll(point, st, et);
        }
    }
}
