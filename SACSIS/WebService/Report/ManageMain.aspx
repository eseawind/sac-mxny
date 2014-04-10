<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMain.aspx.cs" Inherits="SACSIS.Report.ManageMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页维护</title>
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {;
            $('#tableList').datagrid({
                title: '***电厂报表机组',
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
                queryParams: { param: 'Init' },
                idField: 'ID',
                frozenColumns: [[
	                { field: 'ck', checkbox: true }
				]],
                columns: [[
				    { field: 'KEY', title: '报表编号', width: 80, align: 'center' },
                    { field: 'ID', title: '机组编码', width: 80, align: 'center' },
                    { field: 'NAME', title: '机组描述', width: 260, align: 'center' },
					{ field: 'optDel', title: '编辑', width: 150, align: 'center',
					    formatter: function (value, rec, index) {
					        var up = '';
					        up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="Dell(\'' + rec.ID + '\')" data-options="iconCls:icon-search">删  除</a>';
					        return up;
					    }
					}
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnAdd',
                    text: '增加机组',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#dv_table_add').show();
                        $('#dv_table_add').dialog({
                            buttons: [{
                                text: '增 加',
                                handler: function () {
                                    ADD();
                                    showList();
                                }
                            }, {
                                text: '取 消',
                                handler: function () {
                                    $('#dv_table_add').dialog('close');
                                }
                            }]
                        });
                    }
                }],
                onDblClickRow: function (rowIndex, rowData) {
                    $("#dv_table_eidt").show();

                    $("#txtTableName_Edit").val(rowData.NAME);
                    $("#txtTableID_Edit").val(rowData.ID);
                    $("#txtTablePath_Edit").val(rowData.PATH);

                    $('#dv_table_eidt').dialog({
                        buttons: [{
                            text: '编  辑',
                            handler: function () {
                                EditTableInfo(rowData.ID);
                            }
                        }, {
                            text: '取 消',
                            handler: function () {
                                $('#dv_table_eidt').dialog('close');
                            }
                        }]
                    });
                }
            });
        });

        function showList() {
            var query = { param: 'Init' }; //把查询条件拼接成JSON
            $("#tableList").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#tableList").datagrid('reload'); //重新加载
        }

        function ADD() {
            $.post("ManageUnit.aspx", { param: 'add', name: escape($("#txtTableName").val()), id: escape($("#txtId").val()), path: escape($("#txtTablePath").val()) }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('机组添加!', '报表机组添加成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('机组添加!', '报表机组添加失败!', 'info');
            }, 'json');
        }

        function EditTableInfo(id) {
            $.post("ManageUnit.aspx", { param: 'editTable', nameo: escape($("#txtTableName_Edit").val()), ido: escape($("#txtTableID_Edit").val()), patho: escape($("#txtTablePath_Edit").val()), id: escape(id) }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('编辑机组!', '机组编辑成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('编辑机组!', '机组编辑失败!', 'info');
            }, 'json');
        }
        function Dell(id) {
            $.messager.confirm('删除机组', '你确定要删除该机组吗?', function (ok) {
                if (ok) {
                    $.post("ManageUnit.aspx", { param: 'dell', id: escape(id) }, function (data) {
                        if (data.count == "1") {
                            $.messager.alert('删除机组!', '机组删除成功!', 'info');
                            showList();
                        }
                        else
                            $.messager.alert('删除机组!', '机组删除失败!', 'info');
                    }, 'json');
                } else {
                    $.messager.alert('删除机组', '删除已取消!', 'info');
                }
            });
        }


    </script>
</head>
<body>
    <div id="dv_list">
        <table id="tableList">
        </table>
    </div>
    <div id="dv_table_add" style="padding: 5px; width: 320px; height: 190px; display: none;">
        <table class="admintable" width="100%">
            <tr>
                <th class="adminth" colspan="2">
                    增加机组
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    机组编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtId" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    机组名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTableName" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    机组路径
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTablePath" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_table_eidt" style="padding: 5px; width: 320px; height: 190px; display: none;">
        <table class="admintable" width="100%">
            <tr>
                <th class="adminth" colspan="2">
                    编辑报表
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    报表编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTableID_Edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    报表名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTableName_Edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    机组路径
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTablePath_Edit" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
