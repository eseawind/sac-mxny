<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUnit.aspx.cs" Inherits="SACSIS.Report.ManageUnit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机组数据维护</title>
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
                title: '机组维护',
                iconCls: 'icon-search',
                width: 1020,
                height: 420,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                align: 'center',
                collapsible: true,
                url: 'ManageUnit.aspx',
                sortName: 'KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'InitTable' },
                idField: 'ID',
                columns: [[
                    { field: 'KEY', title: '序号', width: 60, align: 'center' },
                    { field: 'ID', title: 'IE_KEY', width: 60, align: 'center', hidden: true },
				    { field: 'UID', title: 'X轴指标', width: 220, align: 'center' },
                    { field: 'UNAME', title: 'Y轴指标', width: 220, align: 'center' },
					{ field: 'optEdit', title: '编 辑', width: 80, align: 'center',
					    formatter: function (value, rec, index) {
					        var up = '';
					        up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="EditPoint(\'' + rec.ID + '\',\'' + rec.UID + '\',\''
                            + rec.UNAME + '\')" data-options="iconCls:icon-search">编 辑</a>';
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
                    text: '增加机组',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#dv_points").attr('title', '增加机组');
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
            $.post("ManageUnit.aspx", { param: 'edit', key: key, x: escape($("#txtX_edit").val()), y: escape($("#txtY_edit").val())
            }, function (data) {
                if (data.count == "1") {
                    $('#dv_point_edit').dialog('close');
                    $.messager.alert('机组!', '机组编辑成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('机组!', '机组编辑失败!', 'info');
            }, 'json');
        }


        //编辑测点
        function EditPoint(key, x, y) {
            $("#txtX_edit").val(x);
            $("#txtY_edit").val(y);

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
            $.messager.confirm('删除机组', '你确定要删除该条数据吗?', function (ok) {
                if (ok) {
                    $.post("ManageUnit.aspx", { param: 'dell', id: id }, function (data) {
                        if (data.count == "1") {
                            $.messager.alert('机组!', '机组删除成功!', 'info');
                            showList();
                        }
                        else
                            $.messager.alert('机组!', '机组删除失败!', 'info');
                    }, 'json');
                } else {
                    $.messager.alert('机组', '删除已取消!', 'info');
                }
            });
        }

        //添加测点
        function add() {
            $.post("ManageUnit.aspx", { param: 'add', x: escape($("#txtX").val()), y: escape($("#txtY").val())
            }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('机组!', '机组添加成功!', 'info');
                    showList();
                    $('#dv_points').dialog('close');
                }
                else
                    $.messager.alert('机组!', '机组添加失败!', 'info');
            }, 'json');

        }

    </script>
</head>
<body>
    <div id="dv_list">
        <table id="tableList">
        </table>
    </div>
    <div id="dv_points" style="display: none; width: 260px; overflow-y: auto; height: 150px;">
        <table id="Table1" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    机组
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtX" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtY" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_point_edit" style="display: none; width: 260px; overflow-y: auto; height: 150px;">
        <table id="Table2" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    机组
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtX_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtY_edit" />
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="tbName" style="display: none;" />
    <input type="text" id="tbID" style="display: none;" />
    <input type="text" id="txtKEY" style="display: none;" />
</body>
</html>
