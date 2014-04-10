<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePoints.aspx.cs" Inherits="SACSIS.Report.ManagePoints" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测点表维护</title>
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //初始化报表
            InitTable();
        });

        //初始化报表数据  报表名称
        function InitTable() {
            $('#tableList').datagrid({
                title: '测点维护',
                iconCls: 'icon-search',
                width: 1020,
                height: 420,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                align: 'center',
                collapsible: true,
                url: 'ManagePoints.aspx',
                sortName: 'KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'InitTable' },
                idField: 'ID',
                columns: [[
                    { field: 'KEY', title: 'KEY', width: 60, align: 'center', hidden: true },
				    { field: 'ID', title: '编号', width: 60, align: 'center' },
                    { field: 'T_UNITID', title: '机组', width: 60, align: 'center' },
                    { field: 'T_POWERTAG', title: '负荷', width: 180, align: 'center' },
                    { field: 'T_FLOWTAG', title: '主汽流量', width: 180, align: 'center' },
                    { field: 'T_PRESSURETAG', title: '主汽压力', width: 180, align: 'center' },
                    { field: 'T_TEMPERATURETAG', title: '主汽温度', width: 180, align: 'center' },
                    { field: 'T_REHEATTEMPERATURETAG', title: '再热温度', width: 180, align: 'center' },
                    { field: 'T_VACUUM', title: '真空', width: 180, align: 'center' },
                    { field: 'T_EFFICIENCY', title: '锅炉效率', width: 180, align: 'center' },
                    { field: 'T_HEATCONSUMPTION', title: '热耗', width: 180, align: 'center' },
                    { field: 'T_COALCONSUMPTION', title: '煤耗', width: 180, align: 'center' },
                    { field: 'T_INFO_RL', title: '负荷标准', width: 180, align: 'center', hidden: true },
					{ field: 'optEdit', title: '编 辑', width: 80, align: 'center',
					    formatter: function (value, rec, index) {
					        var up = '';
					        up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="EditPoint(\'' + rec.KEY + '\',\'' + rec.ID + '\',\''
                            + rec.T_UNITID + '\',\'' + rec.T_POWERTAG + '\',\'' + rec.T_POWERTAG + '\',\'' + rec.T_FLOWTAG + '\',\'' + rec.T_PRESSURETAG + '\',\'' + rec.T_TEMPERATURETAG + '\',\'' + rec.T_REHEATTEMPERATURETAG + '\',\''
                            + rec.T_VACUUM + '\')" data-options="iconCls:icon-search">编 辑</a>';
					        return up;
					    }
					},
                    { field: 'optDel', title: '删 除', width: 80, align: 'center',
                        formatter: function (value, rec, index) {
                            var up = '';
                            up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="Dell(\'' + rec.ID + '\')" data-options="iconCls:icon-search">删 除</a>';
                            return up;
                        }
                    }
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '增加',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#dv_points").attr('title', '增加');
                        $('#dv_points').show();
                        $('#dv_points').dialog({
                            buttons: [{
                                text: '增 加',
                                handler: function () {
                                    add();
                                    showList();
                                }
                            }, {
                                text: '取 消',
                                handler: function () {
                                    $('#dv_points').dialog('close');
                                }
                            }]
                        });
                    }
                }]
            });
        }
        function showList() {
            var query = { param: 'InitTable' }; //把查询条件拼接成JSON
            $("#tableList").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#tableList").datagrid('reload'); //重新加载
        }

        //编辑报表
        function EditPoints(key) {
            var pid = '';
            var sign = "";
            var count = 0;
            var inputs = document.getElementsByTagName('input'); //获取所有的input标签对象。
            for (var i = 0; i < inputs.length; i++) {
                var obj = inputs[i];
                if (obj.type == 'radio') {
                    if (obj.checked == true) {
                        sign += obj.value
                        count = 1;
                    }
                }
            }

            $.post("ManagePoints.aspx", { param: 'edit', key: key, paraid: escape($("#txtPointID_edit").val()), desc: escape($("#txtPointDesc_edit").val()),
                cslx: escape($("#txtCSLX_edit").val()), sql: escape($("#txtSQL_edit").val()), point: escape($("#txtPoint_edit").val()), jz: escape($("#txtJZ_edit").val()),
                zblx: escape($("#txtZBLX_edit").val()), cdlx: escape($("#txtPointType_edit").val()), radio: sign
            }, function (data) {
                if (data.count == "1") {
                    $('#dv_point_edit').dialog('close');
                    $.messager.alert('编辑报表!', '报表编辑成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('编辑报表!', '报表编辑失败!', 'info');
            }, 'json');
        }


        //编辑测点
        function EditPoint(key, paraid, realtime, desc, type, sql, level1, level2, level3, flag) {
            $("#txtPointID_edit").val(paraid);
            $("#txtPointDesc_edit").val(desc);
            $("#txtCSLX_edit").val(type);
            $("#txtSQL_edit").val(sql);
            $("#txtPoint_edit").val(realtime);
            $("#txtJZ_edit").val(level1);
            $("#txtZBLX_edit").val(level2);
            $("#txtPointType_edit").val(level3);

            if (flag == "1")
                $("#radioEdit1").attr("checked", "checked");
            else
                $("#radioEdit0").attr("checked", "checked");
            $("#dv_point_edit").show();
            $('#dv_point_edit').dialog({
                buttons: [{
                    text: '编 辑',
                    iconCls: 'icon-save',
                    handler: function () {
                        $.messager.confirm('编辑测点', '你确定要编辑该条数据吗?', function (ok) {
                            if (ok) {
                                EditPoints(key);
                            } else {
                                $.messager.alert('编辑测点', '编辑已取消!', 'info');
                            }
                        });
                    }
                }, {
                    text: '取 消',
                    handler: function () {
                        $('#dv_point_edit').dialog('close');
                    }
                }]
            });
        }

        //删除测点
        function Dell(id) {
            $.messager.confirm('删除测点', '你确定要删除该测点吗?', function (ok) {
                if (ok) {
                    $.post("ManagePoints.aspx", { param: 'dellPoint', id: id }, function (data) {
                        if (data.count == "1") {
                            $.messager.alert('删除测点!', '测点删除成功!', 'info');
                            showList();
                        }
                        else
                            $.messager.alert('删除测点!', '测点删除失败!', 'info');
                    }, 'json');
                } else {
                    $.messager.alert('删除测点', '删除已取消!', 'info');
                }
            });
        }

        //添加测点
        function add() {
            var pid = '';
            var sign = "";
            var count = 0;
            var inputs = document.getElementsByTagName('input'); //获取所有的input标签对象。
            for (var i = 0; i < inputs.length; i++) {
                var obj = inputs[i];
                if (obj.type == 'radio') {
                    if (obj.checked == true) {
                        sign += obj.value
                        count = 1;
                    }
                }
            }
            $.post("ManagePoints.aspx", { param: 'add', radio: sign, pointType: escape($("#txtPointType").val()), zbType: escape($("#txtZType").val()), point: escape($("#txtPoint").val()),
                jz: escape($("#txtJZ").val()), SQL: escape($("#txtSQL").val()), cType: escape($("#txtCType").val()), ID: $("#txtPointID").val(), desc: escape($("#txtPointDesc").val())
            }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('测点添加!', '测点添加成功!', 'info');
                    showList();
                    $('#dv_points').dialog('close');
                }
                else
                    $.messager.alert('测点添加!', '测点添加失败!', 'info');
            }, 'json');

        }

    </script>
</head>
<body>
    <div id="dv_list">
        <table id="tableList">
        </table>
    </div>
    <div id="dv_points" style="display: none; width: 600px; overflow-y: auto; height: 400px;">
        <table id="Table1" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    添加报表测点
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    测点编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointID" />
                </td>
                <td class="admincls0" align="center">
                    测点描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointDesc" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    参数类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtCType" />
                </td>
                <td class="admincls1" align="center">
                    SQL
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtSQL" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    测点
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPoint" />
                </td>
                <td class="admincls0" align="center">
                    机组
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtJZ" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    指标类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtZType" />
                </td>
                <td class="admincls1" align="center">
                    测点类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointType" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    是否是X轴
                </td>
                <td class="admincls1" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="radio" id="radio0" name="radio" value="1" checked="checked" />是
                    <input type="radio" id="radio1" name="radio" value="0" />否
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_point_edit" style="display: none; width: 600px; overflow-y: auto; height: 400px;">
        <table id="Table2" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    添加报表测点
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    测点编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointID_edit" />
                </td>
                <td class="admincls0" align="center">
                    测点描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointDesc_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    参数类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtCSLX_edit" />
                </td>
                <td class="admincls1" align="center">
                    SQL
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtSQL_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    测点
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPoint_edit" />
                </td>
                <td class="admincls0" align="center">
                    机组
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtJZ_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    指标类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtZBLX_edit" />
                </td>
                <td class="admincls1" align="center">
                    测点类型
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtPointType_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    是否是X轴
                </td>
                <td class="admincls1" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="radio" id="radioEdit1" name="radio" value="1" />是
                    <input type="radio" id="radioEdit0" name="radio" value="0" />否
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="tbName" style="display: none;" />
    <input type="text" id="tbID" style="display: none;" />
    <input type="text" id="txtKEY" style="display: none;" />
</body>
</html>
