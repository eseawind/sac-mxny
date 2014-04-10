using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;

namespace SACSIS.Trend
{
    public partial class ComparaAnalysis : System.Web.UI.Page
    {
        string errMsg = "", rating = "";

        private IList<Hashtable> list = new List<Hashtable>();
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"];
            if ((rating != "") && (rating != null))
            {
                get_data();
            }
            if (!IsPostBack)
            {
                Sec_data_bind();
            }

        }

        private void Sec_data_bind()
        {
            BLL.BLLRealQuery BLQ = new BLL.BLLRealQuery();
            DataSet DS = BLQ.Get_Company_Info(out errMsg);
            if (DS != null && DS.Tables[0].Rows.Count > 0)
            {
                DataSet DSS = BLQ.Get_Electric_Info(DS.Tables[0].Rows[0]["T_COMPANYID"].ToString(), out errMsg);
                DataSet DDS = BLQ.Get_Unit_Info(DSS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
                this.sec_company.DataSource = DS.Tables[0].DefaultView;
                this.sec_company.DataTextField = "T_COMPANYDESC";
                this.sec_company.DataValueField = "T_COMPANYID";
                this.sec_company.DataBind();
                this.sec_electric.DataSource = DSS.Tables[0].DefaultView;
                this.sec_electric.DataTextField = "T_PLANTDESC";
                this.sec_electric.DataValueField = "T_PLANTID";
                this.sec_electric.DataBind();
                this.sec_crew.DataSource = DDS.Tables[0].DefaultView;
                this.sec_crew.DataTextField = "T_UNITDESC";
                this.sec_crew.DataValueField = "T_UNITID";
                this.sec_crew.DataBind();
            }
        }

        private void get_data()
        {
            //3200406;60,70,80,;2013-08-01 00:00:00,2013-08-13 08:39:46;q_fd,Eta_H;多项式 ,2;
            string rating_data = rating;
            string errMsg = "";
            string stime = rating_data.Split(';')[2].Split(',')[0], etime = rating_data.Split(';')[2].Split(',')[1];
            string[] per = new string[rating_data.Split(';')[1].TrimEnd(',').Split(',').Length];
            string[] para_id = new string[2];
            string hanshu = rating_data.Split(';')[4];
            for (int i = 0; i < rating_data.Split(';')[1].TrimEnd(',').Split(',').Length; i++)
            {
                per[i] = rating_data.Split(';')[1].TrimEnd(',').Split(',')[i];
            }
            for (int i = 0; i < 2; i++)
            {
                para_id[i] = rating_data.Split(';')[3].Split(',')[i];
            }
            string unit_id = rating_data.Split(';')[0];
            BLL.BLLComparaAnalysis BCA = new BLL.BLLComparaAnalysis();
            string[] gongshi = new string[per.Length];
            list = BCA.Get_Required_data(unit_id, para_id, per, hanshu, stime, etime, out gongshi, out errMsg);
            object obj = new
            {
                //max_data = max_data.TrimEnd(','),
                //min_data = min_data.TrimEnd(','),
                //str_para_id = para_id,
                gongshi = gongshi,
                title = "趋势呈现数据图",
                list = list
            };

            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            //Response.Write(str_append.TrimEnd('|'));
            Response.End();
        }
    }
}