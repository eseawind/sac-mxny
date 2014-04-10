<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePB.aspx.cs" Inherits="Admin_ManagePB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.01 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>排班设置</title>
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../Js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
    function $(id)        
        { 
            return (document.getElementById) ? document.getElementById(id) : document.all[id] ; 
        }
    function ShowWait() 
        { 
            //document.getElementById("__01").style.display="none";
            document.getElementById("divWait").style.display=""; 
        } 
//		function enable(){
//			$('a.easyui-linkbutton').linkbutton('enable');
//		}
//		function disable(){
//			$('a.easyui-linkbutton').linkbutton('disable');
//		}
//		function changetext(){
//			$('#add').linkbutton({text:'new add'});
//		}
//		
////$(document).ready(function(){
//////		function AddRow(){
//////		    $.post("ManagePB.aspx", { param: 'AddRow'}, function (data) {
//////		    
//////            }, 'json');
//////		
//////		}
////});    
//		function AddRow(){
//		    jQuery.post("ManagePB.aspx", { param: 'AddRow',id:jQuery('#GridPB_ctl02_caltime').val()}, 'json');
//		
//		}
        function on_add(){
            document.getElementById("btnAddRow").className="add_2";
        }
        function out_add(){
            document.getElementById("btnAddRow").className="add_1";
        }
        function on_remove(){
            document.getElementById("btnDelRow").className="remove_2";
        }
        function out_remove(){
            document.getElementById("btnDelRow").className="remove_1";
        }
        function on_save(){
            document.getElementById("btnSure").className="save_2";
        }
        function out_save(){
            document.getElementById("btnSure").className="save_1";
        }
        function on_no(){
            document.getElementById("btnCancel").className="no_2";
        }
        function out_no(){
            document.getElementById("btnCancel").className="no_1";
        }
    </script>

    <style type="text/css">
        #menu
        {
        	border:1px solid #2a88bb;
        }
        .button
        {
            width: 56px; /*图片宽带*/
            background: url(../img/button.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 24px; /*图片高度*/
            color: White;
            vertical-align: middle;
            text-align: center;
        }
        .add_1
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-add-1.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .add_2
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-add-2.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .remove_1
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-remove-1.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .remove_2
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-remove-2.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .save_1
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-save-1.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .save_2
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-save-2.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .no_1
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-no-1.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .no_2
        {
        	width: 56px; /*图片宽带*/
            background: url(../img/PB-no-2.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 26px; /*图片高度*/
        }
        .grid-head
        {
            font-size: 12px;
            font-weight: 100;
            color: #000000;
            background-image: url(../img/Grid_2.jpg);
            text-align: center;
            vertical-align: middle;
            background-color: #ffffff;
            height: 22px;
        }
        .style5
        {
            font-size:13px;     
            color:Black;
        }
        .style6
        {
        	font-size:13px;
        	color:#0a4869;
        	font-weight:bold;
        }
        .style7
        {
        	font-size:13px;
        	color:#15428b;
        	font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--等待层-->
    <div id="divWait" style="border: background: #ffffff; padding: 0px; width: 450px;
        z-index: 1001; display: none; position: absolute; top: 50%; left: 50%; margin: -200px 0 0 -300px;">
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td>
                    <img src="../img/Manage_01.jpg" width="28" height="34" alt="" />
                </td>
                <td colspan="2" style="background: url(../img/Manage_02.jpg)">
                    <img src="../img/Manage_02.jpg" width="380" height="34" alt="" />
                </td>
                <td>
                    <img src="../img/Manage_03.jpg" width="29" height="34" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../img/Manage_12.jpg" width="28" height="5" alt="" />
                </td>
                <td colspan="2" style="background: url(../img/Manage_13.jpg)">
                    <img src="../img/Manage_13.jpg" width="202" height="5" alt="" />
                </td>
                <td>
                    <img src="../img/Manage_14.jpg" width="29" height="5" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../img/Manage_15.jpg" width="28" height="50" alt="" />
                </td>
                <td colspan="2" style="background-color: #b9dcef" align="center">
                    正在排序并上传10年的排班表，可能需要几分钟的时间，请耐心等待...
                </td>
                <td>
                    <img src="../img/Manage_17.jpg" width="29" height="50" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../img/Manage_18.jpg" width="28" height="10" alt="" />
                </td>
                <td colspan="2" style="background: url(../img/Manage_19.jpg)">
                    <img src="../img/Manage_19.jpg" width="202" height="10" alt="" />
                </td>
                <td>
                    <img src="../img/Manage_20.jpg" width="29" height="10" alt="" />
                </td>
            </tr>
            <tr>
                <td style="background: url(../img/Manage_21.jpg)">
                    <img src="../img/Manage_21.jpg" width="28" height="20" alt="" />
                </td>
                <td colspan="2" style="background-color: #ffffff" align="center">
                    <marquee style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt;
                        border-left: black 1px solid; width: 280px; border-bottom: black 1px solid; height: 8px"
                        scrollamount="4" scrolldelay="10" direction="right" bgcolor="#ecf2ff">
                <table height="8" cellpadding="0" cellspacing="0">
                <tbody height="8">
                <tr height="8">
                <td  width="8" height="8" style="background-color:#3399ff;">&nbsp;</td>
                <td height="8">&nbsp;</td>
                <td width="8" height="8" style="background-color:#3399ff;">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="8" height="8" style="background-color:#3399ff;">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="8" height="8" style="background-color:#3399ff;">&nbsp;</td>
                <td>&nbsp;</td></tr></tbody></table></marquee>
                </td>
                <td style="background: url(../img/Manage_23.jpg)">
                    <img src="../img/Manage_23.jpg" width="29" height="20" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../img/Manage_24.jpg" width="28" height="25" alt="" />
                </td>
                <td colspan="2" style="background: url(../img/Manage_25.jpg)">
                    <img src="../img/Manage_25.jpg" width="202" height="25" alt="" />
                </td>
                <td>
                    <img src="../img/Manage_26.jpg" width="29" height="25" alt="" />
                </td>
            </tr>
        </table>
    </div>
    <div id="menu" >
    <table id="Table2" width="100%" height="92%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#FFFFFF" runat="server">
     <tr>
		<td colspan="2" background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;排班管理</td>
	</tr>
	<tr>
		<td colspan="2" background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5"></td>
	</tr>
	<tr>
	<td td colspan="2" align="left" valign="middle" height="10px">
    </td>
    </tr>
    <tr>
		<td colspan="2" background="../img/Grid_3.jpg" height="27px" valign="middle" class="style7">&nbsp;<img src="../img/search.png" />&nbsp;排班列表</td>
	</tr>
        <tr>
            <td td colspan="2" style="background-color: #efefef" align="left" valign="middle" height="40px">
            &nbsp;
                <asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" CssClass="add_1" onmouseover="on_add()" onmouseout="out_add()" Text="增加一行">
                </asp:Button>
                 &nbsp;
                <asp:Button ID="btnDelRow" runat="server" OnClick="btnDelRow_Click" CssClass="remove_1" onmouseover="on_remove()" onmouseout="out_remove()" Text="减去一行">
                </asp:Button>
                &nbsp;
                <asp:Button ID="btnSure" runat="server" OnClick="btnSure_Click" OnPreRender="btnSure_Css" CssClass="save_1" onmouseover="on_save()" onmouseout="out_save()" Text="保存"></asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CausesValidation="False" CssClass="no_1" onmouseover="on_no()" onmouseout="out_no()" Text="取消"></asp:Button>
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffff" align="center" width="60%">
                <asp:GridView ID="GridPB" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                    Width="100%" OnRowDataBound="GridView_list_RowDataBound" Font-Bold="False" BorderColor="#ffffff"
                    OnSelectedIndexChanged="GridPB_SelectedIndexChanged">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#507dd2" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="grid-head" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
            </td>
            <td width="40%" align="left" valign="top">
            <div><table width="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#FFFFFF">
            <tr>
            <td background="../img/Grid_4.jpg" height="28px"></td></tr></table></div>
            </td>
        </tr>
    </table>
    </div>
    </form>

</body>
</html>
