using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;
using System.Collections;
using System.Text;
using System.Data;

namespace SACSIS.Report
{
    public partial class EditPara : System.Web.UI.Page
    {
        private string[] colNames = new string[] { "ParaID", "StatTime", "StatLevel", "StatSn", "StatValue", "ParaState", "IsRelativeCal", "EditCalTime" };
        private Hashtable hashtRow = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
        {
            ParaID.DataSource = PopDatasource().Tables[0].DefaultView;
            ParaID.DataTextField = "ParaID"; //dropdownlist的Text的字段
            ParaID.DataValueField = "ParaID";//dropdownlist的Value的字段
            ParaID.DataBind();

            StatValue.Text = "";
        }

        //临时函数，读取DB2中表的数据，作为数据源给前台空间用，如 DropdownList
        DataSet PopDatasource()
        {
            string strConn = "Provider=IBMDADB2;Data Source=SACSIS;UID=Administrator;PWD=1qazXSW@;";
            string strSql = "select PARAID from STATPARA";

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbCommand cmd = new OleDbCommand(strSql, conn);
                try
                {
                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    return ds;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //临时函数，执行数据插入
        protected void ExecuteSql(string strSql)
        {
            string strConn = "Provider=IBMDADB2;Data Source=SACSIS;UID=Administrator;PWD=1qazXSW@;";

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbCommand cmd = new OleDbCommand(strSql, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }

        //提交按钮对应函数
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int rowsCount = (this.Request.Form.Count - 3) / 8;

            StringBuilder msg = new StringBuilder();

            //确保统计时间不为空
            for (int i = 0; i < rowsCount; ++i)
            {
                if (Request.Form.Get(colNames[1] + (i == 0 ? "" : i.ToString())) == "")
                {
                    msg.AppendFormat("统计时间不能为空 \\nline:{0}", i);
                    Response.Write("<script> alert(\"" + msg.ToString() + "\")</script>");
                    return;
                }
            }

            for (int i = 0; i < rowsCount; ++i)
            {
                foreach (string col in colNames)
                {
                    hashtRow[col] = Request.Form.Get(col + (i == 0 ? "" : i.ToString()));
                }
                ExecuteSql(string.Format("insert into EDITPARA {0}", GetPartInsertSql()));
            }
            return;
        }

        //临时函数，数据插入SQL语句的列和对应数据
        private string GetPartInsertSql()
        {
            StringBuilder sbColNames = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            //数据修正
            switch (hashtRow["StatLevel"].ToString())
            {
                case "小时": hashtRow["StatLevel"] = "0"; hashtRow["StatSn"] = ""; break;
                case "班值": hashtRow["StatLevel"] = "1";
                    switch (hashtRow["StatSn"].ToString())
                    {
                        case "早班": hashtRow["StatSn"] = "1"; break;
                        case "中班": hashtRow["StatSn"] = "2"; break;
                        case "晚班": hashtRow["StatSn"] = "3"; break;
                        default: break;
                    }
                    break;
                case "天": hashtRow["StatLevel"] = "2"; hashtRow["StatSn"] = ""; break;
                default: break;
            }
            if (hashtRow["StatValue"].ToString() == "")
                hashtRow["ParaState"] = "0";
            else
                hashtRow["ParaState"] = "4";

            if (hashtRow["IsRelativeCal"].ToString() == "触发")
                hashtRow["IsRelativeCal"] = "1";
            else
                hashtRow["IsRelativeCal"] = "0";

            sbColNames.Append("(");
            sbValues.Append("values(");
            foreach (string col in colNames)
            {
                if (hashtRow[col].ToString() == "")
                    continue;

                sbColNames.AppendFormat("{0},", col);

                switch (col.ToLower())
                {
                    case "paraid":
                    case "stattime":
                    case "editcaltime":
                        sbValues.AppendFormat("'{0}',", hashtRow[col]);
                        break;
                    default:
                        sbValues.AppendFormat("{0},", hashtRow[col]);
                        break;
                }
            }
            sbColNames.Append("Flag)");
            sbValues.Append("0)");
            return string.Format("{0} {1}", sbColNames.ToString(), sbValues.ToString());
        }
    }
}