<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComparaAnalysis.aspx.cs"
    Inherits="SACSIS.Trend.ComparaAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jQueryEasyUI/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        #menu
        {
            border: 1px solid #2a88bb;
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
        .style5
        {
            font-size: 12px;
            color: Black;
        }
        .style6
        {
            font-size: 13px;
            color: #0a4869;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" defer="defer">

        function () {
            $.post("ComparaAnalysis.aspx", { param: 'Init' }, function (data) {

            }, 'json');
        }


        var data_sb = null; var chart1;
        $(function () {
            chart1 = new Highcharts.Chart({
                //$('#container').highcharts
                chart: {
                    renderTo: "container",
                    type: 'spline'
                },
                title: {
                    text: '数据趋势图'
                },
                subtitle: {//副标题
                    text: ''
                },
                xAxis: {
                    type: 'datetime', reversed: true, allowDecimals: false,
                    tickPixelInterval: 50,
                    //reversed: true(颠倒排列顺序), allowDecimals: false(显示为整数)，tickPixelInterval:50(刻度间隔)
                    labels: { formatter: function () {
                        var vDate = new Date(this.value);
                        return vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds();
                    }
                    }
                },
                yAxis: {
                    title: {
                        text: '百分比 (%)'
                    }
                },
                global: {
                    useUTC: false
                }
            });
            function Hc() {
                var chart;
                Highcharts.theme = {
                    colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
                    chart: {
                        backgroundColor: {
                            linearGradient: [0, 0, 500, 500],
                            stops: [
                    [0, 'rgb(255, 255, 255)'],
                    [1, 'rgb(240, 240, 255)']
                 ]
                        },
                        borderWidth: 2,
                        plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                        plotShadow: true,
                        plotBorderWidth: 1
                    },
                    title: {
                        style: {
                            color: '#000',
                            font: 'bold 16px "Trebuchet MS", Verdana, sans-serif'
                        }
                    },
                    global: { useUTC: false },
                    subtitle: {
                        style: {
                            color: '#666666',
                            font: 'bold 12px "Trebuchet MS", Verdana, sans-serif'
                        }
                    },
                    xAxis: {
                        gridLineWidth: 1,
                        lineColor: '#000',
                        tickColor: '#000',
                        labels: {
                            style: {
                                color: '#000',
                                font: '11px Trebuchet MS, Verdana, sans-serif'
                            }
                        },
                        title: {
                            style: {
                                color: '#333',
                                fontWeight: 'bold',
                                fontSize: '12px',
                                fontFamily: 'Trebuchet MS, Verdana, sans-serif'

                            }
                        }
                    },
                    yAxis: {
                        minorTickInterval: 'auto',
                        lineColor: '#000',
                        lineWidth: 1,
                        tickWidth: 1,
                        tickColor: '#000',
                        labels: {
                            style: {
                                color: '#000',
                                font: '11px Trebuchet MS, Verdana, sans-serif'
                            }
                        },
                        title: {
                            style: {
                                color: '#333',
                                fontWeight: 'bold',
                                fontSize: '12px',
                                fontFamily: 'Trebuchet MS, Verdana, sans-serif'
                            }
                        }
                    },
                    legend: {
                        itemStyle: {
                            font: '9pt Trebuchet MS, Verdana, sans-serif',
                            color: 'black'

                        },
                        itemHoverStyle: {
                            color: '#039'
                        },
                        itemHiddenStyle: {
                            color: 'gray'
                        }
                    },
                    labels: {
                        style: {
                            color: '#99b'
                        }
                    }
                };
            }
            $("input[id='txt_xiaxian']").keyup(function () {  //keyup事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).bind("paste", function () {  //CTR+V事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).css("ime-mode", "disabled");  //CSS设置输入法不可用
            $("input[id='txt_shangxian']").keyup(function () {  //keyup事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).bind("paste", function () {  //CTR+V事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).css("ime-mode", "disabled");  //CSS设置输入法不可用
        });
        function change_xdata() {
            var par = $("#sec_xdata").find("option:selected").val();
            if (par == "锅炉效率") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=二次风比例>二次风比例</option><option value=二次风箱差压>二次风箱差压</option><option value=飞灰大渣含碳量>飞灰大渣含碳量</option><option value=给水温度>给水温度</option><option value=锅炉蒸发量>锅炉蒸发量</option>");
            }
            else if (par == "q_fd") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=Eta_H>高压缸效率</option><option value=Eta_M>中压缸效率</option><option value=凝汽器过冷度>凝汽器过冷度</option>");
            }
            else if (par == "制粉出力") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=Eta_H>煤质指标</option><option value=PA_tot>一次风量</option><option value=一次风温度>一次风温度</option><option value=加载力>加载力</option>");
            }
            else if (par == "凝汽器压力") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=循环水进口温度>循环水进口温度</option><option value=循环水流量>循环水流量</option><option value=凝汽器端差>凝汽器端差</option>");
            }
            else if (par == "机组负荷") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=锅炉效率>锅炉效率</option><option value=汽轮机热耗率>汽轮机热耗率</option><option value=厂用电率>厂用电率</option>");
            }
            else if (par == "供电煤耗") {
                $("#sec_ydata").empty();
                $("#sec_ydata").append("<option value=煤质指标>煤质指标</option>");
            }

        }
        function change_company() {
            var par = $("#sec_company").find("option:selected").val();
            $.post(
        "../datafile/Get_Chart_Data.aspx",
        {
            sec_type_real: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#sec_electric").empty();
        $("#sec_crew").empty();
        if (data == "") {
            $("#sec_electric").append("<option value=请选择>-请选择-</option>");
            $("#sec_crew").append("<option value=请选择>-请选择-</option>");
        }
        else {
            $("#sec_electric").append(array[0]);
            $("#sec_crew").append(array[1]);


        }
    }, "html");
        }
        function change_electric() {
            var par = $("#sec_electric").find("option:selected").val();
            $("#sec_crew").empty();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        electric_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    array = data.split('|');
                    $("#sec_crew").empty();
                    if (data == "") {
                        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
                    }
                    else {
                        $("#sec_crew").append(array[0]);
                    }
                },

                "html");
        }

        function change_crew() {
            var par = $("#sec_crew").find("option:selected").val();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        crew_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    if (data == "") {
                    }
                    else {
                    }
                },

                "html");
        }

        function change_time() {
            var time_style = $("#sec_time").val();
            if (time_style == "1") {
                $("#etime").attr("style", "display:block");
                $("#stime").attr("value", "");
                $("#etime").attr("value", "");
                $("#stime").attr("onfocus", "WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})");
                $("#etime").attr("onfocus", "WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            }
            else if (time_style == "2") {
                $("#stime").attr("value", "");
                $("#etime").attr("style", "display:none");
                $("#stime").attr("onfocus", "WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd',maxDate:'%y-%M-%d'})");
            }
            else if (time_style == "3") {
                $("#stime").attr("value", "");
                $("#etime").attr("style", "display:none");
                $("#stime").attr("onfocus", "WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM'})");
            }
            else if (time_style == "4") {
                $("#stime").attr("value", "");
                $("#etime").attr("style", "display:none");
                $("#stime").attr("onfocus", "WdatePicker({skin:'whyGreen',dateFmt:'yyyy'})");
            }
        }

        function aa() {
            var rating = "", flag = false;
            rating += $("#sec_crew").find("option:selected").val() + ";";
            if ($('input:checkbox[id="ckb_60"]').is(':checked')) {
                rating += (60 - parseFloat($("#sec_per").find("option:selected").val())) + "|" + (60 + parseFloat($("#sec_per").find("option:selected").val())) + ",";
                flag = true;
            }
            if ($('input:checkbox[id="ckb_70"]').is(':checked')) {
                rating += (70 - parseFloat($("#sec_per").find("option:selected").val())) + "|" + (70 + parseFloat($("#sec_per").find("option:selected").val())) + ",";
                flag = true;
            }
            if ($('input:checkbox[id="ckb_80"]').is(':checked')) {
                rating += (80 - parseFloat($("#sec_per").find("option:selected").val())) + "|" + (80 + parseFloat($("#sec_per").find("option:selected").val())) + ",";
                flag = true;
            }
            if ($('input:checkbox[id="ckb_90"]').is(':checked')) {
                rating += (90 - parseFloat($("#sec_per").find("option:selected").val())) + "|" + (90 + parseFloat($("#sec_per").find("option:selected").val())) + ",";
                flag = true;
            }
            if ($('input:checkbox[id="ckb_100"]').is(':checked')) {
                if (($("#txt_xiaxian").val() == "") || ($("#txt_shangxian").val() == "") || (parseInt($("#txt_xiaxian").val()) > 100) || (parseInt($("#txt_xiaxian").val()) > 100) || (parseInt($("#txt_xiaxian").val()) < 40) || (parseInt($("#txt_shangxian").val()) < 40) || (parseInt($("#txt_xiaxian").val()) > parseInt($("#txt_shangxian").val()))) {
                    alert("输入的负荷不能为空，且应该大于40小于100！");
                    flag = false;
                }
                else {
                    rating += parseFloat($("#txt_xiaxian").val()) + "|" + parseFloat($("#txt_shangxian").val()) + ",";
                    flag = true;
                }

            }
            rating += ";";
            if (flag == true) {

                if ($("#sec_time").find("option:selected").val() == "1") {
                    //alert("aa");
                    if (($("#stime").val() != "") && ($("#etime").val() != "") && (flag == true)) {
                        var sTime = new Date($("#stime").val().replace(/-/g, "/")); //开始时间
                        var eTime = new Date($("#etime").val().replace(/-/g, "/")); //结束时间
                        if (parseInt((eTime.getTime() - sTime.getTime()) / parseInt(1000 * 3600)) > 1) {
                            rating += $("#stime").val() + "," + $("#etime").val() + ";";
                            flag == true;
                        }
                        else {
                            alert("时间间隔过小，无法呈现曲线！");
                            flag = false;
                        }
                    }
                    else if (flag == true) {
                        alert("时间不能为空！");
                        flag = false;
                    }
                }
                else if ($("#stime").val() != "") {
                    rating += $("#stime").val() + ";";
                    flag = true;
                }
                else if (flag == true) {

                    alert("时间不能为空！");
                    flag = false;
                }
                rating += $("#sec_xdata").val() + "," + $("#sec_ydata").val() + ";";
                $("input:radio").each(function () {
                    if (this.checked) {
                        if (this.id == "rd_duoxiangshi") {
                            rating += this.nextSibling.data + "," + $("#sec_duoxiangshi").find("option:selected").val() + ";";
                            flag = true;
                        }
                        else {
                            rating += this.nextSibling.data + ",;";
                            flag = true;
                        }
                    }
                });
                if (flag == true) {
                    $.post("ComparaAnalysis.aspx", { rating: rating }, function (data) {
                        //alert(data.list);
                        if (data.list == "") {
                            alert("该机组没有满足条件的曲线！");

                        }
                        else {
                            getLine(data);
                            $("#Label1").html('');
                            $("#Label2").html('');
                            $("#Label3").html('');
                            $("#Label4").html('');
                            $("#Label5").html('');
                            var num = 0;
                            for (var i = 0; i < $("input:checkbox").length; i++) {
                                if ($("input:checkbox")[i].checked == true && $("input:checkbox")[i].id == "ckb_60") {
                                    $("#Label1").html(data.gongshi[num]);
                                    num++;
                                }
                                else if ($("input:checkbox")[i].checked == true && $("input:checkbox")[i].id == "ckb_70") {
                                    $("#Label2").html(data.gongshi[num]);
                                    num++;
                                }
                                else if ($("input:checkbox")[i].checked == true && $("input:checkbox")[i].id == "ckb_80") {
                                    $("#Label3").html(data.gongshi[num]);
                                    num++;
                                }
                                else if ($("input:checkbox")[i].checked == true && $("input:checkbox")[i].id == "ckb_90") {
                                    $("#Label4").html(data.gongshi[num]);
                                    num++;
                                }
                                else if ($("input:checkbox")[i].checked == true && $("input:checkbox")[i].id == "ckb_100") {
                                    $("#Label5").html(data.gongshi[num]);
                                    num++;
                                }
                            }

                            //                        $("input:checkbox").each(function () {
                            //                            
                            //                        });
                        }


                    }, 'json');
                }
            }
            else
            { alert("请选择要拟合的负荷！"); }


        }

        function Load_Rating() {
            if ($('input:radio[id="rd_duoxiangshi"]').is(':checked')) {
                document.getElementById("sec_duoxiangshi").disabled = false; //block
            }
            else {
                document.getElementById("sec_duoxiangshi").disabled = true; //block
            }
        }

        function getLine(list) {
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
            if (list != null) {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container'
                        //,type: 'scatter'
                    },
                    title: {
                        text: list.title
                    },
                    xAxis: {
                },
                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {

            },
            plotOptions: {
                spline: {
                    lineWidth: 0.4,
                    states: {
                        hover: {
                            lineWidth: 0.5
                        }
                    },
                    marker: {
                        enabled: false
                    },
                    enableMouseTracking: false
                }
            },
            series: list.list
        });
    }
    else {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: 'spline'
            },
            title: {
                text: '趋势呈现数据图'
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: ''
                },
                min: 0
            }
        });
    }
}

function formatDate(now) {
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    return year + "-" + month + "-" + date + "&nbsp;" + hour + ":" + minute + ":" + second;
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <table width="100%" height="92%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                    &nbsp;&nbsp;数据对比分析
                </td>
            </tr>
            <tr>
                <td>
                    <table class="style5">
                        <tr>
                            <td>
                                省公司&nbsp;
                            </td>
                            <td>
                                <select id="sec_company" runat="server" onchange="change_company()">
                                </select>&nbsp;
                            </td>
                            <td>
                                电厂&nbsp;
                            </td>
                            <td>
                                <select id="sec_electric" runat="server" onchange="change_electric()">
                                </select>&nbsp;
                            </td>
                            <td>
                                机组&nbsp;
                            </td>
                            <td colspan="2">
                                <select id="sec_crew" runat="server" onchange="change_crew()">
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td rowspan="5" valign="top">
                                负荷选择
                            </td>
                            <td>
                                <input type="checkbox" id="ckb_60" />60%额定负荷 拟合公式：
                            </td>
                            <td rowspan="4">
                                请选择偏差区间<select id="sec_per">
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5" selected="selected">5</option>
                                </select>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="checkbox" id="ckb_70" />70%额定负荷 拟合公式：
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="checkbox" id="ckb_80" />80%额定负荷 拟合公式：
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="checkbox" id="ckb_90" />90%额定负荷 拟合公式：
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td rowspan="2">
                                            <input type="checkbox" id="ckb_100" />
                                        </td>
                                        <td>
                                            请选择负荷下限:<input type="input" id="txt_xiaxian" maxlength="2" style="width: 50px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            请负荷选择上限:<input type="input" id="txt_shangxian" maxlength="2" style="width: 50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                            </td>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="time_show" style="height: 50px" class="style5">
                        <tr>
                            <td>
                                时间设置&nbsp;
                            </td>
                            <td>
                                <select id="sec_time" onchange="change_time()">
                                    <option value="1">指定时间段</option>
                                    <option value="2">天</option>
                                    <option value="3">月份</option>
                                    <option value="4">季度</option>
                                    <option value="5">年份</option>
                                </select>
                            </td>
                            <td id="designation_time">
                                <input id="stime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                    type="text" onfocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})" />&nbsp;
                            </td>
                            <td>
                                <input id="etime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                    type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                数据设置
                            </td>
                            <td>
                                X轴数据
                            </td>
                            <td>
                                <select id="sec_xdata" onchange="change_xdata()">
                                    <option>锅炉效率</option>
                                    <option selected="selected" value="q_fd">汽轮机热耗率</option>
                                    <option>制粉出力</option>
                                    <option>凝汽器压力</option>
                                    <option>机组负荷</option>
                                    <option>供电煤耗</option>
                                </select>
                            </td>
                            <td>
                                Y轴数据
                            </td>
                            <td>
                                <select id="sec_ydata">
                                    <option selected="selected" value="Eta_H">高压缸效率</option>
                                    <option value="Eta_M">中压缸效率</option>
                                    <option value="DeltaT_gl">凝汽器过冷度</option>
                                    <option value="Pdp">真空（绝对值）</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                曲线类型
                            </td>
                            <td>
                                <input type="radio" id="rd_xianxing" name="gongshi" onclick="Load_Rating()" />线性
                            </td>
                            <td>
                                <input type="radio" id="rd_zhishu" name="gongshi" onclick="Load_Rating()" />指数
                            </td>
                            <td>
                                <input type="radio" id="rd_duishu" name="gongshi" onclick="Load_Rating()" />对数
                            </td>
                            <td>
                                <input type="radio" id="rd_mi" name="gongshi" onclick="Load_Rating()" />幂
                            </td>
                            <td>
                                <input type="radio" id="rd_duoxiangshi" name="gongshi" checked="checked" onclick="Load_Rating()" />多项式
                            </td>
                            <td>
                                <select id="sec_duoxiangshi">
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="margin-left: 30%">
            <input id="btn_search" type="button" value="对比分析查询" onclick="aa()" />
        </div>
        <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
        </div>
    </div>
    </form>
</body>
</html>
