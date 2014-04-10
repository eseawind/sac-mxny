<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComparaAnalysisNew.aspx.cs"
    Inherits="SACSIS.Trend.ComparaAnalysisNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>拟合趋势</title>
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jQueryEasyUI/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" defer="defer">



        $(function () {
            /*初始化页面*/
            Init();

            /**/
            change();
        });

        function Init() {
            $.post("ComparaAnalysisNew.aspx", { param: 'Init' }, function (data) {

                var company = data.company;
                if (company != null) {
                    $("#sec_company").empty();
                    for (var i = 0; i < company.length; i++) {
                        $("#sec_company").append("<option value='" + company[i].T_LEVEL1 + "'>" + company[i].T_LEVEL1 + "</option>");
                    }
                }

                var x = data.x;
                if (x != null) {
                    $("#sec_xdata").empty();
                    for (var i = 0; i < x.length; i++) {
                        $("#sec_xdata").append("<option value='" + x[i].T_PARAID + "'>" + x[i].T_PARADESC + "</option>");
                    }
                }

                var y = data.y;
                if (y != null) {
                    $("#sec_ydata").empty();
                    for (var i = 0; i < y.length; i++) {
                        $("#sec_ydata").append("<option value='" + y[i].T_PARAID + "'>" + y[i].T_PARADESC + "</option>");
                    }
                }
            }, 'json');
        }

        function change() {
            /*公司*/
            $("#sec_company").change(function () {
                $.post("ComparaAnalysisNew.aspx", { param: 'unit', id: escape($("#sec_company").val()) }, function (data) {

                    var x = data.x;
                    if (x != null) {
                        $("#sec_xdata").empty();
                        for (var i = 0; i < x.length; i++) {
                            $("#sec_xdata").append("<option value='" + x[i].T_PARAID + "'>" + x[i].T_PARADESC + "</option>");
                        }
                    }

                    var y = data.y;
                    if (y != null) {
                        $("#sec_ydata").empty();
                        for (var i = 0; i < y.length; i++) {
                            $("#sec_ydata").append("<option value='" + y[i].T_PARAID + "'>" + y[i].T_PARADESC + "</option>");
                        }
                    }
                }, 'json');
            });


            /*X*/
            $("#sec_xdata").change(function () {

                $("#sec_ydata").empty();

                $.post("ComparaAnalysisNew.aspx", { param: 'X', id: escape($("#sec_xdata").val()) }, function (data) {

                    var y = data.y;
                    if (y != null) {
                        for (var i = 0; i < y.length; i++) {
                            $("#sec_ydata").append("<option value='" + y[i].T_PARAID + "'>" + y[i].T_PARADESC + "</option>");
                        }
                    }
                }, 'json');
            });

        }

        function Load_Rating() {
            if ($('input:radio[id="rd_duoxiangshi"]').is(':checked')) {
                document.getElementById("sec_duoxiangshi").disabled = false; //block
            }
            else {
                document.getElementById("sec_duoxiangshi").disabled = true; //block
            }
        }


        function aa() {
            var rating = "", flag = false;

            rating += $("#sec_company").val() + ";";
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
                    $.post("ComparaAnalysisNew.aspx", { param: 'line', id: rating }, function (data) {

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
                        }

                    }, 'json');
                }
            }
            else
            { alert("请选择要拟合的负荷！"); }


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
                tooltip: {},
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
    }
    </script>
</head>
<body>
    <%--    <div id="menu">
        <table width="100%" height="92%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                    &nbsp;&nbsp;数据对比分析
                </td>
            </tr>
            <tr>--%>
    <div id="dv_body">
        <div id="dv_privnte">
            <span id="sp_company">&nbsp;机&nbsp;&nbsp;&nbsp;&nbsp;组&nbsp;&nbsp;</span>
            <select id="sec_company" style="width: 120px; text-align: center;">
            </select>
        </div>
        <div>
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
                </tr>
            </table>
        </div>
        <div>
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
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        数据设置
                    </td>
                    <td>
                        X轴数据
                    </td>
                    <td>
                        <%--                     <select id="sec_xdata" onchange="change_xdata()">
                            <option>锅炉效率</option>
                            <option selected="selected" value="q_fd">汽轮机热耗率</option>
                            <option>制粉出力</option>
                            <option>凝汽器压力</option>
                            <option>机组负荷</option>
                            <option>供电煤耗</option>
                        </select>--%>
                        <select id="sec_xdata" style="width: 120px; text-align: center;">
                        </select>
                    </td>
                    <td>
                        Y轴数据
                    </td>
                    <td>
                        <%-- <select id="sec_ydata">
                            <option selected="selected" value="Eta_H">高压缸效率</option>
                            <option value="Eta_M">中压缸效率</option>
                            <option value="DeltaT_gl">凝汽器过冷度</option>
                            <option value="Pdp">真空（绝对值）</option>
                        </select>--%>
                        <select id="sec_ydata" style="width: 120px; text-align: center;">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div>
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
        </div>
    </div>
    <div style="margin-left: 30%">
        <input id="btn_search" type="button" value="对比分析查询" onclick="aa()" />
    </div>
    <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
    </div>
    <%--  </tr> </table> </div>--%>
</body>
</html>
