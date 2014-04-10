using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BLL;
using System.Text;
using System.Collections;
using SAC.OBJ;

namespace SACSIS.WebService
{
    /// <summary>
    /// Line 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Line : System.Web.Services.WebService
    {
        private DataTable dtPoints = new DataTable();
        private RLBLL bllRl = new RLBLL();
        private PointsBLL bllPoints = new PointsBLL();
        private PointBLL bllPoint = new PointBLL();
        private DLBLL bllDl = new DLBLL();
        private string strPower = "", strDl = "YZPP:06.D0";
        private DataRow[] drPoits = null;
        private double[] val = null;
        private object objRl = null;
        private StringBuilder sbl = new StringBuilder();

        #region 设置曲线
        [WebMethod]
        public string SetLine()
        {
            dtPoints = bllPoints.GetAllPoints();
            string[] _strPoints = new string[dtPoints.Rows.Count];
            string _name = "";
            string _points = "";
            for (int i = 0; i < dtPoints.Rows.Count; i++)
            {
                if (dtPoints.Rows[i][1].ToString() == "0")
                {
                    _name += "全厂,";
                    _strPoints[i] = dtPoints.Rows[i][2].ToString() + "|全厂,";
                }
                else
                {
                    _name += dtPoints.Rows[i][1] + ",";
                    _strPoints[i] = dtPoints.Rows[i][2].ToString() + "|" + dtPoints.Rows[i][1].ToString() + ",";
                }

            }
            _name = _name.Substring(0, _name.Length - 1);
            //_points = _points.Substring(0, _points.Length - 1);

            string _stTime = DateTime.Now.ToString("yyyy-MM-dd 0:00:00");
            string _edTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string _strLine1 = "";
            string _strLine2 = "";
            string _strLine3 = "";
            string _strLine4 = "";
            string _strLine5 = "";
            string _strLine6 = "";

            IList<Hashtable> _list = new List<Hashtable>();

            FactoryModel factory = new FactoryModel();
            factory.sTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            factory.eTime = DateTime.Now;
            factory.count = 600;
            _list = factory.line;
            //_list = bllPoint.GetHistValAndTIme(_strPoints, Convert.ToDateTime(_stTime), Convert.ToDateTime(_edTime), 600);
            if (_name.Split(',').Length == 1)
            {
                #region 1条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                #endregion
            }
            else if (_name.Split(',').Length == 2)
            {
                #region 2条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                        else if (_count == 2)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine2 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine2 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                _strLine2 = _strLine2.Substring(0, _strLine2.Length - 1);
                #endregion
            }
            else if (_name.Split(',').Length == 3)
            {
                #region 3条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                        else if (_count == 2)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine2 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine2 += "null,";
                        }
                        else if (_count == 3)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine3 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine3 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                _strLine2 = _strLine2.Substring(0, _strLine2.Length - 1);
                _strLine3 = _strLine3.Substring(0, _strLine3.Length - 1);
                #endregion
            }
            else if (_name.Split(',').Length == 4)
            {
                #region 4条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                        else if (_count == 2)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine2 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine2 += "null,";
                        }
                        else if (_count == 3)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine3 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine3 += "null,";
                        }
                        else if (_count == 4)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine4 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine4 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                _strLine2 = _strLine2.Substring(0, _strLine2.Length - 1);
                _strLine3 = _strLine3.Substring(0, _strLine3.Length - 1);
                _strLine4 = _strLine4.Substring(0, _strLine4.Length - 1);
                #endregion
            }
            else if (_name.Split(',').Length == 5)
            {
                #region 5条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                        else if (_count == 2)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine2 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine2 += "null,";
                        }
                        else if (_count == 3)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine3 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine3 += "null,";
                        }
                        else if (_count == 4)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine4 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine4 += "null,";
                        }
                        else if (_count == 5)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine5 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine5 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                _strLine2 = _strLine2.Substring(0, _strLine2.Length - 1);
                _strLine3 = _strLine3.Substring(0, _strLine3.Length - 1);
                _strLine4 = _strLine4.Substring(0, _strLine4.Length - 1);
                _strLine5 = _strLine5.Substring(0, _strLine5.Length - 1);
                #endregion
            }
            else if (_name.Split(',').Length == 6)
            {
                #region 6条曲线
                int _count = 0;
                foreach (Hashtable ht in _list)
                {
                    _count++;
                    ArrayList _arrayData = (ArrayList)ht["data"];

                    for (int i = 0; i < 144; i++)
                    {
                        ArrayList _arrayVal = new ArrayList();
                        if (_count == 1)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine1 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine1 += "null,";
                        }
                        else if (_count == 2)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine2 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine2 += "null,";
                        }
                        else if (_count == 3)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine3 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine3 += "null,";
                        }
                        else if (_count == 4)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine4 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine4 += "null,";
                        }
                        else if (_count == 5)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine5 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine5 += "null,";
                        }
                        else if (_count == 6)
                        {
                            if (_arrayData.Count > i)
                            {
                                _arrayVal = (ArrayList)_arrayData[i];
                                _strLine6 += _arrayVal[1] + ",";
                            }
                            else
                                _strLine6 += "null,";
                        }
                    }
                }
                _strLine1 = _strLine1.Substring(0, _strLine1.Length - 1);
                _strLine2 = _strLine2.Substring(0, _strLine2.Length - 1);
                _strLine3 = _strLine3.Substring(0, _strLine3.Length - 1);
                _strLine4 = _strLine4.Substring(0, _strLine4.Length - 1);
                _strLine5 = _strLine5.Substring(0, _strLine5.Length - 1);
                _strLine6 = _strLine6.Substring(0, _strLine6.Length - 1);
                #endregion
            }
            _strLine1 = "[" + _strLine1 + "]";
            _strLine2 = "[" + _strLine2 + "]";
            _strLine3 = "[" + _strLine3 + "]";
            _strLine4 = "[" + _strLine4 + "]";
            _strLine5 = "[" + _strLine5 + "]";
            _strLine6 = "[" + _strLine6 + "]";
            _name = "[" + _name + "]";
            string _str = "";

            if (_strLine6.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + ",\"_strLine2\":" + _strLine2 + ",\"_strLine3\":" + _strLine3 + ",\"_strLine4\":" + _strLine4 + ",\"_strLine5\":" + _strLine5 + ",\"_strLine6\":" + _strLine6 + "}";
            else if (_strLine5.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + ",\"_strLine2\":" + _strLine2 + ",\"_strLine3\":" + _strLine3 + ",\"_strLine4\":" + _strLine4 + ",\"_strLine5\":" + _strLine5 + "}";
            else if (_strLine4.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + ",\"_strLine2\":" + _strLine2 + ",\"_strLine3\":" + _strLine3 + ",\"_strLine4\":" + _strLine4 + "}";
            else if (_strLine3.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + ",\"_strLine2\":" + _strLine2 + ",\"_strLine3\":" + _strLine3 + "}";
            else if (_strLine2.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + ",\"_strLine2\":" + _strLine2 + "}";
            else if (_strLine1.Length > 2)
                _str = "{\"_strLine1\":" + _strLine1 + "}";

            return _str;
        }
        #endregion
    }
}
