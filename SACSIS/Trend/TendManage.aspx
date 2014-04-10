<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TendManage.aspx.cs" Inherits="SACSIS.Trend.TendManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Js/jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        a
        {
            text-decoration: none;
        }
        tr
        {
            background-color: #ffffff;
        }
        a:hover
        {
            text-decoration: underline;
        }
        
        .STYLE6
        {
            color: #000000;
            font-size: 12;
            border: solid 1px #A8C7CE;
        }
        .STYLE10
        {
            color: #000000;
            font-size: 12px;
        }
        .STYLE19
        {
            color: #344b50;
            font-size: 12px;
            border: solid 1px #A8C7CE;
            border-top-width: 0px;
        }
        .over
        {
            background-color: #000000;
        }
    </style>
    <script type="text/javascript" language="javascript">
                function add() {
                        $.ajax({
                            type:"POST", //提交的类型
                            url: "ChartManage.aspx?userid="+<%=user_id %>  ,//提交地址<%=user_id %>
                            data: "",//参数
                            success: function(msg){ //回调方法
                            window.location.href= "ChartManage.aspx?user_id="+<%=user_id %> ; //location.href实现客户端页面的跳转
                        }
                    });
                }
                function del()
                {
                $.messager.confirm('提示', '请确定是否删除?', function (r) {
                if (r) {
                    if ($("#gridItem").datagrid("getSelected") != null) {
                       //$("#gridItem").datagrid("getSelected" 
                        var rows =$("#gridItem").datagrid("getSelected"); // 这段代码是获取当前页的所有行。
                        var rating = rows.CHARTID;
                        
                        $.post("TendManage.aspx", { chart_id:rating}, function (data) {
                          GridSta();
                          $('#gridItem').datagrid('reload'); 
            }, 'json');
            
                       // $('#gridItem').datagrid('deleteRow', $("#gridItem").datagrid("getSelected").ID - 1);
                    }
                    else
                    { alert("请选择要删除的模板"); }
                }
            });


//                    if($("#hf_chart_id").val()!="")
//                    {
//                         $.post("TendManage.aspx", { chart_id:$("#hf_chart_id").val()}, function (data) {
//                          GridSta();
//            }, 'json');
//                    }
//                    else
//                    {
//                    alert("请选择要删除的模板");
//                    }
                }
        $(function () {
            GridSta();
});
var num =-1;
        function GridSta() {
            $('#gridItem').datagrid({
                
                nowrap: true,
                autoRowHeight: false,
                fitColumns: true,
                striped: true,
                align: 'center',
                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
                collapsible: true,
                url: 'TendManage.aspx',
                sortName: 'CHARTID',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'search' },
                remoteSort: false,
                idField: 'CHARTID',
                columns: [[
                { field: '多选', checkbox: true },
				    { field: 'id', title: '序号', width: $(window).width()*0.2*0.98, align: 'center' },
                    { field: 'CHARTDESC', title: '模板描述', width: $(window).width()*0.8*0.98, align: 'center' },
                    { field: 'CHARTID', title: 'CHARTID', width: 120, hidden: true }
                ]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'aadd',
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function () {
                        add();                
                    }
                },
		        {
		            id: 'del',
		            text: '删除',
		            iconCls: 'icon-remove',
		            handler: function () {
		                del(); 
		            }
		        }],
                onDblClickRow:function(index,row){  

            location.href = "ChartManage.aspx?user_id=" + <%=user_id %>+"&operate_id=0&chart_id="+row["CHARTID"]; //location.href实现客户端页面的跳转
        },
        onSelect: function (index, row) {
                var rows = $("#gridItem").datagrid("getRows");
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i]["ID"] - 1 != index) {
                        $(".datagrid-row[datagrid-row-index=" + i + "] input[type='checkbox']")[0].checked = false;
                    }
                    else
                    {
                    $(".datagrid-row[datagrid-row-index=" + i + "] input[type='checkbox']")[0].checked = true;
                    }

                }
            },
        onClickRow:function(index,row){  
        if(num==index)
        {
            $("#hf_chart_id").attr("value","");
            num=-1;
        //alert(num+"---"+$("#hf_chart_id").val());
        }
        else
        {
            $("#hf_chart_id").attr("value",row["CHARTID"]);
            num=index;
        //alert(num+"---"+$("#hf_chart_id").val());
        }
        
        
        }
          
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Div2">
    </div>
    <div>
    </div>
    <div>
        <table id="gridItem">
        </table>
    </div>
    <asp:HiddenField ID="hf_chart_id" runat="server" />
    </form>
</body>
</html>
