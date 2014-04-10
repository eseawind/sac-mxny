<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRepModel.aspx.cs"
    Inherits="SACSIS.Report.ManageRepModel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表模板维护</title>
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //            InitRadio();
            $('#tableList').datagrid({
                title: '***电厂报表模板',
                iconCls: 'icon-search',
                width: 1020,
                height: 420,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                align: 'center',
                collapsible: true,
                url: 'ManageRepModel.aspx',
                sortName: 'IDKEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'Init' },
                idField: 'ID',
                frozenColumns: [[
	                { field: 'ck', checkbox: true }
				]],
                columns: [[
				    { field: 'IDKEY', title: '报表编号', width: 80, align: 'center' },
                    { field: 'ID', title: '模板编码', width: 80, align: 'center' },
                    { field: 'NAME', title: '模板描述', width: 260, align: 'center' },
                    { field: 'PATH', title: '模板路径', width: 260, align: 'center' },
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
                    text: '增加模板',
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
            $.post("ManageRepModel.aspx", { param: 'add', name: escape($("#txtTableName").val()), id: escape($("#txtId").val()), path: escape($("#txtTablePath").val()) }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('模板添加!', '报表模板添加成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('模板添加!', '报表模板添加失败!', 'info');
            }, 'json');
        }

        function EditTableInfo(id) {
            $.post("ManageRepModel.aspx", { param: 'editTable', nameo: escape($("#txtTableName_Edit").val()), ido: escape($("#txtTableID_Edit").val()), patho: escape($("#txtTablePath_Edit").val()), id: escape(id) }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('编辑模板!', '模板编辑成功!', 'info');
                    showList();
                }
                else
                    $.messager.alert('编辑模板!', '模板编辑失败!', 'info');
            }, 'json');
        }
        function Dell(id) {
            $.messager.confirm('删除模板', '你确定要删除该模板吗?', function (ok) {
                if (ok) {
                    $.post("ManageRepModel.aspx", { param: 'dell', id: escape(id) }, function (data) {
                        if (data.count == "1") {
                            $.messager.alert('删除模板!', '模板删除成功!', 'info');
                            showList();
                        }
                        else
                            $.messager.alert('删除模板!', '模板删除失败!', 'info');
                    }, 'json');
                } else {
                    $.messager.alert('删除模板', '删除已取消!', 'info');
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
                    增加模板
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    模板编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtId" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    模板名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTableName" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    模板路径
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
                    模板路径
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTablePath_Edit" />
                </td>
            </tr>
        </table>
    </div>
    <%--    <div id="dv_sun_liste">
        <table id="tableSunList" style="display: none;">
        </table>
    </div>
    <div id="dv_sun_table" style="display: none; width: 600px; overflow-y: auto; height: 400px;">
        <table id="tba_up" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    增加报表属性
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    一级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName1" />
                </td>
                <td class="admincls0" align="center">
                    机组
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtJZ" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    显示顺序
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtOrder" />
                </td>
                <td class="admincls1" align="center">
                    单位
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtUnit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    二级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName2" />
                </td>
                <td class="admincls0" align="center">
                    三级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName3" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    一级标注
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtRemark1" />
                </td>
                <td class="admincls1" align="center">
                    二级标注
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtRemark2" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    测点选择
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="dv_radio" style="margin: 10px; width: 500px;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_sun_table_edit" style="display: none; width: 600px; overflow-y: auto;
        height: 400px;">
        <table id="Table1" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    编辑报表属性
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    一级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName1_edit" />
                </td>
                <td class="admincls0" align="center">
                    机组
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtJz_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    显示顺序
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtOrder_edit" />
                </td>
                <td class="admincls1" align="center">
                    单位
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtUnit_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    二级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName2_edit" />
                </td>
                <td class="admincls0" align="center">
                    三级描述
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtName3_edit" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    一级标注
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtRemark1_edit" />
                </td>
                <td class="admincls1" align="center">
                    二级标注
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" id="txtRemark2_edit" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    测点选择
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="dv_radio_eidt" style="margin: 10px; width: 500px;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="tbName" style="display: none;" />
    <input type="text" id="tbID" style="display: none;" />
    <input type="text" id="txtKEY" style="display: none;" />--%>
</body>
</html>
