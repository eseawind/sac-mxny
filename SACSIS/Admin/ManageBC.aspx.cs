using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Admin_ManageBC : System.Web.UI.Page
{
    BLL.BLLRole bz = new BLLRole();
    IList<Hashtable> list = null;
    object obj = null;
    int count = 0;
    string errMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request["param"];
        if (param != "")
        {
            if (param == "seachList")
            {
                GetBCList();
            }
            else if (param == "Add")
            {
                string shiftid = Request.Form["shiftid"];
                string shiftname = Request.Form["shiftname"];
                string St = Request.Form["St"];
                string Et = Request.Form["Et"];
                btnSure_Click("", shiftid, shiftname, St, Et, "");
            }
            else if (param == "Edit")
            {
                string id = Request["id"].ToString();
                string shiftid = Request["shiftid"].ToString();
                string shiftname = Request["shiftname"].ToString();
                string St = Request["St"].ToString();
                string Et = Request["Et"].ToString();
                string Oshift = Request["Oshift"].ToString();
                btnSure_Click(id, shiftid, shiftname,St, Et, Oshift);
            }
            else if (param == "Remove")
            {
                string id = Request["id"].ToString();
                gridBC_RowDeleting(id);
            }
        }
        if (!IsPostBack)
        {
            //GridBC.DataSource = bz.RetAllBC(out errMsg);
            //GridBC.DataBind();
            //GetBCList();
        }
    }
    protected void btnSure_Click(string BcId, string BcMs, string BcNM, string ST, string ET, string OBcMs)
    {
        //string BcId = this.tbBcId.Text.Trim();//this.tbRootName.Text.Trim();
        //string BcMs = this.tbBcMs.Text.Trim();
        //string ST = this.tbStartTime.Text.Trim();
        //string ET = this.tbEndTime.Text.Trim();
        //string OBcMs = this.lbBcMs.Text;
        string obzid = "" ;
        if (BcId == "")
        {
            obzid = "New";
        }
        string message = "";
        //this.lbBcId.Text = "";
        //this.tbBcId.Text = "";
        //this.tbBcMs.Text = "";
        //this.tbStartTime.Text = "";
        //this.tbEndTime.Text = "";
        try
        {
            if (BcMs == "" || BcNM == "" || ST == "" || ET == "")
            {
                message = "班次信息不能为空！";
            }
            else
            {
                if (obzid == "New")
                {
                    if (bz.SaveBC(BcMs,BcNM, ST, ET, out errMsg))
                    {
                        //GridBC.DataSource = bz.RetAllBC(out errMsg);
                        //GridBC.DataBind();
                        message = "添加班次成功！";
                    }
                    else
                    {
                        message = "添加班次失败！请检查是否存在相同的班组编号！";
                    }
                }
                else
                {
                    if (bz.UpDateBC(OBcMs, BcMs,BcNM, ST, ET, out errMsg))
                    {
                        //GridBC.DataSource = bz.RetAllBC(out errMsg);
                        //GridBC.DataBind();
                        message = "修改班次成功";
                    }
                    else
                    {
                        message = "修改班次失败！请检查是否存在相同的班组编号！";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            message = "添加班次失败！失败信息：" + ex.Message.ToString();
        }

        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }

    protected void gridBC_RowDeleting(string BcId)
    {
        //string BcId = GridBC.DataKeys[e.RowIndex].Value.ToString();
        int OBcId = int.Parse(BcId);
        string message = "";
        if (bz.DeleteBC(OBcId, out errMsg))
        {
            //GridBC.DataSource = bz.RetAllBC(out errMsg);
            //GridBC.DataBind();
            message = "删除班次成功";
        }
        else
        {
            message = "修改班次失败！请检查是否存在相同的班次名！";
        }
        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();
    }

    #region 所有班次信息
    public void GetBCList()
    {
        int page = Convert.ToInt32(Request["page"].ToString());
        int rows = Convert.ToInt32(Request["rows"].ToString());
        DataTable dt = bz.GetBCmenu((page - 1) * rows + 1, page * rows);
        count = bz.GetBCCount();
        IList<Hashtable> list = new List<Hashtable>();

        foreach (DataRow item in dt.Rows)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ID_KEY", item["ID_KEY"]);
            ht.Add("T_SHIFTID", item["T_SHIFTID"].ToString());
            ht.Add("T_SHIFTNAME", item["T_SHIFTNAME"].ToString());
            ht.Add("D_STARTTIME", item["D_STARTTIME"].ToString().Substring(9));
            ht.Add("D_ENDTIME", item["D_ENDTIME"].ToString().Substring(9));
            list.Add(ht);
        }
        object obj = new
        {
            total = count,
            rows = list
        };

        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();
    }
    #endregion
}
