<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPara.aspx.cs" Inherits="SACSIS.Report.EditPara" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改参数录入界面</title>
    <style type="text/css">
        body
        {
            height: 100%;
            overflow: auto;
            margin: 0px;
            padding: 0px;
            background-color: #dbeaf9;
			text-align:center;
        }
		.table_title
		{
			color:#000000;
			font-size: 14px;
            font: "宋体";
			background-image:url(img/table_bg_title.jpg);
			background-repeat:repeat-x;
			height:24px;
			line-height:24px;
			text-align:center;
		}
		.td_1
		{
			color:#000000;
			font-size: 14px;
            font: "宋体";
			background-color:#FFFFFF;
			height:24px;
			line-height:24px;
			text-align:center;
		}
		.td_2
		{
			color:#000000;
			font-size: 14px;
            font: "宋体";
			background-color:#f7f6ff;
			height:24px;
			line-height:24px;
			text-align:center;
		}
</style>
    <%--时间选择控件的脚本引入--%>
    <script language="javascript" type="text/javascript" src="../Js/My97DatePicker/WdatePicker.js"></script>    
    
    <script type="text/jscript">

        var rowCount = 1;

        function addRow() {
            rowNode = document.getElementById("TableRow1");

            newNode = rowNode.cloneNode(true);
            setNames(newNode, rowCount);
            rowNode.parentNode.appendChild(newNode);
            ++rowCount;
        }

        function delRow() {
            if (rowCount == 1) {
                return;
            }

            tableNode = document.getElementById("Table1");

            oldNode = tableNode.lastChild.lastChild;
            tableNode.lastChild.removeChild(oldNode);
            --rowCount;
        }

        function setNames(newNode, index) {
            nodes = newNode.childNodes;
            for (var i = 0; i < nodes.length; ++i) {
                node = nodes.item(i).childNodes.item(0);
                oldID = node.getAttribute("name");
                node.setAttribute("name", oldID + rowCount);
            }
        }
    </script>
</head>
<body>
    <div><img src="../img/title_editpara.jpg" width="100%" height="80px" alt="" /></div>
    <form id="form1" method="post" runat="server">
    <div id="div1">
        <asp:Table ID="Table1" runat="server" width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#0f5ba7">
        <asp:TableHeaderRow><asp:TableHeaderCell CssClass="table_title">参数ID</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">统计时间</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">统计级别</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">统计序号</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">统计值</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">统计值状态</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">相关计算触发</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="table_title">参数修改时间</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow ID="TableRow1"><asp:TableCell CssClass="td_1">
                          <asp:DropDownList ID="ParaID" name="ParaID" runat="server"></asp:DropDownList>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_2">
                          <input type="text" id="StatTime" name="StatTime" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_1">
                          <asp:DropDownList ID="StatLevel" name="StatLevel" runat="server">
                          <asp:ListItem>小时</asp:ListItem>
                          <asp:ListItem>班值</asp:ListItem>
                          <asp:ListItem>天</asp:ListItem>
                          </asp:DropDownList>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_2">
                          <asp:DropDownList ID="StatSn" name="StatSn" runat="server">
                          <asp:ListItem>早班</asp:ListItem>
                          <asp:ListItem>中班</asp:ListItem>
                          <asp:ListItem>晚班</asp:ListItem>
                          </asp:DropDownList>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_1">
                          <asp:TextBox ID="StatValue" name="StatValue" runat="server"></asp:TextBox>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_2">
                          <asp:DropDownList ID="ParaState" name="ParaState" runat="server">
                          <asp:ListItem>OK状态</asp:ListItem>
                          <asp:ListItem>空值状态</asp:ListItem>                          
                          </asp:DropDownList>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_1">
                          <asp:DropDownList ID="IsRelativeCal" name="IsRelativeCal" runat="server">
                          <asp:ListItem>是</asp:ListItem>
                          <asp:ListItem>否</asp:ListItem>                          
                          </asp:DropDownList>
                      </asp:TableCell>
                      <asp:TableCell CssClass="td_2">
                          <input type="text" id="EditCalTime" name="EditCalTime" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                      </asp:TableCell></asp:TableRow>
        </asp:Table>        
    </div>
    <br />
    <div style="float:left;">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:addRow()">＋</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="javascript:delRow()">－</asp:HyperLink></div>
    <div>
        <asp:Button ID="BtnSubmit" runat="server" Text="提交" onclick="BtnSubmit_Click" /></div>
    </form>
</body>
</html>
