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

public partial class Admin_ManagePB : System.Web.UI.Page
{
    BLL.BLLRole bz = new BLLRole();

    string errMsg = "";

    int R = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request["param"];
        if (param != "")
        {
            if (param == "AddRow")
            {

                btnAddRow_Click(sender, e);
            }
            else if (param == "Add")
            {

            }
        }
        if (!IsPostBack)
        {

        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowGridView(sender, e);
        }
        if (ViewState["dt"] == null)
        {
            ViewState["dt"] = GetGridViewData();
        }
    }

    //继承System.Web.UI.ITemplate接口，实现它
    public class GridViewTemplate : ITemplate
    {
        BLL.BLLRole BZ = new BLLRole();
        string errMsg = "";
        private string templateType;
        private string columnName;
        private string cId;

        public GridViewTemplate(string type, string colname, string controlId)
        {
            templateType = type;
            columnName = colname;
            cId = controlId;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            // 根据类型创建模版列表头、正文或脚注
            switch (templateType)
            {
                case "HeaderTemplate":
                    Literal myHeadLiteral = new Literal();
                    myHeadLiteral.ID = cId;
                    myHeadLiteral.Text = columnName.ToString();
                    container.Controls.Add(myHeadLiteral);
                    break;
                case "ItemTemplateRowNum":
                    Label myRowNum = new Label();
                    myRowNum.ID = "rn" + cId;
                    myRowNum.Text = cId.ToString();
                    myRowNum.Width = 20;
                    container.Controls.Add(myRowNum);
                    break;
                case "ItemTemplateTime":
                    TextBox myTime = new TextBox();
                    myTime.ID = "cal" + cId;
                    myTime.Attributes.Add("onclick", "WdatePicker({dateFmt:'yyyy-MM-dd'})");
                    myTime.Attributes.Add("style","text-align:center");
                    myTime.Width = 100;
                    DateTime dt = System.DateTime.Now;
                    myTime.Text = dt.ToString("yyyy-MM-dd");
                    container.Controls.Add(myTime);
                    break;
                case "ItemTemplate":
                    DropDownList myDropDownList = new DropDownList();
                    myDropDownList.ID = "ddl" + cId;
                    myDropDownList.DataBinding += new EventHandler(ddl_DataBinding);
                    myDropDownList.Attributes.Add("style", "text-align:center");
                    myDropDownList.Width = 70;
                    container.Controls.Add(myDropDownList);
                    break;
                default:
                    break;
            }
        }

        //为动态产生的控件绑定值
        void cal_DataBinding(object sender, EventArgs e)
        {
            Calendar cal = sender as Calendar;
            GridViewRow gr = cal.NamingContainer as GridViewRow;
        }

        void ddl_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewRow gr = ddl.NamingContainer as GridViewRow;
            ddl.AppendDataBoundItems = true;
            ArrayList BZlist = BZ.BZlist(out errMsg);
            string[] bzms = (string[])BZlist[1];
            if (bzms != null)
            {
                for (int i = 0; i < bzms.Length; i++)
                {
                    if (bzms[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        ddl.Items.Add(new ListItem(bzms[i]));
                    }
                }
            }
        }

    }

    System.Collections.ICollection CreateDataSource()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dr = dt.NewRow();
        dt.Rows.Add(dr);
        DataView dv = new DataView(dt);
        return dv;
    }

    //动态生成GridView
    protected void ShowGridView(object sender, EventArgs e)
    {
        GridPB.AutoGenerateColumns = false;
        //GridPB.DataSource = CreateDataSource();//数据源是DataView
        if (ViewState["dt"] == null)
        {
            GridPB.DataSource = GetGridViewData();//数据源是DataTable
            ViewState["dt"] = GetGridViewData();
        }
        else
        {
            DataTable dt = (DataTable)ViewState["dt"];
            GridPB.DataSource = dt;
        }

        ArrayList BClist = bz.BClist(out errMsg);
        int[] bckey = (int[])BClist[0];
        string[] bcid = (string[])BClist[1];
        string[] bcms = (string[])BClist[2];
        string[] st = (string[])BClist[3];
        string[] et = (string[])BClist[4];

        TemplateField rf = new TemplateField();
        rf.HeaderTemplate = new GridViewTemplate("HeaderTemplate", "", "r");
        rf.ItemTemplate = new GridViewTemplate("ItemTemplateRowNum", "行号", R.ToString());
        this.GridPB.Columns.Add(rf);

        TemplateField ttf = new TemplateField();
        ttf.HeaderTemplate = new GridViewTemplate("HeaderTemplate", "日期", "t");
        ttf.ItemTemplate = new GridViewTemplate("ItemTemplateTime", "日期", "time");
        this.GridPB.Columns.Add(ttf);

        if (bcms != null)
        {
            for (int i = 0; i < bcms.Length; i++)
            {
                if (bcms[i] == null)
                {
                    break;
                }
                else
                {
                    TemplateField tf = new TemplateField();
                    tf.HeaderTemplate = new GridViewTemplate("HeaderTemplate", bcms[i], "控件ID" + i.ToString());
                    tf.ItemTemplate = new GridViewTemplate("ItemTemplate", "班次", "BC" + i.ToString());
                    this.GridPB.Columns.Add(tf);
                }
            }
        }

        GridPB.DataBind();


    }

    //首次生成GridView的数据源DataTable
    private DataTable GetGridViewData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Time", typeof(String));
        ArrayList BClist = bz.BClist(out errMsg);
        string[] bcms = (string[])BClist[1];
        if (bcms != null)
        {
            for (int i = 1; i <= bcms.Length; i++)
            {
                if (bcms[i] == null)
                {
                    break;
                }
                else
                {
                    dt.Columns.Add("BC" + i, typeof(String));
                }
            }
        }
        DataRow dr = dt.NewRow();
        dt.Rows.Add(dr);
        return dt;
    }

    //排序
    protected void btnSure_Click(object sender, EventArgs e)
    {
        string message = "";
        bool obj = true;
        string bcval = "";
        string bcidhtml = "";
        DataTable dt = (DataTable)ViewState["dt"];
        int lhn = dt.Rows.Count;//获得gridview的数据行的行数，既一个排班轮回的天数
        int nlh = 3652 / lhn;//3652是10年总共的天数，nlh是十年总共的排班轮回数
        Session.Contents["t1"] = Request.Form["GridPB$ctl02$caltime"].ToString();
        string timestr = Session.Contents["t1"].ToString();//获取排班开始的时间
        DateTime timedt = Convert.ToDateTime(timestr);//将开始时间转换为datetime格式
        ArrayList BClist = bz.BClist(out errMsg);
        int[] bckey = (int[])BClist[0];
        string[] bcid = (string[])BClist[1];//班次编号
        string[] bcms = (string[])BClist[2];//班次描述
        string[] st = (string[])BClist[3];//起始时间
        string[] et = (string[])BClist[4];//结束时间
        int R = 0;
        if (bcms != null)
        {
            for (int i = 0; i < bcms.Length; i++)//遍历第j行的所有班次，取每个班次的值
            {
                if (bcms[i] == null)
                {
                    break;
                }
                else
                {
                    R++;
                }
            }
        }
        int bcn = R;//获得班次的数目
        int ktbc = 0;//跨天的班次
        bool iskt = false;
        for (int w = 0; w < bcn; w++)//循环bcn次，看是否存在某一班次跨两天的情况，如果有，标示出那一天
        {
            DateTime timeS = Convert.ToDateTime(st[w]);
            DateTime timeE = Convert.ToDateTime(et[w]);
            TimeSpan timejg = timeE - timeS;
            int kt = timejg.Hours;
            if (kt < 0)
            {
                ktbc = w + 1;
                iskt = true;
            }
        }
        bool iszb = true;
        if (ktbc == 1)
        {
            iszb = true;
        }
        else if (ktbc != 1)
        {
            iszb = false;
        }
        for (int j = 0; j < lhn; j++)//循环i次，遍历所有的行
        {
            int l = j + 2;
            if (bcms != null)
            {
                for (int k = 0; k < bcms.Length; k++)//遍历第j行的所有班次，取每个班次的值
                {
                    if (bcms[k] == null)
                    {
                        break;
                    }
                    else
                    {
                        if (l < 10)
                        {
                            bcval = "bcR" + l + "C" + k;
                            bcidhtml = "GridPB$ctl0" + l + "$ddlBC" + k;
                        }
                        else if (l > 9)
                        {
                            bcval = "bcR" + l + "C" + k;
                            bcidhtml = "GridPB$ctl" + l + "$ddlBC" + k;
                        }
                        Session.Contents[bcval] = Request.Form[bcidhtml].ToString();
                    }
                }
            }
        }
        string[] bzms = new string[10000];
        string[] bzid = new string[10000];
        int rr = 0;
        string cuid = "";
        for (int u = 0; u < lhn; u++)
        {
            for (int v = 0; v < bcn; v++)
            {
                rr = u + 2;
                cuid = "bcR" + rr + "C" + v;
                string strid = u.ToString() + v.ToString();
                int inid = Convert.ToInt32(strid);
                bzms[inid] = Session.Contents[cuid].ToString();
                bzid[inid] = bz.BZIDbyBZMS(bzms[inid], out errMsg);
            }
        }
        bool Empty = bz.EmptyPB(out errMsg);//清空数据库中的排班表
        double dayadd = 0;
        string SecondS = "";
        string SecondE = "";
        string STTSD = "";
        string ETTSD = "";
        string BCID = "";
        int rrr = 0;
        string stridd = "";
        int inidd = 0;
        string sqldb2 = "";
        string sqlsql = "";
        for (int x = 0; x < nlh; x++)//要存nlh个轮回
        {
            for (int y = 0; y < lhn; y++)//遍历一个轮回中的lhn天
            {
                string day = timedt.AddDays(dayadd).ToString("yyyy-MM-dd");
                string dayZ = timedt.AddDays(dayadd - 1).ToString("yyyy-MM-dd");
                string dayW = timedt.AddDays(dayadd + 1).ToString("yyyy-MM-dd");
                for (int z = 0; z < bcn; z++)//循环bcn次，向排班表中插入一天中的bcn个班次
                {
                    if (iskt == true)
                    {
                        if (iszb == true)
                        {
                            if (z == ktbc - 1)
                            {
                                SecondS = st[z].ToString();
                                SecondE = et[z].ToString();
                                STTSD = dayZ + " " + SecondS + ".000000";
                                ETTSD = day + " " + SecondE + ".000000";
                                BCID = bcid[z].ToString();
                                rrr = y + 2;
                                stridd = y.ToString() + z.ToString();
                                inidd = Convert.ToInt32(stridd);
                                sqldb2 = "INSERT INTO T_INFO_CLASSTIME (D_STARTTIME,D_ENDTIME,T_CLASSID,T_SHIFTID) VALUES ('" + STTSD + "','" + ETTSD + "','" + bzid[inidd] + "','" + BCID + "')";
                                sqlsql = "";
                                if (bz.InsertPB(sqldb2, sqlsql, out errMsg))
                                {
                                    obj = true;
                                }
                                else
                                {
                                    obj = false;
                                }
                            }
                            else
                            {
                                SecondS = st[z].ToString();
                                SecondE = et[z].ToString();
                                STTSD = day + " " + SecondS + ".000000";
                                ETTSD = day + " " + SecondE + ".000000";
                                BCID = bcid[z].ToString();
                                rrr = y + 2;
                                stridd = y.ToString() + z.ToString();
                                inidd = Convert.ToInt32(stridd);
                                sqldb2 = "INSERT INTO T_INFO_CLASSTIME (D_STARTTIME,D_ENDTIME,T_CLASSID,T_SHIFTID) VALUES ('" + STTSD + "','" + ETTSD + "','" + bzid[inidd] + "','" + BCID + "')";
                                sqlsql = "";
                                if (bz.InsertPB(sqldb2, sqlsql, out errMsg))
                                {
                                    obj = true;
                                }
                                else
                                {
                                    obj = false;
                                }
                            }
                        }
                        else if (iszb == false)
                        {
                            if (z == ktbc - 1)
                            {
                                SecondS = st[z].ToString();
                                SecondE = et[z].ToString();
                                STTSD = day + " " + SecondS + ".000000";
                                ETTSD = dayW + " " + SecondE + ".000000";
                                BCID = bcid[z].ToString();
                                rrr = y + 2;
                                stridd = y.ToString() + z.ToString();
                                inidd = Convert.ToInt32(stridd);
                                sqldb2 = "INSERT INTO T_INFO_CLASSTIME (D_STARTTIME,D_ENDTIME,T_CLASSID,T_SHIFTID) VALUES ('" + STTSD + "','" + ETTSD + "','" + bzid[inidd] + "','" + BCID + "')";
                                sqlsql = "";
                                if (bz.InsertPB(sqldb2, sqlsql, out errMsg))
                                {
                                    obj = true;
                                }
                                else
                                {
                                    obj = false;
                                }
                            }
                            else
                            {
                                SecondS = st[z].ToString();
                                SecondE = et[z].ToString();
                                STTSD = day + " " + SecondS + ".000000";
                                ETTSD = day + " " + SecondE + ".000000";
                                BCID = bcid[z].ToString();
                                rrr = y + 2;
                                stridd = y.ToString() + z.ToString();
                                inidd = Convert.ToInt32(stridd);
                                sqldb2 = "INSERT INTO T_INFO_CLASSTIME (D_STARTTIME,D_ENDTIME,T_CLASSID,T_SHIFTID) VALUES ('" + STTSD + "','" + ETTSD + "','" + bzid[inidd] + "','" + BCID + "')";
                                sqlsql = "";
                                if (bz.InsertPB(sqldb2, sqlsql, out errMsg))
                                {
                                    obj = true;
                                }
                                else
                                {
                                    obj = false;
                                }
                            }
                        }
                    }
                    else if (iskt == false)
                    {
                        SecondS = st[z].ToString();
                        SecondE = et[z].ToString();
                        STTSD = day + " " + SecondS + ".000000";
                        ETTSD = day + " " + SecondE + ".000000";
                        BCID = bcid[z].ToString();
                        rrr = y + 2;
                        stridd = y.ToString() + z.ToString();
                        inidd = Convert.ToInt32(stridd);
                        sqldb2 = "INSERT INTO T_INFO_CLASSTIME (D_STARTTIME,D_ENDTIME,T_CLASSID,T_SHIFTID) VALUES ('" + STTSD + "','" + ETTSD + "','" + bzid[inidd] + "','" + BCID + "')";
                        sqlsql = "";
                        if (bz.InsertPB(sqldb2, sqlsql, out errMsg))
                        {
                            obj = true;
                        }
                        else
                        {
                            obj = false;
                        }
                    }

                }
                dayadd++;
                if (obj == false)
                {
                    break;
                }
            }
            if (obj == false)
            {
                break;
            }
        }
        if (obj == true)
        {
            message = "排班表排序成功！";
        }
        else
        {
            message = "排班表排序失败！请检查排版设置！";
        }
        Response.Write("<script>alert('" + message + "')</script>");
        //Session.Clear();//清空session
        ShowGridView(sender, e);

    }

    protected void btnSure_Css(object sender, EventArgs e)
    {
        this.btnSure.Attributes.Add("onclick", "ShowWait()");
    }

    //取消
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState.Clear();
        Session.Clear();
        ShowGridView(sender, e);
    }

    //添加一行
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        string bcval = "";
        string bcidhtml = "";
        DataTable dt = (DataTable)ViewState["dt"];
        int i = dt.Rows.Count;
        Session.Contents["t1"] = Request.Form["GridPB$ctl02$caltime"].ToString();//取到轮回开始那天的日期值 
        for (int j = 0; j < i; j++)//循环i次，遍历所有的行
        {
            int l = j + 2;
            ArrayList BClist = bz.BClist(out errMsg);
            string[] bcms = (string[])BClist[1];
            if (bcms != null)
            {
                for (int k = 0; k < bcms.Length; k++)//遍历第j行的所有班次，取每个班次的值
                {
                    if (bcms[k] == null)
                    {
                        break;
                    }
                    else
                    {
                        if (l < 10)
                        {
                            bcval = "bcR" + l + "C" + k;
                            bcidhtml = "GridPB$ctl0" + l + "$ddlBC" + k;
                        }
                        else if (l > 9)
                        {
                            bcval = "bcR" + l + "C" + k;
                            bcidhtml = "GridPB$ctl" + l + "$ddlBC" + k;
                        }
                        Session.Contents[bcval] = Request.Form[bcidhtml].ToString();
                    }
                }
            }
        }
        DataRow newRow;
        newRow = dt.Rows[dt.Rows.Count - 1];
        //newRow["Time"] = t1;
        newRow = dt.NewRow();
        dt.Rows.Add(newRow);
        ViewState["dt"] = dt;
        //this.GridPB.DataSource = dt;
        //this.GridPB.DataBind();

        ShowGridView(sender, e);

        //TextBox x2 = (TextBox)this.FindControl(timeid);

    }

    //删除一行
    protected void btnDelRow_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt"];
        if (dt.Rows.Count < 2)
        {

        }
        else
        {
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            ViewState["dt"] = dt;
            //this.GridPB.DataSource = dt;
            //this.GridPB.DataBind();
        }
        ShowGridView(sender, e);
    }

    //gridview的RowDataBound事件，在表的每一行生成后往动态生成的控件里填值
    protected void GridView_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)//判断是否是数据行
        {
            string timeN = "t" + R;
            string rowNum = "";
            string timeid = "";
            string bcval = "";
            string bcidhtml = "";
            if (Session.Contents["t1"] != null)
            {
                int i = R + 1;
                int di = R - 1;
                if (i == 2)
                {
                    TextBox tb = (TextBox)this.FindControl("GridPB$ctl02$caltime");
                    tb.Text = Session.Contents["t1"].ToString();
                    Label lb = (Label)this.FindControl("GridPB$ctl02$rn1");
                    lb.Text = R.ToString();
                }
                else
                {
                    if (i < 10)
                    {
                        timeid = "GridPB$ctl0" + i + "$caltime";
                        rowNum = "GridPB$ctl0" + i + "$rn1";
                    }
                    else if (i > 9)
                    {
                        timeid = "GridPB$ctl" + i + "$caltime";
                        rowNum = "GridPB$ctl" + i + "$rn1";
                    }
                    Label lb = (Label)this.FindControl(rowNum);
                    int rnR = i - 1;
                    lb.Text = rnR.ToString();
                    TextBox tb = (TextBox)this.FindControl(timeid);
                    string timestr = Session.Contents["t1"].ToString();
                    DateTime timedt = Convert.ToDateTime(timestr);
                    double Rd = Convert.ToDouble(di);
                    string timestr2 = timedt.AddDays(Rd).ToString("yyyy-MM-dd");
                    tb.Text = timestr2;

                }
                ArrayList BClist = bz.BClist(out errMsg);
                string[] bcms = (string[])BClist[1];
                if (bcms != null)
                {
                    for (int k = 0; k < bcms.Length; k++)//遍历第R行的所有班次，为本行中的所有BC的dropdownlist赋值。
                    {
                        if (bcms[k] == null)
                        {
                            break;
                        }
                        else
                        {
                            if (i < 10)
                            {
                                bcval = "bcR" + i + "C" + k;
                                bcidhtml = "GridPB$ctl0" + i + "$ddlBC" + k;
                            }
                            else if (i > 9)
                            {
                                bcval = "bcR" + i + "C" + k;
                                bcidhtml = "GridPB$ctl" + i + "$ddlBC" + k;
                            }
                            DropDownList ddl = (DropDownList)this.FindControl(bcidhtml);
                            if (Session.Contents[bcval] == null)
                            {
                                ddl.SelectedValue = bcms[0];
                            }
                            else
                            {
                                ddl.SelectedValue = Session.Contents[bcval].ToString();
                            }
                        }
                    }
                }
            }
            R++;
        }
    }


    protected void GridPB_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

