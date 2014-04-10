using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Collections;

namespace BLL
{
    public class PointBLL
    {
        PointDAL point = new PointDAL();


        /// <summary>
        /// 查询某段时间测点集合的历史值-刘海杰
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme1(string[] points, DateTime st, DateTime et)
        {
            return point.GetHistValAndTIme1(points, st, et);
        }

        /// <summary>
        /// 查询某段时间测点集合的历史值
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme(string[] points, DateTime st, DateTime et, int count)
        {
            return point.GetHistValAndTIme(points, st, et, count);
        }

        /// <summary>
        /// 查询某段时间测点集合的历史值-刘海杰
        /// </summary>
        /// <param name="names">测点集合</param>
        /// <param name="st">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <returns></returns>
        public IList<Hashtable> GetHistValAndTIme2(string[] points, DateTime st, DateTime et, int jiange)
        {
            return point.GetHistValAndTIme2(points, st, et, jiange);
        }

        /// <summary>
        /// 获取测点历史值
        /// </summary>
        /// <param name="points"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double[] GetPointVal(string[] points, string time)
        {
            return point.GetPointVal(points, time);
        }

        /// <summary>
        /// 获取测点最新值
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public double[] GetPointVal(string[] points)
        {
            return point.GetPointVal(points);
        }

    }
}
