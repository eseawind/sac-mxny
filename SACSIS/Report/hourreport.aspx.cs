using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

namespace SACSIS.Report
{
    public partial class hourreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                /////URL格式: http://.....?dcname=gl&reportpath=/Adventure Works/hourreport
                string dcname = Request.QueryString["DCName"].ToString();
                string reportpath = Request.QueryString["ReportPath"].ToString();


                //string dcname = "gl";
                //string reportpath = "/xsbmb/xsb001";

                INI cfg = new INI(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
                this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(cfg.GetVal("Advanced", "ReportServer"));

                this.ReportViewer1.ServerReport.ReportPath = @reportpath;
                ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials();

                //txtData.Value = DateTime.Now.ToString("yyyy-MM-dd");
                //SetParamet(txtData.Value, dcname);
                SetParamet(dcname);

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string dcname = Request.QueryString["DCName"].ToString();
            //SetParamet(txtData.Value, dcname);
        }
        private void SetParamet(string dcname)
        {
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("dcname", dcname);
            this.ReportViewer1.ServerReport.SetParameters(parameters);
        }

        private void SetParamet(string timestring, string dcname)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("timestring", timestring);
            parameters[1] = new ReportParameter("dcname", dcname);
            this.ReportViewer1.ServerReport.SetParameters(parameters);
        }
    }
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {

        // local variable for network credential.
        private string _UserName;
        private string _PassWord;
        private string _DomainName;
        public CustomReportCredentials()
        {
            INI cfg = new INI(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini");
            _UserName = cfg.GetVal("Advanced", "ReportUser");
            _PassWord = cfg.GetVal("Advanced", "ReportPassWord");
            _DomainName = cfg.GetVal("Advanced", "ReportDomain");
        }
        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;  // not use ImpersonationUser
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {

                // use NetworkCredentials
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {

            // not use FormsCredentials unless you have implements a custom autentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }

    }
    class INI
    {
        #region
        //读取键的整型值
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileIntW", CharSet = CharSet.Unicode)]
        private static extern int getKeyIntValue(string section, string Key, int nDefault, string filename);

        //读取字符串键值
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern int getKeyValue(string section, string key, int lpDefault, [MarshalAs(UnmanagedType.LPWStr)] string szValue, int nlen, string filename);

        //写字符串键值
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern bool setKeyValue(string section, string key, string szValue, string filename);
        #endregion

        private string m_Path = null;		//ini文件路径

        /// ini文件路径
        public string Path
        {
            set { m_Path = value; }
            get { return m_Path; }
        }

        public INI(string szPath)
        {
            m_Path = szPath;
        }

        /// 读整型键值
        public int GetInt(string section, string key)
        {
            return getKeyIntValue(section, key, -1, m_Path);
        }

        /// 读字符串键值
        public string GetVal(string section, string key)
        {
            string szBuffer = new string('0', 256);
            int nlen = getKeyValue(section, key, 0, szBuffer, 256, m_Path);
            return szBuffer.Substring(0, nlen);
        }

        /// 写整型键值
        public bool SetInt(string section, string key, int dwValue)
        {
            return setKeyValue(section, key, dwValue.ToString(), m_Path);
        }

        /// 写字符串键值
        public bool SetVal(string section, string key, string szValue)
        {
            return setKeyValue(section, key, szValue, m_Path);
        }
    }
}