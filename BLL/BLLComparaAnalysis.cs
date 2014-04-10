using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

///创建者：刘海杰
///日期：2013-08-08

namespace BLL
{
    public class BLLComparaAnalysis
    {
        DAL.DALComparaAnalysis DCA = new DALComparaAnalysis();

        public IList<Hashtable> Get_Required_data(string unit_id, string[] para_id, string[] per, string hanshu, string stime, string etime, out string[] gongshi, out string errMsg)
        {
            return DCA.Get_Required_data(unit_id, para_id, per, hanshu, stime, etime, out gongshi, out errMsg);
        }
    }
}
