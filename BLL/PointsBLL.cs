using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class PointsBLL
    {
        PointsDAL dal = new PointsDAL();

        /// <summary>
        /// 获取所有测点
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPoints()
        {
            return dal.GetAllPoints();
        }
    }
}
