using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using SAC.Helper;


namespace SAC.RealTimeDB
{
    class Slink
    {
        #region methods

        [DllImport("rtdbapi.dll", EntryPoint = "RT_connect")]
        private extern static int RT_connect(string hostname, int port, ref int handle);

        [DllImport("rtdbapi.dll", EntryPoint = "RT_disconnect")]
        private extern static int RT_disconnect(int handle);

        [DllImport("rtdbapi.dll", EntryPoint = "RT_login")]
        private extern static int RT_login(int handle, string user, string password, ref int priv);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_append_table")]
        private extern static int RTb_append_table(int handle, ref _RT_TABLE table);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_insert_base_point")]
        private extern static int RTb_insert_base_point(int handle, string tag, int type, int tableid, int use_ms, ref int point_id);

        [DllImport("rtdbapi.dll", EntryPoint = "RTs_get_snapshots")]
        private extern static int RTs_get_snapshots(int handle, ref int count, int[] ids, int[] datetimes, int[] ms, double[] values, Int64[] states, short[] qualities, uint[] errors);

        [DllImport("rtdbapi.dll", EntryPoint = "RTs_put_snapshots")]
        private extern static int RTs_put_snapshots(int handle, ref int count, int[] ids, int[] datetimes, int[] ms, double[] values, Int64[] states, short[] qualities, uint[] errors);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_find_points")]
        private extern static int RTb_find_points(int handle, ref int count, string[] table_dot_tags, int[] ids, int[] types, int[] classof, short[] use_ms);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_search")]
        private extern static int RTb_search(int handle, string tagmask, string tablemask, string source, string unit, string desc, string instrument, int mode, int[] ids, ref int count);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_get_points_property")]
        private extern static int RTb_get_points_property(int handle, int count, [In, Out] _RT_POINT[] bas, [In, Out] _RT_SCAN_POINT[] scan, [In, Out] _RT_CALC_POINT[] calc, uint[] errors);

        [DllImport("rtdbapi.dll", EntryPoint = "RT_format_message")]
        private extern static int RT_format_message(uint ecode, byte[] message, byte[] name, long size);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_get_tables")]
        private extern static int RTb_get_tables(int handle, int[] ids, ref int count);

        [DllImport("rtdbapi.dll", EntryPoint = "RTb_get_table_property_by_id")]
        private extern static int RTb_get_table_property_by_id(int handle, ref _RT_TABLE field);

        //[DllImport("rtdbapi.dll", EntryPoint = "RT_login")]
        //private extern static int RT_login(int handle, string user, string password, byte[] priv);

        [DllImport("rtdbapi.dll", EntryPoint = "RT_change_password")]
        private extern static int RT_change_password(int handle, string user, string password);


        [DllImport("rtdbapi.dll", EntryPoint = "RTh_get_single_value")]
        private extern static int RTh_get_single_value(int handle, int id, int mode, ref int datetime, ref int ms, ref double value, ref long state, ref short qual);


        [DllImport("rtdbapi.dll", EntryPoint = "RTh_update_value")]
        private extern static int RTh_update_value(int handle, int id, int datetime, short ms, double value, Int64 state, short quality);
        [DllImport("rtdbapi.dll", EntryPoint = "RT_parse_time")]
        private extern static int RT_parse_time(string str_time, int datetime, short ms);//str_time:时间字符串，如 "2010-1-1" 及 "2010-1-1 8:00:00"；

        #endregion

        //error codes
        public const int SOAP_OK = 0;
        public const int SOAP_FAULT = 12;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _RT_TABLE
        {
            public int id;
            public int type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string desc;
        }


        //
        public enum SEARCHMODE
        {
            SORTBYTABLE = 0,
            SORTBYNAME = 1,
            SORTBYPT = 2
        }

        public enum RT_TYPE
        {
            SR_BOOL = 0,//布尔类型，0值或1值
            SR_UINT8 = 1,//无符号8位整数，占用一个字节
            SR_INT8 = 2,//有符号8位整数，占用一个字节
            SR_CHAR = 3,//单字节字符，占用一个字节
            SR_UINT16 = 4,//无符号16位整数，占用两个字节
            SR_INT16 = 5,//有符号16位整数，占用连个字节
            SR_UINT32 = 6,//无符号32位整数，占用四个字节
            SR_INT32 = 7,//有符号32位整数，占用四个字节
            SR_INT64 = 8,//有符号64位整数，占用八个字节
            SR_REAL16 = 9,//16位浮点数，占用两个字节
            SR_REAL32 = 10,//32位单精度浮点数，占用四个字节
            SR_REAL64 = 11,//64位双精度浮点数，占用八个字节
            SR_COOR = 12,//二维坐标，具有x、y两个维度的浮点数，占用八个字节
            SR_STR = 13,//字符串，长度不超过存储页面的大小
            SR_BLOB = 14//二进制数据块，占用字节不超过存储页面的大小
        }

        //public struct _RT_POINT
        //{
        //    public string tag;//标签点名称
        //    public int id;//全库唯一标识，创建标签点时，系统会自动为标签点分配的唯一标识。
        //    public string desc;//标签点的描述
        //    public float highlimit;//标签点的上限
        //    public float lowlimit;//标签点的下限
        //    public int table;//测点所属表的id
        //    public float typical;//典型值
        //    public TYPE type;//测点类型(标签点数值类型)
        //    public string unit;//测点工程单位
        //    public byte archive;//是否存档
        //    public short digits;//
        //    public byte shutdown;
        //    public byte step;
        //    public byte compress;
        //    public float compdev;
        //    public float compdevpercent;
        //    public int comptimemax;
        //    public int comptimemin;
        //    public float excdev;
        //    public float excdevpercent;
        //    public int exctimemax;
        //    public int exctimemin;
        //    public uint classof;
        //    public int changedate;
        //    public string changer;
        //    public int createdate;
        //    public string creator;
        //    public byte mirror;
        //    public byte microsecond;
        //    public uint scanindex;
        //    public uint calcindex;
        //    public uint alarmindex;
        //    public string table_dot_tag;
        //    public byte[] padding;
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct _RT_POINT
        {

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string tag;
            public int id;
            public int type;
            public int table;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string desc;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string unit;
            public byte archive;
            public short digits;
            public byte shutdown;
            public float lowlimit;
            public float highlimit;
            public byte step;
            public float typical;
            public byte compress;
            public float compdev;
            public float compdevpercent;
            public int comptimemax;
            public int comptimemin;
            public float excdev;
            public float excdevpercent;
            public int exctimemax;
            public int exctimemin;
            public uint classof;
            public int changedate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string changer;
            public int createdate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string creator;
            public byte mirror;
            public byte microsecond;
            public uint scanindex;
            public uint calcindex;
            public uint alarmindex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 160)]
            public string table_dot_tag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] padding;
        }

        //public struct _RT_SCAN_POINT
        //{
        //    public int id;//全库唯一标识，0表示无效
        //    public string source;//数据源
        //    public byte scan;
        //    public string instrument;
        //    public int[] locations;
        //    public int[] userints;
        //    public float[] userreals;
        //    public byte[] paddding;
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct _RT_SCAN_POINT
        {
            public int id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string source;
            public byte scan;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string instrument;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public int[] locations;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] userints;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public float[] userreals;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 164)]
            public byte[] padding;
        }

        //public struct _RT_CALC_POINT
        //{
        //    public int id;//全库唯一标识，0表示无效
        //    public string equation;//实时方程式
        //    public byte trigger;
        //    public byte timecopy;
        //    public int period;
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct _RT_CALC_POINT
        {
            public int id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2036)]
            public string equation;
            public byte trigger;
            public byte timecopy;
            public int period;
        }

        public string Login(int handle, string user, string password)
        {
            //byte[] priv=new byte[1024];

            //if (0 == RT_login(handle, user, password, priv))
            //{
            //    int privnumber = 0;
            //    foreach (byte b in priv)
            //    {
            //        privnumber += Convert.ToInt32(b);
            //    }
            //    return privnumber.ToString();
            //}
            //else
            //{
            //    return "登录失败!";
            //}
            return "";
        }

        /// <summary>
        /// 建立SR数据平台的网络连接
        /// </summary>
        /// <param name="hostname">ip地址</param>
        /// <param name="port">端口</param>
        /// <param name="handle">连接句柄</param>
        /// <returns></returns>
        public int ConnectToSR(string hostname, int port, ref int handle)
        {
            return RT_connect(hostname, port, ref handle);
        }

        public int UpdateValue(int handle, int id, int datetime, short ms, double value, Int64 state, short quality)
        {
            return RTh_update_value(handle, id, datetime, ms, value, state, quality);
        }

        public static int DisconectToSR(int handle)
        {
            return RT_disconnect(handle);
        }
        /// <summary>
        /// 以有效账户登录
        /// </summary>
        /// <param name="handle">连接句柄</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="priv">账户权限</param>
        /// <returns></returns>
        public int LoginSR(int handle, string user, string password, ref int priv)
        {
            return RT_login(handle, user, password, ref priv);
        }

        /// <summary>
        /// 打开实时数据库连接
        /// </summary>
        /// <returns></returns>
        public static int OpenSR(ref int handle)
        {
            int i = 0;
            int _priv = 0;
            string serverName = IniHelper.ReadIniData("RTDB", "DBIP", null);// System.Configuration.ConfigurationSettings.AppSettings["piserver"].ToString();
            string userName = IniHelper.ReadIniData("RTDB", "DBUser", null);// System.Configuration.ConfigurationSettings.AppSettings["piuser"].ToString();
            string userPwd = IniHelper.ReadIniData("RTDB", "DBPwd", null); //System.Configuration.ConfigurationSettings.AppSettings["pipwd"].ToString();
            string PortNumber = IniHelper.ReadIniData("RTDB", "DBPort", null);
            i = RT_connect(serverName, int.Parse(PortNumber), ref handle);
            i = RT_login(handle, userName, userPwd, ref _priv);
            return i;
        }


        public int GetSingleValue(int handle, int id, int mode, ref int datetime, ref int ms, ref double value, ref long state, ref short qual)
        {
            return RTh_get_single_value(handle, id, mode, ref datetime, ref ms, ref value, ref state, ref qual);
        }


        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ChangePsw(int handle, string user, string password)
        {
            return RT_change_password(handle, user, password);
        }
        /// <summary>
        /// 获取所有表的信息
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public int GetTables(ref string Result)
        {
            int handle = 0;
            if (0 != Connect(ref handle))
            {
                return SOAP_FAULT;
            }
            int[] ids = new int[999];
            int count = 999;
            if (0 != RTb_get_tables(handle, ids, ref count))
            {
                return SOAP_FAULT;
            }
            _RT_TABLE field = new _RT_TABLE();
            for (int i = 0; i < count; i++)
            {
                field.id = ids[i];

                try
                {
                    if (0 == RTb_get_table_property_by_id(handle, ref field))
                    {
                        Result += field.id.ToString() + "," + field.name + "," + field.desc + "|";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (string.IsNullOrEmpty(Result))
            {
                return SOAP_FAULT;
            }
            else
            {
                Result = Result.Remove(Result.Length - 1);
            }

            return SOAP_OK;
        }

        public int AppendTable(int handle, ref _RT_TABLE table)
        {
            return RTb_append_table(handle, ref table);
        }

        public int InsertBasePoint(int handle, string tag, int type, int tableid, int use_ms, ref int point_id)
        {
            return RTb_insert_base_point(handle, tag, type, tableid, use_ms, ref point_id);
        }

        public int Search(int handle, string tagmask, string tablemask, string source, string unit, string desc, string instrument, int mode, int[] ids, ref int count)
        {
            return RTb_search(handle, tagmask, tablemask, source, unit, desc, instrument, mode, ids, ref count);
        }

        public int GetPointsProperty(int handle, int count, _RT_POINT[] bas, _RT_SCAN_POINT[] scan, _RT_CALC_POINT[] calc, uint[] errors)
        {
            return RTb_get_points_property(handle, count, bas, scan, calc, errors);
        }

        public int GetSnapshots(int handle, ref int count, int[] ids, int[] datetimes, int[] ms, double[] values, Int64[] states, short[] qualities, uint[] errors)
        {
            return RTs_get_snapshots(handle, ref count, ids, datetimes, ms, values, states, qualities, errors);
        }


        public int PutSnapshots(int handle, ref int count, int[] ids, int[] datetimes, int[] ms, double[] values, Int64[] states, short[] qualities, uint[] errors)
        {
            return RTs_put_snapshots(handle, ref count, ids, datetimes, ms, values, states, qualities, errors);
        }

        public string FormatMessage(uint ecode, byte[] message, byte[] name, long size)
        {
            RT_format_message(ecode, message, name, size);
            return Encoding.Default.GetString(message);
        }



        public int GetPropertys(int[] iv, ref string Result)
        {
            int handle = 0;
            int tagcount = iv.Length;
            if (0 != Connect(ref handle))
            {
                return SOAP_FAULT;
            }

            _RT_POINT[] bas = new _RT_POINT[tagcount];
            _RT_SCAN_POINT[] scan = new _RT_SCAN_POINT[tagcount];
            _RT_CALC_POINT[] calc = new _RT_CALC_POINT[tagcount];
            uint[] errors = new uint[tagcount];
            for (int i = 0; i < tagcount; i++)
            {
                bas[i].id = iv[i];
                scan[i].id = iv[i];
                calc[i].id = iv[i];
            }

            int _result = RTb_get_points_property(handle, tagcount, bas, scan, calc, errors);
            if (0 != _result)
            {
                return SOAP_FAULT;
            }

            for (int i = 0; i < tagcount; i++)
            {
                //Result += Encoding.Default.GetString(bas[i].tag).Replace('\0', ' ').Trim() + "," + Encoding.Default.GetString(bas[i].desc).Replace('\n', ' ').Replace('\0', ' ').Replace('', ' ').Trim() + "," + bas[i].highlimit.ToString() + "," + bas[i].lowlimit.ToString() + "," + bas[i].table.ToString() + "," + bas[i].typical.ToString() + "," + (int)bas[i].type + "," + Encoding.Default.GetString(bas[i].unit).Replace('\0', ' ').Trim() + "|";
            }
            Result = Result.Remove(Result.Length - 1);

            return SOAP_OK;
        }
        /// <summary>
        /// 在表中搜索符合条件的测点，使用测点名称时支持通配符
        /// </summary>
        /// <param name="tag">标签点名称，支持“*”和“?”通配符，如：DEMO_AI004/DEMO_AI00?/*</param>
        /// <param name="tab">所属表，支持“*”和“?”通配符</param>
        /// <param name="Result"></param>
        /// <returns></returns>
        public int SearchPoints(string tag, string tab, ref string Result)
        {
            int handle = 0;
            const int MAXPOINT = 200000;
            int[] pts = new int[MAXPOINT];
            int count = 0;

            if (0 != Connect(ref handle))
            {
                return SOAP_FAULT;
            }

            count = MAXPOINT;

            if (0 == RTb_search(handle, tag, tab, null, null, null, null, (int)SEARCHMODE.SORTBYPT, pts, ref count))
            {
                for (long i = 0; i < count; i++)
                {
                    Result += pts[i].ToString() + "|";
                }
                if (string.IsNullOrEmpty(Result))
                {
                    return SOAP_FAULT;
                }
                else
                {
                    Result = Result.Remove(Result.Length - 1);
                }
            }
            else
            {
                return SOAP_FAULT;
            }

            return SOAP_OK;
        }
        /// <summary>
        /// 根据测点名称得到测点的实时数据
        /// </summary>
        /// <param name="str">测点名称，如：Demo.DEMO_AI004，多个以，分隔</param>
        /// <param name="r"></param>
        /// <returns></returns>
        public int SnapsHotByName(string[] str, ref string r)
        {
            int tagcount = str.Length;
            if (tagcount <= 0)
            {
                return SOAP_FAULT;
            }

            int[] pts = new int[tagcount];
            int[] types = new int[tagcount];
            int[] classof = new int[tagcount];
            short[] use_ms = new short[tagcount];


            if (0 != GetPtsByName(str, ref pts, types, tagcount, classof, use_ms))
            {
                return SOAP_FAULT;
            }

            if (0 != GetSnapshots(tagcount, pts, ref r))
            {
                return SOAP_FAULT;
            }

            return SOAP_OK;
        }

        //返回实时值
        public static double GetRealTimeValue(string tagName, int handle)
        {
            double value = 0;
            int errMsg = 0;
            int count =1;
            string[] tagNameList = new string[1];
            tagNameList[0] = tagName;
            int[] ids = new int[1];
            int[] types = new int[1];
            int[] classof = new int[1];
            short[] use_ms = new short[1];
            errMsg = RTb_find_points(handle, ref count, tagNameList, ids, types, classof, use_ms);
            int[] datetimes = new int[1];
            int[] ms = new int[1];
            double[] values = new double[1];
            Int64[] states=new long[1];
            short[] qualities=new short[1];
            uint[] errors = new uint[1];
            errMsg = RTs_get_snapshots(handle, ref count, ids, datetimes, ms, values, states, qualities, errors);
            if (types[0] == 9 || types[0] == 10 || types[0] == 11)
            {
                value = values[0];
            }
            else
            {
                value = Convert.ToDouble(states[0]);
            }
            return value;
        }

        //返回实时值数组
        public static double[] GetRealTimeValues(int count, string[] tagNameList, int handle)
        {
            int errMsg = 0;
            int[] ids = new int[count];
            int[] types = new int[count];
            int[] classof = new int[count];
            short[] use_ms = new short[count];
            errMsg = RTb_find_points(handle, ref count, tagNameList, ids, types, classof, use_ms);
            int[] datetimes = new int[count];
            int[] ms = new int[count];
            double[] values = new double[count];
            Int64[] states = new long[count];
            short[] qualities = new short[count];
            uint[] errors = new uint[count];
            errMsg = RTs_get_snapshots(handle, ref count, ids, datetimes, ms, values, states, qualities, errors);

            return values;
        }

        //返回历史值
        public static double GetHisValue(string tagName, string hisTime, int handle)
        {
            double value = 0;
            long state = 0;
            int errMsg = 0;
            short qual = 0;
            int count = 1;
            int datetime = 0;
            short ms = 0;
            RT_parse_time(hisTime, datetime, ms);
            string[] tagNameList = new string[1];
            tagNameList[0] = tagName;
            int[] ids = new int[1];
            int[] types = new int[1];
            int[] classof = new int[1];
            short[] use_ms = new short[1];
            errMsg = RTb_find_points(handle, ref count, tagNameList, ids, types, classof, use_ms);
            int[] datetimes = new int[1];
            int[] mss = new int[1];
            datetimes[0] = datetime;
            mss[0] = ms;
            int mode = 1;
            int id = ids[0];
            //mode的含义
            //RT_NEXT,            ///寻找下一个最近的数据；
            //RT_PREVIOUS,        ///寻找上一个最近的数据；
            //RT_EXACT,           ///取指定时间的数据，如果没有则返回错误 RTE_DATA_NOT_FOUND；
            //RT_INTER,           ///取指定时间的内插值数据。
            errMsg = RTh_get_single_value(handle, id, mode, ref datetime, ref mss[0], ref value, ref state, ref qual);
            if (types[0] == 9 || types[0] == 10 || types[0] == 11)
            {
            }
            else
            {
                value = Convert.ToDouble(state);
            }
            return value;
        }

        public static int SetHisValue(ref string tagName, ref string hisTime, ref object val, int handle)
        {
            int errMsg = 0;
            double value = 0;
            long state = 0;
            short qual = 0;
            int count = 1;
            int datetime = 0;
            short ms = 0;
            RT_parse_time(hisTime, datetime, ms);
            string[] tagNameList = new string[1];
            tagNameList[0] = tagName;
            int[] ids = new int[1];
            int[] types = new int[1];
            int[] classof = new int[1];
            short[] use_ms = new short[1];
            errMsg = RTb_find_points(handle, ref count, tagNameList, ids, types, classof, use_ms);
            int[] datetimes = new int[1];
            int[] mss = new int[1];
            datetimes[0] = datetime;
            mss[0] = ms;
            int id = ids[0];
            value = Convert.ToDouble(val);
            errMsg = RTh_update_value(handle, id, datetime, ms, value, state, qual);
            return errMsg;
        }

        private long GetPtsByName(string[] table_dot_tags, ref int[] ids, int[] types, int count, int[] classof, short[] use_ms)
        {
            int ok = 0;
            int handle = 0;

            int status = Connect(ref handle);
            if (ok != status)
            {
                return status;
            }

            int j;
            int trycount = 3;
            for (j = 0; j < trycount; j++)
            {
                if (0x00000000 == RTb_find_points(handle, ref count, table_dot_tags, ids, types, classof, use_ms))
                {
                    break;
                }
            }
            if (trycount == j)
            {
                return 0xffffffff;//未知错误
            }

            return ok;
        }
        /// <summary>
        /// 根据测点ID得到测点的实时数据
        /// </summary>
        /// <param name="iv">测点ID，多个以，分隔</param>
        /// <param name="r"></param>
        /// <returns></returns>
        public int SnapsHotById(int[] iv, ref string r)
        {
            int tagcount = iv.Length;
            if (tagcount <= 0)
            {
                return SOAP_FAULT;
            }
            return GetSnapshots(tagcount, iv, ref r);
        }

        private int GetSnapshots(int count, int[] ids, ref string Result)
        {
            int ok = 0;
            int handle = 0;
            Result = string.Empty;

            int status = Connect(ref handle);
            if (ok != status)
            {
                return status;
            }

            uint[] errors = new uint[count];
            int[] datetimes = new int[count];
            double[] values = new double[count];
            long[] states = new long[count];
            int[] ms = new int[count];
            short[] qualities = new short[count];

            status = RTs_get_snapshots(handle, ref count, ids, datetimes, ms, values, states, qualities, errors);
            if (ok != status)
            {
                return status;
            }
            for (long i = 0; i < count; i++)
            {
                if (0 != errors[i])
                {
                    string description = string.Empty;
                    string name = string.Empty;
                    //RT_format_message(errors[i], ref description, ref name, 50);
                    Result += states[i].ToString() + "," + datetimes[i].ToString();
                }
                else
                {
                    Result += values[i].ToString() + "," + states[i].ToString() + "," + datetimes[i].ToString();
                }
                Result += "|";
            }
            if (string.IsNullOrEmpty(Result))
            {
                return SOAP_FAULT;
            }
            else
            {
                Result = Result.Remove(Result.Length - 1);
            }

            return ok;
        }

        private int Connect(ref int handle)
        {
            int serverPort = 6327;
            string ipaddr = "127.0.0.1";
            //string ipaddr = "192.168.17.198";

            int status = 0;
            int trys = 0;
            int trycount = 3;
            int times = 0;

            while (trys++ < trycount)
            {
                status = ConnectToSR(ipaddr, serverPort, ref handle);
                if (status == times)
                {
                    int priv = 0;
                    string user = "sa";

                    string psw = "smartreal";
                    status = LoginSR(handle, user, psw, ref priv);
                    return status;
                }
                Thread.Sleep(10);
            }
            return status;
        }

    }
}
