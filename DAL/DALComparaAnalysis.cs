using System;
using SAC.Helper;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SAC.DBOperations;

///创建者：刘海杰
///日期：2013-08-08
namespace DAL
{
    public class DALComparaAnalysis
    {
        string rtDBType = "";   //实时数据库
        string rlDBType = "";   //关系数据库

        DBLink dl = new DBLink();
        Plink pk = new Plink();


        public IList<Hashtable> Get_Required_data(string unit_id, string[] para_id, string[] per, string hanshu, string stime, string etime, out string[] hanshu_gongshi, out string errMsg)
        {

            errMsg = "";
            hanshu_gongshi = new string[hanshu.Split(',').Length];
            ArrayList list = new ArrayList();
            DataSet DS = new DataSet();
            IList<Hashtable> listdata = new List<Hashtable>();
            Hashtable ht = new Hashtable();
            //循环曲线个数
            for (int i = 0; i < per.Length; i++)
            {
                //获取每条曲线标准负荷
                string sql = "SELECT T_INFO_RL,T_POWERTAG FROM ADMINISTRATOR.T_BASE_POINT_UNIT where T_UNITID=(select distinct u.T_UNITID from T_BASE_CHARTPARAID b inner join  ADMINISTRATOR.T_BASE_UNIT u on b.T_LEVEL1=u.T_UNITDESC where b.T_LEVEL1='" + unit_id + "')";

                DS = dl.RunDataSet(sql, out errMsg);

                if (DS != null)
                {
                    ht = new Hashtable();
                    ArrayList ld = new ArrayList();
                    ArrayList ldd = new ArrayList();
                    ArrayList lt = new ArrayList();
                    ArrayList ltt = new ArrayList();

                    double min_data = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString()) * Convert.ToDouble(per[i].Split('|')[0]) / 100;
                    double max_data = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString()) * Convert.ToDouble(per[i].Split('|')[1]) / 100;

                    int seconds = Convert.ToInt32((Convert.ToDateTime(etime) - Convert.ToDateTime(stime)).TotalSeconds) / 300;
                    DateTime dtt = Convert.ToDateTime(stime);
                    int count = 0;
                    ArrayList _listTime = new ArrayList();
                    while (dtt < Convert.ToDateTime(etime))
                    {
                        double val = new double();
                        Plink.OpenPi();
                        pk.GetHisValue(DS.Tables[0].Rows[0][1].ToString(), dtt.ToString(), ref val);

                        if (val >= min_data && val <= max_data)
                        {
                            count++;
                            _listTime.Add(dtt);
                        }
                        dtt = dtt.AddSeconds(seconds);
                    }
                    if (_listTime.Count > 0)
                    {
                        double[] x = new double[count];
                        double[] y = new double[count];

                        for (int k = 0; k < _listTime.Count; k++)
                        {
                            ld = new ArrayList();
                            double val = new double();
                            Plink.OpenPi();
                            pk.GetHisValue(para_id[0], _listTime[k].ToString(), ref val);
                            if (k == 0)
                            {
                                min_data = val;
                                max_data = val;
                            }
                            else
                            {
                                if (val > max_data)
                                    max_data = val;
                                if (val < min_data)
                                    min_data = val;
                            }
                            x[k] = val;
                            ld.Add(val);

                            Plink.OpenPi();
                            pk.GetHisValue(para_id[1], _listTime[k].ToString(), ref val);
                            y[k] = val;
                            ld.Add(val);

                            lt.Add(ld);
                        }

                        if (hanshu.Split(',')[0].Trim() == "多项式")
                        {
                            string gongshi = "";
                            double[] z = new double[Convert.ToInt32(hanshu.Split(',')[1]) + 1];
                            funPolynomial(x, y, Convert.ToInt32(hanshu.Split(',')[1]), out gongshi, out z);

                            hanshu_gongshi[i] = gongshi;
                            double num = (max_data - min_data) / 20;
                            for (double m = min_data; m < max_data; m = m + num)
                            {
                                ldd = new ArrayList();
                                ldd.Add(m);
                                double y_data = 0;
                                for (int j = z.Length - 1; j >= 0; j--)
                                {
                                    y_data += z[j] * (Math.Pow(m, j));
                                }
                                ldd.Add(y_data);
                                ltt.Add(ldd);
                            }

                        }
                        else if (hanshu.Split(',')[0].Trim() == "线性")
                        {
                            string gongshi = "", a = "", b = "";
                            funXianxing(x, y, out gongshi, out  a, out  b);
                            hanshu_gongshi[i] = gongshi;
                            double num = (max_data - min_data) / 20;
                            for (double m = min_data; m < max_data; m = m + num)
                            {
                                ldd = new ArrayList();
                                ldd.Add(m);
                                ldd.Add(Convert.ToDouble(a) * m + Convert.ToDouble(b));
                                ltt.Add(ldd);
                            }
                        }
                        else if (hanshu.Split(',')[0].Trim() == "指数")
                        {
                            //y=a*exp(b*x)
                            string gongshi = "", a = "", b = "";
                            funExponent(x, y, out gongshi, out a, out b);
                            hanshu_gongshi[i] = gongshi;
                            double num = (max_data - min_data) / 20;
                            for (double m = min_data; m < max_data; m = m + num)
                            {
                                ldd = new ArrayList();
                                ldd.Add(m);
                                ldd.Add(Convert.ToDouble(a) * (Math.Exp(Convert.ToDouble(b) * m)));
                                ltt.Add(ldd);
                            }
                        }
                        else if (hanshu.Split(',')[0].Trim() == "对数")
                        {
                            //y=a*ln(x)+b
                            string gongshi = "", a = "", b = "";
                            funLogarithm(x, y, out gongshi, out a, out b);
                            hanshu_gongshi[i] = gongshi;
                            double num = (max_data - min_data) / 20;
                            for (double m = min_data; m < max_data; m = m + num)
                            {
                                ldd = new ArrayList();
                                ldd.Add(m);
                                ldd.Add(Convert.ToDouble(a) * Math.Log(m) + Convert.ToDouble(b));
                                ltt.Add(ldd);
                            }

                        }
                        else if (hanshu.Split(',')[0].Trim() == "幂")
                        {
                            //y=a*(x^b)
                            string gongshi = "", a = "", b = "";
                            funPower(x, y, out gongshi, out a, out b);
                            hanshu_gongshi[i] = gongshi;
                            double num = (max_data - min_data) / 20;
                            for (double m = min_data; m < max_data; m = m + num)
                            {
                                ldd = new ArrayList();
                                ldd.Add(m);
                                ldd.Add(Convert.ToDouble(a) * (Math.Pow(m, Convert.ToDouble(b))));
                                ltt.Add(ldd);
                            }

                        }
                        ht.Add("data", lt);
                        ht.Add("type", "scatter");
                        ht.Add("name", "散点图");
                        listdata.Add(ht);
                        ht = new Hashtable();
                        ht.Add("data", ltt);
                        ht.Add("type", "line");
                        ht.Add("name", "拟合公式曲线图");
                        listdata.Add(ht);
                    }
                }
            }
            //for (int i = 0; i < per.Length; i++)
            //{
            //    string sql_Required = "select T_PARAID,T_OUTTABLE from T_BASE_CALCPARA where T_UNITID ='" + unit_id + "' and (T_PARAID ='Pel' or T_PARAID='" + para_id[0] + "' or T_PARAID ='" + para_id[1] + "')";
            //    //string sql = "select T_TYPE from T_BASE_CALCPARA where T_UNITID =" + unit_id + "' and T_PARAID='" + para_id+",";

            //    DS = dl.RunDataSet(sql_Required, out errMsg);

            //    if ((DS.Tables[0].Rows.Count > 0) && (DS.Tables[0].Rows[0]["T_OUTTABLE"].ToString() != "") && (DS.Tables[0].Rows[1]["T_OUTTABLE"].ToString() != "") && (DS.Tables[0].Rows[2]["T_OUTTABLE"].ToString() != ""))
            //    {
            //        ht = new Hashtable();
            //        ArrayList ld = new ArrayList();
            //        ArrayList ldd = new ArrayList();
            //        ArrayList lt = new ArrayList();
            //        ArrayList ltt = new ArrayList();
            //        string str1 = "", str2 = "", str3 = "";

            //        for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
            //        {
            //            if (DS.Tables[0].Rows[j]["T_PARAID"].ToString() == "Pel")
            //            {
            //                str1 = "select  ss.D_VALUE as " + para_id[0] + ",sss.D_VALUE as " + para_id[1] + " from (select  s.D_VALUE,s. T_DATETIME from (select D_VALUE,T_DATETIME  from " + DS.Tables[0].Rows[j]["T_OUTTABLE"].ToString() + ", " +
            //                "(select T_UNITID,\"D_CAPABILITY\" from T_BASE_UNIT where T_UNITID ='" + unit_id + "') as a where  a.T_UNITID = T_INFO_CALCDATA.T_UNITID  and " +
            //                "T_INFO_CALCDATA.T_PARAID = 'Pel' and  T_INFO_CALCDATA.D_VALUE between a.\"D_CAPABILITY\"*" + Convert.ToDouble(per[i].Split('|')[0]) / 100 +
            //                " and a.\"D_CAPABILITY\"*" + Convert.ToDouble(per[i].Split('|')[1]) / 100 + "   ) as s where s.T_DATETIME between '" + stime + "' and '" + etime +
            //                "' group by  s.D_VALUE,s. T_DATETIME order by s.T_DATETIME asc) as q, ";
            //            }
            //            else if (DS.Tables[0].Rows[j]["T_PARAID"].ToString() == para_id[0])
            //            {
            //                str2 = "(select D_VALUE,T_DATETIME  from " + DS.Tables[0].Rows[j]["T_OUTTABLE"].ToString() + " where T_UNITID ='" + unit_id + "' and  T_PARAID='" + para_id[0] + "' ) as ss,";
            //            }
            //            else if (DS.Tables[0].Rows[j]["T_PARAID"].ToString() == para_id[1])
            //            {
            //                str3 = "(select D_VALUE,T_DATETIME  from " + DS.Tables[0].Rows[j]["T_OUTTABLE"].ToString() + " where T_UNITID ='" + unit_id + "' and  T_PARAID='" + para_id[1] + "' ) as sss where  q.T_DATETIME = ss.T_DATETIME and  q.T_DATETIME  =sss.T_DATETIME ";
            //            }
            //        }

            //        string append_sql = str1 + str2 + str3;

            //        DS = dl.RunDataSet(append_sql, out errMsg);

            //        if (DS.Tables[0].Rows.Count > 0)
            //        {

            //            double[] x = new double[DS.Tables[0].Rows.Count];
            //            double[] y = new double[DS.Tables[0].Rows.Count];
            //            double min_data = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString());
            //            double max_data = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString());
            //            for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
            //            {
            //                ld = new ArrayList();
            //                ld.Add(Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()));
            //                if (min_data > Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()))
            //                {
            //                    min_data = Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString());
            //                }
            //                if (max_data < Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()))
            //                {
            //                    max_data = Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString());
            //                }
            //                x[j] = Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString());
            //                ld.Add(Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString()));

            //                y[j] = Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString());
            //                lt.Add(ld);
            //            }
            //            if (hanshu.Split(',')[0].Trim() == "多项式")
            //            {
            //                string gongshi = "";
            //                double[] z = new double[Convert.ToInt32(hanshu.Split(',')[1]) + 1];
            //                funPolynomial(x, y, Convert.ToInt32(hanshu.Split(',')[1]), out gongshi, out z);

            //                hanshu_gongshi[i] = gongshi;
            //                double num = (max_data - min_data) / 20;
            //                for (double m = min_data; m < max_data; m = m + num)
            //                {
            //                    ldd = new ArrayList();
            //                    ldd.Add(m);
            //                    double y_data = 0;
            //                    for (int j = z.Length - 1; j >= 0; j--)
            //                    {
            //                        y_data += z[j] * (Math.Pow(m, j));
            //                    }
            //                    ldd.Add(y_data);
            //                    ltt.Add(ldd);
            //                }

            //            }
            //            else if (hanshu.Split(',')[0].Trim() == "线性")
            //            {
            //                string gongshi = "", a = "", b = "";
            //                funXianxing(x, y, out gongshi, out  a, out  b);
            //                hanshu_gongshi[i] = gongshi;
            //                double num = (max_data - min_data) / 20;
            //                for (double m = min_data; m < max_data; m = m + num)
            //                {
            //                    ldd = new ArrayList();
            //                    ldd.Add(m);
            //                    ldd.Add(Convert.ToDouble(a) * m + Convert.ToDouble(b));
            //                    ltt.Add(ldd);
            //                }
            //            }
            //            else if (hanshu.Split(',')[0].Trim() == "指数")
            //            {
            //                //y=a*exp(b*x)
            //                string gongshi = "", a = "", b = "";
            //                funExponent(x, y, out gongshi, out a, out b);
            //                hanshu_gongshi[i] = gongshi;
            //                double num = (max_data - min_data) / 20;
            //                for (double m = min_data; m < max_data; m = m + num)
            //                {
            //                    ldd = new ArrayList();
            //                    ldd.Add(m);
            //                    ldd.Add(Convert.ToDouble(a) * (Math.Exp(Convert.ToDouble(b) * m)));
            //                    ltt.Add(ldd);
            //                }
            //            }
            //            else if (hanshu.Split(',')[0].Trim() == "对数")
            //            {
            //                //y=a*ln(x)+b
            //                string gongshi = "", a = "", b = "";
            //                funLogarithm(x, y, out gongshi, out a, out b);
            //                hanshu_gongshi[i] = gongshi;
            //                double num = (max_data - min_data) / 20;
            //                for (double m = min_data; m < max_data; m = m + num)
            //                {
            //                    ldd = new ArrayList();
            //                    ldd.Add(m);
            //                    ldd.Add(Convert.ToDouble(a) * Math.Log(m) + Convert.ToDouble(b));
            //                    ltt.Add(ldd);
            //                }

            //            }
            //            else if (hanshu.Split(',')[0].Trim() == "幂")
            //            {
            //                //y=a*(x^b)
            //                string gongshi = "", a = "", b = "";
            //                funPower(x, y, out gongshi, out a, out b);
            //                hanshu_gongshi[i] = gongshi;
            //                double num = (max_data - min_data) / 20;
            //                for (double m = min_data; m < max_data; m = m + num)
            //                {
            //                    ldd = new ArrayList();
            //                    ldd.Add(m);
            //                    ldd.Add(Convert.ToDouble(a) * (Math.Pow(m, Convert.ToDouble(b))));
            //                    ltt.Add(ldd);
            //                }

            //            }
            //            ht.Add("data", lt);
            //            ht.Add("type", "scatter");
            //            ht.Add("name", "散点图");
            //            listdata.Add(ht);
            //            ht = new Hashtable();
            //            ht.Add("data", ltt);
            //            ht.Add("type", "line");
            //            ht.Add("name", "拟合公式曲线图");
            //            listdata.Add(ht);
            //        }
            //    }
            //}
            return listdata;
        }

        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="result">要转换的数值</param>
        /// <param name="num">保留位数</param>
        /// <returns></returns>
        public string getDouble(double result)
        {
            string res = Math.Abs(result).ToString();
            string results;
            int index = res.IndexOf('.');
            if (index > 5)
            {
                results = Math.Round(result, 0).ToString();

            }
            else
            {
                if (result.ToString().IndexOf('E') > 1)
                {
                    results = result.ToString();
                }
                else
                {

                    results = Math.Round(result, 6 - index).ToString();
                }
            }
            return results;
            //string res = result.ToString();
            //string results = "";
            //int index = res.IndexOf('.');

            //if (index > 0)
            //{
            //    index += num;
            //    res += "0000000000000000";
            //    res = res.Remove(0, index + 1);
            //    results = result + "0000000000000000000";
            //    results = results.Substring(0, index + 1);
            //    res = res.Substring(0, 1);

            //    string point = "0.";

            //    for (int count = 0; count < num - 1; count++)
            //    {
            //        point += "0";
            //    }
            //    point += "1";


            //    if (Convert.ToDouble(res) > 4)
            //    {
            //        results = (Convert.ToDouble(results) + Convert.ToDouble(point)).ToString();
            //        res = results;
            //    }
            //    else
            //    {
            //        res = results;
            //    }
            //}
            //else
            //{
            //    res += ".";
            //    for (int i = 0; i < num; i++)
            //    {
            //        res += "0";
            //    }
            //}
            //return Convert.ToDouble(res);
        }
        #endregion

        private void funXianxing(double[] x, double[] y, out string gongshi, out string a, out string b)
        {
            //线性曲线拟合，y=ax+b;
            int Num = x.Length;
            double x2 = 0, y2 = 0, lxx = 0, lxy = 0;
            for (int i = 0; i < Num; i++)
            {
                x2 = x2 + x[i];
                y2 = y2 + y[i];
            }
            x2 = x2 / Num;
            y2 = y2 / Num;
            for (int i = 0; i < Num; i++)
            {
                lxx = lxx + (x[i] - x2) * (x[i] - x2);
                lxy = lxy + (x[i] - x2) * (y[i] - y2);
            }
            a = getDouble(lxy / lxx);
            b = getDouble(y2 - Convert.ToDouble(a) * x2);
            if (b.Substring(0, 1) == "-")
            {
                gongshi = "y= " + a + "x " + b;
            }
            else
            {
                gongshi = "y= " + a + "x +" + b;
            }

        }

        private void funLogarithm(double[] x, double[] y, out string gongshi, out string a, out string b)
        {
            int Num = x.Length;
            //'对数曲线拟合,y=a*ln(x)+b
            //'Num为输入数据点个数
            //'x()为输入数据点横坐标组成的数组
            //'y()为输入数据点纵坐标组成的数组
            //'a，b为待求系数，为输出项
            double x2 = 0, y2 = 0, lxx = 0, lxy = 0;
            for (int i = 0; i < Num; i++)
            {
                x[i] = Math.Log(x[i]); //以e为底数
            }
            for (int i = 0; i < Num; i++)
            {
                x2 = x2 + x[i];
                y2 = y2 + y[i];
            }
            x2 = x2 / Num;
            y2 = y2 / Num;
            for (int i = 0; i < Num; i++)
            {
                lxx = lxx + (x[i] - x2) * (x[i] - x2);
                lxy = lxy + (x[i] - x2) * (y[i] - y2);
            }
            a = getDouble(lxy / lxx);
            b = getDouble(y2 - Convert.ToDouble(a) * x2);
            if (b.Substring(0, 1) == "-")
            {
                gongshi = "y= " + a + "ln(x) " + b;
            }
            else
            {
                gongshi = "y= " + a + "ln(x) +" + b;
            }

        }

        private void funExponent(double[] x, double[] y, out string gongshi, out string a, out string b)
        {
            //'指数曲线拟合,y=a*exp(b*x)
            //'Num为输入数据点个数
            //'x()为输入数据点横坐标组成的数组
            //'y()为输入数据点纵坐标组成的数组
            //'a，b为待求系数，为输出项
            int Num = x.Length;
            double x2 = 0, y2 = 0, lxx = 0, lxy = 0;
            for (int i = 0; i < Num; i++)
            {
                y[i] = Math.Log(y[i]); //以e为底数
            }
            for (int i = 0; i < Num; i++)
            {
                x2 = x2 + x[i];
                y2 = y2 + y[i];
            }
            x2 = x2 / Num;
            y2 = y2 / Num;
            for (int i = 0; i < Num; i++)
            {
                lxx = lxx + (x[i] - x2) * (x[i] - x2);
                lxy = lxy + (x[i] - x2) * (y[i] - y2);
            }
            b = getDouble(lxy / lxx);
            a = getDouble(Math.Exp(y2 - Convert.ToDouble(b) * x2));
            gongshi = "y = " + a + "exp(" + b + "x)";
        }

        private void funPower(double[] x, double[] y, out string gongshi, out string a, out string b)
        {
            //'乘幂曲线拟合,y=a*(x^b)
            //'Num为输入数据点个数
            //'x()为输入数据点横坐标组成的数组
            //'y()为输入数据点纵坐标组成的数组
            int Num = x.Length;
            double x2 = 0, y2 = 0, lxx = 0, lxy = 0;
            for (int i = 0; i < Num; i++)
            {
                x[i] = Math.Log(x[i]);
                y[i] = Math.Log(y[i]);
                x2 = x2 + x[i];
                y2 = y2 + y[i];
            }
            x2 = x2 / Num;
            y2 = y2 / Num;
            for (int i = 0; i < Num; i++)
            {
                lxx = lxx + (x[i] - x2) * (x[i] - x2);
                lxy = lxy + (x[i] - x2) * (y[i] - y2);
            }

            b = getDouble(lxy / lxx);
            a = getDouble(Math.Exp(y2 - Convert.ToDouble(b) * x2));
            gongshi = "y = " + a + "(x^" + b + ")";

        }
        private void funPolynomial(double[] x, double[] y, int Degree, out string gongshi, out double[] z)
        {
            //'多项式曲线拟合 y=a0+a1*x+a2*x^2+an*x^n
            //'Num为输入数据点个数
            //'x()为输入数据点横坐标组成的数组
            //'y()为输入数据点纵坐标组成的数组
            //'Degree为要拟合的多项式曲线次数
            //' AA() 为待求系数，为输出项
            //double a = 0,b = 0,z = 0;
            int Num = x.Length;
            int i = 0, j = 0, K = 0, M = 0;
            M = Degree + 1;
            double[,] a = new double[M, M];
            double[] b = new double[M]; z = new double[M];
            a[0, 0] = Num;
            for (i = 0; i < Num; i++)
            {
                b[0] = b[0] + y[i];
            }
            for (j = 1; j < M; j++)
            {
                for (i = 0; i < Num; i++)
                {
                    a[0, j] = a[0, j] + Math.Pow(x[i], (double)(j));
                }
            }
            for (i = 1; i < M; i++)
            {
                for (j = 0; j < M; j++)
                {
                    for (K = 0; K < Num; K++)
                    {
                        a[i, j] = a[i, j] + Math.Pow(x[K], (double)(i + j));
                        if (j == 1)
                        {
                            b[i] = b[i] + Math.Pow(x[K], (double)(i)) * y[K];
                        }
                    }
                }
            }
            SolveEquation(M, a, b, z);
            string[] pinfang = new string[8] { "", "x", "x2", "x3", "x4", "x5", "x6", "x7" };
            gongshi = "y= ";
            for (i = M - 1; i >= 0; i--)
            {
                if ((i != M - 1) && (z[i].ToString().Substring(0, 1) != "-"))
                {
                    gongshi += "+";

                }
                if (z[i].ToString().Split('E').Length > 1)
                {
                    gongshi += getDouble(Convert.ToDouble(z[i].ToString().Split('E')[0])) + "E" + z[i].ToString().Split('E')[1] + pinfang[i];
                }
                else
                {
                    gongshi += getDouble(z[i]) + pinfang[i];
                }
            }
        }

        private void SolveEquation(int N, double[,] a, double[] b, double[] z)
        {
            //'解AZ=B方程，采用高斯-约当消去法
            //'输入为矩阵a(),向量 b()
            //'输出为z()

            double TempA, ChuShu, Sum;
            int i, j, II, L, K, KK;
            for (i = 0; i < N; i++)
            {
                L = 0; KK = 0;
                for (j = i; j < N; j++)
                {
                    if (a[j, i] == 0)
                    {
                        L = L + 1;
                    }
                }

                for (j = i; j < N - L; j++)
                {
                    if (a[j, i] == 0)
                    {
                        KK = KK + 1;
                        for (K = i; K < N; K++)
                        {
                            TempA = a[j, K];
                            a[j, K] = a[N - KK + 1, K];
                            a[N - KK + 1, K] = TempA;
                        }
                        TempA = b[j];
                        b[j] = b[N - KK + 1];
                        b[N - KK + 1] = TempA;
                    }
                }

                for (II = i; II < N - L; II++)
                {
                    ChuShu = a[II, i];
                    for (j = i; j < N; j++)
                    {
                        a[II, j] = a[II, j] / ChuShu;
                    }
                    b[II] = b[II] / ChuShu;
                }
                for (II = i + 1; II < N - L; II++)
                {
                    for (j = i; j < N; j++)
                    {
                        a[II, j] = a[II, j] - a[i, j];
                    }
                    b[II] = b[II] - b[i];
                }
            }

            for (i = 0; i < N; i++)
            {
                for (j = 0; j < i; j++)
                {
                    a[i, j] = 0;
                }
            }
            z[N - 1] = b[N - 1] / a[N - 1, N - 1];

            for (i = N - 2; i >= 0; i--)
            {
                Sum = 0;
                for (j = i + 1; j < N; j++)
                {
                    Sum = Sum + a[i, j] * z[j];

                }
                z[i] = (b[i] - Sum) / a[i, i];
            }
        }
    }
}
