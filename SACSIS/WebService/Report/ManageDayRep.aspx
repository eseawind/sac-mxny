<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDayRep.aspx.cs" Inherits="SACSIS.Report.ManageDayRep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日报表维护</title>
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //初始化报表
            InitTable();
            //加载报表模板
            ShowModel();
            //初始化单选事件
            RadioChange();
        });

        //初始化报表数据  报表名称
        function InitTable() {
            $('#tableList').datagrid({
                title: '***电厂日报表维护',
                iconCls: 'icon-search',
                width: 1020,
                height: 420,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                align: 'center',
                collapsible: true,
                url: 'ManageDayRep.aspx',
                sortName: 'ID',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'InitTable' },
                idField: 'ID',
                //                frozenColumns: [[
                //	                { field: 'ck', checkbox: true }
                //				]],
                columns: [[
				    { field: 'ID', title: '报表编号', width: 60, align: 'center' },
                    { field: 'T_DCNAME', title: '报表编码', width: 80, align: 'center' },
                    { field: 'NAME', title: '报表名称', width: 260, align: 'center' },
                    { field: 'MNAME', title: '模板名称', width: 80, align: 'center' },
                    { field: 'MPATH', title: '模板路径', width: 80, align: 'center' },
					{ field: 'optEdit', title: '编 辑', width: 80, align: 'center',
					    formatter: function (value, rec, index) {
					        var up = '';
					        up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="editTable(\'' + rec.T_DCNAME + '\',\'' + rec.NAME + '\')" data-options="iconCls:icon-search">编 辑</a>';
					        return up;
					    }
					},
                    { field: 'optDel', title: '删 除', width: 80, align: 'center',
                        formatter: function (value, rec, index) {
                            var up = '';
                            up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="Dell(\'' + rec.T_DCNAME + '\')" data-options="iconCls:icon-search">删 除</a>';
                            return up;
                        }
                    },
                    { field: 'optShow', title: '查 看', width: 80, align: 'center',
                        formatter: function (value, rec, index) {
                            var up = '';
                            up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="showRepott(\'' + rec.T_DCNAME + '\',\'' + rec.MPATH + '\')" data-options="iconCls:icon-search">查  看</a>';
                            return up;
                        }
                    }
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '增加报表',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#dv_table").attr('title', '添加报表');
                        $('#dv_table').show();
                        $('#dv_table').dialog({
                            buttons: [{
                                text: '增 加',
                                handler: function () {
                                    add();
                                    showList();
                                }
                            }, {
                                text: '取 消',
                                handler: function () {
                                    $('#dv_table').dialog('close');
                                }
                            }]
                        });
                    }
                }],
                onDblClickRow: function (rowIndex, rowData) {
                    $("#tbName").val(rowData.NAME);
                    $("#tbID").val(rowData.T_DCNAME);
                    ListSun(rowData.NAME);
                }
            });
        }
        function showList() {
            var query = { param: 'InitTable' }; //把查询条件拼接成JSON
            $("#tableList").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#tableList").datagrid('reload'); //重新加载
        }


        //加载报表属性
        function ListSun(table) {
            $("#dv_list").hide();
            $("#tableSunList").show();
            $("#dv_sun_liste").show();
            InitRadio();
            $('#tableSunList').datagrid({
                title: table + '配置',
                iconCls: 'icon-search',
                width: 1020,
                height: 420,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                align: 'center',
                collapsible: true,
                url: 'ManageDayRep.aspx',
                sortName: 'ID',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'InitSun', name: escape(table) },
                idField: 'ID',
                frozenColumns: [[
	                { field: 'ck', checkbox: true }
				]],
                columns: [[
				    { field: 'ID', title: '编号', width: 50, align: 'center' },
                    { field: 'T_PARAID', title: '原始测点', width: 150, align: 'center' },
                    { field: 'T_DESCRIP', title: '测点描述', width: 100, align: 'center' },
                    { field: 'I_JZ', title: '机组', width: 50, align: 'center' },
                    { field: 'I_SHOWID', title: '显示顺序', width: 50, align: 'center' },
                    { field: 'T_UNIT', title: '单位', width: 50, align: 'center' },
                    { field: 'T_FENDESC', title: '测点描述2', width: 150, align: 'center', hidden: true },
                    { field: 'T_FENDESC2', title: '测点描述2', width: 150, align: 'center', hidden: true },
                    { field: 'T_REMARK', title: '备注1', width: 150, align: 'center', hidden: true },
                    { field: 'T_REMARK2', title: '备注2', width: 150, align: 'center', hidden: true },
                    { field: 'ID_KEY', title: '主键', width: 150, align: 'center', hidden: true },
                    { field: 'optEdit', title: '编辑', width: 80, align: 'center',
                        formatter: function (value, rec, index) {
                            var up = '';
                            up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="edittableInfo(\'' + rec.T_PARAID
                            + '\',\'' + rec.T_DESCRIP + '\',\'' + rec.I_JZ + '\',\'' + rec.I_SHOWID + '\',\'' + rec.T_UNIT + '\',\'' + rec.T_FENDESC + '\',\'' + rec.T_FENDESC2 + '\',\''
                            + rec.T_REMARK + '\',\'' + rec.T_REMARK + '\',\'' + rec.ID_KEY + '\')" data-options="iconCls:icon-search">编 辑</a>';
                            return up;
                        }
                    }, { field: 'optDel', title: '删 除', width: 80, align: 'center',
                        formatter: function (value, rec, index) {
                            var up = '';
                            up = '<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="DellAttribute(\'' + rec.ID_KEY + '\')" data-options="iconCls:icon-search">删 除</a>';
                            return up;
                        }
                    }
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '增加属性',
                    iconCls: 'icon-add',
                    handler: function () {
                        $("#dv_points").attr('title', '添加报表属性');
                        $('#dv_points').show();

                        $('#dv_points').dialog({
                            buttons: [{
                                text: '保存',
                                iconCls: 'icon-ok',
                                handler: function () {
                                    addSun();
                                }
                            }, {
                                text: '取 消',
                                handler: function () {
                                    $('#dv_points').dialog('close');
                                }
                            }]
                        });
                    }
                }, {
                    id: 'btnBlack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        $("#showList").hide();
                        $("#dv_sun_liste").hide();
                        $("#dv_list").show();
                    }
                }]
            });
        }
        //重新加载报表属性
        function showListSUN() {
            var query = { param: 'InitSun' }; //把查询条件拼接成JSON
            $("#tableSunList").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#tableSunList").datagrid('reload'); //重新加载
        }

        //删除报表属性
        function DellAttribute(key) {

            $.messager.confirm('删除报表', '你确定要删除该条数据吗?', function (ok) {
                if (ok) {
                    $.post("ManageDayRep.aspx", { param: 'Attribute', key: key
                    }, function (data) {
                        $.messager.alert('删除报表', '删除成功!', 'info')
                        showListSUN();
                    }, 'json');
                } else {
                    $.messager.alert('删除报表', '删除已取消!', 'info');
                }
            });
        }

        //单选事件
        function RadioChange() {
            $("#dv_radio_eidt").change(function () {
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

                $.post("ManageDayRep.aspx", { param: 'name', id: sign }, function (data) {
                    $("#txtName1_edit").val(data.name);
                }, 'json');
            });
        }

        //编辑报表属性
        function EditTableSunInfo(ID_KEY) {

            var pid = "";
            var sign = "";
            var inputs = document.getElementsByTagName('input'); //获取所有的input标签对象。
            for (var i = 0; i < inputs.length; i++) {
                var obj = inputs[i];
                if (obj.type == 'radio') {
                    if (obj.checked == true) {
                        sign += obj.value
                    }
                }
            }
            pid = sign;

            $.post("ManageDayRep.aspx", { param: 'sunEdit', name1: escape($("#txtName1_edit").val()), jz: $("#txtJz_edit").val(), order: $("#txtOrder_edit").val(), unit: escape($("#txtUnit_edit").val()),
                name2: escape($("#txtName2_edit").val()), name3: escape($("#txtName3_edit").val()), remark1: escape($("#txtRemark1_edit").val()), remark2: escape($("#txtRemark2_edit").val()), id: pid, key: ID_KEY
            }, function (data) {

                if (data.count == "1") {
                    $.messager.alert('编辑报表!', '报表编辑成功!', 'info');
                    $('#dv_sun_table_edit').dialog('close');
                    showListSUN();
                }
                else
                    $.messager.alert('编辑报表!', '报表编辑失败!', 'info');

            }, 'json');
        }

        //初始化单选框
        function EditRadio(id) {
            $.post("ManageDayRep.aspx", { param: 'pointEdit' }, function (data) {
                var list = data.list;
                if (list != null) {

                    //加载单选框
                    var htmlb = '';
                    //绑定风场数据
                    $("#dv_radio_eidt").html('');

                    for (var i = 1; i <= list.length; i++) {
                        if (list[i - 1].ID == id) {
                            htmlb += "<input type='radio' name='item' value='" + list[i - 1].ID + "' checked>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                            $("#txtName1_edit").val(list[i - 1].NAME);
                        } else {
                            htmlb += "<input type='radio' name='item' value='" + list[i - 1].ID + "'>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                        }
                    }
                    $("#dv_radio_eidt").html(htmlb);
                    htmlb = '';
                }
            }, 'json');
        }

        //编辑报表属性
        function edittableInfo(T_PARAID, T_DESCRIP, I_JZ, I_SHOWID, T_UNIT, T_FENDESC, T_FENDESC2, T_REMARK, T_REMARK2, ID_KEY) {
            $("#txtName1_edit").val(T_DESCRIP);
            $("#txtJz_edit").val(I_JZ);
            $("#txtOrder_edit").val(I_SHOWID);
            $("#txtUnit_edit").val(T_UNIT);
            $("#txtName2_edit").val(T_FENDESC);
            $("#txtName3_edit").val(T_FENDESC2);
            $("#txtRemark1_edit").val(T_REMARK);
            $("#txtRemark2_edit").val(T_REMARK2);
            $("#txtKEY").val(ID_KEY);

            EditRadio(T_PARAID);

            $("#dv_sun_table_edit").show();
            $('#dv_sun_table_edit').dialog({
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: function () {
                        $.messager.confirm('编辑报表', '你确定要编辑该条数据吗?', function (ok) {
                            if (ok) {
                                EditTableSunInfo(ID_KEY);

                            } else {
                                $.messager.alert('编辑报表', '编辑已取消!', 'info');
                            }
                        });
                    }
                }, {
                    text: '取 消',
                    handler: function () {
                        $('#dv_sun_table_edit').dialog('close');
                    }
                }]
            });
        }

        //添加报表属性
        function addSun() {
            var pid = "";
            var sign = "";
            var inputs = document.getElementsByTagName('input'); //获取所有的input标签对象。
            for (var i = 0; i < inputs.length; i++) {
                var obj = inputs[i];
                if (obj.type == 'checkbox') {
                    if (obj.checked == true) {
                        sign += obj.value + '*';
                    }
                }
            }
            pid = sign.substring(0, sign.length - 1);

            $.post("ManageDayRep.aspx", { param: 'addSun', tid: escape($("#tbID").val()), tName: escape($("#tbName").val()), id: escape(pid)
            }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('报表属性添加!', '报表名称添加成功!', 'info');
                    $('#dv_points').dialog('close');
                    showListSUN();
                }
                else
                    $.messager.alert('报表属性添加!', '报表名称添加失败!', 'info');
            }, 'json');
        }

        //初始化单选框 复选框
        function InitRadio() {
            $.post("ManageDayRep.aspx", { param: 'point' }, function (data) {
                var list = data.list;
                if (list != null) {
                    var htmlb = '';
                    //绑定风场数据
                    $("#dv_radio").html('');

                    for (var i = 1; i <= list.length; i++) {
                        //                        if (i % 3 == 0) {
                        htmlb += "<input type='radio' name='item' value='" + list[i - 1].ID + "'>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                        //                        } else {
                        //                            htmlb += "<input type='radio' name='item' value='" + list[i - 1].ID + "'>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                        //                        }
                    }
                    $("#dv_radio").html(htmlb);

                    var htmlc = '';
                    for (var i = 1; i <= list.length; i++) {
                        if (i % 2 == 0 && i != 0) {
                            htmlc += "<input type='checkbox' style='width:10px;' name='checked_BG' value='" + list[i - 1].ID + "'>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                        } else {
                            htmlc += "<input type='checkbox' style='width:10px;' name='checked_BG' value='" + list[i - 1].ID + "'>&nbsp;&nbsp;&nbsp;" + list[i - 1].NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                        }
                    }
                    //加载复选框
                    $("#dv_check").html(htmlc);
                    htmlc = '';
                }
            }, 'json');
        }

        //展示报表
        function showRepott(id, path) {
            //弹出页面
            window.open('hourreport.aspx?DCName=' + id + '&ReportPath=' + path);
        }

        //删除报表
        function Dell(id) {
            $.messager.confirm('删除报表', '你确定要删除该报表吗?', function (ok) {
                if (ok) {
                    $.post("ManageDayRep.aspx", { param: 'dellTable', id: id }, function (data) {
                        if (data.count == "1") {
                            $.messager.alert('删除报表!', '报表删除成功!', 'info');
                            showList();
                        }
                        else
                            $.messager.alert('删除报表!', '报表删除失败!', 'info');
                    }, 'json');
                } else {
                    $.messager.alert('删除报表', '删除已取消!', 'info');
                }
            });
        }

        //编辑报表
        function editTableInfo(id, name) {
            $.post("ManageDayRep.aspx", { param: 'edittable', nameo: escape(name), ido: escape(id), name: escape($("#txttableName").val()), id: escape($("#txttableID").val()) }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('编辑报表!', '报表编辑成功!', 'info');
                    showList();
                    $('#dv_table_eidt').dialog('close');
                }
                else
                    $.messager.alert('编辑报表!', '报表编辑失败!', 'info');
            }, 'json');
        }

        //编辑报表
        function editTable(id, name) {
            $("#txttableID").val(id);
            $("#txttableName").val(name);
            $("#dv_table_eidt").show();
            $('#dv_table_eidt').dialog({
                buttons: [{
                    text: '编 辑',
                    iconCls: 'icon-save',
                    handler: function () {

                        $.messager.confirm('编辑报表', '你确定要编辑该条数据吗?', function (ok) {
                            if (ok) {
                                editTableInfo(id, name);
                            } else {
                                $.messager.alert('编辑报表', '编辑已取消!', 'info');
                            }
                        });
                    }
                }, {
                    text: '取 消',
                    handler: function () {
                        $('#dv_table_eidt').dialog('close');
                    }
                }]
            });
        }

        //加载报表模板
        function ShowModel() {
            //加载报表模板
            $.post("ManageRep.aspx", { param: 'showModel' }, function (data) {
                var list = data.list;
                $("#mType").empty();
                if (list != null) {
                    for (var i = 0; i < list.length; i++) {
                        $("#mType").append("<option value='" + list[i].T_ID + "'>" + list[i].T_NAME + "</option>");
                    }
                } else {
                    $.messager.alert('报表添加!', '没有报表模板，请先添加模板!', 'warning');
                }
            }, 'json');
        }

        //添加报表名称
        function add() {           
            $.post("ManageDayRep.aspx", { param: 'add', name: escape($("#txtTableName").val()), id: escape($("#txtId").val()), model: $("#mType").val() }, function (data) {
                if (data.count == "1") {
                    $.messager.alert('报表添加!', '报表名称添加成功!', 'info');
                    showList();
                    $('#dv_table').dialog('close');
                }
                else
                    $.messager.alert('报表添加!', '报表名称添加失败!', 'info');
            }, 'json');
        }

    </script>
</head>
<body>
    <div id="dv_list">
        <table id="tableList">
        </table>
    </div>
    <div id="dv_table" style="padding: 5px; width: 320px; height: 170px; display: none;">
        <table class="admintable" width="100%">
            <tr>
                <th class="adminth" colspan="2">
                    增加报表
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    所属模板
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <select id="mType" style="width: 135px; text-align: center;">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    报表编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtId" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    报表名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTableName" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_table_eidt" style="padding: 5px; width: 320px; height: 140px; display: none;">
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txttableID" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    报表名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txttableName" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_sun_liste">
        <table id="tableSunList" style="display: none;">
        </table>
    </div>
    <div id="dv_points" style="display: none; width: 600px; overflow-y: auto; height: 400px;">
        <table id="Table2" class="admintable">
            <tr>
                <th class="adminth" colspan="4">
                    增加报表属性
                </th>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="dv_check" style="margin: 10px; width: 500px;">
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
    <input type="text" id="txtKEY" style="display: none;" />
</body>
</html>
