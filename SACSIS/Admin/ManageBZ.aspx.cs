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

public partial class Admin_ManageBZ : System.Web.UI.Page
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
                GetBZList();
            }
            else if (param == "Add")
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                btnSure_Click(id,name,"");
            }
            else if (param == "Edit")
            {
                string id = Request["id"].ToString();
                string name = Request["name"].ToString();
                string oid = Request["oid"].ToString();
                btnSure_Click(id, name, oid);
            }
            else if (param == "Remove")
            {
                string id = Request["id"].ToString();
                gridBZ_RowDeleting(id);
            }
        }
        if (!IsPostBack)
        {

        }
    }
    protected void btnSure_Click(string BzId,string BzMs,string sobzid)
    {
        //string BzId = this.tbBzId.Text.Trim();//this.tbRootName.Text.Trim();
        //string BzMs = this.tbBzMs.Text.Trim();
        //string sobzid = this.lbBzId.Text;
        string message = "";
        //this.lbBzId.Text = "";
        //this.tbBzId.Text = "";
        //this.tbBzMs.Text = "";
        try
        {
            if (sobzid == "")
            {
                if (bz.SaveBZ(BzId, BzMs, out errMsg))
                {
                    //GridBZ.DataSource = bz.RetAllBZ(out errMsg);
                    //GridBZ.DataBind();
                    message = "添加职别成功！";
                }
                else
                {
                    message = "添加职别失败！请检查是否存在相同的职别编号！";
                }
            }
            else
            {
                if (bz.UpDateBZ(sobzid, BzId, BzMs, out errMsg))
                {
                    //GridBZ.DataSource = bz.RetAllBZ(out errMsg);
                    //GridBZ.DataBind();
                    message = "修改职别成功";
                }
                else
                {
                    message = "修改职别失败！请检查是否存在相同的职别编号！";
                }
            }
        }
        catch (Exception ex)
        {
            message = "添加职别失败！失败信息：" + ex.Message.ToString();
        }

        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }

    protected void gridBZ_RowDeleting(string SBzId)
    {
        //string SBzId = GridBZ.DataKeys[e.RowIndex].Value.ToString();
        //int BzId = int.Parse(SBzId);
        string message = "";
        if (bz.DeleteBZ(SBzId, out errMsg))
        {
            //GridBZ.DataSource = bz.RetAllBZ(out errMsg);
            //GridBZ.DataBind();
            message = "删除职别成功";
        }
        else
        {
            message = "修改职别失败！请检查是否存在相同的职别名！";
        }
        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();
    }

    #region 所有职别（班组）信息
    public void GetBZList()
    {
        int page = Convert.ToInt32(Request["page"].ToString());
        int rows = Convert.ToInt32(Request["rows"].ToString());
        DataTable dt = bz.GetBZmenu((page - 1) * rows + 1, page * rows);
        count = bz.GetBZCount();
        IList<Hashtable> list = new List<Hashtable>();

        foreach (DataRow item in dt.Rows)
        {
            Hashtable ht = new Hashtable();
            ht.Add("T_CLASSID", item["T_CLASSID"].ToString());
            ht.Add("T_CLASSNAME", item["T_CLASSNAME"].ToString());
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
