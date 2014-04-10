<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartManage.aspx.cs" Inherits="SACSIS.Trend.ChartManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../Js/jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <%--    <script src="../Js/jquery.blockUI.js" type="text/javascript"></script>--%>
    <script src="../Js/highcharts.js" type="text/javascript"></script>
    <script src="../Js/exporting.js" type="text/javascript"></script>
    <script src="../Js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Js/ChartManage.js" type="text/javascript"></script>
    <style type="text/css">
        .button
        {
            width: 76px; /*图片宽带*/
            background: url(../img/button.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 24px; /*图片高度*/
            color: Black;
            vertical-align: middle;
            text-align: center;
        }
    </style>
    <script type="text/javascript" language="javascript">
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="font-size: 12px;">
            <tr>
                <td>
                    起始时间：
                </td>
                <td>
                    <input id="stime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                        type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;
                </td>
                <td>
                    结束时间：
                </td>
                <td>
                    <input id="etime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                        type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;
                </td>
                <td>
                    时间间隔：<input id="txt_jiange" style="width: 30px" value="60" />
                    <select id="sec_jiange">
                        <option value="秒">秒</option>
                        <option value="分">分</option>
                        <option value="小时">小时</option>
                    </select>
                </td>
                <td align="left">
                    <a id="seach" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                        查&nbsp;&nbsp;询</a>&nbsp;&nbsp; <a id="aadd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                            添&nbsp;&nbsp;加</a>&nbsp;&nbsp; <a id="aedit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-edit'">
                                编&nbsp;&nbsp;辑</a>&nbsp;&nbsp; <a id="preserve" href="#" class="easyui-linkbutton"
                                    data-options="iconCls:'icon-save'">保&nbsp;&nbsp;存</a>&nbsp;&nbsp;
                    <a id="return" href="TendManage.aspx" class="easyui-linkbutton" data-options="iconCls:'icon-back'">
                        返&nbsp;&nbsp;回</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
    </div>
    <div id="add" title="Basic Dialog" data-options="iconCls:'icon-add'" style="display: none;
        overflow-y: scroll; overflow-x: hidden; width: 1000px; padding: 10px;">
        <div style="margin-left: 10px; height: 260px; overflow-y: auto;">
            <table style="font-size: 13px; display: block;" width="900px" border="0" cellpadding="0"
                cellspacing="0">
                <tr style="height: 100px">
                    <td align="left">
                        <table>
                            <tr>
                                <td>
                                    <%--                              请选择类型:<select id="sec_type" runat="server" onchange="change_type(1)">
                                        <option value="T_BASE_PARAID_WIND">风电</option>
                                        <option value="T_BASE_PARAID_SUN">光伏</option>
                                    </select>
                                    &nbsp;&nbsp; 请选择分公司名称:
                                    <asp:DropDownList ID="ddl_level1" runat="server" onchange="change_level1()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp; 请选择电厂名称:
                                    <asp:DropDownList ID="ddl_level2" runat="server" onchange="change_level2()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp; 请选择设备名称:
                                    <asp:DropDownList ID="ddl_level3" runat="server" onchange="change_level3()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;--%>
                                    机组 :
                                    <asp:DropDownList ID="ddl_level1" runat="server" onchange="change_level1()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp; 指标类型:
                                    <asp:DropDownList ID="ddl_level2" runat="server" onchange="change_level2()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp; 测点类型:
                                    <asp:DropDownList ID="ddl_level3" runat="server" onchange="change_level3()">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                </td>
                                <td id="id_level4" style="display: none">
                                    请选择级别四:<asp:DropDownList ID="ddl_level4" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        请输入测点描述：<input id="txt_point_name" class="easyui-validatebox" style="border: solid 1px #E0ECF9;
                            text-align: center;" />&nbsp;&nbsp;<input type="button" id="btn_query" class="button"
                                value="查询" onclick="query()" />
                    </td>
                </tr>
                <tr style="height: 100px">
                    <%--<td background="../img/table-head-2.jpg" height="25px" valign="middle" style="font-size:13px;color:Black;" width="30%">
                        &nbsp;菜单列表
                    </td>--%>
                    <td>
                        已选测点名称：<br />
                        <div id="point_choose">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="div_point_name">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" align="center">
                        <input id="btn_sure" type="button" class="button" value="确定" onclick="sure_query()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" id="QX_SURE" class="button" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="edit" title="Basic Dialog" data-options="iconCls:'icon-edit'" style="display: none;
        overflow-y: scroll; overflow-x: hidden; width: 600px; height: 400px; padding: 10px;">
        <div style="margin-left: 10px; height: 250px;">
            <table style="font-size: 12px; display: block;" width="500px">
                <tr>
                    <td style="width: 100px">
                        请选择测点名称：
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="div_value_edit">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="margin-left: 20px;" valign="bottom" class="style1" align="center">
                        <input id="btnupdate" type="button" value="确定" onclick="sure_update()" class="button" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" id="QX_UPDATE" value="取消" class="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="pre" title="Basic Dialog" data-options="iconCls:'icon-save'" style="display: none;
        overflow-y: scroll; overflow-x: hidden; width: 400px; height: 300px; padding: 10px;">
        <div style="margin-left: 10px; height: 250px;">
            <table style="font-size: 13px; display: block;" height="100%" width="400px">
                <tr>
                    <td>
                        请选择模板名称：
                    </td>
                    <td align="left">
                    </td>
                    <td>
                        <input id="txt_chart_name" class="easyui-validatebox" style="border: solid 1px #058DC7;
                            text-align: center;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="margin-left: 20px" valign="bottom" class="style1" align="center">
                        <input id="btn_preserve" type="button" value="确定" onclick="sure_preserve()" class="button" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" id="QX_PRE" value="取消" class="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hf_value" runat="server" />
    <asp:HiddenField ID="hf_value_his" runat="server" />
    </form>
</body>
</html>
